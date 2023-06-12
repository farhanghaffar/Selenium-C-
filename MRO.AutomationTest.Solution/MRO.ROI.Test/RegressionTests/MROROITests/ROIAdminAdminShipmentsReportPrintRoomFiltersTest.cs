using AventStack.ExtentReports;
using DataDrivenProject;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MRO.ROI.Automation.Common;
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
    public class ROIAdminShipmentsReportPrintRoomFiltersTest : ROIBaseTest
    {
        public ROIAdminShipmentsReportPrintRoomFiltersTest() : base(ROITestArea.ROIAdmin)
        {
        }

        [STATestMethodAttribute]
        [TestCategory(ROITestCategory.Regression)]
        // Converted manual test case 11287-ROI-Admin--> Admin Shipments Report - Print Room Filters to automated.
        public void Reg_11287_ROIAdminShipmentsReport()
        {

            logger = extent.CreateTest("Reg_11287_ROIAdminShipmentsReport");
            logger.Log(Status.Info, "Converted manual test case 11287-ROI-Admin--> Admin Shipments Report");

            RemoteWebDriver _driver = Init(TestContext.Properties["browser"].ToString());
            ThreadLocal<RemoteWebDriver> localDriver = new ThreadLocal<RemoteWebDriver>();
            localDriver.Value = _driver;
            RemoteWebDriver driver = localDriver.Value;
            string userRoot = System.Environment.GetEnvironmentVariable("USERPROFILE");
            string downloadFolder = Path.Combine(userRoot, "Downloads\\");

            try
            {
                //For stability deleting any .xlsx files with name roiadmin_report_shipment
                ExcelReaderFile.DeleteExistingFiles(downloadFolder, "xlsx", "roiadmin_report_shipment");
                ROIMenuSelector menuSelector = new ROIMenuSelector(driver, logger, TestContext);
                ROIAdminHomePage rOIAdminHomePage = new ROIAdminHomePage(driver, logger, TestContext);
                Iframe frame = new Iframe(driver, logger, TestContext);
                rOIAdminHomePage.ClickOnShipmentsReports();
                ROIAdminShipmentsReportPage rOIAdminShipmentsReportPage = new ROIAdminShipmentsReportPage(driver, logger, TestContext);
                frame.SwitchToRoiFrame();
                rOIAdminShipmentsReportPage.VerifyPrintRoomDropdown();
                logger.Log(Status.Info, "Verified print room dropdown has 'Dallas' and 'ValleyForge' values", TakeScreenShotAtStep());

                rOIAdminShipmentsReportPage.SetFilterSelectionsAndCreateReport();

                bool isNoResultShowing = rOIAdminShipmentsReportPage.IsNoDataFoundDisplaying();
                if (isNoResultShowing)
                {
                    logger.Log(Status.Pass, "Warning message 'No results found for this table!' is showing", TakeScreenShotAtStep());
                }
                else
                {
                    rOIAdminShipmentsReportPage.VerifyReturnedReports();
                    rOIAdminShipmentsReportPage.ClickOnExcelIcon();
                    ExcelReaderFile excelReaderFile = new ExcelReaderFile();
                    excelReaderFile.ConvertXLS_XLSX(downloadFolder + "roiadmin_report_shipment.xls");
                    ExcelReaderFile.DeleteExistingFiles(downloadFolder, "xls", "roiadmin_report_shipment");

                    string excelFirstRowShipmentID = excelReaderFile.ReadExcelCellData(downloadFolder + "roiadmin_report_shipment.xlsx", 2, 1);
                    string tableFirstRowShipmentID = rOIAdminShipmentsReportPage.ReturnShipmentIDFromTable(1);
                    Assert.AreEqual(tableFirstRowShipmentID, excelFirstRowShipmentID, "Failed to validate excel first row shipment id with ui table shipment id");
                    logger.Log(Status.Info, $"Verified excel shipment id of first row ({excelFirstRowShipmentID}) with UI shipment table of first row ({tableFirstRowShipmentID})", TakeScreenShotAtStep());

                    string excelTwentyFifthRowShipmentID = excelReaderFile.ReadExcelCellData(downloadFolder + "roiadmin_report_shipment.xlsx", 26, 1);
                    string tableTwentyFifthShipmentID = rOIAdminShipmentsReportPage.ReturnShipmentIDFromTable(25);
                    Assert.AreEqual(tableFirstRowShipmentID, excelFirstRowShipmentID, "Failed to validate excel twenty sixth row shipment id with ui table twenty fifth row shipment id");
                    logger.Log(Status.Info, $"Verified excel shipment id of twenty sixth row ({excelTwentyFifthRowShipmentID}) with UI shipment table of twenty fifth row ({tableTwentyFifthShipmentID})", TakeScreenShotAtStep());


                    ExcelReaderFile _excelReaderFile = new ExcelReaderFile(downloadFolder + "roiadmin_report_shipment.xlsx");
                    int excelRowCount = _excelReaderFile.getRowCount("roiadmin_report_shipment");
                    string excelTotalRowCount = (excelRowCount - 1).ToString();
                    string tableTotalRowCount = rOIAdminShipmentsReportPage.GetTotalRowCountFromShipmentTable();
                    Assert.AreEqual(tableTotalRowCount, excelTotalRowCount, "Failed to validate excel total row count and shipment table total row count");
                    logger.Log(Status.Info, $"Verified excel shipment table total row count ({excelTotalRowCount}) with UI shipment table total row count ({tableTotalRowCount})", TakeScreenShotAtStep());

                    rOIAdminShipmentsReportPage.SetShipmentTypeToCDMailedToRequesterAndCreateReport();
                    rOIAdminShipmentsReportPage.VerifyCDReportsReturned();
                    logger.Log(Status.Info, "Verified only 'CD' reports are returned when selected CD", TakeScreenShotAtStep());
                    rOIAdminShipmentsReportPage.SetShipmentTypeToPaperMailedToRequesterAndCreateReport();
                    rOIAdminShipmentsReportPage.VerifyEmailReportsReturned();
                    logger.Log(Status.Info, "Verified only 'Email' reports are returned when selected Email", TakeScreenShotAtStep());
                    rOIAdminShipmentsReportPage.SetShipmentTypeToBlankAndPrintRoomAsDallasAndCreateReport();
                    rOIAdminShipmentsReportPage.VerifyNoResultsReturned();
                    rOIAdminShipmentsReportPage.SetShipmentTypeToBlankAndPrintRoomAsValleyForgeAndCreateReport();
                    rOIAdminShipmentsReportPage.VerifyNoResultsReturned();
                    logger.Log(Status.Info, "Verified no results returned when selected 'Dallas' or 'Valley Forge' in print room dropdown", TakeScreenShotAtStep());
                    rOIAdminShipmentsReportPage.SetAnyFilters();
                    rOIAdminShipmentsReportPage.VerifyINTPDFReportsReturned();
                    logger.Log(Status.Info, "Verified only INT_PDF tesults returned when selected INT_PDF", TakeScreenShotAtStep());
                    
                    frame.switchToDefaut();
                    menuSelector.Select("Profile", "Logout");
                    logger.Log(Status.Pass, "Admin shipments report and print room filters are verified succesfully", TakeScreenShotAtStep());
                    ExcelReaderFile.DeleteExistingFiles(downloadFolder, "xlsx", "roiadmin_report_shipment");

                }
                Cleanup(driver);

            }
            catch (Exception ex)
            {
                ExcelReaderFile.DeleteExistingFiles(downloadFolder, "xlsx", "roiadmin_report_shipment");
                ExcelReaderFile.DeleteExistingFiles(downloadFolder, "xls", "roiadmin_report_shipment");
                LogException(driver, logger, ex);
            }
        }
    }
}