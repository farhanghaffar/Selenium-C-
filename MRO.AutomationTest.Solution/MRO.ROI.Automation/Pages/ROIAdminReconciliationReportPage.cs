using AventStack.ExtentReports;
using DataDrivenProject;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MRO.ROI.Automation.Selenium;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using System;
using System.Collections.ObjectModel;
using System.Net;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;
using System.Collections.Generic;
using OpenQA.Selenium.Support.UI;
using MRO.ROI.Automation.Common;

namespace MRO.ROI.Automation.Pages
{
    public class ROIAdminReconciliationReportPage
    {
        public RemoteWebDriver Driver;
        public ExtentTest logger;
        public TestContext Context;
        public ROIAdminReconciliationReportPage(RemoteWebDriver driver, ExtentTest _loger, TestContext _context)
        {
            Driver = driver;
            logger = _loger;
            Context = _context;
        }
        public By Date = By.XPath("//span[@id='daterange']");
        public By monthButton = By.XPath("//table[@class='table-condensed']//td[contains(@class,'available active')]");
        public By Facility = By.XPath("//select[@id='input_nROIFacilityID']");
        public By includeTest = By.XPath("//input[@id='bIncludeTestDemo']");
        public By CreateReport = By.XPath("//input[@id='btn_submit']");
        public By btnCreateReport = By.XPath("//input[@id='btn_submit']");
        public By colneXpressFacilityID = By.XPath("//th[@id='colneXpressFacilityID-TH']");
        public By colnROIFacilityID = By.XPath("//th[@id='colnROIFacilityID-TH']");
        public By colsFacility = By.XPath("//th[@id='colsFacility-TH']");
        public By colnTotalRequests = By.XPath("//th[@id='colnTotalRequests-TH']");
        public By colnRequestCreated = By.XPath("//th[@id='colnRequestCreated-TH']");
        public By colnNotSubmitted = By.XPath("//th[@id='colnNotSubmitted-TH']");
        public By colnPendingLoggingCompleted = By.XPath("//th[@id='colnPendingLoggingCompleted-TH']");
        public By colnInProcess = By.XPath("//th[@id='colnInProcess-TH']");
        public By colnReleased = By.XPath("//th[@id='colnReleased-TH']");
        public By colnShipped = By.XPath("//th[@id='colnShipped-TH']");
        public By colnOpenIssuesOrActions = By.XPath("//th[@id='colnOpenIssuesOrActions-TH']");
        public By colnRequestCreationFailed = By.XPath("//th[@id='colnRequestCreationFailed-TH']");
        public By colnMissingInROI = By.XPath("//th[@id='colnMissingInROI-TH']");
        public By colnTotalErrors = By.XPath("//th[@id='colnTotalErrors-TH']");
        public By chkIncludeTest = By.XPath("//input[@name='bIncludeTestDemo']");
        public By selectDatePicker = By.XPath("(//span[@id='daterange'])[1]");
        public By fromDate = By.XPath("//input[@name ='daterangepicker_start']");
        public By toDate = By.XPath("//input[@name ='daterangepicker_end']");
        public By selectFromDate = By.XPath("((//table[@class='table-condensed'])[2]//tr//td[contains(text(),'3')])[3]");
        public By selectToDate = By.XPath("((//table[@class='table-condensed'])[1]//tr//td[contains(text(),'1')])[1]");
        public By reSelectFromDate = By.XPath("((//table[@class='table-condensed'])[2]//tr//td[contains(text(),'1')])[1]");
        public By reSelectToDate = By.XPath("((//table[@class='table-condensed'])[1]//tr//td[contains(text(),'1')])[1]");
        public By btnApply = By.XPath("//button[text()='Apply']");
        public By selectLocationDrp = By.XPath("//select[@id='nLocationID']");
        public By reconciliationReportFrame = By.XPath("//iframe[starts-with(@id,'rdFrame')]");
        public By dateRange = By.XPath("//span[@id='range-label']");
        public By excelIcon = By.XPath("//span[@id ='div_Excel_icon']");
        public By pdfIcon = By.XPath("//span[@id ='div_PDF_icon']");
        public By totalRequests = By.XPath("//span[@id='lblnTotalRequests_Row1']");
        public By pageNumber = By.XPath("//input[@id='dtReconciliationDrillDown-PageNr']");
        public By nextButtonIcon = By.XPath(" //img[@alt='Next page']");
        public By lbl_Excel_icon = By.XPath("//i[@id='lbl_Excel_icon']");
        public By lbl_PDF_icon = By.XPath("//i[@id='lbl_PDF_icon']");
        public By btnClearFilters = By.XPath("//input[@id='btn_Clear']");
        public By lastButtonIcon = By.XPath("//img[contains(@alt, 'Last page')]");
        public By lnkCustomize = By.XPath("//a[@id='show_div_user_inputs']/span");
        public By rdFrame = By.XPath("//iframe[starts-with(@id,'rdFrame')]");
        public By reportingGroup = By.XPath("//select[@id='nFacilityReportingGroupID']");
        public By backArrow = By.XPath("//i[@class ='fa fa-arrow-left icon-arrow-left glyphicon glyphicon-arrow-left']");
        public By lnkToday = By.XPath("//div[@class='ranges']/ul/li[1]");

