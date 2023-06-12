using AventStack.ExtentReports;
using AventStack.ExtentReports.MarkupUtils;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MRO.ROI.Automation.Common;
using MRO.ROI.Automation.Pages;
using MRO.ROI.Automation.Selenium;
using MRO.ROI.Test.ExecutionFactory;
using MRO.ROI.Test.Utilities;
using OpenQA.Selenium.Remote;
using System;
using System.Threading;

namespace MRO.ROI.Test.RegressionTests.MROROITests
{
    [TestClass]
    public class CDPasswordSpecificationTest : ROIBaseTest
    {
        public CDPasswordSpecificationTest() : base(ROITestArea.ROIAdmin)
        {
        }


        [STATestMethodAttribute]
        [TestCategory(ROITestCategory.Regression)]
        //Converted manual test case 11416-ROI-Admin-->CD Password Specification - Add capability to use random characters to automated.
        public void Reg_11416_CDPasswordSpecificationTest()
        {
            logger = extent.CreateTest("Reg_11416_CDPasswordSpecificationTest");
            logger.Log(Status.Info, "Converted manual test case 11416-ROI-Admin-->CD Password Specification - Add capability to use random characters to automated");
            RemoteWebDriver _driver = Init(TestContext.Properties["browser"].ToString());
            ThreadLocal<RemoteWebDriver> localDriver = new ThreadLocal<RemoteWebDriver>();
            localDriver.Value = _driver;
            RemoteWebDriver driver = localDriver.Value;
            try
            {
                ROIAdminHomePage adminHomePage = new ROIAdminHomePage(driver, logger, TestContext);
                adminHomePage.FacilityPolicies();
                string header = adminHomePage.VerifyFacilityPoliciesHeader();
                Assert.AreEqual(header, "Facility Policies");
                logger.Log(Status.Info, "Verified that facility policies page opened", TakeScreenShotAtStep());
                ROIAdminFacilityPoliciesPage adminFacilityPoliciesPage = new ROIAdminFacilityPoliciesPage(driver, logger, TestContext);
                adminFacilityPoliciesPage.FindPolicy();
                ROIAdminEditFacilityPolicy adminEditFacilityPolicy = new ROIAdminEditFacilityPolicy(driver, logger, TestContext);
                adminEditFacilityPolicy.ExpandMacors();

                adminFacilityPoliciesPage.FindPolicy();
                adminEditFacilityPolicy.ClickTestMacro();
                adminEditFacilityPolicy.VerifyResultantMacro();
                adminEditFacilityPolicy.FacilityList();
                ROIAdminFacilityListPage adminFacilityListPage = new ROIAdminFacilityListPage(driver, logger, TestContext);
                adminFacilityListPage.GoToDukeUniversity();

                ROIFacilityWorkSummaryPage rOIFacilityWorkSummaryPage = new ROIFacilityWorkSummaryPage(driver, logger, TestContext);
                rOIFacilityWorkSummaryPage.logaNewRequest();
                ROIFacilityLogNewRequestPage rOIFacilityLogNewRequestPage = new ROIFacilityLogNewRequestPage(driver, logger, TestContext);
                rOIFacilityLogNewRequestPage.ClickMRODeliveryTab();
                rOIFacilityLogNewRequestPage.MRODeliveryRequestForDukeStageTestingLocation();
                ROIFacilityRequestStatusPage rOIFacilityRequestStatusPage = new ROIFacilityRequestStatusPage(driver, logger, TestContext);
                string requestid = rOIFacilityRequestStatusPage.GetRequestID();
                logger.Log(Status.Info, $"MRO delivery request created with id ({requestid})", TakeScreenShotAtStep());

                adminHomePage.ROIlookupByRequestId(requestid);
                rOIFacilityRequestStatusPage.CDImportPdfFiles();
                rOIFacilityRequestStatusPage.ReleaseRequestID();
                logger.Log(Status.Info, "Request released");
                rOIFacilityRequestStatusPage.GetRequestId();

                ROIAdminRequestStatusPage adminRequestStatusPage = new ROIAdminRequestStatusPage(driver, logger, TestContext);
                adminRequestStatusPage.assignRequester();
                ROIAdminAssignROIRequesterPage assignROIRequesterPage = new ROIAdminAssignROIRequesterPage(driver, logger, TestContext);
                assignROIRequesterPage.assignTestAttorney();
                logger.Log(Status.Info, "Assigned Test Attorney");
                adminRequestStatusPage.DeliveryOverride("CD");
                logger.Log(Status.Info, $"Delivery override selected as CD", TakeScreenShotAtStep());
                adminRequestStatusPage.ClickOnQcPassButton();
                adminRequestStatusPage.ApplyRate();
                adminRequestStatusPage.CreateInvoice();
                string invoiceId = adminRequestStatusPage.GetInvoiceId();
                logger.Log(Status.Info, $" Verified that invoice created with id ({invoiceId})", TakeScreenShotAtStep());
                logger.Log(Status.Info, "Remaining steps (19-29) are related to shipment manager,needs to be executed manually");
                Cleanup(driver);

            }
            catch (Exception ex)
            {
                LogException(driver, logger, ex);
            }
        }
    }

}

