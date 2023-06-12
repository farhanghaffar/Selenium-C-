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
    public class ROIBOEPortalPendingReportAddRequestStatusFilterAndDailyShipmentsReportAddRequestStatusFilterTest : ROIBaseTest
    {
        public ROIBOEPortalPendingReportAddRequestStatusFilterAndDailyShipmentsReportAddRequestStatusFilterTest() : base(ROITestArea.CBOTestFacility)
        {
        }

        [STATestMethodAttribute]
        [TestCategory(ROITestCategory.Regression)]
        // Converted manual test case 14304-ROI-Admin-->BOE Portal Pending Report- add Request Status filter and Daily Shipments Report- add Request Status Filterto automated.
        public void Reg_14304_ROIBOEPortalPendingReportAddRequestStatusFilterAndDailyShipmentsReportAddRequestStatusFilterTest()
        {
            logger = extent.CreateTest("Reg_14304_ROIBOEPortalPendingReportAddRequestStatusFilterAndDailyShipmentsReportAddRequestStatusFilterTest");
            logger.Log(Status.Info, "Converted manual test case 14304-ROI-Admin-->BOE Portal Pending Report- add Request Status filter and Daily Shipments Report- add Request Status Filter.");
            RemoteWebDriver _driver = Init(TestContext.Properties["browser"].ToString());
            ThreadLocal<RemoteWebDriver> localDriver = new ThreadLocal<RemoteWebDriver>();
            localDriver.Value = _driver;
            RemoteWebDriver driver = localDriver.Value;
            string userRoot = System.Environment.GetEnvironmentVariable("USERPROFILE");
            string downloadFolder = Path.Combine(userRoot, "Downloads\\");


            try
            {
                ExcelReaderFile.DeleteExistingFiles(downloadFolder, "xlsx", "DailyShipmentsReport_2021*");
                ExcelReaderFile.DeleteExistingFiles(downloadFolder, "xls", "DailyShipmentsReport_2021*");
                ExcelReaderFile.DeleteExistingFiles(downloadFolder, "xls", "roifac_CBOPendingReport");
                ExcelReaderFile.DeleteExistingFiles(downloadFolder, "xlsx", "roifac_CBOPendingReport");

                ROIFacilityWorkSummaryPage rOIFacilityWorkSummaryPage = new ROIFacilityWorkSummaryPage(driver, logger, TestContext);
                rOIFacilityWorkSummaryPage.SelectDailyShipmentsReport();
                ROIDailyShipmentsReportPage rOIDailyShipmentsReportPage = new ROIDailyShipmentsReportPage(driver, logger, TestContext);
                bool isRSSDrp=rOIDailyShipmentsReportPage.VerifyRequestStatusDropdown();
                Assert.IsTrue(isRSSDrp, "Failed to verify request status dropdown");
                bool isValuesDisplayed=rOIDailyShipmentsReportPage.VerifyRequestStatusValues();
                Assert.IsTrue(isValuesDisplayed, "Failed to verify request status dropdown values");

                logger.Log(Status.Pass, "Verified that request status dropdown contains values",TakeScreenShotAtStep());
                rOIDailyShipmentsReportPage.CreateReportForClosedStatus("Closed");
                logger.Log(Status.Info, "Verified that report generated successfully", TakeScreenShotAtStep());
                rOIDailyShipmentsReportPage.ClickOnExcelIcon();

                //var reportFiles = Directory.GetFiles(downloadFolder, "DailyShipmentsReport_2021*.xlsx", SearchOption.TopDirectoryOnly);
                //if(reportFiles.Length>=1)
                //{
                //    ExcelReaderFile excelReaderFile1 = new ExcelReaderFile();
                //    ExcelReaderFile.DeleteExistingFiles(downloadFolder, "xlsx", "DailyShipmentsReport_2021*");
                //}

                //ExcelReaderFile _excelReaderFile2 = new ExcelReaderFile(downloadFolder +"DailyShipmentsReport_2021*.xlsx");
                //int excelRowcount = _excelReaderFile2.getRowCount("Sheet1");
                //Assert.AreEqual(1, excelRowcount);


                rOIDailyShipmentsReportPage.CreateReportForClosedStatus("Delivered");
                logger.Log(Status.Info, "Verified that report generated successfully", TakeScreenShotAtStep());
                rOIDailyShipmentsReportPage.ClickOnExcelIcon();


                rOIDailyShipmentsReportPage.CreateReportForClosedStatus("Not Released");
                logger.Log(Status.Info, "Verified that report generated successfully", TakeScreenShotAtStep());
                rOIDailyShipmentsReportPage.ClickOnExcelIcon();

                rOIDailyShipmentsReportPage.CreateReportForClosedStatus("Processing");
                logger.Log(Status.Info, "Verified that report generated successfully", TakeScreenShotAtStep());
                rOIDailyShipmentsReportPage.ClickOnExcelIcon();

                rOIDailyShipmentsReportPage.CreateReportForClosedStatus("Cancelled");
                logger.Log(Status.Info, "Verified that report generated successfully", TakeScreenShotAtStep());
                rOIDailyShipmentsReportPage.ClickOnExcelIcon();

                rOIFacilityWorkSummaryPage.SelectPendingReport();
                logger.Log(Status.Info, "Verified that pending report page opened successfully", TakeScreenShotAtStep());

                ROIPendingReportPage rOIPendingReportPage = new ROIPendingReportPage(driver, logger, TestContext);
                rOIPendingReportPage.VerifyRequestStatusDropdown();
                Assert.IsTrue(isRSSDrp, "Failed to verify request status dropdown");
                bool isValuesDisplayed1 = rOIPendingReportPage.VerifyRequestStatusValues();
                Assert.IsTrue(isValuesDisplayed1, "Failed to verify request status dropdown values");
               

                bool isOnHoldChkBoxDisplayed=rOIPendingReportPage.VerifyOnHoldChkbox();
                Assert.IsTrue(isOnHoldChkBoxDisplayed, "Failed to verify onhold checkbox");
                logger.Log(Status.Pass, "Verified that request status dropdown contains values and on hold check box is displayed", TakeScreenShotAtStep());

                rOIPendingReportPage.CreateReport();
                rOIPendingReportPage.CreateReportWithRequestStatus("Not Released");
                logger.Log(Status.Info, "Verified that report generated successfully", TakeScreenShotAtStep());
                

                string uiRows=rOIPendingReportPage.GetNumberOfRowsFromUI();
                int rows = Convert.ToInt32(uiRows);
                rOIPendingReportPage.ClickOnExcelIcon();
                ExcelReaderFile excelReaderFile = new ExcelReaderFile();
                excelReaderFile.ConvertXLS_XLSX(downloadFolder + "roifac_CBOPendingReport.xls");
                ExcelReaderFile.DeleteExistingFiles(downloadFolder, "xls", "roifac_CBOPendingReport");
                driver.Wait(TimeSpan.FromSeconds(5));


                ExcelReaderFile _excelReaderFile1 = new ExcelReaderFile(downloadFolder + "roifac_CBOPendingReport.xlsx");
                int excelRowcount1 = _excelReaderFile1.getRowCount("roifac_CBOPendingReport");
                Assert.AreEqual(excelRowcount1-1,rows,"Failed to verify rows count");
                ExcelReaderFile.DeleteExistingFiles(downloadFolder, "xlsx", "roifac_CBOPendingReport");


                rOIPendingReportPage.CreateReportWithRequestStatus("Processing");
                string releasedDate = rOIPendingReportPage.VerifyReleasesColumn();
                logger.Log(Status.Info, $"Verified that report generated successfully and released column contains date like {(releasedDate)}", TakeScreenShotAtStep());


                rOIPendingReportPage.CreateReportWithOutRequestStatus();
                logger.Log(Status.Info, "Verified that report generated successfully", TakeScreenShotAtStep());
                rOIPendingReportPage.ClickOnExcelIcon();


                ExcelReaderFile excelReaderFile2 = new ExcelReaderFile();
                excelReaderFile2.ConvertXLS_XLSX(downloadFolder + "roifac_CBOPendingReport.xls");
                ExcelReaderFile.DeleteExistingFiles(downloadFolder, "xls", "roifac_CBOPendingReport");
                driver.Wait(TimeSpan.FromSeconds(5));


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

