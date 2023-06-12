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
    public class FacilityTest : ROITestBase
    {
        public FacilityTest() : base(ROITestArea.ROIFacility)
        {

        }

        [TestMethod]
        [TestCategory("SAMPLEVERIFICATION")]
        public void Can_Login_To_Test_Facility()
        {
            try
            {
                Driver.logger = Driver.extent.CreateTest("Facility Login Test"); //Creating a new test report
                #region Facility Login Test
                Assert.IsTrue(LoginPage.IsAtTestFacility, "Failed to login to the facility");
                Driver.logger.Log(Status.Pass, "Logged in to facility."); //Logging pass
                #endregion
                #region Create MRO Delivery Request
                LogNewRequestPage.GoToLogNewRequestPage();
                bool tab1 = LogNewRequestPage.ClickMRODeliveryTab();
                LogNewRequestPage.CreateNewMRODeliveryRequest();
                LogNewRequestPage.GoToRequestStatusPage();
                FacilityRequestStatusPage.ScanPatientPages(LogNewRequestPage.PatientFirstName, LogNewRequestPage.PatientLastName);
                FacilityRequestStatusPage.ReleaseRequest();
                #endregion
                #region Create Onsite Delivery Request
                LogNewRequestPage.GoToLogNewRequestPage();
                bool tab2 = LogNewRequestPage.ClickOnSiteDeliveryTab();
                LogNewRequestPage.CreateNewOnsiteDeliveryRequest();
                LogNewRequestPage.GoToRequestStatusPage();
                FacilityRequestStatusPage.ScanPatientPages(LogNewRequestPage.PatientFirstName, LogNewRequestPage.PatientLastName);
                FacilityRequestStatusPage.DeliverMedicalRecordOnsite();
                #endregion
                #region Create Internal Portal Delivery Request
                LogNewRequestPage.GoToLogNewRequestPage();
                bool tab3 = LogNewRequestPage.ClickInternalPortalTab();
                LogNewRequestPage.CreateNewInternalPortalRequest();
                LogNewRequestPage.GoToRequestStatusPage();
                FacilityRequestStatusPage.ScanPatientPages(LogNewRequestPage.PatientFirstName, LogNewRequestPage.PatientLastName);
                FacilityRequestStatusPage.acceptalert1();
                #endregion

                //throw new Exception("testing exceptiom");
                LoginPage.LogOut();
                Assert.IsTrue(LoginPage.IsAtLoginPage, "Failed to log out.");
                Driver.logger.Log(Status.Pass, "Successully Logged Out From The Facility."); //Logging pass
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
