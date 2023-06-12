using AventStack.ExtentReports;
using MRO.ROI.Automation.Selenium;
using System;
using OpenQA.Selenium;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.UI;
using System.Collections.Generic;
using OpenQA.Selenium.Interactions;

namespace MRO.ROI.Automation.Pages
{
    public class ROIAdminDeferredRevenueRolloverReportPage
    {
        public RemoteWebDriver Driver;
        public ExtentTest logger;
        public TestContext Context;
        public ROIAdminDeferredRevenueRolloverReportPage(RemoteWebDriver driver, ExtentTest _loger, TestContext _context)
        {
            Driver = driver;
            logger = _loger;
            Context = _context;
        }

        public By startDate = By.Id("mrocontent_txtDateA");
        public By endDate = By.Id("mrocontent_txtDateZ");
        public By facilityDropdown = By.Id("mrocontent_lstFacilities");
        public By includeTestChkBox = By.Id("mrocontent_cbTest");
        public By createButton = By.Id("mrocontent_cmdSummary");
        public static string facilitySelection = "[All]";
        public By paymentCreditEle = By.XPath("(//td[contains(text(),'Payment Credit Card')])[1]/../td[2]/a");
        public By totalElement = By.XPath("//td[contains(text(),'Total')]");
        public By numberOfTableRows = By.XPath("//table[@id='mrocontent_tblSummary']/tbody/tr");
        public By totalFourthElement = By.XPath("//table[@id='mrocontent_tblSummary']/tbody/tr[21]/td[4]");
        public By totalThirdElement = By.XPath("//table[@id='mrocontent_tblSummary']/tbody/tr[21]/td[3]");
        public By totalSecondElement = By.XPath("//table[@id='mrocontent_tblSummary']/tbody/tr[21]/td[2]");
        public By excelIconForHyperlink = By.XPath("//input[@id='mrocontent_lnkExport']");
        public By paymentCreditNonARElementlink = By.XPath("(//td[contains(text(),'Payment Credit Card')])[1]/../td[4]/a");
        public By paymentMRONonARElementHyperLink = By.XPath("(//td[contains(text(),'Payment MRO Check Logged')])[1]/../td[4]/a");
        public By payementTotalHyperLink = By.XPath("(//table[@id='mrocontent_tblSummary']//tr//td[contains(text(),'Total')])[3]/../td[6]/a");
        public By paymentCreditTotalHyperlink = By.XPath("(//td[contains(text(),'Payment Credit Card')])[1]/../td[6]/a");
        public By idHyperlink = By.XPath("//table[@id='mrocontent_tblReport']/tbody/tr[1]/td[3]/a[1]");
        public By excelIconForHyperlinks = By.Id("mrocontent_lnkExportSummary");
        public By tableTotalColCount = By.XPath("//table[@id='mrocontent_tblSummary']//tr[1]//td");
        public By tableTotalRowCount = By.XPath("//table[@id='mrocontent_dgReport']/tbody/tr");



