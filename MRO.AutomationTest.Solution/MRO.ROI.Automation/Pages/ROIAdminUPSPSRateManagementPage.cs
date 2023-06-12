using AventStack.ExtentReports;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MRO.ROI.Automation.Common.Navigation;
using MRO.ROI.Automation.Selenium;
using MRO.ROI.Automation.Utility;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Reflection;
using static MRO.ROI.Automation.Utility.IniFile;


namespace MRO.ROI.Automation.Pages
{
    public class ROIAdminUPSPSRateManagementPage
    {
        public RemoteWebDriver Driver;
        public ExtentTest logger;
        public TestContext Context;
        public ROIAdminUPSPSRateManagementPage(RemoteWebDriver driver, ExtentTest _loger, TestContext _context)
        {
            Driver = driver;
            logger = _loger;
            Context = _context;
        }
        public By manageZones = By.XPath("//*[text()='Manage Zones']");
        public By lookupRequestIdElement = By.XPath("//img[@title='Look up by Request ID']");
        public By zoneChart=By.XPath("//div[@id='mrocontent_GridZones']");
        public By zipCodes = By.XPath("//div[@id='mrocontent_GridZones_itemsHolder']//div[@id='mrocontent_GridZones_i0_c0']//div");
        public ROIAdminUPSPSRateManagementPage ClickManageZonesTab()
        {
            try
            {
                Driver.Click(manageZones);
            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to click manage zones tab with  details as Message : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }

            return new ROIAdminUPSPSRateManagementPage(Driver, logger, Context);
        }
        public ROIAdminRequestStatusPage ROIlookupByRequestId(string rqsID)
        {
            try
            {
                Driver.SwitchTo().DefaultContent();
                Driver.Click(lookupRequestIdElement);
                Driver.SwitchTo().Alert().SendKeys(rqsID);
                Driver.SwitchTo().Alert().Accept();
                Driver.SleepTheThread(5);
            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to navigated to request status page with details as Message : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
            return new ROIAdminRequestStatusPage(Driver, logger, Context);
        }
        public bool ZoneChartCheck()
        {
            try
            {
                
                
                    WebElementHelper helper = new WebElementHelper(Driver, logger, Context);
                    bool present = helper.IsElementPresent("//div[@id='mrocontent_GridZones']");
                    return present;
                
            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to check if zone chart is present or zone chart doesnot exist details as: {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }
        public bool ZipCodedigitcheck()
        {
            bool displayed = false;
            try
            {

                String code = Driver.GetText(zipCodes);
                int lengthofZipcode = code.Length;
                if (lengthofZipcode == 3)
                {
                    displayed = true;
                }

            }
            catch (Exception ex)
            {
                throw new Exception($"Failed  to assign Test Attorney Message : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
            return displayed;
        }
    }
    }