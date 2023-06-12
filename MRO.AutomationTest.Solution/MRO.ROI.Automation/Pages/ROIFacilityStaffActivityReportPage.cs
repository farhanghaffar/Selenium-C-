using AventStack.ExtentReports;
using MRO.ROI.Automation.Selenium;
using System;
using OpenQA.Selenium;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium.Remote;
using System.Collections.ObjectModel;
using OpenQA.Selenium.Interactions;
using System.Collections.Generic;
using DataDrivenProject;
using System.Net;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;
using OpenQA.Selenium.Support.UI;
using MRO.ROI.Automation.Common;

namespace MRO.ROI.Automation.Pages
{
    public class ROIFacilityStaffActivityReportPage
    {
        public RemoteWebDriver Driver;
        public ExtentTest logger;
        public TestContext Context;
        public ROIFacilityStaffActivityReportPage(RemoteWebDriver driver, ExtentTest _loger, TestContext _context)
        {
            Driver = driver;
            logger = _loger;
            Context = _context;
        }
        public By lnkToday = By.XPath("//div[@class='ranges']/ul/li[1]");
        public By staffActivityReportFrame = By.XPath("//iframe[starts-with(@id,'rdFrame')]");
        public By selectDatePicker = By.XPath("(//span[@id='daterange'])[1]");
        public By btnApply = By.XPath("//button[text()='Apply']");
        public By btnCreateReport = By.XPath("//input[@id='btn_submit']");
        public By lastButtonIcon = By.XPath("//img[@alt='Last page']");
        public By thisWeekPicker = By.XPath("//li[contains(text(),'This Week')]");
        public By includeClosedRequests = By.XPath("//input[@id='bIncludeClosed']");
        public By totalLink = By.XPath("//a[@id='show_div_ImagingDelAddedTotal_drilldown']//span");
        public By numOfRequestes = By.Id("lblImagingDelAdded_Row1");
        public By numOfRows = By.XPath("//table[@id='dtStaffActivityDetailReleased']//tbody//tr");
        public By lbl_Excel_icon = By.XPath("//i[@id='lbl_Excel_icon']");
        public By lbl_PDF_icon = By.XPath("//i[@id='lbl_PDF_icon']");
        public By rdFrame = By.XPath("//iframe[starts-with(@id,'rdFrame')]");
        public By childFrame = By.XPath("//iframe[starts-with(@id,'sr_Total')]");
        public By selectDate = By.XPath("//div[@class='calendar left']//table[@class='table-condensed']//tbody/tr[1]/td[2]");
        public By selectMonthDrp = By.XPath("//div[@class ='calendar left']//select[@class='monthselect']");
        public By selectYearDrp = By.XPath("//div[@class ='calendar left']//select[@class='yearselect']");
        public By lnkCustomize = By.XPath("//a[@id='show_div_user_inputs']/span");
        public By deletedUserChkBox = By.Id("bIncludeDeletedUser");
        public By username = By.XPath("//td[@id='colsName_Row30']");
        public By requestNum=By.XPath("//a[@id='show_mirImagingDelAdded_Row1']//span");
        public By requestCount = By.XPath("//table[@id='dtStaffActivityDetailReleased']//tr");
        public By getUserMroRowDataInStaffActivityReport = By.XPath("(//a[@id='show_mirUserTotal_Row1'])[1]/ancestor::tr[2]//child::td[@id!='colsName_Row1']");
        public By customizebutton = By.XPath("//span[contains(text(), 'Customize')]/parent::a");
        public By firstTotalLinkUnderLoggedSection = By.XPath("//tr[@id='sumAll']//child::td[2]//a");
        public By totalFrame = By.XPath("//iframe[@id='sr_Total']");
        public By loggingUser = By.XPath("//th[@id='colsLoggingUser-TH']//a[contains(text(), 'Logging User')]");
        public By firstTotalLinkUnderReleaseSection = By.XPath("//tr[@id='sumAll']//child::td[@id='colMRORel']//a");
        public By releasingUser = By.XPath("//th[@id='colsReleasingUser-TH']//a[contains(text(), 'Releasing User')]");
        public By deletedUsers = By.XPath("//td[contains(@id,'colsName_Row')]//child::td/a//span[contains(text(), '*)')]");
        /// <summary>
        /// Create new Staff Activity Report
        /// </summary>
        public void ApplyFiltersAndCreateReport()
        {
            try
            {
                IWebElement frame = Driver.FindElementBy(staffActivityReportFrame);
                Driver.SwitchTo().Frame(frame);
                IWebElement datePicker = Driver.FindElementBy(selectDatePicker);
                datePicker.Click();
                Driver.Click(lnkToday);
                Driver.SleepTheThread(1);
                //Driver.Click(btnApply);
                Driver.Click(btnCreateReport);
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to create staff activity report : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }

        }

