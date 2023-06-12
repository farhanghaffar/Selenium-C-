using Microsoft.VisualStudio.TestTools.UnitTesting;
using MRO.ROI.Automation.Common.Navigation;
using MRO.ROI.Automation.Pages;
using MRO.ROI.Automation.Pages.ROIFacility;
using MRO.ROI.Automation.Selenium;
using MRO.ROI.Test.Utilities;

namespace MRO.ROI.Test.MRORegressionSuite.ROIAdmin
{
    [TestClass]
    public class RequestPreAuthorizationTest : ROITestBase
    {

        public RequestPreAuthorizationTest() : base(ROITestArea.ROIAdmin)
        {

        }

        [TestMethod]
        public void ROI_Admin_CreateNew_Request()
        {
            //  test = extent.CreateTest("ROI Admin Test");
            MenuSelector.SelectRoiAdmin("Facilities", "Facility List"); ROIAdminFacalitiesListPage.gotoROITestFacility();
            MenuSelector.Select("ROI Requests", "Log a New Request"); LogNewRequestPage.CreateRequest();
            Automation.Utility.ScannerUtil.ScanDocuments();
            string requestID = ROIAdminFacalitiesListPage.getRequestid();
            LogNewRequestPage.RequestStatus(); LogNewRequestPage.PatientNameValidation();
            WebElementHelper.ScrollIntoView("mrocontent_cmdPreAuth", FindElementBy.Id);
            //   WebElementHelper.ScrollIntoView("mrocontent_cmdScan_");  FacilityRequestStatusPage
            LogNewRequestPage.RequestPreAuthorization();
            FacilityRequestStatusPage.ReqPreAuthorizationBtn();


            //  LogNewRequestPage.RequestPreAuthorization();
            //      Automation.Utility.ScannerUtil.ScanDocuments();
            //  LogNewRequestPage.SendEnterKey();
            //  LogNewRequestPage.ReleaseRequest();
            //  WebElementHelper.Click_Enter();
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
