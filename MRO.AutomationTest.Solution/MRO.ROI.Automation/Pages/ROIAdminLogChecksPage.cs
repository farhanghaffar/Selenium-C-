using AventStack.ExtentReports;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MRO.ROI.Automation.Selenium;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MRO.ROI.Automation.Pages
{
    public class ROIAdminLogChecksPage
    {
        public RemoteWebDriver Driver;
        public ExtentTest logger;
        public TestContext Context;
        public ROIAdminLogChecksPage(RemoteWebDriver driver, ExtentTest _loger, TestContext _context)
        {
            Driver = driver;
            logger = _loger;
            Context = _context;
        }

        public By CheckNumber = By.XPath("//input[@id='mrocontent_txtCheckNumber']");
        public By PaymentAmount = By.XPath("//input[@id='mrocontent_txtPaymentAmount']");
        public By LogCheck = By.XPath("//input[@id='mrocontent_cmdLogCheck']");
        public By Yes = By.XPath("//button[contains(text(),'Yes')]");
        public By LogRequestError = By.XPath("//span[@id='mrocontent_lblBalanceDue']");
        public By BalanceDueTxt = By.XPath("//td[@id='mrocontent_tdBalanceDue']");

        public void ClickLogCheck()
        {
            IWebElement checkNumber = Driver.FindElementBy(CheckNumber);
            checkNumber.SendKeys("1254");

            IWebElement paymentAmount = Driver.FindElementBy(PaymentAmount);
            paymentAmount.SendKeys("10");

            IWebElement logCheck = Driver.FindElementBy(LogCheck);
            logCheck.Click();

            IWebElement yes = Driver.FindElementBy(Yes);
            yes.Click();
        }
        public void PayAmountLessThanBalAmount()
        {
            try
            {                
                IWebElement checkNumber = Driver.FindElementBy(CheckNumber);
                checkNumber.SendKeys("1254");               
                string amountToBePaid = Driver.FindElementBy(BalanceDueTxt).Text.ToString(); 
                decimal amountTxt = Convert.ToDecimal(amountToBePaid);
                int amountToBePaidTxt = (int)amountTxt;
                int paymentAmount = amountToBePaidTxt - 1;
                IWebElement paymentAmountTxt = Driver.FindElementBy(PaymentAmount);
                paymentAmountTxt.SendKeys(paymentAmount.ToString());
                IWebElement logCheck = Driver.FindElementBy(LogCheck);
                logCheck.Click();
                IWebElement yes = Driver.FindElementBy(Yes);
                yes.Click();
            }
                catch (Exception ex)
            {
                throw new Exception($"Failed to pay amount less than balance amount Message : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }

        public bool VerifyRequestHasOutstandingDue()
        {
            try
            {
                bool _isDisplayed = false;
                Driver.ScrollToElement(LogRequestError);
                IWebElement requestError = Driver.FindElementBy(LogRequestError);
                if (requestError.Displayed == true)
                {
                    _isDisplayed = true;
                }
                else {
                    _isDisplayed = false;
                }
                return _isDisplayed;

            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to verify request has outstanding due amount Message : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
           
        }
    }

}
