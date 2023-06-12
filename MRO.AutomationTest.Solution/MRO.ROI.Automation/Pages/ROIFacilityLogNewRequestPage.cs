using AventStack.ExtentReports;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MRO.ROI.Automation.Common;
using MRO.ROI.Automation.Common.Navigation;
using MRO.ROI.Automation.Selenium;
using MRO.ROI.Automation.Utility;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Reflection;
using static MRO.ROI.Automation.Utility.IniFile;

namespace MRO.ROI.Automation.Pages
{
    public class ROIFacilityLogNewRequestPage
    {

        public RemoteWebDriver Driver;
        public ExtentTest logger;
        public TestContext Context;
        public CSVReader csvReader;
        public ROIFacilityLogNewRequestPage(RemoteWebDriver driver, ExtentTest _loger, TestContext _context)
        {
            Driver = driver;
            logger = _loger;
            Context = _context;
        }

        public const string dukeStagingLocationValue = "_Duke Stage Testing";
        private  string createdDateTime = DateTime.Now.ToString("dd MMMM yyyy hh:mm:ss");
        public  string PatientFirstName { get; private set; }
        public  string PatientLastName { get; private set; }
        public const string importPDFFrame = "radWndPrompt";
        public const string locationDDValue = "MRO Automated Regression Test Location 1";
        public By mroDelivery= By.XPath("//*[text()='MRO Delivery']");
        public By drpLocation = By.XPath("//div[@id='mrocontent_lstLocation_DropDown']//ul/li");
        public By requestReceivedDate = By.Id("mrocontent_txtRequestRcvdDate");
        public By logRequestButton = By.Id("mrocontent_cmdLogRequest");
        public By ignoreDuplicatesChk = By.XPath("//input[@id='mrocontent_cbDuplicates']");
        public string csvFileName = "ROIBillingOfficeRequestTest.csv";
        public string csvFileName1 = "ROINativePDFB2PIssuesTest.csv";
        public By internalPortalLnk = By.XPath("//span[text()='Internal Portal']");
        internal const string selectInternalPortal = "mrocontent_lstInternal";
        internal const string selectUserName = "mrocontent_lstPortal";
        public const string userNameDDValue = "Test, Tester";
        public By internalPortalElement = By.XPath("//span[contains(text(),'Internal Portal')]");
        public const string bostonProperLocation = "Boston Proper";
        public By internalPortalDrp = By.Id("mrocontent_lstInternal");
        public const string internalPortalSelection = "Test email notification Portal";
        public By usernameDrp = By.Id("mrocontent_lstPortal");
        public const string usernameValue = "boehm, ron";
        public By complianceReviewChkbox = By.Id("mrocontent_cbComplianceHold");
        public By createRequestLnk = By.XPath("//td[contains(text(),'Create a Request')]");
        public By panTxt = By.Id("mrocontent_txtPAN");
        public By requesterDrp = By.XPath("//input[@id='mrocontent_RadCmbBxShipToRequesters_Input']");
        public const string requesterValue = "Aetna Test (fax)";
        public By createRequestBtn = By.XPath("//input[@id='mrocontent_cmdCreateRequest']");
        public const string panValue = "12345";
        public const string roiNativeDOBValue = "01/02/1990";
        public const string roiUserNameDDValue = "Test, Int";
        internal const string shipToRequesterDrp = "mrocontent_lstRequester";
        public const string shipToRequesterDDValue = "Central Billing Office";
        internal const string selectShipToRequesterDD = "mrocontent_lstRequester";
        public By OnsiteDeliveryTab = By.XPath("//*[text()='On-Site Delivery']");
        public By STATChk = By.XPath("//input[@id='mrocontent_cbStatRequest']");
        public By emailDeliveryChkBox = By.Id("mrocontent_cbEnableEmailDelivery");
        public By emailDeliveryTextbox = By.Id("mrocontent_txtDeliveryEmail");
        internal const string purposeOfUseDropdown = "mrocontent_lstPurposeOfUse";
        public const string kOPLocation = "King of Prussia";

        public By billingOffice = By.XPath("//*[contains(text(),'Billing Office')]");
        public By boEportalDrp = By.XPath("//select[@id='mrocontent_lstBOE']");
        public By userName = By.XPath("//select[@id='mrocontent_lstBOEPortal']");
        public By payerRequester = By.XPath("//select[@id='mrocontent_lstPayerRequester']");
        public By subscriberId = By.XPath("//input[@id='mrocontent_txtSubID']");
        public By claimId = By.XPath("//input[@id='mrocontent_txtClaimID']");
        public By referenceId = By.XPath("//input[@id='mrocontent_txtRefID']");
        public By ssnNumber = By.XPath("//input[@id='mrocontent_txtSSN']");
        public By importPdf = By.XPath(" //input[@ id='mrocontent_cmdImportPDFDocument2']");       
        public By importCloseButton = By.Id("mrocontent_btnCloseImport");
        public string importRequestPDFFile = "2.pdf";
        public By importPDFButton = By.Id("mrocontent_btnImportDoc");
        public By completeRequest = By.XPath("//input[@ id='mrocontent_cmdCompleteRequest']");
        public By selectFileButton = By.Id("mrocontent_rauFileUploadfile0");
        public By epicRef = By.XPath("//input[@ id='mrocontent_txtEPICEdit']");
        public const string nativePDFTestLocation = "ROI Native PDF Test";

        public By billingOfficeLink = By.XPath("//span[text()='Billing Office']");
        public By selectBOEPortal = By.Id("mrocontent_lstBOE");
        public By selectUserForBOEPortal = By.Id("mrocontent_lstBOEPortal");
        public By selectPayerRequest = By.Id("mrocontent_lstPayerRequester");
        public By dobForBOERequest = By.Id("mrocontent_txtDOB");
        /// <summary>
        /// Go to Mro Delivery Tab
        /// </summary>
        public ROIFacilityLogNewRequestPage ClickMRODeliveryTab()
        {
            try
            {
                Driver.Click(mroDelivery);
            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to click mro delivery tab with  details as Message : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }

            return new ROIFacilityLogNewRequestPage(Driver,logger,Context);
        }

        /// <summary>
        /// Create new mro delivery request
        /// </summary>
        public ROIFacilityLogNewRequestPage CreateNewMRODeliveryRequestWithoutScan()
        {
            try
            {
                ClickRODeliveryTab();
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
                Driver.SelectValueFromDD(drpLocation, bostonProperLocation);                
                var todaysDate = String.Format("{0:M/dd/yyyy}", DateTime.Now).Replace("-", "/");
                var requestRecievedDate = Driver.FindElementBy(requestReceivedDate);
                requestRecievedDate.SendKeys(todaysDate);                
                WebElementHelper helper = new WebElementHelper(Driver, logger,Context);
                helper.ScrollIntoView("mrocontent_cmdLogRequest", FindElementBy.Id);
                Driver.ScrollIntoViewAndClick(logRequestButton);
                Iframe frame = new Iframe(Driver, logger, Context);
                frame.switchToDefaut();
                ROIMenuSelector menu = new ROIMenuSelector(Driver,logger,Context);
                menu.SelectRecentRequestID();
                
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to create new MRO delivery request Exception details with Message : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
                
            }
            return new ROIFacilityLogNewRequestPage(Driver,logger,Context);
        }


