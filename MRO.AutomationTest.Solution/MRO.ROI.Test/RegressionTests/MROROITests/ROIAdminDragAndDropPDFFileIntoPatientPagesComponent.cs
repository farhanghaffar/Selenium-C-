using AventStack.ExtentReports;
using DataDrivenProject;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MRO.ROI.Automation.Common;
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
    public class ROIAdminDragAndDropPDFFileIntoPatientPagesComponent : ROIBaseTest
    {
        public ROIAdminDragAndDropPDFFileIntoPatientPagesComponent() : base(ROITestArea.ROIAdmin)
        {
        }

        [STATestMethodAttribute]
        [TestCategory(ROITestCategory.Regression)]
        //Converted manual test case 1405-ROI-Admin-->Drag and Drop the PDF file into the Patient Pages component, processing bar(circle) to appear to automated
        public void Reg_1405_ROIAdminDragAndDropPDFFileIntoPatientPagesComponent()
        {
            logger = extent.CreateTest("Reg_1405_ROIAdminDragAndDropPDFFileIntoPatientPagesComponent");
            logger.Log(Status.Info, "Converted manual test case 1405-ROI-Admin-->Drag and Drop the PDF file into the Patient Pages component, processing bar(circle) to appear to automated");
            RemoteWebDriver _driver = Init(TestContext.Properties["browser"].ToString());
            ThreadLocal<RemoteWebDriver> localDriver = new ThreadLocal<RemoteWebDriver>();
            localDriver.Value = _driver;
            RemoteWebDriver driver = localDriver.Value;            
            try
            {
                ROIAdminHomePage rOIAdminHomePage = new ROIAdminHomePage(driver, logger, TestContext);
                Iframe frame = new Iframe(driver, logger, TestContext);
                rOIAdminHomePage.SelectFacilityList();
                ROIAdminFacilityListPage rOIAdminFacilityListPage = new ROIAdminFacilityListPage(driver, logger, TestContext);
                rOIAdminFacilityListPage.GoToROITestFacility();
                ROIFacilityWorkSummaryPage rOIFacilityWorkSummaryPage = new ROIFacilityWorkSummaryPage(driver, logger, TestContext);
                rOIFacilityWorkSummaryPage.logaNewRequest();
                ROIFacilityLogNewRequestPage rOIFacilityLogNewRequestPage = new ROIFacilityLogNewRequestPage(driver, logger, TestContext);
                ROIFacilityRequestStatusPage rOIFacilityRequestStatusPage = new ROIFacilityRequestStatusPage(driver, logger, TestContext);
                frame.SwitchToRoiFrame();
                rOIFacilityLogNewRequestPage.CreateNewROITestFacilityDeliveryRequestWithoutScan();
                string requestid = rOIFacilityRequestStatusPage.GetRequestID();
                logger.Log(Status.Info, $"Request id generated- {requestid}", TakeScreenShotAtStep());
                rOIAdminHomePage.ROIlookupByRequestId(requestid);
                rOIFacilityRequestStatusPage.ImportPdfFilesforAllComponents();

                int totalPatientPagesCountOnRS = rOIFacilityRequestStatusPage.GetTotalPatientPagesCount();
                int requestPagesCountOnRS = rOIFacilityRequestStatusPage.GetRequestPagesCountOnRs();
                int totalPatientAndRequestPagesCountOnRS = totalPatientPagesCountOnRS + requestPagesCountOnRS;
                int totalPatientPagesCountOnViewDoc = rOIFacilityRequestStatusPage.ClickViewDocumnetAndReturnPatientDocumentsCount();
                int requestPagesCountOnViewDoc = rOIFacilityRequestStatusPage.ReturnRequestDocumentsCount();
                int totalRequestAndPatientPagesCountOnViewDoc = rOIFacilityRequestStatusPage.ReturnTotalPatientAndRequestDocumentsCount();
                Assert.AreEqual(totalPatientPagesCountOnRS, totalPatientPagesCountOnViewDoc, $"Patient documents on request status page ({totalPatientPagesCountOnRS}) and view documents ({totalPatientPagesCountOnViewDoc}) are not same");
                logger.Log(Status.Pass, $"Verified patient docs on request status page ({totalPatientPagesCountOnRS}) and view documents page ({totalPatientPagesCountOnViewDoc})");
                Assert.AreEqual(requestPagesCountOnRS, requestPagesCountOnViewDoc, $"request documents on request status page ({requestPagesCountOnRS}) and view documents ({requestPagesCountOnViewDoc}) are not same");
                logger.Log(Status.Pass, $"Verified request docs on request status page ({requestPagesCountOnRS}) and view documents page ({requestPagesCountOnViewDoc})");
                Assert.AreEqual(totalPatientAndRequestPagesCountOnRS, totalRequestAndPatientPagesCountOnViewDoc, $"total Patient and request documents on request status page ({totalPatientAndRequestPagesCountOnRS}) and view documents ({totalRequestAndPatientPagesCountOnViewDoc}) are not same");
                logger.Log(Status.Pass, $"Verified total request and patient docs on request status page ({totalPatientAndRequestPagesCountOnRS}) and view documents page ({totalRequestAndPatientPagesCountOnViewDoc})");
                frame.switchToDefaut();
                Cleanup(driver);
            }
            catch (Exception ex)
            {
                LogException(driver, logger, ex);
            }
        }
    }
}
