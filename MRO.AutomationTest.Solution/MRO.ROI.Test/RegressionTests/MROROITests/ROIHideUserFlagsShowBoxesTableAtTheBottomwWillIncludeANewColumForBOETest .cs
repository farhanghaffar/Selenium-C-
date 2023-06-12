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
    public class ROIHideUserFlagsShowBoxesTableAtTheBottomwWillIncludeANewColumForBOETest : ROIBaseTest
    {
        public ROIHideUserFlagsShowBoxesTableAtTheBottomwWillIncludeANewColumForBOETest() : base(ROITestArea.ROIAdmin)
        {
        }

        [STATestMethodAttribute]
        [TestCategory(ROITestCategory.Passed)]
        // Converted manual test case 890-ROI-Admin-->Hide 'User Flags'  'Show Boxes' table at the bottom will include a new column for 'BOE' to automated.
        public void Reg_890_ROIHideUserFlagsShowBoxesTableAtTheBottomwWillIncludeANewColumForBOETest()
        {
            logger = extent.CreateTest("Reg_890_ROIHideUserFlagsShowBoxesTableAtTheBottomwWillIncludeANewColumForBOETest");
            logger.Log(Status.Info, "Converted manual test case 890-ROI-Admin-->Hide 'User Flags'  'Show Boxes' table at the bottom will include a new column for 'BOE' to automated");
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
                rOIFacilityInfoROITestFacilityPage.SelectFacilityUserFlags();

                ROITestFacilityUserFlagsPage rOITestFacilityUserFlagsPage = new ROITestFacilityUserFlagsPage(driver, logger, TestContext);
                bool isChecked= rOITestFacilityUserFlagsPage.VerifyBOECheckboxes();
                Assert.IsFalse(isChecked, "BOE Checkboxes are checked");
                logger.Log(Status.Pass, "Verified BOE checkboxes are unchecked for Log a Request, and Request status", TakeScreenShotAtStep());

                rOIAdminHomePage.SwitchToNewTabAndLoginROITestFacilityCBO(BaseAddress);
                ROICBOCreateRequestPage rOICBOCreateRequestPage = new ROICBOCreateRequestPage(driver, logger, TestContext);
                rOICBOCreateRequestPage.ClickOnCreateRequest();
                bool isDisplayed=rOICBOCreateRequestPage.VerifyUseFlags();
                Assert.IsFalse(isDisplayed, "User flags are displayed");
                logger.Log(Status.Pass, "Verified  no user flags checkboxes are displayed",TakeScreenShotAtStep());

                rOIAdminHomePage.SwitchToPreviousTab(BaseAddress);
                rOIAdminHomePage.SelectFacilityList();
                rOIAdminFacilityListPage.ClickOnROITFacility();
                rOIFacilityInfoROITestFacilityPage.SelectFacilityUserFlags();
                string message=rOITestFacilityUserFlagsPage.CheckBoeLogRequest();
                Assert.AreEqual(message, "Checkboxes Updated");
                logger.Log(Status.Info, "Verified  boe log request checkbox selected and checkboxes updated message displayed", TakeScreenShotAtStep());

                rOIAdminHomePage.SwitchToPreviousTabCBO(BaseAddress);
                rOICBOCreateRequestPage.ClickOnCreateRequest();
                bool _isDisplayed = rOICBOCreateRequestPage.VerifyUseFlags();
                Assert.IsTrue(_isDisplayed, "User flags are not displayed");
                logger.Log(Status.Pass, "Verified   user flags checkboxes are displayed", TakeScreenShotAtStep());

                rOICBOCreateRequestPage.SelectRecentRequest();
                ROICBORequestStatusPage rOICBORequestStatusPage = new ROICBORequestStatusPage(driver, logger, TestContext);
                rOICBORequestStatusPage.VerifyUseFlagsOnRSS();
                bool isUserFlagsDisplayed = rOICBOCreateRequestPage.VerifyUseFlags();
                Assert.IsFalse(isUserFlagsDisplayed, "User flags are displayed");
                logger.Log(Status.Pass, "Verified  no user flags checkboxes are displayed on Request status page", TakeScreenShotAtStep());


                rOIAdminHomePage.SwitchToPreviousTab(BaseAddress);
                rOIAdminHomePage.SelectFacilityList();
                rOIAdminFacilityListPage.ClickOnROITFacility();
                rOIFacilityInfoROITestFacilityPage.SelectFacilityUserFlags();
                string _message=rOITestFacilityUserFlagsPage.CheckBoeRequestStatus();
                Assert.AreEqual(_message, "Checkboxes Updated");
                logger.Log(Status.Info, "Verified  boe request status checkbox selected and checkboxes updated message displayed", TakeScreenShotAtStep());


                rOIAdminHomePage.SwitchToPreviousTabCBO(BaseAddress);
                rOICBOCreateRequestPage.SelectRecentRequest();
                bool _isUserFlagsDisplayed = rOICBORequestStatusPage.VerifyUseFlagsOnRSS();
               // bool _isUserFlagsDisplayed = rOICBOCreateRequestPage.VerifyUseFlags();
                Assert.IsTrue(_isUserFlagsDisplayed, " No Check boxes for User flags ");
                logger.Log(Status.Pass, "Verified   user flags checkboxes are displayed on Request status page", TakeScreenShotAtStep());
                Cleanup(driver);
            }

            catch (Exception ex)
            {
                LogException(driver, logger, ex);

            }
        }
    }

}

