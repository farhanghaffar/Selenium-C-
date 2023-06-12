using AventStack.ExtentReports;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MRO.ROI.Automation.Pages;
using MRO.ROI.Test.ExecutionFactory;
using MRO.ROI.Test.Utilities;
using OpenQA.Selenium.Remote;
using System;
using System.Threading;

namespace MRO.ROI.Test.RegressionTests.MROROITests
{
    [TestClass]
    public class UIChangesForAdminUserAssigningFARRequestTest:ROIBaseTest
    {
        public UIChangesForAdminUserAssigningFARRequestTest() : base(ROITestArea.ROIAdmin)
        {

        }
        [STATestMethodAttribute]
        [TestCategory(ROITestCategory.Regression)]
        //Converted manual test case 1475 -ROI-Admin--> UI Changes for Admin user assigning a FAR request to automated.
        public void Reg_1475_UIChangesForAdminUserAssigningFARRequestTest()
        {
            logger = extent.CreateTest("Reg_1475_UIChangesForAdminUserAssigningFARRequestTest");
            logger.Log(Status.Info, "Converted manual test case 1475 -ROI-Admin--> UI Changes for Admin user assigning a FAR request to automated");
            RemoteWebDriver _driver = Init(TestContext.Properties["browser"].ToString());
            ThreadLocal<RemoteWebDriver> localDriver = new ThreadLocal<RemoteWebDriver>();
            localDriver.Value = _driver;
            RemoteWebDriver driver = localDriver.Value;
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
                rOIFacilityLogNewRequestPage.MRODeliveryRequestForBostonProper();
                LogNewRequestPage logNewRequestPage = new LogNewRequestPage(driver, logger, TestContext);
                string requestId = logNewRequestPage.getRequestid();
                logger.Log(Status.Info, $"MRO delivery request created ({requestId})", TakeScreenShotAtStep());

                ROIAdminHomePage roiAdminHomePage = new ROIAdminHomePage(driver, logger, TestContext);
                roiAdminHomePage.ROIlookupByRequestId(requestId);

                ROIFacilityRequestStatusPage roiFacilityRequestStatusPage = new ROIFacilityRequestStatusPage(driver, logger, TestContext);
                roiFacilityRequestStatusPage.ImportPdfFiles();
                roiFacilityRequestStatusPage.ReleaseRequestID();
                roiFacilityRequestStatusPage.FacilityLogout();

                ROIAdminHomePage roiadminHomePage = new ROIAdminHomePage(driver, logger, TestContext);
                roiadminHomePage.ROIlookupByRequestId(requestId);
                ROIAdminRequestStatusPage rOIAdminRequestStatusPage = new ROIAdminRequestStatusPage(driver, logger, TestContext);
                rOIAdminRequestStatusPage.assignRequester();               
                ROIAdminAssignROIRequesterPage rOIAdminAssignROIRequester = new ROIAdminAssignROIRequesterPage(driver, logger, TestContext);
                rOIAdminAssignROIRequester.SelectFacilityAdjustedRate();
                rOIAdminAssignROIRequester.assignTestAttorney();
                rOIAdminRequestStatusPage.ClickPassDocsQC();
                rOIAdminRequestStatusPage.CreateInvoice();
                rOIAdminRequestStatusPage.ClickCorrespondensePackageByType("Invoice");
                //last step can not be automated
                Cleanup(driver);

            }
            catch (Exception ex)
            {
                LogException(driver, logger, ex);
            }
        }
    }

}
