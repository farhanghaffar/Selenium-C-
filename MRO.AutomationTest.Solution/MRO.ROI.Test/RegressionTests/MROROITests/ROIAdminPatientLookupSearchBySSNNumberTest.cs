using AventStack.ExtentReports;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MRO.ROI.Automation.Pages;
using MRO.ROI.Test.ExecutionFactory;
using MRO.ROI.Test.Utilities;
using OpenQA.Selenium.Remote;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MRO.ROI.Test.RegressionTests.MROROITests
{
    [TestClass]
    public class ROIAdminPatientLookupSearchBySSNNumberTest : ROIBaseTest
    {
        public ROIAdminPatientLookupSearchBySSNNumberTest() : base(ROITestArea.ROIAdmin)
        {
        }

        [STATestMethodAttribute]
        [TestCategory(ROITestCategory.Regression)]
        // Converted manual test case 1461 -ROI-Admin-->ROI Admin Patient Lookup Search By SSN NumberTest to automated.
        public void Reg_1461_ROIAdminPatientLookupSearchBySSNNumber()
        {
            logger = extent.CreateTest("Reg_1461_ROIAdminDrillInUserPermissionPatientLookupTest");
            logger.Log(Status.Info, "Converted manual test case 1461 -ROI-Admin-->ROI Admin Patient Lookup Search By SSN NumberTest to automated");
            RemoteWebDriver _driver = Init(TestContext.Properties["browser"].ToString());
            ThreadLocal<RemoteWebDriver> localDriver = new ThreadLocal<RemoteWebDriver>();
            localDriver.Value = _driver;
            RemoteWebDriver driver = localDriver.Value;

            try
            {
                ROIAdminHomePage rOIAdminHomePage = new ROIAdminHomePage(driver, logger, TestContext);
                rOIAdminHomePage.ClickFacilityList();
                logger.Log(Status.Info, "Clicked on facility list", TakeScreenShotAtStep());
                ROIAdminFacilityListPage rOIAdminFacilityListPage = new ROIAdminFacilityListPage(driver, logger, TestContext);
                rOIAdminFacilityListPage.ClickOnROITCompIcon();
                logger.Log(Status.Info, "Clicked on computer icon", TakeScreenShotAtStep());
                ROIFacilityWorkSummaryPage rOIFacilityWorkSummaryPage = new ROIFacilityWorkSummaryPage(driver, logger, TestContext);
                rOIFacilityWorkSummaryPage.SelectPatientLookup();
                ROIAdminPatientLookupPage rOIAdminPatientLookupPage = new ROIAdminPatientLookupPage(driver, logger, TestContext);
                rOIAdminPatientLookupPage.ClickFindPatient();
                rOIAdminPatientLookupPage.ViewFullSSN();
                logger.Log(Status.Info, "Viewed full SSN", TakeScreenShotAtStep());
                rOIAdminPatientLookupPage.ClickLogout();
                rOIAdminHomePage.ClickAuditLog();
                logger.Log(Status.Info, "Clicked on audit log", TakeScreenShotAtStep());
                ROIAdminAuditLogPage rOIAdminAuditLogPage = new ROIAdminAuditLogPage(driver, logger, TestContext);
                rOIAdminAuditLogPage.SetAuditFiltersAndSearch();
                rOIAdminAuditLogPage.VerifyResultsExist();
                logger.Log(Status.Pass, "Succesfylly verified Admin Patient Lookup Search By SSN Number Test", TakeScreenShotAtStep());

                Cleanup(driver);
            }
            catch (Exception ex)
            {
                LogException(driver, logger, ex);

            }
        }
    }
}
