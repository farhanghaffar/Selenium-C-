using AventStack.ExtentReports;
using DataDrivenProject;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MRO.ROI.Automation.Selenium;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;

namespace MRO.ROI.Automation.Pages
{
    public class ROIFacilityDocumentsRequiredPage
    {
        public RemoteWebDriver Driver;
        public ExtentTest logger;
        public TestContext Context;
        public ROIFacilityDocumentsRequiredPage(RemoteWebDriver driver, ExtentTest _loger, TestContext _context)
        {
            Driver = driver;
            logger = _loger;
            Context = _context;
        }

        public By AllTab=By.XPath("//span[contains(text(),'All')]");
        public By loggedFromDate = By.XPath("//input[@id='mrocontent_txtFrom']");
        public By loggedToDate = By.XPath("//input[@id='mrocontent_txtTo']");
        public By locationDrp = By.XPath("//input[@id='mrocontent_lstLocation_Input']");
        public By createReportBtn = By.XPath("//input[@id='mrocontent_cmdCreate']");
        public By clearAllFieldsBtn = By.XPath("//input[@id='mrocontent_cmdClearAll']");
        public By excelIcon = By.XPath("//img[@alt='Export to Excel']");
        public By requesterTypeDrp = By.XPath("//select[@id='mrocontent_lstRqrType']");
        public By rqrTypeVal = By.XPath("//*[@id='mrocontent_dgRequest']/tbody/tr[2]/td[17]");
        public By chkIncludePHIInExport = By.XPath("//input[@id='mrocontent_cbIncludePHI']");
        public By columnsList = By.XPath("//tr[@class='TableHeader']//td");
        public void ClickOnAll()
        {
            try
            {
                Driver.Click(AllTab);
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to click all tab with message as : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }

        public void CreateReport(string type)
        {
            try
            {
                Driver.Click(clearAllFieldsBtn);
                Driver.ClearText(loggedFromDate);
                Driver.SendKeys(loggedFromDate, "12/01/2017");
                Driver.ClearText(loggedToDate);
                Driver.SendKeys(loggedToDate, "06/03/2020");
                Driver.SendKeys(locationDrp, "[All Locations]");
                Driver.Wait(TimeSpan.FromSeconds(5));
                SelectElement oSelect1 = new SelectElement(Driver.FindElementBy(requesterTypeDrp));
                oSelect1.SelectByText(type);                
                Driver.Wait(TimeSpan.FromSeconds(2));
                Driver.Click(createReportBtn);
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to create report with message as : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }
        public void ClickOnExportToExcelIcon()
        {
            try
            {
                Driver.Click(excelIcon);
            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to click excel icon with message as : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }

        public string VerifyRqrType()
        {
            try
            {
               string type= Driver.GetText(rqrTypeVal);
               return type;
            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to verify rqr type with message as : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }

        public bool CheckIncludePHIinexportOption()
        {
            try
            {
                bool isVisible = Driver.FindElementBy(chkIncludePHIInExport).Displayed;
                if (isVisible == false)
                {
                    Driver.Click(chkIncludePHIInExport);
                }
                else
                {
                    isVisible = true;
                }

                return isVisible;
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to get visibility of Include PHI In Export checkbox with message : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }

        public bool isAllColumnsVisible()
        {
            bool isVisible = false;
            try
            {
                List<IWebElement> tableData = Driver.FindElementsBy(columnsList);
                for (int z = 0; z < tableData.Count; z++)
                {
                    string dateColum = tableData[z].Text;

                    if (dateColum == "Patient" || dateColum == "MRN" || dateColum == "DOB" || dateColum == "PAN" || dateColum == "DOS From" || dateColum == "DOS To")
                    {
                        isVisible = true;
                    }
                }

            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to verify all the columns with message : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
            return isVisible;
        }


        public bool DownloadAndValidateColumnsFromExcel(string folder)
        {
            bool doesAllColumnsExist = false;
            ExcelReaderFile excelReaderFile = new ExcelReaderFile();
            excelReaderFile.ConvertXLS_XLSX(folder + "DocumentRequired.xls");
            ExcelReaderFile.DeleteExistingFiles(folder, "xls", "DocumentRequired");
            string excelFileName = folder + "DocumentRequired.xlsx";
            var isPatientColumnExists = excelReaderFile.SearchTextinExcelBySheetIndex(excelFileName, 1, "Patient Last Name");

            var isMRNColumnExists = excelReaderFile.SearchTextinExcelBySheetIndex(excelFileName, 1, "MRN");

            var isDOBColumnExists = excelReaderFile.SearchTextinExcelBySheetIndex(excelFileName, 1, "DOB");

            var isPANColumnExists = excelReaderFile.SearchTextinExcelBySheetIndex(excelFileName, 1, "PAN");

            var isDOSFromColumnExists = excelReaderFile.SearchTextinExcelBySheetIndex(excelFileName, 1, "DOS From");

            var isDOSToColumnExists = excelReaderFile.SearchTextinExcelBySheetIndex(excelFileName, 1, "DOS To");

            if (isPatientColumnExists == false && isMRNColumnExists == false && isDOBColumnExists == false && isPANColumnExists == false)
            {
                doesAllColumnsExist = false;
            }
            else if (isPatientColumnExists == true && isMRNColumnExists == true && isDOBColumnExists == true && isPANColumnExists == true)
            {
                doesAllColumnsExist = true;
            }
            ExcelReaderFile.DeleteExistingFiles(folder, "xlsx", "DocumentRequired.xlsx");
            return doesAllColumnsExist;
        }
    }
}
