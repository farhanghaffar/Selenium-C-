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

namespace MRO.ROI.Test.RegressionTests.MROROITests
{
    [TestClass]
    public class ROIAdminMROAnalyzeTurnAroundReportTest: ROIBaseTest
    {
        public ROIAdminMROAnalyzeTurnAroundReportTest() : base(ROITestArea.ROIAdmin)
        {
        }
        [STATestMethodAttribute]
        [TestCategory(ROITestCategory.Development)]
        // Converted manual test case 11919-ROI-Admin-->Infor #065719 - TAT Received to Shipped not working to automated.
        public void Reg_11919_TATReceivedToShippedNotWorking()
        {
            logger = extent.CreateTest("Reg_11919_TATReceivedToShippedNotWorking");
            logger.Log(Status.Info, "Converted manual test case 11919-ROI-Admin-->Infor #065719 - TAT Received to Shipped not working to automated");
            RemoteWebDriver _driver = Init(TestContext.Properties["browser"].ToString());
            ThreadLocal<RemoteWebDriver> localDriver = new ThreadLocal<RemoteWebDriver>();
            localDriver.Value = _driver;
            RemoteWebDriver driver = localDriver.Value;

            try
            {
                ROIAdminHomePage rOIAdminHomePage = new ROIAdminHomePage(driver,logger, TestContext);
                rOIAdminHomePage.SelectFacilityList();
                ROIAdminFacilityListPage rOIAdminFacilityListPage = new ROIAdminFacilityListPage(driver, logger, TestContext);
                rOIAdminFacilityListPage.GoToROITestFacility();
                ROIFacilityWorkSummaryPage rOIFacilityWorkSummaryPage = new ROIFacilityWorkSummaryPage(driver, logger, TestContext);
                rOIFacilityWorkSummaryPage.GoToMROAnalyseSelectTurnAroundReport();
                ROIFacilityTurnaroundReportPage rOIFacilityTurnaroundReportPage = new ROIFacilityTurnaroundReportPage(driver, logger, TestContext);
                rOIFacilityTurnaroundReportPage.CreateMROTurnAroundReport();
                logger.Log(Status.Info, "Turnaround report created");
                Assert.IsTrue(rOIFacilityTurnaroundReportPage.VerifyReceivedToShippedDate());
                logger.Log(Status.Pass, "Received to shipped date verified");
                //TO-DO Connect with client and get clarification on what to verify on the received to shipped amounts on each table 
                rOIFacilityTurnaroundReportPage.VerifyPDFExcelIcon();
                logger.Pass($"Excel and PDF icons displayed");
                //TO DO Ask client in the pdf and excel what content to verify.
                Cleanup(driver);
            }
            catch (Exception ex)
            {
                LogException(driver, logger, ex);
            }        
                     
        }
    }
}
