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
    public class ROIAdminAddNewUserPage
    {
        public RemoteWebDriver Driver;
        public ExtentTest logger;
        public TestContext Context;
        public ROIAdminAddNewUserPage(RemoteWebDriver driver, ExtentTest _loger, TestContext _context)
        {
            Driver = driver;
            logger = _loger;
            Context = _context;
        }

        public By UserLogin = By.XPath("//input[@id='mrocontent_txtLogin']");
        public By FirstName = By.XPath("//input[@id='mrocontent_txtFirstName']");
        public By LastName = By.XPath("//input[@id='mrocontent_txtLastName']");
        public By Email = By.XPath("//input[@id='mrocontent_txtEmail']");
        public By AddUser = By.XPath("//input[@id='mrocontent_cmdUpdate']");
        public By MROEmployee = By.XPath("//input[@id='mrocontent_bMROEmployee']");
        public By UserType = By.XPath("//select[@id='mrocontent_lstROIFacilityUserType']");
        public By RemoteStatus = By.XPath("//select[@id='mrocontent_lstROIFacilityUserRemoteStatus']");
        public By UserAdded = By.XPath("//*[contains(text(),'User added')]");
        public By temporaryPwd = By.XPath("//span[@id='mrocontent_lblPassword']");
        public By createdUserId = By.XPath("//span[@id='mrocontent_lblUser']");
        public By canCreateRequestChkbox = By.XPath("//input[@id='mrocontent_bCanCreateRequest']");
        public By canAdminUsers = By.XPath("//input[@id='mrocontent_bUserAdmin']");

        public void SetUpUser()
        {
            try
            {
                IWebElement userLogin = Driver.FindElementBy(UserLogin);
                userLogin.SendKeys("Rajesh" + DateTime.Now);
                IWebElement firstName = Driver.FindElementBy(FirstName);
                firstName.SendKeys("Rajesh" + DateTime.Now);
                IWebElement lastName = Driver.FindElementBy(LastName);
                lastName.SendKeys("Cigniti" + DateTime.Now);
                IWebElement email = Driver.FindElementBy(Email);
                email.SendKeys("E006201@cigniti.com");
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to setup user with details : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }

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

        public bool SetUserTypeAsFieldStaffAndRemoteStatusBlankAndAddUser()
        {
            bool isPresent = false;
            try
            {
                var userTypeDropDown = Driver.FindElementBy(UserType);
                var selectElement = new SelectElement(userTypeDropDown);
                selectElement.SelectByText("Field Staff");
                var remoteStatusDropDown = Driver.FindElementBy(RemoteStatus);
                var selectElementRemoteStatus = new SelectElement(remoteStatusDropDown);
                selectElementRemoteStatus.SelectByText("");
                IWebElement addUser = Driver.FindElementBy(AddUser);
                addUser.Click();
                isPresent = Driver.IsAlertPresent();

            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to set user type as field staff and remote status as blank : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
            return isPresent;
        }

        public void SetUserTypeAsFieldStaffAndRemoteStatusAsFullTimeAndAddUser()
        {
            try
            {
                var userTypeDropDown = Driver.FindElementBy(UserType);
                var selectElement = new SelectElement(userTypeDropDown);
                selectElement.SelectByText("Field Staff");
                var remoteStatusDropDown = Driver.FindElementBy(RemoteStatus);
                var selectElementRemoteStatus = new SelectElement(remoteStatusDropDown);
                selectElementRemoteStatus.SelectByText("On Site Full-Time");
                IWebElement addUser = Driver.FindElementBy(AddUser);
                addUser.Click();
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to set user type as remote services and remote status as blank : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }

        public string GetFacilityUserAddedText()
        {
            string userAdded = string.Empty;
            try
            {
                userAdded = Driver.FindElementBy(UserAdded).Text;
                userAdded = userAdded.Split('!')[0];
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to get facility user addedText: {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
            return userAdded;
        }

        public string CreateNewFacilityUser()
        {
            try
            {
                Random ran = new Random();
                int val = ran.Next(10, 1000);
                Driver.SendKeys(UserLogin, "TestCigniti" + val);
                Driver.SendKeys(FirstName, "Test");
                Driver.SendKeys(LastName, "Test" + val);
                Driver.SendKeys(Email, "test@cigniti.com");

                if (Driver.FindElementBy(canAdminUsers).Selected == false)
                {
                    Driver.Click(canAdminUsers);
                }

                if (Driver.FindElementBy(canCreateRequestChkbox).Selected == false)
                {
                    Driver.Click(canCreateRequestChkbox);
                }

                var userTypeDropDown = Driver.FindElementBy(UserType);
                var selectElement = new SelectElement(userTypeDropDown);
                selectElement.SelectByText("Remote Services");
                var remoteStatusDropDown = Driver.FindElementBy(RemoteStatus);
                var selectElementRemoteStatus = new SelectElement(remoteStatusDropDown);
                selectElementRemoteStatus.SelectByText("On Site Full-Time");

                Driver.Click(AddUser);
                Driver.Wait(TimeSpan.FromSeconds(2));
                string userId = Driver.GetText(createdUserId);
                return userId;
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to setup user with details : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }

        public string GetTemporaryPassword()
        {
            try
            {
                string pwd = Driver.GetText(temporaryPwd);
                return pwd;
            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to return password with details : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }

        public bool IsUserTypeDropdownIsShowing(){
            return Driver.isElementDisplayed(UserType);
        }

        public bool IsRemoteStatusDropdownIsShowing()
        {
            return Driver.isElementDisplayed(RemoteStatus);
        }

    }
}
