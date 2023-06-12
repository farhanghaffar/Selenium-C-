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
using System.Threading;

namespace MRO.ROI.Automation.Pages
{
    public class ROIFacilityFindARequestPage
    {
        public RemoteWebDriver Driver;
        public ExtentTest logger;
        public TestContext Context;
        public ROIFacilityFindARequestPage(RemoteWebDriver driver, ExtentTest _loger, TestContext _context)
        {
            Driver = driver;
            logger = _loger;
            Context = _context;
        }
        public By btnSearch = By.XPath("//input[@id='mrocontent_cmdSearch']");
        public By searchResultsMessage = By.XPath("//span[@id='mrocontent_lblTooMany']");
        public By btnExportToExcel = By.XPath("//input[@id='mrocontent_lnkExport']");
        public string SearchDataByFirstNameAndGetResultantMessage()
        {
            string firstName = string.Empty;
            string infoMsg = string.Empty;
            firstName = "test";
            IWebElement iFirstName = Driver.FindElementBy(By.Id(PageElements.LogNewRequestPage.firstName_Id));
            iFirstName.Clear();
            iFirstName.SendKeys(firstName);
            logger.Log(Status.Info, $"First Name entered ({firstName})");
            Driver.Click(btnSearch);
            Driver.WaitInSeconds(5);
            infoMsg= Driver.GetText(searchResultsMessage);
            return infoMsg;
        }

        public void ValidateExcelData()
        {
            Driver.Click(btnExportToExcel);       
        
        }

               
    }
}
