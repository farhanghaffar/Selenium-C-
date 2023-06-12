using AventStack.ExtentReports;
using AventStack.ExtentReports.MarkupUtils;
using DataDrivenProject;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MRO.ROI.Automation.Pages;
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
    public class ROIAdminRequesterListReportEnhancementTest : ROIBaseTest
    {
        public ROIAdminRequesterListReportEnhancementTest() : base(ROITestArea.ROIAdmin)
        {
        }

        [STATestMethodAttribute]
        [TestCategory(ROITestCategory.Regression)]
        //Converted manual test case 12435- ROI Admin-->Admin Requester List report enhancement Test to automated
        public void Reg_12435_ROIAdminRequesterListReportEnhancementTest()
        {
            logger = extent.CreateTest("Reg_12435_ROIAdminRequesterListReportEnhancementTest");
            logger.Log(Status.Info, "Converted manual test case 12435- ROI Admin-->Admin Requester List report enhancement Test to automated");
            RemoteWebDriver _driver = Init(TestContext.Properties["browser"].ToString());
            ThreadLocal<RemoteWebDriver> localDriver = new ThreadLocal<RemoteWebDriver>();
            localDriver.Value = _driver;
            RemoteWebDriver driver = localDriver.Value;
            string userRoot = System.Environment.GetEnvironmentVariable("USERPROFILE");
            string downloadFolder = Path.Combine(userRoot, "Downloads\\");
            try
            {


                //For stability deleting any .xlsx files with name roiadmin_requesterlist
                ExcelReaderFile.DeleteExistingFiles(downloadFolder, "xlsx", "roiadmin_requesterlist");
                ExcelReaderFile.DeleteExistingFiles(downloadFolder, "xls", "roiadmin_requesterlist");

                string roitFacility = IniHelper.ReadConfig("ROIAdminRequesterListReportEnhancementTest", "ROIT");
                string yaleFacility = IniHelper.ReadConfig("ROIAdminRequesterListReportEnhancementTest", "Yale");
                string defaultShipment1 = IniHelper.ReadConfig("ROIAdminRequesterListReportEnhancementTest", "shipmentType1");
                string defaultShipment2 = IniHelper.ReadConfig("ROIAdminRequesterListReportEnhancementTest", "shipmentType2");

                ROIAdminHomePage rOIAdminHomePage = new ROIAdminHomePage(driver, logger, TestContext);
                rOIAdminHomePage.SelectRequesterList();

                ROIRequestersListPage rOIRequestersListPage = new ROIRequestersListPage(driver, logger, TestContext);
                rOIRequestersListPage.SearchRecordForSpecificFacility(roitFacility);
                string selectedFacility=rOIRequestersListPage.VerifyROITRecords();
                logger.Log(Status.Pass, $"Verified the Requester Table loads records as per the selected roi test facility:({selectedFacility})");

                rOIRequestersListPage.SearchRecordForSpecificFacility(yaleFacility);
                string selectedFacility1 = rOIRequestersListPage.VerifyYaleFacilityRecords();
                logger.Log(Status.Pass, $"Verified the Requester Table loads records as per the selected yale test facility:({selectedFacility1})");

                rOIRequestersListPage.SelectDefaultShipmentType(defaultShipment1);
                string shipment1 = rOIRequestersListPage.VerifyDefaultShipmentRecords();
                logger.Log(Status.Pass, $"Verified the Requester Table loads records as per the selected default shipment type:({shipment1})");

                rOIRequestersListPage.SelectDefaultShipmentType(defaultShipment2);
                string shipment2 = rOIRequestersListPage.VerifyEmailDeliveryRecords();
                logger.Log(Status.Pass, $"Verified the Requester Table loads records as per the selected default shipment type:({shipment2})",TakeScreenShotAtStep());
                rOIRequestersListPage.ReturnToList();

                rOIRequestersListPage.ClickBillingOfficeDeliveryCheckbox();
                bool isSelected= rOIRequestersListPage.VerifyBillingOfcDeliveryRecors();
                Assert.IsTrue(isSelected, "Billing office delivery check box is deselected");
                logger.Log(Status.Pass, "Verified the Requester Table loads records as per the selected billing office delivery item",TakeScreenShotAtStep());
                rOIRequestersListPage.ReturnToList();

                rOIRequestersListPage.ClickBillingOfficeTargetCheckbox();
                logger.Log(Status.Info, "Verified the Requester Table loads records as per the selected  billing office target facility item", TakeScreenShotAtStep());
                rOIRequestersListPage.ReturnToList();

                rOIRequestersListPage.ClickOnExcelExportIcon();
                ExcelReaderFile _excelReaderFile = new ExcelReaderFile();
                _excelReaderFile.ConvertXLS_XLSX(downloadFolder + "roiadmin_requesterlist.xls");
                ExcelReaderFile.DeleteExistingFiles(downloadFolder, "xls", "roiadmin_requesterlist");



                ExcelReaderFile _excelReaderFile1 = new ExcelReaderFile(downloadFolder + "roiadmin_requesterlist.xlsx");

                int excelRowCount = _excelReaderFile1.getRowCount("roiadmin_requesterlist");
                int tableRowCount = rOIRequestersListPage.GetTableRowCount();
                Assert.AreEqual(excelRowCount, tableRowCount, "Failed to validate excel column count with ui table column count");
                logger.Log(Status.Pass, $"Verified excel  total row count ({excelRowCount}) with ui  report table total row count ({tableRowCount})");

                string _uiIdVal = rOIRequestersListPage.GetTableData(2);
                string _excelIdVal = _excelReaderFile1.ReadExcelCellData(downloadFolder + "roiadmin_requesterlist.xlsx", 2, 1);
                Assert.AreEqual(_uiIdVal, _excelIdVal, "Failed to validate excel first row id  with ui table id  val");
                logger.Log(Status.Info, $"Verified excel data ({_excelIdVal}) with ui  table id val ({_uiIdVal })");


                //For stability deleting any .xlsx files with name roiadmin_requesterlist
                ExcelReaderFile.DeleteExistingFiles(downloadFolder, "xlsx", "roiadmin_requesterlist");

                rOIRequestersListPage.SearchRecordForInternalFacility(roitFacility);
                bool isChecked= rOIRequestersListPage.VerifyInternalFacilityRecords();
                Assert.IsFalse(isChecked, "Validate check box selected");
                logger.Log(Status.Info, "Verified requester table loads records as per selected options with not validated records", TakeScreenShotAtStep());
                rOIRequestersListPage.ReturnToList();

                rOIRequestersListPage.ClickOnExcelExportIcon();
                ExcelReaderFile excelReaderFile = new ExcelReaderFile();
                excelReaderFile.ConvertXLS_XLSX(downloadFolder + "roiadmin_requesterlist.xls");
                ExcelReaderFile.DeleteExistingFiles(downloadFolder, "xls", "roiadmin_requesterlist");

                ExcelReaderFile excelReaderFile1 = new ExcelReaderFile(downloadFolder + "roiadmin_requesterlist.xlsx");

                int _excelRowCount = excelReaderFile1.getRowCount("roiadmin_requesterlist");
                int _tableRowCount = rOIRequestersListPage.GetTableRowCount();
                Assert.AreEqual(_excelRowCount, _tableRowCount, "Failed to validate excel column count with ui table column count");
                logger.Log(Status.Pass, $"Verified excel  total row count ({excelRowCount}) with ui   requester  table total row count ({tableRowCount})");

                string uiIdVal= rOIRequestersListPage.GetTableData(2);
                string excelIdVal = excelReaderFile1.ReadExcelCellData(downloadFolder + "roiadmin_requesterlist.xlsx", 2, 1);
                Assert.AreEqual(uiIdVal, excelIdVal, "Failed to validate excel first row id  with ui table id  val");
                logger.Log(Status.Info, $"Verified excel data ({excelIdVal}) with ui  requester table id val ({uiIdVal })");

                Cleanup(driver);
            }
            catch (Exception ex)
            {
                ExcelReaderFile.DeleteExistingFiles(downloadFolder, "xlsx", "roiadmin_requesterlist");
                ExcelReaderFile.DeleteExistingFiles(downloadFolder, "xls", "roiadmin_requesterlist");
                LogException(driver, logger, ex);

            }
        }
    }

}

