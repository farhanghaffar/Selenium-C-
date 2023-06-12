using AventStack.ExtentReports;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MRO.ROI.Automation.Selenium;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MRO.ROI.Automation.Utility.IniFile;

namespace MRO.ROI.Automation.Pages
{
    public class ROIFacilityLocationReportPage
    {
        public RemoteWebDriver Driver;
        public ExtentTest logger;
        public TestContext Context;
        public ROIFacilityLocationReportPage(RemoteWebDriver driver, ExtentTest _loger, TestContext _context)
        {
            Driver = driver;
            logger = _loger;
            Context = _context;
        }



        public By parentBusinessDrp = By.Id("mrocontent_lstParentBusinessID");
        public By facilityDrp = By.Id("mrocontent_lstFacilities");
        public By facilityFromDate = By.Id("mrocontent_txtFacilityCreatedDateA");
        public By facilityToDate = By.Id("mrocontent_txtFacilityCreatedDateZ");
        public By includeTestChkBox = By.XPath("//input[@id='mrocontent_cbTest']");
        public By includeDisableFacilityChkBox = By.XPath("//input[@id='mrocontent_cbDisabledFac']");
        public By showAddtionalTINChkbox = By.XPath("//input[@id='mrocontent_cbShowAdditionalTIN']");
        public By createReportBtn = By.XPath("//input[@id='mrocontent_cmdCreate']");
        public By excelIcon = By.XPath("//input[@id='mrocontent_MROReportGridBanner_lnkExport']");
        public By tableRows = By.XPath("//div[@id='mrocontent_dgReport_GridData']//table//tr");
        public By tableRowCount = By.XPath("//span[@id='mrocontent_MROReportGridBanner_lblRows']");
        public By requesterExportBtn = By.Id("mrocontent_cmdRequesterExport");
        public By headerElement = By.XPath("//td[@id='MasterHeaderText']");
        public By TableExpandableBtn = By.XPath("//input[@id='mrocontent_dgReport_ctl00_ctl04_GECBtnExpandColumn']");
        public By TableExcelIcon = By.XPath("//input[@id='mrocontent_dgReport_ctl00_ctl06_dgReportDetails_ctl00_ctl02_ctl00_ExportToExcelButton1']");
        public By LocationLogOutBtn = By.XPath("//img[@id='mroheader_MROPageHead1_ctl03_imgLogout']");
        public By TableColumn = By.XPath("//div[@class='RadGrid RadGrid_Default innerGrid']//table[@class='rgMasterTable rgClipCells']//thead//th");




