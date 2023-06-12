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
    public class SummaryDashBoardReceivedToShippedMinusInvoicedToPaidTest : ROIBaseTest
    {
        public SummaryDashBoardReceivedToShippedMinusInvoicedToPaidTest() : base(ROITestArea.ROIAdmin)
        {

        }
        [STATestMethodAttribute]
        [TestCategory(ROITestCategory.Regression)]
        //Converted manual test case 12888-ROI Admin-->Summary Dashboard-Received to Shipped,Minus Invoiced to Paid to automated
        public void Reg_12888_SummaryDashBoardReceivedToShippedMinusInvoicedToPaidTest()
        {
            logger = extent.CreateTest("Reg_12888_SummaryDashBoardReceivedToShippedMinusInvoicedToPaidTest");
            logger.Log(Status.Info, "Converted manual test case 12888 - ROI Admin -->Summary Dashboard-Received to Shipped,Minus Invoiced to Paid to automated");
            RemoteWebDriver _driver = Init(TestContext.Properties["browser"].ToString());
            ThreadLocal<RemoteWebDriver> localDriver = new ThreadLocal<RemoteWebDriver>();
            localDriver.Value = _driver;
            RemoteWebDriver driver = localDriver.Value;
            string userRoot = System.Environment.GetEnvironmentVariable("USERPROFILE");
            string downloadFolder = Path.Combine(userRoot, "Downloads\\");
            bool isTurnaroundTilesVisible = false;
            string sDateSelected = string.Empty;
            string sOverAllDays = string.Empty;
            bool isExcelAndPDFVisibleForVolumeTab = false;
            bool isExcelAndPDFVisibleForTurnAroundTimeTab = false;
            bool isExcelAndPDFVisibleForAgingRequestsTab = false;
            bool isExcelAndPDFVisibleForPendingOnHoldTab = false;
            string sUpdatedDate = string.Empty;
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
                logger.Log(Status.Info, "Summary dashboard report created with filters as [Date:Last 3 Months, Reporting Group:None,Location:All,Exclude:None]", TakeScreenShotAtStep());
                isTurnaroundTilesVisible = roiFacilitySummaryDashBoardPage.ValidateTilesForTurnaroundTab(sDateSelected);
                logger.Log(Status.Pass, "Turnaround Time tab tiles(Turnaround Time Summary,Turnaround Time Totals,Turnaround Time Avg) are visible", TakeScreenShotAtStep());
                sOverAllDays = roiFacilitySummaryDashBoardPage.GetOverAllTimeFromPreBill();
                logger.Log(Status.Info, $"Overall Time from Pre-Bill is {sOverAllDays} ", TakeScreenShotAtStep());
                isExcelAndPDFVisibleForVolumeTab = roiFacilitySummaryDashBoardPage.DoesVolumeTabHasExportAndPdfIcons();
                logger.Log(Status.Pass, $"Does Volume tab contains excel and pdf icons:{isExcelAndPDFVisibleForVolumeTab}", TakeScreenShotAtStep());
                //
                isExcelAndPDFVisibleForTurnAroundTimeTab = roiFacilitySummaryDashBoardPage.DoesTurnaroundTimeTabHasExportAndPdfIcons();
                logger.Log(Status.Pass, $"Does Turnaround Time tab contains excel and pdf icons:{isExcelAndPDFVisibleForVolumeTab}", TakeScreenShotAtStep());
                //
                isExcelAndPDFVisibleForAgingRequestsTab = roiFacilitySummaryDashBoardPage.DoesAgingRequestsTabHasExportAndPdfIcons();
                logger.Log(Status.Pass, $" Does Aging Requests tab contains excel and pdf icons:{isExcelAndPDFVisibleForVolumeTab}", TakeScreenShotAtStep());
                //
                isExcelAndPDFVisibleForPendingOnHoldTab = roiFacilitySummaryDashBoardPage.DoesPendingOnHoldTabHasExportAndPdfIcons();
                logger.Log(Status.Pass, $" Does Pending-OnHold tab contains excel and pdf icons:{isExcelAndPDFVisibleForVolumeTab}", TakeScreenShotAtStep());

                sUpdatedDate = roiFacilitySummaryDashBoardPage.ChangeFiltersAndCreateReport();
                logger.Log(Status.Info, $"Summary dashboard report created with different filters as [Date:3 months from {sUpdatedDate}, Reporting Group:None,Location:All,Exclude:None]", TakeScreenShotAtStep());
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
