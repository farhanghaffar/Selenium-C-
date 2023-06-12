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
    public class Iron_Mountain_OnSiteDeliveryRequestPayByCheck : ROITestBase
    {
        public Iron_Mountain_OnSiteDeliveryRequestPayByCheck() : base(ROITestArea.IronMountainROIFacility)
        {
        }

        [TestMethod]
        // [TestCategory(ROITestCategory.BuildVerification)]
        [TestCategory(ROITestCategory.Regression)]
        public void Reg_IronMountain_OnsiteDelivery_Request_PayByCheck()
        {
            try
            {
                Driver.logger = Driver.extent.CreateTest(" Iron Mountain Onsite Delivery Request Test");
                Driver.logger.Log(Status.Info, "Iron Mountain Onsite Delivery Request Pay by check Test");
                LogNewRequestPage.GoToLogNewRequestPage();
                Assert.IsTrue(LogNewRequestPage.IsAtLogNewRequestPage, "Failed to navigate to Log New Request page.");
                Driver.logger.Info("Sucessfully Clicked On Log a New Request.");

                bool tab = LogNewRequestPage.ClickOnSiteDeliveryTab();
                Assert.IsTrue(tab, "Failed to click on On-Site Delivery tab");
                Driver.logger.Pass("Successfully clicked on On-Site Delivery tab");

                LogNewRequestPage.IronMountainCreateNewOnsiteDeliveryRequest();
                Assert.IsTrue(LogNewRequestPage.NewRequestCreated, "Failed to create new On-Site Delivery");
                Driver.logger.Pass("Successfully created a On-Site Delivery request");
                // LogNewRequestPage.GoToRequestStatusPage();
                Driver.logger.Log(Status.Info, "Fill New Iron Mountain Onsite Delivery Request.");
                Assert.IsTrue(FacilityRequestStatusPage.IsAtRequestStatusPage, "Failed to navigate to facility request status page.");
                //   FacilityRequestStatusPage.ScanPatientPages(LogNewRequestPage.PatientFirstName, LogNewRequestPage.PatientLastName);
                //   LogNewRequestPage.PatientNameValidation();
                Driver.logger.Pass("Successfully navigated to facility request status page");
                // Driver.logger.Log(Status.Info, "Fill New MRO Delivery Request.");
                Assert.IsTrue(FacilityRequestStatusPage.IsAtRequestStatusPage, "Failed to navigate to facility request status page.");
                //   FacilityRequestStatusPage.ScanPatientPages();
                Driver.Wait(TimeSpan.FromSeconds(5));
                FacilityRequestStatusPage.IronMountainDeliverMedicalRecordOnsite();
                Assert.IsTrue(FacilityRequestStatusPage.IsDocumentsDelivered, "Failed to deliver documents");

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