        public void CreateReport()
        {
            try
            {

                string selectedFacility = IniHelper.ReadConfig("ROIFacilitiesAndLocationsRequesterExportFacilitiesAndLocationsListTest", "FacilityValue");
                string selectedParentBusiness = IniHelper.ReadConfig("ROIFacilitiesAndLocationsRequesterExportFacilitiesAndLocationsListTest", "ParentBusinessValue");
                string fromDate = IniHelper.ReadConfig("ROIFacilitiesAndLocationsRequesterExportFacilitiesAndLocationsListTest", "FromDate");
                string toDate = IniHelper.ReadConfig("ROIFacilitiesAndLocationsRequesterExportFacilitiesAndLocationsListTest", "ToDate");
                SelectElement parentBusiness = new SelectElement(Driver.FindElement(parentBusinessDrp));
                parentBusiness.SelectByText(selectedParentBusiness);
                SelectElement facility = new SelectElement(Driver.FindElement(facilityDrp));
                facility.SelectByText(selectedFacility);
                Driver.ClearText(facilityFromDate);
                Driver.SendKeys(facilityFromDate, fromDate);
                Driver.ClearText(facilityToDate);
                Driver.SendKeys(facilityToDate, toDate);
                if (Driver.FindElementBy(includeTestChkBox).Selected == true)
                {
                    Driver.Click(includeTestChkBox);
                }
                if (Driver.FindElementBy(includeDisableFacilityChkBox).Selected == true)
                {
                    Driver.Click(includeDisableFacilityChkBox);
                }
                if (Driver.FindElementBy(showAddtionalTINChkbox).Selected == true)
                {
                    Driver.Click(showAddtionalTINChkbox);
                }
                Driver.Click(createReportBtn);

            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to return page header with Exception: {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");

            }

        }

        public bool VerifyFacility()
        {
            try
            {
                bool isDisplayed = false;
                int numOfRows = Driver.FindElements(tableRows).Count;
                string beforeXpath = "//div[@id='mrocontent_dgReport_GridData']//table//tr[";
                string afterXpath = "]//td[3]";
                for (int i = 1; i < numOfRows - 1; i++)
                {
                    string actualXpath = beforeXpath + i + afterXpath;
                    string value = Driver.GetText(By.XPath(actualXpath));
                    int val = Convert.ToInt32(value);
                    if (val == 1 || val == 7)
                    {

                        isDisplayed = true;
                    }
                    if (val == 1)
                    {
                        isDisplayed = true;
                    }
                    if (val == 1 && val == 7)
                    {
                        isDisplayed = true;
                    }
                }

                return isDisplayed;

            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to verify facility records with Exception: {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }
        public void ClickOnExcelExportIcon()
        {
            try
            {
                Driver.Click(excelIcon);
            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to click excel icon with Exception: {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }

        public bool VerifyTestAndDisabledFacilityRecords()
        {
            try
            {
                bool isDisplayed = false;
                int numOfRows = Driver.FindElements(By.XPath("//div[@id='mrocontent_dgReport_GridData']//table//tr")).Count;
                string beforeXpath = "//div[@id='mrocontent_dgReport_GridData']//table//tr[";
                string afterXpath = "]//td[3]";
                for (int i = 1; i < numOfRows - 1; i++)
                {
                    string actualXpath = beforeXpath + i + afterXpath;
                    string value = Driver.GetText(By.XPath(actualXpath));
                    int val = Convert.ToInt32(value);
                    if (val == 1)
                    {
                        // logger.Log(Status.Info, "Facility are visible");
                        isDisplayed = true;
                    }
                    if (val == 7)
                    {
                        // logger.Log(Status.Info, "Facility are visible");
                        isDisplayed = true;
                    }

                }

                return isDisplayed;

            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to verify  disabled facility records with Exception: {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }

        public void CreateReportForIncludeTest()
        {
            try
            {

                if (Driver.FindElementBy(includeTestChkBox).Selected == false)
                {
                    Driver.Click(includeTestChkBox);
                }

                Driver.Click(createReportBtn);
                Driver.ScrollToElement(By.XPath("//div[@id='mrocontent_dgReport_GridData']//table//tr[7]"));

            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to create  include test report with Exception: {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");

            }

        }

        public void CreateReportForIncludeDisabletFacility()
        {
            try
            {

                if (Driver.FindElementBy(includeTestChkBox).Selected == false)
                {
                    Driver.Click(includeTestChkBox);
                }
                if (Driver.FindElementBy(includeDisableFacilityChkBox).Selected == false)
                {
                    Driver.Click(includeDisableFacilityChkBox);
                }

                Driver.Click(createReportBtn);

            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to create disable facility report with Exception: {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");

            }

        }

        public int GetRowCountFromTable()
        {
            try
            {
                string numOfRows = Driver.GetText(tableRowCount);
                int rowsCount = Convert.ToInt32(numOfRows);
                return rowsCount;

            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to get number of rows from table with Exception: {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }
        public bool VerifyRequesterExportBtn()
        {
            try
            {
                string requesterExport = "//input[@id='mrocontent_cmdRequesterExport']";
                WebElementHelper helper = new WebElementHelper(Driver, logger, Context);
                bool isPresent = helper.IsElementPresent(requesterExport);
                return isPresent;
            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to click requester export with Exception: {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }

        public void ClickOnRequesterExport()
        {
            try
            {
                Driver.Click(requesterExportBtn);
            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to click excel icon with Exception: {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }

        public void CreateReportOnlyForIncludeTest()
        {
            try
            {

                if (Driver.FindElementBy(includeTestChkBox).Selected == false)
                {
                    Driver.Click(includeTestChkBox);
                }

                if (Driver.FindElementBy(includeDisableFacilityChkBox).Selected == true)
                {
                    Driver.Click(includeDisableFacilityChkBox);
                }

                if (Driver.FindElementBy(showAddtionalTINChkbox).Selected == true)
                {
                    Driver.Click(showAddtionalTINChkbox);
                }


                Driver.Click(createReportBtn);

            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to create  include test report with Exception: {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");

            }
        }
        public void IncludeTestCreateReport()
        {
            try
            {

                if (Driver.FindElementBy(includeTestChkBox).Selected == false)
                {
                    Driver.Click(includeTestChkBox);
                }

                Driver.Click(createReportBtn);


            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to create  include test report with Exception: {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");

            }

        }
        public string VerifyHeader()
            {
                try
                {
                    string headerVal = Driver.GetText(headerElement);
                    return headerVal;



                }
                catch (Exception ex)
                {



                    throw new Exception($"Failed to return header with details as : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
                }
            }
            public bool VerifyPageElements()
            {
                bool isDisplayed = false;
                try
                {

                    String Expandable = "//input[@id='mrocontent_MROReportExpander1_btnCollapse']";

                    String ParentBusinessdropdown = "//select[@id='mrocontent_lstParentBusinessID']";
                    String Facilitydropdown = "//select[@id='mrocontent_lstFacilities']";
                    String IncludeTestcheckbox = "//input[@id='mrocontent_cbTest']";
                    String FacilityCreatedFrom = "//input[@id='mrocontent_txtFacilityCreatedDateA']";
                    String LocationCreatedFrom = "//input[@id='mrocontent_txtLocationCreatedDateA']";
                    String FacilityStatedropdown = "//select[@id='mrocontent_lstFacilityState']";
                    String LocationStatedropdown = "//select[@id='mrocontent_lstLocationState']";
                    WebElementHelper helper = new WebElementHelper(Driver, logger, Context);
                    bool ParentBusinessDropdown = helper.IsElementPresent(ParentBusinessdropdown);
                    bool FacilityDropDown = helper.IsElementPresent(Facilitydropdown);
                    bool Includetestcheckbox = helper.IsElementPresent(IncludeTestcheckbox);
                    bool FacilityCreatefrom = helper.IsElementPresent(FacilityCreatedFrom);
                    bool LocationCreatefrom = helper.IsElementPresent(LocationCreatedFrom);
                    bool Facilitystatedropdown = helper.IsElementPresent(FacilityStatedropdown);
                    bool Locationstatedropdown = helper.IsElementPresent(LocationStatedropdown);
                    bool expandable = helper.IsElementPresent(Expandable);
                    if ((ParentBusinessDropdown == true) && (FacilityDropDown == true) &&
                        (Includetestcheckbox == true) && (FacilityCreatefrom == true) &&
                        (LocationCreatefrom == true) && (Facilitystatedropdown == true) &&
                        (Locationstatedropdown == true) && (expandable = true))
                    {
                        isDisplayed = true;
                    }

                    return isDisplayed;


                }
                catch (Exception ex)
                {

                    throw new Exception($"Failed to verify Page Elements as Message : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
                }
            }
            public void ClickONExpandBtn()
            {
                try
                {
                    var tableExpandableBtn = Driver.FindElementBy(TableExpandableBtn);
                    tableExpandableBtn.Click();

                }
                catch (Exception ex)
                {

                    throw new Exception($"Failed to create  include test report with Exception: {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");

                }
            }

            public bool VerifyTableElemnts()
            {
                try
                {
                    bool isDisplayed = false;
                    String ExpandlocationsChkBox = "//input[@id='mrocontent_cbExpandLocations']";
                    String ParentBusiness = "//a[contains(text(),'Parent Business')]";
                    String FacilityID = "//a[contains(text(),'Facility ID')]";
                    String FacilityName = "//a[contains(text(),'Facility Name')]";
                    String FacilityCode = "//a[contains(text(),'Facility Code')]";
                    String FacilityCity = "//a[contains(text(),'Facility City')]";
                    String FacilityState = "//a[contains(text(),'Facility State')]";
                    String FacilityZip = "//a[contains(text(),'Facility Zip')]";
                    String FacilityeRequests = "//a[contains(text(),'Facility E-Requests')]";
                    String FacilityPhone = "//a[contains(text(),'Facility Phone')]";
                    String FacilityCreated = "//a[contains(text(),'Facility Created')]";
                    String LocationID = "//a[contains(text(),'Location ID')]";
                    String LocationName = "//a[contains(text(),'Location Name')]";
                    String LocationCode = "//a[contains(text(),'Location Code')]";
                    String ContractID = "//a[contains(text(),'Contract ID')]";
                    String LocationCreated = "//a[contains(text(),'Location Created')]";
                    String LocationCity = "//a[contains(text(),'Location City')]";
                    String LocationState = "//a[contains(text(),'Location State')]";
                    String LocationZip = "//a[contains(text(),'Location Zip')]";
                    String LocationeRequests = "//a[contains(text(),'Location E-Requests')]";
                    String LocationPhone = "//a[contains(text(),'Location Phone')]";
                    String LoctionNPI = "//a[contains(text(),'Location NPI')]";
                    String LocationTIN = "//a[contains(text(),'Location TIN')]";
                    String LocationDHID = "//a[contains(text(),'Location DHID')]";
                    WebElementHelper helper = new WebElementHelper(Driver, logger, Context);
                    bool expandlocationsChkBox = helper.IsElementPresent(ExpandlocationsChkBox);
                    bool parentBusiness = helper.IsElementPresent(ParentBusiness);
                    bool facilityID = helper.IsElementPresent(FacilityID);
                    bool facilityName = helper.IsElementPresent(FacilityName);
                    bool facilityCode = helper.IsElementPresent(FacilityCode);
                    bool facilityCity = helper.IsElementPresent(FacilityCity);
                    bool facilityState = helper.IsElementPresent(FacilityState);
                    bool facilityZip = helper.IsElementPresent(FacilityZip);
                    bool facilityeRequests = helper.IsElementPresent(FacilityeRequests);
                    bool facilityPhone = helper.IsElementPresent(FacilityPhone);
                    bool facilityCreated = helper.IsElementPresent(FacilityCreated);
                    bool locationID = helper.IsElementPresent(LocationID);
                    bool locationName = helper.IsElementPresent(LocationName);
                    bool locationCode = helper.IsElementPresent(LocationCode);
                    bool contractID = helper.IsElementPresent(ContractID);
                    bool locationCreated = helper.IsElementPresent(LocationCreated);
                    bool locationCity = helper.IsElementPresent(LocationCity);
                    bool locationState = helper.IsElementPresent(LocationState);
                    bool locationZip = helper.IsElementPresent(LocationZip);
                    bool locationeRequests = helper.IsElementPresent(LocationeRequests);
                    bool locationPhone = helper.IsElementPresent(LocationPhone);
                    bool loctionNPI = helper.IsElementPresent(LoctionNPI);
                    bool locationTIN = helper.IsElementPresent(LocationTIN);
                    bool locationDHID = helper.IsElementPresent(LocationDHID);
                    if ((expandlocationsChkBox == true) && (parentBusiness == true) && (facilityID == true) &&
                        (facilityName == true) && (facilityCode == true) && (facilityCity == true) &&
                        (facilityState == true) && (facilityZip == true) && (facilityeRequests == true) &&
                        (facilityPhone == true) && (facilityCreated == true) && (locationID == true) &&
                        (locationName == true) && (locationCode == true) && (contractID == true) &&
                        (locationCreated == true) && (locationCity == true) && (locationState == true) &&
                        (locationZip == true) && (locationeRequests == true) && (locationPhone == true) &&
                        (loctionNPI == true) && (locationTIN == true) && (locationDHID == true))
                    {
                        isDisplayed = true;
                    }
                    return isDisplayed;


                }
                catch (Exception ex)
                {

                    throw new Exception($"Failed to VerifyTableElemnts as Message : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
                }
            }
            public void ClickONExcelIcon()
            {
                try
                {
                    var tableExcelIcon = Driver.FindElementBy(TableExcelIcon);
                    tableExcelIcon.Click();
                }
                catch (Exception ex)
                {

                    throw new Exception($"Failed to create  include test report with Exception: {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");

                }
            }
            public int TableColumnCount()
            {
                try
                {
                    int columnCount = Driver.FindElementsBy(TableColumn).Count;
                    return columnCount;
                }
                catch (Exception ex)
                {

                    throw new Exception($"Failed to count the table columns Exception: {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");

                    ;
                }
            }
            public void LogOut()
            {
                var LogOutBtn = Driver.FindElementBy(LocationLogOutBtn);
                LogOutBtn.Click();

            }


        }
    }

