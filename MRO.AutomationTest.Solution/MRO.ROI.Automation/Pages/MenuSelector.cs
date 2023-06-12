using AventStack.ExtentReports;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MRO.ROI.Automation.Common;
using MRO.ROI.Automation.Selenium;
using MRO.ROI.Automation.Utility;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.UI;
using System;

namespace MRO.ROI.Automation.Pages
{
    public class ROIMenuSelector
    {
        public RemoteWebDriver Driver;
        public ExtentTest logger;
        public TestContext Context;
        public ROIMenuSelector(RemoteWebDriver driver, ExtentTest _loger, TestContext _context)
        {
            Driver = driver;
            logger = _loger;
            Context = _context;
        }

        public void Select(string topLevelMenuText, string subMenuText)
        {
            Iframe frame = new Iframe(Driver, logger, Context);
            frame.switchToDefaut();
            if (Driver.ReturnElementListSize(By.XPath($"//a[contains(text(),'{topLevelMenuText}')]")) > 0)
            {

                Driver.ClickAndCheckNextElement(By.XPath($"//a[contains(text(),'{topLevelMenuText}')]"), By.XPath($"//a[contains(text(),'{subMenuText}')]"));

                Driver.Wait(TimeSpan.FromSeconds(2));

                Driver.JavaScriptClick(By.XPath($"//a[contains(text(),'{subMenuText}')]"));
                Driver.WaitInSeconds(3);
                Driver.MoveToElement(By.XPath("//img[contains(@src, 'MRO-Logo')]"));
                logger.Log(Status.Info, "Clicked on > " + topLevelMenuText + " > " + subMenuText);
            }
            else
            {
                throw new Exception($"Failed to select roi admin menu options {topLevelMenuText}->{subMenuText} : {Environment.NewLine}");
            }

        }

        public void SelectRoiAdmin(string topLevelMenuText, string subMenuText)
        {
            Driver.WaitUntilDOMLoaded();
            Driver.WaitForPageToLoad();
            int size = Driver.ReturnElementListSize(By.XPath($"//div[@id='nav']//li//a[contains(text(),'{topLevelMenuText}')]"));
            if (size > 0)
            {
                Driver.Click(By.XPath($"//div[@id='nav']//li//a[contains(text(),'{topLevelMenuText}')]"));

                Driver.Wait(TimeSpan.FromSeconds(2));
                Driver.Click(By.XPath($"//a[contains(text(),'{subMenuText}')]"));
                Driver.MoveToElement(By.XPath("//img[contains(@src, 'MRO-Logo')]"));
            }
            else
            {
                throw new Exception();
            }
        }

        public void SelectRoiAdminMenuOptions(string sMenuName, string topLevelMenuText, string subMenuText)
        {
            try
            {
                Driver.WaitUntilDOMLoaded();
                Driver.WaitForPageToLoad();

                string path = $"//table[@smenuname='{sMenuName}']//td[contains(text(),'{topLevelMenuText}')]";
                Console.WriteLine(path);
                int size = 0;
                     size = Driver.ReturnElementListSize(By.XPath($"//table[@smenuname='{sMenuName}']//td[contains(text(),'{topLevelMenuText}')]"));
               if (size > 0)
                {

                    Driver.ClickAndCheckNextElement(By.XPath($"//table[@smenuname='{sMenuName}']//td[contains(text(),'{topLevelMenuText}')]"), By.XPath($"//td[contains(text(),'{subMenuText}')]"));
                    Driver.Wait(TimeSpan.FromSeconds(2));
                    Driver.Click(By.XPath($"//td[contains(text(),'{subMenuText}')]"));

                }
                else
                {
                    throw new Exception();
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to select roi admin menu options {sMenuName}->{topLevelMenuText}->{subMenuText} : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
           
        }


        public void SelectRecentRequestID()
        {
            try
            {
                Actions action = new Actions(Driver);
                Driver.Wait(TimeSpan.FromSeconds(3));
                IWebElement roiRequest = Driver.FindElementBy(By.XPath("//*[contains(text(),'ROI Request')]"));
                action.MoveToElement(roiRequest).Perform();
                Driver.Wait(TimeSpan.FromSeconds(1));
                IWebElement recentRequest = Driver.FindElementBy(By.XPath("//*[contains(text(),'Recent Requests')]"), 10);
                action.MoveToElement(recentRequest).Perform();
                Driver.Wait(TimeSpan.FromSeconds(3));
                IWebElement submenuItem = Driver.FindElementBy(By.XPath("(//*[contains(text(),'Recent Requests')]//following-sibling::ul//a)[1]"));
                action.MoveToElement(submenuItem).Click().Build().Perform();
                Driver.Wait(TimeSpan.FromSeconds(1));

            }
            catch (Exception ex)
            {
             //   Driver.ClickAndCheckNextElement(By.XPath($"//table[@smenuname='mnuROIAdmin']//td[contains(text(),'ROI Request')]"), By.XPath($"//td[contains(text(),'Recent Requests')]"));
             //   Driver.Wait(TimeSpan.FromSeconds(2));
             //   Driver.Click(By.XPath($"//td[@id='mroheader_ctl02_ctl01_ctl00_mnuMainMenu-menuItem000-subMenu-menuItem002-subMenu-menuItem000']"));

                Actions action = new Actions(Driver);
                Driver.Wait(TimeSpan.FromSeconds(3));
                IWebElement roiRequest = Driver.FindElementBy(By.XPath("//table[@smenuname='mnuROIFacilityUser']//td[contains(text(),'ROI Request')]"));
                action.MoveToElement(roiRequest).Perform();
                Driver.Wait(TimeSpan.FromSeconds(1));
                IWebElement recentRequest = Driver.FindElementBy(By.XPath("//td[contains(text(),'Recent Requests')]"), 10);
                action.MoveToElement(recentRequest).Perform();
                Driver.Wait(TimeSpan.FromSeconds(3));
                try
                {
                    IWebElement submenuItem = Driver.FindElementBy(By.XPath("//td[@id='mroheader_ctl02_ctl01_ctl00_mnuMainMenu-menuItem000-subMenu-menuItem002-subMenu-menuItem000']"));
                    action.MoveToElement(submenuItem).Click().Build().Perform();
                    Driver.Wait(TimeSpan.FromSeconds(1));
                }
                catch (Exception)
                {
                    Driver.Click(By.XPath($"//td[contains(text(),'FN')]"));
                }


            }
        }

        public void SelectTableBasedMenu(string topLevelMenuText, string subMenuText)
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

        public void ClickLogoutIcon()
        {
            Iframe frame = new Iframe(Driver, logger, Context);
            frame.switchToDefaut();

            Driver.Wait(TimeSpan.FromSeconds(2));

            Driver.JavaScriptClick(By.XPath($"//i[contains(@class, 'mdi-close')]/parent::a"));
            Driver.WaitInSeconds(3);
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
