using AventStack.ExtentReports;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MRO.ROI.Automation.Selenium;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Remote;
using System;


namespace MRO.ROI.Automation.Pages
{ 
    public class ROIAdminFacilityPoliciesPage
    {
        public RemoteWebDriver Driver;
        public ExtentTest logger;
        public TestContext Context;
        public ROIAdminFacilityPoliciesPage(RemoteWebDriver driver, ExtentTest _loger, TestContext _context)
        {
            Driver = driver;
            logger = _loger;
            Context = _context;
        }

        public By facilityDropdown = By.Id("mrocontent_custFacilityPolicySelector_ddlFacility_Arrow");
        public By facilityLocationCheckBox = By.XPath("//label[contains(text(),'Duke University Health System')]");
        public By facilitylocation = By.XPath("//input[@id='mrocontent_custFacilityPolicySelector_ddlFacility_i102_ddlCheckBox']/..");       
        public By locationDropdown=By.Id("mrocontent_custFacilityPolicySelector_ddlLocation_Arrow");
        public By locationSelection = By.XPath("//label[contains(text(),'_Duke Stage Testing')]");
        public By findPolicyButton = By.Id("mrocontent_cmdFindPolicy");
        public By dukeUniversityCheckbox = By.XPath("//tr[@class='rgRow']/td[1]/input");
        public By viewEditButton = By.XPath("//input[@id='mrocontent_custFacilityPolicyList_btnEdit']");
        public By facilityPolicyLable  = By.XPath("//span[contains(text(),'Facility Policy Search Filter:')]");

        /// <summary>
        /// Select  Find Policy
        /// </summary> 
        public ROIAdminEditFacilityPolicy FindPolicy()
        {
            try
            {
                Driver.Click(facilityDropdown);
                Driver.Click(facilityLocationCheckBox);
                Driver.Wait(TimeSpan.FromSeconds(10));
                Driver.DirectClick(facilityPolicyLable);
                Driver.DirectClick(locationDropdown);
                Driver.Wait(TimeSpan.FromSeconds(2));
                Driver.DirectClick(locationSelection);
                Driver.Wait(TimeSpan.FromSeconds(10));
                Driver.Click(findPolicyButton);
                Driver.Click(dukeUniversityCheckbox);
                Driver.Click(viewEditButton);
            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to Find Policy with Message : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }

            return new ROIAdminEditFacilityPolicy(Driver,logger,Context);

        }

       
    }
}
