
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
    public class ROIFacilitiesAndLocationsRequesterExportFacilitiesAndLocationsListTest : ROIBaseTest
    {
        public ROIFacilitiesAndLocationsRequesterExportFacilitiesAndLocationsListTest() : base(ROITestArea.ROIAdmin)
        {

        }
        [STATestMethodAttribute]
        [TestCategory(ROITestCategory.Regression)]
        // Converted manual test case 6746-ROI-Admin-->Facilities and Locations - Requester Export, Facilities and Locations List - Include Closed and Disabled Bug, and Requester Location Code. Test to automated

        public void Reg_6746_ROIFacilitiesAndLocationsRequesterExportFacilitiesAndLocationsListTest()
        {
            logger = extent.CreateTest("Reg_6746_ROIFacilitiesAndLocationsRequesterExportFacilitiesAndLocationsListTest");
            logger.Log(Status.Info, "Converted manual test case 6746-ROI-Admin-->Facilities and Locations - Requester Export, Facilities and Locations List - Include Closed and Disabled Bug, and Requester Location Code. Test to automated");
            RemoteWebDriver _driver = Init(TestContext.Properties["browser"].ToString());
            ThreadLocal<RemoteWebDriver> localDriver = new ThreadLocal<RemoteWebDriver>();
            localDriver.Value = _driver;
            RemoteWebDriver driver = localDriver.Value;
            string userRoot = System.Environment.GetEnvironmentVariable("USERPROFILE");
            string downloadFolder = Path.Combine(userRoot, "Downloads\\");

            try
            {
                ExcelReaderFile.DeleteExistingFiles(downloadFolder, "xlsx", "roiadmin_facilitylocationsreport");
                ExcelReaderFile.DeleteExistingFiles(downloadFolder, "xls", "roiadmin_facilitylocationsreport");

                ROIAdminHomePage rOIAdminHomePage = new ROIAdminHomePage(driver, logger, TestContext);
                rOIAdminHomePage.SelectFeatures();
                ROIAdminFeaturesPage rOIAdminFeaturesPage = new ROIAdminFeaturesPage(driver, logger, TestContext);
                rOIAdminFeaturesPage.ClickOnFacilityLocationExport();
                rOIAdminFeaturesPage.VerifyFacilityLocationExportRemovedorNot();
                rOIAdminFeaturesPage.ClickOnCatagories();
                rOIAdminFeaturesPage.ClickOnRequesterLocationCode();
                rOIAdminFeaturesPage.VerifyRequesterLocationCodeRemovedorNot();

                rOIAdminHomePage.FacilityList();
                ROIAdminFacilityListPage rOIAdminFacilityListPage = new ROIAdminFacilityListPage(driver, logger, TestContext);
                rOIAdminFacilityListPage.ClickOnROITFacility();
                ROIFacilityInfoROITestFacilityPage rOIFacilityInfoROITestFacilityPage = new ROIFacilityInfoROITestFacilityPage(driver, logger, TestContext);
                string header = rOIFacilityInfoROITestFacilityPage.VerifyFacilityInfoPageHeader();
                Assert.AreEqual(header, "Facility Info: ROI Test Facility");
                logger.Log(Status.Pass, "Verified Facility Info page opened successfully", TakeScreenShotAtStep());

                rOIAdminHomePage.FacilityLocation();
                ROIFacilityLocationROITestFacilityPage rOIFacilityLocationROITestFacilityPage = new ROIFacilityLocationROITestFacilityPage(driver, logger, TestContext);
                string locationHeader = rOIFacilityLocationROITestFacilityPage.VerifyFacilityLocationsPageHeader();
                Assert.AreEqual(locationHeader, "Facility Locations: ROI Test Facility");
                logger.Log(Status.Pass, "Verified Facility locations for ROI Test facility window opened successfully", TakeScreenShotAtStep());

                rOIFacilityLocationROITestFacilityPage.SelectHenryFord();
                bool isDisabled = rOIFacilityLocationROITestFacilityPage.CheckRqrLocationDisabled();
                Assert.IsTrue(isDisabled, "rqr location button is enabled");
                logger.Log(Status.Pass, "Verified rqr location button is disabled", TakeScreenShotAtStep());

                rOIAdminHomePage.FacilityLocationList();
                ROIFacilityLocationReportPage rOIFacilityLocationReportPage = new ROIFacilityLocationReportPage(driver, logger, TestContext);
                rOIFacilityLocationReportPage.CreateReport();
                bool isFacilityDisplayed = rOIFacilityLocationReportPage.VerifyFacility();
                Assert.IsFalse(isFacilityDisplayed, "Facility is displayed");
                logger.Log(Status.Pass, "Verified that report returned no test facility and disable facility records", TakeScreenShotAtStep());

                rOIFacilityLocationReportPage.ClickOnExcelExportIcon();
                ExcelReaderFile excelReaderFile = new ExcelReaderFile();
                excelReaderFile.ConvertXLS_XLSX(downloadFolder + "roiadmin_facilitylocationsreport.xls");
                ExcelReaderFile.DeleteExistingFiles(downloadFolder, "xls", "roiadmin_facilitylocationsreport");

                ExcelReaderFile _excelReaderFile1 = new ExcelReaderFile(downloadFolder + "roiadmin_facilitylocationsreport.xlsx");
                int excelRowCount = _excelReaderFile1.getRowCount("roiadmin_facilitylocationsrepor") - 1;
                int tableRowCount = rOIFacilityLocationReportPage.GetRowCountFromTable();
                Assert.AreEqual(excelRowCount, tableRowCount, "Failed to validate excel column count with ui table column count");
                logger.Log(Status.Info, $"Verified excel  total rows count ({excelRowCount}) with ui report table total row count ({tableRowCount})");
                logger.Log(Status.Info, "Verified that the Excel file has all entries from the report");

                ExcelReaderFile.DeleteExistingFiles(downloadFolder, "xlsx", "roiadmin_facilitylocationsreport");


                rOIFacilityLocationReportPage.CreateReportForIncludeTest();
                bool isTestDisplayed = rOIFacilityLocationReportPage.VerifyFacility();
                ////There is a doubt here test facility are displayed
                Assert.IsTrue(isTestDisplayed, " test Facility not displayed");
                logger.Log(Status.Pass, "Verified that report returned  test facility  record", TakeScreenShotAtStep());


                rOIFacilityLocationReportPage.ClickOnExcelExportIcon();
                excelReaderFile.ConvertXLS_XLSX(downloadFolder + "roiadmin_facilitylocationsreport.xls");
                ExcelReaderFile.DeleteExistingFiles(downloadFolder, "xls", "roiadmin_facilitylocationsreport");

                ExcelReaderFile _excelReaderFile2 = new ExcelReaderFile(downloadFolder + "roiadmin_facilitylocationsreport.xlsx");
                int excelRowCount1 = _excelReaderFile2.getRowCount("roiadmin_facilitylocationsrepor") - 1;
                int tableRowCount1 = rOIFacilityLocationReportPage.GetRowCountFromTable();
                Assert.AreEqual(excelRowCount1, tableRowCount1, "Failed to validate excel column count with ui table column count");
                logger.Log(Status.Info, $"Verified excel  total rows count ({excelRowCount1}) with ui  include facility  report table total row count ({tableRowCount1})");


                rOIFacilityLocationReportPage.CreateReportForIncludeDisabletFacility();
                bool isDisabledFacilityDisplayed = rOIFacilityLocationReportPage.VerifyTestAndDisabledFacilityRecords();
                Assert.IsTrue(isDisabledFacilityDisplayed, " disabled Facility not displayed");
                logger.Log(Status.Pass, "Verified that report returned  test facility and disable facility records ", TakeScreenShotAtStep());

                ExcelReaderFile.DeleteExistingFiles(downloadFolder, "xlsx", "roiadmin_facilitylocationsreport");

                rOIFacilityLocationReportPage.ClickOnExcelExportIcon();
                excelReaderFile.ConvertXLS_XLSX(downloadFolder + "roiadmin_facilitylocationsreport.xls");
                ExcelReaderFile.DeleteExistingFiles(downloadFolder, "xls", "roiadmin_facilitylocationsreport");

                ExcelReaderFile _excelReaderFile3 = new ExcelReaderFile(downloadFolder + "roiadmin_facilitylocationsreport.xlsx");
                int excelRowCount2 = _excelReaderFile3.getRowCount("roiadmin_facilitylocationsrepor") - 1;
                int tableRowCount2 = rOIFacilityLocationReportPage.GetRowCountFromTable();
                Assert.AreEqual(excelRowCount2, tableRowCount2, "Failed to validate excel column count with ui table column count");
                logger.Log(Status.Info, $"Verified excel  total rows count ({excelRowCount2}) with ui disabled and test facility report table total row count ({tableRowCount2})");

                ExcelReaderFile.DeleteExistingFiles(downloadFolder, "xlsx", "roiadmin_facilitylocationsreport");

                rOIAdminHomePage.SelectFeatures();
                rOIAdminFeaturesPage.VerifySelectedContext();
                rOIAdminFeaturesPage.ClickOnFacilityLocationExport();
                rOIAdminFeaturesPage.CanSeeFacilityLocationRequesterExport("Add");
                logger.Log(Status.Info, "Verified Can See Facility Location Requester Export feature added to user", TakeScreenShotAtStep());
                rOIAdminFeaturesPage.ClickOnCatagories();

                rOIAdminFeaturesPage.ClickOnRequesterLocationCode();
                rOIAdminFeaturesPage.AddCanEditRequesterLocationCodes("Add");
                logger.Log(Status.Info, "Verified Can Edit Requester Location Codes feature added to user", TakeScreenShotAtStep());


                rOIAdminHomePage.FacilityLocation();
                rOIFacilityLocationROITestFacilityPage.SelectHenryFord();
                bool isDisable = rOIFacilityLocationROITestFacilityPage.CheckRqrLocationDisabled();
                //There is a doubt here, it is disabled with no value
                Assert.IsFalse(isDisable, "Disabled");
                string info = rOIFacilityLocationROITestFacilityPage.AddRqrLocationCode();
                Assert.AreEqual(info, "Location Info Updated");
                logger.Log(Status.Info, "Verified location info updated successfully");

                rOIAdminHomePage.FacilityLocationList();
                bool isVisible = rOIFacilityLocationReportPage.VerifyRequesterExportBtn();
                Assert.IsTrue(isVisible, "Requester Export Button is no visible");
                logger.Log(Status.Pass, "Verified requester export button is visible", TakeScreenShotAtStep());

                rOIFacilityLocationReportPage.CreateReportOnlyForIncludeTest();
                ExcelReaderFile.DeleteExistingFiles(downloadFolder, "xlsx", "roiadmin_facilitylocationsreport");
                rOIFacilityLocationReportPage.ClickOnRequesterExport();


                excelReaderFile.ConvertXLS_XLSX(downloadFolder + "roiadmin_facilitylocationsreport.xls");
                ExcelReaderFile.DeleteExistingFiles(downloadFolder, "xls", "roiadmin_facilitylocationsreport");


                string locationName = excelReaderFile.ReadExcelCellData(downloadFolder + "roiadmin_facilitylocationsreport.xlsx", 3, 17);
                Assert.AreEqual(locationName, "Henry Ford", "Failed to verify location name");
                string locationCode = excelReaderFile.ReadExcelCellData(downloadFolder + "roiadmin_facilitylocationsreport.xlsx", 3, 18);
                Assert.AreEqual(locationCode, "123TEST", "Failed to verify location code");
                logger.Log(Status.Pass, $"Verified location name:({locationName}) and loaction code:({locationCode})  are visible in excel file");
                ExcelReaderFile.DeleteExistingFiles(downloadFolder, "xlsx", "roiadmin_facilitylocationsreport");


                rOIAdminHomePage.FacilityLocation();
                rOIFacilityLocationROITestFacilityPage.SelectHenryFord();
                string code = rOIFacilityLocationROITestFacilityPage.ClearRqrLocationCode();
                rOIAdminHomePage.FacilityLocationList();
                bool isVisible1 = rOIFacilityLocationReportPage.VerifyRequesterExportBtn();
                Assert.IsTrue(isVisible1, "Requester Export Button  not visible");
                logger.Log(Status.Pass, "Verified requester export button is visible", TakeScreenShotAtStep());

                rOIFacilityLocationReportPage.CreateReportOnlyForIncludeTest();
                ExcelReaderFile.DeleteExistingFiles(downloadFolder, "xlsx", "roiadmin_facilitylocationsreport");
                rOIFacilityLocationReportPage.ClickOnRequesterExport();

                excelReaderFile.ConvertXLS_XLSX(downloadFolder + "roiadmin_facilitylocationsreport.xls");
                ExcelReaderFile.DeleteExistingFiles(downloadFolder, "xls", "roiadmin_facilitylocationsreport");


                string _locationName = excelReaderFile.ReadExcelCellData(downloadFolder + "roiadmin_facilitylocationsreport.xlsx", 3, 17);
                Assert.AreEqual(_locationName, "Henry Ford", "Failed to verify location name");
                string _locationCode = excelReaderFile.ReadExcelCellData(downloadFolder + "roiadmin_facilitylocationsreport.xlsx", 3, 18);
                Assert.AreEqual(_locationCode, code, "Failed to verify location code");
                logger.Log(Status.Pass, $"Verified location name:({_locationName}) and loaction code:({_locationCode})  are visible in excel file");
                ExcelReaderFile.DeleteExistingFiles(downloadFolder, "xlsx", "roiadmin_facilitylocationsreport");

                rOIAdminHomePage.SelectFeatures();
                rOIAdminFeaturesPage.VerifySelectedContext();
                rOIAdminFeaturesPage.ClickOnFacilityLocationExport();
                rOIAdminFeaturesPage.CanSeeFacilityLocationRequesterExport("Remove");
                rOIAdminFeaturesPage.ClickOnCatagories();
                rOIAdminFeaturesPage.ClickOnRequesterLocationCode();
                rOIAdminFeaturesPage.CanSeeFacilityLocationRequesterExport("Remove");
                logger.Log(Status.Info, "Verified Can Edit Requester Location Codes and Can See Facility Location Requester Export features are  removed from user account ", TakeScreenShotAtStep());
                //Need to execute manually and fix it.
                Cleanup(driver);
            }
            catch (Exception ex)
            {
                ExcelReaderFile.DeleteExistingFiles(downloadFolder, "xlsx", "roiadmin_facilitylocationsreport");
                ExcelReaderFile.DeleteExistingFiles(downloadFolder, "xls", "roiadmin_facilitylocationsreport");
                LogException(driver, logger, ex);

            }
        }
    }

}


