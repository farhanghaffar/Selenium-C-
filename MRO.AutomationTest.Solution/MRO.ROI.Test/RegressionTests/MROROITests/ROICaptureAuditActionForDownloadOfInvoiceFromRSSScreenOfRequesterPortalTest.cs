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
using System.IO;
using System.Threading;
using static MRO.ROI.Automation.Utility.IniFile;

namespace MRO.ROI.Test.RegressionTests.MROROITests
{
    [TestClass]
    public class ROICaptureAuditActionForDownloadOfInvoiceFromRSSScreenOfRequesterPortalTest : ROIBaseTest
    {
        public ROICaptureAuditActionForDownloadOfInvoiceFromRSSScreenOfRequesterPortalTest() : base(ROITestArea.ROIExternalRequesterPortal)
        {
        }

        [STATestMethodAttribute]
        [TestCategory(ROITestCategory.Regression)]
        // Converted manual test case 13869-ROI-Requester Portal-->Test Case 13869: Capture Audit Action for Download of Invoice from RSS Screen of Requester Portal to automated.
        public void Reg_13869_ROICaptureAuditActionForDownloadOfInvoiceFromRSSScreenOfRequesterPortalTest()
        {
            logger = extent.CreateTest("Reg_13869_ROICaptureAuditActionForDownloadOfInvoiceFromRSSScreenOfRequesterPortalTestt");
            logger.Log(Status.Info, "Converted manual test case 13869-ROI-Requester Portal-->Test Case 13869: Capture Audit Action for Download of Invoice from RSS Screen of Requester Portal to automated");
            RemoteWebDriver _driver = Init(TestContext.Properties["browser"].ToString());
            ThreadLocal<RemoteWebDriver> localDriver = new ThreadLocal<RemoteWebDriver>();
            localDriver.Value = _driver;
            RemoteWebDriver driver = localDriver.Value;
            string userRoot = System.Environment.GetEnvironmentVariable("USERPROFILE");
            string downloadFolder = Path.Combine(userRoot, "Downloads\\");
            string currentReportName = string.Empty;
            try
            {

                string bostonProper = IniHelper.ReadConfig("ROICaptureAuditActionForDownloadOfInvoiceFromRSSScreenOfRequesterPortalTest", "Facility");
                string testAttorney = IniHelper.ReadConfig("ROICaptureAuditActionForDownloadOfInvoiceFromRSSScreenOfRequesterPortalTest", "Requester");
                string facilityVal = IniHelper.ReadConfig("ROICaptureAuditActionForDownloadOfInvoiceFromRSSScreenOfRequesterPortalTest", "AuditLogFacility");
                string action = IniHelper.ReadConfig("ROICaptureAuditActionForDownloadOfInvoiceFromRSSScreenOfRequesterPortalTest", "Action");

                ROITestRequesterPortalHomePage rOITestRequesterPortalHomePage = new ROITestRequesterPortalHomePage(driver, logger, TestContext);
                rOITestRequesterPortalHomePage.ClickOnNotificationPopUp();
                rOITestRequesterPortalHomePage.GotoRequestRecords();
                ROICreateRequestPage rOICreateRequestPage = new ROICreateRequestPage(driver, logger, TestContext);
                rOICreateRequestPage.SelectFacility(bostonProper);
                logger.Log(Status.Pass, "Create request page updated with selected facility information", TakeScreenShotAtStep());

                rOICreateRequestPage.CreateRequestBySelectingRequester(testAttorney);
                bool isDisplayed=rOICreateRequestPage.VerifySubmitRequestPopWindow();
                Assert.IsTrue(isDisplayed, "Failed to verify request popup window");
                string facility=rOICreateRequestPage.VerifyFacility();
                string facilityAtPopupWindow=rOICreateRequestPage.VerifyFacilityAtSubmitRequestWindow();
                Assert.AreEqual(facility, facilityAtPopupWindow);
                logger.Log(Status.Info, "Verified submit request popup window is displayed and selected facility , requester details are displayed", TakeScreenShotAtStep());
                rOICreateRequestPage.ClickOnSubmitRequest();
                string reqId = rOICreateRequestPage.ReturnRequestId();
                logger.Log(Status.Pass, $"Request created with id:({reqId})", TakeScreenShotAtStep());

                ROIAdminHomePage rOIAdminHomePage = new ROIAdminHomePage(driver, logger, TestContext);
                rOIAdminHomePage.SwitchToNewTabAndLoginROIAdmin(BaseAddress);
                rOIAdminHomePage.SearchByRequestId(reqId);
                ROIAdminRequestStatusPage rOIAdminRequestStatusPage = new ROIAdminRequestStatusPage(driver, logger, TestContext);
                rOIAdminRequestStatusPage.DrillInToFacility();

                ROIFacilityCompleteLoggingImportRequestPage rOIFacilityCompleteLoggingImportRequestPage = new ROIFacilityCompleteLoggingImportRequestPage(driver, logger, TestContext);
                rOIFacilityCompleteLoggingImportRequestPage.logRequest();
                ROIFacilityRequestStatusPage rOIFacilityRequestStatusPage = new ROIFacilityRequestStatusPage(driver, logger, TestContext);
                rOIFacilityRequestStatusPage.ImportPatientPages();
                rOIFacilityRequestStatusPage.ReleaseRequestID();

                rOIFacilityRequestStatusPage.FacilityLogout();
                rOIAdminHomePage.SearchByRequestId(reqId);
                ROIAdminRequestStatusPage roiadminrequeststatuspage = new ROIAdminRequestStatusPage(driver, logger, TestContext);
                roiadminrequeststatuspage.assignRequester();
                ROIAdminAssignROIRequesterPage rOIAdminAssignROIRequesterPage = new ROIAdminAssignROIRequesterPage(driver, logger, TestContext);
                rOIAdminAssignROIRequesterPage.ApplyRequester();

                roiadminrequeststatuspage.ClickOnQcPassButton();
                roiadminrequeststatuspage.ClickApplyRateButton();
                roiadminrequeststatuspage.CreateInvoice();
                string adjustedBalance= roiadminrequeststatuspage.AdjustedBalance();
                roiadminrequeststatuspage.ClickOnAuditLog();
                ROIAdminAuditLogPage rOIAdminAuditLogPage = new ROIAdminAuditLogPage(driver, logger, TestContext);
                rOIAdminAuditLogPage.SearchRequestOnAuditLog(facilityVal,action);
                bool isReqDisplayed= rOIAdminAuditLogPage.VerifyRequest(reqId);
                Assert.IsFalse(isReqDisplayed, "Request id Present");

                rOIAdminHomePage.SwitchBackToRequesterPortal(BaseAddress);
                rOITestRequesterPortalHomePage.ClickOnNotificationPopUp();
                rOITestRequesterPortalHomePage.SearchRequestId(reqId);

                bool _isDisplayed=rOICreateRequestPage.VerifyProcessingHistory();
                Assert.IsTrue(_isDisplayed, "Download invoice and view invoice details buttons are not displayed");
                logger.Log(Status.Pass, "Verified under processing history section view invoice and download invoice buttons are displayed", TakeScreenShotAtStep());

                rOICreateRequestPage.ClickOnDownloadInvoice();
                var todaysDate = String.Format("{0:yyyy/MM/dd}", DateTime.Now).Replace("/", " ").Trim();
                var reportFiles = Directory.GetFiles(downloadFolder, $"Invoice_*.pdf", SearchOption.TopDirectoryOnly);
                if(reportFiles.Length>=1)
                {
                    string amount=rOITestRequesterPortalHomePage.GetDataFromPDFFile(reportFiles[1], adjustedBalance);
                    logger.Log(Status.Pass, $"Verified pdf file contains adjusted balance amount:{(amount)}");
                }
                rOIAdminHomePage.SwitchToAdminTab(BaseAddress);
                rOIAdminHomePage.SearchByRequestId(reqId);
                roiadminrequeststatuspage.ClickOnAuditLog();
                rOIAdminAuditLogPage.SearchRequestOnAuditLog(facilityVal,action);

                bool isRequestDisplayed=rOIAdminAuditLogPage.VerifyRequest(reqId);
                Assert.IsTrue(isRequestDisplayed, "Request is not displayed");
                logger.Log(Status.Pass, "Verified search returns the data");

                string actionValue = rOIAdminAuditLogPage.VerifyActionData();
                string info = rOIAdminAuditLogPage.VerifyInfo();
                Assert.AreEqual(actionValue, action);
                Assert.AreEqual(info, "Invoice Downloaded", "Failed to verify info value");
                logger.Log(Status.Pass, $"Verified search results data like action:{(actionValue)} ,info:{(info)} ", TakeScreenShotAtStep());

                rOIAdminHomePage.SwitchBackToRequesterPortal(BaseAddress);
                rOITestRequesterPortalHomePage.ClickOnNotificationPopUp();
                rOITestRequesterPortalHomePage.SearchRequestId(reqId);
                rOICreateRequestPage.ClickOnViewInvoice();
                string balanceDue= rOICreateRequestPage.BalanceDueAtInvoicePage();
                bool isLabelDisplayed=rOICreateRequestPage.VerifyLabels();
                Assert.IsTrue(isLabelDisplayed, "Labels are not displayed");
                Assert.AreEqual(balanceDue, adjustedBalance, "Failed to verify balance due");
                logger.Log(Status.Pass, " Successfully Verified  labels  and amount value matched", TakeScreenShotAtStep());
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

