//using AventStack.ExtentReports_;
using AventStack.ExtentReports;
using AventStack.ExtentReports.MarkupUtils;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MRO.ROI.Automation.Common.Navigation;
using MRO.ROI.Automation.Pages;
using MRO.ROI.Automation.Pages.ROIFacility;
using MRO.ROI.Automation.Selenium;
using MRO.ROI.Test.Utilities;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;

namespace MRO.ROI.Test.MRORegressionSuite
{
    [TestClass]
    public class Reg_Processing_User_To_Be_Audited : ROITestBase
    {
        public Reg_Processing_User_To_Be_Audited() : base(ROITestArea.ROIAdmin)
        {

        }
        //Converted Manual TestCase_914 Allow all changes of processing user to be audited 
        //" Facility Side Docs Required Report Testing"
        [TestMethod]
        [TestCategory(ROITestCategory.Regression)]
        public void Reg_914_Processing_User_Audited()
        {
            try
            {

                Driver.logger = Driver.extent.CreateTest("Reg Reg_Processing User To Be Audited Test");
                Driver.logger.Info("Allow all changes of processing user to be audited Facility Side Docs Required Report ");

                MenuSelector.SelectRoiAdmin("Facilities", "Facility List"); ROIAdminFacalitiesListPage.gotoROITestFacility();
                Driver.Wait(TimeSpan.FromSeconds(3));
                MenuSelector.Select("ROI Requests", "Docs Required");

                Driver.Wait(TimeSpan.FromSeconds(3));
                Driver.takeScreenShot();
                Driver.Wait(TimeSpan.FromSeconds(3));
                Driver.Instance.Value.FindElement(By.Id("mrocontent_dgRequest_selChkBox_0")).Click();
                Driver.Instance.Value.FindElement(By.Id("mrocontent_dgRequest_selChkBox_1")).Click();
                Driver.logger.Log(Status.Info, "Selecting first two records to assign the processing user.");
                SelectElement oSelect = new SelectElement(Driver.Instance.Value.FindElement(By.Id(PageElements.FacilityRequestStatusPage1.facprocessinguser_id)));
                // oSelect.SelectByText("Patient"); Provider
                oSelect.SelectByText("TestUser, Regression");
                Driver.Wait(TimeSpan.FromSeconds(3));
                Driver.Instance.Value.FindElement(By.Id("mrocontent_cmdUpdateProcessingUser")).Click();
                Driver.Wait(TimeSpan.FromSeconds(3));
                WebElementHelper.Click_Enter();
                Driver.Wait(TimeSpan.FromSeconds(3));
                Driver.takeScreenShot();
                Driver.logger.Log(Status.Pass, "Successfully assigned the processing user.");
                Driver.Instance.Value.FindElement(By.Id("mrocontent_dgRequest_selChkBox_0")).Click();
                Driver.Instance.Value.FindElement(By.Id("mrocontent_dgRequest_selChkBox_1")).Click();
                SelectElement oSelect1 = new SelectElement(Driver.Instance.Value.FindElement(By.Id(PageElements.FacilityRequestStatusPage1.facprocessinguser_id)));
                oSelect1.SelectByText("");
                Driver.Wait(TimeSpan.FromSeconds(3));
                Driver.Instance.Value.FindElement(By.Id("mrocontent_cmdUpdateProcessingUser")).Click();
                Driver.Wait(TimeSpan.FromSeconds(3));
                WebElementHelper.Click_Enter();
                Driver.Wait(TimeSpan.FromSeconds(3));
                Driver.takeScreenShot();
                Driver.logger.Log(Status.Info, "Setting up the processing user to blank.");
                LogNewRequestPage.facillogoutbutton();
                Driver.Wait(TimeSpan.FromSeconds(3));
                RequestStatus.roiadmlogout();


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

