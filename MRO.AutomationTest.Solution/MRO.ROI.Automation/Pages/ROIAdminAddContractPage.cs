using AventStack.ExtentReports;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MRO.ROI.Automation.Selenium;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using System;
using static MRO.ROI.Automation.Utility.IniFile;

namespace MRO.ROI.Automation.Pages
{
    public class ROIAdminAddContractPage
    {
        public RemoteWebDriver Driver;
        public ExtentTest logger;
        public TestContext Context;
        public ROIAdminAddContractPage(RemoteWebDriver driver, ExtentTest _loger, TestContext _context)
        {
            Driver = driver;
            logger = _loger;
            Context = _context;
        }

        public Random random = new Random();
        public By toAddContractButton = By.XPath("//input[@id='mrocontent_cmdAdd']");
        public By contractName = By.XPath("//*[@id='mrocontent_txtName']");
        public const string txtName = "TestCigniti";
        public By effectiveDateCalenderBtn = By.XPath("(//td[contains(text(),'Effective Date:')]/..//img)[1]");
        public By effectiveDate = By.XPath("//*[@id='tblPopUpCalendar']/tbody/tr[3]/td[7]/a");
        public By endDateCalenderBtn = By.XPath("//*[@id='mrocontent_tblContractProperties']/tbody/tr[3]/td[4]/a/img");
        public By endDate = By.XPath("//*[@id='tblPopUpCalendar']/tbody/tr[7]/td[6]/a");
        public By contractSaveButton = By.XPath("//*[@id='mrocontent_cmdSave1']");
        public By cmdReturnButton = By.XPath("//*[@id='mrocontent_cmdReturn1']");
        public By monthBackButton = By.CssSelector("table#tblPopUpCalendar tr>td>a");
        public By dates = By.CssSelector("table#tblPopUpCalendar tr>td>a");
        public static string previousContractValue = string.Empty;
        public By sharedRadioBut = By.Id("mrocontent_rbShared");
        public By requestReleaseFeeTxtBox = By.Id("mrocontent_txtRequestsReleasedFee");
        public By releaseRequestFeeRow = By.Id("mrocontent_trRequestsReleasedFee");
        public By saveChangesButton = By.Id("mrocontent_cmdSave");
        public By headerElement = By.XPath("//*[contains(text(), 'Edit Contract')]");
        public By staffedRadioButton = By.XPath("//input[@id='mrocontent_rbStaffed']");
        public By boeTransactionFeeTxt = By.XPath("//input[@id='mrocontent_txtBOETransactionFee']");
        public By serviceFeeTxtbox = By.XPath("//input[@id='mrocontent_txtServiceFee']");
        public By audiTrendsFeeTxtbox = By.XPath("//input[@id='mrocontent_txtMonthlyRACFee']");
        public By messagingFee = By.XPath("//input[@id='mrocontent_txtDSMFee']");
        public By meaningfulFeeTxtbox = By.XPath("//input[@id='mrocontent_txtMeaningfulUseFee']");
        public By elinkFeeTxtbox = By.XPath("//input[@id='mrocontent_txtMonthlyeLinkFee']");
        public By patientFeePortalTxtbox = By.XPath("//input[@id='mrocontent_txtPatientPortalFee']");
        public By interfaceFeeTxtbox = By.XPath("//input[@id='mrocontent_txtMonthlyRevenueRecoveryFee']");
        public By onlineServiceFeeElement = By.XPath("//td[contains(text(),'ROI Online Service Fee:')]");


