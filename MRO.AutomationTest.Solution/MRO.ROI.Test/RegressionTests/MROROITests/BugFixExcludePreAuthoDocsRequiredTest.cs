using AventStack.ExtentReports;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MRO.ROI.Automation.Pages;
using MRO.ROI.Test.Utilities;
using OpenQA.Selenium.Remote;
using System;
using System.Threading;
using MRO.ROI.Test.ExecutionFactory;

namespace MRO.ROI.Test.RegressionTests.MROROITests
{
    [TestClass]
    public class BugFixExcludePreAuthoDocsRequiredTest : ROIBaseTest
    {
        public BugFixExcludePreAuthoDocsRequiredTest() : base(ROITestArea.ROIAdmin)
        {

        }
        [STATestMethodAttribute]
        [TestCategory(ROITestCategory.Regression)]
        //Automated by Shivani
        //Converted manual test case 6859-ROI-Admin-->Bug Fix-Exclude PreAutho Docs Required.
        public void Reg_6859_BugFixExcludePreAuthoDocsRequired()
        {
            logger = extent.CreateTest("Reg_6859_BugFixExcludePreAuthoDocsRequired");
            logger.Log(Status.Info, "Converted manual test case 6859-ROI-Admin-->Bug Fix-Exclude PreAutho Docs Required");
            RemoteWebDriver _driver = Init(TestContext.Properties["browser"].ToString());
            ThreadLocal<RemoteWebDriver> localDriver = new ThreadLocal<RemoteWebDriver>();
            localDriver.Value = _driver;
            RemoteWebDriver driver = localDriver.Value;
            try
            {
                ROIAdminHomePage rOIAdminHomePage = new ROIAdminHomePage(driver, logger, TestContext);
                rOIAdminHomePage.SelectFacilityList();
                ROIAdminFacilityListPage rOIAdminFacilityListPage = new ROIAdminFacilityListPage(driver, logger, TestContext);
                rOIAdminFacilityListPage.GotoROITestFacilityComputerIcon();

                ROIFacilityWorkSummaryPage rOIFacilityWorkSummaryPage = new ROIFacilityWorkSummaryPage(driver, logger, TestContext);
                rOIFacilityWorkSummaryPage.GoToLogNewRequestPage();
                ROIFacilityLogNewRequestPage rOIFacilityLogNewRequestPage = new ROIFacilityLogNewRequestPage(driver, logger, TestContext);
                rOIFacilityLogNewRequestPage.ClickMRODeliveryTab();
                rOIFacilityLogNewRequestPage.CreateNewMRODeliveryRequestForBostonProper();
                ROIFacilityRequestStatusPage rOIFacilityRequestStatusPage = new ROIFacilityRequestStatusPage(driver, logger, TestContext);
                string requestid = rOIFacilityRequestStatusPage.GetRequestID();
                logger.Log(Status.Pass, $"Request created with requestid({requestid})", TakeScreenShotAtStep());
                rOIAdminHomePage.ROIlookupByRequestId(requestid);
                
                logger.Log(Status.Pass, $"Request reopen with requestid({requestid})", TakeScreenShotAtStep());
                rOIFacilityRequestStatusPage.ImportPdfFiles();
                rOIFacilityRequestStatusPage.ReleaseRequestID();
                logger.Log(Status.Info, "Request released", TakeScreenShotAtStep());
                rOIFacilityRequestStatusPage.ReqPreAuthorization();
                logger.Log(Status.Pass, $"Preauthorization page opened for entering values", TakeScreenShotAtStep());
                ROIRequestPreAuthorizationPage rOIRequestPreAuthorizationPage = new ROIRequestPreAuthorizationPage(driver, logger, TestContext);
                rOIRequestPreAuthorizationPage.PreAuthorizationRequired();

                rOIFacilityRequestStatusPage.FacilityLogout();

                //rOIAdminHomePage.SwitchToNewTabAndLoginROIAdmin(BaseAddress);
                rOIAdminHomePage.ROIlookupByRequestId(requestid);

                rOIFacilityRequestStatusPage.VerifyWaitingPrepaycheckbox();
                logger.Log(Status.Pass, $"Verified waiting prepay check box is checked", TakeScreenShotAtStep());
                rOIFacilityRequestStatusPage.AssignRequester();

                ROIAdminAssignROIRequesterPage rOIAdminAssignROIRequester = new ROIAdminAssignROIRequesterPage(driver, logger, TestContext);
                rOIAdminAssignROIRequester.assignTestAttorneys();
                logger.Log(Status.Pass, $"Assigned Test Attorney with id 4160 ", TakeScreenShotAtStep());
                rOIFacilityRequestStatusPage.TestAttorney();
                logger.Log(Status.Pass, $"Edit requester info page opened", TakeScreenShotAtStep());
                ROIFacilityEditUserInfoPage rOIFacilityEditUserInfoPage = new ROIFacilityEditUserInfoPage(driver, logger, TestContext);
                rOIFacilityEditUserInfoPage.ToValidateDownPaymentCheckbox();
                logger.Log(Status.Pass, $"verified down payment checkbox is not checked", TakeScreenShotAtStep());

                rOIFacilityEditUserInfoPage.SearchByRequestId(requestid);
                rOIFacilityRequestStatusPage.VerifyWaitingPrepaycheckbox();
                logger.Log(Status.Pass, $"Verified waiting prepay check box is checked", TakeScreenShotAtStep());
                rOIFacilityRequestStatusPage.FacilityLogout();

                /* bool waitingPrepay = rOIFacilityRequestStatusPage.VerifyWaitingPrepaycheckbox();

                 Assert.IsTrue(waitingPrepay, "Failed to verify that waiting prepay checkbox is not checked");

                 logger.Log(Status.Pass, $"Verified waiting prepay check box is still  checked", TakeScreenShotAtStep());*/
                rOIFacilityRequestStatusPage.FacilityLogout();
                Cleanup(driver);

            }
            catch (Exception ex)
            {
                LogException(driver, logger, ex);

            }
        }
    }
}
