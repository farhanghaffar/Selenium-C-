using AventStack.ExtentReports;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MRO.ROI.Automation.Common;
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
    public class RequesterCancellationPageErrorTest : ROIBaseTest
    {
        public RequesterCancellationPageErrorTest() : base(ROITestArea.ROIAdmin)
        {
        }

        [STATestMethodAttribute]
        [TestCategory(ROITestCategory.Regression)]
        public void Reg_2255_RequesterCancellationPageErrorTest()
        {
            logger = extent.CreateTest("Reg_2255_RequesterCancellationPageErrorTest");
            logger.Log(Status.Info, "Converted manual test case 2255 - ROI-Admin--> Admin Request status page, Requester Cancellation Page Errror test scenario through updateinfo to automated");
            RemoteWebDriver _driver = Init(TestContext.Properties["browser"].ToString());
            ThreadLocal<RemoteWebDriver> localDriver = new ThreadLocal<RemoteWebDriver>();
            localDriver.Value = _driver;
            RemoteWebDriver driver = localDriver.Value;
            string requestStatus = string.Empty;
            try
            {
                ROIAdminHomePage rOIAdminHomePage = new ROIAdminHomePage(driver, logger, TestContext);
                Iframe frame = new Iframe(driver, logger, TestContext);
                rOIAdminHomePage.SelectFacilityList();
                ROIAdminFacilityListPage rOIAdminFacilityListPage = new ROIAdminFacilityListPage(driver, logger, TestContext);
                rOIAdminFacilityListPage.GoToROITestFacility();
                ROIFacilityWorkSummaryPage rOIFacilityWorkSummaryPage = new ROIFacilityWorkSummaryPage(driver, logger, TestContext);
                rOIFacilityWorkSummaryPage.logaNewRequest();
                ROIFacilityLogNewRequestPage rOIFacilityLogNewRequestPage = new ROIFacilityLogNewRequestPage(driver, logger, TestContext);
                frame.SwitchToRoiFrame();
                rOIFacilityLogNewRequestPage.CreateNewROITestFacilityDeliveryRequestWithoutScan();
                ROIFacilityRequestStatusPage rOIFacilityRequestStatusPage = new ROIFacilityRequestStatusPage(driver, logger, TestContext);
                string requestid = rOIFacilityRequestStatusPage.GetRequestID();
                logger.Log(Status.Info, $"ROI request created with id ({requestid})", TakeScreenShotAtStep());
                rOIAdminHomePage.ROIlookupByRequestId(requestid);
                rOIFacilityRequestStatusPage.ImportPdfFiles();
                rOIFacilityRequestStatusPage.ReleaseRequestID();
                logger.Log(Status.Info, "Login to new tab");
                rOIAdminHomePage.SwitchToNewTabAndLoginROIAdmin(BaseAddress);
                rOIAdminHomePage.ROIlookupByRequestId(requestid);
                frame.SwitchToRoiFrame();
                ROIAdminRequestStatusPage adminRequestStatusPage = new ROIAdminRequestStatusPage(driver, logger, TestContext);
                ROIAdminAssignROIRequesterPage assignROIRequesterPage = new ROIAdminAssignROIRequesterPage(driver, logger, TestContext);

                logger.Log(Status.Info, "Assign requester");
                adminRequestStatusPage.assignRequester();
                logger.Log(Status.Info, "Assign Attorney");
                assignROIRequesterPage.assignTestAttorney();
                logger.Log(Status.Info, "Click Pass Docs QC");

                adminRequestStatusPage.ClickPassDocsQC();

                adminRequestStatusPage.RateLink();

                logger.Log(Status.Info, "Select Regression_BaseRate (1173) and apply rate (Save & Exit)", TakeScreenShotAtStep());
                ROIAdminUpdateRequestBillingDetailsPage rOIAdminUpdateRequestBillingDetailsPage = new ROIAdminUpdateRequestBillingDetailsPage(driver, logger, TestContext);
                rOIAdminUpdateRequestBillingDetailsPage.UpdateRegressionBaseRate();
                adminRequestStatusPage.ClickApplyRateButton();

                logger.Log(Status.Info, "Verify invoice created");
                adminRequestStatusPage.CreateInvoice();
                bool _isDisplayed = adminRequestStatusPage.VerifyInvoiceCreated();
                Assert.IsTrue(_isDisplayed, "Failed to create invoice");
                logger.Log(Status.Pass, $"Verified invoice created");

                logger.Log(Status.Info, "Verify request status = No Payment.");
                requestStatus = adminRequestStatusPage.GetRequestStatusFromRSS();
                Assert.AreEqual(requestStatus, "No Payment", "Failed to validate the status of the request");
                logger.Log(Status.Pass, $"Verified the the status as({requestStatus})");
                adminRequestStatusPage.ClickOnUpdateInfoButton();
                adminRequestStatusPage.SetDateUndeCancelledByRequesterField();

                logger.Log(Status.Info, "Verify requester cancelled date set to system date and also verify that adjusted balance is $20.00", TakeScreenShotAtStep());
                string adjustedBalance = adminRequestStatusPage.GetAdjustedBalanceAmountOnRSS();
                try
                {
                    Assert.AreEqual("$20.00", adjustedBalance, $"Failed to validate adjusted balance amount {adjustedBalance}");
                }
                catch (Exception ex)
                {
                    logger.Log(Status.Fail, $"Expected : '$20.00' Found : {adjustedBalance}" + ex.Message.ToString());
                }
                logger.Log(Status.Pass, $"Verified adjustable balance amount as ({adjustedBalance})");

                logger.Log(Status.Info, "Verify request status = Cancelled.", TakeScreenShotAtStep());
                requestStatus = adminRequestStatusPage.GetRequestStatusFromRSS();
                Assert.AreEqual(requestStatus, "Cancelled", "Failed to validate the status of the request");
                logger.Log(Status.Pass, $"Verified the the status as({requestStatus})");

                frame.switchToDefaut();
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