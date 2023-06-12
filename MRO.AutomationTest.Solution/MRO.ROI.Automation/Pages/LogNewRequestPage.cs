using AutoIt;
using AventStack.ExtentReports;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MRO.ROI.Automation.Common.Navigation;
using MRO.ROI.Automation.Selenium;
using MRO.ROI.Automation.Utility;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.UI;
using System;
using System.Diagnostics;
using WindowsInput.Native;
using static MRO.ROI.Automation.Common.Navigation.FacilityMenuNavigation.ROIRequests;
using static MRO.ROI.Automation.Common.Navigation.InternalUserNavigation;
using static MRO.ROI.Automation.Common.Navigation.InternalUserNavigation.CreateARequest;

namespace MRO.ROI.Automation.Pages
{
    public class LogNewRequestPage
    {

        public RemoteWebDriver Driver;
        public ExtentTest logger;
        public TestContext Context;
        public LogNewRequestPage(RemoteWebDriver driver, ExtentTest _loger, TestContext _context)
        {
            Driver = driver;
            logger = _loger;
            Context = _context;
        }

        private static string createdDateTime = DateTime.Now.ToString("dd MMMM yyyy hh:mm:ss");
        public static string dateTime;
        public static object ROIRequestsTopMenu;
        public By drpLocation = By.XPath("//div[@id='mrocontent_lstLocation_DropDown']//ul/li");
        public const string locationDDValue = "MRO Automated Regression Test Location 1";
        public By requestReceivedDate = By.Id("mrocontent_txtRequestRcvdDate");
        public By logRequestButton = By.Id("mrocontent_cmdLogRequest");
        public By MRN = By.XPath("//input[@id='mrocontent_txtMRN']");


        public static bool IsAtLogNewRequestPage
        {
            get
            {
                //TODO: var LogNewRequestPageLabel = Driver.FindElement(By.XPath("//td[contains(text(), 'Log a New Request')]"));
                return true;//LogNewRequestPageLabel.Text == "Log a New Request";
            }
        }

        public static bool NewRequestCreated
        {
            get
            {
                //TODO: Logic to confirm that a new request was created
                return true;
            }
        }

        public static string PatientFirstName { get; private set; }
        public static string PatientLastName { get; private set; }

        public void GoToLogNewRequestPage()
        {
            logger.Log(Status.Info, "Go to log New Request Page");
            LogNewRequest newRequest = new LogNewRequest(Driver, logger, Context);
            newRequest.Select();
        }
        public void GoToLogNewInternalPortalRequestPage()
        {
            CreateAPortalRequest request = new CreateAPortalRequest(Driver, logger, Context);
            request.Select();
        }

        public bool ClickMRODeliveryTab()
        {
            DebugUtil.DebugMessage(Driver.Scripts().ExecuteScript("return document.readyState").ToString());
            WebElementHelper helper = new WebElementHelper(Driver, logger, Context);
            DebugUtil.DebugMessage("Create MRO Delivery request");
            //check if tab is already selected
            if (helper.IsElementPresent(PageElements.LogNewRequestPage.mroDelSelectionTest_xpath))
                return true;

            Driver.DirectClick(By.XPath(PageElements.LogNewRequestPage.mroDelivery_xpath));
            Driver.Wait(TimeSpan.FromSeconds(2));
            return helper.IsElementPresent(PageElements.LogNewRequestPage.mroDelSelectionTest_xpath);
        }


        public string CreateNewMRODeliveryRequest(int nRequestPages = 2)
        {
            PatientFirstName = "FN" + createdDateTime;
            PatientLastName = "LN" + createdDateTime;
            // logger.Log(Status.Info, "Create a New MRO Delivery Request.");
            Driver.FindElement(By.Id(PageElements.LogNewRequestPage.firstName_Id)).SendKeys(PatientFirstName);
            logger.Log(Status.Info, "First Name entered." + PatientFirstName);
            Driver.FindElement(By.Id(PageElements.LogNewRequestPage.lastName_Id)).SendKeys(PatientLastName);
            logger.Log(Status.Info, "Last Name entered." + PatientLastName);
            Driver.FindElement(By.Id(PageElements.LogNewRequestPage.dateOfBith_Id)).SendKeys("1/1/2000");
            // logger.Log(Status.Info, "DOB Entered.");
            // DebugUtil.DebugMessage("Basic information added");

            var locationDropDown = Driver.FindElement(By.Id(PageElements.LogNewRequestPage.locationDropDown_id));
            locationDropDown.Click();
            // logger.Log(Status.Pass, "Location Selected To Boston Proper.");
            //Driver.Wait(TimeSpan.FromSeconds(2));

            //var locationItem = locationDropDown.FindElement(By.XPath("//div[@id='mrocontent_lstLocation_DropDown']/div/ul/li[text()='Boston Proper']"));

            var locationItem = Driver.FindElement(By.XPath("//div[@id='mrocontent_lstLocation_DropDown']/div/ul/li[text()='Boston Proper']"));
            //var locationItem = Driver.FindElement(By.XPath("//div[@id='mrocontent_lstLocation_DropDown']/div/ul/li[text()='MRO Automated Regression Test Location 1']"));
            locationItem.Click();
            DebugUtil.DebugMessage("Location selected");


            IgnoreDuplicates();
            WebElementHelper helper = new WebElementHelper(Driver, logger, Context);
            Driver.FindElement(By.Id("mrocontent_txtRequestRcvdDate")).SendKeys(DateTime.Now.ToShortDateString());
            helper.ScrollIntoView("mrocontent_cmdLogRequest", FindElementBy.Id);
            //Driver.Wait(TimeSpan.FromSeconds(5));
            Driver.FindElement(By.Id("mrocontent_cmdLogRequest")).Click();

            //ctrl _ t


            var win = AutoItX.WinWaitActive($"WebScan: Scan Documents for new request: {PatientLastName}, {PatientFirstName}", "", 10);
            AutoItX.WinActivate($"WebScan: Scan Documents for new request: {PatientLastName}, {PatientFirstName}", "");
            //Driver.Wait(TimeSpan.FromSeconds(5));
            ScannerUtil util = new ScannerUtil(Driver, logger);
            util.ScanDocuments(nRequestPages);
            logger.Log(Status.Pass, "Documents scanned.");
            string requestID = getRequestid();
            return requestID;
            //  DebugUtil.DebugMessage("Documents scanned");
        }

