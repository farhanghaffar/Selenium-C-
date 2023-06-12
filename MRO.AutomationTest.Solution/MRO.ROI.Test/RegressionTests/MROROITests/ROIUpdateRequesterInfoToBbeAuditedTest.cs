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
    public class ROIUpdateRequesterInfoToBbeAuditedTest : ROIBaseTest
    {
        public ROIUpdateRequesterInfoToBbeAuditedTest() : base(ROITestArea.ROITestFacility)
        {
        }

        [STATestMethodAttribute]
        [TestCategory(ROITestCategory.Regression)]
        // Converted manual test case 4337-ROI-Facility-->Test Case 4337: Update Requester Info to be Audited to automated.
        public void Reg_4337_ROIUpdateRequesterInfoToBbeAuditedTest()
        {
            logger = extent.CreateTest("Reg_4337_ROIUpdateRequesterInfoToBbeAuditedTest");
            logger.Log(Status.Info, "Converted manual test case 4337 - ROI - Facility-- > Test Case 4337: Update Requester Info to be Audited to automated.");
            RemoteWebDriver _driver = Init(TestContext.Properties["browser"].ToString());
            ThreadLocal<RemoteWebDriver> localDriver = new ThreadLocal<RemoteWebDriver>();
            localDriver.Value = _driver;
            RemoteWebDriver driver = localDriver.Value;
            try
            {
                string fname = IniHelper.ReadConfig("ROIUpdateRequesterInfoToBbeAuditedTest", "Firstname");
                string lname = IniHelper.ReadConfig("ROIUpdateRequesterInfoToBbeAuditedTest", "LastName");
                string phno = IniHelper.ReadConfig("ROIUpdateRequesterInfoToBbeAuditedTest", "PhoneNumber");
                string emailId = IniHelper.ReadConfig("ROIUpdateRequesterInfoToBbeAuditedTest", "EmailId");
                string fax = IniHelper.ReadConfig("ROIUpdateRequesterInfoToBbeAuditedTest", "FaxNo");
                string address = IniHelper.ReadConfig("ROIUpdateRequesterInfoToBbeAuditedTest", "Address");
                string state = IniHelper.ReadConfig("ROIUpdateRequesterInfoToBbeAuditedTest", "State");
                string city = IniHelper.ReadConfig("ROIUpdateRequesterInfoToBbeAuditedTest", "City");
                string zipCode = IniHelper.ReadConfig("ROIUpdateRequesterInfoToBbeAuditedTest", "Zipcode");

                ROIFacilityWorkSummaryPage rOIFacilityWorkSumaryPage = new ROIFacilityWorkSummaryPage(driver, logger, TestContext);
                rOIFacilityWorkSumaryPage.logaNewRequest();
                ROIAdminHomePage rOIAdminHomePage = new ROIAdminHomePage(driver, logger, TestContext);
                ROIFacilityLogNewRequestPage rOIFacilityLogNewRequestPage = new ROIFacilityLogNewRequestPage(driver, logger, TestContext);
                rOIFacilityLogNewRequestPage.MRODeliveryRequestForBostonProper();
                ROIFacilityRequestStatusPage rOIFacilityRequestStatusPage = new ROIFacilityRequestStatusPage(driver, logger, TestContext);
                string requestID = rOIFacilityRequestStatusPage.GetRequestID();
                logger.Log(Status.Pass, $"Request created with requestid ({requestID})", TakeScreenShotAtStep());
                rOIAdminHomePage.ROIlookupByRequestId(requestID);
                rOIFacilityRequestStatusPage.ImportPdfFiles();
                rOIFacilityRequestStatusPage.ReleaseRequestID();
                logger.Log(Status.Info, "Request released");
                rOIAdminHomePage.SwitchToNewTabAndLoginROIAdmin(BaseAddress);
                rOIAdminHomePage.SearchByRequestId(requestID);
                ROIAdminRequestStatusPage rOIAdminRequestStatusPage = new ROIAdminRequestStatusPage(driver, logger, TestContext);
                rOIAdminRequestStatusPage.assignRequester();
                ROIAdminAssignROIRequesterPage rOIAdminAssignROIRequester = new ROIAdminAssignROIRequesterPage(driver, logger, TestContext);
                rOIAdminAssignROIRequester.AssignRequester("AJ's test requester");
                rOIAdminRequestStatusPage.ClickOnTestAttroney();
                ROIAdminEditRequesterInfoPage rOIAdminEditRequesterInfoPage = new ROIAdminEditRequesterInfoPage(driver, logger, TestContext);
                bool _editRequesterInfoPageHeader = rOIAdminEditRequesterInfoPage.VerifyEditRequesterInfoPage();
                Assert.IsTrue(_editRequesterInfoPageHeader, "Failed to verify edit requester info page");
                logger.Log(Status.Info, "Verified that edit requester info page opened", TakeScreenShotAtStep());

                string ActualFname = rOIAdminEditRequesterInfoPage.GetFirstName();
                string ActualLname = rOIAdminEditRequesterInfoPage.GetLastName();
                string ActualPhoneNo = rOIAdminEditRequesterInfoPage.GetPhoneNumber();
                string ActualEmailId = rOIAdminEditRequesterInfoPage.GetEmailValue();
                rOIAdminEditRequesterInfoPage.UpdateRequesterData(fname, lname, phno, emailId);
                string infoMsg = rOIAdminEditRequesterInfoPage.GetRequesterInfoMsg();
                Assert.AreEqual(infoMsg, "Requester Info Updated!");
                logger.Log(Status.Info, "Verified that requester info updated message is displayed", TakeScreenShotAtStep());
                rOIAdminRequestStatusPage.ClickOnAuditLog();
                ROIAdminAuditLogPage rOIAdminAuditLogPage = new ROIAdminAuditLogPage(driver, logger, TestContext);
                rOIAdminAuditLogPage.SearchRequestOnAuditLog("[All]", "Requester Edit", requestID);
                string requesterInfo = rOIAdminAuditLogPage.VerifyRequestInfo();
                logger.Log(Status.Pass, $"Verified that search returns the time and date with requester info like :{(requesterInfo)}", TakeScreenShotAtStep());
                rOIAdminAuditLogPage.SearchByRequestId(requestID);
                rOIAdminRequestStatusPage.ClickOnTestAttroney();
                rOIAdminEditRequesterInfoPage.UpdateRequesterAddress(address, state, city, fax, zipCode);
                string infoMsg1 = rOIAdminEditRequesterInfoPage.GetRequesterInfoMsg();
                Assert.AreEqual(infoMsg1, "Requester Info Updated!");
                logger.Log(Status.Info, "Verified that requester info updated message is displayed", TakeScreenShotAtStep());
                rOIAdminRequestStatusPage.ClickOnAuditLog();
                rOIAdminAuditLogPage.SearchRequestOnAuditLog("[All]", "Requester Edit", requestID);
                string requesterInfo1 = rOIAdminAuditLogPage.VerifyRequestInfo();
                logger.Log(Status.Pass, $"Verified that search returns the time and date with requester info like :{(requesterInfo1)}", TakeScreenShotAtStep());
                rOIAdminAuditLogPage.SearchByRequestId(requestID);

                rOIAdminRequestStatusPage.ClickOnTestAttroney();
                rOIAdminEditRequesterInfoPage.UpdateRequesterData("Test", "Tester1", "610-994-7000", "AJtest1@mrocorp.com");
                rOIAdminEditRequesterInfoPage.UpdateRequesterAddress("1000 Madison Ave Ste 1000", "PA", "Norristown", "610-994-7509", "19335");
                rOIAdminEditRequesterInfoPage.ClickOnReturnToRequest();
                rOIAdminRequestStatusPage.ClickOnReAssignROIRequester();
                var todaysDate = String.Format("{0:M/dd/yyyy}", DateTime.Now).Replace("-", "/");
                rOIAdminAssignROIRequester.UpdateRequesterInformation("Test Attorney's", fname, lname, phno, fax, emailId, todaysDate);

                string reAssignRequesterValue = rOIAdminRequestStatusPage.VerifyReAssignRequester();
                Assert.AreEqual(reAssignRequesterValue, "TEST Attorney's");
                logger.Log(Status.Info, $"Succcessfully verified at the request status page re-assign requester ({reAssignRequesterValue})");
                rOIAdminRequestStatusPage.ClickOnAuditLog();
                rOIAdminAuditLogPage.SearchRequestOnAuditLog("[All]", "Requester Edit", requestID);
                string requesterInfo2 = rOIAdminAuditLogPage.VerifyRequestInfo();
                logger.Log(Status.Pass, $"Verified that search returns the  info like :{(requesterInfo2)}", TakeScreenShotAtStep());

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

