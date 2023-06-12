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

namespace MRO.ROI.Test.RegressionTests.MROROITests
{
    [TestClass]
    public class ROIWriteOffCapTest : ROIBaseTest
    {

        public ROIWriteOffCapTest() : base(ROITestArea.ROIFacility)
        {
        }


        [STATestMethodAttribute]
        [TestCategory(ROITestCategory.Regression)]
        // Converted manual test case 8549-ROI-Admin--> Write Off Caps to automated
        public void Reg_8549_WriteOffCapTest()
        {
            logger = extent.CreateTest("Reg_8549_WriteOffCapTest");
            logger.Log(Status.Info, "Converted manual test case 8549-ROI-Admin--> Write Off Caps to automated");
            RemoteWebDriver _driver = Init(TestContext.Properties["browser"].ToString());
            ThreadLocal<RemoteWebDriver> localDriver = new ThreadLocal<RemoteWebDriver>();
            localDriver.Value = _driver;
            RemoteWebDriver driver = localDriver.Value;
            try
            {               

                ROIFacilityWorkSummaryPage rOIFacilityWorkSummaryPage = new ROIFacilityWorkSummaryPage(driver, logger, TestContext);
                rOIFacilityWorkSummaryPage.logaNewRequest();
                ROIFacilityLogNewRequestPage rOIFacilityLogNewRequestPage = new ROIFacilityLogNewRequestPage(driver, logger, TestContext);
                rOIFacilityLogNewRequestPage.CreateNewMRODeliveryRequestWithoutScan();
                ROIFacilityRequestStatusPage rOIFacilityRequestStatusPage = new ROIFacilityRequestStatusPage(driver, logger, TestContext);
                string requestid = rOIFacilityRequestStatusPage.GetRequestID();
                logger.Log(Status.Info, $"ROI request created with id ({requestid})", TakeScreenShotAtStep());
                ROIAdminHomePage rOIAdminHomePage = new ROIAdminHomePage(driver, logger, TestContext);
                rOIAdminHomePage.ROIlookupByRequestId(requestid); 
                rOIFacilityRequestStatusPage.ImportPdfFiles();         
                bool statusUnderImportDocument = rOIFacilityRequestStatusPage.VerifyStatusUnderImportDocument();
                Assert.IsTrue(statusUnderImportDocument, "Failed to verify status under import document is uploaded");
                rOIFacilityRequestStatusPage.ReleaseRequestID();
                logger.Log(Status.Info, "Request released");                            
                rOIAdminHomePage.SwitchToNewTabAndLoginROIAdmin(BaseAddress);               
                rOIAdminHomePage.ROIlookupByRequestId(requestid);
                ROIAdminRequestStatusPage adminRequestStatusPage = new ROIAdminRequestStatusPage(driver, logger, TestContext);
                ROIAdminAssignROIRequesterPage assignROIRequesterPage = new ROIAdminAssignROIRequesterPage(driver, logger, TestContext);
                adminRequestStatusPage.assignRequester();
                assignROIRequesterPage.assignTestAttorney();
                logger.Log(Status.Info, "Assigned test attorney");               
                adminRequestStatusPage.ClickPassDocsQC();               
                adminRequestStatusPage.ClickApplyRateButton();
                adminRequestStatusPage.CreateInvoice();
                logger.Log(Status.Info, "Invoice created");
                adminRequestStatusPage.ClickLedgerDetailButton();
                ROIAdminLedgerDetailPage rOIAdminLedgerDetailPage = new ROIAdminLedgerDetailPage(driver, logger, TestContext);
                rOIAdminLedgerDetailPage.ClickOnWriteOffsButton();               
                ROIAdminWriteOffsPage rOIAdminWriteOffsPage = new ROIAdminWriteOffsPage(driver, logger, TestContext);
                string adjustedAmountOnWriteOffsPage = rOIAdminWriteOffsPage.CreateAndVerifyWriteoff();               
                rOIAdminWriteOffsPage.SwitchToDefaultRSSPage();
                LoginPage loginPage = new LoginPage(driver, logger, TestContext);
                loginPage.LogOut();
                rOIAdminHomePage.ROIAdminLoginForSpecificUser();
                rOIAdminHomePage.ROIlookupByRequestId(requestid);               
                string adjustedAmountOnRSSPage = adminRequestStatusPage.GetAdjustedBalanceAmountOnRSS();                
                Assert.AreEqual(adjustedAmountOnWriteOffsPage, adjustedAmountOnRSSPage, $"Successfully verified writeoffs had been applied to the Invoice, and the Adjusted Balance is ({adjustedAmountOnRSSPage})");
                logger.Log(Status.Pass, $"Verified writeoffs had been applied to the Invoice and the Adjusted Balance is ({adjustedAmountOnRSSPage})", TakeScreenShotAtStep());
                Cleanup(driver);
            }
            catch (Exception ex)
            {
                LogException(driver, logger, ex);

            }
        }
    }
}
