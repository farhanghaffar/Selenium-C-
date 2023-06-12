using AventStack.ExtentReports;
using DataDrivenProject;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MRO.ROI.Automation.Pages;
using MRO.ROI.Automation.Pages.Common;
using MRO.ROI.Test.ExecutionFactory;
using MRO.ROI.Test.Utilities;
using OpenQA.Selenium.Remote;
using System;
using System.IO;
using System.Threading;
using MRO.ROI.Automation.Selenium;
using OpenQA.Selenium;

namespace MRO.ROI.Test.RegressionTests.MROROITests
{
   

    [TestClass]
   
        public class UpdateReportCoversAllLogiReportsTest: ROIBaseTest
       {
        public RemoteWebDriver Driver;
        public UpdateReportCoversAllLogiReportsTest() : base(ROITestArea.ROITestFacility)
        {

        }
   
        [STATestMethodAttribute]
        [TestCategory(ROITestCategory.Development)]
        //Written by sandeep
        public void Reg_6432__UpdateReportCoversAllLogiReportsTest()
        {
            logger = extent.CreateTest("Reg_6432__UpdateReportCoversAllLogiReportsTest");
            logger.Log(Status.Info, "Converted manual test case 6432__UpdateReportCoversAllLogiReportsTest");
            RemoteWebDriver _driver = Init(TestContext.Properties["browser"].ToString());
            ThreadLocal<RemoteWebDriver> localDriver = new ThreadLocal<RemoteWebDriver>();
            localDriver.Value = _driver;
            RemoteWebDriver driver = localDriver.Value;
            try
            {
                ROIFacilityWorkSummaryPage rOIFacilityWorkSummaryPage = new ROIFacilityWorkSummaryPage(driver, logger, TestContext);
                ROIAdminHomePage rOIAdminHomePage = new ROIAdminHomePage(driver, logger, TestContext);
                ROIAdminFeaturesPage rOIAdminFeaturesPage = new ROIAdminFeaturesPage(driver, logger, TestContext);
                ROIRequestersListPage rOIRequestersListPage = new ROIRequestersListPage(driver, logger, TestContext);
                ROIAdminPackingListstPage rOIAdminPackingListstPage = new ROIAdminPackingListstPage(driver, logger, TestContext);
                ROIFacilityEnterpriseDashboardPage rOIFacilityEnterpriseDashboardPage = new ROIFacilityEnterpriseDashboardPage(driver, logger, TestContext);
                ROIAdminIssueValidationReportPage rOIAdminIssueValidationReportPage = new ROIAdminIssueValidationReportPage(driver, logger, TestContext);
                ROIAdminReconciliationReportPage rOIAdminReconciliationReportPage = new ROIAdminReconciliationReportPage(driver, logger, TestContext);
                ROIRevenueIntegrityDashboard rOIRevenueIntegrityDashboardPage = new ROIRevenueIntegrityDashboard(driver, logger, TestContext);
                ROIFacilityStaffActivityReportPage rOIFacilityStaffActivityReportPage = new ROIFacilityStaffActivityReportPage(driver, logger, TestContext);

                rOIFacilityWorkSummaryPage.GoToMROAnalyzeEnterpriseDashboard();
                rOIFacilityEnterpriseDashboardPage.CreateEnterpriseDashboardReportPage();
                logger.Log(Status.Info, "Create Enterprise Dashboard Report Page is Displayed");

               
                rOIFacilityWorkSummaryPage.GoToMROAnalyzeRevenueIntegrityDashboard();
                rOIRevenueIntegrityDashboardPage.CreateRevenueIntegrityDashboardReport();
                logger.Log(Status.Info, "Revenue Integrity Dashboard Report Page is Displayed");

                rOIFacilityWorkSummaryPage.GoToMROAnalyzeFacilityDashboard();
                rOIAdminReconciliationReportPage.ClickOnCustomize();
                logger.Log(Status.Info, "Facility Dashboard Report Page is Displayed");

                rOIFacilityWorkSummaryPage.GoToMROAnalyseSelectTurnAroundReport();
                rOIFacilityEnterpriseDashboardPage.CreateEnterpriseDashboardReport();
                rOIAdminReconciliationReportPage.ClickOnExportToPdfAndGetReportName();
                logger.Log(Status.Info, "Turn Around Report Page is Displayed");

                rOIFacilityWorkSummaryPage.GoToMROAnalyzeMonthlySummary();
                rOIRevenueIntegrityDashboardPage.CreateMonthlySummaryDashboardReport();
                logger.Log(Status.Info, "Monthly Summary Report is Displayed");

                rOIFacilityWorkSummaryPage.GoToMROAnalyzeSelectStaffActivityReport();
                rOIFacilityStaffActivityReportPage.ApplyFiltersAndCreateReport();
                logger.Log(Status.Info, "Staff Activity Report is Displayed");

                rOIFacilityWorkSummaryPage.GoToMROAnalyzeQualityAssurance();
                rOIFacilityStaffActivityReportPage.ApplyFiltersAndCreateReport();
                logger.Log(Status.Info, "Quality Assurance Report is Displayed");

                rOIFacilityWorkSummaryPage.GoToMROAnalyzeDailyStaffProductivity();
                rOIRevenueIntegrityDashboardPage.CreateRevenueIntegrityDashboardReport();
                logger.Log(Status.Info, "Daily Staff Productivity Report is Displayed");

                rOIAdminHomePage.SwitchToNewTabAndLoginROIAdmin(BaseAddress);
                logger.Log(Status.Info, "Login with Valid Admin Credentials in New Window");
                rOIAdminHomePage.SelectIssueValidationReport();
                rOIAdminIssueValidationReportPage.CreateReportForIssueValidation();
                logger.Log(Status.Info, "Create Report with Issue Validation");
                rOIAdminIssueValidationReportPage.ClickOnExportToExcel();

                rOIAdminHomePage.SwitchToNewTabAndLoginROIAdmin(BaseAddress);
                rOIAdminHomePage.SelectManageSystemHierarchy();
                logger.Log(Status.Info, "ManageSystemHierarchy Report is Displayed");
                rOIRevenueIntegrityDashboardPage.CreateRevenueIntegrityDashboardReport();

                //Need clarification on few steps

            }
            catch (Exception ex)
            {

                LogException(driver, logger, ex);
            }
        }
    }
}


