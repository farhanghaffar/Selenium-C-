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
    public class ROIAdminDrillInUserPermissionPatientLookupTest : ROIBaseTest
    {
        public ROIAdminDrillInUserPermissionPatientLookupTest() : base(ROITestArea.ROIAdmin)
        {
        }

        [STATestMethodAttribute]
        [TestCategory(ROITestCategory.Passed)]
        // Converted manual test case 4661-ROI-Admin-->Admin/Drill in user permission- Patient lookup to automated.
        public void Reg_4661_ROIAdminDrillInUserPermissionPatientLookupTest()
        {
            logger = extent.CreateTest("Reg_4661_ROIAdminDrillInUserPermissionPatientLookupTest");
            logger.Log(Status.Info, "Converted manual test case 4661-ROI-Admin-->Admin/Drill in user permission- Patient lookup to automated");
            RemoteWebDriver _driver = Init(TestContext.Properties["browser"].ToString());
            ThreadLocal<RemoteWebDriver> localDriver = new ThreadLocal<RemoteWebDriver>();
            localDriver.Value = _driver;
            RemoteWebDriver driver = localDriver.Value;

            try
            {

                ROIAdminHomePage rOIAdminHomePage = new ROIAdminHomePage(driver, logger, TestContext);               
                rOIAdminHomePage.SelectFeatures();
                ROIAdminFeaturesPage rOIAdminFeaturesPage = new ROIAdminFeaturesPage(driver, logger, TestContext);
                string contextVal = rOIAdminFeaturesPage.VerifySelectedContext();
                Assert.AreEqual(contextVal, "Admin");
                logger.Log(Status.Info, "Verified context set to Admin", TakeScreenShotAtStep());

                rOIAdminFeaturesPage.ClickOnFacilityPatientLookUp();
                rOIAdminFeaturesPage.AddFacilityPatientLookUp("Add");               
                logger.Log(Status.Info, "Verified user added for facility patient lookup", TakeScreenShotAtStep());
                rOIAdminFeaturesPage.ClickOnCatagories();

                rOIAdminHomePage.SelectFacilityList();
                ROIAdminFacilityListPage rOIAdminFacilityListPage = new ROIAdminFacilityListPage(driver, logger, TestContext);
                rOIAdminFacilityListPage.ClickOnROITCompIcon();
                ROIFacilityWorkSummaryPage rOIFacilityWorkSummaryPage = new ROIFacilityWorkSummaryPage(driver, logger, TestContext);
                
                rOIFacilityWorkSummaryPage.SelectPatientLookup();

                string header=rOIFacilityWorkSummaryPage.VerifyHeader();
                Assert.AreEqual(header, "Patient Lookup");
                logger.Log(Status.Pass, "Successfully verified patient lookup window opened", TakeScreenShotAtStep());

                LoginPage loginPage = new LoginPage(driver, logger, TestContext);
                loginPage.LogOut();

                rOIAdminHomePage.SelectFeatures();
                rOIAdminFeaturesPage.VerifySelectedContext();
                rOIAdminFeaturesPage.ClickOnFacilityPatientLookUp();
                rOIAdminFeaturesPage.AddFacilityPatientLookUp("Remove");
                logger.Log(Status.Info, "Verified user removed for facility patient lookup", TakeScreenShotAtStep());
                rOIAdminFeaturesPage.ClickOnCatagories();

                rOIAdminHomePage.SelectFacilityList();
                rOIAdminFacilityListPage.ClickOnROITCompIcon();
                bool isDisplayed = rOIFacilityWorkSummaryPage.VerifyPatientLookup();
                Assert.IsFalse(isDisplayed, "Patient lookup  displayed");
                logger.Log(Status.Info, "Verified that  patient lookup is not visible under roi requests", TakeScreenShotAtStep());                
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

