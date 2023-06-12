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

namespace MRO.ROI.Test.SmokeTests.ROIAdmin
{
    [TestClass]
    public class RoiAdminFARSinglePayment : ROITestBase
    {

        //string reqID974 = null;
        static string reqIDDev = null;
        static string reqIDRon = null;
        public RoiAdminFARSinglePayment() : base(ROITestArea.ROIAdmin)
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
            Driver.logger.Info("Version: " + version);
        }
        //[DataRow("ROI3-1-1-974")]
        [DataRow("ROI3-Dev")]
        [DataRow("ROI3-Ron")]
        [TestMethod]
        //[TestCategory(ROITestCategory.Regression)]
        public void Reg_FAR_SinglePayment(string loginOverride)
        {
            try
            {
                Driver.logger = Driver.extent.CreateTest("FAR Rate Test");
                Driver.logger.Info("FAR Teting on this URL: " + Driver.Instance.Value.Url);

                LoginOverride(loginOverride);

                MenuSelector.Select("ROI Requests", "Log a New Request");
                bool tab = LogNewRequestPage.ClickMRODeliveryTab();
                Assert.IsTrue(tab, "Failed to click on MRO delivery tab");
                LogNewRequestPage.CreateNewMRODeliveryRequest();
                Assert.IsTrue(LogNewRequestPage.NewRequestCreated, "Failed to create new MRO delivery request");
                string requestID = ROIAdminFacalitiesListPage.getRequestid();
                Console.WriteLine(requestID);
                Driver.logger.Log(Status.Info, "Request ID: " + requestID);
                if (loginOverride.Equals("ROI3-Dev"))
                //if (loginOverride.Equals("ROI3-1-1-974"))
                {

                    reqIDDev = requestID;
                }
                else
                {
                    reqIDRon = requestID;
                }


                LogNewRequestPage.GoToRequestStatusPage();
                Assert.IsTrue(FacilityRequestStatusPage.IsAtRequestStatusPage, "Failed to navigate to facility request status page.");

                FacilityRequestStatusPage.ScanPatientPages(LogNewRequestPage.PatientFirstName, LogNewRequestPage.PatientLastName);
                FacilityRequestStatusPage.ReleaseRequest();
                LogNewRequestPage.facillogoutbutton();
                ROIAdminFacalitiesListPage.roilookupidadmin(requestID);
                //RequestStatus.roiAssignReq();
                //RequestStatus.roiAdminSearch();
                //RequestStatus.roiSaveDonebtn();
                //RequestStatus.qcStatus();

                //string data1 = Driver.Instance.Value.FindElement(By.Id("mrocontent_lblPatient")).Text;
                //Driver.logger.Info("Patient name: " + data1);


                //string ratetext = Driver.Instance.Value.FindElement(By.Id("mrocontent_lblAppliedRate")).Text;
                //Driver.logger.Pass(ratetext);

                Driver.Wait(TimeSpan.FromSeconds(5));
                Driver.Instance.Value.FindElement(By.Id("mrocontent_cmdLedgerDetail")).Click();
                Driver.Click("//*[@id='mrocontent_cmdPayments']");
                Driver.Type("//*[@id='mrocontent_txtAmount']", "10.00");
                Driver.Type("//*[@id='mrocontent_txtDate']", ".");

                Driver.Click("//*[@id='mrocontent_cmdAdd']");


                if (loginOverride.Equals("ROI3-Dev"))
                //if (loginOverride.Equals("ROI3-1-1-974"))
                {

                    LoginPage.LogOut();
                }
                else
                {

                    Driver.Wait(TimeSpan.FromSeconds(5));
                    MenuSelector.Select("Profile", "QA Test");
                    Driver.Wait(TimeSpan.FromSeconds(2));
                    Driver.Instance.Value.FindElement(By.Id("mrocontent_txtQASP")).Clear();
                    Driver.Instance.Value.FindElement(By.Id("mrocontent_txtQASP")).SendKeys("A1_CheckL1AcrossRequests");
                    Driver.Wait(TimeSpan.FromSeconds(2));
                    Driver.Instance.Value.FindElement(By.Id("mrocontent_txtP1")).Clear();
                    Driver.Instance.Value.FindElement(By.Id("mrocontent_txtP1")).SendKeys(reqIDDev);
                    Driver.Wait(TimeSpan.FromSeconds(2));
                    Driver.Instance.Value.FindElement(By.Id("mrocontent_txtP2")).Clear();
                    Driver.Instance.Value.FindElement(By.Id("mrocontent_txtP2")).SendKeys(reqIDRon);
                    Driver.Wait(TimeSpan.FromSeconds(2));
                    Driver.Instance.Value.FindElement(By.Id("mrocontent_cmdExecute")).Click();
                    string result1 = Driver.Instance.Value.FindElement(By.Id("mrocontent_txtResults")).GetAttribute("value");
                    Console.WriteLine(result1);
                    if (result1.Equals("GOOD"))
                    {
                        Driver.logger.Pass("Version 974 & Ron version Ledger 2 payments are same test passed");
                    }
                    else
                    {
                        Driver.logger.Fail("Version 974 & Ron version Ledger 2 payments are NOT same test failed");
                    }

                    RemoteWebDriver dr1 = Driver.Instance.Value;
                    IJavaScriptExecutor js1 = (IJavaScriptExecutor)Driver.Instance.Value;
                    js1.ExecuteScript("window.scrollBy(0,950)");
                    //Driver.takeScreenShot();
                    Driver.Wait(TimeSpan.FromSeconds(3));

                    Driver.logger.Info("Far Testing Completed at this URL: " + Driver.Instance.Value.Url);
                    Driver.takeScreenShot();
                    js1.ExecuteScript("window.scrollBy(0,-950)");

                    //   }
                    // Ledger 2 A1_CheckL2AcrossRequests comparison
                    Driver.Wait(TimeSpan.FromSeconds(5));
                    MenuSelector.Select("Profile", "QA Test");
                    Driver.Wait(TimeSpan.FromSeconds(2));
                    Driver.Instance.Value.FindElement(By.Id("mrocontent_txtQASP")).Clear();
                    Driver.Instance.Value.FindElement(By.Id("mrocontent_txtQASP")).SendKeys("A1_CheckL2AcrossRequests");
                    Driver.Wait(TimeSpan.FromSeconds(2));
                    Driver.Instance.Value.FindElement(By.Id("mrocontent_txtP1")).Clear();
                    Driver.Instance.Value.FindElement(By.Id("mrocontent_txtP1")).SendKeys(reqIDDev);
                    Driver.Wait(TimeSpan.FromSeconds(2));
                    Driver.Instance.Value.FindElement(By.Id("mrocontent_txtP2")).Clear();
                    Driver.Instance.Value.FindElement(By.Id("mrocontent_txtP2")).SendKeys(reqIDRon);
                    Driver.Wait(TimeSpan.FromSeconds(2));
                    Driver.Instance.Value.FindElement(By.Id("mrocontent_cmdExecute")).Click();
                    string result = Driver.Instance.Value.FindElement(By.Id("mrocontent_txtResults")).GetAttribute("value");
                    Console.WriteLine(result);
                    if (result.Equals("GOOD"))
                    {
                        Driver.logger.Pass("Version 974 & Ron version Ledger 2 payments are same test passed");
                    }
                    else
                    {
                        Driver.logger.Fail("Version 974 & Ron version Ledger 2 payments are NOT same test failed");
                    }
                    Driver.Wait(TimeSpan.FromSeconds(2));
                    RemoteWebDriver dr = Driver.Instance.Value;
                    IJavaScriptExecutor js = (IJavaScriptExecutor)Driver.Instance.Value;
                    js.ExecuteScript("window.scrollBy(0,950)");
                    //Driver.takeScreenShot();
                    Driver.Wait(TimeSpan.FromSeconds(3));

                    Driver.logger.Info("Far Testing Completed at this URL: " + Driver.Instance.Value.Url);
                    Driver.takeScreenShot();
                    js.ExecuteScript("window.scrollBy(0,-950)");

                    //Taxes comparison vertex test
                    // Ledger 2 A1_CheckL2AcrossRequests comparison
                    Driver.Wait(TimeSpan.FromSeconds(5));
                    MenuSelector.Select("Profile", "QA Test");
                    Driver.Wait(TimeSpan.FromSeconds(2));
                    Driver.Instance.Value.FindElement(By.Id("mrocontent_txtQASP")).Clear();
                    Driver.Instance.Value.FindElement(By.Id("mrocontent_txtQASP")).SendKeys("A1_CheckVertexAcrossRequests");
                    Driver.Wait(TimeSpan.FromSeconds(2));
                    Driver.Instance.Value.FindElement(By.Id("mrocontent_txtP1")).Clear();
                    Driver.Instance.Value.FindElement(By.Id("mrocontent_txtP1")).SendKeys(reqIDDev);
                    Driver.Wait(TimeSpan.FromSeconds(2));
                    Driver.Instance.Value.FindElement(By.Id("mrocontent_txtP2")).Clear();
                    Driver.Instance.Value.FindElement(By.Id("mrocontent_txtP2")).SendKeys(reqIDRon);
                    Driver.Wait(TimeSpan.FromSeconds(2));
                    Driver.Instance.Value.FindElement(By.Id("mrocontent_cmdExecute")).Click();
                    string result2 = Driver.Instance.Value.FindElement(By.Id("mrocontent_txtResults")).GetAttribute("value");
                    Console.WriteLine(result2);
                    if (result2.Equals("GOOD"))
                        if (result2.Equals("GOOD"))
                        {
                            Driver.logger.Pass("Version 974 & Ron version Ledger 2 payments are same test passed");
                        }
                        else
                        {
                            Driver.logger.Fail("Version 974 & Ron version Ledger 2 payments are NOT same test failed");
                        }
                    Driver.Wait(TimeSpan.FromSeconds(2));
                    RemoteWebDriver dr2 = Driver.Instance.Value;
                    IJavaScriptExecutor js2 = (IJavaScriptExecutor)Driver.Instance.Value;
                    js2.ExecuteScript("window.scrollBy(0,950)");
                    //Driver.takeScreenShot();
                    Driver.Wait(TimeSpan.FromSeconds(3));

                    Driver.logger.Info("Far Testing Completed at this URL: " + Driver.Instance.Value.Url);
                    Driver.takeScreenShot();
                    js2.ExecuteScript("window.scrollBy(0,-950)");

                    //  RequestStatus.roiadmlogout();

                }
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
