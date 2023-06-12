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
    public class ROIBillingOfficeLogANewRequestMapBOERequesterRequiredFieldsTest : ROIBaseTest
    {
        public ROIBillingOfficeLogANewRequestMapBOERequesterRequiredFieldsTest() : base(ROITestArea.ROIAdmin)
        {
        }

        [STATestMethodAttribute]
        [TestCategory(ROITestCategory.Passed)]
        // Converted manual test case 14301-ROI-Admin-->Billing Office Log a New Request- map BOE Requester Required fields to automated.
        public void Reg_14301_ROIBillingOfficeLogANewRequestMapBOERequesterRequiredFieldsTest()
        {
            logger = extent.CreateTest("Reg_14301_ROIBillingOfficeLogANewRequestMapBOERequesterRequiredFieldsTest");
            logger.Log(Status.Info, "Converted manual test case 14301-ROI-Admin-->Billing Office Log a New Request- map BOE Requester Required fields to automated");
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

                bool _updateInternalPortalSettingsPage = rOIAdminEditRequesterInfoPage.VerifyAndUpdateInternalPortalSettingsBOEChecked();
                Assert.IsTrue(_updateInternalPortalSettingsPage, "Failed to update edit requester info");
                rOIAdminEditRequesterInfoPage.UpdateInternalPortalSettingsBOESubscriberIdChecked();
                logger.Log(Status.Pass, "Successfully verified that BOE Requester Pan and SubscriberId are selected", TakeScreenShotAtStep());

                rOIAdminHomePage.OpenNewTabAndLoginROITestFacility(BaseAddress);
                ROIFacilityWorkSummaryPage rOIFacilityWorkSummaryPage = new ROIFacilityWorkSummaryPage(driver, logger, TestContext);
                rOIFacilityWorkSummaryPage.logaNewRequest();
                ROIFacilityLogNewRequestPage rOIFacilityLogNewRequestPage = new ROIFacilityLogNewRequestPage(driver, logger, TestContext);
                rOIFacilityLogNewRequestPage.CreateBillingOfficeRequestWithPANAndSubscriberId();
               // rOIFacilityLogNewRequestPage.VerifyPANAndSubscriberAlertPresent();                
                              
                rOIFacilityLogNewRequestPage.AcceptSubscriberAlert();
                logger.Log(Status.Info, "Verified that alert present", TakeScreenShotAtStep());


                LogNewRequestPage logNewRequestPage = new LogNewRequestPage(driver, logger, TestContext);
                string requestId = logNewRequestPage.getRequestid();                
                logger.Log(Status.Info, $"Billing office  request created with id:({requestId})", TakeScreenShotAtStep());


                rOIAdminHomePage.SwitchToPreviousTab(BaseAddress);
                rOIAdminHomePage.SearchByRequestId(givenReqId);
                rOIAdminRequestStatusPage.ClickOnAetnaTestFax();
                rOIAdminEditRequesterInfoPage.VerifyAndUpdateInternalPortalSettingsBOEUnchecked();
                rOIAdminEditRequesterInfoPage.UpdateInternalPortalSettingsBOESubscriberIdUnChecked();

                rOIAdminHomePage.SwitchToROITestFacilitySide(BaseAddress);
                rOIFacilityWorkSummaryPage.logaNewRequest();
                rOIFacilityLogNewRequestPage.CreateBillingOfficeRequest();
                              
                logger.Log(Status.Pass, "Verifed that there is  no popup message is like to enter pan details", TakeScreenShotAtStep());                
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

