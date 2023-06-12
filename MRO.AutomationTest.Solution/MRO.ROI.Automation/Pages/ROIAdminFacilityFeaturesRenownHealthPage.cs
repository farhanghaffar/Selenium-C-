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

namespace MRO.ROI.Automation.Pages
{
   public class ROIAdminFacilityFeaturesRenownHealthPage
    {
        public RemoteWebDriver Driver;
        public ExtentTest logger;
        public TestContext Context;
        public ROIAdminFacilityFeaturesRenownHealthPage(RemoteWebDriver driver, ExtentTest _loger, TestContext _context)
        {
            Driver = driver;
            logger = _loger;
            Context = _context;
        }
        public By FTPUpload = By.XPath("//ul[@class='rtsUL']//a[@class='rtsLink']//span[contains(text(),'FTP Upload')]");
        public By FTPFeatutreId = By.XPath("//select[@name='mrocontent$lstROIFacilityFTPUploadFeatures']//option[@selected='selected']");
        public By Add = By.XPath("//input[@id='mrocontent_Add']");

        public void ClickFTPUpload()
        {
            try
            {
                IWebElement ftpUpload = Driver.FindElementBy(FTPUpload);
                ftpUpload.Click();
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to click FTP uploads with Exception details as  new Exception: {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");

            }

        }

        public string GetCurrentFeatureId()
        {
            string currentFeatureId = string.Empty;
            try
            {
                currentFeatureId = Driver.FindElementBy(FTPFeatutreId).Text;
            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to get current feature id Exception detail as  new Exception: {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");

            }
            return currentFeatureId;
        }

        public string GetFeatureIdAfterAdd()
        {
            string featureIdAfterAdd = string.Empty;
            try
            {
                IWebElement addButton = Driver.FindElementBy(Add);
                addButton.Click();

                featureIdAfterAdd = Driver.FindElementBy(FTPFeatutreId).Text;

            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to click add and get feature id Exception detail as  new Exception: {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");

            }
            return featureIdAfterAdd;
        }

    }
}
