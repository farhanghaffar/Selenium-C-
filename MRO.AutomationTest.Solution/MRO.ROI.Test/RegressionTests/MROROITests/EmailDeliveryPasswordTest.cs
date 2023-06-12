using AventStack.ExtentReports;
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
    public class EmailDeliveryPasswordTest:ROIBaseTest
    {
        public EmailDeliveryPasswordTest() : base(ROITestArea.ROIAdmin)
        {

        }
        [STATestMethodAttribute]
        [TestCategory(ROITestCategory.Regression)]
        //Converted manual test case 13105-ROI-Admin-->EMAIL Delivery Password - Add Custom Password and Random String - Facility Policies to automated
        public void Reg_13105_AddCustomPasswordAndRandomStringTest()
        {
            logger = extent.CreateTest("Reg_13105_AddCustomPasswordAndRandomStringTest");
            logger.Log(Status.Info, "Converted manual test case 13105-ROI-Admin-->EMAIL Delivery Password - Add Custom Password and Random String - Facility Policies to automated");
            RemoteWebDriver _driver = Init(TestContext.Properties["browser"].ToString());
            ThreadLocal<RemoteWebDriver> localDriver = new ThreadLocal<RemoteWebDriver>();
            localDriver.Value = _driver;
            RemoteWebDriver driver = localDriver.Value;
            string userRoot = System.Environment.GetEnvironmentVariable("USERPROFILE");
            string downloadFolder = Path.Combine(userRoot, "Downloads\\");
            string requestId = string.Empty;
          
            try
            {
                ROIAdminHomePage adminHomePage = new ROIAdminHomePage(driver, logger, TestContext);
                adminHomePage.FacilityPolicies();
                ROIAdminFacilityPoliciesPage adminFacilityPoliciesPage = new ROIAdminFacilityPoliciesPage(driver, logger, TestContext);
                adminFacilityPoliciesPage.FindPolicy();
                ROIAdminEditFacilityPolicy adminEditFacilityPolicy = new ROIAdminEditFacilityPolicy(driver, logger, TestContext);
                adminEditFacilityPolicy.ExpandMacors();
                adminFacilityPoliciesPage.FindPolicy();
                adminEditFacilityPolicy.CheckSendEMAILPasswordAsSeparateMailingOption();
                adminEditFacilityPolicy.ClickTestMacroForEmailDelivery();
                adminEditFacilityPolicy.VerifyResultantMacro();
                adminEditFacilityPolicy.FacilityList();
                ROIAdminFacilityListPage adminFacilityListPage = new ROIAdminFacilityListPage(driver, logger, TestContext);
                adminFacilityListPage.GoToDukeUniversity();

                ROIFacilityWorkSummaryPage rOIFacilityWorkSummaryPage = new ROIFacilityWorkSummaryPage(driver, logger, TestContext);
                rOIFacilityWorkSummaryPage.logaNewRequest();
                ROIFacilityLogNewRequestPage rOIFacilityLogNewRequestPage = new ROIFacilityLogNewRequestPage(driver, logger, TestContext);
                rOIFacilityLogNewRequestPage.ClickMRODeliveryTab();
                rOIFacilityLogNewRequestPage.CreateMRODeliveryRequestForDukeStageTestingLocation();

                LogNewRequestPage logNewRequestPage = new LogNewRequestPage(driver, logger, TestContext);
                requestId = logNewRequestPage.getRequestid();
                logger.Log(Status.Info, $"MRO delivery request created ({requestId})", TakeScreenShotAtStep());

                ROIFacilityRequestStatusPage rOIFacilityRequestStatusPage = new ROIFacilityRequestStatusPage(driver, logger, TestContext);
                adminHomePage.ROIlookupByRequestId(requestId);
                rOIFacilityRequestStatusPage.CDImportPdfFiles();
                rOIFacilityRequestStatusPage.ReleaseRequestID();
                logger.Log(Status.Info, "Request released");
                LoginPage loginPage = new LoginPage(driver, logger, TestContext);
                loginPage.LogOut();

                adminHomePage.ROIlookupByRequestId(requestId);
                ROIAdminRequestStatusPage adminRequestStatusPage = new ROIAdminRequestStatusPage(driver, logger, TestContext);
                adminRequestStatusPage.assignRequester();
                ROIAdminAssignROIRequesterPage assignROIRequesterPage = new ROIAdminAssignROIRequesterPage(driver, logger, TestContext);
                assignROIRequesterPage.assignTestAttorney();
                logger.Log(Status.Info, "Assigned Test Attorney");
                adminRequestStatusPage.ClickPassDocsQC();
                adminRequestStatusPage.DeliveryOverride("EMAIL");
                logger.Log(Status.Info, $"Delivery override selected as EMAIL", TakeScreenShotAtStep());
                adminRequestStatusPage.ApplyRate();
                adminRequestStatusPage.CreateInvoice();
                string invoiceId = adminRequestStatusPage.GetInvoiceId();
                logger.Log(Status.Info, $"Invoice created with ID({invoiceId})");
                adminRequestStatusPage.ClickOnAddAndSelectEmail();
                adminRequestStatusPage.ClickOnEmailHyperlink();
                ROIAdminShipmentDetailsPage rOIAdminShipmentDetailsPage = new ROIAdminShipmentDetailsPage(driver, logger, TestContext);
                rOIAdminShipmentDetailsPage.ToClickMakeShippableButton();

                var todaysDate = String.Format("{0:M/dd/yyyy}", DateTime.Now).Replace("-", "/");
                string _isDatesDisplayed = rOIAdminShipmentDetailsPage.VerifyAndGetShippedDate();
                Assert.AreEqual(todaysDate, _isDatesDisplayed, "Failed to verify the shipped date set to system date.");
                logger.Log(Status.Info, $"Succcessfully verified the shipped date set to system date {todaysDate}");
                rOIAdminShipmentDetailsPage.GoToRequestStatusPage();
                logger.Log(Status.Info, "Remaining steps(21-28) needs to be executed manually as they are not automatable");
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
