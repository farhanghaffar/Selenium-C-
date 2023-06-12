using AventStack.ExtentReports;
using DataDrivenProject;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MRO.ROI.Automation.Selenium;
using MRO.ROI.Automation.Utility;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using static MRO.ROI.Automation.Utility.IniFile;

namespace MRO.ROI.Automation.Pages
{
    public class ROIFacilityTurnaroundReportPage
    {
        public RemoteWebDriver Driver;
        public ExtentTest logger;
        public TestContext Context;
        public CSVReader csvReader;

        public string csvFileName = "TurnAroundReport.csv";
        public ROIFacilityTurnaroundReportPage(RemoteWebDriver driver, ExtentTest _loger, TestContext _context)
        {
            Driver = driver;
            logger = _loger;
            Context = _context;
        }
        public By turnAroundReportFrame = By.XPath("//iframe[starts-with(@id,'rdFrame')]");
        public By selectDatePicker = By.XPath("(//span[@id='daterange'])[1]");
        public By selectFromDate = By.XPath("((//table[@class='table-condensed'])[2]//tr//td[contains(text(),'1')])[1]");
        public By selectToDate = By.XPath("((//table[@class='table-condensed'])[1]//tr//td[contains(text(),'1')])[13]");
        public By btnApply = By.XPath("//button[text()='Apply']");
        public By selectLocationDrp = By.XPath("//select[@id='nLocationID']");
        public By btnCreateReport = By.XPath("//input[@id='btn_submit']");
        public By txtReceivedToShippedAllDates = By.XPath("//span[@id='lblsGroup_Row1']");
        public By txtReceivedToShippedPreBills = By.XPath("//th[@id='colnDays_RcvdToShipped_minus_InvToPaid_Pre-TH']");
        public By txtReceivedToShippedNonPreBills = By.XPath("//th[@id='colnDays_RcvdToShipped_NonPre-TH']");
        public By lnkReceivedToShippedAllRequestData = By.XPath("//td[@id='colnDays_RcvdToShipped_minus_InvToPaid_Row1']//a");
        public By frameAllRequest = By.XPath("//iframe[@title='sr_dtTurnaroundDrillDown_AllReqs']");
        public By pdfIcon = By.XPath("//span[@id='div_PDF_icon']");
        public By excelIcon = By.XPath("//span[@id='div_Excel_icon']");
        public By drpDeliveryMethod = By.XPath("//select[@id='nRequestType']");
        public By excludeRadioButton = By.XPath("//input[@value='Exclude']");
        public By optToday = By.XPath("//div[@class ='ranges']/ul/li[1]");
        public By lblLogged = By.XPath("//label[text()='Logged']");
        public By colReceivedToLogged = By.XPath("//table[@id='dtTurnaroundAvgAllReqs']//th[@id='colnDays_RcvdToLogged-TH']");
        public By lnkReceivedToLogged = By.XPath("//td[@id='colnDays_RcvdToLogged_Row1']//a[@id='show_div_dtTurnaroundAvgAllReqs_drilldown_container_Row1']");
        public By rdFrame = By.XPath("//iframe[starts-with(@id,'rdFrame')]");
        public By lnkCustomize = By.XPath("//a[@id='show_div_user_inputs']/span");
        public By chkStatRequests = By.XPath("//input[@name ='naExcludeID' and @value=8]");
        public By btnExcludeList = By.XPath("//button[@id='naExcludeID_handler']");
        public By TotalRequestsDelivered = By.XPath("(//table[@id='dtTurnaroundReqTally']//tr/td[2]/a)[2]");
        public By ReceivedToShippedRequestsCount = By.XPath("//table[@id='dtTurnaroundDateRange']//tr[@id='SummaryRow']/td[2]/span[2]");
        public By shippedRadioButton = By.XPath("//input[@id='rbDateFields_3']");
        public By drpUserType = By.XPath("//select[@id='nUserTypeID']");
        public By selectMonthDrp = By.XPath("//div[@class ='calendar left']//select[@class='monthselect']");
        public By selectYearDrp = By.XPath("//div[@class ='calendar left']//select[@class='yearselect']");
        public By selectDate = By.XPath("//div[@class='calendar left']//table[@class='table-condensed']//tbody/tr[1]/td[5]");
        public By dtCriteria = By.XPath("//div[@id='div_DateField']");
        public By dtRange = By.XPath("//div[@id='div_dta_dtz']");
        public By Location = By.XPath("//div[@id='div_nLocationID']");
        public By userTypeID = By.XPath("//div[@id='div_nUserTypeID']");
        public By use = By.XPath("//div[@id='div_bDaysCalendar']");
        public By lblReleased = By.XPath("//label[text()='Released']");
        public By drpRequesterType = By.XPath("//select[@id='nRequesterTypeID']");
        public By drpPurposeOfUse = By.XPath("//select[@id='nPurposeOfUseID']");

