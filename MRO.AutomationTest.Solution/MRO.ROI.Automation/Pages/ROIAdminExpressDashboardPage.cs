using AventStack.ExtentReports;
using MRO.ROI.Automation.Selenium;
using System;
using OpenQA.Selenium;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium.Remote;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace MRO.ROI.Automation.Pages
{
    public class ROIAdminExpressDashboardPage
    {
        public RemoteWebDriver Driver;
        public ExtentTest logger;
        public TestContext Context;
        public ROIAdminExpressDashboardPage(RemoteWebDriver driver, ExtentTest _loger, TestContext _context)
        {
            Driver = driver;
            logger = _loger;
            Context = _context;
        }

        public By requestSummaryLabels = By.XPath("//span[@class='summary-text']");
        public By dashboardTitleCaptions = By.XPath("//label[@class='rdDashboardTitleCaption']");
        public By selectTimeFrameLabel = By.XPath("//span[contains(text(),'Selected Time Frame')]");
        public By userData = By.XPath("//*[contains(text(),'Usage Data')]");
        public By requestData = By.XPath("//span[contains(text(),'Request Data')]");
        public By rdFrame = By.XPath("//iframe[starts-with(@id,'rdFrame')]");
        public By srVolumeByDay = By.XPath("//iframe[@id='srVolumeByDay']");
        public By srRecipientInformation = By.XPath("//iframe[@id='srRecipientInformation']");
        public By srRecordType = By.XPath("//iframe[@id='srRecordType']");
        public By dtRecordType_eXpress = By.XPath("//table[@id='dtRecordType_eXpress']//tr//span[@class='ThemeAlignCenter ThemeTextLarge']");
        public By div_excel_button = By.XPath("//span[@id='div_excel_button']");
        public By dtFeedback_eXpress = By.XPath("//table[@id='dtFeedback_eXpress']//th//a");

        public List<IWebElement> GetRequestSummaryLabels()
        {
            try
            {

                Driver.SwitchTo().Frame("srSummaryViews");
                Driver.SleepTheThread(4);
                return Driver.FindElementsBy(requestSummaryLabels);
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to get request summary labels Message : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }

        public void ClickOnRequestDataTab()
        {
            try
            {
                Driver.Click(requestData);
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to click on request data tab Message : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }

        public List<IWebElement> GetDashboardTitleCaptions()
        {
            try
            {
                Driver.SwitchTo().DefaultContent();
                IWebElement frame = Driver.FindElementBy(rdFrame);
                Driver.SwitchTo().Frame(frame);
                return Driver.FindElementsBy(dashboardTitleCaptions);
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to get dashboard title captions Message : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }

        public IWebElement GetSelectedTimeFrameLabel()
        {

            try
            {
                Driver.SwitchTo().DefaultContent();
                IWebElement frame = Driver.FindElementBy(rdFrame);
                Driver.SwitchTo().Frame(frame);
                Driver.SwitchTo().Frame("srSummaryViews");
                return Driver.FindElementBy(selectTimeFrameLabel);
            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to get time frame label Message : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }

        public bool RequestCountCheckForDSCOrder(string id)
        {
            bool isValidated = false;
            try
            {
                Driver.SwitchTo().DefaultContent();
                IWebElement frame = Driver.FindElementBy(rdFrame);
                Driver.SwitchTo().Frame(frame);
                IWebElement iframeElement = Driver.FindElementBy(srVolumeByDay);
                Driver.SwitchTo().Frame(iframeElement);
                List<IWebElement> records = Driver.FindElementsBy(By.XPath($"//table[@id='{id}']//tr//span[@class='ThemeAlignCenter ThemeTextLarge']"));

                List<int> actualRecords = new List<int>();
                List<int> expectedRecords = actualRecords;
                foreach (var record in records)
                {
                    actualRecords.Add(Convert.ToInt32(record.Text));
                }

                expectedRecords.Reverse();
                if (Enumerable.SequenceEqual(actualRecords, expectedRecords))
                {
                    isValidated = true;
                }
            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to verify request count values for DESC order Message : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
            return isValidated;
        }

        public bool RequestCountCheckForDSCOrderOnUserData(string id)
        {
            bool isValidated = false;
            try
            {
                Driver.SwitchTo().DefaultContent();
                IWebElement frame = Driver.FindElementBy(rdFrame);
                Driver.SwitchTo().Frame(frame);
                IWebElement iframeElement = Driver.FindElementBy(srRecipientInformation);
                Driver.SwitchTo().Frame(iframeElement);
                List<IWebElement> records = Driver.FindElementsBy(By.XPath($"//table[@id='{id}']//tr//span[@class='ThemeAlignCenter ThemeTextLarge']"));

                List<int> actualRecords = new List<int>();
                List<int> expectedRecords = actualRecords;
                foreach (var record in records)
                {
                    actualRecords.Add(Convert.ToInt32(record.Text));
                }

                expectedRecords.Reverse();
                if (Enumerable.SequenceEqual(actualRecords, expectedRecords))
                {
                    isValidated = true;
                }
            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to verify request count values for DESC order Message : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
            return isValidated;
        }


        public bool RequestCountCheckForDSCOrderOnUserDataTab(string id)
        {
            bool isValidated = false;
            try
            {
                Driver.SwitchTo().DefaultContent();
                IWebElement frame = Driver.FindElementBy(rdFrame);
                Driver.SwitchTo().Frame(frame);
                IWebElement iframeElement = Driver.FindElementBy(srRecipientInformation);
                Driver.SwitchTo().Frame(iframeElement);
                List<IWebElement> records = Driver.FindElementsBy(By.XPath($"//table[@id='{id}']//tr//span[@class='ThemeAlignCenter ThemeTextLarge']"));

                List<int> actualRecords = new List<int>();
                List<int> expectedRecords = actualRecords;
                foreach (var record in records)
                {
                    actualRecords.Add(Convert.ToInt32(record.Text));
                }

                expectedRecords.Reverse();
                if (Enumerable.SequenceEqual(actualRecords, expectedRecords))
                {
                    isValidated = true;
                }
            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to verify request count values for DESC order Message : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
            return isValidated;
        }


        public bool RecordTypeCheckForDSCOrder()
        {
            bool isValidated = false;
            try
            {
                Driver.SwitchTo().DefaultContent();
                IWebElement frame = Driver.FindElementBy(rdFrame);
                Driver.SwitchTo().Frame(frame);
                IWebElement iframeElement = Driver.FindElementBy(srRecordType);
                Driver.SwitchTo().Frame(iframeElement);

                List<IWebElement> recordTypes = Driver.FindElementsBy(dtRecordType_eXpress);

                List<double> actualRecords = new List<double>();
                List<double> expectedRecords = actualRecords;
                foreach (var record in recordTypes)
                {
                    actualRecords.Add(Convert.ToDouble(record.Text.Replace('%', ' ').Trim()));
                }

                expectedRecords.Reverse();
                if (Enumerable.SequenceEqual(actualRecords, expectedRecords))
                {
                    isValidated = true;
                }
            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to verify record type values for DESC order Message : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
            return isValidated;
        }

        public void ScrollAndClickOnExcelIcon(IWebElement excelElement)
        {
            int i = 0;
            while (i <= 3)
            {
                try
                {
                    OpenQA.Selenium.Interactions.Actions actions = new OpenQA.Selenium.Interactions.Actions(Driver);
                    actions.MoveToElement(excelElement).Perform();
                    excelElement.Click();
                    break;
                }
                catch (Exception ex)
                {
                    i++;
                    Driver.SleepTheThread(1);
                }
            }
        }

        public void DownloadTheExcelFiles()
        {
            try
            {
                Driver.SwitchTo().DefaultContent();
                IWebElement frame = Driver.FindElementBy(rdFrame);
                Driver.SwitchTo().Frame(frame);
                Driver.SleepTheThread(4);
                var excelElements = Driver.FindElements(div_excel_button);
                foreach (var excelElement in excelElements)
                {
                    Driver.JavaScriptClickWithElement(excelElement);
                    Driver.SleepTheThread(8);
                    excelElements = Driver.FindElements(div_excel_button);
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to download the excel files Message : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }

        public static void DeleteDownloadedExcelFiles()
        {
            try
            {
                List<string> fileNames = new List<string> { "VolumeByMonth", "VolumeByLocation", "Request-Information", "RecordType", "RequestReason", "DeliveryMethod" };
                string userRoot = System.Environment.GetEnvironmentVariable("USERPROFILE");
                string downloadFolder = System.IO.Path.Combine(userRoot, "Downloads\\");
                string[] filePaths = System.IO.Directory.GetFiles(downloadFolder, "*");

                foreach (var filename in fileNames)
                {
                    foreach (string filePath in filePaths)
                    {
                        string ext = filePath.Split('.')?[1];
                        if (ext == "xls" && filePath.Contains(filename))
                        {
                            File.Delete(filePath);
                        }
                    }

                }
            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to delete downloaded excel files Message : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }

        public static void DeleteDownloadedExcelFilesOnUserDataTab()
        {
            try
            {
                List<string> fileNames = new List<string> { "RequesterInformation", "DesktopVsMobile", "PatientAge", "Feedback" };
                string userRoot = System.Environment.GetEnvironmentVariable("USERPROFILE");
                string downloadFolder = System.IO.Path.Combine(userRoot, "Downloads\\");
                string[] filePaths = System.IO.Directory.GetFiles(downloadFolder, "*");

                foreach (var filename in fileNames)
                {
                    foreach (string filePath in filePaths)
                    {
                        string ext = filePath.Split('.')?[1];
                        if (ext == "xls" && filePath.Contains(filename))
                        {
                            File.Delete(filePath);
                        }
                    }

                }
            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to delete downloaded excel files Message : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }

        public bool VerifyExcelFilesAreDownloadedForAllTitles()
        {

            bool isValidated = false;
            int count = 0;
            List<string> fileNames = new List<string> { "VolumeByMonth", "VolumeByLocation", "Request-Information", "RecordType", "RequestReason", "DeliveryMethod" };
            string userRoot = System.Environment.GetEnvironmentVariable("USERPROFILE");
            string downloadFolder = System.IO.Path.Combine(userRoot, "Downloads\\");
            string[] filePaths = System.IO.Directory.GetFiles(downloadFolder, "*");

            try
            {
                foreach (var filename in fileNames)
                {
                    foreach (string filePath in filePaths)
                    {
                        string ext = filePath.Split('.')?[1];
                        if (ext == "xls" && filePath.Contains(filename))
                        {
                            count++;
                            break;
                        }
                    }

                }

                if (fileNames.Count == count)
                {
                    isValidated = true;
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to download the excel files Message : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
            finally
            {
                foreach (var filename in fileNames)
                {
                    foreach (string filePath in filePaths)
                    {
                        string ext = filePath.Split('.')?[1];
                        if (ext == "xls" && filePath.Contains(filename))
                        {
                            File.Delete(filePath);
                            break;
                        }
                    }

                }

            }
            return isValidated;
        }

        public bool VerifyExcelFilesAreDownloadedForAllTitlesOnUserDataTab()
        {

            bool isValidated = false;
            int count = 0;
            List<string> fileNames = new List<string> { "RequesterInformation", "DesktopVsMobile", "PatientAge", "Feedback" };
            string userRoot = System.Environment.GetEnvironmentVariable("USERPROFILE");
            string downloadFolder = System.IO.Path.Combine(userRoot, "Downloads\\");
            string[] filePaths = System.IO.Directory.GetFiles(downloadFolder, "*");

            try
            {
                foreach (var filename in fileNames)
                {
                    foreach (string filePath in filePaths)
                    {
                        string ext = filePath.Split('.')?[1];
                        if (ext == "xls" && filePath.Contains(filename))
                        {
                            count++;
                            break;
                        }
                    }

                }

                if (fileNames.Count == count)
                {
                    isValidated = true;
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to download the excel files Message : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
            finally
            {
                foreach (var filename in fileNames)
                {
                    foreach (string filePath in filePaths)
                    {
                        string ext = filePath.Split('.')?[1];
                        if (ext == "xls" && filePath.Contains(filename))
                        {
                            File.Delete(filePath);
                        }
                    }

                }

            }
            return isValidated;
        }

        public void ClickOnUserDataTab()
        {
            try
            {
                Driver.ScrollIntoViewAndClick(userData);
            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to click on userdata tab Message : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }

        public List<IWebElement> GetExpressFeedbackColumns()
        {
            List<IWebElement> expressColumns;
            try
            {
                Driver.SleepTheThread(3);
                Driver.SwitchTo().DefaultContent();
                IWebElement frame = Driver.FindElementBy(rdFrame);
                Driver.SwitchTo().Frame(frame);
                Driver.SwitchTo().Frame("srFeedback");
                expressColumns = Driver.FindElementsBy(dtFeedback_eXpress);
                return expressColumns;
            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to get express feedback columns Message : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }
    }
}
