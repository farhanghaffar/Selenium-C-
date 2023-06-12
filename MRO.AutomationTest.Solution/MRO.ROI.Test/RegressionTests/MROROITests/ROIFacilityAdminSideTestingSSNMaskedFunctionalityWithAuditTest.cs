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
    public class ROIFacilityAdminSideTestingSSNMaskedFunctionalityWithAuditTest : ROIBaseTest
    {
        public ROIFacilityAdminSideTestingSSNMaskedFunctionalityWithAuditTest() : base(ROITestArea.ROITestFacility)
        {
        }

        [STATestMethodAttribute]
        [TestCategory(ROITestCategory.Development)]        
        // Converted manual test case 1409-ROI-Admin-->Test Case 1409: Facility & Admin side testing SSN masked functionality with audit to automated.
        public void Reg_1409_ROIFacilityAdminSideTestingSSNMaskedFunctionalityWithAuditTest()
        {
            logger = extent.CreateTest("Reg_1409_ROIHideUserFlagsShowBoxesTableAtTheBottomwWillIncludeANewColumForBOETest");
            logger.Log(Status.Info, "Converted manual test case 1409-ROI-Admin-->Test Case 1409: Facility & Admin side testing SSN masked functionality with audit to automated.");
            RemoteWebDriver _driver = Init(TestContext.Properties["browser"].ToString());
            ThreadLocal<RemoteWebDriver> localDriver = new ThreadLocal<RemoteWebDriver>();
            localDriver.Value = _driver;
            RemoteWebDriver driver = localDriver.Value;


            try
            {
                ROIAdminHomePage rOIAdminHomePage = new ROIAdminHomePage(driver, logger, TestContext);
                //rOIAdminHomePage.SelectFacilityList();
                //ROIAdminFacilityListPage rOIAdminFacilityListPage = new ROIAdminFacilityListPage(driver, logger, TestContext);
                //rOIAdminFacilityListPage.ClickOnROITFacility();

                //ROIFacilityInfoROITestFacilityPage roIFacilityInfoROITestFacilityPage = new ROIFacilityInfoROITestFacilityPage(driver, logger, TestContext);
                //roIFacilityInfoROITestFacilityPage.ClickOnViewFeatures();
                //ROIFacilityFeaturesROITestFacilityPage rOIFacilityFeaturesROITestFacilityPage = new ROIFacilityFeaturesROITestFacilityPage(driver, logger, TestContext);
                //rOIFacilityFeaturesROITestFacilityPage.ClickOnROI();
                //logger.Log(Status.Info, "Navigated to ROI Tab", TakeScreenShotAtStep());

                //rOIFacilityFeaturesROITestFacilityPage.CheckHasSSN();
                //logger.Log(Status.Info, "Verified Has SSN is enabled", TakeScreenShotAtStep());
                //rOIFacilityFeaturesROITestFacilityPage.ClickUpdateFeatures();


                //rOIAdminHomePage.OpenNewTabAndLoginROITestFacility(BaseAddress);
                ROIFacilityWorkSummaryPage rOIFacilityWorkSummaryPage = new ROIFacilityWorkSummaryPage(driver, logger, TestContext);
                rOIFacilityWorkSummaryPage.logaNewRequest();
                ROIFacilityLogNewRequestPage rOIFacilityLogNewRequestPage = new ROIFacilityLogNewRequestPage(driver, logger, TestContext);
                rOIFacilityLogNewRequestPage.ClickMRODeliveryTab();
                rOIFacilityLogNewRequestPage.MRODeliveryRequestForBostonProperWithSSNNumber();
                ROIFacilityRequestStatusPage rOIFacilityRequestStatusPage = new ROIFacilityRequestStatusPage(driver, logger, TestContext);
                string requestID = rOIFacilityRequestStatusPage.GetRequestID();
                logger.Log(Status.Pass, $"Request created with requestid ({requestID})", TakeScreenShotAtStep());
                //ROIAdminHomePage rOIAdminHomePage = new ROIAdminHomePage(driver, logger, TestContext);
                rOIAdminHomePage.ROIlookupByRequestId(requestID);

                LogNewRequestPage logNewRequestPage = new LogNewRequestPage(driver, logger, TestContext);
                //string requestId = logNewRequestPage.getRequestid();
                //logger.Log(Status.Info, $"MRO delivery request created ({requestId})", TakeScreenShotAtStep());
               // rOIAdminHomePage.SearchByRequestId(requestId);
                //ROIFacilityRequestStatusPage rOIFacilityRequestStatusPage = new ROIFacilityRequestStatusPage(driver, logger, TestContext);
                rOIFacilityRequestStatusPage.ReOpenRequestID();
                bool isDisplayed = rOIFacilityRequestStatusPage.VerifySSNNumber();
                Assert.IsTrue(isDisplayed, "Not verified ssn number ");
                logger.Log(Status.Info, $"Succcessfully verified  user can see the last 4 digits of ssn nummber", TakeScreenShotAtStep());

                rOIFacilityRequestStatusPage.ClickOnMagnifierIconForSSN();
                logger.Log(Status.Pass, "Verified that pop up window displayed the full ssn number", TakeScreenShotAtStep());

                //rOIAdminHomePage.SwitchToPreviousTab(BaseAddress);
                //rOIAdminHomePage.ClickAuditLog();
                //rOIAdminHomePage.ClickAuditLog();
                //logger.Log(Status.Info, "Verified that audit log page opened successfully", TakeScreenShotAtStep());
                //ROIAdminAuditLogPage rOIAdminAuditLogPage = new ROIAdminAuditLogPage(driver, logger, TestContext);
                //rOIAdminAuditLogPage.SearchRequestOnAuditLog("ROI Test Facility", "Patient SSN Viewed");
                //bool isReqDisplayed = rOIAdminAuditLogPage.VerifyRequest(requestId);
                //Assert.IsFalse(isReqDisplayed, "Request id Present");
                rOIFacilityRequestStatusPage.ClickOnUpdateInfo();
                ROIAdminUpdatePatientInformationPage rOIAdminUpdatePatientInformationPage = new ROIAdminUpdatePatientInformationPage(driver, logger, TestContext);
                rOIAdminUpdatePatientInformationPage.EditSSNNumber();


                //Commented code related admin steps,once admin login working then  need to uncomment those lines
                Cleanup(driver);

            }

            catch (Exception ex)
            {
                LogException(driver, logger, ex);

            }
        }
    }

}

