using AventStack.ExtentReports;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MRO.ROI.Automation.Pages;
using MRO.ROI.Automation.Pages.Common;
using MRO.ROI.Test.ExecutionFactory;
using MRO.ROI.Test.Utilities;
using OpenQA.Selenium.Remote;
using System;
using System.Threading;

namespace MRO.ROI.Test.RegressionTests.MROROITests
{
    [TestClass]
    public class ROIBOEActionMessageFeedbackLoopTest : ROIBaseTest
    {
        public ROIBOEActionMessageFeedbackLoopTest() : base(ROITestArea.ROITestFacility)
        {
        }
        [STATestMethodAttribute]
        [TestCategory(ROITestCategory.Regression)]
        // Converted manual test case 5870-ROI-Facility-->BOE action message feedback loop to automated.
        public void Reg_5870_ROIBOEActionMessageFeedbackLoopTest()
        {
            logger = extent.CreateTest("Reg_5870_ROIBOEActionMessageFeedbackLoopTest");
            logger.Log(Status.Info, "Converted manual test case 5870-ROI-Facility-->BOE action message feedback loop to automated");
            RemoteWebDriver _driver = Init(TestContext.Properties["browser"].ToString());
            ThreadLocal<RemoteWebDriver> localDriver = new ThreadLocal<RemoteWebDriver>();
            localDriver.Value = _driver;
            RemoteWebDriver driver = localDriver.Value;
            try
            {
                ROIFacilityWorkSummaryPage rOIFacilityWorkSumaryPage = new ROIFacilityWorkSummaryPage(driver, logger, TestContext);
                rOIFacilityWorkSumaryPage.logaNewRequest();
                ROIAdminHomePage rOIAdminHomePage = new ROIAdminHomePage(driver, logger, TestContext);
                ROIFacilityLogNewRequestPage rOIFacilityLogNewRequestPage = new ROIFacilityLogNewRequestPage(driver, logger, TestContext);
                rOIFacilityLogNewRequestPage.ClickOnInternalPortalTab();
                rOIFacilityLogNewRequestPage.CreateInternalPortalRequestWithSpecificDetails("ROI-Test Facility CBO", "Aetna Test (fax)");
                ROIFacilityRequestStatusPage rOIFacilityRequestStatusPage = new ROIFacilityRequestStatusPage(driver, logger, TestContext);
                string requestID = rOIFacilityRequestStatusPage.GetRequestID();
                logger.Log(Status.Pass, $"Request created with requestid ({requestID})", TakeScreenShotAtStep());
                rOIAdminHomePage.ROIlookupByRequestId(requestID);
                rOIFacilityRequestStatusPage.ImportPdfFiles();
                rOIFacilityRequestStatusPage.ReleaseRequestForInternalRequest();
                logger.Log(Status.Info, "Request released");
                rOIAdminHomePage.SwitchToNewTabAndLoginROIAdmin(BaseAddress);
                rOIAdminHomePage.SearchByRequestId(requestID);
                ROIAdminRequestStatusPage rOIAdminRequestStatusPage = new ROIAdminRequestStatusPage(driver, logger, TestContext);
                string requestStatusVal = rOIAdminRequestStatusPage.GetRequestType();
                Assert.AreEqual(requestStatusVal, "Billing Office", "Failed to verify request status");
                logger.Log(Status.Pass, $"Verified that request status set to :{(requestStatusVal)}", TakeScreenShotAtStep());
                rOIAdminRequestStatusPage.ClickAddAction();

                ROIRequestActionsPage rOIRequestActionsPage = new ROIRequestActionsPage(driver, logger, TestContext);
                bool isBoeDisplayed = rOIRequestActionsPage.VerifyBOEActionsDropdown();
                Assert.IsTrue(isBoeDisplayed, "Failed to verify BOE Actions dropdown");
                logger.Log(Status.Info, "Verified that BOE Actions dropdown list is displayed", TakeScreenShotAtStep());
                rOIRequestActionsPage.AddBOEActionWithTypeAndNotes("Message to BOE", "This a test for action message for BOE");
                logger.Log(Status.Info, "Verified that action created with todays date and given notes value", TakeScreenShotAtStep());
                rOIRequestActionsPage.AddFacilityActionWithTypeAndNotes("Message to Facility", "This a test for facility's action messages");
                logger.Log(Status.Info, "Verified that action created with todays date and given notes value", TakeScreenShotAtStep());
                rOIRequestActionsPage.ClickBackToRequest();
                rOIAdminRequestStatusPage.DrillInToFacility();
                logger.Log(Status.Info, "Verified that careated action messages are displayed", TakeScreenShotAtStep());
                rOIFacilityRequestStatusPage.ClickReplyBtn();
                bool isDisplayed = rOIFacilityRequestStatusPage.VerifyMsgToFacilty();
                Assert.IsTrue(isDisplayed, "Failed to verify Message to facility text box");
                logger.Log(Status.Pass, "Verified that message to facility textbox is displayed", TakeScreenShotAtStep());
                rOIFacilityRequestStatusPage.ClickSendReply("This is an answer for the action test");
                logger.Log(Status.Info, "Verified that reply action mesaage visible now on RSS", TakeScreenShotAtStep());

                rOIAdminHomePage.SwitchToAdminTab(BaseAddress);
                rOIAdminHomePage.SearchByRequestId(requestID);
                rOIAdminRequestStatusPage.ClickAddAction();
                string notes = rOIRequestActionsPage.VerifyNotes();
                logger.Log(Status.Pass, $"Verified that reply from  facility side is visible and given notes value :{(notes)} ", TakeScreenShotAtStep());
                rOIAdminRequestStatusPage.CloseTheIssue();
                bool isViewActionExists = rOIAdminRequestStatusPage.VerifyViewAction();
                Assert.IsTrue(isViewActionExists, "Failed to verify view action");
                logger.Log(Status.Info, "Verified that view action hypertext is visible and in black color", TakeScreenShotAtStep());
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

