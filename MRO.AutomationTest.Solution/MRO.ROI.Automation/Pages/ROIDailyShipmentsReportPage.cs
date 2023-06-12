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
    public class ROIDailyShipmentsReportPage
    {
        public RemoteWebDriver Driver;
        public ExtentTest logger;
        public TestContext Context;
        public ROIDailyShipmentsReportPage(RemoteWebDriver driver, ExtentTest _loger, TestContext _context)
        {
            Driver = driver;
            logger = _loger;
            Context = _context;
        }

        public By shippedFromDate = By.XPath("//input[@id='mrocontent_txtDateA']");
        public By shippedToDate = By.XPath("//input[@id='mrocontent_txtDateZ']");
        public By requestStatusDrp = By.XPath("//select[@id='mrocontent_lstRequestStatus']");
        public By excelIcon = By.XPath("//input[@id='mrocontent_btnGenerateExcelFile']");
        public By closedState = By.XPath("//option[contains(text(),'Closed')]");
        public By createReportBtn = By.XPath("//input[@id='mrocontent_cmdCreate']");
        
       
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
                string closed = "//option[contains(text(),'Closed')]";
                WebElementHelper helper = new WebElementHelper(Driver, logger, Context);
                bool isClosedDisplayed = helper.IsElementPresent(closed);

                string delivered = "//option[contains(text(),'Delivered')]";
                bool isDeliveredDisplayed = helper.IsElementPresent(delivered);

                string notReleased = "//option[contains(text(),'Not Released')]";
                bool isNotReleasedDisplayed = helper.IsElementPresent(notReleased);

                string processing = "//option[contains(text(),'Processing')]";
                bool isProcessingDisplayed = helper.IsElementPresent(processing);

                string cancelled = "//option[contains(text(),'Cancelled')]";
                bool isCancelledDisplayed = helper.IsElementPresent(cancelled);

                if ((isClosedDisplayed == true) && (isDeliveredDisplayed == true) && (isNotReleasedDisplayed == true) && (isProcessingDisplayed == true) && (isCancelledDisplayed == true))
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

        public void CreateReportForClosedStatus(string status)
        {
            try
            {
                string todayDate = String.Format("{0:M/dd/yyyy}", DateTime.Now).Replace("-", "/");
                Driver.ClearText(shippedFromDate);
                Driver.SendKeys(shippedFromDate, "06/11/2021");
                Driver.ClearText(shippedToDate);
                Driver.SendKeys(shippedToDate, todayDate);
                Driver.SendKeys(requestStatusDrp, status);
                Driver.Click(createReportBtn);
            }
            catch (Exception ex)
            {

                throw new Exception($"Failed  to create  report  with details as: {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }

       

    }
}