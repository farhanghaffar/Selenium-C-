using AventStack.ExtentReports;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MRO.ROI.Automation.Selenium;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using System;

namespace MRO.ROI.Automation.Pages
{
    public class ROIFacilityLinkToAnotherRequestPage
    {
        public RemoteWebDriver Driver;
        public ExtentTest logger;
        public TestContext Context;
        public ROIFacilityLinkToAnotherRequestPage(RemoteWebDriver driver, ExtentTest _loger, TestContext _context)
        {
            Driver = driver;
            logger = _loger;
            Context = _context;
        }
        public By txtLookupRequestID = By.XPath("//input[@id='mrocontent_txtLookupID']");
        public By lookUpButton = By.XPath("//input[@id='mrocontent_cmdLookup']");
        public By connectBtn = By.XPath("//input[@id='mrocontent_cmdConnect']");        
        public By copyDocumentBtn = By.XPath("//input[@id='mrocontent_cmdCopy']");
        public By doneBtn = By.XPath("//input[@id='mrocontent_cmdDone']");
        public By lookupRequestIdElement = By.Id("mrocontent_txtLookupID");       
        public By copyLocationChk4 = By.XPath("(//input[starts-with (@id,'mrocontent_cbCopy')])[2]");
        public By copyLocationChk1 = By.XPath("(//input[starts-with (@id,'mrocontent_cbCopy')])[1]");
        public By copyLocationChk2 = By.XPath("//td[contains(text(),'Test1')]/..//td[1]//input");
        public By copyLocationChk3 = By.XPath("//td[contains(text(),'Test2')]/..//td[1]//input");
        public By patientName = By.Id("mrocontent_lblPatientName");
        public By location = By.Id("mrocontent_tdLocation");
        public By disconnect = By.XPath("//input[@id='mrocontent_cmdDisconnect']");
        string LookUPTxtBox = "//input[@id='mrocontent_txtLookupID']";

        /// <summary>
        /// Verify link to another request page
        /// </summary>
        public ROIFacilityRequestStatusPage LinkToAnotherRequestPage_MultiComp(string requestID)
        {
            try
            {
                Driver.FindElementBy(txtLookupRequestID).SendKeys(requestID);               
                Driver.Click(lookUpButton);
                Driver.Click(connectBtn);
                Driver.Click(copyLocationChk1);
                WebElementHelper webElementHelper = new WebElementHelper(Driver, logger, Context);
                Driver.Wait(TimeSpan.FromSeconds(5));

                Driver.SwitchTo().Alert().Accept();

                Driver.Click(copyLocationChk2);
                Driver.SwitchTo().Alert().Accept();
                Driver.Wait(TimeSpan.FromSeconds(5));

                Driver.Click(copyLocationChk3);
                Driver.SwitchTo().Alert().Accept();
                Driver.Wait(TimeSpan.FromSeconds(2));

                Driver.Click(copyDocumentBtn);
                Driver.SwitchTo().Alert().Accept();
                Driver.Wait(TimeSpan.FromSeconds(5));
                Driver.Click(doneBtn);
                Driver.Wait(TimeSpan.FromSeconds(5));
            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to link to another request : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }

            return new ROIFacilityRequestStatusPage(Driver, logger,Context);

        }

        /// <summary>
        /// Verify link to another request page
        /// </summary>
        public ROIFacilityRequestStatusPage LinkToAnotherRequestPage_ROINativePDF(string requestID)
        {
            try
            {
                Driver.FindElementBy(txtLookupRequestID).SendKeys(requestID);
                Driver.Click(lookUpButton);
                Driver.Click(connectBtn);
                Driver.SleepTheThread(5);
                Driver.Click(copyLocationChk1);
                Driver.SleepTheThread(5);
                WebElementHelper webElementHelper = new WebElementHelper(Driver, logger, Context);
                Driver.SwitchTo().Alert().Accept();
                Driver.Wait(TimeSpan.FromSeconds(5));
                Driver.Click(copyDocumentBtn);
                Driver.SwitchTo().Alert().Accept();
                Driver.Wait(TimeSpan.FromSeconds(5));
                Driver.Click(doneBtn);
                Driver.Wait(TimeSpan.FromSeconds(5));
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to link to another request : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }

            return new ROIFacilityRequestStatusPage(Driver, logger, Context);

        }

        /// <summary>
        ///Click on LookUpId Button
        /// </summary>
        public void ClickOnLookupId(string requestId)
        {
            try
            {
                Driver.SendKeys(lookupRequestIdElement, requestId);
                Driver.Click(lookUpButton);
                Driver.Click(connectBtn);
                Driver.AcceptAlert();
                Driver.Click(doneBtn);
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed  to click on lookupId with details as Message : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }

        }


        public string LinkToAnotherRequestPage_PatientName()
        {
            try
            {
                return Driver.GetText(patientName);

            }
            catch (Exception ex)
            {
                throw new Exception($"Failed  to return patient name with details as Message : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}"); throw;
            }
        }
        public string LinkToAnotherRequestPage_Location()
        {
            try
            {
                string _location = Driver.GetText(location);
                _location = _location.Replace('@', ' ').Trim();
                return _location;

            }
            catch (Exception ex)
            {
                throw new Exception($"Failed  to return location with details as Message : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}"); throw;
            }
        }
        public void DisconnectNClickOnLookupId(string requestId)
        {
            try
            {
                WebElementHelper helper = new WebElementHelper(Driver, logger, Context);
                bool lookuptxtbox = helper.IsElementPresent(LookUPTxtBox);
                if (lookuptxtbox == false)
                {
                    Driver.Click(disconnect);
                    Driver.SendKeys(lookupRequestIdElement, requestId);
                    Driver.Click(lookUpButton);
                    Driver.Click(connectBtn);
                    Driver.AcceptAlert();
                    Driver.Click(doneBtn);
                }
                else
                {
                    Driver.SendKeys(lookupRequestIdElement, requestId);
                    Driver.Click(lookUpButton);
                    Driver.Click(connectBtn);
                    Driver.AcceptAlert();
                    Driver.Click(doneBtn);
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed  to click on lookupId with details as Message : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }

        }

        public void LinkTwoRequests(string requestID)
        {
            try
            {
                Driver.FindElementBy(txtLookupRequestID).SendKeys(requestID);
                Driver.Click(lookUpButton);
                Driver.Click(connectBtn);
                Driver.SleepTheThread(5);
                Driver.Click(copyLocationChk1);
                Driver.SleepTheThread(5);
                WebElementHelper webElementHelper = new WebElementHelper(Driver, logger, Context);
                Driver.SwitchTo().Alert().Accept();
                Driver.Wait(TimeSpan.FromSeconds(5));
                Driver.Click(copyDocumentBtn);
                Driver.SwitchTo().Alert().Accept();
                Driver.Wait(TimeSpan.FromSeconds(5));
                Driver.Click(doneBtn);
                Driver.Wait(TimeSpan.FromSeconds(5));
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to link to another request : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }

    }
}
