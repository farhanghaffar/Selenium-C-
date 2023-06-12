using AventStack.ExtentReports;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MRO.ROI.Automation.Pages;
using MRO.ROI.Test.Utilities;
using OpenQA.Selenium.Remote;
using System;
using System.Threading;
using MRO.ROI.Test.ExecutionFactory;
using MRO.ROI.Automation.Common;

namespace MRO.ROI.Test.RegressionTests.MROROITests
{
    [TestClass]
    public class ROIAdminEnterpriseDashboardTest : ROIBaseTest
    {
        public ROIAdminEnterpriseDashboardTest() : base(ROITestArea.ROIFacility)
        {

        }
        [STATestMethodAttribute]
        [TestCategory(ROITestCategory.Passed)]
        //Converted manual test case 1854-ROI-Admin-->Enterprise Dashboard - Scroll Bar Bug to automated
        public void Reg_1854_ROIAdminEnterpriseDashboardTest()
        {
            logger = extent.CreateTest("Reg_1854_ROIAdminEnterpriseDashboardTest");
            logger.Log(Status.Info, "Converted manual test case 1854-ROI-Admin-->Enterprise Dashboard - Scroll Bar Bug to automated");
            RemoteWebDriver _driver = Init(TestContext.Properties["browser"].ToString());
            ThreadLocal<RemoteWebDriver> localDriver = new ThreadLocal<RemoteWebDriver>();
            localDriver.Value = _driver;
            RemoteWebDriver driver = localDriver.Value;           
            try
            {
                ROIMenuSelector selector = new ROIMenuSelector(driver, logger, TestContext);
                Iframe frame = new Iframe(driver, logger, TestContext);
                try
                {
                    selector.SelectRoiAdminMenuOptions("mnuROIFacilityUser", "MRO Analyze", "Enterprise Dashboard");
                }catch(Exception ex)
                {
                    selector.Select("MRO Analyze", "Enterprise Dashboard");
                }
                ROIFacilityEnterpriseDashboardPage rOIFacilityEnterpriseDashboardPage = new ROIFacilityEnterpriseDashboardPage(driver, logger, TestContext);
                logger.Log(Status.Info, "Create Enterprise Dashboard report");
                rOIFacilityEnterpriseDashboardPage.CreateEnterpriseDashboardReportPage();

                logger.Log(Status.Info, "Verify horizontal scroll after pressing restore button", TakeScreenShotAtStep()); ;
                bool _horizontalScrollStatus = rOIFacilityEnterpriseDashboardPage.MinimizeAndVerifyHorizontalScrollStatus();
                Assert.IsTrue(_horizontalScrollStatus, "Failed to minimize and verify horizontal scroll status");
                logger.Log(Status.Pass, "Successfully verified horizontal scroll bar is present on the page", TakeScreenShotAtStep());

                logger.Log(Status.Info, "Make browser window maximize again");
                driver.Manage().Window.Maximize();
                frame.switchToDefaut();
                try
                {
                    selector.SelectRoiAdminMenuOptions("mnuROIFacilityUser", "MRO Analyze", "Enterprise Dashboard");
                }
                catch (Exception ex)
                {
                    selector.Select("MRO Analyze", "Enterprise Dashboard");
                }
                logger.Log(Status.Info, "Create Enterprise Dashboard report");

                rOIFacilityEnterpriseDashboardPage.CreateEnterpriseDashboardReportPage();

                logger.Log(Status.Info, "Verify vertical scroll after pressing restore button", TakeScreenShotAtStep());
                bool _verticalScrollStatus = rOIFacilityEnterpriseDashboardPage.VerifyVerticalScrollStatus();
                Assert.IsTrue(_verticalScrollStatus, "Failed to verify vertical scroll status");
                logger.Log(Status.Pass, "Successfully verified vertical scroll bar is present on the page", TakeScreenShotAtStep());

                Cleanup(driver);
            }
            catch (Exception ex)
            {
                LogException(driver, logger, ex);
            }
        }
    }
}
