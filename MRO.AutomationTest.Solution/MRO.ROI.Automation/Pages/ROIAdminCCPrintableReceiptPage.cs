using AventStack.ExtentReports;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MRO.ROI.Automation.Selenium;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;

namespace MRO.ROI.Automation.Pages
{
    public class ROIAdminCCPrintableReceiptPage
    {
        public RemoteWebDriver Driver;
        public ExtentTest logger;
        public TestContext Context;
        public ROIAdminCCPrintableReceiptPage(RemoteWebDriver driver, ExtentTest _loger, TestContext _context)
        {
            Driver = driver;
            logger = _loger;
            Context = _context;
        }

        public const string ccPrintableReceiptWindowName = "CCPrintableReceipt";
        By requestId = By.XPath("//span[@id='ccReceipt_lblReqRequestID']");
        By approvalCode = By.XPath("//span[@id='ccReceipt_lblApprovalCode']");
        By transactionalStatus = By.XPath("//span[@id='ccReceipt_lblTransStatus']");
        By transactionDateAndTime = By.XPath("//span[@id='ccReceipt_lblTransDateTime']");
        By order = By.XPath("//span[@id='ccReceipt_lblOrderNumber']");
        By chargeAmount = By.XPath("//span[@id='ccReceipt_lblChargeAmount']");
        By cardInformation = By.XPath("//span[@id='ccReceipt_lblCCNumber']");
        By cardHolderName = By.XPath("//span[@id='ccReceipt_lblCCHolderName']");


        public void SwitchToCCPrintableReceiptWindow()
        {
            try
            {
                Driver.SwitchToWindow(ccPrintableReceiptWindowName);
            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to switch to CCPrintableReceiptWindow Message : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }

        public string ReturnRequestId()
        {
            try
            {
                return Driver.GetText(requestId);
            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to return requestId Message : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }

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

        public void LogTransactionalData()
        {
            
            try
            {
                logger.Log(Status.Info,$"Transaction request ID: {Driver.GetText(requestId)}");
                logger.Log(Status.Info, $"Transaction approval code: {Driver.GetText(approvalCode)}");
                logger.Log(Status.Info, $"Transactional status: {Driver.GetText(transactionalStatus)}");
                logger.Log(Status.Info, $"Transaction date and time: {Driver.GetText(transactionDateAndTime)}");
                logger.Log(Status.Info, $"Transaction order {Driver.GetText(order)}");
                logger.Log(Status.Info, $"Transaction charge amount: {Driver.GetText(chargeAmount)}");
                logger.Log(Status.Info, $"Transaction card information: {Driver.GetText(cardInformation)}");
                logger.Log(Status.Info, $"Transaction card holder name: {Driver.GetText(cardHolderName)}");
            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to log transactional data Message : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }

        public void SwitchToApprovedWindowAndCloseReceiptWindow()
        {
            try
            {
                var windowHandles = Driver.WindowHandles;
                Driver.CloseTheWindow(windowHandles[0]);
                Driver.SwitchTo().Window(windowHandles[0]);

            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to switch to wndiow  {Environment.NewLine} Excepion:{ex}");
            }

        }


        public void CloseCCPrintableReceiptWindow()
        {
            try
            {
                SwitchToApprovedWindowAndCloseReceiptWindow();
                
            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to close cc printable recepit window Message : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }

    }
}
