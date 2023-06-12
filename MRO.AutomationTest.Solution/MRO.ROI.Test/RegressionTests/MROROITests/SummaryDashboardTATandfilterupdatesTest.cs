using AventStack.ExtentReports;
using Microsoft.VisualStudio.TestTools.UnitTesting;
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
    public class SummaryDashboardTATandfilterupdatesTest : ROIBaseTest
    {
        public SummaryDashboardTATandfilterupdatesTest() : base(ROITestArea.ROIAdmin)
        {

        }
        [STATestMethodAttribute]
        [TestCategory(ROITestCategory.Development)]
        //Converted manual test case 13090-ROI Admin-->Summary Dashboard - TAT and filter updates
        //Written by shivani
        public void Reg_13090_SummaryDashboardTATAndFilterUpdatesTest()
        {
            logger = extent.CreateTest("Reg_13090_SummaryDashboardTATAndFilterUpdatesTest");
            logger.Log(Status.Info, "Converted manual test case 13090 - ROI Admin -->Summary Dashboard - TAT and filter updates");
            RemoteWebDriver _driver = Init(TestContext.Properties["browser"].ToString());
            ThreadLocal<RemoteWebDriver> localDriver = new ThreadLocal<RemoteWebDriver>();
            localDriver.Value = _driver;
            RemoteWebDriver driver = localDriver.Value;
            string userRoot = System.Environment.GetEnvironmentVariable("USERPROFILE");
            string downloadFolder = Path.Combine(userRoot, "Downloads\\");
            string sDateSelected = string.Empty;
            string sOverAllDays = string.Empty;           
            string sUpdatedDate = string.Empty;
            bool isAllMonthSearchesVisible = false;
            try
            {
                ROIAdminHomePage rOIAdminHomePage = new ROIAdminHomePage(driver, logger, TestContext);
                rOIAdminHomePage.SelectFacilityList();
                ROIAdminFacilityListPage rOIAdminFacilityListPage = new ROIAdminFacilityListPage(driver, logger, TestContext);
                rOIAdminFacilityListPage.GoToROITestFacility();
                ROIFacilityWorkSummaryPage rOIFacilityWorkSummaryPage = new ROIFacilityWorkSummaryPage(driver, logger, TestContext);
                rOIFacilityWorkSummaryPage.SelectSummaryDashBoard();

                ROIFacilitySummaryDashBoardPage roiFacilitySummaryDashBoardPage = new ROIFacilitySummaryDashBoardPage(driver, logger, TestContext);
                sDateSelected = roiFacilitySummaryDashBoardPage.ApplyFiltersAndCreateReport();
                logger.Log(Status.Info, "Summary dashboard report created with filters as [Date:Recent  Months, Reporting Group:None,Location:All,Exclude:None,Days:Business Days]", TakeScreenShotAtStep());

                isAllMonthSearchesVisible = roiFacilitySummaryDashBoardPage.VerifyAllMonthsFiltersAreAvailable();
                Assert.AreEqual(true, isAllMonthSearchesVisible, "Failed to verify all month searches next to date");
                logger.Log(Status.Info, "Verified that all the month searches are available under dopdown next to date", TakeScreenShotAtStep());
                roiFacilitySummaryDashBoardPage.ApplyFiltersAndCreateReport3Months();
                //roiFacilitySummaryDashBoardPage.VerifyReceivedToShipped();
                logger.Log(Status.Info, "All the 3 Months Filters are available", TakeScreenShotAtStep());

                roiFacilitySummaryDashBoardPage.ApplyFiltersAndCreateReport12Months();
                roiFacilitySummaryDashBoardPage.VerifyReceivedToShipped();
                logger.Log(Status.Info, "All the 12 Months Filters are available", TakeScreenShotAtStep());
                roiFacilitySummaryDashBoardPage.ClickOnTurnaroundTimeTab();
                logger.Log(Status.Info, "Verified the Turnaround Time Average graph is displaying as a Stacked graph with data from Logged to shipped");
                roiFacilitySummaryDashBoardPage.VerifyReceivedToLogged();
                logger.Log(Status.Info, "Verified Turnaround time average graph is displaying Received to Logged, Logged to release, Received to Released, Release to shipped Received to shipped.");                        
                roiFacilitySummaryDashBoardPage.Verify_ReceivedToShipped();
                logger.Log(Status.Info, "Verified the graph for Requester Type dropdownbox");
                roiFacilitySummaryDashBoardPage.ValidateRequesterTypeDropDown();
                logger.Log(Status.Info, "Verified individual filter in the Requester Type filter updates the Turnaround time.");
                
                roiFacilitySummaryDashBoardPage.ClickOnExportToExcelForTurnaroundTab();
                logger.Log(Status.Pass, "Clicked and verified all the excel icons for turnaround table tab");
                roiFacilitySummaryDashBoardPage.ClickOnExportToPdfForTurnaroundTab();
                logger.Log(Status.Pass, "Clicked and verified all the pdf icons for turnaround table tab");
                driver.SwitchTo().DefaultContent();               
                Cleanup(driver);
            }
            catch (Exception ex)
            {
                LogException(driver, logger, ex);
            }
        }
    }
}