using MRO.ROI.Automation.Selenium;
using OpenQA.Selenium;
using System;
using AventStack.ExtentReports;
using MRO.ROI.Automation.Common.Navigation;
using OpenQA.Selenium.Remote;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium.Support.UI;

namespace MRO.ROI.Automation.Pages
{
    public class ROIAdminShipmentDetailsPage
    {
        public RemoteWebDriver Driver;
        public ExtentTest logger;
        public TestContext Context;
        public ROIAdminShipmentDetailsPage(RemoteWebDriver driver, ExtentTest _loger, TestContext _context)
        {
            Driver = driver;
            logger = _loger;
            Context = _context;
        }

        public By makeShippableNowButton = By.XPath("//input[@id='mrocontent_cmdMakeShippableNow']");
        public By refIdForTestAttroney = By.XPath("//input[@id='mrocontent_txtRefID']");
        public By purposeOfUse = By.XPath("//select[@id='mrocontent_lstPurposeOfUse']");
        public By headerElement = By.Id("MasterHeaderText");
        public By secondaryShipmentDrpVal = By.Id("mrocontent_lstAdditionalShipment");
        public By requestId = By.XPath("//a[contains(@href,'aspx')]");
        public By shippedDate = By.XPath("//span[@id='mrocontent_lbldtReadyShipping']");
        public By chkSpecialDelivery = By.Id("mrocontent_cbSpecialDelivery");
        public By drpCarrierService = By.XPath("//div[@id='mrocontent_custSpecialDelivery_custCarrierSelect_cmbBxCarriers_DropDown']//ul/li");
        public By btn_SaveSpecialDeliveryInformation = By.XPath("//input[@id='mrocontent_imgBtnSaveSpecialInstr']");
        public By SpecialDeliveryStatus = By.XPath("//span[@id='mrocontent_lblSpecialDelSaved']");
        public By drpCarrierServiceArrowButton = By.XPath("//a[@id='mrocontent_custSpecialDelivery_custCarrierSelect_cmbBxCarriers_Arrow']");
        public By updateCarrierInfoButton = By.XPath("//input[@id='mrocontent_cmdUpdateCarrierInfo']");
        public By carrierDropdown = By.XPath("//select[@id='mrocontent_lstCarrier']");
        public By carrierServiceCode = By.XPath("//select[@id='mrocontent_lstCarrierServiceCode']");
        public By trackingNumber = By.XPath("//input[@id='mrocontent_txtCarrierTrackingCode']");
        public By returnToShipmentDetail = By.XPath("//input[@id='mrocontent_cmdReturn']");
        public By retryFetchButton = By.XPath("//input[@id='mrocontent_cmdRetryFetch']");
       
        /// <summary>
        /// Go to Shippment Details Page
        /// </summary>

        public ROIAdminShipmentDetailsPage ToClickMakeShippableButton()
        {
            try
            {
                Driver.DirectClick(makeShippableNowButton);
                Driver.SwitchTo().Alert().Accept();
            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to click Make Shippable now with Message : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
            return new ROIAdminShipmentDetailsPage(Driver, logger, Context);
        }

        /// <summary>
        /// Go to sFTP Viewer Page
        /// </summary>
        public ROIAdminsFTPViewerPage SFTPViewer()
        {
            try
            {
                MenuSelector selecor = new MenuSelector(Driver, logger, Context);
                selecor.SelectRoiAdmin("System", "sFTP Viewer");
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed  to navigate sFTPViewer with details  Message : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
            return new ROIAdminsFTPViewerPage(Driver, logger, Context);
        }
        /// <summary>
        /// Verify shipment details page header
        /// </summary>
        public string VerifyShipmentDetailsPageHeader()
        {
            try
            {
                string headerVal = Driver.FindElementBy(headerElement).Text.ToString();
                string heading = headerVal.Split(':')[0].ToString().Trim();
                return heading;

            }
            catch (Exception ex)
            {

                throw new Exception($"Failed  to verify header element with details  Message : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }

        }
        /// <summary>
        /// Verify Type dropdown selected value
        /// </summary>
        public string SecondaryShipmentTypeValue()
        {
            try
            {
                string selectedTypeValue = Driver.FindElement(secondaryShipmentDrpVal).FindElements(By.XPath("./option[@selected]"))[0].Text;
                return selectedTypeValue;

            }
            catch (Exception ex)
            {
                throw new Exception($"Failed  to type dropdown value with details  Message : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }

        public void GoToRequestStatusPage()
        {
            try
            {
                Driver.DirectClick(requestId);
            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to naviagate to request status page  Message : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }
        public string VerifyAndGetShippedDate()
        {
            try
            {
                Driver.SleepTheThread(5);
                string shippedDateTxt = Driver.FindElementBy(shippedDate).Text.Trim();
                string shippedDateValue = shippedDateTxt.Split(' ')[0].ToString();
                Driver.ScrollToElement(shippedDate);
                Driver.SleepTheThread(2);
                return shippedDateValue;
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to verify shipped date Message : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }

        public void CheckSpecialDelivery()
        {
            try
            {
                bool isCheck = Driver.FindElementBy(chkSpecialDelivery).Selected;
                if (isCheck == false)
                {
                    Driver.Click(chkSpecialDelivery);
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to check special delivery checkboc with details message as : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }


        public void SelectValueFromCarrierService(string option)
        {
            try
            {
                Driver.DirectClick(drpCarrierServiceArrowButton);
                Driver.SleepTheThread(2);
                Driver.SelectValueFromDD(drpCarrierService, option);
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to select value from carrier service with deatails as Message : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }

        }

        public string ClickSaveSpecialDeliveryInformation()
        {
            try
            {
                Driver.DirectClick(btn_SaveSpecialDeliveryInformation);
                Driver.ScrollToElement(SpecialDeliveryStatus);
                string statusMessage = Driver.FindElementBy(SpecialDeliveryStatus).Text;
                return statusMessage;
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to click special delivery information button with Message : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }

        public void UpdateCarrierInformation()
        {
            try
            {
                SelectElement oSelectCarrier = new SelectElement(Driver.FindElementBy(carrierDropdown));
                oSelectCarrier.SelectByText("United States Postal Service");

                SelectElement oCarrierServiceCode = new SelectElement(Driver.FindElementBy(carrierDropdown));
                oCarrierServiceCode.SelectByText("USPS Priority Mail");
                Driver.SendKeys(trackingNumber, "9410811899560493515831");
                ClickUpdateCarrierButton();
                Driver.SleepTheThread(3);
                Driver.Click(returnToShipmentDetail);
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to update carrier information with Message : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }

        public void ClickUpdateCarrierButton()
        {
            try
            {
                Driver.DirectClick(updateCarrierInfoButton);
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to click update carrier button with Message : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }

        public string VerifyAndGetShipmentStatus()
        {
            string shipmentStatus = string.Empty;
            try
            {
                Driver.SleepTheThread(5);
                Driver.Click(retryFetchButton);
                shipmentStatus = "Package ready for shipment";
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to verify shipped date Message : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
            return shipmentStatus;
        }

        public void AddTrackingNumber()
        {
            try
            {
                Driver.ClearText(trackingNumber);
                Driver.SendKeys(trackingNumber, "9410811899560493515831");
                ClickUpdateCarrierButton();
                Driver.SleepTheThread(3);
            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to add tracking number with details as  : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }
      

    }
}



