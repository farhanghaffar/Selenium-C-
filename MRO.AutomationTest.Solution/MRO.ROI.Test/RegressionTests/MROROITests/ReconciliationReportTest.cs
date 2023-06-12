using AventStack.ExtentReports;
using DataDrivenProject;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MRO.ROI.Automation.Common;
using MRO.ROI.Automation.Pages;
using MRO.ROI.Automation.Pages.Common;
using MRO.ROI.Test.ExecutionFactory;
using MRO.ROI.Test.Utilities;
using OpenQA.Selenium.Remote;
using System;
using System.IO;
using System.Threading;

namespace MRO.ROI.Test.RegressionTests.MROROITests
{

    [TestClass]
    public class ReconciliationReportTest : ROIBaseTest
    {
        public ReconciliationReportTest() : base(ROITestArea.ROIAdmin)
        {

        }
        [STATestMethodAttribute]
        [TestCategory(ROITestCategory.Regression)]
        //Converted manual test case 9982-ROI-Admin-->MRO eXpress Reconciliation Report Details to automated
        public void Reg_9982_ReconciliationReportDetailsTest()
        {
            logger = extent.CreateTest("Reg_9982_ReconciliationReportDetailsTest");
            logger.Log(Status.Info, "Converted manual test case 9982-ROI-Admin-->MRO eXpress Reconciliation Report Details to automated");
            RemoteWebDriver _driver = Init(TestContext.Properties["browser"].ToString());
            ThreadLocal<RemoteWebDriver> localDriver = new ThreadLocal<RemoteWebDriver>();
            localDriver.Value = _driver;
            RemoteWebDriver driver = localDriver.Value;
            string userRoot = System.Environment.GetEnvironmentVariable("USERPROFILE");
            string downloadFolder = Path.Combine(userRoot, "Downloads\\");
            try
            {
                ROIAdminHomePage adminHomePage = new ROIAdminHomePage(driver, logger, TestContext);
                Iframe frame = new Iframe(driver, logger, TestContext);
                ROIMenuSelector menuSelector = new ROIMenuSelector(driver, logger, TestContext);
                ROIAdminReconciliationReportPage reconciliationReportPage = new ROIAdminReconciliationReportPage(driver, logger, TestContext);
                menuSelector.Select("Reports", "Reconciliation Report");
                frame.SwitchToRoiFrame();
                
                reconciliationReportPage.CreateNewReconciliationReport("[All]", "[All]", true);                
                logger.Log(Status.Info, "Reconciliation report created for Facility:[All],Reporting Group:[None],IncludeTest:True", TakeScreenShotAtStep());

                logger.Log(Status.Info, "Verifying all columns[eXpressFacilityID,PendingLoggingCompleted,InProcess,Released,Shipped,OpenIssuesOrActions] are visible");
                bool isAllColumnsVisible = reconciliationReportPage.IsColumnsVisibleUnderReport();
                Assert.IsTrue(isAllColumnsVisible, "Failed to validate [eXpressFacilityID,PendingLoggingCompleted,InProcess,Released,Shipped,OpenIssuesOrActions] columns names");
                logger.Log(Status.Pass, "Verified all columns[eXpressFacilityID,PendingLoggingCompleted,InProcess,Released,Shipped,OpenIssuesOrActions] are visible", TakeScreenShotAtStep());


                logger.Log(Status.Info, "Verify that a detail view will generate with the same number of requests for each number hyperlink.", TakeScreenShotAtStep());
                bool isAllRequestValidatedinDetail = reconciliationReportPage.ValidateDetailView();
                Assert.IsTrue(isAllRequestValidatedinDetail);
                logger.Log(Status.Pass, "Verified that detail view generated the same number of requests for each number hyperlink");

                ExcelReaderFile.DeleteExistingFiles(downloadFolder, "xlsx", "");
                ExcelReaderFile.DeleteExistingFiles(downloadFolder, "pdf", "");
                logger.Log(Status.Info, "Verify that the Excel file has all entries from the report.", TakeScreenShotAtStep());

                reconciliationReportPage.ClickOnExportToExcel();
                string excelFileName = downloadFolder + "MRO eXpress Reconciliation Report.xlsx";
                ExcelReaderFile excelReaderFile = new ExcelReaderFile(excelFileName);
                int lastRowNumber = excelReaderFile.getRowCount("Sheet1");
                string excelTotalRequests = excelReaderFile.ReadExcelCellData(excelFileName, lastRowNumber, 4);
                string tableTotalRequests = Convert.ToString(reconciliationReportPage.GetFullCountOfTotalRequests());
                Assert.AreEqual(tableTotalRequests, excelTotalRequests, "Failed to validate total requests column from excel with total requests from UI");
                logger.Log(Status.Pass, "Validated total requests column from excel with total requests from UI");

                string excelRequestCreated = excelReaderFile.ReadExcelCellData(excelFileName, lastRowNumber, 5);
                string tableRequestCreated = Convert.ToString(reconciliationReportPage.GetFullCountOfRequestCreated());
                Assert.AreEqual(tableRequestCreated, excelRequestCreated, "Failed to validate request created column from excel with request created from UI");
                logger.Log(Status.Pass, "Validate request created column from excel with request created from UI");

                string excelNotSubmitted = excelReaderFile.ReadExcelCellData(excelFileName, lastRowNumber, 6);
                string tableNotSubmitted = Convert.ToString(reconciliationReportPage.GetFullCountOfNotSubmitted());
                Assert.AreEqual(tableNotSubmitted, excelNotSubmitted, "Failed to validate request created column from excel with request created from UI");
                logger.Log(Status.Pass, "Validated not submitted column from excel with not submitted from UI");

                string excelPendingLoggingCompleted = excelReaderFile.ReadExcelCellData(excelFileName, lastRowNumber, 7);
                string tablePendingLoggingCompleted = Convert.ToString(reconciliationReportPage.GetFullCountOfPendingLoggingCompleted());
                Assert.AreEqual(tablePendingLoggingCompleted, excelPendingLoggingCompleted, "Failed to validate pending logging completed column from excel with pending logging completed from UI");
                logger.Log(Status.Pass, "Validated pending logging completed column from excel with pending logging completed from UI");

                string excelInProcess = excelReaderFile.ReadExcelCellData(excelFileName, lastRowNumber, 8);
                string tableInProcess = Convert.ToString(reconciliationReportPage.GetFullCountOfInprocess());
                Assert.AreEqual(tableInProcess, excelInProcess, "Failed to validate inprocess column from excel with inprocess from UI");
                logger.Log(Status.Pass, "Validated inprocess column from excel with inprocess from UI");

                string excelReleased = excelReaderFile.ReadExcelCellData(excelFileName, lastRowNumber, 9);
                string tableReleased = Convert.ToString(reconciliationReportPage.GetFullCountOfReleased());
                Assert.AreEqual(tableReleased, excelReleased, "Failed to validate released column from excel with inprocess from UI");
                logger.Log(Status.Pass, "Validated released column from excel with inprocess from UI");
;

                string excelShipped = excelReaderFile.ReadExcelCellData(excelFileName, lastRowNumber, 10);
                string tableShipped = Convert.ToString(reconciliationReportPage.GetFullCountOfShipped());
                Assert.AreEqual(tableShipped, excelShipped, "Failed to validate shipped column from excel with shipped from UI");
                logger.Log(Status.Pass, "Validated shipped column from excel with shipped from UI");


                string excelOpenIssuesOrActions = excelReaderFile.ReadExcelCellData(excelFileName, lastRowNumber, 11);
                string tableOpenIssuesOrActions = Convert.ToString(reconciliationReportPage.GetFullCountOfOpenIssuesOrActions());
                Assert.AreEqual(tableOpenIssuesOrActions, excelOpenIssuesOrActions, "Failed to validate open issues Or actions column from excel with open issues Or actions from UI");
                logger.Log(Status.Pass, "validated open issues Or actions column from excel with open issues Or actions from UI");

                string excelRequestCreationFailed = excelReaderFile.ReadExcelCellData(excelFileName, lastRowNumber, 12);
                string tableRequestCreationFailed = Convert.ToString(reconciliationReportPage.GetFullCountOfRequestCreationFailed());
                Assert.AreEqual(tableRequestCreationFailed, excelRequestCreationFailed, "Failed to validate request creation failed column from excel with request creation faileds from UI");
                logger.Log(Status.Pass, "Validated request creation failed column from excel with request creation faileds from UI");

                string excelMissingInROI = excelReaderFile.ReadExcelCellData(excelFileName, lastRowNumber, 13);
                string tableMissingInROI = Convert.ToString(reconciliationReportPage.GetFullCountOfMissingInROI());
                Assert.AreEqual(tableMissingInROI, excelMissingInROI, "Failed to validate missing in roi column from excel with missing in roi from UI");
                logger.Log(Status.Pass, "Validated missing in roi column from excel with missing in roi from UI");

                string excelTotalErrors = excelReaderFile.ReadExcelCellData(excelFileName, lastRowNumber, 14);
                string tableTotalErrors = Convert.ToString(reconciliationReportPage.GetFullCountOfTotalErrors());
                Assert.AreEqual(tableTotalErrors, excelTotalErrors, "Failed to validate total errors column from excel with total errors from UI");
                logger.Log(Status.Pass, "Validated total errors column from excel with total errors from UI");

                logger.Log(Status.Info, "Verified that the Excel file has all entries from the report");

                logger.Log(Status.Info, "Verify that the PDF file has all entries from the report.", TakeScreenShotAtStep());

                string[] pdfEntries = reconciliationReportPage.DownloadPDFAndGetReportData(downloadFolder);
                string pdfTotalRequests = pdfEntries[1];
                Assert.AreEqual(tableTotalRequests, pdfTotalRequests, "Failed to validate total requests column from pdf with total requests from UI");
                logger.Log(Status.Pass, "Validated total requests column from pdf with total requests from UI");

                string pdfRequestCreated = pdfEntries[2];
                Assert.AreEqual(tableRequestCreated, pdfRequestCreated, "Failed to validate request created column from pdf with request created from UI");
                logger.Log(Status.Pass, "Validate request created column from pdf with request created from UI");

                string pdfNotSubmitted = pdfEntries[3];
                Assert.AreEqual(tableNotSubmitted, pdfNotSubmitted, "Failed to validate not submiited column from pdf with not submiited from UI");
                logger.Log(Status.Pass, "Validated not submiited column from pdf with not submiited from UI");

                string pdfPendingLoggingCompleted = pdfEntries[4];
                Assert.AreEqual(tablePendingLoggingCompleted, pdfPendingLoggingCompleted, "Failed to validate pending logging completed column from pdf with pending logging completed from UI");
                logger.Log(Status.Pass, "Validated pending logging completed column from pdf with pending logging completed from UI");

                string pdfInProcess = pdfEntries[5];
                Assert.AreEqual(tableInProcess, pdfInProcess, "Failed to validate in process column from pdf with in process from UI");
                logger.Log(Status.Pass, "Validated in process column from pdf with in process from UI");

                string pdfReleased = pdfEntries[6];
                Assert.AreEqual(tableReleased, pdfReleased, "Failed to validate released column from pdf with released from UI");
                logger.Log(Status.Pass, "Validate released column from pdf with released from UI");

                string pdfShipped = pdfEntries[7];
                Assert.AreEqual(tableShipped, pdfShipped, "Failed to validate shipped column from pdf with shipped from UI");
                logger.Log(Status.Pass, "Validated shipped column from pdf with shipped from UI");

                string pdfOpenIssuesOrActions = pdfEntries[8];
                Assert.AreEqual(tableOpenIssuesOrActions, pdfOpenIssuesOrActions, "Failed to validate open issues or actions column from pdf with open issues or actions from UI");
                logger.Log(Status.Pass, "Validated open issues or actions column from pdf with open issues or actions from UI");

                string pdfRequestCreationFailed = pdfEntries[9];
                Assert.AreEqual(tableRequestCreationFailed, pdfRequestCreationFailed, "Failed to validate request creation failed column from pdf with request creation failed from UI");
                logger.Log(Status.Pass, "validated request creation failed column from pdf with request creation failed from UI");

                string pdfMissingInROI = pdfEntries[10];
                Assert.AreEqual(tableMissingInROI, pdfMissingInROI, "Failed to validate missing in roi column from pdf with missing in roi from UI");
                logger.Log(Status.Pass, "Validated missing in roi column from pdf with missing in roi from UI");

                string pdfTotalErrors = pdfEntries[11];
                Assert.AreEqual(tableTotalErrors, pdfTotalErrors, "Failed to validate total errors column from pdf with total errors from UI");
                logger.Log(Status.Pass, "validate total errors column from pdf with total errors from UI");

                logger.Log(Status.Info,"Verified that the PDF file has all entries from the report");

                
                frame.switchToDefaut();

                menuSelector.Select("Reports", "Reconciliation Report");
                frame.SwitchToRoiFrame();

                reconciliationReportPage.CreateNewReconciliationReport("MRO eXpress TEST", "[All]", true);
                logger.Log(Status.Info, "Filter created for MRO express test facility type", TakeScreenShotAtStep());

                frame.switchToDefaut();
                frame.SwitchToRoiFrame();
                frame.SwitchToRDFrame();
                int detailRequestsCount = reconciliationReportPage.ValidateDetailViewForTotalRequests();
                frame.switchToDefaut();
                int excelrequestsCount = reconciliationReportPage.DownloadAndValidateRequestDataFromExcel(downloadFolder);
                Assert.AreEqual(detailRequestsCount, excelrequestsCount, "Requests count from UI and requests count from excel sheet are not the same");
                logger.Log(Status.Pass, "Verified that the excel file has all entries from the report for MRO eXpress TEST facility type",TakeScreenShotAtStep());
                frame.switchToDefaut();
                reconciliationReportPage.ClickOnClearFilters();
                frame.switchToDefaut();
                menuSelector.ClickLogoutIcon();
                ExcelReaderFile.DeleteExistingFiles(downloadFolder, "xlsx", "");
                ExcelReaderFile.DeleteExistingFiles(downloadFolder, "pdf", "");
                Cleanup(driver);
            }
            catch (Exception ex)
            {
                ExcelReaderFile.DeleteExistingFiles(downloadFolder, "xlsx", "MRO eXpress Reconciliation Report.xlsx");
                ExcelReaderFile.DeleteExistingFiles(downloadFolder, "pdf", "MRO eXpress Reconciliation Report.pdf");
                LogException(driver, logger, ex);
            }
        }
    }
}
