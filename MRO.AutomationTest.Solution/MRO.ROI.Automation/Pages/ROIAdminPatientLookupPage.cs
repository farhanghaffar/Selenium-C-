using AventStack.ExtentReports;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MRO.ROI.Automation.Selenium;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MRO.ROI.Automation.Pages
{
   public class ROIAdminPatientLookupPage
    {

        public RemoteWebDriver Driver;
        public ExtentTest logger;
        public TestContext Context;
        public ROIAdminPatientLookupPage(RemoteWebDriver driver, ExtentTest _loger, TestContext _context)
        {
            Driver = driver;
            logger = _loger;
            Context = _context;
        }

        public By SSN = By.XPath("//input[@id='mrocontent_txtSSN']");
        public By FindPatient = By.XPath("//input[@id='mrocontent_cmdCreate']");
        public By MagnifierIcon = By.XPath("//input[@id='mrocontent_dgReport_ctl00_ctl04_btnViewSSN']");
        public By Logout = By.XPath("//img[@id='mroheader_MROPageHead1_ctl03_imgLogout']");
        public By FullSSN = By.XPath("//div[@id='alert1632300353212_message']");
        public By OK = By.XPath("//span[@class='rwCommandButton rwCloseButton']");
    

        public void ClickFindPatient()
        {
            try
            {
                IWebElement sSN = Driver.FindElementBy(SSN);
                sSN.SendKeys("111111111");

                IWebElement findPatient = Driver.FindElementBy(FindPatient);
                findPatient.Click();
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed  to click on find patient button as Message : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }

        }

        public void ViewFullSSN()
        {
            try
            {
                IWebElement magnifierIcon = Driver.FindElementBy(MagnifierIcon);
                magnifierIcon.Click();
                
                IWebElement clickOK = Driver.FindElementBy(OK);
                clickOK.Click();

            }
            catch (Exception ex)
            {
                throw new Exception($"Failed  to view ful ssn as Message : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }

        public void ClickLogout()
        {
            try
            {
                IWebElement logout = Driver.FindElementBy(Logout);
                logout.Click();
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed  to click logout as Message : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }
    }
}
