using AventStack.ExtentReports;
using AventStack.ExtentReports.MarkupUtils;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MRO.ROI.Automation.Pages;
using MRO.ROI.Automation.Selenium;
using MRO.ROI.Test.Utilities;
using OpenQA.Selenium.Remote;
using System;
using System.Threading;

namespace MRO.ROI.Test.APITest
{
    [TestClass]
    public class APIInboundRequestTest: ROIBaseTest
    {
        public APIInboundRequestTest() : base(ROITestArea.ROIAdmin)
        {
        }

        [TestMethod]
        [TestCategory(ROITestCategory.Regression)]
        // Converted manual test case 8307-ROI-Admin-->API Get Call returns the data for all the facilities -->Inbound Request Endpoint=Enabled to automated
        public void Reg_8307_APIGetCallReturnsDataForAllTheFacilities()
        {
            logger = extent.CreateTest("Reg_8307_APIGetCallReturnsDataForAllTheFacilities");
            logger.Log(Status.Info, "Converted manual test case 8307-ROI-Admin-->API Get Call returns the data for all the facilities -->Inbound Request Endpoint=Enabled to automated");
            RemoteWebDriver _driver = Init(TestContext.Properties["browser"].ToString());
            ThreadLocal<RemoteWebDriver> localDriver = new ThreadLocal<RemoteWebDriver>();
            localDriver.Value = _driver;
            RemoteWebDriver driver = localDriver.Value;

            try
            {
                ROIAPIHelper rOIAPIHelper = new ROIAPIHelper(TestContext);
                ROIAdminHomePage rOIAdminHomePage = new ROIAdminHomePage(driver,logger,TestContext);                         
                rOIAdminHomePage.ClickFacilityList();
                ROIAdminFacilityListPage rOIAdminFacilityListPage = new ROIAdminFacilityListPage(driver, logger, TestContext);
                rOIAdminFacilityListPage.ClickOnROITFGearIcon();
                ROIAdminFacilityFeaturesPage rOIAdminFacilityFeaturesPage = new ROIAdminFacilityFeaturesPage(driver, logger, TestContext);
                rOIAdminFacilityFeaturesPage.SetRequeseterAPIEndPointdisable();
                logger.Log(Status.Info, "Roi test facility - Inbound request endpoint disabled");
                rOIAdminHomePage.ClickFacilityList();
                rOIAdminFacilityListPage.ClickOnRothInstFTGearIcon();
                rOIAdminFacilityFeaturesPage.SetRequeseterAPIEndPointdisable();
                logger.Log(Status.Info, "Rothman Institute - Inbound request endpoint disabled");
                var response = rOIAPIHelper.CreateRequestAndGetFacilityName();

                Assert.AreEqual("OK", response.StatusCode.ToString(), $"Response status code is not OK insted it is {response.StatusCode}");
                Assert.AreEqual("[]", response.Content, $"Response content is not empty, Content : {response.Content}");
                logger.Log(Status.Pass, "Response validated for OK and []");

                rOIAdminHomePage.ClickFacilityList();
                rOIAdminFacilityListPage.ClickOnROITFGearIcon();
                rOIAdminFacilityFeaturesPage.SetRequeseterAPIEndPointEnable();
                logger.Log(Status.Info, "Roi test facility - Inbound request endpoint enabled");
                var requestorResponseROIT = rOIAPIHelper.CreateRequestAndGetFacilityName();

                var rootElementROIT = rOIAPIHelper.ParseResponseData(requestorResponseROIT, "ROI Test Facility");
                string ROIT_facilityName = rootElementROIT["facility_name"];
                string ROIT_facilityCode = rootElementROIT["facility_code"];
                Assert.AreEqual("ROI Test Facility", ROIT_facilityName, "Failed to validate roi testfacility");
                Assert.AreEqual("ROIT", ROIT_facilityCode, "Failed to validate roi testfacility code");
                logger.Log(Status.Pass, "Response validated for ROI Test Facility and ROIT");

                rOIAdminHomePage.ClickFacilityList();
                rOIAdminFacilityListPage.ClickOnRothInstFTGearIcon();
                rOIAdminFacilityFeaturesPage.SetRequeseterAPIEndPointEnable();
                logger.Log(Status.Info, "Rothman Institute - Inbound request endpoint enabled");
                var requestorResponseROTH = rOIAPIHelper.CreateRequestAndGetFacilityName();

                var rootElementROTH = rOIAPIHelper.ParseResponseData(requestorResponseROTH, "Rothman Institute");
                string ROTH_facilityName = rootElementROTH["facility_name"];
                string ROTH_facilityCode = rootElementROTH["facility_code"];
                Assert.AreEqual("Rothman Institute", ROTH_facilityName, "Failed to validate roi testfacility");
                Assert.AreEqual("ROTH", ROTH_facilityCode, "Failed to validate roi testfacility code");
                logger.Log(Status.Pass, "Response validated for Rothman Institute and ROTH");

                var _rootElementROIT = rOIAPIHelper.ParseResponseData(requestorResponseROTH, "ROI Test Facility");
                string _ROIT_facilityName = rootElementROIT["facility_name"];
                string _ROIT_facilityCode = rootElementROIT["facility_code"];
                Assert.AreEqual("ROI Test Facility", _ROIT_facilityName, "Failed to validate roi testfacility");
                Assert.AreEqual("ROIT", _ROIT_facilityCode, "Failed to validate roi testfacility code");
                logger.Log(Status.Pass, "Response validated for ROI Test Facility and ROIT");
                Cleanup(driver);
            }
            catch (Exception ex)
            {
                LogException(driver, logger, ex);
            }
        }
    }
}
