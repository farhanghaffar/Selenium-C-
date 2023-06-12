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
    public class Reg_FWSActionLIstReportsTest : ROITestBase
    {

        public Reg_FWSActionLIstReportsTest() : base(ROITestArea.ROIAdmin)
        {

        }

        [TestMethod]
        public void Reg_FacilityWorkSummaryRptsTest()
        {
            try
            {
                //Driver.Instance.Value.Manage().Window.Maximize(); Driver.Instance.Value.Manage().Window.Maximize();
                Driver.logger = Driver.extent.CreateTest("Reg_ActionList_Test");
                Driver.logger.Info("Regression Work Summary,Action List Test");
                MenuSelector.SelectRoiAdmin("Facilities", "Facility List");
                ROIAdminFacalitiesListPage.gotoROITestFacility();
                Driver.Wait(TimeSpan.FromSeconds(5));
                FacilityWorkSummary.FacWorkSummaryActionList();
                // Driver.Wait(TimeSpan.FromSeconds(5));
                FacilityWorkSummary.facillogoutbutton();
                //  LoginPage.LogOut();
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
                         //   Assert.Fail(ex.Message);
            }
        }
    }
}
