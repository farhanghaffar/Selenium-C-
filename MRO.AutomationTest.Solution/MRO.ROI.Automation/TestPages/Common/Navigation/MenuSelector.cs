using MRO.ROI.Automation.Selenium;
using MRO.ROI.Automation.Utility;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System;

namespace MRO.ROI.Automation.Common.Navigation
{
    public class MenuSelector
    {
        public static void Select(string topLevelMenuText, string subMenuText)
        {
            //Driver.WaitUntilDOMLoaded();
            var topLevelMenu = Driver.FindElement(By.XPath($"//td[contains(text(),'{topLevelMenuText}')]"));
            Actions action = new Actions(Driver.Instance);
            action.Click(topLevelMenu).Build().Perform();
            //action.Perform();
            Driver.Wait(TimeSpan.FromSeconds(2));

            Driver.FindElement(By.XPath($"//td[contains(text(),'{subMenuText}')]")).Click();
        }

        public static void SelectRoiAdmin(string topLevelMenuText, string subMenuText)
        {
            //Driver.WaitUntilDOMLoaded();
            Console.Write($"//td[contains(text(),'{topLevelMenuText}')]");
            var wait = new WebDriverWait(Driver.Instance, new TimeSpan(0, 0, 55));
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.XPath($"//table[@smenuname='mnuROIAdmin']//td[contains(text(),'{topLevelMenuText}')]")));

            var topLevelMenu = Driver.FindElement(By.XPath($"//table[@smenuname='mnuROIAdmin']//td[contains(text(),'{topLevelMenuText}')]"));
            Actions action = new Actions(Driver.Instance);
            action.Click(topLevelMenu).Build().Perform();
            //action.Perform();
            Driver.Wait(TimeSpan.FromSeconds(2));

            Driver.FindElement(By.XPath($"//td[contains(text(),'{subMenuText}')]")).Click();
        }
    }
    
    public class FacMenuSelector
    {
        public static void Select(string topLevelMenuText, string subMenuText)
        {
            //Driver.WaitUntilDOMLoaded();
            var wait = new WebDriverWait(Driver.Instance, new TimeSpan(0, 0, 55));
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.XPath($"//td[contains(text(),'{topLevelMenuText}')]")));

            var topLevelMenu = Driver.FindElement(By.XPath($"//td[contains(text(),'{topLevelMenuText}')]"));
            Actions action = new Actions(Driver.Instance);
            action.Click(topLevelMenu).Build().Perform();
            //action.Perform();
            Driver.Wait(TimeSpan.FromSeconds(2));

            Driver.FindElement(By.XPath($"//td[contains(text(),'{subMenuText}')]")).Click();
        }
    }
}
