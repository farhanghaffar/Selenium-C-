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

namespace MRO.ROI.Test.RegressionTests.MROROITests
{
    [TestClass]
    public class TATReportEnhancementAdminTest: ROIBaseTest
    {
    public TATReportEnhancementAdminTest() : base(ROITestArea.ROIAdmin)
    {
    }
    [STATestMethodAttribute]
    [TestCategory(ROITestCategory.Regression)]
    //Converted manual test case 1412-ROI-Admin--> TAT report enhancement Admin to automated.
    public void Reg_1412_TATReportEnhancementAdminTest()
    {
        logger = extent.CreateTest($"Reg_1412_TATReportEnhancementAdminTest");
        logger.Log(Status.Info, "Converted manual test case 1412-ROI-Admin--> TAT report enhancement Admin to automated");
        RemoteWebDriver _driver = Init(TestContext.Properties["browser"].ToString());
        ThreadLocal<RemoteWebDriver> localDriver = new ThreadLocal<RemoteWebDriver>();
        localDriver.Value = _driver;
        RemoteWebDriver driver = localDriver.Value;
        string userRoot = System.Environment.GetEnvironmentVariable("USERPROFILE");
        string downloadFolder = Path.Combine(userRoot, "Downloads\\");
        string fileOne = string.Empty;
        string fileTwo = string.Empty;
        try
        {
                ROIAdminTurnaroundReportPage rOIAdminTurnaroundReportPage = new ROIAdminTurnaroundReportPage(driver, logger, TestContext);

                ROIMenuSelector menuSelector = new ROIMenuSelector(driver, logger, TestContext);
                menuSelector.SelectRoiAdminMenuOptions("mnuROIAdmin", "Reports", "Turnaround Report");
                bool visibilityStatus = rOIAdminTurnaroundReportPage.GetTheVisibilityStatusOfUserTypeDropdown();
                Assert.AreEqual(true, visibilityStatus,"Usertype dropdown is in disabled state");
                logger.Log(Status.Info, "Verified that Usertype dropdown is in disabled state", TakeScreenShotAtStep());

                rOIAdminTurnaroundReportPage.ApplyFiltersAndCreateReport();
                logger.Log(Status.Info, "Report created with filters", TakeScreenShotAtStep());

                ExcelReaderFile excelReaderFile = new ExcelReaderFile();
                ROIAdminSavedReportsPage rOIAdminSavedReportsPage = new ROIAdminSavedReportsPage(driver, logger, TestContext);
                rOIAdminSavedReportsPage.ClickOnRefresh();                 
                rOIAdminSavedReportsPage.ClickOnSavedReportLink();               
                fileOne = rOIAdminTurnaroundReportPage.ReadXLSFileFromDirectory(downloadFolder);
               
                string excelMonthAndYear = excelReaderFile.ReadExcelCellData(downloadFolder + fileOne, 11, 1);
                logger.Log(Status.Pass, "Verified that excel contains valid data");
                menuSelector.SelectRoiAdminMenuOptions("mnuROIAdmin", "Reports", "Turnaround Report");
                bool vStatus = rOIAdminTurnaroundReportPage.GetTheStatusOfUserTypeDropdown();
                Assert.AreEqual(false, vStatus, "Usertype dropdown is in enabled state");
                logger.Log(Status.Pass, "Verified that Usertype dropdown is in enabled state",TakeScreenShotAtStep());
                rOIAdminTurnaroundReportPage.ReapplyFiltersAndCreateReport();
                logger.Log(Status.Info, "Report created with new filters", TakeScreenShotAtStep());
                rOIAdminSavedReportsPage.ClickOnRefresh();
                rOIAdminSavedReportsPage.ClickOnSavedReportLink();
                fileTwo = rOIAdminTurnaroundReportPage.ReadXLSFileFromDirectory(downloadFolder);
                string sExcelMonthAndYear = excelReaderFile.ReadExcelCellData(downloadFolder + fileTwo, 12, 1);
                logger.Log(Status.Pass, "Verified that excel contains valid data");
                LoginPage loginPage = new LoginPage(driver, logger, TestContext);
                loginPage.LogOut();
                Cleanup(driver);
            }
             catch (Exception ex)
             {
                ExcelReaderFile.DeleteExistingFiles(downloadFolder, "xlsx", fileTwo);
                ExcelReaderFile.DeleteExistingFiles(downloadFolder, "xlsx", fileTwo);
                LogException(driver, logger, ex);
             }

    }
}
}

