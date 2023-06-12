using System;
using AventStack.ExtentReports;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MRO.ROI.Automation.Pages;
using MRO.ROI.Automation.Pages.Common;
using MRO.ROI.Automation.Selenium;
using MRO.ROI.Test.ExecutionFactory;
using MRO.ROI.Test.Utilities;
using OpenQA.Selenium.Remote;
using System.Threading;

namespace MRO.ROI.Test.RegressionTests.MROROITests
{
    [TestClass]
    public class ProfitablityReportDatesComputingTest:ROIBaseTest
    {
        public ProfitablityReportDatesComputingTest() : base(ROITestArea.ROIAdmin)
        {
        }
        [STATestMethodAttribute]
        [TestCategory(ROITestCategory.Passed)]
        //Converted manual test case 1406-ROI-Admin-->Profitablity Report dates computing correctly to automated.
        public void Reg_1406_ProfitablityReportDatesComputingTest()
        {
            logger = extent.CreateTest("Reg_1406_ProfitablityReportDatesComputingTest");
            logger.Log(Status.Info, "Converted manual test case 1406-ROI-Admin-->Profitablity Report dates computing correctly to automated");
            RemoteWebDriver _driver = Init(TestContext.Properties["browser"].ToString());
            ThreadLocal<RemoteWebDriver> localDriver = new ThreadLocal<RemoteWebDriver>();
            localDriver.Value = _driver;
            RemoteWebDriver driver = localDriver.Value;
            bool isDataValidated = false;
            string sFromDate = string.Empty;
            string sToDate = string.Empty;
            bool isAlertVisible = false;
            string sReportDate = string.Empty;
            try
            {
                ROIMenuSelector menuSelector = new ROIMenuSelector(driver, logger, TestContext);
                menuSelector.SelectRoiAdminMenuOptions("mnuROIAdmin", "Financial", "Profitability Report");

                ROIAdminProfitabilityReportPage adminProfitabilityReportPage = new ROIAdminProfitabilityReportPage(driver, logger, TestContext);
                isDataValidated = adminProfitabilityReportPage.VerifyLoadedData();
                Assert.AreEqual(true, isDataValidated, "Failed to validate max and last updated dates from loaded data");
                logger.Log(Status.Info, "Verified max date and last updated date under profitability report",TakeScreenShotAtStep());
                sFromDate = adminProfitabilityReportPage.GetMinDateFromLoadedData();
                sToDate = adminProfitabilityReportPage.GetLastUpdatedDateFromLoadedData();
                adminProfitabilityReportPage.ApplyFiltersAndCreateReport(sFromDate,sToDate);
                isAlertVisible = adminProfitabilityReportPage.IsPopupMessageVisible();
                Assert.AreEqual(true, isAlertVisible, "Failed to get the visibility status of the popup alert");

                logger.Log(Status.Info, "Verified that pop up message is visible with the text the data for this date is not complete",TakeScreenShotAtStep());
                adminProfitabilityReportPage.ReApplyFiltersAndCreateReport(sFromDate, sToDate);
                sReportDate = adminProfitabilityReportPage.GetTheDatesFromReport();
                logger.Log(Status.Info, $"Verified that pop up message is not visible along with the report generated for the {sReportDate}", TakeScreenShotAtStep());
                driver.SwitchToDefaultContent();
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
