using AventStack.ExtentReports;
using AventStack.ExtentReports.MarkupUtils;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MRO.ROI.Automation.Pages.Common;
using MRO.ROI.Automation.Pages.ROIFacility;
using MRO.ROI.Automation.Selenium;
using MRO.ROI.Test.Utilities;
using System;

namespace MRO.ROI.Test.SmokeTests.ROIFacility
{
    [TestClass]
    public class MRODeliveryTest : ROITestBase
    {
        public MRODeliveryTest() : base(ROITestArea.ROIFacility)
        {

        }
        /// <summary>
        /// Test Steps:
        /// Login to Test Facility.
        /// Create New MRO Delivery Request.
        /// Go to Request status page.
        /// Scan Patient Pages.
        /// Release te Request.
        /// Logout if Facility.
        /// </summary>
        [TestMethod]
        [TestCategory(ROITestCategory.BuildVerification), TestCategory(ROITestCategory.Regression)]
        public void Log_New_MRO_Delivery_Request()
        {
            try
            {
                Driver.logger = Driver.extent.CreateTest("BVT MRO Delivery Test");
                LogNewRequestPage.GoToLogNewRequestPage();
                //TODO:wait
                Assert.IsTrue(LogNewRequestPage.IsAtLogNewRequestPage, "Failed to navigate to Log New Request page.");
                Driver.logger.Info("Sucessfully Clicked On Log a New Request.");

                bool tab = LogNewRequestPage.ClickMRODeliveryTab();
                Assert.IsTrue(tab, "Failed to click on MRO delivery tab");
                Driver.logger.Info("Successfully clicked on MRO delivery tab");
                LogNewRequestPage.CreateNewMRODeliveryRequest();


                Assert.IsTrue(LogNewRequestPage.NewRequestCreated, "Failed to create new MRO delivery request");
                Driver.logger.Pass("Successfully created a new MRO delivery request");
                LogNewRequestPage.GoToRequestStatusPage();
                Assert.IsTrue(FacilityRequestStatusPage.IsAtRequestStatusPage, "Failed to navigate to facility request status page.");

                FacilityRequestStatusPage.ScanPatientPages(LogNewRequestPage.PatientFirstName, LogNewRequestPage.PatientLastName);
                Driver.logger.Pass("Successfully navigated to facility request status page");
                LogNewRequestPage.PatientNameValidation();
                //   FacilityRequestStatusPage.ScanPatientPages();
                FacilityRequestStatusPage.ReleaseRequest();
                Assert.IsTrue(FacilityRequestStatusPage.IsRequestReleased, "Failed to release request.");
                Driver.logger.Pass("Successfully released request");

                LoginPage.LogOut();
                Assert.IsTrue(LoginPage.IsAtLoginPage, "Failed to log out successfully.");
                Driver.logger.Pass("Successfully logged out");
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
