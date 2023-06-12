using AventStack.ExtentReports;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MRO.ROI.Automation.Common.Navigation;
using MRO.ROI.Automation.Selenium;
using MRO.ROI.Automation.Utility;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using System;
using System.IO;
using System.Reflection;
using static MRO.ROI.Automation.Utility.IniFile;

namespace MRO.ROI.Automation.Pages
{
    public class ROIAdminEditFacilityPolicy
    {
        public RemoteWebDriver Driver;
        public ExtentTest logger;
        public TestContext Context;
        public CSVReader csvReader;
        public ROIAdminEditFacilityPolicy(RemoteWebDriver driver, ExtentTest _loger, TestContext _context)
        {
            Driver = driver;
            logger = _loger;
            Context = _context;
        }

        public string csvFileName = "CDPasswordSpecification.csv";
        public By usePasswordFormatRadioElement = By.XPath("//input[@id='mrocontent_rbCDPwdFormat']");
        public By expandMacorsicon = By.XPath("//input[@id='mrocontent_custPwdMacro_btnExpand']");
        public By passwordFormatTxtBox = By.XPath("//input[@name='mrocontent$custPwdMacro$txtBxMacro']");
        public By savePolicyButton = By.XPath("//input[@id='mrocontent_cmdSave']");
        public By testMacroButton = By.Id("mrocontent_custPwdMacro_btnTestMacro");
        public By requestIdTextbox = By.XPath("//input[@id='mrocontent_custPwdMacro_txtBxRequestID']");
        public By expandMacro = By.Id("mrocontent_custPwdMacro_btnExpandMacros");
        public By macroExpansion = By.Id("mrocontent_custPwdMacro_lblTestMacro");
        public By chkSendEMAILPasswordAsSeparateMailing = By.XPath("//input[@id ='mrocontent_cbEmailSendPwdLetter']");

        /// <summary>
        /// Click on Expand Macros 
        /// </summary> 
        public void ExpandMacors()
        {
            try
            {
                Driver.Click(usePasswordFormatRadioElement);
                Driver.Click(expandMacorsicon);
                Driver.ClearText(passwordFormatTxtBox);
                csvReader = new CSVReader(Path.GetFullPath(Path.Combine(Assembly.GetExecutingAssembly().Location, "..", "TestData", csvFileName)));
                string randommacroFormat= csvReader.GetDataFromCSVFile("RandomMacroFormat");
                logger.Log(Status.Info, $"Entering the password text {randommacroFormat}");
                Driver.SendKeys(passwordFormatTxtBox, randommacroFormat.ToString());
                Driver.Click(savePolicyButton);

            }
            catch (Exception ex)
            {

                throw new Exception($"Failed  to click Expand Macors   with details as Message : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }

        }
        /// <summary>
        /// Go to  Test Macro 
        /// </summary> 

        public void ClickTestMacro()
        {
            try
            {
                Driver.Click(usePasswordFormatRadioElement);
                Driver.Click(testMacroButton);
                Driver.ScrollIntoViewAndClick(requestIdTextbox);
                string requestId = IniHelper.ReadConfig("CDPasswordSpecification", "RequestId");
                Driver.SendKeys(requestIdTextbox,requestId);
                logger.Log(Status.Info, $"Entered requestid called ({requestId}) in request id texbox");
                Driver.ScrollIntoViewAndClick(expandMacro);

            }
            catch (Exception ex)
            {

                throw new Exception($"Failed  to click Test Macros   with details as Message : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }
        /// <summary>
        ///To vadlidate Resultant Macro Expansion
        /// </summary> 
        public void VerifyResultantMacro()
        {
            try
            {               
                string regexp = @"^[A-Z0-9]";
                Driver.ClickAndCheckNextElement(expandMacro, macroExpansion);
                string resultantMacro = Driver.FindElementBy(macroExpansion).Text;
                if ((resultantMacro.Length == 8) && System.Text.RegularExpressions.Regex.Match(resultantMacro,regexp).Success)
                {
                    logger.Log(Status.Pass, $"Verified resulting macro : ({resultantMacro}) aganist length of the characters to 8 and its is  made up of capital letters and numbers");
                }
                else
                {
                    Assert.Fail($"Failed to verify resultant macro ({resultantMacro}) for its length ({resultantMacro.Length}) and regular expression ({regexp})");
                    
                }
                
            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to verify Resultant Macro Expansion  with details as Message : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }

        /// <summary>
        /// Go to  Facility List  Page
        /// </summary>
        public ROIAdminFacilityListPage FacilityList()
        {
            try
            {
                MenuSelector menuSelector = new MenuSelector(Driver, logger,Context);
                menuSelector.SelectRoiAdmin("Facilities", "Facility List");
                Driver.WaitUntilDOMLoaded();
            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to click Facilities list   with exception details as : {ex.Message}");

            }

            return new ROIAdminFacilityListPage(Driver,logger,Context);
        }

        public void CheckSendEMAILPasswordAsSeparateMailingOption()
        {
            try
            {
                bool isCheck = Driver.FindElementBy(chkSendEMAILPasswordAsSeparateMailing).Selected;
                if (isCheck == false)
                {
                    Driver.Click(chkSendEMAILPasswordAsSeparateMailing);
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to check Send EMAIL Password As Separate Mailing option  with details as Message : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }

        }

        /// <summary>
        /// Go to  Test Macro 
        /// </summary> 
        public void ClickTestMacroForEmailDelivery()
        {
            try
            {
                Driver.Click(usePasswordFormatRadioElement);
                Driver.Click(testMacroButton);
                Driver.ScrollIntoViewAndClick(requestIdTextbox);
                string requestId = IniHelper.ReadConfig("EmailDeliveryPasswordTest", "RequestId");
                Driver.SendKeys(requestIdTextbox, requestId);
                logger.Log(Status.Info, $"Entered requestid called ({requestId}) in request id texbox");
                Driver.ScrollIntoViewAndClick(expandMacro);
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed  to click Test Macros with details as Message : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }

    }
}
