using AventStack.ExtentReports;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MRO.ROI.Automation.Common;
using MRO.ROI.Automation.Pages;
using MRO.ROI.Automation.Utility;
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
    public class ROIAdminImplementAutomationForSyncOffShoreELinkLoggingTest : ROIBaseTest
    {
        public ROIAdminImplementAutomationForSyncOffShoreELinkLoggingTest() : base(ROITestArea.ROIAdmin)
        {
        }
        [TestMethod]
        [TestCategory(ROITestCategory.Regression)]
        //Converted manual test case-ROI-Admin-->MyChart-Add pages to Delivered/Released OSD to automated.
        public void Reg_10404_Implement_Automation_ForSync_OffShore_ELinkLogging_Test()
        {
            logger = extent.CreateTest("Reg_10404_Implement_Automation_ForSync_OffShore_ELinkLogging_Test");
            logger.Log(Status.Info, "Converted manual test case-ROI-Admin-->Implement Automation for sync of Off-Shore E-Link Logging to automated.");
            RemoteWebDriver _driver = Init(TestContext.Properties["browser"].ToString());
            ThreadLocal<RemoteWebDriver> localDriver = new ThreadLocal<RemoteWebDriver>();
            localDriver.Value = _driver;
            RemoteWebDriver driver = localDriver.Value;
            string userRoot = System.Environment.GetEnvironmentVariable("USERPROFILE");
            string downloadFolder = Path.Combine(userRoot, "Downloads\\");

            try
            {
                ROIAdminHomePage rOIAdminHomePage = new ROIAdminHomePage(driver, logger, TestContext);
                Iframe frame = new Iframe(driver, logger, TestContext);
                ROIMenuSelector menu = new ROIMenuSelector(driver, logger, TestContext);
                menu.SelectRoiAdmin("Facilities", "Facility Features");
                frame.SwitchToRoiFrame();
                rOIAdminHomePage.CheckAutoMRBLookup();
                frame.switchToDefaut();
                rOIAdminHomePage.SelectFacilityList();

                ROIAdminFacilityListPage rOIAdminFacilityListPage = new ROIAdminFacilityListPage(driver, logger, TestContext);
                rOIAdminFacilityListPage.ClickOnROITFGearIcon();
                logger.Log(Status.Info, "clicked on ROIT facility gear Icon", TakeScreenShotAtStep());
                ROIFacilityFeaturesROITestFacilityPage rOIFacilityFeaturesROITestFacilityPage = new ROIFacilityFeaturesROITestFacilityPage(driver, logger, TestContext);
                frame.SwitchToRoiFrame();
                rOIFacilityFeaturesROITestFacilityPage.ClickOnROI();
                rOIFacilityFeaturesROITestFacilityPage.CheckHasMRNPatientLookUp();
                logger.Log(Status.Info, "Verified Has Auto MRN lookup , is enabled", TakeScreenShotAtStep());
                rOIFacilityFeaturesROITestFacilityPage.ClickUpdateFeatures();
                frame.switchToDefaut();
                rOIAdminHomePage.SelectFacilityList();
                rOIAdminFacilityListPage.GotoROITestFacilityComputerIcon();
                ROIFacilityWorkSummaryPage rOIFacilityWorkSummaryPage = new ROIFacilityWorkSummaryPage(driver, logger, TestContext);
                rOIFacilityWorkSummaryPage.logaNewRequest();
                frame.SwitchToRoiFrame();
                LogNewRequestPage logNewRequestPage = new LogNewRequestPage(driver, logger, TestContext);
                logNewRequestPage.CreateNewMRODeliveryRequestWithFNAndLN();

                logger.Log(Status.Info, "Verifying MRN automatically populated, MRN=F000623504");
                string mrnNumber = logNewRequestPage.GetMRNNumber();
                Assert.AreEqual(mrnNumber, "F000623504", $"Failed : MRN not automatically populated with MRN=F000623504");
                logger.Log(Status.Pass, "Verified MRN automatically populated, MRN=F000623504", TakeScreenShotAtStep());
                //
                string query = "update tblROIFacilities set bAutoMRNLookup = 0 where  nroiFacilityID = 1";
                bool boeReuqest = MRODBConnection.isUpdate(query);
                logger.Log(Status.Info, "Verified Has Auto MRN lookup , is disabled");


            }
            catch (Exception ex)
            {
                string query = "update tblROIFacilities set bAutoMRNLookup = 0 where  nroiFacilityID = 1";
                bool boeReuqest = MRODBConnection.isUpdate(query);
                logger.Log(Status.Info, "Verified Has Auto MRN lookup , is disabled");
                LogException(driver, logger, ex);
            }
        }
    }
}