        public void CreateNewMRODeliveryRequest(string Location = "MRO Automated Regression Test Location 1")
        {
            try
            {
                Driver.SleepTheThread(3);
                ClickMRODeliveryTab();
                PatientFirstName = "FN" + createdDateTime;
                PatientLastName = "LN" + createdDateTime;
                IWebElement firstName = Driver.FindElementBy(By.Id(PageElements.LogNewRequestPage.firstName_Id));
                firstName.SendKeys(PatientFirstName);
                logger.Log(Status.Info, $"First Name entered ({PatientFirstName})");
                IWebElement lastName = Driver.FindElementBy(By.Id(PageElements.LogNewRequestPage.lastName_Id));
                lastName.SendKeys(PatientLastName);
                logger.Log(Status.Info, $"Last Name entered ({PatientLastName})");
                IWebElement dob = Driver.FindElementBy(By.Id(PageElements.LogNewRequestPage.dateOfBith_Id));
                dob.SendKeys(DateTime.Now.AddYears(-25).ToString("MM/dd/yyy"));
                var locationDropDown = Driver.FindElementBy(By.Id(PageElements.LogNewRequestPage.locationDropDown_id));
                locationDropDown.Click();
                Driver.SleepTheThread(2);
                Driver.SelectValueFromDD(drpLocation, Location);
                var todaysDate = String.Format("{0:M/dd/yyyy}", DateTime.Now).Replace("-", "/");
                var requestRecievedDate = Driver.FindElementBy(requestReceivedDate);
                requestRecievedDate.SendKeys(todaysDate);
                Driver.SleepTheThread(1);
                //Driver.SelectValueFromOptionsTypeDD(By.XPath("//select[@id='mrocontent_lstPurposeOfUse']"), "Other");
                WebElementHelper helper = new WebElementHelper(Driver, logger, Context);
                helper.ScrollIntoView("mrocontent_cmdLogRequest", FindElementBy.Id);
                Driver.Click(logRequestButton);

            }
            catch (Exception ex)
            {
                Driver.SwitchToAlert();
                throw new Exception($"Failed to create new MRO delivery request Exception details with Message : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");

            }
        }

        public bool ClickOnSiteDeliveryTab()
        {
            DebugUtil.DebugMessage(Driver.Scripts().ExecuteScript("return document.readyState").ToString());

            //   DebugUtil.DebugMessage("Create Onsite Delivery request");
            logger.Log(Status.Info, "Create Onsite Delivery request.");
            WebElementHelper helper = new WebElementHelper(Driver, logger, Context);
            //check if tab is already selected
            if (helper.IsElementPresent(PageElements.LogNewRequestPage.onsitedeliverySelectionTest_xpath))
                return true;

            Driver.FindElement(By.XPath(PageElements.LogNewRequestPage.onsitedelivery_xpath)).Click();
            Driver.Wait(TimeSpan.FromSeconds(2));
            return helper.IsElementPresent(PageElements.LogNewRequestPage.onsitedeliverySelectionTest_xpath);
        }

        public void CreateNewOnsiteDeliveryRequest()
        {
            //string FirstName = "FN" + createdDateTime;
            //string LastName = "LN" + createdDateTime;
            PatientFirstName = "FN" + createdDateTime;
            PatientLastName = "LN" + createdDateTime;
            Driver.FindElement(By.Id(PageElements.LogNewRequestPage.firstName_Id)).SendKeys(PatientFirstName);
            logger.Log(Status.Info, "First Name entered." + PatientFirstName);
            Driver.FindElement(By.Id(PageElements.LogNewRequestPage.lastName_Id)).SendKeys(PatientLastName);
            logger.Log(Status.Info, "Last Name entered." + PatientLastName);
            Driver.FindElement(By.Id(PageElements.LogNewRequestPage.dateOfBith_Id)).SendKeys("11/11/1990");
            logger.Log(Status.Info, "DOB Entered.");

            var locationDropDown = Driver.FindElement(By.Id(PageElements.LogNewRequestPage.locationDropDown_id));
            locationDropDown.Click();
            logger.Log(Status.Pass, "Location Selected.");
            Driver.Wait(TimeSpan.FromSeconds(2));
            var locationItem = locationDropDown.FindElement(By.XPath("//div[@id='mrocontent_lstLocation_DropDown']/div/ul/li[text()='MRO Automated Regression Test Location 1']"));
            locationItem.Click();
            // logger.Log(Status.Info, "Location. " + locationItem);
            Driver.Wait(TimeSpan.FromSeconds(2));
            IgnoreDuplicates();

            Driver.FindElement(By.Id(PageElements.LogNewRequestPage.requestRecievedDate_Id)).SendKeys(DateTime.Now.ToShortDateString());
            SelectElement oSelect = new SelectElement(Driver.FindElement(By.Id(PageElements.LogNewRequestPage.requesterType_Id)));
            // oSelect.SelectByText("Patient"); Provider
            oSelect.SelectByText("Provider");
            logger.Log(Status.Pass, "Requester Type Selected To Provider.");

            Driver.FindElement(By.Id(PageElements.LogNewRequestPage.logRequestBtn_Id)).Click();
            var win = AutoItX.WinWaitActive($"WebScan: Scan Documents for new request: {PatientLastName}, {PatientFirstName}", "", 10);
            AutoItX.WinActivate($"WebScan: Scan Documents for new request: {PatientLastName}, {PatientFirstName}", "");
            Driver.Wait(TimeSpan.FromSeconds(5));
            ScannerUtil util = new ScannerUtil(Driver, logger);
            util.ScanDocuments();
            logger.Log(Status.Pass, "Documents scanned.");
        }

