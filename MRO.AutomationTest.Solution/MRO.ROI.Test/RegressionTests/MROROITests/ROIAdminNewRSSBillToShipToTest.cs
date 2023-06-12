using AventStack.ExtentReports;
using AventStack.ExtentReports.MarkupUtils;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MRO.ROI.Automation.Pages;
using MRO.ROI.Automation.Pages.Common;
using MRO.ROI.Automation.Selenium;
using MRO.ROI.Test.ExecutionFactory;
using MRO.ROI.Test.Utilities;
using OpenQA.Selenium.Remote;
using System;
using System.IO;
using System.Threading;
using static MRO.ROI.Automation.Utility.IniFile;

namespace MRO.ROI.Test.RegressionTests.MROROITests
{
    [TestClass]
    public class ROIAdminNewRSSBillToShipToTest : ROIBaseTest
    {
        public ROIAdminNewRSSBillToShipToTest() : base(ROITestArea.ROIAdmin)
        {
        }

        [STATestMethodAttribute]
        [TestCategory(ROITestCategory.Regression)]
        // Converted manual test case 4218-ROIAdmin-->New RSS--Bill to / Ship to to automated.
        public void Reg_4218_ROIAdminNewRSSBillToShipToTest()
        {
            logger = extent.CreateTest("Reg_4218_ROIAdminNewRSSBillToShipToTest");
            logger.Log(Status.Info, "Converted manual test case 4218-ROIAdmin-->New RSS--Bill to / Ship to to automated");
            RemoteWebDriver _driver = Init(TestContext.Properties["browser"].ToString());
            ThreadLocal<RemoteWebDriver> localDriver = new ThreadLocal<RemoteWebDriver>();
            localDriver.Value = _driver;
            RemoteWebDriver driver = localDriver.Value;
            string userRoot = System.Environment.GetEnvironmentVariable("USERPROFILE");
            string downloadFolder = Path.Combine(userRoot, "Downloads\\");
            string currentReportName = string.Empty;


            try
            {

                ROIAdminHomePage adminHomePage = new ROIAdminHomePage(driver, logger, TestContext);
                adminHomePage.FacilityList();
                ROIAdminFacilityListPage rOIAdminFacilityListPage = new ROIAdminFacilityListPage(driver, logger, TestContext);
                rOIAdminFacilityListPage.GotoROITestFacilityComputerIcon();

                ROIFacilityWorkSummaryPage rOIFacilityWorkSummaryPage = new ROIFacilityWorkSummaryPage(driver, logger, TestContext);
                rOIFacilityWorkSummaryPage.GoToLogNewRequestPage();
                ROIFacilityLogNewRequestPage rOIFacilityLogNewRequestPage = new ROIFacilityLogNewRequestPage(driver, logger, TestContext);
                rOIFacilityLogNewRequestPage.ClickMRODeliveryTab();
                rOIFacilityLogNewRequestPage.MRODeliveryRequestForBostonProper();

                LogNewRequestPage logNewRequestPage = new LogNewRequestPage(driver, logger, TestContext);
                string requestId = logNewRequestPage.getRequestid();
                ROIFacilityRequestStatusPage rOIFacilityRequestStatusPage = new ROIFacilityRequestStatusPage(driver, logger, TestContext);
                logger.Log(Status.Info, $"MRO delivery request created with id:({requestId})", TakeScreenShotAtStep());
                adminHomePage.ROIlookupByRequestId(requestId);

                rOIFacilityRequestStatusPage.ImportPdfFiles();
                rOIFacilityRequestStatusPage.ReleaseRequestID();
                logger.Log(Status.Info, $"Request released");
                rOIFacilityRequestStatusPage.FacilityLogout();
                adminHomePage.SearchByRequestId(requestId);

                ROIAdminRequestStatusPage rOIAdminRequestStatusPage = new ROIAdminRequestStatusPage(driver, logger, TestContext);
                rOIAdminRequestStatusPage.assignRequester();
                ROIAdminAssignROIRequesterPage rOIAdminAssignROIRequesterPage = new ROIAdminAssignROIRequesterPage(driver, logger, TestContext);
                string errorMsg = rOIAdminAssignROIRequesterPage.AssignRequestandAddShipTo();
                Assert.AreEqual(errorMsg, "Please select a requester", "Failed to verify error message");
                logger.Log(Status.Info, "Verified please select a requester type message is displayed", TakeScreenShotAtStep());
                rOIAdminAssignROIRequesterPage.AssignTestABC();

                string reAssignRequesterValue = rOIAdminRequestStatusPage.VerifyReAssignRequester();
                Assert.AreEqual(reAssignRequesterValue, "TEST Attorney's");
                logger.Log(Status.Info, $"Succcessfully verified at the request status page re-assign requester ({reAssignRequesterValue})");

                string shipToValue = rOIAdminRequestStatusPage.VerifyShipTo();
                Assert.AreEqual(shipToValue, "TEST ABC");
                logger.Log(Status.Info, $"Succcessfully verified at the request status page ship to ({shipToValue})", TakeScreenShotAtStep());

                adminHomePage.ClickOnRequesterServiceRequestStatus();
                adminHomePage.SearchRequest(requestId);
                string _reAssignRequesterValue = adminHomePage.VerifyReAssignRequester();
                Assert.AreEqual(_reAssignRequesterValue, "TEST Attorney's");
                logger.Log(Status.Info, $"Succcessfully verified at the request status:Processing  page re-assign requester ({_reAssignRequesterValue})");

                string _shipToValue = adminHomePage.VerifyShipTo();
                Assert.AreEqual(_shipToValue, "TEST ABC");
                logger.Log(Status.Info, $"Succcessfully verified at the request status:Processing  page ship to ({_shipToValue})", TakeScreenShotAtStep());



                Cleanup(driver);

            }

            catch (Exception ex)
            {
                LogException(driver, logger, ex);

            }
        }
    }

}

