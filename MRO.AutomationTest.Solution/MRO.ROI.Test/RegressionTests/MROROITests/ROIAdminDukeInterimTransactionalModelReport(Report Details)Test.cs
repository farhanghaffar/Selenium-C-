using AventStack.ExtentReports;
using DataDrivenProject;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MRO.ROI.Automation.Pages;
using MRO.ROI.Automation.Pages.Common;
using MRO.ROI.Automation.Selenium;
using MRO.ROI.Test.Utilities;
using OpenQA.Selenium.Remote;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MRO.ROI.Test.RegressionTests.MROROITests
{
    [TestClass]
    public class ROIAdminDukeInterimTransactionalModelReportDetailsTest : ROIBaseTest
    {
        public ROIAdminDukeInterimTransactionalModelReportDetailsTest() : base(ROITestArea.ROIAdmin)
        {
        }

        [TestMethod]
        [TestCategory(ROITestCategory.Regression)]
        // Converted manual test case 11356-ROI-Admin-->2Duke Interim Transactional Model Report (Report Details) to automated
        public void Reg_11356_ROIAdminDukeInterimTransactionalModelReportDetails()
        {
            logger = extent.CreateTest("Reg_11356_ROIAdminDukeInterimTransactionalModelReportDetails");
            logger.Log(Status.Info, "Converted manual test case 11356-ROI-Admin-->2Duke Interim Transactional Model Report (Report Details) to automated");
            RemoteWebDriver _driver = Init(TestContext.Properties["browser"].ToString());
            ThreadLocal<RemoteWebDriver> localDriver = new ThreadLocal<RemoteWebDriver>();
            localDriver.Value = _driver;
            RemoteWebDriver driver = localDriver.Value;
            ROIAdminTransactionalModelReportPage rOIAdminTransactionalModelReportPage = new ROIAdminTransactionalModelReportPage(driver, logger, TestContext);

            try
            {
                rOIAdminTransactionalModelReportPage.DeleteDownloadedExcelFiles();
                ROIMenuSelector menuSelector = new ROIMenuSelector(driver, logger, TestContext);
                menuSelector.SelectRoiAdminMenuOptions("mnuROIAdmin", "Financial", "Transactional Model Report");
                rOIAdminTransactionalModelReportPage.CreateTransactionalModelReport("ROI Test Facility", "[All]", true);
                logger.Log(Status.Info, "Transactional report created for Facility : ROI Test Facility Contract : [All] IncludeTest : True");

                rOIAdminTransactionalModelReportPage.DownloadExcelReport();
                var excelTransactionalModelAllData = rOIAdminTransactionalModelReportPage.ReturnExcelTransactionalModelReportData();
                var uiTransactionalModelAllData = rOIAdminTransactionalModelReportPage.ReturnUITransactionalModelReportData();
                bool isValidatedTransactionalModelDataForFirstAndLastLinks = rOIAdminTransactionalModelReportPage.ClickOnFirstAndLastLinksAndVerifyPDFAndExcelHasDataIntact();
                Assert.IsTrue(isValidatedTransactionalModelDataForFirstAndLastLinks, "Failed to validate the first and last links of the transactional model data");
                logger.Log(Status.Pass, "Valiated UI and excel transactional model summary report data for filter Facility : [All] Contract : [All] IncludeTest : True");

                rOIAdminTransactionalModelReportPage.SwitchToRDFrame();
                string requestLoggedAmountForMROEmployee = rOIAdminTransactionalModelReportPage.GetSpecificAmountFromLink("Requests Logged by MRO Employees:");
                logger.Log(Status.Info, $"Requests logged count ({requestLoggedAmountForMROEmployee})", TakeScreenShotAtStep());
                string requestReleasedAmountForMROEmployee = rOIAdminTransactionalModelReportPage.GetSpecificAmountFromLink("Requests Released by MRO Employees:");
                logger.Log(Status.Info, $"Requests released count ({requestReleasedAmountForMROEmployee})", TakeScreenShotAtStep());
                string requestAssignments = rOIAdminTransactionalModelReportPage.GetSpecificAmountFromLink("Request Assignments:");
                logger.Log(Status.Info, $"Requests assignments count ({requestReleasedAmountForMROEmployee})", TakeScreenShotAtStep());
                string invoiceGenerated = rOIAdminTransactionalModelReportPage.GetSpecificAmountFromLink("Invoices Generated:");
                logger.Log(Status.Info, $"Invoice generated count ({requestReleasedAmountForMROEmployee})", TakeScreenShotAtStep());
                string billableShipments = rOIAdminTransactionalModelReportPage.GetSpecificAmountFromLink("Billable:");
                logger.Log(Status.Info, $"Billable shipments ({billableShipments})", TakeScreenShotAtStep());
                string paymentsReceivedByMRO = rOIAdminTransactionalModelReportPage.GetSpecificAmountFromLink("Payments Received by MRO:");
                logger.Log(Status.Info, $"Payments received by MRO ({paymentsReceivedByMRO})", TakeScreenShotAtStep());

                ROIAdminHomePage roiAdminHomePage = new ROIAdminHomePage(driver, logger, TestContext);
                roiAdminHomePage.SwitchToNewTabAndLoginROIFacility(BaseAddress);
                ROIFacilityWorkSummaryPage rOIFacilityWorkSummaryPage = new ROIFacilityWorkSummaryPage(driver, logger, TestContext);
                rOIFacilityWorkSummaryPage.GoToLogNewRequestPage();
                LogNewRequestPage logNewRequestPage = new LogNewRequestPage(driver, logger, TestContext);
                logNewRequestPage.CreateNewMRODeliveryRequest("Boston Proper");
                string requestId = logNewRequestPage.getRequestid();
                logger.Log(Status.Info, $"MRO delivery request created ({requestId})");

                roiAdminHomePage.ROIlookupByRequestId(requestId);
                ROIFacilityRequestStatusPage roiFacilityRequestStatusPage = new ROIFacilityRequestStatusPage(driver, logger, TestContext);
                roiFacilityRequestStatusPage.ImportRequestPages();
                roiFacilityRequestStatusPage.ImportPatientPages();
                roiFacilityRequestStatusPage.ReleaseRequestID();
                logger.Log(Status.Info, $"Request released");
                roiFacilityRequestStatusPage.UnReleaseRequestID();
                logger.Log(Status.Info, $"Request un-released");
                roiFacilityRequestStatusPage.ReleaseRequestID();
                logger.Log(Status.Info, $"Request released again");

                roiAdminHomePage.SwitchToNewTabAndLoginROIAdmin(BaseAddress);
                roiAdminHomePage.ROIlookupByRequestId(requestId);
                menuSelector.SelectRoiAdminMenuOptions("mnuROIAdmin", "Financial", "Transactional Model Report");
                rOIAdminTransactionalModelReportPage.CreateTransactionalModelReport("[All]", "[All]", true);
                logger.Log(Status.Info, "Transactional report created for Facility : [All] Contract : [All] IncludeTest : True");

                string requestLoggedAmountForMROEmployeeAfterRelease = rOIAdminTransactionalModelReportPage.GetSpecificAmountFromLink("Requests Logged by MRO Employees:");
                logger.Log(Status.Info, $"Requests logged count ({requestLoggedAmountForMROEmployeeAfterRelease})", TakeScreenShotAtStep());
                string requestReleasedAmountForMROEmployeeAfterRelease = rOIAdminTransactionalModelReportPage.GetSpecificAmountFromLink("Requests Released by MRO Employees:");
                logger.Log(Status.Info, $"Requests released count ({requestReleasedAmountForMROEmployeeAfterRelease})", TakeScreenShotAtStep());
                Assert.IsTrue(Convert.ToInt32(requestLoggedAmountForMROEmployeeAfterRelease) > (Convert.ToInt32(requestLoggedAmountForMROEmployee)), "Failed to validate request logged count");
                Assert.IsTrue(Convert.ToInt32(requestReleasedAmountForMROEmployeeAfterRelease) > (Convert.ToInt32(requestReleasedAmountForMROEmployee)), "Failed to validate request released count");
                logger.Log(Status.Pass, "Validated requests logged and requests released counts");
                rOIAdminTransactionalModelReportPage.ClickOnSpecificAmountLink("Requests Logged by MRO Employees:");
                rOIAdminTransactionalModelReportPage.FilterReportBasedOnRequestDate("sr_dtLoggedRequests");
                bool isRequestIdExistRL = rOIAdminTransactionalModelReportPage.CheckRequestIdExsist(requestId);
                Assert.IsTrue(isRequestIdExistRL, "Failed to validate request under Requests Logged by MRO Employees:");
                logger.Log(Status.Pass, $"Validated requestid ({requestId}) under Requests Logged by MRO Employees:", TakeScreenShotAtStep());
                bool isValidatedRequestsLoggedByMROEmployee = rOIAdminTransactionalModelReportPage.ValidateForRequestIdInPDFAndExcel("sr_dtLoggedRequests", requestId);
                Assert.IsTrue(isValidatedRequestsLoggedByMROEmployee, "Failed to validate Excel and PDF data for request logged by MRO Employees:");
                logger.Log(Status.Pass, "Valiated UI and excel transactional model summary report data for request logged by MRO Employees:");
                rOIAdminTransactionalModelReportPage.ClickOnSpecificAmountLink("Requests Logged by MRO Employees:");
                rOIAdminTransactionalModelReportPage.DeleteDownloadedExcelFiles();
                //Last 3 steps pending for validations
                driver.SwitchToDefaultContent();
                LoginPage loginPage = new LoginPage(driver, logger, TestContext);
                loginPage.LogOut();

                Cleanup(driver);
            }
            catch (Exception ex)
            {
                rOIAdminTransactionalModelReportPage.DeleteDownloadedExcelFiles();
                LogException(driver, logger, ex);
            }
        }
    }
}
