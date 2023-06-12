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
    public class Reg_Admin_Side_Add_Issue_Action_Close : ROITestBase
    {
        public Reg_Admin_Side_Add_Issue_Action_Close() : base(ROITestArea.ROIAdmin)
        {

        }

        [TestMethod]
        [TestCategory(ROITestCategory.BuildVerification), TestCategory(ROITestCategory.Regression)]
        public void ROI_Admin_AddAnIssue()
        {
            try
            {
                Driver.logger = Driver.extent.CreateTest("Reg_Admin-Add Issues,Send Test");
                Driver.logger.Info("Reg_Admin-Add Issues,Send and Close Action Test");
                //  test = extent.CreateTest("ROI Admin Test");
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
                // LogNewRequestPage.CreateRequest();
                //    Automation.Utility.ScannerUtil.ScanDocuments();
                string requestID = ROIAdminFacalitiesListPage.getRequestid();
                Driver.Wait(TimeSpan.FromSeconds(5));
                LogNewRequestPage.RequestStatus();
                LogNewRequestPage.PatientNameValidation();
                WebElementHelper.ScrollIntoView("mrocontent_cmdScan_", FindElementBy.Id);
                LogNewRequestPage.ScanPatientPages();
                Automation.Utility.ScannerUtil.ScanDocuments();
                LogNewRequestPage.SendEnterKey();
                LogNewRequestPage.ReleaseRequest();
                WebElementHelper.Click_Enter();
                Driver.Wait(TimeSpan.FromSeconds(5));
                LogNewRequestPage.facillogoutbutton();
                ROIAdminFacalitiesListPage.roilookupidadmin(requestID);
                //  ROIAdminFacalitiesListPage.adminaddissue();
                Driver.Instance.Value.FindElement(By.Id(PageElements.ROIAdminList.adminaddissue_id)).Click();
                //   mrocontent_cmbBxIssues_Input
                Driver.Wait(TimeSpan.FromSeconds(2));
                Driver.Instance.Value.FindElement(By.Id("mrocontent_cmbBxIssues_Input")).Click();
                Driver.Wait(TimeSpan.FromSeconds(2));
                var locationItem = Driver.Instance.Value.FindElement(By.XPath("//div[@id='mrocontent_cmbBxIssues_DropDown']/div/ul/li[text()='Authorization Not Dated']"));
                locationItem.Click();
                Driver.Wait(TimeSpan.FromSeconds(2));
                Driver.Instance.Value.FindElement(By.Id("mrocontent_cmdAdd")).Click();
                Driver.Wait(TimeSpan.FromSeconds(2));
                RequestStatus.roiAssignReq();
				RequestStatus.roiAdminSearch();
                RequestStatus.roiSaveDonebtn();
				RequestStatus.roiAdminapplyrate();
                RequestStatus.qcStatus();
				RequestStatus.applyRate();
                ROIAdminUpdateReqBlngDetls.pageFees2();
				/*
				RequestStatus.roiCreateInvoice();				
                RequestStatus.roiLogCheck();
                string amountDue = ROIAdminLogChecks.roiamountdue();
                ROIAdminLogChecks.roiCheckNbr();
				ROIAdminLogChecks.roiamountdue();
                ROIAdminLogChecks.paymentamount(amountDue);
				RequestStatus.roiLogCheck();
                //TODO: need to add additional method to read balance due after payment.
                ROIAdminLogChecks.roilogchkviewrequester();
                RequestStatus.roirmvrequester();				
                LogNewRequestPage.acceptalert();
				
				*/
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

