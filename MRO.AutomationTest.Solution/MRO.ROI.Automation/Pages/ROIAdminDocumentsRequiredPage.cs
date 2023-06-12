using AventStack.ExtentReports;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MRO.ROI.Automation.Selenium;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.UI;
using System;

namespace MRO.ROI.Automation.Pages
{
    public class ROIAdminDocumentsRequiredPage
    {
        public RemoteWebDriver Driver;
        public ExtentTest logger;
        public TestContext Context;
        public ROIAdminDocumentsRequiredPage(RemoteWebDriver driver, ExtentTest _loger, TestContext _context)
        {
            Driver = driver;
            logger = _loger;
            Context = _context;
        }

        public By wizardRequestsTab = By.XPath("//span[contains(text(),'Wizard Requests')]");
        public By loggedFromDate = By.XPath("//input[@id='mrocontent_txtFrom']");
        public By loggedToDate = By.XPath("//input[@id='mrocontent_txtTo']");
        public By locationDrp = By.XPath("//input[@id='mrocontent_lstLocation_Input']");
        public By createReportBtn = By.XPath("//input[@id='mrocontent_cmdCreate']");
        public By clearAllFieldsBtn = By.XPath("//input[@id='mrocontent_cmdClearAll']");
        public By excelIcon = By.XPath("//img[@alt='Export to Excel']");

        public void ClickOnWizardRequests()
        {
            try
            {
                Driver.Click(wizardRequestsTab);
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to click wizard requests with message as : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }

        public void CreateReport()
        {
            try
            {
                Driver.ClearText(loggedFromDate);
                Driver.SendKeys(loggedFromDate, "02/13/2019");
                Driver.ClearText(loggedToDate);
                Driver.SendKeys(loggedToDate, "02/03/2020");
                Driver.SendKeys(locationDrp, "[All Locations]");
                Driver.Click(createReportBtn);
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to create report with message as : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }
        public void ClickOnExportToExcelIcon()
        {
            try
            {
                Driver.Click(excelIcon);
            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to click excel icon with message as : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }


    }
}
