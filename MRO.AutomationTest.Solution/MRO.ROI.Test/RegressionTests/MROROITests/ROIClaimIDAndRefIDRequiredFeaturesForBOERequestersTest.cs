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
    public class ROIClaimIDAndRefIDRequiredFeaturesForBOERequestersTest : ROIBaseTest
    {
        public ROIClaimIDAndRefIDRequiredFeaturesForBOERequestersTest() : base(ROITestArea.ROIAdmin)
        {
        }

        [STATestMethodAttribute]
        [TestCategory(ROITestCategory.Passed)]
        // Converted manual test case 14808-ROI-Admin-->Claim ID and Ref ID required features for BOE Requesters to automated.
        public void Reg_14808_ROIClaimIDAndRefIDRequiredFeaturesForBOERequestersTest()
        {
            logger = extent.CreateTest("Reg_14808_ROIClaimIDAndRefIDRequiredFeaturesForBOERequestersTest");
            logger.Log(Status.Info, "Converted manual test case 14808-ROI-Admin-->Claim ID and Ref ID required features for BOE Requesters to automated.");
            RemoteWebDriver _driver = Init(TestContext.Properties["browser"].ToString());
            ThreadLocal<RemoteWebDriver> localDriver = new ThreadLocal<RemoteWebDriver>();
            localDriver.Value = _driver;
            RemoteWebDriver driver = localDriver.Value;
            

            try
            {
                string givenReqId = IniHelper.ReadConfig("ROIBillingOfficeLogANewRequestMapBOERequesterRequiredFieldsTest", "RequestId");
                ROIAdminHomePage rOIAdminHomePage = new ROIAdminHomePage(driver, logger, TestContext);
                rOIAdminHomePage.SearchByRequestId(givenReqId);
                ROIAdminRequestStatusPage rOIAdminRequestStatusPage = new ROIAdminRequestStatusPage(driver, logger, TestContext);
                

                rOIAdminRequestStatusPage.ClickOnAetnaTestFax();
                ROIAdminEditRequesterInfoPage rOIAdminEditRequesterInfoPage = new ROIAdminEditRequesterInfoPage(driver, logger, TestContext);
                bool _editRequesterInfoPageHeader = rOIAdminEditRequesterInfoPage.VerifyEditRequesterInfoPage();
                Assert.IsTrue(_editRequesterInfoPageHeader, "Failed to verify edit requester info page");
                logger.Log(Status.Info, "Verified that edit requester info page opened", TakeScreenShotAtStep());

                rOIAdminEditRequesterInfoPage.VerifyAndUpdateInternalPortalSettingsBOEUnchecked();
                rOIAdminEditRequesterInfoPage.UpdateInternalPortalSettingsBOESubscriberIdUnChecked();
                rOIAdminEditRequesterInfoPage.CheckInternalPortalSettingsBOEClaimIdAndReferenceId();
                logger.Log(Status.Pass, "Successfully verified that BOE Requester claim and referenceid are selected", TakeScreenShotAtStep());

                rOIAdminHomePage.OpenNewTabAndLoginROITestFacility(BaseAddress);
                ROIFacilityWorkSummaryPage rOIFacilityWorkSummaryPage = new ROIFacilityWorkSummaryPage(driver, logger, TestContext);
                rOIFacilityWorkSummaryPage.logaNewRequest();
                ROIFacilityLogNewRequestPage rOIFacilityLogNewRequestPage = new ROIFacilityLogNewRequestPage(driver, logger, TestContext);
                rOIFacilityLogNewRequestPage.CreateBillingOfficeRequestWithClaimAndReferenceId();
                             
                              
                rOIFacilityLogNewRequestPage.AcceptReferenceIdAlert();
                logger.Log(Status.Info, "Verified that alert present", TakeScreenShotAtStep());


                LogNewRequestPage logNewRequestPage = new LogNewRequestPage(driver, logger, TestContext);
                string requestId = logNewRequestPage.getRequestid();                
                logger.Log(Status.Info, $"Billing office  request created with id:({requestId})", TakeScreenShotAtStep());


                rOIAdminHomePage.SwitchToPreviousTab(BaseAddress);
                rOIAdminHomePage.SearchByRequestId(givenReqId);
                rOIAdminRequestStatusPage.ClickOnAetnaTestFax();
                rOIAdminEditRequesterInfoPage.UnCheckInternalPortalSettingsBOEClaimIdAndReferenceId();
               

                rOIAdminHomePage.SwitchToROITestFacilitySide(BaseAddress);
                rOIFacilityWorkSummaryPage.logaNewRequest();
                rOIFacilityLogNewRequestPage.CreateBillingOfficeRequest();
                              
                logger.Log(Status.Pass, "Verifed that there is  no popup message is like to enter clamid details", TakeScreenShotAtStep());                
                string requestId1 = logNewRequestPage.getRequestid();                
                logger.Log(Status.Info, $"Billing office  request created with id:({requestId1})", TakeScreenShotAtStep());


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

