using System;
using AventStack.ExtentReports;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MRO.ROI.Automation.Pages;
using MRO.ROI.Automation.Pages.Common;
using MRO.ROI.Automation.Selenium;
using MRO.ROI.Test.ExecutionFactory;
using MRO.ROI.Test.Utilities;
using OpenQA.Selenium.Remote;
using System.Threading;


namespace MRO.ROI.Test.RegressionTests.MROROITests
{
    [TestClass]
    public class ROIFacilityImportDocumentsScreenUpdateTest:ROIBaseTest
    {
        public ROIFacilityImportDocumentsScreenUpdateTest() : base(ROITestArea.ROIAdmin)
        {
        }
        [STATestMethodAttribute]
        [TestCategory(ROITestCategory.Passed)]
        //Converted manual test case 2583-ROI-Facility-->Browse to PDF - Import Documents screen update to automated.
        public void Reg_2583_ROIFacilityImportDocumentsScreenUpdateTest()
        {
            logger = extent.CreateTest("Reg_2583_ROIFacilityImportDocumentsScreenUpdateTest");
            logger.Log(Status.Info, "Converted manual test case 2583-ROI-Facility-->Browse to PDF-Import Documents screen update to automated");
            RemoteWebDriver _driver = Init(TestContext.Properties["browser"].ToString());
            ThreadLocal<RemoteWebDriver> localDriver = new ThreadLocal<RemoteWebDriver>();
            localDriver.Value = _driver;
            RemoteWebDriver driver = localDriver.Value;
            string requestID = string.Empty;
            int importDocsCount = 0;
            string sErrorMsg = string.Empty;
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
                rOIFacilityLogNewRequestPage.CreateNewMRODeliveryRequestForBostonProper();
                ROIFacilityRequestStatusPage rOIFacilityRequestStatusPage = new ROIFacilityRequestStatusPage(driver, logger, TestContext);
                string requestid = rOIFacilityRequestStatusPage.GetRequestID();
                logger.Log(Status.Pass, $"Request created with requestid({requestid})", TakeScreenShotAtStep());
                rOIAdminHomePage.ROIlookupByRequestId(requestid);
                rOIFacilityRequestStatusPage.SelectDocumentsAndVerifyStatus();
                rOIFacilityRequestStatusPage.RemoveLinksForPatientDocuments();
                rOIFacilityRequestStatusPage.SelectRequestDocumentsAndCancel();
                importDocsCount = rOIFacilityRequestStatusPage.GetRowCountFromImportDocumentsTable();
                Assert.AreEqual(1, importDocsCount, "Failed to validate the uploaded docs count from Import Documents Table");
                logger.Log(Status.Pass, "Verified that no new files uploaded, and the files name are not visible under (Import Documents) section", TakeScreenShotAtStep());
                rOIFacilityRequestStatusPage.UploadDocumentsAndVerifyStatus();
                logger.Log(Status.Pass, "Verified that all files are uploaded, and the files name are visible under (Import Documents) section", TakeScreenShotAtStep());
                sErrorMsg = rOIFacilityRequestStatusPage.UploadDuplicatePatientPagesAndGetErrorMessage();
                Assert.AreEqual("Duplicate", sErrorMsg, "Failed to validate the error message from Import Documents window");
                logger.Log(Status.Pass, "Verified that Duplicate error message is visible while uploading duplicate pdf files");
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