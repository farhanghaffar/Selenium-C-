using AventStack.ExtentReports;
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
    public class AddSTATOptionToTheIncludeOrExcludeListForTATReportTest: ROIBaseTest
    {
     public AddSTATOptionToTheIncludeOrExcludeListForTATReportTest() : base(ROITestArea.ROIAdmin)
    {

    }
    [STATestMethodAttribute]
    [TestCategory(ROITestCategory.Regression)]
     //Converted manual test case 13169-ROI-Admin--> Facility TAT Report - Add STAT as an Option to the Include/Exclude List to automated
    public void Reg_13169_AddSTATOptionToTurnaroundReportTest()
    {
        logger = extent.CreateTest("Reg_13169_AddSTATOptionToTurnaroundReportTest");
        logger.Log(Status.Info,"Converted manual test case 13169-ROI-Admin--> Facility TAT Report - Add STAT as an Option to the Include/Exclude List to automated");
        RemoteWebDriver _driver = Init(TestContext.Properties["browser"].ToString());
        ThreadLocal<RemoteWebDriver> localDriver = new ThreadLocal<RemoteWebDriver>();
        localDriver.Value = _driver;
        RemoteWebDriver driver = localDriver.Value;
        string userRoot = System.Environment.GetEnvironmentVariable("USERPROFILE");
        string downloadFolder = Path.Combine(userRoot, "Downloads\\");
        string requestId = string.Empty;
        bool isColumnVisible = false;
        try
        {
            ROIAdminHomePage rOIAdminHomePage = new ROIAdminHomePage(driver, logger, TestContext);
            rOIAdminHomePage.SelectFacilityList();
            ROIAdminFacilityListPage rOIAdminFacilityListPage = new ROIAdminFacilityListPage(driver, logger, TestContext);
            rOIAdminFacilityListPage.GoToROITestFacility();
            ROIFacilityWorkSummaryPage rOIFacilityWorkSummaryPage = new ROIFacilityWorkSummaryPage(driver, logger, TestContext);
            rOIFacilityWorkSummaryPage.logaNewRequest();
            ROIFacilityLogNewRequestPage rOIFacilityLogNewRequestPage = new ROIFacilityLogNewRequestPage(driver, logger, TestContext);
            rOIFacilityLogNewRequestPage.ClickOnsiteDeliveryTab();
            var newDeliveryRequestData = rOIFacilityLogNewRequestPage.CreateOnsiteDeliveryRequest();
            ROIFacilityRequestStatusPage roiFacilityRequestStatusPage = new ROIFacilityRequestStatusPage(driver, logger, TestContext);
            requestId = roiFacilityRequestStatusPage.GetRequestID();
            requestId = requestId.Trim();
            logger.Log(Status.Info, $"On-Site delivery request created with id ({requestId})", TakeScreenShotAtStep());

            rOIAdminHomePage.ROIlookupByRequestId(requestId);
            roiFacilityRequestStatusPage.ImportPatientPagesForOnsiteDelivery();
            rOIFacilityWorkSummaryPage.GoToMROAnalyseSelectTurnAroundReport();
            ROIFacilityTurnaroundReportPage rOIFacilityTurnaroundReportPage = new ROIFacilityTurnaroundReportPage(driver, logger, TestContext);
            rOIFacilityTurnaroundReportPage.ApplyFiltersAndCreateReport();
            logger.Log(Status.Info, "Turnaround report created with filters as Report Criteria:[Logged],Reporting Group:[None],DateRange:[Today], Location:[Boston Proper],Delivery Method:[On-Site Delivery],Exclude:[True]", TakeScreenShotAtStep());
            isColumnVisible= rOIFacilityTurnaroundReportPage.isColumnReceivedToLoggedVisible();
            logger.Log(Status.Info, $"Column Received to logged is visible under Average Turnaround Times for Logged Requests - All Requests (in days)", TakeScreenShotAtStep());
            rOIFacilityTurnaroundReportPage.ClickRequestNumberLink();
            var requestData = rOIFacilityLogNewRequestPage.GetTableData();
            Assert.AreEqual(requestData[0].FirstName.Trim(), newDeliveryRequestData[0].Value.Trim(), "Failed to validate first name");
            Assert.AreEqual(requestData[0].LastName.Trim(), newDeliveryRequestData[1].Value.Trim(), "Failed to validate last name");
            Assert.AreEqual(requestData[0].Location.Trim(), newDeliveryRequestData[2].Value.Trim(), "Failed to validate location");
            rOIFacilityTurnaroundReportPage.ClickOnCustomize();
            rOIFacilityTurnaroundReportPage.ReapplyFiltersAndCreateReport();
            string reportData = rOIFacilityTurnaroundReportPage.GetReportDataAfterApplyingFilters();
            logger.Log(Status.Info, $"Verified that report contains({reportData}) with filters as Report Criteria:[Logged],Reporting Group:[None],DateRange:[Today], Location:[Boston Proper],Delivery Method:[On-Site Delivery],Exclude:[True],ExcludeDropdown:[STAT Requests]",TakeScreenShotAtStep());
            Cleanup(driver);
        }
        catch (Exception ex)
        {
            LogException(driver, logger, ex);
        }
    }

}
}
