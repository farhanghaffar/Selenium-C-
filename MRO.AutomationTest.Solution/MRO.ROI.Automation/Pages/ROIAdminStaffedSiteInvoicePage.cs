using AventStack.ExtentReports;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MRO.ROI.Automation.Selenium;
using MRO.ROI.Automation.Utility;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using System;

namespace MRO.ROI.Automation.Pages
{
    public class ROIAdminStaffedSiteInvoicePage
    {
        public RemoteWebDriver Driver;
        public ExtentTest logger;
        public TestContext Context;
        public ROIAdminStaffedSiteInvoicePage(RemoteWebDriver driver, ExtentTest _loger, TestContext _context)
        {
            Driver = driver;
            logger = _loger;
            Context = _context;
        }

        public static string facilityDDValue = "Curahealth";
        public By ROIInvoicePageHeader = By.XPath("//td[@id='MasterHeaderText']");
        public By Month = By.XPath("//table[@id='mrocontent_tblOptions']//select[@id='mrocontent_ddlMonth']");
        public By MonthOptions = By.XPath("//table[@id='mrocontent_tblOptions']//select[@id='mrocontent_ddlMonth']//option");
        public By Year = By.XPath("//input[@id='mrocontent_txtBxYear']");
        public By Facility = By.XPath("//select[@id='mrocontent_ddlFacility']");
        public By ShowInvoice = By.XPath("//input[@value='Show Invoice']");
        public By OrderStatements = By.XPath("//input[@id='mrocontent_btnDeferredStatement']");
        public By ReturnToROIInvoice = By.XPath("//a[@id='mrocontent_lnkReturn']");
        public By SavedExport = By.XPath("//a[normalize-space()='Saved Exports']");
        public By facilityDDList = By.CssSelector("select#mrocontent_ddlFacility>option");
        public By StatementDate = By.XPath("//span[@id='mrocontent_lblDate']");
        public By DeferredReports = By.XPath("//a[@id='mrocontent_lnkDeferred']");
        public By Run = By.XPath("//input[@name='mrocontent$RadGridProcess$ctl00$ctl04$ctl01']");

        /// <summary>
        /// Verifies the Header of home page
        /// </summary>
        public void VerifyHeader()
        {
            try
            {
                string headerText = Driver.FindElementBy(ROIInvoicePageHeader).Text;
                Assert.AreEqual(headerText, "ROI Staffed Site Invoice");
            }
            catch (Exception ex)
            {

                throw new Exception($"Failed with Message Unable to verify Header : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }
        /// <summary>
        /// Set value for Month, Year and Facility and clicks on show invoice Btn
        /// </summary>
        public void SetAndClickShowInvoice()
        {

            string monthAndYear = string.Empty;
            try
            {
                string month = UiHelper.ReturnMonthName();
                string headerText = Driver.FindElementBy(ROIInvoicePageHeader).Text;
                Assert.AreEqual(headerText, "ROI Staffed Site Invoice");
                Driver.SendKeys(Month, month);
                Driver.ClearText(Year);
                Driver.SendKeys(Year, DateTime.Now.Year.ToString());
                Driver.SendKeys(Facility, facilityDDValue);
                Driver.Click(ShowInvoice);
                monthAndYear = $"ROIInvoice_{DateTime.Now.Year.ToString()}{month}";
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed with Message Unable to set value for Month, Year and Facility or unable to click Show Invoice Btn : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }
        /// <summary>
        /// Clicks on order statements btn
        /// </summary>
        public void ClickOrderStatements()
        {
            try
            {
                Driver.Click(OrderStatements);
            }
            catch (Exception ex)
            {

                throw new Exception($"Failed with Message Unable to click on order statements btn : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }

        }
        /// <summary>
        /// Clicks on Return To ROI Onvoice link
        /// </summary>
        public void ClickReturnToROIInvoice()
        {
            try
            {
                Driver.Click(ReturnToROIInvoice);
            }
            catch (Exception ex)
            {

                throw new Exception($"Failed with Message Unable to clicks on Return To ROI Onvoice link : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }

        }
        /// <summary>
        /// Clicks Saved Export btn
        /// </summary>
        public void ClickSavedExportBtn()
        {
            try
            {
                IWebElement bySavedExport = Driver.FindElementBy(SavedExport);
                bySavedExport.Click();
            }
            catch (Exception ex)
            {

                throw new Exception($"Failed with Message Unable to click Saved Export btn : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }

        }

        public string GetInvoiceStatementDate()
        {
            try
            {
                string statementDate = Driver.FindElementBy(StatementDate).Text;

                if (statementDate == "January 2021")
                {
                    statementDate = statementDate.Replace("uary 20", "-").Trim();
                }

                else if (statementDate == "February 2021")
                {
                    statementDate = statementDate.Replace("ruary 20", "-").Trim();
                }

                else if (statementDate == "March 2021")
                {
                    statementDate = statementDate.Replace("ch 20", "-").Trim();
                }

                else if (statementDate == "April 2021")
                {
                    statementDate = statementDate.Replace("il 20", "-").Trim();
                }

                else if (statementDate == "May 2021")
                {
                    statementDate = statementDate.Replace(" 20", "-").Trim();
                }

                else if (statementDate == "June 2021")
                {
                    statementDate = statementDate.Replace("e 20", "-").Trim();
                }

                else if (statementDate == "July 2021")
                {
                    statementDate = statementDate.Replace("y 20", "-").Trim();
                }

                else if (statementDate == "August 2021")
                {
                    statementDate = statementDate.Replace("ust 20", "-").Trim();
                }

                else if (statementDate == "September 2021")
                {
                    statementDate = statementDate.Replace("tember 20", "-").Trim();
                }

                else if (statementDate == "October 2021")
                {
                    statementDate = statementDate.Replace("ober 20", "-").Trim();
                }

                else if (statementDate == "November 2021")
                {
                    statementDate = statementDate.Replace("ember 20", "-").Trim();
                }

                else if (statementDate == "December 2021")
                {
                    statementDate = statementDate.Replace("ember 20", "-").Trim();
                }

                return statementDate;
            }

            catch (Exception ex)
            {

                throw new Exception($"Failed with Message Unable to verify Header : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }

        }

        public void ClickDererredReportsAndRun()
        {
            try
            {
                IWebElement deferredReports = Driver.FindElementBy(DeferredReports);
                deferredReports.Click();

                IWebElement run = Driver.FindElementBy(Run);
                run.Click();
                Driver.SwitchTo().Alert().Accept();


            }
            catch (Exception ex)
            {

                throw new Exception($"Failed with Message Unable to click Saved Export btn : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }

        }

    }
}
