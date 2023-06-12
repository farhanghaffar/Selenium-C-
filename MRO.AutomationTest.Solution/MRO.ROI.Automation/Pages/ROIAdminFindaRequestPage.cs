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
    public class ROIAdminFindaRequestPage
    {
        public RemoteWebDriver Driver;
        public ExtentTest logger;
        public TestContext Context;
        public ROIAdminFindaRequestPage(RemoteWebDriver driver, ExtentTest _loger, TestContext _context)
        {
            Driver = driver;
            logger = _loger;
            Context = _context;
        }

        public By systems = By.XPath("//td[@id='mroheader_ctl02_ctl01_ctl00_mnuMainMenu-menuItem016']");
        public By ratesAndFees = By.XPath("//td[@id='mroheader_ctl02_ctl01_ctl00_mnuMainMenu-menuItem016-subMenu-menuItem002']");
        public By UPSPSRates = By.XPath("//td[@id='mroheader_ctl02_ctl01_ctl00_mnuMainMenu-menuItem016-subMenu-menuItem002-subMenu-menuItem007']");

        public void ToUPSPSRates()
        {
            try
            {
                Driver.Click(systems);
                Driver.Click(ratesAndFees);
                Driver.Click(UPSPSRates);
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to open UPS Message : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }
    }
}