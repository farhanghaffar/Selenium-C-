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
    public class ROICBOCreateRequestPage
    {
        public RemoteWebDriver Driver;
        public ExtentTest logger;
        public TestContext Context;
        public ROICBOCreateRequestPage(RemoteWebDriver driver, ExtentTest _loger, TestContext _context)
        {
            Driver = driver;
            logger = _loger;
            Context = _context;
        }
        public By createRequest = By.XPath("//*[starts-with(text(),'Create a Request')]");
        public By recentRequest = By.XPath("//td[contains(text(),'Recent Requests')]");
        public By firstRequest = By.XPath("(//table[starts-with(@id,'mroheader_')])[2]//tr");
        public By firstName = By.XPath("//input[@id='mrocontent_txtFirstName']");
        public By lastName = By.XPath("//input[@id='mrocontent_txtLastName']");
        public const string bostonProperLocation = "Boston Proper";
        public By requestReceivedDate = By.Id("mrocontent_txtRequestRcvdDate");
        public By notes = By.XPath("//textarea[@id='mrocontent_txtNotes']");
        public By claimID = By.XPath("//input[@id='mrocontent_txtClaimID']");
        public By referenceID = By.XPath("//input[@id='mrocontent_txtRefID']");
        public By subscriberID = By.XPath("//input[@id='mrocontent_txtSubscriberID']");
        public By iPlancode = By.Id("mrocontent_lstIPlanCode");
        public By purposeOfUse = By.Id("mrocontent_lstPurposeOfUse");
        public By btnCreateRequest = By.XPath("//input[@id='mrocontent_cmdCreateRequest']");
        private string createdDateTime = DateTime.Now.ToString("dd MMMM yyyy hh:mm:ss");
        public By requester = By.Id("mrocontent_RadCmbBxShipToRequesters_Input");
        public By chkCoverPage = By.XPath("//input[@id='mrocontent_cbCreateCoverPage']");//
        public By btnRequestStatusScreen = By.XPath("//input[@id='mrocontent_cmdViewRequest']");
        public By btnLogANewRequest = By.XPath("//input[@id='mrocontent_cmdLogNew']");
        public By requestID = By.XPath("//td[@id='mrocontent_tdRequestID']");//
        public By addFile = By.Id("mrocontent_RadUploadDocsfile0");
        public string requestPdfFile = "MixedOrientation.pdf";
        public By pan = By.XPath("//input[@id='mrocontent_txtPAN']");
        public By requestStatusScreen = By.XPath("//input[@id='mrocontent_cmdViewRequest']");
        public void ClickOnCreateRequest()
        {
            try
            {
                Driver.Click(createRequest);
            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to click create request with details as Message : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }

        public bool VerifyUseFlags()
        {
            try
            {
                bool isDisplayed = false;
                string recreationalVehicle = "//label[contains(text(),'Recreational Vehicle')]";
                string sandyChkbox = "//label[contains(text(),'Sandy')]";
                string trackOnlyChkbox = "//label[contains(text(),'Track Only')]";
                WebElementHelper helper = new WebElementHelper(Driver, logger, Context);
                bool isReceationalChkbox = helper.IsElementPresent(recreationalVehicle);
                bool isSandyChkBoxPresent = helper.IsElementPresent(sandyChkbox);
                bool isTrackOnlyChkbox = helper.IsElementPresent(trackOnlyChkbox);
                if(isReceationalChkbox==false&&isSandyChkBoxPresent==false&&isTrackOnlyChkbox==false)
                {
                    isDisplayed = false;
                }
                if (isReceationalChkbox == true && isSandyChkBoxPresent == true && isTrackOnlyChkbox == true)
                {
                    isDisplayed = true;
                }
                return isDisplayed;


            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to verify user flags with details as Message : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }

        public void SelectRecentRequest()
        {
            try
            {
                Actions action = new Actions(Driver);
                Driver.Wait(TimeSpan.FromSeconds(2));
                IWebElement recentReq = Driver.FindElementBy(recentRequest);
                action.MoveToElement(recentReq).Perform();
                Driver.Wait(TimeSpan.FromSeconds(1));                
                IWebElement firstReecentRequest = Driver.FindElementBy(firstRequest);
                action.MoveToElement(firstReecentRequest).Click().Build().Perform();
                Driver.Wait(TimeSpan.FromSeconds(1));

            }
            catch (Exception)
            {

                throw;
            }
        }


        public bool VerifyDocumentTypes()
        {
            try
            {
                bool isDisplayed = false;
                string entireChart = "//label[contains(text(),'TOT (Entire Chart)')]";
                string dcSumChkbox = "//label[contains(text(),'D/C Sum')]";
                string ekgChkbox = "//label[contains(text(),'EKG/EEG')]";
                string pertChkbox = "//label[contains(text(),'PERT (Pertinent / Abstract)')]";
                string labsChkbox = "//label[contains(text(),'Labs')]";
                string nurseChkbox = "//input[@id='mrocontent_lstDocTypes_7']";
                string hpChkbox = "//label[contains(text(),'H & P')]";
                string radiologyChkbox = "//label[contains(text(),'Radiology')]";
                string dimuzioChkbox = "//label[contains(text(),'DiMuzio')]";
                WebElementHelper helper = new WebElementHelper(Driver, logger, Context);
                bool isentireChartChkbox = helper.IsElementPresent(entireChart);
                bool isdcSumChkbox = helper.IsElementPresent(dcSumChkbox);
                bool isekgChkbox = helper.IsElementPresent(ekgChkbox);
                bool ispertChkbox = helper.IsElementPresent(pertChkbox);
                bool islabsChkbox = helper.IsElementPresent(labsChkbox);
                bool isnurseChkbox = helper.IsElementPresent(nurseChkbox);
                bool ishpChkbox = helper.IsElementPresent(hpChkbox);
                bool isradiologyChkbox = helper.IsElementPresent(radiologyChkbox);
                bool isdimuzioChkbox = helper.IsElementPresent(dimuzioChkbox);
                if (isentireChartChkbox == false && isdcSumChkbox == false && isekgChkbox == false&&ispertChkbox==false&&islabsChkbox==false&&isnurseChkbox==false&&ishpChkbox==false&&isradiologyChkbox==false&&isdimuzioChkbox==false)
                {
                    isDisplayed = false;
                }
                if (isentireChartChkbox == true && isdcSumChkbox == true && isekgChkbox == true && ispertChkbox == true && islabsChkbox == true && isnurseChkbox == true && ishpChkbox == true && isradiologyChkbox == true && isdimuzioChkbox == true)
                {
                    isDisplayed = true;
                }
                return isDisplayed;


            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to verify document types with details as Message : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }

        public void CreateRequest()
        {
            Random rand = new Random();
            int randValue = rand.Next(10, 1000);
            int panValue = rand.Next(20, 1000);
            string sPanValue = panValue.ToString();
            string sfirstName = "FN" + createdDateTime;
            string sLastName = "LN" + createdDateTime;
            Driver.SendKeys(firstName, sfirstName);
            logger.Log(Status.Info, "First Name entered." + sfirstName);
            Driver.SendKeys(lastName, sLastName);
            logger.Log(Status.Info, "Last Name entered." + sLastName);
            Driver.SendKeys(notes, "This is a test");
            Driver.SendKeys(pan, sPanValue);
            IWebElement dob = Driver.FindElementBy(By.Id(PageElements.LogNewRequestPage.dateOfBith_Id));
            dob.SendKeys(DateTime.Now.AddYears(-25).ToString("MM/dd/yyy"));
            SelectElement locationDropDown = new SelectElement(Driver.FindElementBy(By.Id(PageElements.LogNewRequestPage.intlocation_id)));
            locationDropDown.SelectByText(bostonProperLocation);
            SelectElement iPlanCodeDropdown = new SelectElement(Driver.FindElementBy(iPlancode));
            iPlanCodeDropdown.SelectByText("Test Plan (TES)");
            SelectElement purposeOfUseDropdown = new SelectElement(Driver.FindElementBy(purposeOfUse));
            purposeOfUseDropdown.SelectByText("test");
            Driver.SleepTheThread(5);
            var todaysDate = String.Format("{0:M/dd/yyyy}", DateTime.Now).Replace("-", "/");
            var requestRecievedDate = Driver.FindElementBy(requestReceivedDate);
            requestRecievedDate.Clear();
            requestRecievedDate.SendKeys(todaysDate);
            string sClaimID = "CL" + randValue;
            Driver.SendKeys(claimID, sClaimID);
            string sRefID = "Ref" + randValue;
            Driver.SendKeys(referenceID, sRefID);
            string sSubscriberID = "Sub" + randValue;
            Driver.SendKeys(subscriberID, sSubscriberID);//
            // SelectElement sRequesterDropdown = new SelectElement(Driver.FindElementBy(requester));
            //sRequesterDropdown.SelectByText("Aetna Test (fax)");
            Driver.SendKeys(requester, "Aetna Test (fax)");
            Driver.SleepTheThread(3);
            Driver.Click(btnCreateRequest);
            // ClickOnCreateRequest();
            Driver.SwitchTo().Alert().Accept();
        }

        public bool CheckCreateCoverPage()
        {
            bool isChecked = false;
            try
            {
                Driver.ScrollToEndOfThePage();
                bool bCheckCoverPage = Driver.FindElementBy(chkCoverPage).Selected;
                if (bCheckCoverPage == false)
                {
                    IWebElement iCheckCoverPage = Driver.FindElementBy(chkCoverPage);
                    iCheckCoverPage.Click();
                    isChecked = true;
                }


            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to check Create Cover Page: {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }

            return isChecked;
        }


        public string VerifyButtonsAndGetRequestID()
        {
            bool isDisplayed = false;
            string sRequestID = string.Empty;
            try
            {
                string btnRequestStatus = "//input[@id='mrocontent_cmdViewRequest']";
                string btnLogANewRequest = "//input[@id='mrocontent_cmdLogNew']";
                WebElementHelper helper = new WebElementHelper(Driver, logger, Context);
                bool isBtnRequestStatus = helper.IsElementPresent(btnRequestStatus);
                bool isBtnLogANewRequest = helper.IsElementPresent(btnLogANewRequest);
                if (isBtnRequestStatus == true && isBtnLogANewRequest == true)
                {
                    isDisplayed = true
;
                }
                if (isDisplayed) { sRequestID = Driver.GetText(requestID); }



            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to to verify labels with details as Message : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");

            }
            return sRequestID;
        }

        public void ClickOnCreateRequestButton()
        {
            try
            {
                Driver.Click(btnCreateRequest);
            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to click create request with details as Message : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }

        public void ClickOnRequestStatusScreenButton()
        {
            try
            {
                Driver.DirectClick(btnRequestStatusScreen);
            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to click request status screen button with details as Message : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }

        public void CreateRequestWithPDF()
        {
            Random rand = new Random();
            int randValue = rand.Next(10, 1000);
            int panValue = rand.Next(20, 1000);
            string sPanValue = panValue.ToString();
            string sfirstName = "FN" + createdDateTime;
            string sLastName = "LN" + createdDateTime;
            Driver.SendKeys(firstName, sfirstName);
            logger.Log(Status.Info, "First Name entered." + sfirstName);
            Driver.SendKeys(lastName, sLastName);
            logger.Log(Status.Info, "Last Name entered." + sLastName);
            Driver.SendKeys(notes, "This is a test");
            Driver.SendKeys(pan, sPanValue);
            IWebElement dob = Driver.FindElementBy(By.Id(PageElements.LogNewRequestPage.dateOfBith_Id));
            dob.SendKeys(DateTime.Now.AddYears(-25).ToString("MM/dd/yyy"));
            SelectElement locationDropDown = new SelectElement(Driver.FindElementBy(By.Id(PageElements.LogNewRequestPage.intlocation_id)));
            locationDropDown.SelectByText(bostonProperLocation);
            SelectElement iPlanCodeDropdown = new SelectElement(Driver.FindElementBy(iPlancode));
            iPlanCodeDropdown.SelectByText("Test Plan (TES)");
            SelectElement purposeOfUseDropdown = new SelectElement(Driver.FindElementBy(purposeOfUse));
            purposeOfUseDropdown.SelectByText("test");
            Driver.SleepTheThread(5);
            var todaysDate = String.Format("{0:M/dd/yyyy}", DateTime.Now).Replace("-", "/");
            var requestRecievedDate = Driver.FindElementBy(requestReceivedDate);
            requestRecievedDate.Clear();
            requestRecievedDate.SendKeys(todaysDate);
            string sClaimID = "CL" + randValue;
            Driver.SendKeys(claimID, sClaimID);
            string sRefID = "Ref" + randValue;
            Driver.SendKeys(referenceID, sRefID);
            string sSubscriberID = "Sub" + randValue;
            Driver.SendKeys(subscriberID, sSubscriberID);//           
            Driver.SendKeys(requester, "Aetna Test (fax)");
            Driver.SleepTheThread(3);
            Driver.FindElementBy(addFile).SendKeys(Path.GetFullPath(Path.Combine(Assembly.GetExecutingAssembly().Location, "..", "..", "..", "..", "MRO.ROI.Automation", "Files", requestPdfFile)));
            Driver.SleepTheThread(10);
            Driver.Click(btnCreateRequest);
            // ClickOnCreateRequest();
            Driver.SwitchTo().Alert().Accept();
        }
        public string GetRequestID()
        {
            string sRequestID = string.Empty;
            sRequestID = Driver.GetText(requestID);
            if (sRequestID != null) { }
            return sRequestID;
        }

        public void CreateRequesWithSpecificData()
        {
            try
            {

                Random rand = new Random();
                int randValue = rand.Next(10, 1000);
                int panValue = rand.Next(20, 1000);
                string sPanValue = panValue.ToString();
                string sfirstName = "FN" + createdDateTime;
                string sLastName = "LN" + createdDateTime;
                Driver.SendKeys(firstName, sfirstName);
                logger.Log(Status.Info, "First Name entered." + sfirstName);
                Driver.SendKeys(lastName, sLastName);
                logger.Log(Status.Info, "Last Name entered." + sLastName);
                var todaysDate = String.Format("{0:M/dd/yyyy}", DateTime.Now).Replace("-", "/");
                Driver.SendKeys(notes, todaysDate);
                Driver.SendKeys(pan, sPanValue);

                IWebElement dob = Driver.FindElementBy(By.Id(PageElements.LogNewRequestPage.dateOfBith_Id));
                dob.SendKeys(DateTime.Now.AddYears(-25).ToString("MM/dd/yyy"));
                SelectElement locationDropDown = new SelectElement(Driver.FindElementBy(By.Id(PageElements.LogNewRequestPage.intlocation_id)));
                locationDropDown.SelectByText(bostonProperLocation);
                SelectElement iPlanCodeDropdown = new SelectElement(Driver.FindElementBy(iPlancode));
                iPlanCodeDropdown.SelectByText("Test Plan (TES)");
                SelectElement purposeOfUseDropdown = new SelectElement(Driver.FindElementBy(purposeOfUse));
                purposeOfUseDropdown.SelectByText("test");
                Driver.SleepTheThread(5);

                var requestRecievedDate = Driver.FindElementBy(requestReceivedDate);
                requestRecievedDate.Clear();
                requestRecievedDate.SendKeys(todaysDate);
                Driver.SendKeys(claimID, "5678efgh%%%%");
                Driver.SendKeys(referenceID, "1234abcd$$$$");
                string sSubscriberID = "Sub" + randValue;
                Driver.SendKeys(subscriberID, sSubscriberID);
                Driver.SendKeys(requester, "Aetna Test (fax)");
                Driver.SleepTheThread(3);
                Driver.FindElementBy(addFile).SendKeys(Path.GetFullPath(Path.Combine(Assembly.GetExecutingAssembly().Location, "..", "..", "..", "..", "MRO.ROI.Automation", "Files", requestPdfFile)));
                Driver.SleepTheThread(10);
                Driver.Click(btnCreateRequest);
            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to click create request with details as Message : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }


        }

    }
}
