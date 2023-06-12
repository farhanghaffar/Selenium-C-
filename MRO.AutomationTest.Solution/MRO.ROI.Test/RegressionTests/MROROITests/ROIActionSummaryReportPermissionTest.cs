using AventStack.ExtentReports;
using AventStack.ExtentReports.MarkupUtils;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MRO.ROI.Automation.Common;
using MRO.ROI.Automation.Pages;
using MRO.ROI.Automation.Pages.Common;
using MRO.ROI.Automation.Selenium;
using MRO.ROI.Test.ExecutionFactory;
using MRO.ROI.Test.Utilities;
using OpenQA.Selenium.Remote;
using System;
using System.Threading;
using static MRO.ROI.Automation.Utility.IniFile;

namespace MRO.ROI.Test.RegressionTests.MROROITests
{
    [TestClass]
    public class ROIActionSummaryReportPermissionTest : ROIBaseTest
    {
        public ROIActionSummaryReportPermissionTest() : base(ROITestArea.ROIAdmin)
        {

        }

        [STATestMethodAttribute]
        [TestCategory(ROITestCategory.Passed)]
        // Converted manual test case 4297-ROI-Admin-->Action Summary Report - Permission to automated.
        public void Reg_4297_ROIActionSummaryReportPermissionTest()
        {
            logger = extent.CreateTest("Reg_4297_ROIActionSummaryReportPermissionTest");
            logger.Log(Status.Info, "Converted manual test case 4297-ROI-Admin-->Action Summary Report - Permission to automated");
            RemoteWebDriver _driver = Init(TestContext.Properties["browser"].ToString());
            ThreadLocal<RemoteWebDriver> localDriver = new ThreadLocal<RemoteWebDriver>();
            localDriver.Value = _driver;
            RemoteWebDriver driver = localDriver.Value;

            try
            {

                string fname = IniHelper.ReadConfig("ROIActionSummaryReportPermissionTest", "Firstname");
                string lname = IniHelper.ReadConfig("ROIActionSummaryReportPermissionTest", "Lastname");
                ROIAdminHomePage rOIAdminHomePage = new ROIAdminHomePage(driver, logger, TestContext);
                Iframe frame = new Iframe(driver, logger, TestContext);
                rOIAdminHomePage.GotoActionSummaryReport();
                frame.SwitchToRoiFrame();  
                ROIActionSummaryReportPage rOIActionSummaryReportPage = new ROIActionSummaryReportPage(driver, logger, TestContext);
                string selectedUser=rOIActionSummaryReportPage.VerifyUsername();
                Assert.AreEqual(selectedUser, "Anjali", "Failed to verify users");
                bool isDisabled= rOIActionSummaryReportPage.CheckUsernameDisabled();
                Assert.IsTrue(isDisabled, "User name dropdown is visible");
                logger.Log(Status.Pass, "Verified username selected as login user and username dropdown is disabled", TakeScreenShotAtStep());
                rOIActionSummaryReportPage.ClickOnSummaryButton();
                rOIActionSummaryReportPage.ClickOnExcelIcon();
                rOIAdminHomePage.SelectFeatures();
                ROIAdminFeaturesPage rOIAdminFacilityFeatures = new ROIAdminFeaturesPage(driver, logger, TestContext);
                rOIAdminFacilityFeatures.ClickOnActionSummaryViewAll();
                rOIAdminHomePage.AddActionSummaryReport("Add");
                rOIAdminHomePage.GotoActionSummaryReport();
                //rOIAdminFacilityFeatures.ClickOnActionSummaryViewAll();
                //rOIAdminHomePage.AddActionSummaryReport("Remove");
                rOIAdminHomePage.SwitchToNewTabAndLoginROIAdmin(BaseAddress);
                rOIAdminHomePage.GotoAdminList();
                rOIAdminHomePage.ClickOnSearchList(fname, lname);
                bool isDisplayed= rOIAdminHomePage.VerifyUserDisplayed();
                Assert.IsTrue(isDisplayed, "User not displayed");
                logger.Log(Status.Pass, "Sucessfully verified search returns the data", TakeScreenShotAtStep());
                string userId=rOIAdminHomePage.GetUserId();
                rOIAdminHomePage.SwitchToPreviousTab(BaseAddress);
                rOIAdminHomePage.GotoActionSummaryReport();
                string count=rOIAdminHomePage.GetUserSummaryReport(userId);
                logger.Log(Status.Pass, $"Succesfully verified search returns the report and count for the report is{(count)}", TakeScreenShotAtStep());

                rOIAdminHomePage.SelectFeatures();
                rOIAdminFacilityFeatures.ClickOnActionSummaryViewAll();
                rOIAdminHomePage.AddActionSummaryReport("Remove");

                Cleanup(driver);

            }
            catch (Exception ex)
            {
                LogException(driver, logger, ex);

            }
        }
    }

}

