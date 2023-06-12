using AventStack.ExtentReports;
using AventStack.ExtentReports.MarkupUtils;
using DataDrivenProject;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MRO.ROI.Automation.Pages;
using MRO.ROI.Automation.Pages.Common;
using MRO.ROI.Automation.Selenium;
using MRO.ROI.Test.ExecutionFactory;
using MRO.ROI.Test.Utilities;
using OpenQA.Selenium.Remote;
using System;
using System.IO;
using System.Threading;
using static MRO.ROI.Automation.Utility.IniFile;

namespace MRO.ROI.Test.RegressionTests.MROROITests
{
    [TestClass]
    public class ROIBillingOfficeRequestsAddRefIDAndClaimIDToFacilityUpdatePatientInformationTest: ROIBaseTest
    {
        public ROIBillingOfficeRequestsAddRefIDAndClaimIDToFacilityUpdatePatientInformationTest() : base(ROITestArea.ROIAdmin)
        {
        }

        [STATestMethodAttribute]
        [TestCategory(ROITestCategory.Regression)]
        // Converted manual test case 13132-ROI-Admin-->Billing Office Requests- add Ref ID and Claim ID to Facility Update Patient Information to automated.
        public void Reg_13132_ROIBillingOfficeRequestsAddRefIDAndClaimIDToFacilityUpdatePatientInformationTest()
        {
            logger = extent.CreateTest("Reg_13132_ROIBillingOfficeRequestsAddRefIDAndClaimIDToFacilityUpdatePatientInformationTest");
            logger.Log(Status.Info, "Converted manual test case 13132-ROI-Admin-->Billing Office Requests- add Ref ID and Claim ID to Facility Update Patient Information to automated.");
            RemoteWebDriver _driver = Init(TestContext.Properties["browser"].ToString());
            ThreadLocal<RemoteWebDriver> localDriver = new ThreadLocal<RemoteWebDriver>();
            localDriver.Value = _driver;
            RemoteWebDriver driver = localDriver.Value;
            

            try
            {
                string claimId = IniHelper.ReadConfig("ROIBillingOfficeRequestsAddRefIDAndClaimIDToFacilityUpdatePatientInformationTest", "ClaimId");
                string referenceId = IniHelper.ReadConfig("ROIBillingOfficeRequestsAddRefIDAndClaimIDToFacilityUpdatePatientInformationTest", "ReferenceId");

                ROIAdminHomePage rOIAdminHomePage = new ROIAdminHomePage(driver, logger, TestContext);
                rOIAdminHomePage.SelectRequesterList();
                ROIRequestersListPage rOIRequestersListPage = new ROIRequestersListPage(driver, logger, TestContext);
                rOIRequestersListPage.ClickOnSearch();
                logger.Log(Status.Info, "Internal Portal requester filterd with search filters as Organization:[Test],Type:[Internal]", TakeScreenShotAtStep());
                rOIRequestersListPage.ClickTestPortal();

                ROIAdminEditRequesterInfoPage rOIAdminEditRequesterInfoPage = new ROIAdminEditRequesterInfoPage(driver, logger, TestContext);
                rOIAdminEditRequesterInfoPage.VerifyAndUpdateInternalPortalSettings();

                rOIAdminHomePage.SwitchToNewTabAndLoginROITestFacilityCBO(BaseAddress);
                ROICBOCreateRequestPage rOICBOCreateRequestPage = new ROICBOCreateRequestPage(driver, logger, TestContext);
                rOICBOCreateRequestPage.ClickOnCreateRequest();
                rOICBOCreateRequestPage.CreateRequesWithSpecificData();

                string requestID = rOICBOCreateRequestPage.GetRequestID();
                logger.Log(Status.Info, $"Request created with RequestID({requestID})", TakeScreenShotAtStep());
                rOICBOCreateRequestPage.ClickOnRequestStatusScreenButton();

                ROIFacilityRequestStatusPage rOIFacilityRequestStatusPage = new ROIFacilityRequestStatusPage(driver, logger, TestContext);
                rOIFacilityRequestStatusPage.ClickOnUpdatePtInfo();
                string ClaimVal=rOIFacilityRequestStatusPage.GetClaimId();
                Assert.AreEqual(claimId, ClaimVal, "Failed to verify claim value");
                string referenceVal = rOIFacilityRequestStatusPage.GetReferenceId();

                logger.Log(Status.Pass, "Verified that reference id and claim id are populated ", TakeScreenShotAtStep());
                rOIFacilityRequestStatusPage.ClickOnCancelBtn();

                rOIAdminHomePage.SwitchToNewTabAndLoginROIAdmin(BaseAddress);
                rOIAdminHomePage.SearchByRequestId(requestID);
                logger.Log(Status.Info, "Verified that admin RSS opened", TakeScreenShotAtStep());
                ROIAdminRequestStatusPage rOIAdminRequestStatusPage = new ROIAdminRequestStatusPage(driver, logger, TestContext);
                rOIAdminRequestStatusPage.DrillInToFacility();           

       
                rOIFacilityRequestStatusPage.ClickOnUpdateInfo();
                string ClaimVal1 = rOIFacilityRequestStatusPage.GetClaimId();
                Assert.AreEqual(claimId, ClaimVal1, "Failed to verify claim value");
                string referenceVa1l = rOIFacilityRequestStatusPage.GetReferenceId();
                Assert.AreEqual(referenceId, referenceVa1l, "Failed to verify claim value");
                logger.Log(Status.Pass, "Verified that reference id and claim id are populated on Facility RSS Page ", TakeScreenShotAtStep());
                rOIFacilityRequestStatusPage.ClickOnCancelBtn();

                rOIFacilityRequestStatusPage.FacilityLogout();

                rOIAdminRequestStatusPage.ClickOnReAssignROIRequester();
                ROIAdminAssignROIRequesterPage rOIAdminAssignROIRequesterPage = new ROIAdminAssignROIRequesterPage(driver, logger, TestContext);
                string claimId2=rOIAdminAssignROIRequesterPage.GetClaimIdOnAdminRSS();
                Assert.AreEqual(claimId, claimId2, "Failed to verify claim value");
                string referenceVa12 = rOIFacilityRequestStatusPage.GetReferenceId();
                Assert.AreEqual(referenceId, referenceVa12, "Failed to verify claim value");
                logger.Log(Status.Pass, "Verified that reference id and claim id are populated on Admin RSS Page ", TakeScreenShotAtStep());

                LoginPage loginPage = new LoginPage(driver, logger, TestContext);
                loginPage.LogOut();
                Cleanup(driver);
            }

            catch (Exception ex)
            {
                
                LogException(driver, logger, ex);

            }
        }
    }

}

