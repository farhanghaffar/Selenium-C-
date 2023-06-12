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
    public class ROIImageDeliveryPayForRecords : ROIBaseTest
    {
        public ROIImageDeliveryPayForRecords() : base(ROITestArea.ROIAdmin)
        {

        }
        [STATestMethodAttribute]
        [TestCategory(ROITestCategory.Development)]
        // Converted manual test case 3087-ROI-Image Delivery--Pay For Records

        public void Reg_3087_ROIImageDeliveryPayForRecords()
        {
            logger = extent.CreateTest("Reg_3087_ROIImageDeliveryPayForRecords");
            logger.Log(Status.Info, "Converted manual test case Reg_3087_ROIImageDeliveryPayForRecords");
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
                rOIAdminRequestStatusPage.RateLink();
                ROIAdminUpdateRequestBillingDetailsPage rOIAdminUpdateRequestBillingDetailsPage = new ROIAdminUpdateRequestBillingDetailsPage(driver, logger, TestContext);
                rOIAdminUpdateRequestBillingDetailsPage.UpdateRegressionBaseRate();
                rOIAdminRequestStatusPage.CreateInvoice();
                rOIAdminHomePage.SwitchToNewTabAndLoginROIRequesterPortal(BaseAddress);
                ROITestRequesterPortalHomePage rOITestRequesterPortalHomePage = new ROITestRequesterPortalHomePage(driver, logger, TestContext);
                rOITestRequesterPortalHomePage.ClickOnNotificationPopUp();
                rOITestRequesterPortalHomePage.GotoPayForRecords();
                ROIPayForRecordsPage rOIPayForRecordsPage = new ROIPayForRecordsPage(driver, logger, TestContext);
                rOIPayForRecordsPage.ClickShowRequestButton();
                rOIPayForRecordsPage.VerifyRequestAndBalanceDue(requestid);
                rOIAdminHomePage.SwitchToNewTabAndLoginROIAdmin(BaseAddress);
                rOIAdminHomePage.ROIlookupByRequestId(requestid);
                rOIAdminRequestStatusPage.RateLink();
                rOIAdminUpdateRequestBillingDetailsPage.ChangeAdjustmentAmount("5.00");
                rOIAdminHomePage.SwitchToNewTabAndLoginROIRequesterPortal(BaseAddress);
                rOITestRequesterPortalHomePage.ClickOnNotificationPopUp();
                rOITestRequesterPortalHomePage.GotoPayForRecords();
                rOIPayForRecordsPage.ClickShowRequestButton();
                rOIPayForRecordsPage.VerifyRequestAndBalanceDue(requestid);
                rOIPayForRecordsPage.PayForSelectedRecord(requestid);

                //Check radio button is not working, there are some more steps required to complete this test



            }
            catch (Exception ex)
            {
                LogException(driver, logger, ex);

            }
        }
    }
}
