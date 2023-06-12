using AventStack.ExtentReports;
using AventStack.ExtentReports.MarkupUtils;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MRO.ROI.Automation.Pages;
using MRO.ROI.Automation.Pages.Common;
using MRO.ROI.Automation.Pages.ROIFacility;
using MRO.ROI.Automation.Selenium;
using MRO.ROI.Test.Utilities;
using System;

namespace MRO.ROI.Test.MRORegressionSuite

{
    [TestClass]
    public class Reg_IntPortalChgDlvyTest : ROITestBase
    {
        public Reg_IntPortalChgDlvyTest() : base(ROITestArea.ROIInternalRequesterPortal)
        {

        }
        [TestMethod]
        [TestCategory(ROITestCategory.BuildVerification), TestCategory(ROITestCategory.Regression)]
        public void intFacilityPortReqRecords()
        {
            try
            {
                Driver.logger = Driver.extent.CreateTest("Internal Portal Change Deliery Method");
                LogNewRequestPage.GoToLogNewInternalPortalRequestPage();
                Assert.IsTrue(LogNewRequestPage.IsAtLogNewRequestPage, "Failed to navigate to Create a Portal Request.");
                string CreatePortalRequestAssert = LogNewRequestPage.IntPortalCreateAPortalRequest();
                string rqsID = ROIAdminFacalitiesListPage.getRequestid();
				LogNewRequestPage.facillogoutbutton();
				LoginPage.LoginAs("seleniumautomation").WithPassword("Testauto1$").Login();
				Driver.logger.Log(Status.Pass, "Sucessfully logged a new Request.");
				ROIAdminFacalitiesListPage.faclookupidadmin(rqsID);
				Driver.Wait(TimeSpan.FromSeconds(5));
				LogNewRequestPage.IntPortalReqPreAuthChngDlvyIntToMRO();
                LoginPage.LogOut();
                Assert.IsTrue(LoginPage.IsAtLoginPage, "Failed to log out successfully.");
            }
            catch (Exception ex)
            {
                Driver.logger.Log(Status.Fail, "Test failed with exception");
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






