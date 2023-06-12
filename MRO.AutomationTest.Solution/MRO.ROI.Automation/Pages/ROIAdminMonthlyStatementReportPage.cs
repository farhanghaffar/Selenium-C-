using AventStack.ExtentReports;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MRO.ROI.Automation.Common.Navigation;
using MRO.ROI.Automation.Selenium;
using MRO.ROI.Automation.Utility;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.UI;
using System;
using static MRO.ROI.Automation.Common.Navigation.FacilityMenuNavigation.ROIRequests;
using static MRO.ROI.Automation.Utility.IniFile;

namespace MRO.ROI.Automation.Pages
{
    public class ROIAdminMonthlyStatementReportPage
    {
        public RemoteWebDriver Driver;
        public ExtentTest logger;
        public TestContext Context;
        public ROIAdminMonthlyStatementReportPage(RemoteWebDriver driver, ExtentTest _loger, TestContext _context)
        {
            Driver = driver;
            logger = _loger;
            Context = _context;
        }
        
        public By monthDrpElement = By.XPath("//table[@id='mrocontent_tblOptions']//select[@id='mrocontent_ddlMonth']");           
        public By yearTxtElement = By.Id("mrocontent_txtBxYear");
        public By facilityDrp = By.XPath("//table[@id='mrocontent_tblOptions']//select[@id='mrocontent_lstFac']");
        public static string facilityDrpValue = "MRO Automated Regression Test";
        public By contractDrp = By.XPath("//table[@id='mrocontent_tblOptions']//select[@id='mrocontent_lstContract']");
        public static string contractVal = "1082 - MRO Automated Regression Test Contract";
        public By createStmtBtn = By.Id("mrocontent_cmdCreate");
        public By includeTestFacilityChkbox = By.Id("mrocontent_cbIncludeTest");
        public By showDetailsChkbox = By.Id("mrocontent_cbShowDetailsLinks");
        public By releaseRequestFeeHyperlink = By.XPath("//td[@id='mrocontent_custMonthlyStatement_rptReport_tdBOEReleaseFee_0']/a");
        public By releaseRequestAmount = By.XPath("//td[@id='mrocontent_custMonthlyStatement_rptReport_tdBOEReleaseFee_0']/../td[@class='Amount']");
        public By monthValue = By.Id("mrocontent_lblMonth");
        public By facilityval = By.Id("mrocontent_lblFacCap");
        public By contractElement = By.Id("mrocontent_lblContract");
        public string currentMonthYear = string.Empty;       
        public By boeTransactionFeeHyperlink = By.XPath("//td[@id='mrocontent_custMonthlyStatement_rptReport_tbBOETransactionFee_0']//a");
        public By boeTransactionAmount = By.XPath("//td[@id='mrocontent_custMonthlyStatement_rptReport_tbBOETransactionFee_0']/../td[@class='Amount'][1]");
        public By pageDrp = By.XPath("//select[@id='mrocontent_tblDetails_lstPageSizes']");
        public By BOETransactionFeeHyperLink = By.XPath("//td[@id='mrocontent_custMonthlyStatement_rptReport_tbBOETransactionFee_0']/a");
        public By BOETransactionFeeAmount = By.XPath("(//td[@id='mrocontent_custMonthlyStatement_rptReport_tbBOETransactionFee_0']/../td[@class='Amount'])[1]");

