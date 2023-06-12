using AventStack.ExtentReports;
using AventStack.ExtentReports.MarkupUtils;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MRO.ROI.Automation.Common.Navigation;
using MRO.ROI.Automation.Pages;
using MRO.ROI.Automation.Selenium;
using MRO.ROI.Test.Utilities;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;

namespace MRO.ROI.Test
{
    class MRO_ROI_Rate_Test1
    {
        [TestClass]
        public class RoiAdminTest : ROITestBase
        {
            public object FacilityLoginPage { get; private set; }

            public RoiAdminTest() : base(ROITestArea.ROIAdmin)
            {

            }
            
            [DataRow("23625035")]
            [DataRow("23625036")]
            [DataRow("23625040")]
            [DataRow("23625041")]
            [DataRow("23625133")]
            [DataRow("23625134")]
            [TestMethod]
            //[TestCategory(ROITestCategory.Regression)]
            public void MRO_Rate_PhyDlvy_Test1(string reqID)
            {
                var rates = new List<string>();
                rates.Add("Rate1-3");
                rates.Add("Rate1-4");
                rates.Add("Rate1-5");
                rates.Add("Rate2-3");
                rates.Add("Rate2-6");
                rates.Add("Rate2-7");
                rates.Add("Rate2-6_EF");
                rates.Add("Rate1-4_EF");
                Driver.Wait(TimeSpan.FromSeconds(5));
                Driver.logger = Driver.extent.CreateTest("RequstID" + reqID);
                LoginOverride("ROI3-1-1-974");
                LoginOverride("ROI3-Ron");
                //LoginOverride("ROI3-1-1-974");

                foreach (var rate in rates)
                {
                    Driver.logger = Driver.extent.CreateTest("RequstID: " + reqID + " " + rate);
                    Driver.logger.Info("Version Number ROI3-1-1-974 "); //Toufeeq

                    Console.WriteLine(rate);
                    CompareRate(reqID, rate);
                    Driver.extent.Flush();
                }
            }
            //change from private to public
            public void CompareRate(string reqId, string rate)
            {
                try
                {
                    Driver.logger.Info(rate);

                    RateValues value1 = SetRate(reqId, rate);
                    LoginOverride("ROI3-1-1-974");
                    // LoginOverride("ROI3-Ron");
                    Driver.logger.Info("Version Number ROI3 - Ron "); //Toufeeq
                    RateValues value2 = SetRate(reqId, rate);
                    LoginOverride("ROI3-Ron");
                    //LoginOverride("ROI3-1-1-974");

                    Driver.logger.Info(value2.StroredPaper);
                    Driver.logger.Info(value1.StroredPaper);
                    //compare
                    if (value2.StroredPaper.Equals(value1.StroredPaper))
                    {
                        Driver.logger.Pass("Stored on paper values: On Versions ROI3-1-1-974 & ROI3-Ron are equals test Passed");
                    }
                    else
                    {
                        Driver.logger.Fail("Stored on paper values: On Versions ROI3-1-1-974 & ROI3-Ron are NOT equals test Failed");
                    }

                    if (value2.TotalCharges.Equals(value1.TotalCharges))
                    {
                        Driver.logger.Pass("TotalCharges on Versions ROI3-1-1-974 & ROI3-Ron are equals test Passed Passed");
                    }
                    else
                    {
                        Driver.logger.Fail("TotalCharges on Versions ROI3-1-1-974 & ROI3-Ron are NOT equals test Failed");
                    }
                    //    Driver.takeScreenShot();
                }
                catch (Exception ex)
                {
                    Driver.takeScreenShot();
                    Driver.logger.Log(Status.Fail, "Test failed with exception"); //Logging fail
                    Driver.logger.Log(Status.Error, MarkupHelper.CreateTable(
                        new string[,]
                        {
                        {"Exception", ex.Message
                 },
                        {"StackTrace", ex.StackTrace
                 }
            }));
                    Assert.Fail(ex.Message);
                }
            }

