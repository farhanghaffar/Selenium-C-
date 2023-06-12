using AventStack.ExtentReports;
using AventStack.ExtentReports.MarkupUtils;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MRO.ROI.Automation.Common.Navigation;
using MRO.ROI.Automation.Pages;
using MRO.ROI.Automation.Selenium;
using MRO.ROI.Test.Utilities;
using OpenQA.Selenium.Remote;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MRO.ROI.Test.RegressionTests.MROROITests
{
    [TestClass]
    public class ROIAdminRDABugInforTest : ROIBaseTest
    {
        public ROIAdminRDABugInforTest() : base(ROITestArea.ROITestFacility)
        {
        }

        [TestMethod]
        [TestCategory(ROITestCategory.Development)]
        public void Reg_11408_ROIAdminRDABugInfor()
        {
            logger = extent.CreateTest("Reg_11408_ROIAdminRDABugInfor");
            RemoteWebDriver _driver = Init(TestContext.Properties["browser"].ToString());
            ThreadLocal<RemoteWebDriver> localDriver = new ThreadLocal<RemoteWebDriver>();
            localDriver.Value = _driver;
            RemoteWebDriver driver = localDriver.Value;
            

            try
            {               
                ROIFacilityWorkSummaryPage rOIFacilityWorkSummaryPage = new ROIFacilityWorkSummaryPage(driver, logger, TestContext);
                rOIFacilityWorkSummaryPage.ClickKeyBatchInfo();
                logger.Log(Status.Info, "Clicked on key batch info");                
                ROIFacilityKeyBatchInfoPage rOIFacilityKeyBatchInfoPage = new ROIFacilityKeyBatchInfoPage(driver, logger, TestContext);
                rOIFacilityKeyBatchInfoPage.ClickCreateBatch();
                logger.Log(Status.Info, "Clicked on create batch");                
                rOIFacilityKeyBatchInfoPage.ClickOnSaveBatchAndLogRequest();
                logger.Log(Status.Info, "Clicked on save batch and logged request");
                ROIFacilityScanRequestDocumentsPage rOIFacilityScanRequestDocumentsPage = new ROIFacilityScanRequestDocumentsPage(driver, logger, TestContext);
                rOIFacilityScanRequestDocumentsPage.CheckBoxReqIdIsDisabled();
                string requestid = rOIFacilityScanRequestDocumentsPage.GetRequestId();


                logger.Log(Status.Info, $"request created with id :{(requestid)}");


                ROIAdminHomePage rOIAdminHomePage = new ROIAdminHomePage(driver, logger, TestContext);
                rOIAdminHomePage.OpenNewTabAndLoginROITestFacility(BaseAddress);


                rOIFacilityWorkSummaryPage.SearchByRequestId(requestid);

                
                ROIFacilityLogNewRequestPage rOIFacilityLogNewRequestPage = new ROIFacilityLogNewRequestPage(driver, logger, TestContext);
                rOIFacilityLogNewRequestPage.ImportRequestPages();
                rOIAdminHomePage.SwitchBackToRoiTestFacilityAndRefresh(BaseAddress);

                ROIFacilityCompleteLoggingPage rOIFacilityCompleteLoggingPage = new ROIFacilityCompleteLoggingPage(driver, logger, TestContext);
                rOIFacilityCompleteLoggingPage.ClickViewDocumnetAndReturnRequestDocumentsCount();
                rOIFacilityCompleteLoggingPage.ClickOnPatientHyperLink();

                rOIFacilityLogNewRequestPage.EpicRef();





















                Console.WriteLine("test");



            }
            catch (Exception ex)
            {
                LogException(driver, logger, ex);
            }
        }
    }
}