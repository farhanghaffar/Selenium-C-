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
   public class ROIChangePasswordPage
    {
        public RemoteWebDriver Driver;
        public ExtentTest logger;
        public TestContext Context;
        public ROIChangePasswordPage(RemoteWebDriver driver, ExtentTest _loger, TestContext _context)
        {
            Driver = driver;
            logger = _loger;
            Context = _context;
        }

        public By currentPassword = By.XPath("//input[@id='mrocontent_txtOldPassword']");
        public By newPassword = By.XPath("//input[@id='mrocontent_txtPassword1']");
        public By reenterPassword = By.XPath("//input[@id='mrocontent_txtPassword2']");
        public By changePasswordBtn = By.XPath("//input[@id='mrocontent_cmdChangePassword']");

        public void changePassword(string oldPwd,string newPwd,string  reenterNewPwd)
        {
            try
            {
                Driver.SendKeys(currentPassword, oldPwd);
                Driver.SendKeys(newPassword, newPwd);
                Driver.SendKeys(reenterPassword, reenterNewPwd);
                Driver.Click(changePasswordBtn);
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to change password with details : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }

       
    }
}
