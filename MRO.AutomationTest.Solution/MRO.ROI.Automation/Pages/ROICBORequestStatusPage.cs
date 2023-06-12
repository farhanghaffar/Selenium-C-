using AventStack.ExtentReports;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MRO.ROI.Automation.Common.Navigation;
using MRO.ROI.Automation.Pages;
using MRO.ROI.Automation.Pages.Common;
using MRO.ROI.Automation.Selenium;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.UI;
using System;
using System.IO;
using System.Reflection;

namespace MRO.ROI.Automation.Pages
{
    public class ROICBORequestStatusPage
    {
        public RemoteWebDriver Driver;
        public ExtentTest logger;
        public TestContext Context;
        public ROICBORequestStatusPage(RemoteWebDriver driver, ExtentTest _loger, TestContext _context)
        {
            Driver = driver;
            logger = _loger;
            Context = _context;
        }
       

        public bool VerifyUseFlagsOnRSS()
        {
            try
            {
                bool isDisplayed = false;
                string recreationalVehicle = "//label[contains(text(),'Recreational Vehicle')]";
                string sandyChkbox = "//label[contains(text(),'Sandy')]";
                string trackOnlyChkbox = "//label[contains(text(),'Track Only')]";
                WebElementHelper helper = new WebElementHelper(Driver, logger, Context);
                bool isReceationalChkbox = helper.IsElementPresent(recreationalVehicle);
                bool isSandyChkBoxPresent = helper.IsElementPresent(sandyChkbox);
                bool isTrackOnlyChkbox = helper.IsElementPresent(trackOnlyChkbox);
                if(isReceationalChkbox==false&&isSandyChkBoxPresent==false&&isTrackOnlyChkbox==false)
                {
                    isDisplayed = false;
                }
                if (isReceationalChkbox == true && isSandyChkBoxPresent == true && isTrackOnlyChkbox == true)
                {
                    isDisplayed = true;
                }
                return isDisplayed;


            }
            catch (Exception)
            {

                throw;
            }
        }

        public void SelectRecentRequest()
        {
            try
            {
                Actions action = new Actions(Driver);
                Driver.Wait(TimeSpan.FromSeconds(2));
                IWebElement recentReq = Driver.FindElementBy(By.XPath("//td[contains(text(),'Recent Requests')]"));
                action.MoveToElement(recentReq).Perform();
                Driver.Wait(TimeSpan.FromSeconds(1));
                
                IWebElement firstReecentRequest = Driver.FindElementBy(By.XPath("(//table[starts-with(@id,'mroheader_')])[2]//tr"));
                action.MoveToElement(firstReecentRequest).Click().Build().Perform();
                Driver.Wait(TimeSpan.FromSeconds(1));

            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool VerifyDocumentTypesOnRss()
        {
            try
            {
                bool isDisplayed = false;
                string entireChart = "//label[contains(text(),'TOT (Entire Chart)')]";
                string dcSumChkbox = "//label[contains(text(),'D/C Sum')]";
                string ekgChkbox = "//label[contains(text(),'EKG/EEG')]";
                string pertChkbox = "//label[contains(text(),'PERT (Pertinent / Abstract)')]";
                string labsChkbox = "//label[contains(text(),'Labs')]";
                string nurseChkbox = "//input[@id='mrocontent_lstDocTypes_7']";
                string hpChkbox = "//label[contains(text(),'H & P')]";
                string radiologyChkbox = "//label[contains(text(),'Radiology')]";
                string dimuzioChkbox = "//label[contains(text(),'DiMuzio')]";
                WebElementHelper helper = new WebElementHelper(Driver, logger, Context);
                bool isentireChartChkbox = helper.IsElementPresent(entireChart);
                bool isdcSumChkbox = helper.IsElementPresent(dcSumChkbox);
                bool isekgChkbox = helper.IsElementPresent(ekgChkbox);
                bool ispertChkbox = helper.IsElementPresent(pertChkbox);
                bool islabsChkbox = helper.IsElementPresent(labsChkbox);
                bool isnurseChkbox = helper.IsElementPresent(nurseChkbox);
                bool ishpChkbox = helper.IsElementPresent(hpChkbox);
                bool isradiologyChkbox = helper.IsElementPresent(radiologyChkbox);
                bool isdimuzioChkbox = helper.IsElementPresent(dimuzioChkbox);
                if (isentireChartChkbox == false && isdcSumChkbox == false && isekgChkbox == false && ispertChkbox == false && islabsChkbox == false && isnurseChkbox == false && ishpChkbox == false && isradiologyChkbox == false && isdimuzioChkbox == false)
                {
                    isDisplayed = false;
                }
                if (isentireChartChkbox == true && isdcSumChkbox == true && isekgChkbox == true && ispertChkbox == true && islabsChkbox == true && isnurseChkbox == true && ishpChkbox == true && isradiologyChkbox == true && isdimuzioChkbox == true)
                {
                    isDisplayed = true;
                }
                return isDisplayed;


            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to verify document types with details as Message : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }
    }
}
