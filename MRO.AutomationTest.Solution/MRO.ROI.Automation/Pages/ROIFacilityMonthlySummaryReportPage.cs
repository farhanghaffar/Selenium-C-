using AventStack.ExtentReports;
using DataDrivenProject;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MRO.ROI.Automation.Selenium;
using MRO.ROI.Automation.Utility;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Reflection;
using static MRO.ROI.Automation.Utility.IniFile;

namespace MRO.ROI.Automation.Pages
{
    public class ROIFacilityMonthlySummaryReportPage
    {
        public RemoteWebDriver Driver;
        public ExtentTest logger;
        public TestContext Context;
        public CSVReader csvReader;

        public ROIFacilityMonthlySummaryReportPage(RemoteWebDriver driver, ExtentTest _loger, TestContext _context)
        {
            Driver = driver;
            logger = _loger;
            Context = _context;
        }
        public By monthlySummaryReportFrame = By.XPath("//iframe[starts-with(@id,'rdFrame')]");
        public By selectDatePicker = By.XPath("(//span[@id='daterange'])[1]");
        public By selectFromDate = By.XPath("((//table[@class='table-condensed'])[2]//tr//td[contains(text(),'1')])[1]");
        public By selectToDate = By.XPath("((//table[@class='table-condensed'])[1]//tr//td[contains(text(),'1')])[13]");
        public By btnApply = By.XPath("//button[text()='Apply']");
        public By lnkToday = By.XPath("//div[@class='ranges']/ul/li[1]");
        public By drpReportingGroup = By.XPath("//select[@id='nFacilityReportingGroupID']");
        public By btnCreateReport = By.XPath("//input[@id='btn_submit']");
        public By rdFrame = By.XPath("//iframe[starts-with(@id,'rdFrame')]");
        public By lnkCustomize = By.XPath("//a[@id='show_div_user_inputs']/span");
        public By chkDeliveryMethod = By.XPath("//input[@id ='nRequestTypeMulti_check_all']");
        public By btnDeliveryMethod = By.XPath("//button[@id='nRequestTypeMulti_handler']");
        public By drpUserType = By.XPath("//select[@id='nUserTypeID']");
        public By selectMonthDrp = By.XPath("//div[@class ='calendar left']//select[@class='monthselect']");
        public By selectYearDrp = By.XPath("//div[@class ='calendar left']//select[@class='yearselect']");
        public By selectDate = By.XPath("//div[@class='calendar left']//table[@class='table-condensed']//tbody/tr[1]/td[2]");

        public By drpLocation = By.XPath("//select[@id='nLocationID']");
        public By drpType = By.XPath("//select[@id='nLocationType']");
        public By drpBreakoutBy = By.XPath("//select[@id='nBreakoutMonthLoc']");
        public By chkIncludePHIInExport = By.XPath("//input[@id='bIncludePHI']");
        public By lbl_Excel_icon = By.XPath("//i[@id='lbl_Excel_icon']");
        public By lbl_PDF_icon = By.XPath("//i[@id='lbl_PDF_icon']");
        /// <summary>
        /// Create Monthly Summary Report
        /// </summary>
        public void CreateMonthlySummaryReport()
        {
            try
            {
                IWebElement frame = Driver.FindElementBy(monthlySummaryReportFrame);
                Driver.SwitchTo().Frame(frame);
                IWebElement datePicker = Driver.FindElementBy(selectDatePicker);
                datePicker.Click();
                // Driver.Click(selectFromDate);
                // Driver.Click(selectToDate);
                Driver.Click(lnkToday);
                var reportingGroup = Driver.FindElementBy(drpReportingGroup);
                var selectReportingGroup = new SelectElement(reportingGroup);
                selectReportingGroup.SelectByText("[None]");
                //delivery method
                Driver.Click(btnDeliveryMethod);
                Driver.Click(chkDeliveryMethod);
                //
                var selectLocation = Driver.FindElementBy(drpLocation);
                var selectLocations = new SelectElement(selectLocation);
                selectLocations.SelectByText("[All]");
                //
                var selectType = Driver.FindElementBy(drpType);
                var selectTypes = new SelectElement(selectType);
                selectTypes.SelectByText("[All]");
                var selectBreakout = Driver.FindElementBy(drpBreakoutBy);
                var selectBreakouts = new SelectElement(selectBreakout);
                selectBreakouts.SelectByText("[None]");
                Driver.FindElementBy(btnCreateReport).Click();
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to create mothly summary report with exception detail as : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }

        }


