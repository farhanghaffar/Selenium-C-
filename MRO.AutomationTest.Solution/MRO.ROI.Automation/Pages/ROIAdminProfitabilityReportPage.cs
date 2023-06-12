using AventStack.ExtentReports;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MRO.ROI.Automation.Selenium;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.UI;
using System;

namespace MRO.ROI.Automation.Pages
{
    public class ROIAdminProfitabilityReportPage
    {
        public RemoteWebDriver Driver;
        public ExtentTest logger;
        public TestContext Context;
        public ROIAdminProfitabilityReportPage(RemoteWebDriver driver, ExtentTest _loger, TestContext _context)
        {
            Driver = driver;
            logger = _loger;
            Context = _context;
        }
        //
        public By maxDate = By.XPath("//span[@id ='mrocontent_lblDateMax']");
        public By minDate = By.XPath("//span[@id ='mrocontent_lblDateMin']");
        public By lastUpdateDate = By.XPath("//span[@id ='mrocontent_lblLastUpdated']");
        public By fromDate = By.Id("mrocontent_txtDateA");
        public By toDate = By.Id("mrocontent_txtDateZ");
        public By facilityDropdown = By.Id("mrocontent_lstFacilities");
        public By createReportButton = By.Id("mrocontent_cmdCreate");
        public By reportDate = By.XPath("//div[@class='txtBxDates s3-']");
        public bool VerifyLoadedData()
        {
            string sMaxDate = string.Empty;
            string sLastDate = string.Empty;
            bool isDataLoaded = false;

            try
            {
                sMaxDate = Driver.GetText(maxDate);
                sLastDate = Driver.GetText(lastUpdateDate);
                if (!string.IsNullOrEmpty(sMaxDate) && !string.IsNullOrEmpty(sLastDate))
                { isDataLoaded = true; }
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to validate loaded data under profitability report page with message as : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
            return isDataLoaded;
        }


        public void ApplyFiltersAndCreateReport(string sFromDate, string sToDate, string sFacility= "Rothman Institute")
        {
            try
            {
               string todayDate = String.Format("{0:MM/dd/yyyy}", DateTime.Now).Replace("-", "/");
                Driver.ClearText(fromDate);               
                Driver.Click(fromDate);
                Driver.SendKeys(fromDate, sFromDate);
                Driver.ClearText(toDate);
                DateTime dtTo = Convert.ToDateTime(sToDate);
                string endDate = String.Format("{0:MM/dd/yyyy}", dtTo.AddMonths(1)).Replace("-", "/");
                Driver.Click(toDate);
                Driver.SendKeys(toDate, endDate);
                Driver.Click(facilityDropdown);
                Driver.SelectValueFromDD(facilityDropdown,sFacility);                
                Driver.Click(createReportButton);
                Driver.SleepTheThread(60);
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to create report with details as Message : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }

        }

        public string GetMaxDateFromLoadedData()
        {
            string sMaxDate = string.Empty;
           try
            {
                sMaxDate = Driver.GetText(maxDate);
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to get max date from loaded data under profitability report page with message as : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
            return sMaxDate;
        }


        public string GetLastUpdatedDateFromLoadedData()
        {
           string sLastDate = string.Empty;
            try
            {
               sLastDate = Driver.GetText(lastUpdateDate);
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to get last updated date from loaded data under profitability report page with message as : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
            return sLastDate;
        }

        public string GetMinDateFromLoadedData()
        {
            string sMinDate = string.Empty;
            try
            {
                sMinDate = Driver.GetText(minDate);
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to get min date from loaded data under profitability report page with message as : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
            return sMinDate;
        }

        public bool IsPopupMessageVisible()
        {
            string sMessage = string.Empty;
            bool isAlertVisible = false;
            try
            {
                // IAlert simpleAlert = Driver.SwitchTo().Alert();
                var vAlert = Driver.SwitchTo().Alert();
                sMessage = vAlert.Text;
                if(sMessage.Equals("The data for this date is not complete!"))
                {
                    isAlertVisible = true;
                    Driver.SwitchTo().Alert().Accept();
                }
               
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to get visibility status of the popup with message as : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
            return isAlertVisible;
        }

        public void ReApplyFiltersAndCreateReport(string sFromDate, string sToDate, string sFacility = "Rothman Institute")
        {
            try
            {
                string todayDate = String.Format("{0:MM/dd/yyyy}", DateTime.Now).Replace("-", "/");
                Driver.ClearText(fromDate);
                Driver.Click(fromDate);
                Driver.SendKeys(fromDate, sFromDate);
                Driver.ClearText(toDate);
                DateTime dtTo = Convert.ToDateTime(sToDate);
                string endDate = String.Format("{0:MM/dd/yyyy}", dtTo.AddMonths(-1)).Replace("-", "/");
                Driver.Click(toDate);
                Driver.SendKeys(toDate, endDate);
                Driver.Click(facilityDropdown);
                Driver.SelectValueFromDD(facilityDropdown, sFacility);
                Driver.Click(createReportButton);
                Driver.SleepTheThread(60);
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to create report with details as Message : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }

        }

        public string GetTheDatesFromReport()
        {
            string sReportDate = string.Empty;
            try
            {
                Driver.SwitchTo().DefaultContent();
                IWebElement frame = Driver.FindElementBy(By.XPath("//iframe[@id='mrocontent_ReportViewer_ProductivityReportFrame']"));
                Driver.SwitchTo().Frame(frame);
                sReportDate = Driver.GetText(reportDate);
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to date from under profitability report with message as : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
            return sReportDate;
        }
    }
}
