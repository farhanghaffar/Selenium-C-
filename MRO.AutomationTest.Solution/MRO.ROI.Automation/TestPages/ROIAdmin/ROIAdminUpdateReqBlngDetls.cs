using MRO.ROI.Automation.Selenium;
using OpenQA.Selenium;
using System;


namespace MRO.ROI.Automation.Pages
{
    public class ROIAdminUpdateReqBlngDetls
    {

        public static void pageFees()
        {
            Driver.Wait(TimeSpan.FromSeconds(5));
            Driver.FindElement(By.Id(PageElements.ROIAdminList.storedonpaper1_id)).SendKeys("10");
            Driver.Wait(TimeSpan.FromSeconds(2));
            Driver.FindElement(By.Id(PageElements.ROIAdminList.storedelectronically1_id)).SendKeys("10");
            Driver.Wait(TimeSpan.FromSeconds(2));
            Driver.FindElement(By.Id(PageElements.ROIAdminList.updatenapply_id)).Click();
            Driver.Wait(TimeSpan.FromSeconds(2));
            Driver.FindElement(By.Id(PageElements.ROIAdminList.roiadmsavenexitbtn_id)).Click();
        }
        public static void pageFees2()
        {
            Driver.Wait(TimeSpan.FromSeconds(5));
            Driver.FindElement(By.Id(PageElements.ROIAdminList.roiadmpagefee2_id)).Clear();
            Driver.Wait(TimeSpan.FromSeconds(2));
            Driver.FindElement(By.Id(PageElements.ROIAdminList.roiadmpagefee2_id)).SendKeys("2.00");
            Driver.Wait(TimeSpan.FromSeconds(2));
            Driver.FindElement(By.Id(PageElements.ROIAdminList.roiadmsavenexitbtn_id)).Click();
            // Driver.FindElement(By.XPath(PageElements.ROIAdminList.roiadmsavenexitbtn_xpath)).Click();
            Driver.Wait(TimeSpan.FromSeconds(2));
        }


    }
}