        public By selectDate = By.XPath("//div[@class='calendar left']//table[@class='table-condensed']//tbody/tr[1]/td[2]");
        
        public By selectMonthDrp = By.XPath("//div[@class ='calendar left']//select[@class='monthselect']");
        public By selectYearDrp = By.XPath("//div[@class ='calendar left']//select[@class='yearselect']");
        public By btnFacilityList = By.XPath("//button[@id='input_nROIFacilityID_handler']");
        public By chkFacilityName = By.XPath("//input[@name ='input_nROIFacilityID' and @value =10061]");
        public By chkFacilityAll = By.XPath("//input[@id='input_nROIFacilityID_check_all']");


        public void CreateNewReconciliationReport(string facility, string contract, bool selectIncludeTest)
        {
            try
            {
                
                Driver.SleepTheThread(7);
                Automation.Common.Iframe frame = new Automation.Common.Iframe(Driver, logger, Context);
                frame.SwitchToRDFrame();
                logger.Log(Status.Info, "Enter from date");

                logger.Log(Status.Info, "Select year : 2021");
                Driver.DirectClick(Date);

                //From calender
                By fromYearDD = By.XPath("(//Select[@class='yearselect'])[2]");
                Driver.SleepTheThread(1);
                Driver.SelectValueFromOptionsTypeDD(fromYearDD, "2021");

                By fromMonthDD = By.XPath("(//Select[@class='monthselect'])[2]");
                string fromShortMonth = DateTime.Now.AddDays(-30).ToShortMonthName();
                logger.Log(Status.Info, "Select month : " + fromShortMonth);
                Driver.SelectNameFromOptionsTypeDD(fromMonthDD, fromShortMonth);

                int fromCurrentDateValue = DateTime.Today.AddDays(-30).Day;
                logger.Log(Status.Info, "Select day : " + fromCurrentDateValue);
                By fromDaySelect = By.XPath("//div[@class='calendar left']//table[@class='table-condensed']//tbody//tr//td[@class='available in-range day'or @class='available day']");
                Driver.SelectValueFromDDDirectClick(fromDaySelect, fromCurrentDateValue.ToString());

                logger.Log(Status.Info, "Enter from date");

                //To Calender
                logger.Log(Status.Info, "Select year : 2022");
                By toYearDD = By.XPath("(//Select[@class='yearselect'])[1]");
                Driver.SleepTheThread(1);
                Driver.SelectValueFromOptionsTypeDD(toYearDD, "2022");

                By toMonthDD = By.XPath("(//Select[@class='monthselect'])[1]");
                string toShortMonth = DateTime.Now.ToShortMonthName();
                logger.Log(Status.Info, "Select month : " + toShortMonth);
                Driver.SelectNameFromOptionsTypeDD(toMonthDD, toShortMonth);

                int toCurrentDateValue = DateTime.Today.Day;
                logger.Log(Status.Info, "Select day : " + toCurrentDateValue);

                By toDaySelect = By.XPath("//div[@class='calendar right']//table[@class='table-condensed']//tbody//tr//td[@class='available in-range day'or @class='available day']");
                Driver.SelectValueFromDDDirectClick(toDaySelect, toCurrentDateValue.ToString());

                Driver.SleepTheThread(1);
                By applyButton = By.XPath("//button[@class='applyBtn btn btn-small btn-success']");
                logger.Log(Status.Info, "Click apply button");
                Driver.DirectClick(applyButton);

                logger.Log(Status.Info, "Select facility : " + facility);
                By facilityDD = By.XPath("//button[@id='input_nROIFacilityID_handler']");
                Driver.DirectClick(facilityDD);
                Driver.ScrollIntoViewAndClick(By.XPath("//span[contains(text(),'[All]')]"));
                Driver.ScrollIntoViewAndClick(By.XPath("//span[contains(text(),'[All]')]"));
                Driver.ScrollIntoViewAndClick(By.XPath($"//span[contains(text(),'{facility}')]"));
                logger.Log(Status.Info, "Select Reporting Group : [NONE");
                Driver.DirectClick(By.XPath("//span[contains(text(),'Reporting Group:')]"));

                if (selectIncludeTest)
                {
                    logger.Log(Status.Info, "Check Include Test checkbox");
                    Driver.SelectCheckBoxIfUnchecked(includeTest);
                }
                logger.Log(Status.Info, "Click create report button");
                Driver.DirectClick(CreateReport);
                Driver.SleepTheThread(5);
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to create transactional model report, Exception detail: {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }
        /// <summary>
        /// Create new Reconciliation Report
        /// </summary>
        public void ApplyFilterAndCreateReport()
        {
            try
            {
                IWebElement frame = Driver.FindElementBy(reconciliationReportFrame);
                Driver.SwitchTo().Frame(frame);
                IWebElement datePicker = Driver.FindElementBy(selectDatePicker);
                datePicker.Click();
                Driver.SleepTheThread(1);
                Driver.Click(backArrow);
                Driver.Click(selectFromDate);
                Driver.Click(selectToDate);
                Driver.Click(btnApply);                               
                CheckIncludeTestCheckBox();
                Driver.SleepTheThread(1);
                SelectFacility(chkFacilityAll);
                Driver.SleepTheThread(4);
                Driver.Click(btnCreateReport);
                
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to create Reconciliation report : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }

        }

        /// <summary>
        /// Create new Reconciliation Report
        /// </summary>
        public void ReApplyFilterAndCreateReport()
        {
            try
            {
                ClickOnCustomize();
                IWebElement frame = Driver.FindElementBy(reconciliationReportFrame);
                Driver.SwitchTo().Frame(frame);
                IWebElement datePicker = Driver.FindElementBy(selectDatePicker);
                datePicker.Click();
                Driver.Click(lnkToday);
                datePicker.Click();
                SelectElement oSelect1 = new SelectElement(Driver.FindElementBy(selectMonthDrp));
                oSelect1.SelectByText("Jun");
                SelectElement oSelect2 = new SelectElement(Driver.FindElementBy(selectYearDrp));
                oSelect2.SelectByText("2020");
                Driver.Click(selectDate);                
                IWebElement applyButton = Driver.FindElementBy(btnApply);
                applyButton.Click();
                CheckIncludeTestCheckBox();
                SelectFacility(chkFacilityName);              
                Driver.Click(btnCreateReport);
                Driver.SleepTheThread(3);
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to create Reconciliation report : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }

        }

        /// <summary>
        /// Verify whether report contains all columns
        /// </summary>
        public bool IsColumnsVisibleUnderReport()
        {
            bool isVisible = false;
            try
            {
                IWebElement colXpressFacilityID = Driver.FindElementBy(colneXpressFacilityID);
                IWebElement colPendingLoggingCompleted = Driver.FindElementBy(colnPendingLoggingCompleted);
                IWebElement colInProcess = Driver.FindElementBy(colnInProcess);
                IWebElement colReleased = Driver.FindElementBy(colnReleased);
                IWebElement colShipped = Driver.FindElementBy(colnShipped);
                IWebElement colOpenIssuesOrActions = Driver.FindElementBy(colnOpenIssuesOrActions);

                if (colXpressFacilityID.Displayed == true && colPendingLoggingCompleted.Displayed == true
                    && colInProcess.Displayed == true && colReleased.Displayed == true
                    && colShipped.Displayed == true && colOpenIssuesOrActions.Displayed == true)
                {
                    isVisible = true;
                }

                return isVisible;
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to verify that 'Express Facility ID,Pending Logging Completion,In Process,Released,Shipped,Open Issues or Actions' columns are visible under report {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }

        }

        /// <summary>
        /// To check Include Test  check box
        /// </summary>
        public void CheckIncludeTestCheckBox()
        {
            try
            {
                bool isChecked = Driver.FindElementBy(chkIncludeTest).Selected;
                if (isChecked == false)
                {
                    IWebElement includeTestChk = Driver.FindElementBy(chkIncludeTest);
                    includeTestChk.Click();
                }

            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to check Include Test checkbox  with execption details as: {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
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

        public void ClickOnClearFilters()
        {
            try
            {
                ClickOnCustomize();
                IWebElement btClearFilters = Driver.FindElementBy(btnClearFilters);
                btClearFilters.Click();
                Driver.SwitchTo().DefaultContent();
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to click on clear filters button : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }

        /// <summary>
        /// Click on excel icon
        /// </summary>
        public void ClickOnExportToExcel()
        {
            logger.Log(Status.Info, "Click on export to excel icon");

            try
            {
                IWebElement icon = Driver.FindElementBy(lbl_Excel_icon);
                icon.Click();
                Driver.SleepTheThread(7);
            }
            catch (StaleElementReferenceException)
            {
                IWebElement icon = Driver.FindElementBy(lbl_Excel_icon);
                icon.Click();
                Driver.SleepTheThread(7);

            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to click on export to excel icon : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }


        /// <summary>
        /// Click on pdf icon
        /// </summary>
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



        /// <summary>
        /// Click on left arrow button
        /// </summary>
        public void ClickLeftArrowButtonForTotalRequestByRow(int rowNum)
        {
            try
            {
                Iframe frame = new Iframe(Driver, logger, Context);
                frame.switchToDefaut();
                frame.SwitchToRoiFrame();
                frame.SwitchToRDFrame();

                string leftArrowButton = $"//div[@id='div_total_requests_drilldown_table_condition_Row{rowNum}']//i[@id='lbl_Inputs_left_arrow_Row{rowNum}']";
                IWebElement arrowButton = Driver.FindElementBy(By.XPath(leftArrowButton));
                arrowButton.Click();
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to click left arrow button : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }


        public void ClickLeftArrowButtonForRequestCreatedByRow(int rowNum)
        {
            try
            {
                Iframe frame = new Iframe(Driver, logger, Context);
                frame.switchToDefaut();
                frame.SwitchToRoiFrame();
                frame.SwitchToRDFrame();
                string leftArrowButton = $"//div[@id='div_RequestCreated_drilldown_table_condition_Row{rowNum}']//i[@id='lbl_Inputs_left_arrow_Row{rowNum}']";
                IWebElement arrowButton = Driver.FindElementBy(By.XPath(leftArrowButton));
                arrowButton.Click();
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to click left arrow button : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }


        public void ClickLeftArrowButtonForNotSubmittedByRow(int rowNum)
        {
            try
            {
                Iframe frame = new Iframe(Driver, logger, Context);
                frame.switchToDefaut();
                frame.SwitchToRoiFrame();
                frame.SwitchToRDFrame();
                string leftArrowButton = $"//div[@id='div_NotSubmitted_drilldown_table_condition_Row{rowNum}']//i[@id='lbl_Inputs_left_arrow_Row{rowNum}']";
                IWebElement arrowButton = Driver.FindElementBy(By.XPath(leftArrowButton));
                arrowButton.Click();
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to click left arrow button : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }


        public void ClickLeftArrowButtonForRequestCreationFailedByRow(int rowNum)
        {
            try
            {
                Iframe frame = new Iframe(Driver, logger, Context);
                frame.switchToDefaut();
                frame.SwitchToRoiFrame();
                frame.SwitchToRDFrame();

                string leftArrowButton = $"//div[@id='div_RequestCreationFailed_drilldown_table_Row{rowNum}']//i[@id='lbl_Inputs_left_arrow_Row{rowNum}']";
                IWebElement arrowButton = Driver.FindElementBy(By.XPath(leftArrowButton));
                arrowButton.Click();
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to click left arrow button : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }

        public void ClickLeftArrowButtonForMissingInROIByRow(int rowNum)
        {
            try
            {
                Iframe frame = new Iframe(Driver, logger, Context);
                frame.switchToDefaut();
                frame.SwitchToRoiFrame();
                frame.SwitchToRDFrame();
                string leftArrowButton = $"//div[@id='div_MissingInROI_drilldown_table_Row{rowNum}']//i[@id='lbl_Inputs_left_arrow_Row{rowNum}']";
                IWebElement arrowButton = Driver.FindElementBy(By.XPath(leftArrowButton));
                arrowButton.Click();
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to click left arrow button : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }


        public void ClickLeftArrowButtonForTotalErrorsByRow(int rowNum)
        {
            try
            {
                Iframe frame = new Iframe(Driver, logger, Context);
                frame.switchToDefaut();
                frame.SwitchToRoiFrame();
                frame.SwitchToRDFrame();
                string leftArrowButton = $"//div[@id='div_TotalErrors_drilldown_table_Row{rowNum}']//i[@id='lbl_Inputs_left_arrow_Row{rowNum}']";
                IWebElement arrowButton = Driver.FindElementBy(By.XPath(leftArrowButton));
                arrowButton.Click();
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to click left arrow button : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }
        public string GetReportName(string reportName)
        {
            string sReportName = string.Empty;
            var dateString = DateTime.Now.ToString("yyyy-MM-dd");
            sReportName = reportName + dateString;
            return sReportName;

        }


        public int GetTotalRequestCountFromTableByRow(int rowNum)
        {
            string sTotalRequestsXpath = $"//span[@id='lblnTotalRequests_Row{rowNum}']";
            string sRequestsCount = Driver.GetText(By.XPath(sTotalRequestsXpath));
            int requestCount = Convert.ToInt32(sRequestsCount);
            return requestCount;
        }

        public int GetDetailViewCountForTotalRequestsByRow()
        {
            int rowCount = 0;
            IWebElement table = Driver.FindElement(By.XPath("//table[@id='dtReconciliationDrillDown']"));
            ReadOnlyCollection<IWebElement> allRows = table.FindElements(By.TagName("tr"));
            rowCount = allRows.Count;
            return rowCount - 1;
        }

        public string[] DownloadPDFAndGetReportData(string folderName)
        {
            string fileURLName = string.Empty;
            WebClient webClient = new WebClient();
            string filePath = folderName;
            fileURLName = ClickOnExportToPdfAndGetReportName();
            string completePath = filePath + "MRO eXpress Reconciliation Report.pdf";
            webClient.DownloadFile(fileURLName, completePath);
            string[] sbData = GetDataFromPDFFile(completePath);
            int sCount = sbData.Length;
            var sData = sbData[sCount - 3];
            string[] sPdfData = sData.Split(' ');
            return sPdfData;            
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
            return sWords;
        }



        public bool ValidateDetailView()
        {
            bool isRequestsValidated = false;
            try
            {
                int totalPages = 0;
                int tableRowCount = 0;
                int detailViewRowsCount = 0;
                List<IWebElement> tableData = Driver.FindElementsBy(By.XPath("//table[@id='dtReconciliationReportSummary']//tr[not(contains(@style,'display:none;'))]"));
                for (int z = 0; z < tableData.Count - 1; z++)
                {
                    ReadOnlyCollection<IWebElement> cells = tableData[z].FindElements(By.TagName("td"));
                    for (int y = 0; y < cells.Count; y++)
                    {
                      
                        if (y == 3)
                        {
                            tableRowCount = GetTotalRequestCountFromTableByRow(z);
                            OpenDetailViewForTotalRequests(z);
                            totalPages = GetTotalPagesCount();
                            detailViewRowsCount = GetFinalCount(totalPages);
                            if (tableRowCount == detailViewRowsCount)
                            {
                                isRequestsValidated = true;
                                ClickLeftArrowButtonForTotalRequestByRow(z);
                                logger.Log(Status.Info, $"Verified that detail view of total requests({detailViewRowsCount}) generated the same number of requests({tableRowCount}) for each number hyperlink under Row-{z}");
                            }
                            else
                            {
                                logger.Log(Status.Fail, $"Verified that detail view of total requests({detailViewRowsCount}) not generated the same number of requests({tableRowCount}) for each number hyperlink under Row-{z}");
                            }
                        }
                        else if (y == 4)
                        {
                            tableRowCount = GetRequestCreatedCountFromTableByRow(z);
                            OpenDetailViewForRequestCreated(z);
                            totalPages = GetTotalPagesCount();
                            detailViewRowsCount = GetFinalCount(totalPages);
                            if (tableRowCount == detailViewRowsCount)
                            {
                                isRequestsValidated = true;
                                ClickLeftArrowButtonForRequestCreatedByRow(z);
                                logger.Log(Status.Info, $"Verified that detail view of requests created({detailViewRowsCount}) generated the same number of requests({tableRowCount}) for each number hyperlink under Row-{z} ");
                            }
                            else
                            {
                                logger.Log(Status.Fail, $"Verified that detail view of requests created({detailViewRowsCount}) not generated the same number of requests({tableRowCount}) for each number hyperlink under Row-{z} ");
                            }
                        }
                        else if (y == 5)
                        {
                            tableRowCount = GetNotSubmittedCountFromTableByRow(z);
                            OpenDetailViewForNotSubmitted(z);
                            totalPages = GetTotalPagesCount();
                            detailViewRowsCount = GetFinalCount(totalPages);
                            if (tableRowCount == detailViewRowsCount)
                            {
                                isRequestsValidated = true;
                                ClickLeftArrowButtonForNotSubmittedByRow(z);
                                logger.Log(Status.Info, $"Verified that detail view of not submitted({detailViewRowsCount}) generated the same number of requests({tableRowCount}) for each number hyperlink under Row-{z} ");
                            }
                            else
                            {
                                logger.Log(Status.Fail, $"Verified that detail view of not submitted({detailViewRowsCount}) not generated the same number of requests({tableRowCount}) for each number hyperlink under Row-{z} ");
                            }
                        }

                        else if (y == 11)
                        {
                            tableRowCount = GetRequestCreationFailedFromTableByRow(z);
                            OpenDetailViewForRequestCreationFailed(z);
                            totalPages = GetTotalPagesCount();
                            detailViewRowsCount = GetFinalCount(totalPages);
                            if (tableRowCount == detailViewRowsCount)
                            {
                                isRequestsValidated = true;
                                ClickLeftArrowButtonForRequestCreationFailedByRow(z);
                                logger.Log(Status.Info, $"Verified that detail view of request creation failed({detailViewRowsCount}) generated the same number of requests({tableRowCount}) for each number hyperlink under Row-{z} ");
                            }
                            else
                            {
                                logger.Log(Status.Fail, $"Verified that detail view of request creation failed({detailViewRowsCount}) not generated the same number of requests({tableRowCount}) for each number hyperlink under Row-{z} ");
                            }
                        }

                        else if (y == 12)
                        {
                            tableRowCount = GetMissingInROIFromTableByRow(z);
                            OpenDetailViewForMissingInROI(z);
                            totalPages = GetTotalPagesCount();
                            detailViewRowsCount = GetFinalCount(totalPages);
                            if (tableRowCount == detailViewRowsCount)
                            {
                                isRequestsValidated = true;
                                ClickLeftArrowButtonForMissingInROIByRow(z);
                                logger.Log(Status.Info, $"Verified that detail view of missing in roi({detailViewRowsCount}) generated the same number of requests({tableRowCount}) for each number hyperlink under Row-{z} ");
                            }
                            else
                            {
                                logger.Log(Status.Fail, $"Verified that detail view of missing in roi({detailViewRowsCount}) not generated the same number of requests({tableRowCount}) for each number hyperlink under Row-{z} ");
                            }
                        }

                        else if (y == 13)
                        {
                            tableRowCount = GetTotalErrorsFromTableByRow(z);
                            OpenDetailViewForTotalErrors(z);
                            totalPages = GetTotalPagesCount();
                            detailViewRowsCount = GetFinalCount(totalPages);
                            if (tableRowCount == detailViewRowsCount)
                            {
                                isRequestsValidated = true;
                                ClickLeftArrowButtonForTotalErrorsByRow(z);
                                logger.Log(Status.Info, $"Verified that detail view of total errors({detailViewRowsCount}) generated the same number of requests({tableRowCount}) for each number hyperlink under Row-{z} ");
                            }
                            else
                            {
                                logger.Log(Status.Fail, $"Verified that detail view of total errors({detailViewRowsCount}) not generated the same number of requests({tableRowCount}) for each number hyperlink under Row-{z} ");
                            }
                        }
                    }
                }
            }

            catch (Exception ex)
            {
                throw new Exception($"Failed to validate detail view of requests: {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
            return isRequestsValidated;
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
            IWebElement table = Driver.FindElement(By.XPath("//table[@id='dtReconciliationDrillDown']"));
            ReadOnlyCollection<IWebElement> allRows = table.FindElements(By.TagName("tr"));
            lastPageRowCount = allRows.Count - 1;
            totalRequestsCount = lastPageRowCount + countByNumberOfPages;
            return totalRequestsCount;

        }


        public void ClickOnCustomize()
        {
            try
            {
                Iframe frame = new Iframe(Driver, logger, Context);
                frame.switchToDefaut();
                frame.SwitchToRoiFrame();
                frame.SwitchToRDFrame();
                Driver.DirectClick(lnkCustomize);
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to click the customize link : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }


        public void OpenDetailViewForTotalRequests(int rowNum)
        {
            string requestsXpath = $"//td[@id ='colnTotalRequests_Row{rowNum}']";
            Driver.DirectClick(By.XPath(requestsXpath));
            string sFrameXpath = $"//iframe[@id='sr_dtTotalRequests_Row{rowNum}']";
            IWebElement frame = Driver.FindElementBy(By.XPath(sFrameXpath));
            Driver.SwitchTo().Frame(frame);
        }


        public void OpenDetailViewForRequestCreated(int rowNum)
        {
            string sRequestCreatedXpath = $"//td[@id ='colnRequestCreated_Row{rowNum}']";
            string sFrameXpath = $"//iframe[@id='sr_dtRequestCreated_Row{rowNum}']";
            Driver.DirectClick(By.XPath(sRequestCreatedXpath));
            Driver.SleepTheThread(3);
            IWebElement frame = Driver.FindElementBy(By.XPath(sFrameXpath));
            Driver.SwitchTo().Frame(frame);
        }


        public void OpenDetailViewForNotSubmitted(int rowNum)
        {
            string sRequestCreatedXpath = $"//td[@id ='colnNotSubmitted_Row{rowNum}']";
            string sFrameXpath = $"//iframe[@id='sr_dtNotSubmitted_Row{rowNum}']";
            Driver.DirectClick(By.XPath(sRequestCreatedXpath));
            Driver.SleepTheThread(4);
            IWebElement frame = Driver.FindElementBy(By.XPath(sFrameXpath));
            Driver.SwitchTo().Frame(frame);
        }


        public void OpenDetailViewForRequestCreationFailed(int rowNum)
        {
            string sRequestCreatedXpath = $"//td[@id ='colnRequestCreationFailed_Row{rowNum}']";
            string sFrameXpath = $"//iframe[@id='sr_dtRequestCreationFailed_Row{rowNum}']";
            Driver.DirectClick(By.XPath(sRequestCreatedXpath));
            Driver.SleepTheThread(4);
            IWebElement frame = Driver.FindElementBy(By.XPath(sFrameXpath));
            Driver.SwitchTo().Frame(frame);
        }

        public void OpenDetailViewForTotalErrors(int rowNum)
        {
            string sRequestCreatedXpath = $"//td[@id ='colnTotalErrors_Row{rowNum}']";
            string sFrameXpath = $"//iframe[@id='sr_dtTotalErrors_Row{rowNum}']";
            Driver.DirectClick(By.XPath(sRequestCreatedXpath));
            Driver.SleepTheThread(4);
            IWebElement frame = Driver.FindElementBy(By.XPath(sFrameXpath));
            Driver.SwitchTo().Frame(frame);
        }


        public void OpenDetailViewForMissingInROI(int rowNum)
        {
            string sRequestCreatedXpath = $"//td[@id ='colnMissingInROI_Row{rowNum}']";
            string sFrameXpath = $"//iframe[@id='sr_dtMissingInROI_Row{rowNum}']";
            Driver.DirectClick(By.XPath(sRequestCreatedXpath));
            Driver.SleepTheThread(4);
            IWebElement frame = Driver.FindElementBy(By.XPath(sFrameXpath));
            Driver.SwitchTo().Frame(frame);
        }
        public int GetTotalPagesCount()
        {
            string tRequestsXpath = "//span[@id='dtReconciliationDrillDown-PageOfPages']";
            string sRequestsCount = Driver.GetText(By.XPath(tRequestsXpath));
            int requestCount = Convert.ToInt32(sRequestsCount);
            return requestCount;
        }

        public int GetRequestCreatedCountFromTableByRow(int rowNum)
        {
            string sRequestsCreatedXpath = $"//span[@id='lblnRequestCreated_Row{rowNum}']";
            string sRequestCreatedCount = Driver.GetText(By.XPath(sRequestsCreatedXpath));
            int requestCount = Convert.ToInt32(sRequestCreatedCount);
            return requestCount;
        }

        public int GetNotSubmittedCountFromTableByRow(int rowNum)
        {
            string sNotSubmittedXpath = $"//span[@id='lblnNotSubmitted_Row{rowNum}']";
            string sNotSubmittedCount = Driver.GetText(By.XPath(sNotSubmittedXpath));
            int requestCount = Convert.ToInt32(sNotSubmittedCount);
            return requestCount;
        }


        public int GetRequestCreationFailedFromTableByRow(int rowNum)
        {
            string sRequestCreationFailedXpath = $"//span[@id='lblnRequestCreationFailed_Row{rowNum}']";
            string sRequestCreationFailedCount = Driver.GetText(By.XPath(sRequestCreationFailedXpath));
            int requestCount = Convert.ToInt32(sRequestCreationFailedCount);
            return requestCount;
        }

        public int GetMissingInROIFromTableByRow(int rowNum)
        {
            string sMissingInROIXpath = $"//span[@id='lblnMissingInROI_Row{rowNum}']";
            string sMissingInROICount = Driver.GetText(By.XPath(sMissingInROIXpath));
            int requestCount = Convert.ToInt32(sMissingInROICount);
            return requestCount;
        }

        public int GetTotalErrorsFromTableByRow(int rowNum)
        {
            string sTotalErrorsXpath = $"//span[@id='lblnTotalErrors_Row{rowNum}']";
            string sTotalErrorsCount = Driver.GetText(By.XPath(sTotalErrorsXpath));
            int requestCount = Convert.ToInt32(sTotalErrorsCount);
            return requestCount;
        }


        public int GetFullCountOfTotalRequests()
        {
            string sTotalRequestsXpath = "//td[@id='cellnTotalRequests']";
            string sRequestsCount = Driver.GetText(By.XPath(sTotalRequestsXpath));
            int requestCount = Convert.ToInt32(sRequestsCount);
            return requestCount;
        }

        public int GetFullCountOfRequestCreated()
        {
            string sRequestsCreatedXpath = "//td[@id='cellnRequestCreated']";
            string sRequestCreatedCount = Driver.GetText(By.XPath(sRequestsCreatedXpath));
            int requestCount = Convert.ToInt32(sRequestCreatedCount);
            return requestCount;
        }

        //
        public int GetFullCountOfNotSubmitted()
        {
            string sNotSubmittedXpath = "//td[@id='cellnNotSubmitted']";
            string sNotSubmittedCount = Driver.GetText(By.XPath(sNotSubmittedXpath));
            int requestCount = Convert.ToInt32(sNotSubmittedCount);
            return requestCount;
        }
        //



        public int GetFullCountOfPendingLoggingCompleted()
        {
            string sPendingLoggingCompletedXpath = "//td[@id='cellnPendingLoggingCompleted']";
            string sPendingLoggingCompletedCount = Driver.GetText(By.XPath(sPendingLoggingCompletedXpath));
            int requestCount = Convert.ToInt32(sPendingLoggingCompletedCount);
            return requestCount;
        }

        public int GetFullCountOfInprocess()
        {
            string sInProcessXpath = "//td[@id='cellnInProcess']";
            string sInProcessCount = Driver.GetText(By.XPath(sInProcessXpath));
            int requestCount = Convert.ToInt32(sInProcessCount);
            return requestCount;
        }

        public int GetFullCountOfReleased()
        {
            string sReleasedXpath = "//td[@id='cellnReleased']";
            string sReleasedCount = Driver.GetText(By.XPath(sReleasedXpath));
            int requestCount = Convert.ToInt32(sReleasedCount);
            return requestCount;
        }

        public int GetFullCountOfShipped()
        {
            string sShippedXpath = "//td[@id='cellnShipped']";
            string sShippedCount = Driver.GetText(By.XPath(sShippedXpath));
            int requestCount = Convert.ToInt32(sShippedCount);
            return requestCount;
        }

        public int GetFullCountOfOpenIssuesOrActions()
        {
            string sOpenIssuesOrActionsXpath = "//td[@id='cellnOpenIssuesOrActions']";
            string sOpenIssuesOrActionsCount = Driver.GetText(By.XPath(sOpenIssuesOrActionsXpath));
            int requestCount = Convert.ToInt32(sOpenIssuesOrActionsCount);
            return requestCount;
        }

        public int GetFullCountOfRequestCreationFailed()
        {
            string sRequestCreationFailedXpath = "//td[@id='cellnRequestCreationFailed']";
            string sRequestCreationFailedCount = Driver.GetText(By.XPath(sRequestCreationFailedXpath));
            int requestCount = Convert.ToInt32(sRequestCreationFailedCount);
            return requestCount;
        }

        public int GetFullCountOfMissingInROI()
        {
            string sMissingInROIXpath = "//td[@id='cellnMissingInROI']";
            string sMissingInROICount = Driver.GetText(By.XPath(sMissingInROIXpath));
            int requestCount = Convert.ToInt32(sMissingInROICount);
            return requestCount;
        }

        public int GetFullCountOfTotalErrors()
        {
            string sTotalErrorsXpath = "//td[@id='cellnTotalErrors']";
            string sTotalErrorsCount = Driver.GetText(By.XPath(sTotalErrorsXpath));
            int requestCount = Convert.ToInt32(sTotalErrorsCount);
            return requestCount;
        }

        public bool isReportFiltered()
        {
            bool isResults = false;
            int rCount = 0;
            List<IWebElement> tableData = Driver.FindElementsBy(By.XPath("//table[@id='dtReconciliationReportSummary']//tr[not(contains(@style,'display:none;'))]"));
            rCount = tableData.Count;
            if (rCount > 0) isResults = true;
            Driver.SwitchTo().DefaultContent();
            return isResults;
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

        public void SelectFacility(By chkElement)
        {
            Driver.Click(btnFacilityList);
            Driver.SleepTheThread(3);
            bool isChecked = Driver.FindElementBy(chkElement).Selected;
            if (isChecked == false)
            {
                IWebElement iFacilityChk = Driver.FindElementBy(chkElement);
                iFacilityChk.Click();
            }
                 

        }

        public int ValidateDetailViewForTotalRequests()
        {
            bool isRequestsValidated = false;
            int detailViewRowsCount = 0;
            try
            {
                int totalPages = 0;
                int tableRowCount = 0;               
                string roiRequestID = string.Empty;
                List<IWebElement> tableData = Driver.FindElementsBy(By.XPath("//table[@id='dtReconciliationReportSummary']//tbody//tr[not(contains(@style,'display:none;'))]"));
                for (int z = 0; z < tableData.Count; z++)
                {
                    ReadOnlyCollection<IWebElement> cells = tableData[z].FindElements(By.TagName("td"));
                    for (int y = 0; y < cells.Count; y++)
                    {
                        if (y == 3)
                        {
                            z = z + 1;
                            tableRowCount = GetTotalRequestCountFromTableByRow(z);
                            OpenDetailViewForTotalRequests(z);
                            totalPages = GetTotalPagesCount();                           
                            for (int k = 0; k < totalPages; k++)
                            {
                                IWebElement table = Driver.FindElement(By.XPath("//table[@id='dtReconciliationDrillDown']"));
                                ReadOnlyCollection<IWebElement> allRows = table.FindElements(By.TagName("tr"));
                                if(tableRowCount==0)
                                {
                                    isRequestsValidated = true;
                                }
                                for (int h = 0; h < allRows.Count; h++)
                                {
                                    if (h > 0)
                                    {
                                        ReadOnlyCollection<IWebElement> icells = allRows[h].FindElements(By.TagName("td"));
                                        roiRequestID = icells[4].Text;
                                        bool isRequestIdNull = string.IsNullOrEmpty(roiRequestID);
                                        if (isRequestIdNull) { isRequestsValidated = false; }
                                        else
                                        {
                                            isRequestsValidated = true;
                                        }

                                    }
                                }
                            }
                        }                       
                    }
                }
                if (isRequestsValidated)
                {
                    logger.Log(Status.Info, $"Verified each request in the detail window has an ROI Request ID associated with it");
                    detailViewRowsCount = tableRowCount;
                }

                else {

                    logger.Log(Status.Info, $"Verification failed as some of the requests in the detail window does not have ROI Request ID associated with it");

                }
            }

            catch (Exception ex)
            {
                throw new Exception($"Failed to validate detail view of requests: {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }

            return detailViewRowsCount;
        }

        public bool ValidateEachRowWithRequestID()
        {
            bool isRequestIdAvailable = false;
            int totalRows = 0;
            IWebElement table = Driver.FindElement(By.XPath("//table[@id='dtReconciliationDrillDown']"));
            ReadOnlyCollection<IWebElement> allRows = table.FindElements(By.TagName("tr"));
            totalRows = allRows.Count;

            return isRequestIdAvailable;
        }
   
        public int DownloadAndValidateRequestDataFromExcel(string folder)
        {
            int lastRowNumber = 0;
            try
            {
                
                Automation.Common.Iframe frame = new Automation.Common.Iframe(Driver, logger, Context);
                frame.SwitchToTotalRequestsFrame();
                string todayDate = string.Format("{0:yyyy/MM/dd}", DateTime.Now);
                string sReportDate = todayDate.Replace('/', '-');
                IWebElement icon = Driver.FindElementBy(By.XPath("//span[@id='div_Excel_icon']"));
                icon.Click();
                Driver.SleepTheThread(5);
                string excelFileName = folder + $"ReconciliationReportDetail_{sReportDate}.xlsx";
                ExcelReaderFile excelReaderFile = new ExcelReaderFile(excelFileName);
                lastRowNumber = excelReaderFile.getRowCount("Sheet1");
                ExcelReaderFile.DeleteExistingFiles(folder, "xlsx", "");
                
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to download right excel file exception details as: {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
            return lastRowNumber - 3;
        }

        public void CheckAllFacilities()
        {
            try
            {
                bool isChecked = Driver.FindElementBy(chkIncludeTest).Selected;
                if (isChecked == false)
                {
                    IWebElement includeTestChk = Driver.FindElementBy(chkIncludeTest);
                    includeTestChk.Click();
                }

            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to check All checkbox  with exception details as: {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }

        }

    }
    }

