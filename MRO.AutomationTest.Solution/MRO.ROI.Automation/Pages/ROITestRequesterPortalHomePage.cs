using AventStack.ExtentReports;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MRO.ROI.Automation.Common.Navigation;
using MRO.ROI.Automation.Pages;
using MRO.ROI.Automation.Pages.Common;
using MRO.ROI.Automation.Selenium;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Remote;
using System;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;

namespace MRO.ROI.Automation.Pages
{
    public class ROITestRequesterPortalHomePage
    {
        public RemoteWebDriver Driver;
        public ExtentTest logger;
        public TestContext Context;
        public ROITestRequesterPortalHomePage(RemoteWebDriver driver, ExtentTest _loger, TestContext _context)
        {
            Driver = driver;
            logger = _loger;
            Context = _context;
        }


        public By noElement = By.XPath("//input[@value='No']");
        public By payForRecordsMenuElement = By.XPath("//td[starts-with(@id,'mroheader')and text()='Pay For Records']");
        public By payForRecordsSubMenu = By.XPath("//td[@title='Pay for requests']");
        public By requestRecords = By.XPath("//td[contains(text(),'Request Records')]");
        public By requestIdTxtBox = By.Id("mrocontent_txtRequestID");
        public By searchId = By.Id("mrocontent_cmdSearch");
        public By viewRequest = By.XPath("//a[@title='View Request']");
        public By openIssue = By.XPath("//a[@id='mrocontent_cmdIssues']");
        public By issueName = By.XPath("(//td[@class='TableAlert'])[2]//a");
        public By backToRequest = By.Id("mrocontent_btnRequest");
        public void ClickOnNotificationPopUp()
        {
            try
            {
                Driver.SwitchTo().Frame("radWndPrompt");
                Driver.Click(noElement);
            }
            catch (Exception ex)
            {

                throw new Exception($"Failed  to no button with details as Message : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
           
        }

       
        public void GotoPayForRecords()
        {
            try
            {

                Driver.SwitchTo().DefaultContent();
                Driver.Wait(TimeSpan.FromSeconds(5));
                Driver.FindElementBy(payForRecordsMenuElement).Click();
                Driver.Wait(TimeSpan.FromSeconds(2));
                Driver.Click(payForRecordsSubMenu);
            }

            catch (Exception ex)
            {
                throw new Exception($"Failed  to click pay for records with details as Message : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
           
        }
         public void GotoRequestRecords()
        {
            try
            {
                Driver.SwitchTo().DefaultContent();
                Driver.Wait(TimeSpan.FromSeconds(2));
                Driver.Click(requestRecords);
                Driver.Wait(TimeSpan.FromSeconds(2));
            }
            catch (Exception ex)
            {

                throw new Exception($"Failed  to click request records with details as Message : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }
       public void SearchRequestId(string requestId)
        {
            try
            {
                Driver.SwitchTo().DefaultContent();
                Driver.Wait(TimeSpan.FromSeconds(2));
                Driver.SendKeys(requestIdTxtBox, requestId);
                Driver.Click(searchId);
                Driver.Click(viewRequest);
                Driver.Wait(TimeSpan.FromSeconds(5));
            }
            catch (Exception ex)
            {

                throw new Exception($"Failed  to search request id with details as Message : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }
        public string  ViewOpenIssue()
        {
            try
            {
                Driver.Click(openIssue);
                string issue = Driver.GetText(issueName);
                Driver.Click(backToRequest);
                return issue;
            }
            catch (Exception ex)
            {

                throw new Exception($"Failed  to view open issue with details as Message : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }

        public string GetDataFromPDFFile(string fileName, string amount)
        {
            PdfReader reader = new PdfReader(fileName);
            int intPageNum = reader.NumberOfPages;
            string[] sWords = new string[] { };
            string text = "";

            for (int i = 1; i <= intPageNum; i++)
            {
                text = PdfTextExtractor.GetTextFromPage(reader, i, new LocationTextExtractionStrategy());
                sWords = text.Split('\n');

                if (sWords.ToString().Equals(amount))
                {
                    
                    return amount;
                }              
            }
           return amount;
        }

        public bool VerifyOnNotificationPopUp()
        {
            try
            {
                string popElement = "//td[contains(text(),'Notifications')]";
                WebElementHelper helper = new WebElementHelper(Driver, logger, Context);
                bool isDisplayed = helper.IsElementPresent(popElement);
                return isDisplayed;
            }
            catch (Exception ex)
            {

                throw new Exception($"Failed  to no button with details as Message : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }

        }
    }
}
