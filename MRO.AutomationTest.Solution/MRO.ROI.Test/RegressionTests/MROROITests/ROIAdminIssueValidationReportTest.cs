using AventStack.ExtentReports;
using AventStack.ExtentReports.MarkupUtils;
using DataDrivenProject;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MRO.ROI.Automation.Common;
using MRO.ROI.Automation.Pages;
using MRO.ROI.Automation.Pages.Common;
using MRO.ROI.Automation.Selenium;
using MRO.ROI.Automation.Utility;
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
    public class ROIAdminIssueValidationReportTest : ROIBaseTest
    {
        public ROIAdminIssueValidationReportTest() : base(ROITestArea.ROIAdmin)
        {
        }

        [STATestMethodAttribute]
        [TestCategory(ROITestCategory.Regression)]
        // Converted manual test case 4897-ROI-Admin-->Admin - Issue Validation Report to automated.
        public void Reg_4897_ROIAdminIssueValidationReportTest()
        {
            logger = extent.CreateTest("Reg_4897_ROIAdminIssueValidationReportTest");
            logger.Log(Status.Info, " Converted manual test case 4897-ROI-Admin-->Admin - Issue Validation Report to automated.");
            RemoteWebDriver _driver = Init(TestContext.Properties["browser"].ToString());
            ThreadLocal<RemoteWebDriver> localDriver = new ThreadLocal<RemoteWebDriver>();
            localDriver.Value = _driver;
            RemoteWebDriver driver = localDriver.Value;
            string userRoot = System.Environment.GetEnvironmentVariable("USERPROFILE");
            string downloadFolder = Path.Combine(userRoot, "Downloads\\");

            try
            {
                ExcelReaderFile.DeleteExistingFiles(downloadFolder, "xlsx", "Issue Validation Report");
                ExcelReaderFile.DeleteExistingFiles(downloadFolder, "xls", "Issue Validation Report");
                Iframe frame = new Iframe(driver, logger, TestContext);

                string query = "delete from lnkUserFeatures where nfeatureid ='344' and nuserid in (select nUserID from tblROIFacilityUsers where susername ='CIGNITI-AKOTHURI')";
                bool boeReuqest = MRODBConnection.isUpdate(query);
                logger.Log(Status.Info, "Verified user has been deleted from report");

                ROIAdminHomePage rOIAdminHomePage = new ROIAdminHomePage(driver, logger, TestContext);
                rOIAdminHomePage.SelectFeatures();
                frame.SwitchToRoiFrame();
                ROIAdminFeaturesPage rOIAdminFeaturesPage = new ROIAdminFeaturesPage(driver, logger, TestContext);
                rOIAdminFeaturesPage.HideRequiresLicenseLable();
                logger.Log(Status.Info, "Verifying context set to Admin", TakeScreenShotAtStep());
                string contextVal = rOIAdminFeaturesPage.VerifySelectedContext();
                Assert.AreEqual(contextVal, "Admin");
                logger.Log(Status.Info, "Verified context set to Admin");

                logger.Log(Status.Info, "Verifying user added for issue validation report", TakeScreenShotAtStep());
                rOIAdminFeaturesPage.ClickOnIssueValidationReport();

                logger.Log(Status.Info, "Verify that the User had loaded with the feature name 'Issue Validation Report' visible on top of the page.", TakeScreenShotAtStep());
                bool isIssueValidationReportFeatureShowing = rOIAdminFeaturesPage.IsIssueValidationReportLableShowing();
                Assert.IsTrue(isIssueValidationReportFeatureShowing, "Failed : 'Issue Validation Report' feature is not shwoing");
                logger.Log(Status.Info, "Verified that the User had loaded with the feature name 'Issue Validation Report' visible on top of the page.");

                rOIAdminFeaturesPage.HideRequiresLicenseLable();
              //  rOIAdminFeaturesPage.AddIssueValidationReport("Add");
                rOIAdminFeaturesPage.AddAIDashboardAtAdminSide("Add");
                logger.Log(Status.Info, "Verified user added for issue validation report");
                rOIAdminFeaturesPage.HideRequiresLicenseLable();
                rOIAdminFeaturesPage.ClickOnCatagories();

                frame.switchToDefaut();
                driver.RefreshWebPage();
                driver.WaitInSeconds(5);
                rOIAdminHomePage.SelectIssueValidationReport();

                ROIAdminIssueValidationReportPage rOIAdminIssueValidationReportPage = new ROIAdminIssueValidationReportPage(driver, logger, TestContext);
                rOIAdminIssueValidationReportPage.CreateReportForIssueValidation();
                logger.Log(Status.Info, "Issue validation report created successfully", TakeScreenShotAtStep());
                logger.Log(Status.Pass, $"Verify that data exported and total requests logged", TakeScreenShotAtStep());

                rOIAdminIssueValidationReportPage.ClickOnExportToExcel();

                ExcelReaderFile excelReaderFile = new ExcelReaderFile();              
                string totalRequestsLoggedInExcel = excelReaderFile.ReadExcelCellData(downloadFolder + "Issue Validation Report.xlsx", 8, 2);
                string requestNumFromUI=rOIAdminIssueValidationReportPage.GetNumberOfRequestsLogged();
                Assert.AreEqual(requestNumFromUI, totalRequestsLoggedInExcel, "Failed to total logged requests"); ;
                logger.Log(Status.Pass, $"Successfully verified that data exported and total requests logged is:{(totalRequestsLoggedInExcel)}");
                Cleanup(driver);

                boeReuqest = MRODBConnection.isUpdate(query);
                logger.Log(Status.Info, "Verified user has been deleted from report");

            }

            catch (Exception ex)
            {
                ExcelReaderFile.DeleteExistingFiles(downloadFolder, "xlsx", "Issue Validation Report");
                ExcelReaderFile.DeleteExistingFiles(downloadFolder, "xls", "Issue Validation Report");
                LogException(driver, logger, ex);

            }
        }
    }

}

