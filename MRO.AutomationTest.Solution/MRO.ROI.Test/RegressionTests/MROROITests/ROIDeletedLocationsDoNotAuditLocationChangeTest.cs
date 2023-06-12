using AventStack.ExtentReports;
using AventStack.ExtentReports.MarkupUtils;
using DataDrivenProject;
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
    public class ROIDeletedLocationsDoNotAuditLocationChangeTest : ROIBaseTest
    {
        public ROIDeletedLocationsDoNotAuditLocationChangeTest() : base(ROITestArea.ROIAdmin)
        {
        }

        [STATestMethodAttribute]
        [TestCategory(ROITestCategory.Regression)]
        // Converted manual test case 14173-ROI-Admin-->Deleted locations do not audit location change to automated to automated.
        public void Reg_14173_ROIDeletedLocationsDoNotAuditLocationChangeTest()
        {
            logger = extent.CreateTest("14173_ROIDeletedLocationsDoNotAuditLocationChangeTest");
            logger.Log(Status.Info, "Converted manual test case 14173-ROI-Admin-->Deleted locations do not audit location change to automated.");
            RemoteWebDriver _driver = Init(TestContext.Properties["browser"].ToString());
            ThreadLocal<RemoteWebDriver> localDriver = new ThreadLocal<RemoteWebDriver>();
            localDriver.Value = _driver;
            RemoteWebDriver driver = localDriver.Value;
           

            try
            {
                ROIAdminHomePage rOIAdminHomePage = new ROIAdminHomePage(driver, logger, TestContext);
                rOIAdminHomePage.FacilityLocation();
                ROIFacilityLocationROITestFacilityPage rOIFacilityLocationROITestFacility = new ROIFacilityLocationROITestFacilityPage(driver, logger, TestContext);
                rOIFacilityLocationROITestFacility.AddLocation();
                string locationName = rOIFacilityLocationROITestFacility.GetLocationName();
                logger.Log(Status.Info, "Verified that location created under facility loaction list", TakeScreenShotAtStep());

                rOIAdminHomePage.OpenNewTabAndLoginROITestFacility(BaseAddress);
                ROIFacilityWorkSummaryPage rOIFacilityWorkSummaryPage = new ROIFacilityWorkSummaryPage(driver, logger, TestContext);
                rOIFacilityWorkSummaryPage.logaNewRequest();
                ROIFacilityLogNewRequestPage rOIFacilityLogNewRequestPage = new ROIFacilityLogNewRequestPage(driver, logger, TestContext);
                rOIFacilityLogNewRequestPage.MRODeliveryRequestWithNewLocation(locationName);

                LogNewRequestPage logNewRequestPage = new LogNewRequestPage(driver, logger, TestContext);
                string requestId = logNewRequestPage.getRequestid();
                ROIFacilityRequestStatusPage rOIFacilityRequestStatusPage = new ROIFacilityRequestStatusPage(driver, logger, TestContext);
                logger.Log(Status.Info, $"MRO delivery request created with id:({requestId})", TakeScreenShotAtStep());

                rOIAdminHomePage.SwitchToPreviousTab(BaseAddress);
                rOIAdminHomePage.FacilityLocation();
                rOIFacilityLocationROITestFacility.DeleteCreatedLocation();
                logger.Log(Status.Info, "Verified that created location deleted");
                rOIAdminHomePage.ClickAuditLog();
                logger.Log(Status.Info, "Verified that audit log page opened successfully", TakeScreenShotAtStep());

                ROIAdminAuditLogPage rOIAdminAuditLogPage = new ROIAdminAuditLogPage(driver, logger, TestContext);
                rOIAdminAuditLogPage.CreateReportForAuditLog();
                //bool isDisplayed=rOIAdminAuditLogPage.VerifyRequest(requestId);
                bool isDisplayed = rOIAdminAuditLogPage.CheckRequestIdExsist(requestId);
                Assert.IsTrue(isDisplayed, "Request id not displayed");
                string infoMsg=rOIAdminAuditLogPage.VerifyRequestInfo();
                logger.Log(Status.Pass, $"Verified that search results returns the created request id and info like :{(infoMsg)}", TakeScreenShotAtStep());
                Cleanup(driver);
            }

            catch (Exception ex)
            {
                
                LogException(driver, logger, ex);

            }
        }
    }

}