        public bool ValidateDetailView(string downloadFolderPath)
        {
            bool isRequestsValidated = false;
            try
            {
                int totalPages = 0;
                int eTotalPages = 0;
                int tableRowCount = 0;
                int detailViewRowsCount = 0;
                int eRequestCount = 0;
                List<IWebElement> tableData = Driver.FindElementsBy(By.XPath("//table[@id='dtStaffActivitySummary']//tr[not(contains(@style,'display:none;'))]"));
                int totalrowsCount = tableData.Count - 1;
                for (int z = 0; z < tableData.Count; z++)
                {
                    z = totalrowsCount;
                    ReadOnlyCollection<IWebElement> cells = tableData[z].FindElements(By.TagName("td"));
                    for (int y = 0; y < cells.Count; y++)
                    {
                        if (y == 1)
                        {

                            tableRowCount = GetTotalRequestCountFromTableByRow(z);
                            OpenDetailViewForTotalRequests();
                            totalPages = GetTotalPagesCount();
                            detailViewRowsCount = GetFinalCount(totalPages);
                            //eTotalPages = GetTotalEPagesCount();
                            //eRequestCount = GetFinalERequestsCount(eTotalPages);
                            if (tableRowCount == detailViewRowsCount)
                            {
                                isRequestsValidated = true;
                                logger.Log(Status.Info, $"Verified that detail view of total mro delivery requests({detailViewRowsCount}) generated the same number of requests({tableRowCount}) for each number hyperlink");
                                ValidateExcel(downloadFolderPath, tableRowCount);
                                ClickLeftArrowButtonForMroDeliveryByRow(z);
                            }
                            else
                            {
                                logger.Log(Status.Fail, $"Verified that detail view of total mro delivery requests({detailViewRowsCount}) not generated the same number of requests({tableRowCount}) for each number hyperlink");
                            }

                        }
                        else if (y == 2)
                        {


                        }

                    }
                    break;
                }

            }

            catch (Exception ex)
            {
                throw new Exception($"Failed to validate detail view of requests: {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
            return isRequestsValidated;
        }

        public void ValidateExcel(string downloadFolder, int tableTotalRequests)
        {
            string excelFileName = string.Empty;
            try
            {
                ClickOnExportToExcel();
                Driver.SleepTheThread(10);
                ExcelReaderFile excelReaderFile = new ExcelReaderFile();
                //excelReaderFile.ConvertXLS_XLSX(downloadFolder + "Staff Activity Logged Requests.xls");
                ExcelReaderFile _childexcelReaderFile = new ExcelReaderFile(downloadFolder + "Staff Activity Logged Requests.xlsx");
                int lastRowNumber = _childexcelReaderFile.getRowCount("Sheet1");
                int excelTotalRequests = lastRowNumber - 10;
                Assert.AreEqual(tableTotalRequests, excelTotalRequests, "Failed to validate total requests column from excel with total requests from UI");
                ExcelReaderFile.DeleteExistingFiles(downloadFolder, "xlsx", excelFileName);
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to validate excel data with UI data : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }



        //public int GetTotalRequestCountFromTableByRow(int rowNum)
        //{
        //    string sTotalRequestsXpath = $"//td[@id ='colMRODelivery_Row{rowNum}']";           
        //    string sRequestsCount = Driver.GetText(By.XPath(sTotalRequestsXpath));
        //    int requestCount = Convert.ToInt32(sRequestsCount);
        //    return requestCount;
        //}

        public int GetTotalRequestCountFromTableByRow(int rowNum)
        {
            string sTotalRequestsXpath = "//a[@id='show_div_LoggedMROTotal_drilldown']";
            string sRequestsCount = Driver.GetText(By.XPath(sTotalRequestsXpath));
            int requestCount = Convert.ToInt32(sRequestsCount);
            return requestCount;
        }

        //public void OpenDetailViewForTotalRequests(int rowNum)
        //{
        //    string requestsXpath = $"//a[@id='show_mirMRODelivery_Row{rowNum}']";
        //    Driver.DirectClick(By.XPath(requestsXpath));
        //    string sFrameXpath = $"//iframe[@id='sr_MRODelivery_Row{rowNum}']";
        //    IWebElement frame = Driver.FindElementBy(By.XPath(sFrameXpath));
        //    Driver.SwitchTo().Frame(frame);
        //}

        public void OpenDetailViewForTotalRequests()
        {
            string requestsXpath = "//a[@id='show_div_LoggedMROTotal_drilldown']";
            Driver.DirectClick(By.XPath(requestsXpath));
            string sFrameXpath = "//iframe[@id='sr_Total']";
            IWebElement frame = Driver.FindElementBy(By.XPath(sFrameXpath));
            Driver.SwitchTo().Frame(frame);
        }



        public int GetFinalCount(int pages)
        {
            int countByNumberOfPages = 0;
            int totalRequestsCount = 0;
            int lastPageRowCount = 0;
            int iPages = 0;
            if (pages > 1)
            {
                iPages = pages - 1;
                countByNumberOfPages = iPages * 10;
            }
            ClickOnLastPage();
            Driver.SleepTheThread(8);
            IWebElement table = Driver.FindElement(By.XPath("//table[@id='dtStaffActivityDetailLogged']"));
            ReadOnlyCollection<IWebElement> allRows = table.FindElements(By.TagName("tr"));
            lastPageRowCount = allRows.Count - 1;
            totalRequestsCount = lastPageRowCount + countByNumberOfPages;
            return totalRequestsCount;
        }
        public void ClickOnLastPage()
        {
            try
            {
                Driver.DirectClick(lastButtonIcon);
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to click on last page button : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }


        public int GetTotalPagesCount()
        {
            string tRequestsXpath = "//span[@id='dtStaffActivityDetailLogged-PageOfPages']";
            string sRequestsCount = Driver.GetText(By.XPath(tRequestsXpath));
            int requestCount = Convert.ToInt32(sRequestsCount);
            return requestCount;
        }

        public void ClickLeftArrowButtonForMroDeliveryByRow(int rowNum)
        {
            try
            {
                Driver.SwitchTo().DefaultContent();
                IWebElement frame = Driver.FindElementBy(By.XPath("//iframe[starts-with(@id,'rdFrame')]"));
                Driver.SwitchTo().Frame(frame);
                string leftArrowButton = "//a[@id='hide_div_LoggedMROTotal_drilldown']";
                IWebElement arrowButton = Driver.FindElementBy(By.XPath(leftArrowButton));
                arrowButton.Click();
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to click left arrow button : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }

        public void CreateStaffActivityReport()
        {
            try
            {
                IWebElement datePicker = Driver.FindElementBy(By.XPath("//span[@id='daterange']"));
                datePicker.Click();
                Driver.SleepTheThread(1);
                Driver.Click(thisWeekPicker);
                Driver.Wait(TimeSpan.FromSeconds(5));
                //Driver.Click(btnApply);
                Driver.Wait(TimeSpan.FromSeconds(5));
                if (Driver.FindElementBy(includeClosedRequests).Selected == false)
                {
                    Driver.Click(includeClosedRequests);
                }

                Driver.Click(btnCreateReport);
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to create staff activity report : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }

        }

        public bool VerifyImagingDeliveryIsVisible()
        {
            try
            {
                WebElementHelper helper = new WebElementHelper(Driver, logger, Context);
                string IsImagingDelivery = "//a[contains(text(),'Imaging Delivery Added')]";
                bool isDisplayed = Driver.FindElementBy(By.XPath(IsImagingDelivery)).Displayed;
                return isDisplayed;
            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to create staff activity report : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }

        public bool VerifyReport()
        {
            try
            {
                Driver.Click(totalLink);
                string report = "//a[@id='show_div_ImagingDelAddedTotal_drilldown']//span";
                WebElementHelper helper = new WebElementHelper(Driver, logger, Context);
                bool isDisplayed = helper.IsElementPresent(report);
                Driver.Wait(TimeSpan.FromSeconds(2));
                Driver.Navigate().Refresh();                
                Driver.Wait(TimeSpan.FromSeconds(2));
                return isDisplayed;

            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to validate report : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }

        public string ClickOnTotalNumberHyperLink()
        {
            try
            {

                string num = Driver.GetText(requestNum);
                Driver.Click(numOfRequestes);
                Driver.Wait(TimeSpan.FromSeconds(4));
                int rows = Driver.FindElementsBy(requestCount).Count;               
                return num;

            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to click total number hyperlink with details as : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }
      

        public void ClickOnExportToExcel()
        {
            try
            {
               // Driver.SwitchTo().DefaultContent();
               // IWebElement frame = Driver.FindElementBy(childFrame);
               // Driver.SwitchTo().Frame(frame);
                Driver.SleepTheThread(2);
               // IWebElement icon = Driver.FindElementBy(lbl_Excel_icon);
               // icon.Click();
                List<IWebElement> iElements = Driver.FindElementsBy(lbl_Excel_icon);
                for (int i = 0; i < iElements.Count; i++)
                {
                    if (i == 0)
                    {
                        iElements[i].Click();
                    }
                }
                Driver.SleepTheThread(2);
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to click on export to excel icon : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }
        public string ClickOnExportToPdfAndGetReportName()
        {
            try
            {
                IWebElement icon = Driver.FindElementBy(lbl_PDF_icon);
                icon.Click();
                Driver.SleepTheThread(5);
                string tab1 = Driver.WindowHandles[0];
                string tab2 = Driver.WindowHandles[1];
                Driver.SwitchTo().Window(tab2);
                string pdfName = Driver.Url;
                Driver.SwitchTo().Window(tab2).Close();
                Driver.SleepTheThread(2);
                Driver.SwitchTo().Window(tab1);
                return pdfName;
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to click on export to pdf icon : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }

        public string[] DownloadPDFAndGetReportData(string folderName)
        {
            string fileURLName = string.Empty;
            WebClient webClient = new WebClient();
            string filePath = folderName;
            fileURLName = ClickOnExportToPdfAndGetReportName();
            string completePath = filePath + "Staff Activity.pdf";
            webClient.DownloadFile(fileURLName, completePath);
            string[] sbData = GetDataFromPDFFile(completePath);
            int sCount = sbData.Length;
            string sData = "";
            foreach (string s in sbData)
            {
                sData += s;
            }
            //[] sPdfData = sData.Split(' ');
            return sbData;
        }
        public string[] GetDataFromPDFFile(string fileName)
        {
            PdfReader reader = new PdfReader(fileName);
            int intPageNum = reader.NumberOfPages;
            string[] sWords = new string[] { };
            string text = "";

            for (int i = 1; i <= intPageNum; i++)
            {
                text = PdfTextExtractor.GetTextFromPage(reader, i, new LocationTextExtractionStrategy());
                sWords = text.Split('\n');
            }
            reader.Close();
            reader.Dispose();
            return sWords;
        }

        public void CreateReportForPreviousYear()
        {
            try
            {
               
                IWebElement datePicker = Driver.FindElementBy(selectDatePicker);
                datePicker.Click();
                Driver.Click(lnkToday);
                datePicker.Click();
                SelectElement oSelect2 = new SelectElement(Driver.FindElementBy(selectYearDrp));
                oSelect2.SelectByText("2018");
                Driver.Click(selectDate);
                IWebElement applyButton = Driver.FindElementBy(btnApply);
                applyButton.Click();

                if (Driver.FindElementBy(deletedUserChkBox).Selected == false)
                {
                    Driver.Click(deletedUserChkBox);
                }

                Driver.Click(btnCreateReport);

            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to create report with details as : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }

        public string GetDeletedUser()
        {
            try
            {
                string userName = Driver.GetText(username);
                return userName;

            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to get deleted user with details as : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }

        public List<string> GetUserMroRowDataInStaffActivityReport()
        {
            Driver.WaitInSeconds(2);
            Driver.WaitUntilDOMLoaded();
            Driver.WaitForPageToLoad();
            var element = Driver.FindElementsBy(getUserMroRowDataInStaffActivityReport);
            List<string> getData = new List<string>();
            foreach(var row in element)
            {
                getData.Add(row.Text);
            }
            return getData;
        }

        public void clickCustomizebutton()
        {
            Driver.Click(customizebutton);
        }

        public void ClickFirstTotalLinkUnderLoggedSection()
        {
            Driver.Click(firstTotalLinkUnderLoggedSection);
        }

        public void SwitchToTotalFrame()
        {
            Iframe frame = new Iframe(Driver, logger, Context);
            frame.SwitchToFrameByLocator(totalFrame);
        }

        public bool IsLoggingUserShowing()
        {
           return Driver.isElementDisplayed(loggingUser);
        }

        public void ClickFirstTotalLinkUnderReleaseSection()
        {
            Driver.Click(firstTotalLinkUnderReleaseSection);
        }
        
        public bool IsReleasingUserShowing()
        {
            return Driver.isElementDisplayed(releasingUser);
        }

        public bool IsDeletedUsersShowing()
        {
            return Driver.isElementDisplayed(deletedUsers);
        }
    }
}
