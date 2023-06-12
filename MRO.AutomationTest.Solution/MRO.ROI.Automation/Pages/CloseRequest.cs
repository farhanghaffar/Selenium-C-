using AventStack.ExtentReports;
using MRO.ROI.Automation.Selenium;
using System;
using OpenQA.Selenium;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium.Remote;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace MRO.ROI.Automation.Pages
{
    public class CloseRequest
    {
        public RemoteWebDriver Driver;
        public ExtentTest logger;
        public TestContext Context;
        public CloseRequest(RemoteWebDriver driver, ExtentTest _loger, TestContext _context)
        {
            Driver = driver;
            logger = _loger;
            Context = _context;
        }
        public By other = By.XPath("//input[@id='txtOtherReason']");
        public By closeRequest = By.XPath("//input[@id='mrocontent_cmdClose']");
        public void ClickCloseRequest()
        {
            try
            {
                Driver.SendKeys(other, "Test");
                Driver.Click(closeRequest);
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to click close request button as message : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }

    }
}


