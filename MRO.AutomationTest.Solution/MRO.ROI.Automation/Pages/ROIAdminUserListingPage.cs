using AventStack.ExtentReports;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MRO.ROI.Automation.Selenium;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MRO.ROI.Automation.Pages
{
   public class ROIAdminUserListingPage
    {
        public RemoteWebDriver Driver;
        public ExtentTest logger;
        public TestContext Context;
        public ROIAdminUserListingPage(RemoteWebDriver driver, ExtentTest _loger, TestContext _context)
        {
            Driver = driver;
            logger = _loger;
            Context = _context;
        }

        public By userName = By.XPath("//a[contains(text(),'12322222, test')]");
        public By addUser = By.XPath("//input[@id='mrocontent_cmdAddUser']");
        public By FirstName = By.XPath("//input[@id='mrocontent_txtFirstName']");
        public By LastName = By.XPath("//input[@id='mrocontent_txtLastName']");
        public By Create = By.XPath("//input[@id='mrocontent_cmdRefresh']");
        public By User = By.XPath("//tr[@class='TableBody']//td[3]//a[text()='Bolugunda, Rajesh']");
        public By AddUser = By.XPath("//input[@id='mrocontent_cmdAddUser']");
        public By loginElement = By.Id("mrocontent_txtLogin");
        public By activateChkbox = By.XPath("//table[@id='mrocontent_tblUsers']//tr[2]//td//table//tbody//tr[2]//td[17]//input");
        public By deleteChkbox = By.XPath("//table[@id='mrocontent_tblUsers']//tr[2]//td//table//tbody//tr[2]//td[18]//input");

        public ROIAdminFacilityListPage ClickOnUser()
        {
            try
            {                
                IWebElement User = Driver.FindElementBy(userName);
                User.Click();  
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to click on user : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
            return new ROIAdminFacilityListPage(Driver, logger, Context);
        }

        public void ClickAddUser()
        {
            try
            {               
                IWebElement adduser = Driver.FindElementBy(addUser);
                adduser.Click();
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to click on add user : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
            
        }

        public void SearchUser()
        {
            try
            {
                Automation.Common.Iframe frame = new Automation.Common.Iframe(Driver, logger, Context);
                try
                {
                    Driver.SleepTheThread(10);
                    frame.SwitchToRoiFrame();

                }
                catch (Exception)
                {

                }
                IWebElement firstName = Driver.FindElementBy(FirstName);
                firstName.SendKeys("Rajesh");
                IWebElement lastName = Driver.FindElementBy(LastName);
                lastName.SendKeys("Bolugunda");
                IWebElement create = Driver.FindElementBy(Create);
                create.Click();
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to search user : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }

        public void ClickUser()
        {
            try
            {
                IWebElement user = Driver.FindElementBy(User);
                user.Click();
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to search user : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }

        public void SearchFacilityUser(string userId)
        {
            try
            {
                Driver.SendKeys(loginElement, userId);
                Driver.Click(Create);
            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to search user : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }

        public bool DeActivateFacilityUser()
        {
            try
            {
                if (Driver.FindElementBy(activateChkbox).Selected == true)
                {
                    Driver.Click(activateChkbox);
                    Driver.Wait(TimeSpan.FromSeconds(4));
                    Driver.SwitchTo().Alert().Accept();
                    Driver.Wait(TimeSpan.FromSeconds(2));
                }
                
                bool isChecked = Driver.FindElementBy(activateChkbox).Selected;
                return isChecked;


            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to de-activate user : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }

        public void DeleteFacilityUser()
        {
            try
            {

                Driver.Click(deleteChkbox);
                Driver.Wait(TimeSpan.FromSeconds(5));
                Driver.SwitchTo().Alert().Accept();
                Driver.Wait(TimeSpan.FromSeconds(2));


            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to delete user : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }

       
    }
}
