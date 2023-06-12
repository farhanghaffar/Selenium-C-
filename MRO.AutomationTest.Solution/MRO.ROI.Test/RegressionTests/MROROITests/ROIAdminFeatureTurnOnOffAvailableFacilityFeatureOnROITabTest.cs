using AventStack.ExtentReports;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MRO.ROI.Automation.Common;
using MRO.ROI.Automation.Pages;
using MRO.ROI.Automation.Pages.Common;
using MRO.ROI.Test.ExecutionFactory;
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
    public class ROIAdminFeatureTurnOnOffAvailableFacilityFeatureOnROITabTest : ROIBaseTest
    {
        public ROIAdminFeatureTurnOnOffAvailableFacilityFeatureOnROITabTest() : base(ROITestArea.ROIAdmin)
        {
        }
        [STATestMethodAttribute]
      //  [TestMethod]
        [TestCategory(ROITestCategory.Passed)]
        //Converted manual test case-ROI-Admin-->Feature turn On/Off available for Facility Features,Added 'Has Actions At Logging' as a new facility feature - on the ROI tab to automated.
        public void Reg_780_Feature_turn_On_Off_available()
        {
            logger = extent.CreateTest("Reg_780_Feature_turn_On_Off_available");
            logger.Log(Status.Info, "Converted manual test case-ROI-Admin-->Feature turn On/Off available for Facility Features,Added 'Has Actions At Logging' as a new facility feature - on the ROI tab to automated.");
            RemoteWebDriver _driver = Init(TestContext.Properties["browser"].ToString());
            ThreadLocal<RemoteWebDriver> localDriver = new ThreadLocal<RemoteWebDriver>();
            localDriver.Value = _driver;
            RemoteWebDriver driver = localDriver.Value;
            string userRoot = System.Environment.GetEnvironmentVariable("USERPROFILE");
            string downloadFolder = Path.Combine(userRoot, "Downloads\\");

            try
            {

                logger.Log(Status.Info, "Login into: " + hqiisstgURL);
                ROIAdminHomePage rOIAdminHomePage = new ROIAdminHomePage(driver, logger, TestContext);
                LoginPage loginPage = new LoginPage(driver, logger, TestContext);
                Iframe frame = new Iframe(driver, logger, TestContext);
                loginPage.GoToROIAdminhqiisstgLoginPage(hqiisstgURL);
                rOIAdminHomePage.ROIAdminLoginForSpecificUser();
                rOIAdminHomePage.SelectFacilityList();
                ROIAdminFacilityListPage rOIAdminFacilityListPage = new ROIAdminFacilityListPage(driver, logger, TestContext);
                frame.SwitchToRoiFrame();
                rOIAdminFacilityListPage.ClickOnROITFacility();
                logger.Log(Status.Info, "clicked on ROIT facility", TakeScreenShotAtStep());
                ROIFacilityInfoROITestFacilityPage rOIFacilityInfoROITestFacilityPage = new ROIFacilityInfoROITestFacilityPage(driver, logger, TestContext);
                rOIFacilityInfoROITestFacilityPage.ClickOnViewFeatures();
                ROIFacilityFeaturesROITestFacilityPage rOIFacilityFeaturesROITestFacilityPage = new ROIFacilityFeaturesROITestFacilityPage(driver, logger, TestContext);
                rOIFacilityFeaturesROITestFacilityPage.ClickOnROI();
                rOIFacilityFeaturesROITestFacilityPage.CheckHasActionsAtLogging();
                logger.Log(Status.Info, "Verified Has Actions At Logging, is enabled", TakeScreenShotAtStep());
                rOIFacilityFeaturesROITestFacilityPage.ClickUpdateFeatures();

                frame.switchToDefaut();
                rOIAdminHomePage.SelectFacilityList();
                rOIAdminFacilityListPage.GotoROITestFacilityComputerIcon();
                ROIMenuSelector menuSelector = new ROIMenuSelector(driver, logger, TestContext);
                menuSelector.Select("ROI Requests", "Log a New Request");

                ROIFacilityWorkSummaryPage rOIFacilityWorkSummaryPage = new ROIFacilityWorkSummaryPage(driver, logger, TestContext);
                //step- 9 Not able to verify MRO text box appears
                frame.SwitchToRoiFrame();
                LogNewRequestPage logNewRequstpage = new LogNewRequestPage(driver, logger, TestContext);
                logNewRequstpage.ClickMroDelivry();

                logger.Log(Status.Info, "Verifying 'Action Message For MRO' text box is showing");
                bool statusUnderImportDocument = rOIFacilityWorkSummaryPage.IsActionMessageForMRODisplaying();
                Assert.IsTrue(statusUnderImportDocument, "Failed to verify 'Action Message For MRO' text box is showing");
                logger.Log(Status.Pass, "Verified 'Action Message For MRO' text box is showing");

                rOIAdminHomePage.SwitchToNewTabAndLoginROIAdmin(hqiisstgURL);
                rOIAdminHomePage.SelectFacilityList();
                frame.SwitchToRoiFrame();
                rOIAdminFacilityListPage.ClickOnROITFacility();
                logger.Log(Status.Info, "clicked on ROIT facility after enabling the Has Actions At Logging", TakeScreenShotAtStep());
                rOIFacilityInfoROITestFacilityPage.ClickOnViewFeatures();
                rOIFacilityFeaturesROITestFacilityPage.ClickOnROI();
                rOIFacilityFeaturesROITestFacilityPage.UnCheckHasActionsAtLogging();
                logger.Log(Status.Info, "Verified Has Actions At Logging, is disabled", TakeScreenShotAtStep());
                rOIFacilityFeaturesROITestFacilityPage.ClickUpdateFeatures();

                frame.switchToDefaut();
                rOIAdminHomePage.SelectFacilityList();
                rOIAdminFacilityListPage.GotoROITestFacilityComputerIcon();
                menuSelector.Select("ROI Requests", "Log a New Request");
                frame.SwitchToRoiFrame();
                logNewRequstpage.ClickMroDelivry();

                logger.Log(Status.Info, "Verifying 'Action Message For MRO' text box is not showing");
                statusUnderImportDocument = rOIFacilityWorkSummaryPage.IsActionMessageForMRODisplaying();
                Assert.IsFalse(statusUnderImportDocument, "Failed to verify 'Action Message For MRO' text box is not showing");
                logger.Log(Status.Pass, "Verified 'Action Message For MRO' text box is not showing");

                frame.switchToDefaut();
                rOIAdminHomePage.SelectLogoutFromProfile();

                //step 16 Not able to verify MRO text box appears

                Cleanup(driver);
            }
            catch (Exception ex)
            {

                LogException(driver, logger, ex);
            }
        }
    }
}
