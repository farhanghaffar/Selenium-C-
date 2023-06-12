using AventStack.ExtentReports;
using AventStack.ExtentReports.MarkupUtils;
using DataDrivenProject;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MRO.ROI.Automation.Pages;
using MRO.ROI.Automation.Pages.Common;
using MRO.ROI.Automation.Selenium;
using MRO.ROI.Test.ExecutionFactory;
using MRO.ROI.Test.Utilities;
using OpenQA.Selenium.Remote;
using System;
using System.IO;
using System.Threading;
using static MRO.ROI.Automation.Utility.IniFile;

namespace MRO.ROI.Test.RegressionTests.MROROITests
{
    [TestClass]
    public class ROISAMLSSOEditUserPageTest : ROIBaseTest
    {
        public ROISAMLSSOEditUserPageTest() : base(ROITestArea.ROIAdmin)
        {
        }

        [STATestMethodAttribute]
        [TestCategory(ROITestCategory.Passed)]
        // Converted manual test case 5647-ROI-Admin-->SAML/SSO Edit User Page to automated.
        public void Reg_5647_ROISAMLSSOEditUserPageTest()
        {
            logger = extent.CreateTest("Reg_5647_ROISAMLSSOEditUserPageTest");
            logger.Log(Status.Info, "Converted manual test case 5647-ROI-Admin-->SAML/SSO Edit User Page to automated.");
            RemoteWebDriver _driver = Init(TestContext.Properties["browser"].ToString());
            ThreadLocal<RemoteWebDriver> localDriver = new ThreadLocal<RemoteWebDriver>();
            localDriver.Value = _driver;
            RemoteWebDriver driver = localDriver.Value;
            

            try
            {

                string roitUser = IniHelper.ReadConfig("SearchUser", "ROITuser");
                string stLukes = IniHelper.ReadConfig("SearchUser", "MROStLukesLogin");
                ROIAdminHomePage rOIAdminHomePage = new ROIAdminHomePage(driver, logger, TestContext);
                rOIAdminHomePage.SelectFacilityList();
                ROIAdminFacilityListPage rOIAdminFacilityListPage = new ROIAdminFacilityListPage(driver, logger, TestContext);
                rOIAdminFacilityListPage.ClickOnROITFacility();
                
                ROIFacilityInfoROITestFacilityPage rOIFacilityInfoROITestFacilityPage = new ROIFacilityInfoROITestFacilityPage(driver, logger, TestContext);
                             
                rOIFacilityInfoROITestFacilityPage.SelectFacilitySecurity();
                string header = rOIFacilityInfoROITestFacilityPage.VerifyFacilityInfoPageHeader();
                Assert.AreEqual(header, "Facility Security: ROI Test Facility");
                logger.Log(Status.Pass, "Verified Facility security page opened successfully", TakeScreenShotAtStep());

                ROIFacilitySecurityROITestFacilityPage rOIFacilitySecurityROITestFacilityPage = new ROIFacilitySecurityROITestFacilityPage(driver, logger, TestContext);
                bool isSelected=rOIFacilitySecurityROITestFacilityPage.UnCheckEnableSSOCheckBox();
                Assert.IsFalse(isSelected, "SSO checkbox is selected");
                logger.Log(Status.Info, "Verified that  Enable SSO checkbox is unselected", TakeScreenShotAtStep());

                rOIAdminHomePage.SelectFacilityList();
                rOIAdminFacilityListPage.GotoROITestFacilityComputerIcon();
                ROIAdminFacilityWorkSummarypage facilityWorkSummaryPage = new ROIAdminFacilityWorkSummarypage(driver, logger, TestContext);
                facilityWorkSummaryPage.ToSelectListAllUsers();

                ROIFacilityUserListingPage userListingPage = new ROIFacilityUserListingPage(driver, logger, TestContext);
                userListingPage.SearchFacilityUser(roitUser);
                userListingPage.ClickOnSearchData();
                logger.Log(Status.Info, $"Navigated to Edit User Info page succeesfully", TakeScreenShotAtStep());
                ROIFacilityEditUserInfoPage rOIFacilityEditUserInfoPage = new ROIFacilityEditUserInfoPage(driver, logger, TestContext);
                bool isDisplayed=rOIFacilityEditUserInfoPage.VerifyDisableFacilityUserWebLoginIsVisibbleOrNot();
                Assert.IsFalse(isDisplayed, "Failed to verify facility user web login");
                logger.Log(Status.Pass, "Verified that Disable Facility User Web Login is not visible", TakeScreenShotAtStep());

                LoginPage loginPage = new LoginPage(driver, logger, TestContext);
                loginPage.LogOut();

                rOIAdminHomePage.SelectFacilityList();
                rOIAdminFacilityListPage.GoToMROSTLukesFacility();

               
                rOIFacilityInfoROITestFacilityPage.SelectFacilitySecurity();
                string headerText = rOIFacilityInfoROITestFacilityPage.VerifyFacilityInfoPageHeader();
                Assert.AreEqual(headerText, "Facility Security: MRO St Lukes Missouri eLink Test 3.0");
                logger.Log(Status.Info, "Verified Facility security page opened successfully", TakeScreenShotAtStep());

                bool _isSelected = rOIFacilitySecurityROITestFacilityPage.CheckEnableSSOCheckBox();
                Assert.IsTrue(_isSelected, "SSO checkbox is  not selected");
                logger.Log(Status.Info, "Verified that  Enable SSO checkbox is selected", TakeScreenShotAtStep());

                rOIAdminHomePage.SelectFacilityList();
                rOIAdminFacilityListPage.GoToMROSTLukesFacilityComputerIcon();                
                facilityWorkSummaryPage.ToSelectListAllUsers();               
                userListingPage.SearchFacilityUser(stLukes);
                userListingPage.ClickOnSearchData();
                logger.Log(Status.Info, $"Navigated to Edit User Info page succeesfully", TakeScreenShotAtStep());
               
                bool _isDisplayed = rOIFacilityEditUserInfoPage.VerifyDisableFacilityUserWebLoginIsVisibbleOrNot();
                Assert.IsTrue(_isDisplayed, "Failed to verify facility user web login");
                logger.Log(Status.Pass, "Verified that Disable Facility User Web Login  visible", TakeScreenShotAtStep());

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

