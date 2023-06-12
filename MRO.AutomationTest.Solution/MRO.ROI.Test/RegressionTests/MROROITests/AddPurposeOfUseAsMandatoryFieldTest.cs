using AventStack.ExtentReports;
using DataDrivenProject;
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
    public class AddPurposeOfUseAsMandatoryFieldTest:ROIBaseTest
    {

        public AddPurposeOfUseAsMandatoryFieldTest() : base(ROITestArea.ROIAdmin)
        {

        }
        [STATestMethodAttribute]
        [TestCategory(ROITestCategory.Passed)]
        //Converted manual test case 13116-ROI Admin-->Add Purpose of Use as a Mandatory Field to Assign ROI Requester Screen to automated
        public void Reg_13116_AddPurposeOfUseAsMandatoryFieldTest()
        {
            logger = extent.CreateTest("Reg_13116_AddPurposeOfUseAsMandatoryFieldTest");
            logger.Log(Status.Info, "Converted manual test case 13116-ROI Admin-->Add Purpose of Use as a Mandatory Field to Assign ROI Requester Screen to automated");
            RemoteWebDriver _driver = Init(TestContext.Properties["browser"].ToString());
            ThreadLocal<RemoteWebDriver> localDriver = new ThreadLocal<RemoteWebDriver>();
            localDriver.Value = _driver;
            RemoteWebDriver driver = localDriver.Value;
            string userRoot = System.Environment.GetEnvironmentVariable("USERPROFILE");
            string downloadFolder = Path.Combine(userRoot, "Downloads\\");
            string sPurposeOfUse = string.Empty;
            try
            {
                ROIAdminHomePage rOIAdminHomePage = new ROIAdminHomePage(driver, logger, TestContext);
                rOIAdminHomePage.ClickFacilityList();
                ROIAdminFacilityListPage rOIAdminFacilityListPage = new ROIAdminFacilityListPage(driver, logger, TestContext);
                rOIAdminFacilityListPage.ClickOnROITFGearIcon();               
                ROIFacilityFeaturesROITestFacilityPage rOIFacilityFeaturesROITestFacilityPage = new ROIFacilityFeaturesROITestFacilityPage(driver, logger, TestContext);
                rOIFacilityFeaturesROITestFacilityPage.ClickOnROI();
                rOIFacilityFeaturesROITestFacilityPage.CheckHasPuroseOfUse();
                logger.Log(Status.Info, "Verified Has Purpose Of Use, is enabled", TakeScreenShotAtStep());
                rOIFacilityFeaturesROITestFacilityPage.ClickUpdateFeatures();
                rOIAdminHomePage.ClickFacilityList();
                rOIAdminFacilityListPage.GotoROITestFacilityComputerIcon();
                ROIFacilityWorkSummaryPage rOIFacilityWorkSummaryPage = new ROIFacilityWorkSummaryPage(driver, logger, TestContext);
                rOIFacilityWorkSummaryPage.logaNewRequest();
                ROIFacilityLogNewRequestPage rOIFacilityLogNewRequestPage = new ROIFacilityLogNewRequestPage(driver, logger, TestContext);
                rOIFacilityLogNewRequestPage.CreateNewMRORequestWithPuposeOfUse();
                ROIFacilityRequestStatusPage roiFacilityRequestStatusPage = new ROIFacilityRequestStatusPage(driver, logger, TestContext);
                string requestId = roiFacilityRequestStatusPage.GetRequestID();
                requestId = requestId.Trim();
                logger.Log(Status.Info, $"MRO delivery request created with id ({requestId})", TakeScreenShotAtStep());

                rOIAdminHomePage.ROIlookupByRequestId(requestId);
                roiFacilityRequestStatusPage.ImportPdfFilesForPurposeOfUse();
                logger.Log(Status.Info, "PDF files imported", TakeScreenShotAtStep());

                roiFacilityRequestStatusPage.ReleaseRequestID();
                logger.Log(Status.Info, $"Request released");

                LoginPage loginPage = new LoginPage(driver, logger, TestContext);
                loginPage.LogOut();

                rOIAdminHomePage.ROIlookupByRequestId(requestId);
                ROIAdminRequestStatusPage adminRequestStatusPage = new ROIAdminRequestStatusPage(driver, logger, TestContext);
                adminRequestStatusPage.assignRequester();
                ROIAdminAssignROIRequesterPage assignROIRequesterPage = new ROIAdminAssignROIRequesterPage(driver, logger, TestContext);
                assignROIRequesterPage.assignTestAttorneyAndChangePurposeOfUse();
                sPurposeOfUse = adminRequestStatusPage.GetPurposeOfUseValue();
                logger.Log(Status.Info, "Assigned Test Attorney and changed the purpose of use from Claims Attachment to HEDIS");
                adminRequestStatusPage.ClickOnReAssignROIRequester();
                assignROIRequesterPage.UpdatePurposeOfUse();
                adminRequestStatusPage.ClickRequestHistoryButton();
                sPurposeOfUse = adminRequestStatusPage.GetPurposeOfUseFromRequestHistoryPage();
                Assert.AreEqual(sPurposeOfUse, "*Purpose Of Use has been updated from HEDIS to Claims Attachment.", "Failed to verify purpose of use updated value");
                logger.Log(Status.Info, "Purpose Of Use has been updated");                            
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
