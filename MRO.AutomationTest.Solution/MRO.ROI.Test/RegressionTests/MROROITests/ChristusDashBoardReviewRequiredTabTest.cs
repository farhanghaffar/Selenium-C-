using AventStack.ExtentReports;
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
    public class ChristusDashBoardReviewRequiredTabTest : ROIBaseTest
    {
        public ChristusDashBoardReviewRequiredTabTest() : base(ROITestArea.ROIAdmin)
        {

        }
        [STATestMethodAttribute]
        [TestCategory(ROITestCategory.Regression)]
        //Converted manual test case 9984-ROI-Admin--> Christus Dashboard-Review required Tab to automated
        public void Reg_9984_ChristusDashBoardReviewRequiredTabTest()
        {
            logger = extent.CreateTest("Reg_9984_ChristusDashBoardReviewRequiredTabTest");
            logger.Log(Status.Info, "Converted manual test case 9984-ROI-Admin--> Christus Dashboard-Review required Tab to automated");
            RemoteWebDriver _driver = Init(TestContext.Properties["browser"].ToString());
            ThreadLocal<RemoteWebDriver> localDriver = new ThreadLocal<RemoteWebDriver>();
            localDriver.Value = _driver;
            RemoteWebDriver driver = localDriver.Value;
            string userRoot = System.Environment.GetEnvironmentVariable("USERPROFILE");
            string downloadFolder = Path.Combine(userRoot, "Downloads\\");
            bool isVolumeTilesVisible = false;
            string sDateSelected = string.Empty;
            bool isTurnAroundTilesVisible = false;
            bool isAgingRequestTilesVisible = false;
            bool isAllRequesterTypesSelectedDefault = false;
            bool isExcelAndPDFVisibleForAgingRequestsTab = false;
            try
            {
                ROIAdminHomePage rOIAdminHomePage = new ROIAdminHomePage(driver, logger, TestContext);
                rOIAdminHomePage.SelectFacilityList();
                ROIAdminFacilityListPage rOIAdminFacilityListPage = new ROIAdminFacilityListPage(driver, logger, TestContext);
                rOIAdminFacilityListPage.GoToROITestFacility();
                ROIFacilityWorkSummaryPage rOIFacilityWorkSummaryPage = new ROIFacilityWorkSummaryPage(driver, logger, TestContext);
                rOIFacilityWorkSummaryPage.SelectSummaryDashBoard();
                //
                ROIFacilitySummaryDashBoardPage roiFacilitySummaryDashBoardPage = new ROIFacilitySummaryDashBoardPage(driver, logger, TestContext);
                sDateSelected = roiFacilitySummaryDashBoardPage.ApplyFiltersAndCreateReport();
                logger.Log(Status.Info, "Summary dashboard report created with filters as [Date:Last 3 Months, Reporting Group:None,Location:All,Exclude:Check All]", TakeScreenShotAtStep());
                roiFacilitySummaryDashBoardPage.IsAllTabsVisibleUnderReport();
                logger.Log(Status.Pass, "Verified all tabs[Volume,Turnaround Time,Aging Request,Pending- On Hold] are visible under summary dashboard", TakeScreenShotAtStep());
                roiFacilitySummaryDashBoardPage.ClickOnVolumeTab();
                isVolumeTilesVisible = roiFacilitySummaryDashBoardPage.ValidateTilesForVolumeTab(sDateSelected);
               
                logger.Log(Status.Pass, "Volume tab tiles(REQUEST VOLUME,REQUEST VOLUME (BY SHIPMENT TYPE),SHIPPED REQUEST BY REQUESTER TYPE,VOLUME SUMMARY) are visible", TakeScreenShotAtStep());
                roiFacilitySummaryDashBoardPage.Validatebargraphs(downloadFolder);
                roiFacilitySummaryDashBoardPage.ClickOnTurnaroundTimeTab();
                isTurnAroundTilesVisible = roiFacilitySummaryDashBoardPage.ValidateTilesForTurnaroundTab(sDateSelected);
                roiFacilitySummaryDashBoardPage.ValidatePieChartsForTurnAroundTab(downloadFolder);
                roiFacilitySummaryDashBoardPage.ClickOnAgingRequestsTab();
                isAgingRequestTilesVisible = roiFacilitySummaryDashBoardPage.ValidateTilesForAgingRequestsTab(sDateSelected);
                isAllRequesterTypesSelectedDefault = roiFacilitySummaryDashBoardPage.isRequesterMultiTypeDropdownAvailable();
                Assert.AreEqual(true, isAllRequesterTypesSelectedDefault, "Requester Type dropdown is not selected with all values by default");
                isExcelAndPDFVisibleForAgingRequestsTab = roiFacilitySummaryDashBoardPage.DoesAgingRequestsTabHasExportAndPdfIcons();
                logger.Log(Status.Pass, $" Does Aging Requests tab contains excel and pdf icons:{isExcelAndPDFVisibleForAgingRequestsTab}", TakeScreenShotAtStep());

                roiFacilitySummaryDashBoardPage.ClickOnPendingOnHoldTab();

                roiFacilitySummaryDashBoardPage.ValidatePieChartsForPendingOnHoldTab(downloadFolder);

                sDateSelected = roiFacilitySummaryDashBoardPage.ReapplyFiltersAndCreateReport();
                roiFacilitySummaryDashBoardPage.IsAllTabsVisibleUnderReport();
                logger.Log(Status.Pass, "Verified all tabs[Volume,Turnaround Time,Aging Request,Pending- On Hold] are visible under summary dashboard", TakeScreenShotAtStep());

                sDateSelected = roiFacilitySummaryDashBoardPage.ApplyFiltersAndCreateReport();
                logger.Log(Status.Info, "Summary dashboard report created with filters as [Date:Last 3 Months, Reporting Group:None,Location:All,Exclude:Check All]", TakeScreenShotAtStep());
                roiFacilitySummaryDashBoardPage.IsAllTabsVisibleUnderReport();
                logger.Log(Status.Pass, "Verified all tabs[Volume,Turnaround Time,Aging Request,Pending- On Hold] are visible under summary dashboard", TakeScreenShotAtStep());
                roiFacilitySummaryDashBoardPage.ClickOnVolumeTab();
                logger.Log(Status.Info, "PDF Reports with requests can not be automated");
                driver.SwitchTo().DefaultContent();
                LoginPage loginPage = new LoginPage(driver, logger, TestContext);
                loginPage.LogOut();                
                Cleanup(driver);
            }
            catch (Exception ex)
            {
                LogException(driver, logger, ex);
            }
        }

    }
}