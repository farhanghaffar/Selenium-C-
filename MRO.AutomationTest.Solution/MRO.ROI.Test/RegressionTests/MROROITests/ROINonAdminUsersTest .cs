using AventStack.ExtentReports;
using AventStack.ExtentReports.MarkupUtils;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MRO.ROI.Automation.Common;
using MRO.ROI.Automation.Pages;
using MRO.ROI.Automation.Selenium;
using MRO.ROI.Test.ExecutionFactory;
using MRO.ROI.Test.Utilities;
using OpenQA.Selenium.Remote;
using System;
using System.Threading;

namespace MRO.ROI.Test.RegressionTests.MROROITests
{
    [TestClass]
    public class ROIPermissionsToCreateNonAdminUsersTest : ROIBaseTest
    {
        public ROIPermissionsToCreateNonAdminUsersTest() : base(ROITestArea.ROIAdmin)
        {

        }

        [STATestMethodAttribute]
        [TestCategory(ROITestCategory.Regression)]
        // Converted manual test case 11838-ROI-Admin-->Permissions to Create Non-Admin Users to automated.
        public void Reg_11838_PermissionsToCreateNonAdminUsersTest()
        {
            logger = extent.CreateTest("Reg_11838_PermissionsToCreateNonAdminUsersTest");
            logger.Log(Status.Info, "Converted manual test case 11838-ROI-Admin-->Permissions to Create Non-Admin Users to automated");
            RemoteWebDriver _driver = Init(TestContext.Properties["browser"].ToString());
            ThreadLocal<RemoteWebDriver> localDriver = new ThreadLocal<RemoteWebDriver>();
            localDriver.Value = _driver;
            RemoteWebDriver driver = localDriver.Value;

            try
            {

                ROIAdminHomePage adminHomePage = new ROIAdminHomePage(driver, logger, TestContext);
                ROIAdminFacilityListPage adminFacilityListPage = new ROIAdminFacilityListPage(driver, logger, TestContext);

                Iframe frame = new Iframe(driver, logger, TestContext);

                adminHomePage.SelectFacilityList();
                adminFacilityListPage.GotoROITestFacilityComputerIcon();
                ROIMenuSelector selector = new ROIMenuSelector(driver, logger, TestContext);

                ROIAdminFacilityWorkSummarypage facilityWorkSummaryPage = new ROIAdminFacilityWorkSummarypage(driver, logger, TestContext);
                try
                {
                    selector.Select("Users", "List All Users");
                }
                catch (Exception ex)
                {
                    facilityWorkSummaryPage.ToSelectListAllUsers();
                }
                ROIFacilityUserListingPage userListingPage = new ROIFacilityUserListingPage(driver, logger, TestContext);
                frame.SwitchToRoiFrame();
                userListingPage.EditLoginUser();
                ROIFacilityEditUserInfoPage facilityEditUserInfoPage = new ROIFacilityEditUserInfoPage(driver, logger, TestContext);
                facilityEditUserInfoPage.CheckMROEmployeeChkBox(true);
                facilityEditUserInfoPage.CheckHasUserReportingCheckbox(false);
                facilityEditUserInfoPage.UncheckUserAdminCheckBox();
                logger.Log(Status.Info, "User admin checkbox unchecked", TakeScreenShotAtStep());
                logger.Log(Status.Info, "Verify Users menu option is not visible", TakeScreenShotAtStep());
                bool isUsersMenuNotAvailable = adminHomePage.SwitchToNewTabAndLoginROIFacility(BaseAddress);
                Assert.IsFalse(isUsersMenuNotAvailable, "Failed : Users menu option is visible");
                logger.Log(Status.Pass, "Users menu option is not displayed");

                adminHomePage.OpenNewTabAndLoginROIFacility(BaseAddress);
                try
                {
                    selector.Select("Users", "List All Users");
                }
                catch (Exception ex)
                {
                    facilityWorkSummaryPage.ToSelectListAllUsers();
                }

                frame.SwitchToRoiFrame();

               /* adminHomePage.SelectFacilityList();
                adminFacilityListPage.GoToMROARTestFacilityForROIAdminUsers();
                facilityWorkSummaryPage.ToSelectListAllUsers();*/
                userListingPage.EditLoginUser();

                facilityEditUserInfoPage.CheckMROEmployeeChkBox(true);
                facilityEditUserInfoPage.CheckHasUserReportingCheckbox(true);
                facilityEditUserInfoPage.CheckUserAdminCheckBox();
                logger.Log(Status.Info, "User admin checkbox checked", TakeScreenShotAtStep());
                adminHomePage.SwitchBackToFacilitySide(BaseAddress);
                logger.Log(Status.Info, "Verify Users menu option is visible", TakeScreenShotAtStep());
                bool isUsersMenuAvailable = facilityWorkSummaryPage.RefreshFacilityWorkSummaryPage();
                Assert.IsTrue(isUsersMenuAvailable, "Failed : Users menu option is not visible");
                logger.Log(Status.Pass, "User menu option is displayed");
                Cleanup(driver);
            }
            catch (Exception ex)
            {
                LogException(driver, logger, ex);

            }
        }
    }

}

