using Microsoft.VisualStudio.TestTools.UnitTesting;
using MRO.ROI.Automation.Common.Navigation;
using MRO.ROI.Automation.Pages;
using MRO.ROI.Automation.Pages.Common;
using MRO.ROI.Automation.Pages.ROIFacility;
using MRO.ROI.Automation.Selenium;
using MRO.ROI.Test.Utilities;

namespace MRO.ROI.Test.SmokeTests.ROIAdmin
{
    [TestClass]
    public class ROIAdmin2Test : ROITestBase
    {

        public ROIAdmin2Test() : base(ROITestArea.ROIAdmin)
        {

        }
        
        [TestMethod]
        public void ROI_Admin_CreateNew_Request()
        {
			Driver.logger = Driver.extent.CreateTest("CreateNew_Request");
			//  test = extent.CreateTest("ROI Admin Test");
			MenuSelector.SelectRoiAdmin("Facilities", "Facility List"); ROIAdminFacalitiesListPage.gotoROITestFacility();
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
            LogNewRequestPage.RequestStatus(); LogNewRequestPage.PatientNameValidation();
            
        
            LogNewRequestPage.ReleaseRequest();
            WebElementHelper.Click_Enter();
            LogNewRequestPage.facillogoutbutton(); ROIAdminFacalitiesListPage.roilookupidadmin(requestID);
            RequestStatus.roiAssignReq(); RequestStatus.roiAdminSearch();
            RequestStatus.roiSaveDonebtn(); RequestStatus.roiAdminapplyrate();
            RequestStatus.qcStatus(); RequestStatus.applyRate();
            ROIAdminUpdateReqBlngDetls.pageFees2(); RequestStatus.roiCreateInvoice();
            RequestStatus.roiLogCheck();
            string amountDue = ROIAdminLogChecks.roiamountdue();
            ROIAdminLogChecks.roiCheckNbr(); ROIAdminLogChecks.roiamountdue();
            ROIAdminLogChecks.paymentamount(amountDue); RequestStatus.roiLogCheck();
            //TODO: need to add additional method to read balance due after payment.
            ROIAdminLogChecks.roilogchkviewrequester();
            RequestStatus.roirmvrequester();
            LogNewRequestPage.acceptalert();
            RequestStatus.roiadmlogout();

        }
    }
}
