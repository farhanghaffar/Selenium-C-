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
    public class ROIActionSummaryReportPage
    {
        public RemoteWebDriver Driver;
        public ExtentTest logger;
        public TestContext Context;
        public CSVReader csvReader;
        public static string csvFileName = "CreateRequestAndChangeAPIWebServiceStatus.csv";
        public ROIActionSummaryReportPage(RemoteWebDriver driver, ExtentTest _loger, TestContext _context)
        {
            Driver = driver;
            logger = _loger;
            Context = _context;
        }

        public By username = By.XPath("//select[@id='mrocontent_lstUserID']//option");
        public By usernameDrp = By.XPath("//select[@id='mrocontent_lstUserID']");
        public By fromDate = By.Id("mrocontent_txtDateA");
        public By toDate = By.Id("mrocontent_txtDateZ");
        public By summaryBtn = By.XPath("//input[@id='mrocontent_cmdCreateSummary']");
        public By excelIcon = By.XPath("//input[@id='mrocontent_lnkExport']");

        public string VerifyUsername()
        {
            try
            {
                string user = Driver.GetText(username);
                user = user.Split(',')[1].ToString().Trim();
                return user;

            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to verify username : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }

        public bool CheckUsernameDisabled()
        {
            bool isDisabled = false;
            try
            {
                IWebElement userDrp = Driver.FindElementBy(usernameDrp);

                if (userDrp != null)
                {
                    string value = userDrp.GetAttribute("disabled");

                    if (value == "true")
                    {
                        isDisabled = true;
                    }

                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to  verify user name dropdown, Exception detail: {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
            return isDisabled;
        }

        public void ClickOnSummaryButton()
        {
            try
            {
                int currentmonth = DateTime.Now.Month;
                int previousmonth = currentmonth - 6;
                Driver.ClearText(fromDate);
                string fromdate = String.Format("{0:M/dd/yyyy}", DateTime.Now).Replace("-", "/").Replace("M", "previousmonth");
                string todaydate = String.Format("{0:M/dd/yyyy}", DateTime.Now).Replace("-", "/");
                Driver.SendKeys(fromDate, fromdate);
                Driver.ClearText(toDate);
                Driver.SendKeys(toDate, todaydate);
                Driver.Click(summaryBtn);
            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to  click summary button, Exception detail: {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }

        public void ClickOnExcelIcon()
        {
            try
            {
                Driver.Click(excelIcon);
            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to  click excel icon, Exception detail: {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }
    }
}
