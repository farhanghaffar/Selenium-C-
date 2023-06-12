using AventStack.ExtentReports;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MRO.ROI.Automation.Pages;
using MRO.ROI.Automation.Pages.Common;
using MRO.ROI.Test.Utilities;
using OpenQA.Selenium.Remote;
using System;
using System.Threading;
using MRO.ROI.Automation.Selenium;
using MRO.ROI.Test.ExecutionFactory;
using MRO.ROI.Automation.Common;

namespace MRO.ROI.Test.RegressionTests.MROROITests
{
    [TestClass]
    public class DukeInterimTransactionalModelReportTest : ROIBaseTest
    {
        public DukeInterimTransactionalModelReportTest() : base(ROITestArea.ROIAdmin)
        {
        }

        [STATestMethodAttribute]
        [TestCategory(ROITestCategory.Regression)]
        //Converted manual test case 11285-ROI-Admin-->Duke Interim Transactional Model Report to automated
        public void Reg_11285_ROIAdminDukeInterimTransactionalModelReportTest()
        {

            logger = extent.CreateTest("Reg_11285_ROIAdminDukeInterimTransactionalModelReport");
            logger.Log(Status.Info, "Converted manual test case 11285-ROI-Admin-->Duke Interim Transactional Model Report to automated");
            RemoteWebDriver _driver = Init(TestContext.Properties["browser"].ToString());
            ThreadLocal<RemoteWebDriver> localDriver = new ThreadLocal<RemoteWebDriver>();
            localDriver.Value = _driver;
            RemoteWebDriver driver = localDriver.Value;
            ROIAdminTransactionalModelReportPage rOIAdminTransactionalModelReportPage = new ROIAdminTransactionalModelReportPage(driver, logger, TestContext);
            ROIAdminHomePage rOIAdminHomePage = new ROIAdminHomePage(driver, logger, TestContext);

            try
            {
                rOIAdminTransactionalModelReportPage.DeleteDownloadedExcelFiles();
                ROIMenuSelector menuSelector = new ROIMenuSelector(driver, logger, TestContext);
                Iframe frame = new Iframe(driver, logger, TestContext);
                /*
                 menuSelector.Select("Financial", "Transactional Model Report");
                 frame.SwitchToRoiFrame();
                 rOIAdminTransactionalModelReportPage.CreateTransactionalModelReport("[All]", "[All]", true);
                 logger.Log(Status.Info, "Transactional report created for Facility : [All] Contract : [All] IncludeTest : True");

                 rOIAdminTransactionalModelReportPage.DownloadExcelReport();
                 frame.switchToDefaut();

                 var excelTransactionalModelAllData = rOIAdminTransactionalModelReportPage.ReturnExcelTransactionalModelReportData();
                 logger.Log(Status.Info,"Excel data : " + excelTransactionalModelAllData);
                 var uiTransactionalModelAllData = rOIAdminTransactionalModelReportPage.ReturnUITransactionalModelReportData();
                 bool isValidatedTransactionalModelDataForFirstAndLastLinks = rOIAdminTransactionalModelReportPage.ClickOnFirstAndLastLinksAndVerifyPDFAndExcelHasDataIntact();
                 Assert.IsTrue(isValidatedTransactionalModelDataForFirstAndLastLinks, "Failed to validate the first and last links of the transactional model data");
                 logger.Log(Status.Pass, "Valiated UI and excel transactional model summary report data for filter Facility : [All] Contract : [All] IncludeTest : True");

                 menuSelector.Select("Financial", "Transactional Model Report");
                 frame.SwitchToRoiFrame();
                 rOIAdminTransactionalModelReportPage.CreateTransactionalModelReport("MRO Automated Regression Test", "[All]", true);
                 logger.Log(Status.Info, "Transactional report created for Facility : MRO Automated Regression Test Contract : [All] IncludeTest : True");

                 string requestLoggedAmountForMROEmployee = rOIAdminTransactionalModelReportPage.GetSpecificAmountFromLink("Requests Logged by MRO Employees:");
                 logger.Log(Status.Info, $"Requests logged count ({requestLoggedAmountForMROEmployee})", TakeScreenShotAtStep());
                 string requestReleasedAmountForMROEmployee = rOIAdminTransactionalModelReportPage.GetSpecificAmountFromLink("Requests Released by MRO Employees:");
                 logger.Log(Status.Info, $"Requests released count ({requestReleasedAmountForMROEmployee})", TakeScreenShotAtStep());

                 rOIAdminTransactionalModelReportPage.DownloadExcelReport();
                 var excelTransactionalModelFacilityData = rOIAdminTransactionalModelReportPage.ReturnExcelTransactionalModelReportData();
                 var uiTransactionalModelFacilityData = rOIAdminTransactionalModelReportPage.ReturnUITransactionalModelReportData();

                 frame.switchToDefaut();
                 */

                ROIAdminFacilityListPage roiAdminFacilityListPage = new ROIAdminFacilityListPage(driver, logger, TestContext);
                ROIFacilityWorkSummaryPage rOIFacilityWorkSummaryPage = new ROIFacilityWorkSummaryPage(driver, logger, TestContext);
                LogNewRequestPage logNewRequestPage = new LogNewRequestPage(driver, logger, TestContext);
                ROIAdminHomePage roiAdminHomePage = new ROIAdminHomePage(driver, logger, TestContext);
                ROIFacilityRequestStatusPage roiFacilityRequestStatusPage = new ROIFacilityRequestStatusPage(driver, logger, TestContext);
                ROIAdminRequestStatusPage roiAdminRequestStatusPage = new ROIAdminRequestStatusPage(driver, logger, TestContext);

                /*
                 menuSelector.Select("Facilities", "Facility List");


                 frame.SwitchToRoiFrame();
                 roiAdminFacilityListPage.NavigateToMROAutomationRegressionTestFacility();

                 frame.switchToDefaut();
                 rOIFacilityWorkSummaryPage.GoToLogNewRequestPage();

                 logNewRequestPage.CreateNewMRODeliveryRequest("Boston Proper");
                 string requestId = logNewRequestPage.getRequestid();
                 logger.Log(Status.Info, $"MRO delivery request created ({requestId})");

                 roiAdminHomePage.ROIlookupByRequestId(requestId);

                 roiFacilityRequestStatusPage.ImportPatientPages();
                 roiFacilityRequestStatusPage.ReleaseRequestID();
                 logger.Log(Status.Info, $"Request released");
                 roiFacilityRequestStatusPage.UnReleaseRequestID();
                 logger.Log(Status.Info, $"Request un-released");
                 roiFacilityRequestStatusPage.ReleaseRequestID();
                 logger.Log(Status.Info, $"Request released");
                 roiFacilityRequestStatusPage.HideLicenseRequireLabelIfShowing();
                 LoginPage loginPage = new LoginPage(driver, logger, TestContext);
                 loginPage.LogOut();
                 frame.switchToDefaut();

                 menuSelector.Select("Financial", "Transactional Model Report");
                 frame.SwitchToRoiFrame();
                 rOIAdminTransactionalModelReportPage.CreateTransactionalModelReport("MRO Automated Regression Test", "[All]", true);
                 logger.Log(Status.Info, "Transactional report created for Facility : MRO Automated Regression Test Contract : [All] IncludeTest : True");

                 string requestLoggedAmountForMROEmployeeAfterRelease = rOIAdminTransactionalModelReportPage.GetSpecificAmountFromLink("Requests Logged by MRO Employees:");
                 logger.Log(Status.Info, $"Requests logged count ({requestLoggedAmountForMROEmployeeAfterRelease})", TakeScreenShotAtStep());
                 string requestReleasedAmountForMROEmployeeAfterRelease = rOIAdminTransactionalModelReportPage.GetSpecificAmountFromLink("Requests Released by MRO Employees:");
                 logger.Log(Status.Info, $"Requests released count ({requestReleasedAmountForMROEmployeeAfterRelease})", TakeScreenShotAtStep());

              //   Assert.IsTrue(Convert.ToInt32(requestLoggedAmountForMROEmployeeAfterRelease) > (Convert.ToInt32(requestLoggedAmountForMROEmployee)), "Failed to validate request logged count");
               //  Assert.IsTrue(Convert.ToInt32(requestReleasedAmountForMROEmployeeAfterRelease) > (Convert.ToInt32(requestReleasedAmountForMROEmployee)), "Failed to validate request released count");
                 logger.Log(Status.Pass, "Validated requests logged and requests released counts");

                 rOIAdminTransactionalModelReportPage.ClickOnSpecificAmountLink("Requests Logged by MRO Employees:");
                 rOIAdminTransactionalModelReportPage.FilterReportBasedOnRequestDate("sr_dtLoggedRequests");
                 bool isRequestIdExistRL = rOIAdminTransactionalModelReportPage.CheckRequestIdExsist(requestId);
                 Assert.IsTrue(isRequestIdExistRL, "Failed to validate request under Requests Logged by MRO Employees:");
                 logger.Log(Status.Pass, $"Validated requestid ({requestId}) under Requests Logged by MRO Employees:", TakeScreenShotAtStep());

                 bool isValidatedRequestsLoggedByMROEmployee = rOIAdminTransactionalModelReportPage.ValidateForExcelAndPDFContents("sr_dtLoggedRequests", requestId);
                 Assert.IsTrue(isValidatedRequestsLoggedByMROEmployee, "Failed to validate Excel and PDF data for request logged by MRO Employees:");
                 logger.Log(Status.Pass, "Valiated UI and excel transactional model summary report data for request logged by MRO Employees:");
                 rOIAdminTransactionalModelReportPage.ClickOnSpecificAmountLink("Requests Logged by MRO Employees:");

                 rOIAdminTransactionalModelReportPage.ClickOnSpecificAmountLink("Requests Released by MRO Employees:");
                 rOIAdminTransactionalModelReportPage.FilterReportBasedOnRequestDate("sr_dtReleasedRequests");
                 bool isRequestIdExistRR = rOIAdminTransactionalModelReportPage.CheckRequestIdExsist(requestId);
                 Assert.IsTrue(isRequestIdExistRR, "Failed to validate request under Requests Released by MRO Employees");
                 logger.Log(Status.Pass, $"Validated requestid ({requestId}) under Requests Released by MRO Employees", TakeScreenShotAtStep());

                 bool isValidatedRequestsReleasedByMROEmployee = rOIAdminTransactionalModelReportPage.ValidateForExcelAndPDFContents("sr_dtReleasedRequests", requestId);
                 Assert.IsTrue(isValidatedRequestsReleasedByMROEmployee, "Failed to validate Excel and PDF data for Requests Released by MRO Employees");
                 logger.Log(Status.Pass, "Valiated UI and excel transactional model summary report data for Requests Released by MRO Employees");
                 rOIAdminTransactionalModelReportPage.ClickOnSpecificAmountLink("Requests Released by MRO Employees:");

                 frame.switchToDefaut();
                 roiAdminHomePage.ROIlookupByRequestId(requestId);
                 frame.SwitchToRoiFrame();
                 ROIAdminAssignROIRequesterPage assignROIRequesterPage = new ROIAdminAssignROIRequesterPage(driver, logger, TestContext);

                 assignROIRequesterPage.AssignRequest();
                 logger.Log(Status.Info, $"Request assigned");

                 logger.Log(Status.Info, "Verify that requester 'Test Attorney's' text appears as on the (Request Status) page.");
                 Assert.IsTrue(assignROIRequesterPage.IsAttorneyShowing("Test Attorney"), "Failed : Requester 'Test Attorney's' text not displaying as on the (Request Status) page.");
                 logger.Log(Status.Pass, "Verified that requester 'Test Attorney's' text appears as on the (Request Status) page.", TakeScreenShotAtStep());

                 assignROIRequesterPage.RemoveRequest();
                 logger.Log(Status.Info, $"Request removed");

                 logger.Log(Status.Info, "Verify that requester 'Test Attorney's' removed on the (Request Status) page.");
                 Assert.IsFalse(assignROIRequesterPage.IsAttorneyShowing("Test Attorney"), "Failed : Requester 'Test Attorney's' text appears as on the (Request Status) page.");
                 logger.Log(Status.Pass, "Verified that requester 'Test Attorney's' removed on the (Request Status) page.", TakeScreenShotAtStep());

                 assignROIRequesterPage.AssignRequest();
                 logger.Log(Status.Info, $"Request assigned");
                 roiAdminRequestStatusPage.ClickPassDocsQC();
                 roiAdminRequestStatusPage.DeliveryOverride("EMAIL");
                 logger.Log(Status.Info, $"Delivery override selected as Email");
                 roiAdminRequestStatusPage.CreateInvoice();
                 logger.Log(Status.Info, $"Invoice created");
                 */
                var requestId = "23703688";
                frame.switchToDefaut();

                menuSelector.Select("Financial", "Transactional Model Report");
                /*
                frame.SwitchToRoiFrame();
                rOIAdminTransactionalModelReportPage.CreateTransactionalModelReport("MRO Automated Regression Test", "[All]", true);
                logger.Log(Status.Info, "Transactional report created for Facility : MRO Automated Regression Test Contract : [All] IncludeTest : True");

                rOIAdminTransactionalModelReportPage.ClickOnSpecificAmountLink("Request Assignments:");
                rOIAdminTransactionalModelReportPage.FilterReportBasedOnRequestDate("sr_dtAssignedRequests");
                bool isRequestIdExistRA = rOIAdminTransactionalModelReportPage.CheckRequestIdExsist(requestId);
                Assert.IsTrue(isRequestIdExistRA, $"Failed to validate request {requestId} under Requests Assignments");
                logger.Log(Status.Pass, $"Validated requestid ({requestId}) under Requests Assignments", TakeScreenShotAtStep());
                bool isValidatedRequestsAssignments = rOIAdminTransactionalModelReportPage.ValidateForExcelAndPDFContents("sr_dtAssignedRequests", requestId);
                Assert.IsTrue(isValidatedRequestsAssignments, "Failed to validate Excel and PDF data for requests assignments");
                logger.Log(Status.Pass, "Valiated UI and excel transactional model summary report data for requests assignments");
                rOIAdminTransactionalModelReportPage.ClickOnSpecificAmountLink("Request Assignments:");

                rOIAdminTransactionalModelReportPage.ClickOnSpecificAmountLink("Invoices Generated:");
                rOIAdminTransactionalModelReportPage.FilterReportBasedOnRequestDate("sr_dtInvoicedRequests");
                bool isRequestIdExistIG = rOIAdminTransactionalModelReportPage.CheckRequestIdExsist(requestId);
                Assert.IsTrue(isRequestIdExistIG, $"Failed to validate request {requestId} under invoice generated");
                logger.Log(Status.Pass, $"Validated requestid ({requestId}) under invoice degerated", TakeScreenShotAtStep());
                bool isValidatedInvoiceGenerated = rOIAdminTransactionalModelReportPage.ValidateForExcelAndPDFContents("sr_dtInvoicedRequests", requestId);
                Assert.IsTrue(isValidatedInvoiceGenerated, "Failed to validate Excel and PDF data for invoice generated");
                logger.Log(Status.Pass, "Valiated UI and excel transactional model summary report data for invoice generated");
                rOIAdminTransactionalModelReportPage.ClickOnSpecificAmountLink("Invoices Generated:");

                string billableShipments = rOIAdminTransactionalModelReportPage.GetSpecificAmountFromLink("Billable:");
                logger.Log(Status.Info, $"Billable shipments ({billableShipments})", TakeScreenShotAtStep());
                string paymentsReceivedByMRO = rOIAdminTransactionalModelReportPage.GetSpecificAmountFromLink("Payments Received by MRO:");
                logger.Log(Status.Info, $"Payments received by MRO ({paymentsReceivedByMRO})", TakeScreenShotAtStep());
                */
                //
                roiAdminHomePage.ROIlookupByRequestId(requestId);
                frame.SwitchToRoiFrame();
                string patientName = roiAdminHomePage.ReturnPatientName();
                roiFacilityRequestStatusPage.GotToCCShoppingCartPage();

                logger.Log(Status.Info, $"Verify request id on CC Shopping CartPage ({requestId}) is showing");
                ROIAdminCCShoppingCartPage roiAdminCCShoppingCartPage = new ROIAdminCCShoppingCartPage(driver, logger, TestContext);
                string requestIdOnCCSCP = roiAdminCCShoppingCartPage.GetRequestIdOnCCShoppingCartPage();
                Assert.AreEqual(requestId, requestIdOnCCSCP, "Failed to validate request id on CC Shopping CartPage");
                logger.Log(Status.Pass, $"Validated request id on CC Shopping CartPage ({requestId})", TakeScreenShotAtStep());

                logger.Log(Status.Info, $"Verify patient name on CC Shopping CartPage ({patientName}) is showing", TakeScreenShotAtStep());
                string patientNameOnCCSCP = roiAdminCCShoppingCartPage.GetPatientNameOnCCShoppingCartPage();
                Assert.AreEqual(patientName, patientNameOnCCSCP, "Failed to validate patient name on CC Shopping CartPage");
                logger.Log(Status.Pass, $"Validated patient name on CC Shopping CartPage ({patientName})");

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
                frame.SwitchToRoiFrame();
                string adjustedBalance = roiAdminRequestStatusPage.GetAdjustedBalanceAmountOnRSS();
                Assert.AreEqual("$0.00", adjustedBalance, "Failed to validate adjusted balance amount");
                logger.Log(Status.Pass, $"Verified adjustable balance amount as ({adjustedBalance})", TakeScreenShotAtStep());

                roiAdminRequestStatusPage.CheckForAnyIssuesAndCloseTheIssue();
                string shippableDate = roiAdminRequestStatusPage.AddEmailShipmentReturnShippableDate();
                string todayDate = DateTime.Now.ToString("M/d/yyyy");
                Assert.AreEqual(todayDate, shippableDate, "Failed to validate shipment date as todays date");
                logger.Log(Status.Pass, $"Verified shipment date as ({todayDate})", TakeScreenShotAtStep());

                roiAdminRequestStatusPage.GoToShipmentDetailsWindow();
                ROIAdminShipmentDetailsPage rOIAdminShipmentDetailsPage = new ROIAdminShipmentDetailsPage(driver, logger, TestContext);
                rOIAdminShipmentDetailsPage.ToClickMakeShippableButton();
                rOIAdminShipmentDetailsPage.GoToRequestStatusPage();

                string todayDateForEmailShipment = DateTime.Now.ToString("M/d/yyyy");
                string emailShippableDate = roiAdminRequestStatusPage.GetEmailShippedDate();
                Assert.AreEqual(todayDateForEmailShipment, emailShippableDate, "Failed to validate email shipped date as todays date");
                logger.Log(Status.Pass, $"Verified email shippable and shipped date as ({todayDateForEmailShipment})", TakeScreenShotAtStep());

                menuSelector.SelectRoiAdminMenuOptions("mnuROIAdmin", "Financial", "Transactional Model Report");
                rOIAdminTransactionalModelReportPage.CreateTransactionalModelReport("MRO Automated Regression Test", "[All]", true);
                logger.Log(Status.Info, "Transactional report created for Facility : MRO Automated Regression Test Contract : [All] IncludeTest : True");

                string billableShipmentsAfterShipments = rOIAdminTransactionalModelReportPage.GetSpecificAmountFromLink("Billable:");
                string paymentsReceivedByMROAfterPaymemnts = rOIAdminTransactionalModelReportPage.GetSpecificAmountFromLink("Payments Received by MRO:");
              /*
                Assert.IsTrue(Convert.ToInt32(billableShipmentsAfterShipments) > Convert.ToInt32(billableShipments), "Failed to validate billable shipments");
                logger.Log(Status.Pass, $"Verified billable shipments before ({billableShipments}) and after ({billableShipmentsAfterShipments}) count increased");
                Assert.IsTrue(Convert.ToDouble(paymentsReceivedByMROAfterPaymemnts) > Convert.ToDouble(paymentsReceivedByMRO), "Failed to validate payments received by mro");
                logger.Log(Status.Pass, $"Verified payments received by mro before ({paymentsReceivedByMRO}) and after ({paymentsReceivedByMROAfterPaymemnts}) count increased", TakeScreenShotAtStep());
                rOIAdminTransactionalModelReportPage.ClickOnSpecificAmountLink("Billable:");
              */
                bool isRequestIdExistSBD = rOIAdminTransactionalModelReportPage.CheckBillableShipmentRequestIdExist(requestId);
                Assert.IsTrue(isRequestIdExistSBD, $"Failed to validate request {requestId} under shipment billable details table");
                logger.Log(Status.Pass, $"Validated requestid ({requestId}) under shipment billable details table", TakeScreenShotAtStep());
                rOIAdminTransactionalModelReportPage.SwitchToRDFrame();
                rOIAdminTransactionalModelReportPage.ClickOnSpecificAmountLink("Billable:");

                rOIAdminTransactionalModelReportPage.ClickOnSpecificAmountLink("Payments Received by MRO:");
                rOIAdminTransactionalModelReportPage.FilterReportBasedOnRequestDate("sr_dtPaymentRcvd");
                bool isRequestIdExistPRMRO = rOIAdminTransactionalModelReportPage.CheckRequestIdExsist(requestId);
                Assert.IsTrue(isRequestIdExistPRMRO, $"Failed to validate request {requestId} under payments received by MRO table");
                logger.Log(Status.Pass, $"Validated requestid ({requestId}) under payments received by MRO table", TakeScreenShotAtStep());
                bool isValidatedPRMRO = rOIAdminTransactionalModelReportPage.ValidateForExcelAndPDFContents("sr_dtPaymentRcvd", requestId);
                Assert.IsTrue(isValidatedPRMRO, "Failed to validate Excel and PDF data for payments received by MRO");
                logger.Log(Status.Pass, "Valiated UI and excel transactional model summary report data for payments received by MRO");               
                rOIAdminTransactionalModelReportPage.ClickOnSpecificAmountLink("Payments Received by MRO:");

                driver.SwitchToDefaultContent();
                rOIAdminHomePage.SelectLogoutFromProfile();

                Cleanup(driver);
            }
            catch (Exception ex)
            {
                rOIAdminTransactionalModelReportPage.DeleteDownloadedExcelFiles();
                LogException(driver, logger, ex);
            }
        }
    }
}