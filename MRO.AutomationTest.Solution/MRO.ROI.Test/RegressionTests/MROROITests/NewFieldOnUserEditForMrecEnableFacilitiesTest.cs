using AventStack.ExtentReports;
using DataDrivenProject;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MRO.ROI.Automation.Pages;
using MRO.ROI.Automation.Pages.Common;
using MRO.ROI.Test.Utilities;
using OpenQA.Selenium.Remote;
using System;
using System.IO;
using System.Threading;

namespace MRO.ROI.Test.RegressionTests.MROROITests
{
    [TestClass]
    public class NewFieldOnUserEditForMrecEnableFacilitiesTest:ROIBaseTest
    {       
        public NewFieldOnUserEditForMrecEnableFacilitiesTest() : base(ROITestArea.ROIAdmin)
        {

        }

        [TestMethod]
        [TestCategory(ROITestCategory.Passed)]
        //Converted manual test case 9669-ROI-Admin-->My Char New Field On User Edit For Mrec Enable Facilities to automated
        public void Reg_9669_NewFieldOnUserEditForMrecEnableFacilitiesTest()
        {
            logger = extent.CreateTest("Reg_9669_NewFieldOnUserEditForMrecEnableFacilitiesTest");
            logger.Log(Status.Info, "Converted manual test case 9669-ROI-Admin-->My Char New Field On User Edit For Mrec Enable Facilities to automated");
            RemoteWebDriver _driver = Init(TestContext.Properties["browser"].ToString());
            ThreadLocal<RemoteWebDriver> localDriver = new ThreadLocal<RemoteWebDriver>();
            localDriver.Value = _driver;
            RemoteWebDriver driver = localDriver.Value;
            
            try
            {
                ROIAdminHomePage adminHomePage = new ROIAdminHomePage(driver, logger, TestContext);
                adminHomePage.FacilityList();
                ROIAdminFacilityListPage rOIAdminFacilityListPage = new ROIAdminFacilityListPage(driver, logger, TestContext);
                rOIAdminFacilityListPage.GotoROITestFacilityComputerIcon();
                ROIAdminFacilityWorkSummarypage facilityWorkSummaryPage = new ROIAdminFacilityWorkSummarypage(driver, logger, TestContext);
                facilityWorkSummaryPage.ToSelectListAllUsers();
                ROIFacilityUserListingPage userListingPage = new ROIFacilityUserListingPage(driver, logger, TestContext);
                userListingPage.SearchAndEditUser();
                logger.Log(Status.Info, $"Navigated to Edit User Info page succeesfully", TakeScreenShotAtStep());
                ROIFacilityEditUserInfoPage rOIFacilityEditUserInfoPage = new ROIFacilityEditUserInfoPage(driver, logger, TestContext);
                string epicEmployeeID = rOIFacilityEditUserInfoPage.SetAndGetEpicEmployeeID();
                logger.Log(Status.Info, $"EPIC Employee ID created with id ({epicEmployeeID})",TakeScreenShotAtStep());
                string resultMsg = rOIFacilityEditUserInfoPage.SaveChangesAndGetStatus();
                Assert.AreEqual("User Updated!", resultMsg, "Failed to validate the user updation");
                logger.Log(Status.Pass, "Verified user updated text is visible", TakeScreenShotAtStep());
                logger.Log(Status.Pass, "Remaining steps(9-29)are related to shipment manager, needs to be executed manually");
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
