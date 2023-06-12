using AventStack.ExtentReports;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MRO.ROI.Automation.Selenium;
using MRO.ROI.Automation.Utility;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Reflection;
using static MRO.ROI.Automation.Utility.IniFile;

namespace MRO.ROI.Automation.Pages
{
    public class ROIFacilityEnterpriseDashboardPage
    {
        public RemoteWebDriver Driver;
        public ExtentTest logger;
        public TestContext Context;
        public CSVReader csvReader;

        public string csvFileName = "TurnAroundReport.csv";
        public ROIFacilityEnterpriseDashboardPage(RemoteWebDriver driver, ExtentTest _loger, TestContext _context)
        {
            Driver = driver;
            logger = _loger;
            Context = _context;
        }
        public By turnAroundReportFrame = By.XPath("//iframe[starts-with(@id,'rdFrame')]");
        public By selectDatePicker = By.XPath("//span[@id='daterange']//span[@id='caret']");
        public By selectFromDate = By.XPath("((//table[@class='table-condensed'])[2]//tr//td[contains(text(),'1')])[1]");
        public By selectToDate = By.XPath("((//table[@class='table-condensed'])[1]//tr//td[contains(text(),'1')])[13]");
        public By btnApply = By.XPath("//button[text()='Apply']");
        public By btnCreateReport = By.XPath("//input[@id='btn_submit']");
        public By dateRange = By.XPath("//div[@id='Daterangepicker']//span");
        public By lastYearList = By.XPath("//div[@class='ranges']//ul//li[10]");
        public By reportingGroupDrp = By.XPath("//select[@id='nFacilityReportingGroupID']");
        public By locationDrp = By.XPath("//select[@id='nLocationID']");
        public By excludeRadioBtn = By.XPath("//input[@id='inpExcludeDisplayOnly_1']");
        public By createReportBtn = By.XPath("//input[@id='btn_submit']");
        public By requestOverDaysFrame = By.XPath("//iframe[@id ='srRequestOverDays']");
        public By requesterTypeVal = By.XPath("//*[@id='colRequesterType_Row1']/span");
        public By patientDirectedVal = By.XPath("//*[@id='colPatientDirective_Row1']/span");
        public By lnkToday = By.XPath("//div[@class='ranges']/ul/li[1]");

        public By selectDate = By.XPath("//div[@class='calendar left']//table[@class='table-condensed']//tbody/tr[1]/td[5]");

        public By selectMonthDrp = By.XPath("//div[@class ='calendar left']//select[@class='monthselect']");
        public By selectYearDrp = By.XPath("//div[@class ='calendar left']//select[@class='yearselect']");


