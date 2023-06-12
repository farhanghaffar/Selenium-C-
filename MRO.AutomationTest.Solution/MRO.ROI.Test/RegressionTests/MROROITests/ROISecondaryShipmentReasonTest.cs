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
    public class ROISecondaryShipmentReasonTest : ROIBaseTest
    {
        public ROISecondaryShipmentReasonTest() : base(ROITestArea.ROIFacility)
        {

        }

        [STATestMethodAttribute]
        [TestCategory(ROITestCategory.Regression)]
        // Converted manual test case 7133-ROI-Facility-->Secondary Shipment Reason - "Electronic Shipment Failure" to automated.
        public void Reg_7133_SecondaryShipmentReasonTest()
        {
            logger = extent.CreateTest("Reg_7133_SecondaryShipmentReason-ElectronicShipmentFailure");
            logger.Log(Status.Info, "Converted manual test case 7133-ROI-Facility-->Secondary Shipment Reason - Electronic Shipment Failure to automated");
            RemoteWebDriver _driver = Init(TestContext.Properties["browser"].ToString());
            ThreadLocal<RemoteWebDriver> localDriver = new ThreadLocal<RemoteWebDriver>();
            localDriver.Value = _driver;
            RemoteWebDriver driver = localDriver.Value;

            try
            {
                ROIFacilityWorkSummaryPage rOIFacilityWorkSummaryPage = new ROIFacilityWorkSummaryPage(driver, logger, TestContext);
                rOIFacilityWorkSummaryPage.GoToLogNewRequestPage();
                ROIFacilityLogNewRequestPage rOIFacilityLogNewRequestPage = new ROIFacilityLogNewRequestPage(driver, logger, TestContext);
                rOIFacilityLogNewRequestPage.CreateNewMRODeliveryRequestWithoutScan();
                ROIFacilityRequestStatusPage rOIFacilityRequestStatusPage = new ROIFacilityRequestStatusPage(driver, logger, TestContext);
                string requestid = rOIFacilityRequestStatusPage.GetRequestID();
                logger.Log(Status.Info, $"MRO delivery request created with id ({requestid})");

                rOIFacilityRequestStatusPage.ImportPdfFiles();
                rOIFacilityRequestStatusPage.ReleaseRequestID();
                rOIFacilityRequestStatusPage.GetRequestIdFromFacilitySide(BaseAddress);
                ROIAdminRequestStatusPage adminRequestStatusPage = new ROIAdminRequestStatusPage(driver, logger, TestContext);
                adminRequestStatusPage.assignRequester();
                ROIAdminAssignROIRequesterPage assignROIRequesterPage = new ROIAdminAssignROIRequesterPage(driver, logger, TestContext);
                assignROIRequesterPage.assignTestAttorney();

                adminRequestStatusPage.ClickOnQcPassButton();
                adminRequestStatusPage.ApplyRate();
                adminRequestStatusPage.CreateInvoice();
                string invoiceId = adminRequestStatusPage.GetInvoiceId();
                logger.Log(Status.Info, $"Invoice created with id ({invoiceId})", TakeScreenShotAtStep());

                adminRequestStatusPage.AddEmailShippment();
                ROIAdminPackingListstPage rOIAdminPackingListstPage = new ROIAdminPackingListstPage(driver, logger, TestContext);
                rOIAdminPackingListstPage.VerifyPackageListPageHeader();
                rOIAdminPackingListstPage.CreatePackingList();
                rOIAdminPackingListstPage.VerifyShipmentId();
                bool isResendButDisplayed = rOIAdminPackingListstPage.VerifyResendButton();
                Assert.IsTrue(isResendButDisplayed, "Failed to verify resend button");
                logger.Log(Status.Pass, "Resend Button Visible now", TakeScreenShotAtStep());
                rOIAdminPackingListstPage.ReturnToRss();

                adminRequestStatusPage.clickOnSecondEmailShipment();
                ROIAdminShipmentDetailsPage rOIAdminShipmentDetailsPage = new ROIAdminShipmentDetailsPage(driver, logger, TestContext);
                string headingElement = rOIAdminShipmentDetailsPage.VerifyShipmentDetailsPageHeader();
                Assert.AreEqual(headingElement, "Shipment Details");
                logger.Log(Status.Info, "Shipment Details window opened", TakeScreenShotAtStep());

                string typeVal = rOIAdminShipmentDetailsPage.SecondaryShipmentTypeValue();
                Assert.AreEqual(typeVal, "Electronic Shipment Failure");
                logger.Log(Status.Pass, "Secondary shipment section type selected as Electronic Shipment Failure", TakeScreenShotAtStep());
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

