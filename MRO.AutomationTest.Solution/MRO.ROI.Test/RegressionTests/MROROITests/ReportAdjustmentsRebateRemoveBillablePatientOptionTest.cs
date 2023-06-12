using AventStack.ExtentReports;
using DataDrivenProject;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MRO.ROI.Automation.Common;
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
    public class ReportAdjustmentsRebateRemoveBillablePatientOptionTest: ROIBaseTest
    {
        public ReportAdjustmentsRebateRemoveBillablePatientOptionTest() : base(ROITestArea.ROIAdmin)
        {

        }
        [STATestMethodAttribute]
        [TestCategory(ROITestCategory.Passed)]
        //Converted manual test case 2122-ROI-Admin-->Report adjustments Rebate IV - Remove Billable Patient option to automated
        public void Reg_2122_ReportAdjustmentsRebateRemoveBillablePatientOptionTest()
        {
            logger = extent.CreateTest("Reg_2122_ReportAdjustmentsRebateRemoveBillablePatientOptionTest");
            logger.Log(Status.Info, "Converted manual test case 2122-ROI-Admin-->Report adjustments Rebate IV - Remove Billable Patient option to automated");
            RemoteWebDriver _driver = Init(TestContext.Properties["browser"].ToString());
            ThreadLocal<RemoteWebDriver> localDriver = new ThreadLocal<RemoteWebDriver>();
            localDriver.Value = _driver;
            RemoteWebDriver driver = localDriver.Value;           
            bool isVisible = false;
            try
            {
                ROIMenuSelector menuSelector = new ROIMenuSelector(driver, logger, TestContext);
                Iframe frame = new Iframe(driver, logger, TestContext);

                try
                {
                    menuSelector.SelectRoiAdmin("Financial", "Rebates Report");
                }
                catch (Exception ex)
                {
                    menuSelector.SelectRoiAdminMenuOptions("mnuROIAdmin", "Financial", "Rebates Report");
                }
                frame.SwitchToRoiFrame();
                logger.Log(Status.Info, "Verify that Billable Patient Request Payments has been removed successfully.", TakeScreenShotAtStep());
                ROIAdminRebatesReportPage rebatesReportPage = new ROIAdminRebatesReportPage(driver, logger, TestContext);
                isVisible = rebatesReportPage.IsBillablePatientRequestPaymentsVisible();
                Assert.IsTrue(isVisible, "Failed : Billable Patient Request Payments is visible");
                frame.switchToDefaut();
                logger.Log(Status.Info, "Verified that Billable Patient Request Payments has been removed successfully.");
                LoginPage loginPage = new LoginPage(driver, logger, TestContext);
                loginPage.LogOut();
                //Validate the steps and provide logs.
                Cleanup(driver);
            }
            catch (Exception ex)
            {
                LogException(driver, logger, ex);
            }
        }
    }
}

