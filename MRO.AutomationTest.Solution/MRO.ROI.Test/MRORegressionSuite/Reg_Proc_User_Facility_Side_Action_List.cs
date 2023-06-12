using AventStack.ExtentReports;
using AventStack.ExtentReports.MarkupUtils;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MRO.ROI.Automation.Common.Navigation;
using MRO.ROI.Automation.Pages;
using MRO.ROI.Automation.Pages.Common;
using MRO.ROI.Automation.Selenium;
using MRO.ROI.Test.Utilities;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;

namespace MRO.ROI.Test.MRORegressionSuite
{
    [TestClass]
    public class Reg_Proc_User_Facility_Side_Action_List : ROITestBase
    {
        public Reg_Proc_User_Facility_Side_Action_List() : base(ROITestArea.ROIAdmin)
        {

        }
        //TestCase_917
        //Converted Manual Test case 917 Allow all changes of processing user to be audited " Facility Side Action List Testing"
        [TestMethod]
        [TestCategory(ROITestCategory.Regression)]
        public void Reg_917_Processing_User_Facility_Side_Action_List()
        {
            try
            {
                Driver.logger = Driver.extent.CreateTest("Reg Reg_Processing User To Be Audited Test");
                Driver.logger.Info("Allow all changes of processing user to be audited Facility Side Docs Required Report ");
                MenuSelector.SelectRoiAdmin("Facilities", "Facility List"); ROIAdminFacalitiesListPage.gotoROITestFacility();
                MenuSelector.Select("ROI Requests", "Action List");
                Driver.Wait(TimeSpan.FromSeconds(10));
                Driver.takeScreenShot();
                Driver.Wait(TimeSpan.FromSeconds(10));
                string facprouser = Driver.Instance.Value.FindElement(By.XPath(PageElements.FacilityRequestStatusPage1.facactionlistprouser_xpath)).Text;
                Driver.logger.Log(Status.Info, "Capturing processing user Info If there is any:" + facprouser);
                Driver.Instance.Value.FindElement(By.XPath(PageElements.FacilityRequestStatusPage1.facactinlistcheckbox0_xpath)).Click();
                Driver.Instance.Value.FindElement(By.XPath(PageElements.FacilityRequestStatusPage1.facactinlistcheckbox1_xpath)).Click();
                Driver.logger.Log(Status.Info, "Selecting first two records to assign the processing user.");
                SelectElement oSelect = new SelectElement(Driver.Instance.Value.FindElement(By.Id(PageElements.FacilityRequestStatusPage1.facprocessinguser_id)));
                oSelect.SelectByText("TestUser, Regression");
                Driver.Wait(TimeSpan.FromSeconds(3));
                Driver.Instance.Value.FindElement(By.Id("mrocontent_cmdUpdateProcessingUser")).Click();
                Driver.Wait(TimeSpan.FromSeconds(3));
                WebElementHelper.Click_Enter();
                Driver.Wait(TimeSpan.FromSeconds(10));
                Driver.takeScreenShot();
                Driver.Wait(TimeSpan.FromSeconds(10));
                string facprouser1 = Driver.Instance.Value.FindElement(By.XPath(PageElements.FacilityRequestStatusPage1.facactionlistprouser_xpath)).Text;
                Driver.logger.Log(Status.Info, "Capturing processing user Info: " + facprouser1);
                Driver.logger.Log(Status.Pass, "Successfully assigned the processing user.");
                Driver.Wait(TimeSpan.FromSeconds(3));
                Driver.Instance.Value.FindElement(By.Id("mrocontent_dgReport_ctl00_ctl04_cbSelectedSelectCheckBox")).Click();
                Driver.Instance.Value.FindElement(By.Id("mrocontent_dgReport_ctl00_ctl06_cbSelectedSelectCheckBox")).Click();
                SelectElement oSelect1 = new SelectElement(Driver.Instance.Value.FindElement(By.Id(PageElements.FacilityRequestStatusPage1.facprocessinguser_id)));
                oSelect1.SelectByText("");
                Driver.Wait(TimeSpan.FromSeconds(3));
                Driver.Instance.Value.FindElement(By.Id("mrocontent_cmdUpdateProcessingUser")).Click();
                Driver.Wait(TimeSpan.FromSeconds(3));
                WebElementHelper.Click_Enter();
                Driver.Wait(TimeSpan.FromSeconds(10));
                Driver.takeScreenShot();
                Driver.Wait(TimeSpan.FromSeconds(10));
                Driver.logger.Log(Status.Pass, "Completed the test processing user set back to null: ");
                LoginPage.LogOut();
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

