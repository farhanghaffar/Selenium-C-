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
    public class ROIHotFixClariseRequesterPortalPayForRecordsBugTest : ROIBaseTest
    {
        public ROIHotFixClariseRequesterPortalPayForRecordsBugTest() : base(ROITestArea.ROIExternalRequesterPortal)
        {
        }

        [STATestMethodAttribute]
        [TestCategory(ROITestCategory.Regression)]
        // Converted manual test case 629-ROI-RequesterPortal-->HotFix_Clarise_Requester Portal 'Pay For Records' Bug to automated.
        public void Reg_629_ROIHotFixClariseRequesterPortalPayForRecordsBugTest()
        {
            logger = extent.CreateTest("Reg_629_ROIHotFixClariseRequesterPortalPayForRecordsBugTest");
            logger.Log(Status.Info, "Converted manual test case 629-ROI-RequesterPortal-->HotFix_Clarise_Requester Portal 'Pay For Records' Bug to automated.");
            RemoteWebDriver _driver = Init(TestContext.Properties["browser"].ToString());
            ThreadLocal<RemoteWebDriver> localDriver = new ThreadLocal<RemoteWebDriver>();
            localDriver.Value = _driver;
            RemoteWebDriver driver = localDriver.Value;
            

            try
            {


                string bostonProper = IniHelper.ReadConfig("ROIHotFixClariseRequesterPortalPayForRecordsBugTest", "Facility");
                string testRAC = IniHelper.ReadConfig("ROIHotFixClariseRequesterPortalPayForRecordsBugTest", "Requester");

                ROITestRequesterPortalHomePage rOITestRequesterPortalHomePage = new ROITestRequesterPortalHomePage(driver, logger, TestContext);
                rOITestRequesterPortalHomePage.ClickOnNotificationPopUp();
                rOITestRequesterPortalHomePage.GotoRequestRecords();
                ROICreateRequestPage rOICreateRequestPage = new ROICreateRequestPage(driver, logger, TestContext);
                rOICreateRequestPage.SelectFacility(bostonProper);


                rOICreateRequestPage.CreateRequestBySelectingRequester(testRAC);
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
                string shipToValue = rOIAdminRequestStatusPage.VerifyReAssignRequester();
                Assert.AreEqual(shipToValue, "Test RAC B");
                logger.Log(Status.Info, $"Succcessfully verified at the request status page ship to ({shipToValue})", TakeScreenShotAtStep());

                roiadminrequeststatuspage.ClickOnQcPassButton();
                roiadminrequeststatuspage.ClickApplyRateButton();
                roiadminrequeststatuspage.CreateInvoice();
                string invoiceId = roiadminrequeststatuspage.GetInvoiceId();
                logger.Log(Status.Info, $" Verified that invoice created with id ({invoiceId})", TakeScreenShotAtStep());

                rOIAdminHomePage.SwitchBackToRequesterPortal(BaseAddress);
                rOITestRequesterPortalHomePage.ClickOnNotificationPopUp();
                rOITestRequesterPortalHomePage.SearchRequestId(reqId);
                string amount=rOICreateRequestPage.GetTotalBalanceDue();
                logger.Log(Status.Pass, $"Verified that total balnace due is :{(amount)}");

                bool isDisplayed= rOICreateRequestPage.VerifyPaynowButton();
                Assert.IsTrue(isDisplayed, "Failed to verify paynow button");
                logger.Log(Status.Pass, "Verified that pay now button is visible", TakeScreenShotAtStep());

                rOICreateRequestPage.ClickOnPayNowButton();
                ROIPayForRecordsPage rOIPayForRecordsPage = new ROIPayForRecordsPage(driver, logger, TestContext);
                string header=rOIPayForRecordsPage.VerifyHeader();
                Assert.AreEqual(header, "Pay For Records", "Failed to verify header");
                logger.Log(Status.Info, "Verified that pay for records page is displayed", TakeScreenShotAtStep());


                rOIPayForRecordsPage.ClickShowRequestButton();
                rOIPayForRecordsPage.VerifyRequestAndBalanceDue(reqId);
                rOIPayForRecordsPage.ClickOnPayForSelectedRecord();

                ROIConfirmDeliveryMethodsPage rOIConfirmDeliveryMethodsPage = new ROIConfirmDeliveryMethodsPage(driver, logger, TestContext);                
                rOIConfirmDeliveryMethodsPage.ClickOnContinueButton();
                rOIConfirmDeliveryMethodsPage.SelectCreditCardRadioButtonAndContinue();
                logger.Log(Status.Info, "Verified that credit card radio button is selected",TakeScreenShotAtStep());


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

