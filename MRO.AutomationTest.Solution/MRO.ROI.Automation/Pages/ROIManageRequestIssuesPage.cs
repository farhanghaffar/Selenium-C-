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
    public class ROIManageRequestIssuesPage
    {
        public RemoteWebDriver Driver;
        public ExtentTest logger;
        public TestContext Context;
        public CSVReader csvReader;
        public static string csvFileName = "CreateRequestAndChangeAPIWebServiceStatus.csv";
        public ROIManageRequestIssuesPage(RemoteWebDriver driver, ExtentTest _loger, TestContext _context)
        {
            Driver = driver;
            logger = _loger;
            Context = _context;
        }
        public By testforIssue = By.XPath("//td[contains(text(),'Test for Issues')]");
        public By doneBtn = By.XPath("//input[@id='mrocontent_cmdInfoDone']");
        public By editIssueIcon = By.XPath("//img[@title='Edit Issue']");
        public By notesValue = By.XPath("//*[@id='Table2']/tbody/tr[4]/td[2]");
        public By issueName = By.XPath("//table[@id='Table2']//tbody//tr[2]//td[@class='TableAlert']//a");
        public By authorizationIssue = By.XPath("(//option[contains(text(),'Authorization Not Signed')])[1]");
        public By closeBtn = By.XPath("//input[@id='mrocontent_cmdClose']");
        public By doneeditBtn = By.XPath("//input[@id='mrocontent_cmdEditDone']");


        public string VerifyNotes()
        {
            try
            {
                string notes = Driver.GetText(notesValue);
                return notes;

            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to verify notes : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }
        public void ClickOnDone()
        {
            try
            {
                Driver.Click(doneBtn);
            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to click on done : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }

        public void ClickOnIssueEditIcon()
        {
            try
            {
                Driver.Click(editIssueIcon);
            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to click on edit icon : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }

        public string VerifyIssue()
        {
            try
            {
                string issue = Driver.GetText(issueName);
                return issue;

            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to verify issue : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }
        public void ClickAuthorizationIssue()
        {
            try
            {
                Driver.Click(authorizationIssue);
                Driver.Click(closeBtn);
                Driver.Wait(TimeSpan.FromSeconds(2));
                Driver.SwitchTo().Alert().Accept();
                Driver.Click(doneeditBtn);

            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to verify issue : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }

        }
    }
}
