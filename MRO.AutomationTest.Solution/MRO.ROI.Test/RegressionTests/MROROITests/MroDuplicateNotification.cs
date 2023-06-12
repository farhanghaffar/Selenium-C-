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
    public class MroDuplicateNotificationTest : ROIBaseTest
    {
        public static string MailLocation = string.Empty;
        public MroDuplicateNotificationTest() : base(ROITestArea.ROIAdmin)
        {

        }

        [STATestMethodAttribute]
        [TestCategory(ROITestCategory.Regression)]
        //Converted manual test case 11260-ROI-Admin-->MRO DUPLICATE NOTIFICATIONS- Northside Infor to automated
        public void Reg_11260_MroDuplicateNotificationsTest()
        {
            logger = extent.CreateTest("Reg_11260_MroDuplicateNotificationsTest");
            logger.Log(Status.Info, "Converted manual test case 11260 - ROI-Admin -->MRO DUPLICATE NOTIFICATIONS- Northside Infor to automated");
            RemoteWebDriver _driver = Init(TestContext.Properties["browser"].ToString());
            ThreadLocal<RemoteWebDriver> localDriver = new ThreadLocal<RemoteWebDriver>();
            localDriver.Value = _driver;
            RemoteWebDriver driver = localDriver.Value;
            MailLocation = TestContext.Properties["EmailsLocation"].ToString();
            string requestID = string.Empty;
            string requestIDFromEmail = string.Empty;           
            try
            {
                ROIAdminHomePage rOIAdminHomePage = new ROIAdminHomePage(driver, logger, TestContext);
                rOIAdminHomePage.FacilityList();
                ROIAdminFacilityListPage adminFacilityListPage = new ROIAdminFacilityListPage(driver, logger, TestContext);
                adminFacilityListPage.GoToROITestFacility();

                ROIFacilityWorkSummaryPage rOIFacilityWorkSummaryPage = new ROIFacilityWorkSummaryPage(driver, logger, TestContext);
                rOIFacilityWorkSummaryPage.logaNewRequest();
                ROIFacilityLogNewRequestPage rOIFacilityLogNewRequest = new ROIFacilityLogNewRequestPage(driver, logger, TestContext);
                rOIFacilityLogNewRequest.ClickOnInternalPortalTab();

                rOIFacilityLogNewRequest.CreateInternalPortalTabNewRequest();
                ROIFacilityRequestStatusPage rOIFacilityRequestStatusPage = new ROIFacilityRequestStatusPage(driver, logger, TestContext);
                requestID = rOIFacilityRequestStatusPage.GetRequestID();
                requestID = requestID.Trim();
                logger.Log(Status.Pass, $"Request created with requestid({requestID})", TakeScreenShotAtStep());

                rOIAdminHomePage.ROIlookupByRequestId(requestID);
                rOIFacilityRequestStatusPage.ImportPdfFiles();
                rOIFacilityRequestStatusPage.ReleaseRequest();
                logger.Log(Status.Info, "Request released", TakeScreenShotAtStep());

                requestIDFromEmail = rOIFacilityRequestStatusPage.GetRequestIDFromEmail(MailLocation,requestID);
                Assert.AreEqual(requestID,requestIDFromEmail,"Failed to validate request id from email");
                logger.Log(Status.Pass, $"Newly created internal portal request id({requestID}) is same as the request id({requestIDFromEmail}) extracted from mail");
                Cleanup(driver);
            }
            catch (Exception ex)
            {
                LogException(driver, logger, ex);
            }
        }
    }
}
