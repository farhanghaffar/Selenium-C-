using AventStack.ExtentReports;
using AventStack.ExtentReports.MarkupUtils;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MRO.ROI.Automation.Common.Navigation;
using MRO.ROI.Automation.Pages;
using MRO.ROI.Automation.Pages.ROIFacility;
using MRO.ROI.Automation.Selenium;
using MRO.ROI.Test.Utilities;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using WindowsInput;
using WindowsInput.Native;

namespace MRO.ROI.Test.SmokeTests.ROIAdmin
{

    [TestClass]
    public class Reg_MultiplePatientScanPages : ROITestBase
    {
        public Reg_MultiplePatientScanPages() : base(ROITestArea.ROIAdmin)
        {

        }
        //These will create the number of requests Id's
        [DataRow("")]
        //[DataRow("")]
        //[DataRow("")]
        //[DataRow("")]
        //[DataRow("")]
        //[DataRow("")]
        //[DataRow("")]
        //[DataRow("")]
        //[DataRow("")]
        //[DataRow("")]
        [DataTestMethod]
        //  [TestCategory(ROITestCategory.Regression)]
        public void Reg_MultiplePatientScanPagesTest(string reqID)
        {
            try
            {
                Driver.logger = Driver.extent.CreateTest("MRO ROI Admin Test");
                MenuSelector.SelectRoiAdmin("Facilities", "Facility List");
                ROIAdminFacalitiesListPage.gotoROITestFacility();
                MenuSelector.Select("ROI Requests", "Log a New Request");
                bool tab = LogNewRequestPage.ClickMRODeliveryTab();
                Assert.IsTrue(tab, "Failed to click on MRO delivery tab");
                LogNewRequestPage.CreateNewMRODeliveryRequest();
                Assert.IsTrue(LogNewRequestPage.NewRequestCreated, "Failed to create new MRO delivery request");
                string requestID = ROIAdminFacalitiesListPage.getRequestid();


                LogNewRequestPage.GoToRequestStatusPage();
                Assert.IsTrue(FacilityRequestStatusPage.IsAtRequestStatusPage, "Failed to navigate to facility request status page.");
                FacilityRequestStatusPage.ScanPatientPagesupperhalf(LogNewRequestPage.PatientFirstName, LogNewRequestPage.PatientLastName);
                //FacilityRequestStatusPage.ScanPatientPages(LogNewRequestPage.PatientFirstName, LogNewRequestPage.PatientLastName);

                Driver.Wait(TimeSpan.FromSeconds(5));
                InputSimulator simulator = new InputSimulator();
                for (int i = 0; i < 5; i++)
                {
                    Driver.Wait(TimeSpan.FromSeconds(1));
                    simulator.Keyboard.ModifiedKeyStroke(VirtualKeyCode.CONTROL, VirtualKeyCode.VK_T);
                    simulator.Keyboard.ModifiedKeyStroke(VirtualKeyCode.CONTROL, VirtualKeyCode.VK_T);
                    simulator.Keyboard.ModifiedKeyStroke(VirtualKeyCode.CONTROL, VirtualKeyCode.VK_T);
                    simulator.Keyboard.ModifiedKeyStroke(VirtualKeyCode.CONTROL, VirtualKeyCode.VK_T);
                    simulator.Keyboard.ModifiedKeyStroke(VirtualKeyCode.CONTROL, VirtualKeyCode.VK_T);
                    simulator.Keyboard.ModifiedKeyStroke(VirtualKeyCode.CONTROL, VirtualKeyCode.VK_T);
                    simulator.Keyboard.ModifiedKeyStroke(VirtualKeyCode.CONTROL, VirtualKeyCode.VK_T);
                    simulator.Keyboard.ModifiedKeyStroke(VirtualKeyCode.CONTROL, VirtualKeyCode.VK_T);
                    simulator.Keyboard.ModifiedKeyStroke(VirtualKeyCode.CONTROL, VirtualKeyCode.VK_T);
                    simulator.Keyboard.ModifiedKeyStroke(VirtualKeyCode.CONTROL, VirtualKeyCode.VK_T);
                    simulator.Keyboard.ModifiedKeyStroke(VirtualKeyCode.CONTROL, VirtualKeyCode.VK_T);
                    simulator.Keyboard.ModifiedKeyStroke(VirtualKeyCode.CONTROL, VirtualKeyCode.VK_T);
                    simulator.Keyboard.ModifiedKeyStroke(VirtualKeyCode.CONTROL, VirtualKeyCode.VK_T);
                    simulator.Keyboard.ModifiedKeyStroke(VirtualKeyCode.CONTROL, VirtualKeyCode.VK_T);
                    simulator.Keyboard.ModifiedKeyStroke(VirtualKeyCode.CONTROL, VirtualKeyCode.VK_T);
                }
                Driver.Wait(TimeSpan.FromSeconds(5));
                simulator.Keyboard.ModifiedKeyStroke(VirtualKeyCode.CONTROL, VirtualKeyCode.VK_A);
                Driver.Wait(TimeSpan.FromSeconds(15));
                WebElementHelper.Click_Enter();
                Driver.logger.Pass("Successfully Scanned Patient Pages");


                //   FacilityRequestStatusPage.ScanPatientPages(LogNewRequestPage.PatientFirstName, LogNewRequestPage.PatientLastName);
                FacilityRequestStatusPage.ReleaseRequest1();
                Driver.Wait(TimeSpan.FromSeconds(2));
                //  Driver.Instance.Value.Navigate().Refresh();


                // caputring requestid and total pages scanned for patient
                string reqid = Driver.Instance.Value.FindElement(By.XPath("//*[@id=\"frmServer\"]/table/tbody/tr[1]/td[2]/table/tbody/tr[4]/td/table/tbody/tr/td[2]")).Text;
                Console.Write(reqid);
                Driver.logger.Pass("Request ID: " + reqid);

                string patientpages = Driver.Instance.Value.FindElement(By.XPath("//*[@id=\"mrocontent_tblComponents\"]/tbody/tr[3]/td[3]")).Text;
                Driver.logger.Pass("Patientt pages from request status page: " + patientpages);



                LogNewRequestPage.facillogoutbutton();
                ROIAdminFacalitiesListPage.roilookupidadmin(requestID);
                RequestStatus.roiAssignReq();
                RequestStatus.roiAdminSearch();
                RequestStatus.roiSaveDonebtn();

                RequestStatus.qcStatus();
                Driver.Instance.Value.FindElement(By.Id("mrocontent_cmdSetNonBillable")).Click();
                LogNewRequestPage.acceptalert();
                Driver.Wait(TimeSpan.FromSeconds(1));
                RequestStatus.roiAdminapplyrate();
                Driver.Wait(TimeSpan.FromSeconds(1));

                SelectElement oSelect = new SelectElement(Driver.Instance.Value.FindElement(By.Id("mrocontent_lstShipmentMethod")));
                oSelect.SelectByText("MAIL");
                Driver.logger.Log(Status.Info, "Delivery Override Selected To Mail.");
                Driver.Wait(TimeSpan.FromSeconds(1));
                Driver.Instance.Value.FindElement(By.Id("mrocontent_btnSaveShipOverride")).Click();
                Driver.Wait(TimeSpan.FromSeconds(1));
                RequestStatus.roiCreateInvoice();
                Driver.Wait(TimeSpan.FromSeconds(1));
                Driver.Instance.Value.FindElement(By.Id("mrocontent_lnkAddShipment")).Click();
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
