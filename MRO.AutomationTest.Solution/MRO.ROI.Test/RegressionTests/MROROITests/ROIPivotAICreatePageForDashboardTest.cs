using AventStack.ExtentReports;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MRO.ROI.Automation.Pages;
using MRO.ROI.Test.ExecutionFactory;
using MRO.ROI.Test.Utilities;
using OpenQA.Selenium.Remote;
using System;
using System.Threading;

namespace MRO.ROI.Test.RegressionTests.MROROITests
{
    [TestClass]
    public class ROIPivotAICreatePageForDashboardTest : ROIBaseTest
    {
        public ROIPivotAICreatePageForDashboardTest() : base(ROITestArea.ROIAdmin)
        {

        }
        [STATestMethodAttribute]
        [TestCategory(ROITestCategory.Regression)]
        // Converted manual test case 11342-ROI-Admin-->Pivot AI - create a page for dashboard to automated.
        public void Reg_11342_ROIPivotAICreatePageForDashboardTest()
        {
            logger = extent.CreateTest("Reg_11342_ROIPivotAICreatePageForDashboardTest");
            logger.Log(Status.Info, "Converted manual test case 11342-ROI-Admin-->Pivot AI - create a page for dashboard to automated");
            RemoteWebDriver _driver = Init(TestContext.Properties["browser"].ToString());
            ThreadLocal<RemoteWebDriver> localDriver = new ThreadLocal<RemoteWebDriver>();
            localDriver.Value = _driver;
            RemoteWebDriver driver = localDriver.Value;

            try
            {
                ROIAdminHomePage rOIAdminHomePage = new ROIAdminHomePage(driver, logger, TestContext);
                bool isDisplayed = rOIAdminHomePage.VerifyAutomatedLoggingAtAdminSide();
                Assert.IsFalse(isDisplayed, "Failed to verify automated logging");
                logger.Log(Status.Info, "verified Automated Logging does not exist under Batch Scan selection at admin side");
                rOIAdminHomePage.SelectFeatures();
                ROIAdminFeaturesPage rOIAdminFeaturesPage = new ROIAdminFeaturesPage(driver, logger, TestContext);
                string contextVal = rOIAdminFeaturesPage.VerifySelectedContext();
                Assert.AreEqual(contextVal, "Admin");
                logger.Log(Status.Info, "Verified context set to Admin", TakeScreenShotAtStep());

                rOIAdminFeaturesPage.ClickOnLoggingDashboard();
                rOIAdminFeaturesPage.AddLoggingDashboardAtAdminSide("Add");
                bool isDisplayed1 = rOIAdminHomePage.AddAutomatedLogging();
                Assert.IsTrue(isDisplayed1, "Failed to verify automated logging");
                logger.Log(Status.Pass, "Verified Automated Logging  exist under Batch Scan selection at admin side");
                rOIAdminHomePage.ClickOnAutomattedLogging();
                string header = rOIAdminHomePage.VerifyAIDashboardPage();
                Assert.AreEqual(header, "AI Dashboard");
                logger.Log(Status.Info, "Verified AIDashboard page loads successfully", TakeScreenShotAtStep());

                rOIAdminHomePage.SelectFacilityList();
                ROIAdminFacilityListPage rOIAdminFacilityListPage = new ROIAdminFacilityListPage(driver, logger, TestContext);
                rOIAdminFacilityListPage.ClickOnROITFGearIcon();
                ROIAdminFacilityFeaturesPage rOIAdminFacilityFeaturesPage = new ROIAdminFacilityFeaturesPage(driver, logger, TestContext);
                rOIAdminFacilityFeaturesPage.UnCheckHasAIDashboard();

                rOIAdminHomePage.OpenNewTabAndLoginROITestFacility(BaseAddress);
                ROIFacilityWorkSummaryPage rOIFacilityWorkSummaryPage = new ROIFacilityWorkSummaryPage(driver, logger, TestContext);
                bool isPresent = rOIFacilityWorkSummaryPage.VerifyAutomatedLoggingAtFacilitySide();
                Assert.IsFalse(isPresent, "Failed to verify automated logging");
                logger.Log(Status.Info, "Verified Automated Logging does not exist at facility side", TakeScreenShotAtStep());

                rOIAdminHomePage.SwitchToPreviousTab(BaseAddress);
                rOIAdminHomePage.SelectFacilityList();
                //ROIAdminFacilityListPage rOIAdminFacilityListPage = new ROIAdminFacilityListPage(driver, logger, TestContext);
                rOIAdminFacilityListPage.ClickOnROITFGearIcon();
                //ROIAdminFacilityFeaturesPage rOIAdminFacilityFeaturesPage = new ROIAdminFacilityFeaturesPage(driver, logger, TestContext);
                rOIAdminFacilityFeaturesPage.CheckHasAIDashboard();

                rOIAdminHomePage.SwitchToROITestFacilitySide(BaseAddress);
                rOIFacilityWorkSummaryPage.VerifyAutomatedLoggingAtFacilitySide();
                rOIFacilityWorkSummaryPage.ClickOnAutomattedLoggingAtFacility();
                string headerVal = rOIAdminHomePage.VerifyAIDashboardPage();
                Assert.AreEqual(headerVal, "AI Dashboard");
                logger.Log(Status.Pass, "Verified AI Dashboard loads at facility side", TakeScreenShotAtStep());

                rOIAdminHomePage.SwitchToPreviousTab(BaseAddress);
                rOIAdminHomePage.SelectFeatures();
                rOIAdminFeaturesPage.ClickOnLoggingDashboard();
                rOIAdminFeaturesPage.AddLoggingDashboardAtAdminSide("Remove");
                rOIAdminHomePage.SelectFacilityList();
                rOIAdminFacilityListPage.ClickOnROITFGearIcon();
                bool isSelected = rOIAdminFacilityFeaturesPage.UnCheckHasAIDashboard();
                Assert.IsFalse(isSelected, "Failed to uncheck the has AI Dasboard");
                logger.Log(Status.Pass, "Verified AI Dashboard removed from admin and facility side", TakeScreenShotAtStep());
                Cleanup(driver);

            }
            catch (Exception ex)
            {
                LogException(driver, logger, ex);

            }
        }
    }

}

