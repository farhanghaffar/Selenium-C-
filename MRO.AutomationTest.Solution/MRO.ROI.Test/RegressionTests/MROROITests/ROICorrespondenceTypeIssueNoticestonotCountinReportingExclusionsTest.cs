using AventStack.ExtentReports;
using DataDrivenProject;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MRO.ROI.Automation.Pages;
using MRO.ROI.Automation.Pages.Common;
using MRO.ROI.Test.ExecutionFactory;
using MRO.ROI.Test.Utilities;
using OpenQA.Selenium.Remote;
using System;
using System.IO;
using System.Threading;
namespace MRO.ROI.Test.RegressionTests.MROROITests
{
    [TestClass]
    public class ROICorrespondenceTypeIssueNoticestonotCountinReportingExclusionsTest : ROIBaseTest
    {
        public ROICorrespondenceTypeIssueNoticestonotCountinReportingExclusionsTest() : base(ROITestArea.ROITestFacility)
        {

        }
        [STATestMethodAttribute]
        [TestCategory(ROITestCategory.Regression)]
        public void Reg_4879_ROICorrespondenceTypeIssueNoticestonotCountinReportingExclusionsTest()
        {
            logger = extent.CreateTest("Reg_4879_ROICorrespondenceTypeIssueNoticestonotCountinReportingExclusionsTest");
            logger.Log(Status.Info, "Converted manual test case 4879-ROICorrespondenceTypeIssueNoticestonotCountinReportingExclusionsTest");
            RemoteWebDriver _driver = Init(TestContext.Properties["browser"].ToString());
            ThreadLocal<RemoteWebDriver> localDriver = new ThreadLocal<RemoteWebDriver>();
            localDriver.Value = _driver;
            RemoteWebDriver driver = localDriver.Value;
            string userRoot = System.Environment.GetEnvironmentVariable("USERPROFILE");
            string downloadFolder = Path.Combine(userRoot, "Downloads\\");

            try
            {
                ExcelReaderFile.DeleteExistingFiles(downloadFolder, "xlsx", "Turnaround Report Detail");
                ROIFacilityWorkSummaryPage rOIFacilityWorkSummaryPage = new ROIFacilityWorkSummaryPage(driver, logger, TestContext);
                rOIFacilityWorkSummaryPage.GoToMROAnalyseSelectTurnAroundReport();
                ROIFacilityTurnaroundReportPage rOIFacilityTurnaroundReportPage = new ROIFacilityTurnaroundReportPage(driver, logger, TestContext);
                rOIFacilityTurnaroundReportPage.ApplyFiltersWithReleaseOptionAndCreateReport();
                logger.Log(Status.Info, "Verified User Created Report ", TakeScreenShotAtStep());
                ROIAdminHomePage rOIAdminHomePage = new ROIAdminHomePage(driver, logger, TestContext);

                ROIAdminRequestStatusPage rOIAdminRequestStatusPage = new ROIAdminRequestStatusPage(driver, logger, TestContext);

                ROIFacilityRequestStatusPage rOIFacilityRequestStatusPage = new ROIFacilityRequestStatusPage(driver, logger, TestContext);
                rOIFacilityTurnaroundReportPage.ClickOnTotalRequestsDelivered();
                rOIFacilityTurnaroundReportPage.ClickAnyPatientLink();
                string requestid = rOIFacilityRequestStatusPage.GetRequestID();
                logger.Log(Status.Info, "Verified that Request ID is Captured ", TakeScreenShotAtStep());

                rOIAdminHomePage.SwitchToNewTabAndLoginROIAdmin(BaseAddress);
                rOIAdminHomePage.ROIlookupByRequestId(requestid);

                rOIAdminRequestStatusPage.ClickOnCorrespondence();
                rOIAdminRequestStatusPage.AddCoresspondenceIssueNo("Ammended Request", "ammend");
                logger.Log(Status.Info, "Correspondance added under issue section", TakeScreenShotAtStep());

                rOIAdminHomePage.SwitchToROITestFacilitySide(BaseAddress);
                rOIFacilityWorkSummaryPage.GoToMROAnalyseSelectTurnAroundReport();
                logger.Log(Status.Info, "Verified TurnAroundReport is Generated ", TakeScreenShotAtStep());
                rOIFacilityTurnaroundReportPage.ApplyFiltersWithReleaseOptionAndCreateReport();

                ROIFacilityRequestStatusPage rOIFacilityRequestStatusPages = new ROIFacilityRequestStatusPage(driver, logger, TestContext);
                rOIFacilityTurnaroundReportPage.ClickOnTotalRequestsDelivered();
                rOIFacilityTurnaroundReportPage.ClickAnyPatientLink();
                string requestids = rOIFacilityRequestStatusPage.GetRequestID();
                Assert.AreEqual(requestids, requestid, "Requestid does not exist under Total Requests Delivered view");
                logger.Log(Status.Pass, "Verified requestid exist under Total Requests Delivered view ", TakeScreenShotAtStep());

                rOIAdminHomePage.SwitchToNewTabAndLoginROIAdmin(BaseAddress);
                rOIAdminHomePage.ROIlookupByRequestId(requestid);
                rOIAdminRequestStatusPage.ClickOnCorrespondence();
                rOIAdminRequestStatusPage.AddCoresspondenceIssueNo("Incomplete Records", "this is a test");
                logger.Log(Status.Pass, "Correspondance added under issue section", TakeScreenShotAtStep());

                rOIAdminHomePage.SwitchToROITestFacilitySide(BaseAddress);
                rOIFacilityWorkSummaryPage.GoToMROAnalyseSelectTurnAroundReport();
                rOIFacilityTurnaroundReportPage.ApplyFiltersWithReleaseOptionAndCreateReport();
                rOIFacilityTurnaroundReportPage.ClickOnTotalRequestsDelivered();

                rOIFacilityTurnaroundReportPage.DownloadExcelReport();
                string excelFileName = downloadFolder + "Turnaround Report Detail.xlsx";
                ExcelReaderFile excelReaderFile = new ExcelReaderFile(excelFileName);
                int lastRowNumber = excelReaderFile.getRowCount("Sheet1");
                Assert.IsTrue(rOIFacilityTurnaroundReportPage.VerifyRequestIdDoesNotExist(requestid,lastRowNumber,excelReaderFile,excelFileName));
                logger.Log(Status.Pass, "Verified that the request that have added the correspondence to is not visible in the report", TakeScreenShotAtStep());
                ExcelReaderFile.DeleteExistingFiles(downloadFolder, "xlsx", "Turnaround Report Detail");
                Cleanup(driver);
            }
            catch (Exception ex)
            {
                ExcelReaderFile.DeleteExistingFiles(downloadFolder, "xlsx", "Turnaround Report Detail");
                LogException(driver, logger, ex);
            }
        }
    }
}

