using AventStack.ExtentReports;
using MRO.ROI.Automation.Selenium;
using System;
using OpenQA.Selenium;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium.Remote;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace MRO.ROI.Automation.Pages
{
    public class ROIFacilityDeliveryReportPage
    {
        public RemoteWebDriver Driver;
        public ExtentTest logger;
        public TestContext Context;
        public ROIFacilityDeliveryReportPage(RemoteWebDriver driver, ExtentTest _loger, TestContext _context)
        {
            Driver = driver;
            logger = _loger;
            Context = _context;
        }

        public By DeliveredFromDate = By.XPath("//input[@id='mrocontent_txtDateA']");
        public By DeliveredToDate = By.XPath("//input[@id='mrocontent_txtDateZ']");
        public By CreateReportBtn = By.XPath("//input[@id='mrocontent_cmdCreate']");//
        public By EditColumnsBtn = By.XPath("//input[@id='mrocontent_MROReportGridBanner_cmdEditConfigurationColumns']");
        public By chkPAN = By.Id("mrocontent_MROReportGridBanner_lstRadColumns_5");
        public By SaveColumnsConfigurationBtn = By.Id("mrocontent_MROReportGridBanner_cmdSaveRadColumns");
        public By firstNameTxt = By.XPath("//input[@id='mrocontent_txtPatientFirstName']");
        public By lastNameTxt = By.XPath("//input[@id='mrocontent_txtPatientLastName']");
        //
        public void SetDeliveredDate()
        {
            try
            {
                var todaysDate = String.Format("{0:M/dd/yyyy}", DateTime.Now).Replace("-", "/");
                var fromDate = Driver.FindElementBy(DeliveredFromDate);
                fromDate.Clear();
                fromDate.SendKeys(todaysDate);
                var toDate = Driver.FindElementBy(DeliveredToDate);
                toDate.Clear();
                toDate.SendKeys(todaysDate);

            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to set from and to dates : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }
        public void ClickCreateReportBtn()
        {
            try
            {
                Driver.DirectClick(CreateReportBtn);

            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to create report button as Message : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }

        }

        public void ClickEditColumnsButton()
        {
            try
            {
                Driver.DirectClick(EditColumnsBtn);

            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to click on edit columns button as Message : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }

        }

        public void CheckPAN()
        {
            try
            {
                bool isCheck = Driver.FindElementBy(chkPAN).Selected;
                if (isCheck == false)
                {
                    Driver.Click(chkPAN);
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to check PAN checkbox with message as : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }
        public void ClickSaveColumnsConfiuration()
        {
            try
            {
                Driver.DirectClick(SaveColumnsConfigurationBtn);
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to click on save columns configuration button as Message : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }

        }
        public void FnValues(String fn)
        {
            try
            {
                Driver.FindElementBy(firstNameTxt).SendKeys(fn);

            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to give the fist name values as Message : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }
        public void LnValues(String ln)
        {
            try
            {

                Driver.FindElementBy(lastNameTxt).SendKeys(ln);
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to give the last name values as Message : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }
        public bool CheckRequestIdExsist(string requestId)
        {
            bool isValidated = false;
            try
            {
                var requestElements = Driver.FindElementsBy(By.XPath("//div[@id='mrocontent_dgReport_GridData']//table[@class='rgMasterTable rgClipCells']//tbody//tr//td[1]"));
                foreach (var requestElement in requestElements)
                {
                    if (requestElement.Text.Equals(requestId))
                    {
                        isValidated = true;
                        break;
                    }
                }
            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to check request id aganist request table {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
            return isValidated;
        }
        public void SetDeliverednowDate()
        {
            try
            {
                var todaysDate = String.Format("{0:M/dd/yyyy}", DateTime.Now).Replace("-", "/");
                var fromDate = Driver.FindElementBy(DeliveredFromDate);
                fromDate.Clear();
                fromDate.SendKeys(todaysDate);
                var toDate = Driver.FindElementBy(DeliveredToDate);
                toDate.Clear();
                toDate.SendKeys(todaysDate);

            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to set from and to dates : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }
    }
}

