using AventStack.ExtentReports;
using DataDrivenProject;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MRO.ROI.Automation.Pages;
using MRO.ROI.Automation.Pages.Common;
using MRO.ROI.Test.ExecutionFactory;
using MRO.ROI.Test.Utilities;
using OpenQA.Selenium.Remote;
using System;
using System.IO;
using System.Threading;

namespace MRO.ROI.Test.RegressionTests.MROROITests
{ 

    [TestClass]
    public class ROI2TopPageErrorsTest : ROIBaseTest
    {
        public ROI2TopPageErrorsTest(): base(ROITestArea.ROIExternalRequesterPortal)
        {

        }
        [STATestMethodAttribute]
        [TestCategory(ROITestCategory.Development)]
        public void Reg_5345__2TopPageErrorsTest()
        {
            logger = extent.CreateTest(" Reg_5345_2TopPageErrorsTest");
            logger.Log(Status.Info, "Converted manual test case 5345__2TopPageErrorsTest");
            RemoteWebDriver _driver = Init(TestContext.Properties["browser"].ToString());
            ThreadLocal<RemoteWebDriver> localDriver = new ThreadLocal<RemoteWebDriver>();
            localDriver.Value = _driver;
            RemoteWebDriver driver = localDriver.Value;
            try
            {
                ROIAdminFeaturesPage rOIAdminFeaturePage = new ROIAdminFeaturesPage(driver, logger, TestContext);
                ROIPayForRecordsPage rOIPayForRecordsPage = new ROIPayForRecordsPage(driver, logger, TestContext);
                ROITestRequesterPortalHomePage rOITestRequesterPortalHomePage = new ROITestRequesterPortalHomePage(driver, logger, TestContext);
                ROICreateRequestPage rOICreateRequestPage = new ROICreateRequestPage(driver, logger, TestContext);
                ROIAdminHomePage rOIAdminHomePage = new ROIAdminHomePage(driver, logger, TestContext);
                ROICBORequestStatusPage rOICBORequestStatusPage = new ROICBORequestStatusPage(driver, logger, TestContext);
                rOITestRequesterPortalHomePage.ClickOnNotificationPopUp();
                rOITestRequesterPortalHomePage.GotoPayForRecords();
                rOIPayForRecordsPage.ClickRangeShowRequestButton();
                rOIAdminFeaturePage.ClickOnRqrPortalComposeLinks();


                rOIAdminHomePage.SwitchToNewTabAndLoginROIAdmin(BaseAddress);

                rOICBORequestStatusPage.SelectRecentRequest();

            }
            catch (Exception ex)
            {

                LogException(driver, logger, ex);
            }
        }
    }
}

