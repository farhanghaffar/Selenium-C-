using AventStack.ExtentReports;
using AventStack.ExtentReports.MarkupUtils;
using DataDrivenProject;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MRO.ROI.Automation.Pages;
using MRO.ROI.Automation.Pages.Common;
using MRO.ROI.Automation.Selenium;
using MRO.ROI.Test.ExecutionFactory;
using MRO.ROI.Test.Utilities;
using OpenQA.Selenium.Remote;
using System;
using System.IO;
using System.Threading;
using static MRO.ROI.Automation.Utility.IniFile;

namespace MRO.ROI.Test.RegressionTests.MROROITests
{
    [TestClass]
    public class ROIEmailSearchFieldInROI : ROIBaseTest
    {
        public ROIEmailSearchFieldInROI() : base(ROITestArea.ROIAdmin)
        {

        }
        [STATestMethodAttribute]
        [TestCategory(ROITestCategory.Development)]
        // Converted manual test case 8558-MRO eXpress - E-Mail Search Field in ROI
        //Automated By Mahesh Badampeta
        public void Reg_8558_ROIEmailSearchFieldInROI()
        {
            logger = extent.CreateTest("Reg_8558-MRO eXpress - E-Mail Search Field in ROI");
            logger.Log(Status.Info, "Converted manual test case 8558-MRO eXpress - E-Mail Search Field in ROI");
            RemoteWebDriver _driver = Init(TestContext.Properties["browser"].ToString());
            ThreadLocal<RemoteWebDriver> localDriver = new ThreadLocal<RemoteWebDriver>();
            localDriver.Value = _driver;
            RemoteWebDriver driver = localDriver.Value;

            try
            {
                string requestid = "";
                ROIAdminHomePage rOIAdminHomePage = new ROIAdminHomePage(driver, logger, TestContext);
                rOIAdminHomePage.SelectFacilityList();
                ROIAdminFacilityListPage rOIAdminFacilityListPage = new ROIAdminFacilityListPage(driver, logger, TestContext);
                rOIAdminFacilityListPage.GoToROITestFacility();
                ROIFacilityWorkSummaryPage rOIFacilityWorkSummaryPage = new ROIFacilityWorkSummaryPage(driver, logger, TestContext);
                rOIFacilityWorkSummaryPage.logaNewRequest();
                ROIFacilityLogNewRequestPage rOIFacilityLogNewRequestPage = new ROIFacilityLogNewRequestPage(driver, logger, TestContext);
                ROIFacilityRequestStatusPage rOIFacilityRequestStatusPage = new ROIFacilityRequestStatusPage(driver, logger, TestContext);
                rOIFacilityLogNewRequestPage.CreateNewROITestFacilityDeliveryRequestWithoutScanTwo();
                requestid = rOIFacilityRequestStatusPage.GetRequestID();
                logger.Log(Status.Info, $"Request id generated- {requestid}", TakeScreenShotAtStep());
                rOIFacilityRequestStatusPage.ImportPdfFiles();
                bool statusUnderImportDocument = rOIFacilityRequestStatusPage.VerifyStatusUnderImportDocument();
                Assert.IsTrue(statusUnderImportDocument, "Failed to verify status under import document is uploaded");
                rOIFacilityRequestStatusPage.ReleaseRequestID();
                rOIAdminHomePage.SwitchToNewTabAndLoginROIAdmin(BaseAddress);
                rOIAdminHomePage.ROIlookupByRequestId(requestid);
                ROIAdminRequestStatusPage rOIAdminRequestStatusPage = new ROIAdminRequestStatusPage(driver, logger, TestContext);
                rOIAdminRequestStatusPage.assignRequester();
                ROIAdminAssignROIRequesterPage rOIAdminAssignROIRequesterPage = new ROIAdminAssignROIRequesterPage(driver, logger, TestContext);
                rOIAdminAssignROIRequesterPage.assignTestAttorneyWithAdditionalEmail();
                rOIAdminRequestStatusPage.ClickOnFindARequest();
                rOIAdminHomePage.VerifyHeader();
                rOIAdminHomePage.ClearAllFields();
                rOIAdminHomePage.setRefAndFacility();
                rOIAdminHomePage.VerifyrequestID(requestid);
                rOIAdminHomePage.ClearAllFields();
                rOIAdminHomePage.setReference();
                rOIAdminHomePage.VerifyrequestID(requestid);
                rOIAdminHomePage.ClearAllFields();
                rOIAdminHomePage.setRequesterEmail("test@mrocorp.com; testMRO@mrocorp.com");
                rOIAdminHomePage.VerifyrequestID(requestid);
                rOIAdminHomePage.setRequesterEmail("testMRO@mrocorp.com");
                rOIAdminHomePage.VerifyrequestID(requestid);
                rOIAdminHomePage.ClearAllFields();
                // Need to search with SSN and look for request. SSN is not shared in Test steps

            }
            catch (Exception ex)
            {
                LogException(driver, logger, ex);

            }
        }
    }
}



