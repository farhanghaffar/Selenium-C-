using AventStack.ExtentReports;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MRO.ROI.Automation.Common.Navigation;
using MRO.ROI.Automation.Selenium;
using MRO.ROI.Automation.Utility;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Reflection;
using static MRO.ROI.Automation.Utility.IniFile;

namespace MRO.ROI.Automation.Pages
{
    public class ROIFacilityCompleteLoggingImportRequestPage
    {

        public RemoteWebDriver Driver;
        public ExtentTest logger;
        public TestContext Context;
        public CSVReader csvReader;
        public ROIFacilityCompleteLoggingImportRequestPage(RemoteWebDriver driver, ExtentTest _loger, TestContext _context)
        {
            Driver = driver;
            logger = _loger;
            Context = _context;
        }

        
      
        public By requestReceivedDate = By.Id("mrocontent_txtRequestRcvdDate");
        public By logRequestButton = By.Id("mrocontent_cmdLogRequest");
       

        
        public void logRequest()
        {
            try
            {
                              
                var todaysDate = String.Format("{0:M/dd/yyyy}", DateTime.Now).Replace("-", "/");
                var requestRecievedDate = Driver.FindElementBy(requestReceivedDate);
                requestRecievedDate.SendKeys(todaysDate);                
                WebElementHelper helper = new WebElementHelper(Driver, logger,Context);
                helper.ScrollIntoView("mrocontent_cmdLogRequest", FindElementBy.Id);
                Driver.ScrollIntoViewAndClick(logRequestButton);
                ROIMenuSelector menu = new ROIMenuSelector(Driver,logger,Context);
                menu.SelectRecentRequestID();
                
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to create new MRO delivery request Exception details with Message : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
                
            }
           
        }


        


    }
}