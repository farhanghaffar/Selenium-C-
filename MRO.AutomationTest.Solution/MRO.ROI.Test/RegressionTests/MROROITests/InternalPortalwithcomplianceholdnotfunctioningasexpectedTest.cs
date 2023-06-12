using AventStack.ExtentReports;
using AventStack.ExtentReports.MarkupUtils;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MRO.ROI.Automation.Pages;
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
    public class InternalPortalwithcomplianceholdnotfunctioningasexpectedTest : ROIBaseTest
    {
        public InternalPortalwithcomplianceholdnotfunctioningasexpectedTest() : base(ROITestArea.ROIAdmin)
        {
        }

        [STATestMethodAttribute]
        [TestCategory(ROITestCategory.Regression)]
        //Converted manual test case 10805- ROI Admin-->Internal Portal with compliance hold not functioning as expected Test to automated
        public void Reg_10805_InternalPortalWithComplianceHoldTest()
        {
            logger = extent.CreateTest("Reg_10805_InternalPortalWithComplianceHoldTest");
            logger.Log(Status.Info, "Converted manual test case 10805- ROI Admin-->Internal Portal with compliance hold not functioning as expected Test to automated");
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
                rOIAdminFacilityFeaturesPage.SelectROITab();

                rOIAdminHomePage.FacilityList();
                rOIAdminFacilityListPage.GotoROITestFacilityComputerIcon();
                ROIFacilityWorkSummaryPage rOIFacilityWorkSummaryPage = new ROIFacilityWorkSummaryPage(driver, logger, TestContext);
                rOIFacilityWorkSummaryPage.GoToLogNewRequestPage();
                ROIFacilityLogNewRequestPage rOIFacilityLogNewRequestPage = new ROIFacilityLogNewRequestPage(driver, logger, TestContext);
                rOIFacilityLogNewRequestPage.ClickOnInternalPortalTab();
                rOIFacilityLogNewRequestPage.CreateNewInternalPortalTabLognewRequest();

                LogNewRequestPage logNewRequestPage = new LogNewRequestPage(driver, logger, TestContext);
                string requestId = logNewRequestPage.getRequestid();
                ROIFacilityRequestStatusPage rOIFacilityRequestStatusPage = new ROIFacilityRequestStatusPage(driver, logger, TestContext);
                rOIAdminHomePage.ROIlookupByRequestId(requestId);
                logger.Log(Status.Info, $"Internal portal request created successfully and requestid is:({requestId})", TakeScreenShotAtStep());

                rOIFacilityRequestStatusPage.ImportPdfFilesForInternalRequest();
                string requestStatus = IniHelper.ReadConfig("InternalPortalWithComplianceHoldTest", "RequestStatus");
                string _requestStatus = rOIFacilityRequestStatusPage.VerifyRequestStatus();
                Assert.AreEqual(requestStatus, _requestStatus, "Failed to verify request status");
                logger.Log(Status.Info, "Verified request status set to Records not released by Facility", TakeScreenShotAtStep());

                rOIFacilityLogNewRequestPage.ClickOnComplianceReview();
                ROIFacilityComplianceReviewPage rOIFacilityComplianceReviewPage = new ROIFacilityComplianceReviewPage(driver, logger, TestContext);
                rOIFacilityComplianceReviewPage.CreateReportForComplianceReview();
                string reqIdForCreateReport = rOIFacilityComplianceReviewPage.GetRequestIdFromReport();
                Assert.AreEqual(requestId, reqIdForCreateReport);
                string status = rOIFacilityComplianceReviewPage.VerifyStatus();
                Assert.AreEqual(status, "Not Released", "Failed to verify status for request");
                logger.Log(Status.Pass, "Verified status value is set to Not Released", TakeScreenShotAtStep());
                rOIAdminHomePage.ROIlookupByRequestId(requestId);
                string afterReleaseReqStatus = IniHelper.ReadConfig("InternalPortalWithComplianceHoldTest", "AfterReleaseRequestStatus");
                string _afterReleaseReqStatus = rOIFacilityRequestStatusPage.ReleaseRequestForInternalRequest();
                Assert.AreEqual(afterReleaseReqStatus, _afterReleaseReqStatus, "Failed to verify request status after release");
                logger.Log(Status.Pass, "Verified request status set to Documents sent to Requester", TakeScreenShotAtStep());

                rOIFacilityLogNewRequestPage.ClickOnComplianceReview();
                rOIFacilityComplianceReviewPage.CreateReportForComplianceReview();
                bool searchResult = rOIFacilityComplianceReviewPage.VerifyPatient(requestId);
                Assert.IsFalse(searchResult, "Failed to verify created request id");
                logger.Log(Status.Pass, "Verified patient record does not show in the report");

                string statusVal = rOIFacilityComplianceReviewPage.ClickOnDeliveredCheckbox();
                Assert.AreEqual(statusVal, "Delivered", "Failed to verify request status");
                logger.Log(Status.Pass, "Verified status value is set to Delivered", TakeScreenShotAtStep());

                rOIAdminHomePage.SwitchToNewTabAndLoginROIInternalFacility(BaseAddress);
                rOIAdminHomePage.ROIlookupByRequestId(requestId);
                ROIFacilityRequestStatusPage rOIFacilityRequestStatus = new ROIFacilityRequestStatusPage(driver, logger, TestContext);
                bool isDisabled = rOIFacilityRequestStatus.CheckViewDocumentsDisabled();
                Assert.IsTrue(isDisabled, "View documents button is enabled");
                logger.Log(Status.Pass, "Verified view documents button is disabled", TakeScreenShotAtStep());

                rOIAdminHomePage.SwitchToPreviousTab(BaseAddress);
                rOIAdminHomePage.FacilityList();
                rOIAdminFacilityListPage.GotoROITestFacilityComputerIcon();
                rOIAdminHomePage.ROIlookupByRequestId(requestId);
                rOIFacilityRequestStatusPage.UncheckComplianceHoldCheckbox();

                rOIAdminHomePage.SwitchToNewTabROIInternalFacility(BaseAddress);
                rOIAdminHomePage.ROIlookupByRequestId(requestId);
                bool isDisplayed = rOIFacilityRequestStatusPage.ClickViewDocumentsAndReturnIfCopiedpatientDocumentsDisplayed();
                Assert.IsTrue(isDisplayed, "View documents button is disabled");
                logger.Log(Status.Pass, "Verified View documents is enabled", TakeScreenShotAtStep());

                Cleanup(driver);
            }
            catch (Exception ex)
            {
                LogException(driver, logger, ex);

            }
        }
    }

}

