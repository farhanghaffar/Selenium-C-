using AventStack.ExtentReports;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MRO.ROI.Automation.Selenium;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MRO.ROI.Automation.Pages
{
    public class ROIRequestPreAuthorizationPage
    {
        public RemoteWebDriver Driver;
        public ExtentTest logger;
        public TestContext Context;
        public ROIRequestPreAuthorizationPage(RemoteWebDriver driver, ExtentTest _loger, TestContext _context)
        {
            Driver = driver;
            logger = _loger;
            Context = _context;
        }
        public By estimatedPaper = By.XPath("//input[@ id='mrocontent_txtPageCount']");
        public By estimatedElectronic = By.XPath("//input[@ id='mrocontent_txtPageCountElec']");
        public By reqPreAuth = By.XPath("//input[@ id='mrocontent_cmdOk']");
      
        public void PreAuthorizationRequired()
        {
            try
            {
                Driver.SendKeys(estimatedPaper, "5");
                Driver.SendKeys(estimatedElectronic, "5");
                Driver.Click(reqPreAuth);
            }

            catch (Exception ex)
            {
                throw new Exception($"Failed to setup user with details : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }


    }
}

     
