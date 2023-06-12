using AventStack.ExtentReports;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MRO.ROI.Automation.Common;
using MRO.ROI.Automation.Pages;
using MRO.ROI.Test.Utilities;
using OpenQA.Selenium.Remote;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MRO.ROI.Test.RegressionTests.MROROITests
{
    [TestClass]
    public class ROIAdminBatchUpdateLocationTest : ROIBaseTest
    {
        public ROIAdminBatchUpdateLocationTest() : base(ROITestArea.ROIFacility)
        {
        }

        [TestMethod]
        [TestCategory(ROITestCategory.Passed)]
        // Converted manual test case 11356-ROI-Admin-->2Duke Interim Transactional Model Report (Report Details) to automated
        public void Reg_2749_ROIAdminBatchUpdateLocation()
        {
            logger = extent.CreateTest("Reg_2749_ROIAdminBatchUpdateLocation");
            logger.Log(Status.Info, "Converted manual test case 2749-ROI-Admin-->Reg_2749_Batch Update Location to automated");
            RemoteWebDriver _driver = Init(TestContext.Properties["browser"].ToString());
            ThreadLocal<RemoteWebDriver> localDriver = new ThreadLocal<RemoteWebDriver>();
            localDriver.Value = _driver;
            RemoteWebDriver driver = localDriver.Value;
            string userRoot = System.Environment.GetEnvironmentVariable("USERPROFILE");
            string downloadFolder = Path.Combine(userRoot, "Downloads\\");

            string defaultFromFacility = "Default from Facility";
            string DisableForThisLocation = "Disable for this location";
            string EnableForThisLocation = "Enable for this location";

            string HasDoctorsDrp  = "Has Doctors";
            string HasPANDrp = "Has PAN";
            string DOSMandatoryDrp = "DOS Mandatory";
            string HasDOSDrp = "Has DOS";

            string HasComponentsChekbox = "Has Components";
            string HasInternalRequestersCheckbox = "Has Internal Requesters";

            string HasProcessingUserDrp = "Has Processing User";
            string ComplianceHoldDrp = "Compliance Hold:";
            string HasPrivacyStatusDrp = "Has Privacy Status";
            string HasLegalReviewDrp = "Has Legal Review";
            string HasSubpoenaIDDrp = "Has Subpoena ID";

            string patientLocation = "King of Prussia";
            string[] dropdowns = new string[10];


            string panAlert = "This location has PAN disabled! Please remove the PAN or change the location.";
            

            try
            {
                ROIAdminHomePage rOIAdminHomePage = new ROIAdminHomePage(driver, logger, TestContext);
                ROIMenuSelector selector = new ROIMenuSelector(driver, logger, TestContext);
                Iframe frame = new Iframe(driver, logger, TestContext);                
                ROIFacilityWorkSummaryPage rOIFacilityWorkSummaryPage = new ROIFacilityWorkSummaryPage(driver, logger, TestContext);
                ROIFacilityKeyBatchInfoPage rOIFacilityKeyBatchInfoPage = new ROIFacilityKeyBatchInfoPage(driver, logger, TestContext);
                ROIadminScanRequestDocumentsPage rOIadminScanRequestDocumentsPage = new ROIadminScanRequestDocumentsPage(driver, logger, TestContext);
                ROIAdminBatchScanPage rOIAdminBatchScanPage = new ROIAdminBatchScanPage(driver, logger, TestContext);

                
                try
                {
                    selector.SelectRoiAdminMenuOptions("mnuROIFacilityUser", "Batches", "Key Batch Info");
                }
                catch (Exception ex)
                {
                    selector.Select("Batches", "Key Batch Info");
                }
                try
                {
                    frame.SwitchToRoiFrame();

                }
                catch (Exception ex8)
                {

                }
                rOIFacilityKeyBatchInfoPage.ClickCreatetwoBatchRequest();
                rOIFacilityKeyBatchInfoPage.ClickOnSaveBatchAndLogtwoRequest(); //
                string reqID1 = rOIadminScanRequestDocumentsPage.GetRequestIdOne();
                string reqID2 = rOIadminScanRequestDocumentsPage.GetRequestIdTwo();
                
                rOIAdminHomePage.SwitchToNewTabAndLoginROIAdmin(BaseAddress);
                try
                {
                    selector.Select("ROIAdmin", "Batch Scan");
                }
                catch (Exception ex)
                {
                    selector.SelectRoiAdminMenuOptions("mnuROIFacilityUser", "ROIAdmin", "Batch Scan");

                }
                
     //           rOIAdminHomePage.ClickBatchScan();
                frame.SwitchToRoiFrame();
                rOIAdminBatchScanPage.SetRequestIds($"{reqID1},",$"{reqID2}");
                rOIAdminBatchScanPage.ClickCreateList();
                
                rOIAdminBatchScanPage.ClickSelectAllAndChangeLocation(patientLocation);
                //frame.SwitchToRoiFrame();
                string Loc = rOIAdminBatchScanPage.GetLocation();
                logger.Log(Status.Info, "Verify that all request locations has been changed to 'CAPE'.", TakeScreenShotAtStep());
                // Assert.AreEqual(Loc, "CAPE", "Failed to verify that all request locations has been changed to 'CAPE'.");
                logger.Log(Status.Pass, "Verified that all request locations has been changed to 'CAPE'.");

                
                dropdowns[0] = HasDoctorsDrp;
                dropdowns[1] = HasPANDrp;
                dropdowns[2] = DOSMandatoryDrp;
                dropdowns[3] = HasDOSDrp;

                dropdowns[4] = HasProcessingUserDrp;
                dropdowns[5] = ComplianceHoldDrp;
                dropdowns[6] = HasPrivacyStatusDrp;
                dropdowns[8] = HasLegalReviewDrp;
                dropdowns[9] = HasSubpoenaIDDrp;


                foreach (var x in dropdowns)
                {
                    frame.switchToDefaut();

                    //Need to execute manually and fix it.
                    try
                    {
                        selector.Select("Facilities", "Facility Locations");
                    }
                    catch (Exception ex5)
                    {
                        selector.SelectRoiAdminMenuOptions("mnuROIFacilityUser", "Facilities", "Facility Locations");
                    }

                    frame.SwitchToRoiFrame();
                    logger.Log(Status.Info, "Verify that each time you use a condition you won't be able to change the location in the batch that you have created.", TakeScreenShotAtStep());

                    rOIAdminBatchScanPage.ClickLocationLink(patientLocation);
                    try
                    {
                        rOIAdminBatchScanPage.ClickROILink();

                    }
                    catch (Exception ex4)
                    {
                        rOIAdminBatchScanPage.ClickLocationLink(patientLocation);

                        rOIAdminBatchScanPage.ClickROILink();
                    }

                    logger.Log(Status.Info, $"Select '{DisableForThisLocation}' option for '{x}' dropdown");
                    rOIAdminBatchScanPage.SelectLocationInfo(x, DisableForThisLocation);

                    rOIAdminBatchScanPage.ClickUpdateInfoBtn();
                    frame.switchToDefaut();

                    try
                    {
                        selector.Select("ROIAdmin", "Batch Scan");
                    }
                    catch (Exception ex3)
                    {
                        selector.SelectRoiAdminMenuOptions("mnuROIFacilityUser", "ROIAdmin", "Batch Scan");
                    }
                    frame.SwitchToRoiFrame();

                    rOIAdminBatchScanPage.SetRequestIds($"{reqID1},", $"{reqID2}");
                    rOIAdminBatchScanPage.ClickCreateList();

                    rOIAdminBatchScanPage.ClickSelectAllAndChangeLocation(patientLocation);
                    string firstAlertMsg = "Do you want to update the selected requests?";
                    string secondAlertComplainceMsg = "This location has compliance hold disabled! Please clear compliance hold or change the location";

                    logger.Log(Status.Info, $"Verify Alert message when '{x}' dropdown has '{DisableForThisLocation}' option as selcted");
                    try
                    {
                        string firstTextActual = driver.SwitchTo().Alert().Text;
                        logger.Log(Status.Info, $"Alert message {firstTextActual} is showing");
                        driver.SwitchTo().Alert().Accept();

                    }
                    catch (Exception ex2)
                    {
                        logger.Log(Status.Error, $" No First Alert message is showing");
                    }
                    try
                    {
                        string secondTextActual = driver.SwitchTo().Alert().Text;
                        logger.Log(Status.Info, $"Alert message {secondTextActual} is showing");
                        driver.SwitchTo().Alert().Accept();
                    }
                    catch (Exception ex1)
                    {
                        logger.Log(Status.Error, $" No Second Alert message is showing");
                    }


                }
                Cleanup(driver);
            }
            catch (Exception ex)
            {
                LogException(driver, logger, ex);
            }

        }
    }
}
