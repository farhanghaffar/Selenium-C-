using AventStack.ExtentReports;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MRO.ROI.Automation.Selenium;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Remote;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MRO.ROI.Automation.Pages
{
  public class ROIFacilityInfoROITestFacilityPage
    {
        public RemoteWebDriver Driver;
        public ExtentTest logger;
        public TestContext Context;
        public ROIFacilityInfoROITestFacilityPage(RemoteWebDriver driver, ExtentTest _loger, TestContext _context)
        {
            Driver = driver;
            logger = _loger;
            Context = _context;
        }

        public By ViewFeatures = By.XPath("//input[@id='mrocontent_cmdFeatures']");
        public By pageHeader = By.Id("MasterHeaderText");
        public By facilityElement = By.XPath("(//td[contains(text(),'Facilities')])[2]");
        public By facilityIcon = By.XPath("//*[contains(text(),'Facility >')]");
        public By userFlags = By.XPath("//*[contains(text(),'Facility User Flags')]");
        public By documentTypes = By.XPath("//td[contains(text(),'Facility Document Types' )]");
        public By facilitySecurity = By.XPath("//td[contains(text(),'Facility Security')]");



        public void ClickOnViewFeatures()
        {
            try
            {
                IWebElement viewFeatures = Driver.FindElementBy(ViewFeatures);
                viewFeatures.Click();

            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to click on view features as  new Exception: {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");

            }

        }

        public string VerifyFacilityInfoPageHeader()
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

        public void SelectFacilityUserFlags()
        {
            try
            {
                Actions action = new Actions(Driver);
                Driver.Wait(TimeSpan.FromSeconds(3));
                IWebElement facilities = Driver.FindElementBy(facilityElement);
                action.MoveToElement(facilities).Perform();
                Driver.Wait(TimeSpan.FromSeconds(1));
                IWebElement facility = Driver.FindElementBy(facilityIcon, 5);
                action.MoveToElement(facility).Perform();
                Driver.Wait(TimeSpan.FromSeconds(3));
                IWebElement facilityUserFlags = Driver.FindElementBy(userFlags);
                action.MoveToElement(facilityUserFlags).Click().Build().Perform();
                Driver.Wait(TimeSpan.FromSeconds(1));
            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to select facility user flags with Exception: {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
           
        }

        public void SelectFacilityDocumentTypes()
        {
            try
            {
                Actions action = new Actions(Driver);
                Driver.Wait(TimeSpan.FromSeconds(3));
                IWebElement facilities = Driver.FindElementBy(facilityElement);
                action.MoveToElement(facilities).Perform();
                Driver.Wait(TimeSpan.FromSeconds(1));
                IWebElement facility = Driver.FindElementBy(facilityIcon, 5);
                action.MoveToElement(facility).Perform();
                Driver.Wait(TimeSpan.FromSeconds(3));
                IWebElement facilityDocumentTypes = Driver.FindElementBy(documentTypes);
                action.MoveToElement(facilityDocumentTypes).Click().Build().Perform();
                Driver.Wait(TimeSpan.FromSeconds(1));
            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to select facility document types with Exception: {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }

        }



        public void SelectFacilitySecurity()
        {
            try
            {
                Actions action = new Actions(Driver);
                Driver.Wait(TimeSpan.FromSeconds(3));
                IWebElement facilities = Driver.FindElementBy(facilityElement);
                action.MoveToElement(facilities).Perform();
                Driver.Wait(TimeSpan.FromSeconds(1));
                IWebElement facility = Driver.FindElementBy(facilityIcon, 5);
                action.MoveToElement(facility).Perform();
                Driver.Wait(TimeSpan.FromSeconds(3));
                IWebElement Security = Driver.FindElementBy(facilitySecurity);
                action.MoveToElement(Security).Click().Build().Perform();
                Driver.Wait(TimeSpan.FromSeconds(1));
            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to select facility security with Exception: {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }

        }
    }
}
