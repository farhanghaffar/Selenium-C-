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
    public class ROIFacilityCompleteLoggingPage
    {

        public RemoteWebDriver Driver;
        public ExtentTest logger;
        public TestContext Context;
        public CSVReader csvReader;
        public ROIFacilityCompleteLoggingPage(RemoteWebDriver driver, ExtentTest _loger, TestContext _context)
        {
            Driver = driver;
            logger = _loger;
            Context = _context;
        }



        public By viewDocumentButton = By.XPath("//input[@ name='mrocontent$dgReport$ctl02$ctl02']");
        public By verifyRequestDocuments = By.XPath("//a[@ ng-repeat='tab in tabs']");
        public By patientNameHyperLink = By.XPath("//a[@ title='Click to Complete Logging']");
        

        public void WaitUntillRequestPageLoaded()
        {
            IWebElement verifyDocument = null;
            int retry = 1;
            while (retry <= 5 && verifyDocument == null)
            {
                try
                {
                    Driver.SwitchToWindow("MRO Viewer");
                    verifyDocument = Driver.FindElementBy(verifyRequestDocuments, 2);
                }
                catch (Exception ex)
                {
                    Driver.SleepTheThread(1);
                    retry++;

                }
            }
        }

        public bool ClickViewDocumnetAndReturnRequestDocumentsCount()
        {
            bool isDisplayed = false;
            try
            {
                Driver.Click(viewDocumentButton);
                //WaitUntillPageLoaded();
                WaitUntillRequestPageLoaded();
                Driver.SwitchToWindow("MRO Viewer");
                Driver.Manage().Window.Maximize();
                IWebElement requestDocsElement = Driver.FindElementBy(verifyRequestDocuments, 2);
                if (requestDocsElement.Displayed == true)
                    
                {
                     isDisplayed = true;
                }
                Driver.Wait(TimeSpan.FromSeconds(1));
                Driver.SwitchToWindowAndClose("MRO Viewer");
                Driver.SleepTheThread(3);
                Driver.SwitchToWindow("ROI Log: Request Status");
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to verify request pages count on view documents : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
            return isDisplayed;
        }

        public void ClickOnPatientHyperLink()
        {
            
            try
            {
                Driver.Click(patientNameHyperLink);
              
                
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to click on Patient hyper test link : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
            
        }




    }
}