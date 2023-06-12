using AventStack.ExtentReports;
using DataDrivenProject;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MRO.ROI.Automation.Selenium;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MRO.ROI.Automation.Pages
{
   public class ROIAdminDeferredProcessPage
    {
        public RemoteWebDriver Driver;
        public ExtentTest logger;
        public TestContext Context;
        public ROIAdminDeferredProcessPage(RemoteWebDriver driver, ExtentTest _loger, TestContext _context)
        {
            Driver = driver;
            logger = _loger;
            Context = _context;
        }

        public By Run = By.XPath("//tbody/tr[@id='mrocontent_RadGridProcess_ctl00__0']/td[6]/input[1]");
        public By ReturnToROIInvoice = By.XPath("//a[@id='mrocontent_lnkReturn']");




        public void ClickReturnToROIInvoice()
        {
            try
            {
                IWebElement clickReturnToROIInvoice = Driver.FindElementBy(ReturnToROIInvoice);
                clickReturnToROIInvoice.Click();
                
            }
            catch (Exception ex)
            {

                throw new Exception($"Failed with Message Unable to verify Header : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }


        public void  GetPatientNameFromExcel()
        {
            //string patientName = string.Empty;
            string currentReportName = string.Empty;
            string userRoot = System.Environment.GetEnvironmentVariable("USERPROFILE");
            string downloadFolder = Path.Combine(userRoot, "Downloads\\");
            try
            {              
                
                ExcelReaderFile excelReaderFile = new ExcelReaderFile();
                Microsoft.Office.Interop.Excel.Range RowData = excelReaderFile.ReturnAllRowsOfExcel(downloadFolder + $"({ currentReportName}).xlsx");
                    
                foreach(var row in RowData)
                {
                    if (row.Equals("Patient Name"))
                    {
                        string patientName = row.ToString();
                    }
                }

            }
            catch (Exception ex)
            {

                throw new Exception($"Failed with Message Unable to verify Header : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
            
        }

        //public void GetFileNameFromDownloads()
        //{
        //    //string FileName = "Curahealth_883_2021Oct_ROIInvoiceQB";
        //    string currentReportName = string.Empty;
        //    string userRoot = System.Environment.GetEnvironmentVariable("USERPROFILE");
        //    string downloadFolder = Path.Combine(userRoot, "Downloads\\");
        //    try
        //    {

                

        //        //ExcelReaderFile excelReaderFile = new ExcelReaderFile();
        //        //Microsoft.Office.Interop.Excel.Range RowData = excelReaderFile.ReturnAllRowsOfExcel(downloadFolder + $"({ currentReportName}).xlsx");

        //        //foreach (var row in RowData)
        //        //{
        //        //    if (row.Equals("Patient Name"))
        //        //    {
        //        //        string patientName = row.ToString();
        //        //    }
        //        //}

        //    }
        //    catch (Exception ex)
        //    {

        //        throw new Exception($"Failed with Message Unable to verify Header : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
        //    }

        //}
    }
}