        public bool ClickInternalPortalTab()
        {
            DebugUtil.DebugMessage(Driver.Scripts().ExecuteScript("return document.readyState").ToString());

            DebugUtil.DebugMessage("Create Internal Portal request");
            WebElementHelper helper = new WebElementHelper(Driver, logger, Context);
            //check if tab is already selected
            if (helper.IsElementPresent(PageElements.LogNewRequestPage.internalportalSelectionTest_xpath))
                return true;

            Driver.FindElement(By.XPath(PageElements.LogNewRequestPage.internalportalbtn_xpath)).Click();
            Driver.Wait(TimeSpan.FromSeconds(2));
            return helper.IsElementPresent(PageElements.LogNewRequestPage.internalportalSelectionTest_xpath);
        }

        public void CreateNewInternalPortalRequest()
        {
            //string FirstName = "FN" + createdDateTime;
            //string LastName = "LN" + createdDateTime;
            PatientFirstName = "FN" + createdDateTime;
            PatientLastName = "LN" + createdDateTime;
            var firstNameText = Driver.FindElement(By.Id(PageElements.LogNewRequestPage.firstName_Id));
            firstNameText.SendKeys(PatientFirstName);

            var lastNameText = Driver.FindElement(By.Id(PageElements.LogNewRequestPage.lastName_Id));
            lastNameText.SendKeys(PatientLastName);

            var dob = Driver.FindElement(By.Id(PageElements.LogNewRequestPage.dateOfBith_Id));
            dob.SendKeys("11/11/1990");

            var locationDropDown = Driver.FindElement(By.Id(PageElements.LogNewRequestPage.locationDropDown_id));
            locationDropDown.Click();
            logger.Log(Status.Pass, "Location Selected To Boston Proper.");
            Driver.Wait(TimeSpan.FromSeconds(2));

            var locationItem = locationDropDown.FindElement(By.XPath("//div[@id='mrocontent_lstLocation_DropDown']/div/ul/li[text()='Boston Proper']"));
            locationItem.Click();
            IgnoreDuplicates();
            var requestRecievedDate = Driver.FindElement(By.Id("mrocontent_txtRequestRcvdDate"));
            requestRecievedDate.SendKeys(DateTime.Now.ToShortDateString());

            var internalPortal = Driver.FindElement(By.Id("mrocontent_lstInternal"));

            SelectElement oSelect = new SelectElement(Driver.FindElement(By.Id("mrocontent_lstInternal")));
            oSelect.SelectByText("Business Office");

            Driver.Wait(TimeSpan.FromSeconds(2));

            var portalUserName = Driver.FindElement(By.Id("mrocontent_lstPortal"));

            SelectElement oSelect1 = new SelectElement(Driver.FindElement(By.Id("mrocontent_lstPortal")));
            oSelect1.SelectByText("Office, Business");
            Driver.Wait(TimeSpan.FromSeconds(5));
            WebElementHelper helper = new WebElementHelper(Driver, logger, Context);
            helper.ScrollIntoView("mrocontent_cmdLogRequest", FindElementBy.Id);
            Driver.Wait(TimeSpan.FromSeconds(5));
            var logRequestButton = Driver.FindElement(By.Id("mrocontent_cmdLogRequest"));
            logRequestButton.Click();

            var win = AutoItX.WinWaitActive($"WebScan: Scan Documents for new request: {PatientLastName}, {PatientFirstName}", "", 10);
            AutoItX.WinActivate($"WebScan: Scan Documents for new request: {PatientLastName}, {PatientFirstName}", "");
            Driver.Wait(TimeSpan.FromSeconds(5));
            ScannerUtil util = new ScannerUtil(Driver, logger);
            util.ScanDocuments();
            logger.Log(Status.Pass, "Documents scanned.");
            DebugUtil.DebugMessage("Documents scanned");
        }

        public void IgnoreDuplicates()
        {
            WebElementHelper helper = new WebElementHelper(Driver, logger, Context);
            if (!helper.IsElementMissing(By.Id(PageElements.LogNewRequestPage.ignoreDuplicatesChk_Id), 5))
            {
                if (Driver.FindElements(By.Id(PageElements.LogNewRequestPage.ignoreDuplicatesChk_Id)).Count != 0)
                {
                    Driver.FindElement(By.Id(PageElements.LogNewRequestPage.ignoreDuplicatesChk_Id)).Click();
                }
            }
        }

        public void GoToRequestStatusPage()
        {
            //Driver.Wait(TimeSpan.FromSeconds(5));
            Driver.FindElement(By.Id(PageElements.LogNewRequestPage.requestStatusButton_Id)).Click();
        }


        public void CreateRequest()

        {
            var facilMroDelivery = Driver.FindElement(By.XPath(PageElements.LogNewRequestPage.mroDelivery_xpath));
            facilMroDelivery.Click();

            //Driver.Wait(TimeSpan.FromSeconds(2));
            dateTime = DateTime.Now.ToString();
            var firstNameText = Driver.FindElement(By.Id(PageElements.LogNewRequestPage.firstName_Id));
            firstNameText.SendKeys("Fn" + dateTime);

            var lastNameText = Driver.FindElement(By.Id(PageElements.LogNewRequestPage.lastName_Id));
            lastNameText.SendKeys("Ln" + dateTime);

            var dob = Driver.FindElement(By.Id(PageElements.LogNewRequestPage.dateOfBith_Id));
            dob.SendKeys("11/11/1990");

            var locationDropDown = Driver.FindElement(By.Id(PageElements.LogNewRequestPage.locationDropDown_id));
            locationDropDown.Click();
            //Driver.Wait(TimeSpan.FromSeconds(2));

            var locationItem = locationDropDown.FindElement(By.XPath("//div[@id='mrocontent_lstLocation_DropDown']/div/ul/li[text()='Boston Proper']"));
            locationItem.Click();

            var requestRecievedDate = Driver.FindElement(By.Id("mrocontent_txtRequestRcvdDate"));
            requestRecievedDate.SendKeys(DateTime.Now.ToShortDateString());

            var logRequestButton = Driver.FindElement(By.Id("mrocontent_cmdLogRequest"));
            logRequestButton.Click();
            //Driver.Wait(TimeSpan.FromSeconds(5));
        }


