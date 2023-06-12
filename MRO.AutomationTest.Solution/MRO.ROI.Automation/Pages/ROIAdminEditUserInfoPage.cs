using AventStack.ExtentReports;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MRO.ROI.Automation.Selenium;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MRO.ROI.Automation.Pages
{
    public class ROIAdminEditUserInfoPage
    {
        public RemoteWebDriver Driver;
        public ExtentTest logger;
        public TestContext Context;
        public ROIAdminEditUserInfoPage(RemoteWebDriver driver, ExtentTest _loger, TestContext _context)
        {
            Driver = driver;
            logger = _loger;
            Context = _context;
        }

        public By MROEmployee = By.XPath("//input[@id='mrocontent_bMROEmployee']");
        public By UserType = By.XPath("//select[@id='mrocontent_lstROIFacilityUserType']");
        public By RemoteStatus = By.XPath("//select[@id='mrocontent_lstROIFacilityUserRemoteStatus']");
        public By SaveChanges = By.XPath("//input[@id='mrocontent_cmdUpdate']");
        public By UserUpdated = By.XPath("//span[@id='mrocontent_lblUserUpdated']");


        public void CheckMROEmployee()
        {
            IWebElement empCheckBox = Driver.FindElementBy(MROEmployee);

            if (empCheckBox != null)
            {
                string value = empCheckBox.GetAttribute("checked");

                if (value != "true")
                {
                    empCheckBox.Click();
                }

            }
        }

        public void ClickOnUser()
        {
            try
            {
                logger.Log(Status.Info, "Check MRO employee check box");
                IWebElement User = Driver.FindElementBy(MROEmployee);
                User.Click();

            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to check MRO employee check box : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }

        }

        public bool SetUserTypeAsRemoteServicesAndRemoteStatusEmpty()
        {
            bool isPresent = false;
            try
            {                              
                var userTypeDropDown = Driver.FindElementBy(UserType);
                var selectElement = new SelectElement(userTypeDropDown);
                selectElement.SelectByText("Remote Services");
                var remoteStatusDropDown = Driver.FindElementBy(RemoteStatus);
                var selectElementRemoteStatus = new SelectElement(remoteStatusDropDown);
                selectElementRemoteStatus.SelectByText("");               
                IWebElement saveChanges = Driver.FindElementBy(SaveChanges);
                saveChanges.Click();
                isPresent = Driver.IsAlertPresent();
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to set user type as remote services and remote status as blank : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
            return isPresent;
        }

        public void SetUserTypeAsFloaterAndRemoteStatusAsFullTime()
        {
            try
            {
                logger.Log(Status.Info, "Set user type as floater and remote status as full time");
                //csvReader = new CSVReader(Path.GetFullPath(Path.Combine(Assembly.GetExecutingAssembly().Location, "..", "TestData", csvFileName)));
                var userTypeDropDown = Driver.FindElementBy(UserType);
                var selectElement = new SelectElement(userTypeDropDown);
                selectElement.SelectByText("Floater");
                var remoteStatusDropDown = Driver.FindElementBy(RemoteStatus);
                var selectElementRemoteStatus = new SelectElement(remoteStatusDropDown);
                selectElementRemoteStatus.SelectByText("On Site Full-Time");
                logger.Log(Status.Info, "Click on save changes button");
                IWebElement saveChanges = Driver.FindElementBy(SaveChanges);
                saveChanges.Click();
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to set user type as remote services and remote status as blank : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }

        public void SetUserTypeAsImplementationAndRemoteStatusBlank()
        {
            try
            {
                logger.Log(Status.Info, "Set user type as implementation and remote status as blank");
                //csvReader = new CSVReader(Path.GetFullPath(Path.Combine(Assembly.GetExecutingAssembly().Location, "..", "TestData", csvFileName)));
                var userTypeDropDown = Driver.FindElementBy(UserType);
                var selectElement = new SelectElement(userTypeDropDown);
                selectElement.SelectByText("Implementation");
                var remoteStatusDropDown = Driver.FindElementBy(RemoteStatus);
                var selectElementRemoteStatus = new SelectElement(remoteStatusDropDown);
                selectElementRemoteStatus.SelectByText("");
                logger.Log(Status.Info, "Click on save changes button");
                IWebElement saveChanges = Driver.FindElementBy(SaveChanges);
                saveChanges.Click();
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to set user type as remote services and remote status as blank : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }

        public void UncheckMROEmployeeAndSave()
        {
            try
            {          
                IWebElement User = Driver.FindElementBy(MROEmployee);
                User.Click();
                IWebElement saveChanges = Driver.FindElementBy(SaveChanges);
                saveChanges.Click();
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to uncheck MRO employee and save changes : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }

        public void SetUserTypeAsFieldStaffAndRemoteStatusBlank()
        {
            try
            {
                logger.Log(Status.Info, "Set user type as field staff and remote status as blank");
                //csvReader = new CSVReader(Path.GetFullPath(Path.Combine(Assembly.GetExecutingAssembly().Location, "..", "TestData", csvFileName)));
                var userTypeDropDown = Driver.FindElementBy(UserType);
                var selectElement = new SelectElement(userTypeDropDown);
                selectElement.SelectByText("Field staff");
                var remoteStatusDropDown = Driver.FindElementBy(RemoteStatus);
                var selectElementRemoteStatus = new SelectElement(remoteStatusDropDown);
                selectElementRemoteStatus.SelectByText("");
                logger.Log(Status.Info, "Click on save changes button");
                IWebElement saveChanges = Driver.FindElementBy(SaveChanges);
                saveChanges.Click();
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to set user type as remote services and remote status as blank : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }

        public void SetUserTypeAsFieldStaffAndRemoteStatusAsFullTime()
        {
            try
            {
                logger.Log(Status.Info, "Set user type as field staff and remote status as blank");
                //csvReader = new CSVReader(Path.GetFullPath(Path.Combine(Assembly.GetExecutingAssembly().Location, "..", "TestData", csvFileName)));
                var userTypeDropDown = Driver.FindElementBy(UserType);
                var selectElement = new SelectElement(userTypeDropDown);
                selectElement.SelectByText("Field staff");
                var remoteStatusDropDown = Driver.FindElementBy(RemoteStatus);
                var selectElementRemoteStatus = new SelectElement(remoteStatusDropDown);
                selectElementRemoteStatus.SelectByText("On Site Full-Time");
                logger.Log(Status.Info, "Click on save changes button");
                IWebElement saveChanges = Driver.FindElementBy(SaveChanges);
                saveChanges.Click();
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to set user type as remote services and remote status as blank : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }

        public string GetFacilityUserAddedText()
        {
            string userUpdated = string.Empty;
            try
            {
                return userUpdated = Driver.FindElementBy(UserUpdated).Text;

            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to get facility user addedText: {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }
    }
}
