using AventStack.ExtentReports;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MRO.ROI.Automation.Selenium;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.UI;
using System;

namespace MRO.ROI.Automation.Pages
{
    public class ROIAdminCreditCardPaymentReceiptPage
    {
        public RemoteWebDriver Driver;
        public ExtentTest logger;
        public TestContext Context;
        public ROIAdminCreditCardPaymentReceiptPage(RemoteWebDriver driver, ExtentTest _loger, TestContext _context)
        {
            Driver = driver;
            logger = _loger;
            Context = _context;
        }
        By clearAllBtn = By.XPath("//input[@id='mrocontent_cmdClearAll']");
        By approvalStatus = By.XPath("//select[@id='mrocontent_ddlApproveStatus']");
        public const string approvedTxt = "Approved";
        By fromDate = By.XPath("//input[@id='mrocontent_txtDateFrom']");
        By toDate = By.XPath("//input[@id='mrocontent_txtDateTo']");
        By createReport = By.XPath("//input[@id='mrocontent_cmdResults']");
        By dateFilter = By.XPath("//img[@id='mrocontent_dgCCRequests_tblCCRequests_imgSortSwitch']");
        By approvalStatusTxt = By.XPath("//tr[@class='TableBody'][1]//td//a[contains(text(),'Approved')]");
        By totalAmountTxt = By.XPath("//tr[@class='TableBody'][1]//td[4]");
        By excelIcon = By.XPath("//input[@id='mrocontent_btnExportExcel']");
        By requestIdTxt = By.XPath("//input[@id='mrocontent_txtRequestID']");
        By inncludeTestChk = By.XPath("//input[@id='mrocontent_cbIncludeTest']");


        public void CreateTransactionReport(string requestid)
        {
            try
            {
                Driver.Click(clearAllBtn);
                Driver.SendKeys(requestIdTxt, requestid);
                SelectElement approvalDrp = new SelectElement(Driver.FindElement(approvalStatus));
                approvalDrp.SelectByText(approvedTxt);
                var fromDateTxt = Driver.FindElementBy(fromDate);
                fromDateTxt.Clear();
                fromDateTxt.SendKeys(DateTime.Now.AddMonths(-1).ToString("MM/dd/yyy"));
                var todaysDate = String.Format("{0:M/dd/yyyy}", DateTime.Now).Replace("-", "/");
                var toDateTxt = Driver.FindElementBy(toDate);
                toDateTxt.Clear();
                toDateTxt.SendKeys(todaysDate);                
                Driver.Click(inncludeTestChk);                
                Driver.Click(createReport);
                Driver.SleepTheThread(5);

            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to create transactional report with Message : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }
        public void SortDate()
        {
            try
            {
                string titleTxt = Driver.FindElementBy(dateFilter).GetAttribute("title").ToString();
                if (titleTxt == "Sort Forward")
                {
                    Driver.Click(dateFilter);
                }

            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to sort date with Message : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }
        public string GetApprovalStatus()
        {
            try
            {
                string statusTxt = Driver.GetText(approvalStatusTxt);
                return statusTxt;
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to get approval status with Message : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }
        public string GetTotalAmount()
        {
            try
            {
                string totalAmount = Driver.GetText(totalAmountTxt);
                return totalAmount;
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to get total amount with Message : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }
        public void ClickOnExcelIcon()
        {
            try
            {
                Driver.Click(excelIcon);
                Driver.SleepTheThread(10);
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to click on execl with Message : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }
    }
}
