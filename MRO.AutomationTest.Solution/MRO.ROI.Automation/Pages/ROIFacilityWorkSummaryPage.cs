using AventStack.ExtentReports;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MRO.ROI.Automation.Common.Navigation;
using MRO.ROI.Automation.Selenium;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using System;
using static MRO.ROI.Automation.Common.Navigation.FacilityMenuNavigation.ROIRequests;

namespace MRO.ROI.Automation.Pages
{
    public class ROIFacilityWorkSummaryPage
    {
        public RemoteWebDriver Driver;
        public ExtentTest logger;
        public TestContext Context;
        public ROIFacilityWorkSummaryPage(RemoteWebDriver driver, ExtentTest _loger, TestContext _context)
        {
            Driver = driver;
            logger = _loger;
            Context = _context;
        }

        public By roiRequest = By.XPath("//td[contains(text(),'ROI Requests')]");
        public const string automattedLogging = "//td[contains(text(),'Automated Logging')]";
        public By automatedLoggingElement = By.XPath("//td[contains(text(),'Automated Logging')]");
        public By batchesMenu = By.XPath("//td[contains(text(),'Batches')]");
        public const string importFileSubMenu = "//td[contains(text(),'Import File')]";
        public By importFile = By.XPath("//td[contains(text(),'Import File')]");
        public By batchProcessingReport = By.XPath("//td[starts-with(@id,'mroheader') and contains(text(),'Batch Processing Report')]");
        public By keyBatchInfoSubMenu = By.XPath("(//td[contains(text(),'Key Batch Info')])[1]");
        public By LookUpRequestId = By.XPath("//a[@title='Look up by Request ID']");
        public By headerElement = By.XPath("//td[@id='MasterHeaderText']");
        public By ListAllUsersOption = By.XPath("//td[contains(text(),'List All Users')]");
        public By UserMenuOption = By.XPath("//td[starts-with(@id,'mroheader_MROPageHead1') and text()='Users']");
        public By addNewUser=By.XPath("//td[contains(text(),'Add a New User')]");
        public By ActionMessageForMROTextArea = By.Id("mrocontent_txtComments");

