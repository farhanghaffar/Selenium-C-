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
    public class ROICanAdministerUsersPermissionChangesHasUserReportingTest : ROIBaseTest
    {
        public ROICanAdministerUsersPermissionChangesHasUserReportingTest() : base(ROITestArea.ROIAdmin)
        {
        }

        [STATestMethodAttribute]
        [TestCategory(ROITestCategory.Passed)]
        // Converted manual test case 14386-ROI-Admin-->Can Administer Users-Permission Changes-Has User Reporting to automated.
        public void Reg_14386_ROICanAdministerUsersPermissionChangesHasUserReportingTest()
        {
            logger = extent.CreateTest("Reg_14386_ROICanAdministerUsersPermissionChangesHasUserReportingTest");
            logger.Log(Status.Info, "Converted manual test case 14386-ROI-Admin-->Can Administer Users-Permission Changes-Has User Reporting to automated");
            RemoteWebDriver _driver = Init(TestContext.Properties["browser"].ToString());
            ThreadLocal<RemoteWebDriver> localDriver = new ThreadLocal<RemoteWebDriver>();
            localDriver.Value = _driver;
            RemoteWebDriver driver = localDriver.Value;

            try
            {

                ROIAdminHomePage rOIAdminHomePage = new ROIAdminHomePage(driver, logger, TestContext);

                rOIAdminHomePage.SelectFacilityList();
                ROIAdminFacilityListPage rOIAdminFacilityListPage = new ROIAdminFacilityListPage(driver, logger, TestContext);
                rOIAdminFacilityListPage.GoToMROARTestFacilityForROIAdminUsers();
                Iframe frame = new Iframe(driver, logger, TestContext);
                frame.switchToDefaut();
                ROIAdminFacilityWorkSummarypage rOIAdminFacilityWorkSummaryPage = new ROIAdminFacilityWorkSummarypage(driver, logger, TestContext);
                rOIAdminFacilityWorkSummaryPage.ToSelectListAllUsers();

                ROIFacilityUserListingPage rOIFacilityUserListingPage = new ROIFacilityUserListingPage(driver, logger, TestContext);
                rOIFacilityUserListingPage.SearchMROARTFFacilityUser();
                logger.Log(Status.Info, "Verified that entered loginId returns the data", TakeScreenShotAtStep());

                rOIFacilityUserListingPage.ClickOnSearchData();
                ROIFacilityEditUserInfoPage rOIFacilityEditUserInfoPage = new ROIFacilityEditUserInfoPage(driver, logger, TestContext);

                rOIFacilityEditUserInfoPage.VerifyMROEmployeeChkBoxisCheckedOrNot();

                rOIFacilityEditUserInfoPage.UncheckUserAdminCheckBox();

                logger.Log(Status.Info, "Verified that user updated text is displayed", TakeScreenShotAtStep());

                rOIAdminHomePage.SwitchToNewTabAndLoginROIFacility(BaseAddress);
                ROIFacilityWorkSummaryPage rOIFacilityWorkSummaryPage = new ROIFacilityWorkSummaryPage(driver, logger, TestContext);
                bool isDisplayed = rOIFacilityWorkSummaryPage.VerifyAddNewUser();
                rOIFacilityWorkSummaryPage.ToSelectListAllUsers();

                rOIFacilityUserListingPage.SearchMROARTFFacilityUser();
                rOIFacilityUserListingPage.ClickOnSearchData();
                bool isAdminChkBox = rOIFacilityEditUserInfoPage.VerifyCanAdminUserCheckbox();
                Assert.IsFalse(isAdminChkBox, "Can admin user check box is present");
                logger.Log(Status.Pass, "Succesfully verified that can admin users check box is not displayed", TakeScreenShotAtStep());
                bool isDisabled = rOIFacilityEditUserInfoPage.CheckSaveChangesDisabled();
                Assert.IsTrue(isDisabled, "Save changes button is enabled");
                logger.Log(Status.Pass, "Succesfully verified that save changes button is disabled", TakeScreenShotAtStep());

                rOIAdminHomePage.SwitchToPreviousTab(BaseAddress);
                rOIAdminHomePage.SelectFacilityList();
                rOIAdminFacilityListPage.GoToMROARTestFacilityForROIAdminUsers();
                rOIAdminFacilityWorkSummaryPage.ToSelectListAllUsers();
                rOIFacilityUserListingPage.SearchMROARTFFacilityUser();
                rOIFacilityUserListingPage.ClickOnSearchData();
                rOIFacilityEditUserInfoPage.CheckUserAdminCheckBox();
                logger.Log(Status.Info, "Verified that user updated text is displayed", TakeScreenShotAtStep());

                rOIAdminHomePage.SwitchBackToFacilitySide(BaseAddress);
                bool _isDisplayed = rOIFacilityWorkSummaryPage.VerifyAddNewUser();
                Assert.IsTrue(_isDisplayed, "Add a new user is not displayed");
                logger.Log(Status.Pass, "Successfully verified add a new user is displayed", TakeScreenShotAtStep());
                rOIFacilityWorkSummaryPage.ToSelectListAllUsers();

                rOIFacilityUserListingPage.SearchMROARTFFacilityUser();
                rOIFacilityUserListingPage.ClickOnSearchData();
                bool _isAdminChkBox = rOIFacilityEditUserInfoPage.VerifyCanAdminUserCheckbox();
                Assert.IsTrue(_isAdminChkBox, "Can admin user check box is  not present");
                logger.Log(Status.Pass, "Succesfully verified that can admin users check box is  displayed", TakeScreenShotAtStep());
                bool _isDisabled = rOIFacilityEditUserInfoPage.CheckSaveChangesDisabled();
                Assert.IsFalse(_isDisabled, "Save changes button is disabled");
                logger.Log(Status.Pass, "Succesfully verified that save changes button is enabled", TakeScreenShotAtStep());

                rOIFacilityEditUserInfoPage.UnCheckMROEmployeeChkBox();
                bool isCanAdminChkBox = rOIFacilityEditUserInfoPage.VerifyCanAdminUserCheckbox();
                Assert.IsFalse(isCanAdminChkBox, "Can admin user check box is present");
                logger.Log(Status.Pass, "Succesfully verified that can admin users check box is not displayed", TakeScreenShotAtStep());

                rOIFacilityEditUserInfoPage.CheckMROEmployeeChkBox();


                Cleanup(driver);
            }

            catch (Exception ex)
            {
                LogException(driver, logger, ex);

            }
        }
    }

}

