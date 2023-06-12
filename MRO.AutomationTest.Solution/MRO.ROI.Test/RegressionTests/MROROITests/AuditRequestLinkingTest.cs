using AventStack.ExtentReports;
using AventStack.ExtentReports.MarkupUtils;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MRO.ROI.Automation.Pages;
using MRO.ROI.Automation.Selenium;
using MRO.ROI.Test.ExecutionFactory;
using MRO.ROI.Test.Utilities;
using OpenQA.Selenium.Remote;
using System;
using System.Threading;

namespace MRO.ROI.Test.RegressionTests.MROROITests
{
    [TestClass]
    public class AuditRequestLinkingTest : ROIBaseTest
    {
        public AuditRequestLinkingTest() : base(ROITestArea.ROIFacility)
        {
        }

        [STATestMethodAttribute]
        [TestCategory(ROITestCategory.Regression)]
        //Converted manual test case 11158 -ROI-Facility-->Audit Request Linking to automated.
        public void Reg_11158_AuditRequestLinkingTest()
        {
            logger = extent.CreateTest("Reg_11158_AuditRequestLinkingTest");
            logger.Log(Status.Info, "Converted manual test case 11158 -ROI-Facility-->Audit Request Linking to automated");
            RemoteWebDriver _driver = Init(TestContext.Properties["browser"].ToString());
            ThreadLocal<RemoteWebDriver> localDriver = new ThreadLocal<RemoteWebDriver>();
            localDriver.Value = _driver;
            RemoteWebDriver driver = localDriver.Value;
            try
            {
                ROIFacilityWorkSummaryPage rOIFacilityWorkSummaryPage = new ROIFacilityWorkSummaryPage(driver, logger, TestContext);
                rOIFacilityWorkSummaryPage.GoToLogNewRequestPage();
                ROIFacilityLogNewRequestPage rOIFacilityLogNewRequestPage = new ROIFacilityLogNewRequestPage(driver, logger, TestContext);
                rOIFacilityLogNewRequestPage.ClickMRODeliveryTab();
                rOIFacilityLogNewRequestPage.CreateMRODeliveryRequestForLinkToAnotherRequest();

                ROIFacilityRequestStatusPage rOIFacilityRequestStatusPage = new ROIFacilityRequestStatusPage(driver, logger, TestContext);
                string linkedRequestid = rOIFacilityRequestStatusPage.FormatAndGetRequestId();
                logger.Log(Status.Info, $"Request created with ID ({linkedRequestid})", TakeScreenShotAtStep());


                rOIFacilityWorkSummaryPage.GoToLogNewRequestPage();
                rOIFacilityLogNewRequestPage.CreateNewMRODeliveryRequestWithoutScan();
                string requestId = rOIFacilityRequestStatusPage.FormatAndGetRequestId();
                logger.Log(Status.Info, $"Another request created with ID ({requestId})", TakeScreenShotAtStep());
                string createdUser = rOIFacilityRequestStatusPage.ReturnRequestCreatedUser();
                string patientName = rOIFacilityRequestStatusPage.VerifyPatientName();
                string location = rOIFacilityRequestStatusPage.VerifyLocation();
                rOIFacilityRequestStatusPage.ImportPdfFiles();

                rOIFacilityRequestStatusPage.ClickOnLinkToAnotherRequest();
                ROIFacilityLinkToAnotherRequestPage rOIFacilityLinkToAnotherRequestPage = new ROIFacilityLinkToAnotherRequestPage(driver, logger, TestContext);
                string _patientName = rOIFacilityLinkToAnotherRequestPage.LinkToAnotherRequestPage_PatientName();
                string _location = rOIFacilityLinkToAnotherRequestPage.LinkToAnotherRequestPage_Location();
                Assert.AreEqual(patientName, _patientName, "Failed to verify patient name");
                logger.Log(Status.Pass, $"Verified patient name on RSS page is({patientName}) same as patient name on link to another request page({_patientName})");
                Assert.AreEqual(location, _location, "Failed to verify location");
                logger.Log(Status.Pass, $"Verified location on RSS page is ({patientName}) same as location on link to another request page ({_location})", TakeScreenShotAtStep());


                rOIFacilityLinkToAnotherRequestPage.ClickOnLookupId(linkedRequestid);
                logger.Log(Status.Info, $"Request ({linkedRequestid}) linked with ({requestId})");
                bool verifyLinkedToAnotherRequest = rOIFacilityRequestStatusPage.VerifyLinkedToRequestToAnotherRequest();
                Assert.IsTrue(verifyLinkedToAnotherRequest, "Failed to link to another request");
                logger.Log(Status.Pass, "Verified that current request is linked to previous request", TakeScreenShotAtStep());

                rOIFacilityRequestStatusPage.ClickOnAuditTrail();
                ROIFacilityAuditLogPage rOIFacilityAuditLogPage = new ROIFacilityAuditLogPage(driver, logger, TestContext);
                string action = rOIFacilityAuditLogPage.ReturnAction();
                Assert.AreEqual(action, "Linked Request", "Failed to verify action");
                logger.Log(Status.Pass, "Verified linked request noted under  action column");

                string linkedReqid = rOIFacilityAuditLogPage.ReturnLinkedReqId();
                string linkedReqUserName = rOIFacilityAuditLogPage.ReturnCreatedUser();
                Assert.AreEqual(linkedRequestid.ToString(), linkedReqid);
                Assert.AreEqual(createdUser, linkedReqUserName, "Failed to verify username");
                logger.Log(Status.Pass, $"Verified linked request loaded with correct information with request id({linkedReqid}) and created user({createdUser})", TakeScreenShotAtStep());
                Cleanup(driver);

            }
            catch (Exception ex)
            {
                LogException(driver, logger, ex);
            }
        }
    }

}

