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
    public class LargeFaxDownloadProblemByGUICreateInvoiceTest:ROIBaseTest
    {
        public LargeFaxDownloadProblemByGUICreateInvoiceTest() : base(ROITestArea.ROIAdmin)
        {
        }
        [STATestMethodAttribute]
        [TestCategory(ROITestCategory.Regression)]
        //Converted manual test case 2612-ROI-Facility-->Large Fax Download Problem-GUI (Create Invoice) to automated.
        public void Reg_2612_LargeFaxDownloadProblemByGUICreateInvoiceTest()
        {
            logger = extent.CreateTest("Reg_2612_LargeFaxDownloadProblemByGUICreateInvoiceTest");
            logger.Log(Status.Info, "Converted manual test case 2612-ROI-Facility-->Large Fax Download Problem-GUI (Create Invoice) to automated");
            RemoteWebDriver _driver = Init(TestContext.Properties["browser"].ToString());
            ThreadLocal<RemoteWebDriver> localDriver = new ThreadLocal<RemoteWebDriver>();
            localDriver.Value = _driver;
            RemoteWebDriver driver = localDriver.Value;
            string requestID = string.Empty;
            string sErrorMsg = string.Empty;
            bool isVisible = false;
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
                rOIFacilityRequestStatusPage.ImportDocumentsForLargeFaxDownload();
                //
                rOIAdminHomePage.SwitchToNewTabAndLoginROIAdmin(BaseAddress);
                rOIAdminHomePage.ROIlookupByRequestId(requestid);
                ROIAdminRequestStatusPage adminRequestStatusPage = new ROIAdminRequestStatusPage(driver, logger, TestContext);
                adminRequestStatusPage.assignRequester();
                ROIAdminAssignROIRequesterPage assignROIRequesterPage = new ROIAdminAssignROIRequesterPage(driver, logger, TestContext);
                assignROIRequesterPage.assignTestAttorney();
                logger.Log(Status.Info, "Assigned Test Attorney");               
                adminRequestStatusPage.DeliveryOverride("FAX");
                logger.Log(Status.Info, $"Delivery override selected as Fax", TakeScreenShotAtStep());                
                rOIAdminHomePage.SwitchBackToROITestFacilitySide(BaseAddress);
                rOIAdminHomePage.ROIlookupByRequestId(requestid);
                rOIFacilityRequestStatusPage.ImportPatientDocumentsForLargeFaxDownload();
                rOIFacilityRequestStatusPage.ReleaseRequestID();
                logger.Log(Status.Info, "Request Released");               
                rOIAdminHomePage.SwitchToAdminTab(BaseAddress);
                rOIAdminHomePage.SearchByRequestId(requestid);
                adminRequestStatusPage.ClickPassDocsQC();
                adminRequestStatusPage.ApplyRate();
                isVisible = adminRequestStatusPage.CreateInvoiceAndValidateThePopupMessage();               
                Assert.AreEqual(true, isVisible, "Failed to validate the message under popup alert");
                logger.Log(Status.Pass, "Verified that pop up message is visible with the text (Fax Shipment Exceeds Max Limit (10) - Please Select a Different Shipment Type)");                             
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