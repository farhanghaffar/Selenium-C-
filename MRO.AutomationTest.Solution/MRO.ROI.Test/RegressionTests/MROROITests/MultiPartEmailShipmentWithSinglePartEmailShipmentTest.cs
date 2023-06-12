using AventStack.ExtentReports;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MRO.ROI.Automation.Pages;
using MRO.ROI.Test.Utilities;
using OpenQA.Selenium.Remote;
using System;
using System.Threading;
using MRO.ROI.Test.ExecutionFactory;
using MRO.ROI.Automation.Pages.Common;

namespace MRO.ROI.Test.RegressionTests.MROROITests
{
    [TestClass]
    public class MultiPartEmailShipmentWithSinglePartEmailShipmentTest : ROIBaseTest
    {
        public MultiPartEmailShipmentWithSinglePartEmailShipmentTest() : base(ROITestArea.ROIAdmin)
        {

        }
        [STATestMethodAttribute]
        [TestCategory(ROITestCategory.Development)]
        //Converted manual test case 652-ROI-Facility-->Multi-part Email Shipment - Single part e-mail shipment to automated
        public void Reg_652_MultiPartEmailShipmentWithSinglePartEmailShipmentTest()
        {
            logger = extent.CreateTest("Reg_652_MultiPartEmailShipmentWithSinglePartEmailShipmentTest");
            logger.Log(Status.Info, "Converted manual test case 652-ROI-Facility-->Multi-part Email Shipment - Single part e-mail shipment to automated");
            RemoteWebDriver _driver = Init(TestContext.Properties["browser"].ToString());
            ThreadLocal<RemoteWebDriver> localDriver = new ThreadLocal<RemoteWebDriver>();
            localDriver.Value = _driver;
            RemoteWebDriver driver = localDriver.Value;
            string requestStatus = string.Empty;
            try
            {
                ROIAdminHomePage rOIAdminHomePage = new ROIAdminHomePage(driver, logger, TestContext);
                rOIAdminHomePage.SelectFacilityList();
                ROIAdminFacilityListPage rOIAdminFacilityListPage = new ROIAdminFacilityListPage(driver, logger, TestContext);
                rOIAdminFacilityListPage.GoToROITestFacility();
                ROIFacilityWorkSummaryPage rOIFacilityWorkSummaryPage = new ROIFacilityWorkSummaryPage(driver, logger, TestContext);
                rOIFacilityWorkSummaryPage.logaNewRequest();
                ROIFacilityLogNewRequestPage rOIFacilityLogNewRequestPage = new ROIFacilityLogNewRequestPage(driver, logger, TestContext);
                rOIFacilityLogNewRequestPage.CreateNewROITestFacilityDeliveryRequestWithoutScan();
                ROIFacilityRequestStatusPage rOIFacilityRequestStatusPage = new ROIFacilityRequestStatusPage(driver, logger, TestContext);
                string requestid = rOIFacilityRequestStatusPage.GetRequestID();
                logger.Log(Status.Info, $"ROI request created with id ({requestid})", TakeScreenShotAtStep());
                rOIAdminHomePage.ROIlookupByRequestId(requestid);
                rOIFacilityRequestStatusPage.ImportPatientPagesForMultiPartEmailShipment();
                rOIFacilityRequestStatusPage.ReleaseRequestID();
                rOIAdminHomePage.SwitchToNewTabAndLoginROIAdmin(BaseAddress);
                rOIAdminHomePage.ROIlookupByRequestId(requestid);
                //
                ROIAdminRequestStatusPage adminRequestStatusPage = new ROIAdminRequestStatusPage(driver, logger, TestContext);
                adminRequestStatusPage.ClickPassDocsQC();
                adminRequestStatusPage.DeliveryOverride("EMAIL");
                logger.Log(Status.Info, $"Delivery override selected as EMAIL", TakeScreenShotAtStep());
                adminRequestStatusPage.SetNonBillable();
                adminRequestStatusPage.CreateInvoice();
                string invoiceId = adminRequestStatusPage.GetInvoiceId();
                logger.Log(Status.Info, $"Invoice created with ID({invoiceId})");
                adminRequestStatusPage.ClickOnAddAndSelectEmail();
                adminRequestStatusPage.ClickOnEmailHyperlink();
                ROIAdminShipmentDetailsPage rOIAdminShipmentDetailsPage = new ROIAdminShipmentDetailsPage(driver, logger, TestContext);
                rOIAdminShipmentDetailsPage.ToClickMakeShippableButton();// in progress
                LoginPage loginPage = new LoginPage(driver, logger, TestContext);
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

