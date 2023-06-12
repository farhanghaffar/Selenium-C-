using AventStack.ExtentReports;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MRO.ROI.Automation.Common;
using MRO.ROI.Automation.Pages;
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
    public class ROIAdminMyChartAddpagesToDeliveredReleasedOSDTest : ROIBaseTest
    {
        public ROIAdminMyChartAddpagesToDeliveredReleasedOSDTest() : base(ROITestArea.ROIAdmin)
        {
        }
        [TestMethod]
        [TestCategory(ROITestCategory.Passed)]
        //Converted manual test case-ROI-Admin-->MyChart-Add pages to Delivered/Released OSD to automated.
        public void Reg_12722_MyChart_Add_pages_Delivered_Released_OSD()
        {
            logger = extent.CreateTest("Reg_12722_MyChart_Add_pages_Delivered_Released_OSD");
            logger.Log(Status.Info, "Converted manual test case-ROI-Admin-->MyChart-Add pages to Delivered/Released OSD to automated.");
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
                rOIAdminHomePage.SelectFacilityList();
                ROIAdminFacilityListPage rOIAdminFacilityListPage = new ROIAdminFacilityListPage(driver, logger, TestContext);
                frame.SwitchToRoiFrame();
                rOIAdminFacilityListPage.ClickOnROITFacility();
                logger.Log(Status.Info, "clicked on ROIT facility", TakeScreenShotAtStep());
                ROIFacilityInfoROITestFacilityPage rOIFacilityInfoROITestFacilityPage = new ROIFacilityInfoROITestFacilityPage(driver, logger, TestContext);
                rOIFacilityInfoROITestFacilityPage.ClickOnViewFeatures();
                ROIFacilityFeaturesROITestFacilityPage rOIFacilityFeaturesROITestFacilityPage = new ROIFacilityFeaturesROITestFacilityPage(driver, logger, TestContext);
                rOIFacilityFeaturesROITestFacilityPage.ClickOnELink();
                rOIFacilityFeaturesROITestFacilityPage.UnCheckAddPDFPagesReleased();
                logger.Log(Status.Pass, "Verified 'add PDF pages released' is disabled", TakeScreenShotAtStep());
                rOIFacilityFeaturesROITestFacilityPage.ClickUpdateFeatures();
                frame.switchToDefaut();
                rOIAdminHomePage.SelectFacilityList();
                rOIAdminFacilityListPage.GotoROITestFacilityComputerIcon();
                ROIFacilityWorkSummaryPage rOIFacilityWorkSummaryPage = new ROIFacilityWorkSummaryPage(driver, logger, TestContext);
                rOIFacilityWorkSummaryPage.logaNewRequest();
                ROIFacilityLogNewRequestPage rOIFacilityLogNewRequestPage = new ROIFacilityLogNewRequestPage(driver, logger, TestContext);
                frame.SwitchToRoiFrame();
                rOIFacilityLogNewRequestPage.CreateNewMRODeliveryRequestWithoutScan();
                ROIFacilityRequestStatusPage rOIFacilityRequestStatusPage = new ROIFacilityRequestStatusPage(driver, logger, TestContext);
              //  frame.SwitchToRoiFrame();
                string requestid = rOIFacilityRequestStatusPage.GetRequestID();
                logger.Log(Status.Info, "Import PDF Files");
                rOIFacilityRequestStatusPage.ImportPdfFiles();
                logger.Log(Status.Pass, "Verify status under import document is uploaded", TakeScreenShotAtStep());
                bool statusUnderImportDocument = rOIFacilityRequestStatusPage.VerifyStatusUnderImportDocument();
                Assert.IsTrue(statusUnderImportDocument, "Failed to verify status under import document is uploaded");
                logger.Log(Status.Pass, "Verified status under import document is uploaded");
                logger.Log(Status.Info, "Release request");
                rOIFacilityRequestStatusPage.ReleaseRequestID();

                logger.Log(Status.Info, "Import PDF Files again");
                rOIFacilityRequestStatusPage.ImportPdfFiles("5.pdf", "6.pdf");

                logger.Log(Status.Pass, "Verify status under import document is uploaded for newly uploaded file", TakeScreenShotAtStep());
                bool statusUnderImportDocument1 = rOIFacilityRequestStatusPage.VerifyStatusUnderImportDocument(3);
                Assert.IsTrue(statusUnderImportDocument1, "Failed to verify status under import document is uploaded for newly uploaded file");

                Cleanup(driver);
            }

            catch (Exception ex)
            {
                //throw new Exception(ex.Message);  
                    LogException(driver, logger, ex);
            }
        }
    }
}
