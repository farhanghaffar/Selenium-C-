using AventStack.ExtentReports;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MRO.ROI.Automation.Pages;
using MRO.ROI.Test.Utilities;
using OpenQA.Selenium.Remote;
using System;
using System.Threading;
using MRO.ROI.Test.ExecutionFactory;
using MRO.ROI.Automation.Pages.Common;

namespace MRO.ROI.Test.RegressionTests.MROROITests
{
    [TestClass]
    public class ChangingInvoiceAndPriorPaymentsTest : ROIBaseTest
    {

        public ChangingInvoiceAndPriorPaymentsTest() : base(ROITestArea.ROIAdmin)
        {

        }
        [STATestMethodAttribute]
        [TestCategory(ROITestCategory.Regression)]
        //Converted manual test case 1414-ROI-Admin-->Changing Invoice with expect payment to facility and prior payments to automated
        public void Reg_1414_ROIAdminChangingInvoiceAndPriorPaymentsTest()
        {
            logger = extent.CreateTest("Reg_1414_ROIAdminChangingInvoiceAndPriorPaymentsTest");
            logger.Log(Status.Info, "Converted manual test case 1414-ROI-Admin-->Changing Invoice with expect payment to facility and prior payments to automated");
            RemoteWebDriver _driver = Init(TestContext.Properties["browser"].ToString());
            ThreadLocal<RemoteWebDriver> localDriver = new ThreadLocal<RemoteWebDriver>();
            localDriver.Value = _driver;
            RemoteWebDriver driver = localDriver.Value;
            try
            {
                ROIAdminHomePage rOIAdminHomePage = new ROIAdminHomePage(driver, logger, TestContext);
                rOIAdminHomePage.SelectFacilityList();
                ROIAdminFacilityListPage rOIAdminFacilityListPage = new ROIAdminFacilityListPage(driver, logger, TestContext);
                rOIAdminFacilityListPage.GoToROITestFacility();
                ROIFacilityWorkSummaryPage rOIFacilityWorkSummaryPage = new ROIFacilityWorkSummaryPage(driver, logger, TestContext);
                rOIFacilityWorkSummaryPage.logaNewRequest();
                ROIFacilityLogNewRequestPage rOIFacilityLogNewRequestPage = new ROIFacilityLogNewRequestPage(driver, logger, TestContext);
                rOIFacilityLogNewRequestPage.CreateNewROITestFacilityDeliveryRequestWithoutScan();
                ROIFacilityRequestStatusPage rOIFacilityRequestStatusPage = new ROIFacilityRequestStatusPage(driver, logger, TestContext);
                string requestid = rOIFacilityRequestStatusPage.GetRequestID();
                logger.Log(Status.Info, $"ROI request created with id ({requestid})", TakeScreenShotAtStep());                
                rOIAdminHomePage.ROIlookupByRequestId(requestid);
                rOIFacilityRequestStatusPage.ImportPdfFiles();
                bool statusUnderImportDocument = rOIFacilityRequestStatusPage.VerifyStatusUnderImportDocument();
                Assert.IsTrue(statusUnderImportDocument, "Failed to verify status under import document is uploaded");
                rOIFacilityRequestStatusPage.ReleaseRequestID();
                logger.Log(Status.Info, "Request released");
                rOIAdminHomePage.SwitchToNewTabAndLoginROIAdmin(BaseAddress);
                rOIAdminHomePage.ROIlookupByRequestId(requestid);
                ROIAdminRequestStatusPage adminRequestStatusPage = new ROIAdminRequestStatusPage(driver, logger, TestContext);
                ROIAdminAssignROIRequesterPage assignROIRequesterPage = new ROIAdminAssignROIRequesterPage(driver, logger, TestContext);
                adminRequestStatusPage.assignRequester();
                assignROIRequesterPage.assignTestAttorney();
                adminRequestStatusPage.ClickPassDocsQC();
                adminRequestStatusPage.RateLink();

                ROIAdminUpdateRequestBillingDetailsPage rOIAdminUpdateRequestBillingDetailsPage = new ROIAdminUpdateRequestBillingDetailsPage(driver, logger, TestContext);
                rOIAdminUpdateRequestBillingDetailsPage.UpdateRegressionBaseRate();
                adminRequestStatusPage.ClickApplyRateButton();
                string adjustedAmountOnRSSPage = adminRequestStatusPage.GetAdjustedBalanceAmountOnRSS();
                logger.Log(Status.Info, $"Adjusted balance amount on admin request status page is = ({adjustedAmountOnRSSPage})", TakeScreenShotAtStep());

                adminRequestStatusPage.CreateInvoice();
                bool _isDisplayed = adminRequestStatusPage.VerifyInvoiceCreated();
                Assert.IsTrue(_isDisplayed, "Failed to create invoice");

                ROIAdminWriteOffsPage rOIAdminWriteOffsPage = new ROIAdminWriteOffsPage(driver, logger, TestContext);
                rOIAdminWriteOffsPage.SwitchToDefaultRSSPage();
                LoginPage loginPage = new LoginPage(driver, logger, TestContext);
                loginPage.LogOut();
                rOIAdminHomePage.ROIAdminLoginForSpecificUser();                
                rOIAdminHomePage.SelectFacilityList();                
                rOIAdminFacilityListPage.GoToROITestFacility();
                rOIAdminHomePage.ROIlookupByRequestId(requestid);
                rOIFacilityRequestStatusPage.ClickOnChangeAmountAndMakeOnsitePayment(adjustedAmountOnRSSPage);
                ROIAdminAddNewPaymentMethodPage roiAdminAddNewPaymentMethodPage = new ROIAdminAddNewPaymentMethodPage(driver, logger, TestContext);
                roiAdminAddNewPaymentMethodPage.AddNewPayment();
                logger.Log(Status.Info, $"Payment added");
                ROIAdminPaymentApprovedPage roiAdminPaymentApprovedPage = new ROIAdminPaymentApprovedPage(driver, logger, TestContext);
                string approvalCode = roiAdminPaymentApprovedPage.ReturnApprovalCode();
                roiAdminPaymentApprovedPage.GoToCCPrintableReceiptPage();

                ROIAdminCCPrintableReceiptPage roiAdminCCPrintableReceiptPage = new ROIAdminCCPrintableReceiptPage(driver, logger, TestContext);
                roiAdminCCPrintableReceiptPage.SwitchToCCPrintableReceiptWindow();
                string requestIdCCPrintableReceiptPage = roiAdminCCPrintableReceiptPage.ReturnRequestId();
                string approvalCodeCCPrintableReceiptPage = roiAdminCCPrintableReceiptPage.ReturnApprovalCode();
                Assert.AreEqual(requestIdCCPrintableReceiptPage, requestid, "Failed to validate request id on CCPrintable Receipt Page");
                Assert.AreEqual(approvalCode, approvalCodeCCPrintableReceiptPage, "Failed to validate approval code on CCPrintable Receipt Page");
                logger.Log(Status.Pass, $"Requestid and approvalcode are validated requestid: ({requestIdCCPrintableReceiptPage}) approvalcode: ({approvalCodeCCPrintableReceiptPage})");
                logger.Log(Status.Info, "TransactionDetails for refrence:", TakeScreenShotAtStep());
                roiAdminCCPrintableReceiptPage.CloseCCPrintableReceiptWindow();

                rOIAdminHomePage.SwitchToPreviousTab(BaseAddress);
                rOIAdminHomePage.ROIlookupByRequestId(requestid);
                string adjustedAmountOnRSSPage1 = adminRequestStatusPage.GetAdjustedBalanceAmountOnRSS();
                logger.Log(Status.Info, $"Adjusted balance amount on admin request status page is = ({adjustedAmountOnRSSPage1})", TakeScreenShotAtStep());

                Cleanup(driver);
            }
            catch (Exception ex)
            {
                LogException(driver, logger, ex);
            }
        }
    }
}