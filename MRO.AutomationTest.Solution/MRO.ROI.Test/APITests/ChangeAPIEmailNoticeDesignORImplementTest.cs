using AventStack.ExtentReports;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MRO.ROI.Automation.Pages;
using MRO.ROI.Test.Utilities;
using OpenQA.Selenium.Remote;
using System;
using System.Threading;

namespace MRO.ROI.Test.APITests
{
    [TestClass]
    public class ChangeAPIEmailNoticeDesignORImplementTest : ROIBaseTest
    {
        public ChangeAPIEmailNoticeDesignORImplementTest() : base(ROITestArea.ROIAdmin)
        {
        }

        [TestMethod]
        [TestCategory(ROITestCategory.Development)]
        public void Reg_8403_EmailNoticeDesignOrImplement()
        {
            logger = extent.CreateTest("VerifyEmailNoticeDesignOrImplement");
            logger.Log(Status.Info, "Starting execution for VerifyEmailNoticeDesignOrImplement");
            logger.Log(Status.Info, "Step 1- Login to ROI admin");
            RemoteWebDriver _driver = Init(TestContext.Properties["browser"].ToString());
            ThreadLocal<RemoteWebDriver> localDriver = new ThreadLocal<RemoteWebDriver>();
            localDriver.Value = _driver;
            RemoteWebDriver driver = localDriver.Value;
            try
            {
                ROIAdminHomePage rOIAdminHomePage = new ROIAdminHomePage(driver, logger, TestContext);
                rOIAdminHomePage.VerifyHeader();
                logger.Log(Status.Info, "Step4 - Click on facility list");
                rOIAdminHomePage.ClickFacilityList();
                ROIAdminFacilityListPage rOIAdminFacilityListPage = new ROIAdminFacilityListPage(driver, logger, TestContext);
                rOIAdminFacilityListPage.VerifyHeader();
                logger.Log(Status.Info, "Step5 - Click on gear icon where id =1");
                rOIAdminFacilityListPage.ClickOnROITFGearIcon();
                ROIAdminFacilityFeaturesPage rOIAdminFacilityFeaturesPage = new ROIAdminFacilityFeaturesPage(driver, logger, TestContext);
                rOIAdminFacilityFeaturesPage.VerifyHeader();
                logger.Log(Status.Info, "Step6 - Set the value for inbound request endpoint to disabled");
                rOIAdminFacilityFeaturesPage.SetRequeseterAPIEndPointdisable();
                rOIAdminFacilityFeaturesPage.SetEmailConfigurationAndUpdateFeatures();
                logger.Log(Status.Info, "Step7 - Interacting with Postman");
                ROIAPIHelper roiHelper = new ROIAPIHelper(TestContext);
                logger.Log(Status.Info, "Step8,9,10 - Create request an get request id");
                string requestID = roiHelper.CreateRequestAndChangeAPIStatusRequest();
                logger.Log(Status.Info, "Step12 - Search by request id");
                rOIAdminHomePage.SearchByRequestId(requestID);

                Cleanup(driver);
            }
            catch (Exception ex)
            {
                LogException(driver, logger, ex);

            }
        }
    }
}
