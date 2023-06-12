using AventStack.ExtentReports;
using AventStack.ExtentReports.MarkupUtils;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MRO.ROI.Automation.Pages.Common;
using MRO.ROI.Automation.Pages.ROIFacility;
using MRO.ROI.Automation.Selenium;
using MRO.ROI.Test.Utilities;
using OpenQA.Selenium;
using System;

namespace MRO.ROI.Test.MRORegressionSuite.ROIFacility
{
    [TestClass]

    // Converted manual test case 741 ROI Facility-->Onsite Delivery Credit Card Payment
    public class OnSiteDeliveryPayByCCTest : ROITestBase
    {
        public OnSiteDeliveryPayByCCTest() : base(ROITestArea.ROIFacility)
        {

        }

        [TestMethod]
        [TestCategory(ROITestCategory.Regression)]
        public void Reg_741_OnsiteDelivery_Credit_Card_Payment()
        {
            try
            {
                Driver.logger = Driver.extent.CreateTest(" Reg_741 OnsiteDelivery_Credit_Card_Payment Test");
                LogNewRequestPage.GoToLogNewRequestPage();
                Assert.IsTrue(LogNewRequestPage.IsAtLogNewRequestPage, "Failed to navigate to Log New Request page.");
                Driver.logger.Info("Sucessfully Clicked On Log a New Request.");

                bool tab = LogNewRequestPage.ClickOnSiteDeliveryTab();
                Assert.IsTrue(tab, "Failed to click on On-Site Delivery tab");
                Driver.logger.Pass("Successfully clicked on On-Site Delivery tab");

                LogNewRequestPage.CreateNewOnsiteDeliveryRequest();
                Assert.IsTrue(LogNewRequestPage.NewRequestCreated, "Failed to create new On-Site Delivery");
                Driver.logger.Pass("Successfully created a On-Site Delivery request");
                LogNewRequestPage.GoToRequestStatusPage();
                Driver.logger.Log(Status.Info, "Fill New MRO Delivery Request.");
                Assert.IsTrue(FacilityRequestStatusPage.IsAtRequestStatusPage, "Failed to navigate to facility request status page.");
                FacilityRequestStatusPage.ScanPatientPages(LogNewRequestPage.PatientFirstName, LogNewRequestPage.PatientLastName);
                //   LogNewRequestPage.PatientNameValidation();
                Driver.logger.Pass("Successfully navigated to facility request status page");
                // Driver.logger.Log(Status.Info, "Fill New MRO Delivery Request.");
                Assert.IsTrue(FacilityRequestStatusPage.IsAtRequestStatusPage, "Failed to navigate to facility request status page.");
                //   FacilityRequestStatusPage.ScanPatientPages();
                Driver.Wait(TimeSpan.FromSeconds(5));
                FacilityRequestStatusPage.DeliverMedicalRecordOnsite1();
                Driver.Wait(TimeSpan.FromSeconds(2));
                //Driver.Instance.Value.FindElement(By.Id("mrocontent_cmdPayCreditCard")).Click();
                //Driver.Wait(TimeSpan.FromSeconds(2));
                //Driver.Instance.Value.FindElement(By.Id("mrocontent_cmdCheckOut")).Click();
                //Driver.Wait(TimeSpan.FromSeconds(5));
                //Driver.Instance.Value.FindElement(By.Id("mrocontent_btnTCPay")).Click();
                Driver.Wait(TimeSpan.FromSeconds(2));
                Driver.Instance.Value.SwitchTo().Frame("RadWindow1");
                Driver.Wait(TimeSpan.FromSeconds(5));
                //Driver.Wait(TimeSpan.FromSeconds(5));
                Driver.logger.Log(Status.Info, "Entering Credit Card Information:");
                Driver.Instance.Value.FindElement(By.Id("CreditCard_name")).SendKeys("Test Card");
                Driver.Wait(TimeSpan.FromSeconds(1));
                Driver.Instance.Value.FindElement(By.Id("CreditCard_expDate")).SendKeys("04/22");
                Driver.Wait(TimeSpan.FromSeconds(5));
                Driver.Instance.Value.FindElement(By.Id("CreditCard_cardNumber")).Click();
                Driver.Instance.Value.FindElement(By.Id("CreditCard_cardNumber")).SendKeys("4111-1111-1111-1111");
                Driver.Wait(TimeSpan.FromSeconds(10));
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
                Driver.Wait(TimeSpan.FromSeconds(5));
                Driver.Instance.Value.FindElement(By.Id("cardPay")).Click();
                Driver.Wait(TimeSpan.FromSeconds(5));
                Driver.Instance.Value.SwitchTo().DefaultContent();
                //mrocontent_custCCPayment_spanRef
                //   mrocontent_spanRef mrocontent_custCCPayment_spanRef
                // string ApprovalCode = Driver.Instance.Value.FindElement(By.Id("mrocontent_spanRef")).Text;
                //*[@id="mrocontent_custCCPayment_spanRef"]

                //  Driver.Instance.Value.FindElement(By.XPath(PageElements.ROIRequesterPortal.extportalrequestid_xpath)).Text;

                string ApprovalCode = Driver.Instance.Value.FindElement(By.XPath("//*[@id=\"mrocontent_custCCPayment_spanRef\"]")).Text;
                Driver.logger.Pass("Payment successfully submitted and Approval Code: " + ApprovalCode);
                Driver.Wait(TimeSpan.FromSeconds(5));
                Driver.takeScreenShot();
                Driver.Wait(TimeSpan.FromSeconds(5));
                //LogNewRequestPage.facillogoutbutton();

                //Driver.Wait(TimeSpan.FromSeconds(2));
                //WebElementHelper.Click_Enter();
                Assert.IsTrue(FacilityRequestStatusPage.IsDocumentsDelivered, "Failed to deliver documents");
                LoginPage.LogOut();
                Assert.IsTrue(LoginPage.IsAtLoginPage, "Failed to log out successfully.");
                Driver.logger.Pass("Successfully logged out");
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