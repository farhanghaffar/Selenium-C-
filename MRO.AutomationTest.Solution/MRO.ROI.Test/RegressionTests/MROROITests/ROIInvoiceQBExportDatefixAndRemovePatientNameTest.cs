using AventStack.ExtentReports;
using AventStack.ExtentReports.MarkupUtils;
using DataDrivenProject;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MRO.ROI.Automation;
using MRO.ROI.Automation.Pages;
using MRO.ROI.Automation.Selenium;
using MRO.ROI.Test.Utilities;
using OpenQA.Selenium.Remote;
using System;
using System.IO;
using System.Threading;


namespace MRO.ROI.Test.RegressionTests.MROROITests
{
    [TestClass]
    public class ROIInvoiceQBExportDatefixAndRemovePatientNameTest : ROIBaseTest
    {
        public ROIInvoiceQBExportDatefixAndRemovePatientNameTest() : base(ROITestArea.QbReport)
        {
        }

        [TestMethod]
        [TestCategory(ROITestCategory.Regression)]
        //Converted manual test case 11563-ROI-Admin--> ROI Invoice QB export date fix and remove patient name  to automated.
        public void Reg_11563_ROIInvoiceQBExportDatefixAndRemovePatientName()
        {
            logger = extent.CreateTest("Reg_11563_ROIInvoiceQBExportDatefixAndRemovePatientName");
            logger.Log(Status.Info, "Converted manual test case 11563-ROI-Admin--> ROI Invoice QB export date fix and remove patient name  to automated");
            RemoteWebDriver _driver = Init(TestContext.Properties["browser"].ToString());
            ThreadLocal<RemoteWebDriver> localDriver = new ThreadLocal<RemoteWebDriver>();
            localDriver.Value = _driver;
            RemoteWebDriver driver = localDriver.Value;
            string userRoot = System.Environment.GetEnvironmentVariable("USERPROFILE");
            string downloadFolder = Path.Combine(userRoot, "Downloads\\");
            string currentReportName = string.Empty;

            try
            {

                var files = Directory.GetFiles(downloadFolder, "_ROIInvoiceQB*.xlsx", SearchOption.TopDirectoryOnly);
                ROIAdminHomePage rOIAdminHomePage = new ROIAdminHomePage(driver, logger, TestContext);
                rOIAdminHomePage.ClickROIInvoice();
                ROIAdminStaffedSiteInvoicePage rOIAdminStaffedSiteInvoicePage = new ROIAdminStaffedSiteInvoicePage(driver, logger, TestContext);
                rOIAdminStaffedSiteInvoicePage.SetAndClickShowInvoice();
                logger.Log(Status.Info, "Selected month, year, and facility and clicked on show invoice button", TakeScreenShotAtStep());
                string InvoiceMonthAndYear = rOIAdminStaffedSiteInvoicePage.GetInvoiceStatementDate();
                rOIAdminStaffedSiteInvoicePage.ClickOrderStatements();
                logger.Log(Status.Info, "Clicked on order statement button", TakeScreenShotAtStep());
                ROIAdminDeferredProcessPage rOIAdminDeferredProcessPage = new ROIAdminDeferredProcessPage(driver, logger, TestContext);
                rOIAdminDeferredProcessPage.ClickReturnToROIInvoice();
                rOIAdminStaffedSiteInvoicePage.ClickDererredReportsAndRun();
                logger.Log(Status.Info, "Clicked on deferred reports and run", TakeScreenShotAtStep());
                rOIAdminDeferredProcessPage.ClickReturnToROIInvoice();
                rOIAdminStaffedSiteInvoicePage.ClickSavedExportBtn();
                logger.Log(Status.Info, "Clicked on save export button", TakeScreenShotAtStep());
                ROIAdminSavedReportsPage rOIAdminSavedReportsPage = new ROIAdminSavedReportsPage(driver, logger, TestContext);
                rOIAdminSavedReportsPage.ClickOnSavedReportLink();
                System.Threading.Thread.Sleep(5000);
                Assert.IsTrue(UnzipFiles.UnzipFile(), "Failed to unzip the file");
                logger.Log(Status.Pass, "File unzipped");
                currentReportName = rOIAdminSavedReportsPage.GetCurrentReportName();

                ExcelReaderFile excelReaderFile = new ExcelReaderFile();
                string fileName = UnzipFiles.ReadXLSFileFromDirectory(downloadFolder);
                excelReaderFile.ConvertXLS_XLSX(downloadFolder + fileName + ".xls");
                string excelMonthAndYear = excelReaderFile.ReadExcelCellData(downloadFolder + fileName + ".xlsx", 5, 10);
                Assert.AreEqual(InvoiceMonthAndYear, excelMonthAndYear, "Failed to validate excel month and year with invoice month and year");
                logger.Log(Status.Pass, $"Verified excel month and year ({excelMonthAndYear}) with invoice month and year ({InvoiceMonthAndYear})");

                var reportFiles = Directory.GetFiles(downloadFolder, "Curahealth_883*.xlsx", SearchOption.TopDirectoryOnly);
                if (reportFiles.Length > 0)
                {
                    var isPatientNameExists = excelReaderFile.SearchTextinExcelBySheetIndex(reportFiles[0], 1, "Patient Name");
                    Assert.IsFalse(isPatientNameExists);
                    logger.Log(Status.Pass, $"Verified there is no patient name in the excel sheet");
                    ExcelReaderFile.DeleteExistingFiles(downloadFolder, ".xlsx", Path.GetFileNameWithoutExtension(reportFiles[0]));
                    ExcelReaderFile.DeleteExistingFiles(downloadFolder, ".zip", Path.GetFileNameWithoutExtension(reportFiles[0]));
                    ExcelReaderFile.DeleteExistingFiles(downloadFolder, ".xls", Path.GetFileNameWithoutExtension(reportFiles[0]));
                }

                Cleanup(driver);
            }
            catch (Exception ex)
            {
                ExcelReaderFile.DeleteExistingFiles(downloadFolder, "zip", $"({ currentReportName})");
                ExcelReaderFile.DeleteExistingFiles(downloadFolder, "xls", $"({ currentReportName})");
                LogException(driver, logger, ex);
            }
        }
    }
}

