using AventStack.ExtentReports;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MRO.ROI.Automation.Selenium;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MRO.ROI.Automation.Pages
{
   public class ROIadminMonthlyStatementReportDetailsPage
    {
        public RemoteWebDriver Driver;
        public ExtentTest logger;
        public TestContext Context;
        public ROIadminMonthlyStatementReportDetailsPage(RemoteWebDriver driver, ExtentTest _loger, TestContext _context)
        {
            Driver = driver;
            logger = _loger;
            Context = _context;
        }

        public By RequestId = By.XPath("//table[@id='mrocontent_dgDetails']//tr[@class='TableBody']//td[1]//a");
        public By ExcelIcon = By.XPath("//img[@alt='Export to Excel']");
        public By RequestIds = By.XPath("//table[@id='mrocontent_tblDetails']//tr[@class='TableBody']//td[1]");


        public string VerifyRequestId(string requestID)
        {
            string MSRD_requestID = string.Empty;
            try
            {
                
                var requestIDs = Driver.FindElementsBy(RequestId);
                foreach (var _requestID in requestIDs)
                {
                    if(_requestID.Text == requestID)
                    {
                        MSRD_requestID = _requestID.Text;
                        break;
                    }

                    
                }
            }

            catch (Exception ex)
            {

                throw new Exception($"Failed to check if the request present or not  Message : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
            return MSRD_requestID;
        }


        public void ClickExcelIcon()
        {
            try
            {
                IWebElement excelIcon = Driver.FindElementBy(ExcelIcon);
                excelIcon.Click();
            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to click on excel icon Message : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }

        public string FirstReqIDInUI()
        {
            try
            {
                string firstRequestId = string.Empty;
                string requestIDAtIndexOne = string.Empty;
                List<IWebElement> elements = Driver.FindElementsBy(By.XPath("//table[@id='mrocontent_tblDetails']//tr[@class='TableBody']//td[1]"));
                List<string> requestIDs = new List<string>();
                if (elements.Count > 0)
                {
                    foreach (var _requestIDs in elements)
                    {
                        IWebElement elementAtFirstIndex = elements[0];
                        firstRequestId = elementAtFirstIndex.Text;
                        requestIDs.Add(firstRequestId);
                    }
                }
                requestIDAtIndexOne = requestIDs[1];
                return requestIDAtIndexOne;
            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to get request first request id : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }

        public string ThirdReqIDInUI()
        {
            try
            {
                string thirdRequestId = string.Empty;
                string requestIDAtIndexthree = string.Empty;
                List<IWebElement> elements = Driver.FindElementsBy(By.XPath("//table[@id='mrocontent_tblDetails']//tr[@class='TableBody']//td[1]"));
                List<string> requestIDs = new List<string>();
                if (elements.Count >= 3)
                {
                    foreach (var _requestIDs in elements)
                    {
                        IWebElement elementAtFirstIndex = elements[0];
                        thirdRequestId = elementAtFirstIndex.Text;
                        requestIDs.Add(thirdRequestId);
                    }
                }
                requestIDAtIndexthree = requestIDs[3];
                return requestIDAtIndexthree;
            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to get request first request id : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }
    }
}
