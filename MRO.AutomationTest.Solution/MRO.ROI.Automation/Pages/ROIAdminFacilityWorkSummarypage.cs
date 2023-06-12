using AventStack.ExtentReports;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MRO.ROI.Automation.Selenium;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using System;

namespace MRO.ROI.Automation.Pages
{
    public class ROIAdminFacilityWorkSummarypage
    {
        public RemoteWebDriver Driver;
        public ExtentTest logger;
        public TestContext Context;
        public ROIAdminFacilityWorkSummarypage(RemoteWebDriver driver, ExtentTest _loger, TestContext _context)
        {
            Driver = driver;
            logger = _loger;
            Context = _context;
        }

        public By listAllUsersOption = By.XPath("//td[contains(text(), 'List All Users')]");
        public By userMenuOption = By.XPath("//td[starts-with(text(),'Users')]");
        public By UserMenuOption = By.XPath("//td[starts-with(@id,'mroheader_MROPageHead1') and text()='Users']");
        public By ListAllUsersOption = By.XPath("//td[contains(text(),'List All Users')]");
        public By UsersInEditInfoPage = By.XPath("//td[starts-with(@id,'mroheader') and text()='Users']");
        


        /// <summary>
        /// Go to  List All Users
        /// </summary>
        public ROIFacilityUserListingPage ToSelectListAllUsers()
        {
            try
            {
                ROIMenuSelector menu = new ROIMenuSelector(Driver, logger, Context);

                try
                {
                    menu.SelectRoiAdmin("Users", "List All Users");
                }
                catch (Exception)
                {
                    menu.SelectRoiAdminMenuOptions("mnuROIAdmin", "Users", "List All Users");
                } 
            }
            catch (Exception ex)
            {

                throw new Exception($"Failed  to navigate listallUsers Page with details as  Message : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
            return new ROIFacilityUserListingPage(Driver,logger,Context);
        }

        public bool RefreshFacilityWorkSummaryPage()
        {
            try
            {
                
                Driver.SleepTheThread(5);
                bool isUserMenuDisplayed = Driver.FindElementBy(userMenuOption).Displayed;
                return isUserMenuDisplayed;
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to validate users menu option with details as  Message : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }

        public ROIFacilityUserListingPage ToSelectListAllUsersInEditUserInfoPage()
        {
            try
            {
                IWebElement userMenu = Driver.FindElementBy(UsersInEditInfoPage);
                userMenu.Click();
                IWebElement listallusers = Driver.FindElementBy(ListAllUsersOption);
                listallusers.Click();

            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to navigate listallUsers edit info Page with details as Message : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
            return new ROIFacilityUserListingPage(Driver, logger, Context);
        }

        public void SelectExpressDashboard()
        {
            try
            {
                ROIMenuSelector menu = new ROIMenuSelector(Driver, logger, Context);
                menu.SelectRoiAdminMenuOptions("mnuROIFacilityUser", "MRO Analyze", "eXpress Dashboard");
            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to select Facility List as : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }

        public void SelectPatientLookup()
        {
            try
            {
                ROIMenuSelector selector = new ROIMenuSelector(Driver, logger, Context);
                selector.Select("ROI Requests", "Patient Lookup");
                Driver.SleepTheThread(2);
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to click patient lookup : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }

        }

      



    }
}
