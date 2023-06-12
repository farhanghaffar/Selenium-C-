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
    public class MRODeliveryCreditCardPaymentThroughChangeAmountOrViewDetailsTest : ROIBaseTest
    {
        public MRODeliveryCreditCardPaymentThroughChangeAmountOrViewDetailsTest() : base(ROITestArea.ROIAdmin)
        {

        }

        [STATestMethodAttribute]
        [TestCategory(ROITestCategory.Regression)]
        //Converted manual test case 745 -ROI Facility-->MRO Delivery Credit Card Payment through  "Change Amount/View Details" to automated.
        public void Reg_745_MRODeliveryCreditCardPaymentThroughChangeAmountOrViewDetailsTest()
        {
            logger = extent.CreateTest("Reg_745_MRODeliveryCreditCardPaymentThroughChangeAmountOrViewDetailsTest");
            logger.Log(Status.Info, "Converted manual test case 745 -ROI Facility-->MRO Delivery Credit Card Payment through Change Amount / View Details to automated");
            RemoteWebDriver _driver = Init(TestContext.Properties["browser"].ToString());
            ThreadLocal<RemoteWebDriver> localDriver = new ThreadLocal<RemoteWebDriver>();
            localDriver.Value = _driver;
            RemoteWebDriver driver = localDriver.Value;
            string requestID = string.Empty;
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
                rOIFacilityLogNewRequestPage.CreateNewMRODeliveryRequestForBostonProper();
                ROIFacilityRequestStatusPage rOIFacilityRequestStatusPage = new ROIFacilityRequestStatusPage(driver, logger, TestContext);
                string requestid = rOIFacilityRequestStatusPage.GetRequestID();
                logger.Log(Status.Pass, $"Request created with requestid({requestID})", TakeScreenShotAtStep());

                rOIAdminHomePage.ROIlookupByRequestId(requestid);
                rOIFacilityRequestStatusPage.ImportPdfFiles();
                bool statusUnderImportDocument = rOIFacilityRequestStatusPage.VerifyStatusUnderImportDocument();
                Assert.IsTrue(statusUnderImportDocument, "Failed to verify status under import document is uploaded");
                rOIFacilityRequestStatusPage.ReleaseRequestID();
                
                rOIAdminHomePage.SwitchToNewTabAndLoginROIAdmin(BaseAddress);
                rOIAdminHomePage.ROIlookupByRequestId(requestid);
                ROIAdminRequestStatusPage adminRequestStatusPage = new ROIAdminRequestStatusPage(driver, logger, TestContext);
                adminRequestStatusPage.assignRequester();
                ROIAdminAssignROIRequesterPage assignROIRequesterPage = new ROIAdminAssignROIRequesterPage(driver, logger, TestContext);
                assignROIRequesterPage.assignTestAttorney();
                logger.Log(Status.Info, "Assigned Test Attorney");

                rOIAdminHomePage.SwitchBackToROITestFacilitySide(BaseAddress);
                rOIFacilityWorkSummaryPage.SearchByRequestId(requestid);
                rOIFacilityRequestStatusPage.ClickOnChangeAmountAndMakeOnsitePayment("$10.00");
                ROIAdminAddNewPaymentMethodPage roiAdminAddNewPaymentMethodPage = new ROIAdminAddNewPaymentMethodPage(driver, logger, TestContext);
                roiAdminAddNewPaymentMethodPage.AddNewPayment();
                logger.Log(Status.Info, $"Payment added");
                ROIAdminPaymentApprovedPage roiAdminPaymentApprovedPage = new ROIAdminPaymentApprovedPage(driver, logger, TestContext);
                roiAdminPaymentApprovedPage.RefreshPaymentHistory();
                string approvalCode = roiAdminPaymentApprovedPage.ReturnApprovalCode();               
                roiAdminPaymentApprovedPage.GoToCCPrintableReceiptPage();

                ROIAdminCCPrintableReceiptPage roiAdminCCPrintableReceiptPage = new ROIAdminCCPrintableReceiptPage(driver, logger, TestContext);
                roiAdminCCPrintableReceiptPage.SwitchToCCPrintableReceiptWindow();
                string requestIdCCPrintableReceiptPage = roiAdminCCPrintableReceiptPage.ReturnRequestId();
                string approvalCodeCCPrintableReceiptPage = roiAdminCCPrintableReceiptPage.ReturnApprovalCode();
                Assert.AreEqual(requestIdCCPrintableReceiptPage, requestid, "Failed to validate request id on CCPrintable Receipt Page");
                Assert.AreEqual(approvalCode, approvalCodeCCPrintableReceiptPage, "Failed to validate approval code on CCPrintable Receipt Page");
                logger.Log(Status.Info, "TransactionDetails for reference:", TakeScreenShotAtStep());
                roiAdminCCPrintableReceiptPage.CloseCCPrintableReceiptWindow();

                rOIAdminHomePage.SwitchToPreviousTab(BaseAddress);
                rOIAdminHomePage.ROIlookupByRequestId(requestid);
                string adjustedAmountOnRSSPage1 = adminRequestStatusPage.GetAdjustedBalanceAmountOnRSS();
                logger.Log(Status.Info, $"Adjusted balance amount on admin request status page is = ({adjustedAmountOnRSSPage1})", TakeScreenShotAtStep());
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

