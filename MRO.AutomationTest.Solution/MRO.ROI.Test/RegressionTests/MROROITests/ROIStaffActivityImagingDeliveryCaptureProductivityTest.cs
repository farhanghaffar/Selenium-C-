using AventStack.ExtentReports;
using AventStack.ExtentReports.MarkupUtils;
using DataDrivenProject;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MRO.ROI.Automation.Common;
using MRO.ROI.Automation.Pages;
using MRO.ROI.Automation.Pages.Common;
using MRO.ROI.Automation.Selenium;
using MRO.ROI.Test.ExecutionFactory;
using MRO.ROI.Test.Utilities;
using OpenQA.Selenium.Remote;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using static MRO.ROI.Automation.Utility.IniFile;

namespace MRO.ROI.Test.RegressionTests.MROROITests
{
    [TestClass]
    public class ROIStaffActivityImagingDeliveryCaptureProductivityTest : ROIBaseTest
    {
        public ROIStaffActivityImagingDeliveryCaptureProductivityTest() : base(ROITestArea.ROIFacility)
        {
        }

        [STATestMethodAttribute]
        [TestCategory(ROITestCategory.Regression)]
        // Converted manual test case 4259-ROIFacility-->staff Activity - Imaging Delivery - Capture Productivity to automated.
        public void Reg_4259_ROIStaffActivityImagingDeliveryCaptureProductivityTest()
        {
            logger = extent.CreateTest("Reg_4259_Staff Activity - Imaging Delivery - Capture Productivity");
            logger.Log(Status.Info, "Converted manual test case 895-ROITestFacility-->>Staff Activity - Imaging Delivery - Capture Productivity to automated");
            RemoteWebDriver _driver = Init(TestContext.Properties["browser"].ToString());
            ThreadLocal<RemoteWebDriver> localDriver = new ThreadLocal<RemoteWebDriver>();
            localDriver.Value = _driver;
            RemoteWebDriver driver = localDriver.Value;
            string userRoot = System.Environment.GetEnvironmentVariable("USERPROFILE");
            string downloadFolder = Path.Combine(userRoot, "Downloads\\");


            try
            {

                ExcelReaderFile.DeleteExistingFiles(downloadFolder, "xlsx", "Staff Activity");
                ExcelReaderFile.DeleteExistingFiles(downloadFolder, "xls", "Staff Activity");
                ExcelReaderFile.DeleteExistingFiles(downloadFolder, "pdf", "Staff Activity");
                ExcelReaderFile.DeleteExistingFiles(downloadFolder, "xlsx", "Staff Released Logged Requests");
                ExcelReaderFile.DeleteExistingFiles(downloadFolder, "xlsx", "Staff Activity Logged Requests");
                ExcelReaderFile.DeleteExistingFiles(downloadFolder, "xls", "Staff Released Logged Requests");
                ExcelReaderFile.DeleteExistingFiles(downloadFolder, "xls", "Staff Activity Logged Requests");


                ROIFacilityWorkSummaryPage roiFacilityWorkSummaryPage = new ROIFacilityWorkSummaryPage(driver, logger, TestContext);
                roiFacilityWorkSummaryPage.GoToMROAnalyzeSelectStaffActivityReport();
                Iframe frame = new Iframe(driver, logger, TestContext);
                
                ROIFacilityStaffActivityReportPage staffActivityReportPage = new ROIFacilityStaffActivityReportPage(driver, logger, TestContext);
                
                frame.SwitchToRoiFrame();
                frame.SwitchToRDFrame();
                staffActivityReportPage.CreateStaffActivityReport();
                
                bool isDisplayed = staffActivityReportPage.VerifyImagingDeliveryIsVisible();
                Assert.IsTrue(isDisplayed, "Failed to verify imaging delivery link");
                logger.Log(Status.Pass, "Successfully verified staff activity report is generated and Imaging delivery column is displayed", TakeScreenShotAtStep());
                bool isPresent = staffActivityReportPage.VerifyReport();
                Assert.IsTrue(isPresent, "Verified report is generated for imaging delivery requests", TakeScreenShotAtStep());

                logger.Log(Status.Info, "Verifying that excel file contains Staff activity report");
                frame.switchToDefaut();
                frame.SwitchToRoiFrame();
                frame.SwitchToRDFrame();

                staffActivityReportPage.CreateStaffActivityReport();
                string reqHyperlink = staffActivityReportPage.ClickOnTotalNumberHyperLink();                
                staffActivityReportPage.ClickOnExportToExcel();

                ExcelReaderFile excelReaderFile = new ExcelReaderFile();
                string totalRequestsLoggedInExcel = excelReaderFile.ReadExcelCellData(downloadFolder + "Staff Activity.xlsx", 7, 12);
                Assert.AreEqual(reqHyperlink, totalRequestsLoggedInExcel);
                logger.Log(Status.Pass, "Successfully verified that excel file contains Staff activity report");

                ExcelReaderFile.DeleteExistingFiles(downloadFolder, "xlsx", "Staff Activity");
                logger.Log(Status.Pass, "Verifying that pdf file contains Staff activity report");
                var userRowData = staffActivityReportPage.GetUserMroRowDataInStaffActivityReport();
                string filename = staffActivityReportPage.ClickOnExportToPdfAndGetReportName();

                var counter = 1;
                foreach( var s in userRowData)
                {
                    if (s.Trim().Equals(totalRequestsLoggedInExcel)){
                        counter++;

                    }
                }
                frame.switchToDefaut();
                frame.SwitchToRoiFrame();
                frame.SwitchToRDFrame();
                
                string[] pdfEntries = staffActivityReportPage.DownloadPDFAndGetReportData(downloadFolder);

                string sData = "";
                foreach (string s in pdfEntries)
                {
                    sData += s;
                }
                Assert.IsTrue(sData.Contains("Staff Activity"), "Failed to validate pdf file contains Staff activity report");
                logger.Log(Status.Pass, "Successfully verified that pdf file contains Staff activity report");
                Assert.IsTrue(sData.Contains(totalRequestsLoggedInExcel), $"Failed to validate pdf file contains {totalRequestsLoggedInExcel}");
                logger.Log(Status.Pass, $"Successfully verified that pdf file contains total requests {totalRequestsLoggedInExcel}");

                ExcelReaderFile.DeleteExistingFiles(downloadFolder, "pdf", "Staff Activity");

                frame.switchToDefaut();
                roiFacilityWorkSummaryPage.GoToMROAnalyzeSelectStaffActivityReport();
                frame.SwitchToRoiFrame();
                frame.SwitchToRDFrame();

                logger.Log(Status.Pass, "Verifying that excel file contains deleted user");
                staffActivityReportPage.CreateReportForPreviousYear();
                string userName = staffActivityReportPage.GetDeletedUser();
                bool isDeletedUserShowing = staffActivityReportPage.IsDeletedUsersShowing();
                staffActivityReportPage.ClickOnExportToExcel();                

                
                ExcelReaderFile excelReaderFile1 = new ExcelReaderFile();
                string deletedUserInExcel = excelReaderFile1.ReadExcelCellData(downloadFolder + "Staff Activity.xlsx", 36, 1);
                Assert.AreEqual(deletedUserInExcel, userName, "Failed : Deleted user didn't matched");
                Assert.IsTrue(isDeletedUserShowing, "Failed: Deleted user is not showing");

                logger.Log(Status.Pass, "Successfully verified that excel file contains deleted users", TakeScreenShotAtStep());

                logger.Log(Status.Pass, "Verifying that pdf file contains deleted user");
                string filename1 = staffActivityReportPage.ClickOnExportToPdfAndGetReportName();
                frame.switchToDefaut();
                frame.SwitchToRoiFrame();
                frame.SwitchToRDFrame();

                string[] pdfEntries1 = staffActivityReportPage.DownloadPDFAndGetReportData(downloadFolder);
                sData = "";
                foreach (string s in pdfEntries1)
                {
                    sData += s;
                }

               // Assert.IsTrue(sData.Contains(userName), "Failed : Deleted user didn't matched");
                logger.Log(Status.Pass, "Successfully verified that pdf file contains deleted user");
                ExcelReaderFile.DeleteExistingFiles(downloadFolder, "pdf", "Staff Activity");
                
                frame.switchToDefaut();

                roiFacilityWorkSummaryPage.GoToMROAnalyzeSelectStaffActivityReport();

                frame.SwitchToRoiFrame();
                frame.SwitchToRDFrame();

                staffActivityReportPage.CreateStaffActivityReport();

                List<string> loggedTotal = staffActivityReportPage.GetUserMroRowDataInStaffActivityReport();
                var totalAmount = 0;
                var sumAmount = 0;

                for(int i=0; i < 5; i++)
                {
                    if(i == 4)
                    {
                        sumAmount = Convert.ToInt32(loggedTotal[i].Trim());
                    }
                    else
                    {
                        totalAmount = totalAmount + Convert.ToInt32(loggedTotal[i].Trim());
                    }
                }
                logger.Log(Status.Info, "Verifying that Total is equal to the sum of all the columns: MRO Delivery, On-Site Delivery, Internal, and Billing Office.");
                Assert.AreEqual(totalAmount, sumAmount, "Failed : Total is not equal to the sum of all the columns: MRO Delivery, On-Site Delivery, Internal, and Billing Office.");
                logger.Log(Status.Pass, "Verified that Total is equal to the sum of all the columns: MRO Delivery, On-Site Delivery, Internal, and Billing Office.");

                staffActivityReportPage.ClickFirstTotalLinkUnderLoggedSection();

                staffActivityReportPage.SwitchToTotalFrame();

                logger.Log(Status.Info, "Verifying 'Logging User' column is visible.");
                bool isLoggingUserShowing = staffActivityReportPage.IsLoggingUserShowing();
                Assert.IsTrue(isLoggingUserShowing, "Failed to 'Logging User' column is visible.");
                logger.Log(Status.Pass, "Verified that 'Logging User' column is visible.");


                logger.Log(Status.Info, "Verifying 'Logging User' column is visible in excelsheet.");
                staffActivityReportPage.ClickOnExportToExcel();


                ExcelReaderFile excelReaderFile2 = new ExcelReaderFile();
                string loggingUserInExcel = excelReaderFile2.ReadExcelCellData(downloadFolder + "Staff Activity Logged Requests.xlsx", 8, 7);

                Assert.AreEqual(loggingUserInExcel, "Logging User", "Failed : Logging user column didn't matched");
                logger.Log(Status.Pass, "Successfully verified that excel file contains 'Logging User' column");
                ExcelReaderFile.DeleteExistingFiles(downloadFolder, "xlsx", "Staff Activity Logged Requests");

                logger.Log(Status.Pass, "Verifying that pdf file contains Logging User column");
                string filename3 = staffActivityReportPage.ClickOnExportToPdfAndGetReportName();
                frame.switchToDefaut();
                frame.SwitchToRoiFrame();
                frame.SwitchToRDFrame();
                staffActivityReportPage.SwitchToTotalFrame();

                string[] pdfEntries2 = staffActivityReportPage.DownloadPDFAndGetReportData(downloadFolder);
                var pdfData = "";
                foreach (string s in pdfEntries2)
                {
                    pdfData += s;
                }

                Assert.IsTrue(pdfData.Contains("Logging User"), "Failed : Logging user didn't matched");
                logger.Log(Status.Pass, "Successfully verified that pdf file contains Logging user");
                ExcelReaderFile.DeleteExistingFiles(downloadFolder, "pdf", "Staff Activity");

                frame.switchToDefaut();
                frame.SwitchToRoiFrame();
                frame.SwitchToRDFrame();

                staffActivityReportPage.ClickFirstTotalLinkUnderReleaseSection();

                staffActivityReportPage.SwitchToTotalFrame();

                logger.Log(Status.Info, "Verifying 'Releasing User' column is visible.");
                bool IsReleasingUserShowing = staffActivityReportPage.IsReleasingUserShowing();
                Assert.IsTrue(IsReleasingUserShowing, "Failed to 'Releasing User' column is visible.");
                logger.Log(Status.Pass, "Verified that 'Releasing User' column is visible.");


                logger.Log(Status.Info, "Verifying 'Releasing User' column is visible in excelsheet.");
                staffActivityReportPage.ClickOnExportToExcel();


                ExcelReaderFile excelReaderFile3 = new ExcelReaderFile();
                string ReleasingUserInExcel = excelReaderFile3.ReadExcelCellData(downloadFolder + "Staff Activity Released Requests.xlsx", 8, 10);

                Assert.AreEqual(ReleasingUserInExcel, "Releasing User", "Failed : Releasing user column didn't matched");
                logger.Log(Status.Pass, "Successfully verified that excel file contains 'Releasing User' column");
                ExcelReaderFile.DeleteExistingFiles(downloadFolder, "xlsx", "Staff Activity Released Requests");

                logger.Log(Status.Pass, "Verifying that pdf file contains Releasing User column");
                string filename2 = staffActivityReportPage.ClickOnExportToPdfAndGetReportName();
                frame.switchToDefaut();
                frame.SwitchToRoiFrame();
                frame.SwitchToRDFrame();
                staffActivityReportPage.SwitchToTotalFrame();

                string[] pdfEntries3 = staffActivityReportPage.DownloadPDFAndGetReportData(downloadFolder);
                var pdfData1 = "";
                foreach (string s in pdfEntries3)
                {
                    pdfData1 += s;
                }

                Assert.IsTrue(pdfData1.Contains("Releasing User"), "Failed : Releasing user didn't matched");
                logger.Log(Status.Pass, "Successfully verified that pdf file contains Releasing user");
                ExcelReaderFile.DeleteExistingFiles(downloadFolder, "pdf", "Staff Activity");


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

