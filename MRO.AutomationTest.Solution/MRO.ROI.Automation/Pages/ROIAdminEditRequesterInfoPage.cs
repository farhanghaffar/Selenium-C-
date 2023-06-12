using AventStack.ExtentReports;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MRO.ROI.Automation.Selenium;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.UI;
using System;

namespace MRO.ROI.Automation.Pages
{
    public class ROIAdminEditRequesterInfoPage
    {
        public RemoteWebDriver Driver;
        public ExtentTest logger;
        public TestContext Context;
        public ROIAdminEditRequesterInfoPage(RemoteWebDriver driver, ExtentTest _loger, TestContext _context)
        {
            Driver = driver;
            logger = _loger;
            Context = _context;
        }

        public By bOERequesterChk = By.XPath("//input[@id='mrocontent_cbRequireCBOPAN']");
        public By editRequesterInfoPage = By.XPath("//td[@id='MasterHeaderText']");
        public By updateInfoBtn = By.XPath("//input[@id='mrocontent_cmdUpdate']");
        public By editRequesterInfoHeader = By.XPath("//td[@id='MasterHeaderText']");
        public By chkRequestDocumentsRequired = By.XPath("//input[@id='mrocontent_cbRequireRequestDocsToLog']");
        public By boESubscriberId = By.XPath("//input[@id='mrocontent_cbRequireCBOSubscriberID']");
        public By claimId = By.XPath("//input[@id='mrocontent_cbRequireCBOClaimID']");
        public By referenceId = By.XPath("//input[@id='mrocontent_cbRequireCBORefID']");
        public By enableMRODeliveryChkbox = By.XPath("//input[@id='mrocontent_cbPortalMRODeliveryEnabled']");
        public By enableBillingOfcDeliveryChkbox = By.XPath("//input[@id='mrocontent_cbCBOWF']");
        public By faxNo = By.XPath("//input[@id='mrocontent_txtFax']");
        public By invoiceDeliveryDrp = By.XPath("//select[@id='mrocontent_ddlInvoiceDeliveryType']");
        public By defaultShipmentType = By.XPath("//select[@id='mrocontent_lstShipmentType']");
        public By infoMsg = By.XPath("//span[@id='mrocontent_lblUpdated']");
        public By emailCorrespondenceChkbox = By.XPath("//input[@id='mrocontent_cbCorresEmail']");
        public By faxCorrespondenceChkBox = By.XPath("//input[@id='mrocontent_cbCorresFax']");
        public By emailTextBox = By.XPath("//input[@id='mrocontent_txtEmail']");
        public By returnToRequest = By.XPath("//input[@id='mrocontent_cmdCancel']");

        public By firstName = By.XPath("//input[@id='mrocontent_txtFirstName']");
        public By lastName = By.XPath("//input[@id='mrocontent_txtLastName']");
        public By phoneNum = By.XPath("//input[@id='mrocontent_txtPhone']");
        public By emailId = By.XPath("//input[@id='mrocontent_txtEmail']");
        public By address = By.XPath("//input[@id='mrocontent_txtAddress1']");
        public By cityElement = By.XPath("//input[@id='mrocontent_txtCity']");
        public By state = By.XPath("//input[@id='mrocontent_txtState']");
        public By zipcode = By.XPath("//input[@id='mrocontent_txtZipCode']");
        public By objMailCheckBox = By.XPath("//input[@id='mrocontent_cbMailOnly']");

