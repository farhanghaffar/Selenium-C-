using AventStack.ExtentReports;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MRO.ROI.Automation.Selenium;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MRO.ROI.Automation.Pages
{
   public class ROIFacilityKeyBatchInfoPage
    {
        public RemoteWebDriver Driver;
        public ExtentTest logger;
        public TestContext Context;
        public ROIFacilityKeyBatchInfoPage(RemoteWebDriver driver, ExtentTest _loger, TestContext _context)
        {
            Driver = driver;
            logger = _loger;
            Context = _context;
        }

        public By NumberOfRequests = By.XPath("//input[@id='mrocontent_txtNewRows']");
        public By CreateBatch = By.XPath("//input[@id='mrocontent_cmdCreateBatch']");
        public By Location = By.XPath("//input[@id='mrocontent_lstLocation_Input']");
        public By DateOfRequest = By.XPath("//input[@id='mrocontent_txtOriginalRequest']");
        public By RequiredByDate = By.XPath("//input[@id='mrocontent_txtRequiredBy']");
        public By BatchDate = By.XPath("//input[@id='mrocontent_txtRequestBatch']");
        public By DateReceivedbyFacility=By.XPath("//input[@id='mrocontent_txtRequestRcvd']");
        public By BatchTitle = By.XPath("//input[@id='mrocontent_txtRequestBatchID']");
        public By LastName = By.XPath("//input[@id='mrocontent_sPatientLastName_txt_1']");
        public By FirstName = By.XPath("//input[@id='mrocontent_sPatientFirstName_txt_1']");
        public By DOB = By.XPath("//input[@id='mrocontent_dtPatientBirth_txt_1']");
        public By SSN = By.XPath("//input[@id='mrocontent_sPatientSSN_txt_1']");
        public By MRN = By.XPath("//input[@id='mrocontent_sPatientMRN_txt_1']");
        public By PAN = By.XPath("//input[@id='mrocontent_sPatientPAN_txt_1']");
        public By DOS = By.XPath("//input[@id='mrocontent_dtService_txt_1']");
        public By SaveBatch = By.XPath("//input[@value='Save Batch']");
        public By LogRequests = By.XPath("//input[@value='Log Requests']");
        public By lblLastName = By.XPath("//td[contains(text(),'Last Name')]");
        public By lblFirstName = By.XPath("//td[contains(text(),'First Name')]");
        public By lblDOB = By.XPath("//td[contains(text(),'DOB')]");
        public By lblSSN = By.XPath("//td[contains(text(),'SSN')]");
        public By lblMRN = By.XPath("//td[contains(text(),'MRN')]");
        public By SystemBatchNumber = By.XPath("//span[@id='mrocontent_lblImportBatchID']");
        public By LastName2 = By.XPath("//input[@id='mrocontent_sPatientLastName_txt_2']");
        public By FirstName2 = By.XPath("//input[@id='mrocontent_sPatientFirstName_txt_2']");
        public By DOB2 = By.XPath("//input[@id='mrocontent_dtPatientBirth_txt_2']");
        public By SSN2 = By.XPath("//input[@id='mrocontent_sPatientSSN_txt_2']");
        public By MRN2 = By.XPath("//input[@id='mrocontent_sPatientMRN_txt_2']");
        public By PAN2 = By.XPath("//input[@id='mrocontent_sPatientPAN_txt_2']");
        public By DOS2 = By.XPath("//input[@id='mrocontent_dtService_txt_2']");

        private string createdDateTime = DateTime.Now.ToString("dd MMMM yyyy hh:mm:ss");
        public string PatientFirstName { get; private set; }
        public string PatientLastName { get; private set; }
        public By drpLocation = By.XPath("//div[@id='mrocontent_lstLocation_DropDown']//ul/li");
        public const string bostonProperLocation = "Boston Proper";
        public By ComplainceHold = By.XPath("//*[@id='mrocontent_cbComplianceHold']");

        public void ClickCreateBatch()
        {
            try
            {                
                IWebElement numberOfRequests = Driver.FindElementBy(NumberOfRequests);
                numberOfRequests.SendKeys("1");
                IWebElement createBatch = Driver.FindElementBy(CreateBatch);
                createBatch.Click();
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to click on create batch : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }

        public void ClickOnSaveBatchAndLogRequest()
        {
            try
            {
                var location = Driver.FindElementBy(Location);
                var locationDropDown = Driver.FindElementBy(Location);
                var locationType = new SelectElement(locationDropDown);
                locationType.SelectByText("Boston Proper");
                IWebElement requiredByDate = Driver.FindElementBy(RequiredByDate);
                requiredByDate.SendKeys(DateTime.Now.ToShortDateString());
                IWebElement batchDate = Driver.FindElementBy(BatchDate);
                batchDate.SendKeys(DateTime.Now.ToShortDateString());
                IWebElement dateReceivedbyFacility = Driver.FindElementBy(DateReceivedbyFacility);
                dateReceivedbyFacility.SendKeys(DateTime.Now.ToShortDateString());
                IWebElement batchTitle = Driver.FindElementBy(BatchTitle);
                batchTitle.SendKeys("Set");
                IWebElement lastName = Driver.FindElementBy(LastName);
                lastName.SendKeys("Cigniti" + DateTime.Now);
                IWebElement firstName = Driver.FindElementBy(FirstName);
                firstName.SendKeys("Rajesh" + DateTime.Now);
                IWebElement dOB = Driver.FindElementBy(DOB);
                dOB.SendKeys("04/12/1993");
                IWebElement sSN = Driver.FindElementBy(SSN);
                sSN.SendKeys("123456789");
                IWebElement mRN = Driver.FindElementBy(MRN);
                mRN.SendKeys("MRN");
                IWebElement pAN = Driver.FindElementBy(PAN);
                pAN.SendKeys("PAN");
                IWebElement dOS = Driver.FindElementBy(DOS);
                dOS.SendKeys(DateTime.Now.ToShortDateString());
                IWebElement saveBatch = Driver.FindElementBy(SaveBatch);
                saveBatch.Click();
                IWebElement logRequests = Driver.FindElementBy(LogRequests);
                logRequests.Click();
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to click on create batch : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }

        public void ClickOnSaveBatchAndLogtwoRequest()
        {
            try
            {
                
                var locationDropDown = Driver.FindElementBy(Location);                
                locationDropDown.SendKeys("123Cape May");
                IWebElement batchTitle = Driver.FindElementBy(BatchTitle);
                batchTitle.SendKeys("Set");
                batchTitle.Click();
                Driver.WaitForPageToLoad();
                Driver.WaitInSeconds(5);
                IWebElement dateOfRequest = Driver.FindElementBy(DateOfRequest);
                Driver.ScrollIntoView(dateOfRequest);
                dateOfRequest.SendKeys(DateTime.Now.ToShortDateString());
                IWebElement requiredByDate = Driver.FindElementBy(RequiredByDate);
                requiredByDate.SendKeys(DateTime.Now.ToShortDateString());
                IWebElement batchDate = Driver.FindElementBy(BatchDate);
                batchDate.SendKeys(DateTime.Now.ToShortDateString());
                IWebElement dateReceivedbyFacility = Driver.FindElementBy(DateReceivedbyFacility);
                dateReceivedbyFacility.SendKeys(DateTime.Now.ToShortDateString());                
                IWebElement lastName = Driver.FindElementBy(LastName);
                lastName.SendKeys("Cigniti" + DateTime.Now);
                IWebElement firstName = Driver.FindElementBy(FirstName);
                firstName.SendKeys("Rajesh" + DateTime.Now);
                IWebElement dOB = Driver.FindElementBy(DOB);
                dOB.SendKeys("04/12/1993");
                IWebElement sSN = Driver.FindElementBy(SSN);
                sSN.SendKeys("123456789");
                IWebElement mRN = Driver.FindElementBy(MRN);
                mRN.SendKeys("MRN");
                IWebElement pAN = Driver.FindElementBy(PAN);
                pAN.SendKeys("PAN");
                IWebElement dOS = Driver.FindElementBy(DOS);
                dOS.SendKeys(DateTime.Now.ToShortDateString());
                IWebElement lastName2 = Driver.FindElementBy(LastName2);
                lastName2.SendKeys("Cigniti" + DateTime.Now);
                IWebElement firstName2 = Driver.FindElementBy(FirstName2);
                firstName2.SendKeys("Rajesh" + DateTime.Now);
                IWebElement dOB2 = Driver.FindElementBy(DOB2);
                dOB2.SendKeys("04/12/1993");
                IWebElement sSN2 = Driver.FindElementBy(SSN2);
                sSN2.SendKeys("123456798");
                IWebElement mRN2 = Driver.FindElementBy(MRN2);
                mRN2.SendKeys("MRN");
                IWebElement pAN2 = Driver.FindElementBy(PAN2);
                pAN2.SendKeys("PAN");
                IWebElement dOS2 = Driver.FindElementBy(DOS2);
                dOS2.SendKeys(DateTime.Now.ToShortDateString());
                Driver.FindElementBy(ComplainceHold).Click();
                IWebElement saveBatch = Driver.FindElementBy(SaveBatch);
                saveBatch.Click();
                IWebElement logRequests = Driver.FindElementBy(LogRequests);
                logRequests.Click();
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to click on create batch : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }
        public void ClickCreateBatchRequest()
        {
            try
            {
                IWebElement numberOfRequests = Driver.FindElementBy(NumberOfRequests);
                numberOfRequests.SendKeys("3");
                IWebElement createBatch = Driver.FindElementBy(CreateBatch);
                createBatch.Click();
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to click on create batch : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }

        public void ClickCreatetwoBatchRequest()
        {
            try
            {
                IWebElement numberOfRequests = Driver.FindElementBy(NumberOfRequests);
                numberOfRequests.SendKeys("2");
                IWebElement createBatch = Driver.FindElementBy(CreateBatch);
                createBatch.Click();
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to click on create batch : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }

        public bool VerifyKeyBatchInfoGridHeader()
        {
            bool _isDisplayed = false;
            try
            {
                if (Driver.FindElementBy(lblLastName).Displayed && Driver.FindElementBy(lblFirstName).Displayed
                    && Driver.FindElementBy(lblDOB).Displayed && Driver.FindElementBy(lblSSN).Displayed && Driver.FindElementBy(lblMRN).Displayed)
                {
                    _isDisplayed = true;
                }
                else {
                    _isDisplayed = false;
                }
                return _isDisplayed;
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to verify key batch info grid headers : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }
        public string GetSystemBatchnumber()
        {
            try
            {
                string batchNumber = Driver.FindElementBy(SystemBatchNumber).Text;
                return batchNumber;
            }          
            catch (Exception ex)
            {
                throw new Exception($"Failed to get system batch number : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }
        public void ClickOnSaveBatchAndLogKeyBatchInfoRequest()
        {
            try
            {                
                PatientFirstName = "FN" + createdDateTime;
                PatientLastName = "LN" + createdDateTime;                
                var locationDropDown = Driver.FindElementBy(By.Id(PageElements.LogNewRequestPage.locationDropDown_id));
                locationDropDown.Click();
                Driver.SelectValueFromDD(drpLocation, bostonProperLocation);
                Driver.SleepTheThread(5);
                IWebElement requiredByDate = Driver.FindElementBy(RequiredByDate);
                requiredByDate.Clear();
                requiredByDate.SendKeys(DateTime.Now.ToShortDateString());
                IWebElement batchDate = Driver.FindElementBy(BatchDate);
                batchDate.Clear();
                batchDate.SendKeys(DateTime.Now.ToShortDateString());
                IWebElement dateReceivedbyFacility = Driver.FindElementBy(DateReceivedbyFacility);
                dateReceivedbyFacility.Clear();
                dateReceivedbyFacility.SendKeys(DateTime.Now.ToShortDateString());
                IWebElement batchTitle = Driver.FindElementBy(BatchTitle);
                batchTitle.Clear();
                batchTitle.SendKeys("Test123");
                for (int i = 1; i <= 3; i++)
                {
                    IWebElement lastName = Driver.FindElementBy(By.XPath($"//input[@id='mrocontent_sPatientLastName_txt_{i}']"));
                    lastName.SendKeys(PatientLastName);
                    Driver.SleepTheThread(2);
                    IWebElement firstName = Driver.FindElementBy(By.XPath($"//input[@id='mrocontent_sPatientFirstName_txt_{i}']"));
                    firstName.SendKeys(PatientFirstName);
                    IWebElement dOB = Driver.FindElementBy(By.XPath($"//input[@id='mrocontent_dtPatientBirth_txt_{i}']"));
                    dOB.SendKeys(DateTime.Now.AddYears(-25).ToString("MM/dd/yyy"));
                    IWebElement sSN = Driver.FindElementBy(By.XPath($"//input[@id='mrocontent_sPatientSSN_txt_{i}']"));
                    sSN.SendKeys("123456789");
                    IWebElement mRN = Driver.FindElementBy(By.XPath($"//input[@id='mrocontent_sPatientMRN_txt_{i}']"));
                    mRN.SendKeys("MRN");
                    IWebElement pAN = Driver.FindElementBy(By.XPath($"//input[@id='mrocontent_sPatientPAN_txt_{i}']"));
                    pAN.SendKeys("PAN");
                    IWebElement dOS = Driver.FindElementBy(By.XPath($"//input[@id='mrocontent_dtService_txt_{i}']"));
                    dOS.SendKeys(DateTime.Now.ToShortDateString());
                }
                IWebElement saveBatch = Driver.FindElementBy(SaveBatch);
                saveBatch.Click();
                IWebElement logRequests = Driver.FindElementBy(LogRequests);
                logRequests.Click();
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to click on create key batch info request : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }
    }
}
