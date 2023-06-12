using AventStack.ExtentReports;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MRO.ROI.Automation.Selenium;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Remote;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MRO.ROI.Automation.Pages
{
  public class ROITestFacilityDocumentTypesPage
    {
        public RemoteWebDriver Driver;
        public ExtentTest logger;
        public TestContext Context;
        public ROITestFacilityDocumentTypesPage(RemoteWebDriver driver, ExtentTest _loger, TestContext _context)
        {
            Driver = driver;
            logger = _loger;
            Context = _context;
        }

        public By boeLogRequestChkbox = By.XPath("//input[@id='mrocontent_cbLogBOE']");
        public By boeRequestStatusChkbox = By.XPath("//input[@id='mrocontent_cbStatBOE']");
        public By updateBtn = By.XPath("//input[@id='mrocontent_cmdUpdate']");
        public By updateText = By.Id("mrocontent_lblUpdated");



        public bool VerifyBOECheckboxes()
        {
            try
            {

                bool isChecked = false;
                if(Driver.FindElementBy(boeRequestStatusChkbox).Selected==true)
                {
                    Driver.Click(boeRequestStatusChkbox);
                    Driver.Click(updateBtn);
                }

                if (Driver.FindElementBy(boeLogRequestChkbox).Selected == true)
                {
                    Driver.Click(boeLogRequestChkbox);
                    Driver.Click(updateBtn);
                }
                bool islogRequestChecked = Driver.FindElementBy(boeRequestStatusChkbox).Selected;
                bool isRequestStatusChecked = Driver.FindElementBy(boeRequestStatusChkbox).Selected;
                if(islogRequestChecked==false && isRequestStatusChecked==false)
                {
                     isChecked=false;
                } 
                else
                {
                    isChecked = true;
                    logger.Log(Status.Info, "BOE Check boxes are checked");
                }

                return isChecked;

            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to uncheck boe checkboxes with Exception details as: {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");

            }

        }

        public string CheckBoeLogRequest()
        {
            try
            {
                if (Driver.FindElementBy(boeLogRequestChkbox).Selected == false)
                {
                    Driver.Click(boeLogRequestChkbox);
                    Driver.Click(updateBtn);                   
                    
                }
                string message = Driver.GetText(updateText);
                return message;
            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to click log request check box with Exception details as: {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }

        public string CheckBoeRequestStatus()
        {
            try
            {
                if (Driver.FindElementBy(boeRequestStatusChkbox).Selected == false)
                {
                    Driver.Click(boeRequestStatusChkbox);
                    Driver.Click(updateBtn);
                }
                string message = Driver.GetText(updateText);
                return message;
            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to click log request check box with Exception details as: {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }


    }
}
