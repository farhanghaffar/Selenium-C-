using Microsoft.VisualStudio.TestTools.UnitTesting;
using MRO.ROI.Automation.Pages;
using MRO.ROI.Test.Utilities;


namespace MRO.ROI.Test.SmokeTests
{
    [TestClass]
    public class InternalPortalTest : ROITestBase
    {
        [TestMethod]
        // public void Can_Log_A_MRO_Delivery_New_Request()
        public void Facil_InternalPortal_Delivery_New_Request()
        {
            LogNewRequestPage.GoTo();
            LogNewRequestPage.FacilIntlPrtlCreateNewRequest();
            // LogNewRequestPage.CreateRequest();
            LogNewRequestPage.ScanDocuments();
            LogNewRequestPage.RequestStatus();
            LogNewRequestPage.PatientNameValidation();
            LogNewRequestPage.scrollIntoView("mrocontent_cmdScan_");
            LogNewRequestPage.ScanPatientPages();
            LogNewRequestPage.ScanDocuments();
            LogNewRequestPage.SendEnterKey();
            LogNewRequestPage.acceptalert();
            LogNewRequestPage.facillogoutbutton();

            //Assert.IsTrue(LogNewRequestPage.IsValidRequest, "Log Request failed");
        }
    }
}