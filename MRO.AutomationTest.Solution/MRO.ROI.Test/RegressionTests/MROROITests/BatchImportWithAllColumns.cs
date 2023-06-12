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
using System.Collections.Generic;
using System.IO;
using System.Threading;
using static MRO.ROI.Automation.Utility.IniFile;

namespace MRO.ROI.Test.RegressionTests.MROROITests
{
   
    [TestClass]
    public class BatchImportWithAllColumns : ROIBaseTest
    {
        public BatchImportWithAllColumns() : base(ROITestArea.ROIAdmin)
        {

        }
        [STATestMethodAttribute]
        [TestCategory(ROITestCategory.Development)]
       
        // Converted manual test case 1189-Hotfix_Testing Batch Import with All columns to test parsing functionality

        public void Reg_1189_BatchImportWithAllColumns()
        {
            logger = extent.CreateTest("Reg_1189_BatchImportWithAllColumns");
            logger.Log(Status.Info, "Converted manual test case Reg_1189_BatchImportWithAllColumns");
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
                rOIFacilityImportFilePage.ImportBatchFile(",", "TEST", "SampleBatch422.csv");
                rOIFacilityImportFilePage.MakeARequest();
                ROIFacilityScanRequestDocumentsPage rOIFacilityScanRequestDocumentsPage = new ROIFacilityScanRequestDocumentsPage(driver, logger, TestContext);
                IList<string> requestIds = rOIFacilityScanRequestDocumentsPage.GetRequestIDsAndImportRequestPagesTwo();
                rOIFacilityWorkSummaryPage.GoToBatchAndSelectKeyBatchInfo();
                ROIFacilityKeyBatchInfoPage rOIFacilityKeyBatchInfoPage = new ROIFacilityKeyBatchInfoPage(driver, logger, TestContext);
                rOIFacilityKeyBatchInfoPage.ClickCreateBatch();
                rOIFacilityKeyBatchInfoPage.ClickOnSaveBatchAndLogRequest();
                ROIadminScanRequestDocumentsPage rOIadminScanRequestDocumentsPage = new ROIadminScanRequestDocumentsPage(driver, logger, TestContext);
                string reqID1 = rOIadminScanRequestDocumentsPage.GetRequestIdOne();


                //scan documents option is not available, need to check if there is other way to scan docs
              



            }
            catch (Exception ex)
            {
                LogException(driver, logger, ex);

            }
        }
    }
}
