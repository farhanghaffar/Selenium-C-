using AventStack.ExtentReports;
using AventStack.ExtentReports.MarkupUtils;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MRO.ROI.Automation.Common.Navigation;
using MRO.ROI.Automation.Pages;
using MRO.ROI.Automation.Pages.ROIFacility;
using MRO.ROI.Automation.Selenium;
using MRO.ROI.Test.Utilities;
using OpenQA.Selenium;
using System;

namespace MRO.ROI.Test.SmokeTests.ROIAdmin
{
    [TestClass]
    public class RoiAdminTestFar : ROITestBase
    {
        public RoiAdminTestFar() : base(ROITestArea.ROIAdmin)
        {

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


            MenuSelector.SelectRoiAdmin("Facilities", "Facility List");
            ROIAdminFacalitiesListPage.gotoROITestFacility();
            Driver.Wait(TimeSpan.FromSeconds(10));

            //   LogNewRequestPage.facillogoutbutton();
            //facility drill down
            //logout of facility
            Driver.logger.Info("Version: " + version);
        }
        //[DataRow("ROI3-1-1-974")]
        //[DataRow("ROI3-Dev")]
        //[TestMethod]
        //[TestCategory(ROITestCategory.Regression)]
        //public void Reg_Far_WithRatesMroCreateNew_Request(string loginOverride)
        //{
        //    try
        //    {
        //        Driver.logger = Driver.extent.CreateTest("FAR Rate Test");
        //        Driver.logger.Info("FAR Teting on this URL: " + Driver.Instance.Value.Url);

        //        LoginOverride(loginOverride);

        //        MenuSelector.Select("ROI Requests", "Log a New Request");
        //        //   LogNewRequestPage.GoToLogNewRequestPage();
        //        //   Assert.IsTrue(LogNewRequestPage.IsAtLogNewRequestPage, "Failed to navigate to Log New Request page.");
        //        bool tab = LogNewRequestPage.ClickMRODeliveryTab();
        //        Assert.IsTrue(tab, "Failed to click on MRO delivery tab");
        //        //   Assert.IsTrue(mroDelTab, "Failed to click on MRO delivery tab");
        //        LogNewRequestPage.CreateNewMRODeliveryRequest();
        //        Assert.IsTrue(LogNewRequestPage.NewRequestCreated, "Failed to create new MRO delivery request");
        //        string requestID = ROIAdminFacalitiesListPage.getRequestid();

        //        LogNewRequestPage.GoToRequestStatusPage();
        //        Assert.IsTrue(FacilityRequestStatusPage.IsAtRequestStatusPage, "Failed to navigate to facility request status page.");

        //        FacilityRequestStatusPage.ScanPatientPages(LogNewRequestPage.PatientFirstName, LogNewRequestPage.PatientLastName);
        //        FacilityRequestStatusPage.ReleaseRequest();
        //        LogNewRequestPage.facillogoutbutton();
        //        ROIAdminFacalitiesListPage.roilookupidadmin(requestID);
        //        RequestStatus.roiAssignReq();
        //        RequestStatus.roiAdminSearch();
        //        RequestStatus.roiSaveDonebtn();
        //        //     RequestStatus.roiAdminapplyrate();
        //        RequestStatus.qcStatus();
        //        RequestStatus.applyRate();

        //        string data1 = Driver.Instance.Value.FindElement(By.Id("mrocontent_lblPatient")).Text;
        //        Driver.logger.Info("Patient name: " + data1);

        //        Driver.Instance.Value.FindElement(By.Id(PageElements.ROIAdminList.adminchange_id)).Click();
        //        Driver.Wait(TimeSpan.FromSeconds(5));
        //        Driver.Instance.Value.FindElement(By.Id(PageElements.ROIAdminList.admincharr_id)).Click();
        //        Driver.Wait(TimeSpan.FromSeconds(5));
        //        // Driver.Instance.Value.FindElement(By.LinkText(rate)).Click();
        //        Driver.Instance.Value.FindElement(By.LinkText("Rate1-4")).Click();

        //        //string ratetext = Driver.Instance.Value.FindElement(By.Id("mrocontent_lblAppliedRate")).Text;
        //        //Driver.logger.Pass(ratetext);
        //        Driver.Instance.Value.FindElement(By.Id(PageElements.ROIAdminList.roiadminapplyrate_id)).Click();
        //        Driver.Wait(TimeSpan.FromSeconds(5));
        //        Driver.Instance.Value.FindElement(By.Id("mrocontent_cmdDone")).Click();
        //        Driver.Wait(TimeSpan.FromSeconds(5));
        //        Driver.Instance.Value.FindElement(By.Id("mrocontent_cmdLedgerDetail")).Click();


        //        Driver.Click("//*[@id='mrocontent_cmdPayments']");

        //        //Driver.Wait(TimeSpan.FromSeconds(5));
        //        Driver.Type("//*[@id='mrocontent_txtAmount']", "10.00");
        //        Driver.Type("//*[@id='mrocontent_txtDate']", ".");

        //        Driver.Click("//*[@id='mrocontent_cmdAdd']");
        //        Driver.Click("//*[@id='mrocontent_cmdRequest']");