            private void LoginOverride(string version)
            {
                MenuSelector.SelectRoiAdmin("Users", "Admin List");

                Driver.Instance.Value.FindElement(By.Id(PageElements.ROIRequesterPortal.userlogin_id)).Clear();
                Driver.Instance.Value.FindElement(By.Id(PageElements.LogNewRequestPage.firstName_Id)).Clear();
                Driver.Instance.Value.FindElement(By.Id(PageElements.LogNewRequestPage.firstName_Id)).SendKeys("Sele");
                Driver.Instance.Value.FindElement(By.Id(PageElements.LogNewRequestPage.lastName_Id)).Clear();
                Driver.Instance.Value.FindElement(By.Id(PageElements.LogNewRequestPage.lastName_Id)).SendKeys("Auto");
                Driver.Instance.Value.FindElement(By.Id(PageElements.ROIRequesterPortal.roiReqeusterPortalSearchBtn_Id)).Click();
                Driver.Wait(TimeSpan.FromSeconds(3));
                Driver.Instance.Value.FindElement(By.XPath(PageElements.ROIRequesterPortal.adminverovrride_xpath)).Click();
                Driver.Instance.Value.FindElement(By.Id(PageElements.ROIRequesterPortal.verrsionovrride_id)).Clear();
                Driver.Instance.Value.FindElement(By.Id(PageElements.ROIRequesterPortal.verrsionovrride_id)).SendKeys(version);
                Driver.Instance.Value.FindElement(By.Id(PageElements.ROIRequesterPortal.versavebtn_id)).Click();


                MenuSelector.SelectRoiAdmin("Facilities", "Facility List"); ROIAdminFacalitiesListPage.gotoROITestFacility();
                Driver.Wait(TimeSpan.FromSeconds(10));
                Driver.Instance.Value.FindElement(By.Id(PageElements.LogNewRequestPage.ratesfacilogoutbtn_id)).Click();
                Driver.Wait(TimeSpan.FromSeconds(5));

                //   LogNewRequestPage.facillogoutbutton();
                //facility drill down
                //logout of facility
                Driver.logger.Info("Version: " + version);
            }

            private RateValues SetRate(string reqId, string rate)
            {
                var values = new RateValues();

                ROIAdminFacalitiesListPage.roilookupidadmin(reqId);
                // Driver.logger.Info("Testing Rate for Request ID: " + reqId);
                RequestStatus.applyRate();


                string data1 = Driver.Instance.Value.FindElement(By.Id("mrocontent_lblPatient")).Text;
                Driver.logger.Info("Patient name: " + data1);

                Driver.Instance.Value.FindElement(By.Id(PageElements.ROIAdminList.adminchange_id)).Click();
                Driver.Wait(TimeSpan.FromSeconds(5));
                Driver.Instance.Value.FindElement(By.Id(PageElements.ROIAdminList.admincharr_id)).Click();
                Driver.Wait(TimeSpan.FromSeconds(5));
                Driver.Instance.Value.FindElement(By.LinkText(rate)).Click();

                string ratetext = Driver.Instance.Value.FindElement(By.Id("mrocontent_lblAppliedRate")).Text;
                Driver.logger.Pass(ratetext);
                Driver.Instance.Value.FindElement(By.Id(PageElements.ROIAdminList.roiadminapplyrate_id)).Click();
                Driver.Wait(TimeSpan.FromSeconds(5));
                string data = Driver.Instance.Value.FindElement(By.Id("mrocontent_tdRateDescription")).Text;
                Driver.logger.Info("(Rate Calculation String) " + data);
                // WebElementHelper.ScrollIntoView("mrocontent_cmdUpdatePages");
                // WebElementHelper.ScrollIntoView1();

                //RemoteWebDriver dr = Driver.Instance.Value;
                IJavaScriptExecutor js = (IJavaScriptExecutor)Driver.Instance.Value;
                js.ExecuteScript("window.scrollBy(0,130)");
                //Driver.takeScreenShot();
                Driver.Wait(TimeSpan.FromSeconds(3));



                Driver.logger.Info("URL: " + Driver.Instance.Value.Url);
                Driver.takeScreenShot();
                js.ExecuteScript("window.scrollBy(0,-130)");
                //data collectio
                values.StroredPaper = Driver.Instance.Value.FindElement(By.Id("mrocontent_txtPages")).GetAttribute("value");
                values.TotalCharges = Driver.Instance.Value.FindElement(By.Id("mrocontent_txtBalanceDue")).GetAttribute("value");

                return values;
            }

            //public static void ScrollIntoView1(string locatorKey)
            //{
            //    IWebElement element = Driver.Instance.Value.FindElement(By.Id(locatorKey));
            //    RemoteWebDriver dr = Driver.Instance.Value;
            //    IJavaScriptExecutor js = (IJavaScriptExecutor)dr;
            //    js.ExecuteScript("window.scrollBy(0,750);");
            //    Driver.takeScreenShot();
            //}

        }
    }

    public class RateValues
    {
        public string StroredPaper { get; set; }
        public string TotalCharges { get; set; }
    }
}
