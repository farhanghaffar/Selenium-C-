using AventStack.ExtentReports;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MRO.ROI.Automation.Selenium;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.UI;
using System;

namespace MRO.ROI.Automation.Pages
{
    public class ROIAdminsFTPViewerPage
    {
        public RemoteWebDriver Driver;
        public ExtentTest logger;
        public TestContext Context;
        public ROIAdminsFTPViewerPage(RemoteWebDriver driver, ExtentTest _loger, TestContext _context)
        {
            Driver = driver;
            logger = _loger;
            Context = _context;
        }

        public const string ftpShippmentOption = "Test - FTP Shipments (62)";
        public By preconfiguredconnectionsdropdown = By.Id("mrocontent_lstConnections");
        public By browserbutton = By.Id("mrocontent_btnBrowse");
        public By NRSShipment = By.Id("mrocontent_dgServerContents_lnkFolder_5");

        /// <summary>
        /// To Config FTPShippments
        /// </summary>
        public ROIAdminsFTPViewerPage ToConfigFTPShipments()
        {
            try
            {
                SelectElement oSelect = new SelectElement(Driver.FindElementBy(preconfiguredconnectionsdropdown));
                oSelect.SelectByText(ftpShippmentOption);
                Driver.WaitUntilDOMLoaded();
                IWebElement browseButt = Driver.FindElementBy(browserbutton);
                browseButt.Click();               
            }

            catch (Exception ex)
            {

                throw new Exception($"Failed  to configure 62 FTP Shippments with deatils Message : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }

            return new ROIAdminsFTPViewerPage(Driver,logger,Context);

        }

        public void ClickNRSDirectory()
        {
            try
            {
                IWebElement nrsShipment = Driver.FindElementBy(NRSShipment);
                nrsShipment.Click();
            }
            catch (Exception ex)
            {

                throw new Exception($"Failed  to to click NRS Directory with deatils Message : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
            
        }
    }
}
