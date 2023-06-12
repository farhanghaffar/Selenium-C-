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
    public class ROINativePDFB2PIssuesTest : ROIBaseTest
    {
        public ROINativePDFB2PIssuesTest() : base(ROITestArea.ROIAdmin)
        {
        }

        [STATestMethodAttribute]
        [TestCategory(ROITestCategory.Passed)]        
        //Converted manual test case 11181-ROI-Admin-->Native PDF - B2P Issues (Linked Requests) to automated
        public void Reg_11181_NativePDFToB2PIssuesLinkedTest()
        {
            logger = extent.CreateTest("Reg_11181_NativePDFToB2PIssuesLinkedTest");
            logger.Log(Status.Info, "Converted manual test case 11181-ROI-Admin-->Native PDF - B2P Issues (Linked Requests) to automated");
            RemoteWebDriver _driver = Init(TestContext.Properties["browser"].ToString());
            ThreadLocal<RemoteWebDriver> localDriver = new ThreadLocal<RemoteWebDriver>();
            localDriver.Value = _driver;
            RemoteWebDriver driver = localDriver.Value;
            try
            {
                ROIAdminHomePage rOIAdminHomePage = new ROIAdminHomePage(driver, logger, TestContext);
                ROIAdminFacilityListPage rOIAdminFacilityListPage = new ROIAdminFacilityListPage(driver, logger, TestContext);
                rOIAdminHomePage.SelectFacilityList();
                rOIAdminFacilityListPage.GoToRoiNativePdfTestFacility();                
                ROIFacilityWorkSummaryPage rOIFacilityWorkSummaryPage = new ROIFacilityWorkSummaryPage(driver, logger, TestContext);
                ROIFacilityLogNewRequestPage rOIFacilityLogNewRequestPage = new ROIFacilityLogNewRequestPage(driver, logger, TestContext);
                rOIFacilityWorkSummaryPage.logaNewRequest();
                rOIFacilityLogNewRequestPage.CreateRoiNativePdfTestDeliveryRequestWithoutScan();
                ROIFacilityRequestStatusPage rOIFacilityRequestStatusPage = new ROIFacilityRequestStatusPage(driver, logger, TestContext);
                string requestid = rOIFacilityRequestStatusPage.GetRequestID();
                logger.Log(Status.Info, $"MRO request created with ({requestid})", TakeScreenShotAtStep());
                rOIAdminHomePage.ROIlookupByRequestId(requestid);                                           
                rOIFacilityRequestStatusPage.ImportPdfFiles();
                rOIAdminHomePage.ROIlookupByRequestId(requestid);                            
                bool statusUnderImportDocument = rOIFacilityRequestStatusPage.VerifyStatusUnderImportDocument();
                Assert.IsTrue(statusUnderImportDocument, "Failed to verify status under import document is uploaded");
                int _patientPagesCountOnRSS = rOIFacilityRequestStatusPage.GetPatientPagesCount();
                int _requestPagesCountOnRSS = rOIFacilityRequestStatusPage.GetRequestPagesCountOnRs();
                int _totalPatientAndRequestPagesCountOnRSS = _patientPagesCountOnRSS + _requestPagesCountOnRSS;
                int _totalPatientPagesCountOnViewDoc = rOIFacilityRequestStatusPage.ClickViewDocumnetAndReturnPatientDocumentsCount();
                int _requestPagesCountOnViewDoc = rOIFacilityRequestStatusPage.ReturnRequestDocumentsCount();
                int _totalRequestAndPatientPagesCountOnViewDoc = rOIFacilityRequestStatusPage.ReturnTotalPatientAndRequestDocumentsCount();
                Assert.AreEqual(_patientPagesCountOnRSS, _totalPatientPagesCountOnViewDoc, $"Patient documents on request status page ({_patientPagesCountOnRSS}) and view documents ({_totalPatientPagesCountOnViewDoc}) are not same");
                logger.Log(Status.Pass, $"Verified patient docs on request status page ({_patientPagesCountOnRSS}) and view documents page ({_totalPatientPagesCountOnViewDoc})");
                Assert.AreEqual(_requestPagesCountOnRSS, _requestPagesCountOnViewDoc, $"request documents on request status page ({_requestPagesCountOnRSS}) and view documents ({_requestPagesCountOnViewDoc}) are not same");
                logger.Log(Status.Pass, $"Verified request docs on request status page ({_requestPagesCountOnRSS}) and view documents page ({_requestPagesCountOnViewDoc})");
                Assert.AreEqual(_totalPatientAndRequestPagesCountOnRSS, _totalRequestAndPatientPagesCountOnViewDoc, $"total Patient and request documents on request status page ({_totalPatientAndRequestPagesCountOnRSS}) and view documents ({_totalRequestAndPatientPagesCountOnViewDoc}) are not same");
                logger.Log(Status.Pass, $"Verified total request and patient docs on request status page ({_totalPatientAndRequestPagesCountOnRSS}) and view documents page ({_totalRequestAndPatientPagesCountOnViewDoc})");
                             
                rOIFacilityWorkSummaryPage.logaNewRequest();               
                rOIFacilityLogNewRequestPage.CreateDuplicateRoiNativePdfTestDeliveryRequest();                              
                string requestid1 = rOIFacilityRequestStatusPage.GetRequestID();
                logger.Log(Status.Info, $"Another MRO request created with ({requestid1})", TakeScreenShotAtStep());
                rOIAdminHomePage.ROIlookupByRequestId(requestid1); 
                rOIFacilityRequestStatusPage.ImportPdfFiles();                                            
                bool statusUnderImportDocumentAgain = rOIFacilityRequestStatusPage.VerifyStatusUnderImportDocument();
                Assert.IsTrue(statusUnderImportDocumentAgain, "Failed to verify status under import document is uploaded");               
                rOIFacilityRequestStatusPage.ClickOnLinkToAnotherRequest();                
                ROIFacilityLinkToAnotherRequestPage rOIFacilityLinkToAnotherRequestPage = new ROIFacilityLinkToAnotherRequestPage(driver, logger, TestContext);
                rOIFacilityLinkToAnotherRequestPage.LinkToAnotherRequestPage_ROINativePDF(requestid);
                bool verifyLinkedToAnotherRequest = rOIFacilityRequestStatusPage.VerifyLinkedToRequestToAnotherRequest();
                Assert.IsTrue(verifyLinkedToAnotherRequest, "Failed to link to another request");
                logger.Log(Status.Pass,$"linked one requestid {requestid1} to another requestid {requestid}", TakeScreenShotAtStep());
                int totalPatientPagesCountOnRS = rOIFacilityRequestStatusPage.GetTotalPatientPagesCount();
                int requestPagesCountOnRS = rOIFacilityRequestStatusPage.GetRequestPagesCountOnRs();
                int totalPatientAndRequestPagesCountOnRS = totalPatientPagesCountOnRS + requestPagesCountOnRS;
                logger.Log(Status.Info, $"Total patient docs count and request pages count on request status page is ({totalPatientAndRequestPagesCountOnRS})");
                int totalPatientPagesCountOnViewDoc = rOIFacilityRequestStatusPage.ClickViewDocumnetAndReturnPatientDocumentsCount();
                int requestPagesCountOnViewDoc = rOIFacilityRequestStatusPage.ReturnRequestDocumentsCount();
                int totalRequestAndPatientPagesCountOnViewDoc = rOIFacilityRequestStatusPage.ReturnTotalPatientAndRequestDocumentsCount();
                logger.Log(Status.Info, $"Total Request and patient pages docs count on view document ({totalRequestAndPatientPagesCountOnViewDoc})");
                Assert.AreEqual(totalPatientPagesCountOnRS, totalPatientPagesCountOnViewDoc, $"Patient documents on request status page ({totalPatientPagesCountOnRS}) and view documents ({totalPatientPagesCountOnViewDoc}) are not same");
                logger.Log(Status.Pass, $"Verified patient docs on request status page ({totalPatientPagesCountOnRS}) and view documents page ({totalPatientPagesCountOnViewDoc})");
                Assert.AreEqual(requestPagesCountOnRS, requestPagesCountOnViewDoc, $"request documents on request status page ({requestPagesCountOnRS}) and view documents ({requestPagesCountOnViewDoc}) are not same");
                logger.Log(Status.Pass, $"Verified request docs on request status page ({requestPagesCountOnRS}) and view documents page ({requestPagesCountOnViewDoc})");
                Assert.AreEqual(totalPatientAndRequestPagesCountOnRS, totalRequestAndPatientPagesCountOnViewDoc, $"total Patient and request documents on request status page ({totalPatientAndRequestPagesCountOnRS}) and view documents ({totalRequestAndPatientPagesCountOnViewDoc}) are not same");
                logger.Log(Status.Pass, $"Verified total request and patient docs on request status page ({totalPatientAndRequestPagesCountOnRS}) and view documents page ({totalRequestAndPatientPagesCountOnViewDoc})");
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
