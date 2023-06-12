using AventStack.ExtentReports;
using MRO.ROI.Automation.Common.Navigation;
using MRO.ROI.Automation.Selenium;
using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace MRO.ROI.Automation.TestPages
{
    public class ROIAdminContractListPage
    {
        public Random random = new Random();
        public By drpFacility = By.XPath("//select[@id='mrocontent_lstFac']");
        public const string facilityDDValue = "MRO eXpress TEST";
        public const string browserButton = "mrocontent_btnBrowse";
        public By verifyAllContactButton = By.XPath("//input[@id='mrocontent_cmdVerifyAll']");
        public By headerElement = By.XPath("//td[@id='MasterHeaderText']");
        public By returnButton = By.XPath("//input[@id='mrocontent_btnReturn']");
        public By cmdADD = By.XPath("//input[@id='mrocontent_cmdAdd']");
        public By txtContentName = By.XPath("//*[@id='mrocontent_txtName']");
        public const string txtName = "TestCigniti";
        public By tblContractProperties1 = By.XPath("//*[@id='mrocontent_tblContractProperties']/tbody/tr[3]/td[2]/a/img");
        public By effectiveDate = By.XPath("//*[@id='tblPopUpCalendar']/tbody/tr[3]/td[5]/a");
        public By tblContractProperties2 = By.XPath("//*[@id='mrocontent_tblContractProperties']/tbody/tr[3]/td[4]/a/img");
        public By endDate = By.XPath("//*[@id='tblPopUpCalendar']/tbody/tr[7]/td[6]/a");
        public By cmdSaveButton = By.XPath("//*[@id='mrocontent_cmdSave1']");
        public By cmdReturnButton = By.XPath("//*[@id='mrocontent_cmdReturn1']");
        public By cmdVerifyButton = By.XPath("//input[@id='mrocontent_cmdVerify']");
        public By drpLastSixMonths = By.XPath("//*[@id='mrocontent_lstLastSixMonths']");
        public By dgReport = By.XPath("//*[@id='mrocontent_dgReport_tblReport_imgSortSwitch']");
        public By contractName = By.XPath("//*[@id='mrocontent_dgReport']/tbody/tr[2]/td[6]");
        public By dgReportInfoMsg = By.XPath("//*[@id='mrocontent_dgReport']/tbody/tr[2]/td[12]");
        public By prevMonthContract = By.XPath("//*[@id=_tblReport']/tbody/tr/td");

        public void ContractList()
        {
            try
            {
                MenuSelector.SelectRoiAdmin("Facilities", "Contract List");
                logger.Log(Status.Info, "Contract list Page loads Successfully");
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void FacilityDropdownInContractListPage()
        {
            try
            {
                Driver.SelectValueFromDD(drpFacility, facilityDDValue);
                var contractButton = Driver.FindElementBy(verifyAllContactButton);
                contractButton.Click();
                var headerValue = Driver.FindElementBy(headerElement);
                Driver.FindElementBy(returnButton).Click();
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed with Message : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }

        }

        public void ToAddContracts()
        {
            try
            {
                Driver.FindElementBy(cmdADD, 5).Click();
                int ID = random.Next(10, 2000);               
                Driver.FindElementBy(txtContentName).SendKeys(txtName + ID.ToString());                
                Driver.FindElementBy(tblContractProperties1).Click();
                Driver.FindElementBy(effectiveDate, 5).Click();
                Driver.FindElementBy(tblContractProperties2).Click();
                Driver.FindElementBy(endDate,5).Click();               
                Driver.FindElementBy(cmdSaveButton, 5).Click();
                logger.Log(Status.Pass, " Contract Created Successfully");
                string contractValue = Driver.FindElementBy(txtContentName).GetAttribute("value");
                logger.Log(Status.Info, " Second Contract Created Successfully and created contract is :" +contractValue);
                Driver.FindElementBy(cmdReturnButton).Click();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public void FacilityContractSelection()
        {
            try
            {                
                Driver.FindElementBy(cmdVerifyButton).Click();
                logger.Log(Status.Pass, "Active facility contracts Page load  successfully");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void CurrentMonthContractList()
        {
            try
            {               
                SelectElement oSelect1 = new SelectElement(Driver.FindElementBy(drpLastSixMonths));
                oSelect1.SelectByIndex(1);
                logger.Log(Status.Pass, "Contract list is displayed for the selected month");
                Driver.FindElementBy(dgReport).Click();
                string contractname = Driver.FindElementBy(contractName).Text;
                logger.Log(Status.Info, "Contracts are Created Successfully and created contract is:" + contractname);
                logger.Log(Status.Info, "Created contracts are added Successfully and Contracts are duplicate");
                string infomsg = Driver.FindElementBy(dgReportInfoMsg).Text;
                logger.Log(Status.Info, "Error Info for Contract list is : " + infomsg);
                Driver.FindElementBy(dgReport, 2).Click();
                SelectElement oSelect2 = new SelectElement(Driver.FindElementBy(drpLastSixMonths));
                oSelect2.SelectByIndex(2);
                string previousmonthContracts = Driver.FindElementBy(prevMonthContract).Text;
                logger.Log(Status.Info, "No Contracts are available for selected Previous  month");
            }

            catch (Exception ex)
            {
                throw new Exception($"Failed with Message : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }

    }
}
