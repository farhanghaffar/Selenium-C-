using AventStack.ExtentReports;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MRO.ROI.Automation.Pages;
using MRO.ROI.Automation.Pages.Common;
using MRO.ROI.Test.ExecutionFactory;
using MRO.ROI.Test.Utilities;
using OpenQA.Selenium.Remote;
using System;
using System.Threading;

namespace MRO.ROI.Test.RegressionTests.MROROITests
{
    [TestClass]
    public class LinkedRegularRequestIDWithoutScannedPatientPagesTest : ROIBaseTest
    {
        public LinkedRegularRequestIDWithoutScannedPatientPagesTest() : base(ROITestArea.ROIAdmin)
        {
        }

        [STATestMethodAttribute]
        [TestCategory(ROITestCategory.Regression)]
        //Converted manual test case 1216 - ROI-Facility-->Linked regular request id without scanned patient pages to automated.
        public void Reg_1216_LinkedRegularRequestIDWithoutScannedPatientPagesTest()
        {
            logger = extent.CreateTest("Reg_1216_LinkedRegularRequestIDWithoutScannedPatientPagesTest");
            logger.Log(Status.Info, "Converted manual test case 1216 - ROI-Facility-->Linked regular request id without scanned patient pages to automated");
            RemoteWebDriver _driver = Init(TestContext.Properties["browser"].ToString());
            ThreadLocal<RemoteWebDriver> localDriver = new ThreadLocal<RemoteWebDriver>();
            localDriver.Value = _driver;
            RemoteWebDriver driver = localDriver.Value;
            try
            {
                ROIAdminHomePage rOIAdminHomePage = new ROIAdminHomePage(driver, logger, TestContext);
                rOIAdminHomePage.FacilityList();
                ROIAdminFacilityListPage rOIAdminFacilityListPage = new ROIAdminFacilityListPage(driver, logger, TestContext);
                rOIAdminFacilityListPage.GotoROITestFacilityComputerIcon();
                ROIFacilityWorkSummaryPage rOIFacilityWorkSummaryPage = new ROIFacilityWorkSummaryPage(driver, logger, TestContext);
                rOIFacilityWorkSummaryPage.GoToLogNewRequestPage();
                ROIFacilityLogNewRequestPage rOIFacilityLogNewRequestPage = new ROIFacilityLogNewRequestPage(driver, logger, TestContext);
                rOIFacilityLogNewRequestPage.ClickMRODeliveryTab();
                var requestData = rOIFacilityLogNewRequestPage.CreateNewMRODeliveryRequest();
                string patientFirstName = requestData.Item1;
                string patientLastName = requestData.Item2;
                ROIFacilityRequestStatusPage rOIFacilityRequestStatusPage = new ROIFacilityRequestStatusPage(driver, logger, TestContext);
                string firstRequestId = rOIFacilityRequestStatusPage.FormatAndGetRequestId();
                logger.Log(Status.Info, $"Request created with ID ({firstRequestId})", TakeScreenShotAtStep());
                string createdUser = rOIFacilityRequestStatusPage.ReturnRequestCreatedUser();
                string patientName = rOIFacilityRequestStatusPage.VerifyPatientName();
                string location = rOIFacilityRequestStatusPage.VerifyLocation();
                string dob = rOIFacilityRequestStatusPage.GetDOBForPatient();
                rOIFacilityRequestStatusPage.ImportTwentyPatientPages();                
                rOIFacilityWorkSummaryPage.GoToLogNewRequestPage();
                rOIFacilityLogNewRequestPage.ClickMRODeliveryTab();
                rOIFacilityLogNewRequestPage.CreateDuplicateMRODeliveryRequest(patientFirstName, patientLastName, dob);
                string duplicateRequestId = rOIFacilityRequestStatusPage.GetRequestID();
                logger.Log(Status.Pass, $"Request created with requestid({duplicateRequestId})", TakeScreenShotAtStep());
                rOIAdminHomePage.ROIlookupByRequestId(duplicateRequestId);
                rOIFacilityRequestStatusPage.ImportDocumentsForDuplicateRequest();
                rOIFacilityRequestStatusPage.ClickOnLinkToAnotherRequest();
                ROIFacilityLinkToAnotherRequestPage rOIFacilityLinkToAnotherRequestPage = new ROIFacilityLinkToAnotherRequestPage(driver, logger, TestContext);
                rOIFacilityLinkToAnotherRequestPage.LinkTwoRequests(firstRequestId);
                bool linkedToAnotherRequest = rOIFacilityRequestStatusPage.VerifyLinkedToRequestToAnotherRequest();
                Assert.IsTrue(linkedToAnotherRequest, "Failed to link to another request");
                logger.Log(Status.Info, $"Request ({duplicateRequestId}) linked with another request ({firstRequestId})", TakeScreenShotAtStep());
                int totalPatientPagesCountOnRS = rOIFacilityRequestStatusPage.GetTotalPatientPagesCount();
                int totalPatientPagesCountOnViewDoc = rOIFacilityRequestStatusPage.ClickViewDocumnetAndReturnPatientDocumentsCount();
                int requestPagesCountOnViewDoc = rOIFacilityRequestStatusPage.ReturnRequestDocumentsCount();
                int totalRequestAndPatientPagesCountOnViewDoc = rOIFacilityRequestStatusPage.ReturnTotalPatientAndRequestDocumentsCount();
                Assert.AreEqual(totalPatientPagesCountOnRS, totalPatientPagesCountOnViewDoc, $"Patient documents on request status page ({totalPatientPagesCountOnRS}) and view documents ({totalPatientPagesCountOnViewDoc}) are not same");
                logger.Log(Status.Pass, $"Verified patient docs on request status page ({totalPatientPagesCountOnRS}) and view documents page ({totalPatientPagesCountOnViewDoc})");
                LoginPage loginPage = new LoginPage(driver, logger, TestContext);
                loginPage.LogOut();
                
                logger.Log(Status.Info, $"Repeating the steps 2-20 with ROI Native PDF Test facility, ID=539 ", TakeScreenShotAtStep());
                rOIAdminHomePage.SwitchToNewTabAndLoginROIAdmin(BaseAddress);
                rOIAdminHomePage.SelectFacilityList();
                rOIAdminFacilityListPage.GoToRoiNativePdfTestFacility();                //
                rOIFacilityWorkSummaryPage.GoToLogNewRequestPage();
                rOIFacilityLogNewRequestPage.ClickMRODeliveryTab();
                var result1 = rOIFacilityLogNewRequestPage.CreateNewMRODeliveryRequestForNativePDFTest();
                string patientFirstName1 = result1.Item1;
                string patientLastName1 = result1.Item2;
                string firstRequestId1 = rOIFacilityRequestStatusPage.FormatAndGetRequestId();
                logger.Log(Status.Info,$"Request created with ID ({firstRequestId1})", TakeScreenShotAtStep());
                string dob1 = rOIFacilityRequestStatusPage.GetDOBForPatient();
                rOIFacilityRequestStatusPage.ImportTwentyPatientPages();
                rOIFacilityWorkSummaryPage.GoToLogNewRequestPage();
                rOIFacilityLogNewRequestPage.ClickMRODeliveryTab();
                rOIFacilityLogNewRequestPage.CreateDuplicateMRODeliveryRequestForNativePDFTest(patientFirstName, patientLastName, dob);
                string duplicateRequestId1 = rOIFacilityRequestStatusPage.GetRequestID();
                logger.Log(Status.Pass, $"Request created with requestid({duplicateRequestId1})", TakeScreenShotAtStep());
                rOIFacilityRequestStatusPage.ImportDocumentsForDuplicateRequest();
                rOIFacilityRequestStatusPage.ClickOnLinkToAnotherRequest();
                rOIFacilityLinkToAnotherRequestPage.LinkTwoRequests(firstRequestId1);
                bool linkedToAnotherRequest1 = rOIFacilityRequestStatusPage.VerifyLinkedToRequestToAnotherRequest();
                Assert.IsTrue(linkedToAnotherRequest, "Failed to link to another request");
                logger.Log(Status.Info, $"Request({duplicateRequestId1}) linked with another request ({firstRequestId1})", TakeScreenShotAtStep());
                int totalPatientPagesCountOnRS1 = rOIFacilityRequestStatusPage.GetTotalPatientPagesCount();
                int totalPatientPagesCountOnViewDoc1 = rOIFacilityRequestStatusPage.ClickViewDocumnetAndReturnPatientDocumentsCount();
                int requestPagesCountOnViewDoc1 = rOIFacilityRequestStatusPage.ReturnRequestDocumentsCount();
                int totalRequestAndPatientPagesCountOnViewDoc1 = rOIFacilityRequestStatusPage.ReturnTotalPatientAndRequestDocumentsCount();
                Assert.AreEqual(totalPatientPagesCountOnRS1, totalPatientPagesCountOnViewDoc1, $"Patient documents on request status page ({totalPatientPagesCountOnRS1}) and view documents ({totalPatientPagesCountOnViewDoc1}) are not same");
                logger.Log(Status.Pass, $"Verified patient docs on request status page ({totalPatientPagesCountOnRS1}) and view documents page ({totalPatientPagesCountOnViewDoc1})");                                      
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





