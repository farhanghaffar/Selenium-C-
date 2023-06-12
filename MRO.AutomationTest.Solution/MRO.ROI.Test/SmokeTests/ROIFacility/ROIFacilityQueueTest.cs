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


namespace MRO.ROI.Test.SmokeTests.ROIFacility
{
    [TestClass]
    public class ROIFacilityQueueTest : ROITestBase

    {


        public ROIFacilityQueueTest() : base(ROITestArea.ROIAdmin)
        {

        }

        [TestMethod]
        public void TestMethod1()
        {
            Driver.logger = Driver.extent.CreateTest("Task 322A QC Queue"); //Creating a new test report

            // click Facility List
            MenuSelector.SelectRoiAdmin("Facilities", "Facility List");

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

            // click Scan and enter test documents
            Driver.Instance.Value.FindElement(By.CssSelector("input#mrocontent_cmdScan_")).Click();
            ScannerUtil.ScanDocuments();

            // click yes
            Driver.Instance.Value.FindElement(By.XPath("//span[contains(text(), 'Yes')]")).Click();

            // verify request release
            LogNewRequestPage.ReleaseRequest();
            WebElementHelper.Click_Enter();

            // facility logout
            LogNewRequestPage.facillogoutbutton(); ROIAdminFacalitiesListPage.roilookupidadmin(requestID);

            // admin search and navigate to correct page
            RequestStatus.roiAssignReq();
            RequestStatus.roiAdminSearch();
            RequestStatus.roiSaveDonebtn();
            MenuSelector.SelectRoiAdmin("ROIAdmin", "QC Queue");
            Driver.Instance.Value.FindElement(By.CssSelector("input#mrocontent_cmdSearch")).Click();

            // loop through pages and check for requestID
            bool exists = PaginationUtil.SearchPaginatedList(requestID);
            Assert.IsFalse(exists, "Failed: RequestID appears in QC Queue prior to one hour after entered");
            

            // logout
            RequestStatus.roiadmlogout();
        } 
    }
}
