using AventStack.ExtentReports;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MRO.ROI.Automation.Pages;
using MRO.ROI.Automation.Pages.Common;
using MRO.ROI.Automation.Selenium;
using MRO.ROI.Automation.Utility;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using System;
using System.IO;
using System.Reflection;

namespace MRO.ROI.Automation.Pages
{
    public class ROIAdminCCShoppingCartPage
    {
        public RemoteWebDriver Driver;
        public ExtentTest logger;
        public TestContext Context;
        public CSVReader csvReader;

        public ROIAdminCCShoppingCartPage(RemoteWebDriver driver, ExtentTest _loger, TestContext _context)
        {
            Driver = driver;
            logger = _loger;
            Context = _context;
        }

        public By requestId = By.XPath("//table[@id='mrocontent_tblCartItems']//tr[@class='TableBody']//td[2]");
        public By checkOut = By.XPath("//input[@value='Check Out']");
        public By continueButton = By.XPath("//input[@value='Continue']");
        public By patientName = By.XPath("//table[@id='mrocontent_tblCartItems']//tr[@class='TableBody']//td[3]//a");
        

        public void CheckOut()
        {
            try
            {
                Driver.DirectClick(checkOut);
                Driver.DirectClick(continueButton);
            }
            catch (System.Exception ex)
            {

                throw new Exception($"Failed to checkout on cc shopping cart page Message : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }

        public string GetRequestIdOnCCShoppingCartPage()
        {
            try
            {
                return Driver.GetText(requestId);
            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to get the request id on shopping cart page Message : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }
        public string GetPatientNameOnCCShoppingCartPage()
        {
            try
            {
                return Driver.GetText(patientName).Trim();
            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to get the patient name on shopping cart page Message : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }
    }
}
