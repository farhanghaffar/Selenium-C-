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




namespace MRO.ROI.Test.SmokeTests.ROIAdmin
{
    [TestClass]
    public class AdminAuditLogTest : ROITestBase
    {
        public AdminAuditLogTest() : base(ROITestArea.ROIAdmin)
        { 

        }
        [TestMethod]
        public void adminAuditLogTest()
        {
            MenuSelector.SelectRoiAdmin("User", "Admin List");

            // clear search criteria
            Driver.Instance.Value.FindElement(By.CssSelector("input#mrocontent_txtFirstName")).Clear();
            Driver.Instance.Value.FindElement(By.CssSelector("input#mrocontent_txtLastName")).Clear();
            Driver.Instance.Value.FindElement(By.CssSelector("input#mrocontent_cmdSearch")).Click();

            // check for unchecked "Active" box, click it, and confirm
            IWebElement checkbox = Driver.Instance.Value.FindElement(By.XPath("//input[contains(@id, 'mrocontent_dgROIAdamin_bActive')]"));
            var i = 1;
            bool tralse = true;
            while(tralse)
            {
                checkbox = Driver.Instance.Value.FindElement(By.XPath($"(//input[contains(@id, 'mrocontent_dgROIAdamin_bActive')])[{i}]"));
                if (!checkbox.Selected)
                {
                    tralse = false;
                }
                i++;
            }
            i--;
            String name = Driver.Instance.Value.FindElement(By.XPath($"(//tr[contains(@class, 'TableBody')][{i}]/td)[3]")).Text;
            String id = Driver.Instance.Value.FindElement(By.XPath($"(//tr[contains(@class, 'TableBody')][{i}]/td)")).Text;
            checkbox.Click();
            LogNewRequestPage.SendEnterKey();

            // check audit log on current date
            MenuSelector.SelectRoiAdmin("ROIAdmin", "Audit Log");
            Driver.Instance.Value.FindElement(By.CssSelector("input#mrocontent_cmdClear")).Click();
            Driver.Instance.Value.FindElement(By.CssSelector("input#mrocontent_txtDateA")).SendKeys(DateTime.Now.ToShortDateString());
            Driver.Instance.Value.FindElement(By.CssSelector("input#mrocontent_txtDateZ")).SendKeys(DateTime.Now.ToShortDateString());

            var dropDown = Driver.Instance.Value.FindElement(By.CssSelector("select#mrocontent_lstActions"));
            dropDown.Click();

            var actionItem = dropDown.FindElement(By.XPath("//select[contains(@id, 'mrocontent_lstActions')]/option[contains(text(), 'Admin User Activated')]"));
            actionItem.Click();
            Driver.Instance.Value.FindElement(By.CssSelector("input#mrocontent_cmdCreate")).Click();

            // check activation
            try
            {
                Driver.Instance.Value.FindElement(By.XPath($"//td[contains(text(), 'Activated : User ID #{id}')]"));
            }
            catch
            {
                Assert.IsTrue(false, $"No Activation Found, id: {id}");
            }

            // remove/uncheck "Active" box
            MenuSelector.SelectRoiAdmin("Users", "Admin List");
            Driver.Instance.Value.FindElement(By.XPath($"//tr[td[contains(text(), '{id}')]]/td//input[contains(@id, 'mrocontent_dgROIAdamin_bActive')]")).Click();
            LogNewRequestPage.SendEnterKey();

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
                if(max >1)
                {
                   Driver.Instance.Value.FindElement(By.XPath("//td[contains(text(), 'Page: 1 of (1')]/a")).Click();
                }
                Driver.Instance.Value.FindElement(By.XPath($"//td[contains(text(), 'DeActivated : User ID #{id}')]"));
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
