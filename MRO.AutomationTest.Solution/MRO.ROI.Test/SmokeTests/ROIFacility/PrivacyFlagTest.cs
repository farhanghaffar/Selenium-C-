using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MRO.ROI.Automation.Common.Navigation;
using MRO.ROI.Automation.Pages;
using MRO.ROI.Automation.Pages.Common;
using MRO.ROI.Automation.Pages.ROIFacility;
using MRO.ROI.Automation.Selenium;
using MRO.ROI.Test.Utilities;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using AventStack.ExtentReports;
using MRO.ROI.Automation.Utility;
using System.Diagnostics;
using System.IO;

namespace MRO.ROI.Test.SmokeTests.ROIFacility
{
    [TestClass]
    public class PrivacyFlagTest : ROITestBase
    {

        public PrivacyFlagTest() : base(ROITestArea.ROIAdmin)
        {

        }

        [TestMethod]
        public void PrivacyFlag1()
        {
            Driver.logger = Driver.extent.CreateTest("Task DA 280A && B"); //Creating a new test report

            // click Facility List
            MenuSelector.SelectRoiAdmin("Facilities", "Facility List");
            //Driver.Wait(TimeSpan.FromSeconds(2));

            // click computer icon by ROI Test Facility
            Driver.Instance.Value.FindElement(By.XPath("//a[text()='All']")).Click();
            Driver.Instance.Value.FindElement(By.XPath("(//td[ ./a/text()='ROI Test Facility'])/a[1]/img")).Click();

            // open menu and click Log a New Request
            MenuSelector.Select("ROI Requests", "Log a New Request");

            // make sure MRODelivery tab is selected
            bool tab = LogNewRequestPage.ClickMRODeliveryTab();
            Assert.IsTrue(tab, "Failed to click on MRO delivery tab");

            // fill out form for Delivery Request
            LogNewRequestPage.CreateNewMRODeliveryRequest();

            // save requestID for later
            string requestID = ROIAdminFacalitiesListPage.getRequestid();

            // go to status
            LogNewRequestPage.GoToRequestStatusPage();

            Driver.Instance.Value.FindElement(By.CssSelector("select#mrocontent_lstPrivacy")).Click();
            Driver.Instance.Value.FindElement(By.XPath("//option[text()='Mental Health']")).Click();

            //Driver.Wait(TimeSpan.FromSeconds(2));
            WebElementHelper.Click_Enter();
            Driver.Wait(TimeSpan.FromSeconds(2));
            Driver.Instance.Value.FindElement(By.CssSelector("input#mrocontent_cmdAuditTrail")).Click();

            // check date time and action
            var add = Driver.Instance.Value.FindElement(By.XPath("//tr[td[text()='Privacy Type Added']]/td")).Text;
            var time = Convert.ToString(DateTime.Now);
            Assert.IsTrue(add.Substring(0, 13) == time.Substring(0, 13), "Time does not match");
            

            //Driver.Wait(TimeSpan.FromSeconds(2));

            Driver.Instance.Value.FindElement(By.CssSelector("input#mrocontent_cmdCancel")).Click();



            Driver.Instance.Value.FindElement(By.CssSelector("select#mrocontent_lstPrivacy")).Click();
            Driver.Instance.Value.FindElement(By.XPath("//option[text()='Drugs/Alcohol']")).Click();

            //Driver.Wait(TimeSpan.FromSeconds(2));
            WebElementHelper.Click_Enter();
            Driver.Wait(TimeSpan.FromSeconds(2));
            Driver.Instance.Value.FindElement(By.CssSelector("input#mrocontent_cmdAuditTrail")).Click();

            //check date time and action
            var mod = Driver.Instance.Value.FindElement(By.XPath("//tr[td[text()='Privacy Type Modified']]/td")).Text;
            time = Convert.ToString(DateTime.Now);
            Assert.IsTrue(mod.Substring(0, 13) == time.Substring(0, 13), "Time does not match");

            // facility logout
            LogNewRequestPage.facillogoutbutton(); ROIAdminFacalitiesListPage.roilookupidadmin(requestID);

            Driver.Instance.Value.FindElement(By.CssSelector("input#mrocontent_cmdEventHistory")).Click();

            // verify
            try
            {
                Driver.Instance.Value.FindElement(By.XPath("//td[text()='Privacy Type Added']"));
                Driver.Instance.Value.FindElement(By.XPath("//td[text()='Privacy Type Modified']"));
            }
            catch
            {
                Assert.IsTrue(false, "log does not match actions taken");
            }

            //Driver.Wait(TimeSpan.FromSeconds(5));

            // 280B

            MenuSelector.SelectRoiAdmin("Facilities", "Facility List");
            //Driver.Wait(TimeSpan.FromSeconds(2));

            // click computer icon by ROI Test Facility
            Driver.Instance.Value.FindElement(By.XPath("//a[text()='All']")).Click();
            Driver.Instance.Value.FindElement(By.XPath("(//td[ ./a/text()='ROI Test Facility'])/a[1]/img")).Click();

            // open menu and click Log a New Request
            MenuSelector.Select("ROI Requests", "Find a Request");

            Driver.Instance.Value.FindElement(By.CssSelector("input#mrocontent_txtRequestID")).Clear();
            Driver.Instance.Value.FindElement(By.CssSelector("input#mrocontent_txtRequestID")).SendKeys(requestID);
            Driver.Instance.Value.FindElement(By.CssSelector("input#mrocontent_cmdSearch")).Click();

            Driver.Instance.Value.FindElement(By.XPath($"//tr[td[text()='{requestID}']]/td/a")).Click();

            Driver.Instance.Value.FindElement(By.CssSelector("select#mrocontent_lstPrivacy")).Click();
            Driver.Instance.Value.FindElement(By.XPath("//option[text()='Drugs/Alcohol']")).Click();

            //Driver.Wait(TimeSpan.FromSeconds(2));
            WebElementHelper.Click_Enter();
            Driver.Wait(TimeSpan.FromSeconds(2));
            Driver.Instance.Value.FindElement(By.CssSelector("input#mrocontent_cmdAuditTrail")).Click();

            mod = Driver.Instance.Value.FindElement(By.XPath("//tr[td[text()='Privacy Type Modified']]/td")).Text;
            time = Convert.ToString(DateTime.Now);
            Assert.IsTrue(mod.Substring(0, 12) == time.Substring(0, 12), $"Time does not match: {mod.Substring(0, 12)} {time.Substring(0, 12)}");


            LogNewRequestPage.facillogoutbutton();
            MenuSelector.SelectRoiAdmin("ROIAdmin", "Audit Log");

            Driver.Instance.Value.FindElement(By.CssSelector("input#mrocontent_cmdClear")).Click();
            Driver.Instance.Value.FindElement(By.CssSelector("input#mrocontent_txtDateA")).SendKeys(DateTime.Now.ToShortDateString());
            Driver.Instance.Value.FindElement(By.CssSelector("input#mrocontent_txtDateZ")).SendKeys(DateTime.Now.ToShortDateString());
            Driver.Instance.Value.FindElement(By.CssSelector("input#mrocontent_cmdCreate")).Click();

            // verify excel export

            //Driver.Wait(TimeSpan.FromSeconds(10));

        }
    }
}
