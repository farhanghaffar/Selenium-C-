using AventStack.ExtentReports;
using DataDrivenProject;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MRO.ROI.Automation.Selenium;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MRO.ROI.Automation.Pages
{
    public class ROIAdminTransactionalModelReportPage
    {
        public RemoteWebDriver Driver;
        public ExtentTest logger;
        public TestContext Context;
        public ROIAdminTransactionalModelReportPage(RemoteWebDriver driver, ExtentTest _loger, TestContext _context)
        {
            Driver = driver;
            logger = _loger;
            Context = _context;
        }

        public By Date = By.XPath("//span[@id='daterange']");
        public By Facility = By.XPath("//select[@id='input_nROIFacilityID']");
        public By Contract = By.XPath("//select[@id='nContractID']");
        public By CreateReport = By.XPath("//input[@id='btn_submit']");
        public By includeTest = By.XPath("//input[@id='bIncludeTestDemo']");
        public By lbl_Excel_icon = By.XPath("//i[@id='lbl_Excel_icon']/../..");
        public By div_Excel_icon = By.XPath("//span[@id='div_Excel_icon']//i[@id='lbl_Excel_icon']");
        public By div_PDF_icon = By.XPath("//span[@id='div_PDF_icon']//i[@id='lbl_PDF_icon']");
        public By transactionalModelReportSummaryLabels = By.XPath("//a//span[@class='ThemeHeader ThemeBold']/../../preceding-sibling::td[1]//span");
        public By transactionalModelReportSummaryValues = By.XPath("//a//span[@class='ThemeHeader ThemeBold']");
        public By monthButton = By.XPath("//table[@class='table-condensed']//td[contains(@class,'available active')]");
        public By totalPagesCount = By.XPath("//span[@id='dtTransactionalModelRequestDetail-PageOfPages']");
        public By pageTextBox = By.XPath("//input[@id='dtTransactionalModelRequestDetail-PageNr']");
        public By RequestLoggedByMROEmp = By.XPath("//table[@id='rdRows-1']//tbody//tr//td[3]//a//span[@class='ThemeHeader ThemeBold']");
        public By RequestReleaseByMROEmp = By.XPath("//table[@id='rdRows-3']//tbody//tr//td[3]//a//span[@class='ThemeHeader ThemeBold']");
        public By RequestAssignments = By.XPath("//table[@id='rdRows-4']//tbody//tr//td[3]//a//span[@class='ThemeHeader ThemeBold']");
        public By InvoiceGenerated = By.XPath("//table[@id='rdRows-5']//tbody//tr//td[3]//a//span[@class='ThemeHeader ThemeBold']");
        public By Billable = By.XPath("//table[@id='rdRows-6']//tbody//tr//td[3]//a//span[@class='ThemeHeader ThemeBold']");
        public By PaymentsReceivedByMRO = By.XPath("//table[@id='rdRows-15']//tbody//tr//td[3]//a//span[@class='ThemeHeader ThemeBold']");
        public By nonBillableTxt = By.XPath("//*[@id='rdRows-9']//tbody//tr[5]//td[3]//a//span");
        public By trackedTxt = By.XPath("//*[@id='rdRows-10']//tbody//tr//td[3]//a//span");
        public By totalTxt = By.XPath("//*[@id='rdRows-11']//tbody//tr//td[3]//a//span");
        public By lessSalesTaxTxt = By.XPath("//*[@id='rdRows-16']//tbody//tr//td[3]/a//span");
        public By lessBillablePostageTxt = By.XPath("//*[@id='rdRows-17']//tbody/tr//td[3]//a//span");
        public By totalFaxedPagesTxt = By.XPath("//*[@id='rdRows-18']//tbody//tr[1]//td[3]//a//span");
        public By customizeHyperlink = By.XPath("//a[@id='show_div_user_inputs']//span[text()='Customize']");

        public void CreateTransactionalModelReport(string facility, string contract, bool selectIncludeTest)
        {
            try
            {
                Driver.SleepTheThread(5);
                Automation.Common.Iframe frame = new Automation.Common.Iframe(Driver, logger, Context);
                frame.SwitchToRDFrame();
                logger.Log(Status.Info, "Select current month and year");
                Driver.DirectClick(Date);
                Driver.JavaScriptClick(monthButton);
                logger.Log(Status.Info, "Select facility : " + facility);
                var facilityDropdown = Driver.FindElementBy(Facility);
                facilityDropdown.SendKeys(facility);
                if (selectIncludeTest)
                {
                    logger.Log(Status.Info, "Check Include Test");
                    Driver.SelectCheckBoxIfUnchecked(includeTest);
                }
                logger.Log(Status.Info, "Enter contract : " + contract);
                Driver.SendKeys(Contract, contract);
                Driver.SleepTheThread(1);
                logger.Log(Status.Info, "Click create report button");
                Driver.Click(CreateReport);

            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to create transactional model report, Exception detail: {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }

        public void DownloadExcelReport()
        {
            try
            {
                Driver.ClickOnDisplayedElement(lbl_Excel_icon,60);
            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to download the excel file, Exception detail: {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }

        public List<KeyValuePair<string, string>> ReturnUITransactionalModelReportData()
        {
            List<KeyValuePair<string, string>> transactionData = new List<KeyValuePair<string, string>>();
            try
            {
                var reportSummaryLabels = Driver.FindElementsByReturnsInnerText(transactionalModelReportSummaryLabels);
                var reportSummaryValues = Driver.FindElementsByReturnsInnerText(transactionalModelReportSummaryValues);

                for (int i = 0; i < reportSummaryLabels.Count; i++)
                {
                    string key = reportSummaryLabels[i];
                    string value = reportSummaryValues[i];
                    if (value.Contains("("))
                    {
                        value = $"-{value.Replace("(", "").Replace(")", "").Replace("$", "").Replace(":", "").Trim()}";
                    }
                    if (value.Contains("$"))
                    {
                        value = value.Replace("$", "").Replace(":", "").Trim();
                    }
                    if (key.Contains(":"))
                    {
                        key = key.Replace(":", "").Trim();
                    }
                    if(value.Equals("0.00"))
                    {
                        value = "0";
                    }
                    transactionData.Add(new KeyValuePair<string, string>(key, value));
                    Driver.SwitchTo().DefaultContent();
                }
            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to return UI transactional model data Message : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
            return transactionData;
        }

        public List<KeyValuePair<string, string>> ReturnExcelTransactionalModelReportData()
        {
            string filePath = string.Empty;
            List<KeyValuePair<string, string>> transactionDataExcel = new List<KeyValuePair<string, string>>();
            try
            {
                string userRoot = System.Environment.GetEnvironmentVariable("USERPROFILE");
                string downloadFolder = System.IO.Path.Combine(userRoot, "Downloads\\");
                WaitUntillFileDownloaded();
                string[] filePaths = System.IO.Directory.GetFiles(downloadFolder, "*");

                try
                {
                    foreach (string path in filePaths)
                    {
                        string ext = path.Split('.')?[1];
                        if (ext == "xlsx" && path.Contains("rdDL"))
                        {
                            filePath = path;
                            ExcelReaderFile excelRdr = new ExcelReaderFile(path);
                            int columnCount = excelRdr.getColumnCount("Summary", 6);
                            for (int cln = 0; cln < columnCount; cln++)
                            {
                                string key = excelRdr.getCellData("Summary", cln, 6);
                                string value = excelRdr.getCellData("Summary", cln, 7);
                                if (key.Contains("Total"))
                                {
                                    key = $"{key.Split(' ')[0].Replace(":", "").Trim()}";
                                }
                                if (key.Contains(":"))
                                {
                                    key = key.Replace(":", "").Trim();
                                }
                                if(value.Equals("0.00"))
                                {
                                    value = "0";
                                }
                                transactionDataExcel.Add(new KeyValuePair<string, string>(key, value));
                            }

                            break;
                        }
                    }

                }
                catch (Exception ex)
                {
                    throw new Exception($"Failed to return excel transactional model  data Message : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
                }
                finally
                {
                    File.Delete(filePath);
                }
                return transactionDataExcel;
            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to verify transactional model report with excel data, Exception detail: {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }

        public bool ValidateUIAndExcelSummaryReportData(List<KeyValuePair<string, string>> uiTransactionalData, List<KeyValuePair<string, string>> excelTransactionalData)
        {
            bool isValidated = false;
            try
            {
                for (int i = 0; i < uiTransactionalData.Count; i++)
                {
                    //Ignoring labels comparision as UI and excel labels are not exact some cosmetic mismatch found
                    //Assert.IsTrue(excelTransactionalData[i].Key.Split(' ')[0].Trim().Contains(excelTransactionalData[i].Key));
                    Assert.AreEqual(Convert.ToDouble(uiTransactionalData[i].Value), Convert.ToDouble(excelTransactionalData[i].Value));
                }
                isValidated = true;
            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to verify transactional model report with excel data, Exception detail: {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
            return isValidated;
        }

        public void WaitUntillFileDownloaded()
        {
            try
            {
                int count = Driver.WindowHandles.Count;
                while (count != 1)
                {
                    count = Driver.WindowHandles.Count;
                }
                Driver.SleepTheThread(6);
            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to wait untill excel download Message:{ex.Message} StackTrace : {ex.StackTrace}");
            }
        }

        public string GetRequestIDFromExcelSheet(ExcelReaderFile reader, string requestId)
        {
            string requestIdExsisting = string.Empty;
            try
            {
                int rowNumber = reader.getRowCount("Sheet1");
                for (int i = 4; i <= rowNumber; i++)
                {
                    string excelRequestId = reader.getCellData("Sheet1", 0, i);
                    if (requestId == excelRequestId)
                    {
                        requestIdExsisting = excelRequestId;
                        break;
                    }
                }

            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to get request id form excel Message:{ex.Message} StackTrace : {ex.StackTrace}");
            }
           return requestIdExsisting;
        }

        public bool CheckBillableShipmentRequestIdExist(string requestId)
        {
            bool isValidated = false;
            try
            {
                SwitchToSpecificFrame("sr_dtBillableShipments");
                Driver.SleepTheThread(3);
                int totalPageCount = Convert.ToInt32(Driver.GetText(By.XPath("//span[@id='dtTransactionalModelShipmentDetail-PageOfPages']")));
                if (totalPageCount > 1)
                {

                    IJavaScriptExecutor js = (IJavaScriptExecutor)Driver;
                    js.ExecuteScript($"document.getElementById('dtTransactionalModelShipmentDetail-PageNr').setAttribute('value', '{totalPageCount}')");
                    Driver.ClickOnDisplayedElement(By.Id("dtTransactionalModelShipmentDetail-PageNr"));
                    Driver.FindElementBy(By.Id("dtTransactionalModelShipmentDetail-PageNr")).SendKeys(Keys.Enter);
                }
                Driver.SleepTheThread(3);
                List<IWebElement> elements  = Driver.FindElementsBy(By.XPath("//span[contains(@id,'lblnRequestID_Row')]"));
                int retryCount = 1;
                while (elements.Count == 10 && retryCount != 4)
                {
                    IJavaScriptExecutor _js = (IJavaScriptExecutor)Driver;
                    _js.ExecuteScript($"document.getElementById('dtTransactionalModelShipmentDetail-PageNr').setAttribute('value', '{totalPageCount}')");

                    Driver.ClickOnDisplayedElement(By.Id("dtTransactionalModelShipmentDetail-PageNr"));
                    Driver.FindElementBy(By.Id("dtTransactionalModelShipmentDetail-PageNr")).SendKeys(Keys.Enter);
                    Driver.SleepTheThread(3);

                    elements = Driver.FindElementsBy(By.XPath("//span[contains(@id,'lblnRequestID_Row')]"));
                    retryCount++;
                    Driver.SleepTheThread(3);
                }
                string pageOneLastRequestId = string.Empty;
                if (elements.Count > 1)
                {
                    int firstPageLastRecordCount = (elements.Count-1);
                    pageOneLastRequestId = elements[firstPageLastRecordCount].Text;
                }

                if(pageOneLastRequestId==requestId)
                {
                    isValidated = true;
                }

            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to validate billable shipment request id in table Message:{ex.Message} StackTrace : {ex.StackTrace}");
            }
            return isValidated;
        }

        public bool ValidateForRequestIdInPDFAndExcel(string iframe, string requestId)
        {
            bool isValidated = false;
            bool isExcelValidated = false;
            bool isPDFValidated = false;
            try
            {
                Driver.SleepTheThread(5);
                Driver.SwitchTo().DefaultContent();
                Automation.Common.Iframe frame = new Automation.Common.Iframe(Driver, logger, Context);
                frame.SwitchToRDFrame();

                Driver.SwitchTo().Frame(iframe);

                Driver.SleepTheThread(2);
                Driver.ClickOnDisplayedElement(div_PDF_icon);
                Driver.ClickOnDisplayedElement(div_Excel_icon);

                Driver.SleepTheThread(3);
                string todayDate = string.Format("{0:yyyy/MM/dd}", DateTime.Now);
                string sReportDate = todayDate.Replace('/', '-');
                string userRoot = System.Environment.GetEnvironmentVariable("USERPROFILE");
                string downloadFolder = System.IO.Path.Combine(userRoot, "Downloads\\");
                string excelFileName = downloadFolder + $"ReconciliationReportDetail_{sReportDate}.xlsx";
                ExcelReaderFile excelReaderFile = new ExcelReaderFile(excelFileName);
                int lastRowNumber = excelReaderFile.getRowCount("Sheet1");
                
                for (int i = 0; i < lastRowNumber; i++)
                {
                    string excelRequestId = excelReaderFile.ReadExcelCellData(excelFileName, i, 1);
                    if(excelRequestId==requestId)
                    {
                        isExcelValidated = true;
                        break;
                    }

                }
                var handles = Driver.WindowHandles;
                Driver.SwitchTo().Window(handles[3]);
                System.Text.RegularExpressions.Match requestIdPDF = null;
                string pdfContent = ExcelReaderFile.DownloadPDFFileFromUrl(Driver.Url);
                if (!string.IsNullOrEmpty(requestId))
                {
                     requestIdPDF = System.Text.RegularExpressions.Regex.Match(pdfContent, $"{requestId}", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                    
                }

                if(requestIdPDF.Value.ToString()==requestId)
                {
                    isPDFValidated = true;
                }
                var handles1 = Driver.WindowHandles;
                Driver.SwitchTo().Window(handles1[1]);
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to validate requestid in pdf and excel Message:{ex.Message} StackTrace : {ex.StackTrace}");
            }

            if(isExcelValidated && isPDFValidated)
            {
                isValidated = true;
            }
            SwitchToRDFrame();
            return isValidated;
        }

        public bool ValidateForExcelAndPDFContents(string iframe, string requestId="")
        {
            bool isValidated = false;
            string filePath = string.Empty;
            try
            {
                Driver.SleepTheThread(5);
                Driver.SwitchTo().DefaultContent();
                Automation.Common.Iframe frame = new Automation.Common.Iframe(Driver, logger, Context);
                frame.SwitchToRoiFrame();
                frame.SwitchToRDFrame();

                Driver.SwitchTo().Frame(iframe);
                
                Driver.SleepTheThread(2);
                Driver.ClickOnDisplayedElement(div_PDF_icon);
                Driver.ClickOnDisplayedElement(div_Excel_icon);

                Driver.SleepTheThread(3);
                var handles = Driver.WindowHandles;
                Driver.SwitchTo().Window(handles[0]);
                Driver.SwitchTo().DefaultContent();
                frame.SwitchToRoiFrame(); 
                frame.SwitchToRDFrame();
                Driver.SwitchTo().Frame(iframe);
                string pageOneFirstRequestId = Driver.GetText(By.XPath("//span[@id='lblnRequestID_Row1']"));
                int firstPageLastRecordCount = Driver.FindElementsBy(By.XPath("//span[contains(@id,'lblnRequestID_Row')]")).Count;
                string pageOneLastRequestId = Driver.GetText(By.XPath($"//span[@id='lblnRequestID_Row{firstPageLastRecordCount}']"));

                string pageLastFirstRequestId = string.Empty;
                string pageLastLastRequestId = string.Empty;
                int lastPageFirstRecorsValue=0;
                int lastRecordValue=0;
                int totalPageCount = Convert.ToInt32(Driver.GetText(By.XPath("//span[@id='dtTransactionalModelRequestDetail-PageOfPages'] | //span[@id='dtTransactionalModelShipmentDetail-PageOfPages']")));
                if (totalPageCount > 1)
                {
                    IJavaScriptExecutor js = (IJavaScriptExecutor)Driver;
                    js.ExecuteScript($"document.getElementById('dtTransactionalModelRequestDetail-PageNr').setAttribute('value', '{totalPageCount}')");

                    Driver.ClickOnDisplayedElement(pageTextBox);
                    Driver.FindElementBy(pageTextBox).SendKeys(Keys.Enter);

                    Driver.SleepTheThread(4);
                    List<IWebElement> elements = Driver.FindElementsBy(By.XPath("//span[contains(@id,'lblnRequestID_Row')]"));
                    int lastPageRowCount =0;
                    int retryCount = 1;
                    while(elements.Count == 10 && retryCount!=4)
                    {
                        Driver.SleepTheThread(2);
                        IJavaScriptExecutor _js = (IJavaScriptExecutor)Driver;
                        js.ExecuteScript($"document.getElementById('dtTransactionalModelRequestDetail-PageNr').setAttribute('value', '{totalPageCount}')");
                        Driver.ClickOnDisplayedElement(pageTextBox);
                        Driver.SleepTheThread(2);
                        Driver.FindElementBy(pageTextBox).SendKeys(Keys.Enter);
                        Driver.SleepTheThread(3);
                        elements = Driver.FindElementsBy(By.XPath("//span[contains(@id,'lblnRequestID_Row')]"));
                        retryCount++;
                        Driver.SleepTheThread(1);
                    }
                    lastPageRowCount = elements.Count;
                    lastPageFirstRecorsValue = (((totalPageCount - 1) * 10) + 1);
                    lastRecordValue = (((totalPageCount - 1) * 10) + lastPageRowCount);

                    pageLastFirstRequestId = Driver.GetText(By.XPath($"//span[@id='lblnRequestID_Row{lastPageFirstRecorsValue}']"));
                    pageLastLastRequestId = Driver.GetText(By.XPath($"//span[@id='lblnRequestID_Row{lastRecordValue}']"));
                }

                string userRoot = System.Environment.GetEnvironmentVariable("USERPROFILE");
                string downloadFolder = System.IO.Path.Combine(userRoot, "Downloads\\");
                string[] filePaths = System.IO.Directory.GetFiles(downloadFolder, "*");

                string firstRowRequestId = string.Empty;
                string tenthRowRequestId = string.Empty;
                string lastRowFirstRequestId = string.Empty;
                string LastRowRequestId = string.Empty;
                string actualRequestId = string.Empty;

                foreach (string path in filePaths)
                {
                    string ext = path.Split('.')?[1];
                    if (ext == "xlsx" && path.Contains("ReconciliationReportDetail"))
                    {
                        filePath = path;
                        ExcelReaderFile excelRdr = new ExcelReaderFile(path);

                        firstRowRequestId = excelRdr.getCellData("Sheet1", 0, 4);
                        tenthRowRequestId = excelRdr.getCellData("Sheet1", 0, (firstPageLastRecordCount+3));
                        if (totalPageCount > 1)
                        {
                            lastRowFirstRequestId = excelRdr.getCellData("Sheet1", 0, (lastPageFirstRecorsValue + 3));
                            LastRowRequestId = excelRdr.getCellData("Sheet1", 0, (lastRecordValue + 3));
                        }
                        if (!string.IsNullOrEmpty(requestId))
                        {
                            Driver.SleepTheThread(2);
                            actualRequestId = GetRequestIDFromExcelSheet(excelRdr, requestId);
                        }
                        break;
                    }
                }

                Driver.SleepTheThread(4);

                if (totalPageCount > 1)
                {
                    Assert.AreEqual(pageOneFirstRequestId, firstRowRequestId, $"Expected {pageOneFirstRequestId} and actual {firstRowRequestId} requestid are different");
                    Assert.AreEqual(pageOneLastRequestId, tenthRowRequestId, $"Expected {pageOneLastRequestId} and actual {tenthRowRequestId} requestid are different");

                    Assert.AreEqual(pageLastFirstRequestId, lastRowFirstRequestId, $"Expected {pageLastFirstRequestId} and actual {lastRowFirstRequestId} requestid are different");
                    Assert.AreEqual(pageLastLastRequestId, LastRowRequestId, $"Expected {pageLastLastRequestId} and actual {LastRowRequestId} requestid are different");
                }
                if (!string.IsNullOrEmpty(requestId))
                {
                    Assert.AreEqual(requestId, actualRequestId, $"Expected {requestId} and actual {actualRequestId} requestid are different");
                }

                DeleteDownloadedExcelFiles();

                var _handles = Driver.WindowHandles;
                Driver.SwitchTo().Window(_handles[1]);

                string pdfContent = ExcelReaderFile.DownloadPDFFileFromUrl(Driver.Url);
                System.Text.RegularExpressions.Match pdfPageOneFirstRequestIdMatch = System.Text.RegularExpressions.Regex.Match(pdfContent, $"{pageOneFirstRequestId}",
                System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                Assert.IsTrue(pdfPageOneFirstRequestIdMatch.Success, $"Failed to validate first page first request id in PDF ({pageOneFirstRequestId})");

                System.Text.RegularExpressions.Match pdfPageOneLastRequestIdMatch = System.Text.RegularExpressions.Regex.Match(pdfContent, $"{pageOneLastRequestId}",
                System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                Assert.IsTrue(pdfPageOneLastRequestIdMatch.Success, $"Failed to validate first page last request id in PDF ({pageOneLastRequestId})");

                if (totalPageCount > 1)
                {
                    System.Text.RegularExpressions.Match pdfpageLastFirstRequestIdMatch = System.Text.RegularExpressions.Regex.Match(pdfContent, $"{pageLastFirstRequestId}",
                    System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                    Assert.IsTrue(pdfpageLastFirstRequestIdMatch.Success, $"Failed to validate last page first request id in PDF ({pageLastFirstRequestId})");

                    System.Text.RegularExpressions.Match pdfPageLastLastRequestIdMatch = System.Text.RegularExpressions.Regex.Match(pdfContent, $"{pageLastLastRequestId}",
                    System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                    Assert.IsTrue(pdfPageLastLastRequestIdMatch.Success, $"Failed to validate last page last request id in PDF ({pageLastLastRequestId})");
                }
                if (!string.IsNullOrEmpty(requestId))
                {
                    System.Text.RegularExpressions.Match requestIdPDF = System.Text.RegularExpressions.Regex.Match(pdfContent, $"{requestId}", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                    Assert.IsTrue(requestIdPDF.Success, $"Failed to validate actual request id in PDF ({requestId})");
                }

                CloseThePDFTab();
                isValidated = true;
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to transactional model dashboard pdf and excel data Message:{ex.Message} StackTrace : {ex.StackTrace}");
            }
            return isValidated;
        }


        public bool ClickOnLinkAndValidateForExcelAndPDF(IWebElement valueElement, string iframe)
        {
            bool isValidated = false;
            string filePath = string.Empty;
            try
            {
                Driver.SleepTheThread(6);
                valueElement.Click();
                isValidated =ValidateForExcelAndPDFContents(iframe);

            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to transactional model dashboard pdf and excel data Message:{ex.Message} StackTrace : {ex.StackTrace}");
            }
            return isValidated;
        }


        public bool ClickOnFirstAndLastLinksAndVerifyPDFAndExcelHasDataIntact()
        {
            bool isValidated = false;
            bool firstRowValidated = false;
            bool lastRowValidated = false;
            List<string> frames = new List<string>() { "sr_dtLoggedRequests", "sr_dtBulkLoggedRequests", "sr_dtReleasedRequests",
                "sr_dtAssignedRequests", "sr_dtInvoicedRequests", "sr_dtBillableShipments", "sr_dtNonBillableShipments", 
                "sr_dtTotalShipments", "sr_dtNonBillPostage", "sr_dtTrackedPostage", "sr_dtTotalPostage","sr_dtOSDFax","sr_dtMROFax"
            ,"sr_dtTotalFax", "sr_dtPaymentRcvd", "sr_dtSalesTax", "sr_dtBillablePostage", "sr_dtTotalPayments"};
            try
            {

                Automation.Common.Iframe frame = new Automation.Common.Iframe(Driver, logger, Context);
                frame.SwitchToRoiFrame();
                frame.SwitchToRDFrame();
                var values = Driver.FindElementsBy(transactionalModelReportSummaryValues);
                for (int i = 0; i < values.Count;)
                {
                    firstRowValidated = ClickOnLinkAndValidateForExcelAndPDF(values[i], frames[i]);
                    frame.SwitchToRoiFrame();
                    frame.SwitchToRDFrame();
                    values = Driver.FindElementsBy(transactionalModelReportSummaryValues);
                    values[i].Click();
                    break;
                }

                var lastLink = values.Last();
                Driver.SwitchToDefaultContent();
                Driver.ScrollToEndOfThePage();
                frame.SwitchToRoiFrame();
                frame.SwitchToRDFrame();
                lastRowValidated = ClickOnLinkAndValidateForExcelAndPDF(lastLink, frames.Last());
                frame.SwitchToRoiFrame();
                frame.SwitchToRDFrame();
                values = Driver.FindElementsBy(transactionalModelReportSummaryValues);
                values.Last().Click();
                Driver.SwitchTo().DefaultContent();
            }

            catch (Exception ex)
            {
                throw new Exception($"Failed to return excel transactional model  data Message : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }

            if (firstRowValidated && lastRowValidated)
                isValidated = true;
            
            return isValidated;
        }

        public void SwitchToRDFrame()
        {
            try
            {
                Driver.SwitchTo().DefaultContent();
                Automation.Common.Iframe frame = new Automation.Common.Iframe(Driver, logger, Context);
                frame.SwitchToRDFrame();
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to switch to rd frame Message : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");

            }
        }

        public void CloseThePDFTab()
        {
            try
            {
                Driver.SwitchToWindowAndCloseOtheThanMatchedWindow("ROI Online");
                Driver.SwitchToWindow("ROI Online");
                Automation.Common.Iframe frame = new Automation.Common.Iframe(Driver, logger, Context);
                frame.switchToDefaut();
                frame.SwitchToRoiFrame();
                frame.SwitchToRDFrame();
            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to close the pdf tab  data Message : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }
        public void SwitchToSpecificFrame(string frame)
        {
            Automation.Common.Iframe iFrame = new Automation.Common.Iframe(Driver, logger, Context);
            try
            {
                switch (frame)
                {
                    case "sr_dtLoggedRequests":
                        Driver.SwitchTo().DefaultContent();
                        iFrame.SwitchToRoiFrame();
                        iFrame.SwitchToRDFrame();
                        Driver.SwitchTo().Frame("sr_dtLoggedRequests");
                        break;

                    case "sr_dtBulkLoggedRequests":
                        Driver.SwitchTo().DefaultContent();
                        iFrame.SwitchToRoiFrame();
                        iFrame.SwitchToRDFrame();
                        Driver.SwitchTo().Frame("sr_dtBulkLoggedRequests");
                        break;

                    case "sr_dtReleasedRequests":
                        Driver.SwitchTo().DefaultContent();
                        iFrame.SwitchToRoiFrame();
                        iFrame.SwitchToRDFrame();
                        Driver.SwitchTo().Frame("sr_dtReleasedRequests");
                        break;

                    case "sr_dtAssignedRequests":
                        Driver.SwitchTo().DefaultContent();
                        iFrame.SwitchToRoiFrame();
                        iFrame.SwitchToRDFrame();
                        Driver.SwitchTo().Frame("sr_dtAssignedRequests");
                        break;

                    case "sr_dtInvoicedRequests":
                        Driver.SwitchTo().DefaultContent();
                        iFrame.SwitchToRoiFrame();
                        iFrame.SwitchToRDFrame();
                        Driver.SwitchTo().Frame("sr_dtInvoicedRequests");
                        break;

                    case "sr_dtBillableShipments":
                        Driver.SwitchTo().DefaultContent();
                        iFrame.SwitchToRoiFrame();
                        iFrame.SwitchToRDFrame();
                        Driver.SwitchTo().Frame("sr_dtBillableShipments");
                        break;

                    case "sr_dtNonBillableShipments":
                        Driver.SwitchTo().DefaultContent();
                        iFrame.SwitchToRoiFrame();
                        iFrame.SwitchToRDFrame();
                        Driver.SwitchTo().Frame("sr_dtNonBillableShipments");
                        break;

                    case "sr_dtTotalShipments":
                        Driver.SwitchTo().DefaultContent();
                        iFrame.SwitchToRoiFrame();
                        iFrame.SwitchToRDFrame();
                        Driver.SwitchTo().Frame("sr_dtTotalShipments");
                        break;

                    case "sr_dtNonBillPostage":
                        Driver.SwitchTo().DefaultContent();
                        iFrame.SwitchToRoiFrame();
                        iFrame.SwitchToRDFrame();
                        Driver.SwitchTo().Frame("sr_dtNonBillPostage");
                        break;

                    case "sr_dtTrackedPostage":
                        Driver.SwitchTo().DefaultContent();
                        iFrame.SwitchToRoiFrame();
                        iFrame.SwitchToRDFrame();
                        Driver.SwitchTo().Frame("sr_dtTrackedPostage");
                        break;

                    case "sr_dtTotalPostage":
                        Driver.SwitchTo().DefaultContent();
                        iFrame.SwitchToRoiFrame();
                        iFrame.SwitchToRDFrame();
                        Driver.SwitchTo().Frame("sr_dtTotalPostage");
                        break;

                    case "sr_dtOSDFax":
                        Driver.SwitchTo().DefaultContent();
                        iFrame.SwitchToRoiFrame();
                        iFrame.SwitchToRDFrame();
                        Driver.SwitchTo().Frame("sr_dtOSDFax");
                        break;

                    case "sr_dtMROFax":
                        Driver.SwitchTo().DefaultContent();
                        iFrame.SwitchToRoiFrame();
                        iFrame.SwitchToRDFrame();
                        Driver.SwitchTo().Frame("sr_dtMROFax");
                        break;

                    case "sr_dtTotalFax":
                        Driver.SwitchTo().DefaultContent();
                        iFrame.SwitchToRoiFrame();
                        iFrame.SwitchToRDFrame();
                        Driver.SwitchTo().Frame("sr_dtTotalFax");
                        break;

                    case "sr_dtPaymentRcvd":
                        Driver.SwitchTo().DefaultContent();
                        iFrame.SwitchToRoiFrame();
                        iFrame.SwitchToRDFrame();
                        Driver.SwitchTo().Frame("sr_dtPaymentRcvd");
                        break;

                    case "sr_dtSalesTax":
                        Driver.SwitchTo().DefaultContent();
                        iFrame.SwitchToRoiFrame();
                        iFrame.SwitchToRDFrame();
                        Driver.SwitchTo().Frame("sr_dtSalesTax");
                        break;

                    case "sr_dtBillablePostage":
                        Driver.SwitchTo().DefaultContent();
                        iFrame.SwitchToRoiFrame();
                        iFrame.SwitchToRDFrame();
                        Driver.SwitchTo().Frame("sr_dtBillablePostage");
                        break;

                    case "sr_dtTotalPayments":
                        Driver.SwitchTo().DefaultContent();
                        iFrame.SwitchToRoiFrame();
                        iFrame.SwitchToRDFrame();
                        Driver.SwitchTo().Frame("sr_dtTotalPayments");
                        break;

                    default:
                        throw new Exception("No match found to switch");
                }

            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to switch to frame Message : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }
       
        public void FilterReportBasedOnRequestDate(string frameName)
        {
            int retryCount = 0;
            try
            {
                Driver.SleepTheThread(5);
                SwitchToSpecificFrame(frameName);
                string requestDate= Driver.GetText(By.XPath("//span[@id='lblsRequestDate_Row1']|//span[@id='lbldtRequestDate_Row1']"));
                requestDate = requestDate.Split(' ')[0]?.Trim();
                var currentDate = DateTime.Now.ToString("M/d/yyyy");

                while (requestDate != currentDate && retryCount !=6)
                {
                    Driver.ScrollIntoView(null,By.XPath("//*[contains(text(),'Request Date')]"));
                    Driver.DirectClick(By.XPath("//*[contains(text(),'Request Date')]"));
                    Driver.SleepTheThread(5);
                    requestDate =Driver.GetText(By.XPath("//span[@id='lblsRequestDate_Row1']"));
                    requestDate = requestDate.Split(' ')[0]?.Trim();
                    retryCount++;
                }

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
                var requestElements =Driver.FindElementsBy(By.XPath("//span[contains(@id,'lblnRequestID_Row')]"));
                foreach (var requestElement in requestElements)
                {
                    if(requestElement.Text.Equals(requestId))
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

        public void ClickOnSpecificAmountLink(string Name)
        {
            try
            {
                Driver.DirectClick(By.XPath($"//span[contains(text(),'{Name}')]/../following-sibling::td//a"));
            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to click on specific link {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }

       

        public string GetSpecificAmountFromLink(string requestName)
        {
            string formattedAmountValue = string.Empty;
            try
            {
                string amounts = Driver.GetText(By.XPath($"//span[contains(text(),'{requestName}')]/../following-sibling::td//a//span"));
                formattedAmountValue = amounts.Replace("$", "").Replace("(", "").Replace(")", "");
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to return amount for {requestName} {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
            return formattedAmountValue;
        }

        public string ReturnDisplayedText()
        {
            string drillText = string.Empty;
            try
            {
               var textElements = Driver.FindElementsBy(By.XPath("//span[@class='drill-title']"));
                foreach (var txtElement in textElements)
                {
                    if(txtElement.Displayed)
                    {
                        drillText = txtElement.Text;
                    }

                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to get displayed text {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
            return drillText;
        }

        public void DeleteDownloadedExcelFiles()
        {
            try
            {
                List<string> fileNames = new List<string> { "ReconciliationReportDetail" };
                string userRoot = System.Environment.GetEnvironmentVariable("USERPROFILE");
                string downloadFolder = System.IO.Path.Combine(userRoot, "Downloads\\");
                string[] filePaths = System.IO.Directory.GetFiles(downloadFolder, "*");

                foreach (var filename in fileNames)
                {
                    foreach (string filePath in filePaths)
                    {
                        string ext = filePath.Split('.')?[1];
                        if (ext == "xlsx" && filePath.Contains(filename))
                        {
                            File.Delete(filePath);
                        }
                    }
                    Driver.SleepTheThread(2);
                }
            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to delete downloaded excel files Message : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }

        public string GetRequestsLoggedbyMROEmployees()
        {
            try
            {
                Driver.SwitchTo().DefaultContent();
                IWebElement frame = Driver.FindElementBy(By.XPath("//iframe[starts-with(@id,'rdFrame')]"));
                Driver.SwitchTo().Frame(frame);

                string requestsLoggedbyMROEmployees = Driver.FindElementBy(RequestLoggedByMROEmp).Text;
                return requestsLoggedbyMROEmployees;

            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to request logged by MRO employees:{ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }

        }

        public string GetRequestsReleasedByMROEmployees()
        {
            try
            {
                Driver.SwitchTo().DefaultContent();
                IWebElement frame = Driver.FindElementBy(By.XPath("//iframe[starts-with(@id,'rdFrame')]"));
                Driver.SwitchTo().Frame(frame);

                string requestsReleasedByMROEmployees = Driver.FindElementBy(RequestReleaseByMROEmp).Text;
                return requestsReleasedByMROEmployees;

            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to get request released by MRO employees:{ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }

        }

        public string GetRequestAssignments()
        {
            try
            {
                Driver.SwitchTo().DefaultContent();
                IWebElement frame = Driver.FindElementBy(By.XPath("//iframe[starts-with(@id,'rdFrame')]"));
                Driver.SwitchTo().Frame(frame);

                string requestAssignments = Driver.FindElementBy(RequestAssignments).Text;
                return requestAssignments;

            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to get request assignments:{ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }

        }

        public string GetInvoiceGenerated()
        {
            try
            {
                Driver.SwitchTo().DefaultContent();
                IWebElement frame = Driver.FindElementBy(By.XPath("//iframe[starts-with(@id,'rdFrame')]"));
                Driver.SwitchTo().Frame(frame);

                string invoiceGenerated = Driver.FindElementBy(InvoiceGenerated).Text;
                return invoiceGenerated;
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to get invoice generated:{ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }

        }

        public string Getbillable()
        {
            try
            {
                Driver.SwitchTo().DefaultContent();
                IWebElement frame = Driver.FindElementBy(By.XPath("//iframe[starts-with(@id,'rdFrame')]"));
                Driver.SwitchTo().Frame(frame);

                string billable = Driver.FindElementBy(Billable).Text;
                return billable;
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to get billable value:{ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }

        }

        public double GetPaymentsReceivedByMRO()
        {
            try
            {
                Driver.SwitchTo().DefaultContent();
                IWebElement frame = Driver.FindElementBy(By.XPath("//iframe[starts-with(@id,'rdFrame')]"));
                Driver.SwitchTo().Frame(frame);

                string paymentsReceivedByMRO = Driver.FindElementBy(PaymentsReceivedByMRO).Text.Replace("$", "");
                double paymentsReceivedByMROInt = Math.Round(Convert.ToDouble(paymentsReceivedByMRO), 0);
                return paymentsReceivedByMROInt;
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to get payment received by MRO:{ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }

        }

        public void ClickRequestLoggedByMROEmp()
        {
            try
            {
                Driver.SwitchTo().DefaultContent();
                IWebElement frame = Driver.FindElementBy(By.XPath("//iframe[starts-with(@id,'rdFrame')]"));
                Driver.SwitchTo().Frame(frame);

                IWebElement requestLoggedByMROEmp = Driver.FindElementBy(RequestLoggedByMROEmp);
                requestLoggedByMROEmp.Click();
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to click on request logged by MRO employees:{ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }
        public bool ValidateForExcelAndPDFContentsWithNoRequestID(string iframe, string requestId = "")
        {
            bool isValidated = false;
            string filePath = string.Empty;
            try
            {
                Driver.SleepTheThread(5);
                Driver.SwitchTo().DefaultContent();
                Automation.Common.Iframe frame = new Automation.Common.Iframe(Driver, logger, Context);
                frame.SwitchToRDFrame();

                Driver.SwitchTo().Frame(iframe);

                Driver.SleepTheThread(2);
                Driver.ClickOnDisplayedElement(div_PDF_icon);
                Driver.ClickOnDisplayedElement(div_Excel_icon);

                Driver.SleepTheThread(3);
                var handles = Driver.WindowHandles;
                Driver.SwitchTo().Window(handles[1]);
                Driver.SwitchTo().DefaultContent();
                frame.SwitchToRDFrame();
                Driver.SwitchTo().Frame(iframe);
                string pageOneFirstRequestId = Driver.GetText(By.XPath("//span[@id='lblnRequestID_Row1']"));
                int firstPageLastRecordCount = Driver.FindElementsBy(By.XPath("//span[contains(@id,'lblnRequestID_Row')]")).Count;
                string pageOneLastRequestId = Driver.GetText(By.XPath($"//span[@id='lblnRequestID_Row{firstPageLastRecordCount}']"));

                string pageLastFirstRequestId = string.Empty;
                string pageLastLastRequestId = string.Empty;
                int lastPageFirstRecorsValue = 0;
                int lastRecordValue = 0;
                int totalPageCount = Convert.ToInt32(Driver.GetText(By.XPath("//span[@id='dtTransactionalModelRequestDetail-PageOfPages'] | //span[@id='dtTransactionalModelShipmentDetail-PageOfPages']")));
                if (totalPageCount > 1)
                {
                    IJavaScriptExecutor js = (IJavaScriptExecutor)Driver;
                    js.ExecuteScript($"document.getElementById('dtTransactionalModelRequestDetail-PageNr').setAttribute('value', '{totalPageCount}')");

                    Driver.ClickOnDisplayedElement(pageTextBox);
                    Driver.FindElementBy(pageTextBox).SendKeys(Keys.Enter);

                    Driver.SleepTheThread(6);
                    int lastPageRowCount = Driver.FindElementsBy(By.XPath("//span[contains(@id,'lblnRequestID_Row')]")).Count;

                    lastPageFirstRecorsValue = (((totalPageCount - 1) * 10) + 1);
                    lastRecordValue = (((totalPageCount - 1) * 10) + lastPageRowCount);

                    pageLastFirstRequestId = Driver.GetText(By.XPath($"//span[@id='lblnRequestID_Row{lastPageFirstRecorsValue}']"));
                    pageLastLastRequestId = Driver.GetText(By.XPath($"//span[@id='lblnRequestID_Row{lastRecordValue}']"));
                }

                string userRoot = System.Environment.GetEnvironmentVariable("USERPROFILE");
                string downloadFolder = System.IO.Path.Combine(userRoot, "Downloads\\");
                string[] filePaths = System.IO.Directory.GetFiles(downloadFolder, "*");

                string firstRowRequestId = string.Empty;
                string tenthRowRequestId = string.Empty;
                string lastRowFirstRequestId = string.Empty;
                string LastRowRequestId = string.Empty;
                string actualRequestId = string.Empty;

                foreach (string path in filePaths)
                {
                    string ext = path.Split('.')?[1];
                    if (ext == "xlsx" && path.Contains("ReconciliationReportDetail"))
                    {
                        filePath = path;
                        ExcelReaderFile excelRdr = new ExcelReaderFile(path);

                        firstRowRequestId = excelRdr.getCellData("Sheet1", 0, 4);
                        tenthRowRequestId = excelRdr.getCellData("Sheet1", 0, (firstPageLastRecordCount + 3));
                        if (totalPageCount > 1)
                        {
                            lastRowFirstRequestId = excelRdr.getCellData("Sheet1", 0, (lastPageFirstRecorsValue + 3));
                            LastRowRequestId = excelRdr.getCellData("Sheet1", 0, (lastRecordValue + 3));
                        }
                        if (!string.IsNullOrEmpty(requestId))
                        {
                            actualRequestId = GetRequestIDFromExcelSheet(excelRdr, requestId);
                        }
                        break;
                    }
                }


                Assert.AreEqual(pageOneFirstRequestId, firstRowRequestId, $"Expected {pageOneFirstRequestId} and actual {firstRowRequestId} requestid are different");
                Assert.AreEqual(pageOneLastRequestId, tenthRowRequestId, $"Expected {pageOneLastRequestId} and actual {tenthRowRequestId} requestid are different");

                if (totalPageCount > 1)
                {
                    Assert.AreEqual(pageLastFirstRequestId, lastRowFirstRequestId, $"Expected {pageLastFirstRequestId} and actual {lastRowFirstRequestId} requestid are different");
                    Assert.AreEqual(pageLastLastRequestId, LastRowRequestId, $"Expected {pageLastLastRequestId} and actual {LastRowRequestId} requestid are different");
                }
                if (!string.IsNullOrEmpty(requestId))
                {
                    Assert.AreNotEqual(requestId, actualRequestId, $"Expected {requestId} and actual {actualRequestId} requestid are equal");
                }

                File.Delete(filePath);

                var _handles = Driver.WindowHandles;
                Driver.SwitchTo().Window(_handles[2]);

                string pdfContent = ExcelReaderFile.DownloadPDFFileFromUrl(Driver.Url);
                System.Text.RegularExpressions.Match pdfPageOneFirstRequestIdMatch = System.Text.RegularExpressions.Regex.Match(pdfContent, $"{pageOneFirstRequestId}",
                System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                Assert.IsTrue(pdfPageOneFirstRequestIdMatch.Success, $"Failed to validate first page first request id in PDF ({pageOneFirstRequestId})");

                System.Text.RegularExpressions.Match pdfPageOneLastRequestIdMatch = System.Text.RegularExpressions.Regex.Match(pdfContent, $"{pageOneLastRequestId}",
                System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                Assert.IsTrue(pdfPageOneLastRequestIdMatch.Success, $"Failed to validate first page last request id in PDF ({pageOneLastRequestId})");

                if (totalPageCount > 1)
                {
                    System.Text.RegularExpressions.Match pdfpageLastFirstRequestIdMatch = System.Text.RegularExpressions.Regex.Match(pdfContent, $"{pageLastFirstRequestId}",
                    System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                    Assert.IsTrue(pdfpageLastFirstRequestIdMatch.Success, $"Failed to validate last page first request id in PDF ({pageLastFirstRequestId})");

                    System.Text.RegularExpressions.Match pdfPageLastLastRequestIdMatch = System.Text.RegularExpressions.Regex.Match(pdfContent, $"{pageLastLastRequestId}",
                    System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                    Assert.IsTrue(pdfPageLastLastRequestIdMatch.Success, $"Failed to validate last page last request id in PDF ({pageLastLastRequestId})");
                }
                if (!string.IsNullOrEmpty(requestId))
                {
                    System.Text.RegularExpressions.Match requestIdPDF = System.Text.RegularExpressions.Regex.Match(pdfContent, $"{requestId}", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                    Assert.IsFalse(requestIdPDF.Success, $"Failed to validate actual request id in PDF ({requestId})");
                }

                CloseThePDFTab();
                var _handles2 = Driver.WindowHandles;
                Driver.SwitchTo().Window(_handles2[1]).Close();
                Driver.SwitchTo().Window(_handles2[0]);
                Driver.SwitchTo().DefaultContent();
                frame.SwitchToRDFrame();
                Driver.SwitchTo().Frame(iframe);
                isValidated = true;
            }
            catch (Exception ex)
            {
                File.Delete(filePath);
                throw new Exception($"Failed to transactional model dashboard pdf and excel data Message:{ex.Message} StackTrace : {ex.StackTrace}");
            }
            return isValidated;
        }
        public void ClickOnSpecificAmountLinkBulkLogging(string Name, string iframe)
        {
            try
            {
                Driver.SwitchTo().DefaultContent();
                Automation.Common.Iframe frame = new Automation.Common.Iframe(Driver, logger, Context);
                frame.SwitchToRDFrame();               
                Driver.DirectClick(By.XPath($"//span[contains(text(),'{Name}')]/../following-sibling::td//a"));
            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to click on bulk logging specific link {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }
        public bool CheckRequestIdsExsist(IList<string> requestId)
        {
            bool isValidated = false;
            try
            {
                var requestElements = Driver.FindElementsBy(By.XPath("//span[contains(@id,'lblnRequestID_Row')]"));
                for (int i = 0; i <= 3; i++)
                {
                    foreach (var requestElement in requestElements)
                    {
                        if (requestElement.Text.Equals(requestId[i]))
                        {
                            isValidated = true;
                            break;
                        }
                        else
                        {
                            isValidated = false;

                        }
                    }
                }
            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to check request id aganist request table {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
            return isValidated;
        }
        public bool VerifyNonBillableAndTotalCostUnderPassThroughPostage()
        {
            bool isDisplayed = false;
            try
            {
                Driver.SleepTheThread(15);
                string nonBillable = Driver.GetText(nonBillableTxt).ToString();
                string nonBillableValue = nonBillable.Substring(0, 1);
                string tracked = Driver.GetText(trackedTxt).ToString();
                string trackedValue = tracked.Substring(0, 1);
                string total = Driver.GetText(totalTxt).ToString();
                string totalValue = total.Substring(0, 1);
                if (nonBillableValue == "$" && trackedValue == "$" && totalValue == "$")
                {
                    isDisplayed = true;
                }
                else
                {
                    isDisplayed = false;
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to verify non-billable,tracked and total cost under pass through postage: {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
            return isDisplayed;
        }
        public bool VerifyLessSalesTaxAndBillablePostageAndTotalCostUnderFaxedPages()
        {
            bool isDisplayed = false;
            try
            {
                string lessSalesTax = Driver.GetText(lessSalesTaxTxt).ToString();
                string lessSalesTaxValue = lessSalesTax.Substring(1, 1);
                string lessBillablePostage = Driver.GetText(lessBillablePostageTxt).ToString();
                string lessBillablePostageValue = lessBillablePostage.Substring(1, 1);
                string totalFaxedPages = Driver.GetText(totalFaxedPagesTxt).ToString();
                string totalFaxedPagesValue = totalFaxedPages.Substring(1, 1);
                if (lessSalesTax.Contains("$") && lessBillablePostage.Contains("$") && totalFaxedPages.Contains("$"))
                {
                    isDisplayed = true;
                }
                else
                {
                    isDisplayed = false;
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to verify less sales tax, less billable postage and total cost under faxed pages: {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
            return isDisplayed;
        }
        public void ClickOnCustomize()
        {
            try
            {
                logger.Log(Status.Info, "Click on customize button");
                Driver.SleepTheThread(5);
                Driver.JavaScriptClick(customizeHyperlink);
                Driver.SleepTheThread(5);
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to click on customize: {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }
        public bool VerifyRoiTestFacilitySelected()
        {
            bool isDisplayed = false;
            try
            {
                string facility = Driver.FindElementBy(Facility).GetAttribute("value").ToString();
                if (facility == "1")
                {
                    isDisplayed = true;
                }
                else
                {
                    isDisplayed = false;
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to verify roi test facility selected: {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
            return isDisplayed;
        }
    }
}