        // Facility Abbreviation Facil
        public void FacilOsiteDeliveryCreateNewRequest()
        {
            var facilOsiteDelivery = Driver.FindElement(By.XPath(PageElements.LogNewRequestPage.onsitedelivery_xpath));
            facilOsiteDelivery.Click();

            var firstNameText = Driver.FindElement(By.Id(PageElements.LogNewRequestPage.firstName_Id));
            firstNameText.SendKeys("Fn" + DateTime.Now.ToString());

            var lastNameText = Driver.FindElement(By.Id(PageElements.LogNewRequestPage.lastName_Id));
            lastNameText.SendKeys("Ln" + DateTime.Now.ToString());

            var dob = Driver.FindElement(By.Id(PageElements.LogNewRequestPage.dateOfBith_Id));
            dob.SendKeys("11/11/1990");

            var locationDropDown = Driver.FindElement(By.Id(PageElements.LogNewRequestPage.locationDropDown_id));
            locationDropDown.Click();
            Driver.Wait(TimeSpan.FromSeconds(2));

            var locationItem = locationDropDown.FindElement(By.XPath("//div[@id='mrocontent_lstLocation_DropDown']/div/ul/li[text()='Boston Proper']"));
            locationItem.Click();

            var requestRecievedDate = Driver.FindElement(By.Id("mrocontent_txtRequestRcvdDate"));
            requestRecievedDate.SendKeys(DateTime.Now.ToShortDateString());

            var selectReqType = Driver.FindElement(By.Id("mrocontent_lstOSDRequesterType"));

            SelectElement oSelect = new SelectElement(Driver.FindElement(By.Id("mrocontent_lstOSDRequesterType")));
            oSelect.SelectByText("Patient");

            var logRequestButton = Driver.FindElement(By.Id("mrocontent_cmdLogRequest"));
            logRequestButton.Click();
            Driver.Wait(TimeSpan.FromSeconds(5));

        }

        //  Facility Abbreviation Facil and Internal abbreviation is intl this methos is for facility side internalPortal.  Portal abbreviation is prtl
        public void FacilIntlPrtlCreateNewRequest()
        {
            var facilIntlPrtlDelivery = Driver.FindElement(By.XPath(PageElements.LogNewRequestPage.internalportalbtn_xpath));
            facilIntlPrtlDelivery.Click();

            dateTime = DateTime.Now.ToString();
            var firstNameText = Driver.FindElement(By.Id(PageElements.LogNewRequestPage.firstName_Id));
            firstNameText.SendKeys("Fn" + dateTime);

            var lastNameText = Driver.FindElement(By.Id(PageElements.LogNewRequestPage.lastName_Id));
            lastNameText.SendKeys("Ln" + dateTime);

            var dob = Driver.FindElement(By.Id(PageElements.LogNewRequestPage.dateOfBith_Id));
            dob.SendKeys("11/11/1990");

            var locationDropDown = Driver.FindElement(By.Id(PageElements.LogNewRequestPage.locationDropDown_id));
            locationDropDown.Click();
            Driver.Wait(TimeSpan.FromSeconds(2));

            var locationItem = locationDropDown.FindElement(By.XPath("//div[@id='mrocontent_lstLocation_DropDown']/div/ul/li[text()='Boston Proper']"));
            locationItem.Click();

            var requestRecievedDate = Driver.FindElement(By.Id("mrocontent_txtRequestRcvdDate"));
            requestRecievedDate.SendKeys(DateTime.Now.ToShortDateString());

            var internalPortal = Driver.FindElement(By.Id("mrocontent_lstInternal"));

            SelectElement oSelect = new SelectElement(Driver.FindElement(By.Id("mrocontent_lstInternal")));
            oSelect.SelectByText("Business Office");

            Driver.Wait(TimeSpan.FromSeconds(2));

            var portalUserName = Driver.FindElement(By.Id("mrocontent_lstPortal"));

            SelectElement oSelect1 = new SelectElement(Driver.FindElement(By.Id("mrocontent_lstPortal")));
            oSelect1.SelectByText("Office, Business");

            Driver.Wait(TimeSpan.FromSeconds(2));

            var logRequestButton = Driver.FindElement(By.Id("mrocontent_cmdLogRequest"));
            logRequestButton.Click();
            Driver.Wait(TimeSpan.FromSeconds(2));

        }

        public string IntPortalCreateAPortalRequest()
        {
            Driver.Wait(TimeSpan.FromSeconds(2));
            PatientFirstName = "FN" + createdDateTime;
            PatientLastName = "LN" + createdDateTime;
            Driver.FindElement(By.Id(PageElements.LogNewRequestPage.firstName_Id)).SendKeys(PatientFirstName);
            Driver.FindElement(By.Id(PageElements.LogNewRequestPage.lastName_Id)).SendKeys(PatientLastName);
            Driver.FindElement(By.Id(PageElements.LogNewRequestPage.dateOfBith_Id)).SendKeys("11/11/1990");
            //  var locationDropDown = Driver.FindElement(By.Id(PageElements.LogNewRequestPage.locationDropDown_id));
            //  locationDropDown.Click();
            Driver.Wait(TimeSpan.FromSeconds(2));
            Driver.FindElement(By.Id(PageElements.LogNewRequestPage.intlocation_id)).Click();

            Driver.Wait(TimeSpan.FromSeconds(2));
            SelectElement oSelect = new SelectElement(Driver.FindElement(By.Id(PageElements.LogNewRequestPage.intlocation_id)));
            oSelect.SelectByText("Boston Proper");
            IgnoreDuplicates();
            Driver.Wait(TimeSpan.FromSeconds(2));
            Driver.FindElement(By.Id(PageElements.LogNewRequestPage.inttextnotes_id)).SendKeys("11/11/1990");
            Driver.FindElement(By.Id(PageElements.LogNewRequestPage.intcreaterequest_id)).Click();
            Driver.Wait(TimeSpan.FromSeconds(5));
            IgnoreDuplicates();
            return Driver.FindElement(By.XPath(PageElements.ROIRequesterPortal.intportassertion_xpath)).Text;

        }
        public string ExtPortalRequestRecordst()
        {
            Driver.Wait(TimeSpan.FromSeconds(2));
            Driver.FindElement(By.XPath("//*[text()='Request Records']")).Click();
            Driver.Wait(TimeSpan.FromSeconds(2));
            Driver.FindElement(By.Id(PageElements.ROIRequesterPortal.recentlyusedfacility_id)).Click();
            Driver.Wait(TimeSpan.FromSeconds(6));
            return Driver.FindElement(By.XPath(PageElements.ROIRequesterPortal.mroassertions_xpath)).Text;

        }

