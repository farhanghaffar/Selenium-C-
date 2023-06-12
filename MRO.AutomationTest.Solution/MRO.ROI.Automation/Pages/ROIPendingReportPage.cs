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

namespace MRO.ROI.Automation.Pages
{
    public class ROIPendingReportPage
    {
        public RemoteWebDriver Driver;
        public ExtentTest logger;
        public TestContext Context;
        public ROIPendingReportPage(RemoteWebDriver driver, ExtentTest _loger, TestContext _context)
        {
            Driver = driver;
            logger = _loger;
            Context = _context;
        }

        public By createdFromDate = By.XPath("//input[@id='mrocontent_txtDateA']");
        public By CreatedToDate = By.XPath("//input[@id='mrocontent_txtDateZ']");
        public By requestStatusDrp = By.XPath("//select[@id='mrocontent_lstRequestStatus']");
        public By excelIcon = By.XPath("//input[@id='mrocontent_MROReportGridBanner_lnkExport']");
        public By closedState = By.XPath("//option[contains(text(),'Closed')]");
        public By createReportBtn = By.XPath("//input[@id='mrocontent_cmdCreate']");
        public By onholdChkbox = By.XPath("//input[@id='mrocontent_cbShowOnlyOnHoldActions']");
        public By numOfRows = By.XPath("//span[@id='mrocontent_MROReportGridBanner_lblRows']");
        public By releasedDate = By.XPath("//*[@id='mrocontent_dgReport_ctl00__0']/td[6]");

        public bool VerifyRequestStatusDropdown()
        {
            try
            {

                string requestStatus = "//select[@id='mrocontent_lstRequestStatus']";
                WebElementHelper helper = new WebElementHelper(Driver, logger, Context);
                bool isDisplayed = helper.IsElementPresent(requestStatus);
                return isDisplayed;

            }
            catch (Exception ex)
            {

                throw new Exception($"Failed  verify request status with details as Message : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
           
        }

        public bool VerifyRequestStatusValues()
        {
            bool isDisplayed = false;
            try
            {

                Driver.Click(requestStatusDrp);
                Driver.Wait(TimeSpan.FromSeconds(2));

                WebElementHelper helper = new WebElementHelper(Driver, logger, Context);
                string notReleased = "//option[contains(text(),'Not Released')]";
                bool isNotReleasedDisplayed = helper.IsElementPresent(notReleased);

                string processing = "//option[contains(text(),'Processing')]";
                bool isProcessingDisplayed = helper.IsElementPresent(processing);
                if ((isNotReleasedDisplayed == true) && (isProcessingDisplayed == true))
                {
                    isDisplayed = true;
                }
            }
            catch (Exception ex)
            {

                throw new Exception($"Failed  verify request status with details as Message : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }

            return isDisplayed;

        }

        public string GetNumberOfRowsFromUI()
        {
            try
            {
                string rows = Driver.GetText(numOfRows);
                return rows;
            }
            catch (Exception ex)
            {

                throw new Exception($"Failed  to return number of rows with details as Message : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }

        public void ClickOnExcelIcon()
        {
            try
            {
                Driver.Click(excelIcon);
            }
            catch (Exception ex)
            {

                throw new Exception($"Failed  to click excel icon with details as: {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }
        public bool VerifyOnHoldChkbox()
        {

            try
            {

                Driver.Click(requestStatusDrp);
                Driver.Wait(TimeSpan.FromSeconds(2));

                WebElementHelper helper = new WebElementHelper(Driver, logger, Context);
                string notReleased = "//input[@id='mrocontent_cbShowOnlyOnHoldActions']";
                bool isOnHoldDisplayed = helper.IsElementPresent(notReleased);
                return isOnHoldDisplayed;


            }
            catch (Exception ex)
            {

                throw new Exception($"Failed  verify request status with details as Message : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }



        }

        public void CreateReport()
        {
            try
            {
                string todayDate = String.Format("{0:M/dd/yyyy}", DateTime.Now).Replace("-", "/");
                Driver.ClearText(createdFromDate);
                Driver.SendKeys(createdFromDate, "06/11/2021");
                Driver.ClearText(CreatedToDate);
                Driver.SendKeys(CreatedToDate, todayDate);
             if ( Driver.FindElementBy(onholdChkbox).Selected==false)
                {
                    Driver.Click(onholdChkbox);
                }
                Driver.Click(createReportBtn);
            }
            catch (Exception ex)
            {

                throw new Exception($"Failed  to create  report  with details as: {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }


        public void CreateReportWithRequestStatus(string type)
        {
            try
            {
                string todayDate = String.Format("{0:M/dd/yyyy}", DateTime.Now).Replace("-", "/");
                Driver.ClearText(createdFromDate);
                Driver.SendKeys(createdFromDate, "06/11/2021");
                Driver.ClearText(CreatedToDate);
                Driver.SendKeys(CreatedToDate, todayDate);
                if (Driver.FindElementBy(onholdChkbox).Selected == true)
                {
                    Driver.Click(onholdChkbox);
                }
                Driver.SendKeys(requestStatusDrp, type);
                Driver.Click(createReportBtn);
            }
            catch (Exception ex)
            {

                throw new Exception($"Failed  to create  report  with details as: {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }

        public string VerifyReleasesColumn()
        {
            try
            {
                string relDate = Driver.GetText(releasedDate);
                return relDate;

            }
            catch (Exception ex)
            {

                throw new Exception($"Failed  to verify released column with details as: {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }
        public void CreateReportWithOutRequestStatus()
        {
            try
            {
                string todayDate = String.Format("{0:M/dd/yyyy}", DateTime.Now).Replace("-", "/");
                Driver.ClearText(createdFromDate);
                Driver.SendKeys(createdFromDate, "06/11/2021");
                Driver.ClearText(CreatedToDate);
                Driver.SendKeys(CreatedToDate, todayDate);               
                Driver.Click(createReportBtn);
            }
            catch (Exception ex)
            {

                throw new Exception($"Failed  to create  report  with details as: {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }

    }
}