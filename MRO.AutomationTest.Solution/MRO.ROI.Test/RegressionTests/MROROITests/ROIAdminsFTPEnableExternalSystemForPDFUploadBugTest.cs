using AventStack.ExtentReports;
using AventStack.ExtentReports.MarkupUtils;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MRO.ROI.Automation.Pages;
using MRO.ROI.Automation.Selenium;
using MRO.ROI.Test.ExecutionFactory;
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
    public class ROIAdminsFTPEnableExternalSystemForPDFUploadBugTest : ROIBaseTest
    {
        public ROIAdminsFTPEnableExternalSystemForPDFUploadBugTest() : base(ROITestArea.ROIAdmin)
        {
        }

        [STATestMethodAttribute]
        [TestCategory(ROITestCategory.Passed)]
        // Converted manual test case 11257-ROI-Admin-->sFTP - Enable External System for PDF Upload Bug to automated
        public void Reg_11257_ROIAdminsFTPEnableExternalSystem()
        {
            logger = extent.CreateTest("Reg_11257_ROIAdminsFTP-Enable External System for PDF Upload Bug");
            logger.Log(Status.Info, "Converted manual test case 11257-ROI-Admin-->sFTP - Enable External System for PDF Upload Bug to automated");
            RemoteWebDriver _driver = Init(TestContext.Properties["browser"].ToString());
            ThreadLocal<RemoteWebDriver> localDriver = new ThreadLocal<RemoteWebDriver>();
            localDriver.Value = _driver;
            RemoteWebDriver driver = localDriver.Value;

            try
            {
                ROIAdminHomePage rOIAdminHomePage = new ROIAdminHomePage(driver, logger, TestContext);
                rOIAdminHomePage.ClickFacilityList();
                ROIAdminFacilityListPage rOIAdminFacilityListPage = new ROIAdminFacilityListPage(driver, logger, TestContext);
                rOIAdminFacilityListPage.ClickOnRenownHealth();
                ROIAdminFacilityFeaturesRenownHealthPage rOIAdminFacilityFeaturesRenownHealthPage = new ROIAdminFacilityFeaturesRenownHealthPage(driver, logger, TestContext);
                rOIAdminFacilityFeaturesRenownHealthPage.ClickFTPUpload();
                string currentFeatureId = rOIAdminFacilityFeaturesRenownHealthPage.GetCurrentFeatureId();
                string featureIdAfterAdd = rOIAdminFacilityFeaturesRenownHealthPage.GetFeatureIdAfterAdd();
                logger.Log(Status.Info, $"New FTP feature id created ({featureIdAfterAdd})", TakeScreenShotAtStep());
                Assert.AreNotEqual(currentFeatureId, featureIdAfterAdd);
                logger.Log(Status.Pass, $"Verified feature id ({featureIdAfterAdd})", TakeScreenShotAtStep());
                rOIAdminHomePage.ClickFacilityList();
                rOIAdminFacilityListPage.ClickOnRenownHealth();
                rOIAdminFacilityFeaturesRenownHealthPage.ClickFTPUpload();
                string currentFeatureIdAfterRepeat = rOIAdminFacilityFeaturesRenownHealthPage.GetCurrentFeatureId();
                string featureIdAfterAddAfterRepeat = rOIAdminFacilityFeaturesRenownHealthPage.GetFeatureIdAfterAdd();
                logger.Log(Status.Info, $"Created another feature id ({featureIdAfterAddAfterRepeat})", TakeScreenShotAtStep());
                Assert.AreNotEqual(currentFeatureId, featureIdAfterAdd);
                logger.Log(Status.Pass, $"Verified feature id ({featureIdAfterAddAfterRepeat})", TakeScreenShotAtStep());

                Cleanup(driver);
            }
            catch (Exception ex)
            {
                LogException(driver, logger, ex);
            }
        }
    }
}
