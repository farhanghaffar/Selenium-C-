using AventStack.ExtentReports;
using AventStack.ExtentReports.MarkupUtils;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MRO.ROI.Automation.Pages;
using MRO.ROI.Automation.Selenium;
using MRO.ROI.Test.Utilities;
using OpenQA.Selenium.Remote;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MRO.ROI.Test.APITests
{
    [TestClass]
    public class ChangeAPIRequesterIDWebserviceTest : ROIBaseTest
    {
        public ChangeAPIRequesterIDWebserviceTest() : base(ROITestArea.ROIAdmin)
        {
        }

        [TestMethod]
        [TestCategory(ROITestCategory.Development)]
        public void Reg_8378_ChangeAPIRequesterIDWebservice()
        {
            logger = extent.CreateTest("VerifyChangeAPIRequesterIDWebserviceTest");
            logger.Log(Status.Info, "Starting execution for VerifyChangeAPIRequesterIDWebserviceTest");
            logger.Log(Status.Info, "Step1-Login to ROI admin");
            RemoteWebDriver _driver = Init(TestContext.Properties["browser"].ToString());
            ThreadLocal<RemoteWebDriver> localDriver = new ThreadLocal<RemoteWebDriver>();
            localDriver.Value = _driver;
            RemoteWebDriver driver = localDriver.Value;
            try
            {
                logger.Log(Status.Info, "Step1-5 - Interacting with Postman and getting the Requester ID");
                ROIAPIHelper roiHelper = new ROIAPIHelper(TestContext);
                string requestID = roiHelper.CreateRequestAndChangeAPIStatusRequest();
                logger.Log(Status.Info, "Step6 -Click on lookup id request");                
                ROIAdminHomePage rOIAdminHomePage = new ROIAdminHomePage(driver, logger, TestContext);
                rOIAdminHomePage.SearchByRequestId(requestID);
                rOIAdminHomePage.VerifyHeader();
                
            }
            catch (Exception ex)
            {
                LogException(driver, logger, ex);
            }
        }
    }
}
