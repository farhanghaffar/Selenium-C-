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
    public class ROIAdminPackingListstPage
    {
        public RemoteWebDriver Driver;
        public ExtentTest logger;
        public TestContext Context;
        public ROIAdminPackingListstPage(RemoteWebDriver driver, ExtentTest _loger, TestContext _context)
        {
            Driver = driver;
            logger = _loger;
            Context = _context;
        }
        public By headerElement = By.Id("MasterHeaderText");
        public By componentElement = By.Id("mrocontent_custComponentPrintOrder_RadGridComponentPrintOrder_ctl00_ctl02_ctl00_cbSelectAllRows");
        public By createButton = By.Id("mrocontent_btnAddPackingList");
        public By shippableIdElement = By.XPath("(//a[@title='Show details'])[2]");
        public By secondaryShipmentDropdown = By.Id("mrocontent_dgPackingList_lstDupShipmentType_1");
        public const string shipmentValue = "Electronic Shipment Failure";
        public By addButton = By.XPath("//input[@value='Add']");
        public By resendButton = By.XPath("//input[@name='mrocontent$dgPackingList$ctl03$ctl02']");
        public By returnToRequestElement = By.Id("mrocontent_btnReturnToRequest");
        public By numOfRowsForPackingList = By.XPath("//table[@id='mrocontent_tblPackingList']//tr[2]//table[@id='mrocontent_dgPackingList']/tbody/tr");
        public By shipmentDropdown = By.Id("mrocontent_dgPackingList_lstDupShipmentType_0");
        public By resendButElement = By.Name("mrocontent$dgPackingList$ctl02$ctl02");
        public By shipableValue = By.XPath("//a[@title='Show details']");
        public const string shipmentResend = "Electronic Shipment Resend";
        /// <summary>
        /// Verify Header
        /// </summary>
        public void VerifyPackageListPageHeader()
        {
            try
            {
                string headerVal = Driver.FindElementBy(headerElement).Text.ToString();
                string heading = headerVal.Split('(')[0].ToString().Trim();
                Assert.AreEqual(heading, "Packing Lists");

            }
            catch (Exception ex)
            {
                throw new Exception($"Failed  to verify heading with  detail exception as Message : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }

        }
        public void CreatePackingList()
        {
            try
            {
                Driver.Click(componentElement);
                Driver.Click(createButton);

            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to create package list with details as : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }

        public bool VerifyShipmentId()
        {
            bool isDisplayed = false;
            try
            {
                int rows = Driver.FindElements(numOfRowsForPackingList).Count;
                //int n = rows.Count;
                if (rows == 3)
                {
                    IWebElement id = Driver.FindElementBy(shippableIdElement);
                    string shipmentId = Driver.GetText(shippableIdElement);
                    if (id.Displayed == true)
                    {
                        isDisplayed = true;
                        logger.Log(Status.Pass, $"Shipment Id generated for request and generated shipmentId is:{shipmentId}");
                    }
                }
                if (rows == 2)
                {
                    IWebElement id = Driver.FindElementBy(shipableValue);
                    string shipmentId = Driver.GetText(shipableValue);
                    if (id.Displayed == true)
                    {
                        isDisplayed = true;
                        logger.Log(Status.Pass, $"Shipment Id generated for request and generated shipmentId is:{shipmentId}");
                    }
                }
                //IWebElement id = Driver.FindElementBy(shippableIdElement);
                //string shipmentId = Driver.GetText(shippableIdElement);
                //if (id.Displayed == true)
                //{
                //    isDisplayed = true;
                //    logger.Log(Status.Pass, $"Shipment Id generated for request and generated shipmentId is:{shipmentId}");
                //}
                //else
                //{
                //    isDisplayed = false;
                //    logger.Log(Status.Info, "Failed to generate shipmentId");
                //}
                return isDisplayed;
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to verify shipment id with details as : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }

        }

        public bool VerifyResendButton()
        {
            try
            {
                int rows = Driver.FindElements(numOfRowsForPackingList).Count;
                //int n = rows.Count;
                bool isDisplayed = false;
                if (rows == 2)
                {
                    SelectElement oSelect = new SelectElement(Driver.FindElementBy(shipmentDropdown));
                    oSelect.SelectByText(shipmentValue);
                    Driver.Click(addButton);

                    IWebElement _resendBut = Driver.FindElementBy(resendButElement);

                    if (_resendBut.Displayed == true)
                    {
                        isDisplayed = true;
                    }
                    else
                    {
                        isDisplayed = false;
                        logger.Log(Status.Info, "Failed to verify resend button");
                    }

                }
                if (rows == 3)
                {
                    SelectElement oSelect1 = new SelectElement(Driver.FindElementBy(secondaryShipmentDropdown));
                    oSelect1.SelectByText(shipmentValue);
                    Driver.Click(addButton);
                    IWebElement resendBut = Driver.FindElementBy(resendButton);
                    if (resendBut.Displayed == true)
                    {
                        isDisplayed = true;
                    }
                    else
                    {
                        isDisplayed = false;
                        logger.Log(Status.Info, "Failed to verify resend button");
                    }
                }


                return isDisplayed;
            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to verify resend button  with details as : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }
        public void ReturnToRss()
        {
            try
            {
                Driver.Click(returnToRequestElement);
            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to  click return to request button with details as : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }

        public void AddSecondaryShipment()
        {
            try
            {
                int numofRows = Driver.FindElementsBy(By.XPath("//table[@id='mrocontent_tblPackingList']//tbody//tr[@class='TableBody']")).Count;
                int actualRows = numofRows - 1;
                string beforeXpath = "//select[@id='mrocontent_dgPackingList_lstDupShipmentType";
                string afterXpath = $"_{(actualRows)}']";
                string actualXpath = beforeXpath + afterXpath;
                IWebElement shipmentDropdown = Driver.FindElementBy(By.XPath(actualXpath));
                SelectElement oSelect = new SelectElement(shipmentDropdown);
                oSelect.SelectByText(shipmentResend);
                Driver.Click(addButton);

            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to  click return to request button with details as : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }

        public void ClickOnPdfHyperLink()
        {
            try
            {
                int numofRows = Driver.FindElementsBy(By.XPath("//table[@id='mrocontent_tblShipments']/tbody/tr")).Count;

                string beforeXpath = "//table[@id='mrocontent_tblShipments']/tbody/tr[";
                string afterXpath = $"{(numofRows)}]//td[1]/a";
                string actualXpath = beforeXpath + afterXpath;
                //IWebElement pdfHyperlink = Driver.FindElementBy(By.XPath(actualXpath));
                Driver.FindElementBy(By.XPath(beforeXpath + afterXpath)).Click();
                Driver.Wait(TimeSpan.FromSeconds(2));


            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to  click  to pdf hyperlink with details as : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }
    }
}
