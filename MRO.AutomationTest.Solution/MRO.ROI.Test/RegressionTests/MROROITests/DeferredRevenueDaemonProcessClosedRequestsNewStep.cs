using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AventStack.ExtentReports;
using AventStack.ExtentReports.MarkupUtils;
using DataDrivenProject;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MRO.ROI.Automation;
using MRO.ROI.Automation.Pages;
using MRO.ROI.Automation.Selenium;
using MRO.ROI.Test.Utilities;
using OpenQA.Selenium.Remote;
using System.IO;
using System.Threading;



namespace MRO.ROI.Test.RegressionTests.MROROITests
{

    [TestClass]
    public class DeferredRevenueDaemonProcessClosedRequestsNewStep : ROIBaseTest
    {
        public DeferredRevenueDaemonProcessClosedRequestsNewStep() : base(ROITestArea.ROIAdmin)
        {
        }

        [TestMethod]
        [TestCategory(ROITestCategory.Regression)]
        //Converted manual test case 11690-ROI-Admin-->Deferred Revenue Daemon Process Closed Requests New Step to automated.
        public void Reg_11690_DeferredRevenueDaemonProcessClosedRequestsNewStep()
        {
            logger = extent.CreateTest("Reg_11690_Deferred Revenue Daemon Process Closed Requests NewbStep to automated");
            logger.Log(Status.Info, "Converted manual test case 11690-ROI-Admin-->Deferred Revenue Daemon Process Closed Requests New Step to automated ");
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
                rOIFacilityLogNewRequestPage.CreateNewMRODeliveryRequestForBostonProper();
                ROIFacilityRequestStatusPage rOIFacilityRequestStatusPage = new ROIFacilityRequestStatusPage(driver, logger, TestContext);
                string linkedRequestid = rOIFacilityRequestStatusPage.FormatAndGetRequestId();
                string requestid = rOIFacilityRequestStatusPage.GetRequestID();
                logger.Log(Status.Info, $"Request created with ID ({linkedRequestid})", TakeScreenShotAtStep());

                rOIAdminHomePage.ROIlookupByRequestId(requestid); 
                rOIFacilityRequestStatusPage.ImportDocumentsForRequestPages();
                rOIFacilityRequestStatusPage.FacilityLogout();
                logger.Log(Status.Info, "PDF files imported", TakeScreenShotAtStep());

                rOIAdminHomePage.ROIlookupByRequestId(requestid);
                rOIFacilityRequestStatusPage.ClickCloseRequest();
                CloseRequest closeRequest = new CloseRequest(driver, logger, TestContext);
                closeRequest.ClickCloseRequest();
                logger.Log(Status.Info, "Verified that the request has been closed", TakeScreenShotAtStep());
                logger.Log(Status.Info, "donot have access to HQSMSTG01 so Step-10 must be executed manually");
                Cleanup(driver);
            }
            catch (Exception ex)
            {
                LogException(driver, logger, ex);
            }
        }
    }
}
