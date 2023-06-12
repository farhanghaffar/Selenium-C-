using AventStack.ExtentReports;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MRO.ROI.Automation.Common.Navigation;
using MRO.ROI.Automation.Selenium;
using MRO.ROI.Test.Utilities;
using OpenQA.Selenium;
using System;
using System.Drawing;
using WindowsInput;
using WindowsInput.Native;

namespace MRO.ROI.Test.SmokeTests.ROIAdmin
{
    [TestClass]
    public class Admin_Reports_Pending : ROITestBase
    {
        public Admin_Reports_Pending() : base(ROITestArea.ROIAdmin)
        {

        }


        [TestMethod]
        [TestCategory(ROITestCategory.Regression)]
        // Converted manual test case 740-ROI-Admin-->Request Status-->Pay By to automated.
        public void Reg_Reports_Pending()
        {
            Driver.logger = Driver.extent.CreateTest("Create Admin Reports Pending Test");
            Driver.logger.Log(Status.Info, "Converted Manual Admin Reports Pending.");
            MenuSelector.SelectRoiAdmin("Reports", "Pending");
            Driver.Wait(TimeSpan.FromSeconds(5));
            Driver.Instance.Value.FindElement(By.Id("mrocontent_ctlPending_txtDateA")).Clear();
            Driver.Instance.Value.FindElement(By.Id("mrocontent_ctlPending_txtDateA")).SendKeys("05/09/2019");
            Driver.Instance.Value.FindElement(By.Id("mrocontent_ctlPending_txtDateZ")).Clear();
            Driver.Instance.Value.FindElement(By.Id("mrocontent_ctlPending_txtDateZ")).SendKeys("05/09/2019");
            Driver.Wait(TimeSpan.FromSeconds(5));
            Driver.Instance.Value.FindElement(By.Id("mrocontent_ctlPending_cmdCreate")).Click();
            Driver.Wait(TimeSpan.FromSeconds(5));
            InputSimulator simulator = new InputSimulator();
            //Driver.Instance.Value.FindElement(By.XPath("//*[@alt='Export to Excel']")).Click();
            //"//*[@id=\"mrocontent_dgFacilities\"]/tbody/tr[2]/td[3]/a[1]/img";
            //Driver.Wait(TimeSpan.FromSeconds(3));

            Point coordinates = Driver.Instance.Value.FindElement(By.XPath("//*[@alt='Export to Excel']")).Location;
            Console.WriteLine("Co-ordinates" + coordinates + coordinates.X + " " + coordinates.Y);

            simulator.Mouse.MoveMouseTo(coordinates.X, coordinates.Y);



            simulator.Mouse.LeftButtonClick();
            //simulator.Keyboard.KeyPress(VirtualKeyCode.RETURN);
            //  simulator.Keyboard.KeyRelease(VirtualKeyCode.RETURN);
            Driver.Wait(TimeSpan.FromSeconds(2));
            simulator.Keyboard.ModifiedKeyStroke(VirtualKeyCode.MENU, VirtualKeyCode.VK_S);




        }


    }
}

