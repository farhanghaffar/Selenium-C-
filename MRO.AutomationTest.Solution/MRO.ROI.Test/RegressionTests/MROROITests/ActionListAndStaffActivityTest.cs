
using AventStack.ExtentReports;
using DataDrivenProject;
using Microsoft.VisualStudio.TestTools.UnitTesting;
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
    public class ActionListAndStaffActivityTest : ROIBaseTest
    {
        public ActionListAndStaffActivityTest() : base(ROITestArea.ROIAdmin)
        {

        }
        [STATestMethodAttribute]
        [TestCategory(ROITestCategory.Regression)]
        //Converted manual test case 12213-ROI Admin-->_1 Top SQL-Action list and Staff activity to automated
        public void Reg_12213_ActionListAndStaffActivityTest()
        {
            logger = extent.CreateTest("Reg_12213_ActionListAndStaffActivityTest");
            logger.Log(Status.Info, "Converted manual test case 12213-ROI Admin-->_1 Top SQL-Action list and Staff activity to automated");
            RemoteWebDriver _driver = Init(TestContext.Properties["browser"].ToString());
            ThreadLocal<RemoteWebDriver> localDriver = new ThreadLocal<RemoteWebDriver>();
            localDriver.Value = _driver;
            RemoteWebDriver driver = localDriver.Value;
            string userRoot = System.Environment.GetEnvironmentVariable("USERPROFILE");
            string downloadFolder = Path.Combine(userRoot, "Downloads\\");

            string sUpdatedDate = string.Empty;
            string sResultsMessage = string.Empty;
            try
            {
                ROIAdminHomePage adminHomePage = new ROIAdminHomePage(driver, logger, TestContext);
                adminHomePage.ClickOnActionList();
                ROIAdminActionListPage actionListPage = new ROIAdminActionListPage(driver, logger, TestContext);
                ROIFacilityStaffActivityReportPage staffActivityReportPage = new ROIFacilityStaffActivityReportPage(driver, logger, TestContext);
                actionListPage.ClickOnShowList();
                logger.Log(Status.Info, "Action List Report generated with default filters", TakeScreenShotAtStep());

                actionListPage.CheckQcFailedActionsOnly();
                actionListPage.ClickOnShowList();
                logger.Log(Status.Info, "Action List Report generated with [QC Failed Action Only] checkbox filter", TakeScreenShotAtStep());

                actionListPage.SelectActionType("MRO Only");
                logger.Log(Status.Info, "Action List Report generated with filters as Action Type:[MRO Only]", TakeScreenShotAtStep());

                actionListPage.ClickOnShowList();
                sResultsMessage = actionListPage.SelectOtherFiltersAndCreateReport();
                logger.Log(Status.Info, "Action List Report created with filters as Requester Type:[Patient],Request Type:[Log-Only] ", TakeScreenShotAtStep());

                Assert.AreEqual("No results found for this table!", sResultsMessage);
               
                actionListPage.ClickOnExportToExcel();
                ExcelReaderFile excelReaderFile = new ExcelReaderFile();
                excelReaderFile.ConvertXLS_XLSX(downloadFolder + "roiadmin_actionlist.xls");                
                ExcelReaderFile _excelReaderFile = new ExcelReaderFile(downloadFolder + "roiadmin_actionlist.xlsx");
                ExcelReaderFile.DeleteExistingFiles(downloadFolder, "xlsx", "roiadmin_actionlist.xlsx");
                int excelRowCount = _excelReaderFile.getRowCount("roiadmin_actionlist");
                ExcelReaderFile.DeleteExistingFiles(downloadFolder, "xlsx", "roiadmin_actionlist.xlsx");
                adminHomePage.SelectFacilityList();
                ROIAdminFacilityListPage roiAdminFacilityListPage = new ROIAdminFacilityListPage(driver, logger, TestContext);
                roiAdminFacilityListPage.GoToROITestFacility();
                ROIFacilityWorkSummaryPage roiFacilityWorkSummaryPage = new ROIFacilityWorkSummaryPage(driver, logger, TestContext);
                roiFacilityWorkSummaryPage.GoToMROAnalyzeSelectStaffActivityReport();
                staffActivityReportPage.ApplyFiltersAndCreateReport();
                logger.Log(Status.Info, "Staff Activity Report created with filters as Report Criteria:[Logged],Reporting Group:[None],DateRange:[Today], Location", TakeScreenShotAtStep());
                staffActivityReportPage.ValidateDetailView(downloadFolder);             
                logger.Log(Status.Pass, "Verified that the PDF file has all entries from the report same as the report from UI");
                Cleanup(driver);
            }
            catch (Exception ex)
            {
                ExcelReaderFile.DeleteExistingFiles(downloadFolder, "xls", "roiadmin_actionlist.xls");
                LogException(driver, logger, ex);
            }
        }
    }
}
