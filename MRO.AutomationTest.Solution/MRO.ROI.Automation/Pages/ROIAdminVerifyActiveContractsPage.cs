using AventStack.ExtentReports;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MRO.ROI.Automation.Selenium;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.UI;
using System;

namespace MRO.ROI.Automation.Pages
{
    public class ROIAdminVerifyActiveContractsPage
    {
        public RemoteWebDriver Driver;
        public ExtentTest logger;
        public TestContext Context;
        public ROIAdminVerifyActiveContractsPage(RemoteWebDriver driver, ExtentTest _loger, TestContext _context)
        {
            Driver = driver;
            logger = _loger;
            Context = _context;
        }

        public By drpLastSixMonths = By.XPath("//*[@id='mrocontent_lstLastSixMonths']");
        public By contractIdIcon = By.XPath("//*[@id='mrocontent_dgReport_tblReport_imgSortSwitch']");
        public By contractName = By.XPath("//*[@id='mrocontent_dgReport']/tbody/tr[2]/td[6]");
        public By errorInfoMsg = By.XPath("//*[@id='mrocontent_dgReport']/tbody/tr[2]/td[12]");
        public By prevMonthContract = By.XPath("//*[@id='mrocontent_tblReport']/tbody/tr/td");
        public By cmdReturnButton = By.XPath("//*[@id='mrocontent_cmdReturn1']");      
        public By firstrowContractId = By.XPath("//table[@id='mrocontent_dgReport']/tbody/tr[2]/td[5]/a");
        public By fromDate = By.XPath("//input[@id='mrocontent_txtEffectiveDate']");


        /// <summary>
        /// To verify Previous  month contracts
        /// </summary>
        public int SelectPreviousMonthContractList()
        {
            try
            {
                SelectElement oSelect = new SelectElement(Driver.FindElementBy(drpLastSixMonths));
                oSelect.SelectByIndex(2);
                string selectedMonth = Driver.FindElementBy(drpLastSixMonths).GetAttribute("value");
                selectedMonth= selectedMonth.Split('/')[0].ToString().Trim();
                int month = Convert.ToInt32(selectedMonth);
                return month;

            }
            catch (Exception ex)
            {

                throw new Exception($"Failed  to click last six months dropdown with Message : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");

            }
        }

        public string SelectCurrentMonthContractList()
        {
            try
            {
                SelectElement oSelect1 = new SelectElement(Driver.FindElementBy(drpLastSixMonths));
                oSelect1.SelectByIndex(1);
                Driver.Click(contractIdIcon);
                string contractname = Driver.FindElementBy(contractName).Text;
                string infomsg = Driver.FindElementBy(errorInfoMsg).Text;
                Driver.Click(contractIdIcon);
                return contractname;
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed  to view contracts for selected month with details as Message : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }

        public void ReturnToContractList()
        {
            try
            {

                Driver.Click(cmdReturnButton);
            }
            catch (Exception ex)
            {

                throw new Exception($"Failed  to click return to contract list with details as Message : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }

        public int VerifyPreviousMonthContracts()
        {
            try
            {
                Driver.Click(firstrowContractId);
                string month = Driver.FindElementBy(fromDate).GetAttribute("value");
                month = month.Split('/')[0].ToString().Trim();
                int createdMonth = Convert.ToInt32(month);
                return createdMonth;
            }
            catch (Exception ex)
            {

                throw new Exception($"Failed  to verify previous month contracts  with details as Message : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }
    }
}
