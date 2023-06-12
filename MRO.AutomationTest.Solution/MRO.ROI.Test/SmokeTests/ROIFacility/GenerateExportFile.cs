using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MRO.ROI.Automation.Common.Navigation;
using MRO.ROI.Automation.Pages;
using MRO.ROI.Automation.Pages.Common;
using MRO.ROI.Automation.Pages.ROIFacility;
using MRO.ROI.Automation.Selenium;
using MRO.ROI.Test.Utilities;
using OpenQA.Selenium;
using OpenQA.Selenium.IE;
using System.Threading;

namespace MRO.ROI.Test.SmokeTests.ROIFacility
{
    public class GenerateExportFile1 : ROITestBase
    {
        public GenerateExportFile1() : base(ROITestArea.ROIAdmin)
        {
        }

        [TestMethod]
        public void newTest()
        {
            MenuSelector.SelectRoiAdmin("Facilities", "Facility List");
        }
    }


    [TestClass]
    public class GenerateExportFile : ROITestBase
    {

        public GenerateExportFile() : base(ROITestArea.ROIAdmin)
        {
        }

        [TestMethod]
        public void Task313()
        {
            

            // click Facility List
            MenuSelector.SelectRoiAdmin("Facilities", "Facility List");

            // log into Yale facility
            try
            {
                Driver.Instance.Value.FindElement(By.XPath("//a[text()='Y']")).Click();
                Driver.Instance.Value.FindElement(By.XPath("//a[text()='Yale New Haven Health (TEST)']")).Click();
            }
            catch
            {
                Assert.IsTrue(false, "Failed to log into Yale Test Facility");
            }
            
            // navigate to elink tab
            try
            {
                MenuSelector.SelectRoiAdmin("Facilities", "Facility Features");
                Driver.Instance.Value.FindElement(By.XPath("//span[text()='eLink']")).Click();
            }
            catch
            {
                Assert.IsTrue(false, "Failed to navigate to elink tab");
            }
            
            // click Financial checkbox if not selected and click update
            try
            {
                var checkbox = Driver.Instance.Value.FindElement(By.CssSelector("input#mrocontent_cbEPICFeedbackFinancial"));
                if (!checkbox.Selected)
                {
                    checkbox.Click();
                }
                Driver.Instance.Value.FindElement(By.CssSelector("input#mrocontent_cmdUpdate")).Click();
            }
            catch
            {
                Assert.IsTrue(false, "Failed to click Financial checkbox if not selected and click update");
            }


            var options = new InternetExplorerOptions
            {
                IgnoreZoomLevel = true,
                EnableNativeEvents = true
            };
            string driverPath = TestContext != null ? TestContext.Properties["driverPath"].ToString() : @"C:\WebDrivers";
            InternetExplorerDriver ietest = new InternetExplorerDriver(driverPath, options);

            ietest.Navigate().GoToUrl("https://hqiisstg.mro.com/net4.0/Login/dev/login/LoginMenu.aspx");
            ietest.FindElement(By.XPath("//td[text()='ROI Facility']")).Click();
            ietest.FindElement(By.CssSelector("input#mrocontent_txtClientCode")).SendKeys("elinki");
            ietest.FindElement(By.CssSelector("input#mrocontent_cmdSelect")).Click();
            ietest.FindElement(By.CssSelector("input#mrocontent_txtUserName")).SendKeys("seleniumautomation");
            ietest.FindElement(By.CssSelector("input#mrocontent_txtPassword")).SendKeys("Testauto1$");
            Thread.Sleep((int)(3 * 1000));
            ietest.FindElement(By.CssSelector("input#mrocontent_cmdLogin")).Click();
            ietest.FindElement(By.XPath($"//td[text()='Reports']")).Click();
            ietest.FindElement(By.XPath($"//td[contains(text(),'Daily Shipments Report')]")).Click();

            // verify report button

            try
            {
                ietest.FindElement(By.CssSelector("input#mrocontent_btnGenerateExcelFile"));
                ietest.FindElement(By.CssSelector("input#mrocontent_cmdGenerateExportFile"));
            }
            catch
            {
                Assert.IsTrue(false, "Generate Export File button not found");
            }

            Driver.Instance.Value.FindElement(By.CssSelector("input#mrocontent_cbEPICFeedbackFinancial")).Click();
            Driver.Instance.Value.FindElement(By.CssSelector("input#mrocontent_cbEPICFeedbackFinancial")).Click();
            Driver.Instance.Value.FindElement(By.CssSelector("input#mrocontent_cmdUpdate")).Click();

            ietest.FindElement(By.CssSelector("body")).SendKeys(Keys.F5);
            Thread.Sleep((int)(3 * 1000));

            // test report button

            try
            {
                ietest.FindElement(By.CssSelector("input#mrocontent_btnGenerateExcelFile"));
                ietest.FindElement(By.CssSelector("input#mrocontent_cmdGenerateExportFile"));
                Assert.IsTrue(false, "Failed: Generate Export File button should disappear");
            }
            catch
            {
                
            }

            ietest.Close();
            //MenuSelector.SelectRoiAdmin("Profile", "Logout");
        }
    }

    
}

