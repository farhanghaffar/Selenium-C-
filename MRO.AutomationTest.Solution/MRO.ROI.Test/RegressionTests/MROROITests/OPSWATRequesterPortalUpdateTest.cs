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
    public class OPSWATRequesterPortalUpdateTest : ROIBaseTest
    {
        public OPSWATRequesterPortalUpdateTest() : base(ROITestArea.ROIAdmin)
        {
        }

        [STATestMethodAttribute]
        [TestCategory(ROITestCategory.Regression)]
        //Converted manual test case 12520- ROI Requester Portal-->OPSWAT Requester Portal update Test to automated
        public void Reg_12520_OPSWATRequesterPortalUpdateTest()
        {
            logger = extent.CreateTest("Reg_12520_OPSWATRequesterPortalUpdateTest");
            logger.Log(Status.Info, "Converted manual test case 12520- ROI Requester Portal-->OPSWAT Requester Portal update Test to automated");
            RemoteWebDriver _driver = Init(TestContext.Properties["browser"].ToString());
            ThreadLocal<RemoteWebDriver> localDriver = new ThreadLocal<RemoteWebDriver>();
            localDriver.Value = _driver;
            RemoteWebDriver driver = localDriver.Value;
            try

            {
                ROIAdminHomePage rOIAdminHomePage = new ROIAdminHomePage(driver, logger, TestContext);
                rOIAdminHomePage.SelectSystemInfo();
                ROISystemInfoPage rOISystemInfoPage = new ROISystemInfoPage(driver, logger, TestContext);
                rOISystemInfoPage.EnableOPSWATCheckBox();
                logger.Log(Status.Info, "OPSWAT option enabled", TakeScreenShotAtStep());

                rOIAdminHomePage.SwitchToROIRequesterPortal(BaseAddress);
                ROITestRequesterPortalHomePage rOITestRequesterPortalHomePage = new ROITestRequesterPortalHomePage(driver, logger, TestContext);
                rOITestRequesterPortalHomePage.ClickOnNotificationPopUp();
                rOITestRequesterPortalHomePage.GotoRequestRecords();
                ROICreateRequestPage rOICreateRequestPage = new ROICreateRequestPage(driver, logger, TestContext);
                rOICreateRequestPage.SeelectRecentlyUsedFacility();                                
                logger.Log(Status.Pass, "Create request page updated with selected facility information", TakeScreenShotAtStep());

                rOICreateRequestPage.CreateRequest();
                string reqId = rOICreateRequestPage.ReturnRequestId();
                logger.Log(Status.Pass, $"Request created with id:({reqId})", TakeScreenShotAtStep());

                rOIAdminHomePage.SwitchToNewTabAndLoginROIAdmin(BaseAddress);
                rOIAdminHomePage.SearchByRequestId(reqId);
                ROIAdminRequestStatusPage rOIAdminRequestStatusPage = new ROIAdminRequestStatusPage(driver, logger, TestContext);
                rOIAdminRequestStatusPage.ClickAddIssueBtn();
                ROIAdminAddIssuePage rOIAdminAddIssuePage = new ROIAdminAddIssuePage(driver, logger, TestContext);
                string issueType=rOIAdminAddIssuePage.AddIssue();
                logger.Log(Status.Pass, "Issue added",TakeScreenShotAtStep());

                rOIAdminHomePage.SwitchBackToRequesterPortal(BaseAddress);
                rOITestRequesterPortalHomePage.ClickOnNotificationPopUp();
                rOITestRequesterPortalHomePage.SearchRequestId(reqId);
                string issue= rOITestRequesterPortalHomePage.ViewOpenIssue();
                Assert.AreEqual(issueType, issue);
                rOICreateRequestPage.UploadPdf();                

                //Disable OPSWAT option
                rOIAdminHomePage.SwitchToAdminTab(BaseAddress);
                rOIAdminHomePage.SelectSystemInfo();
                rOISystemInfoPage.DisableOPSWATCheckBox();
                logger.Log(Status.Info, "Successfully deactivated   OPSWAT option", TakeScreenShotAtStep());

                rOIAdminHomePage.SwitchBackToRequesterPortal(BaseAddress);
                rOITestRequesterPortalHomePage.ClickOnNotificationPopUp();
                rOITestRequesterPortalHomePage.SearchRequestId(reqId);
                string fileName= rOICreateRequestPage.CheckPdfFileStatus();
                logger.Log(Status.Pass, $"pdf file uploaded successfully :({fileName})", TakeScreenShotAtStep());
                
                Cleanup(driver);
            }
            catch (Exception ex)
            {
                LogException(driver, logger, ex);

            }
        }
    }

}

