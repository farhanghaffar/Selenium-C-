using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AventStack.ExtentReports;
using AventStack.ExtentReports.MarkupUtils;
using DataDrivenProject;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MRO.ROI.Automation;
using MRO.ROI.Automation.Pages;
using MRO.ROI.Automation.Selenium;
using MRO.ROI.Test.Utilities;
using OpenQA.Selenium.Remote;
using System.IO;
using System.Threading;
using MRO.ROI.Automation.Pages.Common;



namespace MRO.ROI.Test.RegressionTests.MROROITests
{

    [TestClass]
    public class EmailTextForRequesterPortalNotificationEmailTest : ROIBaseTest
    {
        public EmailTextForRequesterPortalNotificationEmailTest() : base(ROITestArea.ROITestFacility)
        {
        }

        [TestMethod]
        [TestCategory(ROITestCategory.Development)]
        public void Reg_2339_EmailTextForRequesterPortalNotificationEmailTest()
        {
            logger = extent.CreateTest("Reg_2339_EmailTextforRequesterPortalNotificationEmail to automated");
            RemoteWebDriver _driver = Init(TestContext.Properties["browser"].ToString());
            ThreadLocal<RemoteWebDriver> localDriver = new ThreadLocal<RemoteWebDriver>();
            localDriver.Value = _driver;
            RemoteWebDriver driver = localDriver.Value;
            string userRoot = System.Environment.GetEnvironmentVariable("USERPROFILE");
            string downloadFolder = Path.Combine(userRoot, "Downloads\\");
            string currentReportName = string.Empty;

            try
            {
                ROIAdminHomePage rOIAdminHomePage = new ROIAdminHomePage(driver, logger, TestContext);
                ROIFacilityWorkSummaryPage rOIFacilityWorkSummaryPage = new ROIFacilityWorkSummaryPage(driver, logger, TestContext);
                rOIFacilityWorkSummaryPage.GoToLogNewRequestPage();
                ROIFacilityLogNewRequestPage rOIFacilityLogNewRequestPage = new ROIFacilityLogNewRequestPage(driver, logger, TestContext);
                rOIFacilityLogNewRequestPage.ClickMRODeliveryTab();
                rOIFacilityLogNewRequestPage.CreateNewMRODeliveryRequestForBostonProper();
                ROIFacilityRequestStatusPage rOIFacilityRequestStatusPage = new ROIFacilityRequestStatusPage(driver, logger, TestContext);
                string requestid = rOIFacilityRequestStatusPage.GetRequestID();
                logger.Log(Status.Pass, $"Request created with requestid({requestid})", TakeScreenShotAtStep());
                rOIAdminHomePage.ROIlookupByRequestId(requestid);
                rOIFacilityRequestStatusPage.ReOpenRequest();
                rOIFacilityRequestStatusPage.ImportDocumentsForFacilityDeliveryReport();
                rOIFacilityRequestStatusPage.ReleaseRequestID();
                logger.Log(Status.Info, "Request Released");

                rOIAdminHomePage.SwitchToNewTabAndLoginROIAdmin(BaseAddress);
                rOIAdminHomePage.ROIlookupByRequestId(requestid);
                ROIAdminRequestStatusPage adminRequestStatusPage = new ROIAdminRequestStatusPage(driver, logger, TestContext);
                adminRequestStatusPage.assignRequester();
                ROIAdminAssignROIRequesterPage assignROIRequesterPage = new ROIAdminAssignROIRequesterPage(driver, logger, TestContext);
                assignROIRequesterPage.assignTestAttorney();
                logger.Log(Status.Info, "Assigned Test Attorney");
                adminRequestStatusPage.ClickOnTestAttroney();
                ROIAdminEditRequesterInfoPage adminEditRequesterInfoPage = new ROIAdminEditRequesterInfoPage(driver, logger, TestContext);
                adminEditRequesterInfoPage.SelectInvoiceDelivery("EMAIL");
                Assert.IsTrue(adminEditRequesterInfoPage.CheckMailIsDisabled(), "Mail check box is enabled.");
                adminEditRequesterInfoPage.ClickOnUpdateInfo();
                string infoMsg = adminEditRequesterInfoPage.GetRequesterInfoMsg();
                Assert.AreEqual(infoMsg, "Requester Info Updated!");
                adminEditRequesterInfoPage.ClickOnReturnToRequest();

                adminRequestStatusPage.ClickPassDocsQC();
                adminRequestStatusPage.ClickAddIssueBtn();
                ROIAdminAddIssuePage rOIAdminAddIssuePage = new ROIAdminAddIssuePage(driver, logger, TestContext);
                rOIAdminAddIssuePage.SetIssue();
                logger.Log(Status.Info, "Issue added");
                adminRequestStatusPage.ClickSendIssueBtn();
                string issueId = adminRequestStatusPage.VerifyCorrespondenceIssue(); ;
                logger.Log(Status.Pass, $"Successfully verified  issue id created under correspondence Packages {(issueId)}", TakeScreenShotAtStep());
                //adminRequestStatusPage.ClickOnIssueID();
                adminRequestStatusPage.ClickOnCloseBtn();

                adminRequestStatusPage.ClickApplyRateButton();
                adminRequestStatusPage.CreateInvoice();

                bool isDisplayed = adminRequestStatusPage.VerifyInvoiceID();
                Assert.IsTrue(isDisplayed, "Not verified invoice id"); 
                logger.Log(Status.Info, $"Succcessfully verified invoice created status: {(isDisplayed)}", TakeScreenShotAtStep());

                bool is_Displayed = adminRequestStatusPage.VerifyEmailStatus();
                Assert.IsTrue(is_Displayed, "Not displayed Emailed status");
                logger.Log(Status.Info, $"Succcessfully verified Emailed status: {(is_Displayed)}", TakeScreenShotAtStep());

                LoginPage loginPage = new LoginPage(driver, logger, TestContext);
                loginPage.LogOut();
                logger.Log(Status.Info, "Step number 16 and 17 are not feasible for automation");
                Cleanup(driver);
            }
            catch (Exception ex)
            {
                ExcelReaderFile.DeleteExistingFiles(downloadFolder, "xlsx", "RadGridExport.xlsx");
                LogException(driver, logger, ex);
            }
        }
    }
}