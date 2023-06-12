using AventStack.ExtentReports;
using AventStack.ExtentReports.MarkupUtils;
using Microsoft.VisualStudio.TestTools.UnitTesting;
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
    public class TATReportEnhancementTest: ROIBaseTest
    {
        public TATReportEnhancementTest() : base(ROITestArea.ROIFacility)
        {
        }
        [STATestMethodAttribute]
        [TestCategory(ROITestCategory.Passed)]
        //Converted manual test case 1411-ROI-Admin--> TAT report enhancement Facility to automated.
        public void Reg_1411_TATReportEnhancementTest()
        {
            logger = extent.CreateTest($"Reg_1411_TATReportEnhancementTest");
            logger.Log(Status.Info, "Converted manual test case 1411-ROI-Admin--> TAT report enhancement Facility to automated");
            RemoteWebDriver _driver = Init(TestContext.Properties["browser"].ToString());
            ThreadLocal<RemoteWebDriver> localDriver = new ThreadLocal<RemoteWebDriver>();
            localDriver.Value = _driver;
            RemoteWebDriver driver = localDriver.Value;

            try
            {
                ROIAdminHomePage rOIAdminHomePage = new ROIAdminHomePage(driver, logger, TestContext);
                ROIMenuSelector selector = new ROIMenuSelector(driver, logger, TestContext);

                //rOIAdminHomePage.SelectFacilityList();
                ROIAdminFacilityListPage rOIAdminFacilityListPage = new ROIAdminFacilityListPage(driver, logger, TestContext);
              //  rOIAdminFacilityListPage.GoToROITestFacility();
                ROIFacilityWorkSummaryPage rOIFacilityWorkSummaryPage = new ROIFacilityWorkSummaryPage(driver, logger, TestContext);
                rOIFacilityWorkSummaryPage.GoToMROAnalyseSelectTurnAroundReport();
                ROIFacilityTurnaroundReportPage rOIFacilityTurnaroundReportPage = new ROIFacilityTurnaroundReportPage(driver, logger, TestContext);
                rOIFacilityTurnaroundReportPage.CheckShippedRadioButton();
                rOIFacilityTurnaroundReportPage.ApplyFiltersWithFacilityAndCreateReport();
                logger.Log(Status.Info, "Turnaround report created as per the first set of filters",TakeScreenShotAtStep());
                string firstheadingText = rOIFacilityTurnaroundReportPage.GetHeaderTextAsPerFilters();
                logger.Log(Status.Info, $"Verifying heading text ({firstheadingText}) as per the report filters", TakeScreenShotAtStep());
                string startDate = "05/04/2017";
                string dateCriteria = "Logged";
                string user = "Business Days";
                string location = IniHelper.ReadConfig("TurnaroundReportFilters", "TurnaroundReportFiltersNewLocation");
                string userType = IniHelper.ReadConfig("TurnaroundReportFilters", "UserType");
                Assert.IsTrue(firstheadingText.Contains(startDate), "Failed : Date range didn't matched : " + startDate);
                Assert.IsTrue(firstheadingText.Contains(dateCriteria), "Failed : Date criteria didn't matched : " + dateCriteria);
                Assert.IsTrue(firstheadingText.Contains(location), "Failed : location didn't matched : " + location);
                Assert.IsTrue(firstheadingText.Contains(userType), "Failed : user type didn't matched : " + userType);
                Assert.IsTrue(firstheadingText.Contains(user), "Failed : user didn't matched : " + user);
                logger.Log(Status.Pass, $"Verified heading text ({firstheadingText}) as per the report filters");
                rOIFacilityTurnaroundReportPage.ClickOnCustomize();
                rOIFacilityTurnaroundReportPage.ApplyFiltersWithReleaseOptionAndCreateReport();
                string dateCriteriaRelease = "Released";
                string releaseStartDate = "01/02/2020";
                logger.Log(Status.Info, "Turnaround report created as per the second set of filters", TakeScreenShotAtStep());
                string secondHeadingText = rOIFacilityTurnaroundReportPage.GetHeaderTextAsPerFilters();
                logger.Log(Status.Info, $"Verifying heading text ({secondHeadingText}) as per the report filters", TakeScreenShotAtStep());
                Assert.IsTrue(secondHeadingText.Contains(releaseStartDate), "Failed : Date range didn't matched : " + releaseStartDate);
                Assert.IsTrue(secondHeadingText.Contains(dateCriteriaRelease), "Failed : Date criteria didn't matched : " + dateCriteriaRelease);
                Assert.IsTrue(secondHeadingText.Contains(location), "Failed : location didn't matched : " + location);
                Assert.IsTrue(secondHeadingText.Contains(userType), "Failed : user type didn't matched : " + userType);
                Assert.IsTrue(secondHeadingText.Contains(user), "Failed : user didn't matched : " + user);

                logger.Log(Status.Pass, $"Verified heading text ({secondHeadingText}) as per the report filters");
                driver.SwitchToDefaultContent();
                selector.ClickLogoutIcon();
                Cleanup(driver);
            }
            catch (Exception ex)
            {
                LogException(driver, logger, ex);
            }

        }
    }
}
