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

namespace MRO.ROI.Test.SmokeTests.ROIAdmin
{
    [TestClass]
    public class Reg_Admin_Cancel_BatchScan : ROITestBase
    {
        public Reg_Admin_Cancel_BatchScan() : base(ROITestArea.ROIAdmin)
        {
        }
        //Houdini_HotFix Test case # 2256 ROI Admin, cancel multiple request through batch scan
        [TestMethod]
        // [TestCategory(ROITestCategory.Regression)]
        public void Reg_AdminCancel_BatchScan()
        {
            try
            {
                Driver.logger = Driver.extent.CreateTest("MRO Cancel Reqeust Update Info Test");
                MenuSelector.SelectRoiAdmin("ROIAdmin", "Batch Scan");
                Driver.Wait(TimeSpan.FromSeconds(5));

                Driver.findElement("//*[@name='mrocontent$txtRequestIDs']").Clear();
                Driver.Wait(TimeSpan.FromSeconds(5));
                Driver.Type("//*[@name='mrocontent$txtRequestIDs']", "23628316,23628317,23628318,23628319");
                Driver.Click("//*[@name='mrocontent$cmdCreate']");
                Driver.Wait(TimeSpan.FromSeconds(5));
                Driver.Type("//*[@name='mrocontent$lstMode']", "Set Requests Requester Cancelled");
                Driver.Click("//*[@name='mrocontent$cmdSetTag']");
                Driver.Wait(TimeSpan.FromSeconds(2));
                // LogNewRequest.acceptalaert();
                // Driver.Instance.Value.SwitchTo().Frame("radWndPrompt");
                //   Driver.Wait(TimeSpan.FromSeconds(2));
                WebElementHelper.Click_Enter();
                //  WebElementHelper.Click_Action();
                // Driver.Instance.Value.FindElement(By.XPath(PageElements.ROIAdminList.confirmDialog1_xpath)).Click();
                // Driver.Click("//*[@name='mrocontent$cmdSetTag']");


                Driver.Wait(TimeSpan.FromSeconds(10));

                IList<IWebElement> Patient = Driver.findElements("//*[@id='mrocontent_dgReport']//tr[@class='TableBody']/td/a");
                for (int count = 0; count < Patient.Count; count++)
                {
                    Driver.logger.Info(Patient[count].Text);

                    Patient[count].Click();
                    Driver.Wait(TimeSpan.FromSeconds(5));
                    string canl = Driver.findElement("//*[@id='mrocontent_tdRequestStatus']").Text;
                    if (canl.Equals("Cancelled"))
                    {
                        Driver.logger.Pass("Cancelled succesfully");
                    }
                    else
                    {
                        Driver.logger.Fail("Not Cancelled");
                    }

                    string l1InvoiceAmount = Driver.findElement("//*[@id='mrocontent_tdInvoiceAmount']").Text;
                    Driver.logger.Info("L1 Invoice Amount:" + l1InvoiceAmount);

                    string l1tdretrievalfees = "$" + Driver.Instance.Value.FindElement(By.Id(PageElements.ROIAdminList.l1retrievalfees_id)).Text;
                    Driver.logger.Info("L1 Retrieval fee:" + l1tdretrievalfees);

                    string l1adjustedbalance = Driver.Instance.Value.FindElement(By.Id(PageElements.ROIAdminList.l1adjustedbalance_id)).Text;
                    Driver.logger.Info("L1 Adjusted Balance:" + l1adjustedbalance);


                    Driver.Instance.Value.Navigate().Back();


                    if (l1adjustedbalance.Equals(l1InvoiceAmount))
                    {
                        Driver.logger.Pass("Page Retrieval Fees And Adjusted Balance Is Same And The Test Passed");
                    }
                    else
                    {
                        Driver.logger.Fail("Page Retrieval Fees And Adjusted Balance After Cancelling The Requester Is NOT Same And The Test Failed");
                    }
                    Patient = Driver.findElements("//*[@id='mrocontent_dgReport']//tr[@class='TableBody']/td/a");

                }

                //  }
                // After test clearning request id's and setting back to No Payment status
                Driver.findElement("//*[@name='mrocontent$txtRequestIDs']").Clear();
                Driver.Wait(TimeSpan.FromSeconds(5));
                Driver.Type("//*[@name='mrocontent$txtRequestIDs']", "23628316,23628317,23628318,23628319");
                Driver.Click("//*[@name='mrocontent$cmdCreate']");
                Driver.Wait(TimeSpan.FromSeconds(5));
                Driver.Type("//*[@name='mrocontent$lstMode']", "Clear Requester Cancelled");
                Driver.Wait(TimeSpan.FromSeconds(5));
                Driver.Click("//*[@name='mrocontent$cmdSetTag']");
                Driver.Wait(TimeSpan.FromSeconds(2));
                // Driver.Instance.Value.FindElement(By.XPath(PageElements.ROIAdminList.confirmDialog1_xpath)).Click();
                WebElementHelper.Click_Enter();
                Driver.Wait(TimeSpan.FromSeconds(10));
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


