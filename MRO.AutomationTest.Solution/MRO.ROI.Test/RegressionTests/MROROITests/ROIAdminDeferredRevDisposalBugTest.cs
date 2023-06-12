using AventStack.ExtentReports;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MRO.ROI.Automation.Pages;
using MRO.ROI.Test.Utilities;
using OpenQA.Selenium.Remote;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MRO.ROI.Test.RegressionTests.MROROITests
{
    [TestClass]
    public class ROIAdminDeferredRevDisposalBugTest : ROIBaseTest
    {

        public ROIAdminDeferredRevDisposalBugTest() : base(ROITestArea.ROIAdmin)
        {
        }

        [TestMethod]
        [TestCategory(ROITestCategory.Regression)]
        // Converted manual test case 11326-ROI-Admin-->Deferred Rev Disposal Bug
        public void Reg_11326_DeferredRevDisposalBug()
        {
            logger = extent.CreateTest("Reg_11326_Deferred Rev Disposal Bug");
            logger.Log(Status.Info, "Converted manual test case 11326-ROI-Admin-->Deferred Rev Disposal Bug");
            RemoteWebDriver _driver = Init(TestContext.Properties["browser"].ToString());
            ThreadLocal<RemoteWebDriver> localDriver = new ThreadLocal<RemoteWebDriver>();
            localDriver.Value = _driver;
            RemoteWebDriver driver = localDriver.Value;
            try
            {
                ROIAdminHomePage rOIAdminHomePage = new ROIAdminHomePage(driver, logger, TestContext);
                rOIAdminHomePage.FacilityList();
                ROIAdminFacilityListPage rOIAdminFacilityListPage = new ROIAdminFacilityListPage(driver, logger, TestContext);
                rOIAdminFacilityListPage.GotoROITestFacilityComputerIcon();
                ROIFacilityWorkSummaryPage rOIFacilityWorkSummaryPage = new ROIFacilityWorkSummaryPage(driver, logger, TestContext);
                rOIFacilityWorkSummaryPage.logaNewRequest();
                ROIFacilityLogNewRequestPage rOIFacilityLogNewRequestPage = new ROIFacilityLogNewRequestPage(driver, logger, TestContext);
                rOIFacilityLogNewRequestPage.ClickMRODeliveryTab();
                rOIFacilityLogNewRequestPage.CreateNewROITestFacilityDeliveryRequestWithoutScan();
                ROIFacilityRequestStatusPage rOIFacilityRequestStatusPage = new ROIFacilityRequestStatusPage(driver, logger, TestContext);
                string requestid = rOIFacilityRequestStatusPage.GetRequestID();
                logger.Log(Status.Info, $"Created a request with request id {requestid}", TakeScreenShotAtStep());
                rOIFacilityRequestStatusPage.ImportPdfFiles();               
                rOIFacilityRequestStatusPage.ReleaseRequestID();
                logger.Log(Status.Info, "Documents are now released for delivery to requester!");               
                rOIFacilityRequestStatusPage.FacilityLogout();
                rOIAdminHomePage.ROIlookupByRequestId(requestid);
                ROIAdminRequestStatusPage rOIAdminRequestStatusPage = new ROIAdminRequestStatusPage(driver, logger, TestContext);
                rOIAdminRequestStatusPage.assignRequester();
                ROIAdminAssignROIRequesterPage rOIAdminAssignROIRequesterPage = new ROIAdminAssignROIRequesterPage(driver, logger, TestContext);
                rOIAdminAssignROIRequesterPage.assignTestAttorney();
                rOIAdminRequestStatusPage.RateLink();
                ROIAdminUpdateRequestBillingDetailsPage rOIAdminUpdateRequestBillingDetailsPage = new ROIAdminUpdateRequestBillingDetailsPage(driver, logger, TestContext);
                rOIAdminUpdateRequestBillingDetailsPage.UpdateRegressionBaseRate();
                string adjustableAmount = rOIAdminRequestStatusPage.GetAdjustedBalanceAmountOnRSS();
                logger.Log(Status.Info, $"Verified adjustable amount as {adjustableAmount}", TakeScreenShotAtStep());
                rOIAdminRequestStatusPage.ClickLogCheck();
                ROIAdminLogChecksPage rOIAdminLogChecksPage = new ROIAdminLogChecksPage (driver, logger, TestContext);
                rOIAdminLogChecksPage.ClickLogCheck();
                rOIAdminHomePage.ROIlookupByRequestId(requestid);
                string l2AdjustableBalance = rOIAdminRequestStatusPage.GetL2AdjustableBal();
                logger.Log(Status.Info, $"Verified ledger-2 adjustable balance as {l2AdjustableBalance}", TakeScreenShotAtStep());
                logger.Log(Status.Info, "Steps (14-15) are related to shipment manager, needs to be executed manually");
                rOIFacilityRequestStatusPage.FacilityLogout();
                Cleanup(driver);
            }

            catch (Exception ex)
            {
                LogException(driver, logger, ex);
            }

        }
    }

}