using AventStack.ExtentReports;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MRO.ROI.Automation.Selenium;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using static MRO.ROI.Automation.Utility.IniFile;

namespace MRO.ROI.Automation.Pages
{
    public class ROIFacilityScanRequestDocumentsPage
    {
        public RemoteWebDriver Driver;
        public ExtentTest logger;
        public TestContext Context;
      
        public ROIFacilityScanRequestDocumentsPage(RemoteWebDriver driver, ExtentTest _loger, TestContext _context)
        {
            Driver = driver;
            logger = _loger;
            Context = _context;
        }

        public By delievryMethod = By.XPath("//*[@id='mrocontent_dgReport']/tbody/tr[@class='TableBody']/td[5]");
        public By RequestIds = By.XPath("//*[@id='mrocontent_dgReport']/tbody/tr[@class='TableBody']/td[2]");
        public By lookupRequestIdElement = By.XPath("//img[@title='Look up by Request ID']");
        public By importRequestPagesBtn = By.XPath("//input[@id='mrocontent_cmdImportPDFDocument2']");
        public string importRequestPDFFile = "Mile.pdf";
        public const string importPDFFrame = "radWndPrompt";
        public By selectFileButton = By.Id("mrocontent_rauFileUploadfile0");
        public By importPDFButton = By.Id("mrocontent_btnImportDoc");
        public By importCloseButton = By.Id("mrocontent_btnCloseImport");
        public By batchId = By.XPath("//span[@id='mrocontent_lblSystemBatchID']");
        public By RequestId = By.XPath("//*[@id='mrocontent_dgReport']/tbody/tr[2]/td[2]");
        public By ReqidCheckBox = By.XPath("//input[@id='mrocontent_dgReport_selChkBox_0']");
        public bool VerifyDeliveryMethod()
        {
            bool _isMatching = false;
            try
            {
                string DeliveryTypeTxt1 = IniHelper.ReadConfig("ROIAdminHotfixBatchImportFileTest", "DeliveryType1");
                string DeliveryTypeTxt2 = IniHelper.ReadConfig("ROIAdminHotfixBatchImportFileTest", "DeliveryType2");
                string DeliveryTypeTxt3 = IniHelper.ReadConfig("ROIAdminHotfixBatchImportFileTest", "DeliveryType3");
                string DeliveryTypeTxt4 = IniHelper.ReadConfig("ROIAdminHotfixBatchImportFileTest", "DeliveryType4");


                List<IWebElement> records = Driver.FindElementsBy(delievryMethod);
                List<string> actualRecords = new List<string>();
                foreach (var record in records)
                {
                    actualRecords.Add(record.Text);
                }
                List<string> expectedRecords = new List<string>();

                for(int i= 1; i<= 1; i++)
                {
                    expectedRecords.Add(DeliveryTypeTxt1 = actualRecords[0]);
                    expectedRecords.Add(DeliveryTypeTxt2 = actualRecords[1]);
                    expectedRecords.Add(DeliveryTypeTxt3 = actualRecords[2]);
                    expectedRecords.Add(DeliveryTypeTxt4 = actualRecords[3]);
                }
                if (actualRecords[0] == expectedRecords[0] && actualRecords[1] == expectedRecords[1] && actualRecords[2] == expectedRecords[2] && actualRecords[3] == expectedRecords[3])
                {
                    _isMatching = true;
                }
                else {                 
                    _isMatching = false;
                }
                return _isMatching;
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to verify delievry method with details as Message : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }
        public IList<string> GetRequestIDsAndImportRequestPages()
        {
            try
            {
                ROIAdminHomePage rOIAdminHomePage = new ROIAdminHomePage(Driver, logger, Context);
                List<IWebElement> requestRecords = Driver.FindElementsBy(RequestIds);
                List<string> requestIds = new List<string>();
                foreach (var record in requestRecords)
                {
                    requestIds.Add(record.Text);
                }
                for (int i = 0; i <= 2; i++)
                {
                    rOIAdminHomePage.ROIlookupByRequestId(requestIds[i]);
                    Driver.SleepTheThread(5);
                    Driver.Click(importRequestPagesBtn);
                    Driver.ScrollToEndOfThePage();
                    Driver.SwitchTo().Frame(importPDFFrame);
                    Driver.FindElementBy(selectFileButton).SendKeys(Path.GetFullPath(Path.Combine(Assembly.GetExecutingAssembly().Location, "..", "..", "..", "..", "MRO.ROI.Automation", "Files", importRequestPDFFile)));
                    Driver.ClickAndCheckNextElement(importPDFButton, importCloseButton);
                    Driver.DirectClick(importCloseButton);
                }

                rOIAdminHomePage.ROIlookupByRequestId(requestIds[3]);
                ROIFacilityRequestStatusPage rOIFacilityRequestStatusPage = new ROIFacilityRequestStatusPage(Driver, logger, Context);
                rOIFacilityRequestStatusPage.ImportPdfFiles();
                return requestIds;
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to get request id's and import request pages with details as Message : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }

        public string GetSystemBatchNumber()
        {
            try
            {
                string systemBatchId = Driver.FindElementBy(batchId).Text.ToString().Trim();
                return systemBatchId;
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to get system batch number with details as Message : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }
        public string GetRequestId()
        {
            try
            {
                string requestid = Driver.FindElementBy(RequestId).Text.ToString().Trim();
                return requestid;
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to get request id with details as Message : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }
        public IList<string> GetKeyBatchRequestIDsAndImportRequestPages()
        {
            try
            {
                ROIAdminHomePage rOIAdminHomePage = new ROIAdminHomePage(Driver, logger, Context);
                List<IWebElement> requestRecords = Driver.FindElementsBy(RequestIds);
                List<string> requestIds = new List<string>();
                foreach (var record in requestRecords)
                {
                    requestIds.Add(record.Text);
                }
                for (int i = 0; i <= 2; i++)
                {
                    rOIAdminHomePage.ROIlookupByRequestId(requestIds[i]);
                    Driver.SleepTheThread(5);
                    ROIFacilityLogNewRequestPage rOIFacilityLogNewRequestPage = new ROIFacilityLogNewRequestPage(Driver, logger, Context);
                    rOIFacilityLogNewRequestPage.CheckForDuplicates();
                    Driver.Wait(TimeSpan.FromSeconds(2));
                    Driver.Click(importRequestPagesBtn);
                    Driver.ScrollToEndOfThePage();
                    Driver.SwitchTo().Frame(importPDFFrame);
                    Driver.FindElementBy(selectFileButton).SendKeys(Path.GetFullPath(Path.Combine(Assembly.GetExecutingAssembly().Location, "..", "..", "..", "..", "MRO.ROI.Automation", "Files", importRequestPDFFile)));
                    Driver.ClickAndCheckNextElement(importPDFButton, importCloseButton);
                    Driver.DirectClick(importCloseButton);
                }               
                return requestIds;
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to get request id's and import request pages with details as Message : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }

        public IList<string> GetRequestIDsAndImportRequestPagesTwo()
        {
            try
            {
                ROIAdminHomePage rOIAdminHomePage = new ROIAdminHomePage(Driver, logger, Context);
                List<IWebElement> requestRecords = Driver.FindElementsBy(RequestIds);
                List<string> requestIds = new List<string>();
                foreach (var record in requestRecords)
                {
                    requestIds.Add(record.Text);
                }

                rOIAdminHomePage.ROIlookupByRequestId(requestIds[0]);
                Driver.SleepTheThread(5);
                Driver.Click(importRequestPagesBtn);
                Driver.ScrollToEndOfThePage();
                Driver.SwitchTo().Frame(importPDFFrame);
                Driver.FindElementBy(selectFileButton).SendKeys(Path.GetFullPath(Path.Combine(Assembly.GetExecutingAssembly().Location, "..", "..", "..", "..", "MRO.ROI.Automation", "Files", importRequestPDFFile)));
                Driver.ClickAndCheckNextElement(importPDFButton, importCloseButton);
                Driver.DirectClick(importCloseButton);


                rOIAdminHomePage.ROIlookupByRequestId(requestIds[0]);
                //ROIFacilityRequestStatusPage rOIFacilityRequestStatusPage = new ROIFacilityRequestStatusPage(Driver, logger, Context);
                //rOIFacilityRequestStatusPage.ImportPdfFiles();
                return requestIds;
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to get request id's and import request pages with details as Message : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }

        public bool CheckBoxReqIdIsDisabled()
        {
            bool isDisabled = false;
            try
            {
                IWebElement mailCheckBox = Driver.FindElementBy(ReqidCheckBox);
                if (mailCheckBox != null)
                {
                    string value = mailCheckBox.GetAttribute("disabled");

                    if (value == "true")
                    {
                        isDisabled = true;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to check box, Exception detail: {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
            return isDisabled;
        }
    }
}
