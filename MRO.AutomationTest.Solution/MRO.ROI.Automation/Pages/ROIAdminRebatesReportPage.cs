using AventStack.ExtentReports;
using DataDrivenProject;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MRO.ROI.Automation.Selenium;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MRO.ROI.Automation.Pages
{
    public class ROIAdminRebatesReportPage
    {
        public RemoteWebDriver Driver;
        public ExtentTest logger;
        public TestContext Context;
        public ROIAdminRebatesReportPage(RemoteWebDriver driver, ExtentTest _loger, TestContext _context)
        {
            Driver = driver;
            logger = _loger;
            Context = _context;
        }

        public By rdBillablePatientPaymentRequests = By.XPath("//label[text()='Billable Patient Request Payments']");

        public bool IsBillablePatientRequestPaymentsVisible()
        {
            bool isVisible = false;
            try
            {
               
                var items = Driver.FindElementsBy(By.XPath("//*[contains(text(),'Billable Patient Request Payments')]"));
                            
                return items.Count == 0;
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to verify Billable Patient Payment Requests is visible {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }

        }
    }
}