        /// <summary>
        /// To Add New Contracts
        /// </summary>
        public string CreateContract()
        {
            try
            {

                int ID = random.Next(10, 2000);
                Driver.SendKeys(contractName, txtName + ID.ToString());
                Driver.Click(effectiveDateCalenderBtn);
                Driver.SelectValueFromDD(dates, "1");
                Driver.Click(endDateCalenderBtn);
                Driver.SelectValueFromDD(dates, "25");
                Driver.Click(saveChangesButton);
                string ConName = Driver.FindElementBy(contractName).GetAttribute("value");
                return ConName;
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed  to create  new contracts with  detail exception as Message : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }

        }
        public void EditSelectedContract()
        {
            try
            {
                Driver.Click(sharedRadioBut);
                bool isDisplayed = Driver.FindElementBy(releaseRequestFeeRow).Displayed;
                if (isDisplayed == true)
                {
                    logger.Log(Status.Info, "Verified Release request fee is visible");
                }
                else
                {
                    logger.Log(Status.Info, "Release request fee is not visible");

                }
                Driver.ScrollIntoViewAndClick(requestReleaseFeeTxtBox);
                Driver.ClearText(requestReleaseFeeTxtBox);
                string releaseFeeVal = IniHelper.ReadConfig("ROIRequestReleasedFeeContractTermTest", "RequestReleasedFee");
                Driver.SendKeys(requestReleaseFeeTxtBox, releaseFeeVal);
                Driver.ScrollIntoViewAndClick(saveChangesButton);
            }
            catch (Exception ex)
            {

                throw new Exception($"Failed  to edit selected contract with  detail exception as Message : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }
        public string VerifyEditContractPageHeader()
        {
            try
            {
                string headingValue = Driver.GetText(headerElement);
                return headingValue;

            }
            catch (Exception ex)
            {

                throw new Exception($"Failed  to verify page header with  detail exception as Message : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }
        public void EditROIFacilityContract()
        {
            try
            {
                Driver.Click(staffedRadioButton);
                Driver.ScrollIntoViewAndClick(boeTransactionFeeTxt);
                Driver.ClearText(boeTransactionFeeTxt);
                string boeTransactionValue = IniHelper.ReadConfig("MultipleContractTermUserStoriesTest", "BOETransactionFee");
                Driver.SendKeys(boeTransactionFeeTxt, boeTransactionValue);
                Driver.ScrollIntoViewAndClick(saveChangesButton);
                Driver.SleepTheThread(5);
            }
            catch (Exception ex)
            {

                throw new Exception($"Failed  to edit roi facility contract with  detail exception as Message : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }

        public bool VerifyMonthlyFeeValues()
        {
            bool isDisplayed = false;
            try
            {
                string onlineServiceFee = "//td[contains(text(),'ROI Online Service Fee:')]";
                string audiTrends = "//td[contains(text(),'AudiTrends Fee:')]";
                string messagingFee = "//td[contains(text(),'Direct Secure Messaging Fee:')]";
                string meaningfulFee = "//td[contains(text(),'Meaningful Use Fee:')]";
                string elinkFee = "//td[contains(text(),'MRO eLink Fee:')]";
                string patientPortalFee = "//td[contains(text(),'Patient Portal Fee:')]";
                string interfaceFee = "//td[contains(text(),'Revenue Recovery Interface Fee:')]";

                WebElementHelper helper = new WebElementHelper(Driver, logger, Context);
                bool isonlineServiceFeeDisplayed = helper.IsElementPresent(onlineServiceFee);
                bool isaudiTrendsDisplayed = helper.IsElementPresent(audiTrends);
                bool ismessagingFeeDisplayed = helper.IsElementPresent(messagingFee);
                bool ismeaningfulFeeDisplayed = helper.IsElementPresent(meaningfulFee);
                bool iselinkFeeDisplayed = helper.IsElementPresent(elinkFee);
                bool ispatientPortalFeeDisplayed = helper.IsElementPresent(patientPortalFee);
                bool isinterfaceFeeDisplayed = helper.IsElementPresent(interfaceFee);

                if ((isonlineServiceFeeDisplayed == true) && (isaudiTrendsDisplayed == true) && (ismessagingFeeDisplayed == true) && (ismeaningfulFeeDisplayed == true) && (iselinkFeeDisplayed == true) && (ispatientPortalFeeDisplayed == true) && (isinterfaceFeeDisplayed == true))
                {

                    isDisplayed = true;
                    Driver.ScrollToElement(onlineServiceFeeElement);
                }

            }
            catch (Exception ex)
            {

                throw new Exception($"Failed  to verify monthly fee with  detail exception as Message : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
            return isDisplayed;
        }

        public void AddingMonthlyFeeTermAmount()
        {
            try
            {

                Driver.ClearText(serviceFeeTxtbox);
                Driver.SendKeys(serviceFeeTxtbox, "250.00");

                Driver.ClearText(audiTrendsFeeTxtbox);
                Driver.SendKeys(audiTrendsFeeTxtbox, "100.00");

                Driver.ClearText(messagingFee);
                Driver.SendKeys(messagingFee, "10.00");

                Driver.ClearText(meaningfulFeeTxtbox);
                Driver.SendKeys(meaningfulFeeTxtbox, "1.00");

                Driver.ClearText(elinkFeeTxtbox);
                Driver.SendKeys(elinkFeeTxtbox, "100.30");

                Driver.ClearText(patientFeePortalTxtbox);
                Driver.SendKeys(patientFeePortalTxtbox, "3.00");

                Driver.ClearText(interfaceFeeTxtbox);
                Driver.SendKeys(interfaceFeeTxtbox, "100.00");

                Driver.Click(saveChangesButton);


            }
            catch (Exception ex)
            {

                throw new Exception($"Failed  to add monthly fee amount values with  detail exception as Message : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }

    }
}
