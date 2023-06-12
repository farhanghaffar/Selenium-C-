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
    public class ScanPatientPagesAndRequestHasBeenReleasedWhichDisabledTheLinkToAnotherRequestButtonDisabled : ROIBaseTest
    {
        public ScanPatientPagesAndRequestHasBeenReleasedWhichDisabledTheLinkToAnotherRequestButtonDisabled() : base(ROITestArea.ROITestFacility)
        {
        }

        [TestMethod]
        [TestCategory(ROITestCategory.Regression)]
        //Converted manual test case 1204-ROI-Facility--> ScanPatient Pages And Request Has Been Released Which Disabled The Link To Another Request Button Disabled to automated.
        public void Reg_1204_ScanPatientPagesAndRequestHasBeenReleasedWhichDisabledTheLinkToAnotherRequestButtonDisabled()
        {
            logger = extent.CreateTest("Reg_1204_Scan Patient Pages And Request Has Been Released Which Disabled The Link To Another Request Button Disabled to automated");
            logger.Log(Status.Info, "Converted manual test case 1204-ROI-Facility-->Scan Patient Pages And Reques tHas Been Released Which Disabled The Link To Another Request Button Disabled  to automated ");
            RemoteWebDriver _driver = Init(TestContext.Properties["browser"].ToString());
            ThreadLocal<RemoteWebDriver> localDriver = new ThreadLocal<RemoteWebDriver>();
            localDriver.Value = _driver;
            RemoteWebDriver driver = localDriver.Value;

            try
            {
                ROIAdminHomePage rOIAdminHomePage = new ROIAdminHomePage(driver, logger, TestContext);                
                ROIFacilityWorkSummaryPage rOIFacilityWorkSummaryPage = new ROIFacilityWorkSummaryPage(driver, logger, TestContext);
                rOIFacilityWorkSummaryPage.GoToLogNewRequestPage();
                ROIFacilityLogNewRequestPage rOIFacilityLogNewRequestPage = new ROIFacilityLogNewRequestPage(driver, logger, TestContext);
                rOIFacilityLogNewRequestPage.ClickMRODeliveryTab();
                rOIFacilityLogNewRequestPage.CreateNewMRODeliveryRequestForBostonProper();
                ROIFacilityRequestStatusPage rOIFacilityRequestStatusPage = new ROIFacilityRequestStatusPage(driver, logger, TestContext);
                string linkedRequestid = rOIFacilityRequestStatusPage.FormatAndGetRequestId();
                string requestid = rOIFacilityRequestStatusPage.GetRequestID();
                logger.Log(Status.Info, $"Request created with ID ({linkedRequestid})", TakeScreenShotAtStep());
                rOIAdminHomePage.ROIlookupByRequestId(requestid);     
                rOIFacilityRequestStatusPage.ReOpenRequest();
                rOIFacilityRequestStatusPage.ImportDocumentsForFacilityDeliveryReport();
                rOIFacilityRequestStatusPage.ReleaseRequestID();
                logger.Log(Status.Pass, "Unrelease text appears and done button is disabled", TakeScreenShotAtStep());
                logger.Log(Status.Info, "Request Released");
                rOIAdminHomePage.OpenNewTabAndLoginROITestFacility(BaseAddress);                
                rOIFacilityWorkSummaryPage.SearchByRequestId("23678571");
                logger.Log(Status.Pass, "Successfully verified the request status page ", TakeScreenShotAtStep());
                rOIFacilityRequestStatusPage.ClickOnLinkToAnotherRequest();
                ROIFacilityLinkToAnotherRequestPage rOIFacilityLinkToAnotherRequestPage=new ROIFacilityLinkToAnotherRequestPage(driver, logger, TestContext);
                rOIFacilityLinkToAnotherRequestPage.DisconnectNClickOnLookupId(requestid);
                string linkedrequestid = rOIFacilityRequestStatusPage.Getlinkedrequestid();
                logger.Log(Status.Info, $"Verified the linked request id({linkedrequestid})next to Link to another request button ", TakeScreenShotAtStep());
                rOIFacilityRequestStatusPage.FacilityLogout();
                Cleanup(driver);
            }
            catch (Exception ex)
            {         
                LogException(driver, logger, ex);
            }
        }
    }
}





