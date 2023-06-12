using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AventStack.ExtentReports;
using AventStack.ExtentReports.MarkupUtils;
using DataDrivenProject;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MRO.ROI.Automation;
using MRO.ROI.Automation.Pages;
using MRO.ROI.Automation.Selenium;
using MRO.ROI.Test.Utilities;
using OpenQA.Selenium.Remote;
using System.IO;
using System.Threading;



namespace MRO.ROI.Test.RegressionTests.MROROITests
{

    [TestClass]
    public class EnhanceFacilitiesandLocationList : ROIBaseTest
    {
        public EnhanceFacilitiesandLocationList() : base(ROITestArea.ROIAdmin)
        {
        }

        [TestMethod]
        [TestCategory(ROITestCategory.Regression)]
        //Converted manual test case 4016-ROI-Admin--> Enhance Facilities and Location List  to automated.
        public void Reg_4016_EnhanceFacilitiesandLocationList()
        {
            logger = extent.CreateTest("Reg_4016_Enhance Facilities and Location List  to automated");
            logger.Log(Status.Info, "Converted manual test case 4016-ROI-Admin-->Enhance Facilities and Location List  to automated ");
            RemoteWebDriver _driver = Init(TestContext.Properties["browser"].ToString());
            ThreadLocal<RemoteWebDriver> localDriver = new ThreadLocal<RemoteWebDriver>();
            localDriver.Value = _driver;
            RemoteWebDriver driver = localDriver.Value;
            string userRoot = System.Environment.GetEnvironmentVariable("USERPROFILE");
            string downloadFolder = Path.Combine(userRoot, "Downloads\\");
            string currentReportName = string.Empty;

            try
            {
                ROIAdminHomePage rOIAdminHomePage = new ROIAdminHomePage(driver, logger, TestContext);
                rOIAdminHomePage.FacilityLocationList();
                ROIFacilityLocationReportPage rOIFacilityLocationReportPage = new ROIFacilityLocationReportPage(driver, logger, TestContext);
                String header = rOIFacilityLocationReportPage.VerifyHeader();
                Assert.AreEqual(header, "Facility Location Report");
                logger.Log(Status.Pass, "Successfully Landed To Facility And Report Page", TakeScreenShotAtStep());

                rOIFacilityLocationReportPage.VerifyPageElements();
                logger.Log(Status.Pass, "Successfully verified all the filters", TakeScreenShotAtStep());
                rOIFacilityLocationReportPage.IncludeTestCreateReport();

                rOIFacilityLocationReportPage.ClickONExpandBtn();
                rOIFacilityLocationReportPage.VerifyTableElemnts();
                logger.Log(Status.Pass, "Successfully verified The Table Columns", TakeScreenShotAtStep());

                rOIFacilityLocationReportPage.ClickONExcelIcon();
                ExcelReaderFile excelReaderFile = new ExcelReaderFile();
                excelReaderFile.ConvertXLS_XLSX(downloadFolder + "RadGridExport.xls");
                ExcelReaderFile _excelReaderFile = new ExcelReaderFile(downloadFolder + "RadGridExport.xlsx");

                ExcelReaderFile.DeleteExistingFiles(downloadFolder, "xlsx", "RadGridExport.xlsx");
                int excelColumnCount = _excelReaderFile.getColumnCount("RadGridExport");
                int ColumnCountFromUI= rOIFacilityLocationReportPage.TableColumnCount();

                Assert.AreEqual(excelColumnCount, ColumnCountFromUI);
                ExcelReaderFile.DeleteExistingFiles(downloadFolder, "xlsx", "RadGridExport.xlsx");
                logger.Log(Status.Pass, "Excel columns count with UI Table column count is verified ", TakeScreenShotAtStep());
                rOIFacilityLocationReportPage.LogOut();


                Cleanup(driver);
            }
            catch (Exception ex)
            {
                ExcelReaderFile.DeleteExistingFiles(downloadFolder, "xlsx", "RadGridExport.xlsx");
                LogException(driver, logger, ex);
            }
        }
    }
}





