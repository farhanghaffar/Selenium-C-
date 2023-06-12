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
    public class ROIAdminBOETransactionFeeUpdateTest : ROIBaseTest
    {
        public ROIAdminBOETransactionFeeUpdateTest() : base(ROITestArea.ROIAdmin)
        {
        }

        [TestMethod]
        [TestCategory(ROITestCategory.Regression)]
        //Converted manual test case-ROI-Admin-->BOE Transaction Fee- Update to automated
        public void Reg_11295_BOE_Transaction_Fee_Update()
        {
            logger = extent.CreateTest("Reg_11295_BOE_Transaction_Fee_Update");
            logger.Log(Status.Info, "Converted manual test case-ROI-Admin-->BOE Transaction Fee- Update to automated");
            RemoteWebDriver _driver = Init(TestContext.Properties["browser"].ToString());
            ThreadLocal<RemoteWebDriver> localDriver = new ThreadLocal<RemoteWebDriver>();
            localDriver.Value = _driver;
            RemoteWebDriver driver = localDriver.Value;
            string userRoot = System.Environment.GetEnvironmentVariable("USERPROFILE");
            string downloadFolder = Path.Combine(userRoot, "Downloads\\");
            try
            {
                string selectedfacility = IniHelper.ReadConfig("ROIAdminBOETransactionFeeUpdateTest", "facility");
                ROIAdminHomePage rOIAdminHomePage = new ROIAdminHomePage(driver, logger, TestContext);
                rOIAdminHomePage.ClickFacilityList();
                ROIAdminFacilityListPage rOIAdminFacilityListPage = new ROIAdminFacilityListPage(driver, logger, TestContext);
                rOIAdminFacilityListPage.GoToROITestFacility();
                ROIFacilityWorkSummaryPage rOIFacilityWorkSummaryPage = new ROIFacilityWorkSummaryPage(driver, logger, TestContext);
                rOIFacilityWorkSummaryPage.logaNewRequest();
                ROIFacilityLogNewRequestPage rOIFacilityLogNewRequestPage = new ROIFacilityLogNewRequestPage(driver, logger, TestContext);
                rOIFacilityLogNewRequestPage.CreateInternalPortalRequestForAj();
                logger.Log(Status.Info, "Logged new internal portal request for AJ", TakeScreenShotAtStep());
                ROIFacilityRequestStatusPage rOIFacilityRequestStatusPage = new ROIFacilityRequestStatusPage(driver, logger, TestContext);
                string requestid = rOIFacilityRequestStatusPage.GetRequestID();
                rOIFacilityRequestStatusPage.ImportPdfFiles();
                bool statusUnderImportDocument = rOIFacilityRequestStatusPage.VerifyStatusUnderImportDocument();
                Assert.IsTrue(statusUnderImportDocument, "Failed to verify status under import document is uploaded");
                rOIFacilityRequestStatusPage.ReleaseRequestForInternalRequest();
                logger.Log(Status.Info, "Request released");
                ROIAdminRequestStatusPage rOIAdminRequestStatusPage = new ROIAdminRequestStatusPage(driver, logger, TestContext);
                rOIFacilityRequestStatusPage.ClickCreatePDF();                           
                driver.SleepTheThread(30);
                rOIFacilityRequestStatusPage.ReloadReqID();
                string ShippedStatus = rOIFacilityRequestStatusPage.GetShipmentStatus();
                Assert.AreEqual("Shipped", ShippedStatus, "Verified shippment status - Shipped");
                logger.Log(Status.Info, "Verified request status as shipped", TakeScreenShotAtStep());
                string ShippedDate = rOIFacilityRequestStatusPage.GetShipmentDate();
                Assert.AreEqual(DateTime.Now.ToString("M/d/yyyy"), ShippedDate, "Verifirf correct shipped date");
                logger.Log(Status.Info, "Verified shipped date", TakeScreenShotAtStep());
                var isDisplayed = rOIFacilityRequestStatusPage.VerifyDownloadBtnVisible();
                Assert.IsTrue(isDisplayed);
                logger.Log(Status.Info, "Verified that download button is displayed", TakeScreenShotAtStep());

                rOIAdminHomePage.SwitchToNewTabAndLoginROIAdmin(BaseAddress);
                rOIAdminHomePage.ClickOnMonthlyStatements();
                ROIAdminMonthlyStatementReportPage rOIAdminMonthlyStatementReportPage = new ROIAdminMonthlyStatementReportPage(driver, logger, TestContext);
                rOIAdminMonthlyStatementReportPage.CreateCurrentMonthStatementForBOE();
                rOIAdminMonthlyStatementReportPage.VerifyBOETransactionFeeAndClick();
                logger.Log(Status.Info, "Verified BOE transaction fee", TakeScreenShotAtStep());
                ROIadminMonthlyStatementReportDetailsPage rOIadminMonthlyStatementReportDetailsPage = new ROIadminMonthlyStatementReportDetailsPage(driver, logger, TestContext);                
                string MSRD_requestID = rOIadminMonthlyStatementReportDetailsPage.VerifyRequestId(requestid);
                Assert.AreNotEqual(requestid, MSRD_requestID, "Verified that request ID not present", TakeScreenShotAtStep());               
                string firstRequestIDUI = rOIadminMonthlyStatementReportDetailsPage.FirstReqIDInUI();
                rOIadminMonthlyStatementReportDetailsPage.ClickExcelIcon();

                ExcelReaderFile excelReaderFile = new ExcelReaderFile();               
                string firstRequestIdExcel = excelReaderFile.ReadExcelCellData(downloadFolder + "ROIMonthlyStatementDetail_ROI Test Facility_1484_202100.xlsx", 6, 1);
                Assert.AreEqual(firstRequestIdExcel, firstRequestIDUI, "Failed to validate UI shipment and excel shipment id");
                logger.Log(Status.Info, $"Verified excel first shipment id ({firstRequestIdExcel}) with UI first shipment id ({firstRequestIDUI})", TakeScreenShotAtStep());
                ExcelReaderFile.DeleteExistingFiles(downloadFolder, "xlsx", "ROIMonthlyStatementDetail_ROI Test Facility_1484_202100.xlsx");                

                rOIAdminHomePage.ClickFacilityList();
                rOIAdminFacilityListPage.GoToROITestFacility();
                rOIFacilityWorkSummaryPage.logaNewRequest();
                rOIFacilityLogNewRequestPage.CreateInternalPortalRequestForAj();
                logger.Log(Status.Info, "Logged new internal portal request for AJ", TakeScreenShotAtStep());
                string requestidexists = rOIFacilityRequestStatusPage.GetRequestID();
                rOIFacilityRequestStatusPage.ImportPdfFiles();
                rOIFacilityRequestStatusPage.ReleaseRequestForInternalRequest();
                logger.Log(Status.Info, "Request released");
                rOIFacilityRequestStatusPage.FacilityLogout();

                rOIAdminHomePage.ROIlookupByRequestId(requestidexists);
                string testFacility = rOIAdminRequestStatusPage.VerifyTestFacility();
                Assert.AreEqual(testFacility, "ROI-Test Facility CBO");
                logger.Log(Status.Info, "Verified facility as ROI-Test Facility CBO");

                string shipText = rOIAdminRequestStatusPage.VerifyShipText();
                Assert.AreEqual(shipText, "AJ & Associates At Law PLLC");
                logger.Log(Status.Info, "Verified shipped text - AJ & Associates At Law PLLC");

                rOIAdminRequestStatusPage.ClickPassDocsQC();
                rOIAdminRequestStatusPage.DeliveryOverride("EMAIL");
                rOIAdminRequestStatusPage.RateLink();   
                ROIAdminUpdateRequestBillingDetailsPage rOIAdminUpdateRequestBillingDetailsPage = new ROIAdminUpdateRequestBillingDetailsPage(driver, logger, TestContext);
                rOIAdminUpdateRequestBillingDetailsPage.ClickonRGBRSelect();
                rOIAdminUpdateRequestBillingDetailsPage.ClickUpdateAnApply();
                logger.Log(Status.Info, "Verified that rate has been applied for the requester", TakeScreenShotAtStep());
                rOIAdminUpdateRequestBillingDetailsPage.ClickSaveAndExit();                
                rOIAdminRequestStatusPage.CreateInvoiceAndYes();
                rOIAdminRequestStatusPage.GetInvoiceId();
                rOIAdminRequestStatusPage.AddEmailAndClickAdd();
                //rOIFacilityRequestStatusPage.ReloadReqID();
                rOIAdminHomePage.ClickOnMonthlyStatements();               
                rOIAdminMonthlyStatementReportPage.CreateCurrentMonthStatementForBOE();
                rOIAdminMonthlyStatementReportPage.VerifyBOETransactionFeeAndClick();
                string msdr_RequestID = rOIadminMonthlyStatementReportDetailsPage.VerifyRequestId(requestidexists);
                Assert.AreEqual(requestid, msdr_RequestID, "Failed to validate request ID in MSRD page");
                string firstRequestIDUI2 = rOIadminMonthlyStatementReportDetailsPage.FirstReqIDInUI();
                rOIadminMonthlyStatementReportDetailsPage.ClickExcelIcon();                
                string firstRequestIdExcel2 = excelReaderFile.ReadExcelCellData(downloadFolder + "ROIMonthlyStatementDetail_ROI Test Facility_1484_202100.xlsx", 6, 1);
                Assert.AreEqual(firstRequestIdExcel, firstRequestIDUI, "Failed to validate UI shipment and excel shipment id");
                logger.Log(Status.Info, $"Verified excel first shipment id ({firstRequestIdExcel}) with UI first shipment id ({firstRequestIDUI})", TakeScreenShotAtStep());
                ExcelReaderFile.DeleteExistingFiles(downloadFolder, "xlsx", "ROIMonthlyStatementDetail_ROI Test Facility_1484_202100.xlsx");

                Cleanup(driver);
            }
            catch (Exception ex)
            {
                LogException(driver, logger, ex);
            }
        }
    }
}
