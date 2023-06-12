using AventStack.ExtentReports;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MRO.ROI.Automation.Common.Navigation;
using MRO.ROI.Automation.Pages;
using MRO.ROI.Automation.Pages.Common;
using MRO.ROI.Automation.Selenium;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Remote;
using System;
using static MRO.ROI.Automation.Utility.IniFile;

namespace MRO.ROI.Automation.Pages
{
    public class ROIConfirmDeliveryMethodsPage
    {
        public RemoteWebDriver Driver;
        public ExtentTest logger;
        public TestContext Context;
        public ROIConfirmDeliveryMethodsPage(RemoteWebDriver driver, ExtentTest _loger, TestContext _context)
        {
            Driver = driver;
            logger = _loger;
            Context = _context;
        }


        public By changeDeliveryMethodBtn = By.Id("mrocontent_btn_ChangeDelivery");
        public By continueButton = By.Id("mrocontent_btn_Cont");
        public By mailRadioBtn = By.Id("mrocontent_dgRecordsEnable_radioBtn_Mail_0");
        public By headerElement = By.Id("MasterHeaderText");
        public By updateDeliveryMethodBtn = By.XPath("//input[@value='Update Delivery Methods']");
        public By creditCardRadioButton = By.Id("mrocontent_radioBtn_CC");
        public By totalChangeAmount = By.Id("mrocontent_lblTotal");
        public By creditCardContinueButton = By.Id("mrocontent_btnTCContinue");
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
        public  By approvalCode = By.XPath("//*[contains(text(),'Approval')]//span");
        public By viewCreditCardReceiptButton = By.XPath("//input[@value='View Credit Card Receipt']");
        public By approvalCodeVal = By.Id("mrocontent_spanRef");
        public By code = By.Id("ccReceipt_lblApprovalCode");
        public By requestId = By.XPath("//span[@id='ccReceipt_lblReqRequestID']");



        public void  ClickShowRequestButton()
        {
            try
            {
                Driver.Click(changeDeliveryMethodBtn);
                string header = Driver.GetText(headerElement);
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to click show request button with detail Message as : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");

            }
           
        }

        public void UpdateDeliveryMethod()
        {
            try
            {
                Driver.Click(mailRadioBtn);
                Driver.Click(updateDeliveryMethodBtn);
                Driver.Click(continueButton);
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to update delivery method with detail message as: {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }
        public string SelectCreditCardRadioButton()
        {
            try
            {
                Driver.Click(creditCardRadioButton);
                Driver.Click(continueButton);
                string amount = Driver.GetText(totalChangeAmount);
                Driver.Click(creditCardContinueButton);
                return amount;
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to select credit card radio button Message : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }


        public string GoToCCPrintableReceiptPage()
        {
            try
            {
                Driver.SwitchTo().DefaultContent();
                Driver.DirectClick(viewCreditCardReceiptButton);
                Driver.WaitForPageToLoad();
                Driver.SwitchToWindow("CCPrintableReceipt");
                Driver.Manage().Window.Maximize();
                string reqID = Driver.GetText(requestId);               
                Driver.Wait(TimeSpan.FromSeconds(1));
                return reqID;

            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to navigate to cc printable receipt page Message : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }

        public void CloseCreditCardPaymentWindow()
        {
            try
            {
                Driver.SwitchToWindowAndClose("CCPrintableReceipt");
                Driver.SleepTheThread(3);
                Driver.SwitchToWindow("Payment Approved!");
            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to close credit card payment window with detail Message  as: {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }





        public string AddNewPayment()
        {
            try
            {
                Driver.SleepTheThread(4);
                string Name = IniHelper.ReadConfig("ROIConfirmThatANewInvoiceRevisionIsCreatedTest", "Name");
                string Date = IniHelper.ReadConfig("ROIConfirmThatANewInvoiceRevisionIsCreatedTest", "Date");
                string CVC = IniHelper.ReadConfig("ROIConfirmThatANewInvoiceRevisionIsCreatedTest", "CVC");
                string CardNumber = IniHelper.ReadConfig("ROIConfirmThatANewInvoiceRevisionIsCreatedTest", "CardNumber");
                string BillingAddress = IniHelper.ReadConfig("ROIConfirmThatANewInvoiceRevisionIsCreatedTest", "BillingAddress");
                string BillingCity = IniHelper.ReadConfig("ROIConfirmThatANewInvoiceRevisionIsCreatedTest", "BillingCity");
                string BillingState = IniHelper.ReadConfig("ROIConfirmThatANewInvoiceRevisionIsCreatedTest", "BillingState");
                string BillingZipCode = IniHelper.ReadConfig("ROIConfirmThatANewInvoiceRevisionIsCreatedTest", "BillingZipCode");

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
                string payAmount = Driver.FindElementBy(payButton).GetAttribute("value");
                payAmount = payAmount.Split(' ')[1].ToString().Trim();
                Driver.DirectClick(payButton);
                Driver.SleepTheThread(8);
                return payAmount;

            }
            catch (Exception ex)
            {

                throw new Exception($"Failed do the add a payment Message : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }

        public string GetApprovalCodeInPaymentPage()
        {
            try
            {
               string approvalCode1 = Driver.GetText(approvalCodeVal);
               return approvalCode1;
            }
            catch (Exception ex)
            {

                throw new Exception($"Failed  to get approval code at payment page Message : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }
        public string GetApprovalCodeInCreditCardPayment()
        {
            try
            {
                string approvalCode1 = Driver.GetText(code);
                
                return approvalCode1;
            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to get approval code at credit card payment page Message : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }


        public void  ClickOnContinueButton()
        {
            try
            {
                Driver.Click(continueButton);
            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to click on continue button with details as: {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }


        public void SelectCreditCardRadioButtonAndContinue()
        {
            try
            {
                Driver.Click(creditCardRadioButton);
                Driver.Click(By.XPath("//input[@id='mrocontent_btn_Cont']"));                
                Driver.Wait(TimeSpan.FromSeconds(2));
                Driver.SwitchToDefaultContent();
                
            }
         catch (Exception ex)
            {
                throw new Exception($"Failed to select credit card radio button Message : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }
    }
}
