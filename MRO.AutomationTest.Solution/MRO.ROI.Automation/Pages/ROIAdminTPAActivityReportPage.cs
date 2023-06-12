using AventStack.ExtentReports;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MRO.ROI.Automation.Common.Navigation;
using MRO.ROI.Automation.Selenium;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using System;

namespace MRO.ROI.Automation.Pages
{
    public class ROIAdminTPAActivityReportPage
    {
        public RemoteWebDriver Driver;
        public ExtentTest logger;
        public TestContext Context;
        public ROIAdminTPAActivityReportPage(RemoteWebDriver driver, ExtentTest _loger, TestContext _context)
        {
            Driver = driver;
            logger = _loger;
            Context = _context;
        }

        public By fromDate = By.XPath("//input[@id='mrocontent_txtDateA']");
        public By toDate = By.XPath("//input[@id='mrocontent_txtDateZ']");
        public By createReportButton = By.XPath("//input[@id='mrocontent_cmdCreate']");
        public By txtValidateColumn = By.XPath("//a[text()='Validated']");

        /// <summary>
        /// Create new TPA Activity Report
        /// </summary>
        public ROIAdminFacilityListPage CreateReport()
        {
            try
            {
                var todaysDate = String.Format("{0:M/dd/yyyy}", DateTime.Now).Replace("-", "/");
                Driver.FindElementBy(fromDate).Clear();
                Driver.FindElementBy(fromDate).SendKeys(todaysDate);
                logger.Log(Status.Info, $"Entered from date as ({todaysDate})");
                Driver.FindElementBy(toDate).Clear();
                Driver.FindElementBy(toDate).SendKeys(todaysDate);
                logger.Log(Status.Info, $"Entered to date as ({todaysDate})");
                Driver.FindElementBy(createReportButton).Click();
                IWebElement txtValidatedColumn = Driver.FindElementBy(txtValidateColumn);
                if (txtValidatedColumn.Displayed == true)
                {
                    logger.Log(Status.Pass, "Report is created successfully with all proper columns including the Validated column");
                }
                else
                {
                    Assert.Fail("Failed to validate report is created successfully with all proper columns including the Validated column");
                }
            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to create TPA Activity report : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }

            return new ROIAdminFacilityListPage(Driver, logger, Context);
        }

        /// <summary>
        /// Select ROI Admin Menu 
        /// </summary>        
        public ROIAdminFacilityListPage SelectFacilityList()
        {
            try
            {
                MenuSelector selector = new MenuSelector(Driver, logger,Context);
                selector.SelectRoiAdmin("Facilities", "Facility List");
            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to select Facility List as : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }

            return new ROIAdminFacilityListPage(Driver,logger,Context);
        }
    }
}
