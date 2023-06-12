using AventStack.ExtentReports;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MRO.ROI.Automation.Selenium;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using System;

namespace MRO.ROI.Automation.Pages
{
    public class ROIAdminLedgerDetailPage
    {

        public RemoteWebDriver Driver;
        public ExtentTest logger;
        public TestContext Context;        
        public ROIAdminLedgerDetailPage(RemoteWebDriver driver, ExtentTest _loger, TestContext _context)
        {
            Driver = driver;
            logger = _loger;
            Context = _context;
        }

        public By writeoffsButton = By.XPath("//input[@id='mrocontent_cmdWriteOffs']");
        public By writeoffsPage = By.XPath("//td[@id='MasterHeaderText']");
        public By invoiceId = By.XPath("//table[@id='mrocontent_dgDetail']//tr[5]//td[1]");
        /// <summary>
        /// Click on writeoff's button
        /// </summary>
        public void ClickOnWriteOffsButton()
        {
            bool isDisplayed = false;
            try
            {
                Driver.Click(writeoffsButton);
                IWebElement verifyWriteoffsPage = Driver.FindElementBy(writeoffsPage);
                if (verifyWriteoffsPage.Displayed == true)
                {
                    isDisplayed = true;
                    logger.Log(Status.Info, "Writeoff's window opened successfully");
                }
                else
                {
                    isDisplayed = false;
                    logger.Log(Status.Info, "Failed to open writeoff's window ");
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to click on writeoff's  : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }

        }
        public string VerifyInvoiceId()
        {
            try
            {

                string id = Driver.GetText(invoiceId);
                Driver.ScrollToElement(invoiceId);
                return id;
            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to return invoice id  : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }

        }
    }
}
