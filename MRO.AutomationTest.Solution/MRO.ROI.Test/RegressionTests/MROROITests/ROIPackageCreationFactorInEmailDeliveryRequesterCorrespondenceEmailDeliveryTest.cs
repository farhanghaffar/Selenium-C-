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
    public class ROIPackageCreationFactorInEmailDeliveryRequesterCorrespondenceEmailDeliveryTest : ROIBaseTest
    {
        public ROIPackageCreationFactorInEmailDeliveryRequesterCorrespondenceEmailDeliveryTest() : base(ROITestArea.ROITestFacility)
        {
        }

        [STATestMethodAttribute]
        [TestCategory(ROITestCategory.Regression)]
        // Converted manual test case 3244-ROI-Facility-->Test Case 3244:Package Creation, factor in e-mail delivery - Requester Correspondence - Email Delivery (using null for email address, fax not checked, fax number is not valid, invoice will be mailed) to automated.
        public void Reg_3244_ROIPackageCreationFactorInEmailDeliveryRequesterCorrespondenceEmailDeliveryTest()
        {
            logger = extent.CreateTest("Reg_3244_ROIPackageCreationFactorInEmailDeliveryRequesterCorrespondenceEmailDeliveryTest");
            logger.Log(Status.Info, "Converted manual test case 3244-ROI-Facility-->Test Case 3244:Package Creation, factor in e-mail delivery - Requester Correspondence - Email Delivery (using null for email address, fax not checked, fax number is not valid, invoice will be mailed) to automated.");
            RemoteWebDriver _driver = Init(TestContext.Properties["browser"].ToString());
            ThreadLocal<RemoteWebDriver> localDriver = new ThreadLocal<RemoteWebDriver>();
            localDriver.Value = _driver;
            RemoteWebDriver driver = localDriver.Value;


            try
            {
                string issueName = IniHelper.ReadConfig("ROIUndoSetIssueNoticesRemoveCommentFieldTest", "AuthorizationIssue");
                string comment = IniHelper.ReadConfig("ROIUndoSetIssueNoticesRemoveCommentFieldTest", "Comments");

                ROIFacilityWorkSummaryPage rOIFacilityWorkSummaryPage = new ROIFacilityWorkSummaryPage(driver, logger, TestContext);
                rOIFacilityWorkSummaryPage.logaNewRequest();
                ROIFacilityLogNewRequestPage rOIFacilityLogNewRequestPage = new ROIFacilityLogNewRequestPage(driver, logger, TestContext);
                rOIFacilityLogNewRequestPage.ClickMRODeliveryTab();
                rOIFacilityLogNewRequestPage.CreateNewMRODeliveryRequestForBostonProper();
                ROIFacilityRequestStatusPage rOIFacilityRequestStatusPage = new ROIFacilityRequestStatusPage(driver, logger, TestContext);
                string requestid = rOIFacilityRequestStatusPage.GetRequestID();
                logger.Log(Status.Pass, $"Request created with requestid({requestid})", TakeScreenShotAtStep());
                ROIAdminHomePage rOIAdminHomePage = new ROIAdminHomePage(driver, logger, TestContext);
                //rOIAdminHomePage.ROIlookupByRequestId(requestid);
                //Going to close the request fix it.
                rOIFacilityRequestStatusPage.ReOpenRequest();
                rOIFacilityRequestStatusPage.ImportPdfFiles();
                rOIFacilityRequestStatusPage.ReleaseRequestID();
                logger.Log(Status.Info, $"Request released");

                rOIAdminHomePage.SwitchToNewTabAndLoginROIAdmin(BaseAddress);
                rOIAdminHomePage.SearchByRequestId(requestid);

                ROIAdminRequestStatusPage rOIAdminRequestStatusPage = new ROIAdminRequestStatusPage(driver, logger, TestContext);
                rOIAdminRequestStatusPage.assignRequester();
                ROIAdminAssignROIRequesterPage rOIAdminAssignROIRequesterPage = new ROIAdminAssignROIRequesterPage(driver, logger, TestContext);
                rOIAdminAssignROIRequesterPage.AssignRequester();

                rOIAdminRequestStatusPage.ClickOnTestAttroney();
                ROIAdminEditRequesterInfoPage rOIAdminEditRequesterInfoPage = new ROIAdminEditRequesterInfoPage(driver, logger, TestContext);
                bool _editRequesterInfoPageHeader = rOIAdminEditRequesterInfoPage.VerifyEditRequesterInfoPage();
                Assert.IsTrue(_editRequesterInfoPageHeader, "Failed to verify edit requester info page");
                logger.Log(Status.Info, "Verified that edit requester info page opened", TakeScreenShotAtStep());

                rOIAdminEditRequesterInfoPage.SelectInvoiceDelivery("EMAIL");                
                rOIAdminEditRequesterInfoPage.UpdateCorrespondenceSettings();
                rOIAdminEditRequesterInfoPage.ClearFaxNumber();
                rOIAdminEditRequesterInfoPage.ClearEmailField();
                rOIAdminEditRequesterInfoPage.ClickOnReturnToRequest();

                rOIAdminRequestStatusPage.ClickOnReAssignROIRequester();
                rOIAdminAssignROIRequesterPage.ClickOnSaveButton();
                rOIAdminRequestStatusPage.ClickOnQcPassButton();
                rOIAdminRequestStatusPage.ClickAddIssueBtn();

                ROIAdminAddIssuePage rOIAdminAddIssuePage = new ROIAdminAddIssuePage(driver, logger, TestContext);
                string issueType = rOIAdminAddIssuePage.AddIssueWithType(issueName, comment);
                logger.Log(Status.Pass, "Issue added", TakeScreenShotAtStep());

                rOIAdminRequestStatusPage.ClickSendIssueBtn();
                rOIAdminRequestStatusPage.ClickSendIssueBtn();
                string issueId = rOIAdminRequestStatusPage.VerifyCorrespondenceIssue(); ;
                logger.Log(Status.Pass, $"Successfully verified  issue id created under correspondence Packages {(issueId)}", TakeScreenShotAtStep());

                rOIAdminRequestStatusPage.ClickOnCloseBtn();
                rOIAdminRequestStatusPage.ClickApplyRateButton();
                rOIAdminRequestStatusPage.CreateInvoice();

                bool isDisplayed = rOIAdminRequestStatusPage.CreatedInvoiceNumber();
                Assert.IsTrue(isDisplayed, "Failed to create invoice");
                logger.Log(Status.Info, $"Succcessfully verified invoice created", TakeScreenShotAtStep());

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

