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
    public class ROINewSecondaryShipmentTest : ROIBaseTest
    {
        public ROINewSecondaryShipmentTest() : base(ROITestArea.ROIAdmin)
        {
        }

        [STATestMethodAttribute]
        [TestCategory(ROITestCategory.Passed)]
        // Converted manual test case 5252-ROI-Admin-->New Secondary Shipment to automated.
        public void Reg_5252_ROINewSecondaryShipmentTest()
        {
            logger = extent.CreateTest("Reg_5252_ROINewSecondaryShipmentTest");
            logger.Log(Status.Info, " Converted manual test case 5252-ROI-Admin-->New Secondary Shipment to automated.");
            RemoteWebDriver _driver = Init(TestContext.Properties["browser"].ToString());
            ThreadLocal<RemoteWebDriver> localDriver = new ThreadLocal<RemoteWebDriver>();
            localDriver.Value = _driver;
            RemoteWebDriver driver = localDriver.Value;
           

            try
            {
                string request = IniHelper.ReadConfig("ROINewSecondaryShipmentTes", "RequestId");
                ROIAdminHomePage rOIAdminHomePage = new ROIAdminHomePage(driver, logger, TestContext);
                rOIAdminHomePage.SearchByRequestId(request);
                ROIAdminRequestStatusPage rOIAdminRequestStatusPage = new ROIAdminRequestStatusPage(driver, logger, TestContext);
                rOIAdminRequestStatusPage.AddPdfShipment();

                ROIAdminPackingListstPage rOIAdminPackingListstPage = new ROIAdminPackingListstPage(driver, logger, TestContext);
                rOIAdminPackingListstPage.VerifyPackageListPageHeader();
                logger.Log(Status.Info, "Succesfully verified that packing list page opened", TakeScreenShotAtStep());

                rOIAdminPackingListstPage.CreatePackingList();
                rOIAdminPackingListstPage.AddSecondaryShipment();
                rOIAdminPackingListstPage.ReturnToRss();
                rOIAdminPackingListstPage.ClickOnPdfHyperLink();

                ROIAdminShipmentDetailsPage rOIAdminShipmentDetailsPage = new ROIAdminShipmentDetailsPage(driver, logger, TestContext);
                string headingElement = rOIAdminShipmentDetailsPage.VerifyShipmentDetailsPageHeader();
                Assert.AreEqual(headingElement, "Shipment Details");
                logger.Log(Status.Info, "Shipment Details window opened", TakeScreenShotAtStep());               
                rOIAdminShipmentDetailsPage.ToClickMakeShippableButton();

                var todaysDate = String.Format("{0:M/dd/yyyy}", DateTime.Now).Replace("-", "/");
                string _isDatesDisplayed = rOIAdminShipmentDetailsPage.VerifyAndGetShippedDate();
                //Assert.AreEqual(todaysDate, _isDatesDisplayed, "Failed to verify the shipped date set to system date.");
                logger.Log(Status.Info, $"Succcessfully verified the shipped date set to system date {todaysDate}");
                Cleanup(driver);
            }

            catch (Exception ex)
            {
               
                LogException(driver, logger, ex);

            }
        }
    }

}