        //        string payMRO = Driver.findElement("//*[@id='mrocontent_tdPaymentRcvd']").Text;
        //        Driver.Click("//*[@id='mrocontent_cmdLedgerDetail']");
        //        Console.WriteLine("Testing $10.00 payment" + payMRO);

        //        //ledger 2
        //        string payMROLedge = Driver.findElement("//*[@id='mrocontent_dgReport']/tbody/tr[3]/td[8]").Text;


        //        Driver.Click("//*[@id='mrocontent_dgDetail']/tbody/tr[2]/td[1]/a");


        //        string RetrievalFee = Driver.findElement("//*[@id='mrocontent_dgLineItems']/tbody/tr[2]/td[5]").Text;

        //        Console.WriteLine(RetrievalFee);
        //        string PageFee = Driver.findElement("//*[@id='mrocontent_dgLineItems']/tbody/tr[3]/td[5]").Text;


        //        Driver.Click("//*[@id='mrocontent_cmRequest']");
        //        string RetrievalFeeReqPage = Driver.findElement("//*[@id='mrocontent_tdRetrievalFee']").Text;
        //        string PageFeeReqPage = Driver.findElement("//*[@id='mrocontent_tdPageFee1']").Text;


        //        Console.WriteLine(RetrievalFee + "" + PageFee + "" + RetrievalFeeReqPage + "" + PageFeeReqPage);

        //        if (RetrievalFee.Equals("-" + RetrievalFeeReqPage))
        //        {
        //            Driver.logger.Pass("RetrievalFee is same: " + RetrievalFee);
        //        }
        //        else
        //        {
        //            Driver.logger.Fail("RetrievalFee is not same");
        //        }

        //        if (PageFee.Equals("-" + PageFeeReqPage))
        //        {
        //            Driver.logger.Pass("PageFee is same: " + PageFee);
        //        }
        //        else
        //        {
        //            Driver.logger.Fail("PageFee is not same");
        //        }

        //        if (payMRO.Equals(payMROLedge))
        //        {
        //            Driver.logger.Pass("Pay MRO is same: " + payMRO);
        //        }
        //        else
        //        {
        //            Driver.logger.Fail("Pay MRO is not same");
        //        }

        //        Driver.Click("//*[@id='mrocontent_cmdLedgerDetail']");
        //        Driver.Wait(TimeSpan.FromSeconds(2));
        //        Driver.Click("//*[@id='mrocontent_cmdPayments']");
        //        Driver.Wait(TimeSpan.FromSeconds(5));
        //        Driver.Type("//*[@id='mrocontent_txtAmount']", "100.00");
        //        Driver.Type("//*[@id='mrocontent_txtDate']", ".");

        //        Driver.Click("//*[@id='mrocontent_cmdAdd']");
        //        Driver.Click("//*[@id='mrocontent_cmdRequest']");

        //        //Ledger 1
        //        string payMRO2 = Driver.findElement("//*[@id='mrocontent_tdPaymentRcvd']").Text;
        //        Driver.logger.Info("Total Payment to MRO: " + payMRO2);
        //        //Adjusted balance
        //        string L1AdjustedBal = Driver.findElement("//*[@id='mrocontent_tdAdjustedBalance']").Text;
        //        Driver.logger.Info("Adjusted Balance: " + L1AdjustedBal);
        //        //23627099
        //        //string data = Driver.Instance.Value.FindElement(By.Id("mrocontent_tdRateDescription")).Text;
        //        //Driver.logger.Info("(Rate Calculation String) " + data);
        //        // WebElementHelper.ScrollIntoView("mrocontent_cmdUpdatePages");
        //        // WebElementHelper.ScrollIntoView1();

        //        RemoteWebDriver dr = Driver.Instance.Value;
        //        IJavaScriptExecutor js = (IJavaScriptExecutor)Driver.Instance.Value;
        //        js.ExecuteScript("window.scrollBy(0,950)");
        //        //Driver.takeScreenShot();
        //        Driver.Wait(TimeSpan.FromSeconds(3));

        //        Driver.logger.Info("Far Testing Completed at this URL: " + Driver.Instance.Value.Url);
        //        Driver.takeScreenShot();
        //        js.ExecuteScript("window.scrollBy(0,-950)");
        //        //data collectio
        //        //values.StroredPaper = Driver.Instance.Value.FindElement(By.Id("mrocontent_txtPages")).GetAttribute("value");
        //        //values.TotalCharges = Driver.Instance.Value.FindElement(By.Id("mrocontent_txtBalanceDue")).GetAttribute("value");


        //        //  RequestStatus.roiadmlogout();
        //    }
        //    catch (Exception ex)
        //    {
        //        Driver.logger.Log(Status.Fail, "Test failed with exception"); //Logging fail
        //        Driver.logger.Log(Status.Error, MarkupHelper.CreateTable(
        //            new string[,]
        //            {
        //                {"Exception", ex.Message },
        //                {"StackTrace", ex.StackTrace }
        //            })); //Logging Error in a tabular format
        //        Assert.Fail(ex.Message);
        //    }
        //}
    }
}
