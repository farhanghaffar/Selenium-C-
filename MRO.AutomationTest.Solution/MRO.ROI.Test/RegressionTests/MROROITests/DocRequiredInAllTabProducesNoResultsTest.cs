using AventStack.ExtentReports;
using DataDrivenProject;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MRO.ROI.Automation.Pages;
using MRO.ROI.Automation.Pages.Common;
using MRO.ROI.Test.ExecutionFactory;
using MRO.ROI.Test.Utilities;
using OpenQA.Selenium.Remote;
using System;
using System.Threading;

namespace MRO.ROI.Test.RegressionTests.MROROITests
{

    [TestClass]
    public class DocRequiredInAllTabProducesNoResultsTest : ROIBaseTest
    {
        public DocRequiredInAllTabProducesNoResultsTest() : base(ROITestArea.ROIAdmin)
        {

        }
        [STATestMethodAttribute]
        [TestCategory(ROITestCategory.Regression)]
        //Automated By Sandeep
        public void Reg_5590__DocRequiredInAllTabProducesNoResultsTest()
        {
            logger = extent.CreateTest(" Reg_5590__DocRequiredInAllTabProducesNoResultsTest");
            logger.Log(Status.Info, "Converted manual test case Reg_5590__DocRequiredInAllTabProducesNoResultsTest");
            RemoteWebDriver _driver = Init(TestContext.Properties["browser"].ToString());
            ThreadLocal<RemoteWebDriver> localDriver = new ThreadLocal<RemoteWebDriver>();
            localDriver.Value = _driver;
            RemoteWebDriver driver = localDriver.Value;
            try
            {
                ROIAdminFeaturesPage rOIAdminFeaturePage = new ROIAdminFeaturesPage(driver, logger, TestContext);
                ROIAdminHomePage rOIAdminHomePage = new ROIAdminHomePage(driver, logger, TestContext);
                ROIAdminFacilityListPage rOIAdminFacilityListPage = new ROIAdminFacilityListPage(driver, logger, TestContext);
                ROIAdminFacilityFeaturesPage rOIAdminFeaturesPage = new ROIAdminFacilityFeaturesPage(driver, logger, TestContext);
                ROIFacilityLogNewRequestPage rOIFacilityLogNewRequestPage = new ROIFacilityLogNewRequestPage(driver, logger, TestContext);
                ROIFacilityWorkSummaryPage rOIFacilityWorkSummaryPage = new ROIFacilityWorkSummaryPage(driver, logger, TestContext);
                ROIFacilityLocationROITestFacilityPage rOIFacilityLocationROITestFacilityPage = new ROIFacilityLocationROITestFacilityPage(driver, logger, TestContext);
                rOIAdminHomePage.ClickFacilityList();                
                rOIAdminFacilityListPage.ClickOnSpecificAlphabet("S");
               
                rOIAdminFacilityListPage.GoToROITestFacilityName();               
                rOIAdminFeaturesPage.ClickFacilityComputerIcon();
                logger.Log(Status.Info, "Work summary page opened", TakeScreenShotAtStep());
               

                rOIFacilityWorkSummaryPage.GoToDocsRequired();                
                rOIFacilityLocationROITestFacilityPage.DocsReqdTabsClick();
                logger.Log(Status.Info, "Verified that all tabs are visible");
                LoginPage loginPage = new LoginPage(driver, logger, TestContext);
                loginPage.LogOut();

                rOIAdminHomePage.ClickFacilityList();               
                rOIAdminFacilityListPage.ClickOnSpecificAlphabet("R");               
                rOIAdminFacilityListPage.GoToROITestFacilityNamer();               
                rOIAdminFeaturesPage.ClickFacilityComputerIcon();
               
                rOIFacilityWorkSummaryPage.GoToDocsRequired();                
                rOIFacilityLocationROITestFacilityPage.DocsReqdTabsClickr();
                logger.Log(Status.Info, "Verified that  each Tab and Document Header Title");
                loginPage.LogOut();
               
                rOIAdminHomePage.ClickFacilityList();
                logger.Log(Status.Info, "Clicks the Available Facilities under Facilities List");
                Cleanup(driver);
            }
            catch (Exception ex)
            {

                LogException(driver, logger, ex);
                logger.Log(Status.Info, "Exception is logged");
            }
        }
    }
}


