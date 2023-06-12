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
    public class LargeFaxDownloadProblemByGUIDeliveryOverrideTest:ROIBaseTest
    {
        public LargeFaxDownloadProblemByGUIDeliveryOverrideTest() : base(ROITestArea.ROIAdmin)
        {
        }
        [STATestMethodAttribute]
        [TestCategory(ROITestCategory.Development)]
        //Converted manual test case 2603-ROI-Facility-->Large Fax Download Problem-GUI (Delivery Override) to automated.
        public void Reg_2603_LargeFaxDownloadProblemByGUIDeliveryOverrideTest()
        {
            logger = extent.CreateTest("Reg_2603_LargeFaxDownloadProblemByGUIDeliveryOverrideTest");
            logger.Log(Status.Info, "Converted manual test case 2603-ROI-Facility-->Large Fax Download Problem-GUI (Delivery Override) to automated");
            RemoteWebDriver _driver = Init(TestContext.Properties["browser"].ToString());
            ThreadLocal<RemoteWebDriver> localDriver = new ThreadLocal<RemoteWebDriver>();
            localDriver.Value = _driver;
            RemoteWebDriver driver = localDriver.Value;
            string requestID = string.Empty;
            string sErrorMsg = string.Empty;
            bool isButtonVisible = false;
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
                rOIFacilityRequestStatusPage.ImportDocumentsForLargeFaxDownloadByDeliveryOverride();
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
                adminRequestStatusPage.DeliveryOverride("FAX");
                logger.Log(Status.Info, $"Delivery override selected as Fax", TakeScreenShotAtStep());
                adminRequestStatusPage.ApplyRate();
                adminRequestStatusPage.CreateInvoice();
                adminRequestStatusPage.ClickOnAddAndSelectFax();
                ROIAdminPackingListsPage adminPackingListsPage = new ROIAdminPackingListsPage(driver, logger, TestContext);
                adminPackingListsPage.CheckAllComponentsFromPackingListAndCreateShipment();
                adminRequestStatusPage.ClearInvoice();
                isButtonVisible =adminRequestStatusPage.CheckClearInvoiceDisabled();
                Assert.AreEqual(true, isButtonVisible, "Failed to get the status of clear invoice button");
                logger.Log(Status.Info, "Verified that (Clear Invoice) button is not visible anymore");
                rOIAdminHomePage.SwitchBackToROITestFacilitySide(BaseAddress);
                rOIFacilityRequestStatusPage.UnReleaseRequestID();
                logger.Log(Status.Info, "Request Unreleased");
                rOIFacilityRequestStatusPage.ImportPatientDocumentsForLargeFaxDownloadByDeliveryOverride();

                rOIAdminHomePage.SwitchToAdminTab(BaseAddress);
                rOIAdminHomePage.SearchByRequestId(requestid);
               // adminRequestStatusPage.ClickPassDocsQC();
                adminRequestStatusPage.ApplyRate();
                adminRequestStatusPage.CreateInvoice();
                logger.Log(Status.Pass, "Verified that rate has been applied and invoice got created)");
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
