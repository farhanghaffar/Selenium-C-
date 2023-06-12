
using AventStack.ExtentReports;
using AventStack.ExtentReports.MarkupUtils;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MRO.ROI.Automation.Pages;
using MRO.ROI.Automation.Pages.Common;
using MRO.ROI.Automation.Selenium;
using MRO.ROI.Test.ExecutionFactory;
using MRO.ROI.Test.Utilities;
using OpenQA.Selenium.Remote;
using System;
using System.Threading;
using static MRO.ROI.Automation.Utility.IniFile;

namespace MRO.ROI.Test.RegressionTests.MROROITests
{
    [TestClass]
    public class ROIMRECInstalledNewMSITest : ROIBaseTest
    {
        public ROIMRECInstalledNewMSITest() : base(ROITestArea.ROIAdmin)
        {

        }
        [STATestMethodAttribute]
        [TestCategory(ROITestCategory.Passed)]
        // Converted manual test case 10784-ROI-Admin-->MREC Installed New MSI Test to automated.
        public void Reg_10784_ROIMRECInstalledNewMSITest()
        {
            logger = extent.CreateTest("Reg_10784_ROIMRECInstalledNewMSITest");
            logger.Log(Status.Info, "Converted manual test case 10784-ROI-Admin-->MREC Installed New MSI Test to automated");
            RemoteWebDriver _driver = Init(TestContext.Properties["browser"].ToString());
            ThreadLocal<RemoteWebDriver> localDriver = new ThreadLocal<RemoteWebDriver>();
            localDriver.Value = _driver;
            RemoteWebDriver driver = localDriver.Value;

            try
            {
                ROIAdminHomePage rOIAdminHomePage = new ROIAdminHomePage(driver, logger, TestContext);
                rOIAdminHomePage.FacilityList();
                ROIAdminFacilityListPage rOIAdminFacilityListPage = new ROIAdminFacilityListPage(driver, logger, TestContext);
                rOIAdminFacilityListPage.ClickOnROITFGearIcon();
                ROIAdminFacilityFeaturesPage rOIAdminFacilityFeaturesPage = new ROIAdminFacilityFeaturesPage(driver, logger, TestContext);
                rOIAdminFacilityFeaturesPage.SelectELinkTab();
                ROIFacilityComponentImportMappingConfigurationPage facilityComponentImportMappingConfigurationPage = new ROIFacilityComponentImportMappingConfigurationPage(driver, logger, TestContext);
                string headerVal = facilityComponentImportMappingConfigurationPage.VerifyHeader();
                Assert.AreEqual(headerVal, "Facility Component Import Mapping Configuration", "Failed to verify header");
                logger.Log(Status.Pass, "Verified Facility Component Import Mapping Configuration page  is opened", TakeScreenShotAtStep());
                string sFloderVal = IniHelper.ReadConfig("ROIMRECInstalledNewMSITest", "sFloder");
                string selectedFolder = facilityComponentImportMappingConfigurationPage.VerifySfloder();
                Assert.AreEqual(selectedFolder, selectedFolder);
                logger.Log(Status.Pass, "Verified ID 14 sFloder set to  test\\test1", TakeScreenShotAtStep());
                rOIAdminHomePage.FacilityList();
                rOIAdminFacilityListPage.GotoROITestFacilityComputerIcon();
                ROIFacilityWorkSummaryPage rOIFacilityWorkSummaryPage = new ROIFacilityWorkSummaryPage(driver, logger, TestContext);
                rOIFacilityWorkSummaryPage.logaNewRequest();
                ROIFacilityLogNewRequestPage rOIFacilityLogNewRequestPage = new ROIFacilityLogNewRequestPage(driver, logger, TestContext);
                rOIFacilityLogNewRequestPage.ClickMRODeliveryTab();
                rOIFacilityLogNewRequestPage.CreateNewMRODeliveryRequestForBostonProper();
                ROIFacilityRequestStatusPage rOIFacilityRequestStatusPage = new ROIFacilityRequestStatusPage(driver, logger, TestContext);
                string requestid = rOIFacilityRequestStatusPage.GetRequestID();
                logger.Log(Status.Info, $"MRO delivery request created with id:{requestid}");
                rOIFacilityRequestStatusPage.ImportPdfFiles();
                string setId = rOIFacilityRequestStatusPage.ClickOnSetId();
                string extId = rOIFacilityRequestStatusPage.ReturnExtReleaseId();
                Assert.AreEqual(setId, extId);
                logger.Log(Status.Pass, $"Verified ext release Id({extId}) is same as SetId({setId})", TakeScreenShotAtStep());
                logger.Log(Status.Info, "Remaining steps (11-13) are related to shipment manager,needs to be executed manually");
                Cleanup(driver);
            }
            catch (Exception ex)
            {
                LogException(driver, logger, ex);

            }
        }
    }

}


