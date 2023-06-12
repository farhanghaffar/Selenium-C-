using AventStack.ExtentReports;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MRO.ROI.Automation.Common;
using MRO.ROI.Automation.Pages;
using MRO.ROI.Test.ExecutionFactory;
using MRO.ROI.Test.Utilities;
using OpenQA.Selenium.Remote;
using System;
using System.Threading;

namespace MRO.ROI.Test.RegressionTests.MROROITests
{
    [TestClass]
    public class ROIAdminRemoteStatusRequiredFieldforMROEmployeeTest : ROIBaseTest
    {
        public ROIAdminRemoteStatusRequiredFieldforMROEmployeeTest() : base(ROITestArea.ROIAdmin)
        {
        }

        [STATestMethodAttribute]
        [TestCategory(ROITestCategory.Passed)]
        // Converted manual test case 10645-ROI-Admin-->Remote Status - Required field for MRO Employee to automated
        public void Reg_10645_ROIAdminRemoteStatusUpdate()
        {
            logger = extent.CreateTest("Reg_10645_ROIAdminRemoteStatusUpdate");
            logger.Log(Status.Info, "Converted manual test case 10645-ROI-Admin-->Remote Status - Required field for MRO Employee to automated");
            RemoteWebDriver _driver = Init(TestContext.Properties["browser"].ToString());
            ThreadLocal<RemoteWebDriver> localDriver = new ThreadLocal<RemoteWebDriver>();
            localDriver.Value = _driver;
            RemoteWebDriver driver = localDriver.Value;

            try
            {
                ROIAdminHomePage rOIAdminHomePage = new ROIAdminHomePage(driver, logger, TestContext);
                ROIAdminAddNewUserPage rOIAdminAddNewUserPage = new ROIAdminAddNewUserPage(driver, logger, TestContext);

                Iframe frame = new Iframe(driver, logger, TestContext);
                rOIAdminHomePage.SelectFacilityList();
                ROIAdminFacilityListPage rOIAdminFacilityListPage = new ROIAdminFacilityListPage(driver, logger, TestContext);
                rOIAdminFacilityListPage.GotoROITestFacilityComputerIcon();
                ROIAdminFacilityWorkSummarypage rOIAdminFacilityWorkSummarypage = new ROIAdminFacilityWorkSummarypage(driver, logger, TestContext);
                ROIMenuSelector selector = new ROIMenuSelector(driver, logger, TestContext);
                try
                {
                    selector.SelectRoiAdminMenuOptions("mnuROIFacilityUser", "Users", "List All Users");
                }
                catch (Exception)
                {
                    selector.Select("Users", "List All Users");
                }

                ROIAdminUserListingPage rOIAdminUserListingPage = new ROIAdminUserListingPage(driver, logger, TestContext);
                rOIAdminUserListingPage.SearchUser();
                rOIAdminUserListingPage.ClickUser();
                logger.Log(Status.Info, "Searched and selected the user to update", TakeScreenShotAtStep());
                ROIAdminEditUserInfoPage rOIAdminEditUserInfoPage = new ROIAdminEditUserInfoPage(driver, logger, TestContext);
                logger.Log(Status.Info, "Click the MRO Employee check box to check the MRO Employee box.");
                rOIAdminEditUserInfoPage.CheckMROEmployee();

                logger.Log(Status.Info, "Verify User type and Remote Status display at bottom of page.");
                bool isUserTypeDroDownShowing = rOIAdminAddNewUserPage.IsUserTypeDropdownIsShowing();
                Assert.IsTrue(isUserTypeDroDownShowing, "Failed : User type dropdown is not showing");
                logger.Log(Status.Pass, "Verified User type displaying at the bottom of page");

                bool isRemoteStatusDropdownIsShowing = rOIAdminAddNewUserPage.IsRemoteStatusDropdownIsShowing();
                Assert.IsTrue(isUserTypeDroDownShowing, "Failed : User type dropdown is not showing");
                logger.Log(Status.Pass, "Verified Remote Status displaying at the bottom of page");

                logger.Log(Status.Info, "Select Remote services in the User type dropdown with Remote status as blank. Then click the Save Changes button");

                logger.Log(Status.Info, "Verify alert is showing");
                bool isAlertPresent = rOIAdminEditUserInfoPage.SetUserTypeAsRemoteServicesAndRemoteStatusEmpty();
                Assert.IsTrue(isAlertPresent, "Failed to verify alert is showing");
                logger.Log(Status.Pass, "Verified alert is showing");
                logger.Log(Status.Info, "Verify A Message window appears displaying the message 'Remote Status is a required field for this User Type!'");

                string getText = driver.SwitchTo().Alert().Text;
                Assert.AreEqual("Remote Status is a required field for this User Type!", getText, "Failed : Alert message 'Remote Status is a required field for this User Type!' didn't matched");
                driver.SwitchTo().Alert().Accept();
                logger.Log(Status.Pass, "Verified A Message window appears displaying the message 'Remote Status is a required field for this User Type!'");

                logger.Log(Status.Info, "Select 'Floater' in the User Type Dropdown with Remote status as 'Remote Full-Time'. Then click the Save Changes button");
                rOIAdminEditUserInfoPage.SetUserTypeAsFloaterAndRemoteStatusAsFullTime();
                logger.Log(Status.Info, "Verify 'User Updated!' Message displays at bottom of page and the Change to user has been saved.", TakeScreenShotAtStep());

                string userUpdated = rOIAdminEditUserInfoPage.GetFacilityUserAddedText();
                Assert.AreEqual(userUpdated, "User Updated!", "Failed : 'User Updated!' Message is not displaying at bottom of page and the Change to user has been saved.");
                logger.Log(Status.Pass, "Updated the user with user type=floater and remote status =fulltime", TakeScreenShotAtStep());

                logger.Log(Status.Info, "Select Implementation in the User Type Dropdown with Remote status as blank. Then click the Save Changes button");
                logger.Log(Status.Info, "Verify 'User Updated!' Message displays at bottom of page and the Change to user has been saved.", TakeScreenShotAtStep());
                rOIAdminEditUserInfoPage.SetUserTypeAsImplementationAndRemoteStatusBlank();
                string userUpdatedforRemoteStatusBlank = rOIAdminEditUserInfoPage.GetFacilityUserAddedText();
                Assert.AreEqual(userUpdated, "User Updated!");
                logger.Log(Status.Pass, "Updated the user with user type=Implementation and remote status =[]", TakeScreenShotAtStep());

                logger.Log(Status.Info, "Click the MRO Employee check box to uncheck the MRO Employee box. and click the Save Changes button.");
                rOIAdminEditUserInfoPage.UncheckMROEmployeeAndSave();

                logger.Log(Status.Info, "Verify User type and Remote Status removed from the bottom of page and that the user was updated.");
                isUserTypeDroDownShowing = rOIAdminAddNewUserPage.IsUserTypeDropdownIsShowing();
                Assert.IsFalse(isUserTypeDroDownShowing, "Failed : User type dropdown is showing");
                logger.Log(Status.Pass, "Verified User type removed from the bottom of page");

                isRemoteStatusDropdownIsShowing = rOIAdminAddNewUserPage.IsRemoteStatusDropdownIsShowing();
                Assert.IsFalse(isUserTypeDroDownShowing, "Failed : User type dropdown is showing");
                logger.Log(Status.Pass, "Verified Remote Status removed from the bottom of page");

                frame.switchToDefaut();
                try
                {
                    selector.SelectRoiAdminMenuOptions("mnuROIFacilityUser", "Users", "List All Users");
                }
                catch (Exception)
                {
                    selector.Select("Users", "List All Users");
                }

                try
                {
                    frame.SwitchToRoiFrame();

                }
                catch (Exception)
                {

                }
                rOIAdminUserListingPage.ClickAddUser();
                rOIAdminAddNewUserPage.CheckMROEmployee();
                rOIAdminAddNewUserPage.SetUpUser();

                isAlertPresent = false;
                logger.Log(Status.Info, "Select Field staff in the User Type Dropdown with Remote status as blank.");
                isAlertPresent = rOIAdminAddNewUserPage.SetUserTypeAsFieldStaffAndRemoteStatusBlankAndAddUser();
                logger.Log(Status.Info, "Verify alert is showing");
                Assert.IsTrue(isAlertPresent, "Failed to verify alert is showing");
                logger.Log(Status.Pass, "Verified alert is showing");
                logger.Log(Status.Info, "Verify A Message window appears displaying the message 'Remote Status is a required field for this User Type!'");
                getText = "";
                getText = driver.SwitchTo().Alert().Text.Trim();
                Assert.AreEqual("Remote Status is a required field for this User Type!", getText, "Failed : Alert message 'Remote Status is a required field for this User Type!' didn't matched");
                driver.SwitchTo().Alert().Accept();
                logger.Log(Status.Pass, "Verified A Message window appears displaying the message 'Remote Status is a required field for this User Type!'");

                logger.Log(Status.Info, "Select Field staff in the User Type Dropdown with Remote status as On Site Full-Time. Then click the Add user button.");
                rOIAdminAddNewUserPage.SetUserTypeAsFieldStaffAndRemoteStatusAsFullTimeAndAddUser();
                string userAdded = rOIAdminAddNewUserPage.GetFacilityUserAddedText();
                Assert.AreEqual(userAdded, "User added", "Failed : 'User Updated!' Message is not displaying at bottom of page and the Change to user has been saved.");
                logger.Log(Status.Pass, "Succesfylly verified remote status is required field for mro employee", TakeScreenShotAtStep());
                Cleanup(driver);
            }
            catch (Exception ex)
            {
                LogException(driver, logger, ex);
            }
        }
    }
}