        public By patientframeselect = By.XPath("//iframe[(@id='sr_dtTurnaroundDrillDown_ReqTally')]");
        public By patientnameselect = By.XPath("//a[@id='ActLinkParent_Row1']");
        public By chkOruncheckHadOpenIssues = By.XPath("//input[@name ='naExcludeID' and @value=0]");
        public By releasedRadioButton = By.XPath("//input[@id='rbDateFields_2']");

        /// <summary>
        /// Create new Turn Around Report
        /// </summary>
        public void CreateMROTurnAroundReport()
        {
            try
            {
                IWebElement frame = Driver.FindElementBy(turnAroundReportFrame);
                Driver.SwitchTo().Frame(frame);
                IWebElement datePicker = Driver.FindElementBy(selectDatePicker);
                datePicker.Click();
                Driver.Click(selectFromDate);
                Driver.Click(selectToDate);
                IWebElement applyButton = Driver.FindElementBy(btnApply);
                applyButton.Click();
                var selectLocatin = Driver.FindElementBy(selectLocationDrp);
                var selectLocations = new SelectElement(selectLocatin);
                csvReader = new CSVReader(Path.GetFullPath(Path.Combine(Assembly.GetExecutingAssembly().Location, "..", "TestData", csvFileName)));
                string location = csvReader.GetDataFromCSVFile("Location");
                selectLocations.SelectByText(location);                
                Driver.FindElementBy(btnCreateReport).Click();
            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to create MRO turn around report exception detail as : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }

        }
        /// <summary>
        /// Validate Turn Around Report 
        /// </summary>
        public bool VerifyReceivedToShippedDate()
        {
            bool isValidated = false;
            try
            {
                List<IWebElement> requestDates = Driver.FindElementsBy(txtReceivedToShippedAllDates);
                foreach (var date in requestDates)
                {
                    Assert.IsNotNull(date.Text,"Received to shipped date is null or empty");
                }
                isValidated = true;      

            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to validate received to shipped date exception detail as : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
            return isValidated;
        }

        public void VerifyPDFExcelIcon()
        {
            try
            {
                Driver.Click(lnkReceivedToShippedAllRequestData);
                IWebElement frame1 = Driver.FindElementBy(frameAllRequest);
                Driver.SwitchTo().Frame(frame1);
                IWebElement turnAroundAllRequestPDF = Driver.FindElementBy(pdfIcon);
                Assert.IsTrue(turnAroundAllRequestPDF.Displayed,"PDF icon not displayed");
                IWebElement turnAroundAllRequestExcel = Driver.FindElementBy(excelIcon);
                Assert.IsTrue(turnAroundAllRequestExcel.Displayed, "Excel icon not displayed");

            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to verify pdf and excel icon for the created turn around report page Exception detail as : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }
        /// <summary>
        /// Create new Turn Around Report
        /// </summary>
        public void ApplyFiltersAndCreateReport()
        {
            try
            {
                IWebElement frame = Driver.FindElementBy(turnAroundReportFrame);
                Driver.SwitchTo().Frame(frame);
                Driver.Click(lblLogged);
                IWebElement datePicker = Driver.FindElementBy(selectDatePicker);
                datePicker.Click();
                Driver.SleepTheThread(2);
                Driver.Click(optToday);
                var selectLocatin = Driver.FindElementBy(selectLocationDrp);
                var selectLocations = new SelectElement(selectLocatin);
                string location = IniHelper.ReadConfig("TurnaroundReportFilters", "Location");
                selectLocations.SelectByText(location);
                var selectDeliveryDropdown = Driver.FindElementBy(drpDeliveryMethod);
                var selectDeliveryMethod = new SelectElement(selectDeliveryDropdown);
                string deliveryMethod = IniHelper.ReadConfig("TurnaroundReportFilters", "DeliveryMethod");
                selectDeliveryMethod.SelectByText(deliveryMethod);

                var selectRequesterType = Driver.FindElementBy(drpRequesterType);
                var selectRequesterTypes = new SelectElement(selectRequesterType);
                string requesterType = IniHelper.ReadConfig("TurnaroundReportFilters", "RequesterType");
                selectRequesterTypes.SelectByText(requesterType);
                //

                var selectUserType = Driver.FindElementBy(drpUserType);
                var selectUserTypes = new SelectElement(selectUserType);
                string userType = IniHelper.ReadConfig("TurnaroundReportFilters", "UserTypeVal");
                selectUserTypes.SelectByText(userType);

                IWebElement excludeButton = Driver.FindElementBy(excludeRadioButton);
                excludeButton.Click();
                CheckOrUncheckStatRequestsOption(false);
                Driver.FindElementBy(btnCreateReport).Click();
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to create MRO turn around report with exception detail as : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }

        public bool isColumnReceivedToLoggedVisible()
        {
            bool isValidated = false;
            try
            {
                IWebElement columnReceievedToLogged = Driver.FindElementBy(colReceivedToLogged);
                if (columnReceievedToLogged.Displayed == true)
                {
                    isValidated = true;
                }
            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to verify the column recieved to logged with exception detail as : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
            return isValidated;
        }

        public void ClickRequestNumberLink()
        {
            Driver.Click(lnkReceivedToLogged);
            IWebElement reportFrame = Driver.FindElementBy(frameAllRequest);
            Driver.SwitchTo().Frame(reportFrame);

        }

        public void ClickOnCustomize()
        {
            try
            {
                //SwitchToRDFrame();
                Driver.DirectClick(lnkCustomize);
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to click the customize link : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }

        public void SwitchToRDFrame()
        {
            try
            {
                Driver.SwitchTo().DefaultContent();
                IWebElement frame = Driver.FindElementBy(rdFrame,360);
                Driver.SwitchTo().Frame(frame);
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to switch to rd fame Message : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }

        /// <summary>
        /// Create new Turn Around Report with filters
        /// </summary>
        public void ReapplyFiltersAndCreateReport()
        {
            try
            {

                Driver.Click(lblLogged);
                IWebElement datePicker = Driver.FindElementBy(selectDatePicker);
                datePicker.Click();
                Driver.SleepTheThread(2);
                Driver.Click(optToday);
                var selectLocatin = Driver.FindElementBy(selectLocationDrp);
                var selectLocations = new SelectElement(selectLocatin);
                string location = IniHelper.ReadConfig("TurnaroundReportFilters", "Location");
                selectLocations.SelectByText(location);
                var selectDeliveryDropdown = Driver.FindElementBy(drpDeliveryMethod);
                var selectDeliveryMethod = new SelectElement(selectDeliveryDropdown);
                string deliveryMethod = IniHelper.ReadConfig("TurnaroundReportFilters", "DeliveryMethod");
                selectDeliveryMethod.SelectByText(deliveryMethod);
                IWebElement excludeButton = Driver.FindElementBy(excludeRadioButton);
                excludeButton.Click();
                CheckOrUncheckStatRequestsOption(true);
                Driver.FindElementBy(btnCreateReport).Click();
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to create MRO turn around report with different filters with exception detail as : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }
        public string GetReportDataAfterApplyingFilters()
        {
            string reportData = string.Empty;
            Driver.SleepTheThread(5);
           var requestsCount = Driver.GetText(ReceivedToShippedRequestsCount);
           if (requestsCount == "0")
            {
                reportData = "No Data";
            }
            return reportData;
        }

        public void CheckOrUncheckStatRequestsOption(bool option)
        {
            Driver.Click(btnExcludeList);
            if (option == true)
            {
                Driver.Click(chkStatRequests);
            }
            else if (option == false)
            {
                Driver.Click(chkStatRequests);
            }
            Driver.Click(btnExcludeList);
        }

        public void CheckShippedRadioButton()
        {
            Driver.SleepTheThread(10);
            Automation.Common.Iframe roiframe = new Automation.Common.Iframe(Driver, logger, Context);
            roiframe.SwitchToRoiFrame();
            Driver.SleepTheThread(10);
            IWebElement frame = Driver.FindElementBy(turnAroundReportFrame);
            Driver.SwitchTo().Frame(frame);
            IWebElement shippedButton = Driver.FindElementBy(shippedRadioButton);
            shippedButton.Click();
        }

        /// <summary>
        /// Create new Turn Around Report with filters
        /// </summary>
        public void ApplyFiltersWithFacilityAndCreateReport()
        {
            try
            {
                Driver.SleepTheThread(5);
                Automation.Common.Iframe frame = new Automation.Common.Iframe(Driver, logger, Context);
                frame.SwitchToRoiFrame();
                Driver.SleepTheThread(5);
                frame.SwitchToRDFrame();
                Driver.WaitInSeconds(2);
                Driver.Click(lblLogged);
                IWebElement datePicker = Driver.FindElementBy(selectDatePicker);
                datePicker.Click();
                Driver.SleepTheThread(2);
                SelectElement oSelect1 = new SelectElement(Driver.FindElementBy(selectMonthDrp));
                oSelect1.SelectByText("May");
                SelectElement oSelect2 = new SelectElement(Driver.FindElementBy(selectYearDrp));
                oSelect2.SelectByText("2017");
                Driver.Click(selectDate);
                IWebElement applyButton = Driver.FindElementBy(btnApply);
                applyButton.Click();
                var selectLocatin = Driver.FindElementBy(selectLocationDrp);
                var selectLocations = new SelectElement(selectLocatin);
                string location = IniHelper.ReadConfig("TurnaroundReportFilters", "TurnaroundReportFiltersNewLocation");
                selectLocations.SelectByText(location);
                var selectUserType = Driver.FindElementBy(drpUserType);
                var selectUserTypes = new SelectElement(selectUserType);
                string userType = IniHelper.ReadConfig("TurnaroundReportFilters", "UserType");
                selectUserTypes.SelectByText(userType);

                Driver.FindElementBy(btnCreateReport).Click();
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to create MRO turn around report with different filters with exception detail as : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }

        public string GetHeaderTextAsPerFilters()
        {
            string headerText = string.Empty;
            headerText = headerText + Driver.GetText(dtCriteria);
            headerText = headerText + "," + Driver.GetText(dtRange);
            headerText = headerText + "," + Driver.GetText(userTypeID);
            headerText = headerText + "," + Driver.GetText(Location);
            headerText = headerText + "," + Driver.GetText(use);
            return headerText;
        }

        /// <summary>
        /// Create new Turn Around Report with filters
        /// </summary>
        public void ApplyFiltersWithReleaseOptionAndCreateReport()
        {
            try
            {
                Driver.Wait(TimeSpan.FromSeconds(5));
                //SwitchToRDFrame();
                Driver.DirectClick(lblReleased,120);
                IWebElement datePicker = Driver.FindElementBy(selectDatePicker);
                datePicker.Click();
                Driver.SleepTheThread(2);
                SelectElement oSelect1 = new SelectElement(Driver.FindElementBy(selectMonthDrp));
                oSelect1.SelectByText("Jan");
                SelectElement oSelect2 = new SelectElement(Driver.FindElementBy(selectYearDrp));
                oSelect2.SelectByText("2020");
                Driver.Click(selectDate);
                IWebElement applyButton = Driver.FindElementBy(btnApply);
                applyButton.Click();
                Driver.SleepTheThread(2);
                SelectElement locationPicker = new SelectElement(Driver.FindElementBy(By.XPath("//select[@id='nLocationID']")));
                string location = IniHelper.ReadConfig("TurnaroundReportFilters", "TurnaroundReportFiltersNewLocation");
                locationPicker.SelectByText(location);
                location = "123Cape May";
                locationPicker.SelectByText(location);

                //locationPicker.SelectByText("[All]");
                Driver.SleepTheThread(1);
                SelectElement userTypeDD = new SelectElement(Driver.FindElementBy(By.XPath("//select[@id='nUserTypeID']")));
                string userType = IniHelper.ReadConfig("TurnaroundReportFilters", "UserType");
                userTypeDD.SelectByText(userType);
                Driver.SleepTheThread(1);
                Driver.FindElementBy(btnCreateReport).Click();
                Driver.SleepTheThread(5);
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to create MRO turn around report with different filters with exception detail as : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }



        public void ClickOnTotalRequestsDelivered()
        {
            try
            {
                Driver.DirectClick(TotalRequestsDelivered);
                Driver.SleepTheThread(2);
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to click on total requests delivered  link exception detail as : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }

        public void ClickAnyPatientLink()
        {
            try
            {
                SwitchToRDFrame();
                IWebElement frameElement = Driver.FindElementBy(patientframeselect);
                Driver.SwitchTo().Frame(frameElement);
                Driver.SleepTheThread(1);
                Driver.DirectClick(patientnameselect);
            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to click patient name link exception detail as : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }

        public void CheckReleasedRadioButton()
        {
            IWebElement frame = Driver.FindElementBy(turnAroundReportFrame);
            Driver.SwitchTo().Frame(frame);
            IWebElement releasedButton = Driver.FindElementBy(releasedRadioButton); ;
            releasedButton.Click();
        }
        public void CheckOrUncheckHadOpenIssues(bool option)
        {
            Driver.Click(btnExcludeList);
            if (option == true)
            {
                Driver.Click(chkOruncheckHadOpenIssues);
            }
            else if (option == true)
            {
                Driver.Click(chkOruncheckHadOpenIssues);
            }
            Driver.Click(btnExcludeList);
        }

        public void DownloadExcelReport()
        {
            try
            {
                SwitchToRDFrame();
                IWebElement frameElement = Driver.FindElementBy(patientframeselect);
                Driver.SwitchTo().Frame(frameElement);
                Driver.SleepTheThread(1);
                Driver.DirectClick(By.XPath("//span[@id='div_Excel_icon']"));
                Driver.SleepTheThread(2);
            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to download the excel file, Exception detail: {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }

        public bool VerifyRequestIdDoesNotExist(string requestID, int lastRowNumber, ExcelReaderFile excelReaderFile, string excelFileName)
        {
            bool isValidated = false;
            try
            {
                for (int i = 0; i < lastRowNumber; i++)
                {
                    string excelRequest = excelReaderFile.ReadExcelCellData(excelFileName, lastRowNumber, 1);
                    Assert.AreNotEqual(requestID, excelRequest);
                }
                isValidated = true;
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to validate requestid for non exsistance under Total Request Tally - Total Requests Delivered, Exception detail: {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
            return isValidated;
        }

        public void ApplyFiltersWithReleaseOptionAndCreateReports()
        {
            try
            {
                Driver.WaitForPageToLoad();
                CheckReleasedRadioButton();
                Driver.Click(lblReleased);
                IWebElement datePicker = Driver.FindElementBy(selectDatePicker);
                datePicker.Click();
                Driver.SleepTheThread(2);
                SelectElement oSelect1 = new SelectElement(Driver.FindElementBy(selectMonthDrp));
                oSelect1.SelectByText("Jan");
                SelectElement oSelect2 = new SelectElement(Driver.FindElementBy(selectYearDrp));
                oSelect2.SelectByText("2020");
                Driver.Click(selectDate);
                IWebElement applyButton = Driver.FindElementBy(btnApply);
                applyButton.Click();

                var selectLocatin = Driver.FindElementBy(selectLocationDrp);
                var selectLocations = new SelectElement(selectLocatin);
                string location = IniHelper.ReadConfig("TurnaroundReportFiltersCorrespondance", "NewLocation");
                selectLocations.SelectByText(location);

                var selectUserType = Driver.FindElementBy(drpUserType);
                var selectUserTypes = new SelectElement(selectUserType);
                string userType = IniHelper.ReadConfig("TurnaroundReportFiltersCorrespondance", "UserType");
                selectUserTypes.SelectByText(userType);

                SelectElement oSelect4 = new SelectElement(Driver.FindElementBy(use));
                oSelect4.SelectByText("Business Days");

                IWebElement excludeButton = Driver.FindElementBy(excludeRadioButton);
                excludeButton.Click();
                CheckOrUncheckHadOpenIssues(false);

                Driver.FindElementBy(btnCreateReport).Click();

                Driver.SleepTheThread(5);


            }


            catch (Exception ex)
            {
                throw new Exception($"Failed to create MRO turn around report with different filters with exception detail as : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }




    }
}
