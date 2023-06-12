using AventStack.ExtentReports;
using AventStack.ExtentReports.MarkupUtils;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MRO.ROI.Automation.Common.Navigation;
using MRO.ROI.Automation.Pages;
using MRO.ROI.Automation.Pages.ROIFacility;
using MRO.ROI.Automation.Selenium;
using MRO.ROI.Test.Utilities;
using System;

namespace MRO.ROI.Test.MRORegressionSuite
{
    [TestClass]
    public class Reg_ReqPreAuthChngDlvyTest : ROITestBase
    {

        public Reg_ReqPreAuthChngDlvyTest() : base(ROITestArea.ROIAdmin)
        {

        }

        [TestMethod]
        public void ROI_Admin_CreateNew_RqstPreAuthorization()
        {
            try
            {

                Driver.logger = Driver.extent.CreateTest("Reg_Request_Pre_Authorization_Test");
                MenuSelector.SelectRoiAdmin("Facilities", "Facility List");
                ROIAdminFacalitiesListPage.gotoROITestFacility();
                MenuSelector.Select("ROI Requests", "Log a New Request"); LogNewRequestPage.CreateRequest();
                Automation.Utility.ScannerUtil.ScanDocuments();
                string requestID = ROIAdminFacalitiesListPage.getRequestid();
                Driver.logger.Log(Status.Info, "Request ID: " + requestID);
                LogNewRequestPage.RequestStatus(); LogNewRequestPage.PatientNameValidation();
                WebElementHelper.ScrollIntoView("mrocontent_cmdPreAuth", FindElementBy.Id);
                LogNewRequestPage.RequestPreAuthorization();
                FacilityRequestStatusPage.ReqPreAuthorizationBtn();
                FacilityRequestStatusPage.ScanPatientPages();
                FacilityRequestStatusPage.ReleaseRequest();
                //  Assert.IsTrue(FacilityRequestStatusPage.IsRequestReleased, "Failed to release request.");
                LogNewRequestPage.ReqPreAuthChngDlvymroToOnsite();
                LogNewRequestPage.Dlvfaxonsite();
                LogNewRequestPage.facillogoutbutton();
                //  Assert.IsTrue(LoginPage.IsAtLoginPage, "Failed to log out successfully.");
                Driver.logger.Log(Status.Pass, "Sucessfully Logged Out From Facility.");


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
