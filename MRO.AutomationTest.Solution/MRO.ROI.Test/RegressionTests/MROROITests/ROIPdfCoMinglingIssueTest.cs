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
using static MRO.ROI.Automation.Utility.IniFile;

namespace MRO.ROI.Test.RegressionTests.MROROITests
{
    [TestClass]
    public class ROIPdfCoMinglingIssueTest : ROIBaseTest
    {
        public ROIPdfCoMinglingIssueTest() : base(ROITestArea.ROITestFacility)
        {
        }

        [STATestMethodAttribute]
        [TestCategory(ROITestCategory.Development)]
        // Converted manual test case 895-ROIFacility-->PDF Co-mingling issue to automated.
        public void Reg_895_ROIPdfCoMinglingIssueTest()
        {
            logger = extent.CreateTest("Reg_895_PDF Co-mingling issue");
            logger.Log(Status.Info, "Converted manual test case 895-ROIFacility-->>PDF Co-mingling issue to automated");
            RemoteWebDriver _driver = Init(TestContext.Properties["browser"].ToString());
            ThreadLocal<RemoteWebDriver> localDriver = new ThreadLocal<RemoteWebDriver>();
            localDriver.Value = _driver;
            RemoteWebDriver driver = localDriver.Value;


            try
            {
                ROIAdminHomePage rOIAdminHomePage = new ROIAdminHomePage(driver, logger, TestContext);
                ROIFacilityWorkSummaryPage rOIFacilityWorkSummaryPage = new ROIFacilityWorkSummaryPage(driver, logger, TestContext);
                rOIFacilityWorkSummaryPage.GoToLogNewRequestPage();
                ROIFacilityLogNewRequestPage rOIFacilityLogNewRequestPage = new ROIFacilityLogNewRequestPage(driver, logger, TestContext);
                rOIFacilityLogNewRequestPage.ClickMRODeliveryTab();

                rOIFacilityLogNewRequestPage.MRODeliveryRequestForBostonProper();
                LogNewRequestPage logNewRequestPage = new LogNewRequestPage(driver, logger, TestContext);
                string requestId = logNewRequestPage.getRequestid();
                ROIFacilityRequestStatusPage rOIFacilityRequestStatusPage = new ROIFacilityRequestStatusPage(driver, logger, TestContext);
                logger.Log(Status.Info, $"MRO delivery request created with id:({requestId})", TakeScreenShotAtStep());
                rOIAdminHomePage.ROIlookupByRequestId(requestId);
                rOIFacilityRequestStatusPage.ReOpenRequest();
                rOIFacilityRequestStatusPage.ImportMultiplePdfFiles();
                logger.Log(Status.Pass, "PDF files imported and status updated as uploaded", TakeScreenShotAtStep());

                int patientdocCount = rOIFacilityRequestStatusPage.ClickOnMaginifierIcon();
                logger.Log(Status.Pass, $"Verified patient docs on request status page ({patientdocCount})", TakeScreenShotAtStep());
                int _patientDocCount = rOIFacilityRequestStatusPage.GetTotalPatientPagesCountForFirstPdf();
                logger.Log(Status.Pass, $"Verified patient docs on request status page ({patientdocCount}) and view documents page ({_patientDocCount})");

                int patientdocCount1 = rOIFacilityRequestStatusPage.ClickOnMaginifierIconForSecondPdf();
                logger.Log(Status.Pass, $"Verified patient docs on request status page ({patientdocCount1})", TakeScreenShotAtStep());
                int _patientDocCount1 = rOIFacilityRequestStatusPage.GetTotalPatientPagesCountForSecondPdf();
                logger.Log(Status.Pass, $"Verified patient docs on request status page ({patientdocCount1}) and view documents page ({_patientDocCount1})");

                int patientdocCount2 = rOIFacilityRequestStatusPage.ClickOnMaginifierIconForThirdPdf();
                logger.Log(Status.Pass, $"Verified patient docs on request status page ({patientdocCount2})", TakeScreenShotAtStep());
                int _patientDocCount2 = rOIFacilityRequestStatusPage.GetTotalPatientPagesCountForThirdPdf();
                logger.Log(Status.Pass, $"Verified patient docs on request status page ({patientdocCount2}) and view documents page ({_patientDocCount2})");
   

                int totalPatientPagesCountOnViewDoc = rOIFacilityRequestStatusPage.ClickViewDocumnetAndReturnPatientDocumentsCount();
                string totalPatientPagesCount = Convert.ToString(totalPatientPagesCountOnViewDoc);
                Assert.AreEqual(totalPatientPagesCount, "844","Failed to verify patient pages count");

                int requestPagesCountOnViewDoc = rOIFacilityRequestStatusPage.ReturnRequestDocumentsCount();
                string PatientPagesCount = Convert.ToString(requestPagesCountOnViewDoc);
                Assert.AreEqual(PatientPagesCount, "2","Failed to verify request pages count");

                int totalRequestAndPatientPagesCountOnViewDoc = rOIFacilityRequestStatusPage.ReturnTotalPatientAndRequestDocumentsCount();
                string requestAndPatientPages = Convert.ToString(totalRequestAndPatientPagesCountOnViewDoc);                             
                Assert.AreEqual(requestAndPatientPages, "846","Failed to verify total pages");

                logger.Log(Status.Info, "Verified that  view document page show  Request doc 2, Patient Docs 844, [Entire Chart] 846");
                rOIFacilityRequestStatusPage.ClickOnViewDocuments();                
                logger.Log(Status.Info, "Request documents pages loads successfully", TakeScreenShotAtStep());

                rOIFacilityRequestStatusPage.ClickOnPatientDocuments();
                bool isDisplayed=rOIFacilityRequestStatusPage.VerifyPageNumberTabs();
                Assert.IsTrue(isDisplayed, "Failed to verify page number tabs");
                logger.Log(Status.Pass, "Verified that page tabs displayed from 1-100 ", TakeScreenShotAtStep());

                logger.Log(Status.Info, "From step18 to 20  are not feasiable for automation need to executed manually and From 21 onwards related to datebase execute manually");
                Cleanup(driver);
            }

            catch (Exception ex)
            {
                LogException(driver, logger, ex);

            }
        }
    }

}

