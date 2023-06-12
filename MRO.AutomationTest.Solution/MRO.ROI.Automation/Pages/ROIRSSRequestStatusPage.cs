using AventStack.ExtentReports;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MRO.ROI.Automation.Selenium;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.UI;
using System;

namespace MRO.ROI.Automation.Pages
{
    public class ROIRSSRequestStatusPage
    {
        public RemoteWebDriver Driver;
        public ExtentTest logger;
        public TestContext Context;
        public ROIRSSRequestStatusPage(RemoteWebDriver driver, ExtentTest _loger, TestContext _context)
        {
            Driver = driver;
            logger = _loger;
            Context = _context;
        }


        public By viewActionsBtn = By.XPath("//span[contains(text(),' View Actions ')]");
        public By headingEle = By.XPath("//div[@class='col col-8']");
        public By openIssuesBtn = By.XPath("//button[@class='text-none page-header-btn v-btn v-btn--contained theme--light v-size--small error']");
        public By reqStatus = By.XPath("//div[@class='page-heading']//div[@class='col col-8']");
        public By lookUpRequestId = By.XPath("//button[@class='v-icon notranslate v-icon--link mdi mdi-help theme--light']");
        public void ClickViewAction()
        {
            try
            {
                Driver.Click(viewActionsBtn);
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to click view actions  button with message as : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }

        public string VerifyOpenIssuesHeader()
        {
            try
            {
                string heading = Driver.GetText(headingEle);
                return heading;
            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to return header with details as : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }

        public void ClickOpenIssues()
        {
            try
            {
                Driver.Click(openIssuesBtn);
            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to click open issues with details as : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }

        public string VerifyRequestStatus()
        {

            try
            {
                string status = Driver.GetText(reqStatus);
                return status;
            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to get request status with details as : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }

        public void SearchByRequestId(string RequestId)
        {
            try
            {
                IWebElement requestId = Driver.FindElementBy(lookUpRequestId);
                requestId.Click();
                Driver.FindElementBy(By.XPath("//div[@class='v-text-field__slot']//input")).SendKeys(RequestId);
                logger.Log(Status.Info, $"Entered request id ({RequestId})");
                Driver.Click(By.XPath("//span[contains(text(),'Full Req Status')]"));
                //Driver.SwitchTo().Alert().Accept();
                Driver.Wait(TimeSpan.FromSeconds(2));

            }
            catch (Exception ex)
            {

                throw new Exception($"Failed with Message not able to click '?' : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }
    }
}
