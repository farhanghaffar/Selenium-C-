using AventStack.ExtentReports;
using AventStack.ExtentReports.MarkupUtils;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MRO.ROI.Automation.Pages.Common;
using MRO.ROI.Automation.Pages.ROIFacility;
using MRO.ROI.Automation.Selenium;
using MRO.ROI.Test.Utilities;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace MRO.ROI.Test.SmokeTests.ROIFacility
{
    [TestClass]
    public class Reg_Comingling_pdf : ROITestBase
    {
        public Reg_Comingling_pdf() : base(ROITestArea.ROIFacility)
        {
        }

        [TestMethod]
        //[TestCategory(ROITestCategory.Regression)]
        public void Reg_Comingling_Pdf_Files()
        {
            try
            {
                Driver.logger = Driver.extent.CreateTest("Regression Co-Mingling PDF Files Test");
                Driver.logger.Log(Status.Pass, "Co-mingling PDF Files Test");

                LogNewRequestPage.GoToLogNewRequestPage();
                Assert.IsTrue(LogNewRequestPage.IsAtLogNewRequestPage, "Failed to navigate to Log New Request page.");
                bool tab = LogNewRequestPage.ClickMRODeliveryTab();
                Assert.IsTrue(tab, "Failed to click on MRO delivery tab");
                string reqestID = LogNewRequestPage.CreateNewMRODeliveryRequest();
                Assert.IsTrue(LogNewRequestPage.NewRequestCreated, "Failed to create new MRO delivery request");
                LogNewRequestPage.GoToRequestStatusPage();

                Assert.IsTrue(FacilityRequestStatusPage.IsAtRequestStatusPage, "Failed to navigate to facility request status page.");
                Driver.logger.Pass("Successfully navigated to facility request status page");
                LogNewRequestPage.PatientNameValidation();
                //
                string reqid = Driver.Instance.Value.FindElement(By.XPath("//*[@id=\"frmServer\"]/table/tbody/tr[1]/td[2]/table/tbody/tr[4]/td/table/tbody/tr/td[2]")).Text;
                Console.Write(reqid);
                Driver.logger.Pass("Request ID: " + reqid);

                //FacilityRequestStatusPage.ScanPatientPages(LogNewRequestPage.PatientFirstName, LogNewRequestPage.PatientLastName);
                ////  LogNewRequestPage.mroToOnsite();
                //FacilityRequestStatusPage.ReleaseRequest();
                //Assert.IsTrue(FacilityRequestStatusPage.IsRequestReleased, "Failed to release request.");
                //Driver.logger.Pass("Successfully released request");


                //COMMENTED THIS CODE TO IMPORT EPIC FILES.
                Driver.Instance.Value.FindElement(By.Id("mrocontent_btnImportDocs_")).Click();
                Driver.Wait(TimeSpan.FromSeconds(3));
                Driver.Instance.Value.SwitchTo().Frame("radWndPrompt");
                //   Driver.Instance.Value.FindElement(By.Id("mrocontent_rauFileUploadfile0")).SendKeys("C:\\TestDocs\\Orignal_Epic_Test1.pdf");
                Driver.Instance.Value.FindElement(By.Id("mrocontent_rauFileUploadfile0")).SendKeys("C:\\TestDocs\\Orignal_Epic_Test1.pdf");

                Driver.Instance.Value.FindElement(By.Id("mrocontent_btnImportDoc")).Click();
                Driver.Wait(TimeSpan.FromSeconds(3));
                Driver.Instance.Value.FindElement(By.Id("mrocontent_btnCloseImport")).Click();
                Driver.logger.Pass("Successfully Uploaded the pdf file");
                Driver.Wait(TimeSpan.FromSeconds(20));
                Driver.Instance.Value.Navigate().Refresh();
                Driver.Instance.Value.SwitchTo().DefaultContent();
                Driver.Wait(TimeSpan.FromSeconds(10));
                Driver.Instance.Value.Navigate().Refresh();
                Driver.Wait(TimeSpan.FromSeconds(3));
                string requestdoc2 = Driver.Instance.Value.FindElement(By.XPath("//*[@id=\"mrocontent_tblComponents\"]/tbody/tr[2]/td[3]")).Text;
                Console.WriteLine(requestdoc2);
                Console.WriteLine("Printing request pages");
                Driver.logger.Pass("Request pages from request status page: " + requestdoc2);
                string patientpages = Driver.Instance.Value.FindElement(By.XPath("//*[@id=\"mrocontent_tblComponents\"]/tbody/tr[3]/td[3]")).Text;
                Driver.logger.Pass("Patient pages from request status page: " + patientpages);

                Driver.Wait(TimeSpan.FromSeconds(5));


                //switching window
                string currentHandle = Driver.Instance.Value.CurrentWindowHandle;
                ReadOnlyCollection<string> originalHandles = Driver.Instance.Value.WindowHandles;
                Driver.Instance.Value.FindElement(By.Id("mrocontent_lnkWVL")).Click();
                WebDriverWait wait = new WebDriverWait(Driver.Instance.Value, TimeSpan.FromSeconds(5));
                string popupWindowHandle = wait.Until<string>((d) =>
                {
                    string foundHandle = null;
                    // Subtract out the list of known handles. In the case of a single
                    // popup, the newHandles list will only have one value.
                    List<string> newHandles = Driver.Instance.Value.WindowHandles.Except(originalHandles).ToList();
                    if (newHandles.Count > 0)
                    {
                        foundHandle = newHandles[0];
                    }
                    return foundHandle;
                });
                Driver.Instance.Value.SwitchTo().Window(popupWindowHandle);

                string requestdoc = Driver.Instance.Value.FindElement(By.XPath("/html/body/div[1]/div[2]/div[1]/div[1]/a[1]")).Text;
                Console.WriteLine(requestdoc);
                Driver.logger.Pass("Request pages from View Documents: " + requestdoc);
                string patientpages1 = Driver.Instance.Value.FindElement(By.XPath("/html/body/div[1]/div[2]/div[1]/div[1]/a[2]")).Text;
                Driver.logger.Pass("Patient pages from View Documents: " + patientpages1);
                string viewpatientname = Driver.Instance.Value.FindElement(By.XPath("//*[@id=\"viewContainer\"]/div[1]/div[1]")).Text;

                Driver.logger.Info("Request Id from view documents: " + viewpatientname);
                Driver.takeScreenShot();
                //Reporting comparing 
                Driver.Wait(TimeSpan.FromSeconds(5));
                Driver.Instance.Value.Close();
                Driver.Instance.Value.SwitchTo().Window(currentHandle);
                //string requestdoc2 = Driver.Instance.Value.FindElement(By.XPath("//*[@id=\"mrocontent_tblComponents\"]/tbody/tr[2]/td[3]")).Text;


                ///html/body/div[1]/div[2]/div[1]/div[1]/a[3]
                //Console.WriteLine(requestdoc2);
                Driver.Wait(TimeSpan.FromSeconds(5));

                // if (requestdoc.Equals(requestdoc2))
                //if (requestdoc2 == requestdoc)
                Driver.Wait(TimeSpan.FromSeconds(5));

                if (requestdoc.EndsWith(requestdoc2))
                {
                    Driver.logger.Log(Status.Pass, "Total request pages are equal");

                }
                else
                {
                    Driver.logger.Log(Status.Fail, "Total request pages are NOT equal");


                }

                if (patientpages1.EndsWith(patientpages))
                {
                    Driver.logger.Log(Status.Pass, "Patient pages from requests status and View documents ARE EQUAL");

                }
                else
                {
                    Driver.logger.Log(Status.Fail, "Patient pages are NOT equal");
                }

                if (viewpatientname.Contains(reqestID))
                {
                    Driver.logger.Log(Status.Pass, "reqestID from requests status and View documents ARE EQUAL");

                }
                else
                {
                    Driver.logger.Log(Status.Fail, "reqestID are NOT equal");
                }
                LoginPage.LogOut();
                Assert.IsTrue(LoginPage.IsAtLoginPage, "Failed to log out successfully.");
                Driver.logger.Log(Status.Pass, "Sucessfully logged out.");
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
