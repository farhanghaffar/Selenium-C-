using AventStack.ExtentReports;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MRO.ROI.Automation.Common;
using MRO.ROI.Automation.Pages;
using MRO.ROI.Test.ExecutionFactory;
using MRO.ROI.Test.Utilities;
using OpenQA.Selenium.Remote;
using System;
using System.Threading;

namespace MRO.ROI.Test.RegressionTests.MROROITests
{
    [TestClass]
    public class ROIAdminRequestStatusPayByCCTest: ROIBaseTest
    { 
    public ROIAdminRequestStatusPayByCCTest() : base(ROITestArea.ROIAdmin)
    {

    }

    [STATestMethodAttribute]
    [TestCategory(ROITestCategory.Regression)]
    //Converted manual test case 740 -ROI-Admin--> Request Status-->Pay By CC to automated.
    public void Reg_740_ROIAdminRequestStatusPayByCCTest()
    {
        logger = extent.CreateTest("Reg_740_ROIAdminRequestStatusPayByCCTest");
        logger.Log(Status.Info, "Converted manual test case 740 -ROI-Admin--> Request Status-->Pay By CC to automated");
        RemoteWebDriver _driver = Init(TestContext.Properties["browser"].ToString());
        ThreadLocal<RemoteWebDriver> localDriver = new ThreadLocal<RemoteWebDriver>();
        localDriver.Value = _driver;
        RemoteWebDriver driver = localDriver.Value;
        try
        {
            Iframe frame = new Iframe(driver, logger, TestContext);
            ROIAdminHomePage rOIAdminHomePage = new ROIAdminHomePage(driver, logger, TestContext);
            rOIAdminHomePage.FacilityList();
            ROIAdminFacilityListPage rOIAdminFacilityListPage = new ROIAdminFacilityListPage(driver, logger, TestContext);
            rOIAdminFacilityListPage.GotoROITestFacilityComputerIcon();
            bool isWorkSummaryShowing = rOIAdminFacilityListPage.IsWorkSummaryHeadingShowing();
            Assert.IsTrue(isWorkSummaryShowing, "Failed to verify work summary page is showing.");
            logger.Log(Status.Info, "Verified work summary page is showing.");
            ROIMenuSelector menuSelector = new ROIMenuSelector(driver, logger, TestContext);
            menuSelector.SelectTableBasedMenu("ROI Requests", "Log a New Request");
           // frame.SwitchToRoiFrame();
            ROIFacilityLogNewRequestPage rOIFacilityLogNewRequestPage = new ROIFacilityLogNewRequestPage(driver, logger, TestContext);
            LogNewRequestPage logNewRequestPage = new LogNewRequestPage(driver, logger, TestContext);
            logNewRequestPage.ClickInternalPortalTab();
            logNewRequestPage.ClickMroDelivry();
            rOIFacilityLogNewRequestPage.MRODeliveryRequestForBostonProper();
            string requestId = logNewRequestPage.getRequestid();
            logger.Log(Status.Info, $"MRO delivery request created ({requestId})", TakeScreenShotAtStep());
          //  frame.switchToDefaut();
            ROIAdminHomePage roiAdminHomePage = new ROIAdminHomePage(driver, logger, TestContext);
            roiAdminHomePage.ROIlookupByRequestId(requestId);
           // frame.SwitchToRoiFrame();

            ROIFacilityRequestStatusPage roiFacilityRequestStatusPage = new ROIFacilityRequestStatusPage(driver, logger, TestContext);
            roiFacilityRequestStatusPage.HideLicenseRequireLabelIfShowing();
            roiFacilityRequestStatusPage.ImportPdfFiles();
            roiFacilityRequestStatusPage.ReleaseRequestID();
            frame.switchToDefaut();
            rOIAdminHomePage.SelectLogoutFromProfile();
            ROIAdminHomePage roiadminHomePage = new ROIAdminHomePage(driver, logger, TestContext);
            roiadminHomePage.ROIlookupByRequestId(requestId);
            ROIAdminRequestStatusPage rOIAdminRequestStatusPage = new ROIAdminRequestStatusPage(driver, logger, TestContext);
            frame.SwitchToRoiFrame();
            rOIAdminRequestStatusPage.assignRequester();
            ROIAdminAssignROIRequesterPage rOIAdminAssignROIRequester = new ROIAdminAssignROIRequesterPage(driver, logger, TestContext);
            rOIAdminAssignROIRequester.assignTestAttorney();

            rOIAdminRequestStatusPage.ClickOnQcPassButton();
            rOIAdminRequestStatusPage.RateLink();
            ROIAdminUpdateRequestBillingDetailsPage rOIAdminUpdateRequestBillingDetailsPage = new ROIAdminUpdateRequestBillingDetailsPage(driver, logger, TestContext);
            rOIAdminUpdateRequestBillingDetailsPage.UpdateRegressionBaseRate();
            // rOIAdminRequestStatusPage.ApplyRate();
            rOIAdminRequestStatusPage.CreateInvoice();
            rOIAdminRequestStatusPage.ClickOnPayByCCButton();
            ROIAdminCCShoppingCartPage roiAdminCCShoppingCartPage = new ROIAdminCCShoppingCartPage(driver, logger, TestContext);
            roiAdminCCShoppingCartPage.CheckOut();

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

            Assert.AreEqual(requestIdCCPrintableReceiptPage, requestId, "Failed to validate request id on CCPrintable Receipt Page");
            Assert.AreEqual(approvalCode, approvalCodeCCPrintableReceiptPage, "Failed to validate approval code on CCPrintable Receipt Page");
            logger.Log(Status.Pass, $"Requestid and approvalcode are validated requestid: ({requestIdCCPrintableReceiptPage}) approvalcode: ({approvalCodeCCPrintableReceiptPage})");
            logger.Log(Status.Info, "TransactionDetails for refrence:", TakeScreenShotAtStep());
            roiAdminCCPrintableReceiptPage.LogTransactionalData();
            logger.Log(Status.Info, $"Requestid and approvalcode are validated requestid: ({requestIdCCPrintableReceiptPage}) approvalcode: ({approvalCodeCCPrintableReceiptPage})");

             roiAdminCCPrintableReceiptPage.CloseCCPrintableReceiptWindow();
             roiAdminPaymentApprovedPage.GoToFindARequestPage();
             roiAdminHomePage.ROIlookupByRequestId(requestId);

             string adjustedBalance = rOIAdminRequestStatusPage.GetAdjustedBalanceAmountOnRSS();
             Assert.AreEqual("$0.00", adjustedBalance, "Failed to validate adjusted balance amount");
             logger.Log(Status.Pass, $"Verified adjustable balance amount as ({adjustedBalance})", TakeScreenShotAtStep());
             Cleanup(driver);

        }
        catch (Exception ex)
        {
            LogException(driver, logger, ex);

        }
    }
}

}


