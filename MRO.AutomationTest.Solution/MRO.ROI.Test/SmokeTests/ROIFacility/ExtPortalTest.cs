using AventStack.ExtentReports;
using AventStack.ExtentReports.MarkupUtils;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MRO.ROI.Automation.Pages.ROIFacility;
using MRO.ROI.Automation.Selenium;
using MRO.ROI.Test.Utilities;
using System;
// Project 2
namespace MRO.ROI.Test.SmokeTests.ROIFacility
{
    [TestClass]
    public class ExtPortalsTest : ROITestBase
    {
        public ExtPortalsTest() : base(ROITestArea.ROIExternalRequesterPortal)
        {
        }
        [TestMethod]
        [TestCategory(ROITestCategory.BuildVerification), TestCategory(ROITestCategory.Regression)]
        public void External_Portal_Find_Request()
        {
            try
            {
				Driver.Wait(TimeSpan.FromSeconds(2));
				FacilityRequestStatusPage.closeRadNotification();
				Driver.Wait(TimeSpan.FromSeconds(2));

				Driver.logger = Driver.extent.CreateTest("BVT External Portal Base Test");
                LogNewRequestPage.ExtPortalFindARequest();
                LogNewRequestPage.facillogoutbutton();
                Driver.logger.Log(Status.Pass, "Successfully Logout From External Portal Facility.");
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
