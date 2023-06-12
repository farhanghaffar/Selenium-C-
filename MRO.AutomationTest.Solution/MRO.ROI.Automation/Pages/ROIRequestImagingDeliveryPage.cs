using AventStack.ExtentReports;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MRO.ROI.Automation.Selenium;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.UI;
using System;

namespace MRO.ROI.Automation.Pages
{
    public class ROIRequestImagingDeliveryPage
    {
        public RemoteWebDriver Driver;
        public ExtentTest logger;
        public TestContext Context;
        public ROIRequestImagingDeliveryPage(RemoteWebDriver driver, ExtentTest _loger, TestContext _context)
        {
            Driver = driver;
            logger = _loger;
            Context = _context;
        }

        public By addnewRecordIcon = By.XPath("//input[@title='Add new record']");
        public By mediaTypeDrp = By.XPath("//input[@class='rcbInput radPreventDecorate']");
        public By cdType = By.XPath("//li[contains(text(),'CD')]");
        public By itemsTxtBox = By.XPath("//input[@id='mrocontent_RadGridMedia_ctl00_ctl03_ctl01_txtBxUnit']");
        public By costTxtBox = By.XPath("//input[@id='mrocontent_RadGridMedia_ctl00_ctl03_ctl01_txtBxCostPerUnit']");
        public By insertIcon = By.XPath("//input[@title='Insert']");
        public By editIcon = By.XPath("//input[@alt='Edit']");
        public By requestImagingDeliveryBtn = By.XPath("//input[@id='mrocontent_cmdOk']");

        public void AddNewRecord()
        {
            try
            {
                Driver.Click(addnewRecordIcon);
                Driver.SendKeys(mediaTypeDrp,"CD");
                Driver.SendKeys(itemsTxtBox, "1");
                Driver.SendKeys(costTxtBox, "10");
                Driver.Wait(TimeSpan.FromSeconds(2));
                Driver.Click(insertIcon);
                Driver.Wait(TimeSpan.FromSeconds(6));
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to add new record button with message as : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }

        public bool VerifyRecord()
        {
            try
            {
                bool isRecordPresent = false;
                WebElementHelper helper = new WebElementHelper(Driver, logger, Context);
                string editIcon = "//input[@alt='Edit']";
                bool isDisplayed = helper.IsElementPresent(editIcon);

                if(isDisplayed==true)
                {
                    isRecordPresent = true;
                }
                return isRecordPresent;

            }
            catch (Exception)
            {

                throw;
            }
        }
        public void ClickOnRequestImagingDelivery()
        {
            try
            {
                Driver.Click(requestImagingDeliveryBtn);


            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to click request imaging delivery button with message as : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }

    }
}
