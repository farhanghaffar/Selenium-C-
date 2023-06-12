using AventStack.ExtentReports;
using AventStack.ExtentReports.MarkupUtils;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MRO.ROI.Automation.Common.Navigation;
using MRO.ROI.Automation.Pages;
using MRO.ROI.Automation.Pages.ROIFacility;
using MRO.ROI.Automation.Selenium;
using MRO.ROI.Test.Utilities;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using System;

namespace MRO.ROI.Test.MRORegressionSuite.ROIFacility
{
    [TestClass]


    public class MRODeliveryChngAmtViewDetails : ROITestBase
    {
        public MRODeliveryChngAmtViewDetails() : base((ROITestArea.ROIAdmin))
        {

        }

        [TestMethod]
        [TestCategory(ROITestCategory.Regression)]
        public void Reg_743_MRODelivery_ChangeAmountViewDetails()
        {
            try
            {
                Driver.logger = Driver.extent.CreateTest("Change Amount/View Details PayBy Credit Card Test");
                Driver.logger.Log(Status.Info, "Converted Manual T/C ROI Facility-->MRO Delivery CC Payment through Change Amount / View Details to automated.");
                MenuSelector.SelectRoiAdmin("Facilities", "Facility List");
                ROIAdminFacalitiesListPage.gotoROITestFacility();
                MenuSelector.Select("ROI Requests", "Log a New Request");
                //LogNewRequestPage.GoToLogNewRequestPage();
                //Assert.IsTrue(LogNewRequestPage.IsAtLogNewRequestPage, "Failed to navigate to Log New Request page.");

                //Driver.logger = Driver.extent.CreateTest(" Change Amount/View Details PayBy Credit Card Test");
                //LogNewRequestPage.GoToLogNewRequestPage();
                //Assert.IsTrue(LogNewRequestPage.IsAtLogNewRequestPage, "Failed to navigate to Log New Request page.");
                //Driver.logger.Info("Sucessfully Clicked On Log a New Request.");
                bool tab = LogNewRequestPage.ClickMRODeliveryTab();
                Driver.logger.Pass("Successfully clicked on On-Site Delivery tab");
                Assert.IsTrue(tab, "Failed to click on MRO delivery tab");
                //   Assert.IsTrue(mroDelTab, "Failed to click on MRO delivery tab");
                LogNewRequestPage.CreateNewMRODeliveryRequest();
                Assert.IsTrue(LogNewRequestPage.NewRequestCreated, "Failed to create new MRO delivery request");
                string requestID = ROIAdminFacalitiesListPage.getRequestid();
                LogNewRequestPage.GoToRequestStatusPage();
                Assert.IsTrue(FacilityRequestStatusPage.IsAtRequestStatusPage, "Failed to navigate to facility request status page.");
                FacilityRequestStatusPage.ScanPatientPages(LogNewRequestPage.PatientFirstName, LogNewRequestPage.PatientLastName);
                FacilityRequestStatusPage.ReleaseRequest();
                LogNewRequestPage.facillogoutbutton();
                ROIAdminFacalitiesListPage.roilookupidadmin(requestID);
                RequestStatus.roiAssignReq();
                RequestStatus.roiAdminSearch();
                RequestStatus.roiSaveDonebtn();
                Driver.Wait(TimeSpan.FromSeconds(5));
                // RequestStatus.roiadmlogout();
                Driver.Instance.Value.FindElement(By.Id("mrocontent_lnkFacility")).Click();

                Driver.Instance.Value.FindElement(By.Id("mrocontent_lnkUpdatePrepayment")).Click();

                //RequestStatus.roiCreateInvoice();
                //Driver.Wait(TimeSpan.FromSeconds(2));
                Driver.Instance.Value.FindElement(By.Id("mrocontent_rbCreditCard")).Click();
                Driver.Wait(TimeSpan.FromSeconds(2));
                Driver.Instance.Value.FindElement(By.Id("mrocontent_custCCPayment_txtBxPayment")).SendKeys("10.00");
                Driver.Wait(TimeSpan.FromSeconds(2));
                Driver.Instance.Value.FindElement(By.Id("mrocontent_custCCPayment_btnEnterCCInfo")).Click();
                Driver.Wait(TimeSpan.FromSeconds(2));
                Driver.Instance.Value.SwitchTo().Frame("RadWindow1");
                Driver.Wait(TimeSpan.FromSeconds(5));
                Driver.logger.Log(Status.Info, "Entering Credit Card Information:");
                Driver.Instance.Value.FindElement(By.Id("CreditCard_name")).SendKeys("Test Card");
                Driver.Wait(TimeSpan.FromSeconds(1));
                Driver.Instance.Value.FindElement(By.Id("CreditCard_expDate")).SendKeys("04/22");
                Driver.Wait(TimeSpan.FromSeconds(2));
                Driver.Instance.Value.FindElement(By.Id("CreditCard_cardNumber")).Click();
                Driver.Instance.Value.FindElement(By.Id("CreditCard_cardNumber")).SendKeys("4111-1111-1111-1111");
                Driver.Wait(TimeSpan.FromSeconds(2));
                Driver.Instance.Value.FindElement(By.Id("CreditCard_cvc")).SendKeys("123");
                Driver.Wait(TimeSpan.FromSeconds(1));
                Driver.Instance.Value.FindElement(By.Id("billingaddress")).SendKeys("123 Test St");
                Driver.Wait(TimeSpan.FromSeconds(1));
                Driver.Instance.Value.FindElement(By.Id("billingcity")).SendKeys("SomeWhere");
                Driver.Wait(TimeSpan.FromSeconds(1));
                Driver.Instance.Value.FindElement(By.Id("billingstate")).SendKeys("CA");
                Driver.Wait(TimeSpan.FromSeconds(1));
                Driver.Instance.Value.FindElement(By.Id("billingzipcode")).SendKeys("90001");
                Driver.Wait(TimeSpan.FromSeconds(2));
                // Driver.takeScreenShot();
                Driver.Wait(TimeSpan.FromSeconds(2));
                Driver.Instance.Value.FindElement(By.Id("cardPay")).Click();
                Driver.Wait(TimeSpan.FromSeconds(2));
                Driver.Instance.Value.SwitchTo().DefaultContent();
                string ApprovalCode = Driver.Instance.Value.FindElement(By.XPath("//*[@id=\"mrocontent_custCCPayment_spanRef\"]")).Text;
                Driver.logger.Pass("Payment successfully submitted and Approval Code: " + ApprovalCode);
                Driver.Wait(TimeSpan.FromSeconds(2));
                Driver.takeScreenShot();
                Driver.Wait(TimeSpan.FromSeconds(2));
                LogNewRequestPage.facillogoutbutton();
                //   ROIAdminFacalitiesListPage.roilookupidadmin(requestID);
                Driver.Wait(TimeSpan.FromSeconds(3));
                RemoteWebDriver dr = Driver.Instance.Value;
                IJavaScriptExecutor js = (IJavaScriptExecutor)Driver.Instance.Value;
                js.ExecuteScript("window.scrollBy(0,1030)");
                //Driver.takeScreenShot();
                Driver.Wait(TimeSpan.FromSeconds(3));
                Driver.takeScreenShot();
                js.ExecuteScript("window.scrollBy(0,-1030)");
                RequestStatus.roiadmlogout();
            }
            catch (Exception ex)
            {
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
