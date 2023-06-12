using AventStack.ExtentReports;
using DataDrivenProject;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MRO.ROI.Automation.Pages;
using MRO.ROI.Test.Utilities;
using OpenQA.Selenium.Remote;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MRO.ROI.Test.RegressionTests.MROROITests
{
    [TestClass]
   public class ROIAdminShipmentsReportAddFetchedUserIDTest : ROIBaseTest
    {
        public ROIAdminShipmentsReportAddFetchedUserIDTest() : base(ROITestArea.ROIAdmin)
        {
        }
        [TestMethod]
        [TestCategory(ROITestCategory.Regression)]
        //Converted manual test case-ROI-Admin-->Shipments Report Add Fetched User ID to automated

        public void Reg_12472_Shipments_Report_Add_Fetched_User_ID()
        {
            logger = extent.CreateTest("Reg_12472_Shipments_Report_Add_Fetched_User_ID");
            logger.Log(Status.Info, "Converted manual test case-ROI-Admin-->Shipments Report Add Fetched User ID to automated");
            RemoteWebDriver _driver = Init(TestContext.Properties["browser"].ToString());
            ThreadLocal<RemoteWebDriver> localDriver = new ThreadLocal<RemoteWebDriver>();
            localDriver.Value = _driver;
            RemoteWebDriver driver = localDriver.Value;
            string userRoot = System.Environment.GetEnvironmentVariable("USERPROFILE");
            string downloadFolder = Path.Combine(userRoot, "Downloads\\");

            try
            {
                ROIAdminHomePage rOIAdminHomePage = new ROIAdminHomePage(driver, logger, TestContext);

                rOIAdminHomePage.ClickOnShipmentsReports();
                ROIAdminShipmentsReportPage rOIAdminShipmentsReportPage = new ROIAdminShipmentsReportPage(driver, logger, TestContext);
                rOIAdminShipmentsReportPage.SetShipmentTypeToPaperMailedToRequesterAndCreateReport();
                rOIAdminShipmentsReportPage.VerifyEmailReportsReturned();
                logger.Log(Status.Info, "Verified only 'Email' reports are returned when selected Email", TakeScreenShotAtStep());
                string firstPaperMailedShipmentIdUI = rOIAdminShipmentsReportPage.GetFirstShipmentID();

                rOIAdminShipmentsReportPage.ClickOnExcelIcon();
                ExcelReaderFile excelReaderFile = new ExcelReaderFile();
                excelReaderFile.ConvertXLS_XLSX(downloadFolder + "roiadmin_report_shipment.xls");
                ExcelReaderFile.DeleteExistingFiles(downloadFolder, "xls", "roiadmin_report_shipment");

                string firstPaperMailedShipmentIdExcel = excelReaderFile.ReadExcelCellData(downloadFolder + "roiadmin_report_shipment.xlsx", 2, 1);
                Assert.AreEqual(firstPaperMailedShipmentIdUI, firstPaperMailedShipmentIdExcel, "Failed to validate UI shipment and excel shipment id");
                logger.Log(Status.Info, $"Verified excel first shipment id ({firstPaperMailedShipmentIdExcel}) with UI first shipment id ({firstPaperMailedShipmentIdUI})", TakeScreenShotAtStep());

                string thirdPaperMailedShipmentIdUI = rOIAdminShipmentsReportPage.GetThirdShipmentID();
                string thirdPaperMailedShipmentIdExcel = excelReaderFile.ReadExcelCellData(downloadFolder + "roiadmin_report_shipment.xlsx", 4, 1);
                Assert.AreEqual(thirdPaperMailedShipmentIdUI, thirdPaperMailedShipmentIdExcel, "Failed to validate UI shipment and excel shipment id");
                logger.Log(Status.Info, $"Verified excel first shipment id ({thirdPaperMailedShipmentIdUI}) with UI first shipment id ({thirdPaperMailedShipmentIdExcel})", TakeScreenShotAtStep());
                ExcelReaderFile.DeleteExistingFiles(downloadFolder, "xlsx", "roiadmin_report_shipment");

                rOIAdminShipmentsReportPage.SetShipmentTypeToCDMailedToRequesterAndCreateReport();
                rOIAdminShipmentsReportPage.VerifyCDReportsReturned();
                logger.Log(Status.Info, "Verified only 'CD' reports are returned when selected CD", TakeScreenShotAtStep());               
                string firstCDMailedShipmentIdUI = rOIAdminShipmentsReportPage.GetFirstShipmentID();
                rOIAdminShipmentsReportPage.ClickOnExcelIcon();                
                excelReaderFile.ConvertXLS_XLSX(downloadFolder + "roiadmin_report_shipment.xls");
                ExcelReaderFile.DeleteExistingFiles(downloadFolder, "xls", "roiadmin_report_shipment");
                string firstCDMailedShipmentIdExcel = excelReaderFile.ReadExcelCellData(downloadFolder + "roiadmin_report_shipment.xlsx", 2, 1);
                Assert.AreEqual(firstCDMailedShipmentIdUI, firstCDMailedShipmentIdExcel, "Failed to validate UI shipment and excel shipment id");
                logger.Log(Status.Info, $"Verified excel first shipment id ({firstCDMailedShipmentIdExcel}) with UI first shipment id ({firstCDMailedShipmentIdUI})", TakeScreenShotAtStep());

                string thirdCDMailedShipmentIdUI = rOIAdminShipmentsReportPage.GetThirdShipmentID();
                string thirdCDMailedShipmentIdExcel = excelReaderFile.ReadExcelCellData(downloadFolder + "roiadmin_report_shipment.xlsx", 4, 1);
                Assert.AreEqual(thirdCDMailedShipmentIdUI, thirdCDMailedShipmentIdExcel, "Failed to validate UI shipment and excel shipment id");
                logger.Log(Status.Info, $"Verified excel first shipment id ({thirdCDMailedShipmentIdUI}) with UI first shipment id ({thirdCDMailedShipmentIdExcel})", TakeScreenShotAtStep());
                ExcelReaderFile.DeleteExistingFiles(downloadFolder, "xlsx", "roiadmin_report_shipment");

                rOIAdminShipmentsReportPage.SetShipmentTypeToPDFEDeliveryToRequesterAndCreateReport();
                rOIAdminShipmentsReportPage.VerifyPDFReportsReturned();
                logger.Log(Status.Info, "Verified only 'PDF' reports are returned when selected PDF delivery", TakeScreenShotAtStep());                
                string firstPDFShipmentIdUI = rOIAdminShipmentsReportPage.GetFirstShipmentID();
                rOIAdminShipmentsReportPage.ClickOnExcelIcon();
                excelReaderFile.ConvertXLS_XLSX(downloadFolder + "roiadmin_report_shipment.xls");
                ExcelReaderFile.DeleteExistingFiles(downloadFolder, "xls", "roiadmin_report_shipment");
                string firstPDFShipmentIdExcel = excelReaderFile.ReadExcelCellData(downloadFolder + "roiadmin_report_shipment.xlsx", 2, 1);
                Assert.AreEqual(firstPDFShipmentIdUI, firstPDFShipmentIdExcel, "Failed to validate UI shipment and excel shipment id");
                logger.Log(Status.Info, $"Verified excel first shipment id ({firstPDFShipmentIdExcel}) with UI first shipment id ({firstPDFShipmentIdUI})", TakeScreenShotAtStep());               

                string thirdPDFShipmentIdUI = rOIAdminShipmentsReportPage.GetThirdShipmentID();  
                string thirdPDFShipmentIdExcel = excelReaderFile.ReadExcelCellData(downloadFolder + "roiadmin_report_shipment.xlsx", 4, 1);
                Assert.AreEqual(thirdPDFShipmentIdUI, thirdPDFShipmentIdExcel, "Failed to validate UI shipment and excel shipment id");
                logger.Log(Status.Info, $"Verified excel first shipment id ({thirdPDFShipmentIdExcel}) with UI first shipment id ({thirdPDFShipmentIdUI})", TakeScreenShotAtStep());
                ExcelReaderFile.DeleteExistingFiles(downloadFolder, "xlsx", "roiadmin_report_shipment");

                Cleanup(driver);
            }
            catch (Exception ex)
            {

                LogException(driver, logger, ex);
            }
        }
        }
}
