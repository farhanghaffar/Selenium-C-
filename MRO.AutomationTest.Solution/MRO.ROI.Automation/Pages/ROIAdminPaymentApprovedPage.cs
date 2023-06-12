using AventStack.ExtentReports;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MRO.ROI.Automation.Selenium;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.UI;
using System;

namespace MRO.ROI.Automation.Pages
{
    public class ROIAdminPaymentApprovedPage
    {
        public RemoteWebDriver Driver;
        public ExtentTest logger;
        public TestContext Context;
        public ROIAdminPaymentApprovedPage(RemoteWebDriver driver, ExtentTest _loger, TestContext _context)
        {
            Driver = driver;
            logger = _loger;
            Context = _context;
        }

        By approvalCode = By.XPath("//*[contains(text(),'Approval')]//span");
        By viewCreditCardReceiptButton = By.XPath("//input[@value='View Credit Card Receipt']");
        By returnToFindRequestPage = By.XPath("//a[@id='mrocontent_lnkStatus']");
        By reportsMenu = By.XPath("//td[text()='Reports']");
        By ccTransactionReportSubmenu = By.XPath("//td[contains(text(),'CC Transactions Report')]");
        By refreshBtn = By.XPath("//input[@title='Refresh']");
        By findARequestPageLink = By.XPath("//a[@id='mrocontent_lnkStatus']");

        public string ReturnApprovalCode()
        {
            try
            {
                return Driver.GetText(approvalCode);
            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to return approval code Message : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }

        public void GoToCCPrintableReceiptPage()
        {
            try
            {
                Driver.DirectClick(viewCreditCardReceiptButton);
            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to navigate to cc printable receipt page Message : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }

        public void GoToFindARequestPage()
        {
            try
            {
                
                Driver.DirectClick(returnToFindRequestPage);
            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to navigate to cc printable receipt page Message : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }

        /// <summary>
        /// Go to reports and cctransaction report
        /// </summary>        
        public void GoToReportsAndSelectCCTransactionReport()
        {
            try
            {
                Driver.Wait(TimeSpan.FromSeconds(5));
                Driver.DirectClick(reportsMenu);
                Driver.Wait(TimeSpan.FromSeconds(2));
                Driver.JavaScriptClick(ccTransactionReportSubmenu);
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to go to reports and select cc transaction reports menu: {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }

        public void RefreshPaymentHistory()
        {
            try
            {
                Driver.DirectClick(refreshBtn);
            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to refresh payment history : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }

        public void ReturnToFindRequestPage()
        {
            try
            {
                Driver.Click(findARequestPageLink);
                Driver.WaitForPageToLoad();
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to go to Find a request page: {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }

    }
}
