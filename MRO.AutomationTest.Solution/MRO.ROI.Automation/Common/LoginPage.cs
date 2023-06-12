using AventStack.ExtentReports;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MRO.ROI.Automation.Selenium;
using MRO.ROI.Automation.Utility;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using System;

namespace MRO.ROI.Automation.Pages.Common
{
    public class LoginPage
    {
        public RemoteWebDriver Driver;
        public ExtentTest logger;
        public TestContext Context;
        public LoginPage(RemoteWebDriver driver, ExtentTest _loger, TestContext _context)
        {
            Driver = driver;
            logger = _loger;
            Context = _context;
        }
        By UserNameSpan = By.XPath("//span[@class='MastheadUserName']");
        public void Login(string UserName, string Password)
        {
            var userNameInput = Driver.FindElement(By.Id("mrocontent_txtUserName"));
            userNameInput.Clear();
            userNameInput.SendKeys(UserName);


            var passwordInput = Driver.FindElement(By.Id("mrocontent_txtPassword"));
            passwordInput.SendKeys("");
            passwordInput.Clear();
            passwordInput.SendKeys(Password);

            Driver.FindElement(By.Id("mrocontent_cmdLogin")).Click();
            // WebElementHelper elementHelper = new WebElementHelper(Driver, logger, Context);
            //elementHelper.Click_Action("mrocontent_cmdLogin", FindElementBy.Id);
            Driver.SleepTheThread(2);
            bool isLoggedIn = Driver.isElementDisplayed(UserNameSpan);
            Assert.IsTrue(isLoggedIn, "Failed to verify user name is showing after logging in.");
            logger.Log(Status.Info, "Logged In successfully");

        }

        public bool IsAtLoginPage
        {
            get
            {
                var LoginPageLabel = Driver.FindElement(By.XPath("//td[contains(text(),'Please Login')]"));
                return LoginPageLabel.Text == "Please Login";
            }
        }


        public bool IsAtTestFacility
        {
            get
            {
                var facilityLabel = Driver.FindElement(By.Id("mroheader_MROPageHead1_ctl00_lblSystem2"));
                return facilityLabel.Text == "ROI Test Facility";
            }
        }

       public void AdvanceAndProceed()
        {
            var Advancebtn =Driver.FindElement(By.XPath("//button[@id='details-button']"));
            var Proceed = Driver.FindElement(By.XPath("//a[@id='proceed-link']"));

            if(Advancebtn.ToString().Length > 0)
            {
                Advancebtn.Click();
            }

            if (Proceed.ToString().Length > 0)
            {
                Proceed.Click();
            }
        }

        public  void GoToROIExternalRequesterPortal(string BaseAddress)
        {
            Driver.Navigate().GoToUrl(BaseAddress);
            Driver.Wait(TimeSpan.FromSeconds(2));
            var extRoiRequesterPortal = Driver.FindElement(By.XPath(PageElements.ROIRequesterPortal.extRoiRequesterPortal_xpath));
            extRoiRequesterPortal.Click();
            Driver.Wait(TimeSpan.FromSeconds(2));
            var facilityCodeInput = Driver.FindElement(By.Id(PageElements.FacilityLoginPage.facilityCode_Id));
            facilityCodeInput.SendKeys("test");
            var okButton = Driver.FindElement(By.Id(PageElements.FacilityLoginPage.facilityOkButton));
            okButton.Click();
        }

        public  void GoToROIInternalRequesterPortal(string BaseAddress)
        {
            Driver.Navigate().GoToUrl(BaseAddress);
            Driver.Wait(TimeSpan.FromSeconds(2));
            var intRoiRequesterPortal = Driver.FindElement(By.XPath(PageElements.ROIRequesterPortal.intRoiRequesterPortal_xpath));
            intRoiRequesterPortal.Click();
            Driver.Wait(TimeSpan.FromSeconds(2));
            var facilityCodeInput = Driver.FindElement(By.Id(PageElements.FacilityLoginPage.facilityCode_Id));
			facilityCodeInput.SendKeys("test");
			//facilityCodeInput.SendKeys("mroartf");
            var okButton = Driver.FindElement(By.Id(PageElements.FacilityLoginPage.facilityOkButton));
            okButton.Click();
        }

        public void GoToROIFaclityLoginPage(string BaseAddress, string facilityType = "test")
        {
            Driver.Navigate().GoToUrl(BaseAddress);
            DebugUtil.DebugMessage("ROI Facility Login page");
            var facilityLink = Driver.FindElementBy(By.XPath(PageElements.FacilityLoginPage.facilityLink_Xpath));
            facilityLink.Click();
            Driver.Wait(TimeSpan.FromSeconds(2));

            var facilityCodeInput = Driver.FindElementBy(By.Id(PageElements.FacilityLoginPage.facilityCode_Id));
            if (facilityType == "test")
            {
                facilityCodeInput.SendKeys("test");
            }
            else
            {
                facilityCodeInput.SendKeys("mroartf");
            }

			var okButton = Driver.FindElement(By.Id(PageElements.FacilityLoginPage.facilityOkButton));
            okButton.Click();
        }

