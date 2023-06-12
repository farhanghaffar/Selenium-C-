using AventStack.ExtentReports;
using DataDrivenProject;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MRO.ROI.Automation.Common;
using MRO.ROI.Automation.Pages;
using MRO.ROI.Automation.Pages.Common;
using MRO.ROI.Automation.Utility;
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
    public class ROIFacilityFindRequestExportToExcelPageErrorTest : ROIBaseTest
    {
        public ROIFacilityFindRequestExportToExcelPageErrorTest() : base(ROITestArea.ROIAdmin)
        {
        }
        [TestMethod]
        [TestCategory(ROITestCategory.Regression)]
        //Converted manual test case-ROI-Admin--> ROI Facility - Find Request - export to excel page error to automated.
        public void Reg_2250_ROIFacilityFindRequestExportToExcelPageErrorTest()
        {
            logger = extent.CreateTest("Reg_2250_ROIFacilityFindRequestExportToExcelPageErrorTest");
            logger.Log(Status.Info, "Converted manual test case-ROI-Admin--> ROI Facility - Find Request - export to excel page error to automated");
            RemoteWebDriver _driver = Init(TestContext.Properties["browser"].ToString());
            ThreadLocal<RemoteWebDriver> localDriver = new ThreadLocal<RemoteWebDriver>();
            localDriver.Value = _driver;
            RemoteWebDriver driver = localDriver.Value;
            string userRoot = System.Environment.GetEnvironmentVariable("USERPROFILE");
            string downloadFolder = Path.Combine(userRoot, "Downloads\\");
            string resultsMessage = string.Empty;
            try
            {
                ExcelReaderFile.DeleteExistingFiles(downloadFolder, "xlsx", "roifac_FindRequest.xlsx");
                ExcelReaderFile.DeleteExistingFiles(downloadFolder, "xls", "roifac_FindRequest.xls");

                ROIAdminHomePage rOIAdminHomePage = new ROIAdminHomePage(driver, logger, TestContext);
                Iframe frame = new Iframe(driver, logger, TestContext);

                rOIAdminHomePage.SelectFacilityList();

                ROIAdminFacilityListPage rOIAdminFacilityListPage = new ROIAdminFacilityListPage(driver, logger, TestContext);
                frame.SwitchToRoiFrame();
                rOIAdminFacilityListPage.ClickOnROITFacility();
                logger.Log(Status.Info, "Clicked on ROIT Facility", TakeScreenShotAtStep());
                ROIFacilityInfoROITestFacilityPage rOIFacilityInfoROITestFacilityPage = new ROIFacilityInfoROITestFacilityPage(driver, logger, TestContext);
                rOIFacilityInfoROITestFacilityPage.ClickOnViewFeatures();
                ROIFacilityFeaturesROITestFacilityPage rOIFacilityFeaturesROITestFacilityPage = new ROIFacilityFeaturesROITestFacilityPage(driver, logger, TestContext);
                rOIFacilityFeaturesROITestFacilityPage.ClickOnROI();
                logger.Log(Status.Info, "Navigated to ROI Tab", TakeScreenShotAtStep());
                rOIFacilityFeaturesROITestFacilityPage.CheckHasSSN();
                logger.Log(Status.Info, "Verified Has SSN is enabled", TakeScreenShotAtStep());
                rOIFacilityFeaturesROITestFacilityPage.ClickUpdateFeatures();
                frame.switchToDefaut();

                rOIAdminHomePage.SelectFacilityList();
                rOIAdminFacilityListPage.GotoROITestFacilityComputerIcon();
                ROIFacilityWorkSummaryPage rOIFacilityWorkSummaryPage = new ROIFacilityWorkSummaryPage(driver, logger, TestContext);
                ROIFacilityFindARequestPage rOIFacilityFindARequestPage = new ROIFacilityFindARequestPage(driver, logger, TestContext);
                rOIFacilityWorkSummaryPage.FindARequest("test");

                frame.SwitchToRoiFrame();
                resultsMessage = rOIFacilityFindARequestPage.SearchDataByFirstNameAndGetResultantMessage();
                
                logger.Log(Status.Info, "Veriying that search returned some results", TakeScreenShotAtStep());
                Assert.AreEqual("1000 or more requests were returned - only the first 1000 are displayed - please limit your search", resultsMessage, "Search did not result any records");
                logger.Log(Status.Pass, "Verified that search returned some results");

                logger.Log(Status.Info, "Verifying that excelsheet inclueds 1000 or more searched results", TakeScreenShotAtStep());
                rOIFacilityFindARequestPage.ValidateExcelData();               
                ExcelReaderFile excelReaderFile = new ExcelReaderFile();
                excelReaderFile.ConvertXLS_XLSX(downloadFolder + "roifac_FindRequest.xls");
                ExcelReaderFile _excelReaderFile = new ExcelReaderFile(downloadFolder + "roifac_FindRequest.xlsx");               
                int excelRowCount = _excelReaderFile.getRowCount("roifac_FindRequest");
                Assert.IsTrue(excelRowCount > 999, "Failed : Excelsheet inclueds 1000 or more searched results");
                logger.Log(Status.Pass, "Verified that excelsheet inclueds 1000 or more searched results");

                ExcelReaderFile.DeleteExistingFiles(downloadFolder, "xlsx", "roifac_FindRequest.xlsx");
                ExcelReaderFile.DeleteExistingFiles(downloadFolder, "xls", "roifac_FindRequest.xls");
                frame.switchToDefaut();
                LoginPage loginPage = new LoginPage(driver, logger, TestContext);
                loginPage.LogOut();
                Cleanup(driver);

                // here update query base on update to reset the has SSN flag
                string query = "update tblROIFacilities set bHasSSN = 0 where  nroiFacilityID = 1";
                bool boeReuqest = MRODBConnection.isUpdate(query);
                logger.Log(Status.Info, "Unchecked Has SSN checkbox using SQL query");


            }
            catch (Exception ex)
            {
                string query = "update tblROIFacilities set bHasSSN = 0 where  nroiFacilityID = 1";
                bool boeReuqest = MRODBConnection.isUpdate(query);
                logger.Log(Status.Info, "Unchecked Has SSN checkbox using SQL query");

                LogException(driver, logger, ex);
                ExcelReaderFile.DeleteExistingFiles(downloadFolder, "xls", "roifac_FindRequest.xls");
                ExcelReaderFile.DeleteExistingFiles(downloadFolder, "xlsx", "roifac_FindRequest.xlsx");
            }
        }
    }
}

