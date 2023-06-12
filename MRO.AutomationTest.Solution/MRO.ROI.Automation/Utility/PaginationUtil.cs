using System;
using AventStack.ExtentReports;
using MRO.ROI.Automation.Selenium;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;

namespace MRO.ROI.Automation.Utility
{
    public class PaginationUtil
    {
        public RemoteWebDriver Driver;
        public ExtentTest logger;
        public PaginationUtil(RemoteWebDriver driver, ExtentTest _loger)
        {
            Driver = driver;
            logger = _loger;
        }


        public  bool SearchPaginatedList(string searchFor)
        {
            var totalPages = 5;
            try
            {
                totalPages = Int32.Parse(Driver.FindElement(By.XPath("//td[contains(text(), 'Page:')]/a[1]")).Text);
            }
            catch
            {
                try
                {
                    Driver.FindElement(By.XPath($"//td[text()={searchFor}]"));
                    return true;
                }
                catch
                {
                    return false;
                }
            }
            var i = 2;
            var exists = false;
            while (i <= totalPages)
            {
                try
                {
                    Driver.FindElement(By.XPath($"//td[text()={searchFor}]"));
                    exists = true;
                }
                catch
                {

                }
                Driver.FindElement(By.XPath($"//td[contains(text(), 'Page:')]/a[contains(text(), '{i}')]")).Click();
                i++;
            }
            logger.Pass("Success: RequestID does not appear in QC Queue prior to one hour after entered");
            return exists;
        }

    }
}
