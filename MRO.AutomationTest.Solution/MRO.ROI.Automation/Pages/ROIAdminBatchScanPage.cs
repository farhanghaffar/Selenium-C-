using AventStack.ExtentReports;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MRO.ROI.Automation.Selenium;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MRO.ROI.Automation.Pages
{
   public class ROIAdminBatchScanPage
    {
        public RemoteWebDriver Driver;
        public ExtentTest logger;
        public TestContext Context;
        public ROIAdminBatchScanPage(RemoteWebDriver driver, ExtentTest _loger, TestContext _context)
        {
            Driver = driver;
            logger = _loger;
            Context = _context;
        }

        public By RequestIDs = By.XPath("//textarea[@id='mrocontent_txtRequestIDs']");
        public By CreateList = By.XPath("//input[@id='mrocontent_cmdCreate']");
        public By SelectAll = By.XPath("//input[@id='mrocontent_btnSelAll']");
        public By ChangeLocation = By.XPath("//select[@id='mrocontent_lstMode']");
        public By Location = By.XPath("//select[@id='mrocontent_lstLocation']");
        public By SelectedRequests = By.XPath("//input[@id='mrocontent_cmdSetTag']");
        public By ChangedLocation = By.XPath("//table[@id='mrocontent_dgReport']//tr[2]//td[4]");
        public By ComplianceDemoLink = By.XPath("//a[text()='Compliance Demo']");
        public By ROILink = By.XPath("(//span[contains(@class,'rtsTxt') and contains(text(), 'ROI')]/ancestor::a)[1]");
        public By UpdateInfoBtn = By.XPath("//*[@id='mrocontent_cmdUpdate']");

        public void SetRequestIds(string ID1, string ID2)
        {
            try
            {
                Automation.Common.Iframe frame = new Automation.Common.Iframe(Driver, logger, Context);
                //frame.SwitchToRoiFrame();
                Driver.Wait(TimeSpan.FromSeconds(1));

                IWebElement requestIds = Driver.FindElementBy(RequestIDs);
                requestIds.SendKeys(ID1);
                requestIds.SendKeys(ID2);
            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to set request id's : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }

        }

        public void ClickCreateList()
        {
            try
            {
                IWebElement createList = Driver.FindElementBy(CreateList);
                createList.Click();
            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to click on create list : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
            
        }

        public void ClickSelectAllAndChangeLocation(string PatientLocation)
        {
            try
            {
                IWebElement selectAll = Driver.FindElementBy(SelectAll);
                selectAll.Click();
                IWebElement changeLocation = Driver.FindElementBy(ChangeLocation);
                changeLocation.SendKeys("Change Location");
                IWebElement location = Driver.FindElementBy(Location);
                location.SendKeys(PatientLocation);
                IWebElement selectRequest = Driver.FindElementBy(SelectedRequests);
                selectRequest.Click();

            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to click on select all : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }

        }

        public string GetLocation()
        {
            Driver.WaitInSeconds(2);
            string changedLocation = Driver.FindElementBy(ChangedLocation).Text;
            return changedLocation;
        }

        public void ClickComplianceDemoLink()
        {
            Driver.FindElementBy(ComplianceDemoLink).Click();
        }
        public void ClickLocationLink(string location)
        {
            By LocationLink = By.XPath($"//a[text()='{location}']");
            Driver.FindElementBy(LocationLink).Click();
        }

        public void ClickROILink()
        {
            Driver.WaitUntilDOMLoaded();
            Driver.FindElementBy(ROILink).Click();
        }


        public void SelectLocationInfo(string property, string optionToSelect)
        {
            By path = By.XPath($"(//td[contains(text(), '{property}')]/following-sibling::td//select)[1]");

            var electElement = new SelectElement(Driver.FindElementBy(path));
            electElement.SelectByText(optionToSelect);

        }

        public void ClickUpdateInfoBtn()
        {
            Driver.FindElementBy(UpdateInfoBtn);
        }
    }
}
