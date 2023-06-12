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
    public class ROIAdminAddIssuePage
    {
        public RemoteWebDriver Driver;
        public ExtentTest logger;
        public TestContext Context;
        public CSVReader csvReader;
        public static string csvFileName = "CreateRequestAndChangeAPIWebServiceStatus.csv";
        public ROIAdminAddIssuePage(RemoteWebDriver driver, ExtentTest _loger, TestContext _context)
        {
            Driver = driver;
            logger = _loger;
            Context = _context;
        }

        public By issueDropDown = By.XPath("//input[@id='mrocontent_cmbBxIssues_Input']");
        public By AddIssueButton = By.XPath("//input[@id='mrocontent_cmdAdd']");
        public static string selectedIssue = "Cover Letter Missing";
        public By commentsTxtbox = By.XPath("//textarea[@id='mrocontent_txtAddComments']");
        public By addissueBtn = By.XPath("//input[@id='mrocontent_cmdAddIssue']");
        public static string comments = "test";

        public void SetIssue()
        {
            try
            {
                csvReader = new CSVReader(Path.GetFullPath(Path.Combine(Assembly.GetExecutingAssembly().Location, "..", "TestData", csvFileName)));
                string issueDDValue = csvReader.GetDataFromCSVFile("IssueDropDown");
                Driver.FindElementBy(issueDropDown).SendKeys(issueDDValue);
                Driver.FindElementBy(AddIssueButton).Click();
            }
            catch (Exception ex)
            {

                throw new Exception($"Failed with Message not able to click '?' : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }

        public void SetIssueANS()
        {
            try
            {
                csvReader = new CSVReader(Path.GetFullPath(Path.Combine(Assembly.GetExecutingAssembly().Location, "..", "TestData", csvFileName)));
                string issueDDValue = csvReader.GetDataFromCSVFile("IssueDropDownANS");
                Driver.FindElementBy(issueDropDown).SendKeys(issueDDValue);
                Driver.FindElementBy(AddIssueButton).Click();
            }
            catch (Exception ex)
            {

                throw new Exception($"Failed with Message not able to click '?' : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }

        public string AddIssue()
        {
            try
            {

                Driver.FindElementBy(issueDropDown).SendKeys(selectedIssue);
                Driver.SendKeys(commentsTxtbox, comments);
                string selectedIssueType = Driver.FindElementBy(issueDropDown).GetAttribute("value");
                Driver.FindElementBy(AddIssueButton).Click();
                Driver.Wait(TimeSpan.FromSeconds(5));
                Driver.ScrollToElement(addissueBtn);
                return selectedIssueType;
            }
            catch (Exception ex)
            {

                throw new Exception($"Failed  to add issue with details as : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }


        public string AddIssueWithType(string issuename, string comment)
        {
            try
            {

                Driver.FindElementBy(issueDropDown).SendKeys(issuename);
                Driver.Wait(TimeSpan.FromSeconds(2));
                Driver.Click(commentsTxtbox);
                Driver.SendKeys(commentsTxtbox, comment);
                Driver.Wait(TimeSpan.FromSeconds(2));
                string selectedIssueType = Driver.FindElementBy(issueDropDown).GetAttribute("value");
                Driver.FindElementBy(AddIssueButton).Click();
                Driver.Wait(TimeSpan.FromSeconds(5));
                Driver.ScrollToElement(addissueBtn);
                return selectedIssueType;
            }
            catch (Exception ex)
            {

                throw new Exception($"Failed  to add issue with details as : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }
    }
}
