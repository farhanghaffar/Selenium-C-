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

namespace MRO.ROI.Automation.Pages
{
    public class ROISystemInfoPage
    {
        public RemoteWebDriver Driver;
        public ExtentTest logger;
        public TestContext Context;
        public ROISystemInfoPage(RemoteWebDriver driver, ExtentTest _loger, TestContext _context)
        {
            Driver = driver;
            logger = _loger;
            Context = _context;
        }

        public By opswatEnableChkbox = By.Id("mrocontent_bOPSWATEnabled");
        public By updateSystemInfoBtn = By.Id("mrocontent_cmdUpdate");
        public By marqueeLink = By.Id("mrocontent_lnkWorkSummaryMarquee");//
        public By marqueeWidth = By.Id("mrocontent_txtWidth");
        public By marqueeText = By.XPath("//textarea[@id='mrocontent_txtMsg']");//
        public By saveButton = By.Id("mrocontent_cmdSave");
        public By clearButton = By.Id("mrocontent_cmdClear");
        public void EnableOPSWATCheckBox()
        {
            try
            {
                Driver.ScrollToElement(opswatEnableChkbox);
                if (Driver.FindElementBy(opswatEnableChkbox).Selected == false)
                 {
                        Driver.Click(opswatEnableChkbox);
                       
                 }              
                Driver.Click(updateSystemInfoBtn);
                Driver.SwitchTo().Alert().Accept();
                Driver.Wait(TimeSpan.FromSeconds(2));
                Driver.ScrollToElement(updateSystemInfoBtn);
                    
            }
            catch (Exception ex)
            {

                throw new Exception($"Failed  click OPSWAT check box with details as Message : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
           
        }

        public void DisableOPSWATCheckBox()
        {
            try
            {
                if (Driver.FindElementBy(opswatEnableChkbox).Selected == true)
                {
                    Driver.Click(opswatEnableChkbox);
                }
                Driver.Click(updateSystemInfoBtn);
                Driver.SwitchTo().Alert().Accept();
                Driver.Wait(TimeSpan.FromSeconds(2));
                Driver.ScrollToElement(updateSystemInfoBtn);
                
            }
            catch (Exception ex)
            {

                throw new Exception($"Failed  to un check OPSWAT check box with details as Message : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }

        }

        public void ClickWorkSummaryMarquee()
        {
            try
            {
                Driver.Click(marqueeLink);

            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to click on Work Summary Marquee Link with details as Message : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }

        }

        public void EditWorkSummaryMarqueeInformation()
        {
            try
            {
                Driver.SendKeys(marqueeWidth, "1000");
                Driver.SendKeys(marqueeText, "Testing Marquee");
                Driver.SleepTheThread(2);
                Driver.Click(saveButton);
                Driver.SwitchTo().Alert().Accept();
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to update marquee data with details as Message : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }

        }

        public string GetMarqueeText()
        {
            string marqueeValue = string.Empty;
            try
            {
                marqueeValue = Driver.FindElementBy(By.TagName("marquee")).Text;
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to update marquee data with details as Message : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }          
            return marqueeValue;
        }

        public void ClearMarquee()
        {
            try
            {
                Driver.Click(clearButton);
                Driver.SwitchTo().Alert().Accept();

            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to click on clear button with details as Message : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }

        }


    }
}