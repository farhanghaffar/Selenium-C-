using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AventStack.ExtentReports;
using AventStack.ExtentReports.MarkupUtils;
using DataDrivenProject;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MRO.ROI.Automation;
using MRO.ROI.Automation.Pages;
using MRO.ROI.Automation.Selenium;
using MRO.ROI.Test.Utilities;
using OpenQA.Selenium.Remote;
using System.IO;
using System.Threading;



namespace MRO.ROI.Test.RegressionTests.MROROITests
{

    [TestClass]
    public class LocationBasedInvoicing : ROIBaseTest
    {
        public LocationBasedInvoicing() : base(ROITestArea.ROITestFacility)
        {
        }

        [TestMethod]
        [TestCategory(ROITestCategory.Regression)]
        //Converted manual test case 11409-ROI-Facility--> Location Based Invoicing to automated.
        public void Reg_11409_LocationBasedInvoicing()
        {
            logger = extent.CreateTest("Reg_11409_Location Based Invoicing to automated");
            logger.Log(Status.Info, "Converted manual test case 11409-ROI-Admin-->Location Based Invoicing to automated ");
            RemoteWebDriver _driver = Init(TestContext.Properties["browser"].ToString());
            ThreadLocal<RemoteWebDriver> localDriver = new ThreadLocal<RemoteWebDriver>();
            localDriver.Value = _driver;
            RemoteWebDriver driver = localDriver.Value;
            string userRoot = System.Environment.GetEnvironmentVariable("USERPROFILE");
            string downloadFolder = Path.Combine(userRoot, "Downloads\\");
            string currentReportName = string.Empty;

            try
            {
                ROIFacilityWorkSummaryPage rOIFacilityWorkSummaryPage = new ROIFacilityWorkSummaryPage(driver, logger, TestContext);
                rOIFacilityWorkSummaryPage.GoToLogNewRequestPage();
                ROIFacilityLogNewRequestPage rOIFacilityLogNewRequestPage = new ROIFacilityLogNewRequestPage(driver, logger, TestContext);
                rOIFacilityLogNewRequestPage.ClickMRODeliveryTab();
               rOIFacilityLogNewRequestPage.MRODeliveryRequestForBostonProper();
                ROIFacilityRequestStatusPage rOIFacilityRequestStatusPage = new ROIFacilityRequestStatusPage(driver, logger, TestContext);
                string requestID = rOIFacilityRequestStatusPage.GetRequestID();
                logger.Log(Status.Pass, $"Request created with requestid ({requestID})", TakeScreenShotAtStep());
                ROIAdminHomePage rOIAdminHomePage = new ROIAdminHomePage(driver, logger, TestContext);
                rOIAdminHomePage.ROIlookupByRequestId(requestID);
                

                rOIFacilityRequestStatusPage.ImportMultiplePatientPdfFiles();
                rOIAdminHomePage.ROIlookupByRequestId(requestID);
                rOIFacilityRequestStatusPage.ReleaseRequestID();
                logger.Log(Status.Info, "Request Released");
                rOIAdminHomePage.SwitchToNewTabAndLoginROIAdmin(BaseAddress);
                ROIAdminFindaRequestPage rOIAdminFindaRequestPage=new ROIAdminFindaRequestPage(driver, logger, TestContext);
                rOIAdminFindaRequestPage.ToUPSPSRates();
                ROIAdminUPSPSRateManagementPage rOIAdminUPSPSRateManagementPage=new ROIAdminUPSPSRateManagementPage(driver, logger, TestContext);
                rOIAdminUPSPSRateManagementPage.ClickManageZonesTab();


                bool present = rOIAdminUPSPSRateManagementPage.ZoneChartCheck();
                Assert.IsTrue(present);
                logger.Log(Status.Pass, "Zone Chart is verified successully", TakeScreenShotAtStep());
                bool code= rOIAdminUPSPSRateManagementPage.ZipCodedigitcheck();
                Assert.IsTrue(code);
                logger.Log(Status.Pass, "3 digit zip codes are verified successully", TakeScreenShotAtStep());
                rOIAdminUPSPSRateManagementPage.ROIlookupByRequestId(requestID);
                ROIAdminRequestStatusPage adminRequestStatusPage = new ROIAdminRequestStatusPage(driver, logger, TestContext);
                adminRequestStatusPage.assignRequester();
                ROIAdminAssignROIRequesterPage assignROIRequesterPage = new ROIAdminAssignROIRequesterPage(driver, logger, TestContext);
                assignROIRequesterPage.AssignTestDonnaandAddShipTo();
                assignROIRequesterPage.UpadateRequester("Patient");


                ROIAdminRequestStatusPage rOIAdminRequestStatusPage = new ROIAdminRequestStatusPage(driver, logger, TestContext);
                string reAssignRequesterValue = rOIAdminRequestStatusPage.VerifyReAssignRequester();
                Assert.AreEqual(reAssignRequesterValue,"TEST- Donna");
                logger.Log(Status.Info, $"Succcessfully verified at the request status page re-assign requester ({reAssignRequesterValue})");
                string shipToValue = rOIAdminRequestStatusPage.VerifyShipTo();
                Assert.AreEqual(shipToValue,"Personal");
                logger.Log(Status.Info, $"Succcessfully verified at the request status page ship to ({shipToValue})");
                adminRequestStatusPage.ClickPassDocsQC();
                logger.Log(Status.Info, "Remaining steps are related to database so should be executed manually");
                Cleanup(driver);
            }
            catch (Exception ex)
            {

                LogException(driver, logger, ex);
            }
        }
    }
}






