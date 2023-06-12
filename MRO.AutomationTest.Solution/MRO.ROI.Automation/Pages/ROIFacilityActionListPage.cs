using AventStack.ExtentReports;
using DataDrivenProject;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MRO.ROI.Automation.Selenium;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.UI;
using System;
using static MRO.ROI.Automation.Utility.IniFile;

namespace MRO.ROI.Automation.Pages
{
    public class ROIFacilityActionListPage
    {
        public RemoteWebDriver Driver;
        public ExtentTest logger;
        public TestContext Context;
        public ROIFacilityActionListPage(RemoteWebDriver driver, ExtentTest _loger, TestContext _context)
        {
            Driver = driver;
            logger = _loger;
            Context = _context;
        }

        public By fromdate = By.XPath("//input[@id='mrocontent_txtDateA']");
        public By todate = By.XPath("//input[@id='mrocontent_txtDateZ']");
        public By createBtn = By.XPath("//input[@id='mrocontent_cmdCreate']");
        public By noRecordToDisplayElement = By.XPath("//*[@class='rgMasterTable rgClipCells']/tbody/tr/td/div");
        public By requestIdElement = By.XPath("//table[@class='rgMasterTable rgClipCells']//tbody//tr//td[2]");
        //public By requestIdElement = By.XPath("//table[@class='rgMasterTable rgClipCells']//tbody//tr//td[2]");
        public By excelOldBtn = By.Id("mrocontent_MROReportGridBanner_cmdExportReport");
        public By excelNewBtn = By.Id("mrocontent_MROReportGridBanner_lnkExport");
        public By rowCount = By.Id("mrocontent_MROReportGridBanner_lblRows");

        public void CreateReport()
        {
            try
            {
                string todayDate = String.Format("{0:M/dd/yyyy}", DateTime.Now).Replace("-", "/");
                Driver.ClearText(fromdate);
                Driver.SendKeys(fromdate, todayDate);
                Driver.ClearText(todate);
                Driver.SendKeys(todate, todayDate);
                Driver.Click(createBtn);
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to create report with message as : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }
        public bool VerifyRequestId(string requestId)
        {
            bool isDisplayed = false;
            try
            {
                string noRecords = IniHelper.ReadConfig("ROIAddLoggingUserColumnToFacilityActionListTest", "NoRecordsText");
                //string text = Driver.GetText(noRecordToDisplayElement);
                string isNoRecordsExists = "//*[@class='rgMasterTable rgClipCells']/tbody/tr/td/div";
                string isRecordsExists = "//table[@class='rgMasterTable rgClipCells']//tbody//tr[1]//td[2]";
                WebElementHelper helper = new WebElementHelper(Driver, logger, Context);
                bool isNoRecordsPresent = helper.IsElementPresent(isNoRecordsExists);
                bool isRecordsPresent = helper.IsElementPresent(isRecordsExists);
                if (isNoRecordsPresent == true)
                {
                    isDisplayed = false;
                }
                if(isRecordsPresent==true)
                {                    
                        var requestElements = Driver.FindElementsBy(requestIdElement);
                        foreach (var requestElement in requestElements)
                        {
                            if (requestElement.Text.Equals(requestId))
                            {
                                isDisplayed = true;
                                break;
                            }
                        }
                        
                    
                }


            }
            
            catch (Exception ex)
            {

                throw new Exception($"Failed to verify request id in action list page: {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
            return isDisplayed;
        }

        public void ClickExcelReports()
        {
            try
            {
                Driver.Click(excelOldBtn);
                Driver.Click(excelNewBtn);
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to click on export to excel buttons with message as : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }


        public bool ValidateExcel(string downloadFolder, int tableTotalRequests)
        {
            string excelFileName = string.Empty;
            bool isSearchResultsValid = false;
            try
            {
                Driver.SleepTheThread(10);
                ExcelReaderFile excelReaderFile = new ExcelReaderFile();
                excelReaderFile.ConvertXLS_XLSX(downloadFolder + "roifac_ActionList3Report.xls");
                ExcelReaderFile _excelReaderFile = new ExcelReaderFile(downloadFolder + "roifac_ActionList3Report.xlsx");
                int excelRowCount = _excelReaderFile.getRowCount("roifac_ActionList3Report");
                int excelTotalRequests = excelRowCount - 1;

                Assert.AreEqual(tableTotalRequests, excelTotalRequests, "Failed to validate search results from excel with search requests from UI");
                ExcelReaderFile.DeleteExistingFiles(downloadFolder, "xlsx", "roifac_ActionList3Report.xlsx");
                ExcelReaderFile.DeleteExistingFiles(downloadFolder, "xls", "roifac_ActionList3Report.xls");

                if (tableTotalRequests == excelTotalRequests) { isSearchResultsValid = true; }
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to validate excel data with UI data : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
            return isSearchResultsValid;
        }

        public int GetSearchResultsCountFromTable()
        {
            int requestCount = 0;
            try
            {
                string sRequestsCount = Driver.GetText(rowCount);
                requestCount = Convert.ToInt32(sRequestsCount);
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to click on export to excel buttons with message as : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
            return requestCount;
        }

        public void ClearDatesAndCreateReport()
        {
            try
            {
                Driver.ClearText(fromdate);
                Driver.ClearText(todate);
                Driver.Click(createBtn);
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to create report with date paramters as null with the message as : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }

        public void CreateReportWithoutAnyParameters()
        {
            try
            {
                Driver.Click(createBtn);
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to create report with message as : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }
    }
}

