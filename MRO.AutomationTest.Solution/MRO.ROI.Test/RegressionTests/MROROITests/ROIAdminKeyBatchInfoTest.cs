using AventStack.ExtentReports;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MRO.ROI.Automation.Pages;
using MRO.ROI.Test.ExecutionFactory;
using MRO.ROI.Test.Utilities;
using OpenQA.Selenium.Remote;
using System;
using System.Collections.Generic;
using System.Threading;

namespace MRO.ROI.Test.RegressionTests.MROROITests
{
    [TestClass]
    public class ROIAdminKeyBatchInfoTest : ROIBaseTest
    {
        public ROIAdminKeyBatchInfoTest() : base(ROITestArea.ROIFacility)
        {
        }
        [STATestMethodAttribute]
        [TestCategory(ROITestCategory.Regression)]
        //Converted manual test case 1224-ROI-Admin-->Key Batch Info (default testing for regular grid) to automated
        public void Reg_1224_ROIAdminKeyBatchInfoTest()
        {
            logger = extent.CreateTest("Reg_1224_ROIAdminKeyBatchInfoTest");
            logger.Log(Status.Info, "Converted manual test case 1224-ROI-Admin-->Key Batch Info (default testing for regular grid) to automated");
            RemoteWebDriver _driver = Init(TestContext.Properties["browser"].ToString());
            ThreadLocal<RemoteWebDriver> localDriver = new ThreadLocal<RemoteWebDriver>();
            localDriver.Value = _driver;
            RemoteWebDriver driver = localDriver.Value;
            try
            {
                ROIFacilityWorkSummaryPage rOIFacilityWorkSummaryPage = new ROIFacilityWorkSummaryPage(driver, logger, TestContext);
                rOIFacilityWorkSummaryPage.GoToBatchAndSelectKeyBatchInfo();
                ROIFacilityKeyBatchInfoPage rOIFacilityKeyBatchInfoPage = new ROIFacilityKeyBatchInfoPage(driver, logger, TestContext);
                rOIFacilityKeyBatchInfoPage.ClickCreateBatchRequest();
                bool _keyBatchInfoHeaders = rOIFacilityKeyBatchInfoPage.VerifyKeyBatchInfoGridHeader();
                Assert.IsTrue(_keyBatchInfoHeaders, "Failed to verify default grids loaded successfully");
                logger.Log(Status.Pass, $"Verified all default grids loaded successfully(Last name, First Name, DOB, MRN,SSN)", TakeScreenShotAtStep());
                string txtBatchNumber=  rOIFacilityKeyBatchInfoPage.GetSystemBatchnumber();
                rOIFacilityKeyBatchInfoPage.ClickOnSaveBatchAndLogKeyBatchInfoRequest();
                ROIFacilityScanRequestDocumentsPage rOIFacilityScanRequestDocumentsPage = new ROIFacilityScanRequestDocumentsPage(driver, logger, TestContext);
                IList<string> requestIds = rOIFacilityScanRequestDocumentsPage.GetKeyBatchRequestIDsAndImportRequestPages();
                logger.Log(Status.Info, $"successfully requestid's got created on batch processing report : ({requestIds[0]}), ({requestIds[1]}),({requestIds[2]})");
                rOIFacilityWorkSummaryPage.GoToBatchAndBatchProcessingReport();
                ROIFacilityBatchProcessingReportPage rOIFacilityBatchProcessingReportPage = new ROIFacilityBatchProcessingReportPage(driver, logger, TestContext);
                rOIFacilityBatchProcessingReportPage.CreateBatchProcessingReport(txtBatchNumber);
                rOIFacilityBatchProcessingReportPage.SelectAllAndMakeRequest();
                rOIFacilityWorkSummaryPage.GoToBatchAndBatchProcessingReport();
                rOIFacilityBatchProcessingReportPage.CreateBatchProcessingReportAndIncludeCompleteChk(txtBatchNumber);

                string _batchNumber= rOIFacilityBatchProcessingReportPage.VerifyBatchNumber();
                Assert.AreEqual(txtBatchNumber,_batchNumber, "Failed to verify batch number");
                logger.Log(Status.Pass, $"successfully verified batch number: ({_batchNumber}) on batch processing report");

                string _actualStatus = "Complete";
                string _ExpectedStatus = rOIFacilityBatchProcessingReportPage.VerifyStatusOfBatchReport();
                Assert.AreEqual(_actualStatus, _ExpectedStatus, "Failed to verify status of batch report");
                logger.Log(Status.Pass, $"successfully verified status is ({_ExpectedStatus}) on batch processing report" , TakeScreenShotAtStep());
                Cleanup(driver);
            }
            catch (Exception ex)
            {
                LogException(driver, logger, ex);
            }
        }
    }
}