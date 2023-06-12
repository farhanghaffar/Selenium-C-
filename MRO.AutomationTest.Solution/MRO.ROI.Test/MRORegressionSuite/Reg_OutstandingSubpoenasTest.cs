using AventStack.ExtentReports;
using AventStack.ExtentReports.MarkupUtils;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MRO.ROI.Automation.Common.Navigation;
using MRO.ROI.Automation.Pages;
using MRO.ROI.Automation.Selenium;
using MRO.ROI.Test.Utilities;
using OpenQA.Selenium;
using System;

namespace MRO.ROI.Test.MRORegressionSuite
{
    [TestClass]
    public class Reg_OutstandingSubpoenasTest : ROITestBase
    {
        public Reg_OutstandingSubpoenasTest() : base(ROITestArea.ROIAdmin)
        {

        }
        [TestMethod]
        [TestCategory(ROITestCategory.Regression)]
        public void Reg_Outstanding_Subpoenas()
        {
            try
            {
                //Driver.Instance.Value.Manage().Window.Maximize(); Driver.Instance.Value.Manage().Window.Maximize();
                Driver.logger = Driver.extent.CreateTest("Reg_Outstanding_SubpoenasTest");
                Driver.logger.Info("Regression Work Summary,Documents Required Test");
                MenuSelector.SelectRoiAdmin("Facilities", "Facility List");
                ROIAdminFacalitiesListPage.gotoROITestFacility();
                Driver.Wait(TimeSpan.FromSeconds(5));
                WebElementHelper.ScrollIntoView1();


                Driver.Instance.Value.SwitchTo().DefaultContent();
                Driver.Wait(TimeSpan.FromSeconds(5));
                Driver.Instance.Value.SwitchTo().Frame(0);
                Driver.Wait(TimeSpan.FromSeconds(1));
                Driver.Instance.Value.SwitchTo().Frame("rdForm");
                Driver.Wait(TimeSpan.FromSeconds(1));
                IWebElement rowsoutside = Driver.Instance.Value.FindElement(By.XPath("//*[@id='pending-subpoenas']"));

                string rowsoutsideText = rowsoutside.Text;
                Driver.logger.Log(Status.Info, "Print Number of rows from Electronic Requests: " + rowsoutsideText);
                rowsoutside.Click(); Driver.Wait(TimeSpan.FromSeconds(5)); Driver.Instance.Value.SwitchTo().DefaultContent();
                IWebElement rowsinside = Driver.Instance.Value.FindElement(By.XPath(PageElements.ROIAdminList.pendingRequst_xpath));


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
                         //   Assert.Fail(ex.Message);
            }
        }
    }
}

