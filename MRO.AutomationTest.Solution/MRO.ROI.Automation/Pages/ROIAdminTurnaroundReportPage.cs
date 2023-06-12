using AventStack.ExtentReports;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MRO.ROI.Automation.Selenium;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace MRO.ROI.Automation.Pages
{
    public class ROIAdminTurnaroundReportPage
    {
        public RemoteWebDriver Driver;
        public ExtentTest logger;
        public TestContext Context;
       
        public ROIAdminTurnaroundReportPage(RemoteWebDriver driver, ExtentTest _loger, TestContext _context)
        {
            Driver = driver;
            logger = _loger;
            Context = _context;
        }
        public By shippedRadioButton = By.XPath("//input[@id='mrocontent_rbShipped']");
        public By drpUserType = By.XPath("//select[@id='mrocontent_lstUserTypes']");
        public By loggedRadioButton = By.XPath("//input[@id='mrocontent_rbLogged']");
        public By chkFacilityName = By.XPath("//input[@name ='mrocontent$ddlFacility' and @value='Texas Cardiology Consultants']");
        public By btnFacility = By.XPath("//a[@id ='mrocontent_ddlFacility_Arrow']");
        public By drpMonth = By.XPath("//select[@id='mrocontent_lstMonth']");
        public By btnExportToExcel = By.XPath("//input[@id='mrocontent_cmdCreate']");
        public By releasedRadioButton = By.XPath("//input[@id='mrocontent_rbReleased']");

        public bool GetTheVisibilityStatusOfUserTypeDropdown()
        {
            bool isDisabled = false;
            try
            {
                IWebElement shippedButton = Driver.FindElementBy(shippedRadioButton);
                shippedButton.Click();
                Driver.SleepTheThread(2);
                IWebElement userTypeDropdown = Driver.FindElementBy(drpUserType);
                if (userTypeDropdown != null)
                {
                    string visibilityStatus = userTypeDropdown.GetAttribute("disabled");

                    if (visibilityStatus == "true")
                    {
                        isDisabled = true;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to get the status of usertype dropdown with Exception details: {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
            return isDisabled;
           
        }

        /// <summary>
        /// Create new Turn Around Report
        /// </summary>
        public void ApplyFiltersAndCreateReport()
        {
            try
            {
                IWebElement loggedButton = Driver.FindElementBy(loggedRadioButton);
                loggedButton.Click();
                SelectElement oSelect1 = new SelectElement(Driver.FindElementBy(drpMonth));
                oSelect1.SelectByText("June");
                IWebElement iYear = Driver.FindElementBy(By.Id("mrocontent_txtYear"));
                iYear.SendKeys("2018");
                Driver.Click(btnFacility);
                Driver.Click(chkFacilityName);
                Driver.SwitchToDefaultContent();             
                Driver.Click(btnExportToExcel);
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to create MRO turn around report with exception detail as : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }

        public string ReadXLSFileFromDirectory(string path)
        {
            string fileName = string.Empty;
            try
            {
                DirectoryInfo Dir = new DirectoryInfo(path);
                List<FileInfo> files = Dir.GetFiles().ToList();

                foreach (FileInfo fileinfo in files)
                {
                    if (fileinfo.Extension == ".xlsx" && fileinfo.Name.Contains("_TurnAround_2018"))
                    {
                        System.Threading.Thread.Sleep(1000);
                        fileName = fileinfo.ToString().Replace(".xlsx", "").Trim();
                    }
                }

            }


            catch (Exception ex)
            {
                throw new Exception($"Failed to read file from directory whose exception message is :{ex.Message} \n whose stack trace is :{ex.StackTrace}");
            }
            return fileName;
        }

        public bool GetTheStatusOfUserTypeDropdown()
        {
            bool isDisabled = false;
            try
            {
                IWebElement loggedButton = Driver.FindElementBy(loggedRadioButton);
                loggedButton.Click();
                Driver.SleepTheThread(2);
                IWebElement userTypeDropdown = Driver.FindElementBy(drpUserType);
                if (userTypeDropdown != null)
                {
                    string visibilityStatus = userTypeDropdown.GetAttribute("disabled");

                    if (visibilityStatus == "true")
                    {
                        isDisabled = true;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to get the status of usertype dropdown with Exception details: {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
            return isDisabled;

        }

        /// <summary>
        /// Create new Turn Around Report with new filters
        /// </summary>
        public void ReapplyFiltersAndCreateReport()
        {
            try
            {
                IWebElement releasedButton = Driver.FindElementBy(releasedRadioButton);
                releasedButton.Click();
                SelectElement selectUserType = new SelectElement(Driver.FindElementBy(drpUserType));
                selectUserType.SelectByText("Field Staff");               
                Driver.Click(btnExportToExcel);
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to create MRO turn around report with change in filters with exception detail as : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }
    }
}
