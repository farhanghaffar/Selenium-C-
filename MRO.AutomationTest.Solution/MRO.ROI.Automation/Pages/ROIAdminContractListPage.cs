using AventStack.ExtentReports;
using MRO.ROI.Automation.Selenium;
using System;
using OpenQA.Selenium;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.UI;

namespace MRO.ROI.Automation.Pages
{
    public class ROIAdminContractListPage
    {
        public RemoteWebDriver Driver;
        public ExtentTest logger;
        public TestContext Context;
        public ROIAdminContractListPage(RemoteWebDriver driver, ExtentTest _loger, TestContext _context)
        {
            Driver = driver;
            logger = _loger;
            Context = _context;
        }

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
        public By prevMonthContract = By.XPath("//*[@id='mrocontent_tblReport']/tbody/tr/td");
        public By returnToContractListButton = By.XPath("//input[@id='mrocontent_btnReturn']");
        public const string mroartfFacility = "MRO Automated Regression Test";
        public By includeTestChkBox = By.XPath("//input[@id='mrocontent_cbTest']");
        public By activeContractsChkBox = By.Id("mrocontent_cbShowActive");
        public By facilitylinkSelection = By.XPath("//table[@id='mrocontent_dgReport']//tr[2]//a[@title='edit contract']");
        public const string roiFacilityDDValue = "ROI Test Facility";


        public By toAddContractButton = By.XPath("//input[@id='mrocontent_cmdAdd']");
        public By effectiveDateCalenderBtn = By.XPath("(//td[contains(text(),'Effective Date:')]/..//img)[1]");
        public By endDateCalenderBtn = By.XPath("//*[@id='mrocontent_tblContractProperties']/tbody/tr[3]/td[4]/a/img");
        public By contractSaveButton = By.XPath("//input[@id='mrocontent_cmdSave']");
        public By monthBackButton = By.CssSelector("table#tblPopUpCalendar tr>td>a");
        public By dates = By.CssSelector("table#tblPopUpCalendar tr>td>a");

        string createdContractName = string.Empty;
        public By contractNameTxtBox = By.XPath("//*[@id='mrocontent_txtName']");
        public By contractTxtVal = By.XPath("//table[@id='mrocontent_dgReport']/tbody/tr[2]/td[4]");
        public By verifyAllContractButton = By.XPath("//input[@id='mrocontent_cmdVerifyAll']");
        public By contractIdSort = By.XPath("//img[@id='mrocontent_dgReport_tblReport_imgSortSwitch']");
        public static string conName = string.Empty;
        public By tableRows = By.XPath("//table[@id='mrocontent_dgReport']//tr");
        public By rowoneDeleteCheckBox = By.XPath("//table[@id='mrocontent_dgReport']/tbody/tr[2]//td[15]//input");


        /// <summary>
        /// Go to  Verify Active Contracts Page
        /// </summary>
        public ROIAdminContractListPage FacilityDropdownInContractListPage()
        {
            try
            {
                SelectElement oSelect = new SelectElement(Driver.FindElementBy(drpFacility));
                oSelect.SelectByText(facilityDDValue);
                Driver.Wait(TimeSpan.FromSeconds(2));
                DeleteContract();
                //var contractButton = Driver.FindElementBy(verifyAllContactButton);
                //contractButton.Click();
                //var headerValue = Driver.FindElementBy(headerElement);
                //Assert.AreEqual(headerValue.Text, "Verify Active Contracts");
                //IWebElement contractListButton= Driver.FindElementBy(returnToContractListButton);
                //contractListButton.Click();
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed  to click verify active contract button deatails as with Message : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }

            return new ROIAdminContractListPage(Driver,logger,Context);

        }

        /// <summary>
        /// Go to   Active  facility Contracts Page
        /// </summary>

