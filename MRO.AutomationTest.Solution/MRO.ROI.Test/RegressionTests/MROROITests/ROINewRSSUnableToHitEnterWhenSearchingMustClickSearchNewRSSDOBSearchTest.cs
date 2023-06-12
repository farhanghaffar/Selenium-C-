using AventStack.ExtentReports;
using AventStack.ExtentReports.MarkupUtils;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MRO.ROI.Automation.Common;
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
    public class ROINewRSSUnableToHitEnterWhenSearchingMustClickSearchNewRSSDOBSearchTest : ROIBaseTest
    {
        public ROINewRSSUnableToHitEnterWhenSearchingMustClickSearchNewRSSDOBSearchTest() : base(ROITestArea.ROIAdmin)
        {
        }

        [STATestMethodAttribute]
        [TestCategory(ROITestCategory.Passed)]
        // Converted manual test case 4195-ROIAdmin-->New RSS - Unable to hit enter when searching, must click "Search" & New RSS - D.O.B Search to automated.
        public void Reg_4195_ROINewRSSUnableToHitEnterWhenSearchingMustClickSearchNewRSSDOBSearchTest()
        {
            logger = extent.CreateTest("Reg_4195_ROINewRSSUnableToHitEnterWhenSearchingMustClickSearchNewRSSDOBSearchTest");
            logger.Log(Status.Info, "Converted manual test case 4195-ROIAdmin-->New RSS - Unable to hit enter when searching, must click Search& New RSS - D.O.B Search to automated");
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
                Iframe frame = new Iframe(driver, logger, TestContext);
                adminHomePage.SelectFacilityList();
                ROIAdminFacilityListPage rOIAdminFacilityListPage = new ROIAdminFacilityListPage(driver, logger, TestContext);
                rOIAdminFacilityListPage.GotoROITestFacilityComputerIcon();

                ROIFacilityWorkSummaryPage rOIFacilityWorkSummaryPage = new ROIFacilityWorkSummaryPage(driver, logger, TestContext);
                rOIFacilityWorkSummaryPage.logaNewRequest();
                ROIFacilityLogNewRequestPage rOIFacilityLogNewRequestPage = new ROIFacilityLogNewRequestPage(driver, logger, TestContext);
                frame.SwitchToRoiFrame();
                rOIFacilityLogNewRequestPage.ClickMRODeliveryTab();
                rOIFacilityLogNewRequestPage.MRODeliveryRequestForBostonProper();

                LogNewRequestPage logNewRequestPage = new LogNewRequestPage(driver, logger, TestContext);

                string requestId = logNewRequestPage.getRequestid();
                ROIFacilityRequestStatusPage rOIFacilityRequestStatusPage = new ROIFacilityRequestStatusPage(driver, logger, TestContext);
                logger.Log(Status.Info, $"MRO delivery request created with id:({requestId})", TakeScreenShotAtStep());
                adminHomePage.ROIlookupByRequestId(requestId);
                frame.SwitchToRoiFrame();
                rOIFacilityRequestStatusPage.ImportPdfFiles();
                rOIFacilityRequestStatusPage.ReleaseRequestID();
                logger.Log(Status.Info, $"Request released");
                frame.switchToDefaut();
                LoginPage login = new LoginPage(driver, logger, TestContext);
                login.LogOut();
                adminHomePage.ClickOnRequesterServiceRequestStatus();

                adminHomePage.SearchRequest(requestId);

                ROIAdminRequestStatusProcessingPage rOIAdminRequestStatusProcessingPage = new ROIAdminRequestStatusProcessingPage(driver, logger, TestContext);
                string header=rOIAdminRequestStatusProcessingPage.VerifyHeader();
                Assert.AreEqual(header, "Request Status: Processing", "Failed to verify header");
                logger.Log(Status.Pass, "Verified Request Status Proccessing page opened and patient information visible", TakeScreenShotAtStep());
                string dob=rOIAdminRequestStatusProcessingPage.GetDOB();

                rOIAdminRequestStatusProcessingPage.ClickOnRSSRequestStatus();
                logger.Log(Status.Info, "Verified find request page loaded", TakeScreenShotAtStep());

                logger.Log(Status.Info, $"Verifying {dob} search return data");
                rOIAdminRequestStatusProcessingPage.SearchRequestBasedOnDOB(dob);
                rOIAdminRequestStatusProcessingPage.VerifyDOBSearchReturnsData();
                logger.Log(Status.Pass, "Verified dob search returns data", TakeScreenShotAtStep());
                rOIAdminRequestStatusProcessingPage.Logout();               
                Cleanup(driver);
                
            }

            catch (Exception ex)
            {
                LogException(driver, logger, ex);

            }
        }
    }

}

