using AventStack.ExtentReports;
using AventStack.ExtentReports.MarkupUtils;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MRO.ROI.Automation;
using MRO.ROI.Automation.Pages;
using MRO.ROI.Automation.Selenium;
using MRO.ROI.Test.Utilities;
using Newtonsoft.Json.Linq;
using OpenQA.Selenium.Remote;
using System;
using System.Threading;

namespace MRO.ROI.Test.APITest
{
    [TestClass]
    public class CreateRequestAndChangeAPIWebServiceStatusTest: ROIBaseTest
    {
        public CreateRequestAndChangeAPIWebServiceStatusTest() : base(ROITestArea.ROIAdmin)
        {
        }

        [TestMethod]
        [TestCategory(ROITestCategory.Regression)]
        // Converted manual test case 8305-ROI-Admin-->Create Request and change API status web service status to automated.
        public void Reg_8305_CreateRequestAndChangeAPIWebServiceStatus()
        {

            logger = extent.CreateTest("Reg_8305_CreateRequestAndChangeAPIWebServiceStatus");
            logger.Log(Status.Info, "Converted manual test case 8305-ROI-Admin-->Create Request and change API status web service status to automated.");
            RemoteWebDriver _driver = Init(TestContext.Properties["browser"].ToString());
            ThreadLocal<RemoteWebDriver> localDriver = new ThreadLocal<RemoteWebDriver>();
            localDriver.Value = _driver;
            RemoteWebDriver driver = localDriver.Value;
            try
            {
                ROIAPIHelper roiHelper = new ROIAPIHelper(TestContext);
                ROIAdminHomePage rOIAdminHomePage = new ROIAdminHomePage(driver, logger, TestContext);          
                roiHelper.CreateRequestAndChangeAPIStatusRequest();
                logger.Log(Status.Pass, "Request created");
                rOIAdminHomePage.SearchByRequestId(MetaDataValues.RequesterRefValue);
                ROIAdminRequestStatusPage rOIAdminRequestStatusPage = new ROIAdminRequestStatusPage(driver, logger, TestContext);                
                rOIAdminRequestStatusPage.ClickAddIssueBtn();
                ROIAdminAddIssuePage rOIAdminAddIssuePage = new ROIAdminAddIssuePage(driver, logger, TestContext);                
                rOIAdminAddIssuePage.SetIssue();
                logger.Log(Status.Info, "Issue added");
                var response= roiHelper.GetStatusresponse();
                var details = JObject.Parse(response.Content);

                string statusResponseIssue_Name= ((Newtonsoft.Json.Linq.JValue)details?["issue_name"]).Value.ToString();
                string statusResponseOpen_Issue = ((Newtonsoft.Json.Linq.JValue)details?["has_open_issues"]).Value.ToString();
                string statusResponseIssue_Description = ((Newtonsoft.Json.Linq.JValue)details?["issue_description"]).Value.ToString();

                Assert.AreEqual(statusResponseIssue_Name, "Cover Letter Missing");
                Assert.AreEqual(statusResponseOpen_Issue, "true");
                Assert.IsNotNull(statusResponseIssue_Description);
                logger.Log(Status.Pass, "Issue reasponse details validated");

                rOIAdminRequestStatusPage.DeleteIssue();
                logger.Log(Status.Info, "Issue deleted");

                var deletedResponse= roiHelper.GetStatusresponseAfterDeleting();
                var deletedDetails = JObject.Parse(response.Content);

                string deletedIssue_Name = ((Newtonsoft.Json.Linq.JValue)details?["issue_name"]).Value.ToString();
                string deletedOpen_Issue = ((Newtonsoft.Json.Linq.JValue)details?["has_open_issues"]).Value.ToString();
                string deletedIssue_Description = ((Newtonsoft.Json.Linq.JValue)details?["issue_description"]).Value.ToString();

                Assert.IsNull(deletedIssue_Name, "Issue name returned from api is not null");
                Assert.AreEqual(deletedOpen_Issue, "false","Deleted open issue returned from api as true");
                Assert.IsNull(deletedIssue_Description, "Deleted issue description returned from api is null");
                logger.Log(Status.Pass, "Deleted issue response validated");
                Cleanup(driver);

            }
            catch (Exception ex)
            {
                LogException(driver, logger, ex);
            }
        }
    }
}
