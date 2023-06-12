using AventStack.ExtentReports;
using AventStack.ExtentReports.MarkupUtils;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MRO.ROI.Automation.Pages.Common;
using MRO.ROI.Automation.Pages.ROIFacility;
using MRO.ROI.Automation.Selenium;
using MRO.ROI.Test.Utilities;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;

namespace MRO.ROI.Test.SmokeTests.ROIFacility
{
    [TestClass]
    public class ChangeDelMethodMROtoOnsiteTest : ROITestBase
    {
        public ChangeDelMethodMROtoOnsiteTest() : base(ROITestArea.ROIFacility)
        {
        }
        /// <summary>
        /// Test Steps:
        /// Login to Test Facility.
        /// Create New MRO Delivery Request.
        /// Go to Request status page.
        /// Validate Patient Name.
        /// Change Delivery Type from MRO to ONSITE
        /// Logout of Facility.
        /// </summary>
        [TestMethod]
        [TestCategory(ROITestCategory.BuildVerification), TestCategory(ROITestCategory.Regression)]
        public void Can_Log_New_MRO_Delivery_Request()
        {
            try
            {
                Driver.logger = Driver.extent.CreateTest("BVT Change MRO Delivery Method Test");
                Driver.logger.Log(Status.Pass, "Start Change MRO Delivery Method Test");
                LogNewRequestPage.GoToLogNewRequestPage();
                Assert.IsTrue(LogNewRequestPage.IsAtLogNewRequestPage, "Failed to navigate to Log New Request page.");
                bool tab = LogNewRequestPage.ClickMRODeliveryTab();
                Assert.IsTrue(tab, "Failed to click on MRO delivery tab");
                LogNewRequestPage.CreateNewMRODeliveryRequest();
                Assert.IsTrue(LogNewRequestPage.NewRequestCreated, "Failed to create new MRO delivery request");
                LogNewRequestPage.GoToRequestStatusPage();
                Assert.IsTrue(FacilityRequestStatusPage.IsAtRequestStatusPage, "Failed to navigate to facility request status page.");
                Driver.logger.Pass("Successfully navigated to facility request status page");
                LogNewRequestPage.PatientNameValidation();
                // LogNewRequestPage.mroToOnsite();
                // make changes to test action list user story: 201
                SelectElement objSelect1 = new SelectElement(Driver.Instance.Value.FindElement(By.Id("mrocontent_lstActions")));
                objSelect1.SelectByText("Please Process ASAP, requester needs records");
                Driver.logger.Pass("Please Process ASAP, requester needs records");
                Driver.Instance.Value.FindElement(By.Id(PageElements.LogNewRequestPage.sendmsgmrotxt_id)).SendKeys("Test Message to MRO");
                Driver.logger.Pass("Successfully send a test message to MRO");

                LoginPage.LogOut();
                Assert.IsTrue(LoginPage.IsAtLoginPage, "Failed to log out successfully.");
                Driver.logger.Log(Status.Pass, "Sucessfully logged out.");
            }
            catch (Exception ex)
            {
                Driver.logger.Log(Status.Fail, "Test failed with exception"); //Logging fail
                Driver.logger.Log(Status.Error, MarkupHelper.CreateTable(
                    new string[,]
                    {
                        {"Exception", ex.Message },
                        {"StackTrace", ex.StackTrace }
                    })); //Logging Error in a tabular format
                Assert.Fail(ex.Message);
            }
        }
    }
}
