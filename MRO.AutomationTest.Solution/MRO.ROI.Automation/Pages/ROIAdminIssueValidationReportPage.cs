using AventStack.ExtentReports;
using DataDrivenProject;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MRO.ROI.Automation.Selenium;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using System;
using System.Collections.ObjectModel;
using System.Net;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;
using System.Collections.Generic;
using OpenQA.Selenium.Support.UI;
using MRO.ROI.Automation.Common;

namespace MRO.ROI.Automation.Pages
{
    public class ROIAdminIssueValidationReportPage
    {
        public RemoteWebDriver Driver;
        public ExtentTest logger;
        public TestContext Context;
        public ROIAdminIssueValidationReportPage(RemoteWebDriver driver, ExtentTest _loger, TestContext _context)
        {
            Driver = driver;
            logger = _loger;
            Context = _context;
        }
        public By btnCreateReport = By.XPath("//input[@id='btn_submit']");       
        public By chkIncludeTest = By.XPath("//input[@name='bIncludeTestDemo']");
        public By selectDatePicker = By.XPath("(//span[@id='daterange'])[1]");
        public By fromDate = By.XPath("//input[@name ='daterangepicker_start']");
        public By toDate = By.XPath("//input[@name ='daterangepicker_end']");
        public By lnkToday = By.XPath("//div[@class='ranges']/ul/li[1]");

        public By btnApply = By.XPath("//button[text()='Apply']");
        public By selectLocationDrp = By.XPath("//select[@id='nLocationID']");
        public By IssueValidationReportFrame = By.XPath("//iframe[starts-with(@id,'rdFrame')]");
        public By dateRange = By.XPath("//span[@id='range-label']");
        public By excelIcon = By.XPath("//span[@id ='div_Excel_icon']");        
        public By lbl_Excel_icon = By.XPath("//i[@id='lbl_Excel_icon']");        
        public By btnClearFilters = By.XPath("//input[@id='btn_Clear']");       

        public By selectDate = By.XPath("//div[@class='calendar left']//table[@class='table-condensed']//tbody/tr[1]/td[2]");        
        public By selectMonthDrp = By.XPath("//div[@class ='calendar left']//select[@class='monthselect']");
        public By selectYearDrp = By.XPath("//div[@class ='calendar left']//select[@class='yearselect']");      
        public By breakOutDrp = By.XPath("//select[@id='nBreakoutID']");
        public By requestsLogged = By.XPath("(//*[@id='show_div_drilldown_container']/span)[1]");
       
        /// <summary>
        /// Create new Issue Validation Report
        /// </summary>
        public void CreateReportForIssueValidation()
        {
            try
            {
                Iframe frame = new Iframe(Driver, logger, Context);

                frame.SwitchToRoiFrame();
                frame.SwitchToRDFrame();
                Driver.Click(btnClearFilters);
                CheckIncludeTestCheckBox();
                Driver.RefreshWebPage();
                Driver.WaitUntilDOMLoaded();
                Driver.WaitForPageToLoad();
                frame.SwitchToRoiFrame();
                frame.SwitchToRDFrame();
                IWebElement datePicker = Driver.FindElementBy(selectDatePicker);
                datePicker.Click();
                Driver.Click(lnkToday);
                datePicker.Click();                
                SelectElement oSelect2 = new SelectElement(Driver.FindElementBy(selectYearDrp));
                oSelect2.SelectByText("2020");
                Driver.Click(selectDate);                
                IWebElement applyButton = Driver.FindElementBy(btnApply);
                Driver.WaitInSeconds(2);
                applyButton.Click();
                Driver.SendKeys(breakOutDrp, "Issue");           
                Driver.Click(btnCreateReport);
                Driver.SleepTheThread(3);
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to create Reconciliation report : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }

        }

       

        /// <summary>
        /// To check Include Test  check box
        /// </summary>
        public void CheckIncludeTestCheckBox()
        {
            try
            {
                bool isChecked = Driver.FindElementBy(chkIncludeTest).Selected;
                if (isChecked == false)
                {
                    IWebElement includeTestChk = Driver.FindElementBy(chkIncludeTest);
                    includeTestChk.Click();
                }

            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to check Include Test checkbox  with execption details as: {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }

        }

      

       

        /// <summary>
        /// Click on excel icon
        /// </summary>
        public void ClickOnExportToExcel()
        {
            try
            {
                IWebElement icon = Driver.FindElementBy(lbl_Excel_icon);
                icon.Click();
                Driver.SleepTheThread(7);
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to click on export to excel icon : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }


        public string GetNumberOfRequestsLogged()
        {
            try
            {
                string numOfRequests = Driver.GetText(requestsLogged);
                return numOfRequests;
            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to get number of requests logged  with exception details as: {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }

    }
    }

