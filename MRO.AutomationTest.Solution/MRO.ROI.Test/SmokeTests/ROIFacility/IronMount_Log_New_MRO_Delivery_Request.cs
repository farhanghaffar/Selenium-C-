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
    public class Iron_Mountain_Log_New_MRO_Delivery_Request : ROITestBase
    {
        public Iron_Mountain_Log_New_MRO_Delivery_Request() : base(ROITestArea.IronMountainROIFacility)
        {
        }

        [TestMethod]
        // [TestCategory(ROITestCategory.BuildVerification)]
        [TestCategory(ROITestCategory.Regression)]
        public void Reg_Iron_Mountain_Log_New_MRO_Delivery_Request()
        {
            try
            {
                Driver.logger = Driver.extent.CreateTest("BVT Iron Mountain MRO Delivery Test");
                Driver.logger.Log(Status.Pass, "BVT Iron Mountain MRO Delivery Test");
                LogNewRequestPage.GoToLogNewRequestPage();
                Assert.IsTrue(LogNewRequestPage.IsAtLogNewRequestPage, "Failed to navigate to Log New Request page.");
                bool tab = LogNewRequestPage.ClickMRODeliveryTab();
                Assert.IsTrue(tab, "Failed to click on MRO delivery tab");
                LogNewRequestPage.IronMountainCreateNewMRODeliveryRequest();
                Assert.IsTrue(LogNewRequestPage.NewRequestCreated, "Failed to create new MRO delivery request");
                LogNewRequestPage.GoToRequestStatusPage();
                Assert.IsTrue(FacilityRequestStatusPage.IsAtRequestStatusPage, "Failed to navigate to facility request status page.");
                Driver.logger.Pass("Successfully navigated to facility request status page");
                LogNewRequestPage.PatientNameValidation();
                FacilityRequestStatusPage.ScanPatientPages();
                FacilityRequestStatusPage.ReleaseRequest();
                //    LogNewRequestPage.mroToOnsite();
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
