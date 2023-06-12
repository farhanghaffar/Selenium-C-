using AventStack.ExtentReports;
using AventStack.ExtentReports.MarkupUtils;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MRO.ROI.Automation.Pages;
using MRO.ROI.Automation.Selenium;
using MRO.ROI.Test.ExecutionFactory;
using MRO.ROI.Test.Utilities;
using OpenQA.Selenium.Remote;
using System;
using System.Threading;
using static MRO.ROI.Automation.Utility.IniFile;

namespace MRO.ROI.Test.RegressionTests.MROROITests
{
    [TestClass]
    public class ROIProfitabilityReportIncludeConfirmFeesIncludedInMonthlyFeesColumnTest : ROIBaseTest
    {
        public ROIProfitabilityReportIncludeConfirmFeesIncludedInMonthlyFeesColumnTest() : base(ROITestArea.ROIAdmin)
        {
        }

        [STATestMethodAttribute]
        [TestCategory(ROITestCategory.Passed)]
        // Converted manual test case 4320-ROI-Admin-->Test Case 4320: Profitability Report - Include/confirm fees included in Monthly Fees Column to automated.
        public void Reg_4320_ROIProfitabilityReportIncludeConfirmFeesIncludedInMonthlyFeesColumnTest()
        {
            logger = extent.CreateTest("Reg_4320_ROIProfitabilityReportIncludeConfirmFeesIncludedInMonthlyFeesColumnTest");
            logger.Log(Status.Info, "Converted manual test case 4320-ROI-Admin-->Test Case 4320: Profitability Report - Include/confirm fees included in Monthly Fees Column to automated");
            RemoteWebDriver _driver = Init(TestContext.Properties["browser"].ToString());
            ThreadLocal<RemoteWebDriver> localDriver = new ThreadLocal<RemoteWebDriver>();
            localDriver.Value = _driver;
            RemoteWebDriver driver = localDriver.Value;


            try
            {
                ROIAdminHomePage rOIAdminHomePage = new ROIAdminHomePage(driver, logger, TestContext);
                rOIAdminHomePage.ContractList();
                logger.Log(Status.Info, "Verified that contract list page opened successfully");
                ROIAdminContractListPage rOIAdminContractListPage = new ROIAdminContractListPage(driver, logger, TestContext);
                rOIAdminContractListPage.SelectROIFacilityFromDropdown();

                ROIAdminAddContractPage rOIAdminAddContractPage = new ROIAdminAddContractPage(driver, logger, TestContext);
                string pageName = rOIAdminAddContractPage.VerifyEditContractPageHeader();
                Assert.AreEqual(pageName, "Edit Contract");
                logger.Log(Status.Info, "Verified edit contract page opened successfully",TakeScreenShotAtStep());

                bool isDisplayed= rOIAdminAddContractPage.VerifyMonthlyFeeValues();
                Assert.IsTrue(isDisplayed, "Failed to verify monthly fee values");
                logger.Log(Status.Pass, "Verified that monthly fee sections contains the terms", TakeScreenShotAtStep());

                rOIAdminAddContractPage.AddingMonthlyFeeTermAmount();
                logger.Log(Status.Pass, "Verified that amount added successfully", TakeScreenShotAtStep());

                logger.Log(Status.Info, "Step-8 related to the database so should be executed manually");
                Cleanup(driver);

            }

            catch (Exception ex)
            {
                LogException(driver, logger, ex);

            }
        }
    }

}

