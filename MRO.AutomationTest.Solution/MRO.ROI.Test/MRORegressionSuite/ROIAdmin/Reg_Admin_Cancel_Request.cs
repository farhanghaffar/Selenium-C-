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

namespace MRO.ROI.Test.SmokeTests.ROIAdmin
{
    [TestClass]
    public class Reg_Admin_Cancel_Request : ROITestBase
    {
        public Reg_Admin_Cancel_Request() : base(ROITestArea.ROIAdmin)
        {

        }
        //Houdini_HotFix Test case # 2254 Admin Request status page, Requester Cancellation Page Error test scenario
        [TestMethod]
        //  [TestCategory(ROITestCategory.Regression)]
        public void Reg_AdminCancelRequest()
        {
            try
            {
                Driver.logger = Driver.extent.CreateTest("MRO Cancel Reqeust Test");
                MenuSelector.SelectRoiAdmin("Facilities", "Facility List");
                ROIAdminFacalitiesListPage.gotoROITestFacility();
                MenuSelector.Select("ROI Requests", "Log a New Request");

                bool tab = LogNewRequestPage.ClickMRODeliveryTab();
                Assert.IsTrue(tab, "Failed to click on MRO delivery tab");

                LogNewRequestPage.CreateNewMRODeliveryRequest();
                Assert.IsTrue(LogNewRequestPage.NewRequestCreated, "Failed to create new MRO delivery request");
                string requestID = ROIAdminFacalitiesListPage.getRequestid();

                LogNewRequestPage.GoToRequestStatusPage();
                Assert.IsTrue(FacilityRequestStatusPage.IsAtRequestStatusPage, "Failed to navigate to facility request status page.");

                FacilityRequestStatusPage.ScanPatientPages(LogNewRequestPage.PatientFirstName, LogNewRequestPage.PatientLastName);
                FacilityRequestStatusPage.ReleaseRequest();
                LogNewRequestPage.facillogoutbutton();
                ROIAdminFacalitiesListPage.roilookupidadmin(requestID);
                RequestStatus.roiAssignReq();
                RequestStatus.roiAdminSearch();
                RequestStatus.roiSaveDonebtn();
                RequestStatus.roiAdminapplyrate();
                RequestStatus.qcStatus();
                RequestStatus.roiCreateInvoice();

                string l1tdretrievalfees = "$" + Driver.Instance.Value.FindElement(By.Id(PageElements.ROIAdminList.l1retrievalfees_id)).Text;
                Driver.logger.Info("L1 Retrieval fee:" + l1tdretrievalfees);

                string l1adjustedbalance = Driver.Instance.Value.FindElement(By.Id(PageElements.ROIAdminList.l1adjustedbalance_id)).Text;
                Driver.logger.Info("L1 Adjusted Balance:" + l1adjustedbalance);

                Driver.Instance.Value.FindElement(By.Id(PageElements.ROIAdminList.cancelrequest_id)).Click();
                Driver.Wait(TimeSpan.FromSeconds(5));
                Driver.Instance.Value.FindElement(By.XPath(PageElements.ROIAdminList.confirmDialog_xpath)).Click();
                Driver.Wait(TimeSpan.FromSeconds(5));
                string requestercancel = Driver.Instance.Value.FindElement(By.Id(PageElements.ROIAdminList.canceldate_id)).Text;
                Driver.Wait(TimeSpan.FromSeconds(5));
                Driver.logger.Info("Request Cancelled On Dated: " + requestercancel);
                string requeststatius = Driver.Instance.Value.FindElement(By.Id(PageElements.ROIAdminList.requeststatus_id)).Text;
                Driver.logger.Info("Request Status : " + requeststatius);
                Driver.Wait(TimeSpan.FromSeconds(5));
                string l1tdretrievalfees1 = "$" + Driver.Instance.Value.FindElement(By.Id(PageElements.ROIAdminList.l1retrievalfees_id)).Text;

                Driver.Wait(TimeSpan.FromSeconds(5));
                string l1adjustedbalance1 = Driver.Instance.Value.FindElement(By.Id(PageElements.ROIAdminList.l1adjustedbalance_id)).Text;
                Driver.Wait(TimeSpan.FromSeconds(5));

                if (l1tdretrievalfees1.Equals(l1adjustedbalance1))
                {
                    Driver.logger.Pass("Page Retrieval Fees And Adjusted Balance Is Same And The Test Passed");
                }
                else
                {
                    Driver.logger.Fail("Page Retrieval Fees And Adjusted Balance After Cancelling The Requester Is NOT Same And The Test Failed");
                }
                Driver.logger.Info("L1 Retrieval fee: " + l1tdretrievalfees1);
                Driver.logger.Info("L1 Adjusted Balance: " + l1adjustedbalance1);
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

