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
    public class ROIUndoSetIssueNoticesRemoveCommentFieldTest : ROIBaseTest
    {
        public ROIUndoSetIssueNoticesRemoveCommentFieldTest() : base(ROITestArea.ROITestFacility)
        {

        }

        [STATestMethodAttribute]
        [TestCategory(ROITestCategory.Regression)]
        // Converted manual test case 3468-ROI-Facility-->UNDO SET: Issue Notices – Remove Comment Field to automated.
        public void Reg_3468_ROIUndoSetIssueNoticesRemoveCommentFieldTest()
        {
            logger = extent.CreateTest("Reg_3468_ROIUndoSetIssueNoticesRemoveCommentField");
            logger.Log(Status.Info, "Converted manual test case 3468-ROI-Facility-->UNDO SET: Issue Notices – Remove Comment Field to automated");
            RemoteWebDriver _driver = Init(TestContext.Properties["browser"].ToString());
            ThreadLocal<RemoteWebDriver> localDriver = new ThreadLocal<RemoteWebDriver>();
            localDriver.Value = _driver;
            RemoteWebDriver driver = localDriver.Value;

            try
            {
                string issueName = IniHelper.ReadConfig("ROIUndoSetIssueNoticesRemoveCommentFieldTest", "AuthorizationIssue");
                string comment = IniHelper.ReadConfig("ROIUndoSetIssueNoticesRemoveCommentFieldTest", "Comments");
                string comment1 = IniHelper.ReadConfig("ROIUndoSetIssueNoticesRemoveCommentFieldTest", "NewComment");
                string comment2 = IniHelper.ReadConfig("ROIUndoSetIssueNoticesRemoveCommentFieldTest", "AdminSideComment");
                string corresissue = IniHelper.ReadConfig("ROIUndoSetIssueNoticesRemoveCommentFieldTest", "correspondenceIssue");
                string comment3 = IniHelper.ReadConfig("ROIUndoSetIssueNoticesRemoveCommentFieldTest", "correspondenceComment");
                ROIFacilityWorkSummaryPage rOIFacilityWorkSummaryPage = new ROIFacilityWorkSummaryPage(driver, logger, TestContext);
                rOIFacilityWorkSummaryPage.GoToLogNewRequestPage();
                ROIFacilityLogNewRequestPage rOIFacilityLogNewRequestPage = new ROIFacilityLogNewRequestPage(driver, logger, TestContext);
                rOIFacilityLogNewRequestPage.CreateNewMRODeliveryRequestForBostonProper();

                ROIFacilityRequestStatusPage rOIFacilityRequestStatusPage = new ROIFacilityRequestStatusPage(driver, logger, TestContext);
                string requestid = rOIFacilityRequestStatusPage.GetRequestID();
                logger.Log(Status.Info, $"MRO delivery request created with id ({requestid})");

                rOIFacilityRequestStatusPage.ImportRequestPages();
                rOIFacilityRequestStatusPage.clickOnManageIssue();

                ROIAdminAddIssuePage rOIAdminAddIssuePage = new ROIAdminAddIssuePage(driver, logger, TestContext);
                rOIAdminAddIssuePage.AddIssueWithType(issueName,comment);
                logger.Log(Status.Info, "Issue added");

                ROIManageRequestIssuesPage manageRequestIssuesPage = new ROIManageRequestIssuesPage(driver, logger, TestContext);
                string notes=manageRequestIssuesPage.VerifyNotes();
                Assert.AreEqual(notes, comment, "Failed to verify notes");
                logger.Log(Status.Pass, $"Successfully verified manage request issues window and notes field set to :{(notes)}", TakeScreenShotAtStep());
                manageRequestIssuesPage.ClickOnDone();

                rOIFacilityRequestStatusPage.ClickOnListOfIssues();
                manageRequestIssuesPage.ClickOnIssueEditIcon();
                ROIRequestIssuesPage rOIRequestIssuesPage = new ROIRequestIssuesPage(driver, logger, TestContext);
                string commentVal=rOIRequestIssuesPage.VerifyComments();
                Assert.AreEqual(commentVal, comment, "Failed to verify comments value");
                logger.Log(Status.Pass, $"Successfully verified  request issues window and comments field set to :{(comment)}", TakeScreenShotAtStep());

                rOIRequestIssuesPage.ClearCommentsTextarea(comment1);
                string notes1 = manageRequestIssuesPage.VerifyNotes();
                Assert.AreEqual(notes1, comment1, "Failed to verify notes");
                logger.Log(Status.Pass, $"Successfully verified manage request issues window and notes field set to :{(notes1)}", TakeScreenShotAtStep());
                manageRequestIssuesPage.ClickOnDone();
                rOIFacilityRequestStatusPage.ImportPatientPages();
                rOIFacilityRequestStatusPage.ReleaseRequestID();

                ROIAdminHomePage rOIAdminHomePage = new ROIAdminHomePage(driver, logger, TestContext);
                rOIAdminHomePage.SwitchToNewTabAndLoginROIAdmin(BaseAddress);
                rOIAdminHomePage.SearchByRequestId(requestid);
                ROIAdminRequestStatusPage rOIAdminRequestStatusPage = new ROIAdminRequestStatusPage(driver, logger, TestContext);
                rOIAdminRequestStatusPage.assignRequester();
                ROIAdminAssignROIRequesterPage rOIAdminAssignROIRequesterPage = new ROIAdminAssignROIRequesterPage(driver, logger, TestContext);
                rOIAdminAssignROIRequesterPage.assignTestAttorney();
                rOIAdminRequestStatusPage.ClickOnQcPassButton();
                rOIAdminRequestStatusPage.ClickOnManageIssuesBtn();
                rOIAdminRequestStatusPage.ClickOnComments(comment2);
                rOIAdminRequestStatusPage.ClickSendIssueBtn();
                string issueId= rOIAdminRequestStatusPage.VerifyCorrespondenceIssue();
                logger.Log(Status.Pass, $"Successfully verified  issue id created under correspondence Packages {(issueId)}", TakeScreenShotAtStep());
                rOIAdminRequestStatusPage.ClickOnIssueMagnifierIcon();

                rOIAdminRequestStatusPage.ClickOnCorrespondence();
                rOIAdminRequestStatusPage.AddCoresspondenceIssue(corresissue, comment3);

                string corress = rOIAdminRequestStatusPage.VerifyCorrespondencePackage();
                logger.Log(Status.Pass, $"Successfully verified  an correspondence issue added  under correspondence Packages  with id{(corress)}", TakeScreenShotAtStep());
                rOIAdminRequestStatusPage.ClickOnCorrespondenceIssueMagnifierIcon();
                rOIAdminRequestStatusPage.ClickOnCloseBtn();

                rOIAdminRequestStatusPage.ClickApplyRateButton();
                rOIAdminRequestStatusPage.CreateInvoice();               

                bool isDisplayed = rOIAdminRequestStatusPage.CreatedInvoiceNumber();
                Assert.IsTrue(isDisplayed, "Failed to create invoice");
                logger.Log(Status.Info, $"Succcessfully verified invoice created",TakeScreenShotAtStep());

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

