using AventStack.ExtentReports;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MRO.ROI.Automation.Selenium;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.UI;
using System;

namespace MRO.ROI.Automation.Pages
{
    public class ROIAdminActionListPage
    {
        public RemoteWebDriver Driver;
        public ExtentTest logger;
        public TestContext Context;
        public ROIAdminActionListPage(RemoteWebDriver driver, ExtentTest _loger, TestContext _context)
        {
            Driver = driver;
            logger = _loger;
            Context = _context;
        }

        public By btnShowList = By.XPath("//input[@id ='mrocontent_cmdShow']");
        public By chkQCFailedActionsOnly = By.XPath("//input[@id ='mrocontent_cbQCFailedActionsOnly']");
        public By actionTypeDropdown = By.XPath("//select[@id ='mrocontent_lstCanSee']");
        public By requesterTypeDropdown = By.XPath("//select[@id='mrocontent_lstRequesterType']");//
        public By requestTypeDropdown = By.XPath("//select[@id='mrocontent_lstType']");
        public By resultantMessage = By.XPath("//table[@id='mrocontent_tblRequests']//tr//td");
        public By btnExportToExcel = By.XPath("//img[@alt='Export to Excel']");
        public By lnkToday = By.XPath("//div[@class='ranges']/ul/li[1]");
        public By actionDropdownSelection = By.XPath("//select[@id='mrocontent_lstType']");
        public By addActionBtn = By.XPath("//input[@id='mrocontent_cmdAdd']");
        public By includeDropdown = By.XPath("//select[@id='mrocontent_lstActionsToInclude']");
        public void ClickOnShowList()
        {
            try
            {
                Driver.Click(btnShowList);
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to click show list button with message as : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }

        public void CheckQcFailedActionsOnly()
        {
            try
            {
                bool isCheck = Driver.FindElementBy(chkQCFailedActionsOnly).Selected;
                if (isCheck == false)
                {
                    Driver.Click(chkQCFailedActionsOnly);
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to check QC Failed Actions only with message : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }

        public void SelectActionType(string actionType)
        {
            try
            {
                Driver.MoveToElement(actionTypeDropdown);
                SelectElement oSelect = new SelectElement(Driver.FindElementBy(actionTypeDropdown));
                oSelect.SelectByText(actionType);
            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to select action type with message : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }

        public string SelectOtherFiltersAndCreateReport()
        {
            SelectRequesterType("Patient");
            SelectRequestType("Log-Only");
            ClickOnShowList();
            string sMessage = Driver.GetText(resultantMessage);
            return sMessage;
        }


        public void SelectRequesterType(string requesterType)
        {
            try
            {
                Driver.MoveToElement(requesterTypeDropdown);
                SelectElement oSelect = new SelectElement(Driver.FindElementBy(requesterTypeDropdown));
                oSelect.SelectByText(requesterType);
            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to select requester type with message : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }
        //
        public void SelectRequestType(string requestType)
        {
            try
            {
                Driver.MoveToElement(requestTypeDropdown);
                SelectElement oSelect = new SelectElement(Driver.FindElementBy(requestTypeDropdown));
                oSelect.SelectByText(requestType);
            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to select requester type with message : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }

        public void ClickOnExportToExcel()
        {
            try
            {
                Driver.Click(btnExportToExcel);
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to click on Export to Excel with message as : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }

        public void ClickOnAddAction(string actionType)
        {
            try
            {

                SelectElement oSelect = new SelectElement(Driver.FindElementBy(actionDropdownSelection));
                oSelect.SelectByText(actionType);
                Driver.Click(addActionBtn);
            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to select action type with message : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }

        public void CreateReport()
        {
            try
            {
                SelectRequesterType("Legal");
                SelectActionType("MRO/Facility");
                SelectValueFromIncludeDropdown("Most Recent Open Action Only");
                ClickOnShowList();
            }
            catch (StaleElementReferenceException ex1)
            {
                SelectRequesterType("Legal");
                SelectActionType("MRO/Facility");
                SelectValueFromIncludeDropdown("Most Recent Open Action Only");
                ClickOnShowList();
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to create report with message : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }

        public void SelectValueFromIncludeDropdown(string value)
        {
            try
            {
                Driver.MoveToElement(includeDropdown);
                SelectElement oSelect = new SelectElement(Driver.FindElementBy(includeDropdown));
                oSelect.SelectByText(value);
            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to select value from include dropdown with message : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }

        public void SelectAllRequesterTypeAndCreateReport()
        {
            try
            {
                SelectRequesterType("[All Types]");
                ClickOnShowList();
            }
            catch (StaleElementReferenceException ex1)
            {
                SelectRequesterType("[All Types]");
                ClickOnShowList();
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to create report with all requester types+: {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }
    }
}
