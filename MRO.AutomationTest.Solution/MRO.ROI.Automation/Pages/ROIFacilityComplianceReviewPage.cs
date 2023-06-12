using AventStack.ExtentReports;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MRO.ROI.Automation.Common.Navigation;
using MRO.ROI.Automation.Pages;
using MRO.ROI.Automation.Selenium;
using MRO.ROI.Automation.Utility;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.UI;
using System;
using System.IO;
using System.Reflection;
using static MRO.ROI.Automation.Utility.IniFile;

namespace MRO.ROI.Automation.Pages
{
    public class ROIFacilityComplianceReviewPage
    {
        public RemoteWebDriver Driver;
        public ExtentTest logger;
        public TestContext Context;
        public CSVReader csvReader;
        public ROIFacilityComplianceReviewPage(RemoteWebDriver driver, ExtentTest _loger, TestContext _context)
        {
            Driver = driver;
            logger = _loger;
            Context = _context;
        }
        public string csvFileName = "InternalPortalWithComplianceHold.csv";
        public By deliveredChkbox = By.Id("mrocontent_cbDelivered");
        public By fromDate = By.Id("mrocontent_txtDateA");
        public By toDate = By.Id("mrocontent_txtDateZ");
        public By closedChkbox = By.Id("mrocontent_cbClosed");
        public By cancelChkbox = By.Id("mrocontent_cbCancelled");
        public By createReportBtn = By.Id("mrocontent_cmdCreate");
        public By locationdrp = By.Id("mrocontent_lstLoc");
        public By locationVal = By.XPath("//option[contains(text(),'Boston Proper')]");
        public By requestId = By.XPath("//*[@id='mrocontent_dgReport']/tbody/tr[2]/td[1]");
        public By statusVal = By.XPath("//*[@id='mrocontent_dgReport']/tbody/tr[2]/td[11]");
        public By searchPatient = By.XPath("//*[@id='mrocontent_tblReport']/tbody/tr/td");
        public By releseRequestButton = By.XPath("//a[contains(text(),'Release Request')]");
        public By requestIdSortElement = By.Id("mrocontent_dgReport_tblReport_imgSortSwitch");
        public string noRecordsElement = "//*[@id='mrocontent_tblReport']/tbody/tr/td";
        public string requestVal = "//tr[@class='TableBody']//td[1]";
        public By requestIdElement = By.XPath("//tr[@class='TableBody']//td[1]");

        /// <summary>
        /// Go to complianceReview page
        /// </summary>
        public void CreateReportForComplianceReview()
        {
            try
            {
                if (Driver.FindElementBy(deliveredChkbox).Displayed == true)
                {
                    logger.Log(Status.Pass, "Verified Delivered check box appears on compliance review report");
                }
                string todayDate = String.Format("{0:M/dd/yyyy}", DateTime.Now).Replace("-", "/");
                Driver.ClearText(fromDate);
                Driver.Click(fromDate);
                Driver.SendKeys(fromDate, todayDate);
                Driver.ClearText(toDate);
                Driver.Click(toDate);
                Driver.SendKeys(toDate, todayDate);
                string locationVal = IniHelper.ReadConfig("InternalPortalWithComplianceHoldTest", "Location");                
                SelectElement oSelect = new SelectElement(Driver.FindElement(locationdrp));
                oSelect.SelectByText(locationVal);
                if (Driver.FindElementBy(closedChkbox).Selected == true)
                {
                    Driver.Click(closedChkbox);
                }
                if (Driver.FindElementBy(cancelChkbox).Selected == true)
                {
                    Driver.Click(cancelChkbox);
                }
                if (Driver.FindElementBy(deliveredChkbox).Selected == true)
                {
                    Driver.Click(deliveredChkbox);
                }
                Driver.Click(createReportBtn);

            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to create report with deatails as Message : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }
        /// <summary>
        /// Get RequestId
        /// </summary>
        public string getRequestIdFromReport()
        {
            try
            {
                string reqId = Driver.GetText(requestId);
                return reqId;
            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to requestid with details as : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }
        /// <summary>
        ///To Verify Request status
        /// </summary>
        public string VerifyStatus()
        {
            try
            {
                Driver.Click(requestIdSortElement);
                string status = Driver.GetText(statusVal);
                Driver.Click(requestIdSortElement);
                return status;
                
            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to to verify request status with deatails as Message : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }

        }

        /// <summary>
        ///To Verify Patient data
        /// </summary>

        public bool VerifyPatient(string requestId)
        {
            bool isValidated = false;
            try
            {
                WebElementHelper helper = new WebElementHelper(Driver, logger, Context);
                if(helper.IsElementPresent(requestVal))
                {
                    var requestElements = Driver.FindElementsBy(requestIdElement);
                    foreach (var requestElement in requestElements)
                    {
                        if (requestElement.Text.Equals(requestId))
                        {
                            isValidated = true;
                            break;
                        }
                    }
                    //Assert.IsFalse(isValidated, "Failed to validate request id");
                   // return isValidated;
                }
                if(helper.IsElementPresent(noRecordsElement))
                {
                    //string result = Driver.GetText(searchPatient);                    
                    isValidated = false;
                     
                }

                return isValidated;
            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to check request id aganist request table {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
            
        

        // string result = Driver.GetText(searchPatient);
        // return result;

        }   
           
        
        /// <summary>
        ///To click on DeliveredCheckbox
        /// </summary>
        public string ClickOnDeliveredCheckbox()
        {
            try
            {
                Driver.Click(deliveredChkbox);
                Driver.Click(createReportBtn);
                Driver.Click(requestIdSortElement);
                string result = Driver.GetText(statusVal);
                Driver.Click(requestIdSortElement);
                return result;               
                
            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to verify patient status with deatails as Message : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }

        /// <summary>
        /// Get RequestId
        /// </summary>
        public string GetRequestIdFromReport()
        {
            try
            {
                Driver.Click(requestIdSortElement);
                string reqId = Driver.GetText(requestId);
                Driver.Click(requestIdSortElement);
                return reqId;
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to requestid with details as : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }
    }  
}
