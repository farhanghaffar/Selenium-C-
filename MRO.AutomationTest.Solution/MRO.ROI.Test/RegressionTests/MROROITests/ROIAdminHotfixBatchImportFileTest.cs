using AventStack.ExtentReports;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MRO.ROI.Automation.Pages;
using MRO.ROI.Test.ExecutionFactory;
using MRO.ROI.Test.Utilities;
using OpenQA.Selenium.Remote;
using System;
using System.Collections.Generic;
using System.Threading;
using static MRO.ROI.Automation.Utility.IniFile;

namespace MRO.ROI.Test.RegressionTests.MROROITests
{
    [TestClass]
    public class ROIAdminHotfixBatchImportFileTest : ROIBaseTest
    {
        public ROIAdminHotfixBatchImportFileTest() : base(ROITestArea.ROIAdmin)
        {
        }
        [STATestMethodAttribute]
        [TestCategory(ROITestCategory.Passed)]
        //Converted manual test case 1186-ROI-Admin-->Hotfix_Batch_ImportFile to be automated
        public void Reg_1186_HotfixBatchImportFile()
        {
            logger = extent.CreateTest("Reg_1186_HotfixBatchImportFile");
            logger.Log(Status.Info, "Converted manual test case 1186-ROI-Admin-->Hotfix_Batch_ImportFile to be automated");
            RemoteWebDriver _driver = Init(TestContext.Properties["browser"].ToString());
            ThreadLocal<RemoteWebDriver> localDriver = new ThreadLocal<RemoteWebDriver>();
            localDriver.Value = _driver;
            RemoteWebDriver driver = localDriver.Value;
            try
            {
                ROIAdminHomePage rOIAdminHomePage = new ROIAdminHomePage(driver, logger, TestContext);
                rOIAdminHomePage.SelectFacilityList();
                ROIAdminFacilityListPage rOIAdminFacilityListPage = new ROIAdminFacilityListPage(driver, logger, TestContext);
                rOIAdminFacilityListPage.GoToROITestFacility();
                ROIFacilityWorkSummaryPage rOIFacilityWorkSummaryPage = new ROIFacilityWorkSummaryPage(driver, logger, TestContext);
                rOIFacilityWorkSummaryPage.GoToBatchAndSelectImportFile();
                ROIFacilityImportFilePage rOIFacilityImportFilePage = new ROIFacilityImportFilePage(driver, logger, TestContext);
                string batchIDTxt1 = IniHelper.ReadConfig("ROIAdminHotfixBatchImportFileTest", "BatchID1");
                string delimiterTxt1 = IniHelper.ReadConfig("ROIAdminHotfixBatchImportFileTest", "Delimiter1");
                rOIFacilityImportFilePage.ImportBatchFile(delimiterTxt1, batchIDTxt1);
                bool _importStatus = rOIFacilityImportFilePage.VerifyImportFileStatus();
                Assert.IsTrue(_importStatus, "Failed to verify request Status=Import Error");
                logger.Log(Status.Info, "Successfully verified request status = import error", TakeScreenShotAtStep());

                bool _problemsOnImport = rOIFacilityImportFilePage.VerifyProblemsonImportError();
                Assert.IsTrue(_problemsOnImport, "Failed to verify Problems on Import columns having errors.");
                logger.Log(Status.Info, "Successfully verified problems on import columns having errors.", TakeScreenShotAtStep());

                rOIFacilityWorkSummaryPage.GoToBatchAndSelectImportFile();
                string batchIDTxt2 = IniHelper.ReadConfig("ROIAdminHotfixBatchImportFileTest", "BatchID2");
                string delimiterTxt2 = IniHelper.ReadConfig("ROIAdminHotfixBatchImportFileTest", "Delimiter2");
                rOIFacilityImportFilePage.ImportBatchFile(delimiterTxt2, batchIDTxt2);
                bool _problemsOnImportForSecondBatch = rOIFacilityImportFilePage.VerifyProblemsonImportErrorforSecondBatch();
                Assert.IsTrue(_problemsOnImportForSecondBatch, "Failed to verify problems on import columns should not contains any errors.");
                logger.Log(Status.Info, "Successfully verified problems on import columns should not contains any errors.", TakeScreenShotAtStep());

                rOIFacilityImportFilePage.MakeARequest();
                ROIFacilityScanRequestDocumentsPage rOIFacilityScanRequestDocumentsPage = new ROIFacilityScanRequestDocumentsPage(driver, logger, TestContext);
                bool _isDeliveryMethod = rOIFacilityScanRequestDocumentsPage.VerifyDeliveryMethod();
                Assert.IsTrue(_isDeliveryMethod, "Failed to verify that the delivery method column for each request is the same to the one used in the CSV file (MRO Delivery, On-Site, etc.)");
                logger.Log(Status.Info, "Successfully verified the delivery method column for each request is the same to the one used in the CSV file (MRO Delivery, On-Site, etc.)", TakeScreenShotAtStep());

                string _batchId = rOIFacilityScanRequestDocumentsPage.GetSystemBatchNumber();
                string requestid = rOIFacilityScanRequestDocumentsPage.GetRequestId();
                IList<string> requestIds = rOIFacilityScanRequestDocumentsPage.GetRequestIDsAndImportRequestPages();               
                rOIFacilityWorkSummaryPage.GoToBatchAndBatchProcessingReport();
                ROIFacilityBatchProcessingReportPage rOIFacilityBatchProcessingReportPage = new ROIFacilityBatchProcessingReportPage(driver, logger, TestContext);
                rOIFacilityBatchProcessingReportPage.CreateBatchProcessingReport(_batchId);
                rOIFacilityBatchProcessingReportPage.SelectAllAndMakeRequest();
                rOIAdminHomePage.SwitchToNewTabAndLoginROIAdmin(BaseAddress);

                ROIMenuSelector menuSelector = new ROIMenuSelector(driver, logger, TestContext);
                menuSelector.SelectRoiAdminMenuOptions("mnuROIAdmin", "Financial", "Transactional Model Report");
                ROIAdminTransactionalModelReportPage rOIAdminTransactionalModelReportPage = new ROIAdminTransactionalModelReportPage(driver, logger, TestContext);
                rOIAdminTransactionalModelReportPage.CreateTransactionalModelReport("ROI Test Facility", "[All]", true);
                logger.Log(Status.Info, "Transactional report created for Facility : ROI Test Facility Contract : [All] IncludeTest : True");

                rOIAdminTransactionalModelReportPage.ClickOnSpecificAmountLink("Requests Logged by MRO Employees:");
                rOIAdminTransactionalModelReportPage.FilterReportBasedOnRequestDate("sr_dtLoggedRequests");
                bool isRequestIdExistRL = rOIAdminTransactionalModelReportPage.CheckRequestIdsExsist(requestIds);
                Assert.IsFalse(isRequestIdExistRL, "Verified that the requestid's that logged through batch import are visible");
                logger.Log(Status.Pass, $" successfully verified requestid's ({requestIds[0]}), ({requestIds[1]}), ({requestIds[2]}), ({requestIds[3]}) that are logged through batch import are NOT visible", TakeScreenShotAtStep());
                    
                bool isValidatedRequestsLoggedByMROEmployee = rOIAdminTransactionalModelReportPage.ValidateForExcelAndPDFContentsWithNoRequestID("sr_dtLoggedRequests", requestid);
                Assert.IsTrue(isValidatedRequestsLoggedByMROEmployee, "Verified that the requestid's that logged through batch import are visible on excel and pdf files");
                logger.Log(Status.Pass, "Successfully verified requestid's that are logged through batch import are NOT visible on the excel and pdf files logged by MRO empolyees");

                rOIAdminTransactionalModelReportPage.ClickOnSpecificAmountLinkBulkLogging("Bulk Logging:", "sr_dtBulkLoggedRequests");
                rOIAdminTransactionalModelReportPage.FilterReportBasedOnRequestDate("sr_dtBulkLoggedRequests");
                bool _isRequestIdExistRL = rOIAdminTransactionalModelReportPage.CheckRequestIdsExsist(requestIds);
                Assert.IsTrue(_isRequestIdExistRL, "Failed to validate requestid's that logged through batch import are NOT visible");
                logger.Log(Status.Pass, $" successfully verified requestid's ({requestIds[0]}), ({requestIds[1]}), ({requestIds[2]}), ({requestIds[3]}) that are logged through batch import are visible", TakeScreenShotAtStep());

                bool _isValidatedRequestsLoggedByBulkLogging = rOIAdminTransactionalModelReportPage.ValidateForExcelAndPDFContents("sr_dtBulkLoggedRequests", requestid);
                Assert.IsTrue(_isValidatedRequestsLoggedByBulkLogging, "Verified that the requestid's that logged through batch import are NOT visible on excel and pdf files");
                logger.Log(Status.Pass, "Successfully verified requestid's that are logged through batch import are visible on the excel and pdf files logged by bulk logging.");

                Cleanup(driver);
            }
            catch (Exception ex)
            {
                LogException(driver, logger, ex);
            }
        }
    }
}
