using AventStack.ExtentReports;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MRO.ROI.Automation.Selenium;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using System;

namespace MRO.ROI.Automation.Pages
{
    public  class ROIAdminSavedReportsPage
    {
        public RemoteWebDriver Driver;
        public ExtentTest logger;
        public TestContext Context;
        public ROIAdminSavedReportsPage(RemoteWebDriver driver, ExtentTest _loger, TestContext _context)
        {
            Driver = driver;
            logger = _loger;
            Context = _context;
        }

        public By reportsList = By.CssSelector("table#mrocontent_RadGridReports_ctl00>tbody>tr");
        public By CurrentReportName = By.XPath("//table[@id='mrocontent_RadGridReports_ctl00']//tr[@id='mrocontent_RadGridReports_ctl00__0']");
        public By refreshImage = By.XPath("//a[@title ='Refresh']");
        public void ClickOnSavedReportLink()
        {
            try
            {
               var reportElements = Driver.FindElementsBy(reportsList);

                foreach (var reportLink in reportElements)
                {
                    reportLink.Click();
                    break;
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed with Message Unable to verify Header : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }

        public string GetCurrentReportName()
        {
            try
            {
                string currentReportName = Driver.FindElementBy(CurrentReportName).Text;
                return currentReportName.Replace(".zip", " ").Trim();
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed with Message Unable to verify Header : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }
        public void ClickOnRefresh()
        {
            try
            {
                Driver.Click(refreshImage);
            }

            catch (Exception ex)
            {
                throw new Exception($"Failed with Message unable to click refresh : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }
    }
}
