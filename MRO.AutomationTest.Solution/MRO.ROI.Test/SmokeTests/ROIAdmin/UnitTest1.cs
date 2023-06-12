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
    public class UnitTest1 : ROITestBase
    {
        public UnitTest1() : base(ROITestArea.ROIAdmin)
        { 

        }
        [TestMethod]
        public void someTest()
        {
            MenuSelector.SelectRoiAdmin("User", "Admin List");
            Driver.Instance.FindElement(By.CssSelector("input#mrocontent_txtFirstName")).Clear();
            Driver.Instance.FindElement(By.CssSelector("input#mrocontent_txtLastName")).Clear();
            Driver.Instance.FindElement(By.CssSelector("input#mrocontent_cmdSearch")).Click();

            IWebElement checkbox = Driver.Instance.FindElement(By.XPath("//input[contains(@id, 'mrocontent_dgROIAdamin_bActive')]"));
            var i = 1;
            bool tralse = true;

            while(tralse)
            {
                checkbox = Driver.Instance.FindElement(By.XPath($"(//input[contains(@id, 'mrocontent_dgROIAdamin_bActive')])[{i}]"));
                if (!checkbox.Selected)
                {
                    tralse = false;
                }
                i++;
            }
            i--;
            String id = Driver.Instance.FindElement(By.XPath($"(//tr[contains(@class, 'TableBody')]/td)[{i}]")).Text;
            checkbox.Click();

            LogNewRequestPage.SendEnterKey();
            MenuSelector.SelectRoiAdmin("ROIAdmin", "Audit Log");
            Driver.Instance.FindElement(By.CssSelector("input#mrocontent_cmdClear")).Click();
            Driver.Instance.FindElement(By.CssSelector("input#mrocontent_txtDateA")).SendKeys(DateTime.Now.ToShortDateString());
            Driver.Instance.FindElement(By.CssSelector("input#mrocontent_txtDateZ")).SendKeys(DateTime.Now.ToShortDateString());

            var dropDown = Driver.Instance.FindElement(By.CssSelector("select#mrocontent_lstActions"));
            dropDown.Click();
           
            Driver.Wait(TimeSpan.FromSeconds(2));

            var actionItem = dropDown.FindElement(By.XPath("//select[contains(@id, 'mrocontent_lstActions')]/option[contains(text(), 'Admin User Activated')]"));
            actionItem.Click();

            Driver.Instance.FindElement(By.CssSelector("input#mrocontent_cmdCreate")).Click();

            MenuSelector.SelectRoiAdmin("User", "Admin List");
            Driver.Instance.FindElement(By.XPath($"//tr[td[contains(text(), '{id}')]]/td//input[contains(@id, 'mrocontent_dgROIAdamin_bActive')]")).Click();
            LogNewRequestPage.SendEnterKey();

            MenuSelector.SelectRoiAdmin("ROIAdmin", "Audit Log");

            Driver.Instance.FindElement(By.CssSelector("input#mrocontent_cmdClear")).Click();
            Driver.Instance.FindElement(By.CssSelector("input#mrocontent_txtDateA")).SendKeys(DateTime.Now.ToShortDateString());
            Driver.Instance.FindElement(By.CssSelector("input#mrocontent_txtDateZ")).SendKeys(DateTime.Now.ToShortDateString());
            Driver.Instance.FindElement(By.CssSelector("input#mrocontent_cmdCreate")).Click();

            Driver.Wait(TimeSpan.FromSeconds(5));
            RequestStatus.roiadmlogout();
        }
    }
}
