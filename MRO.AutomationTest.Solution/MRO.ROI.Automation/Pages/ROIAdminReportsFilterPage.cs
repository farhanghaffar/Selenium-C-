using AventStack.ExtentReports;
using MRO.ROI.Automation.Selenium;
using System;
using OpenQA.Selenium;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium.Remote;

namespace MRO.ROI.Automation.Pages
{
    public class ROIAdminReportsFilterPage
    {
        public RemoteWebDriver Driver;
        public ExtentTest logger;
        public TestContext Context;
        public ROIAdminReportsFilterPage(RemoteWebDriver driver, ExtentTest _loger, TestContext _context)
        {
            Driver = driver;
            logger = _loger;
            Context = _context;
        }

        public string dateRangeDate = string.Empty;
        public By dateRange = By.XPath("//span[@id='range-label']");
        public By reportingGroup = By.XPath("//select[@id='nFacilityReportingGroupID']");
        public By location = By.XPath("//select[@id='nLocationID']");
        public By createReport = By.CssSelector("input#btn_submit");
        public By selectDatePicker = By.XPath("(//span[@id='daterange'])[1]");
        public By selectFromDate = By.XPath("((//table[@class='table-condensed'])[2]//tr//td[contains(text(),'1')])[1]");
        public By selectToDate = By.XPath("((//table[@class='table-condensed'])[1]//tr//td[contains(text(),'1')])[13]");
        public By btnApply = By.XPath("//button[text()='Apply']");
        public By turnAroundReportFrame = By.XPath("//iframe[starts-with(@id,'rdFrame')]");
        public By yearSelect = By.XPath("(//select[@class='yearselect'])[2]");

        public void ApplyFilterAndCreateReport()
        {
            try
            {
                Driver.FindElementBy(By.CssSelector("td#MasterHeaderText"));
                Driver.SleepTheThread(5);
                IWebElement frame = Driver.FindElementBy(By.TagName("iframe"));
                Driver.SwitchTo().Frame(frame);
                Driver.FindElementBy(selectDatePicker);
                Driver.SleepTheThread(3);
                dateRangeDate = Driver.GetText(By.XPath("(//span[@id='daterange'])[1]//span"));
                Driver.SleepTheThread(2);

                Driver.SendKeys(reportingGroup, "[None]");
                Driver.SendKeys(location, "[All]");
                Driver.SleepTheThread(2);
                Driver.DirectClick(createReport);
                Driver.SleepTheThread(3);

            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to apply a filter and create report Message : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }
    }
}
