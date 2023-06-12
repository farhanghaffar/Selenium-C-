using System;
using AventStack.ExtentReports;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MRO.ROI.Automation.Pages;
using MRO.ROI.Automation.Pages.Common;
using MRO.ROI.Test.ExecutionFactory;
using MRO.ROI.Test.Utilities;
using OpenQA.Selenium.Remote;
using System.Threading;
using static MRO.ROI.Automation.Utility.IniFile;
using System.IO;
using DataDrivenProject;

namespace MRO.ROI.Test.RegressionTests.MROROITests
{
    [TestClass]
    public class AddRequesterTypeFilterToActionListTest : ROIBaseTest
    {
        public AddRequesterTypeFilterToActionListTest() : base(ROITestArea.ROIAdmin)
        {
        }
        [STATestMethodAttribute]
        [TestCategory(ROITestCategory.Regression)]
        //Converted manual test case 5614-ROI-Facility-->Add Requester Type Filter to Action List (Facility) to automated
        public void Reg_5614_AddRequesterTypeFilterToActionListTest()
        {
            logger = extent.CreateTest("Reg_5614_AddRequesterTypeFilterToActionListTest");
            logger.Log(Status.Info, "Converted manual test case 5614-ROI-Facility-->Add Requester Type Filter to Action List (Facility) to automated");
            RemoteWebDriver _driver = Init(TestContext.Properties["browser"].ToString());
            ThreadLocal<RemoteWebDriver> localDriver = new ThreadLocal<RemoteWebDriver>();
            localDriver.Value = _driver;
            RemoteWebDriver driver = localDriver.Value;
            string userRoot = System.Environment.GetEnvironmentVariable("USERPROFILE");
            string downloadFolder = Path.Combine(userRoot, "Downloads\\");
            string requestID = string.Empty;
            string sErrorMsg = string.Empty;
            bool isSearchResultsFound = false;
            int searchRequestsCount = 0;
            try
            {
                ExcelReaderFile.DeleteExistingFiles(downloadFolder, "xls", "roiadmin_actionlist.xls");
                ExcelReaderFile.DeleteExistingFiles(downloadFolder, "xlsx", "roiadmin_actionlist.xlsx");
                ExcelReaderFile.DeleteExistingFiles(downloadFolder, "xlsx", "roifac_ActionList3Report.xlsx");
                ExcelReaderFile.DeleteExistingFiles(downloadFolder, "xls", "roifac_ActionList3Report.xls");

                ROIAdminHomePage rOIAdminHomePage = new ROIAdminHomePage(driver, logger, TestContext);
                rOIAdminHomePage.FacilityList();
                ROIAdminFacilityListPage rOIAdminFacilityListPage = new ROIAdminFacilityListPage(driver, logger, TestContext);
                rOIAdminFacilityListPage.GotoROITestFacilityComputerIcon();
                ROIFacilityWorkSummaryPage rOIFacilityWorkSummaryPage = new ROIFacilityWorkSummaryPage(driver, logger, TestContext);
                rOIFacilityWorkSummaryPage.GoToLogNewRequestPage();

                ROIFacilityLogNewRequestPage rOIFacilityLogNewRequestPage = new ROIFacilityLogNewRequestPage(driver, logger, TestContext);
                rOIFacilityLogNewRequestPage.ClickMRODeliveryTab();
                rOIFacilityLogNewRequestPage.CreateNewMRODeliveryRequestForBostonProper();

                ROIFacilityRequestStatusPage rOIFacilityRequestStatusPage = new ROIFacilityRequestStatusPage(driver, logger, TestContext);
                string requestid = rOIFacilityRequestStatusPage.GetRequestID();
                logger.Log(Status.Pass, $"Request created with requestid({requestid})", TakeScreenShotAtStep());
                rOIAdminHomePage.ROIlookupByRequestId(requestid);


                rOIFacilityRequestStatusPage.ReOpenRequest();
                rOIFacilityRequestStatusPage.ImportDocumentsForFacility();
                rOIFacilityRequestStatusPage.GoToActionList();

                ROIFacilityActionListPage rOIFacilityActionListPage = new ROIFacilityActionListPage(driver, logger, TestContext);
                rOIFacilityActionListPage.CreateReportWithoutAnyParameters();
                logger.Log(Status.Pass, $"Report created with out any parameters selected", TakeScreenShotAtStep());
                bool isDisplayed = rOIFacilityActionListPage.VerifyRequestId(requestid);
                Assert.IsFalse(isDisplayed, "Request id present");
                logger.Log(Status.Pass, "Successfully verified request id not exist in the  action list report page", TakeScreenShotAtStep());

                rOIFacilityActionListPage.ClickExcelReports();
                searchRequestsCount = rOIFacilityActionListPage.GetSearchResultsCountFromTable();
                isSearchResultsFound = rOIFacilityActionListPage.ValidateExcel(downloadFolder, searchRequestsCount);
                logger.Log(Status.Pass, "Verified that search results from UI match with search results from excel");
                string actionMsg = IniHelper.ReadConfig("ROIAddLoggingUserColumnToFacilityActionListTest", "NoAuthorization");

                rOIFacilityRequestStatusPage.SelectRecentRequest();
                rOIFacilityRequestStatusPage.SelectActionMessage();
                string msg = rOIFacilityRequestStatusPage.VerifyActionMessage();
                Assert.AreEqual(msg, actionMsg, "Failed to verify action message");
                logger.Log(Status.Pass, $"Successfully verified action message created with name:{(actionMsg)} and  system date and time", TakeScreenShotAtStep());

                rOIFacilityRequestStatusPage.GoToActionList();
                rOIFacilityActionListPage.CreateReport();
                logger.Log(Status.Pass, $"Report created with today's date as date paramters", TakeScreenShotAtStep());
                bool isDisplayed2 = rOIFacilityActionListPage.VerifyRequestId(requestid);
                Assert.IsTrue(isDisplayed2, "Request id not exist");
                logger.Log(Status.Pass, "Successfully verified request id  exist in the  action list report page", TakeScreenShotAtStep());


                rOIFacilityActionListPage.ClearDatesAndCreateReport();
                logger.Log(Status.Pass, $"Report created with out action dates", TakeScreenShotAtStep());
                bool isDisplayed3 = rOIFacilityActionListPage.VerifyRequestId(requestid);
                Assert.IsTrue(isDisplayed3, "Request id not exist");
                logger.Log(Status.Pass, "Successfully verified request id  exist in the  action list report page", TakeScreenShotAtStep());

               
                rOIFacilityActionListPage.ClickExcelReports();
                searchRequestsCount = rOIFacilityActionListPage.GetSearchResultsCountFromTable();
                isSearchResultsFound = rOIFacilityActionListPage.ValidateExcel(downloadFolder, searchRequestsCount);
                logger.Log(Status.Pass, "Verified that search results from UI match with search results from excel");
               
                rOIAdminHomePage.SwitchToNewTabAndLoginROIAdmin(BaseAddress);
                rOIAdminHomePage.ROIlookupByRequestId(requestid);
                ROIAdminRequestStatusPage adminRequestStatusPage = new ROIAdminRequestStatusPage(driver, logger, TestContext);
                adminRequestStatusPage.assignRequester();

                ROIAdminAssignROIRequesterPage assignROIRequesterPage = new ROIAdminAssignROIRequesterPage(driver, logger, TestContext);
                assignROIRequesterPage.assignTestAttorney();
                logger.Log(Status.Info, "Assigned Test Attorney");

                rOIFacilityRequestStatusPage.GoToActionList();
                ROIAdminActionListPage actionListPage = new ROIAdminActionListPage(driver, logger, TestContext);
                actionListPage.CreateReport();
                logger.Log(Status.Pass, $"Action List Report created with the parameters as Requester Type:Legal,Include:Most Recent Action, Action Type:MRO/Facility ", TakeScreenShotAtStep());
                actionListPage.SelectAllRequesterTypeAndCreateReport();
                logger.Log(Status.Pass, $"Action List Report created with the parameters as Requester Type:All Types", TakeScreenShotAtStep());

                actionListPage.ClickOnExportToExcel();
                ExcelReaderFile excelReaderFile = new ExcelReaderFile();
                excelReaderFile.ConvertXLS_XLSX(downloadFolder + "roiadmin_actionlist.xls");
                ExcelReaderFile _excelReaderFile = new ExcelReaderFile(downloadFolder + "roiadmin_actionlist.xlsx");

                int excelRowCount = _excelReaderFile.getRowCount("roiadmin_actionlist");
                ExcelReaderFile.DeleteExistingFiles(downloadFolder, "xls", "roiadmin_actionlist.xls");
                ExcelReaderFile.DeleteExistingFiles(downloadFolder, "xlsx", "roiadmin_actionlist.xlsx");
                logger.Log(Status.Pass, "Verified that search results from UI match with search results from excel");

                LoginPage loginPage = new LoginPage(driver, logger, TestContext);
                loginPage.LogOut();
                Cleanup(driver);

            }
            catch (Exception ex)
            {
                ExcelReaderFile.DeleteExistingFiles(downloadFolder, "xls", "roiadmin_actionlist.xls");
                ExcelReaderFile.DeleteExistingFiles(downloadFolder, "xlsx", "roiadmin_actionlist.xlsx");
                ExcelReaderFile.DeleteExistingFiles(downloadFolder, "xlsx", "roifac_ActionList3Report.xlsx");
                ExcelReaderFile.DeleteExistingFiles(downloadFolder, "xls", "roifac_ActionList3Report.xls");
                LogException(driver, logger, ex);
            }

        }
    }
}
