using AventStack.ExtentReports;
using AventStack.ExtentReports.MarkupUtils;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MRO.ROI.Automation.Pages;
using MRO.ROI.Automation.Selenium;
using MRO.ROI.Automation.Utility;
using MRO.ROI.Test.ExecutionFactory;
using MRO.ROI.Test.Utilities;
using OpenQA.Selenium.Remote;
using System;
using System.IO;
using System.Reflection;
using System.Threading;

namespace MRO.ROI.Test.RegressionTests.MROROITests
{
    [TestClass]
    public class ROIFacilityLinkedRequestWithMultipeCompTest: ROIBaseTest
    {
        public static string csvFileName = "ROIFacilityLinkedRequestWithMultipeComp.csv";
        public ROIFacilityLinkedRequestWithMultipeCompTest() : base(ROITestArea.ROIFacility)
        {
        }


        [STATestMethodAttribute]
        [TestCategory(ROITestCategory.Passed)]
        // Converted manual test case 11490-ROI-Facility-->Native PDF - B2P Issues (Linked Requests with multiple comp.) to automated.
        public void Reg_11490_LinkedRequestsWithMultipleComp()
        {
            logger = extent.CreateTest("Reg_11490_LinkedRequestsWithMultipleComp");
            logger.Log(Status.Info, "Converted manual test case 11490-ROI-Facility-->Native PDF - B2P Issues (Linked Requests with multiple comp.) to automated.");
            RemoteWebDriver _driver = Init(TestContext.Properties["browser"].ToString());
            ThreadLocal<RemoteWebDriver> localDriver = new ThreadLocal<RemoteWebDriver>();
            localDriver.Value = _driver;
            RemoteWebDriver driver = localDriver.Value;
            try
            {
                ROIFacilityWorkSummaryPage rOIFacilityWorkSummaryPage = new ROIFacilityWorkSummaryPage(driver, logger, TestContext); 
                rOIFacilityWorkSummaryPage.GoToLogNewRequestPage();
                ROIFacilityLogNewRequestPage rOIFacilityLogNewRequestPage = new ROIFacilityLogNewRequestPage(driver, logger, TestContext);
                rOIFacilityLogNewRequestPage.CreateNewMRODeliveryRequestWithoutScan();
                ROIFacilityRequestStatusPage rOIFacilityRequestStatusPage = new ROIFacilityRequestStatusPage(driver, logger, TestContext);
                ROIAdminHomePage rOIAdminHomePage = new ROIAdminHomePage(driver, logger, TestContext);
                string requestid= rOIFacilityRequestStatusPage.GetRequestID();
                logger.Log(Status.Info, $"MRO delivery request created with id ({requestid})",TakeScreenShotAtStep());
                rOIAdminHomePage.ROIlookupByRequestId(requestid);
                rOIFacilityRequestStatusPage.ImportPdfFiles();
                rOIFacilityRequestStatusPage.ClickAddComponent();
                ROIFacilityAddComponentPage rOIFacilityAddComponentPage = new ROIFacilityAddComponentPage(driver, logger, TestContext);
                CSVReader csvReader = new CSVReader(Path.GetFullPath(Path.Combine(Assembly.GetExecutingAssembly().Location, "..", "TestData", csvFileName)));
                rOIFacilityAddComponentPage.AddComponent(csvReader.GetDataFromCSVFile("Description1"));
                logger.Log(Status.Info, $"Component added with {csvReader.GetDataFromCSVFile("Description1")}");
                rOIFacilityRequestStatusPage.ImportMROTestPDFFiles();
                rOIAdminHomePage.ROIlookupByRequestId(requestid);
                rOIFacilityRequestStatusPage.ClickAddComponent();                
                rOIFacilityAddComponentPage.AddComponent(csvReader.GetDataFromCSVFile("Description2"));
                logger.Log(Status.Info, $"Component added with {csvReader.GetDataFromCSVFile("Description2")}");
                rOIFacilityRequestStatusPage.ImportMROTestPDFFiles1();                
                rOIAdminHomePage.ROIlookupByRequestId(requestid);               
                int _totalPatientPagesCountOnRS = rOIFacilityRequestStatusPage.GetTotalPatientPagesCount();
                int _RequestPagesCountOnRS = rOIFacilityRequestStatusPage.GetRequestPagesCount();
                int _totalPatientAndRequestPagesCountOnRS = _totalPatientPagesCountOnRS + _RequestPagesCountOnRS;
                int _totalPatientPagesCountOnViewDoc = rOIFacilityRequestStatusPage.ClickViewDocumnetAndReturnPatientDocumentsCount();
                int _requestPagesCountOnViewDoc = rOIFacilityRequestStatusPage.ReturnRequestDocumentsCount();
                int _totalRequestAndPatientPagesCountOnViewDoc = rOIFacilityRequestStatusPage.ReturnTotalPatientAndRequestDocumentsCount();
                Assert.AreEqual(_totalPatientPagesCountOnRS, _totalPatientPagesCountOnViewDoc, $"Patient documents on request status page ({_totalPatientPagesCountOnRS}) and view documents ({_totalPatientPagesCountOnViewDoc}) are not same");
                logger.Log(Status.Pass, $"Verified patient docs on request status page ({_totalPatientPagesCountOnRS}) and view documents page ({_totalPatientPagesCountOnViewDoc})");
                Assert.AreEqual(_RequestPagesCountOnRS, _requestPagesCountOnViewDoc, $"request documents on request status page ({_RequestPagesCountOnRS}) and view documents ({_requestPagesCountOnViewDoc}) are not same");
                logger.Log(Status.Pass, $"Verified request docs on request status page ({_RequestPagesCountOnRS}) and view documents page ({_requestPagesCountOnViewDoc})");
                Assert.AreEqual(_totalPatientAndRequestPagesCountOnRS, _totalRequestAndPatientPagesCountOnViewDoc, $"total Patient and request documents on request status page ({_totalPatientAndRequestPagesCountOnRS}) and view documents ({_totalRequestAndPatientPagesCountOnViewDoc}) are not same");
                logger.Log(Status.Pass, $"Verified total request and patient docs on request status page ({_totalPatientAndRequestPagesCountOnRS}) and view documents page ({_totalRequestAndPatientPagesCountOnViewDoc})");
                
                rOIFacilityWorkSummaryPage.GoToLogNewRequestPage();
                rOIFacilityLogNewRequestPage.CreateDuplicateMRODeliveryRequest();                
                string duplicateRequestid = rOIFacilityRequestStatusPage.GetRequestID();
                logger.Log(Status.Info, $"Another request created with id ({duplicateRequestid})",TakeScreenShotAtStep());                
                rOIFacilityRequestStatusPage.ImportPdfFiles();                
                rOIAdminHomePage.ROIlookupByRequestId(duplicateRequestid);
                rOIFacilityRequestStatusPage.ClickOnLinkToAnotherRequest();
                ROIFacilityLinkToAnotherRequestPage rOIFacilityLinkToAnotherRequestPage = new ROIFacilityLinkToAnotherRequestPage(driver, logger, TestContext);
                rOIFacilityLinkToAnotherRequestPage.LinkToAnotherRequestPage_MultiComp(requestid);
                bool linkedToAnotherRequest = rOIFacilityRequestStatusPage.VerifyLinkedToRequestToAnotherRequest();          
                Assert.IsTrue(linkedToAnotherRequest, "Failed to link to another request");
                logger.Log(Status.Info,$"Request ({duplicateRequestid}) linked with another request ({requestid})", TakeScreenShotAtStep());
                int totalPatientPagesCountOnRS = rOIFacilityRequestStatusPage.GetTotalPatientPagesCount();
                int requestPagesCountOnRS = rOIFacilityRequestStatusPage.GetRequestPagesCount();
                int totalPatientAndRequestPagesCountOnRS = totalPatientPagesCountOnRS + requestPagesCountOnRS;
                int totalPatientPagesCountOnViewDoc = rOIFacilityRequestStatusPage.ClickViewDocumnetAndReturnPatientDocumentsCount();
                int requestPagesCountOnViewDoc = rOIFacilityRequestStatusPage.ReturnRequestDocumentsCount();
                int totalRequestAndPatientPagesCountOnViewDoc = rOIFacilityRequestStatusPage.ReturnTotalPatientAndRequestDocumentsCount();
                Assert.AreEqual(totalPatientPagesCountOnRS, totalPatientPagesCountOnViewDoc, $"Patient documents on request status page ({totalPatientPagesCountOnRS}) and view documents ({totalPatientPagesCountOnViewDoc}) are not same");
                logger.Log(Status.Pass, $"Verified patient docs on request status page ({totalPatientPagesCountOnRS}) and view documents page ({totalPatientPagesCountOnViewDoc})");
                Assert.AreEqual(requestPagesCountOnRS, requestPagesCountOnViewDoc, $"request documents on request status page ({requestPagesCountOnRS}) and view documents ({requestPagesCountOnViewDoc}) are not same");
                logger.Log(Status.Pass, $"Verified request docs on request status page ({requestPagesCountOnRS}) and view documents page ({requestPagesCountOnViewDoc})");
                Assert.AreEqual(totalPatientAndRequestPagesCountOnRS, totalRequestAndPatientPagesCountOnViewDoc, $"total Patient and request documents on request status page ({totalPatientAndRequestPagesCountOnRS}) and view documents ({totalRequestAndPatientPagesCountOnViewDoc}) are not same");
                logger.Log(Status.Pass, $"Verified total request and patient docs on request status page ({totalPatientAndRequestPagesCountOnRS}) and view documents page ({totalRequestAndPatientPagesCountOnViewDoc})");
                Cleanup(driver);
            }
            catch (Exception ex)
            {
                LogException(driver, logger, ex);
            }
        }
    }
}
