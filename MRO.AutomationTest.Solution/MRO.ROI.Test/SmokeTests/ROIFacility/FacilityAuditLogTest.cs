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
using System.Linq;

namespace MRO.ROI.Test.SmokeTests.ROIFacility
{
    [TestClass]
    public class FacilityAuditLogTest : ROITestBase
    {
        public FacilityAuditLogTest() : base(ROITestArea.ROIAdmin)
        {
        }

        [TestMethod]
        public void facilityLogTest()
        {
            // click Facility List
            MenuSelector.SelectRoiAdmin("Facilities", "Facility List");

            // click computer icon by ROI Test Facility and go to List All Users
            Driver.Instance.Value.FindElement(By.XPath("//a[text()='All']")).Click();
            Driver.Instance.Value.FindElement(By.XPath("(//td[ ./a/text()='ROI Test Facility'])/a[1]/img")).Click();
            Driver.Instance.Value.FindElement(By.XPath("(//td[contains(text(), 'Users')])[2]")).Click();
            Driver.Instance.Value.FindElement(By.XPath("//td[contains(text(), 'List All Users')]")).Click();

            // pick inactive user and activate
            IWebElement checkbox = Driver.Instance.Value.FindElement(By.XPath("//input[contains(@id, 'mrocontent_dgUsers_bActive')]"));
            var i = 1;
            bool tralse = true;
            while (tralse)
            {
                checkbox = Driver.Instance.Value.FindElement(By.XPath($"(//input[contains(@id, 'mrocontent_dgUsers_bActive')])[{i}]"));
                if (!checkbox.Selected)
                {
                    tralse = false;
                }
                i++;
            }
            i--;
            String id = Driver.Instance.Value.FindElement(By.XPath($"(//tr[contains(@class, 'TableBody')])[{i}]/td[2]")).Text;
            checkbox.Click();
            LogNewRequestPage.SendEnterKey();

            // exit to admin
            Driver.Instance.Value.FindElement(By.CssSelector("img#mroheader_ctl02_ctl03_imgLogout")).Click();


           
            Driver.WaitUntilDOMLoaded();
            MenuSelector.SelectRoiAdmin("ROIAdmin", "Audit Log");

            // search changes today
            Driver.Instance.Value.FindElement(By.CssSelector("input#mrocontent_cmdClear")).Click();
            Driver.Instance.Value.FindElement(By.CssSelector("input#mrocontent_txtDateA")).SendKeys(DateTime.Now.ToShortDateString());
            Driver.Instance.Value.FindElement(By.CssSelector("input#mrocontent_txtDateZ")).SendKeys(DateTime.Now.ToShortDateString());

            var dropDown = Driver.Instance.Value.FindElement(By.CssSelector("select#mrocontent_lstActions"));
            dropDown.Click();

            var actionItem = dropDown.FindElement(By.XPath("//select[contains(@id, 'mrocontent_lstActions')]/option[text()='User Activated']"));
            actionItem.Click();
            Driver.Instance.Value.FindElement(By.CssSelector("input#mrocontent_cmdCreate")).Click();

            // verify activation
            try
            {
                var verify = Driver.Instance.Value.FindElement(By.XPath($"//tr[td[contains(text(), '{id}')]]/td[text()='User Activated']"));
            }
            catch
            {
                Assert.IsTrue(false, $"User {id} not activated");
            }

            // log in to facility and deactivate
            // click Facility List
            MenuSelector.SelectRoiAdmin("Facilities", "Facility List");

            // click computer icon by ROI Test Facility and go to List All Users
            Driver.Instance.Value.FindElement(By.XPath("(//td[ ./a/text()='ROI Test Facility'])/a[1]/img")).Click();
            Driver.Instance.Value.FindElement(By.XPath("(//td[contains(text(), 'Users')])[2]")).Click();
            Driver.Instance.Value.FindElement(By.XPath("//td[contains(text(), 'List All Users')]")).Click();

            // deactivate
            Driver.Instance.Value.FindElement(By.XPath($"//tr[td[contains(text(), '{id}')]]/td/input[contains(@id, 'mrocontent_dgUsers_bActive')]")).Click();
            LogNewRequestPage.SendEnterKey();

            // exit to admin again
            Driver.Instance.Value.FindElement(By.CssSelector("img#mroheader_ctl02_ctl03_imgLogout")).Click();

            // search audit log again
            MenuSelector.SelectRoiAdmin("ROIAdmin", "Audit Log");
            Driver.Instance.Value.FindElement(By.CssSelector("input#mrocontent_cmdClear")).Click();
            Driver.Instance.Value.FindElement(By.CssSelector("input#mrocontent_txtDateA")).SendKeys(DateTime.Now.ToShortDateString());
            Driver.Instance.Value.FindElement(By.CssSelector("input#mrocontent_txtDateZ")).SendKeys(DateTime.Now.ToShortDateString());
            Driver.Instance.Value.FindElement(By.CssSelector("input#mrocontent_cmdCreate")).Click();

            // check deactivation
            try
            {
                var max = Int32.Parse(Driver.Instance.Value.FindElement(By.XPath("//td[contains(text(), 'Page: 1 of (1')]/a")).Text);
                if (max > 1)
                {
                    Driver.Instance.Value.FindElement(By.XPath("//td[contains(text(), 'Page: 1 of (1')]/a")).Click();
                }
                try
                {
                    Driver.Instance.Value.FindElement(By.XPath($"//tr[td[contains(text(), '{id}')]]/td[contains(text(), 'DeActivated : User ID')]"));
                }
                catch
                {
                    Driver.Instance.Value.FindElement(By.XPath($"//td/a[text()='{(max-1)}']")).Click();
                    Driver.Instance.Value.FindElement(By.XPath($"//tr[td[contains(text(), '{id}')]]/td[contains(text(), 'DeActivated : User ID')]"));
                }
                
            }
            catch
            {
                Assert.IsTrue(false, "No DeActivation Found");
            }


            // logout
            MenuSelector.SelectRoiAdmin("Profile", "Logout");

        }
    }
}
