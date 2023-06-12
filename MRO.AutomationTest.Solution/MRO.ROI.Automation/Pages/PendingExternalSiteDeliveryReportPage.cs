using AventStack.ExtentReports;
using MRO.ROI.Automation.Selenium;
using System;
using OpenQA.Selenium;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium.Remote;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace MRO.ROI.Automation.Pages
{
    public class PendingExternalSiteDeliveryReportPage
    {
        public RemoteWebDriver Driver;
        public ExtentTest logger;
        public TestContext Context;
        public PendingExternalSiteDeliveryReportPage(RemoteWebDriver driver, ExtentTest _loger, TestContext _context)
        {
            Driver = driver;
            logger = _loger;
            Context = _context;
        }

        public By loggedFromDate = By.Id("mrocontent_rptExtSiteDelivery_txtBxDtLoggedA");
        public By loggedToDate = By.Id("mrocontent_rptExtSiteDelivery_txtBxDtLoggedZ");
        public By searchButton = By.Id("mrocontent_rptExtSiteDelivery_btnSearch");
        public By radioMROShipment = By.Id("ExtSiteDeliveryConfirm_rbMROShipment");
        public By btnCancel = By.Id("ExtSiteDeliveryConfirm_btnCancel");
        public By btnConfirmDelivery = By.Id("ExtSiteDeliveryConfirm_btnConfirm");
        public const string shipmentFrame = "radWndDeliverShipment";
        public By requestID = By.XPath("//input[@id='mrocontent_rptExtSiteDelivery_txtBxRequestID']");
        public void SetDatesAndFlterData()
        {
            try
            {
                var todaysDate = String.Format("{0:M/dd/yyyy}", DateTime.Now).Replace("-", "/");
                var fromDate = Driver.FindElementBy(loggedFromDate);
                fromDate.Clear();
                fromDate.SendKeys(todaysDate);
                var toDate = Driver.FindElementBy(loggedToDate);
                toDate.Clear();
                toDate.SendKeys(todaysDate);
                Driver.DirectClick(searchButton);
            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to set from and to dates : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }

        public void DeliverRequestByRequestID(string requestID)
        {
            try
            {
                List<IWebElement> tableDataRows = Driver.FindElementsBy(By.XPath("//table[@id='mrocontent_rptExtSiteDelivery_RadGridExtUploads_ctl00']//tr"));

                for (int z = 0; z < tableDataRows.Count; z++)
                {
                    System.Collections.ObjectModel.ReadOnlyCollection<IWebElement> cells = tableDataRows[z].FindElements(By.TagName("td"));
                    string tdRequestID = cells[0].Text;
                    if (tdRequestID == requestID)
                    {
                        System.Collections.ObjectModel.ReadOnlyCollection<IWebElement> deliverButtons = tableDataRows[z].FindElements(By.XPath("//input[@title ='Deliver']"));
                        deliverButtons[z].Click();
                    }
                }
            }

            catch (Exception ex)
            {
                throw new Exception($"Failed to click deliver button : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }

        public void CheckMROShipmentOption()
        {
            try
            {
                Driver.SwitchTo().Frame(shipmentFrame);
                bool isCheck = Driver.FindElementBy(radioMROShipment).Selected;
                if (isCheck == false)
                {
                    Driver.Click(radioMROShipment);
                }

            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to select MRO Shipment radio button message as : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }

        public void ClickCancel()
        {
            try
            {
                Driver.DirectClick(btnCancel);

            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to click cancel button as Message : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }

        }

        public void DownloadOriginalPdfDocuments()
        {
            try
            {
                Driver.SwitchTo().Frame(shipmentFrame);
                List<IWebElement> tableDataRows = Driver.FindElementsBy(By.XPath("//table[@id='ExtSiteDeliveryConfirm_RadGridImport_ctl00']//tr"));

                for (int z = 0; z < tableDataRows.Count - 1; z++)
                {
                    System.Collections.ObjectModel.ReadOnlyCollection<IWebElement> downLoadButtons = tableDataRows[z].FindElements(By.XPath("//input[@title ='Download']"));
                    downLoadButtons[z].Click();
                    Driver.SleepTheThread(4);
                }
            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to click download button : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }

        public void ClickConfirmDelivery()
        {
            try
            {
                Driver.DirectClick(btnConfirmDelivery);
                Driver.SleepTheThread(2);
                Driver.SwitchTo().Alert().Accept();
                Driver.SleepTheThread(2);
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to click confirm delivery button as Message : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }

        }

        public void DownloadMroShipmentDocuments()
        {
            try
            {
                Driver.SwitchTo().Frame(shipmentFrame);
                List<IWebElement> tableDataRows = Driver.FindElementsBy(By.XPath("//table[@id='ExtSiteDeliveryConfirm_ROIMultiPartShipment_RadGridShipmentParts_ctl00']//tr"));

                for (int z = 0; z < tableDataRows.Count; z++)
                {
                    System.Collections.ObjectModel.ReadOnlyCollection<IWebElement> downLoadButtons = tableDataRows[z].FindElements(By.XPath("//input[@title ='Download']"));
                    downLoadButtons[z].Click();
                    Driver.SleepTheThread(4);
                }
            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to click download button : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }
        public void SetDatesAndRequestID(String reqID)
        {
            try
            {
                var reqid = Driver.FindElementBy(requestID);
                reqid.Clear();
                reqid.SendKeys(reqID);
                var todaysDate = String.Format("{0:M/dd/yyyy}", DateTime.Now).Replace("-", "/");
                var fromDate = Driver.FindElementBy(loggedFromDate);
                fromDate.Clear();
                fromDate.SendKeys(todaysDate);
                var toDate = Driver.FindElementBy(loggedToDate);
                toDate.Clear();
                toDate.SendKeys(todaysDate);
                Driver.DirectClick(searchButton);
            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to set from and to dates : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }
    }
}
