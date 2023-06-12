using AventStack.ExtentReports;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MRO.ROI.Automation.Selenium;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.UI;
using System;

namespace MRO.ROI.Automation.Pages
{
    public class ROIRequestActionsPage
    {
        public RemoteWebDriver Driver;
        public ExtentTest logger;
        public TestContext Context;
        public ROIRequestActionsPage(RemoteWebDriver driver, ExtentTest _loger, TestContext _context)
        {
            Driver = driver;
            logger = _loger;
            Context = _context;
        }

        public By boeActionDrp = By.XPath("//select[@id='mrocontent_ddlBOEActions']");
        public By notesTxtBox = By.XPath("//textarea[@id='mrocontent_txtNotes']");
        public By addActionBtn = By.XPath("//input[@id='mrocontent_cmdAddAction']");
        public By facilityActionDrp = By.XPath("//select[@id='mrocontent_ddlFacilityActions']");
        public By backToRequest = By.XPath("//input[@id='mrocontent_cmdRequest']");
        public By mroactionTypeDrp = By.XPath("//select[@id='mrocontent_ddlMROActions']");

        public bool VerifyBOEActionsDropdown()
        {
            try
            {
                string boeDrp = "//select[@id='mrocontent_ddlBOEActions']";
                WebElementHelper helper = new WebElementHelper(Driver, logger, Context);
                bool isBoeExists = helper.IsElementPresent(boeDrp);
                return isBoeExists;
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to verify boe actions dropdown with message as : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }

       
        public void AddBOEActionWithTypeAndNotes(string type,string notes)
        {
            try
            {
                Driver.SendKeys(boeActionDrp, type);
                Driver.SendKeys(notesTxtBox, notes);
                Driver.Click(addActionBtn);


            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to add boe actions  with message as : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }


        public void AddFacilityActionWithTypeAndNotes(string type, string notes)
        {
            try
            {
                Driver.SendKeys(facilityActionDrp, type);
                Driver.SendKeys(notesTxtBox, notes);
                Driver.Click(addActionBtn);


            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to add facility actions  with message as : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }


        public void ClickBackToRequest()
        {
            try
            {
                Driver.Click(backToRequest);

            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to click back to request with message as : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }


        public string VerifyNotes()
        {
            try
            {
                string rowXpath = "//table[@id='mrocontent_dgActions']//tr[";
                string colXpath = "]//td[5]";               
                int rowCount = Driver.FindElements(By.XPath("//table[@id='mrocontent_dgActions']//tr")).Count;
                string actualXpath = rowXpath + rowCount + colXpath;                
                string info = Driver.GetText(By.XPath(actualXpath));
                Driver.ScrollToElement(By.XPath(actualXpath));
                return info;
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to verify notes with details as {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }


        public void AddMROActionWithType(string type)
        {
            try
            {
                Driver.SendKeys(mroactionTypeDrp, type);
                Driver.Click(addActionBtn);

            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to add MRO action  with message as : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }
    }
}
