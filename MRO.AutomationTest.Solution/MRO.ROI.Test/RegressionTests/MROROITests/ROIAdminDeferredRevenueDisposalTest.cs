using AventStack.ExtentReports;
using AventStack.ExtentReports.MarkupUtils;
using DataDrivenProject;
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

namespace MRO.ROI.Test.RegressionTests.MROROITests
{
    [TestClass]
    public class ROIAdminDeferredRevenueDisposalTest : ROIBaseTest
    {
        public ROIAdminDeferredRevenueDisposalTest() : base(ROITestArea.ROIAdmin)
        {
        }
        [DoNotParallelize]
        [STATestMethodAttribute]
        [TestCategory(ROITestCategory.Regression)]
        public void Reg_10869_DeferredRevenueDisposalTest()
        {

            logger = extent.CreateTest("Reg_10869_Deferred Revenue Disposal - Deferred Revenue Rollover Report Total enhancement");
            logger.Log(Status.Info, "Converted manual test case 10869- ROI Admin-->Deferred Revenue Disposal - Deferred Revenue Rollover Report Total enhancement to  automated");

            RemoteWebDriver _driver = Init(TestContext.Properties["browser"].ToString());
            ThreadLocal<RemoteWebDriver> localDriver = new ThreadLocal<RemoteWebDriver>();
            localDriver.Value = _driver;
            RemoteWebDriver driver = localDriver.Value;
            string userRoot = System.Environment.GetEnvironmentVariable("USERPROFILE");
            string downloadFolder = Path.Combine(userRoot, "Downloads\\");
            try
            {
                //For stability deleting any .xlsx files with name roiadmin_report_shipment
                ExcelReaderFile.DeleteExistingFiles(downloadFolder, "xlsx", "roiadmin_DeferredRevenueRolloverReport");
                ExcelReaderFile.DeleteExistingFiles(downloadFolder, "xls", "roiadmin_DeferredRevenueRolloverReport");
                ROIAdminHomePage adminHomePage = new ROIAdminHomePage(driver, logger, TestContext);
                Iframe frame = new Iframe(driver, logger, TestContext);
                ROIMenuSelector menuSelector = new ROIMenuSelector(driver, logger, TestContext);

                adminHomePage.ClickOnDeferredRevenueRolloverReport();
                frame.SwitchToRoiFrame();
                ROIAdminDeferredRevenueRolloverReportPage rOIAdminDeferredRevenueRolloverReportPage = new ROIAdminDeferredRevenueRolloverReportPage(driver, logger, TestContext);
                
                bool isDisplayed = rOIAdminDeferredRevenueRolloverReportPage.CreateReport();
                Assert.IsTrue(isDisplayed, "Failed to verify total column is displayed");
                logger.Log(Status.Pass, "Verified total column  is displayed", TakeScreenShotAtStep());
                
                Double sumOfNotShippedLedger = rOIAdminDeferredRevenueRolloverReportPage.VerifySecondColSum();
                string SecondColTotal = rOIAdminDeferredRevenueRolloverReportPage.TotalAmountValueFromTable(1);
                Double _SecondColTotal = Convert.ToDouble(SecondColTotal);
                Assert.AreEqual(sumOfNotShippedLedger, _SecondColTotal);
                logger.Log(Status.Pass, $"Verfied not shipped ledger during period all rows sum amount ({sumOfNotShippedLedger}) with total amount ({_SecondColTotal})", TakeScreenShotAtStep());

                Double sumOfShippedDuringPeriod = rOIAdminDeferredRevenueRolloverReportPage.VerifyThirdColSum();
                string thirdColTotal = rOIAdminDeferredRevenueRolloverReportPage.TotalAmountValueFromTable(2);
                Double _thirdColTotal = Convert.ToDouble(thirdColTotal);
                Assert.AreEqual(sumOfShippedDuringPeriod, _thirdColTotal);
                logger.Log(Status.Pass, $"Verfied shipping during period ledger before period all rows sum amount ({sumOfShippedDuringPeriod}) with total amount ({_thirdColTotal})", TakeScreenShotAtStep());


                Double sumOfNonARLedger = rOIAdminDeferredRevenueRolloverReportPage.VerifyFourthColSum();
                string fourthColTotal = rOIAdminDeferredRevenueRolloverReportPage.TotalAmountValueFromTable(3);
                Double _fourthColTotal = Convert.ToDouble(fourthColTotal);
                Assert.AreEqual(sumOfNonARLedger, _fourthColTotal);
                logger.Log(Status.Pass, $"Verfied non-ar ledger before period all rows sum  amount ({sumOfNonARLedger}) with total amount ({_fourthColTotal}) ", TakeScreenShotAtStep());

                logger.Log(Status.Info, "Click export icon");
                rOIAdminDeferredRevenueRolloverReportPage.ClickOnExportIcon();

                ExcelReaderFile excelReaderFile = new ExcelReaderFile();
                excelReaderFile.ConvertXLS_XLSX(downloadFolder + "roiadmin_DeferredRevenueRolloverReport.xls");
                ExcelReaderFile.DeleteExistingFiles(downloadFolder, "xls", "roiadmin_DeferredRevenueRolloverReport");
                driver.Wait(TimeSpan.FromSeconds(5));


                logger.Log(Status.Info, $"Verifying excelsheet data with ui deferred report table data");
                string tableFirstRowBalanceDate = rOIAdminDeferredRevenueRolloverReportPage.ReturnColNameFromTable(1);
                string excelFirstRowBalanceDate = excelReaderFile.ReadExcelCellData(downloadFolder + "roiadmin_DeferredRevenueRolloverReport.xlsx", 1, 1);
                Assert.AreEqual(tableFirstRowBalanceDate, excelFirstRowBalanceDate, "Failed to validate excel balance  start date with ui table balance  start date" + " Expected: " + tableFirstRowBalanceDate + " Actual: "+ excelFirstRowBalanceDate);
                logger.Log(Status.Pass, $"Verified excel balance  start date ({excelFirstRowBalanceDate}) with ui deferred report table  balance start date ({tableFirstRowBalanceDate })", TakeScreenShotAtStep());

                string tableSecondRowBalanceDate = rOIAdminDeferredRevenueRolloverReportPage.ReturnColNameFromTable(2);
                string excelSecondRowBalanceDate = excelReaderFile.ReadExcelCellData(downloadFolder + "roiadmin_DeferredRevenueRolloverReport.xlsx", 2, 1);
                Assert.AreEqual(tableSecondRowBalanceDate, excelSecondRowBalanceDate, "Failed to validate excel balance end date with ui table  balance end date" + " Expected: " + tableSecondRowBalanceDate + " Actual: " + excelSecondRowBalanceDate);
                logger.Log(Status.Pass, $"Verified excel balance end date ({excelSecondRowBalanceDate}) with ui deferred report table  balance end date ({tableSecondRowBalanceDate })", TakeScreenShotAtStep());

                ExcelReaderFile _excelReaderFile1 = new ExcelReaderFile(downloadFolder + "roiadmin_DeferredRevenueRolloverReport.xlsx");
                int excelColCount = _excelReaderFile1.getColumnCount("roiadmin_DeferredRevenueRollove");
                int tableColCount = rOIAdminDeferredRevenueRolloverReportPage.GetTotalColumnCountFromCreateReportTable();
                Assert.AreEqual(excelColCount, tableColCount, "Failed to validate excel column count with ui table column count" + " Expected: " + tableColCount + " Actual: " + excelColCount);
                logger.Log(Status.Pass, $"Verified excel  total column count ({excelColCount}) with ui deferred report table total column count ({tableColCount})", TakeScreenShotAtStep());


                string excelTotalSecondColVal = excelReaderFile.ReadExcelCellData(downloadFolder + "roiadmin_DeferredRevenueRolloverReport.xlsx", 13, 2);
                string tableSecondColTotal = rOIAdminDeferredRevenueRolloverReportPage.TotalAmountValueFromTable(1);
                Assert.AreEqual(tableSecondColTotal, excelTotalSecondColVal, "Failed to validate excel totalSecondCol value with ui deferred report table totalSecondCol value" + " Expected: " + tableSecondColTotal + " Actual: " + excelTotalSecondColVal);
                logger.Log(Status.Pass, $"Verified excel total amount of not shipped ledger during period ({excelTotalSecondColVal}) with ui deferred report table of not shipped ledger during period total ({tableSecondColTotal })", TakeScreenShotAtStep());

                string excelTotalThirdColVal = excelReaderFile.ReadExcelCellData(downloadFolder + "roiadmin_DeferredRevenueRolloverReport.xlsx", 13, 3);
                double _excelTotalThirdColVal = Convert.ToDouble(excelTotalThirdColVal);
                string tableThirdColTotal = rOIAdminDeferredRevenueRolloverReportPage.TotalAmountValueFromTable(2);
                double _tableThirdColTotal = Convert.ToDouble(tableThirdColTotal);
                //_tableThirdColTotal = Math.Round(_tableThirdColTotal);
                Assert.AreEqual(_tableThirdColTotal, _excelTotalThirdColVal, "Failed to validate excel first row value  with ui table  " + " Expected: " + tableThirdColTotal + " Actual: " + excelTotalThirdColVal);
                logger.Log(Status.Pass, $"Verified excel total amount of shipped during period ({excelTotalThirdColVal}) with ui deferred report table of shipped during period ({tableThirdColTotal })", TakeScreenShotAtStep());

                string excelTotalFourthColVal = excelReaderFile.ReadExcelCellData(downloadFolder + "roiadmin_DeferredRevenueRolloverReport.xlsx", 13, 4);
                string tableFourthColTotal = rOIAdminDeferredRevenueRolloverReportPage.TotalAmountValueFromTable(3);
                Assert.AreEqual(tableFourthColTotal, excelTotalFourthColVal, "Failed to validate excel totalFourthCol value with ui deferred report table totalFourthCol value" + " Expected: " + tableFourthColTotal + " Actual: " + excelTotalFourthColVal);
                logger.Log(Status.Pass, $"Verified excel total amount of non-ar ledger before period ({excelTotalFourthColVal}) with ui deferred report table of non-ar ledger before period ({tableFourthColTotal })", TakeScreenShotAtStep());

                string excelTotalSixthColVal = excelReaderFile.ReadExcelCellData(downloadFolder + "roiadmin_DeferredRevenueRolloverReport.xlsx", 13, 6);
                string tableSixthColTotal = rOIAdminDeferredRevenueRolloverReportPage.TotalAmountValueFromTable(5);
                Assert.AreEqual(tableSixthColTotal, excelTotalSixthColVal, "Failed to validate excel totalSixthCol value with ui deferred report table totalSixthCol value" + " Expected: " + tableSixthColTotal + " Actual: " + excelTotalSixthColVal);
                logger.Log(Status.Pass, $"Verified excel total amount ({excelTotalSixthColVal}) with ui deferred report table total amount ({tableSixthColTotal })", TakeScreenShotAtStep());

                ////Deleting excel file for stability
                ExcelReaderFile.DeleteExistingFiles(downloadFolder, "xlsx", "roiadmin_DeferredRevenueRolloverReport");
                ////Downloading hyperlink for paymenylink
                rOIAdminDeferredRevenueRolloverReportPage.ClickOnExcelIcon();
                ExcelReaderFile excelReaderFile1 = new ExcelReaderFile();
                excelReaderFile1.ConvertXLS_XLSX(downloadFolder + "roiadmin_DeferredRevenueRolloverReport.xls");
                ExcelReaderFile.DeleteExistingFiles(downloadFolder, "xls", "roiadmin_DeferredRevenueRolloverReport");

                ExcelReaderFile _excelReaderFile2 = new ExcelReaderFile(downloadFolder + "roiadmin_DeferredRevenueRolloverReport.xlsx");
                int excelRowcount = _excelReaderFile2.getRowCount("roiadmin_DeferredRevenueRollove");
                int _tableRowCount = rOIAdminDeferredRevenueRolloverReportPage.GetTotalRowCountFromPaymentsTotaltHyperlink(); 
                double excelFirstRowAmount = Convert.ToDouble(_excelReaderFile1.ReadExcelCellData(downloadFolder + "roiadmin_DeferredRevenueRolloverReport.xlsx", 2, 7));
                excelFirstRowAmount = Math.Round(excelFirstRowAmount, 2);
                double tableFirstRowAmount = Convert.ToDouble(rOIAdminDeferredRevenueRolloverReportPage.PaymentAmountFromTable(2));
                tableFirstRowAmount = Math.Round(tableFirstRowAmount, 2);
                Assert.AreEqual(tableFirstRowAmount, excelFirstRowAmount, "Failed to validate  excel firstRowAmount value with ui deferred report  table firstRowAmount value" + " Expected: " + tableFirstRowAmount + " Actual: " + excelFirstRowAmount);
                logger.Log(Status.Pass, $"Verified excel payment credit card amount of first row ({excelFirstRowAmount}) with ui deferred report table of payment credit card amount first row ({tableFirstRowAmount })", TakeScreenShotAtStep());

                if (excelRowcount > 2)
                {
                    double excelThirdRowAmount = Convert.ToDouble(excelReaderFile.ReadExcelCellData(downloadFolder + "roiadmin_DeferredRevenueRolloverReport.xlsx", 3, 7));
                    excelThirdRowAmount = Math.Round(excelThirdRowAmount, 2);
                    double tableThirdRowAmount = Convert.ToDouble(rOIAdminDeferredRevenueRolloverReportPage.PaymentAmountFromTable(3));
                    tableThirdRowAmount = Math.Round(tableThirdRowAmount, 2);
                    Assert.AreEqual(tableThirdRowAmount, excelThirdRowAmount, "Failed to validate  excel thirdRowAmount value with ui deferred report  table thirdRowAmount value" + " Expected: " + tableThirdRowAmount + " Actual: " + excelThirdRowAmount);
                    logger.Log(Status.Pass, $"Verified excel payment credit card  amount of third row ({excelThirdRowAmount}) with ui deferred report table of  payments third row ({tableThirdRowAmount })", TakeScreenShotAtStep());

                    double excellastRowAmount = Convert.ToDouble(excelReaderFile.ReadExcelCellData(downloadFolder + "roiadmin_DeferredRevenueRolloverReport.xlsx", _tableRowCount, 7));
                    excellastRowAmount = Math.Round(excellastRowAmount, 2);
                    double tablelastRowAmount1 = Convert.ToDouble(rOIAdminDeferredRevenueRolloverReportPage.PaymentAmountFromTable(_tableRowCount));
                    tablelastRowAmount1 = Math.Round(tablelastRowAmount1, 2);
                    Assert.AreEqual(tablelastRowAmount1, excellastRowAmount, "Failed to validate  excel thirdRowAmount value with ui deferred report  table thirdRowAmount value" + " Expected: " + tablelastRowAmount1 + " Actual: " + excellastRowAmount);
                    logger.Log(Status.Pass, $"Verified excel payment credit card  amount of last row ({excellastRowAmount}) with ui deferred report table of payment credit card last row ({tablelastRowAmount1 })", TakeScreenShotAtStep());

                }
                else
                {
                    logger.Log(Status.Info, $"Excel sheet have less than 2 rows");
                }

                ////Deleting excel file for stability
                ExcelReaderFile.DeleteExistingFiles(downloadFolder, "xlsx", "roiadmin_DeferredRevenueRolloverReport");

                logger.Log(Status.Info, $"Get total payment");
                string tablePaymentTotal = rOIAdminDeferredRevenueRolloverReportPage.GetPaymentTotal();
                logger.Log(Status.Pass, $"<b style='color: green;'>Verifying excel total amount of all row with UI deferred report table total amount ({tablePaymentTotal})</b>", TakeScreenShotAtStep());

                logger.Log(Status.Info, $"Click on Payment Total hyper link with amount : {tablePaymentTotal}");
                rOIAdminDeferredRevenueRolloverReportPage.ClickOnPaymentTotalHyperLink();

                ExcelReaderFile excelReaderFile3 = new ExcelReaderFile();
                driver.SleepTheThread(10);
                excelReaderFile3.ConvertXLS_XLSX(downloadFolder + "roiadmin_DeferredRevenueRolloverReport.xls");
                ExcelReaderFile.DeleteExistingFiles(downloadFolder, "xls", "roiadmin_DeferredRevenueRolloverReport");
                ExcelReaderFile _excelReaderFile = new ExcelReaderFile(downloadFolder + "roiadmin_DeferredRevenueRolloverReport.xlsx");
                int _excelRowCount = _excelReaderFile.getRowCount("roiadmin_DeferredRevenueRollove");
                int tableRowCount = rOIAdminDeferredRevenueRolloverReportPage.GetTotalRowCountFromPaymentsTotaltHyperlink();

                double _excelFirstRowAmount = Convert.ToDouble(_excelReaderFile.ReadExcelCellData(downloadFolder + "roiadmin_DeferredRevenueRolloverReport.xlsx", 2, 7));
                _excelFirstRowAmount = Math.Round(_excelFirstRowAmount, 2);
                double _tableFirstRowAmount = Convert.ToDouble(rOIAdminDeferredRevenueRolloverReportPage.PaymentAmountFromTable(2));
                _tableFirstRowAmount = Math.Round(_tableFirstRowAmount, 2);
                Assert.AreEqual(_tableFirstRowAmount, _excelFirstRowAmount, "Failed to validate  excel SecondRowAmount value with ui deferred report  table SecondRowAmount value" + " Expected: " + _tableFirstRowAmount + " Actual: " + _excelFirstRowAmount);
                logger.Log(Status.Pass, $"Verified excel total amount of first row ({_excelFirstRowAmount}) with UI deferred report table total amount of first row ({_tableFirstRowAmount })", TakeScreenShotAtStep());

                double excelRowAmount = Convert.ToDouble(_excelReaderFile.ReadExcelCellData(downloadFolder + "roiadmin_DeferredRevenueRolloverReport.xlsx", tableRowCount, 7));
                excelRowAmount = Math.Round(excelRowAmount, 2);
                double tablelastRowAmount = Convert.ToDouble(rOIAdminDeferredRevenueRolloverReportPage.PaymentAmountFromTable(tableRowCount));
                tablelastRowAmount = Math.Round(tablelastRowAmount, 2);
                Assert.AreEqual(tablelastRowAmount, excelRowAmount, "Failed to validate  excel thirdRowAmount value with ui deferred report  table thirdRowAmount value" + " Expected: " + tablelastRowAmount + " Actual: " + excelRowAmount);
                logger.Log(Status.Pass, $"Verified excel payments total amount of last row ({excelRowAmount}) with UI deferred report table of  payments total last row ({tablelastRowAmount })", TakeScreenShotAtStep());


                double _excelTotalAmount = 0.00;
                for (int i = 2; i <= _excelRowCount; i++)
                {
                    string data = _excelReaderFile.ReadExcelCellData(downloadFolder + "roiadmin_DeferredRevenueRolloverReport.xlsx", i, 7);
                    Console.WriteLine("Data : " + data);
                    _excelTotalAmount = _excelTotalAmount + Convert.ToDouble(data);
                }
                _excelTotalAmount = Math.Round(_excelTotalAmount, 2);
                double _tablePaymentTotal = Convert.ToDouble(tablePaymentTotal);
                _tablePaymentTotal = Math.Round(_tablePaymentTotal, 2);
                Assert.AreEqual(_tablePaymentTotal, _excelTotalAmount, "Failed to validate  excel Sum of all row amount value with ui deferred report  table total amount value" + " Expected: " + _tablePaymentTotal + " Actual: " + _excelTotalAmount);
                logger.Log(Status.Pass, $"Verified excel total amount of all row ({_excelTotalAmount}) with UI deferred report table total amount  ({_tablePaymentTotal})", TakeScreenShotAtStep());

                frame.switchToDefaut();
                menuSelector.ClickLogoutIcon();
                logger.Log(Status.Info, $"Logged out of application");
                Cleanup(driver);

            }
            catch (Exception ex)
            {
                ExcelReaderFile.DeleteExistingFiles(downloadFolder, "xlsx", "roiadmin_DeferredRevenueRolloverReport");
                ExcelReaderFile.DeleteExistingFiles(downloadFolder, "xls", "roiadmin_DeferredRevenueRolloverReport");
                LogException(driver, logger, ex);

            }
        }
    }

}

