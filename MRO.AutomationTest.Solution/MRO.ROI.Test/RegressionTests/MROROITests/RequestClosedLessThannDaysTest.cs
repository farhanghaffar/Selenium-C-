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
    public class RequestClosedLessThannDaysTest : ROIBaseTest
    {
        public RequestClosedLessThannDaysTest() : base(ROITestArea.ROIAdmin)
        {
        }
        [STATestMethodAttribute]
        [TestCategory(ROITestCategory.Regression)]
        // Converted manual test case 11298-ROI-Admin--> Requests closed < ndays to automated
        public void Reg_11298_RequestClosedLessThanNDays()
        {
            logger = extent.CreateTest("Reg_11298_RequestClosedLessThanNDays");
            logger.Log(Status.Info, "Converted manual test case 11298-ROI-Admin--> Requests closed < ndays to automated");
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
                ROIFacilityRequestStatusPage rOIFacilityRequestStatusPage = new ROIFacilityRequestStatusPage(driver, logger, TestContext);
                rOIFacilityLogNewRequestPage.CreateNewROITestFacilityDeliveryRequestWithoutScan();
                string requestid = rOIFacilityRequestStatusPage.GetRequestID();
                logger.Log(Status.Info, $"Request id generated- {requestid}" , TakeScreenShotAtStep());
                rOIAdminHomePage.ROIlookupByRequestId(requestid);               
                rOIFacilityRequestStatusPage.ImportPdfFiles();
                rOIAdminHomePage.ROIlookupByRequestId(requestid);
                bool statusUnderImportDocument = rOIFacilityRequestStatusPage.VerifyStatusUnderImportDocument();
                Assert.IsTrue(statusUnderImportDocument, "Failed to verify status under import document is uploaded");                
                rOIFacilityRequestStatusPage.ReleaseRequestID();                
                LoginPage loginPage = new LoginPage(driver, logger, TestContext);
                loginPage.LogOut();
                rOIAdminHomePage.ROIlookupByRequestId(requestid);                
                ROIAdminRequestStatusPage adminRequestStatusPage = new ROIAdminRequestStatusPage(driver, logger, TestContext);
                ROIAdminAssignROIRequesterPage assignROIRequesterPage = new ROIAdminAssignROIRequesterPage(driver, logger, TestContext);
                adminRequestStatusPage.assignRequester();
                assignROIRequesterPage.assignTestAttorney();
                adminRequestStatusPage.ClickPassDocsQC();                
                adminRequestStatusPage.RateLink();
                ROIAdminUpdateRequestBillingDetailsPage rOIAdminUpdateRequestBillingDetailsPage = new ROIAdminUpdateRequestBillingDetailsPage(driver, logger, TestContext);
                rOIAdminUpdateRequestBillingDetailsPage.UpdateRegressionBaseRate();
                string adjustedAmountOnRSSPage = adminRequestStatusPage.GetAdjustedBalanceAmountOnRSS();
                logger.Log(Status.Info, $"Adjusted balance amount on admin request status page is = ({adjustedAmountOnRSSPage})" , TakeScreenShotAtStep());                
                adminRequestStatusPage.CreateInvoice();
                bool _isDisplayed = adminRequestStatusPage.VerifyInvoiceCreated();
                Assert.IsTrue(_isDisplayed, "Failed to create invoice");
                logger.Log(Status.Info, "Successfully Verified invoice is created and ready for delivery", TakeScreenShotAtStep());
                logger.Log(Status.Info, "Remaining step (12) requires staging server access(daemon process 38) and needs to be executed manually");
                Cleanup(driver);

            }
            catch (Exception ex)
            {
                LogException(driver, logger, ex);

            }
        }

    }
}
