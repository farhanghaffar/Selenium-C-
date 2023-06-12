using AventStack.ExtentReports;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MRO.ROI.Automation.Selenium;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;

namespace MRO.ROI.Automation.Pages
{
    public class ROIAdminAuditLogPage
    {
        public RemoteWebDriver Driver;
        public ExtentTest logger;
        public TestContext Context;
        public ROIAdminAuditLogPage(RemoteWebDriver driver, ExtentTest _loger, TestContext _context)
        {
            Driver = driver;
            logger = _loger;
            Context = _context;
        }

        public By FromDate = By.XPath("//input[@id='mrocontent_txtDateA']");
        public By ToDate = By.XPath("//input[@id='mrocontent_txtDateZ']");
        public By Facility = By.XPath("//select[@id='mrocontent_lstFacilities']");
        public By Action = By.XPath("//select[@id='mrocontent_lstActions']");
        public By Search = By.XPath("//input[@id='mrocontent_cmdCreate']");
        public By DateColumn = By.XPath("//tr[@class='TableHeader']//td[1]");
        public By SSNViewed = By.XPath("//table[@id='mrocontent_tblReport']//tr[2]//tr[2]//td[2]");
        public By fromDate = By.XPath("//input[@id='mrocontent_txtDateA']");
        public By toDate = By.XPath("//input[@id='mrocontent_txtDateZ']");
        public By facilityDrp = By.XPath("//select[@id='mrocontent_lstFacilities']");
        public By actionDrp = By.XPath("//select[@id='mrocontent_lstActions']");
        public By clearSearchBtn = By.XPath("//input[@id='mrocontent_cmdClear']");
        public By searchBtn = By.XPath("//input[@id='mrocontent_cmdCreate']");
        public By requestIdElement = By.XPath("//tr[@class='TableBody']//td[4]");//
        public By pageSizeDrp = By.XPath("//select[@id='mrocontent_tblReport_lstPageSizes']");