        public void ExtPortalFindARequest()

        {
            var lastNameText = Driver.FindElement(By.Id(PageElements.LogNewRequestPage.firstName_Id));
            lastNameText.SendKeys("Test");
            logger.Log(Status.Info, "Successfully Type Test.");
            // logger.Log(Status.Pass, "Web Driver closed successfully.");
            Driver.FindElement(By.Id(PageElements.ROIRequesterPortal.roiReqeusterPortalSearchBtn_Id)).Click();
            Driver.Wait(TimeSpan.FromSeconds(5));
            Driver.FindElement(By.XPath("//*[@title='View Request']")).Click(); //PageElements.roiRequesterPortal.extportalpatientname_xpath)).Click();

        }

        public string ExtPortalRequestRecordst2()
        {
            var recusedfaci = Driver.FindElement(By.XPath("//div[@id='mrocontent_RadComboBox_RecentFacilities_DropDown']/div/ul/li[text()='ROI Test Facility - Boston Proper']"));
            recusedfaci.Click();
            Driver.FindElement(By.Id(PageElements.ROIRequesterPortal.extportnextbtn_id)).Click();
            Driver.FindElement(By.Id(PageElements.ROIRequesterPortal.extrequestedby_id)).SendKeys("Test RAC B (#62810)");
            Driver.Wait(TimeSpan.FromSeconds(10));

            //  Driver.FindElement(By.Id(PageElements.roiRequesterPortal.extportbrowsebtn_id)).SendKeys("C:\\Users\\tmirza\\Desktop\\C1\\PDFFOLDER\\Testingsample1.pdf");
            Driver.FindElement(By.Id(PageElements.ROIRequesterPortal.extportbrowsebtn_id)).SendKeys("C:\\TestDocs\\Test 10 Pages.pdf");
            PatientFirstName = "FN" + createdDateTime;
            PatientLastName = "LN" + createdDateTime;
            Driver.FindElement(By.Id(PageElements.ROIRequesterPortal.mrocontent_txtBxLastName_id)).SendKeys(PatientFirstName);
            Driver.FindElement(By.Id(PageElements.ROIRequesterPortal.mrocontent_txtBxFirstName_id)).SendKeys(PatientLastName);
            Driver.FindElement(By.Id(PageElements.ROIRequesterPortal.mrocontent_txtBxDOB_id)).SendKeys("01/09/1991");
            Driver.FindElement(By.Id(PageElements.ROIRequesterPortal.mrocontent_rbAnyOrAllRadioBtn_id)).Click();
            IgnoreDuplicates();
            Driver.FindElement(By.Id(PageElements.ROIRequesterPortal.mrocontent_btnLogRequestBtn_id)).Click();

            Driver.Wait(TimeSpan.FromSeconds(1));
            WebElementHelper webElementHelper = new WebElementHelper(Driver, logger, Context);
            webElementHelper.Click_Enter();

            Driver.FindElement(By.Id(PageElements.ROIRequesterPortal.mrocontent_cmdRequestStatusBtn_id)).Click();

            return Driver.FindElement(By.XPath(PageElements.ROIRequesterPortal.extportalrequestid_xpath)).Text;
            // debug removed from line 180 to test further.
        }
        public void extportgetRequestid()
        {

            string extportreqid = Driver.FindElement(By.XPath(PageElements.ROIRequesterPortal.extportalrequestid_xpath)).Text;
            Console.Write(extportreqid);


        }

        public void extportbrowsepdf()
        {
            //  Driver.Wait(TimeSpan.FromSeconds(2));
            // Driver.FindElement(By.Id(PageElements.roiRequesterPortal.extportbrowsebtn_id)).Click();
        }




        public string getRequestid()
        {
            string reqId = string.Empty;
            try
            {
                 reqId = Driver.FindElement(By.Id(PageElements.LogNewRequestPage.mrocontenttdRequestID_id)).Text;
                //Console.Write(reqid);
                
            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to get request id Exception details with Message : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
                
            }
            return reqId;

        }
        public void sendReqId()
        {
            Driver.Wait(TimeSpan.FromSeconds(5));
            //   var lastNameText = Driver.FindElement(By.Id(PageElements.LogNewRequestPage.lastName_Id));
            //   lastNameText.SendKeys("Ln" + dateTime);
        }


        public void RequestStatus()
        {

            Driver.Wait(TimeSpan.FromSeconds(5));
            var requestStatusButton = Driver.FindElement(By.Id(PageElements.LogNewRequestPage.requestStatusButton_Id));
            requestStatusButton.Click();

        }



        public void RequestPreAuthorization()
        {
            Driver.FindElement(By.Id(PageElements.LogNewRequestPage.requestpreauth_id)).Click();
            Debug.WriteLine("Request Pre-Authorization Button Clicked");
            Driver.Wait(TimeSpan.FromSeconds(5));
        }

        public void ScanPatientPages()
        {
            click_action();
            Debug.WriteLine("scan button clicked");
        }

        private IWebElement getElement(string v)
        {
            return Driver.FindElement(By.Id(v));

        }

        private void click_action()
        {
            var btn = Driver.FindElement(By.Id(PageElements.LogNewRequestPage.scanButton_Id));
            Actions action = new Actions(Driver);
            action.Click(btn).Build().Perform();
        }


        public void ReleaseRequest()
        {
            Driver.SwitchTo().Frame("radWndPrompt");
            Driver.FindElement(By.Id("rbYes")).Click();

            Driver.FindElement(By.Id(PageElements.LogNewRequestPage.btnrelease_id)).Click();

            // Driver.SwitchTo().Alert().Accept();
            // Driver.SwitchTo().DefaultContent();
        }

