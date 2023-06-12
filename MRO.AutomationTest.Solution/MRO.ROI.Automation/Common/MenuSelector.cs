using AventStack.ExtentReports;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MRO.ROI.Automation.Selenium;
using MRO.ROI.Automation.Utility;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.UI;
using System;

namespace MRO.ROI.Automation.Common.Navigation
{
    public class MenuSelector
    {
        public RemoteWebDriver Driver;
        public ExtentTest logger;
        public TestContext Context;
        public MenuSelector(RemoteWebDriver driver, ExtentTest _loger, TestContext _context)
        {
            Driver = driver;
            logger = _loger;
            Context = _context;
        }


        public  void Select(string topLevelMenuText, string subMenuText)
        {
            Driver.Click(By.XPath($"//td[contains(text(),'{topLevelMenuText}')]"));
            Driver.Wait(TimeSpan.FromSeconds(2));
            Driver.FindElement(By.XPath($"//td[contains(text(),'{subMenuText}')]")).Click();
        }

        public  void SelectRoiAdmin(string topLevelMenuText, string subMenuText)
        {
            //Driver.WaitUntilDOMLoaded();
            Console.Write($"//td[contains(text(),'{topLevelMenuText}')]");
            var wait = new WebDriverWait(Driver, new TimeSpan(0, 0, 55));
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.XPath($"//table[@smenuname='mnuROIAdmin']//td[contains(text(),'{topLevelMenuText}')]")));

            var topLevelMenu = Driver.FindElementBy(By.XPath($"//table[@smenuname='mnuROIAdmin']//td[contains(text(),'{topLevelMenuText}')]"));
            Actions action = new Actions(Driver);
            action.Click(topLevelMenu).Build().Perform();
            //action.Perform();
            Driver.Wait(TimeSpan.FromSeconds(5));

            Driver.JavaScriptClick(By.XPath($"//td[contains(text(),'{subMenuText}')]"));
        }

        public  void SelectRecentRequestID()
        {
            Actions action = new Actions(Driver);
            Driver.Wait(TimeSpan.FromSeconds(1));
            IWebElement roiRequest = Driver.FindElementBy(By.XPath("//*[contains(text(),'ROI Request')]"));
            action.MoveToElement(roiRequest).Perform();
            Driver.Wait(TimeSpan.FromSeconds(1));
            IWebElement recentRequest = Driver.FindElementBy(By.XPath("//*[contains(text(),'Recent Requests')]"), 10);
            action.MoveToElement(recentRequest).Perform();
            Driver.Wait(TimeSpan.FromSeconds(3));
            try
            {
                IWebElement submenuItem = Driver.FindElement(By.XPath("(//table[starts-with(@id,'mroheader_')])[2]//tr"));
                action.MoveToElement(submenuItem).Click().Build().Perform();
                Driver.Wait(TimeSpan.FromSeconds(1));
            }
            catch (Exception)
            {
                IWebElement submenuItem = Driver.FindElement(By.XPath("(//*[contains(text(),'Recent Requests')]//following-sibling::ul//a)[1]"));
                action.MoveToElement(submenuItem).Click().Build().Perform();
                Driver.Wait(TimeSpan.FromSeconds(1));

            }
        }
    }
    
    public class FacMenuSelector
    {
        public RemoteWebDriver Driver;
        public ExtentTest logger;
        public FacMenuSelector(RemoteWebDriver driver, ExtentTest _loger)
        {
            Driver = driver;
            logger = _loger;
        }


        public void Select(string topLevelMenuText, string subMenuText)
        {
            //Driver.WaitUntilDOMLoaded();
            var wait = new WebDriverWait(Driver, new TimeSpan(0, 0, 55));
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.XPath($"//td[contains(text(),'{topLevelMenuText}')]")));

            var topLevelMenu = Driver.FindElement(By.XPath($"//td[contains(text(),'{topLevelMenuText}')]"));
            Actions action = new Actions(Driver);
            action.Click(topLevelMenu).Build().Perform();
            //action.Perform();
            Driver.Wait(TimeSpan.FromSeconds(2));

            Driver.FindElement(By.XPath($"//td[contains(text(),'{subMenuText}')]")).Click();
        }
    }
}
