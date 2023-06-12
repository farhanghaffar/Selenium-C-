using AventStack.ExtentReports;
using AventStack.ExtentReports.MarkupUtils;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MRO.ROI.Automation.Pages.Common;
using MRO.ROI.Automation.Pages.ROIFacility;
using MRO.ROI.Automation.Selenium;
using MRO.ROI.Test.Utilities;
using System;

namespace MRO.ROI.Test.SmokeTests.ROIRequesterPortal
{
    [TestClass]
    public class InternalFacilityPortalTest : ROITestBase
    {
        public InternalFacilityPortalTest() : base(ROITestArea.ROIInternalRequesterPortal)
        {

        }

        [TestMethod]
        [TestCategory(ROITestCategory.BuildVerification), TestCategory(ROITestCategory.Regression)]
        public void Log_New_Internal_Portal_Request()
        {
            try
            {
                Driver.logger = Driver.extent.CreateTest("Internal Portal Request Test");
                LogNewRequestPage.GoToLogNewInternalPortalRequestPage();
                Assert.IsTrue(LogNewRequestPage.IsAtLogNewRequestPage, "Failed to navigate to Create a Portal Request.");

                string CreatePortalRequestAssert = LogNewRequestPage.IntPortalCreateAPortalRequest();
                //   Assert.AreEqual("Create a Request", CreatePortalRequestAssert, "Failed to navigate to Create Request page.");
                LogNewRequestPage.facillogoutbutton();
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