        public void acceptalert()
        {
            Driver.SwitchTo().Alert().Accept();
        }

        public void PatientNameValidation()
        {

            //Pending needs to implement assert validation.
            string patientname = Driver.FindElement(By.Id("mrocontent_tdPatientName")).Text;
            // test.info("Request ID: " + reqeustid);
            Console.WriteLine(PatientFirstName + " " + PatientLastName);
            Console.WriteLine(patientname);

            if (patientname.Equals(PatientFirstName + " " + PatientLastName))
            {
                logger.Log(Status.Pass, "patient name is equal");

            }
            else
            {
                logger.Log(Status.Fail, "patient name is not equal");


            }
            string dobvalidation = Driver.FindElement(By.Id(PageElements.LogNewRequestPage.dobvalidation_id)).Text;
            Console.WriteLine(dobvalidation);
            if (dobvalidation.Equals("11/11/1990"))
            {
                Console.WriteLine("date of birth passed");
            }
            else
            {
                Console.WriteLine("date of birth failed");

            }

            string deliverymethod = Driver.FindElement(By.Id(PageElements.LogNewRequestPage.dlvymethod_id)).Text;
            Console.WriteLine(deliverymethod);

            if (deliverymethod.Equals(""))
            {

            }

            Driver.Wait(TimeSpan.FromSeconds(5));
        }

        public void DeliverMedicalRecordOnsite()
        {
            var dob = Driver.FindElement(By.Id(PageElements.LogNewRequestPage.requesterfax_id));
            dob.SendKeys("555-555-5555");
            var stroredonpaper = Driver.FindElement(By.Id(PageElements.LogNewRequestPage.storedonpaper_id));
            stroredonpaper.SendKeys("2");
            var storedelectronically = Driver.FindElement(By.Id(PageElements.LogNewRequestPage.storedelectronically_id));
            storedelectronically.SendKeys("2");
            var creaeinvoicebtn = Driver.FindElement(By.Id(PageElements.LogNewRequestPage.createinvoiceButton_id));
            creaeinvoicebtn.Click();
            string balanceDue = Driver.FindElement(By.XPath(PageElements.LogNewRequestPage.balancedue_xpath)).Text;
            Driver.Wait(TimeSpan.FromSeconds(5));
            Driver.FindElement(By.XPath(PageElements.LogNewRequestPage.cashcheck_xpath)).Click();
            Driver.Wait(TimeSpan.FromSeconds(5));
            Driver.FindElement(By.Id(PageElements.LogNewRequestPage.amount_id)).SendKeys(balanceDue);
            Driver.FindElement(By.XPath(PageElements.LogNewRequestPage.amountuncheck_xpath)).Click();
            Driver.Wait(TimeSpan.FromSeconds(5));
            Driver.FindElement(By.XPath(PageElements.LogNewRequestPage.sendfax_xpath)).Click();
            Driver.Wait(TimeSpan.FromSeconds(5));
            // Driver.FindElement(By.XPath(PageElements.LogNewRequestPage.logoutbutton_xpath)).Click();
            Driver.FindElement(By.Id(PageElements.LogNewRequestPage.facillogoutbtn_id)).Click();
        }
        public void Dlvfaxonsite()
        {
            Driver.FindElement(By.Id(PageElements.FacilityRequestStatusPage.deliverfaxosdnow_id)).Click();
            Driver.Wait(TimeSpan.FromSeconds(5));
            Driver.FindElement(By.XPath(PageElements.LogNewRequestPage.dlvymethod_xpath)).Click();
            Driver.Wait(TimeSpan.FromSeconds(2));
            var stroredonpaper = Driver.FindElement(By.Id(PageElements.LogNewRequestPage.storedonpaper_id));
            stroredonpaper.SendKeys("10");
            var storedelectronically = Driver.FindElement(By.Id(PageElements.LogNewRequestPage.storedelectronically_id));
            storedelectronically.SendKeys("10");
            var creaeinvoicebtn = Driver.FindElement(By.Id(PageElements.LogNewRequestPage.createinvoiceButton_id));
            creaeinvoicebtn.Click();
            string balanceDue = Driver.FindElement(By.XPath(PageElements.LogNewRequestPage.balancedue_xpath)).Text;
            Driver.Wait(TimeSpan.FromSeconds(5));
            Driver.FindElement(By.XPath(PageElements.LogNewRequestPage.cashcheck_xpath)).Click();
            Driver.Wait(TimeSpan.FromSeconds(5));
            Driver.FindElement(By.Id(PageElements.LogNewRequestPage.amount_id)).SendKeys(balanceDue);
            Driver.FindElement(By.XPath(PageElements.LogNewRequestPage.amountuncheck_xpath)).Click();
            Driver.Wait(TimeSpan.FromSeconds(5));
            Driver.FindElement(By.XPath(PageElements.LogNewRequestPage.osddlvrecords_xpath)).Click();

        }

        public void SendEnterKey()
        {
            Debug.WriteLine("Accept the scan");
            Driver.Wait(TimeSpan.FromSeconds(10));
            // InputSimulator simulator = new InputSimulator();
            //simulator.Keyboard.KeyPress(VirtualKeyCode.RETURN);
            //  Driver.Wait(TimeSpan.FromSeconds(5));
            //       VK_ENTER);
            //    simulator.Keyboard.ModifiedKeyStroke(VirtualKeyCode.CONTROL, VirtualKeyCode.VK_T);

            Driver.Wait(TimeSpan.FromSeconds(15));

            //      simulator.Keyboard.ModifiedKeyStroke(VirtualKeyCode.CONTROL, VirtualKeyCode.VK_A);

        }

        public void facillogoutbutton()
        {
            //Driver.Wait(TimeSpan.FromSeconds(5));
            var loutbutton = Driver.FindElement(By.Id(PageElements.LogNewRequestPage.facillogoutbtn_id));
            loutbutton.Click();

        }

