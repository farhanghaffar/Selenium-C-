using AventStack.ExtentReports;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MRO.ROI.Automation.Selenium;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using System;
using System.IO;
using System.Reflection;
using static MRO.ROI.Automation.Utility.IniFile;

namespace MRO.ROI.Automation.Pages
{
    public class ROIFacilityImportFilePage
    {
        public RemoteWebDriver Driver;
        public ExtentTest logger;
        public TestContext Context;        
        public ROIFacilityImportFilePage(RemoteWebDriver driver, ExtentTest _loger, TestContext _context)
        {
            Driver = driver;
            logger = _loger;
            Context = _context;
        }
        public By delimiter = By.XPath("//input[@id='mrocontent_txtDelimiter']");
        public const string importSampleCsv = "SampleBatch.csv";
        public By chooseFile = By.XPath("//input[@id='mrocontent_txtUpload']");
        public By loadBtn = By.XPath("//input[@id='mrocontent_cmdLoad']");
        public By batchId = By.XPath("//input[@id='mrocontent_txtBatchID']");
        public By importBtn = By.XPath("//input[@id='mrocontent_cmdImport']");
        public By importStatus = By.XPath("//tr[@class='TableBody'][1]//td[text()='Import Error']");
        public const string error = "Import Error";
        public By problemsOnImport = By.XPath("//tr[@class='TableBody'][1]//td[contains(text(),' Field (Last Name) is mandatory and missing! Field (First Name) is mandatory and missing!' )]");
        public By problemsOnImportforSecondBatch = By.XPath("//tr[@class='TableBody'][1]//td[@align='center']//following-sibling::td[5]");
        public By selectAllBtn = By.XPath("//*[@id='mrocontent_btnSelAll']");
        public By makeRequestBtn = By.XPath("//*[@id='mrocontent_cmdImportSelected']");

       


        public void ImportBatchFile(string delimiterTxt, string batchID)
        {
            try
            {
                
                Driver.SleepTheThread(5);
                Driver.ClearText(delimiter);
                Driver.SendKeys(delimiter, delimiterTxt);
                Driver.FindElementBy(chooseFile).SendKeys(Path.GetFullPath(Path.Combine(Assembly.GetExecutingAssembly().Location, "..", "..", "..", "..", "MRO.ROI.Automation", "Files", importSampleCsv)));
                Driver.SendKeys(batchId, batchID);
                Driver.Click(loadBtn);
                Driver.SleepTheThread(5);
                Driver.Click(importBtn);
                Driver.SleepTheThread(5);
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to import batch files with details as Message : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }
        public bool VerifyImportFileStatus()
        {
            bool _isDispalyed = false;
            try
            {
                string status = Driver.FindElementBy(importStatus).Text.ToString();
                if (status == error)
                {
                    _isDispalyed = true;
                }
                else {
                    _isDispalyed = false;
                }
                return _isDispalyed;
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to verify import file status with details as Message : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }
        public bool VerifyProblemsonImportError()
        {
            bool _isDispalyed = false;
            try
            {
                IWebElement problemsOnImportError = Driver.FindElementBy(problemsOnImport);
                if (problemsOnImportError.Displayed == true )
                {
                    _isDispalyed = true;
                }
                else
                {
                    _isDispalyed = false;
                }
                return _isDispalyed;
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to verify problems on import error with details as Message : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }
        public bool VerifyProblemsonImportErrorforSecondBatch()
        {
            bool _isDispalyed = false;
            try
            {
                string problemsOnImportErrorSecond = Driver.FindElementBy(problemsOnImportforSecondBatch).Text.ToString();
                if (problemsOnImportErrorSecond == " ")
                {
                    _isDispalyed = true;
                }
                else
                {
                    _isDispalyed = false;
                }
                return _isDispalyed;
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to verify problems on import error for second batch details with Message : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }
        public void MakeARequest()
        {
            try
            {
                Driver.Click(selectAllBtn);
                Driver.Click(makeRequestBtn);
                Driver.AcceptAlert();
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to make a request details with Message : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }

        }

        public void ImportBatchFile(string delimiterTxt, string batchID, string filename)
        {
            try
            {

                Driver.SleepTheThread(5);
                Driver.ClearText(delimiter);
                Driver.SendKeys(delimiter, delimiterTxt);
                Driver.FindElementBy(chooseFile).SendKeys(Path.GetFullPath(Path.Combine(Assembly.GetExecutingAssembly().Location, "..", "..", "..", "..", "MRO.ROI.Automation", "Files", filename)));
                Driver.SendKeys(batchId, batchID);
                Driver.Click(loadBtn);
                Driver.SleepTheThread(5);
                Driver.Click(importBtn);
                Driver.SleepTheThread(5);
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to import batch files with details as Message : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }

    }
}
