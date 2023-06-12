
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
    public class ROIConfirmThatANewInvoiceRevisionIsCreatedTest : ROIBaseTest
    {
        public ROIConfirmThatANewInvoiceRevisionIsCreatedTest() : base(ROITestArea.ROIAdmin)
        {

        }

        [STATestMethodAttribute]
        [TestCategory(ROITestCategory.Regression)]
        // Converted manual test case 10795-ROI-Admin-->Confirm that a new invoice revision is created when the payment is applied to automated.
        public void Reg_10795_ROIConfirmThatANewInvoiceRevisionIsCreatedTest()
        {
            logger = extent.CreateTest("Reg_10795_ROIConfirmThatANewInvoiceRevisionIsCreatedTest");
            logger.Log(Status.Info, "Converted manual test case 10795-ROI-Admin-->Confirm that a new invoice revision is created when the payment is applied to automated");
            RemoteWebDriver _driver = Init(TestContext.Properties["browser"].ToString());
            ThreadLocal<RemoteWebDriver> localDriver = new ThreadLocal<RemoteWebDriver>();
            localDriver.Value = _driver;
            RemoteWebDriver driver = localDriver.Value;

            try
            {

                ROIAdminHomePage rOIAdminHomePage = new ROIAdminHomePage(driver, logger, TestContext);
                rOIAdminHomePage.FacilityList();
                ROIAdminFacilityListPage rOIAdminFacilityListPage = new ROIAdminFacilityListPage(driver, logger, TestContext);
                rOIAdminFacilityListPage.GotoROITestFacilityComputerIcon();

                ROIFacilityWorkSummaryPage rOIFacilityWorkSummaryPage = new ROIFacilityWorkSummaryPage(driver, logger, TestContext);
                rOIFacilityWorkSummaryPage.GoToLogNewRequestPage();
                ROIFacilityLogNewRequestPage rOIFacilityLogNewRequestPage = new ROIFacilityLogNewRequestPage(driver, logger, TestContext);
                rOIFacilityLogNewRequestPage.ClickMRODeliveryTab();
                rOIFacilityLogNewRequestPage.MRODeliveryRequestForBostonProper();
                LogNewRequestPage logNewRequestPage = new LogNewRequestPage(driver, logger, TestContext);
                string requestId = logNewRequestPage.getRequestid();
                logger.Log(Status.Info, $"MRO delivery request created ({requestId})", TakeScreenShotAtStep());

                ROIAdminHomePage roiAdminHomePage = new ROIAdminHomePage(driver, logger, TestContext);
                roiAdminHomePage.ROIlookupByRequestId(requestId);

                ROIFacilityRequestStatusPage roiFacilityRequestStatusPage = new ROIFacilityRequestStatusPage(driver, logger, TestContext);
                roiFacilityRequestStatusPage.ImportPdfFilesForNewInvoiceRevision();
                roiFacilityRequestStatusPage.ReleaseRequestID();
                roiFacilityRequestStatusPage.FacilityLogout();

                ROIAdminHomePage roiadminHomePage = new ROIAdminHomePage(driver, logger, TestContext);
                roiadminHomePage.ROIlookupByRequestId(requestId);
                ROIAdminRequestStatusPage rOIAdminRequestStatusPage = new ROIAdminRequestStatusPage(driver, logger, TestContext);
                rOIAdminRequestStatusPage.assignRequester();
                ROIAdminAssignROIRequesterPage rOIAdminAssignROIRequester = new ROIAdminAssignROIRequesterPage(driver, logger, TestContext);
                rOIAdminAssignROIRequester.assignTestAttorney();

                rOIAdminRequestStatusPage.DeliveryOverride("FTP");
                logger.Log(Status.Info, $"Delivery override selected as FTP", TakeScreenShotAtStep());
                rOIAdminRequestStatusPage.ClickOnQcPassButton();

                rOIAdminRequestStatusPage.RateLink();
                ROIAdminUpdateRequestBillingDetailsPage rOIAdminUpdateRequestBillingDetailsPage = new ROIAdminUpdateRequestBillingDetailsPage(driver, logger, TestContext);
                rOIAdminUpdateRequestBillingDetailsPage.UpdateRegressionBaseRate();
                // rOIAdminRequestStatusPage.ApplyRate();
                rOIAdminRequestStatusPage.CreateInvoice();

                roiAdminHomePage.SwitchToNewTabAndLoginROIRequesterPortal(BaseAddress);
                ROITestRequesterPortalHomePage rOITestRequesterPortalHomePage = new ROITestRequesterPortalHomePage(driver, logger, TestContext);
                rOITestRequesterPortalHomePage.ClickOnNotificationPopUp();
                rOITestRequesterPortalHomePage.GotoPayForRecords();
                ROIPayForRecordsPage rOIPayForRecordsPage = new ROIPayForRecordsPage(driver, logger, TestContext);
                rOIPayForRecordsPage.ClickShowRequestButton();

                rOIPayForRecordsPage.PayForSelectedRecord(requestId);
                ROIConfirmDeliveryMethodsPage rOIConfirmDeliveryMethodsPage = new ROIConfirmDeliveryMethodsPage(driver, logger, TestContext);
                rOIConfirmDeliveryMethodsPage.ClickShowRequestButton();
                rOIConfirmDeliveryMethodsPage.UpdateDeliveryMethod();
                string ChangeAmount = rOIConfirmDeliveryMethodsPage.SelectCreditCardRadioButton();

                string amount = rOIConfirmDeliveryMethodsPage.AddNewPayment();
                Assert.AreEqual(ChangeAmount, amount, "Failed to verify change amount");
                logger.Log(Status.Pass, "Verified payment generated successfully", TakeScreenShotAtStep());
                string approvalCode = rOIConfirmDeliveryMethodsPage.GetApprovalCodeInPaymentPage();
                string generatedRequId = rOIConfirmDeliveryMethodsPage.GoToCCPrintableReceiptPage();
                logger.Log(Status.Pass, $"Verified transaction information generated with  request id:{(generatedRequId)} ", TakeScreenShotAtStep());
                string _approvalCode = rOIConfirmDeliveryMethodsPage.GetApprovalCodeInCreditCardPayment();
                //Assert.AreEqual(approvalCode, _approvalCode, "Approval code differnt in payment and credit card payment page");
                rOIConfirmDeliveryMethodsPage.CloseCreditCardPaymentWindow();

                roiAdminHomePage.SwitchToPreviousTab(BaseAddress);
                roiAdminHomePage.ROIlookupByRequestId(requestId);
                string selectedMethod= rOIAdminRequestStatusPage.SelectedDeliveryMethod();
                logger.Log(Status.Info, $"Verified delivery method set to {(selectedMethod)}",TakeScreenShotAtStep());

                rOIAdminRequestStatusPage.ClickLedgerDetailButton();
                ROIAdminLedgerDetailPage rOIAdminLedgerDetailPage = new ROIAdminLedgerDetailPage(driver, logger, TestContext);
                string invoiceId = rOIAdminLedgerDetailPage.VerifyInvoiceId();
                logger.Log(Status.Pass, $"Verified new invoice revision id created in ledger details page with id:{(invoiceId)}", TakeScreenShotAtStep());
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


