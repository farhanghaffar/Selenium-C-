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
using static MRO.ROI.Automation.Utility.IniFile;

namespace MRO.ROI.Automation.Pages
{
   public class ROIAdminUpdatePatientInformationPage
    {
        public static string csvFileName = "ROIAdminBOERequesterPANRequiredTest.csv";
        public RemoteWebDriver Driver;
        public ExtentTest logger;
        public TestContext Context;
        public ROIAdminUpdatePatientInformationPage(RemoteWebDriver driver, ExtentTest _loger, TestContext _context)
        {
            Driver = driver;
            logger = _loger;
            Context = _context;
        }
        public By updatePatientInfoHeader = By.XPath("//td[contains(text(),'Update Patient Information')]");
        public By panOnUpdateInfoPage = By.XPath("//input[@id='mrocontent_txtPAN']");
        public By updateBtn = By.XPath("//input[@id='mrocontent_cmdUpdate']");
        public By editSSNNumber = By.XPath("//input[@id='mrocontent_imgBtnEditPatientSSN']");

        public bool VerifyUpdatePatientInformationPage()
        {
            bool isDisplayed = false;
            try
            {
                IWebElement panTxt = Driver.FindElementBy(updatePatientInfoHeader);
                if (panTxt.Displayed == true)
                {
                    isDisplayed = true;                    
                }
                else
                {
                    isDisplayed = false;

                }
                return isDisplayed;
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to verify update patient information page: {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }

        public int GetPanNumberOnUpdatePatientInfo()
        {
            try
            {
                string panNumberTxt = Driver.FindElementBy(panOnUpdateInfoPage).GetAttribute("value").ToString();
                int panNumber = Convert.ToInt32(panNumberTxt);
                return panNumber;
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to get pan number on update patient info page : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }

        public void UpdatePanNumber()
        {
            try
            {
                string _updatePanNumber = IniHelper.ReadConfig("ROIAdminBOERequesterPANRequiredTest", "UpdatePanNumber");                           
                Driver.FindElementBy(panOnUpdateInfoPage).Clear();                
                Driver.SendKeys(panOnUpdateInfoPage, _updatePanNumber);
                Driver.Click(updateBtn);
                Driver.SleepTheThread(5);
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to get pan number on update patient info page : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }

        }

        public void EditSSNNumber()
        {
            try
            {
                Driver.Click(editSSNNumber);
                Driver.SwitchTo().Alert().SendKeys("001-10-1336");
                //logger.Log(Status.Info, $"Entered request id ({})");
                Driver.SwitchTo().Alert().Accept();
                Driver.Wait(TimeSpan.FromSeconds(2));
                Driver.Click(updateBtn);
                Driver.SleepTheThread(5);
            }
            catch (Exception ex)
            {


                throw new Exception($"Failed to update ssn number on update patient info page : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        
        }
    }
}
