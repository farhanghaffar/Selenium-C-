using AventStack.ExtentReports;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MRO.ROI.Automation.Selenium;
using MRO.ROI.Automation.Utility;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

//Written by sandeep
namespace MRO.ROI.Automation.Pages
{
    public class ROIRevenueIntegrityDashboard
    {
        public RemoteWebDriver Driver;
        public ExtentTest logger;
        public TestContext Context;
        public CSVReader csvReader;


        public ROIRevenueIntegrityDashboard(RemoteWebDriver driver, ExtentTest _loger, TestContext _context)
        {
            Driver = driver;
            logger = _loger;
            Context = _context;
        }
        public By btnCreateReport = By.XPath("//input[@id='btn_submit']");

        public void CreateRevenueIntegrityDashboardReport()
        {
            try
            {
                Driver.SleepTheThread(10);
                Automation.Common.Iframe frame = new Automation.Common.Iframe(Driver, logger, Context);
                frame.SwitchToRDFrame();
                Driver.Click(btnCreateReport);
                Driver.SleepTheThread(5);
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to create Revenue Integrity dashboard Exception detail as new Exception: {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");

            }
        }
        public void CreateMonthlySummaryDashboardReport()
        {
            try
            {
               
                ROIFacilityMonthlySummaryReportPage rOIFacilityMonthlySummaryReportPage = new ROIFacilityMonthlySummaryReportPage(Driver, logger, Context);
                rOIFacilityMonthlySummaryReportPage.CreateMonthlySummaryReport();
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to create Summary Dashboard ReportException detail as new Exception: {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");

            }
        }
    }
}
