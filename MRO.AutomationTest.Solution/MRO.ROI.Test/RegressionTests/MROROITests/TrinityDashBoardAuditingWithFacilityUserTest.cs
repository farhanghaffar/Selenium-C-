using System;
using AventStack.ExtentReports;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MRO.ROI.Automation.Pages;
using MRO.ROI.Automation.Pages.Common;
using MRO.ROI.Test.ExecutionFactory;
using MRO.ROI.Test.Utilities;
using OpenQA.Selenium.Remote;
using System.Threading;

namespace MRO.ROI.Test.RegressionTests.MROROITests
{
    [TestClass]
    public class TrinityDashBoardAuditingWithFacilityUserTest : ROIBaseTest
    {
        public TrinityDashBoardAuditingWithFacilityUserTest() : base(ROITestArea.ROIAdmin)
        {
        }
        [STATestMethodAttribute]
        [TestCategory(ROITestCategory.Regression)]
        //Converted manual test case 748-ROI-Facility-->Trinity DashBoard Auditing with Facility user for MRO Analyze-Enteprise dashboard & Facility dashboard to automated
        public void Reg_748_TrinityDashBoardAuditingWithFacilityUserTest()
        {
            logger = extent.CreateTest("Reg_748_TrinityDashBoardAuditingWithFacilityUserTest");
            logger.Log(Status.Info,"Converted manual test case 748 -ROI-Facility -->Trinity DashBoard Auditing with Facility user for MRO Analyze-Enteprise dashboard & Facility dashboard to automated");
            RemoteWebDriver _driver = Init(TestContext.Properties["browser"].ToString());
            ThreadLocal<RemoteWebDriver> localDriver = new ThreadLocal<RemoteWebDriver>();
            localDriver.Value = _driver;
            RemoteWebDriver driver = localDriver.Value;
            bool isRecordFound = false;
            try
            {
                ROIAdminHomePage rOIAdminHomePage = new ROIAdminHomePage(driver, logger, TestContext);
                rOIAdminHomePage.FacilityList();
                ROIAdminFacilityListPage rOIAdminFacilityListPage = new ROIAdminFacilityListPage(driver, logger, TestContext);
                rOIAdminFacilityListPage.GotoROITestFacilityComputerIcon();
                ROIMenuSelector menuSelector = new ROIMenuSelector(driver, logger, TestContext);
                menuSelector.Select("MRO Analyze","Enterprise Dashboard");
                ROIFacilityEnterpriseDashboardPage rOIFacilityEnterpriseDashboardPage = new ROIFacilityEnterpriseDashboardPage(driver, logger, TestContext);
                rOIFacilityEnterpriseDashboardPage.ApplyFiltersAndCreateReport();
                rOIAdminHomePage.SwitchToNewTabAndLoginROIAdmin(BaseAddress);
                rOIAdminHomePage.ClickAuditLog();
                logger.Log(Status.Info, "Verified that audit log page opened successfully", TakeScreenShotAtStep());
                ROIAdminAuditLogPage rOIAdminAuditLogPage = new ROIAdminAuditLogPage(driver, logger, TestContext);
                rOIAdminAuditLogPage.ApplyFiltersAndCreateReportForAuditLog();
                logger.Log(Status.Info, "Report created with filters as Facility:ROI Test Facility,Action:Facility Activity- Report Generated,Startdate and End date:Today's Date", TakeScreenShotAtStep());
                rOIAdminHomePage.SwitchBackToROITestFacilitySide(BaseAddress);
                menuSelector.Select("MRO Analyze", "Facility Dashboard");               
                rOIAdminHomePage.SwitchToPreviousTab(BaseAddress);
                rOIAdminHomePage.ClickAuditLog();
                rOIAdminAuditLogPage.ApplyFiltersAndCreateReportForAuditLog();
                isRecordFound = rOIAdminAuditLogPage.ValidateSearchResults();
                Assert.AreEqual(true, isRecordFound, "Failed to validate the search results");
                logger.Log(Status.Pass, "Verified that search results has single entry for Facility Dashboard");
                LoginPage loginPage = new LoginPage(driver, logger, TestContext);
                loginPage.LogOut();
                Cleanup(driver);
            }
            catch (Exception ex)
            {
                LogException(driver, logger, ex);
            }

        }
    }
}


