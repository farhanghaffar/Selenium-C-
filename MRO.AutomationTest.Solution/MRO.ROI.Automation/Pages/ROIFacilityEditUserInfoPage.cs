using AventStack.ExtentReports;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MRO.ROI.Automation.Selenium;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using System;

namespace MRO.ROI.Automation.Pages
{
    public class ROIFacilityEditUserInfoPage
    {
        public RemoteWebDriver Driver;
        public ExtentTest logger;
        public TestContext Context;
        public ROIFacilityEditUserInfoPage(RemoteWebDriver driver, ExtentTest _loger, TestContext _context)
        {
            Driver = driver;
            logger = _loger;
            Context = _context;
        }

        public By administerUsersCheckbox = By.XPath("//input[@id='mrocontent_bUserAdmin']");
        public By saveChangesButton = By.XPath("//input[@id='mrocontent_cmdUpdate']");
        public By updateLableElement = By.XPath("//span[@id='mrocontent_lblUserUpdated']");
        public By returnToListElement = By.XPath("//input[@id='mrocontent_cmdCancel']");
        public By mroEmployeeChkBox = By.Id("mrocontent_bMROEmployee");
        public By logoutElement = By.XPath("//img[@title='Log Out']");
        public By epicEmployeeID = By.Id("mrocontent_txtEPICUserID");
        public By downPaymentCheckbox = By.XPath("//input[@ id='mrocontent_cbDeadbeat']");
        public By updateInfoBtn = By.XPath("//input[@ id='mrocontent_cmdUpdate']");
        public By hasUserReportingCheckbox = By.XPath("//input[@id='mrocontent_cbUserReporting']");
        /// <summary>
        /// To Uncheck users admin check box
        /// </summary>
        public ROIFacilityEditUserInfoPage UncheckUserAdminCheckBox()
        {
            try
            {
                bool result = Driver.FindElementBy(administerUsersCheckbox).Selected;
                if (result == true)
                {
                    IWebElement adminUsersChk = Driver.FindElementBy(administerUsersCheckbox);
                    adminUsersChk.Click();
                }
                IWebElement saveChangesButt = Driver.FindElementBy(saveChangesButton);
                saveChangesButt.Click();
                string statusMessage = Driver.FindElementBy(updateLableElement).Text;
                Assert.AreEqual("User Updated!", statusMessage,"Failed to validate the user is updated");

            }
            catch (Exception ex)
            {

                throw new Exception($"Failed  to uncheck admin users checkbox in EditUserInfo with details as Message : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
            return new ROIFacilityEditUserInfoPage(Driver,logger,Context);
        }


        /// <summary>
        /// To validate Users adminster Check box
        /// </summary>

        public bool ToValidateUsersAdminCheckBox()
        {
            bool isSelected = true;
            try
            {
                if (Driver.FindElementBy(administerUsersCheckbox).Selected == false) 
                {
                    isSelected = false;
                }

            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to validate user administer check box with Message : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
            return isSelected;
        }

        public void CheckUserAdminCheckBox()
        {
            try
            {
                bool isAdminUsersChecked = Driver.FindElementBy(administerUsersCheckbox).Selected;
                if (isAdminUsersChecked == false)
                {
                    IWebElement adminUsersChk = Driver.FindElementBy(administerUsersCheckbox);
                    adminUsersChk.Click();
                }
                IWebElement saveChangesButt = Driver.FindElementBy(saveChangesButton);
                saveChangesButt.Click();
                string statusMessage = Driver.FindElementBy(updateLableElement).Text;
                Assert.AreEqual("User Updated!", statusMessage, "Failed to validate the user is updated");
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed  to uncheck admin users checkbox in EditUserInfo with details as Message : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }

        }
        public string CheckMROEmployeeChkBox()
        {
            try
            {
                if (Driver.FindElementBy(mroEmployeeChkBox).Selected == false)
                {
                    Driver.Click(mroEmployeeChkBox);                  
                    
                }
                Driver.Click(saveChangesButton);
                string statusMessage = Driver.FindElementBy(updateLableElement).Text;
                Driver.Click(logoutElement);
                return statusMessage;
            }
            catch (Exception ex)
            {

                throw new Exception($"Failed  save changes in edituser info page with details as Message : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }
        public string SetAndGetEpicEmployeeID()
        {
            try
            {
                string sEpicEmployeeID = string.Empty;
                if (Driver.FindElementBy(mroEmployeeChkBox).Selected == false)
                {
                    Driver.Click(mroEmployeeChkBox);

                }
                Random rand = new Random();
                int iEpicEmployeeID = rand.Next(0, 1000);
                sEpicEmployeeID = iEpicEmployeeID.ToString();
                Driver.ClearText(epicEmployeeID);
                Driver.SendKeys(epicEmployeeID, sEpicEmployeeID);
                return sEpicEmployeeID;
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to set epic employee id in edituser info page with details as Message : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }

        public string SaveChangesAndGetStatus()
        {
            Driver.Click(saveChangesButton);
            Driver.ScrollToElement(updateLableElement);
            string statusMessage = Driver.FindElementBy(updateLableElement).Text;
            return statusMessage;
        }

        public bool VerifyCanAdminUserCheckbox()
        {
            try
            {
              
                string canAdminUserChkbox = "//input[@id='mrocontent_bUserAdmin']";
                WebElementHelper helper = new WebElementHelper(Driver, logger, Context);
                bool isDisplayed = helper.IsElementPresent(canAdminUserChkbox);
                return isDisplayed;
            }
            catch (Exception ex)
            {

                throw new Exception($"Failed  to verify admin users check box with details as  Message : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }

        public bool CheckSaveChangesDisabled()
        {
            bool isDisabled = false;
            try
            {
                bool result = Driver.FindElement(saveChangesButton).Displayed;
                IWebElement saveChangesElement = Driver.FindElementBy(saveChangesButton);

                if (saveChangesElement != null)
                {
                    string value = saveChangesElement.GetAttribute("disabled");

                    if (value == "true")
                    {
                        isDisabled = true;
                    }

                }
                Driver.ScrollToElement(saveChangesButton);
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to check save changes, Exception detail: {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
            return isDisabled;
        }

        public string UnCheckMROEmployeeChkBox()
        {
            try
            {
                if (Driver.FindElementBy(mroEmployeeChkBox).Selected == true)
                {
                    Driver.Click(mroEmployeeChkBox);

                }
                Driver.Click(saveChangesButton);
                string statusMessage = Driver.FindElementBy(updateLableElement).Text;
                return statusMessage;
            }
            catch (Exception ex)
            {

                throw new Exception($"Failed  save changes in edituser info page with details as Message : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }

        public void VerifyMROEmployeeChkBoxisCheckedOrNot()
        {
            try
            {
                if (Driver.FindElementBy(mroEmployeeChkBox).Selected == false)
                {
                    Driver.Click(mroEmployeeChkBox);

                }
                Driver.Click(saveChangesButton);

            }
            catch (Exception ex)
            {

                throw new Exception($"Failed  save changes in edituser info page with details as Message : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }

        public bool VerifyDisableFacilityUserWebLoginIsVisibbleOrNot()
        {
            try
            {
                string weblogin = "//select[@id='mrocontent_lstDisableFacilityUserWebLogin']";
                WebElementHelper helper = new WebElementHelper(Driver, logger, Context);
                bool isWebloginPresent = helper.IsElementPresent(weblogin);
                return isWebloginPresent;


            }
            catch (Exception ex)
            {

                throw;
            }

        }
        public bool ToValidateDownPaymentCheckbox()
        {
            Driver.ScrollToEndOfThePage();
            Driver.ScrollToElement(downPaymentCheckbox);
            Driver.HighlightingWebElement(downPaymentCheckbox);
            bool isSelected = false;
            try
            {
                if (Driver.FindElementBy(downPaymentCheckbox).Selected == true)
                {
                    Driver.Click(downPaymentCheckbox);

                }
                Driver.Click(saveChangesButton);
                Driver.Click(updateInfoBtn);

            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to verify down payment checkbox: {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");

            }
            Driver.ScrollToEndOfThePage();
            return isSelected;
        }

       
public void SearchByRequestId(string RequestId)
        {
            try
            {
                Driver.Click(lookupRequestIdElement);
                Driver.SwitchTo().Alert().SendKeys(RequestId);
                logger.Log(Status.Info, $"Entered request id ({RequestId})");
                Driver.SwitchTo().Alert().Accept();
                Driver.Wait(TimeSpan.FromSeconds(2));
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed with Message not able to click '?' : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }
        public By lookupRequestIdElement = By.XPath("//a[@ id='mroheader_ctl02_ctl03_imgQuery']");


        public void CheckMROEmployeeChkBox(bool isCheck)
        {
            if (isCheck)
            {
                if (Driver.FindElementBy(mroEmployeeChkBox).Selected == false)
                {
                    Driver.Click(mroEmployeeChkBox);

                }
            }
            else
            {
                if (Driver.FindElementBy(mroEmployeeChkBox).Selected == true)
                {
                    Driver.Click(mroEmployeeChkBox);
                }

            }

        }
        public ROIFacilityEditUserInfoPage CheckHasUserReportingCheckbox(bool isCheck)
        {
            try
            {
                bool result = Driver.FindElementBy(hasUserReportingCheckbox).Selected;
                if (isCheck)
                {
                    if (result == false)
                    {
                        IWebElement adminUsersChk = Driver.FindElementBy(hasUserReportingCheckbox);
                        adminUsersChk.Click();
                    }
                }
                else
                {
                    if (result == true)
                    {
                        IWebElement adminUsersChk = Driver.FindElementBy(hasUserReportingCheckbox);
                        adminUsersChk.Click();
                    }

                }
            }
            catch (Exception ex)
            {

                throw new Exception($"Failed  to uncheck Has User Reporting: checkbox in EditUserInfo with details as Message : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
            return new ROIFacilityEditUserInfoPage(Driver, logger, Context);
        }

    }

    
}
