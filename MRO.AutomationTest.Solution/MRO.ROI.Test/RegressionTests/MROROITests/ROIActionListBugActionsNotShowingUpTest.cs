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
    public class ROIActionListBugActionsNotShowingUpTest : ROIBaseTest
    {
        public ROIActionListBugActionsNotShowingUpTest() : base(ROITestArea.ROIAdmin)
        {
        }

        [STATestMethodAttribute]
        [TestCategory(ROITestCategory.Passed)]
        // Converted manual test case 5143-ROI-Admin-->Action List Bug - Actions Not Showing up to automated.
        public void Reg_5143_ROIActionListBugActionsNotShowingUpTest()
        {
            logger = extent.CreateTest("Reg_5143_ROIActionListBugActionsNotShowingUpTest");
            logger.Log(Status.Info, "Converted manual test case 5143-ROI-Admin-->Action List Bug - Actions Not Showing up to automated.");
            RemoteWebDriver _driver = Init(TestContext.Properties["browser"].ToString());
            ThreadLocal<RemoteWebDriver> localDriver = new ThreadLocal<RemoteWebDriver>();
            localDriver.Value = _driver;
            RemoteWebDriver driver = localDriver.Value;
           

            try
            {
                string action = IniHelper.ReadConfig("ROIDeletedLocationsDoNotAuditLocationChangeTest", "ActionName");
                string facility = IniHelper.ReadConfig("ROIDeletedLocationsDoNotAuditLocationChangeTest", "Facility");
                string newActionMsg = IniHelper.ReadConfig("ROIDeletedLocationsDoNotAuditLocationChangeTest", "SecondActionName");
                string roitfacility = IniHelper.ReadConfig("ROIDeletedLocationsDoNotAuditLocationChangeTest", "ROITFacility");
                ROIAdminHomePage rOIAdminHomePage = new ROIAdminHomePage(driver, logger, TestContext);
                rOIAdminHomePage.SelectFacilityActionList();

                ROIAdminActionListPage actionListPage = new ROIAdminActionListPage(driver, logger, TestContext);
                actionListPage.ClickOnAddAction("Facility to MRO");
                logger.Log(Status.Info, "Action created with Type:[Facility to MRO]", TakeScreenShotAtStep());

                ROIAddMROToMROActionItemPage rOIAddMROToMROActionItemPage = new ROIAddMROToMROActionItemPage(driver, logger, TestContext);
                rOIAddMROToMROActionItemPage.CreateNewAction(action,facility);
                string message=rOIAddMROToMROActionItemPage.VerifyActionMessage();
                logger.Log(Status.Pass, $"Verified that message create at top of the page is :{(message)}", TakeScreenShotAtStep());
                rOIAddMROToMROActionItemPage.ClickOnReturnToList();

                bool isDisplayed=rOIAddMROToMROActionItemPage.VerifyCreatedAction(action);
                logger.Log(Status.Pass, "Verified that new entry added under actionlist page", TakeScreenShotAtStep());

                rOIAdminHomePage.OpenNewTabAndLoginROITestFacility(BaseAddress);
                ROIFacilityRequestStatusPage rOIFacilityRequestStatusPage = new ROIFacilityRequestStatusPage(driver, logger, TestContext);
                rOIFacilityRequestStatusPage.SelectRecentRequest();
                bool isActionDisplayed=rOIFacilityRequestStatusPage.VerifyActionMessageAtROITestFacility();
                Assert.IsTrue(isActionDisplayed, "Action message is not in the list");
                logger.Log(Status.Info, " Verified that action name Please Process ASAP - RS Henry Ford is in the dropdown list");

                rOIAdminHomePage.OpenNewTabAndLoginROIFacility(BaseAddress);               
                rOIFacilityRequestStatusPage.SelectRecentRequest();
                bool _isActionDisplayed = rOIFacilityRequestStatusPage.VerifyActionMessageAtROITestFacility();
                Assert.IsTrue(_isActionDisplayed, "Action message is not in the list");                
                logger.Log(Status.Info, " Verified that action name Please Process ASAP - RS Henry Ford is in the dropdown list",TakeScreenShotAtStep());


                rOIAdminHomePage.SwitchToPreviousTab(BaseAddress);
                rOIAdminHomePage.SelectFacilityActionList();
                actionListPage.ClickOnAddAction("Facility to MRO");
                rOIAddMROToMROActionItemPage.CreateNewAction(newActionMsg, roitfacility);
                string _message = rOIAddMROToMROActionItemPage.VerifyActionMessage();
                logger.Log(Status.Pass, $"Verified that message create at top of the page is :{(_message)}", TakeScreenShotAtStep());
                rOIAddMROToMROActionItemPage.ClickOnReturnToList();

                rOIAdminHomePage.SwitchBackToFacilitySide(BaseAddress);
                rOIFacilityRequestStatusPage.SelectRecentRequest();
                bool isActionExists = rOIFacilityRequestStatusPage.VerifyActionMessageAtFacilitySide();
                Assert.IsFalse(isActionExists, "Action message is not in the list");
                logger.Log(Status.Info, " Verified that action name Please Process ASAP, requester needs records - RS Henry Ford is not  in the dropdown list", TakeScreenShotAtStep());

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