        public void CreateCurrentMonthStatement()
        {
            try
            {              
                currentMonthYear= String.Format("{0:MMMM/yyyy}", DateTime.Now).Replace('/',' ');
                string facilityDrpValue = IniHelper.ReadConfig("ROIRequestReleasedFeeContractTermTest", "facility");
                string contractVal = IniHelper.ReadConfig("ROIRequestReleasedFeeContractTermTest", "contract");                 
                int month = DateTime.Now.Month;
                bool isIncludeTestFacility = Driver.FindElementBy(includeTestFacilityChkbox).Selected;
                bool isShowDetailsChkbox = Driver.FindElementBy(showDetailsChkbox).Selected;
                SelectElement monthSelection = new SelectElement(Driver.FindElementBy(monthDrpElement));
                monthSelection.SelectByIndex(month);                
                Driver.ClearText(yearTxtElement);
                Driver.SendKeys(yearTxtElement, DateTime.Now.Year.ToString());
                Driver.SendKeys(facilityDrp, facilityDrpValue);
                Driver.SendKeys(contractDrp, contractVal);
                if(isIncludeTestFacility==false)
                {
                    Driver.Click(includeTestFacilityChkbox);
                }
                if(isShowDetailsChkbox==false)
                {
                    Driver.Click(showDetailsChkbox);
                }

                Driver.DirectClick(createStmtBtn);
                Driver.Wait(TimeSpan.FromSeconds(15));
                Driver.SwitchTo().Alert().Accept();
                Driver.Wait(TimeSpan.FromSeconds(2));
            }
            catch (Exception ex)
            {

                throw new Exception($"Failed  to create statement with details  Message : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }
        public void VerifyMonthlyStatementAmount()
        {
            try
            {
                string text = Driver.GetText(releaseRequestFeeHyperlink);
                string numofRequests = text.Split(' ')[3].ToString().Trim();
                numofRequests = numofRequests.Replace('(', ' ');
                int _noOfRequ = Convert.ToInt32(numofRequests);
                string releaseFeeVal = text.Split(' ')[7].ToString().Trim();
                releaseFeeVal = releaseFeeVal.Replace(')', ' ');
                releaseFeeVal = releaseFeeVal.Replace('$', ' ').Trim();
                decimal feeVal = Convert.ToDecimal(releaseFeeVal);
                decimal result = (_noOfRequ) * (feeVal);
                string amount = Driver.GetText(releaseRequestAmount);
                amount = amount.Replace('$', ' ').Trim();
                if (amount == result.ToString())
                {
                    
                    logger.Log(Status.Pass, $"Verified request release fee as dollar value and dollar value is:${(amount)}");
                }
                Driver.ScrollIntoViewAndClick(releaseRequestFeeHyperlink);
            }
            catch (Exception ex)
            {

                throw new Exception($"Failed  to verify release request amount with details  Message : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }
        public string Selectedmonth()
        {
            try
            {
                return Driver.GetText(monthValue);
            }
            catch (Exception ex)
            {

                throw new Exception($"Failed  to get selected month  with details  Message : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }
        public string SelectedFacility()
        {
            try
            {
                return Driver.GetText(facilityval);
            }
            catch (Exception ex)
            {

                throw new Exception($"Failed  to return facility value with details  Message : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
             
        }
        public void CreateCurrentMonthStatementForROITestFacility()
        {
            try
            {
                currentMonthYear = String.Format("{0:MMMM/yyyy}", DateTime.Now).Replace('/', ' ');
                string facilityDrpValue = IniHelper.ReadConfig("MultipleContractTermUserStoriesTest", "Facility");
                string contractVal = IniHelper.ReadConfig("MultipleContractTermUserStoriesTest", "Contract");
                int month = DateTime.Now.Month;
                bool isIncludeTestFacility = Driver.FindElementBy(includeTestFacilityChkbox).Selected;
                bool isShowDetailsChkbox = Driver.FindElementBy(showDetailsChkbox).Selected;
                SelectElement monthSelection = new SelectElement(Driver.FindElementBy(monthDrpElement));
                monthSelection.SelectByIndex(month);
                Driver.ClearText(yearTxtElement);
                Driver.SendKeys(yearTxtElement, DateTime.Now.Year.ToString());
                Driver.SendKeys(facilityDrp, facilityDrpValue);
                Driver.SleepTheThread(5);
                Driver.SendKeys(contractDrp, contractVal);
                Driver.SleepTheThread(5);
                if (isIncludeTestFacility == false)
                {
                    Driver.Click(includeTestFacilityChkbox);
                }
                if (isShowDetailsChkbox == false)
                {
                    Driver.Click(showDetailsChkbox);
                }
                Driver.FindElementBy(createStmtBtn).Click();
                Driver.SleepTheThread(10);
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to create statement for roi test facility with details  Message : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }

        public void VerifyMonthlyStatementAmountForBOETransactionFee()
        {
            try
            {
                string text = Driver.GetText(boeTransactionFeeHyperlink);
                string numofRequests = text.Split(' ')[3].ToString().Trim();
                numofRequests = numofRequests.Replace('(', ' ');
                int _noOfRequ = Convert.ToInt32(numofRequests);
                string shipmentFeeVal = text.Split(' ')[7].ToString().Trim();
                shipmentFeeVal = shipmentFeeVal.Replace(')', ' ');
                shipmentFeeVal = shipmentFeeVal.Replace('$', ' ').Trim();
                decimal feeVal = Convert.ToDecimal(shipmentFeeVal);
                decimal result = (_noOfRequ) * (feeVal);
                string amount = Driver.GetText(boeTransactionAmount);
                amount = amount.Replace('$', ' ').Trim();
                if (amount == result.ToString())
                {
                    logger.Log(Status.Pass, $"Verified boe transaction fee amount :${(amount)}");
                }
                Driver.ScrollIntoViewAndClick(boeTransactionFeeHyperlink);
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to verify boe transaction fee amount with details  Message : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }
        public void ClickOnRequestID(string requestid)
        {
            try
            {
                string requestID = requestid.Trim();
                SelectElement selectPage = new SelectElement(Driver.FindElementBy(pageDrp));
                selectPage.SelectByText("1000");
                Driver.ScrollToElement(By.XPath($"//a[contains(text(),'{requestID}')]"));
                Driver.FindElementBy(By.XPath($"//a[contains(text(),'{requestID}')]")).Click();
                Driver.SleepTheThread(5);
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to click request id with details  Message : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }

        }

        public void CreateCurrentMonthStatementForBOE()
        {
            try
            {
                currentMonthYear = String.Format("{0:MMMM/yyyy}", DateTime.Now).Replace('/', ' ');
                string facilityDrpValue = IniHelper.ReadConfig("ROIAdminBOETransactionFeeUpdateTest", "FacilityDD");
                string contractVal = IniHelper.ReadConfig("ROIAdminBOETransactionFeeUpdateTest", "ContractDD");
                int month = DateTime.Now.Month;
                bool isIncludeTestFacility = Driver.FindElementBy(includeTestFacilityChkbox).Selected;
                bool isShowDetailsChkbox = Driver.FindElementBy(showDetailsChkbox).Selected;
                SelectElement monthSelection = new SelectElement(Driver.FindElementBy(monthDrpElement));
                monthSelection.SelectByIndex(month);
                Driver.ClearText(yearTxtElement);
                Driver.SendKeys(yearTxtElement, DateTime.Now.Year.ToString());
                Driver.SendKeys(facilityDrp, facilityDrpValue);
                Driver.SendKeys(contractDrp, contractVal);
                if (isIncludeTestFacility == false)
                {
                    Driver.Click(includeTestFacilityChkbox);
                }
                if (isShowDetailsChkbox == false)
                {
                    Driver.Click(showDetailsChkbox);
                }

                Driver.Click(createStmtBtn);
                Driver.Wait(TimeSpan.FromSeconds(5));

            }
            catch (Exception ex)
            {

                throw new Exception($"Failed  to create statement with details  Message : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }

        public void VerifyBOETransactionFeeAndClick()
        {
            try
            {
                string text = Driver.GetText(BOETransactionFeeHyperLink);
                string numofRequests = text.Split(' ')[3].ToString().Trim();
                numofRequests = numofRequests.Replace('(', ' ');
                int _noOfRequ = Convert.ToInt32(numofRequests);
                string releaseFeeVal = text.Split(' ')[7].ToString().Trim();
                releaseFeeVal = releaseFeeVal.Replace(')', ' ');
                releaseFeeVal = releaseFeeVal.Replace('$', ' ').Trim();
                decimal feeVal = Convert.ToDecimal(releaseFeeVal);
                decimal result = (_noOfRequ) * (feeVal);
                string amount = Driver.FindElementBy(BOETransactionFeeAmount).Text;
                amount = amount.Replace('$', ' ').Trim();
                if (amount == result.ToString())
                {

                    logger.Log(Status.Pass, $"Verified request release fee as dollar value and dollar value is:${(amount)}");
                }
                Driver.ScrollIntoViewAndClick(BOETransactionFeeHyperLink);
            }
            catch (Exception ex)
            {

                throw new Exception($"Failed  to verify release request amount with details  Message : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }


    }
}
