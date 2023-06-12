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
    public class SearchByPatientNameFacilitySideDeliveryReport : ROIBaseTest
    {
        public SearchByPatientNameFacilitySideDeliveryReport() : base(ROITestArea.ROITestFacility)
        {
        }

        [TestMethod]
        [TestCategory(ROITestCategory.Development)]
        //Converted manual test case 3449-ROI-Admin--> Search by Patient Name - Facility Side Delivery Report to automated.
        public void Reg_3449_SearchByPatientNameFacilitySideDeliveryReport()
        {
            logger = extent.CreateTest("Reg_3449_SearchByPatientNameFacilitySideDeliveryReport to automated");
            logger.Log(Status.Info, "Converted manual test case 3449-ROI-Admin-->Search By Patient Name Facility Side Delivery Report to automated ");
            RemoteWebDriver _driver = Init(TestContext.Properties["browser"].ToString());
            ThreadLocal<RemoteWebDriver> localDriver = new ThreadLocal<RemoteWebDriver>();
            localDriver.Value = _driver;
            RemoteWebDriver driver = localDriver.Value;
            string userRoot = System.Environment.GetEnvironmentVariable("USERPROFILE");
            string downloadFolder = Path.Combine(userRoot, "Downloads\\");
            string currentReportName = string.Empty;

            try
            {
                ROIAdminHomePage rOIAdminHomePage = new ROIAdminHomePage(driver, logger, TestContext);
                ROIFacilityWorkSummaryPage rOIFacilityWorkSummaryPage = new ROIFacilityWorkSummaryPage(driver, logger, TestContext);
                rOIFacilityWorkSummaryPage.GoToLogNewRequestPage();
                ROIFacilityLogNewRequestPage rOIFacilityLogNewRequestPage = new ROIFacilityLogNewRequestPage(driver, logger, TestContext);
                rOIFacilityLogNewRequestPage.ClickMRODeliveryTab();
                rOIFacilityLogNewRequestPage.MRODeliveryRequestForBostonProper();
                String FirstName = rOIFacilityLogNewRequestPage.GetPatientFirstName();
                String LastName = rOIFacilityLogNewRequestPage.GetPatientLastName();
                ROIFacilityRequestStatusPage rOIFacilityRequestStatusPage = new ROIFacilityRequestStatusPage(driver, logger, TestContext);
                string requestID = rOIFacilityRequestStatusPage.GetRequestID();
                logger.Log(Status.Pass, $"Request created with requestid ({requestID})", TakeScreenShotAtStep());
                rOIAdminHomePage.ROIlookupByRequestId(requestID);
                rOIFacilityRequestStatusPage.ImportDocumentsForFacilityDeliveryReport();
                rOIFacilityRequestStatusPage.ReleaseRequestID();
                logger.Log(Status.Info, "Request Released");
                rOIFacilityRequestStatusPage.ClickDeliveryReport();
                ROIFacilityDeliveryReportPage deliveryReportPage = new ROIFacilityDeliveryReportPage(driver, logger, TestContext);
                deliveryReportPage.SetDeliveredDate();
                deliveryReportPage.ClickCreateReportBtn();
                bool IsDisplayed=deliveryReportPage.CheckRequestIdExsist(requestID);
                Assert.IsFalse(IsDisplayed);
                logger.Log(Status.Info,"patient's data doesnot exist");
                rOIAdminHomePage.SwitchToNewTabAndLoginROIAdmin(BaseAddress);
                rOIAdminHomePage.ROIlookupByRequestId(requestID);
                ROIAdminRequestStatusPage adminRequestStatusPage = new ROIAdminRequestStatusPage(driver, logger, TestContext);
                adminRequestStatusPage.assignRequester();
                ROIAdminAssignROIRequesterPage assignROIRequesterPage = new ROIAdminAssignROIRequesterPage(driver, logger, TestContext);
                assignROIRequesterPage.assignTestAttorney();
                logger.Log(Status.Info, "Assigned Test Attorney");
                adminRequestStatusPage.ClickPassDocsQC();
                adminRequestStatusPage.RateLink();
                ROIAdminUpdateRequestBillingDetailsPage rOIAdminUpdateRequestBillingDetailsPage=new ROIAdminUpdateRequestBillingDetailsPage(driver, logger, TestContext);
                rOIAdminUpdateRequestBillingDetailsPage.UpdateRegressionBaseRate();
                adminRequestStatusPage.ApplyRate();
                string adjustedAmountOnRSSPage = adminRequestStatusPage.GetAdjustedBalanceAmountOnRSS();
                logger.Log(Status.Info, $"Adjusted balance amount on admin request status page is = ({adjustedAmountOnRSSPage})", TakeScreenShotAtStep());
                adminRequestStatusPage.CreateInvoice();
                string invoiceId = adminRequestStatusPage.GetInvoiceId();
                logger.Log(Status.Info, $"Invoice created with ID({invoiceId})");
                string adjustedBalance = adminRequestStatusPage.GetAdjustedBalanceAmountOnRSS();
                logger.Log(Status.Pass, $"Verified adjustable balance amount as ({adjustedBalance})", TakeScreenShotAtStep());
                string shippableDate = adminRequestStatusPage.AddEmailShipmentReturnShippableDate();
                string todayDate = DateTime.Now.ToString("M/d/yyyy");
                Assert.AreEqual(todayDate, shippableDate, "Failed to validate shipment date as todays date");
                logger.Log(Status.Pass, $"Verified shipment date as ({todayDate})", TakeScreenShotAtStep());
                rOIAdminHomePage.FacilityList();
                ROIAdminFacilityListPage rOIAdminFacilityListPage = new ROIAdminFacilityListPage(driver, logger, TestContext);
                rOIAdminFacilityListPage.GotoROITestFacilityComputerIcon();
                rOIFacilityWorkSummaryPage.SearchByRequestId(requestID);
                rOIFacilityRequestStatusPage.ClickDeliveryReport();
                deliveryReportPage.FnValues(FirstName);
                deliveryReportPage.SetDeliverednowDate();
                deliveryReportPage.ClickCreateReportBtn();
                bool isDisplayed = deliveryReportPage.CheckRequestIdExsist(requestID);
                Assert.IsTrue(IsDisplayed);
                logger.Log(Status.Info, "patient data exist");
                deliveryReportPage.LnValues(LastName);
                deliveryReportPage.SetDeliverednowDate();
                deliveryReportPage.ClickCreateReportBtn();
                bool Isdisplayed = deliveryReportPage.CheckRequestIdExsist(requestID);
                Assert.IsTrue(IsDisplayed);
                logger.Log(Status.Info, "patient data exist");
                //Blocked at step-14-report is not getting generated
                Cleanup(driver);
            }
            catch (Exception ex)
            {
               
                LogException(driver, logger, ex);
            }
        }
    }
}