        public ROIFacilityLogNewRequestPage logaNewRequest()
        {
            try
            {
                ROIMenuSelector selector = new ROIMenuSelector(Driver, logger, Context);
                try
                {
                    selector.Select("ROI Requests", "Log a New Request");

                }
                catch (Exception)
                {
                    selector.SelectRoiAdminMenuOptions("mnuROIFacilityUser", "ROI Requests", "Log a New Request");

                }
            }
            catch (Exception ex)
            {

                throw new Exception($"Failed  to navigate to log a new request with details as Message : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
            return new ROIFacilityLogNewRequestPage(Driver,logger, Context);
        }

        /// <summary>
        /// Go to MroAnalyze select Turn Around Report
        /// </summary>        
        public ROIFacilityTurnaroundReportPage GoToMROAnalyseSelectTurnAroundReport()
        {
            try
            {
                ROIMenuSelector selector = new ROIMenuSelector(Driver, logger, Context);
                selector.Select("MRO Analyze", "Turnaround Report");
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to go to turnAround report page : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }

            return new ROIFacilityTurnaroundReportPage(Driver, logger, Context);
        }
        /// <summary>
        /// Go to log a new request page
        /// </summary>        
        public ROIFacilityLogNewRequestPage GoToLogNewRequestPage()
        {
            try
            {
                LogNewRequest loginRequest = new LogNewRequest(Driver,logger,Context);
                loginRequest.Select();
                Assert.IsTrue(LogNewRequestPage.IsAtLogNewRequestPage, "Failed to navigate to log new request page.");
            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to go to log a new request Page : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
            return new ROIFacilityLogNewRequestPage(Driver, logger, Context);

        }

        public void ClickKeyBatchInfo()
        {
            try
            {
                MenuSelector menuSelector = new MenuSelector(Driver, logger, Context);
                menuSelector.Select("Batches", "Key Batch Info");

            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to click key batch info : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }

        }
        public bool VerifyAutomatedLoggingAtFacilitySide()
        {
            try
            {
                Driver.Click(roiRequest);
                bool isDisplayed = false;
                WebElementHelper helper = new WebElementHelper(Driver, logger, Context);
                bool IsAutomattedLogging = helper.IsElementPresent(automattedLogging);
                if (IsAutomattedLogging == true)
                {
                    isDisplayed = true;
                }
                else
                {
                    isDisplayed = false;
                }
                return isDisplayed;               
            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to verify automatted logging with details as: {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }
        public void ClickOnAutomattedLoggingAtFacility()
        {
            try
            {
                Driver.Click(roiRequest);
                Driver.Click(automatedLoggingElement);

            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to click automated logging with details message as : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }
        /// <summary>
        /// Go to Batches select Import file
        /// </summary>        
        public void GoToBatchAndSelectImportFile()
        {
            try
            {
                Driver.Wait(TimeSpan.FromSeconds(5));
                Driver.DirectClick(batchesMenu);
                Driver.Wait(TimeSpan.FromSeconds(2));
                Driver.JavaScriptClick(importFile);
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to go to batches and select import file: {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }
        public void GoToBatchAndBatchProcessingReport()
        {
            try
            {
                Driver.Wait(TimeSpan.FromSeconds(5));
                Driver.SwitchTo().DefaultContent();
                Driver.DirectClick(batchesMenu);
                Driver.Wait(TimeSpan.FromSeconds(2));
                Driver.JavaScriptClick(batchProcessingReport);
                Driver.Wait(TimeSpan.FromSeconds(5));
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to go to batches and select batch processing report: {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }

        }

        /// <summary>
        /// Go to MroAnalyze--->Select Summary Dashboard
        /// </summary>        
        public void SelectSummaryDashBoard()
        {
            try
            {                
                ROIMenuSelector selector = new ROIMenuSelector(Driver, logger, Context);
                selector.Select("MRO Analyze", "Summary Dashboard");
                Driver.SleepTheThread(20);
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to select summary dashboard : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }

        }
        /// <summary>
        /// Go to Batches and select key batch info
        /// </summary>        
        public void GoToBatchAndSelectKeyBatchInfo()
        {
            try
            {
                Driver.Wait(TimeSpan.FromSeconds(5));
                Driver.DirectClick(batchesMenu);
                Driver.Wait(TimeSpan.FromSeconds(2));
                Driver.JavaScriptClick(keyBatchInfoSubMenu);
                Driver.Wait(TimeSpan.FromSeconds(5));
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to go to batches and select key batch info: {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }


        public void SearchByRequestId(string RequestId)
        {
            try
            {
                IWebElement requestId = Driver.FindElementBy(LookUpRequestId);
                requestId.Click();
                Driver.SwitchTo().Alert().SendKeys(RequestId);
                logger.Log(Status.Info, $"Entered request id ({RequestId})");
                Driver.SwitchTo().Alert().Accept();
            }
            catch (Exception ex)
            {

                throw new Exception($"Failed with Message not able to click '?' : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }

        /// <summary>
        /// Go to MroAnalyze select Staff Activity Report
        /// </summary>
        public void GoToMROAnalyzeSelectStaffActivityReport()
        {
            try
            {
                ROIMenuSelector selector = new ROIMenuSelector(Driver, logger, Context);
                selector.Select("MRO Analyze", "Staff Activity");
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to go to staff activity report page : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }


        public void FindARequest(string RequestId)
        {
            try
            {
                ROIMenuSelector selector = new ROIMenuSelector(Driver, logger, Context);
                try
                {
                    selector.SelectRoiAdminMenuOptions("mnuROIFacilityUser", "ROI Requests", "Find a Request");

                }
                catch(Exception ex)
                {
                    selector.Select("ROI Requests", "Find a Request");
                }

            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to click on Find a Request : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }
        public void SelectPatientLookup()
        {
            try
            {
                ROIMenuSelector selector = new ROIMenuSelector(Driver, logger, Context);
                selector.Select("ROI Requests", "Patient Lookup");
                Driver.SleepTheThread(2);
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to click patient lookup : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }

        }

        public string VerifyHeader()
        {
            try
            {
                string headerVal = Driver.GetText(headerElement);
                return headerVal;

            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to return header with details as : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }

        public bool VerifyPatientLookup()
        {
            try
            {
                Driver.Click(roiRequest);
                bool isDisplayed = false;
                WebElementHelper helper = new WebElementHelper(Driver, logger, Context);
                string patientLookup = "//td[contains(text(),'Patient Lookup')] ";
                bool IsPatientLookUp = helper.IsElementPresent(patientLookup);
                if (IsPatientLookUp == true)
                {
                    isDisplayed = true;
                }
                else
                {
                    isDisplayed = false;
                }
                return isDisplayed;
            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to verify patient lookup with details as: {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }

        public void ToSelectListAllUsers()
        {
            try
            {
                Driver.ClickAndCheckNextElement(UserMenuOption, ListAllUsersOption);
                Driver.DirectClick(ListAllUsersOption);
            }
            catch (Exception ex)
            {

                throw new Exception($"Failed  to navigate list all Users Page with details as  Message : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
           
        }

        public bool VerifyAddNewUser()
        {
            try
            {
                Driver.Click(UserMenuOption);
                string addNewUser = "//td[contains(text(),'Add a New User')]";
                WebElementHelper helper = new WebElementHelper(Driver, logger, Context);
                bool isDisplayed = helper.IsElementPresent(addNewUser);
                return isDisplayed;
            }
            catch (Exception ex)
            {

                throw new Exception($"Failed  to navigate listallUsers Page with details as  Message : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }


        public void ClickOnAddNewUser()
        {
            try
            {

                Driver.ClickAndCheckNextElement(UserMenuOption, addNewUser);
                Driver.DirectClick(addNewUser);
            }
            catch (Exception ex)
            {

                throw new Exception($"Failed  to click add new user with details as  Message : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }

        public void ClickOnDocsRequired()
        {
            try
            {
                ROIMenuSelector selector = new ROIMenuSelector(Driver, logger, Context);
                selector.Select("ROI Requests", "Docs Required");
            }
            catch (Exception ex)
            {

                throw new Exception($"Failed  to navigate to log a new request with details as Message : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
            
        }

        public void SelectDailyShipmentsReport()
        {
            try
            {
                ROIMenuSelector selector = new ROIMenuSelector(Driver, logger, Context);
                selector.Select("Reports", "Daily Shipments Report");
                Driver.SleepTheThread(2);
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to select daily shipments report: {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }

        }

        public void SelectPendingReport()
        {
            try
            {
                ROIMenuSelector selector = new ROIMenuSelector(Driver, logger, Context);
                selector.Select("Reports", "Pending Report" );
                Driver.SleepTheThread(2);
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to select pending report with details as: {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }

        }
        //Sandeep
        public ROIFacilityLogNewRequestPage GoToDocsRequired()
        {
            try
            {
                LogNewRequest GoToDocsRequired = new LogNewRequest(Driver, logger, Context);
                GoToDocsRequired.SelectDocsRequired();
                Assert.IsTrue(LogNewRequestPage.IsAtLogNewRequestPage, "Failed to navigate to log new request page.");
            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to go to log a new request Page : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
            return new ROIFacilityLogNewRequestPage(Driver, logger, Context);

        }

        // Written by Sandeep
        public void GoToMROAnalyzeQualityAssurance()
        {
            try
            {
                ROIMenuSelector selector = new ROIMenuSelector(Driver, logger, Context);
                selector.Select("MRO Analyze", "Quality Assurance");
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to go to Quality Assurance report page : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }

        // written by sandeep 
        public void GoToMROAnalyzeDailyStaffProductivity()
        {
            try
            {
                ROIMenuSelector selector = new ROIMenuSelector(Driver, logger, Context);
                selector.Select("MRO Analyze", "Daily Staff Productivity");
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to go to DailyStaffProductivity report page : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }
        // written by sandeep 
        public void GoToMROAnalyzeRevenueIntegrityDashboard()
        {
            try
            {
                ROIMenuSelector selector = new ROIMenuSelector(Driver, logger, Context);
                selector.Select("MRO Analyze", "Revenue Integrity Dashboard");
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to go to Revenue Integrity Dashboard  report page : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }

        // written by sandeep 
        public void GoToMROAnalyzeMonthlySummary()
        {
            try
            {
                ROIMenuSelector selector = new ROIMenuSelector(Driver, logger, Context);
                selector.Select("MRO Analyze", "Monthly Summary");
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to go to Monthly Summary report page : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }

        // written by sandeep 
        public void GoToMROAnalyzeFacilityDashboard()
        {
            try
            {
                ROIMenuSelector selector = new ROIMenuSelector(Driver, logger, Context);
                selector.Select("MRO Analyze", "Facility Dashboard");
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to go to FacilityDashboard  report page : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }

        // written by sandeep 
        public void GoToMROAnalyzeEnterpriseDashboard()
        {
            try
            {
                ROIMenuSelector selector = new ROIMenuSelector(Driver, logger, Context);
                selector.Select("MRO Analyze", "Enterprise Dashboard");
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to go to EnterpriseDashboard  report page : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }

        /// <summary>
        /// Verify Status Under ImportDocument
        /// </summary>
        public bool IsActionMessageForMRODisplaying()
        {
           
            bool isStatus = false;
            try
            {
                 isStatus = Driver.isElementDisplayed(ActionMessageForMROTextArea);
                return isStatus;
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to get visibility of element : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }
    }
}
