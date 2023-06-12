using MRO.ROI.Automation.Selenium;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using System;

namespace MRO.ROI.Automation.Pages
{
    public class RequestStatus:ROIBasePage
    {

        public static void roiAssignReq()
        {
            Driver.Wait(TimeSpan.FromSeconds(5));
            Driver.FindElement(By.Id(PageElements.ROIAdminList.roiadminassgnreq_id)).Click();
        }

        public static void roiAdminSearch(string sSearch = "Test Attorney")
        //Here I am typig test in search text box and click on search button.
        {
            Driver.Wait(TimeSpan.FromSeconds(5));
            Driver.FindElement(By.Id(PageElements.ROIAdminList.roiadminorg_id)).SendKeys(sSearch);
            Driver.FindElement(By.Id(PageElements.ROIAdminList.roiadminsearchbtn_id)).Click();
            Driver.Wait(TimeSpan.FromSeconds(5));
            scrollIntoView("mrocontent_RadAjaxPanelRequesterDetail");
            Driver.FindElement(By.Id(PageElements.ROIAdminList.roiadminchkbox_id)).Click();
            Driver.FindElement(By.Id(PageElements.ROIAdminList.oiadmincpyallbtn_id)).Click();
        }
        public static void roiSaveDonebtn()
        //After copying all 
        {
            Driver.Wait(TimeSpan.FromSeconds(5));
            Driver.FindElement(By.Id(PageElements.ROIAdminList.roisavebtn_id)).Click();
            Driver.Wait(TimeSpan.FromSeconds(2));
            Driver.FindElement(By.Id(PageElements.ROIAdminList.roidonebtn_id)).Click();
            Driver.Wait(TimeSpan.FromSeconds(2));
            //Driver.FindElement(By.Id(PageElements.ROIAdminList.roidonebtn_id)).Click();
            //    Driver.FindElement(By.Id(PageElements.ROIAdminList.roidlogoutbtn_id)).Click();

        }
        public static void roiAdminapplyrate()
        {
            Driver.Wait(TimeSpan.FromSeconds(5));
            Driver.FindElement(By.Id(PageElements.ROIAdminList.roiadminapplyrate_id)).Click();
            Driver.Wait(TimeSpan.FromSeconds(15));

        }

        public static void qcStatus()
        {
            //Driver.Wait(TimeSpan.FromSeconds(2));
            Driver.FindElement(By.Id(PageElements.ROIAdminList.roiadminqcpass_id)).Click();
            //Driver.Wait(TimeSpan.FromSeconds(5));
        }

        public static void applyRate()
        {
            //Driver.Wait(TimeSpan.FromSeconds(5));
            Driver.FindElement(By.Id(PageElements.ROIAdminList.roiadmchgrate_id)).Click();
            //Driver.Wait(TimeSpan.FromSeconds(5));

        }

        public static void roiCreateInvoice()
        {
			Driver.FindElement(By.Id("mrocontent_cmdCreateInvoice")).Click();
            //Driver.Wait(TimeSpan.FromSeconds(5));
            //Driver.FindElement(By.Id(PageElements.ROIAdminList.roiadmcreateinvoice_id)).Click();
			Driver.Wait(TimeSpan.FromSeconds(2));
			//WebElementHelper.Click_Enter();
            //Driver.Wait(TimeSpan.FromSeconds(5));
            Driver.FindElement(By.XPath(PageElements.ROIAdminList.confirmDialog_xpath)).Click();
            //*[@aria-describedby='dialog-confirm']//*[text()='Yes']
            //  return Driver.FindElement(By.XPath(PageElements.ROIAdminList.invoicetext_xpath)).Text;
            string invoice = Driver.FindElement(By.XPath(PageElements.ROIAdminList.invoicetext_xpath)).Text;
            //Driver.Wait(TimeSpan.FromSeconds(5));
			logger.Info("Invoice Package Created");

			//Console.WriteLine("Checking invoice status " + invoice);

        }

        public static void roiLogCheck()
        {
            Driver.Wait(TimeSpan.FromSeconds(2));
            Driver.FindElement(By.Id(PageElements.ROIAdminList.roilogcheck_id)).Click();
        }

        public static void roirmvrequester()
        {
            Driver.Wait(TimeSpan.FromSeconds(2));
            Driver.FindElement(By.Id(PageElements.ROIAdminList.roiremoverequester_id)).Click();
            Driver.Wait(TimeSpan.FromSeconds(2));
        }

        public static void roiadmlogout()
        {
            Driver.Wait(TimeSpan.FromSeconds(2));
            Driver.FindElement(By.Id(PageElements.ROIAdminList.roidlogoutbtn_id)).Click();
        }

        public static void scrollIntoView(string locatorKey)
        {
            IWebElement element = getElement(locatorKey);
            RemoteWebDriver dr = Driver.Instance;
            IJavaScriptExecutor js = (IJavaScriptExecutor)dr;
            js.ExecuteScript("arguments[0].scrollIntoView(true);", element);
        }
        private static IWebElement getElement(string v)
        {
            return Driver.FindElement(By.Id(v));

        }
    }
}
