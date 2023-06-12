using AventStack.ExtentReports;
using AventStack.ExtentReports.MarkupUtils;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MRO.ROI.Automation.Pages;
using MRO.ROI.Automation.Pages.Common;
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
    public class ROIIssuePackageCreationErrorMessageTest : ROIBaseTest
    {
        public ROIIssuePackageCreationErrorMessageTest() : base(ROITestArea.ROITestFacility)
        {
        }

        [STATestMethodAttribute]
        [TestCategory(ROITestCategory.Regression)]
        // Converted manual test case 4312-ROI-Facility-->Issue Package Creation Error Message to automated.
        public void Reg_4312_ROIIssuePackageCreationErrorMessageTest()
        {
            logger = extent.CreateTest("Reg_4312_ROIIssuePackageCreationErrorMessageTest");
            logger.Log(Status.Info, "Converted manual test case 4312-ROI-Facility-->Issue Package Creation Error Message to automated");
            RemoteWebDriver _driver = Init(TestContext.Properties["browser"].ToString());
            ThreadLocal<RemoteWebDriver> localDriver = new ThreadLocal<RemoteWebDriver>();
            localDriver.Value = _driver;
            RemoteWebDriver driver = localDriver.Value;

            try
            {
                string issueName = IniHelper.ReadConfig("ROIIssuePackageCreationErrorMessageTest", "BehavioralIssue");
                string comment = IniHelper.ReadConfig("ROIIssuePackageCreationErrorMessageTest", "Comments");
                ROIFacilityWorkSummaryPage rOIFacilityWorkSummaryPage = new ROIFacilityWorkSummaryPage(driver, logger, TestContext);
                rOIFacilityWorkSummaryPage.GoToLogNewRequestPage();
                ROIFacilityLogNewRequestPage rOIFacilityLogNewRequestPage = new ROIFacilityLogNewRequestPage(driver, logger, TestContext);
                rOIFacilityLogNewRequestPage.CreateNewMRODeliveryRequestForBostonProper();

                ROIFacilityRequestStatusPage rOIFacilityRequestStatusPage = new ROIFacilityRequestStatusPage(driver, logger, TestContext);
                string requestid = rOIFacilityRequestStatusPage.GetRequestID();
                logger.Log(Status.Info, $"MRO delivery request created with id ({requestid})");
                rOIFacilityRequestStatusPage.ImportRequestPages();

                rOIFacilityRequestStatusPage.clickOnManageIssue();
                ROIAdminAddIssuePage rOIAdminAddIssuePage = new ROIAdminAddIssuePage(driver, logger, TestContext);
                rOIAdminAddIssuePage.AddIssueWithType(issueName, comment);
                logger.Log(Status.Info, "Issue added");

                ROIManageRequestIssuesPage manageRequestIssuesPage = new ROIManageRequestIssuesPage(driver, logger, TestContext);
                string selectedIssue = manageRequestIssuesPage.VerifyIssue();
                Assert.AreEqual(selectedIssue, issueName, "Failed to verify issue");
                logger.Log(Status.Pass, $"Successfully verified manage request issues window  opened and status added  under outstanding  issues section :{(selectedIssue)}", TakeScreenShotAtStep());
                manageRequestIssuesPage.ClickOnDone();

                rOIFacilityRequestStatusPage.ImportPatientPages();
                rOIFacilityRequestStatusPage.ReleaseRequestID();

                ROIAdminHomePage rOIAdminHomePage = new ROIAdminHomePage(driver, logger, TestContext);
                rOIAdminHomePage.SwitchToNewTabAndLoginROIAdmin(BaseAddress);
                rOIAdminHomePage.SearchByRequestId(requestid);

                ROIAdminRequestStatusPage rOIAdminRequestStatusPage = new ROIAdminRequestStatusPage(driver, logger, TestContext);
                rOIAdminRequestStatusPage.assignRequester();
                ROIAdminAssignROIRequesterPage rOIAdminAssignROIRequesterPage = new ROIAdminAssignROIRequesterPage(driver, logger, TestContext);
                rOIAdminAssignROIRequesterPage.assignTestAttorney();
                string requesterValue = rOIAdminRequestStatusPage.VerifyReAssignRequester();
                Assert.AreEqual(requesterValue, "TEST Attorney's");
                logger.Log(Status.Info, $"Succcessfully verified at the request status page ship to ({requesterValue})", TakeScreenShotAtStep());

                rOIAdminRequestStatusPage.ClickSendIssueBtn();
                rOIAdminRequestStatusPage.VerifyAlertMessage();
                driver.SwitchTo().Alert().Accept();
                logger.Log(Status.Pass, "Successfully verified that alert message displayed", TakeScreenShotAtStep());
                rOIAdminRequestStatusPage.VerifyCorrespondenceIssueCreatedOrNot();
                logger.Log(Status.Pass, "Successfully verified that no issue created under Correspondence Packages and Correspondence History", TakeScreenShotAtStep());

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

