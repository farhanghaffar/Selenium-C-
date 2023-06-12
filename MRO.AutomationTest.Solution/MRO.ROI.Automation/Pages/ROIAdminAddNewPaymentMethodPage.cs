using AventStack.ExtentReports;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MRO.ROI.Automation.Pages;
using MRO.ROI.Automation.Pages.Common;
using MRO.ROI.Automation.Selenium;
using MRO.ROI.Automation.Utility;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using System;
using System.IO;
using System.Reflection;
using static MRO.ROI.Automation.Utility.IniFile;

namespace MRO.ROI.Automation.Pages
{
    public class ROIAdminAddNewPaymentMethodPage
    {
        public RemoteWebDriver Driver;
        public ExtentTest logger;
        public TestContext Context;
        public CSVReader csvReader;

        public ROIAdminAddNewPaymentMethodPage(RemoteWebDriver driver, ExtentTest _loger, TestContext _context)
        {
            Driver = driver;
            logger = _loger;
            Context = _context;
        }

        public string paymentIframe = "RadWindow1";
        public By nameOnTheCard = By.XPath("//input[@title ='Name On Card']");
        public By date = By.XPath("//input[@title ='Exp Date (MM/YY)']");
        public By cardNumber = By.XPath("//input[@title ='Card Number']");
        public By cvc = By.XPath("//input[@title ='CVC']");
        public By billingAddress = By.XPath("//input[@title ='Billing Address']");
        public By billingCity = By.XPath("//input[@title ='Billing City']");
        public By billingState = By.XPath("//input[@title ='Billing State/Province/Region']");
        public By billingZipCode = By.XPath("//input[@title ='Billing Zip/Postal Code']");
        public By payButton = By.XPath("//input[@id='cardPay']");
        public By approvalCode = By.XPath("//*[contains(text(),'Approval')]//span");

        public void AddNewPayment()
        {
            try
            {
                Driver.SleepTheThread(4);
                string Name = IniHelper.ReadConfig("CardDetails", "Name");
                string Date = IniHelper.ReadConfig("CardDetails", "Date");
                string CVC = IniHelper.ReadConfig("CardDetails", "CVC");
                string CardNumber = IniHelper.ReadConfig("CardDetails", "CardNumber");
                string BillingAddress = IniHelper.ReadConfig("CardDetails", "BillingAddress");
                string BillingCity = IniHelper.ReadConfig("CardDetails", "BillingCity");
                string BillingState = IniHelper.ReadConfig("CardDetails", "BillingState");
                string BillingZipCode = IniHelper.ReadConfig("CardDetails", "BillingZipCode");

                Driver.SwitchTo().Frame(paymentIframe);
                Driver.SendKeys(nameOnTheCard, Name);
                Driver.SendKeys(date, Date);
                Driver.SendKeys(cvc, CVC);
                Driver.SendKeysUsingJavaScript(cardNumber, CardNumber);
                Driver.SendKeys(billingAddress, BillingAddress);
                Driver.SendKeys(billingCity, BillingCity);
                Driver.SendKeys(billingState, BillingState);
                Driver.SendKeys(billingZipCode, BillingZipCode);
                Driver.SleepTheThread(3);
                Driver.ClickAndCheckNextElement(payButton, approvalCode);
                Driver.SleepTheThread(8);

            }
            catch (Exception ex)
            {

                throw new Exception($"Failed do the add a payment Message : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }
    }
}
