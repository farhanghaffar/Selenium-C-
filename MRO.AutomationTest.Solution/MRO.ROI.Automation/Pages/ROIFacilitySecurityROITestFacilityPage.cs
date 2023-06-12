using AventStack.ExtentReports;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MRO.ROI.Automation.Selenium;
using MRO.ROI.Automation.Utility;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using System;
using System.IO;
using System.Reflection;

namespace MRO.ROI.Automation.Pages
{
    public class ROIFacilitySecurityROITestFacilityPage
    {
        public RemoteWebDriver Driver;
        public ExtentTest logger;
        public TestContext Context;
      
        public ROIFacilitySecurityROITestFacilityPage(RemoteWebDriver driver, ExtentTest _loger, TestContext _context)
        {
            Driver = driver;
            logger = _loger;
            Context = _context;
        }

        public By enableSSOChkbox = By.XPath("//input[@id='mrocontent_cbEnableSSO']");
        public By updateSecurityOptionsBtn = By.XPath("//input[@id='mrocontent_cmdUpdate']");

        public bool UnCheckEnableSSOCheckBox()
        {
            try
            {
               if(Driver.FindElementBy(enableSSOChkbox).Selected==true)
                {
                    Driver.Click(enableSSOChkbox);
                    Driver.Click(updateSecurityOptionsBtn);

                }
                bool isChecked = Driver.FindElementBy(enableSSOChkbox).Selected;
                Driver.ScrollToElement(enableSSOChkbox);
                return isChecked;
            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to verify enable sso checkbox with details as : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }


        public bool CheckEnableSSOCheckBox()
        {
            try
            {
                if (Driver.FindElementBy(enableSSOChkbox).Selected == false)
                {
                    Driver.Click(enableSSOChkbox);
                    Driver.Click(updateSecurityOptionsBtn);

                }
                bool isChecked = Driver.FindElementBy(enableSSOChkbox).Selected;
                Driver.ScrollToElement(enableSSOChkbox);
                return isChecked;
            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to verify enable sso checkbox with details as : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }


    }
}
