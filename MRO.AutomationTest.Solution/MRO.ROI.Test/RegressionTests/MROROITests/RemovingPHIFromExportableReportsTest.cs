using AventStack.ExtentReports;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MRO.ROI.Automation.Pages;
using MRO.ROI.Test.ExecutionFactory;
using MRO.ROI.Test.Utilities;
using OpenQA.Selenium.Remote;
using System;
using System.IO;
using System.Threading;

namespace MRO.ROI.Test.RegressionTests.MROROITests
{
    [TestClass]
    public class RemovingPHIFromExportableReportsTest : ROIBaseTest
    {
        public RemovingPHIFromExportableReportsTest() : base(ROITestArea.ROITestFacility)
        {
        }

        [STATestMethodAttribute]
        [TestCategory(ROITestCategory.Regression)]
        //Converted manual test case 14261-ROIFacility--> Removing PHI from Exportable Reports (Facility Monthly Summary and Doc Required reports)to automated.
        public void Reg_14261_RemovingPHIFromExportableReportsTest()
        {
            logger = extent.CreateTest("Reg_14261_RemovingPHIFromExportableReportsTest");
            logger.Log(Status.Info, "Converted manual test case 14261-ROIFacility-->Removing PHI from Exportable Reports (Facility Monthly Summary and Doc Required reports) to automated");
            RemoteWebDriver _driver = Init(TestContext.Properties["browser"].ToString());
            ThreadLocal<RemoteWebDriver> localDriver = new ThreadLocal<RemoteWebDriver>();
            localDriver.Value = _driver;
            RemoteWebDriver driver = localDriver.Value;
            string userRoot = System.Environment.GetEnvironmentVariable("USERPROFILE");
            string downloadFolder = Path.Combine(userRoot, "Downloads\\");
            bool isOptionVisible = false;
            bool isColumnsVisible = false;
            bool isCheckBoxVisible = false;
            try
            {
                ROIAdminHomePage rOIAdminHomePage = new ROIAdminHomePage(driver, logger, TestContext);
                ROIMenuSelector menuSelector = new ROIMenuSelector(driver, logger, TestContext);
                menuSelector.Select("MRO Analyze", "Monthly Summary");
                ROIFacilityMonthlySummaryReportPage rOIFacilityMonthlySummaryReportPage = new ROIFacilityMonthlySummaryReportPage(driver, logger, TestContext);
                rOIFacilityMonthlySummaryReportPage.CreateMonthlySummaryReport();
                isOptionVisible = rOIFacilityMonthlySummaryReportPage.OpenDetailWindowAndValidateIncluePHIInExportOption();
                logger.Log(Status.Pass, "Verified that Include PHI in Export checkbox is visible", TakeScreenShotAtStep());
                Assert.IsFalse(isOptionVisible, "Failed to get the visibility status of the checkbox:[Include PHI in export]");
                isColumnsVisible = rOIFacilityMonthlySummaryReportPage.DownloadAndValidateColumnsFromExcel(downloadFolder);
                Assert.IsFalse(isColumnsVisible, "Failed to verify that the columns[SSN, DOB, MRN, PAN,DOS] are not visible under excel");
                logger.Log(Status.Pass, "Verified that the Excel files does not have the following columns (not visible) SSN, DOB, MRN,PAN,DOS");
                rOIFacilityMonthlySummaryReportPage.CheckIncludePHIinexportOption();
                isColumnsVisible = rOIFacilityMonthlySummaryReportPage.DownloadAndValidateColumnsFromExcel(downloadFolder);
                Assert.IsTrue(isColumnsVisible, "Failed to verify that the columns[SSN, DOB, MRN, PAN,DOS] are visible under excel");
                logger.Log(Status.Pass, "Verified that the Excel files does have the following columns (SSN, DOB, MRN, PAN,DOS");
                //
                rOIAdminHomePage.SwitchToNewTabAndLoginROIAdmin(BaseAddress);
                rOIAdminHomePage.ClickAuditLog();
                logger.Log(Status.Info, "Clicked on audit log", TakeScreenShotAtStep());
                ROIAdminAuditLogPage rOIAdminAuditLogPage = new ROIAdminAuditLogPage(driver, logger, TestContext);
                rOIAdminAuditLogPage.ApplyFiltersAndSearchData();
                logger.Log(Status.Info, "Verified that the Audit Log results captured", TakeScreenShotAtStep());
                rOIAdminHomePage.SwitchBackToROITestFacilitySide(BaseAddress);
                ROIFacilityWorkSummaryPage rOIFacilityWorkSummaryPage = new ROIFacilityWorkSummaryPage(driver, logger, TestContext);
                rOIFacilityWorkSummaryPage.ClickOnDocsRequired();
                ROIFacilityDocumentsRequiredPage rOIFacilityDocumentsRequiredPage = new ROIFacilityDocumentsRequiredPage(driver, logger, TestContext);
                rOIFacilityDocumentsRequiredPage.ClickOnAll();
                isCheckBoxVisible = rOIFacilityDocumentsRequiredPage.CheckIncludePHIinexportOption();
                rOIFacilityDocumentsRequiredPage.CreateReport("Other Billable");
                //
                isColumnsVisible = rOIFacilityDocumentsRequiredPage.isAllColumnsVisible();
                Assert.IsTrue(isColumnsVisible, "Failed to verify that the columns[Patient,DOB,MRN,PAN,DOS] are visible under excel");
                logger.Log(Status.Pass, "Verified that the report contains the following columns (Patient,DOB,MRN,PAN,DOS");

                rOIFacilityDocumentsRequiredPage.ClickOnExportToExcelIcon();
                isColumnsVisible = rOIFacilityDocumentsRequiredPage.DownloadAndValidateColumnsFromExcel(downloadFolder);
                Assert.IsFalse(isColumnsVisible, "Failed to verify that the columns[Patient, DOB, MRN, PAN,DOS] are not visible under excel");
                logger.Log(Status.Pass, "Verified that the Excel file does not have the following columns (Patient, DOB, MRN, PAN,DOS");
                //
                isCheckBoxVisible = rOIFacilityDocumentsRequiredPage.CheckIncludePHIinexportOption();
                isColumnsVisible = rOIFacilityDocumentsRequiredPage.DownloadAndValidateColumnsFromExcel(downloadFolder);
                Assert.IsTrue(isColumnsVisible, "Failed to verify that the columns[Patient, DOB, MRN, PAN,DOS] are visible under excel");
                logger.Log(Status.Pass, "Verified that the Excel file does have the following columns (Patient, DOB, MRN, PAN,DOS");

                rOIAdminHomePage.SwitchToNewTabAndLoginROIAdmin(BaseAddress);
                rOIAdminHomePage.ClickAuditLog();
                logger.Log(Status.Info, "Clicked on audit log", TakeScreenShotAtStep());
                rOIAdminAuditLogPage.ApplyFiltersAndSearchData();
                logger.Log(Status.Info, "Verified that the Audit Log results captured", TakeScreenShotAtStep());
                Cleanup(driver);
            }
            catch (Exception ex)
            {
                LogException(driver, logger, ex);
            }
        }
    }
}


