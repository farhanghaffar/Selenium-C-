using AventStack.ExtentReports;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MRO.ROI.Automation.Pages;
using MRO.ROI.Test.Utilities;
using OpenQA.Selenium.Remote;
using System;
using System.Threading;
using MRO.ROI.Test.ExecutionFactory;
using MRO.ROI.Automation.Pages.Common;

namespace MRO.ROI.Test.RegressionTests.MROROITests
{
    [TestClass]
    public class WorkSummaryBannerDecoupledFromLogiTest : ROIBaseTest
    {
        public WorkSummaryBannerDecoupledFromLogiTest() : base(ROITestArea.ROIAdmin)
        {

        }
        [STATestMethodAttribute]
        [TestCategory(ROITestCategory.Regression)]
        //Converted manual test case 2093 -ROI-Admin--> Work Summary Banner De-Coupled From Logi Should Load Asychonously From The Logi Report to automated
        public void Reg_2093_WorkSummaryBannerDecoupledFromLogiTest()
        {
            logger = extent.CreateTest("Reg_2093_WorkSummaryBannerDecoupledFromLogiTest");
            logger.Log(Status.Info,"Converted manual test case 2093 -ROI-Admin--> Work Summary Banner De-Coupled From Logi Should Load Asychonously From The Logi Report to automated");
            RemoteWebDriver _driver = Init(TestContext.Properties["browser"].ToString());
            ThreadLocal<RemoteWebDriver> localDriver = new ThreadLocal<RemoteWebDriver>();
            localDriver.Value = _driver;
            RemoteWebDriver driver = localDriver.Value;
            string marqueeText = string.Empty;
            try
            {
                ROIAdminHomePage rOIAdminHomePage = new ROIAdminHomePage(driver, logger, TestContext);
                rOIAdminHomePage.SelectSystemInfo();

                ROISystemInfoPage rOISystemInfoPage = new ROISystemInfoPage(driver, logger, TestContext);
                rOISystemInfoPage.ClickWorkSummaryMarquee();
                logger.Log(Status.Info, "Clicked on Work Summary Marquee Link", TakeScreenShotAtStep());
                rOISystemInfoPage.EditWorkSummaryMarqueeInformation();
                logger.Log(Status.Info, "Edited Work Summary Marquee information", TakeScreenShotAtStep());
                marqueeText = rOISystemInfoPage.GetMarqueeText();
                Assert.AreEqual("Testing Marquee", marqueeText, "Failed to validate updated marquee text");
                logger.Log(Status.Pass, "Verified Marquee text and tag is displayed and working as expected", TakeScreenShotAtStep());
                rOISystemInfoPage.ClearMarquee();
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

