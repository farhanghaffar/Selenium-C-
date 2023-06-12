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
    public class MultipleContractTermUserStoriesTest : ROIBaseTest
    {
        public MultipleContractTermUserStoriesTest() : base(ROITestArea.ROIAdmin)
        {
        }

        [STATestMethodAttribute]
        [TestCategory(ROITestCategory.Regression)]
        //Converted manual test case 9187-ROI-Admin-->multiple contract term user stories (6034-6036) to automated
        public void Reg_9187_MultipleContractTermUserStoriesTest()
        {
            logger = extent.CreateTest("Reg_9187_MultipleContractTermUserStoriesTest");
            logger.Log(Status.Info, "Converted manual test case 9187-ROI-Admin-->multiple contract term user stories (6034-6036) to automated");
            RemoteWebDriver _driver = Init(TestContext.Properties["browser"].ToString());
            ThreadLocal<RemoteWebDriver> localDriver = new ThreadLocal<RemoteWebDriver>();
            localDriver.Value = _driver;
            RemoteWebDriver driver = localDriver.Value;
            try
            {
                ROIAdminHomePage adminHomePage = new ROIAdminHomePage(driver, logger, TestContext);
                adminHomePage.ContractList();
                ROIAdminContractListPage rOIAdminContractListPage = new ROIAdminContractListPage(driver, logger, TestContext);
                rOIAdminContractListPage.SelectROIFacilityFromDropdown();
                ROIAdminAddContractPage rOIAdminAddContractPage = new ROIAdminAddContractPage(driver, logger, TestContext);
                string pageName = rOIAdminAddContractPage.VerifyEditContractPageHeader();
                Assert.AreEqual(pageName, "Edit Contract");
                logger.Log(Status.Info, "Verified edit contract page opened successfully");

                rOIAdminAddContractPage.EditROIFacilityContract();
                ROIAdminHomePage rOIAdminHomePage = new ROIAdminHomePage(driver, logger, TestContext);
                rOIAdminHomePage.SelectFacilityList();
                ROIAdminFacilityListPage rOIAdminFacilityListPage = new ROIAdminFacilityListPage(driver, logger, TestContext);
                rOIAdminFacilityListPage.GoToROITestFacility();
                ROIFacilityWorkSummaryPage rOIFacilityWorkSummaryPage = new ROIFacilityWorkSummaryPage(driver, logger, TestContext);
                rOIFacilityWorkSummaryPage.logaNewRequest();
                ROIFacilityLogNewRequestPage rOIFacilityLogNewRequestPage = new ROIFacilityLogNewRequestPage(driver, logger, TestContext);
                rOIFacilityLogNewRequestPage.CreateInternalPortalROITestFacilityRequest();
                ROIFacilityRequestStatusPage rOIFacilityRequestStatusPage = new ROIFacilityRequestStatusPage(driver, logger, TestContext);
                string requestid = rOIFacilityRequestStatusPage.GetRequestID();
                logger.Log(Status.Info, $"Request id generated- {requestid}",TakeScreenShotAtStep());
                rOIAdminHomePage.ROIlookupByRequestId(requestid);

                string deliveryMethodValue = rOIFacilityRequestStatusPage.GetDeliveryMethod();
                Assert.AreEqual(deliveryMethodValue, "Billing Office");
                logger.Log(Status.Info, $"Succcessfully verified at the request status page Delivery method ({deliveryMethodValue})");

                rOIFacilityRequestStatusPage.ImportPdfFiles();
                rOIFacilityRequestStatusPage.ReleaseRequest();
                LoginPage loginPage = new LoginPage(driver, logger, TestContext);
                loginPage.LogOut();
                rOIAdminHomePage.ROIlookupByRequestId(requestid);

                ROIAdminRequestStatusPage rOIAdminRequestStatusPage = new ROIAdminRequestStatusPage(driver, logger, TestContext);
                string reAssignRequesterValue = rOIAdminRequestStatusPage.VerifyReAssignRequester();
                Assert.AreEqual(reAssignRequesterValue, "ROI-Test Facility CBO");
                logger.Log(Status.Info, $"Succcessfully verified at the request status page re-assign requester ({reAssignRequesterValue})");

                string shipToValue = rOIAdminRequestStatusPage.VerifyShipTo();
                Assert.AreEqual(shipToValue, "Central Billing Office");
                logger.Log(Status.Info, $"Succcessfully verified at the request status page ship to ({shipToValue})");

                rOIAdminRequestStatusPage.ClickOnQcPassButton();
                rOIAdminRequestStatusPage.DeliveryOverride("EMAIL");
                rOIAdminRequestStatusPage.RateLink();
                ROIAdminUpdateRequestBillingDetailsPage rOIAdminUpdateRequestBillingDetailsPage = new ROIAdminUpdateRequestBillingDetailsPage(driver, logger, TestContext);
                rOIAdminUpdateRequestBillingDetailsPage.UpdateRegressionBaseRate();

                string regressionNaseRateTxt = rOIAdminRequestStatusPage.VerifyRateRegressionBaseRate();
                Assert.AreEqual(regressionNaseRateTxt, "Regression_BaseRate");
                logger.Log(Status.Info, $"Succcessfully verified at the request status page rate applied ({regressionNaseRateTxt})");

                rOIAdminRequestStatusPage.CreateInvoice();
                bool _isDisplayed = rOIAdminRequestStatusPage.VerifyInvoiceCreated();
                Assert.IsTrue(_isDisplayed, "Failed to create invoice");
                logger.Log(Status.Info, $"Succcessfully verified invoice created");

                rOIAdminRequestStatusPage.ClickOnAddAndSelectEmail();
                rOIAdminRequestStatusPage.ClickOnEmailHyperlink();
                ROIAdminShipmentDetailsPage rOIAdminShipmentDetailsPage = new ROIAdminShipmentDetailsPage(driver, logger, TestContext);
                rOIAdminShipmentDetailsPage.ToClickMakeShippableButton();

                var todaysDate = String.Format("{0:M/dd/yyyy}", DateTime.Now).Replace("-", "/");
                string _isDatesDisplayed = rOIAdminShipmentDetailsPage.VerifyAndGetShippedDate();
                Assert.AreEqual(todaysDate, _isDatesDisplayed, "Failed to verify the shipped date set to system date.");
                logger.Log(Status.Info, $"Succcessfully verified the shipped date set to system date {todaysDate}");

                rOIAdminHomePage.ClickOnMonthlyStatements();
                ROIAdminMonthlyStatementReportPage rOIAdminMonthlyStatementReportPage = new ROIAdminMonthlyStatementReportPage(driver, logger, TestContext);
                rOIAdminMonthlyStatementReportPage.CreateCurrentMonthStatementForROITestFacility();
                rOIAdminMonthlyStatementReportPage.VerifyMonthlyStatementAmountForBOETransactionFee();
                rOIAdminMonthlyStatementReportPage.ClickOnRequestID(requestid);              

                bool _requestStatusPage = rOIAdminRequestStatusPage.VerifyRequestStatusPage();
                Assert.IsTrue(_requestStatusPage, "Failed to verify request status page");
                logger.Log(Status.Info, $"Succcessfully verified user landed on request status page", TakeScreenShotAtStep());

                Cleanup(driver);

            }
            catch (Exception ex)
            {
                LogException(driver, logger, ex);
            }
        }
    }
}
        
    