        public void extportallogoutbtn()
        {
            Driver.Wait(TimeSpan.FromSeconds(5));
            var extportallogoutbtn = Driver.FindElement(By.Id(PageElements.ROIRequesterPortal.extportalLogoutbtn_xpath));
            extportallogoutbtn.Click();

        }
        public void mroToOnsite()
        {
            string deliverymethod = Driver.FindElement(By.Id("mrocontent_lblRequestType")).Text;
            Console.WriteLine("Current Delivery Method Set To #" + deliverymethod);
            logger.Pass("Current Delivery Method Set To: " + deliverymethod);
            // logger.Log(Status.Pass, "Sucessfully Logged Out From Facility.");
            Driver.Wait(TimeSpan.FromSeconds(2));
            Driver.FindElement(By.Id(PageElements.LogNewRequestPage.changedlvymethod_id)).Click();
            Driver.Wait(TimeSpan.FromSeconds(2));
            SelectElement objSelect = new SelectElement(Driver.FindElement(By.Id("mrocontent_lstChanges")));
            objSelect.SelectByText("Change to On-Site");
            Driver.Wait(TimeSpan.FromSeconds(5));
            string deliverymethod1 = Driver.FindElement(By.Id("mrocontent_lblRequestType")).Text;
            Console.WriteLine("Check Delivery Type Change from MRO To # " + deliverymethod1);
            logger.Pass("Successfully Change Delivery Method From To: " + deliverymethod1);
            Driver.Wait(TimeSpan.FromSeconds(5));
            Driver.FindElement(By.Id(PageElements.LogNewRequestPage.sendmsgmrobtn_id)).Click();
            Driver.Wait(TimeSpan.FromSeconds(2));
            SelectElement objSelect1 = new SelectElement(Driver.FindElement(By.Id("mrocontent_lstActions")));
            objSelect1.SelectByText("No Authorization Required");
            logger.Pass("Selected No Authorization Required From Action Messages");
            Driver.FindElement(By.Id(PageElements.LogNewRequestPage.sendmsgmrotxt_id)).SendKeys("Test Message to MRO");
            logger.Pass("Successfully send a test message to MRO");
            Driver.Wait(TimeSpan.FromSeconds(2));

            //  Assert.assertTrue(objSelect == objSelect);

        }

        public void ReqPreAuthChngDlvymroToOnsite()
        {
            string deliverymethod = Driver.FindElement(By.Id("mrocontent_lblRequestType")).Text;
            Console.WriteLine("Current Delivery Method Set To #" + deliverymethod);
            // logger.Log(Status.Pass, "Sucessfully Logged Out From Facility.");
            Driver.Wait(TimeSpan.FromSeconds(2));
            Driver.FindElement(By.Id(PageElements.LogNewRequestPage.changedlvymethod_id)).Click();
            Driver.Wait(TimeSpan.FromSeconds(2));
            SelectElement objSelect = new SelectElement(Driver.FindElement(By.Id("mrocontent_lstChanges")));
            objSelect.SelectByText("Change to On-Site");
            Driver.Wait(TimeSpan.FromSeconds(5));
            string deliverymethod1 = Driver.FindElement(By.Id("mrocontent_lblRequestType")).Text;
            Console.WriteLine("Check Delivery Type Change from MRO To # " + deliverymethod1);
            Driver.Wait(TimeSpan.FromSeconds(5));
            //  Assert.assertTrue(objSelect == objSelect);

        }
        public void IntPortalReqPreAuthChngDlvyIntToMRO()
        {
            string deliverymethod = Driver.FindElement(By.Id("mrocontent_lblRequestType")).Text;
            Console.WriteLine("Current Delivery Method Set To #" + deliverymethod);
            // logger.Log(Status.Pass, "Sucessfully Logged Out From Facility.");
            Driver.Wait(TimeSpan.FromSeconds(2));
            Driver.FindElement(By.Id(PageElements.LogNewRequestPage.changedlvymethod_id)).Click();
            Driver.Wait(TimeSpan.FromSeconds(2));
            SelectElement objSelect = new SelectElement(Driver.FindElement(By.Id("mrocontent_lstChanges")));
            objSelect.SelectByText("Change to MRO Delivery");
            Driver.Wait(TimeSpan.FromSeconds(5));
            string deliverymethod1 = Driver.FindElement(By.Id("mrocontent_lblRequestType")).Text;
            Console.WriteLine("Check Delivery Type Change from Internal To # " + deliverymethod1);
            Driver.Wait(TimeSpan.FromSeconds(5));
            //  Assert.assertTrue(objSelect == objSelect);

        }

        public string IronMountainCreateNewMRODeliveryRequest()
        {
            PatientFirstName = "FN" + createdDateTime;
            PatientLastName = "LN" + createdDateTime;
            logger.Log(Status.Info, "Create A New MRO Delivery Request.");
            Driver.FindElement(By.Id(PageElements.LogNewRequestPage.firstName_Id)).SendKeys(PatientFirstName);
            logger.Log(Status.Info, "First Name entered." + PatientFirstName);
            Driver.FindElement(By.Id(PageElements.LogNewRequestPage.lastName_Id)).SendKeys(PatientLastName);
            logger.Log(Status.Info, "Last Name entered." + PatientLastName);
            Driver.FindElement(By.Id(PageElements.LogNewRequestPage.dateOfBith_Id)).SendKeys("11/11/1990");
            // logger.Log(Status.Info, "DOB Entered.");
            // DebugUtil.DebugMessage("Basic information added");

            var locationDropDown = Driver.FindElement(By.Id(PageElements.LogNewRequestPage.locationDropDown_id));
            locationDropDown.Click();
            logger.Log(Status.Pass, "Location Selected To: Doctors Who Care Medical Group.");
            Driver.Wait(TimeSpan.FromSeconds(2));

            var locationItem = locationDropDown.FindElement(By.XPath("//div[@id='mrocontent_lstLocation_DropDown']/div/ul/li[text()='Doctors Who Care Medical Group']"));
            locationItem.Click();
            DebugUtil.DebugMessage("Location selected");


            IgnoreDuplicates();
            WebElementHelper helper = new WebElementHelper(Driver, logger, Context);
            Driver.FindElement(By.Id("mrocontent_txtRequestRcvdDate")).SendKeys(DateTime.Now.ToShortDateString());
            helper.ScrollIntoView("mrocontent_cmdLogRequest", FindElementBy.Id);
            Driver.Wait(TimeSpan.FromSeconds(5));
            Driver.FindElement(By.Id("mrocontent_cmdLogRequest")).Click();
            string requestID = getRequestid();
            var win = AutoItX.WinWaitActive($"WebScan: Scan Documents for new request: {PatientLastName}, {PatientFirstName}", "", 10);
            AutoItX.WinActivate($"WebScan: Scan Documents for new request: {PatientLastName}, {PatientFirstName}", "");
            Driver.Wait(TimeSpan.FromSeconds(5));
            ScannerUtil util = new ScannerUtil(Driver, logger);
            util.ScanDocuments();
            logger.Log(Status.Pass, "Documents scanned.");
            return requestID;
            //  DebugUtil.DebugMessage("Documents scanned");
        }

