using AventStack.ExtentReports;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MRO.ROI.Automation.Selenium;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.UI;
using System;

namespace MRO.ROI.Automation.Pages
{
    public class ROIInvoicingQueuePage
    {
        public RemoteWebDriver Driver;
        public ExtentTest logger;
        public TestContext Context;
        public ROIInvoicingQueuePage(RemoteWebDriver driver, ExtentTest _loger, TestContext _context)
        {
            Driver = driver;
            logger = _loger;
            Context = _context;
        }
        public By newSearchBtn = By.XPath("//input[@id='mrocontent_cmdNewSearch']");
        public By logDate = By.XPath("//input[@id='mrocontent_txtLogDate']");
        public By includeTestChkbox = By.XPath("//input[@id='mrocontent_cbIncludeTest']");
        public By invoicedDrp = By.XPath("//select[@id='mrocontent_lstInvoicedOnly']");
        public By searchBtn = By.XPath("//input[@id='mrocontent_cmdSearch']");
        public By requestIdElement = By.XPath("//*[@id='mrocontent_dgRequests']/tbody/tr//td[1]");
        public By startProcessBtn = By.XPath("//input[@id='mrocontent_btnStart']");
        public By processingStatus = By.XPath("//span[@id='mrocontent_lblProcessStatus']");

        public void ClickOnSearch()
        {
            try
            {

                var todaysDate = String.Format("{0:M/dd/yyyy}", DateTime.Now).Replace("-", "/");
                Driver.Click(newSearchBtn);
                Driver.Wait(TimeSpan.FromSeconds(2));
                Driver.SendKeys(logDate, todaysDate);
                if(Driver.FindElementBy(includeTestChkbox).Selected==false)
                {
                    Driver.Click(includeTestChkbox);
                }
                
                Driver.SendKeys(invoicedDrp, "[Select All]");
                Driver.Click(searchBtn);

            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to click search with message as : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }

       public bool CheckRequestIdsExistsOrNot(string requestId)
        {
            bool isDisplayed=false;
            try
            {


                var requestElements = Driver.FindElementsBy(requestIdElement);
                foreach (var requestElement in requestElements)
                {

                    if (requestElement.Text.Equals(requestId))
                    {
                        isDisplayed = true;
                        break;
                    }
                }
            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to click search with message as : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
            return isDisplayed;
        }

        public bool VerifyProcessingStatus()
        {
            try
            {
                string isProcessingStatus = "//span[@id='mrocontent_lblProcessStatus']";
                WebElementHelper helper = new WebElementHelper(Driver, logger, Context);
                bool isDisplayed = helper.IsElementPresent(isProcessingStatus);
                return isDisplayed;

            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to verify processing status with message as : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }


        public bool VerifyStartProcess()
        {
            try
            {
                string isProcessingStatus = "//input[@id='mrocontent_btnStart']";
                WebElementHelper helper = new WebElementHelper(Driver, logger, Context);
                bool isDisplayed = helper.IsElementPresent(isProcessingStatus);
                return isDisplayed;

            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to verify processing status with message as : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }
        public bool VerifyEndProcess()
        {
            try
            {
                string isProcessingStatus = "//input[@id='mrocontent_btnEnd']";
                WebElementHelper helper = new WebElementHelper(Driver, logger, Context);
                bool isDisplayed = helper.IsElementPresent(isProcessingStatus);
                return isDisplayed;

            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to verify processing status with message as : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }

        public void ClickOnStartProcess()
        {
            try
            {
                Driver.Click(startProcessBtn);
                

            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to verify processing status with message as : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }

        public string ReturnProcessingStatus()
        {
            try
            {
                string processingVal = Driver.GetText(processingStatus);
                return processingVal;

            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to verify processing status with message as : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }

        public bool VerifyResumeProcess()
        {
            try
            {
                string isResumeProcess = "//input[@id='mrocontent_btnStart']";
                WebElementHelper helper = new WebElementHelper(Driver, logger, Context);
                bool isDisplayed = helper.IsElementPresent(isResumeProcess);
                return isDisplayed;

            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to verify resume process with message as : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }
    }
}
