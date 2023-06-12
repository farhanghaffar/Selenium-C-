using AventStack.ExtentReports;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MRO.ROI.Automation.Selenium;
using MRO.ROI.Automation.Utility;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;

namespace MRO.ROI.Automation.Pages.Common
{
    public  class LoginMenuPage
    {
        public RemoteWebDriver Driver;
        public ExtentTest logger;
        public TestContext Context;
        public LoginMenuPage(RemoteWebDriver driver, ExtentTest _loger, TestContext _context)
        {
            Driver = driver;
            logger = _loger;
            Context = _context;
        }

        public  bool IsAtLoginMenuPage
        {
            get
            {
                var loginMenuLabel = Driver.FindElement(By.XPath("//td[contains(text(),'Choose a Login Page')]"));
                return loginMenuLabel.Text == "Choose a Login Page";
            }
        }
    }
}
