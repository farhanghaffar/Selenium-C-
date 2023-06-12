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
    public class ROIPayForRecordsPage
    {
        public RemoteWebDriver Driver;
        public ExtentTest logger;
        public TestContext Context;
        public ROIPayForRecordsPage(RemoteWebDriver driver, ExtentTest _loger, TestContext _context)
        {
            Driver = driver;
            logger = _loger;
            Context = _context;
        }


        public By fromDate = By.Id("mrocontent_txtDateA");
        public By toDate = By.Id("mrocontent_txtDateZ");
        public By showRequestBtn = By.Id("mrocontent_cmdShowRequests");
        public By clearAllBtn = By.Id("mrocontent_btnClearAll");
        public By payforSelectedRecordBtn = By.Id("mrocontent_cmdPayment");
        public By sortElement = By.Id("mrocontent_dgRecords_tblRecords_imgSortSwitch");
        public By requestIdChckBox = By.Id("mrocontent_dgRecords_selRequest_0");
        public By headerText = By.XPath("//td[@id='MasterHeaderText']");


        public void ClickShowRequestButton()
        {
            try
            {
                string todayDate = DateTime.Now.ToString("MM/dd/yyyy");                 
                Driver.ClearText(fromDate);
                Driver.SendKeys(fromDate, todayDate);               
                Driver.ClearText(toDate);
                Driver.SendKeys(toDate, todayDate);
                Driver.Click(showRequestBtn);
            }
            catch (Exception ex)
            {

                throw new Exception($"Failed  to click on show request button with details as Message : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
           
        }


        public void PayForSelectedRecord(string requestid)
        {
            try
            {

                string requestID = requestid.Trim();              
                Driver.Click(clearAllBtn);               
                string req = Driver.FindElementBy(requestIdChckBox).GetAttribute("value");                
                Driver.FindElementBy(By.XPath($"(//input[@value='{requestID}'])")).Click();
                Driver.Click(payforSelectedRecordBtn);

            }
            catch (Exception ex)
            {

                throw new Exception($"Failed  to click on pay for selected record with details as Message : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }

        }

        public void VerifyRequestAndBalanceDue(string requestid)
        {
            try
            {
                string requestID = requestid.Trim();
                Driver.FindElementBy(By.XPath($"//td[contains(text(),'{requestID}')]"));
                Driver.FindElementBy(By.XPath($"//td[contains(text(),'{requestID}')]//following-sibling::td/span"));
            }
            catch (Exception ex)
            {

                throw new Exception($"Failed  to verify the request: {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }

        public string VerifyHeader()
        {
            try
            {
                string header = Driver.GetText(headerText);
                return header;
            }
            catch (Exception ex)
            {

                throw new Exception($"Failed  to return header text with details as: {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }

        public void ClickOnPayForSelectedRecord()
        {
            try
            {

                Driver.Click(payforSelectedRecordBtn);
            }
            catch (Exception ex)
            {

                throw new Exception($"Failed  to click pay for records button  with details as: {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }

        public void ClickRangeShowRequestButton()
        {
            try
            {
                string todayDate = DateTime.Now.ToString("05/25/2019");
                string endDate = DateTime.Now.ToString("02/13/2020");
                Driver.ClearText(fromDate);
                Driver.SendKeys(fromDate, todayDate);
                Driver.ClearText(toDate);
                Driver.SendKeys(toDate, endDate);
                Driver.Click(showRequestBtn);
            }
            catch (Exception ex)
            {

                throw new Exception($"Failed  to click on show request button with details as Message : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }

        }

    }
}
