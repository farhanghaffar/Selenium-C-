using MRO.ROI.Automation.Selenium;
using MRO.ROI.Automation.Utility;
using OpenQA.Selenium;

namespace MRO.ROI.Automation.Pages.Common
{
    public static class LoginMenuPage
    {
        public static void GoToLoginMenuPage()
        {
            DebugUtil.DebugMessage("Login Menu page");
            Driver.Navigate().GoToUrl(Driver.BaseAddress);
        }

        public static bool IsAtLoginMenuPage
        {
            get
            {
                var loginMenuLabel = Driver.FindElement(By.XPath("//td[contains(text(),'Choose a Login Page')]"));
                return loginMenuLabel.Text == "Choose a Login Page";
            }
        }
    }
}
