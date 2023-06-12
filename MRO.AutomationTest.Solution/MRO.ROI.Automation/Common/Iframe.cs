using AventStack.ExtentReports;
using MRO.ROI.Automation.Selenium;
using System;
using OpenQA.Selenium;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium.Remote;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace MRO.ROI.Automation.Common
{
    public class Iframe
    {
        public RemoteWebDriver Driver;
        public ExtentTest logger;
        public TestContext Context;
        public Iframe(RemoteWebDriver driver, ExtentTest _loger, TestContext _context)
        {
            Driver = driver;
            logger = _loger;
            Context = _context;
        }
        public By rdFrame = By.XPath("//iframe[starts-with(@id,'rdFrame')]");

        public void SwitchToRDFrame()
        {
            try
            {
                if (Driver.FindElementsByInTenSeconds(rdFrame).Count > 0)
                {

                    //Driver.SwitchTo().DefaultContent();
                    IWebElement frame = Driver.FindElementBy(rdFrame);
                    Driver.SwitchTo().Frame(frame);
                }
            }
            catch (Exception ex)
            {
                //throw new Exception($"Failed to switch to rd fame Message : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }

        public void SwitchToTotalRequestsFrame()
        {
            try
            {
                SwitchToRoiFrame();
                SwitchToRDFrame();
                IWebElement frame = Driver.FindElementBy(By.XPath("//iframe[@id='sr_dtTotalRequests_Row1']"));
                Driver.SwitchTo().Frame(frame);
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to switch to total requests fame Message : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }

        public By roiFrame = By.XPath("//iframe[@id='ROIFrame']");

        public void SwitchToRoiFrame()
        {
            try
            {
               
                Driver.SleepTheThread(1);
                Driver.SwitchTo().DefaultContent();
                if (Driver.FindElementsByInTenSeconds(roiFrame).Count > 0)
                {
                    IWebElement frame = Driver.FindElementBy(roiFrame);
                    Driver.SwitchTo().Frame(frame);
                    Driver.Wait(TimeSpan.FromSeconds(1));
                }
            }
            catch (Exception ex)
            {
                //throw new Exception($"Failed to switch to roi fame Message : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }
        public void switchToDefaut()
        {
            Driver.Wait(TimeSpan.FromSeconds(1));
            Driver.SwitchToDefaultContent();
        }

        public void SwitchToFrameByLocator(By by)
        {
                Driver.SleepTheThread(3);
                IWebElement frame = Driver.FindElementBy(by);
                Driver.SwitchTo().Frame(frame);
                Driver.Wait(TimeSpan.FromSeconds(1));
                logger.Log(Status.Info, "Switched to iframe");
        }

    }
}
