using System;
using AventStack.ExtentReports;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MRO.ROI.Automation.Pages;
using MRO.ROI.Automation.Pages.Common;
using MRO.ROI.Test.ExecutionFactory;
using MRO.ROI.Test.Utilities;
using OpenQA.Selenium.Remote;
using System.Threading;

namespace MRO.ROI.Test.RegressionTests.MROROITests
{
    [TestClass]
    public class AddPanAsColumnOptionToFacilityDeliveryReportTest:ROIBaseTest
    {
        public AddPanAsColumnOptionToFacilityDeliveryReportTest() : base(ROITestArea.ROIAdmin)
        {
        }
        [STATestMethodAttribute]
        [TestCategory(ROITestCategory.Development)]
        //Converted manual test case 1810-ROI-Facility-->Facility Delivery Report- Add PAN as column option & Email Ship Manager - MRO delivery (E-mail) to automated .
        public void Reg_1810_AddPanAsColumnOptionToFacilityDeliveryReportTest()
        {
            logger = extent.CreateTest("Reg_1810_AddPanAsColumnOptionToFacilityDeliveryReportTest");
            logger.Log(Status.Info, "Converted manual test case 2603-ROI-Facility-->Large Fax Download Problem-GUI (Delivery Override) to automated");
            RemoteWebDriver _driver = Init(TestContext.Properties["browser"].ToString());
            ThreadLocal<RemoteWebDriver> localDriver = new ThreadLocal<RemoteWebDriver>();
            localDriver.Value = _driver;
            RemoteWebDriver driver = localDriver.Value;
            string requestID = string.Empty;
            string sErrorMsg = string.Empty;
           try
            {
                ROIAdminHomePage rOIAdminHomePage = new ROIAdminHomePage(driver, logger, TestContext);
                rOIAdminHomePage.FacilityList();
                ROIAdminFacilityListPage rOIAdminFacilityListPage = new ROIAdminFacilityListPage(driver, logger, TestContext);
                rOIAdminFacilityListPage.GotoROITestFacilityComputerIcon();
                ROIFacilityWorkSummaryPage rOIFacilityWorkSummaryPage = new ROIFacilityWorkSummaryPage(driver, logger, TestContext);
                rOIFacilityWorkSummaryPage.GoToLogNewRequestPage();
                ROIFacilityLogNewRequestPage rOIFacilityLogNewRequestPage = new ROIFacilityLogNewRequestPage(driver, logger, TestContext);
                rOIFacilityLogNewRequestPage.ClickMRODeliveryTab();
                rOIFacilityLogNewRequestPage.CreateNewMRODeliveryRequestForBostonProper();
                ROIFacilityRequestStatusPage rOIFacilityRequestStatusPage = new ROIFacilityRequestStatusPage(driver, logger, TestContext);
                string requestid = rOIFacilityRequestStatusPage.GetRequestID();
                logger.Log(Status.Pass, $"Request created with requestid({requestid})", TakeScreenShotAtStep());
                rOIAdminHomePage.ROIlookupByRequestId(requestid);
                rOIFacilityRequestStatusPage.ReOpenRequest();
                rOIFacilityRequestStatusPage.ImportDocumentsForFacilityDeliveryReport();
                rOIFacilityRequestStatusPage.ReleaseRequestID();
                logger.Log(Status.Info, "Request Released");
                //
                rOIAdminHomePage.SwitchToNewTabAndLoginROIAdmin(BaseAddress);
                rOIAdminHomePage.ROIlookupByRequestId(requestid);
                ROIAdminRequestStatusPage adminRequestStatusPage = new ROIAdminRequestStatusPage(driver, logger, TestContext);
                adminRequestStatusPage.assignRequester();
                ROIAdminAssignROIRequesterPage assignROIRequesterPage = new ROIAdminAssignROIRequesterPage(driver, logger, TestContext);
                assignROIRequesterPage.assignTestAttorney();
                logger.Log(Status.Info, "Assigned Test Attorney");
                adminRequestStatusPage.ClickPassDocsQC();
                adminRequestStatusPage.ApplyRate();
                adminRequestStatusPage.DeliveryOverride("EMAIL");
                logger.Log(Status.Info, $"Delivery override selected as EMAIL", TakeScreenShotAtStep());
                adminRequestStatusPage.CreateInvoice();
                string invoiceId = adminRequestStatusPage.GetInvoiceId();
                logger.Log(Status.Info, $"Invoice created with ID({invoiceId})");
                adminRequestStatusPage.ClickOnAddAndSelectEmail();
                adminRequestStatusPage.ClickOnEmailHyperlink();
                ROIAdminShipmentDetailsPage rOIAdminShipmentDetailsPage = new ROIAdminShipmentDetailsPage(driver, logger, TestContext);
                //in progress
                rOIAdminShipmentDetailsPage.GoToRequestStatusPage();
                adminRequestStatusPage.DrillInToFacility();
                rOIFacilityRequestStatusPage.ClickDeliveryReport();
                ROIFacilityDeliveryReportPage deliveryReportPage = new ROIFacilityDeliveryReportPage(driver, logger, TestContext);
                deliveryReportPage.SetDeliveredDate();
                deliveryReportPage.ClickCreateReportBtn();
                deliveryReportPage.ClickEditColumnsButton();
                deliveryReportPage.CheckPAN();
                deliveryReportPage.ClickSaveColumnsConfiuration();//Remaining 3 steps needs to be automated
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

