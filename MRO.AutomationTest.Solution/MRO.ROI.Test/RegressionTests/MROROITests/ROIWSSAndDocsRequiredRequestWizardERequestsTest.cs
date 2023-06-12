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
    public class ROIWSSAndDocsRequiredRequestWizardERequestsTest : ROIBaseTest
    {
        public ROIWSSAndDocsRequiredRequestWizardERequestsTest() : base(ROITestArea.ROIAdmin)
        {
        }

        [STATestMethodAttribute]
        [TestCategory(ROITestCategory.Regression)]
        // Converted manual test case 4750-ROI-Admin-->Test Case 4750:WSS and Docs Required - Request Wizard E-Requeststo automated.
        public void Reg_4750_ROIWSSAndDocsRequiredRequestWizardERequestsTest()
        {
            logger = extent.CreateTest("Reg_4750_ROIWSSAndDocsRequiredRequestWizardERequestsTest");
            logger.Log(Status.Info, "Converted manual test case 4750-ROI-Admin-->Test Case 4750:WSS and Docs Required - Request Wizard E-Requeststo automated.");
            RemoteWebDriver _driver = Init(TestContext.Properties["browser"].ToString());
            ThreadLocal<RemoteWebDriver> localDriver = new ThreadLocal<RemoteWebDriver>();
            localDriver.Value = _driver;
            RemoteWebDriver driver = localDriver.Value;
            string userRoot = System.Environment.GetEnvironmentVariable("USERPROFILE");
            string downloadFolder = Path.Combine(userRoot, "Downloads\\");


            try
            {
                ExcelReaderFile.DeleteExistingFiles(downloadFolder, "xlsx", "DocumentRequired");
                ExcelReaderFile.DeleteExistingFiles(downloadFolder, "xls", "DocumentRequired");

                ROIAdminHomePage rOIAdminHomePage = new ROIAdminHomePage(driver, logger, TestContext);
                rOIAdminHomePage.SelectFacilityList();
                ROIAdminFacilityListPage rOIAdminFacilityListPage = new ROIAdminFacilityListPage(driver, logger, TestContext);
                rOIAdminFacilityListPage.ClickOnUniversalHealthSystemCompIcon();
                logger.Log(Status.Info, "Verified that Universal Health Services, Inc facility page opened successfully", TakeScreenShotAtStep());

                ROIFacilityWorkSummaryPage rOIFacilityWorkSummaryPage = new ROIFacilityWorkSummaryPage(driver, logger, TestContext);
                rOIFacilityWorkSummaryPage.ClickOnDocsRequired();

                ROIAdminDocumentsRequiredPage rOIAdminDocumentsRequiredPage = new ROIAdminDocumentsRequiredPage(driver, logger, TestContext);
                rOIAdminDocumentsRequiredPage.ClickOnWizardRequests();
                rOIAdminDocumentsRequiredPage.CreateReport();               

                rOIAdminDocumentsRequiredPage.ClickOnExportToExcelIcon();
                ExcelReaderFile excelReaderFile = new ExcelReaderFile();
                excelReaderFile.ConvertXLS_XLSX(downloadFolder + "DocumentRequired.xls");
                ExcelReaderFile.DeleteExistingFiles(downloadFolder, "xls", "DocumentRequired");
                driver.Wait(TimeSpan.FromSeconds(2));
                logger.Log(Status.Info, "Verified that excel sheet downloaded sucessfully", TakeScreenShotAtStep());
                ExcelReaderFile.DeleteExistingFiles(downloadFolder, "xlsx", "DocumentRequired");

                rOIAdminHomePage.OpenNewTabAndLoginROITestFacility(BaseAddress);
                ROIFacilityDocumentsRequiredPage rOIFacilityDocumentsRequiredPage = new ROIFacilityDocumentsRequiredPage(driver, logger, TestContext);
                rOIFacilityWorkSummaryPage.ClickOnDocsRequired();
                rOIFacilityDocumentsRequiredPage.ClickOnAll();
                rOIFacilityDocumentsRequiredPage.CreateReport("Other Billable");
                string type = rOIFacilityDocumentsRequiredPage.VerifyRqrType();
                Assert.AreEqual(type, "Other Billable");
                logger.Log(Status.Pass, $"Verified that report returns data with rqr type:({type})", TakeScreenShotAtStep());

                rOIFacilityDocumentsRequiredPage.ClickOnExportToExcelIcon();
                ExcelReaderFile excelReaderFile1 = new ExcelReaderFile();
                excelReaderFile1.ConvertXLS_XLSX(downloadFolder + "DocumentRequired.xls");
                ExcelReaderFile.DeleteExistingFiles(downloadFolder, "xls", "DocumentRequired");
                driver.Wait(TimeSpan.FromSeconds(2));
                logger.Log(Status.Pass, "Verified that excel sheet downloaded sucessfully", TakeScreenShotAtStep());

                rOIFacilityDocumentsRequiredPage.CreateReport("Patient");
                string type1 = rOIFacilityDocumentsRequiredPage.VerifyRqrType();
                Assert.AreEqual(type1, "Patient");
                logger.Log(Status.Pass, $"Verified that report returns data with rqr type:({type1})", TakeScreenShotAtStep());

                rOIFacilityDocumentsRequiredPage.CreateReport("Legal");
                string type2 = rOIFacilityDocumentsRequiredPage.VerifyRqrType();
                Assert.AreEqual(type2, "Legal");
                logger.Log(Status.Info, $"Verified that report returns data with rqr type:({type2})", TakeScreenShotAtStep());

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

