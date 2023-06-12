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
    public class ROI_Admin_PayByCC : ROITestBase
    {
        public ROI_Admin_PayByCC() : base(ROITestArea.ROIAdmin)
        {

        }
        //TestCase_740
        [TestMethod]
        [TestCategory(ROITestCategory.Regression)]
        // Converted manual test case 740-ROI-Admin-->Request Status-->Pay By to automated.
        public void Reg_740_Create_Admin_PayByCC()
        {
            try
            {
                Driver.logger = Driver.extent.CreateTest("Create Admin PayBy Credit Card Test");
                Driver.logger.Log(Status.Info, "Converted Manual T/C 740-ROI-Admin-->Request Status-->Pay By to automated.");
                MenuSelector.SelectRoiAdmin("Facilities", "Facility List");
                ROIAdminFacalitiesListPage.gotoROITestFacility();
                MenuSelector.Select("ROI Requests", "Log a New Request");
                //   LogNewRequestPage.GoToLogNewRequestPage();
                //   Assert.IsTrue(LogNewRequestPage.IsAtLogNewRequestPage, "Failed to navigate to Log New Request page.");
                bool tab = LogNewRequestPage.ClickMRODeliveryTab();
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
                RequestStatus.roiAdminapplyrate();
                RequestStatus.qcStatus();
                //   RequestStatus.applyRate();
                //   ROIAdminUpdateReqBlngDetls.pageFees2();
				
                RequestStatus.roiCreateInvoice();
                Driver.Wait(TimeSpan.FromSeconds(2));
                Driver.Instance.Value.FindElement(By.Id("mrocontent_cmdPayCreditCard")).Click();
                Driver.Wait(TimeSpan.FromSeconds(2));
                Driver.Instance.Value.FindElement(By.Id("mrocontent_cmdCheckOut")).Click();
                Driver.Wait(TimeSpan.FromSeconds(5));
                Driver.Instance.Value.FindElement(By.Id("mrocontent_btnTCPay")).Click();
                Driver.Wait(TimeSpan.FromSeconds(2));
                Driver.Instance.Value.SwitchTo().Frame("RadWindow1");
                Driver.Wait(TimeSpan.FromSeconds(5));
                Driver.Wait(TimeSpan.FromSeconds(5));
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
                string ApprovalCode = Driver.Instance.Value.FindElement(By.Id("mrocontent_spanRef")).Text;
                Driver.logger.Pass("Payment successfully submitted and Approval Code: " + ApprovalCode);
                Driver.takeScreenShot();
				
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
