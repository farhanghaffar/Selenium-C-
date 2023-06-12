using AventStack.ExtentReports;
using AventStack.ExtentReports.MarkupUtils;
using DataDrivenProject;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MRO.ROI.Automation.Pages;
using MRO.ROI.Automation.Pages.Common;
using MRO.ROI.Automation.Selenium;
using MRO.ROI.Test.ExecutionFactory;
using MRO.ROI.Test.Utilities;
using OpenQA.Selenium.Remote;
using System;
using System.IO;
using System.Threading;
using static MRO.ROI.Automation.Utility.IniFile;

namespace MRO.ROI.Test.RegressionTests.MROROITests
{
    [TestClass]
    public class ROIDisablePayForCCButton : ROIBaseTest
    {
        public ROIDisablePayForCCButton() : base(ROITestArea.ROIAdmin)
        {

        }
        [STATestMethodAttribute]
        [TestCategory(ROITestCategory.Regression)]
        // Converted manual test case 6844-ROI-Disable Pay for CC Button When $0 or Negative Balance

        public void Reg_6844_ROIDisablePayForCCButton()
        {
            logger = extent.CreateTest("Reg_6844_ROIDisablePayForCCButtonWhenZeroOrNegativeBalanceTest");
            logger.Log(Status.Info, "Converted manual test case 6844-ROI-Disable Pay for CC Button When $0 or Negative Balance");
            RemoteWebDriver _driver = Init(TestContext.Properties["browser"].ToString());
            ThreadLocal<RemoteWebDriver> localDriver = new ThreadLocal<RemoteWebDriver>();
            localDriver.Value = _driver;
            RemoteWebDriver driver = localDriver.Value;

            try
            {
                string requestid = "";
                ROIAdminHomePage rOIAdminHomePage = new ROIAdminHomePage(driver, logger, TestContext);
                rOIAdminHomePage.SelectFacilityList();
                ROIAdminFacilityListPage rOIAdminFacilityListPage = new ROIAdminFacilityListPage(driver, logger, TestContext);
                rOIAdminFacilityListPage.GoToROITestFacility();
                ROIFacilityWorkSummaryPage rOIFacilityWorkSummaryPage = new ROIFacilityWorkSummaryPage(driver, logger, TestContext);
                rOIFacilityWorkSummaryPage.logaNewRequest();

                ROIFacilityLogNewRequestPage rOIFacilityLogNewRequestPage = new ROIFacilityLogNewRequestPage(driver, logger, TestContext);
                ROIFacilityRequestStatusPage rOIFacilityRequestStatusPage = new ROIFacilityRequestStatusPage(driver, logger, TestContext);
                rOIFacilityLogNewRequestPage.CreateNewROITestFacilityDeliveryRequestWithoutScan();
                requestid = rOIFacilityRequestStatusPage.GetRequestID();
                logger.Log(Status.Info, $"Request id generated- {requestid}", TakeScreenShotAtStep());
                rOIFacilityRequestStatusPage.ImportPdfFiles();

                bool statusUnderImportDocument = rOIFacilityRequestStatusPage.VerifyStatusUnderImportDocument();
                Assert.IsTrue(statusUnderImportDocument, "Failed to verify status under import document is uploaded");
                rOIFacilityRequestStatusPage.ReleaseRequestID();
                rOIAdminHomePage.SwitchToNewTabAndLoginROIAdmin(BaseAddress);
                rOIAdminHomePage.ROIlookupByRequestId(requestid);
                ROIAdminRequestStatusPage rOIAdminRequestStatusPage = new ROIAdminRequestStatusPage(driver, logger, TestContext);
                rOIAdminRequestStatusPage.assignRequester();

                ROIAdminAssignROIRequesterPage rOIAdminAssignROIRequesterPage = new ROIAdminAssignROIRequesterPage(driver, logger, TestContext);
                rOIAdminAssignROIRequesterPage.assignTestAttorney();
                rOIAdminRequestStatusPage.ClickOnQcPassButton();
                rOIAdminRequestStatusPage.ApplyRate();
                rOIAdminRequestStatusPage.CreateInvoice();

                ROIFacilityRequestStatusPage roiFacilityRequestStatusPage = new ROIFacilityRequestStatusPage(driver, logger, TestContext);
                roiFacilityRequestStatusPage.GotToCCShoppingCartPage();
                ROIAdminCCShoppingCartPage roiAdminCCShoppingCartPage = new ROIAdminCCShoppingCartPage(driver, logger, TestContext);
                string requestIdOnCCSCP = roiAdminCCShoppingCartPage.GetRequestIdOnCCShoppingCartPage();
                Assert.AreEqual(requestid, requestIdOnCCSCP, "Failed to validate request id on CC Shopping CartPage");
                logger.Log(Status.Pass, $"Validated request id on CC Shopping CartPage ({requestid})", TakeScreenShotAtStep());
                roiAdminCCShoppingCartPage.CheckOut();

                ROIAdminAddNewPaymentMethodPage roiAdminAddNewPaymentMethodPage = new ROIAdminAddNewPaymentMethodPage(driver, logger, TestContext);
                roiAdminAddNewPaymentMethodPage.AddNewPayment();
                logger.Log(Status.Info, $"Payment added");
                ROIAdminPaymentApprovedPage roiAdminPaymentApprovedPage = new ROIAdminPaymentApprovedPage(driver, logger, TestContext);
                string approvalCode = roiAdminPaymentApprovedPage.ReturnApprovalCode();
                roiAdminPaymentApprovedPage.ReturnToFindRequestPage();
                rOIAdminHomePage.ROIlookupByRequestId(requestid);

                rOIAdminRequestStatusPage.VerifyPayByCCDisabled();
                rOIAdminRequestStatusPage.VerifyAdjustedBalance("$0.00");
                rOIAdminHomePage.ClickOnRequesterServiceRequestStatus();
                logger.Log(Status.Info, "Verified find request page loaded", TakeScreenShotAtStep());
                rOIAdminHomePage.SearchRequest(requestid);

                ROIAdminRequestStatusProcessingPage rOIAdminRequestStatusProcessingPage = new ROIAdminRequestStatusProcessingPage(driver, logger, TestContext);
                string header = rOIAdminRequestStatusProcessingPage.VerifyHeader();
                Assert.AreEqual(header, "Request Status: Processing", "Failed to verify header");
                logger.Log(Status.Pass, "Verified Request Status Proccessing page opened and patient information visible", TakeScreenShotAtStep());
                rOIAdminRequestStatusProcessingPage.VerifyPayByCCDisabled();
                rOIAdminRequestStatusProcessingPage.SelectFacilityList();
                rOIAdminFacilityListPage.GoToROITestFacility();
                rOIFacilityWorkSummaryPage.logaNewRequest();

                rOIFacilityLogNewRequestPage.CreateNewROITestFacilityDeliveryRequestWithoutScanTwo();
                requestid = rOIFacilityRequestStatusPage.GetRequestID();
                logger.Log(Status.Info, $"Request id generated- {requestid}", TakeScreenShotAtStep());
                rOIFacilityRequestStatusPage.ImportPdfFiles();
                bool statusUnderImportDocument1 = rOIFacilityRequestStatusPage.VerifyStatusUnderImportDocument();
                Assert.IsTrue(statusUnderImportDocument, "Failed to verify status under import document is uploaded");
                rOIFacilityRequestStatusPage.ReleaseRequestID();

                rOIAdminHomePage.SwitchToNewTabAndLoginROIAdmin(BaseAddress);
                rOIAdminHomePage.ROIlookupByRequestId(requestid);
                rOIAdminRequestStatusPage.assignRequester();
                rOIAdminAssignROIRequesterPage.assignTestAttorney();
                rOIAdminRequestStatusPage.ClickOnQcPassButton();

                rOIAdminRequestStatusPage.SetNonBillable();
                rOIAdminRequestStatusPage.CreateInvoice();
                rOIAdminRequestStatusPage.VerifyPayByCCDisabled();
                rOIAdminRequestStatusPage.VerifyAdjustedBalance("$0.00");
                rOIAdminHomePage.ClickOnRequesterServiceRequestStatus();

                logger.Log(Status.Info, "Verified find request page loaded", TakeScreenShotAtStep());
                rOIAdminHomePage.SearchRequest(requestid);
                Assert.AreEqual(header, "Request Status: Processing", "Failed to verify header");
                logger.Log(Status.Pass, "Verified Request Status Proccessing page opened and patient information visible", TakeScreenShotAtStep());
                rOIAdminRequestStatusProcessingPage.VerifyPayByCCDisabled();
                Cleanup(driver);
            }
            catch (Exception ex)
            {
                LogException(driver, logger, ex);

            }
        }
    }
}
