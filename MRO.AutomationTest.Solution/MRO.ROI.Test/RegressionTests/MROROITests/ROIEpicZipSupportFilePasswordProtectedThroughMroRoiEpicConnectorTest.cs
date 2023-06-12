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
    public class ROIEpicZipSupportFilePasswordProtectedThroughMroRoiEpicConnectorTest : ROIBaseTest
    {
        public ROIEpicZipSupportFilePasswordProtectedThroughMroRoiEpicConnectorTest() : base(ROITestArea.ROIAdmin)
        {
        }
        [STATestMethodAttribute]
        [TestCategory(ROITestCategory.Passed)]
        // Converted manual test case 8288-EPIC ZIP Support File, password protected testing through MRO ROI Epic Connector tool to automated.
        public void Reg_8288_ROIEpicZipSupportFilePasswordProtectedThroughMroRoiEpicConnectorTest()
        {
            logger = extent.CreateTest("Reg_8288_ROIEpicZipSupportFilePasswordProtectedThroughMroRoiEpicConnectorTest");
            logger.Log(Status.Info, "Converted manual test case 8288-EPIC ZIP Support File, password protected testing through MRO ROI Epic Connector tool to automated.");
            RemoteWebDriver _driver = Init(TestContext.Properties["browser"].ToString());
            ThreadLocal<RemoteWebDriver> localDriver = new ThreadLocal<RemoteWebDriver>();
            localDriver.Value = _driver;
            RemoteWebDriver driver = localDriver.Value;

            try
            {

                ROIAdminHomePage rOIAdminHomePage = new ROIAdminHomePage(driver, logger, TestContext);
                ROIAdminFacilityListPage rOIAdminFacilityListPage = new ROIAdminFacilityListPage(driver, logger, TestContext);                
                rOIAdminHomePage.SelectFacilityList();                
                rOIAdminFacilityListPage.ClickOnGearIconROINativePdfTestFacility();                
                ROIAdminFacilityFeaturesPage rOIAdminFacilityFeaturesPage = new ROIAdminFacilityFeaturesPage(driver, logger, TestContext);
                rOIAdminFacilityFeaturesPage.ClickOnElinkAndUpdateFeatures();                
                rOIAdminHomePage.SelectFacilityList();
                rOIAdminFacilityListPage.GoToRoiNativePdfTestFacility();                
                ROIFacilityWorkSummaryPage rOIFacilityWorkSummaryPage = new ROIFacilityWorkSummaryPage(driver, logger, TestContext);
                rOIFacilityWorkSummaryPage.logaNewRequest();               
                ROIFacilityLogNewRequestPage rOIFacilityLogNewRequestPage = new ROIFacilityLogNewRequestPage(driver, logger, TestContext);
                rOIFacilityLogNewRequestPage.CreateRoiNativePdfTestDeliveryRequestWithoutScanForParticularDOB();
                ROIFacilityRequestStatusPage rOIFacilityRequestStatusPage = new ROIFacilityRequestStatusPage(driver, logger, TestContext);
                string requestid = rOIFacilityRequestStatusPage.GetRequestID();
                logger.Log(Status.Info, $"Request id generated- {requestid}" , TakeScreenShotAtStep());
                rOIAdminHomePage.ROIlookupByRequestId(requestid);                              
                rOIFacilityRequestStatusPage.ImportPdfFiles();
                rOIAdminHomePage.ROIlookupByRequestId(requestid);                
                int epicRoiId = rOIFacilityRequestStatusPage.SetEpicRoiIdAndClickOnSaveDeliveryDate();
                rOIFacilityRequestStatusPage.epicRoiIDLookup(epicRoiId);
                logger.Log(Status.Info, $"epic roi id provided- {epicRoiId}" , TakeScreenShotAtStep());
                logger.Log(Status.Info, "Remaining steps (13-22) are related to shipment manager and needs to be executed manually");
                Cleanup(driver);
            }
            catch (Exception ex)
            {
                LogException(driver, logger, ex);
            }
        }
    }
}
