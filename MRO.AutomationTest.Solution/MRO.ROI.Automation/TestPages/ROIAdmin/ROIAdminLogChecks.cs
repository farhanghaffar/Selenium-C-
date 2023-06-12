using MRO.ROI.Automation.Selenium;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MRO.ROI.Automation.Pages
{
    public class ROIAdminLogChecks
    {

        public static void roiCheckNbr()
        {
            Driver.Wait(TimeSpan.FromSeconds(5));
            Driver.FindElement(By.Id(PageElements.ROIAdminList.roichecknbr_id)).SendKeys("0786");
            Driver.Wait(TimeSpan.FromSeconds(2));
        }
        public static string roiamountdue()
        {
            return Driver.FindElement(By.Id(PageElements.ROIAdminList.roibaldue_id)).Text;
        }

        public static void paymentamount(String paymentAmt)
        {
            Driver.Wait(TimeSpan.FromSeconds(2));
            Driver.FindElement(By.Id(PageElements.ROIAdminList.roipaymentamt_id)).SendKeys(paymentAmt);
            Driver.Wait(TimeSpan.FromSeconds(2));

        }
        public static void roilogchkviewrequester()
        {
            Driver.FindElement(By.Id(PageElements.ROIAdminList.roilogcheckviewrequester_id)).Click();
        }

        }
    }

