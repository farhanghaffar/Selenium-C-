using AventStack.ExtentReports;
using AventStack.ExtentReports.MarkupUtils;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MRO.ROI.Automation.Common.Navigation;
using MRO.ROI.Automation.Pages;
using MRO.ROI.Automation.Pages.ROIFacility;
using MRO.ROI.Automation.Selenium;
using MRO.ROI.Test.Utilities;
using OpenQA.Selenium;
using System;

namespace MRO.ROI.Test.MRORegressionSuite
{
    [TestClass]
    public class Reg_Dos_Enter_Date_Without_Slashes : ROITestBase
    {
        public Reg_Dos_Enter_Date_Without_Slashes() : base(ROITestArea.ROIAdmin)
        {

        }
        //TestCase_927
        //Converted Manual Test case 927 "User can enter the date without any slashes."
        [TestMethod]
        [TestCategory(ROITestCategory.Regression)]
        public void Reg_927_Enter_Date_Without_Slashes()
        {
            try
            {
				Driver.logger = Driver.extent.CreateTest("Reg Date Of Service Test");
				MenuSelector.SelectRoiAdmin("Facilities", "Facility List");
				ROIAdminFacalitiesListPage.gotoROITestFacility();
				MenuSelector.Select("ROI Requests", "Log a New Request");
				//   LogNewRequestPage.GoToLogNewRequestPage();
				//   Assert.IsTrue(LogNewRequestPage.IsAtLogNewRequestPage, "Failed to navigate to Log New Request page.");
				bool tab = LogNewRequestPage.ClickMRODeliveryTab();
				Assert.IsTrue(tab, "Failed to click on MRO delivery tab");
				//   Assert.IsTrue(mroDelTab, "Failed to click on MRO delivery tab");
				LogNewRequestPage.CreateNewMRODeliveryRequest();
				Assert.IsTrue(LogNewRequestPage.NewRequestCreated, "Failed to create new MRO delivery request");
                string requestID = ROIAdminFacalitiesListPage.getRequestid();
                LogNewRequestPage.RequestStatus();
				LogNewRequestPage.PatientNameValidation();
                WebElementHelper.ScrollIntoView("mrocontent_cmdScan_", FindElementBy.Id);
                LogNewRequestPage.ScanPatientPages();
                Automation.Utility.ScannerUtil.ScanDocuments();
                LogNewRequestPage.SendEnterKey();
                LogNewRequestPage.ReleaseRequest();
                WebElementHelper.Click_Enter();
                Driver.Wait(TimeSpan.FromSeconds(5));
                Driver.Instance.Value.FindElement(By.Id("mrocontent_cmdUpdateInfo")).Click();
                Driver.Wait(TimeSpan.FromSeconds(3));
                Driver.Instance.Value.FindElement(By.Id("mrocontent_txtDOS")).SendKeys("04172019");
                Driver.logger.Log(Status.Info, "DOS: Start date entered without slashes.");
                //     Driver.Instance.Value.FindElement(By.Id(PageElements.FacilityRequestStatusPage1.facdosstartDate_id)).SendKeys("04172019");
                Driver.Instance.Value.FindElement(By.Id("mrocontent_txtDOSEnd")).SendKeys("04172019");
                Driver.logger.Log(Status.Info, "DOS: End date entered without slashes.");
                Driver.Wait(TimeSpan.FromSeconds(3));
                Driver.takeScreenShot();
                Driver.Instance.Value.FindElement(By.Id("mrocontent_cmdUpdate")).Click();
                Driver.Wait(TimeSpan.FromMilliseconds(3));
                string dosdate = Driver.Instance.Value.FindElement(By.XPath(PageElements.FacilityRequestStatusPage1.facdostext_xpath)).Text;
                Console.WriteLine(dosdate);
                Driver.logger.Log(Status.Pass, "DOS Validation: Slashed added to the dates:");
                Driver.takeScreenShot();
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

