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
    public class ROIAdminRequestStatusPageRequesterCancellationPageErrorTestScenarioTest : ROIBaseTest
    {
        public ROIAdminRequestStatusPageRequesterCancellationPageErrorTestScenarioTest() : base(ROITestArea.ROITestFacility)
        {
        }

        [STATestMethodAttribute]
        [TestCategory(ROITestCategory.Regression)]
        // Converted manual test case 2254-ROI-Facility-->Admin Request status page, Requester Cancellation Page Errror test scenario to automated.
        public void Reg_2254_ROIAdminRequestStatusPageRequesterCancellationPageErrorTestScenarioTest()
        {
            logger = extent.CreateTest("Reg_2254_ROIAdminRequestStatusPageRequesterCancellationPageErrorTestScenarioTest");
            logger.Log(Status.Info, "Converted manual test case 2254-ROI-Facility-->Admin Request status page, Requester Cancellation Page Errror test scenario to automated.");
            RemoteWebDriver _driver = Init(TestContext.Properties["browser"].ToString());
            ThreadLocal<RemoteWebDriver> localDriver = new ThreadLocal<RemoteWebDriver>();
            localDriver.Value = _driver;
            RemoteWebDriver driver = localDriver.Value;
            string userRoot = System.Environment.GetEnvironmentVariable("USERPROFILE");
            string downloadFolder = Path.Combine(userRoot, "Downloads\\");

            try
            {
                ROIAdminHomePage rOIAdminHomePage = new ROIAdminHomePage(driver, logger, TestContext);
                ROIFacilityWorkSummaryPage rOIFacilityWorkSummaryPage = new ROIFacilityWorkSummaryPage(driver, logger, TestContext);
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
                rOIFacilityRequestStatusPage.ImportTwoPatientPages();
                rOIFacilityRequestStatusPage.ReleaseRequestID();
                logger.Log(Status.Info, $"Request released");

                rOIAdminHomePage.SwitchToNewTabAndLoginROIAdmin(BaseAddress);
                rOIAdminHomePage.SearchByRequestId(requestId);
                ROIAdminRequestStatusPage rOIAdminRequestStatusPage = new ROIAdminRequestStatusPage(driver, logger, TestContext);
                rOIAdminRequestStatusPage.assignRequester();
                ROIAdminAssignROIRequesterPage rOIAdminAssignROIRequesterPage = new ROIAdminAssignROIRequesterPage(driver, logger, TestContext);
                rOIAdminAssignROIRequesterPage.assignTestAttorney();
                rOIAdminRequestStatusPage.RateLink();
                ROIAdminUpdateRequestBillingDetailsPage rOIAdminUpdateRequestBillingDetailsPage = new ROIAdminUpdateRequestBillingDetailsPage(driver, logger, TestContext);
                rOIAdminUpdateRequestBillingDetailsPage.UpdateStateRate();
                
                string adjustedAmountOnRSSPage = rOIAdminRequestStatusPage.GetAdjustedBalanceAmountOnRSS();
                logger.Log(Status.Info, $"Adjusted balance amount on admin request status page is = ({adjustedAmountOnRSSPage})", TakeScreenShotAtStep());

                rOIAdminRequestStatusPage.DeliveryOverride("MAIL");
                logger.Log(Status.Info, $"Delivery override selected as MAIL", TakeScreenShotAtStep());
                rOIAdminRequestStatusPage.ApplyRate();

                string retrivalAmount = rOIAdminRequestStatusPage.GetRetrievalAmountOnRSS();
                string pageFeeAmount = rOIAdminRequestStatusPage.GetPageFeetOnRSS();
                string shippingAmount = rOIAdminRequestStatusPage.GetShippingOnRSS();
                string adjustedDue = rOIAdminRequestStatusPage.GetAdjustedBalanceAmountOnRSS();
                logger.Log(Status.Info, $"Verified that on RSS page retrieval fee is:{(retrivalAmount)} pageFee is :{(pageFeeAmount)} and shipping fee is:{(shippingAmount)} adjusted balance due is:{(adjustedDue)}", TakeScreenShotAtStep());

                rOIAdminRequestStatusPage.ClickOnQcPassButton();
                rOIAdminRequestStatusPage.CreateInvoice();
                bool _isDisplayed = rOIAdminRequestStatusPage.VerifyInvoiceCreated();
                Assert.IsTrue(_isDisplayed, "Failed to create invoice");

                string requestStatusVal=rOIAdminRequestStatusPage.GetRequestStatus();
                Assert.AreEqual(requestStatusVal, "No Payment", "Failed to verify request status");
                logger.Log(Status.Pass, $"Verified that request status set to :{(requestStatusVal)}", TakeScreenShotAtStep());

                rOIAdminRequestStatusPage.ClickOnCancel();
                string requestStatusVa11 = rOIAdminRequestStatusPage.GetRequestStatus();
                string afterAdjustedBalance = rOIAdminRequestStatusPage.GetAdjustedBalanceAmountOnRSS();
                Assert.AreEqual(afterAdjustedBalance,"22.48","Failed to verify adjusted balance");
                logger.Log(Status.Info, $"Verified that adjusted balance set to :{(afterAdjustedBalance)} ", TakeScreenShotAtStep());

                Assert.AreEqual(requestStatusVa11, "Cancelled","Failed to verify request status");
                logger.Log(Status.Pass, $"Verified that request status set to :{(requestStatusVa11)}", TakeScreenShotAtStep());

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

