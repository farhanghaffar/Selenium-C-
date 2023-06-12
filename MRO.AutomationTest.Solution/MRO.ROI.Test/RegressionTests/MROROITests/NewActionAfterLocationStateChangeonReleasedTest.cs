using AventStack.ExtentReports;
using AventStack.ExtentReports.MarkupUtils;
using Microsoft.VisualStudio.TestTools.UnitTesting;
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
    public class NewActionAfterLocationStateChangeonReleasedTest : ROIBaseTest
    {
        public NewActionAfterLocationStateChangeonReleasedTest() : base(ROITestArea.ROIAdmin)
        {
        }

        [STATestMethodAttribute]
        [TestCategory(ROITestCategory.Regression)]
        //Converted manual test case 11130- ROI Admin-->New Action After Location State Change on Released/Invoiced Request Test to automated
        public void Reg_11130_NewActionAfterLocationStateChangeonReleasedTest()
        {

            logger = extent.CreateTest("Reg_11130_New Action After Location State Change on Released/Invoiced Request");
            logger.Log(Status.Info, "Converted manual test case 11130- ROI Admin-->New Action After Location State Change on Released/Invoiced Request Test to automated");
            RemoteWebDriver _driver = Init(TestContext.Properties["browser"].ToString());
            ThreadLocal<RemoteWebDriver> localDriver = new ThreadLocal<RemoteWebDriver>();
            localDriver.Value = _driver;
            RemoteWebDriver driver = localDriver.Value;
            try
            {

                ROIAdminHomePage adminHomePage = new ROIAdminHomePage(driver, logger, TestContext);
                adminHomePage.FacilityList();
                ROIAdminFacilityListPage rOIAdminFacilityListPage = new ROIAdminFacilityListPage(driver, logger, TestContext);
                rOIAdminFacilityListPage.GotoROITestFacilityComputerIcon();

                ROIFacilityWorkSummaryPage rOIFacilityWorkSummaryPage = new ROIFacilityWorkSummaryPage(driver, logger, TestContext);
                rOIFacilityWorkSummaryPage.GoToLogNewRequestPage();
                ROIFacilityLogNewRequestPage rOIFacilityLogNewRequestPage = new ROIFacilityLogNewRequestPage(driver, logger, TestContext);
                rOIFacilityLogNewRequestPage.ClickMRODeliveryTab();
                rOIFacilityLogNewRequestPage.MRODeliveryRequestForBostonProper();
                LogNewRequestPage logNewRequestPage = new LogNewRequestPage(driver, logger, TestContext);
                string requestId = logNewRequestPage.getRequestid();
                ROIFacilityRequestStatusPage rOIFacilityRequestStatusPage = new ROIFacilityRequestStatusPage(driver, logger, TestContext);
                logger.Log(Status.Info, $"MRO delivery request created with id:({requestId})", TakeScreenShotAtStep());
                adminHomePage.ROIlookupByRequestId(requestId);

                rOIFacilityRequestStatusPage.ImportPdfFiles();
                rOIFacilityRequestStatusPage.ReleaseRequestID();
                logger.Log(Status.Info, $"Request released");
                rOIFacilityRequestStatusPage.FacilityLogout();
                adminHomePage.ROIlookupByRequestId(requestId);

                ROIAdminRequestStatusPage rOIAdminRequestStatusPage = new ROIAdminRequestStatusPage(driver, logger, TestContext);
                rOIFacilityRequestStatusPage.DrillIntoFacility();
                rOIFacilityRequestStatusPage.ReleaseRequestID();
                rOIFacilityRequestStatusPage.UpdateLocation();
                LoginPage loginPage = new LoginPage(driver, logger, TestContext); ;
                loginPage.LogOut();
                bool isDisplayed = rOIAdminRequestStatusPage.ViewAction();
                Assert.IsFalse(isDisplayed, "Failed verify close action");
                logger.Log(Status.Pass, "Verified there is no open action", TakeScreenShotAtStep());

                string locationVal1 = IniHelper.ReadConfig("NewActionAfterLocationStateChangeonReleasedTest", "Location1");
                rOIAdminRequestStatusPage.UpdateInfoAtRoiAdminSide(locationVal1);
                logger.Log(Status.Info, "Location Updated");
                bool isDisplayed1 = rOIAdminRequestStatusPage.ViewAction();
                Assert.IsFalse(isDisplayed1, "Failed verify close action");
                logger.Log(Status.Pass, "Verified there is no open action");


                rOIAdminRequestStatusPage.assignRequester();
                ROIAdminAssignROIRequesterPage rOIAdminAssignROIRequester = new ROIAdminAssignROIRequesterPage(driver, logger, TestContext);
                rOIAdminAssignROIRequester.assignTestAttorney();
                logger.Log(Status.Info, $"Request assigned");
                rOIAdminRequestStatusPage.ApplyRate();
                rOIAdminRequestStatusPage.ClickOnQcPassButton();
                rOIAdminRequestStatusPage.CreateInvoice();
                string invoiceId = rOIAdminRequestStatusPage.GetInvoiceId();
                logger.Log(Status.Info, $"Invoice created with id ({invoiceId})", TakeScreenShotAtStep());

                string locationVal2 = IniHelper.ReadConfig("NewActionAfterLocationStateChangeonReleasedTest", "Location2");
                rOIAdminRequestStatusPage.UpdateInfoAtRoiAdminSide(locationVal2);
                bool isDisplayed2 = rOIAdminRequestStatusPage.ViewAction();
                Assert.IsFalse(isDisplayed2, "Failed verify close action");
                logger.Log(Status.Pass, "Verified there is no open action", TakeScreenShotAtStep());
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

