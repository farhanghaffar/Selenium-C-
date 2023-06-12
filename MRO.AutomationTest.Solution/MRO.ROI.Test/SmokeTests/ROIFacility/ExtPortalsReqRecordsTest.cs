using AventStack.ExtentReports;
using AventStack.ExtentReports.MarkupUtils;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MRO.ROI.Automation.Pages;
using MRO.ROI.Automation.Pages.Common;
using MRO.ROI.Automation.Pages.ROIFacility;
using MRO.ROI.Automation.Selenium;
using MRO.ROI.Test.Utilities;
using System;

namespace MRO.ROI.Test.SmokeTests.ROIFacility
{
    [TestClass]
    public class ExtPortalsReqRecordsTest : ROITestBase
    {
        public ExtPortalsReqRecordsTest() : base(ROITestArea.ROIExternalRequesterPortal)
        {
        }

        [TestMethod]
        [TestCategory(ROITestCategory.BuildVerification), TestCategory(ROITestCategory.Regression)]
        public void ExtPortReqRecords()
        {
            try
            {
				Driver.Wait(TimeSpan.FromSeconds(2));
				FacilityRequestStatusPage.closeRadNotification();
				Driver.Wait(TimeSpan.FromSeconds(2));

				Driver.logger = Driver.extent.CreateTest("BVT ExtPortalsReqRecordsTest");
                Driver.logger.Log(Status.Info, "Starting test");

				//logger = extent.CreateTest("External Portal Requester REcords Test");
				string CreatReqAssert = LogNewRequestPage.ExtPortalRequestRecordst();
				//Assert.AreEqual("Create Request", CreatReqAssert, "Failed to navigate to Create Request page.");
				//Driver.logger.Log(Status.Pass, "Success to navigate to Create Request page.");
				string reqID = LogNewRequestPage.ExtPortalRequestRecordst2();
                LogNewRequestPage.facillogoutbutton();
                LoginPage.GoToROIAdminLoginPage();
                LoginPage.LoginAs("seleniumautomation").WithPassword("Testauto1$").Login();
				
                ROIAdminFacalitiesListPage.roilookupidadmin(reqID);
                ROIAdminFacalitiesListPage.drilltofacility();
                LogNewRequestPage.PatientNameValidation();
                //   FacilityRequestStatusPage.ScanPatientPages();
                FacilityRequestStatusPage.ScanPatientPages(LogNewRequestPage.PatientFirstName, LogNewRequestPage.PatientLastName);
                FacilityRequestStatusPage.ReleaseRequest();
                Assert.IsTrue(FacilityRequestStatusPage.IsRequestReleased, "Failed to release request.");
                Driver.logger.Log(Status.Pass, "Successfully release request.");
                LoginPage.LogOut();
                //  Assert.IsTrue(LoginPage.IsAtLoginPage, "Failed to log out successfully.");
                // ROIAdminFacalitiesListPage.roilookupidadmin(reqID);
                ROIAdminFacalitiesListPage.linkreqeusted();
				
                RequestStatus.roiadmlogout();
                Assert.IsTrue(LoginPage.IsAtLoginPage, "Failed to log out successfully.");
                Driver.logger.Log(Status.Pass, "logged out successfully.");
                // TODO Need to implement admin logout page.
                //  RequestStatus.roiAssignReq();
                Driver.logger.Log(Status.Pass, "Logged out of facility.");
            }
            catch (Exception ex)
            {
                Driver.Wait(TimeSpan.FromSeconds(5));
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
