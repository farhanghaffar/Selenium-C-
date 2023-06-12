using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using AventStack.ExtentReports.Reporter.Configuration;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MRO.ROI.Automation.Selenium
{
    public class ExtentHelper
    {

        public static void TakeScreenShot(RemoteWebDriver driver, ExtentTest logger, string testName, string path)
        {
            try
            {
                Screenshot ss = ((ITakesScreenshot)driver).GetScreenshot();
                string date = DateTime.Now.ToString("yyyy-MM-dd-hh-mm-ss-tt");
                ss.SaveAsFile(path);               
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed  to take screenshot whose exception message is: {ex.Message} {Environment.NewLine} Whose stackTrace is: {ex.StackTrace}");
            }
        }

    }
}
