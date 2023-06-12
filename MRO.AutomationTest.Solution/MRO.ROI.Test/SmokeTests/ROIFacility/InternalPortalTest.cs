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
    public class InternalPortalTest : ROITestBase
    {
        public InternalPortalTest() : base(ROITestArea.ROIFacility)
        {

        }

        [TestMethod]
        [TestCategory(ROITestCategory.BuildVerification), TestCategory(ROITestCategory.Regression)]
        public void Log_New_InternalPortal_Delivery_Request()
        {
            try
            {
                Driver.logger = Driver.extent.CreateTest("BVT InternalPortalTest");
                LogNewRequestPage.GoToLogNewRequestPage();
                Assert.IsTrue(LogNewRequestPage.IsAtLogNewRequestPage, "Failed to navigate to Log New Request page.");
                Driver.logger.Info("Sucessfully Clicked On Log a New Request.");


                bool tab = LogNewRequestPage.ClickInternalPortalTab();
                Assert.IsTrue(tab, "Failed to click on Internal Portal delivery tab");
                Driver.logger.Pass("Successfully clicked on Internal Portal tab");
                LogNewRequestPage.CreateNewInternalPortalRequest();  //tou

                Assert.IsTrue(LogNewRequestPage.NewRequestCreated, "Failed to create new InternalPortal request");
                Driver.logger.Pass("Successfully created a new Internal Portal request");
                LogNewRequestPage.GoToRequestStatusPage();
                Assert.IsTrue(FacilityRequestStatusPage.IsAtRequestStatusPage, "Failed to navigate to facility request status page.");
                Driver.logger.Pass("Successfully navigated to facility request status page");
                LogNewRequestPage.PatientNameValidation();
                
                FacilityRequestStatusPage.ScanPatientPages(LogNewRequestPage.PatientFirstName, LogNewRequestPage.PatientLastName);

                //  Assert.IsTrue(FacilityRequestStatusPage.IsRequestReleased, "Failed to release request.");
                // FacilityRequestStatusPage.SendEnterKey();
                FacilityRequestStatusPage.acceptalert1();

                LoginPage.LogOut();
                Assert.IsTrue(LoginPage.IsAtLoginPage, "Failed to log out successfully.");
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

