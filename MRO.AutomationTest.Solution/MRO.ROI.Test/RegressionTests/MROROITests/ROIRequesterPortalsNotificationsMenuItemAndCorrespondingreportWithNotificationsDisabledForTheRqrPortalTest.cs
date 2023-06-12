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
    public class ROIRequesterPortalsNotificationsMenuItemAndCorrespondingreportWithNotificationsDisabledForTheRqrPortalTest : ROIBaseTest
    {
        public ROIRequesterPortalsNotificationsMenuItemAndCorrespondingreportWithNotificationsDisabledForTheRqrPortalTest() : base(ROITestArea.ROIAdmin)
        {
        }
        [STATestMethodAttribute]
        [TestCategory(ROITestCategory.Regression)]
        //Converted manual test case 3243-ROI-Admin-->Requester Portals - 'Notifications' menu item and corresponding report, With Notifications disabled for the RqrPortal to automated
        public void Reg_3243_ROIRequesterPortalsNotificationsMenuItemAndCorrespondingreportWithNotificationsDisabledForTheRqrPortalTest()
        {
            logger = extent.CreateTest("Reg_3243_ROIRequesterPortalsNotificationsMenuItemAndCorrespondingreportWithNotificationsDisabledForTheRqrPortalTest");
            logger.Log(Status.Info, "Converted manual test case 3243-ROI-Admin-->Requester Portals - 'Notifications' menu item and corresponding report, With Notifications disabled for the RqrPortal to automated");
            RemoteWebDriver _driver = Init(TestContext.Properties["browser"].ToString());
            ThreadLocal<RemoteWebDriver> localDriver = new ThreadLocal<RemoteWebDriver>();
            localDriver.Value = _driver;
            RemoteWebDriver driver = localDriver.Value;

            try
            {


                string issueName = IniHelper.ReadConfig("ROIUndoSetIssueNoticesRemoveCommentFieldTest", "AuthorizationIssue");
                string comment = IniHelper.ReadConfig("ROIUndoSetIssueNoticesRemoveCommentFieldTest", "Comments");

                ROIAdminHomePage rOIAdminHomePage = new ROIAdminHomePage(driver, logger, TestContext);
                rOIAdminHomePage.SelectFeatures();
                ROIAdminFeaturesPage rOIAdminFeaturesPage = new ROIAdminFeaturesPage(driver, logger, TestContext);
                rOIAdminFeaturesPage.ClickOnSync();

                rOIAdminFeaturesPage.ClickAddNewEnumsDatabase();
                string header=rOIAdminFeaturesPage.VerifyHeader();
                Assert.AreEqual(header, "Features","Failed to verify header");
                logger.Log(Status.Pass, "Verified that still on the features page", TakeScreenShotAtStep());

                rOIAdminFeaturesPage.SelectRoles();
                rOIAdminFeaturesPage.SelectContext("RqrPort", "[All]");
                bool isDisplayed=rOIAdminFeaturesPage.VerifyRqrPortal();
                Assert.IsTrue(isDisplayed, "Failed to verify Rqr portal");

                logger.Log(Status.Info, "Verified that RqrPortal-User is displayed", TakeScreenShotAtStep());

                rOIAdminFeaturesPage.ClickOnRqrPortalComposeLink();
                string header1 = rOIAdminFeaturesPage.VerifyHeader();
                Assert.AreEqual(header1, "Roles: RqrPort_RqrPortal - User", "Failed to verify header");
                logger.Log(Status.Pass, $"Verified that {(header1)} is opened ", TakeScreenShotAtStep());

                rOIAdminFeaturesPage.UnCheckNotificationFloder();
                rOIAdminHomePage.OpenNewTabAndLoginROITestFacility(BaseAddress);

                ROIFacilityWorkSummaryPage rOIFacilityWorkSummaryPage = new ROIFacilityWorkSummaryPage(driver, logger, TestContext);
                rOIFacilityWorkSummaryPage.logaNewRequest();
                ROIFacilityLogNewRequestPage rOIFacilityLogNewRequestPage = new ROIFacilityLogNewRequestPage(driver, logger, TestContext);
                rOIFacilityLogNewRequestPage.ClickMRODeliveryTab();
                rOIFacilityLogNewRequestPage.MRODeliveryRequestForBostonProper();

                LogNewRequestPage logNewRequestPage = new LogNewRequestPage(driver, logger, TestContext);
                string requestId = logNewRequestPage.getRequestid();
                ROIFacilityRequestStatusPage rOIFacilityRequestStatusPage = new ROIFacilityRequestStatusPage(driver, logger, TestContext);
                logger.Log(Status.Info, $"MRO delivery request created with id:({requestId})", TakeScreenShotAtStep());
                rOIAdminHomePage.ROIlookupByRequestId(requestId);

                //Going to close the request fix it.
                rOIFacilityRequestStatusPage.ReOpenRequest();
                rOIFacilityRequestStatusPage.ImportPdfFiles();
                rOIFacilityRequestStatusPage.ReleaseRequestID();
                logger.Log(Status.Info, $"Request released");
                rOIAdminHomePage.SwitchToNewTabAndLoginROIAdmin(BaseAddress);
                rOIAdminHomePage.SearchByRequestId(requestId);

                ROIAdminRequestStatusPage rOIAdminRequestStatusPage = new ROIAdminRequestStatusPage(driver, logger, TestContext);
                rOIAdminRequestStatusPage.assignRequester();
                ROIAdminAssignROIRequesterPage rOIAdminAssignROIRequesterPage = new ROIAdminAssignROIRequesterPage(driver, logger, TestContext);
                rOIAdminAssignROIRequesterPage.assignTestAttorney();
               

                string reAssignRequesterValue = rOIAdminRequestStatusPage.VerifyReAssignRequester();
                Assert.AreEqual(reAssignRequesterValue, "TEST Attorney's");
                logger.Log(Status.Info, $"Succcessfully verified at the request status page  requester ({reAssignRequesterValue})");

                rOIAdminRequestStatusPage.ClickOnQcPassButton();
                rOIAdminRequestStatusPage.ClickAddIssueBtn();

                ROIAdminAddIssuePage rOIAdminAddIssuePage = new ROIAdminAddIssuePage(driver, logger, TestContext);
                string issueType = rOIAdminAddIssuePage.AddIssueWithType(issueName,comment);
                logger.Log(Status.Pass, "Issue added", TakeScreenShotAtStep());

                rOIAdminRequestStatusPage.ClickSendIssueBtn();
                rOIAdminRequestStatusPage.ClickSendIssueBtn();
                string issueId = rOIAdminRequestStatusPage.VerifyCorrespondenceIssue();                
                logger.Log(Status.Pass, $"Successfully verified  issue id created under correspondence Packages {(issueId)}", TakeScreenShotAtStep());

                rOIAdminHomePage.SwitchToNewTabAndLoginROIRequesterPortal(BaseAddress);
                ROITestRequesterPortalHomePage rOITestRequesterPortalHomePage = new ROITestRequesterPortalHomePage(driver, logger, TestContext);                
               // rOITestRequesterPortalHomePage.ClickOnNotificationPopUp();
                bool isPopUpExist=rOITestRequesterPortalHomePage.VerifyOnNotificationPopUp();
                Assert.IsFalse(isPopUpExist, "Failed to verify no popup notifications");

                rOIAdminHomePage.SwitchToNewTabAndLoginROIAdmin(BaseAddress);
                rOIAdminHomePage.SelectFeatures();                
                rOIAdminFeaturesPage.ClickOnSync();

                rOIAdminFeaturesPage.ClickAddNewEnumsDatabase();               
                rOIAdminFeaturesPage.SelectRoles();
                rOIAdminFeaturesPage.SelectContext("RqrPort", "[All]");              
                rOIAdminFeaturesPage.ClickOnRqrPortalComposeLink();
                rOIAdminFeaturesPage.UnCheckNotificationFloder();

                rOIAdminHomePage.SwitchToNewTabAndLoginROIRequesterPortal(BaseAddress);
                rOITestRequesterPortalHomePage.ClickOnNotificationPopUp();
                bool _isPopUpExist = rOITestRequesterPortalHomePage.VerifyOnNotificationPopUp();
               // Assert.IsTrue(_isPopUpExist, "Failed to verify no popup notifications");

                LoginPage loginPage = new LoginPage(driver, logger, TestContext);
                loginPage.LogOut();
                //Need to re-execute manually and fix it.
                Cleanup(driver);
            }
            catch (Exception ex)
            {
                LogException(driver, logger, ex);

            }
        }
    }
}
