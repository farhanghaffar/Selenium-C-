using AventStack.ExtentReports;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MRO.ROI.Automation.Selenium;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MRO.ROI.Automation.Utility.IniFile;

namespace MRO.ROI.Automation.Pages
{
  public class ROIFacilityLocationROITestFacilityPage
    {
        public RemoteWebDriver Driver;
        public ExtentTest logger;
        public TestContext Context;
        public ROIFacilityLocationROITestFacilityPage(RemoteWebDriver driver, ExtentTest _loger, TestContext _context)
        {
            Driver = driver;
            logger = _loger;
            Context = _context;
        }

        
        public By pageHeader = By.Id("MasterHeaderText");
        public By henryFordRow = By.XPath("//a[contains(text(),'Henry Ford')]");
        public By rqrLocationElement = By.Id("mrocontent_txtRequesterLocationCode");
        public By updateInfoBtn = By.Id("mrocontent_cmdUpdate");
        public By infoMsg = By.Id("mrocontent_lblUpdated");
        public By code = By.XPath("//input[@id='mrocontent_txtCode']");
        public By genearlTab = By.XPath("//span[@class='rtsTxt' and contains(text(),'General')]");
        public By locationId = By.XPath("//input[@id='mrocontent_txtAddID']");
        public By locationName = By.XPath("//input[@id='mrocontent_txtAddName']");
        public By locationCode = By.XPath("//input[@id='mrocontent_txtAddCode']");
        public By addLocationBtn = By.XPath("//input[@id='mrocontent_cmdAddLocation']");
        public By reolacementLocationId = By.XPath("//input[@id='mrocontent_txtReplacementLocationID']");

        public By AllTab = By.XPath("//span[@class='rtsTxt'and contains(text(),'All')]");
        public By InternalPortalTab = By.XPath("//span[@class='rtsTxt'and contains(text(),'Internal Portal')]");
        public By OnSiteDeliveryTab = By.XPath("//span[@class='rtsTxt'and contains(text(),'On-Site')]");
        public By MRODeliveryTab = By.XPath("//span[@class='rtsTxt'and contains(text(),'MRO Delivery')]");
        public By BillingInfoTab = By.XPath("//span[@class='rtsTxt'and contains(text(),'Billing Office')]");





        public string VerifyFacilityLocationsPageHeader()
        {
            try
            {
                string header = Driver.GetText(pageHeader);
                header = header.Trim();
                return header;


            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to return page header with Exception: {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");

            }

        }

        public void SelectHenryFord()
        {
            try
            {
                Driver.Click(henryFordRow);
            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to click henry ford  Exception with details message as: {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }

        public bool CheckRqrLocationDisabled()
        {
            bool isDisabled = false;
            try
            {
                bool result = Driver.FindElement(rqrLocationElement).Displayed;
                IWebElement rqrLocElement = Driver.FindElementBy(rqrLocationElement);

                if (rqrLocElement != null)
                {
                    string value = rqrLocElement.GetAttribute("disabled");

                    if (value == "true")
                    {
                        isDisabled = true;
                    }

                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to check rqrLocation, Exception detail: {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
            return isDisabled;
        }

        public string AddRqrLocationCode()
        {
            try
            {
                Driver.Click(genearlTab);
                Driver.ClearText(rqrLocationElement);
                Driver.SendKeys(rqrLocationElement, "123Test");
                Driver.Click(updateInfoBtn);
                string info = Driver.GetText(infoMsg);
                return info;
            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to check rqrLocation, Exception detail: {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }

        }

        public string ClearRqrLocationCode()
        {
            try
            {
                Driver.Click(genearlTab);
                Driver.ClearText(rqrLocationElement);
                Driver.Click(updateInfoBtn);
                string codeVal = Driver.FindElement(code).GetAttribute("value");
                return codeVal;
            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to check rqrLocation, Exception detail: {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }

        }

        public void AddLocation()
        {
            try
            {
                string name = IniHelper.ReadConfig("AddLocationDetails", "LocationName");
                string code = IniHelper.ReadConfig("AddLocationDetails", "LocationCode");
                Random rand = new Random();
                int value = rand.Next(1000, 10000);
                Driver.SendKeys(locationId, value.ToString());
                Driver.SendKeys(locationName, name + value);
                string locName = Driver.GetText(locationName);
                Driver.SendKeys(locationCode, value.ToString());
                Driver.Click(addLocationBtn);               


            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to check rqrLocation, Exception detail: {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }

        }

        public string GetLocationName()
        {
            try
            {
                int numOfRows = Driver.FindElements(By.XPath("//*[@id='mrocontent_dgLocations']/tbody/tr")).Count;
                string beforeXpath= "//*[@id='mrocontent_dgLocations']/tbody/tr[";
                string afterXpath = "]/td[3]/a";
                string actualXpath = beforeXpath + numOfRows + afterXpath;
                string locationName = Driver.GetText(By.XPath(actualXpath));
                return locationName;

            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to get location name, Exception detail: {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }

        }

        public void DeleteCreatedLocation()
        {
            try
            {
                int numOfRows = Driver.FindElements(By.XPath("//*[@id='mrocontent_dgLocations']/tbody/tr")).Count;
                string beforeXpath = "//*[@id='mrocontent_dgLocations']/tbody/tr[";
                string afterXpath = "]/td[13]/input";
                string actualXpath = beforeXpath + numOfRows + afterXpath;
                Driver.Click(By.XPath(actualXpath));
               
                Driver.SwitchTo().Alert().Accept();
                Driver.Wait(TimeSpan.FromSeconds(2));
                Driver.SwitchTo().Alert().Accept();
                Random rand = new Random();
                int value = rand.Next(1000, 10000);
                Driver.SendKeys(reolacementLocationId, value.ToString());
                Driver.Click(By.XPath(actualXpath));
                Driver.SwitchTo().Alert().Accept();
                Driver.Wait(TimeSpan.FromSeconds(2));
            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to delete location, Exception detail: {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }



        public void DocsReqdTabsClick()
        {
            try
            {
                Driver.Click(InternalPortalTab);
                Driver.Click(OnSiteDeliveryTab);
                Driver.Click(MRODeliveryTab);
                Driver.Click(AllTab);

            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to check rqrLocation, Exception detail: {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }

        }


        public void DocsReqdTabsClickr()
        {
            try
            {
                Driver.Click(InternalPortalTab);
                Driver.Click(OnSiteDeliveryTab);
                Driver.Click(MRODeliveryTab);
                Driver.Click(AllTab);
                Driver.Click(BillingInfoTab);
            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to check rqrLocation, Exception detail: {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }

        }

    }
}
