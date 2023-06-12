using AventStack.ExtentReports;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MRO.ROI.Automation.Selenium;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.UI;
using System;

namespace MRO.ROI.Automation.Pages
{
    public class ROIAdminAssignROIRequesterPage
    {
        public RemoteWebDriver Driver;
        public ExtentTest logger;
        public TestContext Context;
        public ROIAdminAssignROIRequesterPage(RemoteWebDriver driver, ExtentTest _loger, TestContext _context)
        {
            Driver = driver;
            logger = _loger;
            Context = _context;
        }

        public Random random = new Random();
        public const string scrollElement = "mrocontent_RadAjaxPanelRequesterDetail";
        public const string puposeOfElementSelection = "Other";
        public By searchIdElement = By.Id("mrocontent_txtSearchOrganization");
        public By searchButton = By.Id("mrocontent_btnSearchRequester");
        public By selectionCheckbox = By.Id("mrocontent_dgRequester_ctl00_ctl04_ClientSelectColumn1SelectCheckBox");
        public By copyallButton = By.Id("btnCopyAll");
        public By saveButon = By.Id("mrocontent_btnSave");
        public By doneButton = By.Id("mrocontent_btnClose");
        public By refIdForTestAttroney = By.XPath("//input[@id='mrocontent_txtRefID']");
        public By purposeOfUse = By.XPath("//select[@id='mrocontent_lstPurposeOfUse']");
        public By removeRequestButton = By.XPath("//input[@id='mrocontent_cmdRemoveRequester']");
        public By shipToBtn = By.XPath("//input[@id='mrocontent_btnShipTo']");
        public By assignROIRequesterHeader = By.XPath("//td[@id='MasterHeaderText']");
        public By cancelBtn = By.XPath("//input[@id='mrocontent_btnCancel']");
        public By emailTxtBox = By.Id("mrocontent_txtEmail");
        public By labelMsg = By.Id("mrocontent_lblMsg");
        public By requesterIdTxtBox = By.Id("mrocontent_txtSearchRequesterID");
        public By requesterTypeVal = By.Id("mrocontent_lblReqType");
        public const string purposeOfUseSelection = "HEDIS";
        //public By labelMsg = By.Id("mrocontent_lblMsg");
        public const string requesterID = "73666";
        public By drpFacilityAdjustedRate = By.XPath("//select[@id='mrocontent_lstFacilityAdjustedRates']");
        public const string facilityAdjustedRate = "FAR Rate - flat $6.50";
        public By claimId = By.Id("mrocontent_txtClaimID");
        public By firstName = By.XPath("//input[@id='mrocontent_txtFirstName']");
        public By lastname = By.XPath("//input[@id='mrocontent_txtLastName']");
        public By phoneNo = By.XPath("//input[@id='mrocontent_txtPhone']");
        public By fax = By.XPath("//input[@id='mrocontent_txtFax']");
        public By receivedDate = By.XPath("//input[@id='mrocontent_txtRcvdDate']");
        public By searchByIdElement = By.Id("mrocontent_txtSearchRequesterID");
        public By type = By.XPath("//select[@id='mrocontent_lstSearchType']");
        public By typeElement = By.XPath("//option[contains(text(),'Patient')]");
        public By fN = By.XPath("//input[@id='mrocontent_txtFirstName']");
        public By lN = By.XPath("//input[@id='mrocontent_txtLastName']");
        public By city = By.XPath("//input[@id='mrocontent_txtCity']");
        public By state = By.XPath("//input[@id='mrocontent_txtState']");
        public By zipCode = By.XPath("//input[@id='mrocontent_txtZipCode']");
        /// <summary>
        /// To assign Test attroney to created request
        /// </summary>
        public ROIAdminRequestStatusPage assignTestAttorney(string sSearch = "Test Attorney's")
        {
            try
            {
                Driver.ScrollIntoViewAndClick(searchIdElement);
                Driver.SendKeys(searchIdElement, sSearch);                
                Driver.Click(searchButton);
                Driver.FindElementBy(selectionCheckbox);
                Driver.ScrollIntoViewAndClick(selectionCheckbox);
                Driver.Click(copyallButton);
                int refId = random.Next(10, 1000);
                Driver.Wait(TimeSpan.FromSeconds(2));
                Driver.ScrollIntoViewAndClick(refIdForTestAttroney);
                Driver.SendKeys(refIdForTestAttroney, refId.ToString());                
                SelectElement oSelect = new SelectElement(Driver.FindElementBy(purposeOfUse));
                oSelect.SelectByText(puposeOfElementSelection);
                Driver.DirectClick(saveButon);
                Driver.Wait(TimeSpan.FromSeconds(2));
                Driver.DirectClick(doneButton);
                Driver.Wait(TimeSpan.FromSeconds(2));

            }
            catch(Exception ex)
            {
                throw new Exception($"Failed  to assign Test Attorney with Message : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }

            return new ROIAdminRequestStatusPage(Driver, logger, Context);

        }

        public void AssignRequest(string sSearch = "Test Attorney's")
        {
            try
            {
                ROIAdminRequestStatusPage adminRequestStatusPage = new ROIAdminRequestStatusPage(Driver, logger, Context);
                adminRequestStatusPage.assignRequester();
                Driver.ScrollIntoViewAndClick(searchIdElement);
                Driver.SendKeys(searchIdElement, sSearch);
                Driver.Click(searchButton);
                Driver.FindElementBy(selectionCheckbox);
                Driver.ScrollIntoViewAndClick(selectionCheckbox);
                Driver.Click(copyallButton);
                int refId = random.Next(10, 1000);
                Driver.ScrollIntoViewAndClick(refIdForTestAttroney);
                Driver.SendKeys(refIdForTestAttroney, refId.ToString());
                SelectElement oSelect = new SelectElement(Driver.FindElementBy(purposeOfUse));
                oSelect.SelectByText(puposeOfElementSelection);
                Driver.DirectClick(saveButon);
                Driver.Wait(TimeSpan.FromSeconds(2));
                Driver.DirectClick(doneButton);

            }
            catch (Exception ex)
            {
                throw new Exception($"Failed  to assign Test Attorney Message : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }

        public void RemoveRequest()
        {
            try
            {
                Driver.DirectClick(removeRequestButton);
                Driver.AcceptAlert();

            }
            catch (Exception ex)
            {
                throw new Exception($"Failed remove request Message : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }
       
        public  void scrollIntoView(string locatorKey)
        {
            try
            {
                IWebElement element = getElement(locatorKey);
                RemoteWebDriver dr = Driver;
                IJavaScriptExecutor js = (IJavaScriptExecutor)dr;
                js.ExecuteScript("arguments[0].scrollIntoView(true);", element);
            }
            catch(Exception ex)
            {
                throw new Exception($"Failed to scroll in to view : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }

        private  IWebElement getElement(string v)
        {
            try
            {
                return Driver.FindElement(By.Id(v));

            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to get element : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            } 
        }
        public void ClickOnShipTo()
        {
            try
            {
                Driver.Click(shipToBtn);
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to click on ship to : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }

        public bool VerifyAssignROIRequesterpage()
        {
            bool isDisplayed = false;
            try
            {
                IWebElement assignRequesterPage = Driver.FindElementBy(assignROIRequesterHeader);
                if (assignRequesterPage.Displayed == true)
                {
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
                throw new Exception($"Failed to verify assign roi requester page : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }

        }

        public void ClickOnCancelTwice()
        {
            try
            {
                Driver.Click(cancelBtn);
                Driver.SleepTheThread(5);
                //Driver.Click(cancelBtn);
                //Driver.SleepTheThread(5);
            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to click on cancel twice : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }



        public void AssignRequestWithMailId(string sSearch = "Test Attorney's")
        {
            try
            {

                Driver.ScrollIntoViewAndClick(searchIdElement);
                Driver.SendKeys(searchIdElement, sSearch);
                Driver.Click(searchButton);
                Driver.FindElementBy(selectionCheckbox);
                Driver.ScrollIntoViewAndClick(selectionCheckbox);
                Driver.Click(copyallButton);
                Driver.ClearText(emailTxtBox);
                Driver.SendKeys(emailTxtBox, "test@gmail.com");
                SelectElement oSelect = new SelectElement(Driver.FindElementBy(purposeOfUse));
                oSelect.SelectByText(puposeOfElementSelection);
                Driver.DirectClick(saveButon);
                Driver.Wait(TimeSpan.FromSeconds(2));
                Driver.DirectClick(doneButton);

            }
            catch (Exception ex)
            {
                throw new Exception($"Failed  to assign Test Attorney Message : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }

        public string AssignPatientRequester(string requesterId)
        {
            try
            {
                Driver.SendKeys(requesterIdTxtBox, requesterId);
                Driver.Click(searchButton);
                Driver.FindElementBy(selectionCheckbox);
                Driver.ScrollIntoViewAndClick(selectionCheckbox);
                Driver.Click(copyallButton);
                //string requesterType =VerifyRequesterType();
                string requesterType = Driver.GetText(requesterTypeVal);
                Driver.ClearText(emailTxtBox);
                Driver.SendKeys(emailTxtBox, "test@gmail.com");
                SelectElement oSelect = new SelectElement(Driver.FindElementBy(purposeOfUse));
                oSelect.SelectByText(puposeOfElementSelection);
                Driver.DirectClick(saveButon);
                Driver.Wait(TimeSpan.FromSeconds(5));
                Driver.Click(shipToBtn);
                Driver.Wait(TimeSpan.FromSeconds(2));
                //Driver.ScrollToElement(requesterTypeVal);
                return requesterType;

            }
            catch (Exception ex)
            {
                throw new Exception($"Failed  to assign patient requester with Message : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }
        public string VerifyRequesterType()
        {
            try
            {
                string requesterType = Driver.GetText(requesterTypeVal);
                return requesterType;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public string VerifyInfo()
        {
            try
            {
                string billToMsg = Driver.GetText(labelMsg);
                return billToMsg;
            }
            catch (Exception ex)
            {

                throw new Exception($"Failed  to get info with Message : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }

        public void ApplyRequester()
        {
            try
            {
                int refId = random.Next(10, 1000);
                Driver.ScrollIntoViewAndClick(refIdForTestAttroney);
                Driver.SendKeys(refIdForTestAttroney, refId.ToString());
                SelectElement oSelect = new SelectElement(Driver.FindElementBy(purposeOfUse));
                oSelect.SelectByText(puposeOfElementSelection);
                Driver.DirectClick(saveButon);
                Driver.Wait(TimeSpan.FromSeconds(2));
                Driver.DirectClick(doneButton);
            }
            catch (Exception ex)
            {

                throw new Exception($"Failed  to assign requester with Message : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }

        public string AssignRequestandAddShipTo(string sSearch = "Test Attorney's")
        {
            try
            {
               
                Driver.ScrollIntoViewAndClick(searchIdElement);
                Driver.SendKeys(searchIdElement, sSearch);
                Driver.Click(searchButton);
                Driver.FindElementBy(selectionCheckbox);
                Driver.ScrollIntoViewAndClick(selectionCheckbox);
                Driver.Click(copyallButton);
                int refId = random.Next(10, 1000);
                Driver.ScrollIntoViewAndClick(refIdForTestAttroney);
                Driver.SendKeys(refIdForTestAttroney, refId.ToString());
                SelectElement oSelect = new SelectElement(Driver.FindElementBy(purposeOfUse));
                oSelect.SelectByText(puposeOfElementSelection);
                Driver.DirectClick(saveButon);
                Driver.Wait(TimeSpan.FromSeconds(2));
                Driver.Click(shipToBtn);
                //Driver.Click(saveButon);
                Driver.Wait(TimeSpan.FromSeconds(5));
                Driver.DirectClick(doneButton);
                string msg=Driver.GetText(labelMsg);
                return msg;


            }
            catch (Exception ex)
            {
                throw new Exception($"Failed  to assign Test Attorney Message : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }


        public void AssignTestABC(string sSearch = "Test ABC")
        {
            try
            {

                Driver.ScrollIntoViewAndClick(searchIdElement);
                Driver.SendKeys(searchIdElement, sSearch);
                Driver.Click(searchButton);
                Driver.FindElementBy(selectionCheckbox);
                Driver.ScrollIntoViewAndClick(selectionCheckbox);
                Driver.Click(copyallButton);                
                Driver.DirectClick(saveButon);
                Driver.Wait(TimeSpan.FromSeconds(5));              
                Driver.DirectClick(doneButton);             


            }
            catch (Exception ex)
            {
                throw new Exception($"Failed  to assign Test ABC Message : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }

        public ROIAdminRequestStatusPage assignTestAttorneyAndChangePurposeOfUse(string sSearch = "Test Attorney's")
        {
            try
            {
                Driver.ScrollIntoViewAndClick(searchIdElement);
                Driver.SendKeys(searchIdElement, sSearch);
                Driver.Click(searchButton);
                Driver.FindElementBy(selectionCheckbox);
                Driver.ScrollIntoViewAndClick(selectionCheckbox);
                Driver.Click(copyallButton);
                int refId = random.Next(10, 1000);
                Driver.ScrollIntoViewAndClick(refIdForTestAttroney);
                Driver.SendKeys(refIdForTestAttroney, refId.ToString());
                SelectElement oSelect = new SelectElement(Driver.FindElementBy(purposeOfUse));
                oSelect.SelectByText(purposeOfUseSelection);
                Driver.DirectClick(saveButon);
                Driver.Wait(TimeSpan.FromSeconds(2));
                Driver.DirectClick(doneButton);

            }
            catch (Exception ex)
            {
                throw new Exception($"Failed  to assign Test Attorney with Message : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }

            return new ROIAdminRequestStatusPage(Driver, logger, Context);

        }
        public void AssignTestGATaxRequester()
        {
            try
            {
                Driver.SendKeys(requesterIdTxtBox, requesterID);
                Driver.Click(searchButton);
                Driver.FindElementBy(selectionCheckbox);
                Driver.ScrollIntoViewAndClick(selectionCheckbox);
                Driver.Click(copyallButton);
                int refId = random.Next(10, 1000);
                Driver.ScrollIntoViewAndClick(refIdForTestAttroney);
                Driver.SendKeys(refIdForTestAttroney, refId.ToString());
                SelectElement oSelect = new SelectElement(Driver.FindElementBy(purposeOfUse));
                oSelect.SelectByText(puposeOfElementSelection);
                Driver.DirectClick(saveButon);
                Driver.Wait(TimeSpan.FromSeconds(2));
                Driver.DirectClick(doneButton);

            }
            catch (Exception ex)
            {
                throw new Exception($"Failed  to assign test GA tax requester with Message : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }

        public void SelectFacilityAdjustedRate()
        {

            SelectElement sFacilityAdjustedRate = new SelectElement(Driver.FindElementBy(drpFacilityAdjustedRate));
            sFacilityAdjustedRate.SelectByText(facilityAdjustedRate);

        }

        public void UpdatePurposeOfUse(string sSearch = "Test Attorney's")
        {
            try
            {
                Driver.ScrollIntoViewAndClick(searchIdElement);
                Driver.SendKeys(searchIdElement, sSearch);
                Driver.Click(searchButton);
                Driver.FindElementBy(selectionCheckbox);
                Driver.ScrollIntoViewAndClick(selectionCheckbox);
                Driver.Click(copyallButton);
                int refId = random.Next(10, 1000);
                Driver.ScrollIntoViewAndClick(refIdForTestAttroney);
                Driver.SendKeys(refIdForTestAttroney, refId.ToString());
                SelectElement oSelect = new SelectElement(Driver.FindElementBy(purposeOfUse));
                oSelect.SelectByText("Claims Attachment");
                Driver.DirectClick(saveButon);
                Driver.Wait(TimeSpan.FromSeconds(2));
                Driver.DirectClick(doneButton);

            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to update purpose of use with Message : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }          

        }

        public void AssignDukeClinicalResearchInstitute(string requesterId = "20159")
        {
            try
            {
                Driver.ScrollIntoViewAndClick(searchIdElement);
                Driver.Click(searchButton);
                Driver.FindElementBy(selectionCheckbox);
                Driver.ScrollIntoViewAndClick(selectionCheckbox);
                Driver.Click(copyallButton);
                int refId = random.Next(10, 1000);
                Driver.ScrollIntoViewAndClick(refIdForTestAttroney);
                Driver.SendKeys(refIdForTestAttroney, refId.ToString());
                SelectElement oSelect = new SelectElement(Driver.FindElementBy(purposeOfUse));
                oSelect.SelectByText(puposeOfElementSelection);
                Driver.DirectClick(saveButon);
                Driver.Wait(TimeSpan.FromSeconds(2));
                Driver.DirectClick(doneButton);

            }
            catch (Exception ex)
            {
                throw new Exception($"Failed  to assign requester with Message : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
          
        }

        public string GetClaimIdOnAdminRSS()
        {
            try
            {
                string claimNum = Driver.FindElementBy(claimId).GetAttribute("value");
                return claimNum;
            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to get claim id with details as: {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }


        public void AssignRequester(string sSearch = "Test Attorney")
        {
            try
            {
                Driver.ScrollIntoViewAndClick(searchIdElement);
                Driver.SendKeys(searchIdElement, sSearch);
                Driver.Click(searchButton);
                Driver.FindElementBy(selectionCheckbox);
                Driver.ScrollIntoViewAndClick(selectionCheckbox);
                Driver.Click(copyallButton);
                int refId = random.Next(10, 1000);
                Driver.ScrollIntoViewAndClick(refIdForTestAttroney);
                Driver.SendKeys(refIdForTestAttroney, refId.ToString());
                SelectElement oSelect = new SelectElement(Driver.FindElementBy(purposeOfUse));
                oSelect.SelectByText(puposeOfElementSelection);
                Driver.DirectClick(saveButon);
                Driver.Wait(TimeSpan.FromSeconds(2));
                Driver.DirectClick(doneButton);

            }
            catch (Exception ex)
            {
                throw new Exception($"Failed  to assign Test Attorney with Message : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }

          

        }

        public string GetReferenceIdOnAdminRSS()
        {
            try
            {
                string claimNum = Driver.FindElementBy(refIdForTestAttroney).GetAttribute("value");
                return claimNum;
            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to get reference id with details as: {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }

        public void ClickOnSaveButton()
        {
            try
            {
                Driver.Click(saveButon);
                Driver.Wait(TimeSpan.FromSeconds(2));
                Driver.Click(doneButton);
                Driver.Wait(TimeSpan.FromSeconds(2));
            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to click on save button with details as: {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }

        public ROIAdminRequestStatusPage assignTestAttorneyWithAdditionalEmail(string sSearch = "Test Attorney's")
        {
            try
            {
                Driver.ScrollIntoViewAndClick(searchIdElement);
                Driver.SendKeys(searchIdElement, sSearch);
                Driver.Click(searchButton);
                Driver.FindElementBy(selectionCheckbox);
                Driver.ScrollIntoViewAndClick(selectionCheckbox);
                Driver.Click(copyallButton);
                Driver.SendKeys(emailTxtBox, "test@mrocorp.com; testMRO@mrocorp.com");
                Driver.ScrollIntoViewAndClick(refIdForTestAttroney);
                Driver.SendKeys(refIdForTestAttroney, "67676");
                SelectElement oSelect = new SelectElement(Driver.FindElementBy(purposeOfUse));
                oSelect.SelectByText(puposeOfElementSelection);

                Driver.DirectClick(saveButon);
                Driver.Wait(TimeSpan.FromSeconds(2));
                Driver.DirectClick(doneButton);

            }
            catch (Exception ex)
            {
                throw new Exception($"Failed  to assign Test Attorney with Message : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }

            return new ROIAdminRequestStatusPage(Driver, logger, Context);

        }


        public void UpdateRequesterInformation(string sSearch,string fname , string laname,string phno,string faxVal, string emailVal,string recDate)
        {
            try
            {

                Driver.ScrollIntoViewAndClick(searchIdElement);
                Driver.SendKeys(searchIdElement, sSearch);
                Driver.Click(searchButton);
                Driver.FindElementBy(selectionCheckbox);
                Driver.ScrollIntoViewAndClick(selectionCheckbox);
                Driver.Click(copyallButton);
                int refId = random.Next(10, 1000);

                Driver.ScrollIntoViewAndClick(refIdForTestAttroney);
                Driver.SendKeys(refIdForTestAttroney, refId.ToString());
                Driver.ClearText(firstName);
                Driver.SendKeys(firstName, fname);
                Driver.ClearText(lastname);
                Driver.SendKeys(lastname, laname);

                Driver.ClearText(phoneNo);
                Driver.SendKeys(phoneNo, phno);
                Driver.ClearText(fax);
                Driver.SendKeys(fax, faxVal);

                Driver.ClearText(emailTxtBox);
                Driver.SendKeys(emailTxtBox, emailVal);
                Driver.ClearText(receivedDate);
                Driver.SendKeys(receivedDate, recDate);

                SelectElement oSelect = new SelectElement(Driver.FindElementBy(purposeOfUse));
                oSelect.SelectByText(puposeOfElementSelection);

                Driver.DirectClick(saveButon);
                Driver.Wait(TimeSpan.FromSeconds(2));
                Driver.DirectClick(doneButton);

            }
            catch (Exception ex)
            {

                throw new Exception($"Failed  to update patient details with Message : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }
        public void AssignTestDonnaandAddShipTo(string sSearch = "265981")
        {
            try
            {

                Driver.ScrollIntoViewAndClick(searchByIdElement);
                Driver.SendKeys(searchByIdElement, sSearch);
                Driver.Click(searchButton);
                Driver.FindElementBy(selectionCheckbox);
                Driver.ScrollIntoViewAndClick(selectionCheckbox);
                Driver.Click(copyallButton);
                int refId = random.Next(10, 1000);
                Driver.ScrollIntoViewAndClick(refIdForTestAttroney);
                Driver.SendKeys(refIdForTestAttroney, refId.ToString());
                SelectElement oSelect = new SelectElement(Driver.FindElementBy(purposeOfUse));
                oSelect.SelectByText(puposeOfElementSelection);
                Driver.Wait(TimeSpan.FromSeconds(2));
                Driver.DirectClick(saveButon);
                Driver.SleepTheThread(5);
                Driver.Click(shipToBtn);
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed  to assign Test-Donna Message : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }
        public void UpadateRequester(String typeElement)
        {
            try
            {
                Driver.Click(type);
                Driver.SendKeys(type, typeElement);
                Driver.Wait(TimeSpan.FromSeconds(2));
                Driver.ScrollIntoViewAndClick(searchByIdElement);
                Driver.ClearText(searchByIdElement);
                Driver.SendKeys(searchByIdElement, "2027");
                Driver.Click(searchButton);
                Driver.FindElementBy(selectionCheckbox);
                Driver.ScrollIntoViewAndClick(selectionCheckbox);
                Driver.SendKeys(fN, "Test");
                Driver.SendKeys(lN, "Patient");
                Driver.SendKeys(city, "Boston");
                Driver.SendKeys(state, "MA");
                Driver.SendKeys(zipCode, "02116");

                Driver.DirectClick(doneButton);

            }
            catch (Exception ex)
            {
                throw new Exception($"Failed  to update requester Message : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }

        }

        public void AssignRequestandAddFaxNo(string faxno, string sSearch = "Test Attorney's")
        {
            try
            {

                Driver.ScrollIntoViewAndClick(searchIdElement);
                Driver.SendKeys(searchIdElement, sSearch);
                Driver.Click(searchButton);
                Driver.FindElementBy(selectionCheckbox);
                Driver.ScrollIntoViewAndClick(selectionCheckbox);
                Driver.Click(copyallButton);
                int refId = random.Next(10, 1000);
                Driver.SendKeys(fax, faxno);
                Driver.ScrollIntoViewAndClick(refIdForTestAttroney);
                Driver.SendKeys(refIdForTestAttroney, refId.ToString());
                SelectElement oSelect = new SelectElement(Driver.FindElementBy(purposeOfUse));
                oSelect.SelectByText(puposeOfElementSelection);
                Driver.DirectClick(saveButon);
                Driver.Wait(TimeSpan.FromSeconds(2));
                //Driver.Click(shipToBtn);
                //Driver.Click(saveButon);
                Driver.Wait(TimeSpan.FromSeconds(5));
                Driver.DirectClick(doneButton);



            }
            catch (Exception ex)
            {
                throw new Exception($"Failed  to assign Test Attorney Message : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }
        public ROIAdminRequestStatusPage assignTestAttorneys(string sSearch = "Test Attorney")
        {
            try
            {
                Driver.ScrollIntoViewAndClick(searchIdElement);
                Driver.SendKeys(searchIdElement, sSearch);
                Driver.Click(searchButton);
                Driver.FindElementBy(selectionCheckbox);
                Driver.ScrollIntoViewAndClick(selectionCheckbox);
                Driver.Click(copyallButton);
                int refId = random.Next(10, 1000);
                Driver.Wait(TimeSpan.FromSeconds(2));
                Driver.ScrollIntoViewAndClick(refIdForTestAttroney);
                Driver.SendKeys(refIdForTestAttroney, refId.ToString());
                SelectElement oSelect = new SelectElement(Driver.FindElementBy(purposeOfUse));
                oSelect.SelectByText(puposeOfElementSelection);
                Driver.DirectClick(saveButon);
                Driver.Wait(TimeSpan.FromSeconds(2));
                Driver.DirectClick(doneButton);
                Driver.Wait(TimeSpan.FromSeconds(2));

            }
            catch (Exception ex)
            {
                throw new Exception($"Failed  to assign Test Attorney with Message : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }

            return new ROIAdminRequestStatusPage(Driver, logger, Context);

        }


        public bool IsAttorneyShowing(string text)
        {
            By attorneyPath = By.XPath($"//*[@id='mrocontent_tdRqrContact' and contains(text(), '{text}')]");
            return Driver.isElementDisplayed(attorneyPath);
        }




    }

}