        /// <summary>
        /// Create Report
        /// </summary>
        public bool CreateReport()
        {
            try
            {
                string date = (DateTime.Now.AddYears(-1).ToString("MM/dd/yyy"));
                string previousDate = String.Format("{0:MM/dd/yyyy}", DateTime.Now.AddYears(-1)).Replace("-", "/");
                string todayDate = String.Format("{0:MM/dd/yyyy}", DateTime.Now).Replace("-", "/");
                logger.Log(Status.Info, "Enter start date : " + previousDate);
                Driver.ClearText(startDate);
                //Driver.FindElement(startDate).Clear();
                Driver.Click(startDate);
                Driver.SendKeys(startDate, previousDate);
                logger.Log(Status.Info, "Enter end date : " + todayDate);
                Driver.ClearText(endDate);
                Driver.Click(endDate);
                Driver.SendKeys(endDate, todayDate);
                logger.Log(Status.Info, "Select facility : " + facilitySelection);
                Driver.Click(facilityDropdown);
                Driver.SelectValueFromDD(facilityDropdown, facilitySelection);
                logger.Log(Status.Info, "Checked 'Include Test Facilities' checkbox");
                if (Driver.FindElementBy(includeTestChkBox).Selected == false)
                {
                    Driver.Click(includeTestChkBox);
                }
                logger.Log(Status.Info, "Click Create Report button");
                Driver.Click(createButton);
                bool isDisplayed = Driver.isElementDisplayed(totalElement);
                return isDisplayed;
            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to create report with details as Message : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }

        public Double VerifySecondColSum()
        {
            try
            {
                Double secondColSum = 0;
                int numofRows = Driver.FindElements(numberOfTableRows).Count;
                string beforeXpath = "//table[@id='mrocontent_tblSummary']/tbody/tr[";
                string afterXpath = "]/td[2]";
                for (int i = 8; i <= numofRows - 1; i++)
                {

                    if (i == 15)
                    {
                        continue;
                        i++;
                    }
                    string actualXpath = beforeXpath + i + afterXpath;
                    bool isPresent = Driver.isElementDisplayed(By.XPath(actualXpath));
                    if (isPresent)
                    {
                        IWebElement elementValue = Driver.FindElementBy(By.XPath(actualXpath));
                        string amount = elementValue.Text;
                        amount = amount.Replace('(', '-').Replace(')', ' ');
                        Double converVal = Convert.ToDouble(amount);
                        secondColSum = secondColSum + converVal;

                    }
                }
                secondColSum = Math.Round(secondColSum, 2);
                return secondColSum;
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to verify second column sum with  Message : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }
        public Double VerifyThirdColSum()
        {
            try
            {
                Double thirdColSum = 0.00;
                int numofRows = Driver.FindElements(numberOfTableRows).Count;
                string beforeXpath = "//table[@id='mrocontent_tblSummary']/tbody/tr[";
                string afterXpath = "]/td[3]";
                for (int i = 8; i <= numofRows - 1; i++)
                {

                    if (i == 15)
                    {
                        continue;
                        i++;
                    }
                    string actualXpath = beforeXpath + i + afterXpath;
                    bool isPresent = Driver.isElementDisplayed(By.XPath(actualXpath));
                    if (isPresent)
                    {
                        IWebElement elementValue = Driver.FindElementBy(By.XPath(actualXpath));
                        string amount = elementValue.Text;
                        amount = amount.Replace('(', '-').Replace(')', ' ');
                        Double converVal = Convert.ToDouble(amount);
                        thirdColSum = thirdColSum + converVal;
                    }
                }
                thirdColSum = Math.Round(thirdColSum, 2);
                return thirdColSum;
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to verify third column sum with  Message : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }
        public Double VerifyFourthColSum()
        {
            try
            {
                Double fourthColSum = 0.00;
                int numofRows = Driver.FindElements(numberOfTableRows).Count;
                string beforeXpath = "//table[@id='mrocontent_tblSummary']/tbody/tr[";
                string afterXpath = "]/td[4]";
                for (int i = 8; i <= numofRows - 1; i++)
                {

                    if (i == 15)
                    {
                        continue;
                        i++;
                    }
                    string actualXpath = beforeXpath + i + afterXpath;
                    bool isPresent = Driver.isElementDisplayed(By.XPath(actualXpath));
                    if (isPresent)
                    {
                        IWebElement elementValue = Driver.FindElementBy(By.XPath(actualXpath));
                        string amount = elementValue.Text;
                        amount = amount.Replace('(', '-').Replace(')', ' ');
                        Double converVal = Convert.ToDouble(amount);
                        fourthColSum = fourthColSum + converVal;
                    }

                }
                fourthColSum = Math.Round(fourthColSum, 2);
                return fourthColSum;
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to verify fourth column sum with  Message : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }
        public void ClickOnExportIcon()
        {
            try
            {
                Driver.Click(excelIconForHyperlinks);
            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to click excel icon with details as : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }
        public void ClickOnExcelIcon()
        {
            try
            {
                if (Driver.FindElementBy(paymentCreditEle).Displayed == true)
                {
                    Driver.Click(paymentCreditEle);

                }
                else if (Driver.FindElementBy(paymentCreditNonARElementlink).Displayed)
                {
                    Driver.Click(paymentCreditNonARElementlink);
                }
                Driver.ScrollIntoViewAndClick(excelIconForHyperlink);
                Driver.SleepTheThread(5);
            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to click payments credit hyperlink: {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }
        public string TotalAmountValueFromTable(int colCount)
        {
            try
            {

                IWebElement totalVal = Driver.FindElementBy(By.XPath($"//td[contains(text(), 'Total')]//following-sibling::td[{colCount}]"));
                return totalVal.Text.Replace('(', '-').Replace(')', ' ').Trim().Replace(".00", " ").Trim();
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to return shipment id from table : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }
        public string ReturnColNameFromTable(int rowCount)
        {
            try
            {

                IWebElement ColumnNames = Driver.FindElementBy(By.XPath($"//table[@id='mrocontent_tblSummary']//tr[{rowCount}]//td[1]"));
                return ColumnNames.Text;
            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to get column count from table : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }

        public string PaymentAmountFromTable(int rowCount)
        {
            try
            {
                IWebElement amount = Driver.FindElementBy(By.XPath($"//table[@id='mrocontent_dgReport']//tr[{rowCount}]//td[8]"));
                return amount.Text.Replace(".00", " ").Trim();
            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to return payments amounts from table : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }

        public int GetTotalColumnCountFromCreateReportTable()
        {
            try
            {
                int totalColCount = Driver.FindElements(tableTotalColCount).Count;
                return totalColCount;
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to return col count from table : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }


        public int GetTotalColumnCountFromPaymentsCreditElementHyperlink()
        {
            try
            {
                int totalColCount = Driver.FindElements(By.XPath("//table[@id='mrocontent_dgReport']//tr[1]//td")).Count;
                return totalColCount;
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to return payments credit col count from table : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }
        public int GetTotalRowCountFromPaymentsTotaltHyperlink()
        {
            try
            {
                int totalRowCount = Driver.FindElements(tableTotalRowCount).Count;
                return totalRowCount;
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to return payments credit row count from table : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }


        public bool VerifyPaymentTotalHyperLink()
        {
            try
            {
                bool isPresent = Driver.FindElementBy(paymentMRONonARElementHyperLink).Displayed;
                return isPresent;
            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to verify payment total hyperlinkwith details as: {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }

        }
        public void ClickOnPaymentTotalHyperLink()
        {
            try
            {
                Driver.ClickAndCheckNextElement(payementTotalHyperLink, excelIconForHyperlink);
                Driver.FindElementBy(excelIconForHyperlink, 60).Click();
                Driver.SleepTheThread(10);

            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to click on payment total hyperlink with details as: {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }
        public void lastPageClick()
        {
            try
            {

                Driver.DirectClick(By.XPath("//table[@id='mrocontent_tblReport']/tbody/tr[1]/td[3]/a"));
                Driver.SleepTheThread(10);

            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to click on last page with details as: {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }

        public string GetPaymentTotal()
        {
            return Driver.GetText(payementTotalHyperLink).Trim();
        }
    }
}
