using AventStack.ExtentReports;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MRO.ROI.Automation.Selenium;
using MRO.ROI.Automation.Utility;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using System;

namespace MRO.ROI.Automation.Pages
{
    public class ROIFacilityBatchProcessingReportPage
    {
        public RemoteWebDriver Driver;
        public ExtentTest logger;
        public TestContext Context;
        public CSVReader csvReader;
        public ROIFacilityBatchProcessingReportPage(RemoteWebDriver driver, ExtentTest _loger, TestContext _context)
        {
            Driver = driver;
            logger = _loger;
            Context = _context;
        }        
        public By batchDateFrom = By.XPath("//input[@id='mrocontent_txtDateA']");
        public By batchDateTo = By.XPath("//input[@id='mrocontent_txtDateZ']");
        public By createListBtn = By.XPath("//input[@id='mrocontent_cmdCreate']");
        public By systemBatchNumberTxt = By.XPath("//input[@id='mrocontent_txtImportBatchID']");
        public By batchStatusLnk = By.XPath("//tr[@class='TableBody']//td[2]");
        public By selectAllBtn = By.XPath("//input[@id='mrocontent_btnSelAll']");
        public By completeSelectedBtn = By.XPath("//input[@id='mrocontent_cmdCompleteSelected']");
        public By includeCompleteChk = By.XPath("//input[@id='mrocontent_cbComplete']");
        public By batchNumberTxt = By.XPath("//*[@id='mrocontent_dgReport']/tbody/tr[2]/td[1]");
        public By txtStatus = By.XPath("//*[@id='mrocontent_dgReport']/tbody/tr[2]/td[2]");
        public By clearFields = By.XPath("//input[@id='mrocontent_cmdClear']");


        public void CreateBatchProcessingReport(string batchId)
        {
            try
            {
                var todaysDate = String.Format("{0:M/dd/yyyy}", DateTime.Now).Replace("-", "/");
                Driver.Click(clearFields);
                Driver.ClearText(systemBatchNumberTxt);
                Driver.SendKeys(systemBatchNumberTxt, batchId);
                Driver.ClearText(batchDateFrom);
                Driver.SendKeys(batchDateFrom, todaysDate);
                Driver.ClearText(batchDateTo);
                Driver.SendKeys(batchDateTo, todaysDate);
                Driver.Click(createListBtn);
                Driver.SleepTheThread(5);
                Driver.Click(batchStatusLnk);
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to create batch processing report with details as Message : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }
        public void SelectAllAndMakeRequest()
        {
            try
            {
                Driver.Click(selectAllBtn);
                Driver.Click(completeSelectedBtn);
                Driver.SwitchTo().Alert().Accept();
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to select all and make request with details as Message : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }

        }
        public void CreateBatchProcessingReportAndIncludeCompleteChk(string batchId)
        {
            try
            {
                var todaysDate = String.Format("{0:M/dd/yyyy}", DateTime.Now).Replace("-", "/");
                Driver.Click(clearFields);
                Driver.ClearText(systemBatchNumberTxt);
                Driver.SendKeys(systemBatchNumberTxt, batchId);
                Driver.ClearText(batchDateFrom);
                Driver.SendKeys(batchDateFrom, todaysDate);
                Driver.ClearText(batchDateTo);
                Driver.SendKeys(batchDateTo, todaysDate);
                Driver.Click(includeCompleteChk);
                Driver.Click(createListBtn);
                Driver.SleepTheThread(5);                
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to create batch processing report with details as Message : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }
        public string VerifyBatchNumber()
        {
            try
            {
                string txtBatchNumber = Driver.FindElementBy(batchNumberTxt).Text;
                return txtBatchNumber;
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to verify batch number with details as Message : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }
        public string VerifyStatusOfBatchReport()
        {
            try
            {
                string statusTxt = Driver.FindElementBy(txtStatus).Text;
                return statusTxt;
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to verify batch status with details as Message : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }
    }
}
