using AventStack.ExtentReports;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MRO.ROI.Automation.Common.Navigation;
using MRO.ROI.Automation.Pages;
using MRO.ROI.Automation.Pages.Common;
using MRO.ROI.Automation.Selenium;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using static MRO.ROI.Automation.Utility.IniFile;

namespace MRO.ROI.Automation.Pages
{
    public class ROIFacilityComponentImportMappingConfigurationPage
    {
        public RemoteWebDriver Driver;
        public ExtentTest logger;
        public TestContext Context;
        public ROIFacilityComponentImportMappingConfigurationPage(RemoteWebDriver driver, ExtentTest _loger, TestContext _context)
        {
            Driver = driver;
            logger = _loger;
            Context = _context;
        }


        public By sfloderEle = By.XPath("//tr[@class='rgRow']/td[3]");
        public By headerElement = By.XPath("//td[@id='MasterHeaderText']");



        public string VerifySfloder()
        {
            try
            {
                string sfloderValue = Driver.GetText(sfloderEle);                
                return sfloderValue;
            }

            catch (Exception ex)
            {

                throw new Exception($"Failed verify Sfloder with details as Message : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }

        }

        public string VerifyHeader()
        {
            try
            {
                string header = Driver.GetText(headerElement);
                return header;
            }
            catch (Exception ex)
            {

                throw new Exception($"Failed verify header  with details as Message : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }

        }
    }
}
