using AventStack.ExtentReports;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MRO.ROI.Automation.Selenium;
using MRO.ROI.Automation.Utility;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.UI;
using System;
using System.IO;
using System.Reflection;

namespace MRO.ROI.Automation.Pages
{
    public class ROIFacilityAddComponentPage
    {
        public RemoteWebDriver Driver;
        public ExtentTest logger;
        public TestContext Context;
        public CSVReader csvReader;
        
        public ROIFacilityAddComponentPage(RemoteWebDriver driver, ExtentTest _loger, TestContext _context)
        {
            Driver = driver;
            logger = _loger;
            Context = _context;
        }
        public By selectComponentLocation = By.XPath("//select[@id='mrocontent_lstLocations']");
        public By txtDescription = By.XPath("//input[@id='mrocontent_txtDescription']");
        public By addButton = By.XPath("//input[@id='mrocontent_cmdAddComponent']");
        public By addComponentButton = By.XPath("//input[@id='mrocontent_cmdAddEdit']");

        /// <summary>
        ///Add components
        /// </summary>
        public ROIFacilityRequestStatusPage AddComponent(string componentDescription)
        {
            try
            {
                
                var selectComponentLoc = Driver.FindElementBy(selectComponentLocation);
                var selectComponent = new SelectElement(selectComponentLoc);
                selectComponent.SelectByText("MRO Automated Regression Test Location");
                Driver.SendKeys(txtDescription,componentDescription);
                
                Driver.DirectClick(addButton);
                Driver.DirectClick(addComponentButton);
                logger.Log(Status.Info, $"Component added with description as ({componentDescription})");
            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to open add component page  : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }

            return new ROIFacilityRequestStatusPage(Driver, logger, Context);

        }

        public void AddComponentWithType(String type, string componentDescription)
        {
            try
            {
                Driver.SendKeys(selectComponentLocation, type);
                Driver.SendKeys(txtDescription, componentDescription);

                Driver.DirectClick(addButton);
                Driver.DirectClick(addComponentButton);
                logger.Log(Status.Info, $"Component added with description as ({componentDescription})");
            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to open add component page  : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }



        }
    }
}
