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
    public class ROIContractValidationTest : ROIBaseTest
    {
        public ROIContractValidationTest() : base(ROITestArea.ROIAdmin)
        {
        }

        [STATestMethodAttribute]
        [TestCategory(ROITestCategory.Regression)]
        // Converted manual test case 11547-ROI-Admin-->Contract Validation improvements to automated.
        public void Reg_11547_ContractValidationimprovements()
        {
            logger = extent.CreateTest("Reg_11547_ContractValidationimprovements");
            logger.Log(Status.Info, "Converted manual test case 11547-ROI-Admin--> Contract Validation improvements to automated");
            RemoteWebDriver _driver = Init(TestContext.Properties["browser"].ToString());
            ThreadLocal<RemoteWebDriver> localDriver = new ThreadLocal<RemoteWebDriver>();
            localDriver.Value = _driver;
            RemoteWebDriver driver = localDriver.Value;

            try
            {
                string selectedfacility = IniHelper.ReadConfig("ContractValidationimprovements", "ROIT");
                ROIAdminHomePage adminHomePage = new ROIAdminHomePage(driver, logger, TestContext);
                adminHomePage.ContractList();
                ROIAdminContractListPage contractListPage = new ROIAdminContractListPage(driver, logger, TestContext);
                contractListPage.SelectFacilityInContractListPage(selectedfacility);
                contractListPage.ClickOnAddContract();


                ROIAdminAddContractPage addContractPage = new ROIAdminAddContractPage(driver, logger, TestContext);
                string contractOne = addContractPage.CreateContract();
                ROIAdminVerifyActiveContractsPage activeContractPage = new ROIAdminVerifyActiveContractsPage(driver, logger, TestContext);
                logger.Log(Status.Pass, $"Created contract name is :{(contractOne)}", TakeScreenShotAtStep());
                activeContractPage.ReturnToContractList();

                contractListPage.ClickOnAddContract();
                string contractTwo = addContractPage.CreateContract();
                logger.Log(Status.Pass, $"Created contract name is :{(contractTwo)}", TakeScreenShotAtStep());
                activeContractPage.ReturnToContractList();

                string _contractOne = contractListPage.VerifyCurrentMonthContracts();
                Assert.AreEqual(contractTwo, _contractOne, "Failed to verify contract names");
                logger.Log(Status.Pass, "Verified created contracts are saved", TakeScreenShotAtStep());

                contractListPage.ClickOnVerifyAllContractButton();
                int selectedPreviousMonth = activeContractPage.SelectPreviousMonthContractList();
                int contractFromDate = activeContractPage.VerifyPreviousMonthContracts();
                Assert.AreEqual(selectedPreviousMonth-1, contractFromDate, "Failed to verify previous month contracts");
                logger.Log(Status.Pass, "Verified that contracts are displayed as per selected month", TakeScreenShotAtStep());

                activeContractPage.ReturnToContractList();
                contractListPage.DeleteCreatedCurrentMonthContract();
                bool isDeleted = contractListPage.VerifyDeletedContracts();
                Assert.IsTrue(isDeleted, "Contracts are not deleted");
                logger.Log(Status.Info, "Created contracts are deleted", TakeScreenShotAtStep());
                Cleanup(driver);
            }

            catch (Exception ex)
            {
                LogException(driver, logger, ex);

            }
        }
    }

}

