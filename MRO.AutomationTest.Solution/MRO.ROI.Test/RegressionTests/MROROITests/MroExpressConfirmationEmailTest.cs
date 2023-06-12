using AventStack.ExtentReports;
using DataDrivenProject;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MRO.ROI.Automation.Pages;
using MRO.ROI.Test.ExecutionFactory;
using MRO.ROI.Test.Utilities;
using OpenQA.Selenium.Remote;
using System;
using System.IO;
using System.Threading;

namespace MRO.ROI.Test.RegressionTests.MROROITests
{
    [TestClass]
    public class MroExpressConfirmationEmailTest: ROIBaseTest
    {
        public MroExpressConfirmationEmailTest() : base(ROITestArea.ROIAdmin)
        {
        }

        [STATestMethodAttribute]
        [TestCategory(ROITestCategory.Development)]
        //Converted manual test case 8632-ROI-Admin--> MRO eXpress - Confirmation Email from ROI to automated.
        public void Reg_8632_ROIAdminMROExpressConfirmationEmailFromROI()
        {
            logger = extent.CreateTest("Reg_8632_ROIAdminMROExpressConfirmationEmailFromROI");
            logger.Log(Status.Info, "Converted manual test case 8632-ROI-Admin--> MRO eXpress - Confirmation Email from ROI to automated.");

            RemoteWebDriver _driver = NavigateToMROExpressURL();
            ThreadLocal<RemoteWebDriver> localDriver = new ThreadLocal<RemoteWebDriver>();
            localDriver.Value = _driver;
            RemoteWebDriver driver = localDriver.Value;
            try
            {
                MroExpressWizardPage mroExpressWizardPage = new MroExpressWizardPage(driver,logger,TestContext);
                mroExpressWizardPage.SubitMROExpressServiceRequest();
                //There is a bug on this test case after 26 steps - under e- requests requestid is not created
                Cleanup(driver);
                
            }
            catch (Exception ex)
            {
                LogException(driver, logger, ex);
            }

        }
    }
}
