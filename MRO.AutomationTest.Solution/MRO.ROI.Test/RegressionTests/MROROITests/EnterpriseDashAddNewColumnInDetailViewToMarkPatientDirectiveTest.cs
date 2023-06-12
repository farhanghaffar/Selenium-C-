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
    public class EnterpriseDashAddNewColumnInDetailViewToMarkPatientDirectiveTest : ROIBaseTest
    {
        public EnterpriseDashAddNewColumnInDetailViewToMarkPatientDirectiveTest() : base(ROITestArea.ROITestFacility)
        {
        }

        [STATestMethodAttribute]
        [TestCategory(ROITestCategory.Regression)]
        //Converted manual test case 1833- ROI Facility-->Enterprise Dash - Add new column in detail view to mark patient directive Test to automated
        public void Reg_1833_EnterpriseDashAddNewColumnInDetailViewToMarkPatientDirectiveTest()
        {
            logger = extent.CreateTest("Reg_1833_EnterpriseDashAddNewColumnInDetailViewToMarkPatientDirectiveTest");
            logger.Log(Status.Info, "Converted manual test case 1833- ROI Facility-->Enterprise Dash - Add new column in detail view to mark patient directive Test to automated");
            RemoteWebDriver _driver = Init(TestContext.Properties["browser"].ToString());
            ThreadLocal<RemoteWebDriver> localDriver = new ThreadLocal<RemoteWebDriver>();
            localDriver.Value = _driver;
            RemoteWebDriver driver = localDriver.Value;
            try

            {

                string dummyEmailId = IniHelper.ReadConfig("EnterpriseDashAddNewColumnInDetailViewToMarkPatientDirectiveTest", "TestEmailId");
                ROIAdminHomePage rOIAdminHomePage = new ROIAdminHomePage(driver, logger, TestContext);
                ROIFacilityWorkSummaryPage rOIFacilityWorkSummaryPage = new ROIFacilityWorkSummaryPage(driver, logger, TestContext);
                rOIFacilityWorkSummaryPage.GoToLogNewRequestPage();

                ROIFacilityLogNewRequestPage rOIFacilityLogNewRequestPage = new ROIFacilityLogNewRequestPage(driver, logger, TestContext);
                rOIFacilityLogNewRequestPage.ClickMRODeliveryTab();
                rOIFacilityLogNewRequestPage.MRODeliveryRequestWithMailId();
                LogNewRequestPage logNewRequestPage = new LogNewRequestPage(driver, logger, TestContext);
                string requestId = logNewRequestPage.getRequestid();
                ROIFacilityRequestStatusPage rOIFacilityRequestStatusPage = new ROIFacilityRequestStatusPage(driver, logger, TestContext);
                logger.Log(Status.Info, $"MRO delivery request created with id:({requestId})", TakeScreenShotAtStep());

                rOIAdminHomePage.ROIlookupByRequestId(requestId);
                rOIFacilityRequestStatusPage.ImportPdfFiles();
                rOIFacilityRequestStatusPage.ReleaseRequestID();
                logger.Log(Status.Info, $"Request released");

                rOIAdminHomePage.SwitchToNewTabAndLoginROIAdmin(BaseAddress);
                rOIAdminHomePage.SearchByRequestId(requestId);
                ROIAdminRequestStatusPage rOIAdminRequestStatusPage = new ROIAdminRequestStatusPage(driver, logger, TestContext);
                rOIAdminRequestStatusPage.assignRequester();
                ROIAdminAssignROIRequesterPage assignROIRequesterPage = new ROIAdminAssignROIRequesterPage(driver, logger, TestContext);
                string requesterType = assignROIRequesterPage.AssignPatientRequester("96365");
                Assert.AreEqual(requesterType, "Patient", "Failed to verify requester type");
                logger.Log(Status.Info, "Verified at update requester info section requester type set to Patient", TakeScreenShotAtStep());

                string text = assignROIRequesterPage.VerifyInfo();
                Assert.AreEqual(text, "Bill To requester updated", "Failed to verify text");
                logger.Log(Status.Info, "Verified text Bill To requester updated");
                assignROIRequesterPage.AssignRequestWithMailId();
                string shipToValue = rOIAdminRequestStatusPage.VerifyShipTo();
                Assert.AreEqual(shipToValue, "TEST Attorney's");
                logger.Log(Status.Info, $"Successfully verified at request status page ship to set to{(shipToValue)}", TakeScreenShotAtStep());

                string emailValue = rOIAdminRequestStatusPage.VerifyEmail();
                Assert.AreEqual(emailValue, dummyEmailId);
                logger.Log(Status.Info, $"Successfully verified at request status page email id to set to{(emailValue)}");
                rOIAdminHomePage.SwitchBackToROITestFacilitySide(BaseAddress);
                rOIFacilityWorkSummaryPage.SearchByRequestId(requestId);


                rOIFacilityRequestStatusPage.SelectEnterpriseDashboard();
                ROIFacilityEnterpriseDashboardPage rOIFacilityEnterpriseDashboardPage = new ROIFacilityEnterpriseDashboardPage(driver, logger, TestContext);
                string selectedDateRange = rOIFacilityEnterpriseDashboardPage.CreateEnterpriseDashboardReport();
                string requestAgingDateRange = rOIFacilityEnterpriseDashboardPage.VerifyRequestAgingDateRange();
                Assert.AreEqual(selectedDateRange, requestAgingDateRange);
                logger.Log(Status.Info, $"Successfully verified  request aging set to selected date range as:{(requestAgingDateRange)}", TakeScreenShotAtStep());


                int childrenCount= rOIFacilityEnterpriseDashboardPage.Validatebargraphs();
                if(childrenCount>=1)
                {

                    bool isPresent = rOIFacilityEnterpriseDashboardPage.FilterReportBasedOnRequestDate(requestId);
                    //rOIFacilityEnterpriseDashboardPage.CloseSecondTab();
                }
                
                logger.Log(Status.Pass, "Verified patient does not exist on the request aging details page", TakeScreenShotAtStep());
               
                rOIAdminHomePage.SwitchBackToROITestFacilitySide(BaseAddress);
                rOIFacilityWorkSummaryPage.SearchByRequestId(requestId);
                rOIFacilityRequestStatusPage.UnReleaseRequestID();


                rOIFacilityRequestStatusPage.SelectEnterpriseDashboard();
                rOIFacilityEnterpriseDashboardPage.CreateEnterpriseDashboardReport();
                int count=rOIFacilityEnterpriseDashboardPage.Validatebargraphs();
                if(count>=1)
                {
                    rOIFacilityEnterpriseDashboardPage.SwitchToTab();
                    bool _isPresent = rOIFacilityEnterpriseDashboardPage.FilterReportBasedOnRequestDate(requestId);
                    Assert.IsTrue(_isPresent, "Request id not present", TakeScreenShotAtStep());
                }
                
                logger.Log(Status.Pass, "Verified patient  exist on the request aging details page", TakeScreenShotAtStep());

                string requesterVal = rOIFacilityEnterpriseDashboardPage.VerifyRequesterType();
                Assert.AreEqual(requesterVal, "Patient", "Failed to verify requester type");
                string patientDirected = rOIFacilityEnterpriseDashboardPage.VerifyPatientDirected();
                Assert.AreEqual(patientDirected, "True", "Failed to verify requester type");
                logger.Log(Status.Pass, $"Successfully verified Patient directed marked as {(patientDirected)} and requester type marked as{(requesterVal)}", TakeScreenShotAtStep());
                rOIFacilityEnterpriseDashboardPage.ClickOnCreatedPatient(requestId);

                string shiptoTxt = rOIFacilityRequestStatusPage.VerifyShipTo();
                string emailId = rOIFacilityRequestStatusPage.VerifyEmailId();
                Assert.AreEqual(shiptoTxt, "Test Attorney", "Failed to verify ship to value");
                Assert.AreEqual(emailId, dummyEmailId);
                logger.Log(Status.Pass, $"Successfully verified ship to test on facility request status page  is:{(shiptoTxt)} and email id is:{(emailId)} ", TakeScreenShotAtStep());
                Cleanup(driver);
            }
            catch (Exception ex)
            {
                LogException(driver, logger, ex);

            }
        }
    }

}

