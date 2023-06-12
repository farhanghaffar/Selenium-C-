using AventStack.ExtentReports;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MRO.ROI.Automation.Common.Navigation;
using MRO.ROI.Automation.Pages;
using MRO.ROI.Automation.Pages.Common;
using MRO.ROI.Automation.Selenium;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.UI;
using System;
using static MRO.ROI.Automation.Utility.IniFile;

namespace MRO.ROI.Automation.Pages
{
    public class ROIAdminHomePage
    {
        public RemoteWebDriver Driver;
        public ExtentTest logger;
        public TestContext Context;
        public ROIAdminHomePage(RemoteWebDriver driver, ExtentTest _loger, TestContext _context)
        {
            Driver = driver;
            logger = _loger;
            Context = _context;
        }

        public By lookupRequestIdElement = By.XPath("//img[@title='Look up by Request ID']");
        public By LookUpRequestId = By.XPath("//a[@id='mroheader_ctl02_ctl03_imgQuery']");
        public By HomePageHeader = By.XPath("//td[@id ='MasterHeaderText']");
        public By Facility = By.XPath("//td[@id='mroheader_ctl02_ctl01_ctl00_mnuMainMenu-menuItem014']");
        public By Facilitylist = By.XPath("//td[@id='mroheader_ctl02_ctl01_ctl00_mnuMainMenu-menuItem014-subMenu-menuItem000']");
        public By financialTab = By.XPath("(//td[@class='mnuStyle' and contains(text(),'Financial')])[2]");
        public By statementsORInvoices = By.XPath("//td[contains(text(), 'Statement/Invoice')]");
        public By roiInvoice = By.XPath("(//td[contains(text(),'ROI Invoice')])[1]");
        public By monthlyStatements = By.XPath("//td[contains(text(),'Monthly Statement')]");
        public By roiAdmin = By.XPath("//td[contains(text(),'ROIAdmin')]");
        public const string automattedLogging = "//td[contains(text(),'Automated Logging')]";
        public By automatedLoggingElement = By.XPath("//td[contains(text(),'Automated Logging')]");
        public By system = By.XPath("(//td[starts-with(text(),'System')])[1]");
        public By manage = By.XPath("//*[contains(text(),'Manage >')]");
        public By systemInfo = By.XPath("//td[contains(text(),'System Info')]");
        public By BatchScan = By.XPath("//td[@id='mroheader_ctl02_ctl01_ctl00_mnuMainMenu-menuItem000-subMenu-menuItem017']");
        public By requestIdTxtBox = By.XPath("//input[@type='number']");
        public By searchBtn = By.XPath("(//span[contains(text(),'Search')])[1]");
        public By roiAdminmenu = By.XPath("//a[contains(text(),'ROIAdmin')]");
        public By rssRequestStatus = By.XPath("//a[contains(text(),'RSS Request Status')]");
        public By dob = By.XPath("//label[contains(text(),'DOB')]");
        public By dobSearchBox = By.XPath("//*[@id='app']/div[1]/main/div/div/div[2]/div/div[2]/div[2]/div[1]/div/div[3]/div[2]");
        public By findRequestBtn = By.XPath("//span[contains(text(),'Find a New Request')]");
        public By reAssignRequesterTxt = By.XPath("//*[@id='app']/div[1]/main/div/div/div[2]/div/div[2]/div[2]/div[2]/div/div[1]/div[1]/div[2]/span");
        public By shipToTxt = By.XPath("//span[contains(text(),'TEST ABC')]");
        public const string actionSummaryViewAll = "//td[contains(text(),'Action Summary View All')]";
        public By selectingUserId = By.XPath("//select[@id='mrocontent_ctlUsers_lstAvailable']//option[text()='Kothuri, Anjali (cigniti-akothuri)']");
        public By addButton = By.Id("mrocontent_ctlUsers_cmdAdd");
        public By categoriesLink = By.XPath("//td[text()='Categories']");
        public By removeButton = By.Id("mrocontent_ctlUsers_cmdRemove");
        public By facilityDropdown = By.XPath("//div[@id='mrocontent_pnlUsers']//table//select[@id='mrocontent_ctlUsers_lstFac']");
        public By firstName = By.XPath("//input[@id='mrocontent_txtFirstName']");
        public By lastName = By.XPath("//input[@id='mrocontent_txtLastName']");
        public By searchlistBtn = By.XPath("//input[@id='mrocontent_cmdSearch']");
        public By userId = By.XPath("//table[@id='mrocontent_dgROIAdamin']//tbody//tr[2]//td[1]");
        public By userIdTxtBox = By.XPath("//input[@id='mrocontent_txtUserID']");
        public By summaryBtn = By.XPath("//input[@id='mrocontent_cmdCreateSummary']");
        public By count = By.XPath("//table[@id='mrocontent_tblSummary']//tbody//tr[2]//td[2]");
        public By AuditLog = By.XPath("//td[@id='mroheader_ctl02_ctl01_ctl00_mnuMainMenu-menuItem000-subMenu-menuItem006']");
        public By reportsElement = By.XPath("//td[starts-with(text(),'Reports')]");
        public By issuesInvoiceElement = By.XPath("//td[contains(text(),'Issues/Invoices >')]");
        public By validationReportElement = By.XPath("//td[@title='Issue Validation Report']");
        public By listElement = By.XPath("//td[contains(text(),'Lists >')]");
        public By actionListElement = By.XPath("//td[contains(text(),'Actions List')] ");
        public By invoicingQueueElement = By.XPath("//*[contains(text(),'Invoicing Queue')] ");
        public By requesterElement = By.XPath("//td[contains(text(),'Requesters')]");
        public By requesterList = By.XPath("//td[contains(text(),' Requester List')]");


        public By clearFields = By.XPath("//input[@id='mrocontent_cmdNewSearch']");
        public By referenceID = By.XPath("//input[@id='mrocontent_txtRefID']");
        public By facilitylist = By.XPath("//Select[@id='mrocontent_lstFacilities']");
        public By IncludeTest = By.XPath("//input[@id='mrocontent_cbTest']");
        public By SearchBtn = By.XPath("//input[@id='mrocontent_cmdSearch']");
        public By emailLoc = By.XPath("//input[@id='mrocontent_txtRequeterEmail']");

