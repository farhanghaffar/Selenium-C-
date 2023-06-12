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
        public class ROIStartStopProcessInvoicingQueuedocTest : ROIBaseTest
        {
            public ROIStartStopProcessInvoicingQueuedocTest() : base(ROITestArea.ROITestFacility)
            {
            }

            [STATestMethodAttribute]
            [TestCategory(ROITestCategory.Regression)]
            // Converted manual test case 4398-ROI-Facility-->Start / Stop Process - Invoicing Queue [doc] to automated.
            public void Reg_4398_ROIStartStopProcessInvoicingQueuedocTest()
            {
                logger = extent.CreateTest("Reg_4897_ROIAdminIssueValidationReportTest");
                logger.Log(Status.Info, "Converted manual test case 4398-ROI-Facility-->Start / Stop Process - Invoicing Queue [doc] to automated.");
                RemoteWebDriver _driver = Init(TestContext.Properties["browser"].ToString());
                ThreadLocal<RemoteWebDriver> localDriver = new ThreadLocal<RemoteWebDriver>();
                localDriver.Value = _driver;
                RemoteWebDriver driver = localDriver.Value;


                try
               {
                    ROIFacilityWorkSummaryPage rOIFacilityWorkSummaryPage = new ROIFacilityWorkSummaryPage(driver, logger, TestContext);
                    ROIAdminHomePage rOIAdminHomePage = new ROIAdminHomePage(driver, logger, TestContext);
                    rOIFacilityWorkSummaryPage.GoToLogNewRequestPage();
                    ROIFacilityLogNewRequestPage rOIFacilityLogNewRequestPage = new ROIFacilityLogNewRequestPage(driver, logger, TestContext);
                    rOIFacilityLogNewRequestPage.ClickMRODeliveryTab();
                    rOIFacilityLogNewRequestPage.MRODeliveryRequestForBostonProper();
                    LogNewRequestPage logNewRequestPage = new LogNewRequestPage(driver, logger, TestContext);
                    string requestId1 = logNewRequestPage.getRequestid();
                    logger.Log(Status.Pass, $"Request created with requestid({requestId1})", TakeScreenShotAtStep());
                    ROIFacilityRequestStatusPage rOIFacilityRequestStatusPage = new ROIFacilityRequestStatusPage(driver, logger, TestContext);
                    rOIAdminHomePage.ROIlookupByRequestId(requestId1);
                    rOIFacilityRequestStatusPage.ImportPdfFiles();
                    rOIFacilityRequestStatusPage.ReleaseRequestID();


                    rOIFacilityWorkSummaryPage.GoToLogNewRequestPage();
                    rOIFacilityLogNewRequestPage.MRODeliveryRequestForBostonProper();
                    string requestId2 = logNewRequestPage.getRequestid();
                    logger.Log(Status.Pass, $"Request created with requestid({requestId2})", TakeScreenShotAtStep());
                    rOIAdminHomePage.ROIlookupByRequestId(requestId2);
                    rOIFacilityRequestStatusPage.ImportPdfFiles();
                    rOIFacilityRequestStatusPage.ReleaseRequestID();


                    rOIFacilityWorkSummaryPage.GoToLogNewRequestPage();
                    rOIFacilityLogNewRequestPage.MRODeliveryRequestForBostonProper();
                    string requestId3 = logNewRequestPage.getRequestid();
                    logger.Log(Status.Pass, $"Request created with requestid({requestId3})", TakeScreenShotAtStep());
                    rOIAdminHomePage.ROIlookupByRequestId(requestId3);
                    rOIFacilityRequestStatusPage.ImportPdfFiles();
                    rOIFacilityRequestStatusPage.ReleaseRequestID();


                    rOIAdminHomePage.SwitchToNewTabAndLoginROIAdmin(BaseAddress);
                    rOIAdminHomePage.ClickInvoicingQueue();

                    ROIInvoicingQueuePage rOIInvoicingQueuePage = new ROIInvoicingQueuePage(driver, logger, TestContext);
                    rOIInvoicingQueuePage.ClickOnSearch();
                    bool isDisplayed = rOIInvoicingQueuePage.CheckRequestIdsExistsOrNot(requestId1);
                    Assert.IsFalse(isDisplayed, "Failed to verify request id's");
                    rOIInvoicingQueuePage.CheckRequestIdsExistsOrNot(requestId2);
                    rOIInvoicingQueuePage.CheckRequestIdsExistsOrNot(requestId3);
                    logger.Log(Status.Info, "Verified that search does not return created requests");

                    ROIAdminRequestStatusPage rOIAdminRequestStatusPage = new ROIAdminRequestStatusPage(driver, logger, TestContext);
                    rOIAdminHomePage.SearchByRequestId(requestId1);
                    rOIAdminRequestStatusPage.assignRequester();
                    ROIAdminAssignROIRequesterPage rOIAdminAssignROIRequesterPage = new ROIAdminAssignROIRequesterPage(driver, logger, TestContext);
                    rOIAdminAssignROIRequesterPage.assignTestAttorney();
                    rOIAdminRequestStatusPage.ClickOnQcPassButton();

                    rOIAdminHomePage.SearchByRequestId(requestId2);
                    rOIAdminRequestStatusPage.assignRequester();
                    rOIAdminAssignROIRequesterPage.assignTestAttorney();
                    rOIAdminRequestStatusPage.ClickOnQcPassButton();

                    rOIAdminHomePage.SearchByRequestId(requestId3);
                    rOIAdminRequestStatusPage.assignRequester();
                    rOIAdminAssignROIRequesterPage.assignTestAttorney();
                    rOIAdminRequestStatusPage.ClickOnQcPassButton();

                    rOIAdminHomePage.ClickInvoicingQueue();
                    rOIInvoicingQueuePage.ClickOnSearch();

                    bool _isDisplayed = rOIInvoicingQueuePage.CheckRequestIdsExistsOrNot(requestId1);
                    Assert.IsTrue(_isDisplayed, "Failed to verify request id's");

                    bool isDisplayed1 = rOIInvoicingQueuePage.CheckRequestIdsExistsOrNot(requestId2);
                    Assert.IsTrue(isDisplayed1, "Failed to verify request id's");

                    bool _isDisplayed2 = rOIInvoicingQueuePage.CheckRequestIdsExistsOrNot(requestId3);
                    Assert.IsTrue(_isDisplayed2, "Failed to verify request id's");
                    logger.Log(Status.Info, "Verified that search returns the  created requests",TakeScreenShotAtStep());

                    bool isProcessingStatusDisplayed = rOIInvoicingQueuePage.VerifyProcessingStatus();
                    Assert.IsTrue(isProcessingStatusDisplayed, "Failed to verify processing status");


                    bool isStartProcessDisplayed = rOIInvoicingQueuePage.VerifyStartProcess();
                    Assert.IsTrue(isProcessingStatusDisplayed, "Failed to verify start process");

                    bool isEndProcessDisplayed = rOIInvoicingQueuePage.VerifyEndProcess();
                    Assert.IsTrue(isProcessingStatusDisplayed, "Failed to verify end process");

                    logger.Log(Status.Pass, $"Verified that processing status like :{(isProcessingStatusDisplayed)} and start process and End process buttons are visible", TakeScreenShotAtStep());

                    rOIInvoicingQueuePage.ClickOnStartProcess();                    
                    bool isNextBtnVisible = rOIAdminRequestStatusPage.VerifyNextButtonIsVisibleOrNot();
                    Assert.IsTrue(isNextBtnVisible, "Failed to verify next button ");
                    logger.Log(Status.Info, "Verified RSS Page loads successfully and Next and EndProcess are visible", TakeScreenShotAtStep());

                    rOIAdminRequestStatusPage.ClickApplyRateButton();
                    rOIAdminRequestStatusPage.CreateInvoice();
                    bool isInvoiceCreated = rOIAdminRequestStatusPage.CreatedInvoiceNumber();
                    Assert.IsTrue(isInvoiceCreated, "Failed to create invoice");
                    logger.Log(Status.Info, $"Succcessfully verified invoice created", TakeScreenShotAtStep());
                    
                    rOIAdminRequestStatusPage.ClickOnNextButton();
                    rOIAdminRequestStatusPage.ClickInvoicingQueue();
                    rOIInvoicingQueuePage.ClickOnSearch();

                    string processingStatus= rOIInvoicingQueuePage.ReturnProcessingStatus();
                    Assert.AreEqual(processingStatus, "Invoicing in progress.");

                    bool isResumeProcessDisplayed = rOIInvoicingQueuePage.VerifyResumeProcess();
                    Assert.IsTrue(isResumeProcessDisplayed, "Failed to verify resume process");

                    logger.Log(Status.Info, $"Verified that processing status is {(processingStatus)} and Resume Process and End Process buttons are visible", TakeScreenShotAtStep());

                    rOIInvoicingQueuePage.ClickOnStartProcess();
                    rOIAdminRequestStatusPage.ClickApplyRateButton();
                    rOIAdminRequestStatusPage.CreateInvoice();
                    bool isInvoiceCreated1 = rOIAdminRequestStatusPage.CreatedInvoiceNumber();
                    Assert.IsTrue(isInvoiceCreated1, "Failed to create invoice");
                    logger.Log(Status.Info, $"Succcessfully verified invoice created", TakeScreenShotAtStep());

                    rOIAdminRequestStatusPage.ClickOnNextButton();
                    rOIAdminRequestStatusPage.ClickApplyRateButton();
                    rOIAdminRequestStatusPage.CreateInvoice();
                    bool isInvoiceCreated2 = rOIAdminRequestStatusPage.CreatedInvoiceNumber();
                    Assert.IsTrue(isInvoiceCreated2, "Failed to create invoice");
                    logger.Log(Status.Info, $"Successfully verified invoice created", TakeScreenShotAtStep());

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


          