        /// <summary>
        /// Create new Turn Around Report
        /// </summary>
        public string CreateEnterpriseDashboardReport()
        {
            try
            {
                Driver.Wait(TimeSpan.FromSeconds(15));

                int currentDate = DateTime.Now.Day;
                int previousDate = currentDate - 4;
                //int previousDate = 30;
                string fromDate = $"((//table[@class='table-condensed'])[2]//tr//td[contains(text(),'{(previousDate)}')])[1]";
                string toDate = $"((//table[@class='table-condensed'])[1]//tr//td[contains(text(),'{(currentDate)}')])[1]";
                IWebElement frame = Driver.FindElementBy(turnAroundReportFrame);
                Driver.SwitchTo().Frame(frame);

                Driver.Click(selectDatePicker);
                Driver.Wait(TimeSpan.FromSeconds(2));
                Driver.Click(By.XPath("//div[@class='ranges']//ul//li[1]"));
                Driver.Wait(TimeSpan.FromSeconds(2));

                Driver.Click(selectDatePicker);
                Driver.Wait(TimeSpan.FromSeconds(2));
                Driver.Click(By.XPath(fromDate));
                Driver.Wait(TimeSpan.FromSeconds(5));
                Driver.Click(By.XPath(toDate));
                Driver.Wait(TimeSpan.FromSeconds(5));
                IWebElement applyButton = Driver.FindElementBy(btnApply);
                applyButton.Click();
                string dateRange = Driver.GetText(By.XPath("//span[@id='range-label']"));
                dateRange = dateRange.Replace("to", "-");
                Driver.FindElementBy(btnCreateReport).Click();
                Driver.Wait(TimeSpan.FromSeconds(2));

                //Driver.SwitchTo().DefaultContent();
                return dateRange;

            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to create Enterprise dashboard report exception detail as : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }

        }
        public string VerifyRequestAgingDateRange()
        {
            try
            {

                string range = Driver.GetText(By.XPath("(//span[@id='dbrdEnterprise']//table//tr[@id='rdDashboardPanels']//label[@id='rdDashboardCaptionID'])[8]"));
                range = range.Split(':')[1].ToString().Trim();
                return range;
                

            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to verify date range exception detail as : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }

       

        public void CreateEnterpriseDashboardReportPage()
        {
            try
            {
                Driver.SleepTheThread(10);
                Automation.Common.Iframe frame = new Automation.Common.Iframe(Driver, logger, Context);
                frame.SwitchToRoiFrame();
                Driver.Wait(TimeSpan.FromSeconds(1));
                frame.SwitchToRDFrame();
                Driver.Wait(TimeSpan.FromSeconds(1));
                Driver.ScrollIntoViewSmoothly(dateRange);
                Driver.Click(dateRange);
                Driver.Wait(TimeSpan.FromSeconds(5));
                logger.Log(Status.Info, "Select last year as date range");
                Driver.ScrollIntoViewSmoothly(lastYearList);
                Driver.Click(lastYearList);
                var reportingGroup = Driver.FindElementBy(reportingGroupDrp);
                var selectElement1 = new SelectElement(reportingGroup);
                logger.Log(Status.Info, "Select Group by as [None]");
                selectElement1.SelectByText("[None]");
                var location = Driver.FindElementBy(locationDrp);
                logger.Log(Status.Info, "Select location as [All]");
                var selectElement2 = new SelectElement(location);
                selectElement2.SelectByText("[All]");
                string _isChecked = Driver.FindElementBy(excludeRadioBtn).GetAttribute("checked").ToString();
                if (_isChecked == "false")
                {
                    Driver.Click(excludeRadioBtn);
                }
                logger.Log(Status.Info, "'Exclude' radio button selected");
                Driver.Click(createReportBtn);
                logger.Log(Status.Info, "'Create Report' button clickced");
                Driver.SleepTheThread(5);
                Driver.WaitForPageToLoad();
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to create enterprise dashboard report Exception detail as new Exception: {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");

            }
        }

    
        public bool MinimizeAndVerifyHorizontalScrollStatus()
        {
            try
            {
                bool _isHorizontalDisplayed = false;
                Driver.SwitchTo().DefaultContent();
                Driver.Manage().Window.Minimize();
                IJavaScriptExecutor javascript = (IJavaScriptExecutor)Driver;
                //Check If horizontal scroll Is present or not.
                Boolean horizontalScrollBar = (Boolean)javascript.ExecuteScript("return document.documentElement.scrollWidth>document.documentElement.clientWidth;");
                if (horizontalScrollBar == true)
                {
                    _isHorizontalDisplayed = true;
                }
                else
                {
                    _isHorizontalDisplayed = false;
                }
                return _isHorizontalDisplayed;
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to minimize and verify horizontal scroll status Exception detail as new Exception: {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");

            }
        }
        public bool VerifyVerticalScrollStatus()
        {
            try
            {
                bool _isVerticalDisplayed = false;
                Driver.SwitchTo().DefaultContent();
                Driver.Manage().Window.Minimize();
                IJavaScriptExecutor javascript = (IJavaScriptExecutor)Driver;
                //Check If vertical scroll Is present or not.
                Boolean verticalScrollBar = (Boolean)javascript.ExecuteScript("return document.documentElement.scrollHeight>document.documentElement.clientHeight;");
                if (verticalScrollBar == true)
                {
                    _isVerticalDisplayed = true;
                }
                else
                {
                    _isVerticalDisplayed = false;
                }
                Driver.WaitInSeconds(3);
                return _isVerticalDisplayed;
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to verify vertical scroll status Exception detail as new Exception: {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");

            }
        }


        public int Validatebargraphs()
        {
            // bool isValidated = false;
            try
            {

                IWebElement frame = Driver.FindElementBy(requestOverDaysFrame);
                Driver.SwitchTo().Frame(frame);

                IWebElement parent = Driver.FindElementBy(By.ClassName("highcharts-series-group"));
                ReadOnlyCollection<IWebElement> children = parent.FindElements(By.TagName("rect"));

                if (children.Count == 0)
                {
                    logger.Log(Status.Info, "No data to validate request aging details page");
                }

                if (children.Count >= 1)
                {
                    Actions action = new Actions(Driver);
                    for (int z = 0; z < 1; z++)
                    {

                        children[z].Click();
                        Driver.SleepTheThread(2);
                        string tab1 = Driver.WindowHandles[0];
                        string tab2 = Driver.WindowHandles[1];
                        string tab3 = Driver.WindowHandles[2];
                        Driver.SwitchTo().Window(tab3);
                    }

                }
                return children.Count;

            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to to validate bar graph {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }


        }
      
        public bool FilterReportBasedOnRequestDate(string requestId)
        {
            bool isPresent = false;
            int retryCount = 0;
            try
            {
                Driver.SleepTheThread(5);
                int totalPageCount = Convert.ToInt32(Driver.GetText(By.XPath("//span[@id='dtEnterpriseRequestAgingDetail-PageOfPages']")));
                if (totalPageCount > 1)
                {

                    IJavaScriptExecutor js = (IJavaScriptExecutor)Driver;
                    js.ExecuteScript($"document.getElementById('dtEnterpriseRequestAgingDetail-PageNr').setAttribute('value', '{totalPageCount}')");
                    Driver.ClickOnDisplayedElement(By.Id("dtEnterpriseRequestAgingDetail-PageNr"));
                    Driver.FindElementBy(By.Id("dtEnterpriseRequestAgingDetail-PageNr")).SendKeys(Keys.Enter);
                    Driver.DirectClick(By.XPath("//span[@id='dtEnterpriseRequestAgingDetail-PageOfPages']"));
                    isPresent = CheckRequestIdExsist(requestId);
                }
                else
                {
                    isPresent = CheckRequestIdExsist(requestId);
                }
                return isPresent;

            }

            catch (Exception ex)
            {

                throw new Exception($"Failed to filter report based on request date {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }


        public bool CheckRequestIdExsist(string requestId)
        {
            bool isValidated = false;
            try
            {
                var requestElements = Driver.FindElementsBy(By.XPath("//a[contains(@id,'actLinkParent_Row')]"));
                foreach (var requestElement in requestElements)
                {
                    if (requestElement.Text.Equals(requestId))
                    {
                        isValidated = true;
                        break;
                    }
                }

                //string tab3 = Driver.WindowHandles[2];
                //Driver.SwitchTo().Window(tab3).Close();
            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to check request id aganist request table {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
            return isValidated;
        }

        public void CloseSecondTab()
        {
            try
            {
                //string tab1 = Driver.WindowHandles[0];
                string tab2 = Driver.WindowHandles[1];
                Driver.SwitchTo().Window(tab2).Close();
                Driver.SleepTheThread(2);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void SwitchToTab()
        {
            string tab4 = Driver.WindowHandles[3];
            Driver.SwitchTo().Window(tab4);
        }
        public string VerifyRequesterType()
        {
            try
            {
                string requesterType = Driver.GetText(requesterTypeVal);
                return requesterType;
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to return  {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }

        public string VerifyPatientDirected()
        {
            try
            {
                string patientDirected = Driver.GetText(patientDirectedVal);
                return patientDirected;
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to return patient directed  {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }

        public void ClickOnCreatedPatient(string requestId)
        {
            try
            {

                Driver.Click(By.XPath($"//span[contains(text(),'{(requestId)}')]"));

            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to click on patient name {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }

        public void ApplyFiltersAndCreateReport()
        {
            try
            {
                Automation.Common.Iframe frame = new Automation.Common.Iframe(Driver, logger, Context);
                frame.SwitchToRDFrame();
                IWebElement datePicker = Driver.FindElementBy(selectDatePicker);
                datePicker.Click();
                Driver.Click(lnkToday);
                datePicker.Click();
                SelectElement oSelect1 = new SelectElement(Driver.FindElementBy(selectMonthDrp));
                oSelect1.SelectByText("Mar");
                SelectElement oSelect2 = new SelectElement(Driver.FindElementBy(selectYearDrp));
                oSelect2.SelectByText("2018");
                Driver.Click(selectDate);
                IWebElement applyButton = Driver.FindElementBy(btnApply);
                applyButton.Click();
                var reportingGroup = Driver.FindElementBy(reportingGroupDrp);
                var selectElement1 = new SelectElement(reportingGroup);
                selectElement1.SelectByText("[None]");
                var location = Driver.FindElementBy(locationDrp);
                var selectElement2 = new SelectElement(location);
                selectElement2.SelectByText("[All]");
                Driver.Click(btnCreateReport);
                Driver.SleepTheThread(3);
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to create Enterprise Dashboard report : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }

        }


     
    }

}
