using AventStack.ExtentReports;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MRO.ROI.Automation.Common.Navigation;
using MRO.ROI.Automation.Pages;
using MRO.ROI.Automation.Pages.Common;
using MRO.ROI.Automation.Selenium;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.UI;
using System;
using System.IO;
using System.Reflection;

namespace MRO.ROI.Automation.Pages
{
    public class ROICreateRequestPage
    {
        public RemoteWebDriver Driver;
        public ExtentTest logger;
        public TestContext Context;
        public ROICreateRequestPage(RemoteWebDriver driver, ExtentTest _loger, TestContext _context)
        {
            Driver = driver;
            logger = _loger;
            Context = _context;
        }
        public string PatientFirstName { get; private set; }
        public string PatientLastName { get; private set; }
        private string createdDateTime = DateTime.Now.ToString("dd MMMM yyyy hh:mm:ss");
        public By recentlyUsedFacilitiesRadioBtn = By.XPath("//label[contains(text(),'Recently used facilities')]");
        public By recentlyUsedFacilitiesDrpdown = By.Id("mrocontent_RadComboBox_RecentFacilities_Arrow");
        public By selectedFacility = By.XPath("(//li[contains(text(),'MRO Automated Regression Test - Boston Proper')])[1]");
        //public static string selectedFacility = "MRO Automated Regression Test - Boston Proper";
        public By searchBtn = By.Id("mrocontent_btnSearch");
        public By nextButton = By.XPath("//input[@id='mrocontent_btnNextBottom']");
        public By requestedByDrp = By.Id("mrocontent_ddlRequesters");
        public By firstName = By.Id("mrocontent_txtBxFirstName");
        public By lastName = By.Id("mrocontent_txtBxLastName");
        public By dateOfBirth = By.Id("mrocontent_txtBxDOB");
        public By dateOfServiceRadioBtn = By.XPath("//label[contains(text(),'Any or All')]");
        public string pdfFile = "Carilion.PDF";
        public string pdfFile1 = "UnityPoint2.PDF";
        public By browseBtn = By.XPath("//input[@class='ruButton ruBrowse ruButtonHover']");
        public By submitBtn = By.Id("mrocontent_btnLogRequest_input");
        public By returnToRequest = By.Id("mrocontent_cmdRequestStatus");
        public By pdfFileName = By.Id("mrocontent_repeaterResults_lblUploadedFile_0");
        public By addFile = By.Id("mrocontent_RadUpload1file0");
        public By submitRequest = By.Id("mrocontent_radWndPrompt_C_btnYes_input");
        public By uploadPdfFileBtn = By.Id("mrocontent_btnUploadFiles");
        public By uploadedFileName = By.XPath("//table[@id='mrocontent_dgUploadedDocuments']/tbody/tr[2]/td[2]");
        public By requestId = By.XPath("//*[@id='frmServer']/table/tbody/tr[2]/td[2]/table/tbody/tr[2]/td[2]/table/tbody/tr[1]/td/table/tbody/tr[2]/td[2]/b");
        public string wellspanPdf = "Wellspan.pdf";
        public By viewInvoiceDetailsBtn = By.XPath("//input[@id='mrocontent_cmdInvoice']");
        public By downloadInvoiceBtn = By.XPath("//input[@id='mrocontent_cmdDownloadInvoice']");
        public By balanceDueValue=By.XPath("//input[@name='cyBalance']");
        public By totalBalanceDue = By.XPath("//*[@id='frmServer']/table/tbody/tr[2]/td[2]/table/tbody/tr[2]/td[2]/table/tbody/tr[1]/td/table/tbody/tr[11]/td[2]/strong");
        public By paynowButton=By.XPath("//input[@id='mrocontent_cmdPay']");

