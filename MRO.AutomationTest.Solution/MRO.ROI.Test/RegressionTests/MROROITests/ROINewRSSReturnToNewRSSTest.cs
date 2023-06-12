using AventStack.ExtentReports;
using AventStack.ExtentReports.MarkupUtils;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MRO.ROI.Automation.Common.Navigation;
using MRO.ROI.Automation.Pages;
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
    public class ROINewRSSReturnToNewRSSTest : ROIBaseTest
    {
        public ROINewRSSReturnToNewRSSTest() : base(ROITestArea.ROITestFacility)
        {
        }

        [STATestMethodAttribute]
        [TestCategory(ROITestCategory.Development)]
        // Converted manual test case 5276-ROI-Facility-->Test Case 5276:New RSS-Return to New RSS to automated.
        public void Reg_5276_ROINewRSSReturnToNewRSSTest()
        {
            logger = extent.CreateTest("Reg_5276_ROINewRSSReturnToNewRSSTest");
            logger.Log(Status.Info, "Converted manual test case 5276-ROI-Facility-->Test Case 5276:New RSS-Return to New RSS to automated");
            RemoteWebDriver _driver = Init(TestContext.Properties["browser"].ToString());
            ThreadLocal<RemoteWebDriver> localDriver = new ThreadLocal<RemoteWebDriver>();
            localDriver.Value = _driver;
            RemoteWebDriver driver = localDriver.Value;


            try
            {


                string issueName = IniHelper.ReadConfig("ROIUndoSetIssueNoticesRemoveCommentFieldTest", "AuthorizationIssue");
                string comment = IniHelper.ReadConfig("ROIUndoSetIssueNoticesRemoveCommentFieldTest", "Comments");

                string corresissue = IniHelper.ReadConfig("ROIUndoSetIssueNoticesRemoveCommentFieldTest", "correspondenceIssue");
                string comment3 = IniHelper.ReadConfig("ROIUndoSetIssueNoticesRemoveCommentFieldTest", "correspondenceComment");

                string issueName1 = IniHelper.ReadConfig("ROIIssuePackageCreationErrorMessageTest", "BehavioralIssue");
                string comment1 = IniHelper.ReadConfig("ROIIssuePackageCreationErrorMessageTest", "Comments");

                string corresissue1 = IniHelper.ReadConfig("ROINewRSSReturnToNewRSSTest", "CorrespondenceIssue");

                ROIFacilityWorkSummaryPage rOIFacilityWorkSummaryPage = new ROIFacilityWorkSummaryPage(driver, logger, TestContext);
                rOIFacilityWorkSummaryPage.logaNewRequest();
                ROIFacilityLogNewRequestPage rOIFacilityLogNewRequestPage = new ROIFacilityLogNewRequestPage(driver, logger, TestContext);
                rOIFacilityLogNewRequestPage.ClickMRODeliveryTab();

                rOIFacilityLogNewRequestPage.MRODeliveryRequestForBostonProper();
                LogNewRequestPage logNewRequestPage = new LogNewRequestPage(driver, logger, TestContext);
                string requestId = logNewRequestPage.getRequestid();
                ROIFacilityRequestStatusPage rOIFacilityRequestStatusPage = new ROIFacilityRequestStatusPage(driver, logger, TestContext);
                logger.Log(Status.Info, $"MRO delivery request created with id:({requestId})", TakeScreenShotAtStep());
                ROIAdminHomePage rOIAdminHomePage = new ROIAdminHomePage(driver, logger, TestContext);
                rOIAdminHomePage.ROIlookupByRequestId(requestId);


                rOIFacilityRequestStatusPage.ReOpenRequest();
                rOIFacilityRequestStatusPage.ImportPdfFiles();
                rOIFacilityRequestStatusPage.ReleaseRequestID();
                logger.Log(Status.Info, $"Request released");

                rOIAdminHomePage.SwitchToNewTabAndLoginROIAdmin(BaseAddress);
                rOIAdminHomePage.SearchByRequestId(requestId);

                ROIAdminRequestStatusPage rOIAdminRequestStatusPage = new ROIAdminRequestStatusPage(driver, logger, TestContext);
                rOIAdminRequestStatusPage.ClickAddAction();
                ROIRequestActionsPage rOIRequestActionsPage = new ROIRequestActionsPage(driver, logger, TestContext);
                rOIRequestActionsPage.AddMROActionWithType("(RSS) Email Records");
                logger.Log(Status.Info, "Verified that action created with todays date and given notes value", TakeScreenShotAtStep());

                rOIRequestActionsPage.ClickBackToRequest();
                bool isViewActionExists = rOIAdminRequestStatusPage.VerifyViewAction();
                Assert.IsTrue(isViewActionExists, "Failed to verify view action");
                logger.Log(Status.Info, "Verified that view action hypertext is visible", TakeScreenShotAtStep());

                rOIAdminRequestStatusPage.assignRequester();
                ROIAdminAssignROIRequesterPage rOIAdminAssignROIRequester = new ROIAdminAssignROIRequesterPage(driver, logger, TestContext);
                rOIAdminAssignROIRequester.AssignRequestandAddFaxNo("610-994-7509");
                logger.Log(Status.Info, "Assigned Test Attorney");

                rOIAdminRequestStatusPage.ClickOnTestAttroney();
                ROIAdminEditRequesterInfoPage adminEditRequesterInfoPage = new ROIAdminEditRequesterInfoPage(driver, logger, TestContext);
                adminEditRequesterInfoPage.SelectInvoiceDelivery("Fax");
                Assert.IsTrue(adminEditRequesterInfoPage.CheckMailIsDisabled(), "Mail check box is enabled.");
                adminEditRequesterInfoPage.UpdateCorrespondenceSettingsForFaxAndEmail();
                string infoMsg = adminEditRequesterInfoPage.GetRequesterInfoMsg();
                Assert.AreEqual(infoMsg, "Requester Info Updated!");
                adminEditRequesterInfoPage.ClickOnReturnToRequest();

                rOIAdminRequestStatusPage.ClickAddIssueBtn();
                ROIAdminAddIssuePage rOIAdminAddIssuePage = new ROIAdminAddIssuePage(driver, logger, TestContext);
                string issueType = rOIAdminAddIssuePage.AddIssueWithType(issueName, comment);
                logger.Log(Status.Pass, "Issue added", TakeScreenShotAtStep());


              
                rOIAdminRequestStatusPage.ClickSendIssueBtn();
                string issueId = rOIAdminRequestStatusPage.VerifyCorrespondenceIssue(); 
                logger.Log(Status.Pass, $"Successfully verified  issue id created under correspondence Packages {(issueId)}", TakeScreenShotAtStep());

                rOIAdminRequestStatusPage.ClickOnCorrespondence();

                rOIAdminRequestStatusPage.AddCoresspondenceIssue(corresissue, comment3);

                string corress = rOIAdminRequestStatusPage.VerifyCorrespondencePackage();
                logger.Log(Status.Pass, $"Successfully verified  an correspondence issue added  under correspondence Packages  with id{(corress)}", TakeScreenShotAtStep());

                rOIAdminRequestStatusPage.ClickRSSRequestStatus();
                rOIAdminRequestStatusPage.FindRequest(requestId);

                ROIRSSRequestStatusPage rOIRSSRequestStatusPage = new ROIRSSRequestStatusPage(driver, logger, TestContext);
                rOIRSSRequestStatusPage.ClickOpenIssues();
                logger.Log(Status.Info, "Verified that manage issues page loaded", TakeScreenShotAtStep());

                ROIManageRequestIssuesPage rOIManageRequestIssuesPage = new ROIManageRequestIssuesPage(driver, logger, TestContext);                
                rOIManageRequestIssuesPage.ClickAuthorizationIssue();
                string status= rOIRSSRequestStatusPage.VerifyRequestStatus();
                logger.Log(Status.Pass, $"Verified that return to RSS pagr and request status :{(status)}", TakeScreenShotAtStep());

                rOIRSSRequestStatusPage.ClickViewAction();
                logger.Log(Status.Info, "Verified thar request actions window opened", TakeScreenShotAtStep());
                rOIRequestActionsPage.ClickBackToRequest();

                rOIRSSRequestStatusPage.SearchByRequestId(requestId);

                rOIAdminRequestStatusPage.ClickOnTestAttroney();             
                adminEditRequesterInfoPage.SelectInvoiceDelivery("EMAIL");
                adminEditRequesterInfoPage.UpdateCorrespondenceSettingsForEmail();
                adminEditRequesterInfoPage.ClickOnReturnToRequest();

                rOIAdminRequestStatusPage.ClickAddIssueBtn();
                rOIAdminAddIssuePage.AddIssueWithType(issueName1, comment1);
                logger.Log(Status.Pass, "Issue added", TakeScreenShotAtStep());

                rOIAdminRequestStatusPage.ClickSendIssueBtn();
                string issueId1 = rOIAdminRequestStatusPage.VerifyCorrespondenceIssue(); 
                logger.Log(Status.Pass, $"Successfully verified  issue id created under correspondence Packages {(issueId1)}", TakeScreenShotAtStep());
                rOIAdminRequestStatusPage.ClickOnCorrespondence();

                

                rOIAdminRequestStatusPage.AddCoresspondenceIssue(corresissue1, comment3);
                string corresIssue = rOIAdminRequestStatusPage.VerifyCorrespondencePackage();
                logger.Log(Status.Pass, $"Successfully verified  an correspondence issue added  under correspondence Packages  with id{(corresIssue)}", TakeScreenShotAtStep());

                rOIAdminRequestStatusPage.ClickRSSRequestStatus();
                rOIAdminRequestStatusPage.FindRequest(requestId);

               
                rOIRSSRequestStatusPage.ClickViewAction();
               
                rOIAdminRequestStatusPage.CloseTheIssue();
                logger.Log(Status.Info, "Verified that issue closed with today's date and time", TakeScreenShotAtStep());

                rOIAdminRequestStatusPage.ClickOnQcPassButton();
                rOIAdminRequestStatusPage.DeliveryOverride("EMAIL");
                rOIAdminRequestStatusPage.ApplyRate();

                rOIAdminRequestStatusPage.ClickOnCloseBtn();
                rOIAdminRequestStatusPage.CreateInvoice();


                rOIAdminRequestStatusPage.ClickOnAddAndSelectEmail();
                rOIAdminRequestStatusPage.ClickOnEmailHyperlink();

                logger.Log(Status.Info, "Verified that shipment details page opened", TakeScreenShotAtStep());
                ROIAdminShipmentDetailsPage rOIAdminShipmentDetailsPage = new ROIAdminShipmentDetailsPage(driver, logger, TestContext);
                rOIAdminShipmentDetailsPage.ToClickMakeShippableButton();

                rOIAdminShipmentDetailsPage.ClickUpdateCarrierButton();
                rOIAdminShipmentDetailsPage.AddTrackingNumber();

                rOIAdminHomePage.SearchByRequestId(requestId);
                rOIAdminRequestStatusPage.ClickOnAddAndSelectMail();

                ROIAdminPackingListstPage rOIAdminPackingListstPage = new ROIAdminPackingListstPage(driver, logger, TestContext);
                rOIAdminPackingListstPage.VerifyPackageListPageHeader();
                logger.Log(Status.Info, "Succesfully verified that packing list page opened", TakeScreenShotAtStep());

                rOIAdminPackingListstPage.CreatePackingList();
                rOIAdminPackingListstPage.AddSecondaryShipment();
                rOIAdminPackingListstPage.ReturnToRss();

                rOIAdminPackingListstPage.VerifyShipmentId();
                rOIAdminRequestStatusPage.ClickOnMailHyperlink();

                rOIAdminShipmentDetailsPage.ToClickMakeShippableButton();
                rOIAdminShipmentDetailsPage.ClickUpdateCarrierButton();
                rOIAdminShipmentDetailsPage.UpdateCarrierInformation();





                //At step-37 create invoice button is disabled so it is blocked,automated till step-36.


                Cleanup(driver);

            }

            catch (Exception ex)
            {
                LogException(driver, logger, ex);

            }
        }
    }

}