        /// <summary>
        /// Create new mro delivery request
        /// </summary>
        public ROIFacilityRequestStatusPage CreateMRODeliveryRequestWithoutScan()
        {
            try
            {
                logger.Log(Status.Info, "log a new MRO delivery request");
                PatientFirstName = "FN" + createdDateTime;
                PatientLastName = "LN" + createdDateTime;
                logger.Log(Status.Info, "Create a New MRO Delivery Request.");
               Driver.FindElementBy(By.Id(PageElements.LogNewRequestPage.firstName_Id)).SendKeys(PatientFirstName);
                logger.Log(Status.Info, "First Name entered." + PatientFirstName);
               Driver.FindElementBy(By.Id(PageElements.LogNewRequestPage.lastName_Id)).SendKeys(PatientLastName);
                logger.Log(Status.Info, "Last Name entered." + PatientLastName);
               Driver.FindElementBy(By.Id(PageElements.LogNewRequestPage.dateOfBith_Id)).SendKeys(DateTime.Now.AddYears(-25).ToString("MM/dd/yyy"));
                logger.Log(Status.Info, "DOB Entered.");
                var locationDropDown =Driver.FindElementBy(By.Id(PageElements.LogNewRequestPage.locationDropDown_id));
                locationDropDown.Click();
               Driver.SelectValueFromDD(drpLocation, locationDDValue);
                DebugUtil.DebugMessage("Location selected");
                var todaysDate = String.Format("{0:M/dd/yyyy}", DateTime.Now).Replace("-", "/");
                var requestRecievedDate =Driver.FindElementBy(requestReceivedDate);
                requestRecievedDate.SendKeys(todaysDate);
                WebElementHelper helper = new WebElementHelper(Driver, logger,Context);
                helper.ScrollIntoView("mrocontent_cmdLogRequest", FindElementBy.Id);
               Driver.FindElementBy(logRequestButton).Click();
                logger.Log(Status.Info, "RequestID created successfully");
                ROIMenuSelector menu = new ROIMenuSelector(Driver, logger,Context);
                menu.SelectRecentRequestID();
                Assert.IsTrue(LogNewRequestPage.NewRequestCreated, "Failed to create new MRO delivery request");
                logger.Log(Status.Pass, "Sucessfully logged a new MRO Delivery request");
            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to create new MRO delivery request id : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
            return new ROIFacilityRequestStatusPage(Driver, logger,Context);
        }


        /// <summary>
        /// Verify MRO Delivery tab status
        /// </summary>        
        public ROIFacilityRequestStatusPage ClickRODeliveryTab()
        {
            try
            {
                LogNewRequestPage logNewRequstpage = new LogNewRequestPage(Driver, logger,Context);
                bool tab = logNewRequstpage.ClickMRODeliveryTab();
            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to verify MRO delivery tab status : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
            return new ROIFacilityRequestStatusPage(Driver,logger,Context);

        }

        /// <summary>
        /// Create new mro delivery request
        /// </summary>
        public ROIFacilityRequestStatusPage MRODeliveryRequestForDukeStageTestingLocation()
        {
            try
            {
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
                Driver.SelectValueFromDD(drpLocation, dukeStagingLocationValue);               
                var todaysDate = String.Format("{0:M/dd/yyyy}", DateTime.Now).Replace("-", "/");
                var requestRecievedDate = Driver.FindElementBy(requestReceivedDate);
                requestRecievedDate.SendKeys(todaysDate);             
                WebElementHelper webElementHelper = new WebElementHelper(Driver, logger,Context);
                webElementHelper.ScrollIntoView("mrocontent_cmdLogRequest", FindElementBy.Id);
                Driver.Click(logRequestButton);
                MenuSelector menuSelector = new MenuSelector(Driver, logger, Context);
                menuSelector.SelectRecentRequestID();
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to create new MRO delivery request Exception details with Message : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");

            }
            return new ROIFacilityRequestStatusPage(Driver,logger,Context);
        }

        public void CheckForDuplicates()
        {
            try
            {
                var chkIgnoreDuplicates = Driver.FindElementsBy(ignoreDuplicatesChk);
                if (chkIgnoreDuplicates.Count > 0)
                {
                    chkIgnoreDuplicates[0].Click();
                }
            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to check duplicates Message : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }

        /// <summary>
        /// Create a duplicate mro delivery request
        /// </summary>
        public ROIFacilityRequestStatusPage CreateDuplicateMRODeliveryRequest()
        {
            try
            {
                PatientFirstName = "FN" + createdDateTime;
                PatientLastName = "LN" + createdDateTime;
                Driver.FindElementBy(By.Id(PageElements.LogNewRequestPage.firstName_Id)).SendKeys(PatientFirstName);
                logger.Log(Status.Info, $"First Name entered ({PatientFirstName})");
                Driver.FindElementBy(By.Id(PageElements.LogNewRequestPage.lastName_Id)).SendKeys(PatientLastName);
                logger.Log(Status.Info, $"Last Name entered ({PatientLastName})");
                Driver.FindElementBy(By.Id(PageElements.LogNewRequestPage.dateOfBith_Id)).SendKeys(DateTime.Now.AddYears(-25).ToString("MM/dd/yyy"));               
                var locationDropDown = Driver.FindElementBy(By.Id(PageElements.LogNewRequestPage.locationDropDown_id));
                locationDropDown.Click();
                Driver.SendKeys(drpLocation, locationDDValue);
                CheckForDuplicates();
                Driver.Wait(TimeSpan.FromSeconds(2));
                var todaysDate = String.Format("{0:M/dd/yyyy}", DateTime.Now).Replace("-", "/");
                var requestRecievedDate = Driver.FindElementBy(requestReceivedDate);
                requestRecievedDate.SendKeys(todaysDate);
                WebElementHelper webElementHelper = new WebElementHelper(Driver, logger,Context);
                webElementHelper.ScrollIntoView("mrocontent_cmdLogRequest", FindElementBy.Id);
                Driver.FindElementBy(logRequestButton).Click();
                CheckForDuplicates();
                MenuSelector menuSelector = new MenuSelector(Driver, logger, Context);
                menuSelector.SelectRecentRequestID();

            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to create duplicate MRO delivery request id : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
            return new ROIFacilityRequestStatusPage(Driver, logger,Context);
        }

        // <summary>
        /// Create new roi test facility delivery request
        /// </summary>
        public ROIFacilityLogNewRequestPage CreateNewROITestFacilityDeliveryRequestWithoutScan()
        {
            try
            {
                LogNewRequestPage logNewRequstpage = new LogNewRequestPage(Driver, logger, Context);
                logNewRequstpage.ClickMRODeliveryTab();
                PatientFirstName = "FN" + createdDateTime;
                PatientLastName = "LN" + createdDateTime;
                Driver.FindElementBy(By.Id(PageElements.LogNewRequestPage.firstName_Id)).SendKeys(PatientFirstName);
                logger.Log(Status.Info, $"First Name entered. ({ PatientFirstName})");
                Driver.FindElementBy(By.Id(PageElements.LogNewRequestPage.lastName_Id)).SendKeys(PatientLastName);
                logger.Log(Status.Info, $"Last Name entered. ({PatientLastName})");
                Driver.FindElementBy(By.Id(PageElements.LogNewRequestPage.dateOfBith_Id)).SendKeys(DateTime.Now.AddYears(-25).ToString("MM/dd/yyy"));
                logger.Log(Status.Info, $"DOB Entered. ({DateTime.Now.AddYears(-25).ToString("MM/dd/yyy")})");
                csvReader = new CSVReader(Path.GetFullPath(Path.Combine(Assembly.GetExecutingAssembly().Location, "..", "TestData", csvFileName1)));
                string locationValueROITest = csvReader.GetDataFromCSVFile("locationDDValueROITest");
                var locationDropDown = Driver.FindElementBy(By.Id(PageElements.LogNewRequestPage.locationDropDown_id));
                locationDropDown.Click();
                Driver.SelectValueFromDD(drpLocation, locationValueROITest);
                logger.Log(Status.Info, $"Location selected as: ({locationValueROITest})");
                var todaysDate = String.Format("{0:M/dd/yyyy}", DateTime.Now).Replace("-", "/");
                var requestRecievedDate = Driver.FindElementBy(requestReceivedDate);
                requestRecievedDate.SendKeys(todaysDate);
                logger.Log(Status.Info, $"Request received date is: ({todaysDate})");
                WebElementHelper helper = new WebElementHelper(Driver, logger, Context);
                helper.ScrollIntoView("mrocontent_cmdLogRequest", FindElementBy.Id);
                logger.Log(Status.Info, "Click on log request button");
                Driver.FindElementBy(logRequestButton).Click();
                MenuSelector menuSelector = new MenuSelector(Driver, logger, Context);
                Iframe frame = new Iframe(Driver, logger, Context);
                frame.switchToDefaut();
                menuSelector.SelectRecentRequestID();
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to create new MRO delivery request Exception details with Message : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");

            }
            return new ROIFacilityLogNewRequestPage(Driver, logger, Context);
        }

        /// <summary>
        /// Create a internal portal request
        /// </summary>
        public void CreateInternalPortalRequest()
        {
            try
            {
                    Driver.Click(internalPortalLnk);
                PatientFirstName = "FN" + createdDateTime;
                PatientLastName = "LN" + createdDateTime;
                Driver.FindElementBy(By.Id(PageElements.LogNewRequestPage.firstName_Id)).SendKeys(PatientFirstName);
                logger.Log(Status.Info, $"First name entered. ({PatientFirstName})");
                Driver.FindElementBy(By.Id(PageElements.LogNewRequestPage.lastName_Id)).SendKeys(PatientLastName);
                logger.Log(Status.Info, $"Last name entered. ({PatientLastName})");
                Driver.FindElementBy(By.Id(PageElements.LogNewRequestPage.dateOfBith_Id)).SendKeys(DateTime.Now.AddYears(-25).ToString("MM/dd/yyy"));
               
                csvReader = new CSVReader(Path.GetFullPath(Path.Combine(Assembly.GetExecutingAssembly().Location, "..", "TestData", csvFileName)));
                string locationValue = csvReader.GetDataFromCSVFile("locationDDValue");
                var locationDropDown = Driver.FindElementBy(By.Id(PageElements.LogNewRequestPage.locationDropDown_id));
                locationDropDown.Click();
                Driver.SelectValueFromDD(drpLocation, locationValue);
                Driver.Wait(TimeSpan.FromSeconds(2));
                var todaysDate = String.Format("{0:M/dd/yyyy}", DateTime.Now).Replace("-", "/");
                var requestRecievedDate = Driver.FindElementBy(requestReceivedDate);
                requestRecievedDate.SendKeys(todaysDate);
                var internalPortalDropDown = Driver.FindElementBy(By.Id(selectInternalPortal));
                var selectElement = new SelectElement(internalPortalDropDown);
                string internalPortalValue = csvReader.GetDataFromCSVFile("internalPortalDDValue");
                internalPortalValue = "test portal abc";
                selectElement.SelectByText(internalPortalValue);
                var usernameDropDown = Driver.FindElementBy(By.Id(selectUserName));
                var selectElement1 = new SelectElement(usernameDropDown);
                //selectElement1.SelectByText(userNameDDValue);
                selectElement1.SelectByText("<Any User>");
                WebElementHelper helper = new WebElementHelper(Driver, logger, Context);
                helper.ScrollIntoView("mrocontent_cmdLogRequest", FindElementBy.Id);
                Driver.FindElementBy(logRequestButton).Click();
                Iframe frame = new Iframe(Driver, logger, Context);
                frame.switchToDefaut();
                MenuSelector menuSelector = new MenuSelector(Driver, logger, Context);
                menuSelector.SelectRecentRequestID();
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to create new internal portal Request id : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }

        /// <summary>
        /// Create new Roi native Pdf test delivery request
        /// </summary>
        public ROIFacilityRequestStatusPage CreateRoiNativePdfTestDeliveryRequestWithoutScan()
        {
            try
            {
                LogNewRequestPage logNewRequstpage = new LogNewRequestPage(Driver, logger, Context);
                logNewRequstpage.ClickMRODeliveryTab();
                PatientFirstName = "FN" + createdDateTime;
                PatientLastName = "LN" + createdDateTime;
                Driver.FindElementBy(By.Id(PageElements.LogNewRequestPage.firstName_Id)).SendKeys(PatientFirstName);
                logger.Log(Status.Info, $"First Name entered. ({ PatientFirstName})");
                Driver.FindElementBy(By.Id(PageElements.LogNewRequestPage.lastName_Id)).SendKeys(PatientLastName);
                logger.Log(Status.Info, $"Last Name entered. ({PatientLastName})");
                Driver.FindElementBy(By.Id(PageElements.LogNewRequestPage.dateOfBith_Id)).SendKeys(DateTime.Now.AddYears(-25).ToString("MM/dd/yyy"));
                csvReader = new CSVReader(Path.GetFullPath(Path.Combine(Assembly.GetExecutingAssembly().Location, "..", "TestData", csvFileName1)));
                string locationValueROINative = csvReader.GetDataFromCSVFile("locationDDValueROINative");
                var locationDropDown = Driver.FindElementBy(By.Id(PageElements.LogNewRequestPage.locationDropDown_id));
                locationDropDown.Click();
                Driver.SelectValueFromDD(drpLocation, locationValueROINative);
                var todaysDate = String.Format("{0:M/dd/yyyy}", DateTime.Now).Replace("-", "/");
                var requestRecievedDate = Driver.FindElementBy(requestReceivedDate);
                requestRecievedDate.SendKeys(todaysDate);
                WebElementHelper helper = new WebElementHelper(Driver, logger, Context);
                helper.ScrollIntoView("mrocontent_cmdLogRequest", FindElementBy.Id);
                Driver.FindElementBy(logRequestButton).Click();
                MenuSelector menuSelector = new MenuSelector(Driver, logger, Context);
                menuSelector.SelectRecentRequestID();
            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to create new MRO delivery request id : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
            return new ROIFacilityRequestStatusPage(Driver, logger, Context);
        }

        /// <summary>
        /// Create a duplicate Roi native Pdf test delivery request
        /// </summary>
        public ROIFacilityRequestStatusPage CreateDuplicateRoiNativePdfTestDeliveryRequest()
        {
            try
            {
                LogNewRequestPage logNewRequstpage = new LogNewRequestPage(Driver, logger, Context);
                logNewRequstpage.ClickMRODeliveryTab();
                PatientFirstName = "FN" + createdDateTime;
                PatientLastName = "LN" + createdDateTime;
                Driver.FindElementBy(By.Id(PageElements.LogNewRequestPage.firstName_Id)).SendKeys(PatientFirstName);
                logger.Log(Status.Info, $"First Name entered. ({ PatientFirstName})");
                Driver.FindElementBy(By.Id(PageElements.LogNewRequestPage.lastName_Id)).SendKeys(PatientLastName);
                logger.Log(Status.Info, $"Last Name entered. ({PatientLastName})");
                Driver.FindElementBy(By.Id(PageElements.LogNewRequestPage.dateOfBith_Id)).SendKeys(DateTime.Now.AddYears(-25).ToString("MM/dd/yyy"));
                csvReader = new CSVReader(Path.GetFullPath(Path.Combine(Assembly.GetExecutingAssembly().Location, "..", "TestData", csvFileName1)));
                string locationValueROINative = csvReader.GetDataFromCSVFile("locationDDValueROINative");
                var locationDropDown = Driver.FindElementBy(By.Id(PageElements.LogNewRequestPage.locationDropDown_id));
                locationDropDown.Click();
                Driver.SelectValueFromDD(drpLocation, locationValueROINative);
                IWebElement chkIgnoreDuplicates = Driver.FindElementBy(ignoreDuplicatesChk);
                chkIgnoreDuplicates.Click();
                Driver.Wait(TimeSpan.FromSeconds(2));
                var todaysDate = String.Format("{0:M/dd/yyyy}", DateTime.Now).Replace("-", "/");
                var requestRecievedDate = Driver.FindElementBy(requestReceivedDate);
                requestRecievedDate.SendKeys(todaysDate);
                WebElementHelper helper = new WebElementHelper(Driver, logger, Context);
                helper.ScrollIntoView("mrocontent_cmdLogRequest", FindElementBy.Id);
                Driver.FindElementBy(logRequestButton).Click();
                MenuSelector menuSelector = new MenuSelector(Driver, logger, Context);
                menuSelector.SelectRecentRequestID();
            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to create duplicate Roi Native Pdf Test delivery request id : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
            return new ROIFacilityRequestStatusPage(Driver, logger, Context);
        }

        
        /// <summary>
        /// Click on Internal portal tab
        /// </summary>
        public void ClickOnInternalPortalTab()
        {
            try
            {
                Driver.Click(internalPortalElement);
            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to click internal portal Exception details with Message : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }

        }

        /// <summary>
        /// Create new mro delivery request
        /// </summary>
        public void CreateNewInternalPortalTabLognewRequest()
        {
            try
            {
                PatientFirstName = "FN" + createdDateTime;
                PatientLastName = "LN" + createdDateTime;
                IWebElement firstName = Driver.FindElementBy(By.Id(PageElements.LogNewRequestPage.firstName_Id));
                firstName.SendKeys(PatientFirstName);
                logger.Log(Status.Info, "First Name entered." + PatientFirstName);
                IWebElement lastName = Driver.FindElementBy(By.Id(PageElements.LogNewRequestPage.lastName_Id));
                lastName.SendKeys(PatientLastName);
                logger.Log(Status.Info, "Last Name entered." + PatientLastName);
                IWebElement dob = Driver.FindElementBy(By.Id(PageElements.LogNewRequestPage.dateOfBith_Id));
                dob.SendKeys(DateTime.Now.AddYears(-25).ToString("MM/dd/yyy"));
                var locationDropDown = Driver.FindElementBy(By.Id(PageElements.LogNewRequestPage.locationDropDown_id));
                locationDropDown.Click();
                Driver.SelectValueFromDD(drpLocation, bostonProperLocation);
                var todaysDate = String.Format("{0:M/dd/yyyy}", DateTime.Now).Replace("-", "/");
                var requestRecievedDate = Driver.FindElementBy(requestReceivedDate);
                requestRecievedDate.SendKeys(todaysDate);
                Driver.ScrollIntoViewAndClick(complianceReviewChkbox);
                string internalPortalVal = IniHelper.ReadConfig("InternalPortalWithComplianceHoldTest", "InternalPortalDrpVal");
                SelectElement _internalPortalDrp = new SelectElement(Driver.FindElement(internalPortalDrp));
                _internalPortalDrp.SelectByText(internalPortalVal);
                string userName = IniHelper.ReadConfig("InternalPortalWithComplianceHoldTest", "InternalUsername");
                SelectElement _usernameDrp = new SelectElement(Driver.FindElement(usernameDrp));
                _usernameDrp.SelectByText(userName);
                WebElementHelper helper = new WebElementHelper(Driver, logger, Context);
                helper.ScrollIntoView("mrocontent_cmdLogRequest", FindElementBy.Id);
                IWebElement logRequest = Driver.FindElementBy(logRequestButton);
                logRequest.Click();
                //MenuSelector menuSelector = new MenuSelector(Driver, logger, Context);
                //menuSelector.SelectRecentRequestID();

            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to create new MRO delivery request Exception details with Message : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");

            }
        }

        /// <summary>
        /// Go to ComplianceReview page
        /// </summary>
        public void ClickOnComplianceReview()
        {
            try
            {
                MenuSelector menu = new MenuSelector(Driver, logger, Context);
                menu.Select("ROI Requests", "Compliance Review");
            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to click compliance review Exception details with Message : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }

        /// <summary> 
        /// Create new mro delivery request for Boston Proper location
        /// </summary>
        public void CreateNewMRODeliveryRequestForBostonProper()
        {
            try
            {
                PatientFirstName = "FN" + createdDateTime;
                PatientLastName = "LN" + createdDateTime;
                logger.Log(Status.Info, "Create a New MRO Delivery Request.");
                IWebElement firstName = Driver.FindElementBy(By.Id(PageElements.LogNewRequestPage.firstName_Id));
                firstName.SendKeys(PatientFirstName);
                logger.Log(Status.Info, "First Name entered." + PatientFirstName);
                IWebElement lastName = Driver.FindElementBy(By.Id(PageElements.LogNewRequestPage.lastName_Id));
                lastName.SendKeys(PatientLastName);
                logger.Log(Status.Info, "Last Name entered." + PatientLastName);
                IWebElement dob = Driver.FindElementBy(By.Id(PageElements.LogNewRequestPage.dateOfBith_Id));
                dob.SendKeys(DateTime.Now.AddYears(-25).ToString("MM/dd/yyy"));
                var locationDropDown = Driver.FindElementBy(By.Id(PageElements.LogNewRequestPage.locationDropDown_id));
                locationDropDown.Click();
                Driver.SelectValueFromDD(drpLocation, bostonProperLocation);
                var todaysDate = String.Format("{0:M/dd/yyyy}", DateTime.Now).Replace("-", "/");
                var requestRecievedDate = Driver.FindElementBy(requestReceivedDate);
                requestRecievedDate.SendKeys(todaysDate);
                WebElementHelper helper = new WebElementHelper(Driver, logger, Context);
                helper.ScrollIntoView("mrocontent_cmdLogRequest", FindElementBy.Id);
                IWebElement logRequest = Driver.FindElementBy(logRequestButton);
                logRequest.Click();
                MenuSelector menuSelector = new MenuSelector(Driver, logger, Context);
                menuSelector.SelectRecentRequestID();

            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to create new MRO delivery request Exception details with Message : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");

            }

        }
        /// <summary>
        /// Create a new internal portal  request
        /// </summary>data:image/pjpeg;base64,/9j/4AAQSkZJRgABAQEAYABgAAD/2wBDAAEBAQEBAQEBAQEBAQEBAQEBAQEBAQEBAQEBAQEBAQEBAQEBAQEBAQEBAQEBAQEBAQEBAQEBAQEBAQEBAQEBAQH/2wBDAQEBAQEBAQEBAQEBAQEBAQEBAQEBAQEBAQEBAQEBAQEBAQEBAQEBAQEBAQEBAQEBAQEBAQEBAQEBAQEBAQEBAQH/wAARCABAAEADASIAAhEBAxEB/8QAHwAAAQUBAQEBAQEAAAAAAAAAAAECAwQFBgcICQoL/8QAtRAAAgEDAwIEAwUFBAQAAAF9AQIDAAQRBRIhMUEGE1FhByJxFDKBkaEII0KxwRVS0fAkM2JyggkKFhcYGRolJicoKSo0NTY3ODk6Q0RFRkdISUpTVFVWV1hZWmNkZWZnaGlqc3R1dnd4eXqDhIWGh4iJipKTlJWWl5iZmqKjpKWmp6ipqrKztLW2t7i5usLDxMXGx8jJytLT1NXW19jZ2uHi4+Tl5ufo6erx8vP09fb3+Pn6/8QAHwEAAwEBAQEBAQEBAQAAAAAAAAECAwQFBgcICQoL/8QAtREAAgECBAQDBAcFBAQAAQJ3AAECAxEEBSExBhJBUQdhcRMiMoEIFEKRobHBCSMzUvAVYnLRChYkNOEl8RcYGRomJygpKjU2Nzg5OkNERUZHSElKU1RVVldYWVpjZGVmZ2hpanN0dXZ3eHl6goOEhYaHiImKkpOUlZaXmJmaoqOkpaanqKmqsrO0tba3uLm6wsPExcbHyMnK0tPU1dbX2Nna4uPk5ebn6Onq8vP09fb3+Pn6/9oADAMBAAIRAxEAPwD+vCiiivnzzwooooAKKKKACiiigAooooA5bxn458FfDnw9eeLviF4w8L+A/CmnPaxah4n8ZeINJ8MeHrCS+uobGxjvNa1u7sdNtXvL24t7O1Se5Rri6nht4Q8siI13w14o8NeNNB0vxT4O8Q6H4s8Ma3bC90bxH4a1aw13QdXs2ZkW70zV9LuLrT7+2Z0dBPa3EsRZGUNlSB+J/wDwVLnf9pv9o79jH/gnTpN1cnSviN43Hxr+OSadM0F1afDDwXDq6Wts1zHuWD+1NP03x9Pbi5ASLWtM8NTLHPLLAo1P+CL/AI213wR4W/aH/YX8f3pm8cfshfGDxJoujefmE6j8PPE+t6rNZX2nwyEvPYjxNY69q63EbNDFpvirQEAVJYWl4Vi5PFvD8i9nd01Vu9a0YRqyha1tKclb+8pLo7Z+09/ltpeyl/esnbfz7b6H7A3/AMTfhvpfjnRvhhqfxA8E6d8S/Eemzaz4e+Hl94r0K08c69pFumpSz6ro3hO4v49e1TToI9G1iSa+srCe2jTSdSd5VWxujFU8dfFz4U/C6TQ4fiZ8Tfh98O5vE9zNZeGovHXjPw54Sk8Q3ls1qlxaaGmv6lp7atcwNfWSzQWAuJYmu7UOim4iD/gt+2/8WfAnwK/4LVfsp/F34ma3D4e8D+A/2SfHOua/qco3usEWhftQw21lY24Kve6tq19Na6Vo+nQk3Gpare2dhbq09xGp9V/Ym+Cnjv8Abh+Oy/8ABTH9qXQpdO8NwH7L+xp8F9VP2nT/AAZ4LtLmaTSfiPqVnKohuNVunZtS0G/liU6prdxdeM7WC20218DG1iONnOpOjTpqVWNd07NvljRjGlKVWdlonzuMFf3pWW17HO3JxS15reSikryf32S6vyuz91qKKK9FX66+ZoFMkkjhjeWV0iiiRpJZZGWOOONFLPJI7EKiIoLM7EKqgkkAZp9VL+wsdVsb3S9Us7XUdN1K0uLDUNPvreK7sr6xvIXt7uzvLWdJILm1uYJJIbiCZHimid45FZGIIB/LR+zt4N/a6/by/a//AGr/ANur9l746eEvgjpmleMLj4GfD/xT4u8AaV8QV1nwDpdnpRt7DQdN1/R9Z0zQ5xoOjeENf1i6t7Zb6S88W31vDdLb3WoLea8Phz9pD/gnz/wVA+AXxq/af+Lfhf4sWv7Y9vqPwf8AH/xB8K+ENP8AAWkyzRReE/C+iQa3o+k6dpOiWlzoWrR/DbWJtWWyhe80i01VpXeaG9uj/Sl8Pfhh8NvhJ4fPhP4VfD7wV8NPC5v7nVD4c8A+FtE8IaEdTvEhju9QOk+H7HT7A310ltbpcXZg8+ZIIVkkZYkC0/iL8IPhP8X7PSdP+LHwy+H/AMTbDQdR/tfQ7Lx/4O8PeMLXRtV8owf2npVv4g0/UItP1DyWMX2y0SK48slPM2nFeZ/Z7UIyVaf1hVliOZzqOi6ntOeX7q/KlKN4Npc1m3fVmPstE+Z8yfNdt2vdN3XZrT0Pwj/bR+GXgj4zf8FtP2T/AIWfEjQbXxL4I8c/skePNA8RaNeA+XdWN34e/aj2ywTKRLZ6hY3CQ3+l6jbNHeaZqVraahZTQ3dtDKmh+yD8YfG//BNb9pBv+CdX7TGv3Wp/A7xtqM+q/sg/GjWn8qxjsda1Fxa+B9YvZNttaQXupT/2bPa7ol8MeNZGjSKTwt4t0bUNP/dLUfhT8L9X+IOhfFrVfhz4F1P4p+F9Lm0Pw18Sb/wnoN5488P6Lcx6rFcaRoni64sJNf0vTJ4td1uOaxsb+C1kTWNTR4it/dCWj8Svgr8HPjPbaTZ/F/4UfDf4qWmgXFzd6Ha/EXwR4a8a2+jXd5HFFd3OlQ+I9M1KPT7i6iggjuJrRYpJkhiWRmWNAKWCnCdSvTnFV3iHVjJ35ZUZRpwnRqLqnyOUWvhlytPRlcju5JpS5rpvX3WleLtbtdb2dn5HptFRQww20MVvbxRwwQRRwwQxII4oYYkCRxRooCpHGiqiIoCqoAAAFS16Kv1362NAooooAKKKKACiiigAooooA//Z
        public void CreateNewInternalPortalRequest()
        {
            try
            {
                Driver.Click(createRequestLnk);
                Driver.SleepTheThread(5);
                PatientFirstName = "FN" + createdDateTime;
                PatientLastName = "LN" + createdDateTime;
                logger.Log(Status.Info, "Create a new internal portal request.");
                IWebElement firstName = Driver.FindElementBy(By.Id(PageElements.LogNewRequestPage.firstName_Id));
                firstName.SendKeys(PatientFirstName);
                logger.Log(Status.Info, "First Name entered." + PatientFirstName);
                IWebElement lastName = Driver.FindElementBy(By.Id(PageElements.LogNewRequestPage.lastName_Id));
                lastName.SendKeys(PatientLastName);
                logger.Log(Status.Info, "Last Name entered." + PatientLastName);
                IWebElement dob = Driver.FindElementBy(By.Id(PageElements.LogNewRequestPage.dateOfBith_Id));
                dob.SendKeys(DateTime.Now.AddYears(-25).ToString("MM/dd/yyy"));
                SelectElement locationDropDown = new SelectElement(Driver.FindElementBy(By.Id(PageElements.LogNewRequestPage.intlocation_id)));
                locationDropDown.SelectByText(bostonProperLocation);
                Driver.SleepTheThread(5);
                var todaysDate = String.Format("{0:M/dd/yyyy}", DateTime.Now).Replace("-", "/");
                var requestRecievedDate = Driver.FindElementBy(requestReceivedDate);
                requestRecievedDate.Clear();
                requestRecievedDate.SendKeys(todaysDate);
                Driver.SleepTheThread(5);
                Driver.SendKeys(panTxt, panValue);
                Driver.FindElementBy(By.Id(PageElements.LogNewRequestPage.inttextnotes_id)).SendKeys(todaysDate);
                IWebElement requesterTxt = Driver.FindElementBy(requesterDrp);
                requesterTxt.SendKeys(requesterValue);
                Driver.Wait(TimeSpan.FromSeconds(5));
                Driver.Click(createRequestBtn);
                Driver.Wait(TimeSpan.FromSeconds(5));

            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to create new internal portal request exception details with message : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");

            }

        }
        /// <summary>
        /// Create new Roi native Pdf test delivery request
        /// </summary>
        public void CreateRoiNativePdfTestDeliveryRequestWithoutScanForParticularDOB()
        {
            try
            {
                LogNewRequestPage logNewRequstpage = new LogNewRequestPage(Driver, logger, Context);
                logNewRequstpage.ClickMRODeliveryTab();
                PatientFirstName = "FN" + createdDateTime;
                PatientLastName = "LN" + createdDateTime;
                Driver.FindElementBy(By.Id(PageElements.LogNewRequestPage.firstName_Id)).SendKeys(PatientFirstName);
                logger.Log(Status.Info, $"First Name entered. ({ PatientFirstName})");
                Driver.FindElementBy(By.Id(PageElements.LogNewRequestPage.lastName_Id)).SendKeys(PatientLastName);
                logger.Log(Status.Info, $"Last Name entered. ({PatientLastName})");
                Driver.FindElementBy(By.Id(PageElements.LogNewRequestPage.dateOfBith_Id)).SendKeys(roiNativeDOBValue);
                logger.Log(Status.Info, $"DOB Entered. ({roiNativeDOBValue})");
                csvReader = new CSVReader(Path.GetFullPath(Path.Combine(Assembly.GetExecutingAssembly().Location, "..", "TestData", csvFileName1)));
                string locationValueROINative = csvReader.GetDataFromCSVFile("locationDDValueROINative");
                var locationDropDown = Driver.FindElementBy(By.Id(PageElements.LogNewRequestPage.locationDropDown_id));
                locationDropDown.Click();
                Driver.SelectValueFromDD(drpLocation, locationValueROINative);
                logger.Log(Status.Info, $"Location selected as: ({locationValueROINative})");
                var todaysDate = String.Format("{0:M/dd/yyyy}", DateTime.Now).Replace("-", "/");
                var requestRecievedDate = Driver.FindElementBy(requestReceivedDate);
                requestRecievedDate.SendKeys(todaysDate);
                logger.Log(Status.Info, $"Request received date is: ({todaysDate})");
                WebElementHelper helper = new WebElementHelper(Driver, logger, Context);
                helper.ScrollIntoView("mrocontent_cmdLogRequest", FindElementBy.Id);
                logger.Log(Status.Info, "Click on log request button");
                Driver.FindElementBy(logRequestButton).Click();
                ROIMenuSelector menu = new ROIMenuSelector(Driver, logger, Context);
                menu.SelectRecentRequestID();
                logger.Log(Status.Pass, "Sucessfully logged a new Roi Native Pdf Test Delivery request");
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to create new Roi Native Pdf Test Delivery request id : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }            
        }
        /// <summary>
        /// Create a internal portal request
        /// </summary>
        public void CreateInternalPortalROITestFacilityRequest()
        {
            try
            {
                Driver.Click(internalPortalLnk);
                PatientFirstName = "FN" + createdDateTime;
                PatientLastName = "LN" + createdDateTime;
                Driver.FindElementBy(By.Id(PageElements.LogNewRequestPage.firstName_Id)).SendKeys(PatientFirstName);
                logger.Log(Status.Info, $"First name entered. ({PatientFirstName})");
                Driver.FindElementBy(By.Id(PageElements.LogNewRequestPage.lastName_Id)).SendKeys(PatientLastName);
                logger.Log(Status.Info, $"Last name entered. ({PatientLastName})");
                Driver.FindElementBy(By.Id(PageElements.LogNewRequestPage.dateOfBith_Id)).SendKeys(DateTime.Now.AddYears(-25).ToString("MM/dd/yyy"));

                csvReader = new CSVReader(Path.GetFullPath(Path.Combine(Assembly.GetExecutingAssembly().Location, "..", "TestData", csvFileName)));
                string locationValue = csvReader.GetDataFromCSVFile("locationDDValue");
                var locationDropDown = Driver.FindElementBy(By.Id(PageElements.LogNewRequestPage.locationDropDown_id));
                locationDropDown.Click();
                Driver.SelectValueFromDD(drpLocation, locationValue);
                Driver.Wait(TimeSpan.FromSeconds(2));
                var todaysDate = String.Format("{0:M/dd/yyyy}", DateTime.Now).Replace("-", "/");
                var requestRecievedDate = Driver.FindElementBy(requestReceivedDate);
                requestRecievedDate.SendKeys(todaysDate);
                var internalPortalDropDown = Driver.FindElementBy(By.Id(selectInternalPortal));
                var selectElement = new SelectElement(internalPortalDropDown);
                string internalPortalValue = csvReader.GetDataFromCSVFile("roiInternalPortalDDValue");
                selectElement.SelectByText(internalPortalValue);
                var usernameDropDown = Driver.FindElementBy(By.Id(selectUserName));
                var selectElement1 = new SelectElement(usernameDropDown);
                selectElement1.SelectByText(roiUserNameDDValue);
                var shipToRequesterdrp = Driver.FindElementBy(By.Id(shipToRequesterDrp));
                var selectElement2 = new SelectElement(shipToRequesterdrp);
                selectElement2.SelectByText(shipToRequesterDDValue);
                WebElementHelper helper = new WebElementHelper(Driver, logger, Context);
                helper.ScrollIntoView("mrocontent_cmdLogRequest", FindElementBy.Id);
                Driver.FindElementBy(logRequestButton).Click();
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to create new internal roi test facility portal Request id : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }
        public void MRODeliveryRequestForBostonProper()
        {
            try
            {
                Random rand = new Random();
                int value = rand.Next(10, 1000);
                PatientFirstName = "FN" + createdDateTime;
                PatientLastName = "LN" + value;
                logger.Log(Status.Info, "Create a New MRO Delivery Request.");
                IWebElement firstName = Driver.FindElementBy(By.Id(PageElements.LogNewRequestPage.firstName_Id));
                firstName.SendKeys(PatientFirstName);
                logger.Log(Status.Info, "First Name entered." + PatientFirstName);
                IWebElement lastName = Driver.FindElementBy(By.Id(PageElements.LogNewRequestPage.lastName_Id));
                lastName.SendKeys(PatientLastName);
                logger.Log(Status.Info, "Last Name entered." + PatientLastName);
                IWebElement dob = Driver.FindElementBy(By.Id(PageElements.LogNewRequestPage.dateOfBith_Id));
                dob.SendKeys(DateTime.Now.AddYears(-25).ToString("MM/dd/yyy"));
                var locationDropDown = Driver.FindElementBy(By.Id(PageElements.LogNewRequestPage.locationDropDown_id));
                locationDropDown.Click();
               // Driver.Click(By.XPath("//a[@id='mrocontent_lstLocation_Input']"));
                Driver.Click(By.XPath($"//li[contains(@class, 'rcb') and contains(text(), '{bostonProperLocation}')]"));

                //Driver.SelectValueFromDD(drpLocation, bostonProperLocation);
                CheckForDuplicates();
                Driver.SleepTheThread(2);
                var todaysDate = String.Format("{0:M/dd/yyyy}", DateTime.Now).Replace("-", "/");
                var requestRecievedDate = Driver.FindElementBy(requestReceivedDate);
                requestRecievedDate.SendKeys(todaysDate);
                WebElementHelper helper = new WebElementHelper(Driver, logger, Context);
                helper.ScrollIntoView("mrocontent_cmdLogRequest", FindElementBy.Id);
                IWebElement logRequest = Driver.FindElementBy(logRequestButton);
                logRequest.Click();
               
            }
            catch (Exception ex)
            {
                Driver.SwitchToAlert();
                throw new Exception($"Failed to create new MRO delivery request Exception details with Message : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");

            }

        }
        public ROIFacilityLogNewRequestPage CreateMRODeliveryRequestForLinkToAnotherRequest()
        {
            try
            {
                ClickRODeliveryTab();
                string createdDateTime = DateTime.Now.ToString("dd/MM/yyyy");
                Random rand = new Random();
                int value = rand.Next(10, 1000);
                PatientFirstName = "FN" + createdDateTime;
                PatientLastName = "LN" + value;
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
                Driver.SelectValueFromDD(drpLocation, bostonProperLocation);
                Driver.SleepTheThread(2);
                var todaysDate = String.Format("{0:M/dd/yyyy}", DateTime.Now).Replace("-", "/");
                var requestRecievedDate = Driver.FindElementBy(requestReceivedDate);
                requestRecievedDate.SendKeys(todaysDate);
                WebElementHelper helper = new WebElementHelper(Driver, logger, Context);
                helper.ScrollIntoView("mrocontent_cmdLogRequest", FindElementBy.Id);
                Driver.ScrollIntoViewAndClick(logRequestButton);
                ROIMenuSelector menu = new ROIMenuSelector(Driver, logger, Context);
                menu.SelectRecentRequestID();

            }
            catch (Exception ex)
            {
                Driver.SwitchToAlert();
                throw new Exception($"Failed to create new MRO delivery request Exception details with Message : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");

            }
            return new ROIFacilityLogNewRequestPage(Driver, logger, Context);
        }

        public void CreateInternalPortalRequestForAj()
        {
            try
            {
                Driver.Click(internalPortalLnk);
                PatientFirstName = "FN" + createdDateTime;
                PatientLastName = "LN" + createdDateTime;
                Driver.FindElementBy(By.Id(PageElements.LogNewRequestPage.firstName_Id)).SendKeys(PatientFirstName);
                logger.Log(Status.Info, $"First name entered. ({PatientFirstName})");
                Driver.FindElementBy(By.Id(PageElements.LogNewRequestPage.lastName_Id)).SendKeys(PatientLastName);
                logger.Log(Status.Info, $"Last name entered. ({PatientLastName})");
                Driver.FindElementBy(By.Id(PageElements.LogNewRequestPage.dateOfBith_Id)).SendKeys(DateTime.Now.AddYears(-25).ToString("MM/dd/yyy"));

                string locationDD = IniHelper.ReadConfig("ROIAdminBOETransactionFeeUpdateTest", "LocationDD").Replace("[", "");
                string locationDDValue = locationDD.Replace("]", "");
                var locationDropDown = Driver.FindElementBy(By.Id(PageElements.LogNewRequestPage.locationDropDown_id));
                locationDropDown.Click();
                Driver.SelectValueFromDD(drpLocation, locationDDValue);
                Driver.Wait(TimeSpan.FromSeconds(2));
                CheckForDuplicates();

                var todaysDate = String.Format("{0:M/dd/yyyy}", DateTime.Now).Replace("-", "/");
                var requestRecievedDate = Driver.FindElementBy(requestReceivedDate);
                requestRecievedDate.SendKeys(todaysDate);

                string internalPortal = IniHelper.ReadConfig("ROIAdminBOETransactionFeeUpdateTest", "InternalPortal").Replace("[", "");
                string internalPortalValue = internalPortal.Replace("]", "");
                var internalPortalDropDown = Driver.FindElementBy(By.Id(selectInternalPortal));
                var selectElement = new SelectElement(internalPortalDropDown);
                selectElement.SelectByText(internalPortalValue);

                string userName = IniHelper.ReadConfig("ROIAdminBOETransactionFeeUpdateTest", "UserName").Replace("[", "");
                string userNameValue = userName.Replace("]", "");
                var userNameDD = Driver.FindElementBy(By.Id(selectUserName));
                var selectUserNameValue = new SelectElement(userNameDD);
                selectUserNameValue.SelectByText(userNameValue);
                Driver.Wait(TimeSpan.FromSeconds(2));

                string shipToRequester = IniHelper.ReadConfig("ROIAdminBOETransactionFeeUpdateTest", "ShiptoRequester").Replace("[", "");
                string shipToRequesterValue = shipToRequester.Replace("]", "");
                var shipRequesterDD = Driver.FindElementBy(By.Id(selectShipToRequesterDD));
                var selectshipRequesterValue = new SelectElement(shipRequesterDD);
                selectshipRequesterValue.SelectByText(shipToRequesterValue);
                Driver.Wait(TimeSpan.FromSeconds(2));


                WebElementHelper helper = new WebElementHelper(Driver, logger, Context);
                helper.ScrollIntoView("mrocontent_cmdLogRequest", FindElementBy.Id);
                Driver.FindElementBy(logRequestButton).Click();
                MenuSelector menuSelector = new MenuSelector(Driver, logger, Context);
                menuSelector.SelectRecentRequestID();
            }
            catch (Exception ex)
            {
                Driver.SwitchToAlert();
                throw new Exception($"Failed to create new internal portal Request id : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }

        /// <summary>
        /// Create new internal portal request
        /// </summary>
        public void CreateInternalPortalTabNewRequest()
        {
            try
            {
                PatientFirstName = "FN" + createdDateTime;
                PatientLastName = "LN" + createdDateTime;
                IWebElement firstName = Driver.FindElementBy(By.Id(PageElements.LogNewRequestPage.firstName_Id));
                firstName.SendKeys(PatientFirstName);
                logger.Log(Status.Info, "First Name entered." + PatientFirstName);
                IWebElement lastName = Driver.FindElementBy(By.Id(PageElements.LogNewRequestPage.lastName_Id));
                lastName.SendKeys(PatientLastName);
                logger.Log(Status.Info, "Last Name entered." + PatientLastName);
                IWebElement dob = Driver.FindElementBy(By.Id(PageElements.LogNewRequestPage.dateOfBith_Id));
                dob.SendKeys(DateTime.Now.AddYears(-25).ToString("MM/dd/yyy"));
                var locationDropDown = Driver.FindElementBy(By.Id(PageElements.LogNewRequestPage.locationDropDown_id));
                locationDropDown.Click();
                Driver.SelectValueFromDD(drpLocation, bostonProperLocation);
                Driver.SleepTheThread(3);
                var todaysDate = String.Format("{0:M/dd/yyyy}", DateTime.Now.AddDays(-1)).Replace("-", "/");
                var requestRecievedDate = Driver.FindElementBy(requestReceivedDate);
                requestRecievedDate.SendKeys(todaysDate);
                string internalPortalVal = IniHelper.ReadConfig("InternalPortalWithComplianceHoldTest", "InternalPortalDrpVal");
                SelectElement _internalPortalDrp = new SelectElement(Driver.FindElement(internalPortalDrp));
                _internalPortalDrp.SelectByText(internalPortalVal);
                string userName = IniHelper.ReadConfig("InternalPortalWithComplianceHoldTest", "InternalPortalUsername");
                SelectElement _usernameDrp = new SelectElement(Driver.FindElement(usernameDrp));
                _usernameDrp.SelectByText(userName);
                WebElementHelper helper = new WebElementHelper(Driver, logger, Context);
                helper.ScrollIntoView("mrocontent_cmdLogRequest", FindElementBy.Id);
                IWebElement logRequest = Driver.FindElementBy(logRequestButton);
                logRequest.Click();
            }
            catch (Exception ex)
            {
                Driver.SwitchToAlert();
                throw new Exception($"Failed to create new internal delivery request with message : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }
        /// <summary>
        /// Go to Onsite Delivery Tab
        /// </summary>
        public ROIFacilityLogNewRequestPage ClickOnsiteDeliveryTab()
        {
            try
            {
                Driver.Click(OnsiteDeliveryTab);
            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to click On-Site Delivery Tab with  details as Message : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }

            return new ROIFacilityLogNewRequestPage(Driver, logger, Context);
        }

        /// <summary>
        /// Create new onsite delivery request
        /// </summary>
        public List<KeyValuePair<string, string>> CreateOnsiteDeliveryRequest()
        {
            List<KeyValuePair<string, string>> requestData = new List<KeyValuePair<string, string>>();

            try
            {
                PatientFirstName = "FN" + createdDateTime;
                PatientLastName = "LN" + createdDateTime;
                IWebElement firstName = Driver.FindElementBy(By.Id(PageElements.LogNewRequestPage.firstName_Id));
                firstName.SendKeys(PatientFirstName);
                logger.Log(Status.Info, "First Name entered." + PatientFirstName);
                IWebElement lastName = Driver.FindElementBy(By.Id(PageElements.LogNewRequestPage.lastName_Id));
                lastName.SendKeys(PatientLastName);
                logger.Log(Status.Info, "Last Name entered." + PatientLastName);
                IWebElement dob = Driver.FindElementBy(By.Id(PageElements.LogNewRequestPage.dateOfBith_Id));
                dob.SendKeys(DateTime.Now.AddYears(-25).ToString("MM/dd/yyy"));
                var locationDropDown = Driver.FindElementBy(By.Id(PageElements.LogNewRequestPage.locationDropDown_id));
                locationDropDown.Click();
                Driver.SleepTheThread(4);
                Driver.SelectValueFromDD(drpLocation, bostonProperLocation);
                var todaysDate = String.Format("{0:M/dd/yyyy}", DateTime.Now).Replace("-", "/");
                var requestRecievedDate = Driver.FindElementBy(requestReceivedDate);
                requestRecievedDate.SendKeys(todaysDate);
                Driver.ScrollIntoViewAndClick(STATChk);
                //
                requestData.Add(new KeyValuePair<string, string>("FirstName", PatientFirstName));
                requestData.Add(new KeyValuePair<string, string>("LastName", PatientLastName));
                requestData.Add(new KeyValuePair<string, string>("Location", bostonProperLocation));
                //
                WebElementHelper helper = new WebElementHelper(Driver, logger, Context);
                helper.ScrollIntoView("mrocontent_cmdLogRequest", FindElementBy.Id);
                IWebElement logRequest = Driver.FindElementBy(logRequestButton);
                logRequest.Click();
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to create new MRO delivery request Exception details with Message : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
            return requestData;
        }

        public List<OnsiteDeliveryRequestData> GetTableData()
        {
            OnsiteDeliveryRequestData tableData = new OnsiteDeliveryRequestData();
            List<OnsiteDeliveryRequestData> allRequestsData = new List<OnsiteDeliveryRequestData>();
            List<IWebElement> tableDataRows = Driver.FindElementsBy(By.XPath("//table[@id ='dtTurnaroundDrillDown']//tr"));

            for (int z = 0; z < tableDataRows.Count; z++)
            {
                if (z == tableDataRows.Count - 1)
                {
                    ReadOnlyCollection<IWebElement> cells = tableDataRows[z].FindElements(By.TagName("td"));
                    string patientName = cells[0].Text;
                    string[] names = patientName.Split(',');
                    string location = cells[1].Text;
                    string requestType = cells[3].Text;
                    tableData.LastName = names[0].ToString();
                    tableData.FirstName = names[1].ToString();
                    tableData.Location = location;
                    tableData.RequestType = requestType;
                    allRequestsData.Add(tableData);
                }
                else
                {

                }
            }
            return allRequestsData;
        }

        public class OnsiteDeliveryRequestData
        {
            private string firstname; // field
            public string FirstName   // property
            {
                get { return firstname; }
                set { firstname = value; }
            }

            private string lastname; // field
            public string LastName   // property
            {
                get { return lastname; }
                set { lastname = value; }
            }
            private string location; // field
            public string Location   // property
            {
                get { return location; }
                set { location = value; }
            }

            private string requesttype; // field
            public string RequestType   // property
            {
                get { return requesttype; }
                set { requesttype = value; }
            }
        }



        public void MRODeliveryRequestWithMailId()
        {
            try
            {
                Random rand = new Random();
                int value = rand.Next(10, 1000);
                PatientFirstName = "FN" + createdDateTime;
                PatientLastName = "LN" + value;
                logger.Log(Status.Info, "Create a New MRO Delivery Request.");
                IWebElement firstName = Driver.FindElementBy(By.Id(PageElements.LogNewRequestPage.firstName_Id));
                firstName.SendKeys(PatientFirstName);
                logger.Log(Status.Info, "First Name entered." + PatientFirstName);
                IWebElement lastName = Driver.FindElementBy(By.Id(PageElements.LogNewRequestPage.lastName_Id));
                lastName.SendKeys(PatientLastName);
                logger.Log(Status.Info, "Last Name entered." + PatientLastName);
                IWebElement dob = Driver.FindElementBy(By.Id(PageElements.LogNewRequestPage.dateOfBith_Id));
                dob.SendKeys(DateTime.Now.AddYears(-25).ToString("MM/dd/yyy"));
                var locationDropDown = Driver.FindElementBy(By.Id(PageElements.LogNewRequestPage.locationDropDown_id));
                locationDropDown.Click();
                Driver.SelectValueFromDD(drpLocation, bostonProperLocation);
                CheckForDuplicates();
                Driver.SleepTheThread(2);
                var todaysDate = String.Format("{0:M/dd/yyyy}", DateTime.Now).Replace("-", "/");
                var requestRecievedDate = Driver.FindElementBy(requestReceivedDate);
                requestRecievedDate.SendKeys(todaysDate);
                if (Driver.FindElementBy(emailDeliveryChkBox).Selected == false)
                {
                    Driver.Click(emailDeliveryChkBox);
                }
                Driver.SendKeys(emailDeliveryTextbox, "test@gmail.com");
                WebElementHelper helper = new WebElementHelper(Driver, logger, Context);
                helper.ScrollIntoView("mrocontent_cmdLogRequest", FindElementBy.Id);
                IWebElement logRequest = Driver.FindElementBy(logRequestButton);
                logRequest.Click();
                

            }
            catch (Exception ex)
            {
                Driver.SwitchToAlert();
                throw new Exception($"Failed to create new MRO delivery request Exception details with Message : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");

            }

        }

        /// <summary>
        /// Create new mro delivery request
        /// </summary>
        public ROIFacilityRequestStatusPage CreateMRODeliveryRequestForDukeStageTestingLocation()
        {
            try
            {
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
                Driver.SelectValueFromDD(drpLocation, dukeStagingLocationValue);
                var todaysDate = String.Format("{0:M/dd/yyyy}", DateTime.Now).Replace("-", "/");
                var requestRecievedDate = Driver.FindElementBy(requestReceivedDate);
                requestRecievedDate.SendKeys(todaysDate);
                WebElementHelper webElementHelper = new WebElementHelper(Driver, logger, Context);
                webElementHelper.ScrollIntoView("mrocontent_cmdLogRequest", FindElementBy.Id);
                Driver.Click(logRequestButton);
            }
            catch (Exception ex)
            {
                Driver.SwitchToAlert();
                throw new Exception($"Failed to create new MRO delivery request Exception details with Message : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");

            }
            return new ROIFacilityRequestStatusPage(Driver, logger, Context);
        }

        public ROIFacilityLogNewRequestPage CreateNewMRORequestWithPuposeOfUse()
        {
            try
            {
                LogNewRequestPage logNewRequstpage = new LogNewRequestPage(Driver, logger, Context);
                logNewRequstpage.ClickMRODeliveryTab();
                PatientFirstName = "FN" + createdDateTime;
                PatientLastName = "LN" + createdDateTime;
                Driver.FindElementBy(By.Id(PageElements.LogNewRequestPage.firstName_Id)).SendKeys(PatientFirstName);
                logger.Log(Status.Info, $"First Name entered. ({ PatientFirstName})");
                Driver.FindElementBy(By.Id(PageElements.LogNewRequestPage.lastName_Id)).SendKeys(PatientLastName);
                logger.Log(Status.Info, $"Last Name entered. ({PatientLastName})");
                Driver.FindElementBy(By.Id(PageElements.LogNewRequestPage.dateOfBith_Id)).SendKeys(DateTime.Now.AddYears(-25).ToString("MM/dd/yyy"));
                logger.Log(Status.Info, $"DOB Entered. ({DateTime.Now.AddYears(-25).ToString("MM/dd/yyy")})");
                csvReader = new CSVReader(Path.GetFullPath(Path.Combine(Assembly.GetExecutingAssembly().Location, "..", "TestData", csvFileName1)));
                string locationValueROITest = csvReader.GetDataFromCSVFile("locationDDValueROITest");
                var locationDropDown = Driver.FindElementBy(By.Id(PageElements.LogNewRequestPage.locationDropDown_id));
                locationDropDown.Click();
                Driver.SelectValueFromDD(drpLocation, locationValueROITest);
                logger.Log(Status.Info, $"Location selected as: ({locationValueROITest})");
                var todaysDate = String.Format("{0:M/dd/yyyy}", DateTime.Now).Replace("-", "/");
                var requestRecievedDate = Driver.FindElementBy(requestReceivedDate);
                requestRecievedDate.SendKeys(todaysDate);
                logger.Log(Status.Info, $"Request received date is: ({todaysDate})");
                //
                var purposeOfUseDD = Driver.FindElementBy(By.Id(purposeOfUseDropdown));
                var selectPurposeOfUseValue = new SelectElement(purposeOfUseDD);
                selectPurposeOfUseValue.SelectByText("Claims Attachment");

                WebElementHelper helper = new WebElementHelper(Driver, logger, Context);
                helper.ScrollIntoView("mrocontent_cmdLogRequest", FindElementBy.Id);
                logger.Log(Status.Info, "Click on log request button");
                Driver.FindElementBy(logRequestButton).Click();
                //MenuSelector menuSelector = new MenuSelector(Driver, logger, Context);
                //menuSelector.SelectRecentRequestID();
            }
            catch (Exception ex)
            {
                Driver.SwitchToAlert();
                throw new Exception($"Failed to create new MRO delivery request Exception details with Message : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");

            }
            return new ROIFacilityLogNewRequestPage(Driver, logger, Context);
        }
        public void CreateNewMRODeliveryRequestForKOPLocation()
        {
            try
            {
                LogNewRequestPage logNewRequstpage = new LogNewRequestPage(Driver, logger, Context);
                logNewRequstpage.ClickMRODeliveryTab();
                PatientFirstName = "FN" + createdDateTime;
                PatientLastName = "LN" + createdDateTime;
                logger.Log(Status.Info, "Create a New MRO Delivery Request.");
                IWebElement firstName = Driver.FindElementBy(By.Id(PageElements.LogNewRequestPage.firstName_Id));
                firstName.SendKeys(PatientFirstName);
                logger.Log(Status.Info, "First Name entered." + PatientFirstName);
                IWebElement lastName = Driver.FindElementBy(By.Id(PageElements.LogNewRequestPage.lastName_Id));
                lastName.SendKeys(PatientLastName);
                logger.Log(Status.Info, "Last Name entered." + PatientLastName);
                IWebElement dob = Driver.FindElementBy(By.Id(PageElements.LogNewRequestPage.dateOfBith_Id));
                dob.SendKeys(DateTime.Now.AddYears(-25).ToString("MM/dd/yyy"));
                var locationDropDown = Driver.FindElementBy(By.Id(PageElements.LogNewRequestPage.locationDropDown_id));
                locationDropDown.Click();
                Driver.SelectValueFromDD(drpLocation, kOPLocation);
                var todaysDate = String.Format("{0:M/dd/yyyy}", DateTime.Now).Replace("-", "/");
                var requestRecievedDate = Driver.FindElementBy(requestReceivedDate);
                requestRecievedDate.SendKeys(todaysDate);
                WebElementHelper helper = new WebElementHelper(Driver, logger, Context);
                helper.ScrollIntoView("mrocontent_cmdLogRequest", FindElementBy.Id);
                IWebElement logRequest = Driver.FindElementBy(logRequestButton);
                logRequest.Click();
                

            }
            catch (Exception ex)
            {
                Driver.SwitchToAlert();
                throw new Exception($"Failed to create new MRO delivery request Exception details with Message : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");

            }

        }

        /// <summary>
        /// Create new internal portal request
        /// </summary>
        public void CreateInternalPortalDeliveryRequest()
        {
            try
            {
                PatientFirstName = "FN" + createdDateTime;
                PatientLastName = "LN" + createdDateTime;
                IWebElement firstName = Driver.FindElementBy(By.Id(PageElements.LogNewRequestPage.firstName_Id));
                firstName.SendKeys(PatientFirstName);
                logger.Log(Status.Info, "First Name entered." + PatientFirstName);
                IWebElement lastName = Driver.FindElementBy(By.Id(PageElements.LogNewRequestPage.lastName_Id));
                lastName.SendKeys(PatientLastName);
                logger.Log(Status.Info, "Last Name entered." + PatientLastName);
                IWebElement dob = Driver.FindElementBy(By.Id(PageElements.LogNewRequestPage.dateOfBith_Id));
                dob.SendKeys(DateTime.Now.AddYears(-25).ToString("MM/dd/yyy"));
                var locationDropDown = Driver.FindElementBy(By.Id(PageElements.LogNewRequestPage.locationDropDown_id));
                locationDropDown.Click();
                Driver.SelectValueFromDD(drpLocation, bostonProperLocation);
                var todaysDate = String.Format("{0:M/dd/yyyy}", DateTime.Now).Replace("-", "/");
                var requestRecievedDate = Driver.FindElementBy(requestReceivedDate);
                requestRecievedDate.SendKeys(todaysDate);
                string internalPortalVal = IniHelper.ReadConfig("InternalPortalWithComplianceHoldTest", "InternalPortalVal");
                SelectElement _internalPortalDrp = new SelectElement(Driver.FindElement(internalPortalDrp));
                _internalPortalDrp.SelectByText(internalPortalVal);
                string userName = IniHelper.ReadConfig("InternalPortalWithComplianceHoldTest", "UserName");
                SelectElement _usernameDrp = new SelectElement(Driver.FindElement(usernameDrp));
                _usernameDrp.SelectByText(userName);
                WebElementHelper helper = new WebElementHelper(Driver, logger, Context);
                helper.ScrollIntoView("mrocontent_cmdLogRequest", FindElementBy.Id);
                IWebElement logRequest = Driver.FindElementBy(logRequestButton);
                logRequest.Click();
            }
            catch (Exception ex)
            {
                Driver.SwitchToAlert();
                throw new Exception($"Failed to create new internal delivery request with message : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }

        public void MRODeliveryRequestWithNewLocation(string location)
        {
            try
            {
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
                Driver.SelectValueFromDD(drpLocation, location);
                var todaysDate = String.Format("{0:M/dd/yyyy}", DateTime.Now).Replace("-", "/");
                var requestRecievedDate = Driver.FindElementBy(requestReceivedDate);
                requestRecievedDate.SendKeys(todaysDate);
                WebElementHelper webElementHelper = new WebElementHelper(Driver, logger, Context);
                webElementHelper.ScrollIntoView("mrocontent_cmdLogRequest", FindElementBy.Id);
                Driver.Click(logRequestButton);
            }
            catch (Exception ex)
            {
                Driver.SwitchToAlert();
                throw new Exception($"Failed to create new MRO delivery request Exception details with Message : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");

            }
            
        }
        public ROIFacilityLogNewRequestPage CreateNewROITestFacilityDeliveryRequestWithoutScanTwo()
        {
            try
            {
                LogNewRequestPage logNewRequstpage = new LogNewRequestPage(Driver, logger, Context);
                logNewRequstpage.ClickMRODeliveryTab();
                PatientFirstName = "FN" + DateTime.Now.ToString("dd MMMM yyyy hh:mm:ss"); ;
                PatientLastName = "LN" + DateTime.Now.ToString("dd MMMM yyyy hh:mm:ss"); ;
                Driver.FindElementBy(By.Id(PageElements.LogNewRequestPage.firstName_Id)).SendKeys(PatientFirstName);
                logger.Log(Status.Info, $"First Name entered. ({ PatientFirstName})");
                Driver.FindElementBy(By.Id(PageElements.LogNewRequestPage.lastName_Id)).SendKeys(PatientLastName);
                logger.Log(Status.Info, $"Last Name entered. ({PatientLastName})");
                Driver.FindElementBy(By.Id(PageElements.LogNewRequestPage.dateOfBith_Id)).SendKeys(DateTime.Now.AddYears(-25).ToString("MM/dd/yyy"));
                logger.Log(Status.Info, $"DOB Entered. ({DateTime.Now.AddYears(-25).ToString("MM/dd/yyy")})");
                csvReader = new CSVReader(Path.GetFullPath(Path.Combine(Assembly.GetExecutingAssembly().Location, "..", "TestData", csvFileName1)));
                string locationValueROITest = csvReader.GetDataFromCSVFile("locationDDValueROITest");
                var locationDropDown = Driver.FindElementBy(By.Id(PageElements.LogNewRequestPage.locationDropDown_id));
                locationDropDown.Click();
                Driver.SelectValueFromDD(drpLocation, locationValueROITest);
                logger.Log(Status.Info, $"Location selected as: ({locationValueROITest})");
                var todaysDate = String.Format("{0:M/dd/yyyy}", DateTime.Now).Replace("-", "/");
                var requestRecievedDate = Driver.FindElementBy(requestReceivedDate);
                requestRecievedDate.SendKeys(todaysDate);
                logger.Log(Status.Info, $"Request received date is: ({todaysDate})");
                WebElementHelper helper = new WebElementHelper(Driver, logger, Context);
                helper.ScrollIntoView("mrocontent_cmdLogRequest", FindElementBy.Id);
                logger.Log(Status.Info, "Click on log request button");
                Driver.FindElementBy(logRequestButton).Click();
                MenuSelector menuSelector = new MenuSelector(Driver, logger, Context);
                menuSelector.SelectRecentRequestID();
            }
            catch (Exception ex)
            {
                Driver.SwitchToAlert();
                throw new Exception($"Failed to create new MRO delivery request Exception details with Message : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");

            }
            return new ROIFacilityLogNewRequestPage(Driver, logger, Context);
        }
        public void CreateBillingOfficeRequestWithPANAndSubscriberId()
        {
            try
            {
                Driver.Click(billingOffice);
                Driver.Wait(TimeSpan.FromSeconds(2));
                PatientFirstName = "FN" + createdDateTime;
                PatientLastName = "LN" + createdDateTime;
                IWebElement firstName = Driver.FindElementBy(By.Id(PageElements.LogNewRequestPage.firstName_Id));
                firstName.SendKeys(PatientFirstName);
                logger.Log(Status.Info, "First Name entered." + PatientFirstName);
                IWebElement lastName = Driver.FindElementBy(By.Id(PageElements.LogNewRequestPage.lastName_Id));
                lastName.SendKeys(PatientLastName);
                logger.Log(Status.Info, "Last Name entered." + PatientLastName);
                IWebElement dob = Driver.FindElementBy(By.Id(PageElements.LogNewRequestPage.dateOfBith_Id));
                dob.SendKeys(DateTime.Now.AddYears(-25).ToString("MM/dd/yyy"));
                var locationDropDown = Driver.FindElementBy(By.Id(PageElements.LogNewRequestPage.locationDropDown_id));
                locationDropDown.Click();
                Driver.SelectValueFromDD(drpLocation, bostonProperLocation);
                var todaysDate = String.Format("{0:M/dd/yyyy}", DateTime.Now).Replace("-", "/");
                var requestRecievedDate = Driver.FindElementBy(requestReceivedDate);
                requestRecievedDate.SendKeys(todaysDate);
                string boePortalVal = IniHelper.ReadConfig("ROIBillingOfficeLogANewRequestMapBOERequesterRequiredFieldsTest", "RoitCBO");
                SelectElement _boePortalDrp = new SelectElement(Driver.FindElement(boEportalDrp));
                _boePortalDrp.SelectByText(boePortalVal);
                string userVal = IniHelper.ReadConfig("ROIBillingOfficeLogANewRequestMapBOERequesterRequiredFieldsTest", "UserName");
                SelectElement _usernameDrp = new SelectElement(Driver.FindElement(userName));
                _usernameDrp.SelectByText(userVal);
                string payerVal = IniHelper.ReadConfig("ROIBillingOfficeLogANewRequestMapBOERequesterRequiredFieldsTest", "PayerRequester");
                Driver.SendKeys(payerRequester, payerVal);
                Driver.Wait(TimeSpan.FromSeconds(2));
                WebElementHelper helper = new WebElementHelper(Driver, logger, Context);
                helper.ScrollIntoView("mrocontent_cmdLogRequest", FindElementBy.Id);
                IWebElement logRequest = Driver.FindElementBy(logRequestButton);
                logRequest.Click();

                string panAlerrtMsg = IniHelper.ReadConfig("ROIBillingOfficeLogANewRequestMapBOERequesterRequiredFieldsTest", "PanAlert");
                string subscriberIdAlerrtMsg = IniHelper.ReadConfig("ROIBillingOfficeLogANewRequestMapBOERequesterRequiredFieldsTest", "SubscriberId");
                Driver.Wait(TimeSpan.FromSeconds(2));
                Driver.SwitchTo().Alert().Equals(panAlerrtMsg);
                Driver.Wait(TimeSpan.FromSeconds(2));
                Driver.SwitchTo().Alert().Accept();
                Driver.Wait(TimeSpan.FromSeconds(2));
                Driver.SendKeys(panTxt, "25631425");               
                Driver.Click(logRequestButton);
                Driver.Wait(TimeSpan.FromSeconds(2));
                Driver.SwitchTo().Alert().Equals(subscriberIdAlerrtMsg);


            }
            catch (Exception ex)
            {
                Driver.SwitchToAlert();
                throw new Exception($"Failed to create billing office  request with message : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }


        public void CreateBillingOfficeRequest()
        {
            try
            {
                Random ran = new Random();
                int val = ran.Next(10, 1000);
                Driver.Click(billingOffice);
                Driver.Wait(TimeSpan.FromSeconds(2));
                PatientFirstName = "FN" + createdDateTime;
                PatientLastName = "LN" + val;
                IWebElement firstName = Driver.FindElementBy(By.Id(PageElements.LogNewRequestPage.firstName_Id));
                firstName.SendKeys(PatientFirstName);
                logger.Log(Status.Info, "First Name entered." + PatientFirstName);
                IWebElement lastName = Driver.FindElementBy(By.Id(PageElements.LogNewRequestPage.lastName_Id));
                lastName.SendKeys(PatientLastName);
                logger.Log(Status.Info, "Last Name entered." + PatientLastName);
                IWebElement dob = Driver.FindElementBy(By.Id(PageElements.LogNewRequestPage.dateOfBith_Id));
                dob.SendKeys(DateTime.Now.AddYears(-25).ToString("MM/dd/yyy"));
                var locationDropDown = Driver.FindElementBy(By.Id(PageElements.LogNewRequestPage.locationDropDown_id));
                locationDropDown.Click();
                Driver.SelectValueFromDD(drpLocation, bostonProperLocation);
                var todaysDate = String.Format("{0:M/dd/yyyy}", DateTime.Now).Replace("-", "/");
                var requestRecievedDate = Driver.FindElementBy(requestReceivedDate);
                requestRecievedDate.SendKeys(todaysDate);
                string boePortalVal = IniHelper.ReadConfig("ROIBillingOfficeLogANewRequestMapBOERequesterRequiredFieldsTest", "RoitCBO");
                SelectElement _boePortalDrp = new SelectElement(Driver.FindElement(boEportalDrp));
                _boePortalDrp.SelectByText(boePortalVal);
                string userVal = IniHelper.ReadConfig("ROIBillingOfficeLogANewRequestMapBOERequesterRequiredFieldsTest", "UserName");
                SelectElement _usernameDrp = new SelectElement(Driver.FindElement(userName));
                _usernameDrp.SelectByText(userVal);
                string payerVal = IniHelper.ReadConfig("ROIBillingOfficeLogANewRequestMapBOERequesterRequiredFieldsTest", "PayerRequester");
                Driver.SendKeys(payerRequester, payerVal);
                Driver.Wait(TimeSpan.FromSeconds(2));
                WebElementHelper helper = new WebElementHelper(Driver, logger, Context);
                helper.ScrollIntoView("mrocontent_cmdLogRequest", FindElementBy.Id);
                IWebElement logRequest = Driver.FindElementBy(logRequestButton);
                logRequest.Click();
                Driver.Wait(TimeSpan.FromSeconds(2));
                string panAlerrtMsg = IniHelper.ReadConfig("ROIBillingOfficeLogANewRequestMapBOERequesterRequiredFieldsTest", "PanAlert");
                string subscriberIdAlerrtMsg = IniHelper.ReadConfig("ROIBillingOfficeLogANewRequestMapBOERequesterRequiredFieldsTest", "SubscriberId");



            }
            catch (Exception ex)
            {
                Driver.SwitchToAlert();
                throw new Exception($"Failed to create billing office  request with message : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }
        public void VerifyPANAndSubscriberAlertPresent()
        {
            try
            {

                string panAlerrtMsg = IniHelper.ReadConfig("ROIBillingOfficeLogANewRequestMapBOERequesterRequiredFieldsTest", "PanAlert");
                string subscriberIdAlerrtMsg = IniHelper.ReadConfig("ROIBillingOfficeLogANewRequestMapBOERequesterRequiredFieldsTest", "SubscriberId");

                Driver.Wait(TimeSpan.FromSeconds(2));
                Driver.SwitchTo().Alert().Equals(panAlerrtMsg);
                Driver.Wait(TimeSpan.FromSeconds(2));
                Driver.SwitchTo().Alert().Accept();
                Driver.Wait(TimeSpan.FromSeconds(2));
                Driver.SendKeys(panTxt, "25631425");
                IWebElement logRequest = Driver.FindElementBy(logRequestButton);
                logRequest.Click();
                Driver.Wait(TimeSpan.FromSeconds(2));
                Driver.SwitchTo().Alert().Equals(subscriberIdAlerrtMsg);



            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to verify alert with message : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }


        public void AcceptSubscriberAlert()
        {
            try
            {

                Driver.Wait(TimeSpan.FromSeconds(2));
                Driver.SwitchTo().Alert().Accept();
                Driver.Wait(TimeSpan.FromSeconds(2));
                Driver.SendKeys(subscriberId, "2563142512");
                IWebElement logRequest = Driver.FindElementBy(logRequestButton);
                logRequest.Click();
            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to accept alert message with message : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }

        public void CreateBillingOfficeRequestWithClaimAndReferenceId()
        {
            try
            {
                Driver.Click(billingOffice);
                Driver.Wait(TimeSpan.FromSeconds(2));
                PatientFirstName = "FN" + createdDateTime;
                PatientLastName = "LN" + createdDateTime;
                IWebElement firstName = Driver.FindElementBy(By.Id(PageElements.LogNewRequestPage.firstName_Id));
                firstName.SendKeys(PatientFirstName);
                logger.Log(Status.Info, "First Name entered." + PatientFirstName);
                IWebElement lastName = Driver.FindElementBy(By.Id(PageElements.LogNewRequestPage.lastName_Id));
                lastName.SendKeys(PatientLastName);
                logger.Log(Status.Info, "Last Name entered." + PatientLastName);
                IWebElement dob = Driver.FindElementBy(By.Id(PageElements.LogNewRequestPage.dateOfBith_Id));
                dob.SendKeys(DateTime.Now.AddYears(-25).ToString("MM/dd/yyy"));
                var locationDropDown = Driver.FindElementBy(By.Id(PageElements.LogNewRequestPage.locationDropDown_id));
                locationDropDown.Click();
                Driver.SelectValueFromDD(drpLocation, bostonProperLocation);
                var todaysDate = String.Format("{0:M/dd/yyyy}", DateTime.Now).Replace("-", "/");
                var requestRecievedDate = Driver.FindElementBy(requestReceivedDate);
                requestRecievedDate.SendKeys(todaysDate);
                string boePortalVal = IniHelper.ReadConfig("ROIBillingOfficeLogANewRequestMapBOERequesterRequiredFieldsTest", "RoitCBO");
                SelectElement _boePortalDrp = new SelectElement(Driver.FindElement(boEportalDrp));
                _boePortalDrp.SelectByText(boePortalVal);
                string userVal = IniHelper.ReadConfig("ROIBillingOfficeLogANewRequestMapBOERequesterRequiredFieldsTest", "UserName");
                SelectElement _usernameDrp = new SelectElement(Driver.FindElement(userName));
                _usernameDrp.SelectByText(userVal);
                string payerVal = IniHelper.ReadConfig("ROIBillingOfficeLogANewRequestMapBOERequesterRequiredFieldsTest", "PayerRequester");
                Driver.SendKeys(payerRequester, payerVal);
                Driver.Wait(TimeSpan.FromSeconds(2));
                WebElementHelper helper = new WebElementHelper(Driver, logger, Context);
                helper.ScrollIntoView("mrocontent_cmdLogRequest", FindElementBy.Id);
                IWebElement logRequest = Driver.FindElementBy(logRequestButton);
                logRequest.Click();

                string claimAlerrtMsg = IniHelper.ReadConfig("ROIBillingOfficeLogANewRequestMapBOERequesterRequiredFieldsTest", "ClaimId");
                string referenceIdAlerrtMsg = IniHelper.ReadConfig("ROIBillingOfficeLogANewRequestMapBOERequesterRequiredFieldsTest", "ReferenceId");
                Driver.Wait(TimeSpan.FromSeconds(2));
                Driver.SwitchTo().Alert().Equals(claimAlerrtMsg);
                Driver.Wait(TimeSpan.FromSeconds(2));
                Driver.SwitchTo().Alert().Accept();
                Driver.Wait(TimeSpan.FromSeconds(2));
                Driver.SendKeys(claimId, "25631425");
                Driver.Click(logRequestButton);
                Driver.Wait(TimeSpan.FromSeconds(2));
                Driver.SwitchTo().Alert().Equals(referenceIdAlerrtMsg);


            }
            catch (Exception ex)
            {
                Driver.SwitchToAlert();
                throw new Exception($"Failed to create billing office  request with message : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }


        public void AcceptReferenceIdAlert()
        {
            try
            {

                Driver.Wait(TimeSpan.FromSeconds(2));
                Driver.SwitchTo().Alert().Accept();
                Driver.Wait(TimeSpan.FromSeconds(2));
                Driver.SendKeys(referenceId, "2563142512");
                IWebElement logRequest = Driver.FindElementBy(logRequestButton);
                logRequest.Click();
            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to accept alert message with message : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }
        public Tuple<string, string> CreateNewMRODeliveryRequest()
        {
            try
            {
                PatientFirstName = "FN" + createdDateTime;
                PatientLastName = "LN" + createdDateTime;
                logger.Log(Status.Info, "Create a New MRO Delivery Request");
                IWebElement firstName = Driver.FindElementBy(By.Id(PageElements.LogNewRequestPage.firstName_Id));
                firstName.SendKeys(PatientFirstName);
                logger.Log(Status.Info, "First Name entered." + PatientFirstName);
                IWebElement lastName = Driver.FindElementBy(By.Id(PageElements.LogNewRequestPage.lastName_Id));
                lastName.SendKeys(PatientLastName);
                logger.Log(Status.Info, "Last Name entered." + PatientLastName);
                IWebElement dob = Driver.FindElementBy(By.Id(PageElements.LogNewRequestPage.dateOfBith_Id));
                dob.SendKeys(DateTime.Now.AddYears(-25).ToString("MM/dd/yyy"));
                var locationDropDown = Driver.FindElementBy(By.Id(PageElements.LogNewRequestPage.locationDropDown_id));
                locationDropDown.Click();
                Driver.SelectValueFromDD(drpLocation, bostonProperLocation);
                var todaysDate = String.Format("{0:M/dd/yyyy}", DateTime.Now).Replace("-", "/");
                var requestRecievedDate = Driver.FindElementBy(requestReceivedDate);
                requestRecievedDate.SendKeys(todaysDate);
                WebElementHelper helper = new WebElementHelper(Driver, logger, Context);
                helper.ScrollIntoView("mrocontent_cmdLogRequest", FindElementBy.Id);
                IWebElement logRequest = Driver.FindElementBy(logRequestButton);
                logRequest.Click();
                MenuSelector menuSelector = new MenuSelector(Driver, logger, Context);
                menuSelector.SelectRecentRequestID();
                var tupleData = new Tuple<string, string>(PatientFirstName, PatientLastName);
                return tupleData;
            }
            catch (Exception ex)
            {
                Driver.SwitchToAlert();
                throw new Exception($"Failed to create new MRO delivery request Exception details with Message : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");

            }

        }

        public void CreateDuplicateMRODeliveryRequest(string patientFirstName, string patientLastName, string dateOfBirth)
        {
            try
            {
                logger.Log(Status.Info, "Create a New MRO Delivery Request with duplcate data");
                IWebElement firstName = Driver.FindElementBy(By.Id(PageElements.LogNewRequestPage.firstName_Id));
                firstName.SendKeys(patientFirstName);
                logger.Log(Status.Info, "First Name entered." + patientFirstName);
                IWebElement lastName = Driver.FindElementBy(By.Id(PageElements.LogNewRequestPage.lastName_Id));
                lastName.SendKeys(patientLastName);
                logger.Log(Status.Info, "Last Name entered." + patientLastName);
                IWebElement dob = Driver.FindElementBy(By.Id(PageElements.LogNewRequestPage.dateOfBith_Id));
                dob.SendKeys(dateOfBirth);
                var locationDropDown = Driver.FindElementBy(By.Id(PageElements.LogNewRequestPage.locationDropDown_id));
                locationDropDown.Click();
                Driver.SelectValueFromDD(drpLocation, bostonProperLocation);
                CheckForDuplicates();
                var todaysDate = String.Format("{0:M/dd/yyyy}", DateTime.Now).Replace("-", "/");
                var requestRecievedDate = Driver.FindElementBy(requestReceivedDate);
                requestRecievedDate.SendKeys(todaysDate);
                WebElementHelper helper = new WebElementHelper(Driver, logger, Context);
                helper.ScrollIntoView("mrocontent_cmdLogRequest", FindElementBy.Id);
                IWebElement logRequest = Driver.FindElementBy(logRequestButton);
                logRequest.Click();
                MenuSelector menuSelector = new MenuSelector(Driver, logger, Context);
                menuSelector.SelectRecentRequestID();
            }
            catch (Exception ex)
            {
                Driver.SwitchToAlert();
                throw new Exception($"Failed to create new MRO delivery request with duplicate data Exception details with Message : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");

            }

        }

        public void CreateInternalPortalRequestWithSpecificDetails(string internalPortalVal,string shipToVal)
        {
            try
            {
                PatientFirstName = "FN" + createdDateTime;
                PatientLastName = "LN" + createdDateTime;
                IWebElement firstName = Driver.FindElementBy(By.Id(PageElements.LogNewRequestPage.firstName_Id));
                firstName.SendKeys(PatientFirstName);
                logger.Log(Status.Info, "First Name entered." + PatientFirstName);
                IWebElement lastName = Driver.FindElementBy(By.Id(PageElements.LogNewRequestPage.lastName_Id));
                lastName.SendKeys(PatientLastName);
                logger.Log(Status.Info, "Last Name entered." + PatientLastName);
                IWebElement dob = Driver.FindElementBy(By.Id(PageElements.LogNewRequestPage.dateOfBith_Id));
                dob.SendKeys(DateTime.Now.AddYears(-25).ToString("MM/dd/yyy"));
                var locationDropDown = Driver.FindElementBy(By.Id(PageElements.LogNewRequestPage.locationDropDown_id));
                locationDropDown.Click();
                Driver.SelectValueFromDD(drpLocation, bostonProperLocation);
                Driver.SleepTheThread(3);
                var todaysDate = String.Format("{0:M/dd/yyyy}", DateTime.Now.AddDays(-1)).Replace("-", "/");
                var requestRecievedDate = Driver.FindElementBy(requestReceivedDate);
                requestRecievedDate.SendKeys(todaysDate);
                //string internalPortalVal = IniHelper.ReadConfig("InternalPortalWithComplianceHoldTest", "InternalPortalDrpVal");
                SelectElement _internalPortalDrp = new SelectElement(Driver.FindElement(internalPortalDrp));
                _internalPortalDrp.SelectByText(internalPortalVal);
                string userName = IniHelper.ReadConfig("InternalPortalWithComplianceHoldTest", "InternalPortalUsername");
                SelectElement _usernameDrp = new SelectElement(Driver.FindElement(usernameDrp));
                _usernameDrp.SelectByText(userName);

                var shipToRequesterdrp = Driver.FindElementBy(By.Id(shipToRequesterDrp));
                var selectElement2 = new SelectElement(shipToRequesterdrp);

                //SelectElement shipToReqDrp = new SelectElement(Driver.FindElement(shipToRequesterDrp));
                selectElement2.SelectByText(shipToVal);
                WebElementHelper helper = new WebElementHelper(Driver, logger, Context);
                helper.ScrollIntoView("mrocontent_cmdLogRequest", FindElementBy.Id);
                IWebElement logRequest = Driver.FindElementBy(logRequestButton);
                logRequest.Click();
            }
            catch (Exception ex)
            {
                Driver.SwitchToAlert();
                throw new Exception($"Failed to create new internal delivery request with message : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }
        public String GetPatientFirstName()
        {
            try
            {
                String fn = Driver.FindElement(By.Id("mrocontent_txtFirstName")).GetAttribute("value");
                return fn;
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to accept alert message with message : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }
        public String GetPatientLastName()
        {
            try
            {
                String ln = Driver.FindElement(By.Id("mrocontent_txtLastName")).GetAttribute("value");
                return ln;
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to accept alert message with message : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }

        public void MRODeliveryRequestForBostonProperWithSSNNumber()
        {
            try
            {
                Random rand = new Random();
                int value = rand.Next(10, 1000);
                PatientFirstName = "FN" + createdDateTime;
                PatientLastName = "LN" + value;
                logger.Log(Status.Info, "Create a New MRO Delivery Request.");
                IWebElement firstName = Driver.FindElementBy(By.Id(PageElements.LogNewRequestPage.firstName_Id));
                firstName.SendKeys(PatientFirstName);
                logger.Log(Status.Info, "First Name entered." + PatientFirstName);
                IWebElement lastName = Driver.FindElementBy(By.Id(PageElements.LogNewRequestPage.lastName_Id));
                lastName.SendKeys(PatientLastName);
                logger.Log(Status.Info, "Last Name entered." + PatientLastName);

                Driver.SendKeys(ssnNumber, "001-10-1236");
                IWebElement dob = Driver.FindElementBy(By.Id(PageElements.LogNewRequestPage.dateOfBith_Id));
                dob.SendKeys(DateTime.Now.AddYears(-25).ToString("MM/dd/yyy"));
                var locationDropDown = Driver.FindElementBy(By.Id(PageElements.LogNewRequestPage.locationDropDown_id));
                locationDropDown.Click();
                Driver.SelectValueFromDD(drpLocation, bostonProperLocation);
                CheckForDuplicates();
                Driver.SleepTheThread(2);
                var todaysDate = String.Format("{0:M/dd/yyyy}", DateTime.Now).Replace("-", "/");
                var requestRecievedDate = Driver.FindElementBy(requestReceivedDate);
                requestRecievedDate.SendKeys(todaysDate);
                WebElementHelper helper = new WebElementHelper(Driver, logger, Context);
                helper.ScrollIntoView("mrocontent_cmdLogRequest", FindElementBy.Id);
                IWebElement logRequest = Driver.FindElementBy(logRequestButton);
                logRequest.Click();


            }
            catch (Exception ex)
            {
                Driver.SwitchToAlert();
                throw new Exception($"Failed to create new MRO delivery request Exception details with Message : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");

            }

        }

        public void ImportRequestPages()
        {
            try
            {


                Driver.ScrollToEndOfThePage();
                Driver.DirectClick(importPdf);
                Driver.SwitchTo().Frame(importPDFFrame);


                string fulldirectory = SeleniumHelper.ROITestFileUploads.LocationPath;
                string requesPagesFileName = $"\\{importRequestPDFFile}";
                string requestDocsPath = fulldirectory + requesPagesFileName;

                Driver.FindElementBy(selectFileButton).SendKeys(requestDocsPath);

                Driver.ClickAndCheckNextElement(importPDFButton, importCloseButton);
                Driver.DirectClick(importCloseButton);
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to accept alert message with message : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }

        public void EpicRef()
        {
            try
            {
                Driver.SendKeys(epicRef, "123456");
                IWebElement completeReq = Driver.FindElementBy(completeRequest);
                completeReq.Click();
                Driver.SwitchTo().Alert().Accept();
            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to accept alert message with message : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }

        public Tuple<string, string> CreateNewMRODeliveryRequestForNativePDFTest()
        {
            try
            {
                PatientFirstName = "FN" + createdDateTime;
                PatientLastName = "LN" + createdDateTime;
                logger.Log(Status.Info, "Create a New MRO Delivery Request");
                IWebElement firstName = Driver.FindElementBy(By.Id(PageElements.LogNewRequestPage.firstName_Id));
                firstName.SendKeys(PatientFirstName);
                logger.Log(Status.Info, "First Name entered." + PatientFirstName);
                IWebElement lastName = Driver.FindElementBy(By.Id(PageElements.LogNewRequestPage.lastName_Id));
                lastName.SendKeys(PatientLastName);
                logger.Log(Status.Info, "Last Name entered." + PatientLastName);
                IWebElement dob = Driver.FindElementBy(By.Id(PageElements.LogNewRequestPage.dateOfBith_Id));
                dob.SendKeys(DateTime.Now.AddYears(-25).ToString("MM/dd/yyy"));
                var locationDropDown = Driver.FindElementBy(By.Id(PageElements.LogNewRequestPage.locationDropDown_id));
                locationDropDown.Click();
                Driver.SelectValueFromDD(drpLocation, nativePDFTestLocation);
                var todaysDate = String.Format("{0:M/dd/yyyy}", DateTime.Now).Replace("-", "/");
                var requestRecievedDate = Driver.FindElementBy(requestReceivedDate);
                requestRecievedDate.SendKeys(todaysDate);
                WebElementHelper helper = new WebElementHelper(Driver, logger, Context);
                helper.ScrollIntoView("mrocontent_cmdLogRequest", FindElementBy.Id);
                IWebElement logRequest = Driver.FindElementBy(logRequestButton);
                logRequest.Click();
                MenuSelector menuSelector = new MenuSelector(Driver, logger, Context);
                menuSelector.SelectRecentRequestID();
                var tupleData = new Tuple<string, string>(PatientFirstName, PatientLastName);
                return tupleData;
            }
            catch (Exception ex)
            {
                Driver.SwitchToAlert();
                throw new Exception($"Failed to create new MRO delivery request with exception message : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");

            }

        }

        public void CreateDuplicateMRODeliveryRequestForNativePDFTest(string patientFirstName, string patientLastName, string dateOfBirth)
        {
            try
            {
                logger.Log(Status.Info, "Create a New MRO Delivery Request with duplcate data");
                IWebElement firstName = Driver.FindElementBy(By.Id(PageElements.LogNewRequestPage.firstName_Id));
                firstName.SendKeys(patientFirstName);
                logger.Log(Status.Info, "First Name entered." + patientFirstName);
                IWebElement lastName = Driver.FindElementBy(By.Id(PageElements.LogNewRequestPage.lastName_Id));
                lastName.SendKeys(patientLastName);
                logger.Log(Status.Info, "Last Name entered." + patientLastName);
                IWebElement dob = Driver.FindElementBy(By.Id(PageElements.LogNewRequestPage.dateOfBith_Id));
                dob.SendKeys(dateOfBirth);
                var locationDropDown = Driver.FindElementBy(By.Id(PageElements.LogNewRequestPage.locationDropDown_id));
                locationDropDown.Click();
                Driver.SelectValueFromDD(drpLocation,nativePDFTestLocation);
                CheckForDuplicates();
                var todaysDate = String.Format("{0:M/dd/yyyy}", DateTime.Now).Replace("-", "/");
                var requestRecievedDate = Driver.FindElementBy(requestReceivedDate);
                requestRecievedDate.SendKeys(todaysDate);
                WebElementHelper helper = new WebElementHelper(Driver, logger, Context);
                helper.ScrollIntoView("mrocontent_cmdLogRequest", FindElementBy.Id);
                IWebElement logRequest = Driver.FindElementBy(logRequestButton);
                logRequest.Click();
                MenuSelector menuSelector = new MenuSelector(Driver, logger, Context);
                menuSelector.SelectRecentRequestID();
            }
            catch (Exception ex)
            {
                Driver.SwitchToAlert();
                throw new Exception($"Failed to create new MRO delivery request with duplicate data exception Message : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");

            }

        }


        /// <summary>
        /// Create a billing office request
        /// </summary>
        public void CreateBillingOfficeRequest(string BOEPortal)
        {
            try
            {
                Driver.Click(billingOfficeLink);

                var selectBOEPortalDropDown = Driver.FindElementBy(selectBOEPortal);
                var selectElement = new SelectElement(selectBOEPortalDropDown);
                selectElement.SelectByText(BOEPortal);
                Driver.Wait(TimeSpan.FromSeconds(1));

                var usernameDropDown = Driver.FindElementBy(selectUserForBOEPortal);
                var selectElement1 = new SelectElement(usernameDropDown);
                //selectElement1.SelectByText(userNameDDValue);
                selectElement1.SelectByText("<Any User>");

                /*
                var selectPayerRequestDropDown = Driver.FindElementBy(selectPayerRequest);
                var selectElementForPayerReq = new SelectElement(selectPayerRequestDropDown);
                selectElementForPayerReq.SelectByText("BOE FTP Test");
                Driver.WaitInSeconds(1);
                */

                PatientFirstName = "FN" + createdDateTime;
                PatientLastName = "LN" + createdDateTime;
                Driver.FindElementBy(By.Id(PageElements.LogNewRequestPage.firstName_Id)).SendKeys(PatientFirstName);
                logger.Log(Status.Info, $"First name entered. ({PatientFirstName})");
                Driver.FindElementBy(By.Id(PageElements.LogNewRequestPage.lastName_Id)).SendKeys(PatientLastName);
                logger.Log(Status.Info, $"Last name entered. ({PatientLastName})");
                Driver.FindElementBy(By.Id(PageElements.LogNewRequestPage.dateOfBith_Id)).SendKeys(DateTime.Now.AddYears(-25).ToString("MM/dd/yyy"));

                csvReader = new CSVReader(Path.GetFullPath(Path.Combine(Assembly.GetExecutingAssembly().Location, "..", "TestData", csvFileName)));
                string locationValue = csvReader.GetDataFromCSVFile("locationDDValue");
                var locationDropDown = Driver.FindElementBy(By.Id(PageElements.LogNewRequestPage.locationDropDown_id));
                locationDropDown.Click();
                Driver.SelectValueFromDD(drpLocation, locationValue);

                var todaysDate = String.Format("{0:M/dd/yyyy}", DateTime.Now).Replace("-", "/");
                var requestRecievedDate = Driver.FindElementBy(requestReceivedDate);
                requestRecievedDate.SendKeys(todaysDate);

                WebElementHelper helper = new WebElementHelper(Driver, logger, Context);

                helper.ScrollIntoView("mrocontent_cmdLogRequest", FindElementBy.Id);
                Driver.FindElementBy(logRequestButton).Click();
                Iframe frame = new Iframe(Driver, logger, Context);
                frame.switchToDefaut();
                MenuSelector menuSelector = new MenuSelector(Driver, logger, Context);
                menuSelector.SelectRecentRequestID();
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to create new internal portal Request id : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }


    }
}