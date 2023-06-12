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
    public class InternalPortalDeliveryCreditCardPaymentThroughChangeAmountOrViewDetailsTest: ROIBaseTest
    {
        public InternalPortalDeliveryCreditCardPaymentThroughChangeAmountOrViewDetailsTest() : base(ROITestArea.ROIAdmin)
        {

        }

        [STATestMethodAttribute]
        [TestCategory(ROITestCategory.Regression)]
        //Converted manual test case 744 -ROI Facility-->Internal Portal Delivery Credit Card Payment through  "Change Amount/View Details to automated.
        public void Reg_744_InternalPortalDeliveryCreditCardPaymentThroughChangeAmountOrViewDetailsTest()
        {
            logger = extent.CreateTest("Reg_744_InternalPortalDeliveryCreditCardPaymentThroughChangeAmountOrViewDetailsTest");
            logger.Log(Status.Info, "Converted manual test case 744 -ROI Facility-->Internal Portal Delivery Credit Card Payment through Change Amount / View Details to automated");
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
                rOIFacilityLogNewRequestPage.ClickOnInternalPortalTab();
                rOIFacilityLogNewRequestPage.CreateInternalPortalDeliveryRequest();
                ROIFacilityRequestStatusPage rOIFacilityRequestStatusPage = new ROIFacilityRequestStatusPage(driver, logger, TestContext);
                requestID = rOIFacilityRequestStatusPage.GetRequestID();
                requestID = requestID.Trim();
                logger.Log(Status.Pass, $"Request created with requestid({requestID})", TakeScreenShotAtStep());
                rOIAdminHomePage.ROIlookupByRequestId(requestID);
                rOIAdminHomePage.SwitchToNewTabAndLoginROIAdmin(BaseAddress);
                rOIAdminHomePage.ROIlookupByRequestId(requestID);
                ROIAdminRequestStatusPage rOIAdminRequestStatusPage = new ROIAdminRequestStatusPage(driver, logger, TestContext);
                string adjustedBalance = rOIAdminRequestStatusPage.GetAdjustedBalanceAmountOnRSS();
                Assert.AreEqual("$0.00", adjustedBalance, "Failed to validate adjusted balance amount");
                logger.Log(Status.Pass, $"Verified adjustable balance amount as ({adjustedBalance})", TakeScreenShotAtStep());
                ROIAdminWriteOffsPage rOIAdminWriteOffsPage = new ROIAdminWriteOffsPage(driver, logger, TestContext);
                rOIAdminWriteOffsPage.SwitchToDefaultRSSPage();
                LoginPage loginPage = new LoginPage(driver, logger, TestContext);
                loginPage.LogOut();
                rOIAdminHomePage.ROIAdminLoginForSpecificUser();
                rOIAdminHomePage.SelectFacilityList();
                rOIAdminFacilityListPage.GoToROITestFacility();
                rOIAdminHomePage.ROIlookupByRequestId(requestID);
                rOIFacilityRequestStatusPage.ClickOnChangeAmountOrViewDetails("$10.00");

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
                Assert.AreEqual(requestIdCCPrintableReceiptPage, requestID, "Failed to validate request id on CCPrintable Receipt Page");
                Assert.AreEqual(approvalCode, approvalCodeCCPrintableReceiptPage, "Failed to validate approval code on CCPrintable Receipt Page");
                logger.Log(Status.Pass, $"Requestid and approvalcode are validated requestid: ({requestIdCCPrintableReceiptPage}) approvalcode: ({approvalCodeCCPrintableReceiptPage})");
                logger.Log(Status.Info, "TransactionDetails for refrence:",TakeScreenShotAtStep());
                roiAdminCCPrintableReceiptPage.CloseCCPrintableReceiptWindow();
            }
            catch (Exception ex)
            {
             LogException(driver, logger, ex);
            }
        }
    }

}
