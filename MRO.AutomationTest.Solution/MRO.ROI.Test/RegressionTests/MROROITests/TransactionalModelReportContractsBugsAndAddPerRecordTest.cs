using AventStack.ExtentReports;
using AventStack.ExtentReports.MarkupUtils;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MRO.ROI.Automation.Common;
using MRO.ROI.Automation.Pages;
using MRO.ROI.Automation.Pages.Common;
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
    public class TransactionalModelReportContractsBugsAndAddPerRecordTest : ROIBaseTest
    {

        public TransactionalModelReportContractsBugsAndAddPerRecordTest() : base(ROITestArea.ROIAdmin)
        {

        }
        [STATestMethodAttribute]
        [TestCategory(ROITestCategory.Regression)]
        // Converted manual test case 12135-ROI-Admin-->Transactional Model Report - Contracts bugs and Add $ per record to automated.
        public void Reg_12135_TransactionalModelReportContractsBugsAndAddPerRecordTest()
        {
            logger = extent.CreateTest("Reg_12135_TransactionalModelReportContractsBugsAndAddPerRecordTest");
            logger.Log(Status.Info, "Converted manual test case 12135-ROI-Admin-->Transactional Model Report - Contracts bugs and Add $ per record to automated.");
            RemoteWebDriver _driver = Init(TestContext.Properties["browser"].ToString());
            ThreadLocal<RemoteWebDriver> localDriver = new ThreadLocal<RemoteWebDriver>();
            localDriver.Value = _driver;
            RemoteWebDriver driver = localDriver.Value;
            try
            {
                ROIMenuSelector menuSelector = new ROIMenuSelector(driver, logger, TestContext);
                Iframe frame = new Iframe(driver, logger, TestContext);
                menuSelector.Select("Financial", "Transactional Model Report");
                frame.SwitchToRoiFrame();

                ROIAdminTransactionalModelReportPage rOIAdminTransactionalModelReportPage = new ROIAdminTransactionalModelReportPage(driver, logger, TestContext);
                rOIAdminTransactionalModelReportPage.CreateTransactionalModelReport("[All]", "[All]", true);
                logger.Log(Status.Info, "Transactional report created for Facility : [All] Contract : [All] IncludeTest : True");

                logger.Log(Status.Info, "Verifying the non billable,tracked and total for passthrough postage section of the report all have dollar signs", TakeScreenShotAtStep());
                bool _postageCostAndShippingDetailsPassThroughPostage = rOIAdminTransactionalModelReportPage.VerifyNonBillableAndTotalCostUnderPassThroughPostage();
                Assert.IsTrue(_postageCostAndShippingDetailsPassThroughPostage, "Failed to verify non billable,tracked and total for passthrough postage section of the report all have dollar signs");
                logger.Log(Status.Pass, "Successfully verified the non billable,tracked and total for passthrough postage section of the report all have dollar signs");

                logger.Log(Status.Info, "Verifying the less sales tax, less billable postage and total at the bottom of the report all have dollar signs ", TakeScreenShotAtStep());
                bool _postageCostAndShippingDetailsFaxedPages = rOIAdminTransactionalModelReportPage.VerifyLessSalesTaxAndBillablePostageAndTotalCostUnderFaxedPages();
                Assert.IsTrue(_postageCostAndShippingDetailsFaxedPages, "Failed to verify less sales tax, less billable postage and total at the bottom of the report all have dollar signs");
                logger.Log(Status.Pass, "Successfully verified the less sales tax, less billable postage and total at the bottom of the report all have dollar signs");


                rOIAdminTransactionalModelReportPage.ClickOnCustomize();                
                rOIAdminTransactionalModelReportPage.CreateTransactionalModelReport("ROI Test Facility", "[All]", true);
                rOIAdminTransactionalModelReportPage.ClickOnCustomize();

                logger.Log(Status.Info, "Verifying the requests and locations within the records are associated to the ROI test facility");
                bool _roiTestFacility = rOIAdminTransactionalModelReportPage.VerifyRoiTestFacilitySelected();
                Assert.IsTrue(_roiTestFacility, "Failed to verify the requests and locations within the records are associated to the ROI test facility");
                logger.Log(Status.Pass, "Successfully verified the requests and locations within the records are associated to the ROI test facility");

                Cleanup(driver);
            }
            catch (Exception ex)
            {
                LogException(driver, logger, ex);
            }
        }
    }

}