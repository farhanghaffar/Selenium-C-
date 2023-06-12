using AventStack.ExtentReports;
using AventStack.ExtentReports.MarkupUtils;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MRO.ROI.Automation.Pages;
using MRO.ROI.Automation.Pages.Common;
using MRO.ROI.Automation.Selenium;
using MRO.ROI.Test.Utilities;
using OpenQA.Selenium.Remote;
using System;
using System.Threading;

namespace MRO.ROI.Test.RegressionTests.MROROITests
{
    [TestClass]
    public class ROIAdminTPAActivityReportTest : ROIBaseTest
    {

        public ROIAdminTPAActivityReportTest() : base(ROITestArea.ROIAdmin)
        {
        }
        [TestMethod]
        [TestCategory(ROITestCategory.Passed)]
        //Converted manual test case 11726-ROI-Admin-->QC Assist Enhancement - TPA Report Enhancement to be automated
        public void Reg_11726_QCAssistEnhancementTPAReportEnhancement()
        {
            logger = extent.CreateTest("Reg_11726_QCAssistEnhancementTPAReportEnhancement");
            logger.Log(Status.Info, "Converted manual test case 11726-ROI-Admin-->QC Assist Enhancement - TPA Report Enhancement to be automated");
            RemoteWebDriver _driver = Init(TestContext.Properties["browser"].ToString());
            ThreadLocal<RemoteWebDriver> localDriver = new ThreadLocal<RemoteWebDriver>();
            localDriver.Value = _driver;
            RemoteWebDriver driver = localDriver.Value;
            try
            {

                ROIAdminHomePage rOIAdminHomePage = new ROIAdminHomePage(driver, logger, TestContext);
                rOIAdminHomePage.GoToTPAActivityReport();
                ROIAdminTPAActivityReportPage rOIAdminTPAActivityReportPage = new ROIAdminTPAActivityReportPage(driver, logger, TestContext);
                rOIAdminTPAActivityReportPage.CreateReport();
                rOIAdminTPAActivityReportPage.SelectFacilityList();
                ROIAdminFacilityListPage rOIAdminFacilityListPage = new ROIAdminFacilityListPage(driver, logger, TestContext);
                rOIAdminFacilityListPage.GoToROITestFacility();
                ROIFacilityWorkSummaryPage rOIFacilityWorkSummaryPage = new ROIFacilityWorkSummaryPage(driver, logger, TestContext);
                rOIFacilityWorkSummaryPage.logaNewRequest();
                ROIFacilityLogNewRequestPage rOIFacilityLogNewRequestPage = new ROIFacilityLogNewRequestPage(driver, logger, TestContext);
                rOIFacilityLogNewRequestPage.CreateNewMRODeliveryRequestForBostonProper();
                ROIFacilityRequestStatusPage rOIFacilityRequestStatusPage = new ROIFacilityRequestStatusPage(driver, logger, TestContext);
                string requestID = rOIFacilityRequestStatusPage.GetRequestID();
                logger.Log(Status.Pass, $"Request created with requestid ({requestID})", TakeScreenShotAtStep());
                rOIAdminHomePage.ROIlookupByRequestId(requestID);
                rOIFacilityRequestStatusPage.ImportPdfFiles();
                rOIFacilityRequestStatusPage.ReleaseRequestID();
                logger.Log(Status.Info, "Request released");
                logger.Log(Status.Info, "Remaining steps (8-12) are related to heat grid and needs to be executed manually");
                Cleanup(driver);
            }
            catch (Exception ex)
            {
                LogException(driver, logger, ex);
            }
        }
    }
}
