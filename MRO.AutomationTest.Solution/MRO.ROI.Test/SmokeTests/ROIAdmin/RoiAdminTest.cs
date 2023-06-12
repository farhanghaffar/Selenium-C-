using AventStack.ExtentReports;
using AventStack.ExtentReports.MarkupUtils;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MRO.ROI.Automation.Common.Navigation;
using MRO.ROI.Automation.Pages;
using MRO.ROI.Automation.Pages.ROIFacility;
using MRO.ROI.Automation.Selenium;
using MRO.ROI.Test.Utilities;
using System;

namespace MRO.ROI.Test.SmokeTests.ROIAdmin
{
    [TestClass]
    public class RoiAdminTest : ROITestBase
    {
        public RoiAdminTest() : base(ROITestArea.ROIAdmin)
        {

        }

        [TestMethod]
        [TestCategory(ROITestCategory.BuildVerification), TestCategory(ROITestCategory.Regression)]
        public void ROI_Admin_CreateNew_Request()
        {
            try
            {
                Driver.logger = Driver.extent.CreateTest("MRO ROI Admin Test");
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
                //LogNewRequestPage.RequestStatus();
                //LogNewRequestPage.PatientNameValidation();
                //WebElementHelper.ScrollIntoView("mrocontent_cmdScan_");
                //LogNewRequestPage.ScanPatientPages();
                //ScannerUtil.ScanDocuments();
                //LogNewRequestPage.SendEnterKey();
                //LogNewRequestPage.ReleaseRequest();
                //WebElementHelper.Click_Enter();                

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