        public ROIAdminVerifyActiveContractsPage  FacilityContractSelection()
        {
            try
            {
               Driver.DirectClick(cmdVerifyButton);
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to load active facility contracts with  deatails as Message : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
            return  new ROIAdminVerifyActiveContractsPage(Driver, logger,Context);
        }

       public void SelectFacilityFromDropdown()
        {
            try
            {
                SelectElement oSelect = new SelectElement(Driver.FindElementBy(drpFacility));
                oSelect.SelectByText(mroartfFacility);
                if(Driver.FindElementBy(includeTestChkBox).Selected==true)
                {
                    Driver.Click(includeTestChkBox);
                }
                if(Driver.FindElementBy(activeContractsChkBox).Selected==false)
                {
                    Driver.Click(activeContractsChkBox);
                }
                Driver.Click(facilitylinkSelection);
            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to select  facility contracts with  deatails as Message : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }

        public void SelectROIFacilityFromDropdown()
        {
            try
            {
                if (Driver.FindElementBy(includeTestChkBox,120).Selected == false)
                {
                    Driver.Click(includeTestChkBox);
                }
                if (Driver.FindElementBy(activeContractsChkBox,120).Selected == false)
                {
                    Driver.Click(activeContractsChkBox);
                }
                Driver.SleepTheThread(5);
                SelectElement oSelect = new SelectElement(Driver.FindElementBy(drpFacility));
                oSelect.SelectByText(roiFacilityDDValue);
                Driver.SleepTheThread(5);
                Driver.Click(facilitylinkSelection);
                Driver.SleepTheThread(5);
            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to select roi test facility contracts with  deatails as Message : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }
        public void DeleteContract()
        {
            try
            {
                //Driver.Click(By.Id("mrocontent_cbShowActive"));
                int numofRows = Driver.FindElements(By.XPath("//table[@id='mrocontent_dgReport']/tbody/tr")).Count;
                //table[@id="mrocontent_dgReport"]/tbody/tr[2]/td[15]/input
                string beforeXpath = "//table[@id='mrocontent_dgReport']//tbody//tr[";
                string afterXpath = "]//td[15]//input";
                for (int i = 2; i <= 50; i++)
                {


                    string actualXpath = beforeXpath + i + afterXpath;
                   // IWebElement elementValue = Driver.FindElementBy(By.XPath(actualXpath));
                    Driver.Click(By.XPath(actualXpath));
                    Driver.SwitchTo().Alert().Accept();
                    Driver.Wait(TimeSpan.FromSeconds(1));

                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        public void DeleteCreatedCurrentMonthContract()
        {
            try
            {
                Driver.Click(contractIdSort);
                Driver.Wait(TimeSpan.FromSeconds(2));
                Driver.Click(rowoneDeleteCheckBox);
                Driver.Wait(TimeSpan.FromSeconds(2));
                Driver.SwitchTo().Alert().Accept();
                Driver.Wait(TimeSpan.FromSeconds(2));
                Driver.Click(rowoneDeleteCheckBox);
                Driver.SwitchTo().Alert().Accept();
                Driver.Wait(TimeSpan.FromSeconds(2));
                Driver.Click(contractIdSort);

            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to delete created contracts with  deatails as Message : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }

        public void SelectFacilityInContractListPage(string facility)
        {
            try
            {
                if (Driver.FindElementBy(includeTestChkBox).Selected == false)
                {
                    Driver.Click(includeTestChkBox);
                }
                if (Driver.FindElementBy(activeContractsChkBox).Selected == false)
                {
                    Driver.Click(activeContractsChkBox);
                }
                SelectElement oSelect = new SelectElement(Driver.FindElementBy(drpFacility));
                oSelect.SelectByText(facility);
                Driver.Wait(TimeSpan.FromSeconds(2));
                var contractButton = Driver.FindElementBy(verifyAllContactButton);
                contractButton.Click();
                var headerValue = Driver.FindElementBy(headerElement);
                Assert.AreEqual(headerValue.Text, "Verify Active Contracts");
                IWebElement contractListButton = Driver.FindElementBy(returnToContractListButton);
                contractListButton.Click();

            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to select facility with  deatails as Message : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }

        public void ClickOnAddContract()
        {
            try
            {
                Driver.Click(toAddContractButton);
            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to click add contract button with  deatails as Message : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }

        public string VerifyCurrentMonthContracts()
        {
            try
            {
                Driver.Wait(TimeSpan.FromSeconds(2));
                Driver.Click(contractIdSort);
                Driver.Wait(TimeSpan.FromSeconds(2));
                conName = Driver.GetText(contractTxtVal);
                Driver.Click(contractIdSort);
                Driver.Wait(TimeSpan.FromSeconds(2));
                return conName;
            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to verify contracts name with  deatails as Message : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");

            }
        }
        public void ClickOnVerifyAllContractButton()
        {
            try
            {
                Driver.Click(verifyAllContactButton);

            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to  click verify all contracts button with  deatails as Message : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");

            }
        }

        public bool VerifyDeletedContracts()
        {
            try
            {
                int numOfRows = Driver.FindElements(tableRows).Count;
                string beforeXpath = "//table[@id='mrocontent_dgReport']//tr[";
                string afterXpath = "]//td[4]";
                Boolean isDeleted = true;
                if (numOfRows >= 2)
                {
                    for (int i = 2; i <= numOfRows; i++)
                    {
                        string actualXpath = beforeXpath + i + afterXpath;
                        string contractName = Driver.GetText(By.XPath(actualXpath));
                        if (contractName == conName)
                        {
                            logger.Log(Status.Info, "Contracts are not deleted");
                            isDeleted = false;

                        }
                    }
                }
                return isDeleted;
            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to verify deleted  contracts with  deatails as Message : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }

    }
}
