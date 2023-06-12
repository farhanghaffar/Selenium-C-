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
    public class Reg_ElectronicRequestTest : ROITestBase
    {

        public Reg_ElectronicRequestTest() : base(ROITestArea.ROIAdmin)
        {

        }

        [TestMethod]
        [TestCategory(ROITestCategory.CiginitiDemo)]
        public void Reg_ElectronicReqstTest()
        {
            try
            {
                //Driver.Instance.Value.Manage().Window.Maximize(); Driver.Instance.Value.Manage().Window.Maximize();
                Driver.logger = Driver.extent.CreateTest("Reg_ElectronicRequestTest");
                Driver.logger.Info("Regression Work Summary,Documents Required Test");
                MenuSelector.SelectRoiAdmin("Facilities", "Facility List");
                ROIAdminFacalitiesListPage.gotoROITestFacility();
                Driver.Wait(TimeSpan.FromSeconds(5));

                FacilityWorkSummary.FacWorkSummaryEletronicRequest("//*[@id='pending-rqrportal-erequest']/span", PageElements.ROIAdminList.adminelectotal_xpath);
                FacilityWorkSummary.FacWorkSummaryEletronicRequest("//*[@id='pending-epic-warning']/span", PageElements.ROIAdminList.adminelectotal_xpath);
                FacilityWorkSummary.FacWorkSummaryEletronicRequest("//*[@id='pending-electronic-copy']/span", "//*[@id='mrocontent_MROReportGridBanner_lblRows']");


                FacilityWorkSummary.FacWorkSummaryEletronicRequest("//*[@id='new-dsm-emails']/span", "//*[@id='mrocontent_ctl00_tblResultsDirectERequests']/tbody/tr/td[2]");
                FacilityWorkSummary.FacWorkSummaryEletronicRequest("//*[@id='pending-staffsite-deliveries']/span", "//*[@id='mrocontent_tblReport']/tbody/tr/td[2]");
                //FacilityWorkSummary.FacWorkSummaryEletronicRequest("//*[@id='pending-extupload-deliveries']/span", "//*[@id='mrocontent_ctl00_tblResultsDirectERequests']/tbody/tr/td[2]");
                Driver.Instance.Value.SwitchTo().DefaultContent();
                Driver.Wait(TimeSpan.FromSeconds(5));
                Driver.Instance.Value.SwitchTo().Frame(0);
                Driver.Wait(TimeSpan.FromSeconds(2));
                Driver.Instance.Value.SwitchTo().Frame("srElectronicRequests");
                Driver.Wait(TimeSpan.FromSeconds(2));
                IWebElement rowsoutside = Driver.Instance.Value.FindElement(By.XPath("//*[@id='pending-extupload-deliveries']/span"));
                string rowsoutsideText = rowsoutside.Text;
                Driver.Instance.Value.Navigate().Back();
                Driver.Instance.Value.SwitchTo().DefaultContent();
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
