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
   public class ROIadminScanRequestDocumentsPage
    {
        public RemoteWebDriver Driver;
        public ExtentTest logger;
        public TestContext Context;
        public ROIadminScanRequestDocumentsPage(RemoteWebDriver driver, ExtentTest _loger, TestContext _context)
        {
            Driver = driver;
            logger = _loger;
            Context = _context;
        }

        public By RequestIdOne = By.XPath("//table[@id='mrocontent_dgReport']//tr[2]//td[2]");
        public By RequestIdTwo = By.XPath("//table[@id='mrocontent_dgReport']//tr[3]//td[2]");


        public string GetRequestIdOne()
        {
            try
            {
                string requestIDOne = Driver.FindElementBy(RequestIdOne).Text;
                return requestIDOne;
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to get request id one : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }

        public string GetRequestIdTwo()
        {
            try
            {
                string requestIDTwo = Driver.FindElementBy(RequestIdTwo).Text;
                return requestIDTwo;
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to get request id two : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }
    }
}
