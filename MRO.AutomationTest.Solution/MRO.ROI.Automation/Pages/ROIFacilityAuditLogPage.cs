using AventStack.ExtentReports;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MRO.ROI.Automation.Pages;
using MRO.ROI.Automation.Pages.Common;
using MRO.ROI.Automation.Selenium;
using MRO.ROI.Automation.Utility;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using System;
using System.IO;
using System.Reflection;
using System.Threading;

namespace MRO.ROI.Automation.Pages
{
    public class ROIFacilityAuditLogPage
    {
        public RemoteWebDriver Driver;
        public ExtentTest logger;
        public TestContext Context;
        public ROIFacilityAuditLogPage(RemoteWebDriver driver, ExtentTest _loger, TestContext _context)
        {
            Driver = driver;
            logger = _loger;
            Context = _context;
        }
        public By linkedRequestId = By.XPath("//*[@id='mrocontent_dgReport']/tbody/tr[3]/td[4]");
        public By createdUserElement = By.XPath("//table[@id='mrocontent_dgReport']/tbody/tr[3]/td[3]");
        public By actionElement = By.XPath("//table[@id='mrocontent_dgReport']/tbody/tr[3]/td[2]");
        public By dateElement = By.XPath("//table[@id='mrocontent_dgReport']/tbody/tr[3]/td[1]");
        public By requestElement = By.XPath("//table[@id='mrocontent_dgReport']/tbody/tr[3]/td[6]");
        public By facilityElement = By.XPath("//table[@id='mrocontent_dgReport']/tbody/tr[3]/td[5]");


        /// <summary>
        /// Verify linked request information
        /// </summary>
        public void VerificationForAuditLog()
        {
            try
            {
                string infomsg = Driver.GetText(linkedRequestId);
                logger.Log(Status.Info, $"Audit log:{infomsg}");
                string user = Driver.GetText(createdUserElement);
                string action = Driver.GetText(actionElement);
                string date = Driver.GetText(dateElement);
                logger.Log(Status.Info, $"Created date :{date}");
                string facilityname = Driver.GetText(facilityElement);
                logger.Log(Status.Info, $"Selected facility name : ({facilityname})");
                string request = Driver.GetText(requestElement);
                logger.Log(Status.Info, $"Created Request : ({request})");
            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to get linked request information with details as Message : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }
        /// <summary>
        ///Get RequestId
        /// </summary>
        public string ReturnLinkedReqId()
        {
            try
            {
                string requestID = Driver.FindElementBy(linkedRequestId).Text.ToString();
                string id = requestID.Split(':')[1].ToString().Trim();
                return id;

            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to get request id with details as Message : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }

        public string ReturnAction()
        {
            try
            {
                return Driver.GetText(actionElement);
            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to get action  with details as Message : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }
        public string ReturnCreatedUser()
        {
            try
            {
                string username = Driver.GetText(createdUserElement);
                string _username = username.Split('(')[1].Replace(')', ' ').Trim();
                return _username;
            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to get action  with details as Message : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }

    }

}
