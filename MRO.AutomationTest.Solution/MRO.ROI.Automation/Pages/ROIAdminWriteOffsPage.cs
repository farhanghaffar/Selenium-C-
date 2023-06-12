using AventStack.ExtentReports;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MRO.ROI.Automation.Selenium;
using MRO.ROI.Automation.Utility;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.UI;
using System;
using System.IO;
using System.Reflection;

namespace MRO.ROI.Automation.Pages
{
    public class ROIAdminWriteOffsPage
    {
        public RemoteWebDriver Driver;
        public ExtentTest logger;
        public TestContext Context;
        public CSVReader csvReader;
        public ROIAdminWriteOffsPage(RemoteWebDriver driver, ExtentTest _loger, TestContext _context)
        {
            Driver = driver;
            logger = _loger;
            Context = _context;
        }
        public string csvFileName = "ROIWriteOffCapTest.csv";
        public By selectAccountType = By.XPath("//select[@id='mrocontent_lstReason']");
        public By ammountTxt = By.XPath("//input[@id='mrocontent_txtAmount']");
        public By refDate = By.XPath("//input[@id='mrocontent_txtDate']");
        public By refNumber = By.XPath("//input[@id='mrocontent_txtCheckNumber']");
        public By commitButton = By.XPath("//input[@id='mrocontent_cmdAdd']");
        public By invoiceAmount = By.XPath("//span[@id='mrocontent_lblL2Inv']");
        public By writeoffsHistory = By.XPath("//div[@id='mrocontent_pnlReport']");
        public By adjustedBal = By.XPath("//span[@id='mrocontent_lblL2ARBalance']");


        /// <summary>
        /// Create writeoffs
        /// </summary>
        public string CreateAndVerifyWriteoff()
        {           
            try
            {
                csvReader = new CSVReader(Path.GetFullPath(Path.Combine(Assembly.GetExecutingAssembly().Location, "..", "TestData", csvFileName)));
                string accountTypeValue = csvReader.GetDataFromCSVFile("AccountType");
                var accountTypeDropDown = Driver.FindElementBy(selectAccountType);
                var selectElement = new SelectElement(accountTypeDropDown);
                selectElement.SelectByText(accountTypeValue);                              
                string amountValue = csvReader.GetDataFromCSVFile("Amount");
                Driver.FindElementBy(ammountTxt).SendKeys(amountValue); 
                var todaysDate = String.Format("{0:M/dd/yyyy}", DateTime.Now).Replace("-", "/");
                var referenceDate = Driver.FindElementBy(refDate);
                referenceDate.SendKeys(todaysDate);                
                string refNumberValue = csvReader.GetDataFromCSVFile("RefNumber");
                Driver.FindElementBy(refNumber).SendKeys(refNumberValue);
                Driver.Click(commitButton);
                Driver.Wait(TimeSpan.FromSeconds(2));
                LogNewRequestPage logNewRequestPage = new LogNewRequestPage(Driver, logger, Context);
                logNewRequestPage.acceptalert();
                Driver.Wait(TimeSpan.FromSeconds(2));
                logger.Log(Status.Pass, "Popup displayed with the message - Write Off amount can't be greater than invoice amount!");
                Driver.FindElementBy(ammountTxt).Clear();
                string getInvoiceAmount = Driver.FindElementBy(invoiceAmount).Text.Trim();                
                Driver.FindElementBy(ammountTxt).SendKeys(getInvoiceAmount);
                Driver.Click(commitButton);
                IWebElement writeoffHistory = Driver.FindElementBy(writeoffsHistory);
                if (writeoffHistory.Displayed == true)
                {
                   logger.Log(Status.Pass, "Successfully verified WriteOff History had been updated with your Date,User name and Amount");
                }                
                string adjustedBalance = Driver.FindElementBy(adjustedBal).Text.Trim();
                if (adjustedBalance == "$0.00")
                {                    
                    logger.Log(Status.Pass, $"Successfully verified adjusted balance is :({adjustedBalance})");
                }               
                return adjustedBalance;

            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to create and verify writeoff's  : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
            
        }

        public void SwitchToDefaultRSSPage()
        {
            try
            {
                RemoteWebDriver dr = Driver;
                IJavaScriptExecutor js = (IJavaScriptExecutor)dr;
                string tab1 = Driver.WindowHandles[0];
                string tab2 = Driver.WindowHandles[1];
                Driver.SwitchTo().Window(tab1);

            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to switch to default RSS Page  : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }
    }
}
