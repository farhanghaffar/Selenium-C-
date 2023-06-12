using MRO.ROI.Automation.Selenium;
using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using AventStack.ExtentReports;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MRO.ROI.Automation.Pages
{
    public class ROIAdminUsersPage
    {
        public RemoteWebDriver Driver;
        public ExtentTest logger;
        public TestContext Context;
        public ROIAdminUsersPage(RemoteWebDriver driver, ExtentTest _loger, TestContext _context)
        {
            Driver = driver;
            logger = _loger;
            Context = _context;
        }

        public const string userid = "akothuri";
        public const string password = "TestingCigniti";
        public const string adminUserid = "cigniti-akothuri";
        public const string adminPassword = "TestingMRO@123";     
        
        /// <summary>
        /// ROI Admin  Login For Specific User
        /// </summary>
        public void ROIAdminLoginForSpecificUser()
        {
            try
            {
                Driver.FindElement(By.XPath(PageElements.ROIAdminLoginPage.roiAdminUsername_xpath)).SendKeys(adminUserid);
                logger.Log(Status.Info, $"Entered username ({adminUserid})");
                Driver.FindElement(By.XPath(PageElements.ROIAdminLoginPage.roiAdminPassword_xpath)).SendKeys(adminPassword);
                logger.Log(Status.Info, $"Entered password ({password})");
                Driver.FindElement(By.XPath(PageElements.ROIAdminLoginPage.okButton_xpath)).Click();
            }
            catch (Exception ex)
            {

                throw new Exception($"Failed  to login for roiAdmin with details as Message : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
           
        }
      
        /// <summary>
        /// Login to ROI Facility for Specific User
        /// </summary>
        public  void ROIFacilityLoginForSpecificUser()
        {
            try
            {

                Driver.FindElement(By.XPath(PageElements.ROIAdminLoginPage.roiAdminUsername_xpath)).SendKeys(userid);
                Driver.FindElement(By.XPath(PageElements.ROIAdminLoginPage.roiAdminPassword_xpath)).SendKeys(password);
                Driver.FindElement(By.XPath(PageElements.ROIAdminLoginPage.okButton_xpath)).Click();

            }
            catch (Exception ex)
            {

                throw new Exception($"Failed  to login as Facility user exception details as : {ex.Message}");
            }
            
        }
       
               
    }
		
}
