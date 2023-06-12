using AventStack.ExtentReports;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MRO.ROI.Automation.Pages;
using MRO.ROI.Test.ExecutionFactory;
using MRO.ROI.Test.Utilities;
using OpenQA.Selenium.Remote;
using System;
using System.Threading;

namespace MRO.ROI.Test.RegressionTests.MROROITests
{
    [TestClass]
    public class ROIAdminPreCacheButtonClickSQlAndCachePurgeTest : ROIBaseTest
    {

        public ROIAdminPreCacheButtonClickSQlAndCachePurgeTest() : base(ROITestArea.ROIAdmin)
        {
        }
        [STATestMethodAttribute]
        [TestCategory(ROITestCategory.Passed)]
        //Converted manual test case 10790-ROI-Admin-->Precache button click SQL & Cache Purge - New process for empty folders to be automated
        public void Reg_10790_PreCacheButtonClickSQlAndCachePurge()
        {
            logger = extent.CreateTest("Reg_10790_PreCacheButtonClickSQlAndCachePurge");
            logger.Log(Status.Info, "Converted manual test case 10790-ROI-Admin-->Precache button click SQL & Cache Purge - New process for empty folders to be automated");
            RemoteWebDriver _driver = Init(TestContext.Properties["browser"].ToString());
            ThreadLocal<RemoteWebDriver> localDriver = new ThreadLocal<RemoteWebDriver>();
            localDriver.Value = _driver;
            RemoteWebDriver driver = localDriver.Value;

            try
            {
                ROIAdminHomePage rOIAdminHomePage = new ROIAdminHomePage(driver, logger, TestContext);
                ROIAdminFacilityListPage rOIAdminFacilityListPage = new ROIAdminFacilityListPage(driver, logger, TestContext);
                rOIAdminHomePage.SelectFacilityList();
                rOIAdminFacilityListPage.GoToRoiNativePdfTestFacility();
                ROIFacilityWorkSummaryPage rOIFacilityWorkSummaryPage = new ROIFacilityWorkSummaryPage(driver, logger, TestContext);
                ROIFacilityLogNewRequestPage rOIFacilityLogNewRequestPage = new ROIFacilityLogNewRequestPage(driver, logger, TestContext);
                rOIFacilityWorkSummaryPage.logaNewRequest();
                rOIFacilityLogNewRequestPage.CreateRoiNativePdfTestDeliveryRequestWithoutScan();
                ROIFacilityRequestStatusPage rOIFacilityRequestStatusPage = new ROIFacilityRequestStatusPage(driver, logger, TestContext);
                string requestid = rOIFacilityRequestStatusPage.GetRequestID();
                logger.Log(Status.Info, $"MRO request created with ({requestid})", TakeScreenShotAtStep());
                rOIAdminHomePage.ROIlookupByRequestId(requestid);               
                rOIFacilityRequestStatusPage.ImportPdfFiles();                
                bool statusUnderImportDocument = rOIFacilityRequestStatusPage.VerifyStatusUnderImportDocument();
                Assert.IsTrue(statusUnderImportDocument, "Failed to verify status under import document is uploaded");
                rOIFacilityRequestStatusPage.ReleaseRequest();
                logger.Log(Status.Info, "Request released");
                logger.Log(Status.Info, "Remaining steps (7-12) are related to shipment manager and needs to be executed manually");


                Cleanup(driver);


            }
            catch (Exception ex)
            {
                LogException(driver, logger, ex);
            }
        }
    }
}
