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
using static MRO.ROI.Automation.Utility.IniFile;

namespace MRO.ROI.Test.RegressionTests.MROROITests
{
    [TestClass]
    public class ROIBugPDFFileSizeLinkingRequestsTest : ROIBaseTest
    {
        public ROIBugPDFFileSizeLinkingRequestsTest() : base(ROITestArea.ROIAdmin)
        {
        }

        [STATestMethodAttribute]
        [TestCategory(ROITestCategory.Regression)]
        // Converted manual test case 14298-ROI-Admin-->Test Case 14298: Bug - PDF file size (Linking requests) to automated.
        public void Reg_14298_ROIBugPDFFileSizeLinkingRequestsTest()
        {
            logger = extent.CreateTest("Reg_14298_ROIBugPDFFileSizeLinkingRequestsTest");
            logger.Log(Status.Info, "Converted manual test case 14298-ROI-Admin-->Test Case 14298: Bug - PDF file size (Linking requests) to automated.");
            RemoteWebDriver _driver = Init(TestContext.Properties["browser"].ToString());
            ThreadLocal<RemoteWebDriver> localDriver = new ThreadLocal<RemoteWebDriver>();
            localDriver.Value = _driver;
            RemoteWebDriver driver = localDriver.Value;


            try
            {
                ROIAdminHomePage rOIAdminHomePage = new ROIAdminHomePage(driver, logger, TestContext);
                rOIAdminHomePage.SelectFacilityList();
                ROIAdminFacilityListPage rOIAdminFacilityListPage = new ROIAdminFacilityListPage(driver, logger, TestContext);
                rOIAdminFacilityListPage.ClickOnComputerIconROINativePdfTestFacility();

                ROIFacilityWorkSummaryPage rOIFacilityWorkSummaryPage = new ROIFacilityWorkSummaryPage(driver, logger, TestContext);
                rOIFacilityWorkSummaryPage.logaNewRequest();
                ROIFacilityLogNewRequestPage rOIFacilityLogNewRequestPage = new ROIFacilityLogNewRequestPage(driver, logger, TestContext);
                rOIFacilityLogNewRequestPage.ClickMRODeliveryTab();

                rOIFacilityLogNewRequestPage.CreateRoiNativePdfTestDeliveryRequestWithoutScan();
                ROIFacilityRequestStatusPage rOIFacilityRequestStatusPage = new ROIFacilityRequestStatusPage(driver, logger, TestContext);
                string requestid = rOIFacilityRequestStatusPage.GetRequestID();
                logger.Log(Status.Info, $"MRO request created with ({requestid})", TakeScreenShotAtStep());
                rOIAdminHomePage.ROIlookupByRequestId(requestid);
                rOIFacilityRequestStatusPage.ImportFilesForSpecificPdfs();


                bool statusUnderImportDocument = rOIFacilityRequestStatusPage.VerifyStatusUnderImportDocument();
                Assert.IsTrue(statusUnderImportDocument, "Failed to verify status under import document is uploaded");
                int _patientPagesCountOnRSS = rOIFacilityRequestStatusPage.GetPatientPagesCount();
                logger.Log(Status.Pass, $"Verified that improt documents status is uploaded and patient documents count is :{(_patientPagesCountOnRSS)}", TakeScreenShotAtStep());

                rOIFacilityRequestStatusPage.ClickAddComponent();
                ROIFacilityAddComponentPage rOIFacilityAddComponentPage = new ROIFacilityAddComponentPage(driver, logger, TestContext);
                rOIFacilityAddComponentPage.AddComponentWithType("testone", "Test1");

                rOIFacilityRequestStatusPage.ImportPDFFileForAddComponent();
                logger.Log(Status.Info, "Verified that pdf uploaded for testone component", TakeScreenShotAtStep());

                rOIFacilityRequestStatusPage.ClickAddComponent();
                rOIFacilityAddComponentPage.AddComponentWithType("testone", "Test2");
                rOIFacilityRequestStatusPage.ImportPDFFileForAddComponentWithTwoPdfs();
                logger.Log(Status.Info, "Verified that status under import documents are uploaded ", TakeScreenShotAtStep());

                //rOIFacilityRequestStatusPage.ClickOnViewDocuments();
                int _totalPatientPagesCountOnViewDoc = rOIFacilityRequestStatusPage.ClickViewDocumnetAndReturnPatientDocumentsCount();
                int _requestPagesCountOnViewDoc = rOIFacilityRequestStatusPage.ReturnRequestDocumentsCount();
                int _totalRequestAndPatientPagesCountOnViewDoc = rOIFacilityRequestStatusPage.ReturnTotalPatientAndRequestDocumentsCount();

                logger.Log(Status.Pass, $"Verified that all the uploaded files are visible and total patient pages are{(_totalPatientPagesCountOnViewDoc)} and request pages are {(_requestPagesCountOnViewDoc)} and total pages are {(_totalRequestAndPatientPagesCountOnViewDoc)}");
                logger.Log(Status.Info, "Remaining steps are related to Database , so  need to do it manually");

                Cleanup(driver);

            }

            catch (Exception ex)
            {

                LogException(driver, logger, ex);

            }
        }
    }

}

