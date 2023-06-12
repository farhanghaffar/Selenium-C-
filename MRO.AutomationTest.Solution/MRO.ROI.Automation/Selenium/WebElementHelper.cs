using AventStack.ExtentReports;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.UI;
using System;
using WindowsInput;
using WindowsInput.Native;


namespace MRO.ROI.Automation.Selenium
{
    public class WebElementHelper
    {
        public RemoteWebDriver Driver;
        public ExtentTest logger;
        public TestContext Context;
        public WebElementHelper(RemoteWebDriver driver, ExtentTest _loger, TestContext _context)
        {
            Driver = driver;
            logger = _loger;
            Context = _context;
        }

        private static object window;

        /// <summary>
        /// Perform click action on the web element
        /// </summary>
        /// <param name="webElementName"></param>
        /// <param name="findElementBy"></param>
        public  void Click_Action(string webElementName, FindElementBy findElementBy)
        {
            IWebElement webElement;

            switch (findElementBy)
            {
                case FindElementBy.Id:
                    webElement = Driver.FindElement(By.Id(webElementName));
                    break;

                case FindElementBy.Xpath:
                    webElement = Driver.FindElement(By.XPath(webElementName));
                    break;
                default:
                    throw new Exception("Invalid element type");
            }

            Actions action = new Actions(Driver);
            action.Click(webElement).Build().Perform();
        }

        /// <summary>
        /// Simulates keyboard return/enter key press
        /// </summary>
        public  void Click_Enter()
        {
            Driver.SleepTheThread(2);
            InputSimulator simulator = new InputSimulator();
            simulator.Keyboard.KeyPress(VirtualKeyCode.RETURN);
        }

        public  void ScrollIntoView(string locatorKey, FindElementBy findElementBy)
        {
			IWebElement element;
			if (findElementBy == FindElementBy.Id)
			{
				element = Driver.FindElementBy(By.Id(locatorKey));
			}
			else if(findElementBy == FindElementBy.Xpath)
			{
				element = Driver.FindElementBy(By.XPath(locatorKey));
			}
			else
			{
				element = Driver.FindElementBy(By.TagName(locatorKey));
			}
             
            RemoteWebDriver dr = Driver;
            IJavaScriptExecutor js = (IJavaScriptExecutor)dr;
            js.ExecuteScript("arguments[0].scrollIntoView(true);", element);
        }

        public static void Click_Action()
        {
            throw new NotImplementedException();
        }

        public  void ScrollIntoView1()
        {
            //IWebElement element = Driver.FindElement(By.Id(locatorKey));
            RemoteWebDriver dr = Driver;
            IJavaScriptExecutor js = (IJavaScriptExecutor)dr;
            js.ExecuteScript("window.scrollBy(0,950);");

        }
        public  bool IsElementPresent(string locatorKey)
        {
            int i = Driver.FindElements(By.XPath(locatorKey)).Count;
            Console.WriteLine(i + locatorKey);
            if (i > 0)
            {
                return true;
            }
            return false;
        }
        public  bool IsElementMissing(By elementLocator, int timeout = 2)
        {
            bool bRet = false;
            try
            {
                Driver.TurnOffWait();
                var wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(timeout));
                wait.Until(x => x.FindElement(elementLocator));
            }
            catch (Exception ex)
            {
                //Console.WriteLine("Element with locator: '" + elementLocator + "' was not found in current context page.");
                bRet = true;
            }
            Driver.TurnOnWait();
            return bRet;
        }
        public  IWebElement WaitUntilElementExists(By elementLocator, int timeout = 30)
        {
            try
            {
                var wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(timeout));
                return wait.Until(x => x.FindElement(elementLocator));
            }
            catch (NoSuchElementException)
            {
                Console.WriteLine("Element with locator: '" + elementLocator + "' was not found in current context page.");
                throw;
            }
        }

        public  void AcceptAlert()
        {
            Driver.Wait(TimeSpan.FromSeconds(2));
            Driver.SwitchTo().Alert().Accept();
        }
    }

}
