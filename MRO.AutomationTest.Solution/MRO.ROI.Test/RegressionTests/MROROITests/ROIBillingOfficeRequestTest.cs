using AventStack.ExtentReports;
using AventStack.ExtentReports.MarkupUtils;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MRO.ROI.Automation.Common;
using MRO.ROI.Automation.Pages;
using MRO.ROI.Automation.Selenium;
using MRO.ROI.Automation.Utility;
using MRO.ROI.Test.ExecutionFactory;
using MRO.ROI.Test.Utilities;
using OpenQA.Selenium.Remote;
using System;
using System.Threading;

namespace MRO.ROI.Test.RegressionTests.MROROITests
{
    [TestClass]
    public class ROIBillingOfficeRequestTest : ROIBaseTest
    {
        public ROIBillingOfficeRequestTest() : base(ROITestArea.ROIFacility)
        {
        }
        [STATestMethodAttribute]
        [TestCategory(ROITestCategory.Regression)]
        //Converted manual test case 11233-ROI-Admin-->Unique Identifier for Billing Office Requests to automated
        public void Reg_11233_UniqueIdentifierforBillingOfficeRequests()
        {

            logger = extent.CreateTest("Reg_11233_UniqueIdentifierforBillingOfficeRequests");
            logger.Log(Status.Info, "Converted manual test case 11233-ROI-Admin-->Unique Identifier for Billing Office Requests to automated");
            RemoteWebDriver _driver = Init(TestContext.Properties["browser"].ToString());
            ThreadLocal<RemoteWebDriver> localDriver = new ThreadLocal<RemoteWebDriver>();
            localDriver.Value = _driver;
            RemoteWebDriver driver = localDriver.Value;
            string BOEPortal = "Risk Management";

            try
            {
                ROIAdminHomePage rOIAdminHomePage = new ROIAdminHomePage(driver, logger, TestContext);
                Iframe frame = new Iframe(driver, logger, TestContext);
/*
                rOIAdminHomePage.SelectFacilityList();
                rOIAdminFacilityListPage.GoToROITestFacility();
*/
                ROIAdminFacilityListPage rOIAdminFacilityListPage = new ROIAdminFacilityListPage(driver, logger, TestContext);
                ROIFacilityWorkSummaryPage rOIFacilityWorkSummaryPage = new ROIFacilityWorkSummaryPage(driver, logger, TestContext);
                rOIFacilityWorkSummaryPage.logaNewRequest();
                ROIFacilityLogNewRequestPage rOIFacilityLogNewRequestPage = new ROIFacilityLogNewRequestPage(driver, logger, TestContext);
                frame.SwitchToRoiFrame();
                rOIFacilityLogNewRequestPage.CreateBillingOfficeRequest(BOEPortal);
                ROIFacilityRequestStatusPage rOIFacilityRequestStatusPage = new ROIFacilityRequestStatusPage(driver, logger, TestContext);
                string requestid = rOIFacilityRequestStatusPage.GetRequestID();
                logger.Log(Status.Info, $"ROI request created with id ({requestid})", TakeScreenShotAtStep());
                frame.SwitchToRoiFrame();
                rOIFacilityRequestStatusPage.ScanPatientPage();
                //rOIFacilityRequestStatusPage.ImportPdfFiles();
                //rOIAdminHomePage.ROIlookupByRequestId(requestid);

                // bool statusUnderImportDocument = rOIFacilityRequestStatusPage.VerifyStatusUnderImportDocument();
                // frame.SwitchToRoiFrame();
                bool statusUnderImportDocument = rOIFacilityRequestStatusPage.VerifyStatusForScannedDocument();
                Assert.IsTrue(statusUnderImportDocument, "Failed to verify status for scanned document is uploaded");
                logger.Log(Status.Pass, "Successfully verified status for scanned documents is uploaded", TakeScreenShotAtStep());

                //logger.Log(Status.Info, $"Execute this query manually as database validations are not incorporated - Use SQL Query (USE MRO_ROI Select nBOErequesterID,* From tblRequests WITH (NOLOCK) where nRequestID ='{requestid}')");
               
                logger.Log(Status.Info, $"Verify the nBOERequest ID has been created successfully for ROI request with id ({requestid})", TakeScreenShotAtStep());

                string query = $"Select nBOErequesterID,* From tblRequests WITH (NOLOCK) where nRequestID ='{requestid}';";
                string boeReuqest = MRODBConnection.GetQueryResult(query);
                if (boeReuqest.ToLower().Equals("null"))
                {
                    Assert.IsTrue(false, $"Failed : nBOErequesterID ({boeReuqest}) is null");
                    logger.Log(Status.Error, $"nBOErequesterID Id ({boeReuqest}) is null for ROI request with id ({requestid})");
                }
                else
                {
                    Assert.IsTrue(true, $"Failed : nBOErequesterID ({boeReuqest}) is not null");
                    logger.Log(Status.Info, $"nBOErequesterID Id ({boeReuqest}) is not null for ROI request with id ({requestid})");
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