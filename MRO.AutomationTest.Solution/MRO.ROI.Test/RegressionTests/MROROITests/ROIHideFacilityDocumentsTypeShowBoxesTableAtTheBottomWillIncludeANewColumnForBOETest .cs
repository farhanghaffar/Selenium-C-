using AventStack.ExtentReports;
using AventStack.ExtentReports.MarkupUtils;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MRO.ROI.Automation.Pages;
using MRO.ROI.Automation.Selenium;
using MRO.ROI.Test.ExecutionFactory;
using MRO.ROI.Test.Utilities;
using OpenQA.Selenium.Remote;
using System;
using System.Threading;
using static MRO.ROI.Automation.Utility.IniFile;

namespace MRO.ROI.Test.RegressionTests.MROROITests
{
    [TestClass]
    public class ROIHideFacilityDocumentsTypeShowBoxesTableAtTheBottomWillIncludeANewColumnForBOETest : ROIBaseTest
    {
        public ROIHideFacilityDocumentsTypeShowBoxesTableAtTheBottomWillIncludeANewColumnForBOETest() : base(ROITestArea.ROIAdmin)
        {
        }

        [STATestMethodAttribute]
        [TestCategory(ROITestCategory.Passed)]
        // Converted manual test case 893-ROI-Admin-->Test Case 893: Hide 'Facility Documents Type 'Show Boxes' table at the bottom will include a new column for 'BOE' to automated.
        public void Reg_893_ROIHideFacilityDocumentsTypeShowBoxesTableAtTheBottomWillIncludeANewColumnForBOETest()
        {
            logger = extent.CreateTest("Reg_893_ROIHideUserFlagsShowBoxesTableAtTheBottomwWillIncludeANewColumForBOETest");
            logger.Log(Status.Info, "Converted manual test case 893-ROI-Admin-->Test Case 893: Hide 'Facility Documents Type 'Show Boxes' table at the bottom will include a new column for 'BOE' to automated");
            RemoteWebDriver _driver = Init(TestContext.Properties["browser"].ToString());
            ThreadLocal<RemoteWebDriver> localDriver = new ThreadLocal<RemoteWebDriver>();
            localDriver.Value = _driver;
            RemoteWebDriver driver = localDriver.Value;


            try
            {
                ROIAdminHomePage rOIAdminHomePage = new ROIAdminHomePage(driver, logger, TestContext);
                rOIAdminHomePage.SelectFacilityList();
                ROIAdminFacilityListPage rOIAdminFacilityListPage = new ROIAdminFacilityListPage(driver, logger, TestContext);
                rOIAdminFacilityListPage.ClickOnROITFacility();
                ROIFacilityInfoROITestFacilityPage rOIFacilityInfoROITestFacilityPage = new ROIFacilityInfoROITestFacilityPage(driver, logger, TestContext);
                rOIFacilityInfoROITestFacilityPage.SelectFacilityDocumentTypes();


                ROITestFacilityDocumentTypesPage rOITestFacilityDocumentTypesPage = new ROITestFacilityDocumentTypesPage(driver, logger, TestContext);
                bool isChecked = rOITestFacilityDocumentTypesPage.VerifyBOECheckboxes();
                Assert.IsFalse(isChecked, "BOE Checkboxes are checked");
                logger.Log(Status.Pass, "Verified BOE checkboxes are unchecked for Log a Request, and Request status", TakeScreenShotAtStep());

                rOIAdminHomePage.SwitchToNewTabAndLoginROITestFacilityCBO(BaseAddress);
                ROICBOCreateRequestPage rOICBOCreateRequestPage = new ROICBOCreateRequestPage(driver, logger, TestContext);
                rOICBOCreateRequestPage.ClickOnCreateRequest();
                bool isDisplayed = rOICBOCreateRequestPage.VerifyDocumentTypes();
                Assert.IsFalse(isDisplayed, "Document types are displayed");
                logger.Log(Status.Pass, "Verified  no  document type checkboxes are displayed", TakeScreenShotAtStep());


                rOIAdminHomePage.SwitchToPreviousTab(BaseAddress);
                rOIAdminHomePage.SelectFacilityList();
                rOIAdminFacilityListPage.ClickOnROITFacility();
                rOIFacilityInfoROITestFacilityPage.SelectFacilityDocumentTypes();
                string message=rOITestFacilityDocumentTypesPage.CheckBoeLogRequest();
                Assert.AreEqual(message, "Checkboxes Updated");
                logger.Log(Status.Info, "Verified  boe log request checkbox selected and checkboxes updated message displayed",TakeScreenShotAtStep());


                rOIAdminHomePage.SwitchToPreviousTabCBO(BaseAddress);
                rOICBOCreateRequestPage.ClickOnCreateRequest();
                bool _isDisplayed = rOICBOCreateRequestPage.VerifyDocumentTypes();
                Assert.IsTrue(_isDisplayed, "Document types are  not displayed");
                logger.Log(Status.Pass, "Verified  Document type checkboxes  are displayed", TakeScreenShotAtStep());

                rOICBOCreateRequestPage.SelectRecentRequest();
                ROICBORequestStatusPage rOICBORequestStatusPage = new ROICBORequestStatusPage(driver, logger, TestContext);
                bool isDocumentTypeDisplayed = rOICBORequestStatusPage.VerifyDocumentTypesOnRss();
                //bool isUserFlagsDisplayed = rOICBOCreateRequestPage.VerifyDocumentTypes();
                Assert.IsFalse(isDocumentTypeDisplayed, "Document types are displayed on RSS");
                logger.Log(Status.Pass, "Verified   Document type checkboxes are  not displayed on Request status page", TakeScreenShotAtStep());

                rOIAdminHomePage.SwitchToPreviousTab(BaseAddress);
                rOIAdminHomePage.SelectFacilityList();
                rOIAdminFacilityListPage.ClickOnROITFacility();
                rOIFacilityInfoROITestFacilityPage.SelectFacilityDocumentTypes();
                string _message=rOITestFacilityDocumentTypesPage.CheckBoeRequestStatus();
                Assert.AreEqual(_message, "Checkboxes Updated");
                logger.Log(Status.Info, "Verified  boe request status checkbox selected and checkboxes updated message displayed", TakeScreenShotAtStep());

                rOIAdminHomePage.SwitchToPreviousTabCBO(BaseAddress);
                rOICBOCreateRequestPage.SelectRecentRequest();
                bool _isDocumentTypesDisplayed = rOICBORequestStatusPage.VerifyDocumentTypesOnRss();            
                Assert.IsTrue(_isDocumentTypesDisplayed, "No Check boxes for document types ");
                logger.Log(Status.Pass, "Verified  document types checkboxes are displayed on Request status page", TakeScreenShotAtStep());
                Cleanup(driver);

            }

            catch (Exception ex)
            {
                LogException(driver, logger, ex);

            }
        }
    }

}

