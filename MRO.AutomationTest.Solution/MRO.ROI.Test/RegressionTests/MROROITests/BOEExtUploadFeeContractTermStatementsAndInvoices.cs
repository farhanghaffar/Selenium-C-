using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AventStack.ExtentReports;
using AventStack.ExtentReports.MarkupUtils;
using DataDrivenProject;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MRO.ROI.Automation;
using MRO.ROI.Automation.Pages;
using MRO.ROI.Automation.Selenium;
using MRO.ROI.Test.Utilities;
using OpenQA.Selenium.Remote;
using System.IO;
using System.Threading;



namespace MRO.ROI.Test.RegressionTests.MROROITests
{

    [TestClass]
    public class BOEExtUploadFeeContractTermStatementsAndInvoices : ROIBaseTest
    {
        public BOEExtUploadFeeContractTermStatementsAndInvoices() : base(ROITestArea.ROITestFacility)
        {
        }

        [TestMethod]
        [TestCategory(ROITestCategory.Development)]
        //Converted manual test case 15007-ROI-Facility--> BOE Ext Upload Fee Contract Term Statements And Invoices to automated.
        public void Reg_15007_BOEExtUploadFeeContractTermStatementsAndInvoices()
        {
            logger = extent.CreateTest("Reg_15007_BOE Ext Upload Fee Contract Term Statements And Invoices to automated");
            logger.Log(Status.Info, "Converted manual test case 15007-ROI-Admin-->BOE Ext Upload Fee Contract Term Statements And Invoices to automated");
            RemoteWebDriver _driver = Init(TestContext.Properties["browser"].ToString());
            ThreadLocal<RemoteWebDriver> localDriver = new ThreadLocal<RemoteWebDriver>();
            localDriver.Value = _driver;
            RemoteWebDriver driver = localDriver.Value;
             try
            {
                ROIFacilityWorkSummaryPage rOIFacilityWorkSummaryPage = new ROIFacilityWorkSummaryPage(driver, logger, TestContext);
                rOIFacilityWorkSummaryPage.GoToLogNewRequestPage();
                ROIFacilityLogNewRequestPage rOIFacilityLogNewRequestPage = new ROIFacilityLogNewRequestPage(driver, logger, TestContext);
                rOIFacilityLogNewRequestPage.CreateBillingOfficeRequest();
                ROIFacilityRequestStatusPage rOIFacilityRequestStatusPage = new ROIFacilityRequestStatusPage(driver, logger, TestContext);
                string requestID = rOIFacilityRequestStatusPage.GetRequestID();
                logger.Log(Status.Pass, $"Request created with requestid ({requestID})", TakeScreenShotAtStep());
                ROIAdminHomePage rOIAdminHomePage = new ROIAdminHomePage(driver, logger, TestContext);
                rOIAdminHomePage.ROIlookupByRequestId(requestID);
                rOIFacilityRequestStatusPage.ReOpenRequest();
                rOIFacilityRequestStatusPage.ImportDocumentsForFacility();
                rOIFacilityRequestStatusPage.ReleaseRequest();
                logger.Log(Status.Info, "Request Released");
                rOIAdminHomePage.SwitchToNewTabAndLoginROIAdmin(BaseAddress);
                rOIAdminHomePage.ROIlookupByRequestId(requestID);
                ROIAdminRequestStatusPage adminRequestStatusPage = new ROIAdminRequestStatusPage(driver, logger, TestContext);
                adminRequestStatusPage.ClickPassDocsQC();
                adminRequestStatusPage.RateLink();
                ROIAdminUpdateRequestBillingDetailsPage rOIAdminUpdateRequestBillingDetailsPage = new ROIAdminUpdateRequestBillingDetailsPage(driver, logger, TestContext);
                rOIAdminUpdateRequestBillingDetailsPage.UpdateRegressionBaseRate();
                adminRequestStatusPage.ApplyRate();
                adminRequestStatusPage.CreateInvoice();
                string invoiceId = adminRequestStatusPage.GetInvoiceId();
                logger.Log(Status.Info, $"Invoice created with ID({invoiceId})");
                adminRequestStatusPage.SelectEXTUpload();
                ROIAdminShipmentDetailsPage rOIAdminShipmentDetailsPage=new ROIAdminShipmentDetailsPage(driver, logger, TestContext);
                rOIAdminShipmentDetailsPage.ToClickMakeShippableButton();
                //string todayDate = DateTime.Now.ToString("M/d/yyyy");
                //string fetchedOn = rOIAdminShipmentDetailsPage.FetchOnDate();
                //Assert.AreEqual(todayDate,fetchedOn,"Failed to validate shipment date as todays date");
               // logger.Log(Status.Pass, $"Verified shipment date as ({todayDate})", TakeScreenShotAtStep());
                rOIAdminHomePage.ROIlookupByRequestId(requestID);
                rOIAdminHomePage.FacilityList();
                ROIAdminFacilityListPage rOIAdminFacilityListPage = new ROIAdminFacilityListPage(driver, logger, TestContext);
                rOIAdminFacilityListPage.GotoROITestFacilityComputerIcon();
                rOIFacilityWorkSummaryPage.SearchByRequestId(requestID);
                rOIFacilityRequestStatusPage.ClickPendingExternalSiteUploads();
                PendingExternalSiteDeliveryReportPage pendingExternalSiteDeliveryReport = new PendingExternalSiteDeliveryReportPage(driver, logger, TestContext);
                pendingExternalSiteDeliveryReport.SetDatesAndRequestID(requestID);
                pendingExternalSiteDeliveryReport.DeliverRequestByRequestID(requestID);
                pendingExternalSiteDeliveryReport.DownloadMroShipmentDocuments();
                rOIAdminHomePage.ROIlookupByRequestId(requestID);
                rOIAdminHomePage.SwitchToNewTabAndLoginROIAdmin(BaseAddress);
                rOIAdminHomePage.ContractList();
                Cleanup(driver);
            }
            catch (Exception ex)
            {

                LogException(driver, logger, ex);
            }
        }
    }
}