        public void IronMountainCreateNewOnsiteDeliveryRequest()
        {
            //string FirstName = "FN" + createdDateTime;
            //string LastName = "LN" + createdDateTime;
            PatientFirstName = "FN" + createdDateTime;
            PatientLastName = "LN" + createdDateTime;
            Driver.FindElement(By.Id(PageElements.LogNewRequestPage.firstName_Id)).SendKeys(PatientFirstName);
            logger.Log(Status.Info, "First Name entered." + PatientFirstName);
            Driver.FindElement(By.Id(PageElements.LogNewRequestPage.lastName_Id)).SendKeys(PatientLastName);
            logger.Log(Status.Info, "Last Name entered." + PatientLastName);
            Driver.FindElement(By.Id(PageElements.LogNewRequestPage.dateOfBith_Id)).SendKeys("11/11/1990");
            logger.Log(Status.Info, "DOB Entered.");

            var locationDropDown = Driver.FindElement(By.Id(PageElements.LogNewRequestPage.locationDropDown_id));
            locationDropDown.Click();
            logger.Log(Status.Pass, "Location Selected To: Doctors Who Care Medical Group.");
            Driver.Wait(TimeSpan.FromSeconds(2));

            var locationItem = locationDropDown.FindElement(By.XPath("//div[@id='mrocontent_lstLocation_DropDown']/div/ul/li[text()='Doctors Who Care Medical Group']"));
            locationItem.Click();
            DebugUtil.DebugMessage("Location selected");
            IgnoreDuplicates();
            Driver.FindElement(By.Id(PageElements.LogNewRequestPage.requestRecievedDate_Id)).SendKeys(DateTime.Now.ToShortDateString());
            SelectElement oSelect = new SelectElement(Driver.FindElement(By.Id("mrocontent_lstOSDNames")));
            // oSelect.SelectByText("Patient"); Provider
            oSelect.SelectByText("Dr Smith Cardiology");
            logger.Log(Status.Info, "Select Dr. Smith Cardiology.");
            Driver.Wait(TimeSpan.FromSeconds(10));

            Driver.FindElement(By.Id(PageElements.LogNewRequestPage.logRequestBtn_Id)).Click();
            var win = AutoItX.WinWaitActive($"WebScan: Scan Documents for new request: {PatientLastName}, {PatientFirstName}", "", 10);
            AutoItX.WinActivate($"WebScan: Scan Documents for new request: {PatientLastName}, {PatientFirstName}", "");
            Driver.Wait(TimeSpan.FromSeconds(5));
            ScannerUtil util = new ScannerUtil(Driver, logger);
            util.ScanDocuments();
            logger.Log(Status.Pass, "Documents scanned.");
        }

        public void CreateNewMRODeliveryRequestWithFNAndLN()
        {
            try
            {
                Driver.SleepTheThread(3);
                ClickMRODeliveryTab();
                PatientFirstName = "TERTBEL";
                PatientLastName = "ABEZNA";
                IWebElement firstName = Driver.FindElementBy(By.Id(PageElements.LogNewRequestPage.firstName_Id));
                firstName.SendKeys(PatientFirstName);
                logger.Log(Status.Info, $"First Name entered ({PatientFirstName})");
                IWebElement lastName = Driver.FindElementBy(By.Id(PageElements.LogNewRequestPage.lastName_Id));
                lastName.SendKeys(PatientLastName);
                logger.Log(Status.Info, $"Last Name entered ({PatientLastName})");
                var locationDropDown = Driver.FindElementBy(By.Id(PageElements.LogNewRequestPage.locationDropDown_id));
                locationDropDown.SendKeys("Boston Proper");
                IgnoreDuplicates();
                IWebElement dob = Driver.FindElementBy(By.Id(PageElements.LogNewRequestPage.dateOfBith_Id));
                dob.SendKeys("2/26/1948");
                var todaysDate = String.Format("{0:M/dd/yyyy}", DateTime.Now).Replace("-", "/");
                var requestRecievedDate = Driver.FindElementBy(requestReceivedDate);
                requestRecievedDate.SendKeys(todaysDate);
                WebElementHelper helper = new WebElementHelper(Driver, logger, Context);
                helper.ScrollIntoView("mrocontent_cmdLogRequest", FindElementBy.Id);
                Driver.Click(logRequestButton);
                bool isPresent = Driver.IsAlertPresent();
                if (isPresent)
                {
                    string alert = Driver.SwitchTo().Alert().Text;
                    logger.Log(Status.Info,$"Alert with text {alert} is showing");
                    Driver.AcceptAlert();
                    IgnoreDuplicates();
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to create new MRO delivery request Exception details with Message : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");

            }
        }

        public string GetMRNNumber()
        {
            try
            {

                string mrnNumber = Driver.GetAttributeValue(MRN, "value");
                return mrnNumber;
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to get MRN number with Message : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");

            }
        }

        public void ClickMroDelivry()
        {
            try
            {
                Driver.DirectClick(By.XPath(PageElements.LogNewRequestPage.mroDelivery_xpath));
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to click on MRO Delivery button : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");

            }
        }
    }
}
