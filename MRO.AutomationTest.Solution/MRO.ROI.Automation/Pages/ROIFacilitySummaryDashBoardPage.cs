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
using System.IO;
using System.Linq;
using OpenQA.Selenium.Support.UI;

namespace MRO.ROI.Automation.Pages
{
    public class ROIFacilitySummaryDashBoardPage
    {
        public RemoteWebDriver Driver;
        public ExtentTest logger;
        public TestContext Context;
        public ROIFacilitySummaryDashBoardPage(RemoteWebDriver driver, ExtentTest _loger, TestContext _context)
        {
            Driver = driver;
            logger = _loger;
            Context = _context;
        }

        //
        public By Date = By.XPath("//span[@id='daterange']");
        public By Location = By.XPath("//select[@id='input_nROIFacilityID']");
        public By Contract = By.XPath("//select[@id='nContractID']");
        public By CreateReport = By.XPath("//input[@id='btn_submit']");
        public By includeTest = By.XPath("//input[@id='bIncludeTestDemo']");
        public By lbl_Excel_icon = By.XPath("//i[@id='lbl_Excel_icon']");
        public By div_Excel_icon = By.XPath("//span[@id='div_Excel_icon']//i[@id='lbl_Excel_icon']");
        public By div_PDF_icon = By.XPath("//span[@id='div_PDF_icon']//i[@id='lbl_PDF_icon']");
        public By pdfIcon = By.XPath("//span[@id='div-pdf-icon']//i[@id='lbl_PDF_icon']");
        public By excelIcon = By.XPath("//div[@id='div-excel-icon']//i[@id='lbl_Excel_icon']");
        public By monthButton = By.XPath("//table[@class='table-condensed']//td[contains(@class,'available active')]");
        public By totalPagesCount = By.XPath("//span[@id='dtTransactionalModelRequestDetail-PageOfPages']");
        public By pageTextBox = By.XPath("//input[@id='dtTransactionalModelRequestDetail-PageNr']");
        public By tileCaptions = By.XPath("//label[@class='rdDashboardTitleCaption']");
        public By selectTimeFrameLabel = By.XPath("//span[@id='range-label']");
        public By rdFrame = By.XPath("//iframe[starts-with(@id,'rdFrame')]");
        public By tab_Volume = By.XPath("//span[@class ='rdDashboardTab' and text()='Volume']");
        public By tab_TurnaroundTime = By.XPath("//span[@class ='rdDashboardTab' and text()='Turnaround Time']");
        public By tab_AgingRequests = By.XPath("//span[@class ='rdDashboardTab' and text()='Aging Requests']");
        public By tab_PendingOnHold = By.XPath("//span[@class ='rdDashboardTab' and text()='Pending - On Hold']");
        public By volumeTab = By.XPath("//span[contains(text(),'Volume')]");
        public By volumeTileCaptions = By.XPath("//label[@class='rdDashboardTitleCaption']");
        public By requestVolumeFrame = By.XPath("//iframe[@id ='srRequestVolume']");
        public By turnAroundTimeFrame = By.XPath("//iframe[@id='srTATTotal']");//
        public By pendingOnHoldTimeFrame = By.XPath("//iframe[@id='srOpenStatus']");
        public By turnAroundTimeTab = By.XPath("//span[(text() ='Turnaround Time')]");
        public By preBillOverAllDays = By.XPath("//div[@id='div-tat-b']//span[@class ='summary-value']");
        public By summaryFrame = By.XPath("//iframe[@id ='srTATSummary']");
        public By lblExcelIcon = By.XPath("//span[@id='div_excel_button']");
        public By lblPdfIcon = By.XPath("//span[@id='div_pdf_button']");
        public By agingRequestsTab = By.XPath("//span[(text() ='Aging Requests')]");
        public By pendingOnHoldTab = By.XPath("//span[(text() ='Pending - On Hold')]");
        public By prevMonth = By.XPath("//input[@id='btnPrev']");
        public By lastButtonIcon = By.XPath("//img[@alt='Last page']");
        public By requestAgingNonPatientFrame = By.XPath("//iframe[@id ='srRequestAgingNonPatient']");
        public By drpRequesterTypeDefaultText = By.XPath("//button[@id='nRequesterTypeID_multi_handler']/span[@class='rd-checkboxlist-caption']");
        public By excludeRadioButton = By.XPath("//input[@value='Exclude']");
        public By chkCheckAll = By.XPath("//input[@id='naExcludeID_check_all']");
        public By btnExcludeList = By.XPath("//button[@id='naExcludeID_handler']");
        public By lnkCustomize = By.XPath("//a[@id='show_div_user_inputs']/span");
        public By requesterCheckAll = By.XPath("//input[@id='nRequesterTypeID_multi_check_all']");
        public By excludeDrp = By.XPath("//button[@id='naExcludeID_handler']");       
        public By MonthsRange = By.XPath("//select[@name='inpLastXMonths']");
        public By ThreeMonths = By.XPath("//select[@ id='inpLastXMonths']");
        public By SixMonths = By.XPath("//select[@ id='inpLastXMonths']");
        public By TwelveMonths = By.XPath("//select[@ id='inpLastXMonths']");
        public By graghFrame = By.XPath("//*[@name='srTATAvg']");
        public By recLog = By.XPath("(//*[@text-anchor='start'])[1]");//
        public By turnAroundExcelIcon = By.XPath("//span[@id='div_excel_button']//i[@id='lblExportExcel']");
        public By turnAroundPDFIcon = By.XPath("//span[@id='div_pdf_button']//i[@id='lblExportPdf']");
        public By daysCalendar = By.XPath("//*[@id='bDaysCalendar']");
        public By customize = By.XPath("//span[@id='div_user_inputs_right_arrow']");
        public string ApplyFiltersAndCreateReport()
        {
            string dateSelected = string.Empty;
            try
            {
                Driver.SleepTheThread(4);
                Automation.Common.Iframe frame = new Automation.Common.Iframe(Driver, logger, Context);
                frame.SwitchToRDFrame();
                //Driver.JavaScriptClick(monthButton);
                dateSelected = Driver.GetText(selectTimeFrameLabel);
                Driver.SleepTheThread(5);
                Driver.DirectClick(CreateReport);
                Driver.SleepTheThread(5);
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to create summary dashboard report,Exception detail: {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
            return dateSelected;
        }

        public bool IsAllTabsVisibleUnderReport()
        {
            bool isVisible = false;
            try
            {
                IWebElement tabVolume = Driver.FindElementBy(tab_Volume);
                IWebElement tabTurnaroundTime = Driver.FindElementBy(tab_TurnaroundTime);
                IWebElement tabAgingRequests = Driver.FindElementBy(tab_AgingRequests);
                IWebElement tabPendingOnHold = Driver.FindElementBy(tab_PendingOnHold);

                if (tabVolume.Displayed == true && tabTurnaroundTime.Displayed == true
                    && tabAgingRequests.Displayed == true && tabPendingOnHold.Displayed == true)
                {
                    isVisible = true;
                }
                return isVisible;
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to verify that all tabs are visible under report {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }

        }

        public void ClickOnVolumeTab()
        {
            try
            {
                Driver.DirectClick(volumeTab);
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to click on volume tab Message : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }

        public bool ValidateTilesForVolumeTab(string dateToValidate)
        {
            bool isTilesValidated = false;
            string tileName = string.Empty;
            bool isTileVisible = false;
            string sDate = string.Empty;
            try
            {
                Driver.SwitchTo().DefaultContent();
                IWebElement frame = Driver.FindElementBy(rdFrame);
                Driver.SwitchTo().Frame(frame);
                //ClickOnVolumeTab();
                var volumeTabs = Driver.FindElementsBy(volumeTileCaptions);
                sDate = dateToValidate.ToUpper();
                foreach (var tab in volumeTabs)
                {
                    tileName = tab.Text;
                    isTileVisible = tab.Displayed;
                    if (tileName.Contains(sDate) && isTileVisible == true)
                    {
                        isTilesValidated = true;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to get get volume tab tile captions Message : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
            return isTilesValidated;
        }

        public void Validatebargraphs( string downloadFolderPath)
        {
            IWebElement frame = Driver.FindElementBy(requestVolumeFrame);
            Driver.SwitchTo().Frame(frame);

            IWebElement parent = Driver.FindElementBy(By.ClassName("highcharts-series-group"));
            ReadOnlyCollection<IWebElement> children = parent.FindElements(By.TagName("rect"));
            int detailedRequestsCount = 0;
            Actions action = new Actions(Driver);
            for (int z = 0; z < children.Count; z++)
            {
                children[z].Click();
                Driver.SleepTheThread(2);
                string tab1 = Driver.WindowHandles[0];
                string tab2 = Driver.WindowHandles[1];
                Driver.SwitchTo().Window(tab2);
                var result = ValidateRequests();
                detailedRequestsCount = result.Item1;
                //ValidateDetailView();
                ValidateExcel(downloadFolderPath, detailedRequestsCount);
                Driver.SwitchTo().Window(tab2).Close();
                Driver.SleepTheThread(2);
                Driver.SwitchTo().Window(tab1);
               break;
            }
        }

        public void ClickOnTurnaroundTimeTab()
        {
            try
            {
                Automation.Common.Iframe frame = new Automation.Common.Iframe(Driver, logger, Context);
                frame.SwitchToRDFrame();
                Driver.DirectClick(turnAroundTimeTab);
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to click on TurnAround Time Tab with message : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }
        public bool ValidateTilesForTurnaroundTab(string dateToValidate)
        {
            bool isTilesValidated = false;
            string tileName = string.Empty;
            bool isTileVisible = false;
            string sDate = string.Empty;
            try
            {
                //Driver.SwitchTo().DefaultContent();
                //IWebElement frame = Driver.FindElementBy(rdFrame);
                //Driver.SwitchTo().Frame(frame);
                //ClickOnTurnaroundTimeTab();
                var turnAroundTimeTabs = Driver.FindElementsBy(tileCaptions);
                sDate = dateToValidate.ToUpper();
                foreach (var tab in turnAroundTimeTabs)
                {
                    tileName = tab.Text;
                    isTileVisible = tab.Displayed;
                    if (tileName.Contains(sDate) && isTileVisible == true)
                    {
                        isTilesValidated = true;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to get get turnraound time tab's tile captions with message : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
            return isTilesValidated;
        }

        public string GetOverAllTimeFromPreBill()
        {
            ClickOnTurnaroundTimeTab();
            IWebElement frame = Driver.FindElementBy(summaryFrame);
            Driver.SwitchTo().Frame(frame);
            string overAllTime = string.Empty;
            IWebElement iOverAllDaysForPreBill = Driver.FindElementBy(preBillOverAllDays);
            overAllTime = iOverAllDaysForPreBill.Text;
            return overAllTime;
        }

        public void ClickOnAgingRequestsTab()
        {
            try
            {
                Automation.Common.Iframe frame = new Automation.Common.Iframe(Driver, logger, Context);
                frame.SwitchToRDFrame();
                Driver.DirectClick(agingRequestsTab);
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to click on Aging Requests Tab with message : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }

        public void ClickOnPendingOnHoldTab()
        {
            try
            {
                Automation.Common.Iframe frame = new Automation.Common.Iframe(Driver, logger, Context);
                frame.SwitchToRDFrame();
                Driver.DirectClick(pendingOnHoldTab);
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to click on Pending OnHold Tab Tab with message : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }

        public bool DoesVolumeTabHasExportAndPdfIcons()
        {
            bool isPdfAndExcelIconVisible = false;
            Driver.SwitchTo().DefaultContent();
            IWebElement frame = Driver.FindElementBy(rdFrame);
            Driver.SwitchTo().Frame(frame);
            ClickOnVolumeTab();
            IWebElement iPdf = Driver.FindElementBy(lblPdfIcon);
            IWebElement iExcel = Driver.FindElementBy(lblExcelIcon);

            if (iPdf.Displayed == true && iExcel.Displayed == true)
            {
                isPdfAndExcelIconVisible = true;
            }
            return isPdfAndExcelIconVisible;
        }

        public bool DoesTurnaroundTimeTabHasExportAndPdfIcons()
        {
            bool isPdfAndExcelIconVisible = false;
            Driver.SwitchTo().DefaultContent();
            IWebElement frame = Driver.FindElementBy(rdFrame);
            Driver.SwitchTo().Frame(frame);
            ClickOnTurnaroundTimeTab();
            IWebElement iPdf = Driver.FindElementBy(lblPdfIcon);
            IWebElement iExcel = Driver.FindElementBy(lblExcelIcon);

            if (iPdf.Displayed == true && iExcel.Displayed == true)
            {
                isPdfAndExcelIconVisible = true;
            }
            return isPdfAndExcelIconVisible;
        }

        public bool DoesAgingRequestsTabHasExportAndPdfIcons()
        {
            bool isPdfAndExcelIconVisible = false;
            ClickOnAgingRequestsTab();
            IWebElement iPdf = Driver.FindElementBy(lblPdfIcon);
            IWebElement iExcel = Driver.FindElementBy(lblExcelIcon);

            if (iPdf.Displayed == true && iExcel.Displayed == true)
            {
                isPdfAndExcelIconVisible = true;
            }
            return isPdfAndExcelIconVisible;
        }


        public bool DoesPendingOnHoldTabHasExportAndPdfIcons()
        {
            bool isPdfAndExcelIconVisible = false;
            Driver.SwitchTo().DefaultContent();
            IWebElement frame = Driver.FindElementBy(rdFrame);
            Driver.SwitchTo().Frame(frame);
            ClickOnPendingOnHoldTab();
            IWebElement iPdf = Driver.FindElementBy(lblPdfIcon);
            IWebElement iExcel = Driver.FindElementBy(lblExcelIcon);

            if (iPdf.Displayed == true && iExcel.Displayed == true)
            {
                isPdfAndExcelIconVisible = true;
            }
            return isPdfAndExcelIconVisible;
        }

        public string ChangeFiltersAndCreateReport()
        {
            string dateSelected = string.Empty;
            try
            {
                Driver.Click(lnkCustomize);
                Driver.SleepTheThread(3);
                Automation.Common.Iframe frame = new Automation.Common.Iframe(Driver, logger, Context);
                frame.SwitchToRDFrame();
                Driver.JavaScriptClick(prevMonth);
                dateSelected = Driver.GetText(selectTimeFrameLabel);
                Driver.SleepTheThread(5);
                Driver.JavaScriptClick(CreateReport);
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to create summary dashboard report,Exception detail: {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
            return dateSelected;
        }
        //public bool ValidateDetailView()
        //{
        //    bool isRequestsValidated = false;
        //    try
        //    {
        //        int totalPages = 0;               
        //        bool isCountValid = true;
        //        List<IWebElement> tableData = Driver.FindElementsBy(By.XPath("//table[@id='RequestVolumeDetail']//tr[not(contains(@style,'display:none;'))]"));
        //        int eachPageRowCount = tableData.Count - 1;
        //        int previousPages = 0;
        //        totalPages = GetTotalPagesCount();
        //        previousPages = totalPages - 1;
        //        int totalRecordsExcludingLastPage = eachPageRowCount *previousPages;
        //        isCountValid = ValidateRequestsCount(totalPages, totalRecordsExcludingLastPage);
        //        if (isCountValid)
        //        {
        //            isRequestsValidated = true;
        //            logger.Log(Status.Info, $"Verified that detail view of total requests generated the same number of requests for each number hyperlink");
        //        }
        //        else
        //        {
        //            logger.Log(Status.Fail, $"Verified that detail view of total requests not generated the same number of requests for each number hyperlink");
        //        }
        //    }

        //    catch (Exception ex)
        //    {
        //        throw new Exception($"Failed to validate detail view of requests: {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
        //    }
        //    return isRequestsValidated;
        //}

       

        public int GetTotalPagesCount()
        {
            string tRequestsXpath = "//span[@id='RequestVolumeDetail-PageOfPages']";
            string sRequestsCount = Driver.GetText(By.XPath(tRequestsXpath));
            int requestCount = Convert.ToInt32(sRequestsCount);
            return requestCount;
        }

        public int ValidateRequestsCount(int pages,int totalRecordsTillLastPage)
        {
            int countByNumberOfPages = 0;
            int totalRequestsCount = 0;
            int lastPageRowCount = 0;
            int iPages = 0;
            bool isValid = false;
            if (pages > 1)
            {
                iPages = pages - 1;
                countByNumberOfPages = iPages * 20;
            }
            ClickOnLastPage();
            Driver.SleepTheThread(8);
            IWebElement table = Driver.FindElement(By.XPath("//table[@id='RequestVolumeDetail']"));
            ReadOnlyCollection<IWebElement> allRows = table.FindElements(By.TagName("tr"));
            lastPageRowCount = allRows.Count - 1;
            totalRequestsCount = lastPageRowCount + countByNumberOfPages;
           if(totalRequestsCount> totalRecordsTillLastPage && countByNumberOfPages== totalRecordsTillLastPage)
            {
                isValid = true;
            }
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

        public void ClickOnExportToExcel()
        {
            try
            {
                IWebElement icon = Driver.FindElementBy(excelIcon);
                icon.Click();
                Driver.SleepTheThread(2);
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to click on export to excel icon : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }

        public void ClickOnExportToPdf()
        {
            try
            {
                IWebElement icon = Driver.FindElementBy(pdfIcon);
                icon.Click();
                Driver.SleepTheThread(2);
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to click on export to pdf icon : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }


        public Tuple<int, bool> ValidateRequests()
        {
            bool isRequestsValidated = false;
            int totalPages = 0;
            int totalRequests = 0;
            try
            {
                
                List<IWebElement> tableData = Driver.FindElementsBy(By.XPath("//table[@id='RequestVolumeDetail']//tr[not(contains(@style,'display:none;'))]"));
                int eachPageRowCount = tableData.Count - 1;
                int previousPages = 0;
                totalPages = GetTotalPagesCount();
                previousPages = totalPages - 1;
                int totalRecordsExcludingLastPage = eachPageRowCount * previousPages;
                totalRequests = ValidateRequestsCount(totalPages, totalRecordsExcludingLastPage);
                if (totalRequests> totalRecordsExcludingLastPage)
                {
                    isRequestsValidated = true;
                    logger.Log(Status.Info, $"Verified that detail view of total requests generated the same number of requests for each number hyperlink");
                }
                else
                {
                    logger.Log(Status.Fail, $"Verified that detail view of total requests not generated the same number of requests for each number hyperlink");
                }
            }

            catch (Exception ex)
            {
                throw new Exception($"Failed to validate detail view of requests: {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
          

            var tuple = new Tuple<int,bool>(totalRequests,isRequestsValidated);
            return tuple;
        }

        public void ValidateExcel(string downloadFolder, int tableTotalRequests)
        {
            string excelFileName = string.Empty;
            try {
                //ExcelReaderFile.DeleteExistingFiles(downloadFolder, "xlsx", "MRO eXpress Reconciliation Report.xlsx");
                ClickOnExportToExcel();
                Driver.SleepTheThread(10);
                excelFileName = ReadXLSFileFromDirectory(downloadFolder);               
                excelFileName = downloadFolder + excelFileName + ".xlsx";               
                ExcelReaderFile excelReaderFile = new ExcelReaderFile(excelFileName);
                int lastRowNumber = excelReaderFile.getRowCount("Sheet1");
                int excelTotalRequests = lastRowNumber - 3;
                Assert.AreEqual(tableTotalRequests, excelTotalRequests, "Failed to validate total requests column from excel with total requests from UI");
                ExcelReaderFile.DeleteExistingFiles(downloadFolder, "xlsx", excelFileName);
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to validate excel data with UI data : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }

        public string ReadXLSFileFromDirectory(string path)
        {
            string fileName = string.Empty;
            try
            {
                DirectoryInfo Dir = new DirectoryInfo(path);
                List<FileInfo> files = Dir.GetFiles().ToList();

                foreach (FileInfo fileinfo in files)
                {
                    if (fileinfo.Extension == ".xlsx" && fileinfo.Name.Contains("rdD"))
                    {
                        System.Threading.Thread.Sleep(1000);
                        fileName = fileinfo.ToString().Replace(".xlsx", "").Trim();
                    }
                }

            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to read file from directory whose exception message is :{ex.Message} \n whose stack trace is :{ex.StackTrace}");
            }
            return fileName;
        }

        public void ValidatePieChartsForTurnAroundTab(string downloadFolderPath)
        {
            IWebElement frame = Driver.FindElementBy(turnAroundTimeFrame);
            Driver.SwitchTo().Frame(frame);

            IWebElement parent = Driver.FindElementBy(By.ClassName("highcharts-series-group"));
            ReadOnlyCollection<IWebElement> children = parent.FindElements(By.TagName("path"));
            int detailedRequestsCount = 0;
            Actions action = new Actions(Driver);
            for (int z = 0; z < children.Count; z++)
            {
                // action.MoveToElement(children[z]).Perform();
                // action.Click(children[z]).Build().Perform();
                //action.MoveToElement(children[z]).Perform();
                children[z].Click();
                Driver.SleepTheThread(2);
                string tab1 = Driver.WindowHandles[0];
                string tab2 = Driver.WindowHandles[1];
                Driver.SwitchTo().Window(tab2);
                var result = ValidateTurnAroundRequests();
                detailedRequestsCount = result.Item1;
                ValidateExcel(downloadFolderPath, detailedRequestsCount);
                Driver.SwitchTo().Window(tab2).Close();
                Driver.SleepTheThread(2);
                Driver.SwitchTo().Window(tab1);
                break;
            }
        }

        public Tuple<int, bool> ValidateTurnAroundRequests()
        {
            bool isRequestsValidated = false;
            int totalPages = 0;
            int totalRequests = 0;
            try
            {

                List<IWebElement> tableData = Driver.FindElementsBy(By.XPath("//table[@id='dtTATTotalDetail']//tr[not(contains(@style,'display:none;'))]"));
                int eachPageRowCount = tableData.Count - 1;
                int previousPages = 0;
                totalPages = GetTotalPagesCountForTurnAroundRequests();
                previousPages = totalPages - 1;
                int totalRecordsExcludingLastPage = eachPageRowCount * previousPages;
                totalRequests = ValidateTurnAroundRequestsCount(totalPages, totalRecordsExcludingLastPage);
                if (totalRequests > totalRecordsExcludingLastPage)
                {
                    isRequestsValidated = true;
                    logger.Log(Status.Info, $"Verified that detail view of total requests generated the same number of requests for each number hyperlink");
                }
                else
                {
                    logger.Log(Status.Fail, $"Verified that detail view of total requests not generated the same number of requests for each number hyperlink");
                }
            }

            catch (Exception ex)
            {
                throw new Exception($"Failed to validate detail view of requests: {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }

            var tuple = new Tuple<int, bool>(totalRequests, isRequestsValidated);
            return tuple;
        }

        public int GetTotalPagesCountForTurnAroundRequests()
        {
            string tRequestsXpath = "//span[@id='dtTATTotalDetail-PageOfPages']";
            string sRequestsCount = Driver.GetText(By.XPath(tRequestsXpath));
            int requestCount = Convert.ToInt32(sRequestsCount);
            return requestCount;
        }

        public int ValidateTurnAroundRequestsCount(int pages, int totalRecordsTillLastPage)
        {
            int countByNumberOfPages = 0;
            int totalRequestsCount = 0;
            int lastPageRowCount = 0;
            int iPages = 0;
            bool isValid = false;
            if (pages > 1)
            {
                iPages = pages - 1;
                countByNumberOfPages = iPages * 20;
            }
            ClickOnLastPage();
            Driver.SleepTheThread(8);
            IWebElement table = Driver.FindElement(By.XPath("//table[@id='dtTATTotalDetail']"));
            ReadOnlyCollection<IWebElement> allRows = table.FindElements(By.TagName("tr"));
            lastPageRowCount = allRows.Count - 1;
            totalRequestsCount = lastPageRowCount + countByNumberOfPages;
            if (totalRequestsCount > totalRecordsTillLastPage && countByNumberOfPages == totalRecordsTillLastPage)
            {
                isValid = true;
            }
            return totalRequestsCount;
        }
        public bool ValidateTilesForAgingRequestsTab(string dateToValidate)
        {
            bool isTilesValidated = false;
            string tileName = string.Empty;
            bool isTileVisible = false;
            string sDate = string.Empty;
            try
            {
                //Driver.SwitchTo().DefaultContent();
                //IWebElement frame = Driver.FindElementBy(rdFrame);
                //Driver.SwitchTo().Frame(frame);
                //ClickOnTurnaroundTimeTab();
                var agingRequestsTabs = Driver.FindElementsBy(tileCaptions);
                sDate = dateToValidate.ToUpper();
                foreach (var tab in agingRequestsTabs)
                {
                    tileName = tab.Text;
                    isTileVisible = tab.Displayed;
                    if (tileName.Contains(sDate) && isTileVisible == true)
                    {
                        isTilesValidated = true;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to get get aging requests tab's tile captions with message : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
            return isTilesValidated;
        }

        public bool isRequesterMultiTypeDropdownAvailable()
        {
            bool isValid = false;
            IWebElement frame = Driver.FindElementBy(requestAgingNonPatientFrame);
            Driver.SwitchTo().Frame(frame);
            string defaultSelectedOption = string.Empty;
            defaultSelectedOption = Driver.GetText(drpRequesterTypeDefaultText);
            if(defaultSelectedOption == "14 selected")
            {
                isValid = true;
            }
            if (defaultSelectedOption == "Select options")
            {
                Driver.Click(requesterCheckAll);
                Driver.Wait(TimeSpan.FromSeconds(2));
                isValid = true;
            }
            return isValid;
        }

        public string ReapplyFiltersAndCreateReport()
        {
            string dateSelected = string.Empty;
            try
            {
                ClickOnCustomize();
                Driver.SleepTheThread(4);
                Automation.Common.Iframe frame = new Automation.Common.Iframe(Driver, logger, Context);
                frame.SwitchToRDFrame();
                dateSelected = Driver.GetText(selectTimeFrameLabel);
                IWebElement excludeButton = Driver.FindElementBy(btnExcludeList);
                excludeButton.Click();
                CheckOrUncheckAllExcludeOptions(false);
                Driver.Click(CreateReport);
                Driver.SleepTheThread(5);
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to create summary dashboard report,Exception detail: {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
            return dateSelected;
        }

        public void CheckOrUncheckAllExcludeOptions(bool option)
        {
            Driver.Click(btnExcludeList);
            if (option == true)
            {
                Driver.Click(chkCheckAll);
            }
            else if (option == false)
            {
                Driver.Click(chkCheckAll);
            }
            Driver.Click(btnExcludeList);
        }

        public void ClickOnCustomize()
        {
            try
            {
                SwitchToRDFrame();
                Driver.DirectClick(lnkCustomize);
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to click the customize link : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }

        public void SwitchToRDFrame()
        {
            try
            {
                Driver.SwitchTo().DefaultContent();
                IWebElement frame = Driver.FindElementBy(rdFrame);
                Driver.SwitchTo().Frame(frame);
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to switch to rd fame Message : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }

        public void ValidatePieChartsForPendingOnHoldTab(string downloadFolderPath)
        {
            IWebElement frame = Driver.FindElementBy(pendingOnHoldTimeFrame);
            Driver.SwitchTo().Frame(frame);

            IWebElement parent = Driver.FindElementBy(By.ClassName("highcharts-series-group"));
            ReadOnlyCollection<IWebElement> children = parent.FindElements(By.TagName("path"));
            int detailedRequestsCount = 0;
            Actions action = new Actions(Driver);
            for (int z = 0; z < children.Count; z++)
            {                
                children[z].Click();
                Driver.SleepTheThread(2);
                string tab1 = Driver.WindowHandles[0];
                string tab2 = Driver.WindowHandles[1];
                Driver.SwitchTo().Window(tab2);
                var result = ValidatePendingOnHoldRequests();
                detailedRequestsCount = result.Item1;
                ValidateExcel(downloadFolderPath, detailedRequestsCount);
                Driver.SwitchTo().Window(tab2).Close();
                Driver.SleepTheThread(2);
                Driver.SwitchTo().Window(tab1);
                break;
            }
        }

        public Tuple<int, bool> ValidatePendingOnHoldRequests()
        {
            bool isRequestsValidated = false;
            int totalPages = 0;
            int totalRequests = 0;
            try
            {

                List<IWebElement> tableData = Driver.FindElementsBy(By.XPath("//table[@id='OpenStatusDetail']//tr[not(contains(@style,'display:none;'))]"));
                int eachPageRowCount = tableData.Count - 1;
                int previousPages = 0;
                totalPages = GetTotalPagesCountForPendingOnHoldRequests();
                previousPages = totalPages - 1;
                int totalRecordsExcludingLastPage = eachPageRowCount * previousPages;
                totalRequests = ValidatePendingOnHoldRequestsCount(totalPages, totalRecordsExcludingLastPage);
                if (totalRequests > totalRecordsExcludingLastPage)
                {
                    isRequestsValidated = true;
                    logger.Log(Status.Info, $"Verified that detail view of total requests generated the same number of requests for each number hyperlink");
                }
                else
                {
                    logger.Log(Status.Fail, $"Verified that detail view of total requests not generated the same number of requests for each number hyperlink");
                }
            }

            catch (Exception ex)
            {
                throw new Exception($"Failed to validate detail view of requests: {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }

            var tuple = new Tuple<int, bool>(totalRequests, isRequestsValidated);
            return tuple;
        }

        public int GetTotalPagesCountForPendingOnHoldRequests()
        {
            string tRequestsXpath = "//span[@id='OpenStatusDetail-PageOfPages']";
            string sRequestsCount = Driver.GetText(By.XPath(tRequestsXpath));
            int requestCount = Convert.ToInt32(sRequestsCount);
            return requestCount;
        }

        public int ValidatePendingOnHoldRequestsCount(int pages, int totalRecordsTillLastPage)
        {
            int countByNumberOfPages = 0;
            int totalRequestsCount = 0;
            int lastPageRowCount = 0;
            int iPages = 0;
            bool isValid = false;
            if (pages > 1)
            {
                iPages = pages - 1;
                countByNumberOfPages = iPages * 20;
            }
            ClickOnLastPage();
            Driver.SleepTheThread(8);
            IWebElement table = Driver.FindElement(By.XPath("//table[@id='OpenStatusDetail']"));
            ReadOnlyCollection<IWebElement> allRows = table.FindElements(By.TagName("tr"));
            lastPageRowCount = allRows.Count - 1;
            totalRequestsCount = lastPageRowCount + countByNumberOfPages;
            if (totalRequestsCount > totalRecordsTillLastPage && countByNumberOfPages == totalRecordsTillLastPage)
            {
                isValid = true;
            }
            return totalRequestsCount;
        }


        public int ValidatebargraphsForRequestAgingNonPatient()
        {

            try
            {
                IWebElement parent = Driver.FindElementBy(By.ClassName("highcharts-series-group"));
                ReadOnlyCollection<IWebElement> children = parent.FindElements(By.TagName("rect"));

                if (children.Count == 0)
                {
                    logger.Log(Status.Info, "No data to validate request aging details page");
                }

                if (children.Count >= 1)
                {
                    Actions action = new Actions(Driver);
                    for (int z = 0; z < 1; z++)
                    {

                        Driver.Wait(TimeSpan.FromSeconds(2));
                        children[z].Click();
                        Driver.SleepTheThread(2);
                        string tab1 = Driver.WindowHandles[0];
                        string tab2 = Driver.WindowHandles[1];
                        //string tab3 = Driver.WindowHandles[2];
                        Driver.SwitchTo().Window(tab1);
                        Driver.SwitchTo().Window(tab2);

                    }

                }
                return children.Count;

            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to to validate bar graph {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }


        }

        public bool FilterReportBasedOnRequestDate(string requestId)
        {
            Driver.SwitchToDefaultContent();
            bool isPresent = false;
            int retryCount = 0;
            try
            {
                Driver.SleepTheThread(5);
                int totalPageCount = Convert.ToInt32(Driver.GetText(By.XPath("//span[@id='dtRequestAgingDetail-PageOfPages']")));
                if (totalPageCount > 1)
                {

                    IJavaScriptExecutor js = (IJavaScriptExecutor)Driver;
                    js.ExecuteScript($"document.getElementById('dtRequestAgingDetail-PageNr').setAttribute('value', '{totalPageCount}')");
                    Driver.ClickOnDisplayedElement(By.Id("dtRequestAgingDetail-PageNr"));
                    Driver.FindElementBy(By.Id("dtRequestAgingDetail-PageNr")).SendKeys(Keys.Enter);
                    Driver.DirectClick(By.XPath("//span[@id='dtRequestAgingDetail-PageOfPages']"));
                    isPresent = CheckRequestIdExsist(requestId);
                }
                else
                {
                    isPresent = CheckRequestIdExsist(requestId);
                }
                return isPresent;

            }

            catch (Exception ex)
            {

                throw new Exception($"Failed to filter report based on request date {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }


        public bool CheckRequestIdExsist(string requestId)
        {
            bool isValidated = false;
            try
            {
                var requestElements = Driver.FindElementsBy(By.XPath("//a[contains(@id,'actLinkParent_Row')]"));
                foreach (var requestElement in requestElements)
                {
                    if (requestElement.Text.Equals(requestId))
                    {
                        isValidated = true;
                        break;
                    }
                }


            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to check request id aganist request table {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
            return isValidated;
        }
        public void CloseTab()
        {
            try
            {
                string tab1 = Driver.WindowHandles[0];
                string tab4 = Driver.WindowHandles[3];               
                Driver.SwitchTo().Window(tab4);
                
            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to close the tab with details as {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }

        }
        public string CreateReportWithFliters()
        {
            string dateSelected = string.Empty;
            try
            {
                Driver.SleepTheThread(10);
                Automation.Common.Iframe frame = new Automation.Common.Iframe(Driver, logger, Context);
                frame.SwitchToRDFrame();
                //Driver.JavaScriptClick(monthButton);
                dateSelected = Driver.GetText(selectTimeFrameLabel);
                Driver.DirectClick(By.XPath("//span[@id='div_naExclude']//span//button"));
                bool isSelected = Driver.FindElementBy(chkCheckAll).Selected;
                if (isSelected == true)
                {
                    Driver.DirectClick(chkCheckAll);
                    Driver.Wait(TimeSpan.FromSeconds(2));
                    Driver.FindElementBy(By.XPath("//span[@id='div_naExclude']//span//button")).SendKeys(Keys.Enter);
                }

                Driver.DirectClick(CreateReport);
                Driver.SleepTheThread(5);
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to create summary dashboard report,Exception detail: {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
            return dateSelected;
        }


        public void SwitchToFirstTab()
        {
            try
            {
                string tab1 = Driver.WindowHandles[0];               
                Driver.SwitchTo().Window(tab1);
                
            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to switch to first tab,Exception detail: {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }

        }


        public bool VerifyAllMonthsFiltersAreAvailable()
        {
            bool isAllMonthSearchesAvailable = false;
            try
            {
                Driver.Click(lnkCustomize);
                Driver.SleepTheThread(4);
                Automation.Common.Iframe frame = new Automation.Common.Iframe(Driver, logger, Context);
                frame.SwitchToRDFrame();               
                //Driver.DirectClick(MonthsRange);
                var allMonths = Driver.FindElementBy(MonthsRange);
                var selectallMonthSearches = new SelectElement(allMonths);
                IList<IWebElement> optionValues = selectallMonthSearches.Options;
                foreach (IWebElement option in optionValues)
                {
                    if (option.Text == "Last 3 Months" || option.Text == "Last 6 Months" || option.Text == "Last 12 Months")
                    {
                        isAllMonthSearchesAvailable = true;
                    }
                }
                //Driver.DirectClick(ThreeMonths);
                //Driver.DirectClick(SixMonths);
                //Driver.DirectClick(TwelveMonths);
                //Driver.DirectClick(CreateReport);               
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to verify all month searches under dropdown next to date dropdown list: {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
            return isAllMonthSearchesAvailable;
        }


        public string ApplyFiltersAndCreateReport3Months()
        {
            string dateSelected = string.Empty;
            try
            {
                Driver.SleepTheThread(4);
                Automation.Common.Iframe frame = new Automation.Common.Iframe(Driver, logger, Context);
                frame.SwitchToRDFrame();
                Driver.SleepTheThread(3);
                var allMonths = Driver.FindElementBy(MonthsRange);
                var selectallMonthSearches = new SelectElement(allMonths);
                selectallMonthSearches.SelectByText("Last 3 Months");
                var days_Calender = Driver.FindElementBy(daysCalendar);
                var selectDays_Calender = new SelectElement(days_Calender);
                selectDays_Calender.SelectByText("Calendar Days");
                Driver.DirectClick(CreateReport);
                Driver.SleepTheThread(5);
                VerifyReceivedToShipped();
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to create summary dashboard report,Exception detail: {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
            return dateSelected;
        }

        public string ApplyFiltersAndCreateReport12Months()
        {
            string dateSelected = string.Empty;
            try
            {
                Driver.SleepTheThread(4);
                Automation.Common.Iframe frame = new Automation.Common.Iframe(Driver, logger, Context);
                frame.SwitchToRDFrame();
                Driver.SleepTheThread(3);
                IWebElement element = Driver.FindElementBy(customize);
                if (element.Displayed)
                {
                    Driver.DirectClick(customize);
                }
                var allMonths = Driver.FindElementBy(TwelveMonths);
                var selectallMonthSearches = new SelectElement(allMonths);
                selectallMonthSearches.SelectByText("Last 12 Months");
                var days_Calender = Driver.FindElementBy(daysCalendar);
                var selectDays_Calender = new SelectElement(days_Calender);
                selectDays_Calender.SelectByText("Business Days");
                Driver.DirectClick(CreateReport);
                Driver.SleepTheThread(5);
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to create summary dashboard report,Exception detail: {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
            return dateSelected;
        }




        public string VerifyReceivedToLogged()
        {
            string dateSelected = string.Empty;
            try
            {
                IWebElement frame = Driver.FindElementBy(graghFrame);
                Driver.SwitchTo().Frame(frame);
                bool flagRecLog = Driver.VerifyWebElement(recLog);
                bool flagLogRel = Driver.VerifyWebElement(By.XPath("(//*[@text-anchor='start'])[2]"));
                bool flagRecRel = Driver.VerifyWebElement(By.XPath("(//*[@text-anchor='start'])[3]"));
                bool flagRelShip = Driver.VerifyWebElement(By.XPath("(//*[@text-anchor='start'])[4]"));
                bool flagRecShip = Driver.VerifyWebElement(By.XPath("(//*[@text-anchor='start'])[5]"));
                Assert.IsTrue(flagRecLog & flagLogRel & flagRecRel & flagRelShip & flagRecShip);
                logger.Log(Status.Info, "Verified the Turnaround time average graph is displaying Received to Logged, Logged to release, Received to Released, Release to shipped Received to shipped");
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to create summary dashboard report,Exception detail: {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
            return dateSelected;
        }

        public void VerifyReceivedToShipped()
        {
            try
            {
                IWebElement objWebElement = Driver.FindElementBy(By.XPath("(//*[@class='highcharts-series highcharts-tracker'])[5]/*[1]"));
                Driver.ScrollIntoView(objWebElement);
                bool flag1 = Driver.VerifyWebElement(By.XPath("(//*[@class='highcharts-series highcharts-tracker'])[5]/*[1]"));
                bool flag2 = Driver.VerifyWebElement(By.XPath("(//*[@class='highcharts-series highcharts-tracker'])[5]/*[2]"));

                Assert.IsTrue(flag1 & flag2);
                logger.Log(Status.Info, "Verified Received and Shipped gragh data.");
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to create summary dashboard report,Exception detail: {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }




        public void ValidateRequesterTypeDropDown()
        {
            try
            {
                IWebElement objWebElement1 = Driver.FindElementBy(By.XPath("//button[@id='nRequesterTypeID_multi_handler']"));
                objWebElement1.Click();
                Driver.SleepTheThread(3);
                IWebElement objWebElement = Driver.FindElementBy(By.XPath("//table[@id='nRequesterTypeID_multi_container']/tbody/tr/td/ul/li[1]/label/input"));
                objWebElement.Click();
                Driver.SleepTheThread(2);
                bool flagRecLog = Driver.VerifyWebElement(By.XPath("//*[contains(text(),'No data to display')]"));
                Assert.IsTrue(flagRecLog);
                objWebElement.Click();
                Driver.SleepTheThread(2);
                VerifyReceivedToShipped();
                logger.Log(Status.Info, "Verified Received and Shipped graph data");
                Driver.SwitchToDefaultContent();
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to override the delivery  Message : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }

        public void ClickOnExportToExcelForTurnaroundTab()
        {
            try
            {               
                Automation.Common.Iframe frame = new Automation.Common.Iframe(Driver, logger, Context);
                frame.SwitchToRDFrame();
                List<IWebElement> excelLinks = Driver.FindElementsBy(turnAroundExcelIcon);
                for (int i = 0; i < excelLinks.Count; i++)
                {
                    excelLinks[i].Click();

                }
                Driver.SleepTheThread(15);
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to click on export to excel icons: {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }

        public void ClickOnExportToPdfForTurnaroundTab()
        {
            try
            {
                Automation.Common.Iframe frame = new Automation.Common.Iframe(Driver, logger, Context);
                frame.SwitchToRDFrame();
                List<IWebElement> pdfLinks = Driver.FindElementsBy(turnAroundPDFIcon);
                for (int i = 0; i < pdfLinks.Count; i++)
                {
                    pdfLinks[i].Click();

                }
                string tab1 = Driver.WindowHandles[0];
                string tab2 = Driver.WindowHandles[1];
                string tab3 = Driver.WindowHandles[2];
                Driver.SwitchTo().Window(tab3).Close();
                Driver.SwitchTo().Window(tab2).Close();
                Driver.SwitchTo().Window(tab1);
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to click on export to pdf icon : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }

        public void Verify_ReceivedToShipped()
        {
            try
            {
                IWebElement objWebElement = Driver.FindElementBy(By.XPath("(//*[@class='highcharts-series highcharts-tracker'])[5]/*[1]"));
                Driver.ScrollIntoView(objWebElement);
                bool flag1 = Driver.VerifyWebElement(By.XPath("(//*[@class='highcharts-series highcharts-tracker'])[5]/*[1]"));
                bool flag2 = Driver.VerifyWebElement(By.XPath("(//*[@class='highcharts-series highcharts-tracker'])[5]/*[2]"));
                bool flag3 = Driver.VerifyWebElement(By.XPath("(//*[@class='highcharts-series highcharts-tracker'])[5]/*[3]"));

                Assert.IsTrue(flag1 & flag2 & flag3);
                logger.Log(Status.Info, "Verified Received and Shipped gragh data.");
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to create summary dashboard report,Exception detail: {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }



    }
}
