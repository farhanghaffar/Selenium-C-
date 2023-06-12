using AventStack.ExtentReports;
using DataDrivenProject;
using Microsoft.VisualStudio.TestTools.UnitTesting;
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
    public class ROIAdminABCPDFAndTelerikBugsTest : ROIBaseTest
    {
        
        public ROIAdminABCPDFAndTelerikBugsTest() : base(ROITestArea.ROIFacility)
        {
        }

        [TestMethod]
        [TestCategory(ROITestCategory.Regression)]
        // Converted manual test case 9754-ROI-Admin-->ABCPDF and Telerik Bugs automated
        public void Reg_9754_ROIAdminABCPDFAndTelerikBugsTest()
        {
            logger = extent.CreateTest("Reg_9754_ABCPDF and Telerik Bugs");
            logger.Log(Status.Info, "Converted manual test case 9754-ROI-Admin-->ABCPDF and Telerik Bugs automated");
            RemoteWebDriver _driver = Init(TestContext.Properties["browser"].ToString());
            ThreadLocal<RemoteWebDriver> localDriver = new ThreadLocal<RemoteWebDriver>();
            localDriver.Value = _driver;
            RemoteWebDriver driver = localDriver.Value;
            try
            {
                ROIFacilityWorkSummaryPage rOIFacilityWorkSummaryPage = new ROIFacilityWorkSummaryPage(driver, logger, TestContext);
                rOIFacilityWorkSummaryPage.logaNewRequest();
                ROIFacilityLogNewRequestPage rOIFacilityLogNewRequestPage = new ROIFacilityLogNewRequestPage(driver, logger, TestContext);
                rOIFacilityLogNewRequestPage.CreateNewMRODeliveryRequestWithoutScan();
                ROIFacilityRequestStatusPage rOIFacilityRequestStatusPage = new ROIFacilityRequestStatusPage(driver, logger, TestContext);
                string requestid = rOIFacilityRequestStatusPage.GetRequestID();                
                rOIFacilityRequestStatusPage.ImportPdfFiles();
                bool statusUnderImportDocument = rOIFacilityRequestStatusPage.VerifyStatusUnderImportDocument();
                Assert.IsTrue(statusUnderImportDocument, "Failed to verify status under import document is uploaded");
                rOIFacilityRequestStatusPage.ReleaseRequestID();
                ROIAdminHomePage rOIAdminHomePage = new ROIAdminHomePage(driver, logger, TestContext);
                rOIAdminHomePage.SwitchToNewTabAndLoginROIAdmin(BaseAddress);
                rOIAdminHomePage.ROIlookupByRequestId(requestid);
                ROIAdminRequestStatusPage rOIAdminRequestStatusPage = new ROIAdminRequestStatusPage(driver, logger, TestContext);
                string recordsSentDate = rOIAdminRequestStatusPage.GetRecordsSentByFacilityDate();
                string systemDate = rOIAdminRequestStatusPage.GetSystemDate();
                Assert.AreEqual(recordsSentDate, systemDate, $"Verified records sent date({recordsSentDate}) and system date(systemDate) are same. ");
                rOIAdminRequestStatusPage.assignRequester();
                ROIAdminAssignROIRequesterPage rOIAdminAssignROIRequesterPage = new ROIAdminAssignROIRequesterPage(driver, logger, TestContext);
                rOIAdminAssignROIRequesterPage.assignTestAttorney();
                rOIAdminRequestStatusPage.ClickOnQcPassButton();
                rOIAdminRequestStatusPage.ApplyRate();
                rOIAdminRequestStatusPage.AddEmailShippment();
                rOIAdminRequestStatusPage.CreateInvoice();
                rOIAdminRequestStatusPage.ClickAddIssueBtn();
                ROIAdminAddIssuePage rOIAdminAddIssuePage = new ROIAdminAddIssuePage(driver, logger, TestContext);
                rOIAdminAddIssuePage.SetIssueANS();
                rOIAdminRequestStatusPage.ClickSendIssueBtn();

                Cleanup(driver);
            }
            catch (Exception ex)
            {
                LogException(driver, logger, ex);
            }

        }
    }
}
