using AventStack.ExtentReports;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MRO.ROI.Automation.Pages;
using MRO.ROI.Automation.Pages.Common;
using MRO.ROI.Test.Utilities;
using OpenQA.Selenium.Remote;
using System;
using System.IO;
using System.Threading;
using MRO.ROI.Test.ExecutionFactory;

namespace MRO.ROI.Test.RegressionTests.MROROITests
{
    [TestClass]
    public class OriginalPDFDeliveryTest : ROIBaseTest
    {
        public OriginalPDFDeliveryTest() : base(ROITestArea.ROIAdmin)
        {

        }
        [STATestMethodAttribute]
        [TestCategory(ROITestCategory.Development)]
        //Converted manual test case 9961-ROI-Admin--> Original PDF Delivery test to automated
        public void Reg_9961_OriginalPDFDeliveryTest()
        {
            logger = extent.CreateTest("Reg_9961_OriginalPDFDeliveryTest");
            logger.Log(Status.Info, "Converted manual test case 9961-ROI-Admin--> Original PDF Delivery test to automated");
            RemoteWebDriver _driver = Init(TestContext.Properties["browser"].ToString());
            ThreadLocal<RemoteWebDriver> localDriver = new ThreadLocal<RemoteWebDriver>();
            localDriver.Value = _driver;
            RemoteWebDriver driver = localDriver.Value;

            try
            {
                ROIAdminHomePage rOIAdminHomePage = new ROIAdminHomePage(driver, logger, TestContext);
                rOIAdminHomePage.FacilityList();
                ROIAdminFacilityListPage rOIAdminFacilityListPage = new ROIAdminFacilityListPage(driver, logger, TestContext);
                rOIAdminFacilityListPage.ClickOnROITFGearIcon();
                ROIAdminFacilityFeaturesPage rOIAdminFacilityFeaturesPage = new ROIAdminFacilityFeaturesPage(driver, logger, TestContext);
                rOIAdminFacilityFeaturesPage.SelectShippingTab();
                rOIAdminFacilityFeaturesPage.CheckAllowOriginalPdfDelivery();
                rOIAdminFacilityFeaturesPage.ClickFacilityComputerIcon();
                ROIFacilityWorkSummaryPage rOIFacilityWorkSummaryPage = new ROIFacilityWorkSummaryPage(driver, logger, TestContext);
                rOIFacilityWorkSummaryPage.logaNewRequest();
                ROIFacilityLogNewRequestPage rOIFacilityLogNewRequestPage = new ROIFacilityLogNewRequestPage(driver, logger, TestContext);
                //rOIFacilityLogNewRequestPage.ClickMRODeliveryTab();
                rOIFacilityLogNewRequestPage.CreateNewROITestFacilityDeliveryRequestWithoutScan();
                ROIFacilityRequestStatusPage roiFacilityRequestStatusPage = new ROIFacilityRequestStatusPage(driver, logger, TestContext);
                string requestId = roiFacilityRequestStatusPage.GetRequestID();
                requestId = requestId.Trim();
                logger.Log(Status.Info, $"MRO delivery request created with id ({requestId})", TakeScreenShotAtStep());

                rOIAdminHomePage.ROIlookupByRequestId(requestId);

                roiFacilityRequestStatusPage.ImportRequestPagesForPDFDelivery();
                roiFacilityRequestStatusPage.ImportPatientPagesForPDFDelivery();
                roiFacilityRequestStatusPage.ReleaseRequestID();
                logger.Log(Status.Info, $"Request released");
                LoginPage loginPage = new LoginPage(driver, logger, TestContext);
                loginPage.LogOut();
                rOIAdminHomePage.ROIlookupByRequestId(requestId);
                ROIAdminAssignROIRequesterPage assignROIRequesterPage = new ROIAdminAssignROIRequesterPage(driver, logger, TestContext);
                assignROIRequesterPage.AssignRequest();
                logger.Log(Status.Info, $"Requester assigned");
                ROIAdminRequestStatusPage roiAdminRequestStatusPage = new ROIAdminRequestStatusPage(driver, logger, TestContext);
                roiAdminRequestStatusPage.DeliveryOverride("ExtUpload");
                logger.Log(Status.Info, $"Delivery override selected as ExtUpload");//step17

                roiAdminRequestStatusPage.RateLink();
                ROIAdminUpdateRequestBillingDetailsPage rOIAdminUpdateRequestBillingDetailsPage = new ROIAdminUpdateRequestBillingDetailsPage(driver, logger, TestContext);
                rOIAdminUpdateRequestBillingDetailsPage.UpdateRegCustomPostageRate();
                rOIAdminUpdateRequestBillingDetailsPage.ClickUpdateAnApply();
                rOIAdminUpdateRequestBillingDetailsPage.ClickSaveAndExit();
                roiAdminRequestStatusPage.ClickOnQcPassButton();
                roiAdminRequestStatusPage.CreateInvoice();
                bool isDisplayed = roiAdminRequestStatusPage.VerifyInvoiceCreated();
                Assert.IsTrue(isDisplayed, "Failed to create invoice");
                logger.Log(Status.Info, $"Succcessfully verified invoice created");

                roiAdminRequestStatusPage.ClickOnAddAndSelectExtUpload();
                roiAdminRequestStatusPage.ClickOnExtUploadHyperlink();
                ROIAdminShipmentDetailsPage rOIAdminShipmentDetailsPage = new ROIAdminShipmentDetailsPage(driver, logger, TestContext);
                rOIAdminShipmentDetailsPage.CheckSpecialDelivery();
                rOIAdminShipmentDetailsPage.SelectValueFromCarrierService("FedEx - 2Day");
                string resultMsg = rOIAdminShipmentDetailsPage.ClickSaveSpecialDeliveryInformation();
                Assert.AreEqual("Special Delivery instructions saved.", resultMsg, "Failed to save special delivery instructions");
                rOIAdminShipmentDetailsPage.ToClickMakeShippableButton();
                rOIAdminShipmentDetailsPage.GoToRequestStatusPage();

                //string todayDateForExtUploadShipment = DateTime.Now.ToString("M/d/yyyy");
                var todayDateForExtUploadShipment = String.Format("{0:M/dd/yyyy}", DateTime.Now).Replace("-", "/");
                string extUploadShippedDate = roiAdminRequestStatusPage.GetEmailShippedDate();
                //Assert.AreEqual(todayDateForExtUploadShipment, extUploadShippedDate, "Failed to validate extupload shipped date as todays date");
                logger.Log(Status.Pass, $"Verified extupload shipped date is updated as({todayDateForExtUploadShipment})", TakeScreenShotAtStep());

                roiAdminRequestStatusPage.DrillInToFacility();
                string ShippedStatus = roiFacilityRequestStatusPage.GetShipmentStatus();
                Assert.AreEqual("Awaiting Delivery", ShippedStatus, "Shipment status is not same as expected");
                roiFacilityRequestStatusPage.ClickPendingExternalSiteUploads();
                PendingExternalSiteDeliveryReportPage pendingExternalSiteDeliveryReport = new PendingExternalSiteDeliveryReportPage(driver, logger, TestContext);
                pendingExternalSiteDeliveryReport.SetDatesAndFlterData();
                pendingExternalSiteDeliveryReport.DeliverRequestByRequestID(requestId);
                pendingExternalSiteDeliveryReport.CheckMROShipmentOption();
                //step30
                pendingExternalSiteDeliveryReport.DownloadMroShipmentDocuments();
                pendingExternalSiteDeliveryReport.ClickCancel();
                loginPage.LogOut();
                rOIAdminHomePage.FacilityList();
                rOIAdminFacilityListPage.ClickOnROITFGearIcon();
                rOIAdminFacilityFeaturesPage.SelectShippingTab();
                rOIAdminFacilityFeaturesPage.CheckAllowOriginalPdfDelivery();
                rOIAdminFacilityFeaturesPage.ClickFacilityComputerIcon();
                //
                roiFacilityRequestStatusPage.ClickPendingExternalSiteUploads();
                pendingExternalSiteDeliveryReport.SetDatesAndFlterData();
                pendingExternalSiteDeliveryReport.DeliverRequestByRequestID(requestId);

                pendingExternalSiteDeliveryReport.DownloadOriginalPdfDocuments();
                pendingExternalSiteDeliveryReport.ClickConfirmDelivery();
                string shipmentStatus = roiFacilityRequestStatusPage.GetShipmentStatus();
                Assert.AreEqual("Shipped", shipmentStatus, "Shipment status in not updated to shipped");
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
