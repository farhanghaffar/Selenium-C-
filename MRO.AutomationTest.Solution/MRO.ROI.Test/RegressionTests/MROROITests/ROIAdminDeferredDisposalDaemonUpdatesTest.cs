using AventStack.ExtentReports;
using DataDrivenProject;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MRO.ROI.Automation.Pages;
using MRO.ROI.Automation.Pages.Common;
using MRO.ROI.Test.ExecutionFactory;
using MRO.ROI.Test.Utilities;
using OpenQA.Selenium.Remote;
using System;
using System.IO;
using System.Threading;


namespace MRO.ROI.Test.RegressionTests.MROROITests
{
    [TestClass]
    public class ROIAdminDeferredDisposalDaemonUpdatesTest : ROIBaseTest
    {
        public ROIAdminDeferredDisposalDaemonUpdatesTest() : base(ROITestArea.ROIFacility)
        {
        }
        [STATestMethodAttribute]
        [TestCategory(ROITestCategory.Regression)]
        //Converted manual test case 11922-ROI-Admin-->Deferred Disposal Daemon Updates to automated
        public void Reg_11922_ROIAdminDeferredDisposalDaemonUpdatesTest()
        {
            logger = extent.CreateTest("Reg_11922_ROIAdminDeferredDisposalDaemonUpdatesTest");
            logger.Log(Status.Info, "Converted manual test case 11922-ROI-Admin-->Deferred Disposal Daemon Updates to automated");
            RemoteWebDriver _driver = Init(TestContext.Properties["browser"].ToString());
            ThreadLocal<RemoteWebDriver> localDriver = new ThreadLocal<RemoteWebDriver>();
            localDriver.Value = _driver;
            RemoteWebDriver driver = localDriver.Value;           
            try
            {
                ROIFacilityWorkSummaryPage rOIFacilityWorkSummaryPage = new ROIFacilityWorkSummaryPage(driver, logger, TestContext);
                rOIFacilityWorkSummaryPage.logaNewRequest();
                ROIFacilityLogNewRequestPage rOIFacilityLogNewRequestPage = new ROIFacilityLogNewRequestPage(driver, logger, TestContext);
                rOIFacilityLogNewRequestPage.CreateNewMRODeliveryRequestWithoutScan();
                ROIFacilityRequestStatusPage rOIFacilityRequestStatusPage = new ROIFacilityRequestStatusPage(driver, logger, TestContext);
                string requestid = rOIFacilityRequestStatusPage.GetRequestID();
                logger.Log(Status.Info, $"ROI request created with id ({requestid})", TakeScreenShotAtStep());
                ROIAdminHomePage rOIAdminHomePage = new ROIAdminHomePage(driver, logger, TestContext);
                rOIAdminHomePage.ROIlookupByRequestId(requestid);
                rOIFacilityRequestStatusPage.ImportPdfFiles();
                bool statusUnderImportDocument = rOIFacilityRequestStatusPage.VerifyStatusUnderImportDocument();
                Assert.IsTrue(statusUnderImportDocument, "Failed to verify status under import document is uploaded"); 
                rOIAdminHomePage.SwitchToNewTabAndLoginROIAdmin(BaseAddress);
                rOIAdminHomePage.ROIlookupByRequestId(requestid);
                ROIAdminRequestStatusPage adminRequestStatusPage = new ROIAdminRequestStatusPage(driver, logger, TestContext);
                ROIAdminAssignROIRequesterPage assignROIRequesterPage = new ROIAdminAssignROIRequesterPage(driver, logger, TestContext);
                adminRequestStatusPage.assignRequester();
                assignROIRequesterPage.assignTestAttorney();
                logger.Log(Status.Info, "Assigned test attorney");                
                adminRequestStatusPage.ClickApplyRateButton();
                adminRequestStatusPage.CreateInvoice();
                logger.Log(Status.Info, "Invoice created");
                string _adjustedAmountOnRSSPage = adminRequestStatusPage.GetAdjustedBalanceAmountOnRSS();
                logger.Log(Status.Pass, $"The adjusted amount on request status page is ({_adjustedAmountOnRSSPage})", TakeScreenShotAtStep());
                adminRequestStatusPage.ClickLogCheck();
                ROIAdminLogChecksPage rOIAdminLogChecksPage = new ROIAdminLogChecksPage(driver, logger, TestContext);
                rOIAdminLogChecksPage.PayAmountLessThanBalAmount();
                logger.Log(Status.Info, "Payment paid through log check");
                bool requestOutstandingBalError = rOIAdminLogChecksPage.VerifyRequestHasOutstandingDue();
                Assert.IsTrue(requestOutstandingBalError, "Failed to validate request has outstanding balance due!");
                logger.Log(Status.Info, $"Successfully Verified request has outstanding balance due!", TakeScreenShotAtStep());
                rOIAdminHomePage.ROIlookupByRequestId(requestid);
                string adjustedAmountOnRSSPage = adminRequestStatusPage.GetAdjustedBalanceAmountOnRSS();               
                logger.Log(Status.Pass, $"The balance amount on request status page is ({adjustedAmountOnRSSPage})", TakeScreenShotAtStep());
                logger.Log(Status.Info, "Remaining step (9-10) requires Run Daemon 49 Deferred Disposal Daemon as it normally runs every 12 hours this needs to be run manually");
                Cleanup(driver);
            }
            catch (Exception ex)
            {
                LogException(driver, logger, ex);
            }
        }
    }
}