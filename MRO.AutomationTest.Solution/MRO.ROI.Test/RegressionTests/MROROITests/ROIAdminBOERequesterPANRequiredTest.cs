using AventStack.ExtentReports;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MRO.ROI.Automation.Pages;
using MRO.ROI.Test.ExecutionFactory;
using MRO.ROI.Test.Utilities;
using OpenQA.Selenium.Remote;
using System;
using System.Threading;
using static MRO.ROI.Automation.Utility.IniFile;

namespace MRO.ROI.Test.RegressionTests.MROROITests
{
    [TestClass]
    public class ROIAdminBOERequesterPANRequiredTest : ROIBaseTest
    {
        public static string csvFileName = "ROIAdminBOERequesterPANRequiredTest.csv";
        public ROIAdminBOERequesterPANRequiredTest() : base(ROITestArea.ROIAdminBOE)
        {
        }
        [STATestMethodAttribute]
        [TestCategory(ROITestCategory.Regression)]
        //Converted manual test case 7000-ROI-Admin-->BOE Requester PAN Required to be automated
        public void Reg_7000_BOERequesterPANRequired()
        {
            logger = extent.CreateTest("Reg_7000_BOERequesterPANRequired");
            logger.Log(Status.Info, "Converted manual test case 7000-ROI-Admin-->BOE Requester PAN Required to be automated");
            RemoteWebDriver _driver = Init(TestContext.Properties["browser"].ToString());
            ThreadLocal<RemoteWebDriver> localDriver = new ThreadLocal<RemoteWebDriver>();
            localDriver.Value = _driver;
            RemoteWebDriver driver = localDriver.Value;

            try
            {
                string LookUpRequestID = IniHelper.ReadConfig("ROIAdminBOERequesterPANRequiredTest", "RequestID");
                ROIAdminHomePage rOIAdminHomePage = new ROIAdminHomePage(driver, logger, TestContext);
                rOIAdminHomePage.ROIlookupByRequestId(LookUpRequestID);
                ROIAdminRequestStatusPage rOIAdminRequestStatusPage = new ROIAdminRequestStatusPage(driver, logger, TestContext);
                bool _requestStatusPageHeader = rOIAdminRequestStatusPage.VerifyRequestStatusPage();
                Assert.IsTrue(_requestStatusPageHeader, "Failed to open request status page.");
                rOIAdminRequestStatusPage.ClickOnAetnaTestFax();
                ROIAdminEditRequesterInfoPage rOIAdminEditRequesterInfoPage = new ROIAdminEditRequesterInfoPage(driver, logger, TestContext);
                bool _editRequesterInfoPageHeader = rOIAdminEditRequesterInfoPage.VerifyEditRequesterInfoPage();
                Assert.IsTrue(_editRequesterInfoPageHeader, "Failed to verify edit requester info page");
                bool _updateInternalPortalSettingsPage = rOIAdminEditRequesterInfoPage.VerifyAndUpdateInternalPortalSettingsBOEChecked();
                Assert.IsTrue(_updateInternalPortalSettingsPage, "Failed to update edit requester info");
                rOIAdminHomePage.SwitchToNewTabAndLoginROIInternalFacilityBOE(BaseAddress);
                ROIFacilityLogNewRequestPage rOIFacilityLogNewRequestPage = new ROIFacilityLogNewRequestPage(driver, logger, TestContext);
                rOIFacilityLogNewRequestPage.CreateNewInternalPortalRequest();
                ROIFacilityRequestStatusPage rOIFacilityRequestStatusPage = new ROIFacilityRequestStatusPage(driver, logger, TestContext);
                string requestid = rOIFacilityRequestStatusPage.GetRequestID();
                logger.Log(Status.Info, $"ROI request created with id ({requestid})", TakeScreenShotAtStep());
                rOIAdminHomePage.ROIlookupByRequestId(requestid);
                rOIFacilityRequestStatusPage.ImportRequestPages();
                rOIAdminHomePage.SwitchToPreviousTabBOE(BaseAddress);
                rOIAdminHomePage.ROIlookupByRequestId(requestid);
                bool _panNumberOnRSS = rOIAdminRequestStatusPage.VerifyPanOnRSS();
                Assert.IsTrue(_panNumberOnRSS, "Failed to verify pan number on the RSS page");
                rOIAdminRequestStatusPage.ClickOnReAssignROIRequester();
                string _PanNumber = IniHelper.ReadConfig("ROIAdminBOERequesterPANRequiredTest", "PanNumber");
                int _internalPortalPanNumber = Convert.ToInt32(_PanNumber);
                int _panNumberOnAssignROIRequester = rOIAdminRequestStatusPage.GetPanNumberOnAssignROIRequester();
                Assert.AreEqual(_internalPortalPanNumber, _panNumberOnAssignROIRequester, "Failed to verify PAN number that entered from the internal portal is not visible");
                logger.Log(Status.Pass, $"Verified PAN number that entered from the internal portal is ({_internalPortalPanNumber}) same as pan number visible on assign roi requester page ({_panNumberOnAssignROIRequester}).", TakeScreenShotAtStep());
                ROIAdminAssignROIRequesterPage rOIAdminAssignROIRequesterPage = new ROIAdminAssignROIRequesterPage(driver, logger, TestContext);
                rOIAdminAssignROIRequesterPage.ClickOnShipTo();
                bool _assignRoiRequesterPage = rOIAdminAssignROIRequesterPage.VerifyAssignROIRequesterpage();
                Assert.IsTrue(_assignRoiRequesterPage, "Failed to verify assign roi requester page opened");
                rOIAdminAssignROIRequesterPage.ClickOnCancelTwice();
                bool _requestStatusPage = rOIAdminRequestStatusPage.VerifyRequestStatusPage();
                Assert.IsTrue(_requestStatusPage, "Failed to open request status page.");
                rOIAdminRequestStatusPage.ClickOnUpdateInfo();
                ROIAdminUpdatePatientInformationPage rOIAdminUpdatePatientInformationPage = new ROIAdminUpdatePatientInformationPage(driver, logger, TestContext);
                bool _updatePatientInfoPage = rOIAdminUpdatePatientInformationPage.VerifyUpdatePatientInformationPage();
                Assert.IsTrue(_assignRoiRequesterPage, "Failed to open update patient information page");
                int _panNumberOnUpdatePatientInfo = rOIAdminUpdatePatientInformationPage.GetPanNumberOnUpdatePatientInfo();
                Assert.AreEqual(_internalPortalPanNumber, _panNumberOnUpdatePatientInfo, "Failed to verify PAN number that entered from the internal portal is not visible");
                rOIAdminUpdatePatientInformationPage.UpdatePanNumber();
                string _updatePanNumber = IniHelper.ReadConfig("ROIAdminBOERequesterPANRequiredTest", "UpdatePanNumber");
                int _updatedPanNumber = Convert.ToInt32(_updatePanNumber);
                int _updatedPanNumberOnRSS = rOIAdminRequestStatusPage.GetUpdatedPanNumberOnRSS();
                Assert.AreEqual(_updatedPanNumber, _updatedPanNumberOnRSS, "Failed to verify updated PAN number is not visbile on request status page");
                logger.Log(Status.Pass, $"Verified updated PAN number is ({_updatedPanNumber}) same as pan number visible on request status page ({_updatedPanNumberOnRSS}).", TakeScreenShotAtStep());
                rOIAdminRequestStatusPage.ClickOnAetnaTestFax();
                bool _updateBOEREquesterInternalPortalSettingsPage = rOIAdminEditRequesterInfoPage.VerifyAndUpdateInternalPortalSettingsBOEUnchecked();
                Assert.IsTrue(_updateBOEREquesterInternalPortalSettingsPage, "Failed to update edit requester info");
                rOIAdminHomePage.SwitchToNewTabROIInternalFacilityBOE(BaseAddress);
                rOIFacilityLogNewRequestPage.CreateNewInternalPortalRequest();
                string _requestid = rOIFacilityRequestStatusPage.GetRequestID();
                logger.Log(Status.Info, $"ROI request created with id ({_requestid})", TakeScreenShotAtStep());
                rOIAdminHomePage.ROIlookupByRequestId(_requestid);
                rOIFacilityRequestStatusPage.ImportRequestPages();
                Cleanup(driver);

            }
            catch (Exception ex)
            {
                LogException(driver, logger, ex);
            }
        }

    }
}
