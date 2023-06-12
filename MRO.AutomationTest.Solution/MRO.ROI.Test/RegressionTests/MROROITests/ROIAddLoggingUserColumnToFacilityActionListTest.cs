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
    public class ROIAddLoggingUserColumnToFacilityActionListTest : ROIBaseTest
    {
        public ROIAddLoggingUserColumnToFacilityActionListTest() : base(ROITestArea.ROIAdmin)
        {

        }

        [STATestMethodAttribute]
        [TestCategory(ROITestCategory.Regression)]
        // Converted manual test case 4311-ROI-Facility--> Add ‘Logging User’ Column to Facility Action Listto automated.
        public void Reg_4311_ROIAddLoggingUserColumnToFacilityActionListTest()
        {
            logger = extent.CreateTest("4311_ROIAddLoggingUserColumnToFacilityActionListTest");
            logger.Log(Status.Info, "Converted manual test case 4311-ROI-Facility--> Add ‘Logging User’ Column to Facility Action List to automated.");
            RemoteWebDriver _driver = Init(TestContext.Properties["browser"].ToString());
            ThreadLocal<RemoteWebDriver> localDriver = new ThreadLocal<RemoteWebDriver>();
            localDriver.Value = _driver;
            RemoteWebDriver driver = localDriver.Value;

            try
            {

                string actionMsg = IniHelper.ReadConfig("ROIAddLoggingUserColumnToFacilityActionListTest", "NoAuthorization");
                ROIAdminHomePage rOIAdminHomePage = new ROIAdminHomePage(driver, logger, TestContext);
                rOIAdminHomePage.SelectFacilityList();
                ROIAdminFacilityListPage roiAdminFacilityListPage = new ROIAdminFacilityListPage(driver, logger, TestContext);
                roiAdminFacilityListPage.GoToROITestFacility();

                ROIFacilityWorkSummaryPage rOIFacilityWorkSummaryPage = new ROIFacilityWorkSummaryPage(driver, logger, TestContext);
                rOIFacilityWorkSummaryPage.ClickOnAddNewUser();
                ROIAdminAddNewUserPage rOIAdminAddNewUserPage = new ROIAdminAddNewUserPage(driver, logger, TestContext);
                rOIAdminAddNewUserPage.CheckMROEmployee();
                string userId=rOIAdminAddNewUserPage.CreateNewFacilityUser();
                string pwd=rOIAdminAddNewUserPage.GetTemporaryPassword();

                
                rOIAdminHomePage.OpenNewTabAndLoginAsNewFacilityUser(BaseAddress, userId, pwd);
                ROIChangePasswordPage rOIChangePasswordPage = new ROIChangePasswordPage(driver, logger, TestContext);
                rOIChangePasswordPage.changePassword(pwd,"TestingMRO@123", "TestingMRO@123");

                rOIAdminHomePage.SwitchToPreviousTab(BaseAddress);
                rOIAdminHomePage.SelectFacilityList();
                roiAdminFacilityListPage.GoToROITestFacility();
                rOIFacilityWorkSummaryPage.GoToLogNewRequestPage();               
                ROIFacilityLogNewRequestPage rOIFacilityLogNewRequestPage = new ROIFacilityLogNewRequestPage(driver, logger, TestContext);
                rOIFacilityLogNewRequestPage.CreateNewMRODeliveryRequestForBostonProper();

                ROIFacilityRequestStatusPage rOIFacilityRequestStatusPage = new ROIFacilityRequestStatusPage(driver, logger, TestContext);
                string requestid = rOIFacilityRequestStatusPage.GetRequestID();
                logger.Log(Status.Info, $"MRO delivery request created with id ({requestid})");
                rOIFacilityRequestStatusPage.ImportPdfFiles();
                rOIFacilityRequestStatusPage.ReleaseRequestID();

                rOIFacilityRequestStatusPage.GoToActionList();
                ROIFacilityActionListPage rOIFacilityActionListPage = new ROIFacilityActionListPage(driver, logger, TestContext);
                rOIFacilityActionListPage.CreateReport();
                bool isDisplayed = rOIFacilityActionListPage.VerifyRequestId(requestid);
                Assert.IsFalse(isDisplayed, "Request id present");
                logger.Log(Status.Pass, "Successfully verified request id not exist in the  action list report page", TakeScreenShotAtStep());
                rOIFacilityRequestStatusPage.SelectRecentRequest();
                rOIFacilityRequestStatusPage.SelectActionMessage();
                string msg = rOIFacilityRequestStatusPage.VerifyActionMessage();
                Assert.AreEqual(msg, actionMsg, "Failed to verify action message");
                logger.Log(Status.Pass, $"Successfully verified action message created with name:{(actionMsg)} and  system date and time", TakeScreenShotAtStep());

                
                rOIAdminHomePage.OpenNewTabAndLoginROIDummyTestingFacilityUser(BaseAddress,userId);
                rOIFacilityWorkSummaryPage.GoToLogNewRequestPage();
                rOIFacilityLogNewRequestPage.MRODeliveryRequestForBostonProper();
                LogNewRequestPage logNewRequestPage = new LogNewRequestPage(driver, logger, TestContext);
                string requestId1 = logNewRequestPage.getRequestid();
                //ROIFacilityRequestStatusPage rOIFacilityRequestStatusPage = new ROIFacilityRequestStatusPage(driver, logger, TestContext);
                logger.Log(Status.Info, $"MRO delivery request created with id:({requestId1})", TakeScreenShotAtStep());
                rOIAdminHomePage.ROIlookupByRequestId(requestId1);


                //string requestid1 = rOIFacilityRequestStatusPage.GetRequestID();
                logger.Log(Status.Info, $"MRO delivery request created with id ({requestId1})");
                rOIFacilityRequestStatusPage.ImportPdfFiles();
                rOIFacilityRequestStatusPage.ReleaseRequestID();

                rOIFacilityRequestStatusPage.GoToActionList();

                rOIFacilityActionListPage.CreateReport();
                bool isDisplayed1 = rOIFacilityActionListPage.VerifyRequestId(requestId1);
                Assert.IsFalse(isDisplayed1, "Request id present");
                logger.Log(Status.Pass, "Successfully verified request id not exist in the  action list report page", TakeScreenShotAtStep());
                rOIFacilityRequestStatusPage.SelectRecentRequest();
                rOIFacilityRequestStatusPage.SelectActionMessage();
                string msg1 = rOIFacilityRequestStatusPage.VerifyActionMessage();
                Assert.AreEqual(msg1, actionMsg, "Failed to verify action message");
                logger.Log(Status.Pass, $"Successfully verified action message created with name:{(actionMsg)} and  system date and time", TakeScreenShotAtStep());

                rOIFacilityRequestStatusPage.SwitchToDefaultWindow();
                rOIFacilityRequestStatusPage.GoToActionList();
                rOIFacilityActionListPage.CreateReport();
                bool isDisplayed2 = rOIFacilityActionListPage.VerifyRequestId(requestId1);
                Assert.IsTrue(isDisplayed2, "Request id not exist");
                logger.Log(Status.Pass, "Successfully verified request id  exist in the  action list report page", TakeScreenShotAtStep());
                

                rOIFacilityRequestStatusPage.SelectRecentRequest();
                ROIAdminFacilityWorkSummarypage facilityWorkSummaryPage = new ROIAdminFacilityWorkSummarypage(driver, logger, TestContext);
              
                rOIFacilityRequestStatusPage.SelectAllUsers();
                ROIAdminUserListingPage rOIAdminUserListingPage = new ROIAdminUserListingPage(driver, logger, TestContext);
                rOIAdminUserListingPage.SearchFacilityUser(userId);
                bool isDeactivate = rOIAdminUserListingPage.DeActivateFacilityUser();
                Assert.IsFalse(isDeactivate, "Check box is checked");
                logger.Log(Status.Pass, "Successfully verified activate checkbox is unchecked and delete check is visible", TakeScreenShotAtStep());
                rOIAdminUserListingPage.DeleteFacilityUser();

                rOIFacilityRequestStatusPage.GoToActionList();
                rOIFacilityActionListPage.CreateReport();
                bool _isDisplayed = rOIFacilityActionListPage.VerifyRequestId(requestId1);
                Assert.IsTrue(_isDisplayed, "Request id present");
                logger.Log(Status.Pass, "Successfully verified request id  exist and user name contains '*' in the  action list report page", TakeScreenShotAtStep());

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

