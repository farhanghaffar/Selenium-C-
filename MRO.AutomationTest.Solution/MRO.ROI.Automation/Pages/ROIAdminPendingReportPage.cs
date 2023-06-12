using AventStack.ExtentReports;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MRO.ROI.Automation.Selenium;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.UI;
using System;

namespace MRO.ROI.Automation.Pages
{
    public class ROIAdminPendingReportPage
    {
        public RemoteWebDriver Driver;
        public ExtentTest logger;
        public TestContext Context;
        public ROIAdminPendingReportPage(RemoteWebDriver driver, ExtentTest _loger, TestContext _context)
        {
            Driver = driver;
            logger = _loger;
            Context = _context;
        }

        public By requestLoggedFrom = By.XPath("//input[@id='mrocontent_ctlPending_txtDateA']");
        public By requestLoggedTo = By.XPath("//input[@id='mrocontent_ctlPending_txtDateZ']");
        public By facilityDrp = By.XPath("//select[@id='mrocontent_ctlPending_lstFacilities']");
        public By includeTestChk = By.XPath("//input[@id='mrocontent_ctlPending_cbTest']");
        public By createReportBtn = By.XPath("//input[@id='mrocontent_ctlPending_cmdCreate']");
        public By requestId = By.XPath("//*[@id='mrocontent_ctlPending_dgRequests']//tbody//tr[2]//td[3]");
        public By selectNumberOfRecordsDrp = By.XPath("//select[@id='mrocontent_ctlPending_tblRequests_lstPageSizes']");
        public void CreatePendingReport()
        {
            try
            {
                var todaysDate = String.Format("{0:M/dd/yyyy}", DateTime.Now).Replace("-", "/");
                Driver.ClearText(requestLoggedFrom);
                Driver.SendKeys(requestLoggedFrom, todaysDate);
                Driver.ClearText(requestLoggedTo);
                Driver.SendKeys(requestLoggedTo, todaysDate);
                var facilityDrpdown = Driver.FindElementBy(facilityDrp);
                var selectElement1 = new SelectElement(facilityDrpdown);
                selectElement1.SelectByText("ROI Test Facility");
                try
                {
                    string isIncludeTest = Driver.FindElementBy(includeTestChk).GetAttribute("checked").ToString();
                    if (isIncludeTest == "unchecked")
                    {
                        Driver.Click(includeTestChk);
                    }

                }catch(Exception ex)
                {
                    Driver.Click(includeTestChk);
                }
                Driver.Click(createReportBtn);
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to create pending report Exception: {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }

        }

        public void SelectTopHundredRecords()
        {
            var selectNumberOfRecordsDrpdown = Driver.FindElementBy(selectNumberOfRecordsDrp);
            var selectElement2 = new SelectElement(selectNumberOfRecordsDrpdown);
            selectElement2.SelectByText("100");

        }
        public bool VerifyRequestAtPendingReport(string requestId)
        {
            try
            {
                string path = $"//*[@id='mrocontent_ctlPending_dgRequests']//tbody//tr[2]//following::td[text()='{requestId}']";
                try
                {
                    IWebElement requestIdAtPendingReport = Driver.FindElementByWithOutThrow(By.XPath(path));
                    return requestIdAtPendingReport.Displayed;

                }catch(Exception ex)
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to verify requestid at pending report page Exception: {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }

        }
    }
}
