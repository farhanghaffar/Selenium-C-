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
    public class ROIRequestIssuesPage
    {
        public RemoteWebDriver Driver;
        public ExtentTest logger;
        public TestContext Context;
        public CSVReader csvReader;
        public static string csvFileName = "CreateRequestAndChangeAPIWebServiceStatus.csv";
        public ROIRequestIssuesPage(RemoteWebDriver driver, ExtentTest _loger, TestContext _context)
        {
            Driver = driver;
            logger = _loger;
            Context = _context;
        }
        public By commentsTxtBox = By.XPath("//textarea[@name='mrocontent$txtEditComments']");
        public By updateCommentBtn = By.XPath("//input[@id='mrocontent_cmdUpdateComment']");
        


        public string VerifyComments()
        {
            try
            {
                string comments = Driver.GetText(commentsTxtBox);
                return comments;

            }
            catch (Exception ex)
            {

                throw new Exception($"Failed get comments value : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }
        public void ClearCommentsTextarea(string newcomment)
        {
            try
            {
                Driver.ClearContent(commentsTxtBox);
                Driver.SendKeys(commentsTxtBox, newcomment);
                Driver.Click(updateCommentBtn);
                Driver.SwitchTo().Alert().Accept();

            }
            catch (Exception ex)
            {

                throw new Exception($"Failed clear comments : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }
       


    }
}
