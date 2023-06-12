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

namespace MRO.ROI.Test.RegressionTests.MROROITests
{
    [TestClass]
    public class ROISummaryDashboardRequestAgingImagingDeliveryRequestsNonPatientRequestTest : ROIBaseTest
    {
        public ROISummaryDashboardRequestAgingImagingDeliveryRequestsNonPatientRequestTest() : base(ROITestArea.ROITestFacility)
        {

        }
        [STATestMethodAttribute]
        [TestCategory(ROITestCategory.Regression)]
        //Converted manual test case 14136-ROIFacility-->Summary Dashboard - Request aging imaging delivery requests (Non-patient request) to automated to automated.
        public void Reg_14136_ROISummaryDashboardRequestAgingImagingDeliveryRequestsNonPatientRequestTest()
        {
            logger = extent.CreateTest("Reg_14136_ROISummaryDashboardRequestAgingImagingDeliveryRequestsNonPatientRequestTest");
            logger.Log(Status.Info, "Converted manual test case 14136-ROIFacility-->Summary Dashboard - Request aging imaging delivery requests (Non-patient request) to automated");
            RemoteWebDriver _driver = Init(TestContext.Properties["browser"].ToString());
            ThreadLocal<RemoteWebDriver> localDriver = new ThreadLocal<RemoteWebDriver>();
            localDriver.Value = _driver;
            RemoteWebDriver driver = localDriver.Value;
            string sDateSelected = string.Empty;
            bool isTurnAroundTilesVisible = false;
            bool isAgingRequestTilesVisible = false;
            bool isAllRequesterTypesSelectedDefault = false;

            try
            {
                ROIFacilityWorkSummaryPage rOIFacilityWorkSummaryPage = new ROIFacilityWorkSummaryPage(driver, logger, TestContext);
                ROIAdminHomePage rOIAdminHomePage = new ROIAdminHomePage(driver, logger, TestContext);
                rOIFacilityWorkSummaryPage.GoToLogNewRequestPage();
                ROIFacilityLogNewRequestPage rOIFacilityLogNewRequestPage = new ROIFacilityLogNewRequestPage(driver, logger, TestContext);
                rOIFacilityLogNewRequestPage.ClickMRODeliveryTab();
                rOIFacilityLogNewRequestPage.MRODeliveryRequestForBostonProper();

                LogNewRequestPage logNewRequestPage = new LogNewRequestPage(driver, logger, TestContext);
                string requestId = logNewRequestPage.getRequestid();
                ROIFacilityRequestStatusPage rOIFacilityRequestStatusPage = new ROIFacilityRequestStatusPage(driver, logger, TestContext);
                logger.Log(Status.Info, $"MRO delivery request created with id:({requestId})", TakeScreenShotAtStep());
                rOIAdminHomePage.ROIlookupByRequestId(requestId);

                rOIFacilityRequestStatusPage.ImportRequestPages();
                rOIFacilityRequestStatusPage.clickOnRequestImagingDelivery();

                ROIRequestImagingDeliveryPage rOIRequestImagingDeliveryPage = new ROIRequestImagingDeliveryPage(driver, logger, TestContext);
                rOIRequestImagingDeliveryPage.AddNewRecord();
                bool isPresent = rOIRequestImagingDeliveryPage.VerifyRecord();
                Assert.IsTrue(isPresent, "Record not added");
                logger.Log(Status.Pass, "Verified record added successfully", TakeScreenShotAtStep());

                rOIRequestImagingDeliveryPage.ClickOnRequestImagingDelivery();
                logger.Log(Status.Info, "Successfully verified action is visible with name  Require pre-authorization CD", TakeScreenShotAtStep());
                bool isDisplayed = rOIFacilityRequestStatusPage.VerifyWaitingPrepaymentTxtVisible();
                Assert.IsTrue(isDisplayed, "Failed to verify waiting prepayment text", TakeScreenShotAtStep());
                rOIFacilityRequestStatusPage.SelectSummaryDashboard();

                ROIFacilitySummaryDashBoardPage rOIFacilitySummaryDashBoardPage = new ROIFacilitySummaryDashBoardPage(driver, logger, TestContext);
                sDateSelected = rOIFacilitySummaryDashBoardPage.CreateReportWithFliters();
                logger.Log(Status.Info, "Summary dashboard report created with filters as [Date:Last 3 Months, Reporting Group:None,Location:All,Exclude:Check All]", TakeScreenShotAtStep());
                rOIFacilitySummaryDashBoardPage.ClickOnAgingRequestsTab();
                isAgingRequestTilesVisible = rOIFacilitySummaryDashBoardPage.ValidateTilesForAgingRequestsTab(sDateSelected);
                isAllRequesterTypesSelectedDefault = rOIFacilitySummaryDashBoardPage.isRequesterMultiTypeDropdownAvailable();
                Assert.AreEqual(true, isAllRequesterTypesSelectedDefault, "Requester Type dropdown is not selected with all values by default");

                int childrenCount = rOIFacilitySummaryDashBoardPage.ValidatebargraphsForRequestAgingNonPatient();
                if (childrenCount >= 1)
                {


                    bool isRequestDisplayed = rOIFacilitySummaryDashBoardPage.FilterReportBasedOnRequestDate(requestId);
                    Assert.IsTrue(isRequestDisplayed, "Request id not present");

                }

                logger.Log(Status.Pass, "Verified patient does not exist on the request aging details page", TakeScreenShotAtStep());

                rOIAdminHomePage.SwitchToNewTabAndLoginROIAdmin(BaseAddress);
                rOIAdminHomePage.SearchByRequestId(requestId);
                ROIAdminRequestStatusPage rOIAdminRequestStatusPage = new ROIAdminRequestStatusPage(driver, logger, TestContext);
                rOIAdminRequestStatusPage.CheckForAnyIssuesAndCloseTheIssue();
                logger.Log(Status.Info, "Verified request return to RSS page and action was closed", TakeScreenShotAtStep());

                rOIAdminRequestStatusPage.assignRequester();
                ROIAdminAssignROIRequesterPage rOIAdminAssignROIRequesterPage = new ROIAdminAssignROIRequesterPage(driver, logger, TestContext);
                rOIAdminAssignROIRequesterPage.assignTestAttorney();
                string requesterValue = rOIAdminRequestStatusPage.VerifyReAssignRequester();
                Assert.AreEqual(requesterValue, "TEST Attorney's");
                logger.Log(Status.Info, $"Succcessfully verified at the request status page ship to ({requesterValue})", TakeScreenShotAtStep());

                rOIAdminRequestStatusPage.RateLink();
                ROIAdminUpdateRequestBillingDetailsPage rOIAdminUpdateRequestBillingDetailsPage = new ROIAdminUpdateRequestBillingDetailsPage(driver, logger, TestContext);
                rOIAdminUpdateRequestBillingDetailsPage.UpdateRegressionBaseRate();
                rOIAdminRequestStatusPage.ClickApplyRateButton();
                //rOIAdminRequestStatusPage.ApplyRate();
                rOIAdminRequestStatusPage.CreateInvoice();
                string invoiceId = rOIAdminRequestStatusPage.GetInvoiceId();
                logger.Log(Status.Info, $"Invoice created with id ({invoiceId})", TakeScreenShotAtStep());

                rOIAdminRequestStatusPage.ClickOnPayByCCButton();
                ROIAdminCCShoppingCartPage roiAdminCCShoppingCartPage = new ROIAdminCCShoppingCartPage(driver, logger, TestContext);
                string requestIdOnCCSCP = roiAdminCCShoppingCartPage.GetRequestIdOnCCShoppingCartPage();
                Assert.AreEqual(requestId, requestIdOnCCSCP, "Failed to validate request id on CC Shopping CartPage");
                logger.Log(Status.Pass, $"Validated request id on CC Shopping CartPage ({requestId})", TakeScreenShotAtStep());
                roiAdminCCShoppingCartPage.CheckOut();

                ROIAdminAddNewPaymentMethodPage roiAdminAddNewPaymentMethodPage = new ROIAdminAddNewPaymentMethodPage(driver, logger, TestContext);
                roiAdminAddNewPaymentMethodPage.AddNewPayment();
                logger.Log(Status.Info, $"Payment added");
                rOIFacilityRequestStatusPage.SwitchToDefaultWindow();
                rOIAdminHomePage.SearchByRequestId(requestId);

                rOIAdminRequestStatusPage.CheckForAnyIssuesAndCloseTheIssue();
                rOIAdminHomePage.SwitchBackToROITestFacilitySide(BaseAddress);               
                rOIFacilityWorkSummaryPage.SearchByRequestId(requestId);
                rOIFacilityRequestStatusPage.ClickOnAddImagingDelivery();
                logger.Log(Status.Info, "Successfully verified imaging delivery window opened", TakeScreenShotAtStep());
                rOIFacilityRequestStatusPage.AddShippingDetails();

                bool isImagingDeliveryDisplayed = rOIFacilityRequestStatusPage.VerifyShipmentType();
                Assert.IsTrue(isImagingDeliveryDisplayed, "Failed to verify imaging facility delivery option");
                string status = rOIFacilityRequestStatusPage.VerifyShipmentStatus();
                Assert.AreEqual(status, "Shipped");
                logger.Log(Status.Pass, $"Succesfully verified that Imaging facility delivery is visble and status  set to :{(status)} ", TakeScreenShotAtStep());


                rOIFacilityRequestStatusPage.SelectSummaryDashboard();
                sDateSelected = rOIFacilitySummaryDashBoardPage.CreateReportWithFliters();
                rOIFacilitySummaryDashBoardPage.isRequesterMultiTypeDropdownAvailable();
                int _childrenCount = rOIFacilitySummaryDashBoardPage.ValidatebargraphsForRequestAgingNonPatient();                
                rOIFacilitySummaryDashBoardPage.CloseTab();
                if (_childrenCount >= 1)
                {
                    bool _isRequestDisplayed = rOIFacilitySummaryDashBoardPage.FilterReportBasedOnRequestDate(requestId);
                    Assert.IsFalse(_isRequestDisplayed, "Request id  present");
                   
                }
                rOIFacilitySummaryDashBoardPage.SwitchToFirstTab();
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

