using AventStack.ExtentReports;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MRO.ROI.Automation.Pages;
using MRO.ROI.Automation.Pages.Common;
using MRO.ROI.Test.ExecutionFactory;
using MRO.ROI.Test.Utilities;
using OpenQA.Selenium.Remote;
using System;
using System.IO;
using System.Threading;

namespace MRO.ROI.Test.RegressionTests.MROROITests
{
    [TestClass]
    public class DukeIntegrateDeliveryConfirmationTest:ROIBaseTest
    {
    public DukeIntegrateDeliveryConfirmationTest() : base(ROITestArea.ROIAdmin)
    {

    }
    [STATestMethodAttribute]
    [TestCategory(ROITestCategory.Regression)]
      //Converted manual test case 12297-ROI-Admin-->Duke- Integrate Delivery Confirmation for Daemon Process to automated
        public void Reg_12297_DukeIntegrateDeliveryConfirmationTest()
        {
        logger = extent.CreateTest("Reg_12297_DukeIntegrateDeliveryConfirmationTest");
        logger.Log(Status.Info, "Converted manual test case 12297-ROI-Admin-->Duke- Integrate Delivery Confirmation for Daemon Process to automated");
        RemoteWebDriver _driver = Init(TestContext.Properties["browser"].ToString());
        ThreadLocal<RemoteWebDriver> localDriver = new ThreadLocal<RemoteWebDriver>();
        localDriver.Value = _driver;
        RemoteWebDriver driver = localDriver.Value;        
        string requestId = string.Empty;
        string shipmentStatus = string.Empty;
        try
        {
           ROIAdminHomePage rOIAdminHomePage = new ROIAdminHomePage(driver, logger, TestContext);
           rOIAdminHomePage.ClickFacilityList();
           ROIAdminFacilityListPage adminFacilityListPage = new ROIAdminFacilityListPage(driver, logger, TestContext);
           adminFacilityListPage.GoToDukeUniversity();
            ROIFacilityWorkSummaryPage rOIFacilityWorkSummaryPage = new ROIFacilityWorkSummaryPage(driver, logger, TestContext);
            rOIFacilityWorkSummaryPage.logaNewRequest();
            ROIFacilityLogNewRequestPage rOIFacilityLogNewRequestPage = new ROIFacilityLogNewRequestPage(driver, logger, TestContext);
            rOIFacilityLogNewRequestPage.ClickMRODeliveryTab();
            rOIFacilityLogNewRequestPage.CreateMRODeliveryRequestForDukeStageTestingLocation();
            LogNewRequestPage logNewRequestPage = new LogNewRequestPage(driver, logger, TestContext);
            requestId = logNewRequestPage.getRequestid();
            logger.Log(Status.Info, $"MRO delivery request created ({requestId})", TakeScreenShotAtStep());
            ROIFacilityRequestStatusPage rOIFacilityRequestStatusPage = new ROIFacilityRequestStatusPage(driver, logger, TestContext);
            rOIAdminHomePage.ROIlookupByRequestId(requestId);
            rOIFacilityRequestStatusPage.ImportPdfFilesForIntegrateDelivery();
            rOIFacilityRequestStatusPage.ReleaseRequestID();
            logger.Log(Status.Info, $"Request released");
            LoginPage loginPage = new LoginPage(driver, logger, TestContext);
            loginPage.LogOut();
            rOIAdminHomePage.ROIlookupByRequestId(requestId);
            ROIAdminRequestStatusPage adminRequestStatusPage = new ROIAdminRequestStatusPage(driver, logger, TestContext);
            adminRequestStatusPage.assignRequester();
            ROIAdminAssignROIRequesterPage assignROIRequesterPage = new ROIAdminAssignROIRequesterPage(driver, logger, TestContext);
            assignROIRequesterPage.AssignDukeClinicalResearchInstitute();
            logger.Log(Status.Info, "Assigned Organization as Duke Clinical Research Institute");
            adminRequestStatusPage.ClickPassDocsQC();
            adminRequestStatusPage.DeliveryOverride("MAIL");
            logger.Log(Status.Info, $"Delivery override selected as MAIL",TakeScreenShotAtStep());
            adminRequestStatusPage.ApplyRate();
            adminRequestStatusPage.CreateInvoice();
            string invoiceId = adminRequestStatusPage.GetInvoiceId();
            logger.Log(Status.Info, $"Invoice created with ID({invoiceId})");
            adminRequestStatusPage.ClickOnAddAndSelectMail();
            adminRequestStatusPage.ClickOnMailHyperlink();

            ROIAdminShipmentDetailsPage rOIAdminShipmentDetailsPage = new ROIAdminShipmentDetailsPage(driver, logger, TestContext);
            rOIAdminShipmentDetailsPage.ClickUpdateCarrierButton();
            rOIAdminShipmentDetailsPage.UpdateCarrierInformation();
            rOIAdminShipmentDetailsPage.ToClickMakeShippableButton();
            shipmentStatus = rOIAdminShipmentDetailsPage.VerifyAndGetShipmentStatus();
            Assert.AreEqual(shipmentStatus, "Package ready for shipment", "Failed to get the status of the shipment");
            logger.Log(Status.Pass, "Succcessfully verified the shipment status");
            loginPage.LogOut();
            Cleanup(driver);
        }
        catch (Exception ex)
        {
            LogException(driver, logger, ex);
        }
    }

}
}
