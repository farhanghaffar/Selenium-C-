using AventStack.ExtentReports;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MRO.ROI.Automation.Selenium;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.UI;
using System;
using static MRO.ROI.Automation.Utility.IniFile;

namespace MRO.ROI.Automation.Pages
{
    public class ROIAddMROToMROActionItemPage
    {
        public RemoteWebDriver Driver;
        public ExtentTest logger;
        public TestContext Context;
        public ROIAddMROToMROActionItemPage(RemoteWebDriver driver, ExtentTest _loger, TestContext _context)
        {
            Driver = driver;
            logger = _loger;
            Context = _context;
        }

        public By actionNameTxt = By.XPath("//input[@id='mrocontent_txtActionName']");
        public By facilityDrp = By.XPath("//select[@id='mrocontent_lstFacility']");
        public By addBtn = By.XPath("//input[@id='mrocontent_cmdUpdate']");
        public By headerText = By.XPath("//td[@id='MasterHeaderText']");
        public By returnToListBtn = By.XPath("//input[@id='mrocontent_cmdCancel']");
        public By actionElementsCount = By.XPath(" //tr[starts-with(@id,'mrocontent_dgReport_ct')]//td[2]");

        public void CreateNewAction(string action, string facility)
        {
            try
            {

                Driver.SendKeys(actionNameTxt, action);
                Driver.SendKeys(facilityDrp, facility);
                Driver.Click(addBtn);
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to create action with message as : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }



        public string VerifyActionMessage()
        {
            try
            {
                string headerVal = Driver.GetText(headerText);
                return headerVal;
            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to verify action  with message : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }

        public void ClickOnReturnToList()
        {
            try
            {
                Driver.Click(returnToListBtn);

            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to click on return to list with message : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }

        public bool VerifyCreatedAction(string text)
        {
            bool isDisplayed = false;
            try
            {
                var actionElements = Driver.FindElementsBy(actionElementsCount);
                foreach (var actionElement in actionElements)
                {

                    if (actionElement.Text.Equals(text))
                    {
                        isDisplayed = true;
                        break;
                    }

                }

            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to verify action with message : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
            return isDisplayed;
        }

    }
}