        public By managehierarchy = By.XPath("//td[contains(text(),'Management Hierarchy Report')]");
        public By lookUpIdInput = By.XPath("//input[@id='lookupmessage']");
        public By okButton = By.XPath("//button[text()='OK']");
        public By patientFullName = By.XPath("//*[@id='mrocontent_lblPatientName']");
        public By ROIBtnInFacilityFeatures = By.XPath("//span[text()='ROI']");
        public By autoMRNLookup = By.XPath("//input[@id='mrocontent_cbAutoMRNLookup']");
        public By updateFeature = By.XPath("//input[@id='mrocontent_cmdUpdate']");

        /// <summary>
        /// Go to Contract List Page
        /// </summary>
        public ROIAdminContractListPage ContractList()
        {
            try
            {
                ROIMenuSelector menu = new ROIMenuSelector(Driver, logger, Context);
                menu.SelectRoiAdmin("Facilities", "Contract List");
            }

            catch (Exception ex)
            {
                throw new Exception($"Failed  to navigate Contract list with details as Message : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
            return new ROIAdminContractListPage(Driver, logger, Context);
        }

        /// <summary>
        /// Go to  Facility List  Page
        /// </summary>
        public ROIAdminFacilityListPage FacilityList()
        {
            try
            {
                ROIMenuSelector menu = new ROIMenuSelector(Driver, logger, Context);
                menu.SelectRoiAdmin("Facilities", "Facility List");
                Driver.WaitUntilDOMLoaded();
            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to click Facilities list   with exception details as : {ex.Message}");

            }

            return new ROIAdminFacilityListPage(Driver, logger, Context);
        }


        /// <summary>
        /// Navigation to Request status for given request id
        /// </summary>
        /// <param name="by"><see cref="rqsID"/></param>

        public ROIAdminRequestStatusPage ROIlookupByRequestId(string rqsID)
        {
            try
            {
                try
                {
                    Driver.SwitchTo().DefaultContent();
                    Driver.Click(lookupRequestIdElement);
                    Driver.SwitchTo().Alert().SendKeys(rqsID);
                    Driver.SwitchTo().Alert().Accept();
                    Driver.SleepTheThread(5);

                }
                catch(Exception ex1)
                {
                    Driver.SwitchTo().DefaultContent();
                    By lookupRequestIdElement2 = By.XPath("(//div[@class='toolbar-icons']//button)[2]");
                    Driver.Click(lookupRequestIdElement2);
                    try
                    {
                        Driver.FindElementBy(lookUpIdInput).SendKeys(rqsID);
                        Driver.FindElementBy(okButton).Click();
                    }
                    catch (Exception ex2)
                    {
                        By fullRequestStatusButton = By.XPath("//button[contains(text(),'FULL REQ STATUS')]");
                        By lookUpIdInput2 = By.XPath("//input[@id='fname']");
                        Driver.FindElementBy(lookUpIdInput2).SendKeys(rqsID);
                        Driver.FindElementBy(fullRequestStatusButton).Click();
                    }
                }

            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to navigated to request status page with details as Message : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
            return new ROIAdminRequestStatusPage(Driver, logger, Context);
        }


        /// <summary>
        /// Select ROI Admin Menu 
        /// </summary>        
        public ROIAdminFacilityListPage SelectFacilityList()
        {
            try
            {
                ROIMenuSelector menu = new ROIMenuSelector(Driver, logger, Context);
                try
                {
                    menu.SelectRoiAdmin("Facilities", "Facility List");
                }
                catch (Exception ex)
                {
                    menu.SelectRoiAdminMenuOptions("mnuROIAdmin", "Facilities", "Facility List");
                }

            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to select Facility List as : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }

            return new ROIAdminFacilityListPage(Driver, logger, Context);
        }


        /// <summary>
        /// Go to TPAActivity Report page
        /// </summary>        
        public ROIAdminTPAActivityReportPage GoToTPAActivityReport()
        {
            try
            {
                ROIMenuSelector menu = new ROIMenuSelector(Driver, logger, Context);
                menu.SelectRoiAdmin("Reports", "TPA Activity Report");
            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to go to TPA Activity report page : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }

            return new ROIAdminTPAActivityReportPage(Driver, logger, Context);
        }

        /// <summary>
        /// Verify Page Header
        /// </summary>
        public void VerifyHeader()
        {
            try
            {
                string headerText = Driver.FindElementBy(HomePageHeader).Text;
                Assert.AreEqual(headerText, "Find a Request L2 RS");
            }
            catch (Exception ex)
            {

                throw new Exception($"Failed with Message Unable to verify Header : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }

        /// <summary>
        /// Clicks on the '?', enter request and accept Alert 
        /// </summary>
        /// 
        public void SearchByRequestId(string RequestId)
        {
            try
            {
                IWebElement requestId = Driver.FindElementBy(LookUpRequestId);
                requestId.Click();
                Driver.SwitchTo().Alert().SendKeys(RequestId);
                logger.Log(Status.Info, $"Entered request id ({RequestId})");
                Driver.SwitchTo().Alert().Accept();
                Driver.Wait(TimeSpan.FromSeconds(2));
               
            }
            catch (Exception ex)
            {

                throw new Exception($"Failed with Message not able to click '?' : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }


        /// <summary>
        /// Mousehovers to Facility and clicks on Facility List
        /// </summary>
        public void ClickFacilityList()
        {
            try
            {
                IWebElement facility = Driver.FindElementBy(Facility);
                facility.Click();
                IWebElement facilityList = Driver.FindElementBy(Facilitylist);
                facilityList.Click();
            }
            catch (Exception ex)
            {

                throw new Exception($"Failed with Message unable to click on Facility List '?' : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }

        public void ClickBatchScan()
        {
            try
            {
                IWebElement RoiAdmin = Driver.FindElementBy(roiAdmin);
                RoiAdmin.Click();
                IWebElement batchScan = Driver.FindElementBy(BatchScan);
                batchScan.Click();
            }
            catch (Exception ex)
            {

                throw new Exception($"Failed with Message unable to click on batch scan '?' : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }

        /// <summary>
        /// Go to Facility Policy  Page
        /// </summary>
        public ROIAdminFacilityPoliciesPage FacilityPolicies()
        {
            try
            {
                MenuSelector menuSelector = new MenuSelector(Driver, logger, Context);
                menuSelector.SelectRoiAdmin("System", "Facility Policies");
            }

            catch (Exception ex)
            {
                throw new Exception($"Failed  to click Facility Policies  with details as Message : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
            return new ROIAdminFacilityPoliciesPage(Driver, logger, Context);
        }

        /// <summary>
        /// Clicking on roi invoice button
        /// </summary>
        public void ClickROIInvoice()
        {
            try
            {
                Actions action = new Actions(Driver);
                IWebElement byFinancial = Driver.FindElementBy(financialTab, 10);
                action.MoveToElement(byFinancial).Perform();
                IWebElement byStatementsORInvoice = Driver.FindElementBy(statementsORInvoices, 10);
                action.MoveToElement(byStatementsORInvoice).Perform();
                IWebElement byROIInvoice = Driver.FindElement(roiInvoice);
                action.MoveToElement(byROIInvoice).Click().Build().Perform();

            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to click ROI Invoice : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }

        }

        public void ClickOnShipmentsReports()
        {
            try
            {
                ROIMenuSelector menu = new ROIMenuSelector(Driver, logger, Context);
                menu.SelectRoiAdmin("Shipping", "Shipments Report");
            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to click on shipment reports : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }

        /// <summary>
        /// Switch to new window and login as roi admin
        /// </summary>
        public void SwitchToNewTabAndLoginROIAdmin(string url)
        {

            try
            {

                Driver.SleepTheThread(10);
                RemoteWebDriver dr = Driver;
                IJavaScriptExecutor js = (IJavaScriptExecutor)dr;
                ((IJavaScriptExecutor)js).ExecuteScript("window.open('" + url + "');");
                string tab1 = Driver.WindowHandles[0];
                string tab2 = Driver.WindowHandles[1];
                Driver.SwitchTo().Window(tab2);
                Driver.SleepTheThread(10);
                LoginPage loginPage = new LoginPage(Driver, logger, Context);
                loginPage.GoToROIAdminLoginPage(url);
                ROIAdminLoginForSpecificUser();
                Driver.WaitUntilDOMLoaded();

            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to click ROI Invoice : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }

        public void ROIAdminLoginForSpecificUser()
        {
            Driver.FindElementBy(By.XPath(PageElements.ROIAdminLoginPage.roiAdminUsername_xpath)).Clear();
            Driver.FindElementBy(By.XPath(PageElements.ROIAdminLoginPage.roiAdminUsername_xpath)).SendKeys(Context.Properties["adminUserName"].ToString());
            Driver.FindElementBy(By.XPath(PageElements.ROIAdminLoginPage.roiAdminPassword_xpath)).SendKeys(Context.Properties["adminPassword"].ToString());
            Driver.FindElementBy(By.XPath(PageElements.ROIAdminLoginPage.okButton_xpath)).Click();
        }

        /// <summary> 
        /// Go to  Deferred Revenue Rollover Report page
        /// </summary>
        public void ClickOnDeferredRevenueRolloverReport()
        {
            try
            {

                ROIMenuSelector menu = new ROIMenuSelector(Driver, logger, Context);
                menu.SelectRoiAdmin("Financial", "Deferred Revenue Rollover Report");
                Driver.WaitUntilDOMLoaded();
                logger.Log(Status.Info, "Navigate to > Financial > Deferred Revenue Rollover Report");
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to click DeferredRevenueRolloverReport   with exception details as : {ex.Message}");

            }

        }

        /// <summary>
        /// Switch to new window and login as roi admin
        /// </summary>
        public bool SwitchToNewTabAndLoginROIFacility(string url)
        {
            try
            {
                Driver.SleepTheThread(5);
                RemoteWebDriver dr = Driver;
                IJavaScriptExecutor js = (IJavaScriptExecutor)dr;
                ((IJavaScriptExecutor)js).ExecuteScript("window.open('" + url + "');");
                string adminTab = Driver.WindowHandles[0];
                string facilityTab = Driver.WindowHandles[1];
                Driver.SwitchTo().Window(facilityTab);
                Driver.SleepTheThread(10);
                LoginPage loginPage = new LoginPage(Driver, logger, Context);
                loginPage.GoToROIFaclityLoginPage(url,"test");
                ROIFacilityLoginForSpecificUser();
                Driver.SleepTheThread(90);
                WebElementHelper helper = new WebElementHelper(Driver, logger, Context);
               // bool isUsersMenuVisible = helper.IsElementPresent("//td[starts-with(@id,'mroheader_MROPageHead1') and text()='Users']");
                bool isUsersMenuVisible = helper.IsElementPresent("//a[starts-with(text(), 'Users')]");
                return isUsersMenuVisible;
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to switch to another tab : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }

        public void ROIFacilityLoginForSpecificUser()
        {
            Driver.FindElementBy(By.XPath(PageElements.ROIAdminLoginPage.roiAdminUsername_xpath)).SendKeys(Context.Properties["facilityUserName"].ToString());
            Driver.FindElementBy(By.XPath(PageElements.ROIAdminLoginPage.roiAdminPassword_xpath)).SendKeys(Context.Properties["facilityPassword"].ToString());
            Driver.FindElementBy(By.XPath(PageElements.ROIAdminLoginPage.okButton_xpath)).Click();
        }

        public void SwitchToPreviousTab(string url)
        {
            try
            {
                string adminTab = Driver.WindowHandles[0];
                Driver.SwitchTo().Window(adminTab);
                Driver.SleepTheThread(5);
                LoginPage loginPage = new LoginPage(Driver, logger, Context);
                loginPage.GoToROIAdminLoginPage(url);
                ROIAdminLoginForSpecificUser();
                Driver.WaitUntilDOMLoaded();
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to switch to another tab : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }

        public void ClickTransactionalModelReport()
        {
            ROIMenuSelector menu = new ROIMenuSelector(Driver, logger, Context);
            menu.SelectRoiAdmin("Financial", "Transactional Model Report");
        }

        public void OpenNewTabAndLoginROIFacility(string url)
        {
            try
            {
                Driver.SleepTheThread(5);
                RemoteWebDriver dr = Driver;
                IJavaScriptExecutor js = (IJavaScriptExecutor)dr;
                ((IJavaScriptExecutor)js).ExecuteScript("window.open('" + url + "');");
                string adminTab = Driver.WindowHandles[0];
                string facilityTab = Driver.WindowHandles[1];
                Driver.SwitchTo().Window(facilityTab);
                Driver.SleepTheThread(10);
                LoginPage loginPage = new LoginPage(Driver, logger, Context);
                loginPage.GoToROIFaclityLoginPage(url);
                ROIFacilityLoginForSpecificUser();
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to switch to another tab : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }
        public void ClickOnMonthlyStatements()
        {
            try
            {
                Actions action = new Actions(Driver);
                IWebElement byFinancial = Driver.FindElementBy(financialTab, 10);
                action.MoveToElement(byFinancial).Perform();
                IWebElement byStatementsORInvoice = Driver.FindElementBy(statementsORInvoices, 10);
                action.MoveToElement(byStatementsORInvoice).Perform();
                IWebElement byMonthlyStatements = Driver.FindElement(monthlyStatements);
                action.MoveToElement(byMonthlyStatements).Click().Build().Perform();

            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to click Monthly statements with details message as : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }

        }

        public bool VerifyAutomatedLoggingAtAdminSide()
        {
            try
            {
                Driver.Click(roiAdmin);
                bool isDisplayed = false;
                WebElementHelper helper = new WebElementHelper(Driver, logger, Context);
                bool IsAutomattedLogging = helper.IsElementPresent(automattedLogging);
                if (IsAutomattedLogging == true)
                {

                    ROIAdminFeaturesPage rOIAdminFeaturesPage = new ROIAdminFeaturesPage(Driver, logger, Context);
                    SelectFeatures();
                    rOIAdminFeaturesPage.ClickOnLoggingDashboard();
                    rOIAdminFeaturesPage.AddLoggingDashboardAtAdminSide("Remove");
                    Driver.Click(roiAdmin);
                    bool IsAutomattedLogging1 = helper.IsElementPresent(automattedLogging);
                    return IsAutomattedLogging1;

                }
                else
                {
                    isDisplayed = false;
                }
                return isDisplayed;
            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to click roi admin with details message as : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }

        }
        public void ClickOnAutomattedLogging()
        {
            try
            {
                Driver.Click(roiAdmin);
                Driver.Click(automatedLoggingElement);

            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to click automated logging with details message as : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }

        public void SelectFeatures()
        {
            try
            {
                ROIMenuSelector menu = new ROIMenuSelector(Driver, logger, Context);
                menu.SelectRoiAdmin("Users", "Features");
            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to select features with details message as : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }

        public string VerifyAIDashboardPage()
        {
            try
            {
                return Driver.GetText(HomePageHeader);
            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to verify dashboard page with details message as : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }

        }

        public void SwitchBackToFacilitySide(string url)
        {
            try
            {

                string adminTab = Driver.WindowHandles[0];
                string facilityTab = Driver.WindowHandles[1];
                Driver.SwitchTo().Window(facilityTab);
                Driver.SleepTheThread(10);
                LoginPage loginPage = new LoginPage(Driver, logger, Context);
                loginPage.GoToROIFaclityLoginPage(url);
                ROIFacilityLoginForSpecificUser();
                Driver.SleepTheThread(10);

            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to switch to facility  tab  with details as: {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }
        /// <summary>
        /// Switch to new window and login as roi facility
        /// </summary>
        public void SwitchToNewTabAndLoginROIInternalFacility(string url)
        {
            try
            {
                Driver.SleepTheThread(10);
                RemoteWebDriver dr = Driver;
                IJavaScriptExecutor js = (IJavaScriptExecutor)dr;
                ((IJavaScriptExecutor)js).ExecuteScript("window.open('" + url + "');");
                string tab1 = Driver.WindowHandles[0];
                string tab2 = Driver.WindowHandles[1];
                Driver.SwitchTo().Window(tab2);
                Driver.SleepTheThread(10);
                LoginPage loginPage = new LoginPage(Driver, logger, Context);
                loginPage.GoToROIInternalRequesterPortal(url);
                LoginROIFacilityInternalPortal();
                Driver.WaitUntilDOMLoaded();
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to switch to new tab and login as a roi internal facility : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }

        public void LoginROIFacilityInternalPortal()
        {
            Driver.FindElementBy(By.XPath(PageElements.ROIAdminLoginPage.roiAdminUsername_xpath)).SendKeys(Context.Properties["intPortalUserName"].ToString());
            Driver.FindElementBy(By.XPath(PageElements.ROIAdminLoginPage.roiAdminPassword_xpath)).SendKeys(Context.Properties["intPortalPassword"].ToString());
            Driver.FindElementBy(By.XPath(PageElements.ROIAdminLoginPage.okButton_xpath)).Click();
        }

        public void SwitchToNewTabROIInternalFacility(string url)
        {
            try
            {
                Driver.SleepTheThread(5);
                RemoteWebDriver dr = Driver;
                string tab1 = Driver.WindowHandles[0];
                string tab2 = Driver.WindowHandles[1];
                Driver.SwitchTo().Window(tab2);
                Driver.SleepTheThread(5);
                LoginPage loginPage = new LoginPage(Driver, logger, Context);
                loginPage.LogOut();
                loginPage.GoToROIInternalRequesterPortal(url);
                LoginROIFacilityInternalPortal();
                Driver.WaitUntilDOMLoaded();

            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to switch to new tab and login as a roi internal facility : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }

        public void SelectReconciliationReport()
        {
            try
            {
                ROIMenuSelector menu = new ROIMenuSelector(Driver, logger, Context);
                menu.SelectRoiAdminMenuOptions("mnuROIAdmin", "Reports", "Reconciliation Report");
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to click on Reconciliation Report : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }

        public string VerifyFacilityPoliciesHeader()
        {
            try
            {
                return Driver.GetText(HomePageHeader);

            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to return page header with exception details as : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }
        public bool AddAutomatedLogging()
        {
            try
            {
                Driver.Click(roiAdmin);
                bool isDisplayed = false;
                WebElementHelper helper = new WebElementHelper(Driver, logger, Context);
                bool IsAutomattedLogging = helper.IsElementPresent(automattedLogging);
                if (IsAutomattedLogging == false)
                {
                    ROIAdminFeaturesPage rOIAdminFeaturesPage = new ROIAdminFeaturesPage(Driver, logger, Context);
                    SelectFeatures();
                    rOIAdminFeaturesPage.ClickOnLoggingDashboard();
                    rOIAdminFeaturesPage.AddLoggingDashboardAtAdminSide("Add");
                    isDisplayed = true;
                }
                else
                {
                    isDisplayed = true;
                }
                return isDisplayed;
            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to click roi admin with details message as : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }

        }

        public void LoginROIExternalRequesterlPortal()
        {
            Driver.FindElementBy(By.XPath(PageElements.ROIAdminLoginPage.roiAdminUsername_xpath)).SendKeys(Context.Properties["extPortalUserName"].ToString());
            Driver.FindElementBy(By.XPath(PageElements.ROIAdminLoginPage.roiAdminPassword_xpath)).SendKeys(Context.Properties["extPortalPassword"].ToString());
            Driver.FindElementBy(By.XPath(PageElements.ROIAdminLoginPage.okButton_xpath)).Click();
        }
        public void SwitchToNewTabAndLoginROIRequesterPortal(string url)
        {
            try
            {
                Driver.SleepTheThread(10);
                RemoteWebDriver dr = Driver;
                IJavaScriptExecutor js = (IJavaScriptExecutor)dr;
                ((IJavaScriptExecutor)js).ExecuteScript("window.open('" + url + "');");
                string tab1 = Driver.WindowHandles[0];
                string tab2 = Driver.WindowHandles[1];
                Driver.SwitchTo().Window(tab2);
                Driver.SleepTheThread(10);
                LoginPage loginPage = new LoginPage(Driver, logger, Context);
                loginPage.GoToROIExternalRequesterPortal(url);
                LoginROIExternalRequesterlPortal();
                Driver.WaitUntilDOMLoaded();
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to switch to new tab and login as a roi internal facility : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }
        public void SwitchToNewTabAndLoginROIInternalFacilityBOE(string url)
        {
            try
            {
                Driver.SleepTheThread(10);
                RemoteWebDriver dr = Driver;
                IJavaScriptExecutor js = (IJavaScriptExecutor)dr;
                ((IJavaScriptExecutor)js).ExecuteScript("window.open('" + url + "');");
                string tab1 = Driver.WindowHandles[0];
                string tab2 = Driver.WindowHandles[1];
                Driver.SwitchTo().Window(tab2);
                Driver.SleepTheThread(10);
                LoginPage loginPage = new LoginPage(Driver, logger, Context);
                loginPage.GoToROIInternalRequesterPortal(url);
                LoginROIFacilityInternalPortalBOE();
                Driver.WaitUntilDOMLoaded();
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to switch to new tab and login as a roi internal facility : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }
        public void LoginROIFacilityInternalPortalBOE()
        {
            Driver.FindElementBy(By.XPath(PageElements.ROIAdminLoginPage.roiAdminUsername_xpath)).SendKeys(Context.Properties["boeIntPortalUserName"].ToString());
            Driver.FindElementBy(By.XPath(PageElements.ROIAdminLoginPage.roiAdminPassword_xpath)).SendKeys(Context.Properties["boeIntPortalPassword"].ToString());
            Driver.FindElementBy(By.XPath(PageElements.ROIAdminLoginPage.okButton_xpath)).Click();
        }
        public void SwitchToPreviousTabBOE(string url)
        {
            try
            {
                string adminTab = Driver.WindowHandles[0];
                Driver.SwitchTo().Window(adminTab);
                Driver.SleepTheThread(5);
                LoginPage loginPage = new LoginPage(Driver, logger, Context);
                loginPage.GoToROIAdminLoginPage(url);
                ROIAdminLoginForSpecificUserBOE();
                Driver.WaitUntilDOMLoaded();
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to switch to another tab : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }
        public void ROIAdminLoginForSpecificUserBOE()
        {
            Driver.FindElementBy(By.XPath(PageElements.ROIAdminLoginPage.roiAdminUsername_xpath)).SendKeys(Context.Properties["boeAdminUserName"].ToString());
            Driver.FindElementBy(By.XPath(PageElements.ROIAdminLoginPage.roiAdminPassword_xpath)).SendKeys(Context.Properties["boeAdminPassword"].ToString());
            Driver.FindElementBy(By.XPath(PageElements.ROIAdminLoginPage.okButton_xpath)).Click();
        }
        public void SwitchToNewTabROIInternalFacilityBOE(string url)
        {
            try
            {
                Driver.SleepTheThread(5);
                RemoteWebDriver dr = Driver;
                string tab1 = Driver.WindowHandles[0];
                string tab2 = Driver.WindowHandles[1];
                Driver.SwitchTo().Window(tab2);
                Driver.SleepTheThread(5);
                LoginPage loginPage = new LoginPage(Driver, logger, Context);
                loginPage.LogOut();
                loginPage.GoToROIInternalRequesterPortal(url);
                LoginROIFacilityInternalPortalBOE();
                Driver.WaitUntilDOMLoaded();

            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to switch to new tab and login as a roi internal facility : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }
        public void SwitchBackToRequesterPortal(string url)
        {
            try
            {
                Driver.SleepTheThread(5);
                string requesterPortalTab = Driver.WindowHandles[0];
                Driver.SwitchTo().Window(requesterPortalTab);
                Driver.SleepTheThread(5);
                LoginPage loginPage = new LoginPage(Driver, logger, Context);
                loginPage.GoToROIExternalRequesterPortal(url);
                LoginROIExternalRequesterlPortal();
                Driver.WaitUntilDOMLoaded();

            }

            catch (Exception ex)
            {
                throw new Exception($"Failed to switch to another tab : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }

        public void SelectSystemInfo()
        {
            Actions action = new Actions(Driver);
            Driver.Wait(TimeSpan.FromSeconds(2));
            action.MoveToElement(Driver.FindElementBy(system)).Perform();
            Driver.Wait(TimeSpan.FromSeconds(1));
            action.MoveToElement(Driver.FindElementBy(manage, 10)).Perform();
            Driver.Wait(TimeSpan.FromSeconds(3));
            action.MoveToElement(Driver.FindElement(systemInfo)).Click().Build().Perform();
            Driver.Wait(TimeSpan.FromSeconds(1));
        }

        public void SwitchToROIRequesterPortal(string url)
        {
            try
            {

                Driver.SleepTheThread(5);
                RemoteWebDriver dr = Driver;
                Driver.SleepTheThread(5);
                LoginPage loginPage = new LoginPage(Driver, logger, Context);
                loginPage.GoToROIExternalRequesterPortal(url);
                LoginROIExternalRequesterlPortal();
                Driver.WaitUntilDOMLoaded();
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to switch to new tab and login as a roi requester portal : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }

        public void SwitchToAdminTab(string url)
        {
            try
            {
                string adminTab = Driver.WindowHandles[1];
                Driver.SwitchTo().Window(adminTab);
                Driver.SleepTheThread(5);
                LoginPage loginPage = new LoginPage(Driver, logger, Context);
                loginPage.GoToROIAdminLoginPage(url);
                ROIAdminLoginForSpecificUser();
                Driver.WaitUntilDOMLoaded();
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to switch to another tab : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }

        public void FacilityLocation()
        {
            try
            {
                ROIMenuSelector menu = new ROIMenuSelector(Driver, logger, Context);
                menu.SelectRoiAdmin("Facilities", "Facility Locations");
                Driver.WaitUntilDOMLoaded();
            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to click Facilities location with exception details as : {ex.Message}  {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");

            }

        }
        public void FacilityLocationList()
        {
            try
            {
                ROIMenuSelector menu = new ROIMenuSelector(Driver, logger, Context);
                menu.SelectRoiAdmin("Facilities", "Facilities and Locations List");
                Driver.WaitUntilDOMLoaded();
            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to click Facilities location with exception details as : {ex.Message}  {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");

            }


        }

        public void OpenNewTabAndLoginROITestFacility(string url)
        {
            try
            {
                Driver.SleepTheThread(5);
                RemoteWebDriver dr = Driver;
                IJavaScriptExecutor js = (IJavaScriptExecutor)dr;
                ((IJavaScriptExecutor)js).ExecuteScript("window.open('" + url + "');");
                string adminTab = Driver.WindowHandles[0];
                string facilityTab = Driver.WindowHandles[1];
                Driver.SwitchTo().Window(facilityTab);
                Driver.SleepTheThread(10);
                LoginPage loginPage = new LoginPage(Driver, logger, Context);
                loginPage.GoToROITestFaclityLoginPage(url);
                ROITestFacilityLoginForSpecificUser();
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to switch to another tab : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }


        public void SwitchToROITestFacilitySide(string url)
        {
            try
            {

                string adminTab = Driver.WindowHandles[0];
                string facilityTab = Driver.WindowHandles[1];
                Driver.SwitchTo().Window(facilityTab);
                Driver.SleepTheThread(10);
                LoginPage loginPage = new LoginPage(Driver, logger, Context);
                loginPage.GoToROITestFaclityLoginPage(url);
                ROITestFacilityLoginForSpecificUser();
                Driver.SleepTheThread(10);

            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to switch to facility  tab  with details as: {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }
        public void ROITestFacilityLoginForSpecificUser()
        {
            Driver.FindElementBy(By.XPath(PageElements.ROIAdminLoginPage.roiAdminUsername_xpath)).SendKeys(Context.Properties["roiFacilityUserName"].ToString());
            Driver.FindElementBy(By.XPath(PageElements.ROIAdminLoginPage.roiAdminPassword_xpath)).SendKeys(Context.Properties["roiFacilityPassword"].ToString());
            Driver.FindElementBy(By.XPath(PageElements.ROIAdminLoginPage.okButton_xpath)).Click();
        }

        public void SwitchBackToROITestFacilitySide(string url)
        {
            try
            {

                string facilityTab = Driver.WindowHandles[0];
                string adminTab = Driver.WindowHandles[1];
                Driver.SwitchTo().Window(facilityTab);
                Driver.SleepTheThread(10);
                LoginPage loginPage = new LoginPage(Driver, logger, Context);
                loginPage.GoToROITestFaclityLoginPage(url);
                ROITestFacilityLoginForSpecificUser();
                Driver.SleepTheThread(10);

            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to switch to facility  tab  with details as: {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }


        public void SelectRequesterList()
        {
            try
            {
                ROIMenuSelector menu = new ROIMenuSelector(Driver, logger, Context);
                menu.SelectRoiAdmin("Requesters", "Requester List");
            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to s Requester List  with exception details as : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }

        }

        public void ClickOnActionList()
        {
            ROIMenuSelector menu = new ROIMenuSelector(Driver, logger, Context);
            menu.SelectRoiAdmin("ROIAdmin", "Action List");
        }

        public void SwitchToNewTabAndLoginROITestFacilityCBO(string url)
        {
            try
            {
                Driver.SleepTheThread(10);
                RemoteWebDriver dr = Driver;
                IJavaScriptExecutor js = (IJavaScriptExecutor)dr;
                ((IJavaScriptExecutor)js).ExecuteScript("window.open('" + url + "');");
                string tab1 = Driver.WindowHandles[0];
                string tab2 = Driver.WindowHandles[1];
                Driver.SwitchTo().Window(tab2);
                Driver.SleepTheThread(10);
                LoginPage loginPage = new LoginPage(Driver, logger, Context);
                loginPage.GoToROITestFaclityLoginPage(url);
                LoginROITestFacilityCBO();
                Driver.WaitUntilDOMLoaded();
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to switch to new tab and login as a roi internal facility : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }
        public void LoginROITestFacilityCBO()
        {
            Driver.FindElementBy(By.XPath(PageElements.ROIAdminLoginPage.roiAdminUsername_xpath)).SendKeys(Context.Properties["cboUserName"].ToString());
            Driver.FindElementBy(By.XPath(PageElements.ROIAdminLoginPage.roiAdminPassword_xpath)).SendKeys(Context.Properties["cboPassword"].ToString());
            Driver.FindElementBy(By.XPath(PageElements.ROIAdminLoginPage.okButton_xpath)).Click();
        }
        public void SwitchToPreviousTabCBO(string url)
        {
            try
            {
                string cboTab = Driver.WindowHandles[1];
                Driver.SwitchTo().Window(cboTab);
                Driver.SleepTheThread(5);
                LoginPage loginPage = new LoginPage(Driver, logger, Context);
                loginPage.GoToROITestFaclityLoginPage(url);
                LoginROITestFacilityCBO();
                Driver.WaitUntilDOMLoaded();
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to switch to another tab : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }

        public void ClickOnRequesterServiceRequestStatus()
        {
            try
            {
                ROIMenuSelector menu = new ROIMenuSelector(Driver, logger, Context);
                menu.SelectRoiAdmin("ROIAdmin", "RSS Request Status");
                Driver.WaitUntilDOMLoaded();
            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to click requester service request status : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }

        public void SearchRequest(string requestId)
        {
            try
            {
                Driver.SendKeys(requestIdTxtBox, requestId);
                Driver.Click(searchBtn);

            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to search request : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }

        public string VerifyReAssignRequester()
        {
            try
            {
                string reAssignRequesterValue = Driver.GetText(reAssignRequesterTxt).Trim();
                return reAssignRequesterValue;
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to verify re assign requester: {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }
        public string VerifyShipTo()
        {
            try
            {
                string shipToValue = Driver.GetText(shipToTxt).Trim();
                return shipToValue;
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to verify shipto : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }



        public void GotoActionSummaryReport()
        {
            try
            {
                ROIMenuSelector menu = new ROIMenuSelector(Driver, logger, Context);
                menu.SelectRoiAdmin("Reports", "Action Summary Report");
            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to go to action summary report page : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }

        }

        public string GetUserSummaryReport(string id)
        {
            try
            {
                Driver.ClearText(userIdTxtBox);
                Driver.SendKeys(userIdTxtBox, id);
                Driver.Click(summaryBtn);
                string countval=Driver.GetText(count);
                return countval;

            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to go to action summary report page : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }

        public void GotoAdminList()
        {
            try
            {
                ROIMenuSelector menu = new ROIMenuSelector(Driver, logger, Context);
                menu.SelectRoiAdmin("Users", "Admin List");
            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to go to admin list page : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }

        }
        public void ClickOnSearchList(string fname,string lname)
        {
            try
            {
                Driver.ClearText(firstName);
                Driver.SendKeys(firstName, fname);
                Driver.ClearText(lastName);
                Driver.SendKeys(lastName, lname);
                Driver.Click(searchlistBtn);
                
            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to click on search list: {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }

        }

        public bool VerifyUserDisplayed()
        {
            try
            {
                string table = "//table[@id='mrocontent_tblROIAdmin']";
                WebElementHelper helper = new WebElementHelper(Driver, logger, Context);
                bool isDisplayed = helper.IsElementPresent(table);
                return isDisplayed;

            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to verify search list : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }

        }
        public string GetUserId()
        {
            try
            {
               string id= Driver.GetText(userId);
               return id;
            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to get user id from search list : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }

        public void AddActionSummaryReport(string option)
        {
            try
            {
                string facilityVal = IniHelper.ReadConfig("ROIPivotAICreatePageForDashboardTest", "facility");
                string userName = IniHelper.ReadConfig("ROIPivotAICreatePageForDashboardTest", "username");
                Driver.SendKeys(facilityDropdown, facilityVal);
                string selectedUser1 = $"//select[@id='mrocontent_ctlUsers_lstIncluded']//option[text()='{userName}']";
                WebElementHelper helper = new WebElementHelper(Driver, logger, Context);
                bool isPresent = helper.IsElementPresent(selectedUser1);
                if (option == "Add")
                {
                    if (isPresent == true)
                    {

                        logger.Log(Status.Info, "User added already");
                    }
                    else
                    {

                        Driver.Click(By.XPath($"//option[text()='{userName}']"));
                        Driver.Click(addButton);
                    }

                }

                if (option == "Remove")
                {
                    Driver.Click(By.XPath($"//option[text()='{userName}']"));
                    Driver.Click(removeButton);
                }
                Driver.Click(categoriesLink);
            }

            catch (Exception ex)
            {

                throw new Exception($"Failed to go to action summary report page : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }



        }

        public void TestingFacilityLoginForSpecificUser(string userid)
        {
            Driver.FindElementBy(By.XPath(PageElements.ROIAdminLoginPage.roiAdminUsername_xpath)).SendKeys(userid);
            Driver.FindElementBy(By.XPath(PageElements.ROIAdminLoginPage.roiAdminPassword_xpath)).SendKeys("TestingMRO@123");
            Driver.FindElementBy(By.XPath(PageElements.ROIAdminLoginPage.okButton_xpath)).Click();
        }

        public void OpenNewTabAndLoginROIDummyTestingFacilityUser(string url,string userId)
        {
            try
            {
                Driver.SleepTheThread(5);
                RemoteWebDriver dr = Driver;
                IJavaScriptExecutor js = (IJavaScriptExecutor)dr;
                ((IJavaScriptExecutor)js).ExecuteScript("window.open('" + url + "');");
                string adminTab = Driver.WindowHandles[0];
                string facilityTab = Driver.WindowHandles[1];
                Driver.SwitchTo().Window(facilityTab);
                Driver.SleepTheThread(10);
                LoginPage loginPage = new LoginPage(Driver, logger, Context);
                loginPage.GoToROITestFaclityLoginPage(url);
                TestingFacilityLoginForSpecificUser(userId);
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to switch to another tab : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }

        public void ClickAuditLog()
        {
            try
            {
                IWebElement roiADmin = Driver.FindElementBy(roiAdmin);
                roiADmin.Click();
                IWebElement auditLog = Driver.FindElementBy(AuditLog);
                auditLog.Click();
            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to click on ROIAdmin or Audit log : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }

        public void SelectIssueValidationReport()
        {
            try
            {
                try
                {
                    Driver.Click(By.XPath($"//div[@id='nav']//li//a[contains(text(),'Reports')]"));

                    Actions action = new Actions(Driver);
                    IWebElement byInvoice = Driver.FindElementBy(By.XPath("//a[contains(text(),'Issues/Invoices >')]"), 10);
                    action.MoveToElement(byInvoice).Perform();

                    IWebElement validationReport = Driver.FindElement(By.XPath("//a[contains(text(), 'Issue Validation Report')]"));
                    action.MoveToElement(validationReport).Click().Build().Perform();
                    Driver.MoveToElement(By.XPath("//img[contains(@src, 'MRO-Logo')]"));

                }
                catch (Exception ex)
                {

                    Actions action = new Actions(Driver);
                    IWebElement byReportElement = Driver.FindElementBy(reportsElement, 10);
                    action.MoveToElement(byReportElement).Perform();
                    IWebElement byInvoice = Driver.FindElementBy(issuesInvoiceElement, 10);
                    action.MoveToElement(byInvoice).Perform();
                    IWebElement validationReport = Driver.FindElement(validationReportElement);
                    action.MoveToElement(validationReport).Click().Build().Perform();
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to click issue validation report with details message as : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }

        }

        public void SelectFacilityActionList()
        {
            try
            {
                Actions action = new Actions(Driver);
                IWebElement bySystem = Driver.FindElementBy(system, 5);
                action.MoveToElement(bySystem).Perform();
                IWebElement byListElement = Driver.FindElementBy(listElement, 5);
                action.MoveToElement(byListElement).Perform();
                IWebElement byActionList = Driver.FindElement(actionListElement);
                action.MoveToElement(byActionList).Click().Build().Perform();
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to click on Reconciliation Report : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }


        public void LoginForNewTestFacility(string username,string password)
        {
            Driver.FindElementBy(By.XPath(PageElements.ROIAdminLoginPage.roiAdminUsername_xpath)).SendKeys(username);
            Driver.FindElementBy(By.XPath(PageElements.ROIAdminLoginPage.roiAdminPassword_xpath)).SendKeys(password);
            Driver.FindElementBy(By.XPath(PageElements.ROIAdminLoginPage.okButton_xpath)).Click();
        }


        public void OpenNewTabAndLoginAsNewFacilityUser(string url,string username,string password)
        {
            try
            {
                Driver.SleepTheThread(5);
                RemoteWebDriver dr = Driver;
                IJavaScriptExecutor js = (IJavaScriptExecutor)dr;
                ((IJavaScriptExecutor)js).ExecuteScript("window.open('" + url + "');");
                string adminTab = Driver.WindowHandles[0];
                string facilityTab = Driver.WindowHandles[1];
                Driver.SwitchTo().Window(facilityTab);
                Driver.SleepTheThread(10);
                LoginPage loginPage = new LoginPage(Driver, logger, Context);
                loginPage.GoToROITestFaclityLoginPage(url);
                LoginForNewTestFacility(username,password);
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to switch to another tab : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }


        public void ClickInvoicingQueue()
        {
            try
            {
                Driver.Click(roiAdmin);
                Driver.Click(invoicingQueueElement);
            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to click on invoicing queue with details as : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }

        public void ClearAllFields()
        {
            try
            {
                Driver.ScrollIntoViewAndClick(clearFields);

            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to click on Clear Fields: {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }

        public void setRefAndFacility()
        {
            try
            {
                Driver.SendKeys(referenceID, "67676");
                SelectElement oSelectCarrier = new SelectElement(Driver.FindElementBy(facilitylist));
                oSelectCarrier.SelectByText("ROI Test Facility");
                Driver.Click(IncludeTest);
                Driver.Click(SearchBtn);

            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to set values: {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }

        }

        public void VerifyrequestID(string rqstID)
        {
            try
            {
                Driver.FindElementBy(By.XPath($"//td[contains(text(),'{rqstID}')]"));
                Driver.SleepTheThread(5);

            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to search request: {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }

        public void setReference()
        {
            try
            {
                Driver.SendKeys(referenceID, "67676");
                Driver.Click(SearchBtn);

            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to search request: {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }

        }
        //Written by sandeep
        public void setRequesterEmail(string email)
        {
            Driver.SendKeys(emailLoc, email);
            Driver.Click(SearchBtn);
        }

        //Written by sandeep
        public void SelectManageSystemHierarchy()
        {
            Actions action = new Actions(Driver);
            Driver.Wait(TimeSpan.FromSeconds(2));
            action.MoveToElement(Driver.FindElementBy(system)).Perform();
            Driver.Wait(TimeSpan.FromSeconds(1));
            action.MoveToElement(Driver.FindElementBy(manage, 17)).Perform();
            Driver.Wait(TimeSpan.FromSeconds(3));
            action.MoveToElement(Driver.FindElement(managehierarchy)).Click().Build().Perform();
            Driver.Wait(TimeSpan.FromSeconds(1));
        }


        public void SwitchBackToRoiTestFacilityAndRefresh(string url)
        {
            try
            {

                string facilityTab = Driver.WindowHandles[0];

                Driver.SwitchTo().Window(facilityTab);
                Driver.SleepTheThread(5);
                Driver.Navigate().Refresh();
                Driver.SleepTheThread(5);
                Driver.SwitchTo().Alert().Accept();
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to switch to facility  tab  with details as: {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }

        public ROIAdminFacilityListPage SelectLogoutFromProfile()
        {
            try
            {
                ROIMenuSelector menu = new ROIMenuSelector(Driver, logger, Context);
                menu.SelectRoiAdmin("Profile", "Logout");
            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to select Logout as : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }

            return new ROIAdminFacilityListPage(Driver, logger, Context);
        }

        public void switchToDefaut()
        {
            Driver.SwitchToDefaultContent();
        }
        public string ReturnPatientName()
        {
            try
            {
                return Driver.GetText(patientFullName).Trim();
            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to return patient name Message : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }

        public void CheckAutoMRBLookup()
        {
            Driver.Click(ROIBtnInFacilityFeatures);
            if (!Driver.FindElementBy(autoMRNLookup).Selected)
            {
                Driver.Click(autoMRNLookup);
            }
            Driver.Click(updateFeature);
        }
    }
}
