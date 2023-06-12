using AventStack.ExtentReports;
using AventStack.ExtentReports.MarkupUtils;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MRO.ROI.Automation.Pages;
using MRO.ROI.Automation.Selenium;
using MRO.ROI.Test.Utilities;
using OpenQA.Selenium;
using System;

namespace MRO.ROI.Test
{
    class MRO_ROI_Rate_Test3

    {
        [TestClass]
        public class RoiAdminTest : ROITestBase
        {
            public RoiAdminTest() : base(ROITestArea.ROIAdmin)
            {

            }
            [TestMethod]
            //[TestCategory(ROITestCategory.Regression)]
            public void MRO_Rate_PhyDlvy_Test3()
            {
                try
                {
                    Driver.logger = Driver.extent.CreateTest("Physical_Delivery_RequestID_23625037");
                    Driver.logger.Info("Login Complete");
                    ROIAdminFacalitiesListPage.roilookupidadmin("23625037");
                    Driver.logger.Info("Testing Rate for Request ID: 23625037");
                    RequestStatus.applyRate();
                    string data1 = Driver.Instance.Value.FindElement(By.Id("mrocontent_lblPatient")).Text;
                    Driver.logger.Info("Patient name: " + data1);
                    Driver.Instance.Value.FindElement(By.Id(PageElements.ROIAdminList.adminchange_id)).Click();
                    Driver.Instance.Value.FindElement(By.Id(PageElements.ROIAdminList.admincharr_id)).Click();
                    Driver.Wait(TimeSpan.FromSeconds(5));
                    Driver.Instance.Value.FindElement(By.LinkText("Rate1-3")).Click();
                    string ratetext = Driver.Instance.Value.FindElement(By.Id("mrocontent_lblAppliedRate")).Text;
                    Driver.logger.Pass(ratetext);
                    Driver.Instance.Value.FindElement(By.Id(PageElements.ROIAdminList.roiadminapplyrate_id)).Click();
                    Driver.Wait(TimeSpan.FromSeconds(5));
                    string data = Driver.Instance.Value.FindElement(By.Id("mrocontent_tdRateDescription")).Text;
                    Driver.logger.Info("(Rate Calculation String) " + data);
                    Driver.takeScreenShot();

                    Driver.Instance.Value.FindElement(By.Id(PageElements.ROIAdminList.adminchange_id)).Click();
                    Driver.Wait(TimeSpan.FromSeconds(5));
                    Driver.Instance.Value.FindElement(By.LinkText("Rate1-4")).Click();
                    string ratetext1 = Driver.Instance.Value.FindElement(By.Id("mrocontent_lblAppliedRate")).Text;
                    Driver.logger.Pass(ratetext1);
                    Driver.Instance.Value.FindElement(By.Id(PageElements.ROIAdminList.roiadminapplyrate_id)).Click();
                    Driver.Wait(TimeSpan.FromSeconds(5));
                    string data2 = Driver.Instance.Value.FindElement(By.Id("mrocontent_tdRateDescription")).Text;
                    Driver.logger.Info("(Rate Calculation String) " + data2);
                    Driver.takeScreenShot();

                    Driver.Instance.Value.FindElement(By.Id(PageElements.ROIAdminList.adminchange_id)).Click();
                    Driver.Wait(TimeSpan.FromSeconds(5));
                    Driver.Instance.Value.FindElement(By.LinkText("Rate1-5")).Click();
                    string ratetext2 = Driver.Instance.Value.FindElement(By.Id("mrocontent_lblAppliedRate")).Text;
                    Driver.logger.Pass(ratetext2);
                    Driver.Instance.Value.FindElement(By.Id(PageElements.ROIAdminList.roiadminapplyrate_id)).Click();
                    Driver.Wait(TimeSpan.FromSeconds(5));
                    string data3 = Driver.Instance.Value.FindElement(By.Id("mrocontent_tdRateDescription")).Text;
                    Driver.logger.Info("(Rate Calculation String) " + data3);
                    Driver.takeScreenShot();


                    Driver.Instance.Value.FindElement(By.Id(PageElements.ROIAdminList.adminchange_id)).Click();
                    Driver.Wait(TimeSpan.FromSeconds(5));
                    Driver.Instance.Value.FindElement(By.LinkText("Rate2-3")).Click();
                    string ratetext3 = Driver.Instance.Value.FindElement(By.Id("mrocontent_lblAppliedRate")).Text;
                    Driver.logger.Pass(ratetext3);
                    Driver.Instance.Value.FindElement(By.Id(PageElements.ROIAdminList.roiadminapplyrate_id)).Click();
                    Driver.Wait(TimeSpan.FromSeconds(5));
                    string data4 = Driver.Instance.Value.FindElement(By.Id("mrocontent_tdRateDescription")).Text;
                    Driver.logger.Info("(Rate Calculation String) " + data4);
                    Driver.takeScreenShot();

                    Driver.Instance.Value.FindElement(By.Id(PageElements.ROIAdminList.adminchange_id)).Click();
                    Driver.Wait(TimeSpan.FromSeconds(5));
                    Driver.Instance.Value.FindElement(By.LinkText("Rate2-6")).Click();
                    string ratetext4 = Driver.Instance.Value.FindElement(By.Id("mrocontent_lblAppliedRate")).Text;
                    Driver.logger.Pass(ratetext4);
                    Driver.Instance.Value.FindElement(By.Id(PageElements.ROIAdminList.roiadminapplyrate_id)).Click();
                    Driver.Wait(TimeSpan.FromSeconds(5));
                    string data5 = Driver.Instance.Value.FindElement(By.Id("mrocontent_tdRateDescription")).Text;
                    Driver.logger.Info("(Rate Calculation String) " + data5);
                    Driver.takeScreenShot();

                    Driver.Instance.Value.FindElement(By.Id(PageElements.ROIAdminList.adminchange_id)).Click();
                    Driver.Wait(TimeSpan.FromSeconds(5));
                    Driver.Instance.Value.FindElement(By.LinkText("Rate2-7")).Click();
                    string ratetext5 = Driver.Instance.Value.FindElement(By.Id("mrocontent_lblAppliedRate")).Text;
                    Driver.logger.Pass(ratetext5);
                    Driver.Instance.Value.FindElement(By.Id(PageElements.ROIAdminList.roiadminapplyrate_id)).Click();
                    Driver.Wait(TimeSpan.FromSeconds(5));
                    string data6 = Driver.Instance.Value.FindElement(By.Id("mrocontent_tdRateDescription")).Text;
                    Driver.logger.Info("(Rate Calculation String) " + data6);
                    Driver.takeScreenShot();
                }
                catch (Exception ex)
                {
                    Driver.takeScreenShot();
                    Driver.logger.Log(Status.Fail, "Test failed with exception"); //Logging fail
                    Driver.logger.Log(Status.Error, MarkupHelper.CreateTable(
                        new string[,]
                        {
                        {"Exception", ex.Message },
                        {"StackTrace", ex.StackTrace }
                        })); //Logging Error in a tabular format
                    Assert.Fail(ex.Message);
                }

            }
        }
    }
}