        /// <summary>
        /// Verify edit requester info page
        /// </summary>     
        public bool VerifyEditRequesterInfoPage()
        {
            try
            {
                bool isDisplayed = false;
                IWebElement editRequesterInfoPageTxt = Driver.FindElementBy(editRequesterInfoPage);
                if (editRequesterInfoPageTxt.Displayed == true)
                {
                    isDisplayed = true;                    
                }
                else {
                    isDisplayed = false;
                }
                return isDisplayed;
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to verify edit requester info page : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");

            }
        }
        /// <summary>
        /// Verify and update internal portal settings
        /// </summary>
        public bool VerifyAndUpdateInternalPortalSettingsBOEChecked()
        {
            bool _isDispalyed = false;
            try
            {
                Driver.ScrollToEndOfThePage();
                bool boeRequesterChkStatus = Driver.FindElementBy(bOERequesterChk).Selected;
                if (boeRequesterChkStatus == false)
                {
                    IWebElement bOERequesterCheckbox = Driver.FindElementBy(bOERequesterChk);
                    bOERequesterCheckbox.Click();
                }

                Driver.Click(updateInfoBtn);
                IWebElement boeRequesterChkStatusTxt = Driver.FindElementBy(editRequesterInfoHeader);
                if (boeRequesterChkStatusTxt.Displayed == true)
                {
                    _isDispalyed = true;
                }
                return _isDispalyed;
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to verify and update internal portal settings page  : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }

        }
        public bool VerifyAndUpdateInternalPortalSettingsBOEUnchecked()
        {
            bool _isDispalyed = false;
            try
            {
                Driver.ScrollToEndOfThePage();
                bool boeRequesterChkStatus = Driver.FindElementBy(bOERequesterChk).Selected;
                if (boeRequesterChkStatus == true)
                {
                    IWebElement bOERequesterCheckbox = Driver.FindElementBy(bOERequesterChk);
                    bOERequesterCheckbox.Click();
                }

                Driver.Click(updateInfoBtn);
                IWebElement boeRequesterChkStatusTxt = Driver.FindElementBy(editRequesterInfoHeader);
                if (boeRequesterChkStatusTxt.Displayed == true)
                {
                    _isDispalyed = true;
                }
                return _isDispalyed;
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to verify and update internal portal settings page  : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }

        }
        /// <summary>
        /// Check Request Documents Required
        /// </summary>
        public bool CheckeRequestDocumentsRequired()
        {
            bool _isDispalyed = false;
            try
            {
                Driver.ScrollToEndOfThePage();
                bool bRequestDocumentsRequired = Driver.FindElementBy(chkRequestDocumentsRequired).Selected;
                if (bRequestDocumentsRequired == false)
                {
                    IWebElement iRequestDocumentsRequired = Driver.FindElementBy(chkRequestDocumentsRequired);
                    iRequestDocumentsRequired.Click();
                }

                Driver.Click(updateInfoBtn);
                IWebElement iRequestDocumentsRequiredTxt = Driver.FindElementBy(editRequesterInfoHeader);
                if (iRequestDocumentsRequiredTxt.Displayed == true)
                {
                    _isDispalyed = true;
                }
                return _isDispalyed;
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to check Request Documents Required: {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }

        }

        /// <summary>
        /// UnCheck Request Documents Required
        /// </summary>
        public bool UnCheckRequestDocumentsRequired()
        {
            bool _isDispalyed = false;
            try
            {
                Driver.ScrollToEndOfThePage();
                bool bRequestDocumentsRequired = Driver.FindElementBy(chkRequestDocumentsRequired).Selected;
                if (bRequestDocumentsRequired == true)
                {
                    IWebElement iRequestDocumentsRequired = Driver.FindElementBy(chkRequestDocumentsRequired);
                    iRequestDocumentsRequired.Click();
                }

                Driver.Click(updateInfoBtn);
                IWebElement iRequestDocumentsRequiredTxt = Driver.FindElementBy(editRequesterInfoHeader);
                if (iRequestDocumentsRequiredTxt.Displayed == true)
                {
                    _isDispalyed = true;
                }
                return _isDispalyed;
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to uncheck Request Documents Required: {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }

        }

        public void UpdateInternalPortalSettingsBOESubscriberIdChecked()
        {

            try
            {
                Driver.ScrollToEndOfThePage();
                bool boeRequesterChkStatus = Driver.FindElementBy(boESubscriberId).Selected;
                if (boeRequesterChkStatus == false)
                {
                    IWebElement bOERequesterCheckbox = Driver.FindElementBy(boESubscriberId);
                    bOERequesterCheckbox.Click();
                }

                Driver.Click(updateInfoBtn);
                Driver.ScrollToElement(boESubscriberId);

            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to verify and update internal portal settings page  : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }

        }


        public void UpdateInternalPortalSettingsBOESubscriberIdUnChecked()
        {

            try
            {
                Driver.ScrollToEndOfThePage();
                bool boeRequesterChkStatus = Driver.FindElementBy(boESubscriberId).Selected;
                if (boeRequesterChkStatus == true)
                {
                    IWebElement bOERequesterCheckbox = Driver.FindElementBy(boESubscriberId);
                    bOERequesterCheckbox.Click();
                }

                Driver.Click(updateInfoBtn);
                Driver.ScrollToElement(boESubscriberId);

            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to verify and update internal portal settings page  : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }

        }


        public void CheckInternalPortalSettingsBOEClaimIdAndReferenceId()
        {

            try
            {
                Driver.ScrollToEndOfThePage();
                bool boeRequesterClaimChkStatus = Driver.FindElementBy(claimId).Selected;
                if (boeRequesterClaimChkStatus == false)
                {
                    IWebElement bOERequesterCheckbox = Driver.FindElementBy(claimId);
                    bOERequesterCheckbox.Click();
                }

                bool boeRequesterReferenceChkStatus = Driver.FindElementBy(referenceId).Selected;
                if (boeRequesterReferenceChkStatus == false)
                {
                    IWebElement bOERequesterCheckbox = Driver.FindElementBy(referenceId);
                    bOERequesterCheckbox.Click();
                }

                Driver.Click(updateInfoBtn);
                Driver.ScrollToElement(claimId);

            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to verify and update internal portal settings page  : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }

        }


        public void UnCheckInternalPortalSettingsBOEClaimIdAndReferenceId()
        {

            try
            {
                Driver.ScrollToEndOfThePage();
                bool boeRequesterClaimChkStatus = Driver.FindElementBy(claimId).Selected;
                if (boeRequesterClaimChkStatus == true)
                {
                    IWebElement bOERequesterCheckbox = Driver.FindElementBy(claimId);
                    bOERequesterCheckbox.Click();
                }

                bool boeRequesterReferenceChkStatus = Driver.FindElementBy(referenceId).Selected;
                if (boeRequesterReferenceChkStatus == true)
                {
                    IWebElement bOERequesterCheckbox = Driver.FindElementBy(referenceId);
                    bOERequesterCheckbox.Click();
                }

                Driver.Click(updateInfoBtn);
                Driver.ScrollToElement(claimId);

            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to verify and update internal portal settings page  : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }

        }

        public void VerifyAndUpdateInternalPortalSettings()
        {
            try
            {
                if (Driver.FindElementBy(enableMRODeliveryChkbox).Selected == true)
                {
                    Driver.Click(enableMRODeliveryChkbox);
                }
                if (Driver.FindElementBy(enableBillingOfcDeliveryChkbox).Selected == false)
                {
                    Driver.Click(enableBillingOfcDeliveryChkbox);
                }

                Driver.Click(updateInfoBtn);
            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to verify and update internal portal settings page  : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }

        public void ClearFaxNumber()
        {
            try
            {
                Driver.ClearText(faxNo);
            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to verify and update internal portal settings page  : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }

        public void SelectInvoiceDelivery(string type)
        {
            try
            {
                Driver.SendKeys(invoiceDeliveryDrp, type);
            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to select invoice type with details as  : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }

        public void SelectDefaultShipmentType(string type)
        {
            try
            {
                Driver.SendKeys(defaultShipmentType, type);
                Driver.Click(updateInfoBtn);
            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to select default shipment type with details as  : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }

        public string GetRequesterInfoMsg()
        {
            try
            {
                string resultMsg=Driver.GetText(infoMsg);
                return resultMsg;
            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to return info with details as  : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }
        public void UpdateCorrespondenceSettings()
        {
            try
            {
                if(Driver.FindElementBy(emailCorrespondenceChkbox).Selected==true)
                {
                    Driver.Click(emailCorrespondenceChkbox);
                }
                if(Driver.FindElementBy(faxCorrespondenceChkBox).Selected==true)
                {
                    Driver.Click(faxCorrespondenceChkBox);
                }
                Driver.Click(updateInfoBtn);
            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to update correspondence settings with details as  : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }

        public void ClearEmailField()
        {
            try
            {
                Driver.ClearText(emailTextBox);
                Driver.Click(updateInfoBtn);
            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to clear email text box with details as : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }

        public void ClickOnReturnToRequest()
        {
            try
            {
               
                Driver.Click(returnToRequest);
            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to clear email text box with details as : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }


        public string GetFirstName()
        {
            try
            {
                string fname = Driver.GetText(firstName);
                return fname;

            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to get first name with details as : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }

        public string GetLastName()
        {
            try
            {
                string lname = Driver.GetText(lastName);
                return lname;

            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to get last name with details as : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }

        public string GetPhoneNumber()
        {
            try
            {
                string phno = Driver.GetText(phoneNum);
                return phno;

            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to get phone number with details as : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }

        public string GetEmailValue()
        {
            try
            {
                string email = Driver.GetText(emailId);
                return email;

            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to get email  with details as : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }

        public void UpdateRequesterData(string fname, string lname, string phno, string email)
        {
            try
            {
                Driver.ClearText(firstName);
                Driver.SendKeys(firstName, fname);
                Driver.ClearText(lastName);
                Driver.SendKeys(lastName, lname);
                Driver.ClearText(phoneNum);
                Driver.SendKeys(phoneNum, phno);
                Driver.ClearText(emailId);
                Driver.SendKeys(emailId, email);
                Driver.Click(updateInfoBtn);
            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to update the requester data with details as : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }

        public void UpdateRequesterAddress(string add, string stateVal, string city, string faxVal, string zip)
        {
            try
            {
                Driver.ClearText(address);
                Driver.SendKeys(address, add);

                Driver.ClearText(state);
                Driver.SendKeys(state, stateVal);

                Driver.ClearText(cityElement);
                Driver.SendKeys(cityElement, city);


                Driver.ClearText(faxNo);
                Driver.SendKeys(faxNo, faxVal);

                Driver.ClearText(zipcode);
                Driver.SendKeys(zipcode, zip);
                Driver.Click(updateInfoBtn);
            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to update the requester  address data with details as : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }

        public bool CheckMailIsDisabled()
        {
            bool isDisabled = false;
            try
            {
                IWebElement mailCheckBox = Driver.FindElementBy(objMailCheckBox);

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

        public void ClickOnUpdateInfo()
        {
            try
            {
                Driver.Click(updateInfoBtn);
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to update Info button as : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }

        public void UpdateCorrespondenceSettingsForFaxAndEmail()
        {
            try
            {
                if (Driver.FindElementBy(emailCorrespondenceChkbox).Selected == true)
                {
                    Driver.Click(emailCorrespondenceChkbox);
                }
                if (Driver.FindElementBy(faxCorrespondenceChkBox).Selected == false)
                {
                    Driver.Click(faxCorrespondenceChkBox);
                }
                Driver.Click(updateInfoBtn);
            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to update correspondence settings with details as  : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }


        public void UpdateCorrespondenceSettingsForEmail()
        {
            try
            {
                if (Driver.FindElementBy(emailCorrespondenceChkbox).Selected == false)
                {
                    Driver.Click(emailCorrespondenceChkbox);
                }

                Driver.Click(updateInfoBtn);
            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to update correspondence settings with details as  : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }
    }
}
