using AventStack.ExtentReports;
using AventStack.ExtentReports.MarkupUtils;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MRO.ROI.Automation.Common;
using MRO.ROI.Automation.Pages;
using MRO.ROI.Automation.Pages.Common;
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
    public class ROIRequestReleasedFeeContractTermTest : ROIBaseTest
    {
        public ROIRequestReleasedFeeContractTermTest() : base(ROITestArea.ROIAdmin)
        {

        }
        [STATestMethodAttribute]
        [TestCategory(ROITestCategory.Regression)]
        // Converted manual test case 9545-ROI-Admin-->Request Released Fee - Contract Term to automated.
        public void Reg_9545_ROIRequestReleasedFeeContractTermTest()
        {
            logger = extent.CreateTest("Reg_9545_Request Released Fee - Contract Term");
            logger.Log(Status.Info, "Converted manual test case 9545-ROI-Facility-->Request Released Fee - Contract Term to automated");
            RemoteWebDriver _driver = Init(TestContext.Properties["browser"].ToString());
            ThreadLocal<RemoteWebDriver> localDriver = new ThreadLocal<RemoteWebDriver>();
            localDriver.Value = _driver;
            RemoteWebDriver driver = localDriver.Value;
            try
            {
                Iframe frame = new Iframe(driver, logger, TestContext);
                ROIMenuSelector menuSelector = new ROIMenuSelector(driver, logger, TestContext);

                string selectedfacility = IniHelper.ReadConfig("ROIRequestReleasedFeeContractTermTest", "facility");
                ROIAdminHomePage roiadminHomePage = new ROIAdminHomePage(driver, logger, TestContext);
                roiadminHomePage.ContractList();
                ROIAdminContractListPage contractListPage = new ROIAdminContractListPage(driver, logger, TestContext);
                frame.SwitchToRoiFrame();
                contractListPage.SelectFacilityFromDropdown();
                ROIAdminAddContractPage rOIAdminAddContractPage = new ROIAdminAddContractPage(driver, logger, TestContext);
                frame.switchToDefaut();
                logger.Log(Status.Info, "Verifying edit contract page opened", TakeScreenShotAtStep());
                string pageName = rOIAdminAddContractPage.VerifyEditContractPageHeader();
                Assert.AreEqual(pageName, "Edit Contract", "Failed : edit contract'' page title is not displaying");
                frame.SwitchToRoiFrame();
                logger.Log(Status.Pass, "Verified edit contract page opened");

                rOIAdminAddContractPage.EditSelectedContract();
                ROIAdminHomePage rOIAdminHomePage = new ROIAdminHomePage(driver, logger, TestContext);
                frame.switchToDefaut();
                roiadminHomePage.SelectFacilityList();
                frame.SwitchToRoiFrame();
                ROIAdminFacilityListPage rOIAdminFacilityListPage = new ROIAdminFacilityListPage(driver, logger, TestContext);
                rOIAdminFacilityListPage.GoToMROARTestFacilityForROIAdminUsers();
                ROIAdminFacilityWorkSummarypage rOIadminFacilityWorkSummaryPage = new ROIAdminFacilityWorkSummarypage(driver, logger, TestContext);
                rOIadminFacilityWorkSummaryPage.ToSelectListAllUsers();

                ROIFacilityUserListingPage userListingPage = new ROIFacilityUserListingPage(driver, logger, TestContext);
                userListingPage.EditLoginUser();
                ROIFacilityEditUserInfoPage rOIFacilityEditUserInfoPage = new ROIFacilityEditUserInfoPage(driver, logger, TestContext);
                string resultMsg = rOIFacilityEditUserInfoPage.CheckMROEmployeeChkBox();
                Assert.AreEqual("User Updated!", resultMsg, "Failed to validate the user is updated");
                logger.Log(Status.Pass, "Verified user updated text  is visible", TakeScreenShotAtStep());
                ROIFacilityRequestStatusPage rOIFacilityRequestStatuspage = new ROIFacilityRequestStatusPage(driver, logger, TestContext);
                menuSelector.ClickLogoutIcon();

                roiadminHomePage.OpenNewTabAndLoginROIFacility(BaseAddress);
                ROIFacilityWorkSummaryPage rOIFacilityWorkSummaryPage = new ROIFacilityWorkSummaryPage(driver, logger, TestContext);
                rOIFacilityWorkSummaryPage.logaNewRequest();
                ROIFacilityLogNewRequestPage rOIFacilityLogNewRequestPage = new ROIFacilityLogNewRequestPage(driver, logger, TestContext);
                frame.SwitchToRoiFrame();
                rOIFacilityLogNewRequestPage.ClickMRODeliveryTab();
                rOIFacilityLogNewRequestPage.CreateNewMRODeliveryRequestWithoutScan();
                ROIFacilityRequestStatusPage rOIFacilityRequestStatusPage = new ROIFacilityRequestStatusPage(driver, logger, TestContext);
                string requestid = rOIFacilityRequestStatusPage.GetRequestID();
                logger.Log(Status.Info, $"MRO  Delivery request created with id ({requestid})", TakeScreenShotAtStep());
                frame.SwitchToRoiFrame();

                rOIFacilityRequestStatusPage.ImportPdfFiles();
                rOIFacilityRequestStatusPage.ReleaseRequestID();
                logger.Log(Status.Info, "Request released");

                roiadminHomePage.SwitchToPreviousTab(BaseAddress);
                roiadminHomePage.ClickOnMonthlyStatements();
                ROIAdminMonthlyStatementReportPage rOIAdminMonthlyStatementReportPage = new ROIAdminMonthlyStatementReportPage(driver, logger, TestContext);
                rOIAdminMonthlyStatementReportPage.CreateCurrentMonthStatement();
                logger.Log(Status.Info, $"Monthly statement report created for month: ({rOIAdminMonthlyStatementReportPage.currentMonthYear}) and facility :({selectedfacility})", TakeScreenShotAtStep());

                rOIAdminMonthlyStatementReportPage.VerifyMonthlyStatementAmount();
                string month = rOIAdminMonthlyStatementReportPage.Selectedmonth();
                Assert.AreEqual(rOIAdminMonthlyStatementReportPage.currentMonthYear, month, "Failed to verify month value");
                string facility = rOIAdminMonthlyStatementReportPage.SelectedFacility();
                Assert.AreEqual(facility, selectedfacility, "Failed to verify month");
                logger.Log(Status.Pass, $"Verified current month and facility name  on  monthly statements report details page as month :({month}) and facility :({facility}) ", TakeScreenShotAtStep());
                Cleanup(driver);

            }
            catch (Exception ex)
            {
                LogException(driver, logger, ex);

            }
        }
    }

}

