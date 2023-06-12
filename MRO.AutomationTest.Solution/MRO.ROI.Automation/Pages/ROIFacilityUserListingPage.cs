using AventStack.ExtentReports;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MRO.ROI.Automation.Selenium;
using MRO.ROI.Automation.Utility;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using System;
using System.IO;
using System.Reflection;
using static MRO.ROI.Automation.Utility.IniFile;

namespace MRO.ROI.Automation.Pages
{
    public class ROIFacilityUserListingPage
    {
        public RemoteWebDriver Driver;
        public ExtentTest logger;
        public TestContext Context;
        public ROIFacilityUserListingPage(RemoteWebDriver driver, ExtentTest _loger, TestContext _context)
        {
            Driver = driver;
            logger = _loger;
            Context = _context;
        }

        public const string userid = "akothuri";
        public By loginElement = By.XPath("//*[@id='mrocontent_txtLogin']");
        public By createButton = By.XPath("//input[@id='mrocontent_cmdRefresh']");
        public By usernameHyperlink = By.XPath("//*[@id='mrocontent_dgUsers']/tbody/tr[2]/td[3]/a");
        public By firstNameEle = By.Id("mrocontent_txtFirstName");
        public By lastNameEle = By.Id("mrocontent_txtLastName");
        public const string csvFileName = "ROINonAdminUsers.csv";
        public By loginIDEle = By.XPath("//input[@id ='mrocontent_txtLogin']");


        /// <summary>
        /// Go to EditUserInfo
        /// </summary>
        public void EditLoginUser()
        {
            try
            {
                string firstnameVal = IniHelper.ReadConfig("PermissionsToCreateNonAdminUsersTest", "Firstname");
                string secondnameVal = IniHelper.ReadConfig("PermissionsToCreateNonAdminUsersTest", "Lastname");
                //CSVReader csvReader = new CSVReader(Path.GetFullPath(Path.Combine(Assembly.GetExecutingAssembly().Location, "..", "TestData", csvFileName)));
                //string firstnameVal = csvReader.GetDataFromCSVFile("Firstname");
                //string secondnameVal = csvReader.GetDataFromCSVFile("Lastname");
                Driver.Click(firstNameEle);
                Driver.SendKeys(firstNameEle, firstnameVal);
                Driver.Click(lastNameEle);
                Driver.SendKeys(lastNameEle, secondnameVal);
                IWebElement createButt = Driver.FindElementBy(createButton);
                createButt.Click();
                IWebElement userNameLink = Driver.FindElement(usernameHyperlink);
                userNameLink.Click();
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed  to edit login user details with Message : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        
    }

        /// <summary>
        /// Search and Edit User
        /// </summary>
        public void SearchAndEditUser()
        {
            try
            {
                string loginID = IniHelper.ReadConfig("SearchAndEditUser", "LoginID");
                Driver.SendKeys(loginIDEle, loginID);
                IWebElement createButt = Driver.FindElementBy(createButton);
                createButt.Click();
                IWebElement userNameLink = Driver.FindElement(usernameHyperlink);
                userNameLink.Click();
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to search and edit login user details with Message : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }

        public void SearchMROARTFFacilityUser()
        {
            try
            {
                string loginID = IniHelper.ReadConfig("CanAdministerUsersPermissionChangesHasUserReportingTest", "LoginID");
                Driver.SendKeys(loginIDEle, loginID);
                Driver.Wait(TimeSpan.FromSeconds(2));
                IWebElement createButt = Driver.FindElementBy(createButton);
                createButt.Click();
                
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to search and edit login user details with Message : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }

        public void ClickOnSearchData()
        {
            try
            {
                IWebElement userNameLink = Driver.FindElement(usernameHyperlink);
                userNameLink.Click();
            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to search and edit login user details with Message : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }

        public void SearchFacilityUser(string userId)
        {
            try
            {
               
                Driver.SendKeys(loginIDEle, userId);
                Driver.Wait(TimeSpan.FromSeconds(3));
                IWebElement createButt = Driver.FindElementBy(createButton);
                createButt.Click();

            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to search and edit login user details with Message : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }

    }
}
