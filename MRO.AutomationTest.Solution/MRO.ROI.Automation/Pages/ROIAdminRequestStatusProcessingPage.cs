using AventStack.ExtentReports;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MRO.ROI.Automation.Selenium;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.UI;
using System;

namespace MRO.ROI.Automation.Pages
{
    public class ROIAdminRequestStatusProcessingPage
    {
        public RemoteWebDriver Driver;
        public ExtentTest logger;
        public TestContext Context;
        public ROIAdminRequestStatusProcessingPage(RemoteWebDriver driver, ExtentTest _loger, TestContext _context)
        {
            Driver = driver;
            logger = _loger;
            Context = _context;
        }

        public By headerElement = By.XPath("//div[@class='page-heading']");
        public By searchBtn = By.XPath("(//span[contains(text(),'Search')])[1]");
        public By roiAdminmenu = By.XPath("//a[contains(text(),'ROIAdmin')]");
        public By rssRequestStatus = By.XPath("//a[contains(text(),'RSS Request Status')]");
        public By dob = By.XPath("//label[contains(text(),'DOB')]");
        public By dobSearchBox = By.XPath("//*[@id='app']/div[1]/main/div/div/div[2]/div/div[2]/div[2]/div[1]/div/div[3]/div[2]");
        public By findRequestBtn = By.XPath("//span[contains(text(),'Find a New Request')]");
        public By logoutIcon = By.XPath("//div[@class='toolbar-icons']//a//i[@class='v-icon notranslate mdi mdi-close theme--light']");
        public By disabledPayByCC = By.XPath("//a[@class='v-btn v-btn--contained v-btn--disabled theme--light v-size--small']");
        public By disabledPayByCCToolTipTxt = By.XPath("//div[@class='v-tooltip__content']/span");
        public By DOBTableHeading = By.XPath("//span[text()='DOB']/parent::th");

        public string VerifyHeader()
        {
            try
            {
                string header = Driver.GetText(headerElement);
                return header;
            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to verify header  : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }
        public void SearchRequestBasedOnDOB(string dobval)
        {
            try
            {
                Driver.SendKeys(dob, dobval);
                Driver.Click(searchBtn);
                Driver.Wait(TimeSpan.FromSeconds(5));
            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to search request : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }

        }

        public void ClickOnRSSRequestStatus()
        {
            try
            {
                Driver.Click(roiAdminmenu);
                Driver.Click(rssRequestStatus);

            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to click Rss RequestSstatus : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }
        public string GetDOB()
        {
            try
            {
                string dob = Driver.GetText(dobSearchBox);
                Driver.ScrollToElement(findRequestBtn);
                return dob;
            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to get dob: {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }

        public void Logout()
        {
            try
            {
                Driver.Click(logoutIcon);
            }
            catch (Exception ex)
            {


                throw new Exception($"Failed to click logout icon: {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }


        public void VerifyPayByCCDisabled()
        {
            try
            {
                Driver.Wait(TimeSpan.FromSeconds(5));
                Driver.FindElement(disabledPayByCC);
                //Assert.AreEqual(Driver.FindElement(disabledPayByCCToolTipTxt).GetAttribute("innerText"), "Request has $0 or negative balance due.");
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to Pay By CC button : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }

        public void SelectFacilityList()
        {
            try
            {
                Driver.Click(By.XPath("//ul[@class='main-nav pa-0']//li/a[(text()='Facilities')]"));
                Driver.Wait(TimeSpan.FromSeconds(2));
                Driver.Click(By.XPath("//ul[@class='main-nav pa-0']//li/a[(text()='Facility List')]"));
            }

            catch (Exception ex)
            {

                throw new Exception($"Failed to select Facility List as : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }

        public bool VerifyDOBSearchReturnsData()
        {
            try
            {
                bool header = Driver.isElementDisplayed(DOBTableHeading);
                return header;
            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to verify DOB Search return results  : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }


    }
}
