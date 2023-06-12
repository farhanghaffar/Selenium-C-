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
    public class ROIAutoInvoiceAutomaticallySendFaxPackageTest : ROIBaseTest
    {
        public ROIAutoInvoiceAutomaticallySendFaxPackageTest() : base(ROITestArea.ROIAdmin)
        {
        }

        [STATestMethodAttribute]
        [TestCategory(ROITestCategory.Regression)]
        // Converted manual test case 3127-ROI-Admin-->Auto-Invoice – automatically send fax package to automated.
        public void Reg_3127_ROIAutoInvoiceAutomaticallySendFaxPackageTest()
        {
            logger = extent.CreateTest("Reg_3127_ROIAutoInvoiceAutomaticallySendFaxPackageTest");
            logger.Log(Status.Info, "Converted manual test case 3127-ROI-Admin-->Auto-Invoice – automatically send fax package to automated.");
            RemoteWebDriver _driver = Init(TestContext.Properties["browser"].ToString());
            ThreadLocal<RemoteWebDriver> localDriver = new ThreadLocal<RemoteWebDriver>();
            localDriver.Value = _driver;
            RemoteWebDriver driver = localDriver.Value;
            

            try
            {
                string givenRequestId = IniHelper.ReadConfig("ROIAutoInvoiceAutomaticallySendFaxPackageTest", "GivenRequestId");
                string faxNo = IniHelper.ReadConfig("ROIAutoInvoiceAutomaticallySendFaxPackageTest", "FaxNumber");

                ROIAdminHomePage rOIAdminHomePage = new ROIAdminHomePage(driver, logger, TestContext);
                rOIAdminHomePage.SelectFacilityList();
                ROIAdminFacilityListPage rOIAdminFacilityListPage = new ROIAdminFacilityListPage(driver, logger, TestContext);
                rOIAdminFacilityListPage.ClickOnROITCompIcon();

                ROIFacilityWorkSummaryPage rOIFacilityWorkSummaryPage = new ROIFacilityWorkSummaryPage(driver, logger, TestContext);
                rOIFacilityWorkSummaryPage.logaNewRequest();
                ROIFacilityLogNewRequestPage rOIFacilityLogNewRequestPage = new ROIFacilityLogNewRequestPage(driver, logger, TestContext);               
                rOIFacilityLogNewRequestPage.ClickMRODeliveryTab();
                rOIFacilityLogNewRequestPage.MRODeliveryRequestForBostonProper();

                LogNewRequestPage logNewRequestPage = new LogNewRequestPage(driver, logger, TestContext);
                string requestId=logNewRequestPage.getRequestid();
                logger.Log(Status.Info, $"MRO delivery request created ({requestId})", TakeScreenShotAtStep());
                ROIFacilityRequestStatusPage rOIFacilityRequestStatusPage = new ROIFacilityRequestStatusPage(driver, logger, TestContext);
                rOIAdminHomePage.ROIlookupByRequestId(requestId);

                rOIFacilityRequestStatusPage.ImportPdfFiles();
                rOIFacilityRequestStatusPage.ReleaseRequestID();
                logger.Log(Status.Info, "Request released");

                rOIAdminHomePage.SwitchToNewTabAndLoginROIAdmin(BaseAddress);
                rOIAdminHomePage.SearchByRequestId(givenRequestId);
                ROIAdminRequestStatusPage rOIAdminRequestStatusPage = new ROIAdminRequestStatusPage(driver, logger, TestContext);
                rOIAdminRequestStatusPage.ClickOnTestAttroney();

                ROIAdminEditRequesterInfoPage rOIAdminEditRequesterInfoPage = new ROIAdminEditRequesterInfoPage(driver, logger, TestContext);
                bool _editRequesterInfoPageHeader = rOIAdminEditRequesterInfoPage.VerifyEditRequesterInfoPage();
                Assert.IsTrue(_editRequesterInfoPageHeader, "Failed to verify edit requester info page");
                logger.Log(Status.Info, "Verified that edit requester info page opened", TakeScreenShotAtStep());

                rOIAdminEditRequesterInfoPage.ClearFaxNumber();
                rOIAdminEditRequesterInfoPage.SelectInvoiceDelivery("FAX");
                rOIAdminEditRequesterInfoPage.SelectDefaultShipmentType("Paper Mailed to Requester");
                string infoMsg = rOIAdminEditRequesterInfoPage.GetRequesterInfoMsg();
                Assert.AreEqual(infoMsg, "Requester Info Updated!");
                logger.Log(Status.Info, "Verified that requester info updated message is displayed", TakeScreenShotAtStep());

                rOIAdminHomePage.SearchByRequestId(requestId);
                rOIAdminRequestStatusPage.assignRequester();
                ROIAdminAssignROIRequesterPage rOIAdminAssignROIRequesterPage = new ROIAdminAssignROIRequesterPage(driver, logger, TestContext);
                rOIAdminAssignROIRequesterPage.assignTestAttorney();

                rOIAdminRequestStatusPage.ClickOnUpdateInfoForRequester();
                rOIAdminRequestStatusPage.EnterFaxNumber(faxNo);
                bool isDisplayed=rOIAdminRequestStatusPage.VerifyFaxNumber();
                Assert.IsTrue(isDisplayed, "Failed to verify fax number");
                logger.Log(Status.Info, "Verified that fax number is displayed under requester", TakeScreenShotAtStep());

                rOIAdminRequestStatusPage.ClickOnQcPassButton();
                logger.Log(Status.Info, "Step-14 we need to wait for 15 to 20 mins for generating auto-invoice this step not feasiable for automation ,needs to be executed manually");

                Cleanup(driver);
            }

            catch (Exception ex)
            {
               
                LogException(driver, logger, ex);

            }
        }
    }

}