        public bool OpenDetailWindowAndValidateIncluePHIInExportOption()
        {
            bool isOptionAvailable = false;
            bool isChecked = false;
            try
            {
                int tableRowCount = 0;
                string roiRequestID = string.Empty;
                List<IWebElement> tableData = Driver.FindElementsBy(By.XPath("//table[@id='dtMonthlySummary']//tbody//tr[not(contains(@style,'display:none;'))]"));
                for (int z = 0; z < tableData.Count; z++)
                {
                    ReadOnlyCollection<IWebElement> cells = tableData[z].FindElements(By.TagName("td"));
                    if (cells.Count > 0)
                    {
                        for (int y = 0; y < cells.Count; y++)
                        {
                            if (y == 2)
                            {
                                z = z + 1;
                                tableRowCount = GetTotalRequestCountFromTableByRow(z);
                                OpenDetailViewForTotalRequests(z);
                                isChecked = Driver.FindElementBy(chkIncludePHIInExport).Selected;
                            }
                        }
                    }
                    break;
                }
                if (isChecked) { isOptionAvailable = true; }
            }

            catch (Exception ex)
            {
                throw new Exception($"Failed to validate Include PHI In export checkbox:{ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }

            return isOptionAvailable;
        }

        public int GetTotalRequestCountFromTableByRow(int rowNum)
        {
            string sTotalRequestsXpath = $"//span[@id='lblnLegal_Row{rowNum}']";
            string sRequestsCount = Driver.GetText(By.XPath(sTotalRequestsXpath));
            int requestCount = Convert.ToInt32(sRequestsCount);
            return requestCount;
        }

        public void OpenDetailViewForTotalRequests(int rowNum)
        {
            string requestsXpath = $"//td[@id ='colnLegal_Row{rowNum}']";
            Driver.DirectClick(By.XPath(requestsXpath));
            string sFrameXpath = $"//iframe[@id='sr_dtLegalTotalDetail_Row{rowNum}']";
            IWebElement frame = Driver.FindElementBy(By.XPath(sFrameXpath));
            Driver.SwitchTo().Frame(frame);
        }

        public bool DownloadAndValidateColumnsFromExcel(string folder)
        {
            bool isColumnsVisible = false;
            List<IWebElement> excelLinks = Driver.FindElementsBy(lbl_Excel_icon);
            excelLinks[0].Click();
            Driver.SleepTheThread(10);
            string excelFileName = folder + "Monthly Summary Report.xlsx";
            ExcelReaderFile excelReaderFile = new ExcelReaderFile(excelFileName);
            var isColumnExists = excelReaderFile.SearchTextinExcelBySheetIndex(excelFileName, 1, "SSN");
            if (isColumnExists == false)
            {
                isColumnsVisible = false;
            }
            else if (isColumnExists == true)
            {
                isColumnsVisible = true;
            }

            ExcelReaderFile.DeleteExistingFiles(folder, "xlsx", "Monthly Summary Report.xlsx");
            return isColumnsVisible;
        }

        public void CheckIncludePHIinexportOption()
        {
            try
            {
                bool isCheck = Driver.FindElementBy(chkIncludePHIInExport).Selected;
                if (isCheck == false)
                {
                    Driver.Click(chkIncludePHIInExport);
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to check Include PHI In Export checkbox with message : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }


    }
}
