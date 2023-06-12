using MRO.ROI.Automation.Selenium;
using MRO.ROI.Automation.Utility;
using OpenQA.Selenium;
using System;

namespace MRO.ROI.Automation.Pages.Common
{
    public class LoginPage:ROIBasePage
    {
        public static bool IsAtLoginPage
        {
            get
            {
                var LoginPageLabel = Driver.FindElement(By.XPath("//td[contains(text(),'Please Login')]"));
                return LoginPageLabel.Text == "Please Login";
            }
        }

        public static bool IsAtTestFacility
        {
            get
            {
                var facilityLabel = Driver.FindElement(By.Id("mroheader_MROPageHead1_ctl00_lblSystem2"));
                return facilityLabel.Text == "ROI Test Facility";
            }
        }

        public static void GoToROIExternalRequesterPortal()
        {
            Driver.Navigate().GoToUrl(Driver.BaseAddress);
            Driver.Wait(TimeSpan.FromSeconds(2));
            var extRoiRequesterPortal = Driver.FindElement(By.XPath(PageElements.ROIRequesterPortal.extRoiRequesterPortal_xpath));
            extRoiRequesterPortal.Click();
            Driver.Wait(TimeSpan.FromSeconds(2));
            var facilityCodeInput = Driver.FindElement(By.Id(PageElements.FacilityLoginPage.facilityCode_Id));
            facilityCodeInput.SendKeys("test");
            var okButton = Driver.FindElement(By.Id(PageElements.FacilityLoginPage.facilityOkButton));
            okButton.Click();
        }

        public static void GoToROIInternalRequesterPortal()
        {
            Driver.Navigate().GoToUrl(Driver.BaseAddress);
            Driver.Wait(TimeSpan.FromSeconds(2));
            var intRoiRequesterPortal = Driver.FindElement(By.XPath(PageElements.ROIRequesterPortal.intRoiRequesterPortal_xpath));
            intRoiRequesterPortal.Click();
            Driver.Wait(TimeSpan.FromSeconds(2));
            var facilityCodeInput = Driver.FindElement(By.Id(PageElements.FacilityLoginPage.facilityCode_Id));
			//facilityCodeInput.SendKeys("test");
			facilityCodeInput.SendKeys("mroartf");
            var okButton = Driver.FindElement(By.Id(PageElements.FacilityLoginPage.facilityOkButton));
            okButton.Click();
        }

        public static void GoToROIFaclityLoginPage()
        {
            DebugUtil.DebugMessage("ROI Facility Login page");
            var facilityLink = Driver.FindElement(By.XPath(PageElements.FacilityLoginPage.facilityLink_Xpath));
            facilityLink.Click();
            Driver.Wait(TimeSpan.FromSeconds(2));

            var facilityCodeInput = Driver.FindElement(By.Id(PageElements.FacilityLoginPage.facilityCode_Id));
			//facilityCodeInput.SendKeys("test");
			facilityCodeInput.SendKeys("mroartf");

			var okButton = Driver.FindElement(By.Id(PageElements.FacilityLoginPage.facilityOkButton));
            okButton.Click();
        }

        //Added new method to log in to Iron Mountain.

        public static void GoToIronMountainROIFaclityLoginPage()
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

        public static void GoToROIAdminLoginPage()
        {
            //   var facilityLink = Driver.FindElement(By.XPath(PageElements.ROIAdminLoginPage.roiAdmin_xpath));
            //  facilityLink.Click();
            Driver.Navigate().GoToUrl(Driver.BaseAddress);
            Driver.Wait(TimeSpan.FromSeconds(2));
            var roiadminlink = Driver.FindElement(By.XPath(PageElements.ROIAdminLoginPage.roiAdmin_xpath));
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

        public static LoginCommand LoginAs(string userName)
        {
            return new LoginCommand(userName);
        }

        public static void LogOut()
        {
            var LogOutBtn = Driver.FindElementBy(By.XPath(PageElements.FacilityLoginPage.facilityLogOutButton_Xpath));
            LogOutBtn.Click();

        }
    }

    public class LoginCommand
    {
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

        public void Login()
        {
            var userNameInput = Driver.FindElement(By.Id("mrocontent_txtUserName"));
            userNameInput.Clear();
            userNameInput.SendKeys(userName);


            var passwordInput = Driver.FindElement(By.Id("mrocontent_txtPassword"));
            passwordInput.SendKeys("");
            passwordInput.Clear();
            passwordInput.SendKeys(password);

            WebElementHelper.Click_Action("mrocontent_cmdLogin", FindElementBy.Id);

            //var loginButton = Driver.FindElement(By.Id("mrocontent_cmdLogin"));
            //loginButton.Click();

            Driver.Wait(TimeSpan.FromSeconds(2));
        }

    }
}
