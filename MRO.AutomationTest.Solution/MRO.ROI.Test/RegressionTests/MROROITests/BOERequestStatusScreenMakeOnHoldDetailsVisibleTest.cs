using AventStack.ExtentReports;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MRO.ROI.Automation.Pages;
using MRO.ROI.Test.Utilities;
using OpenQA.Selenium.Remote;
using System;
using System.Threading;
using MRO.ROI.Test.ExecutionFactory;
using MRO.ROI.Automation.Common;

namespace MRO.ROI.Test.RegressionTests.MROROITests
{
    [TestClass]
    public class BOERequestStatusScreenMakeOnHoldDetailsVisibleTest : ROIBaseTest
    {
        public BOERequestStatusScreenMakeOnHoldDetailsVisibleTest() : base(ROITestArea.ROIAdmin)
        {

        }
        [STATestMethodAttribute]
        [TestCategory(ROITestCategory.Regression)]
        //Converted manual test case 12126-ROI-Admin-->BOE request status screen – make On-Hold details visible to automated
        public void Reg_12126_BOERequestStatusScreenMakeOnHoldDetailsVisibleTest()
        {
            logger = extent.CreateTest("Reg_12126_BOERequestStatusScreenMakeOnHoldDetailsVisibleTest");
            logger.Log(Status.Info, "Converted manual test case 12126-ROI-Admin-->BOE request status screen – make On-Hold details visible to automated");
            RemoteWebDriver _driver = Init(TestContext.Properties["browser"].ToString());
            ThreadLocal<RemoteWebDriver> localDriver = new ThreadLocal<RemoteWebDriver>();
            localDriver.Value = _driver;
            RemoteWebDriver driver = localDriver.Value;
            try
            {
                ROIAdminHomePage rOIAdminHomePage = new ROIAdminHomePage(driver, logger, TestContext);
                rOIAdminHomePage.SelectFacilityList();
                Iframe frame = new Iframe(driver, logger, TestContext);

                ROIAdminFacilityListPage rOIAdminFacilityListPage = new ROIAdminFacilityListPage(driver, logger, TestContext);
                rOIAdminFacilityListPage.GoToROITestFacility();
                ROIFacilityWorkSummaryPage rOIFacilityWorkSummaryPage = new ROIFacilityWorkSummaryPage(driver, logger, TestContext);
                rOIFacilityWorkSummaryPage.logaNewRequest();
                ROIFacilityLogNewRequestPage rOIFacilityLogNewRequestPage = new ROIFacilityLogNewRequestPage(driver, logger, TestContext);
                frame.SwitchToRoiFrame();
                rOIFacilityLogNewRequestPage.CreateInternalPortalRequest();
                ROIFacilityRequestStatusPage rOIFacilityRequestStatusPage = new ROIFacilityRequestStatusPage(driver, logger, TestContext);
                string requestid = rOIFacilityRequestStatusPage.GetRequestID();
                logger.Log(Status.Info, $"Request id generated- {requestid}", TakeScreenShotAtStep());
                rOIAdminHomePage.ROIlookupByRequestId(requestid);
                frame.SwitchToRoiFrame();
                rOIFacilityRequestStatusPage.ImportPdfFiles();

                rOIFacilityRequestStatusPage.ClickOnHoldCheckbox();                
                rOIAdminHomePage.SwitchToNewTabAndLoginROIInternalFacility(BaseAddress);
                rOIAdminHomePage.ROIlookupByRequestId(requestid);
                //
                //
                //
                //
                frame.SwitchToRoiFrame();
               bool _onHoldReason = rOIFacilityRequestStatusPage.VerifyOnHoldReason();
               Assert.IsTrue(_onHoldReason, "Failed to verify the Hold reason in the top left of the page is red and has the Correct on hold reason");
               logger.Log(Status.Pass, "Successfully verified the Hold reason in the top left of the page is red and has the Correct on hold reason", TakeScreenShotAtStep());

                rOIAdminHomePage.SwitchToPreviousTab(BaseAddress);
                frame.switchToDefaut();
                ROIMenuSelector menu = new ROIMenuSelector(driver, logger, TestContext);
                menu.SelectRoiAdmin("Reports", "Pending");
                frame.SwitchToRoiFrame();
                ROIAdminPendingReportPage rOIAdminPendingReportPage = new ROIAdminPendingReportPage(driver, logger, TestContext);

                rOIAdminPendingReportPage.CreatePendingReport();

                rOIAdminPendingReportPage.SelectTopHundredRecords();
                bool requestIdAtPendingReport = rOIAdminPendingReportPage.VerifyRequestAtPendingReport(requestid);
                Assert.AreEqual(true, requestIdAtPendingReport,"Failed to verify requestid appears at pending report page.");
                logger.Log(Status.Pass, "Successfully verified the requestid that appeared on the pending report page", TakeScreenShotAtStep());

               // Cleanup(driver);
            }
            catch (Exception ex)
            {
                LogException(driver, logger, ex);
            }
        }
    }
}
