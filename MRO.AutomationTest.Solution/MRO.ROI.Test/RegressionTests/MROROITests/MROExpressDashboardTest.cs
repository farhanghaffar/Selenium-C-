using AventStack.ExtentReports;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MRO.ROI.Automation.Common;
using MRO.ROI.Automation.Pages;
using MRO.ROI.Automation.Pages.Common;
using MRO.ROI.Test.ExecutionFactory;
using MRO.ROI.Test.Utilities;
using OpenQA.Selenium.Remote;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MRO.ROI.Test.RegressionTests.MROROITests
{
    [TestClass]
    public class MROExpressDashboardTest : ROIBaseTest
    {
        public MROExpressDashboardTest() : base(ROITestArea.ROIAdmin)
        {
        }

        [STATestMethodAttribute]
        [TestCategory(ROITestCategory.Passed)]
        //Converted manual test case 9983-ROI-Admin-->MRO eXpress Dashboard - Facility to automated
        public void Reg_9983_MROExpressDashBoardFacilityTest()
        {
            logger = extent.CreateTest("Reg_9983_MROExpressDashBoardFacility");
            logger.Log(Status.Info, "//Converted manual test case 9983-ROI-Admin-->MRO eXpress Dashboard - Facility to automated");
            RemoteWebDriver _driver = Init(TestContext.Properties["browser"].ToString());
            ThreadLocal<RemoteWebDriver> localDriver = new ThreadLocal<RemoteWebDriver>();
            localDriver.Value = _driver;
            RemoteWebDriver driver = localDriver.Value;

            try
            {
                ROIAdminHomePage rOIAdminHomePage = new ROIAdminHomePage(driver, logger, TestContext);
                Iframe frame = new Iframe(driver, logger, TestContext);
                rOIAdminHomePage.SelectFacilityList();
                ROIAdminFacilityListPage adminFacilityListPage = new ROIAdminFacilityListPage(driver, logger, TestContext);
                frame.SwitchToRoiFrame();
                adminFacilityListPage.GoToMROExpressTestFacility();
                ROIAdminFacilityWorkSummarypage rOIAdminFacilityWorkSummarypage = new ROIAdminFacilityWorkSummarypage(driver, logger, TestContext);
                rOIAdminFacilityWorkSummarypage.SelectExpressDashboard();
                ROIAdminReportsFilterPage roiAdminReportsFilterPage = new ROIAdminReportsFilterPage(driver, logger, TestContext);
                roiAdminReportsFilterPage.ApplyFilterAndCreateReport();
                logger.Log(Status.Info, $"Express dashboard filter applied for Daterange :({roiAdminReportsFilterPage.dateRangeDate}) reporting group :([None]) location :([All])",TakeScreenShotAtStep());
                frame.switchToDefaut();
                frame.SwitchToRDFrame();
                ROIAdminExpressDashboardPage roiAdminExpressDashboardPage = new ROIAdminExpressDashboardPage(driver, logger, TestContext);
                roiAdminExpressDashboardPage.ClickOnRequestDataTab();
                var requestSummaryLabels = roiAdminExpressDashboardPage.GetRequestSummaryLabels();

                Assert.AreEqual("Requests Today", requestSummaryLabels[0].Text, "Failed to validate requests today label");
                Assert.AreEqual("Last 30 Days", requestSummaryLabels[1].Text, "Failed to validate last 30 days label");
                Assert.AreEqual("Total", requestSummaryLabels[2].Text, "Failed to validate total label");
                logger.Log(Status.Pass, "Request summary labels verified for (Requests Today) (Last 30 Days) (Total)");

                var dashboardTitleCaptions = roiAdminExpressDashboardPage.GetDashboardTitleCaptions();

                Assert.AreEqual("REQUEST VOLUME BY LOCATION", dashboardTitleCaptions[1].Text, "Failed to validate Request Volume By Location caption");
                Assert.AreEqual("REQUEST STATUS INFORMATION", dashboardTitleCaptions[2].Text, "Failed to validate Request Status Information caption");
                Assert.AreEqual("REQUEST VOLUME BY MONTH", dashboardTitleCaptions[3].Text, "Failed to validate Request Volume By Month caption");
                Assert.AreEqual("DELIVERY METHOD", dashboardTitleCaptions[4].Text, "Failed to validate Delivery Method caption");
                Assert.AreEqual("RECORD TYPE", dashboardTitleCaptions[5].Text, "Failed to validate Record Type caption");
                Assert.AreEqual("REQUEST REASON", dashboardTitleCaptions[6].Text, "Failed to validate Request Reason caption");
                logger.Log(Status.Pass, "Dashboard title captions verified for (Request Volume By Location) (Request Status Information) (Request Volume By Month) (Delivery Method) (Record Type) (Request Reason)",TakeScreenShotAtStep());


                var timeFramelable = roiAdminExpressDashboardPage.GetSelectedTimeFrameLabel();
                Assert.IsTrue(timeFramelable.Displayed, "Failed to validate selected time frame label");
                logger.Log(Status.Pass, "On request Request summary page selected time frame label dispalyed");


                string timeframe = timeFramelable.Text.Split(':')?[1].Trim();
                Assert.AreEqual(roiAdminReportsFilterPage.dateRangeDate.Replace("to", "-"), timeframe, "Failed to validate selected date range on dashboad page");
                logger.Log(Status.Pass, "On request Request summary page selected date range value validated");

                bool rsiIsValidated = roiAdminExpressDashboardPage.RequestCountCheckForDSCOrder("dtRequestInformation_eXpress");
                Assert.IsTrue(rsiIsValidated, "Failed to validate the records for decending order of request status information");
                logger.Log(Status.Pass, "Records validated for decending order of request status information");

                bool rtIsValidated = roiAdminExpressDashboardPage.RecordTypeCheckForDSCOrder();
                Assert.IsTrue(rtIsValidated, "Failed to validate the records for decending order of request type");
                logger.Log(Status.Pass, "Records validated for decending order of request type");

                roiAdminExpressDashboardPage.DownloadTheExcelFiles();
                bool excelsIsValidated = roiAdminExpressDashboardPage.VerifyExcelFilesAreDownloadedForAllTitles();
                Assert.IsTrue(excelsIsValidated, "Failed to validate excel files downloaded for for its tiles");
                logger.Log(Status.Pass, "validated excel files downloaded for for its tiles");

                roiAdminExpressDashboardPage.ClickOnUserDataTab();
                var requestSummaryLabelsUD = roiAdminExpressDashboardPage.GetRequestSummaryLabels();

                Assert.AreEqual("Requests Today", requestSummaryLabelsUD[0].Text, "Failed to validate requests today label");
                Assert.AreEqual("Last 30 Days", requestSummaryLabelsUD[1].Text, "Failed to validate last 30 days label");
                Assert.AreEqual("Total", requestSummaryLabelsUD[2].Text, "Failed to validate total label");
                logger.Log(Status.Pass, "Request summary labels verified for (Requests Today) (Last 30 Days) (Total) on user data tab");

                var dashboardTitleCaptionsUD = roiAdminExpressDashboardPage.GetDashboardTitleCaptions();

                Assert.AreEqual("PATIENT AGE", dashboardTitleCaptionsUD[2].Text, "Failed to validate Patient Age caption");
                Assert.AreEqual("DESKTOP VS MOBILE", dashboardTitleCaptionsUD[3].Text, "Failed to validate Desktop vs Mobile caption");
                Assert.AreEqual("EXPRESS FEEDBACK", dashboardTitleCaptionsUD[4].Text, "Failed to validate eXpress Feedback caption");
                Assert.AreEqual("RECIPIENT INFORMATION", dashboardTitleCaptionsUD[5].Text, "Failed to validate Recipient Information caption");
                Assert.AreEqual("REQUESTER RELATIONSHIP INFORMATION", dashboardTitleCaptionsUD[6].Text, "Failed to validate Requester Relationship Information caption");
                logger.Log(Status.Pass, "Dashboard title captions verified for (Patient Age) (Desktop vs Mobile) (eXpress Feedback) (Recipient Information) (Requester Relationship Information) on user data tab",TakeScreenShotAtStep());


                bool rsiIsValidatedUD = roiAdminExpressDashboardPage.RequestCountCheckForDSCOrderOnUserDataTab("dtRecipientInformation_eXpress");
                Assert.IsTrue(rsiIsValidatedUD, "Failed to validate the records for decending order of recipient information");
                logger.Log(Status.Pass, "Records validated for decending order of recipient information");

                bool rriIsValidatedUD = roiAdminExpressDashboardPage.RequestCountCheckForDSCOrderOnUserData("dtRequesterInformation_eXpress");
                Assert.IsTrue(rriIsValidatedUD, "Failed to validate the records for decending order of request relationship information");
                logger.Log(Status.Pass, "Records validated for decending order of request relationship information");

                var timeFramelableUD = roiAdminExpressDashboardPage.GetSelectedTimeFrameLabel();
                Assert.IsTrue(timeFramelableUD.Displayed, "Failed to validate selected time frame label of user data tab");
                logger.Log(Status.Pass, "On request Request summary page selected time frame label dispalyed on user data tab");


                string timeframeUD = timeFramelableUD.Text.Split(':')?[1].Trim();
                Assert.AreEqual(roiAdminReportsFilterPage.dateRangeDate.Replace("to", "-"), timeframeUD, "Failed to validate selected date range on dashboad page fo user data tab");
                logger.Log(Status.Pass, "On request summary page selected date range value validated on user data tab",TakeScreenShotAtStep());

                roiAdminExpressDashboardPage.DownloadTheExcelFiles();
                bool excelsIsValidatedUD = roiAdminExpressDashboardPage.VerifyExcelFilesAreDownloadedForAllTitlesOnUserDataTab();
                Assert.IsTrue(excelsIsValidated, "Failed to validate excel files downloaded for for its tiles on user data tab");
                logger.Log(Status.Pass, "Validated excel files downloaded for for its tiles on user data tab");

                var expressFeedbackColumns = roiAdminExpressDashboardPage.GetExpressFeedbackColumns();

                Assert.AreEqual("Request ID", expressFeedbackColumns[0].Text, "Failed to validate requester id column");
                Assert.AreEqual("Date", expressFeedbackColumns[1].Text, "Failed to validate Date column");
                Assert.AreEqual("Age", expressFeedbackColumns[2].Text, "Failed to validate Age column");
                Assert.AreEqual("Feedback", expressFeedbackColumns[3].Text, "Failed to validate Feedback column");
                Assert.AreEqual("Star Rating", expressFeedbackColumns[4].Text, "Failed to validate Star Rating column");
                logger.Log(Status.Pass, "Validated columns (Request ID) (Date) (Age) (Feedback) (Star Rating) for express feedback");

                driver.SwitchTo().DefaultContent();

                LoginPage loginPage = new LoginPage(driver, logger, TestContext);
                loginPage.LogOut();

                Cleanup(driver);
            }
            catch (Exception ex)
            {
                ROIAdminExpressDashboardPage.DeleteDownloadedExcelFilesOnUserDataTab();
                ROIAdminExpressDashboardPage.DeleteDownloadedExcelFiles();
                LogException(driver, logger, ex);
            }
        }

    }
}