        public By requestId = By.XPath("//input[@id='mrocontent_txtRequestID']");
        public By LookUpRequestId = By.XPath("//img[@title='Look up by Request ID']");
        public By ParentBusiness = By.XPath("//select[@id='mrocontent_lstParentBusinessID']");
        public void SearchRequestOnAuditLog(string facility, string action)
        {
            try
            {
                var todaysDate = String.Format("{0:M/dd/yyyy}", DateTime.Now).Replace("-", "/");
                Driver.Click(clearSearchBtn);
                Driver.Wait(TimeSpan.FromSeconds(2));
                Driver.SendKeys(fromDate, todaysDate);
                Driver.SendKeys(toDate, todaysDate);
                Driver.SendKeys(facilityDrp, facility);
                Driver.Wait(TimeSpan.FromSeconds(2));
                Driver.SendKeys(actionDrp, action);
                Driver.Wait(TimeSpan.FromSeconds(2));
                Driver.Click(searchBtn);


            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to create report with details as {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }
        public bool CheckRequestIdExsist(string requestId)
        {
            bool isValidated = false;
            try
            {
                var requestElements = Driver.FindElementsBy(By.XPath("//a[starts-with(@title,'View Request')]"));
                foreach (var requestElement in requestElements)
                {

                    string request = requestElement.Text;
                    request = request.Split('#')[1].Replace(')', ' ').Trim();
                    if (request.Equals(requestId))
                    {
                        isValidated = true;
                        break;
                    }
                   
                }
               
            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to check request id aganist request table {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
            return isValidated;
        }

        public bool VerifyRequest(string requestId)
        {
            bool isValidated = false;
            try
            {
                WebElementHelper helper = new WebElementHelper(Driver, logger, Context);
                string requestVal = "//tr[@class='TableBody']//td[7]";
                string rowXpath = "//table[@id='mrocontent_dgReport']//tr[";
                string colXpath = "]//td[7]";
                string noRecordsElement = "//*[@id='mrocontent_dgReport']/tbody/tr/td";
                int rowCount = Driver.FindElements(By.XPath("//table[@id='mrocontent_dgReport']//tr")).Count;
                if(rowCount>1)
                {
                    for (int i = 2; i <= rowCount; i++)
                    {
                        string actualXpath = rowXpath + i + colXpath;
                        IWebElement reqId = Driver.FindElementBy(By.XPath(actualXpath));
                        string value = Driver.GetText(By.XPath(actualXpath));
                        if(value==" ")
                        {
                            i++;
                        }
                        else
                        {
                            string request = reqId.Text;
                            request = request.Split('#')[1].Replace(')', ' ').Trim();
                            if (request.Equals(requestId))
                            {
                                isValidated = true;
                                break;
                            }
                        }
                        

                    }
                }

                else
                {

                    if (helper.IsElementPresent(noRecordsElement))
                    {

                        isValidated = false;

                    }
                }


                return isValidated;
            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to check request id aganist request table {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }

        }

        public string VerifyActionData()
        {
            try
            {

                string action = Driver.GetText(By.XPath("//*[@id='mrocontent_dgReport']/tbody/tr[2]/td[2]"));
                return action;
            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to verify action data with details as: {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }

        public string VerifyInfo()
        {
            try
            {

                string action = Driver.GetText(By.XPath("//*[@id='mrocontent_dgReport']/tbody/tr[2]/td[5]"));
                return action;
            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to  verify info with details as: {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }

        public void SetAuditFiltersAndSearch()
        {
            try
            {
                IWebElement fromDate = Driver.FindElementBy(FromDate);
                fromDate.SendKeys(DateTime.Now.ToShortDateString());

                IWebElement toDate = Driver.FindElementBy(ToDate);
                toDate.SendKeys(DateTime.Now.ToShortDateString());
                IWebElement facility = Driver.FindElementBy(Facility);
                facility.SendKeys("ROI Test Facility");
                IWebElement action = Driver.FindElementBy(Action);
                action.SendKeys("Patient SSN Viewed");

                IWebElement search = Driver.FindElementBy(Search);
                search.Click();

            }
            catch (Exception ex)
            {
                throw new Exception($"Failed  to set audit filters as Message : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }

        public void VerifyResultsExist()
        {
            try
            {
                string dateColum = Driver.FindElementBy(DateColumn).Text;

                if (dateColum == "Date ")
                {
                    logger.Log(Status.Info, "Search results exist");
                }
                string ssnViewed = Driver.FindElementBy(SSNViewed).Text;
                Assert.AreEqual("Patient SSN Viewed", ssnViewed);

            }
            catch (Exception ex)
            {
                throw new Exception($"Failed  verify results as Message : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }

        public void CreateReportForAuditLog()
        {
            try
            {
                var todaysDate = String.Format("{0:M/dd/yyyy}", DateTime.Now).Replace("-", "/");
                Driver.Click(clearSearchBtn);
                Driver.Wait(TimeSpan.FromSeconds(2));
                Driver.SendKeys(fromDate, todaysDate);
                Driver.SendKeys(toDate, todaysDate);
                IWebElement facility = Driver.FindElementBy(Facility);
                facility.SendKeys("ROI Test Facility");
                //Driver.SendKeys(facilityDrp, "[All]");
                Driver.Wait(TimeSpan.FromSeconds(2));               
                Driver.Click(searchBtn);


            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to create report with details as {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }

        public string VerifyRequestInfo()
        {
            try
            {


                WebElementHelper helper = new WebElementHelper(Driver, logger, Context);
                string requestVal = "//tr[@class='TableBody']//td[7]";
                string rowXpath = "//table[@id='mrocontent_dgReport']//tr[";
                string colXpath = "]//td[13]";
                string noRecordsElement = "//*[@id='mrocontent_dgReport']/tbody/tr/td";
                int rowCount = Driver.FindElements(By.XPath("//table[@id='mrocontent_dgReport']//tr")).Count;
                string actualXpath = rowXpath + rowCount + colXpath;
                //IWebElement info = Driver.FindElementBy(By.XPath(actualXpath));
                string info = Driver.GetText(By.XPath(actualXpath));
                Driver.ScrollToElement(By.XPath(actualXpath));
                return info;
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to verify request info with details as {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }

        public void ApplyFiltersAndCreateReportForAuditLog()
        {
            try
            {
                var todaysDate = String.Format("{0:M/dd/yyyy}", DateTime.Now).Replace("-", "/");
                Driver.Click(clearSearchBtn);
                Driver.Wait(TimeSpan.FromSeconds(2));
                Driver.SendKeys(fromDate, todaysDate);
                Driver.SendKeys(toDate, todaysDate);
                IWebElement facility = Driver.FindElementBy(Facility);
                facility.SendKeys("ROI Test Facility");
                IWebElement action = Driver.FindElementBy(Action);
                action.SendKeys("Facility Activity - Report Generated");
                Driver.Wait(TimeSpan.FromSeconds(2));
                Driver.Click(searchBtn);
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to create report with details as {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }


        public bool ValidateSearchResults()
        {
            string type = string.Empty;
            string info = string.Empty;
            int searchResultsCount = 0;
            bool isRecordsFound = false;
            try
            {
                Driver.SendKeys(pageSizeDrp, "1000");
                List<IWebElement> tableDataRows = Driver.FindElementsBy(By.XPath("//table[@id='mrocontent_dgReport']//tr[@class='TableBody']"));

                for (int z = 0; z < tableDataRows.Count; z++)
                {
                    System.Collections.ObjectModel.ReadOnlyCollection<IWebElement> cells = tableDataRows[z].FindElements(By.TagName("td"));
                    info = cells[4].Text;
                    if (!string.IsNullOrEmpty(info) && info.Contains("Facility Dashboard"))
                    {
                        searchResultsCount = searchResultsCount + 1;
                    }
                }
                if (searchResultsCount > 0) { isRecordsFound = true; }               
            }

            catch (Exception ex)
            {
                throw new Exception($"Failed to get search results and validate the search data:{ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
            return isRecordsFound;

        }


        public void SearchRequestOnAuditLog(string facility, string action, string id)
        {
            try
            {
                var todaysDate = String.Format("{0:M/dd/yyyy}", DateTime.Now).Replace("-", "/");
                Driver.Click(clearSearchBtn);
                Driver.Wait(TimeSpan.FromSeconds(2));
                Driver.SendKeys(fromDate, todaysDate);
                Driver.SendKeys(toDate, todaysDate);
                Driver.SendKeys(facilityDrp, facility);
                Driver.Wait(TimeSpan.FromSeconds(2));
                Driver.SendKeys(actionDrp, action);
                Driver.Wait(TimeSpan.FromSeconds(2));
                Driver.SendKeys(requestId, id);
                Driver.Click(searchBtn);


            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to create report with details as {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }

        public string VerifyAdditionalInfo()
        {
            try
            {

                WebElementHelper helper = new WebElementHelper(Driver, logger, Context);
                string rowXpath = "//table[@id='mrocontent_dgReport']//tr[";
                string colXpath = "]//td[13]";
                int rowCount = Driver.FindElements(By.XPath("//table[@id='mrocontent_dgReport']//tr")).Count;
                string actualXpath = rowXpath + rowCount + colXpath;
                string info = Driver.GetText(By.XPath(actualXpath));
                Driver.ScrollToElement(By.XPath(actualXpath));
                return info;
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to verify additional info with details as {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
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
                Driver.Wait(TimeSpan.FromSeconds(2));

            }
            catch (Exception ex)
            {

                throw new Exception($"Failed with Message not able to click '?' : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }

        public void ApplyFiltersAndSearchData()
        {
            try
            {
                var todaysDate = String.Format("{0:M/dd/yyyy}", DateTime.Now).Replace("-", "/");
                Driver.Click(clearSearchBtn);
                Driver.Wait(TimeSpan.FromSeconds(2));
                Driver.SendKeys(fromDate, todaysDate);
                Driver.SendKeys(toDate, todaysDate);
                IWebElement facility = Driver.FindElementBy(Facility);
                facility.SendKeys("[All]");
                IWebElement action = Driver.FindElementBy(Action);
                action.SendKeys("Exported PHI in Report");
                Driver.Wait(TimeSpan.FromSeconds(2));
                IWebElement parentBusiness = Driver.FindElementBy(ParentBusiness);
                parentBusiness.SendKeys("[All]");
                Driver.Click(searchBtn);
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to create report with details as {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }
    }
}
