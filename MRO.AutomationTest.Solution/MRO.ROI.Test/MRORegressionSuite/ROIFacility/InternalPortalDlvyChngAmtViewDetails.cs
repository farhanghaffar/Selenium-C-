using AventStack.ExtentReports;
using AventStack.ExtentReports.MarkupUtils;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MRO.ROI.Automation.Common.Navigation;
using MRO.ROI.Automation.Pages;
using MRO.ROI.Automation.Pages.Common;
using MRO.ROI.Automation.Pages.ROIFacility;
using MRO.ROI.Automation.Selenium;
using MRO.ROI.Test.Utilities;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using System;

namespace MRO.ROI.Test.MRORegressionSuite.ROIFacility
{
    [TestClass]
    public class InternalPortalDlvyChngAmtViewDetails : ROITestBase
    {
        public InternalPortalDlvyChngAmtViewDetails() : base((ROITestArea.ROIAdmin))
        {
        }
        [TestMethod]
        [TestCategory(ROITestCategory.Regression)]
        //Converted Manual to Automated "Test Case 744" ROI Facility-->Internal Portal Delivery Credit Card Payment through  "Change Amount/View Details"
        public void Reg_744_InternalPortalDelivery_ChangeAmountViewDetails()
        {
            try
            {
                Driver.logger = Driver.extent.CreateTest("Change Amount/View Details PayBy Credit Card Test");
                Driver.logger.Log(Status.Info, "Converted Manual T/C 744 Internal Portal Delivery Credit Card Payment through Change Amount / View Details to automated.");
                MenuSelector.SelectRoiAdmin("Facilities", "Facility List");
                ROIAdminFacalitiesListPage.gotoROITestFacility();
                MenuSelector.Select("ROI Requests", "Log a New Request");
                bool tab = LogNewRequestPage.ClickInternalPortalTab();
                Assert.IsTrue(tab, "Failed to click on Internal Portal delivery tab");
                Driver.logger.Pass("Successfully clicked on Internal Portal tab");
                LogNewRequestPage.CreateNewInternalPortalRequest();  //tou
                Assert.IsTrue(LogNewRequestPage.NewRequestCreated, "Failed to create new InternalPortal request");
                Driver.logger.Pass("Successfully created a new Internal Portal request");
                string requestID = ROIAdminFacalitiesListPage.getRequestid();
                LogNewRequestPage.GoToRequestStatusPage();
                Assert.IsTrue(FacilityRequestStatusPage.IsAtRequestStatusPage, "Failed to navigate to facility request status page.");
                Driver.logger.Pass("Successfully navigated to facility request status page");
                LogNewRequestPage.PatientNameValidation();
                Driver.Wait(TimeSpan.FromSeconds(5));
                LogNewRequestPage.facillogoutbutton();
                ROIAdminFacalitiesListPage.roilookupidadmin(requestID);
                Driver.Wait(TimeSpan.FromSeconds(5));
                RemoteWebDriver dr = Driver.Instance.Value;
                IJavaScriptExecutor js = (IJavaScriptExecutor)Driver.Instance.Value;
                js.ExecuteScript("window.scrollBy(0,1030)");
                Driver.Wait(TimeSpan.FromSeconds(3));
                Driver.takeScreenShot();
                js.ExecuteScript("window.scrollBy(0,-1030)");
                Driver.Instance.Value.FindElement(By.Id("mrocontent_lnkFacility")).Click();
                Driver.Wait(TimeSpan.FromSeconds(5));
                Driver.Instance.Value.FindElement(By.Id("mrocontent_lnkUpdatePrepayment")).Click();
                Driver.Instance.Value.FindElement(By.Id("mrocontent_rbCreditCard")).Click();
                Driver.Wait(TimeSpan.FromSeconds(2));
                Driver.Instance.Value.FindElement(By.Id("mrocontent_custCCPayment_txtBxPayment")).SendKeys("10.00");
                Driver.Wait(TimeSpan.FromSeconds(2));
                Driver.Instance.Value.FindElement(By.Id("mrocontent_custCCPayment_btnEnterCCInfo")).Click();
                Driver.Wait(TimeSpan.FromSeconds(2));
                Driver.Instance.Value.SwitchTo().Frame("RadWindow1");
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
                Driver.Instance.Value.FindElement(By.Id("cardPay")).Click();
                Driver.Wait(TimeSpan.FromSeconds(2));
                Driver.Instance.Value.SwitchTo().DefaultContent();
                string ApprovalCode = Driver.Instance.Value.FindElement(By.XPath("//*[@id=\"mrocontent_custCCPayment_spanRef\"]")).Text;
                Driver.logger.Pass("Payment successfully submitted and Approval Code: " + ApprovalCode);
                Driver.Wait(TimeSpan.FromSeconds(2));
                Driver.takeScreenShot();
                Driver.Wait(TimeSpan.FromSeconds(2));
                LogNewRequestPage.facillogoutbutton();
                ROIAdminFacalitiesListPage.roilookupidadmin(requestID);
                js.ExecuteScript("window.scrollBy(0,1030)");
                Driver.Wait(TimeSpan.FromSeconds(3));
                Driver.takeScreenShot();
                js.ExecuteScript("window.scrollBy(0,-1030)");
                RequestStatus.roiadmlogout();
                Assert.IsTrue(LoginPage.IsAtLoginPage, "Failed to log out successfully.");
            }
            catch (Exception ex)
            {
                Driver.logger.Log(Status.Fail, "Test failed with exception"); //Logging failed
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



