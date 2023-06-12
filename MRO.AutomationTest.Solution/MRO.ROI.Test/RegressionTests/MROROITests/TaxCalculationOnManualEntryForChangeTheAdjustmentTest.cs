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
    public class TaxCalculationOnManualEntryForChangeTheAdjustmentTest : ROIBaseTest
    {
        public TaxCalculationOnManualEntryForChangeTheAdjustmentTest() : base(ROITestArea.ROIAdmin)
        {

        }
        [STATestMethodAttribute]
        [TestCategory(ROITestCategory.Regression)]
        // Converted manual test case 1863-ROI-Admin-->Tax calculation on manual entry for change the adjustment (1657 & 1863 almost same, one additonal step in 1863 to validate) to automated.
        public void Reg_1863_TaxCalculationOnManualEntryForChangeTheAdjustmentTest()
        {
            logger = extent.CreateTest("Reg_1863_TaxCalculationOnManualEntryForChangeTheAdjustmentTest");
            logger.Log(Status.Info, "Converted manual test case 1863-ROI-Admin-->Tax calculation on manual entry for change the adjustment (1657 & 1863 almost same, one additonal step in 1863 to validate) to automated.");
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
                rOIFacilityLogNewRequestPage.CreateNewMRODeliveryRequestForKOPLocation();
                LogNewRequestPage logNewRequestPage = new LogNewRequestPage(driver, logger, TestContext);
                string requestID =logNewRequestPage.getRequestid();
                ROIFacilityRequestStatusPage rOIFacilityRequestStatusPage = new ROIFacilityRequestStatusPage(driver, logger, TestContext);                
                logger.Log(Status.Pass, $"Request created with requestid ({requestID})", TakeScreenShotAtStep());
                rOIAdminHomePage.ROIlookupByRequestId(requestID);
                rOIFacilityRequestStatusPage.ImportPdfFiles();
                LoginPage loginPage = new LoginPage(driver, logger, TestContext);
                loginPage.LogOut();
                rOIAdminHomePage.ROIlookupByRequestId(requestID);
                ROIAdminRequestStatusPage adminRequestStatusPage = new ROIAdminRequestStatusPage(driver, logger, TestContext);
                ROIAdminAssignROIRequesterPage assignROIRequesterPage = new ROIAdminAssignROIRequesterPage(driver, logger, TestContext);
                adminRequestStatusPage.assignRequester();
                assignROIRequesterPage.AssignTestGATaxRequester();
                logger.Log(Status.Info, "Assigned Test GA Tax Requester");
                adminRequestStatusPage.ClickApplyRateButton();

                bool _verifyRetrievalPageFeeAndSales = adminRequestStatusPage.VerifyRetrievalPageAndSalesTaxValues();
                Assert.IsTrue(_verifyRetrievalPageFeeAndSales, "Failed to verify Retrieval Fee is $22.48, Page fee 1 is $6.20, Shipping is $1.40, and Sales Tax is $0.53, with a total invoice of $30.61");
                logger.Log(Status.Pass, "Successfully verified the Retrieval Fee is $22.48, Page fee 1 is $6.20, Shipping is $1.40, and Sales Tax is $0.53, with a total invoice of $30.61");

                adminRequestStatusPage.RateLink();
                ROIAdminUpdateRequestBillingDetailsPage rOIAdminUpdateRequestBillingDetailsPage = new ROIAdminUpdateRequestBillingDetailsPage(driver, logger, TestContext);
                rOIAdminUpdateRequestBillingDetailsPage.UpdatePageFee1();

                bool _verifyUpdatedPageFee1 = adminRequestStatusPage.VerifyUpdatedPageFee1();
                Assert.IsTrue(_verifyUpdatedPageFee1, "Failed to verify updated page fee 1 is $16.20");
                logger.Log(Status.Pass, "Successfully verified the updated page fee1 is $16.20");

                bool _verifyUpdatedSalesTaxAndInvoiceAmount = adminRequestStatusPage.VerifyUpdatedSalesTaxAndInvoiceAmountValues();
                Assert.IsTrue(_verifyUpdatedSalesTaxAndInvoiceAmount, "Failed to verify updated sales tax from 0.53 to 1.23 and total Invoice Amount increased to $41.31");
                logger.Log(Status.Pass, "Successfully verified the updated sales tax from 0.53 to 1.23 and total Invoice Amount increased to $41.31");

                adminRequestStatusPage.RateLink();
                rOIAdminUpdateRequestBillingDetailsPage.ChangeAdjustmentAmount();

                bool _verifyUpdatedAdjustmentSalesAndAdjustedBalance = adminRequestStatusPage.VerifyUpdatedAdjustmentSalesTaxAndAdjustedBalance();
                Assert.IsTrue(_verifyUpdatedAdjustmentSalesAndAdjustedBalance, "Failed to verify Ledger1 adjustments is set to 18.69 and the sales tax from 1.23 to 2.54 and adjusted balance will be set to $61.31");
                logger.Log(Status.Pass, "Successfully verified the Ledger1 adjustments is set to 18.69 and the sales tax from 1.23 to 2.54 and adjusted balance will be set to $61.31");

                Cleanup(driver);
            }
            catch (Exception ex)
            {
                LogException(driver, logger, ex);
            }
        }
    }

}