        public void SeelectRecentlyUsedFacility()
        {
            try
            {
                Driver.Click(recentlyUsedFacilitiesRadioBtn);
                Driver.Click(recentlyUsedFacilitiesDrpdown);
                Driver.Click(selectedFacility);            
                Driver.Click(searchBtn);                
                Driver.Click(nextButton);
                
            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to select recently used facility with details as Message : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
           
        }

        public  string SelectedFacilityAtCreateRequestPage()
        {
            try
            {
                string facility = Driver.GetText(By.Id("mrocontent_lblFacilityAddr"));
                return facility;
            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to return facility with details as Message : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }
        public void CreateRequest()
        {
            try
            {
                
                SelectElement monthSelection = new SelectElement(Driver.FindElementBy(requestedByDrp));
                monthSelection.SelectByIndex(1);
                Random rand = new Random();
                int value = rand.Next(10, 1000);
                PatientFirstName = "FN" + createdDateTime;
                PatientLastName = "LN" + value;
                Driver.SendKeys(firstName, PatientFirstName);
                Driver.SendKeys(lastName, PatientLastName);               
                logger.Log(Status.Info, "First Name entered." + PatientFirstName);               
                logger.Log(Status.Info, "Last Name entered." + PatientLastName);
                IWebElement dob = Driver.FindElementBy(dateOfBirth);
                dob.SendKeys(DateTime.Now.AddYears(-25).ToString("MM/dd/yyy"));
                Driver.Click(dateOfServiceRadioBtn);                
                Driver.FindElementBy(addFile).SendKeys(Path.GetFullPath(Path.Combine(Assembly.GetExecutingAssembly().Location, "..", "..", "..", "..", "MRO.ROI.Automation", "Files", pdfFile)));
                Driver.Wait(TimeSpan.FromSeconds(50));
                Driver.Click(submitBtn);
                Driver.Wait(TimeSpan.FromSeconds(10));
                Driver.Click(submitRequest);
               
            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to create request with details as Message : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }
        public void UploadPdf()
        {
            try
            {
                Driver.FindElementBy(addFile).SendKeys(Path.GetFullPath(Path.Combine(Assembly.GetExecutingAssembly().Location, "..", "..", "..", "..", "MRO.ROI.Automation", "Files", pdfFile1)));
                Driver.Wait(TimeSpan.FromSeconds(40));
                Driver.FindElementBy(uploadPdfFileBtn).Click();
                Driver.Wait(TimeSpan.FromSeconds(60));              
               
                
            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to upload pdf file  with details as Message : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }


        public string CheckPdfFileStatus()
        {
            try
            {
                Driver.ScrollToElement(uploadedFileName);
                string fileName = Driver.GetText(uploadedFileName);
                return fileName;

            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to  get  pdf file  status with details as Message : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }
        public string ReturnRequestId()
        {
            try
            {
                Driver.Click(returnToRequest);
                string reqId = Driver.GetText(requestId);
                return reqId;
            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to return created request with details as Message : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }
        public void SelectFacility(string facility)
        {
            try
            {
                Driver.Click(recentlyUsedFacilitiesRadioBtn);              
                Driver.SendKeys(recentlyUsedFacilitiesDrpdown, facility);            
                Driver.Click(searchBtn);
                Driver.Click(nextButton);

            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to select recently used facility with details as Message : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }

        }

        public void CreateRequestBySelectingRequester(string Requester)
        {
            try
            {

                Driver.SendKeys(requestedByDrp, Requester);
                Random rand = new Random();
                int value = rand.Next(10, 1000);
                PatientFirstName = "FN" + value;
                PatientLastName = "LN" + value;
                Driver.Click(firstName);
                Driver.SendKeys(firstName, PatientFirstName);
                Driver.Wait(TimeSpan.FromSeconds(5));
                Driver.SendKeys(lastName, PatientLastName);
                Driver.Wait(TimeSpan.FromSeconds(5));
                logger.Log(Status.Info, "First Name entered." + PatientFirstName);
                logger.Log(Status.Info, "Last Name entered." + PatientLastName);
                IWebElement iDob = Driver.FindElementBy(dateOfBirth);
                string dob = String.Format("{0:MM/dd/yyyy}", DateTime.Now.AddYears(-25)).Replace("-", "/");
                iDob.SendKeys(dob);               
                Driver.Click(dateOfServiceRadioBtn);
                Driver.FindElementBy(addFile).SendKeys(Path.GetFullPath(Path.Combine(Assembly.GetExecutingAssembly().Location, "..", "..", "..", "..", "MRO.ROI.Automation", "Files", wellspanPdf)));
                Driver.Wait(TimeSpan.FromSeconds(5));
                Driver.Click(submitBtn);                
                Driver.Wait(TimeSpan.FromSeconds(5));              

            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to create request with details as Message : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }

        public void ClickOnSubmitRequest()
        {
            try
            {
                Driver.Click(submitRequest);
                Driver.Wait(TimeSpan.FromSeconds(2));

            }
            catch (Exception)
            {

                throw;
            }
        }
        public bool VerifySubmitRequestPopWindow()
        {
            try
            {
                string submitWindow = "//div[@id='RadWindowWrapper_mrocontent_radWndPrompt']";
                WebElementHelper helper = new WebElementHelper(Driver, logger, Context);
                bool isDisplayed = helper.IsElementPresent(submitWindow);
                return isDisplayed;
            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to verify submit popup window with details as Message : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        
        }
        public string VerifyFacility()
        {
            try
            {
                string facility = Driver.GetText(By.XPath("//span[@id='mrocontent_lblFacilityAddr']"));
                return facility;
            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to verify facility with details as Message : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }

        public string VerifyFacilityAtSubmitRequestWindow()
        {
            try
            {
                string facility = Driver.GetText(By.XPath("//table[@id='mrocontent_radWndPrompt_C_tblConfirmInfo']//tr[1]//td[2]"));
                return facility;
            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to verify  facility at submit popup window with details as Message : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }

        public bool VerifyProcessingHistory()
        {
            bool isDisplayed = false;
            try
            {            
                string invoiceDetails = "//input[@id='mrocontent_cmdInvoice']";
                string downloadInvoice = "//input[@id='mrocontent_cmdDownloadInvoice']";
                WebElementHelper helper = new WebElementHelper(Driver, logger, Context);
                bool isInvoiceDetailsDisplayed = helper.IsElementPresent(invoiceDetails);
                bool isDownloadInvoiceDisplayed = helper.IsElementPresent(downloadInvoice);
                if(isInvoiceDetailsDisplayed==true&&isDownloadInvoiceDisplayed==true)
                {
                    Driver.ScrollToElement(By.XPath(invoiceDetails));
                    isDisplayed = true;
                }
                else
                {
                    isDisplayed = false;
                }
                return isDisplayed;

            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to verify processing info with details as Message : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }
        public void ClickOnDownloadInvoice()
        {
            try
            {
                Driver.Click(downloadInvoiceBtn);
            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to click on download inoice with details as Message : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }


     
        public void ClickOnViewInvoice()
        {
            try
            {
                Driver.Click(viewInvoiceDetailsBtn);
            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to click viewinvoicedetails with details as Message : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }

        public string  BalanceDueAtInvoicePage()
        {
            try
            {
                
                string balanceDue= Driver.FindElement(balanceDueValue).GetAttribute("value");
                return balanceDue;
            }
            catch (Exception ex)
            {


                throw new Exception($"Failed to to get invoice amount with details as Message : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            
            }
        }

        public bool VerifyLabels()
        {
            bool isDisplayed = false;
            try
            {
                string invoiceElement = "//td[contains(text(),'Invoice Amount:')]";
                string collectedByFacility = "//td[contains(text(),'Collected by Facility:')]";
                string paymentElement = "//td[contains(text(),'Payment to MRO:')]";
                WebElementHelper helper = new WebElementHelper(Driver, logger, Context);
                bool isInvoice = helper.IsElementPresent(invoiceElement);
                bool isFacility = helper.IsElementPresent(collectedByFacility);
                bool isPayment = helper.IsElementPresent(paymentElement);
                if(isInvoice==true&&isFacility==true&&isPayment==true)
                {
                    isDisplayed = true
;                }


            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to to verify labels with details as Message : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");

            }
            return isDisplayed;
        }


        public string GetTotalBalanceDue()
        {
            try
            {
                string balance = Driver.GetText(totalBalanceDue);
                return balance;

            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to to verify balance due amount with details as Message : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }

        public bool VerifyPaynowButton()
        {
            try
            {
                string paynowButton = "//input[@id='mrocontent_cmdPay']";
                WebElementHelper helper = new WebElementHelper(Driver, logger, Context);
                bool isDisplayed = helper.IsElementPresent(paynowButton);
                return isDisplayed;

            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to to verify paynow button with details as Message : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }


        public void ClickOnPayNowButton()
        {
            try
            {

                Driver.Click(paynowButton);

            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to click paynow button with details as Message : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }
    }
}