        public void GoToROITestFaclityLoginPage(string BaseAddress)
        {
            Driver.Navigate().GoToUrl(BaseAddress);
            DebugUtil.DebugMessage("ROI Facility Login page");
            var facilityLink = Driver.FindElementBy(By.XPath(PageElements.FacilityLoginPage.facilityLink_Xpath));
            facilityLink.Click();
            Driver.Wait(TimeSpan.FromSeconds(2));

            var facilityCodeInput = Driver.FindElementBy(By.Id(PageElements.FacilityLoginPage.facilityCode_Id));
            facilityCodeInput.SendKeys("test");


            var okButton = Driver.FindElement(By.Id(PageElements.FacilityLoginPage.facilityOkButton));
            okButton.Click();
        }

        //Added new method to log in to Iron Mountain.

        public  void GoToIronMountainROIFaclityLoginPage(string BaseAddress)
        {
            DebugUtil.DebugMessage("ROI Facility Login page");
            var facilityLink = Driver.FindElement(By.XPath(PageElements.FacilityLoginPage.facilityLink_Xpath));
            facilityLink.Click();
            Driver.Wait(TimeSpan.FromSeconds(2));

            var facilityCodeInput = Driver.FindElement(By.Id(PageElements.FacilityLoginPage.facilityCode_Id));
            facilityCodeInput.SendKeys("irmtest");

            var okButton = Driver.FindElement(By.Id(PageElements.FacilityLoginPage.facilityOkButton));
            okButton.Click();
        }

        public  void GoToROIAdminLoginPage(string BaseAddress)
        {
            Driver.Navigate().GoToUrl(BaseAddress);
            Driver.Wait(TimeSpan.FromSeconds(2));
            var roiadminlink = Driver.FindElementBy(By.XPath(PageElements.ROIAdminLoginPage.roiAdmin_xpath));
            roiadminlink.Click();
            Driver.Wait(TimeSpan.FromSeconds(2));
        }

        public static void GoToROIRequestorPortalLoginPage()
        {
            //TODO: Implement navigation to requestor portal
        }

        public static void GoToMROAdminLoginPage()
        {
            //TODO: Implement navigation to MRO Admin
        }

        public static void GoToROITrackerLoginPage()
        {
            //TODO: Implement navigation to ROI Tracker.
        }

        public static void GoToChartOnlineLoginPage()
        {
            //TODO: Implement navigation to Chart Online
        }

        public static void GoToFilingCabinetOnlinePage()
        {
            //TODO: Implement navigation to Filing Cabinet Online
        }

        public static void GoToParentBusinessAdminPage()
        {
            //TODO: Implement navigation to Parent Business Admin
        }

        public void GoToROIAdminhqiisstgLoginPage(string hqiisstgURL)
        {
            Driver.Navigate().GoToUrl(hqiisstgURL);
            Driver.Wait(TimeSpan.FromSeconds(2));
            AdvanceAndProceed();
            var roiadminlink = Driver.FindElementBy(By.XPath(PageElements.ROIAdminLoginPage.roiAdmin_xpath));
            roiadminlink.Click();
            Driver.Wait(TimeSpan.FromSeconds(2));
        }
        public static LoginCommand LoginAs(string userName)
        {
            return new LoginCommand(userName);
        }

        public  void LogOut()
        {
            try
            {
                var LogOutBtn = Driver.FindElementBy(By.XPath(PageElements.FacilityLoginPage.facilityLogOutButton_Xpath));
                LogOutBtn.Click();

            }
            catch (Exception ex)
            {
                var LogOutBtn = Driver.FindElementBy(By.XPath("//i[contains(@class, 'mdi-close')]/parent::a"));
                LogOutBtn.Click();

            }
            logger.Log(Status.Info, "Clicked logout icon");

        }
    }

    public class LoginCommand
    {
        public RemoteWebDriver Driver;
        public ExtentTest logger;
        public LoginCommand(RemoteWebDriver driver, ExtentTest _loger)
        {
            Driver = driver;
            logger = _loger;
        }

        private readonly string userName;
        private string password;

        public LoginCommand(string userName)
        {
            this.userName = userName;
        }

        public LoginCommand WithPassword(string password)
        {
            this.password = password;
            return this;
        }
    }
}
