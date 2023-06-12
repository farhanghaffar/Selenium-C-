using AventStack.ExtentReports;
using AventStack.ExtentReports.MarkupUtils;
using Microsoft.VisualStudio.TestTools.UnitTesting;
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
    public class ROINRSTodeliverToFTPTest : ROIBaseTest
    {

        public ROINRSTodeliverToFTPTest() : base(ROITestArea.ROIFacility)
        {

        }
        [STATestMethodAttribute]
        [TestCategory(ROITestCategory.Passed)]
        public void Reg_11470_DirectNoRecordsStatementsToDeliverSeparateFTPDirectory()
        {
            logger = extent.CreateTest("Reg_11470_Direct No Records Statements To Deliver to a Separate FTP Directory");
            logger.Log(Status.Info, "Converted manual test case 11470-ROI-Facility-->Direct No Records Statements To Deliver to a Separate FTP Directory to automated");            
            RemoteWebDriver _driver = Init(TestContext.Properties["browser"].ToString());
            ThreadLocal<RemoteWebDriver> localDriver = new ThreadLocal<RemoteWebDriver>();
            localDriver.Value = _driver;
            RemoteWebDriver driver = localDriver.Value;
            string requestID = string.Empty;
            try
            {

                ROIFacilityWorkSummaryPage rOIFacilityWorkSummaryPage = new ROIFacilityWorkSummaryPage(driver, logger, TestContext);                
                rOIFacilityWorkSummaryPage.logaNewRequest();
                ROIFacilityLogNewRequestPage rOIFacilityLogNewRequest = new ROIFacilityLogNewRequestPage(driver, logger, TestContext);               
                rOIFacilityLogNewRequest.ClickMRODeliveryTab();
                rOIFacilityLogNewRequest.CreateNewMRODeliveryRequestWithoutScan();
                ROIFacilityRequestStatusPage rOIFacilityRequestStatusPage = new ROIFacilityRequestStatusPage(driver, logger, TestContext);
                requestID = rOIFacilityRequestStatusPage.GetRequestID();
                logger.Log(Status.Pass, $" MRO Delivery Request created with requestid({requestID})");
                
                rOIFacilityRequestStatusPage.ImportPdfFilesForAddNoRecordsComponent();
                rOIFacilityRequestStatusPage.ReleaseRequestForAddnoRecordsComponent();  
                
                rOIFacilityRequestStatusPage.GetRequestIdFromFacilitySide(BaseAddress);
                ROIAdminRequestStatusPage adminRequestStatusPage = new ROIAdminRequestStatusPage(driver, logger, TestContext);                
                adminRequestStatusPage.assignRequester();
                ROIAdminAssignROIRequesterPage assignROIRequesterPage = new ROIAdminAssignROIRequesterPage(driver, logger, TestContext);               
                assignROIRequesterPage.assignTestAttorney();  
                
                adminRequestStatusPage.RateLink();
                ROIAdminUpdateRequestBillingDetailsPage updateRequestBillingDetailsPage = new ROIAdminUpdateRequestBillingDetailsPage(driver, logger, TestContext);
                string appliedRate=updateRequestBillingDetailsPage.UpdateBillingInfoForFTP();
                Assert.AreEqual(appliedRate, "Reg_CustomPostageRate", "Failed to verify applied rate");
                logger.Log(Status.Pass, "Verified that applied rate set to Reg_CustomPostageRate", TakeScreenShotAtStep());
               
                updateRequestBillingDetailsPage.SelectRecentRequestIDFromROIAdmin();               
                
                adminRequestStatusPage.DeliveryOverride("FTP");
                adminRequestStatusPage.ClickOnQcPassButton();
                adminRequestStatusPage.CreateInvoice();
                string invoiceId = adminRequestStatusPage.GetInvoiceId();
                logger.Log(Status.Info, $"Invoice created with id ({invoiceId})", TakeScreenShotAtStep());
                string selectedType=adminRequestStatusPage.ToSelectFTPOption();
                Assert.AreEqual(selectedType, "FTP", "Failed to add FTP option under type");
                logger.Log(Status.Pass, "Verified  that FTP added  under type");

                ROIAdminShipmentDetailsPage shipmentDetailsPage = new ROIAdminShipmentDetailsPage(driver, logger, TestContext);               
                shipmentDetailsPage.ToClickMakeShippableButton();                
                shipmentDetailsPage.SFTPViewer();
                ROIAdminsFTPViewerPage rOIAdminsFTPViewer = new ROIAdminsFTPViewerPage(driver, logger, TestContext);               
                rOIAdminsFTPViewer.ToConfigFTPShipments();
                rOIAdminsFTPViewer.ClickNRSDirectory();
                Cleanup(driver);
            }
            catch (Exception ex)
            {
                LogException(driver, logger, ex);

            }
        }



    }

}

