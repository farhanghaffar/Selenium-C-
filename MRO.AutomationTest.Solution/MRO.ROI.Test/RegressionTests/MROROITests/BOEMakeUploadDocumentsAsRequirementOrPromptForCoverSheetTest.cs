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
    public class BOEMakeUploadDocumentsAsRequirementOrPromptForCoverSheetTest: ROIBaseTest
    {
        public BOEMakeUploadDocumentsAsRequirementOrPromptForCoverSheetTest() : base(ROITestArea.ROIAdmin)
        {

        }

        [STATestMethodAttribute]
        [TestCategory(ROITestCategory.Regression)]
        //Converted manual test case 12689-ROI-Admin--> BOE - make upload documents a requirement or prompt for cover sheet to automated.
        public void Reg_12689_BOEMakeUploadDocumentsAsRequirementOrPromptForCoverSheetTest()
        {
            logger = extent.CreateTest("Reg_12689_BOEMakeUploadDocumentsAsRequirementOrPromptForCoverSheetTest");
            logger.Log(Status.Info, "Converted manual test case 12689-ROI-Admin--> BOE - make upload documents a requirement or prompt for cover sheet to automated");
            RemoteWebDriver _driver = Init(TestContext.Properties["browser"].ToString());
            ThreadLocal<RemoteWebDriver> localDriver = new ThreadLocal<RemoteWebDriver>();
            localDriver.Value = _driver;
            RemoteWebDriver driver = localDriver.Value;
            bool isCoverPageChecked = false;
            string requestID = string.Empty;
            try
            {
                ROIAdminHomePage rOIAdminHomePage = new ROIAdminHomePage(driver, logger, TestContext);
                rOIAdminHomePage.SelectRequesterList();
                ROIRequestersListPage rOIRequestersListPage = new ROIRequestersListPage(driver, logger, TestContext);
                rOIRequestersListPage.ApplyFiltersAndSearchData();
                logger.Log(Status.Info, "Internal Portal requester filterd with search filters as Organization:[ROI-Test Facility CBO],Internal Facility:[All],Type:[All]", TakeScreenShotAtStep());

                rOIRequestersListPage.ClickOrganizationLink();
                ROIAdminEditRequesterInfoPage rOIAdminEditRequesterInfoPage = new ROIAdminEditRequesterInfoPage(driver, logger, TestContext);
                bool _editRequesterInfoPageHeader = rOIAdminEditRequesterInfoPage.VerifyEditRequesterInfoPage();
                Assert.IsTrue(_editRequesterInfoPageHeader, "Failed to verify edit requester info page");
                
                bool isChecked = rOIAdminEditRequesterInfoPage.CheckeRequestDocumentsRequired();
                Assert.IsTrue(isChecked, "Failed to verify request documents required ");
                rOIAdminHomePage.SwitchToNewTabAndLoginROITestFacilityCBO(BaseAddress);
                ROICBOCreateRequestPage rOICBOCreateRequestPage = new ROICBOCreateRequestPage(driver, logger, TestContext);
                rOICBOCreateRequestPage.ClickOnCreateRequest();
                rOICBOCreateRequestPage.CreateRequest();
                isCoverPageChecked = rOICBOCreateRequestPage.CheckCreateCoverPage();
                if (isCoverPageChecked) { rOICBOCreateRequestPage.ClickOnCreateRequestButton(); }
                logger.Log(Status.Info, "Popup with the text Request Documents are required. If unavailable click Create Cover Page is visible", TakeScreenShotAtStep());
                requestID = rOICBOCreateRequestPage.VerifyButtonsAndGetRequestID();
                logger.Log(Status.Info, $"Request created with RequestID({requestID})", TakeScreenShotAtStep());
                rOICBOCreateRequestPage.ClickOnRequestStatusScreenButton();
                ROIFacilityRequestStatusPage rOIFacilityRequestStatusPage = new ROIFacilityRequestStatusPage(driver, logger, TestContext);
                //bool isDisplayed = rOIFacilityRequestStatusPage.ClickViewDocumentsAndReturnIfCopiedpatientDocumentsDisplayed();
                //rOIAdminHomePage.SwitchBackToInternalPortal();
                rOIAdminHomePage.SwitchToNewTabAndLoginROITestFacilityCBO(BaseAddress);
                rOICBOCreateRequestPage.ClickOnCreateRequest();
                rOICBOCreateRequestPage.CreateRequestWithPDF();
                rOICBOCreateRequestPage.ClickOnCreateRequestButton();
                requestID = rOICBOCreateRequestPage.GetRequestID();
                logger.Log(Status.Info, $"Request created with RequestID({requestID})", TakeScreenShotAtStep());
                bool isSelected = rOIAdminEditRequesterInfoPage.UnCheckRequestDocumentsRequired();
                Assert.IsTrue(isSelected, "Failed to uncheck request documents required ");
                Cleanup(driver);
            }
            catch (Exception ex)
            {
                LogException(driver, logger, ex);
            }
        }
    }

}