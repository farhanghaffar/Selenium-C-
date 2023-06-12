using AventStack.ExtentReports;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MRO.ROI.Automation.Common.Navigation;
using MRO.ROI.Automation.Pages;
using MRO.ROI.Automation.Pages.Common;
using MRO.ROI.Automation.Selenium;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Remote;
using System;
using static MRO.ROI.Automation.Utility.IniFile;

namespace MRO.ROI.Automation.Pages
{
    public class ROIRequestersListPage
    {
        public RemoteWebDriver Driver;
        public ExtentTest logger;
        public TestContext Context;
        public ROIRequestersListPage(RemoteWebDriver driver, ExtentTest _loger, TestContext _context)
        {
            Driver = driver;
            logger = _loger;
            Context = _context;
        }

        public By bilingOfficeTargetFacilityDrp = By.XPath("//select[@id='mrocontent_lstBOETargetFacility']");
        public By searchButton = By.Id("mrocontent_cmdCreate");
        public By clearFields = By.XPath("//input[@id='mrocontent_cmdClearFields']");
        public By firstRowRecord = By.XPath("//table[@id='mrocontent_dgRequesters']//tr[2]//td[3]//a");
        public By bilingOfcDrp = By.XPath("//select[@id='mrocontent_lstCBOTargetFacilityID']");
        public By returnToList = By.XPath("//input[@id='mrocontent_cmdCancel']");
        public By secondRowRecord = By.XPath("//table[@id='mrocontent_dgRequesters']//tr[3]//td[3]//a");
        string selectedVal = string.Empty;
        public By defaultShipmentTypeDrp = By.Id("mrocontent_lstDefaultShipmentType");
        public By selectedShipment = By.Id("mrocontent_lstShipmentType");
        public By bilingOfcDeliveryChkbox = By.XPath("//input[@id='mrocontent_cbHasBOEDelivery']");
        public By deliveryChkbox = By.Id("mrocontent_cbCBOWF");
        public By clearFieldsBtn = By.XPath("//input[@id='mrocontent_cmdClearFields']");
        public By notValidatedChkBox = By.XPath("//input[@id='mrocontent_cbNotValidated']");
        public By requestDocsScannedChkBox = By.Id("mrocontent_cbRequestDocsScanned");
        public By internalFacilityDrpdown = By.Id("mrocontent_lstFacility");
        public By validatedChkbox = By.Id("mrocontent_cbValidated");
        public By excelIcon = By.XPath("//input[@id='mrocontent_lnkExport']");
        public By tableRowCount = By.XPath("//table[@id='mrocontent_dgRequesters']//tbody//tr");
        public By billingOfcTargetFacilityChkbox = By.Id("//input[@id='mrocontent_cbHasBOETargetFacility']");
        public By organizationField = By.XPath("//input[@id='mrocontent_txtOrganization']");
        public By typesDrpdown = By.Id("mrocontent_lstType");
        public By organizationLink = By.XPath("//table[@id='mrocontent_dgRequesters']/tbody/tr[2]/td[3][a]");
        public By testPortalHyperlink = By.XPath("//a[starts-with(text(),'Test Portal')]");

        public void SearchRecordForSpecificFacility( string facilityVal)
        {
            try
            {
                Driver.Click(clearFields);
                Driver.SendKeys(bilingOfficeTargetFacilityDrp, facilityVal);
                Driver.Click(searchButton);
            }
            catch (Exception ex)
            {

                throw new Exception($"Failed  search records with details as Message : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
           
        }


        public void SelectDefaultShipmentType(string type)
        {
            try
            {
                
                Driver.SendKeys(defaultShipmentTypeDrp, type);
                Driver.Click(searchButton);
            }
            catch (Exception ex)
            {

                throw new Exception($"Failed  to return default shipment type  records with details as Message : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }

        }


        public string VerifyROITRecords()
        {
            try
            {
                string roitFacility = IniHelper.ReadConfig("ROIAdminRequesterListReportEnhancementTest", "ROIT");
                int numOfRows = Driver.FindElements(By.XPath("//table[@id='mrocontent_dgRequesters']//tr")).Count;
                string beforeXpath = "//table[@id='mrocontent_dgRequesters']//tr[";
                string afterXpath = "]//td[3]//a";                
                for (int i=2;i<=numOfRows;i++)
                {
                    string actualpath = beforeXpath + i + afterXpath;
                    Driver.Click(By.XPath(actualpath));
                    selectedVal = Driver.FindElement(bilingOfcDrp).FindElements(By.XPath("./option[@selected]"))[0].Text;                    
                    if (selectedVal!=roitFacility)
                    {
                        logger.Log(Status.Info, $"Requester table returns records other than selected facility:({selectedVal})");
                    }
                    Driver.Click(returnToList);
                }
                
                return selectedVal;
            }
            catch (Exception ex)
            {

                throw new Exception($"Failed   to verify  roi facility records  with details as Message : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }


        public string VerifyYaleFacilityRecords()
        {
            try
            {
                
                string yaleFacility = IniHelper.ReadConfig("ROIAdminRequesterListReportEnhancementTest", "Yale");
                int numOfRows = Driver.FindElements(By.XPath("//table[@id='mrocontent_dgRequesters']//tr")).Count;
                string beforeXpath = "//table[@id='mrocontent_dgRequesters']//tr[";
                string afterXpath = "]//td[3]//a";
                for (int i = 2; i <= numOfRows; i++)
                {
                    string actualpath = beforeXpath + i + afterXpath;
                    Driver.Click(By.XPath(actualpath));
                    selectedVal = Driver.FindElement(bilingOfcDrp).FindElements(By.XPath("./option[@selected]"))[0].Text;
                    if (selectedVal != yaleFacility)
                    {
                        logger.Log(Status.Info, $"Requester table returns records other than selected facility:({selectedVal})");
                    }
                    Driver.Click(returnToList);
                }               
                return selectedVal;
            }
            catch (Exception ex)
            {

                throw new Exception($"Failed   to verify  roi facility records  with details as Message : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }


        public string VerifyDefaultShipmentRecords()
        {
            try
            {

                string externalSite = IniHelper.ReadConfig("ROIAdminRequesterListReportEnhancementTest", "shipmentType1");
                string email = IniHelper.ReadConfig("ROIAdminRequesterListReportEnhancementTest", "shipmentType2");
                int numOfRows = Driver.FindElements(By.XPath("//table[@id='mrocontent_dgRequesters']//tr")).Count;
                string beforeXpath = "//table[@id='mrocontent_dgRequesters']//tr[";
                string afterXpath = "]//td[3]//a";
                for (int i = 2; i <=numOfRows; i++)
                {
                    string actualpath = beforeXpath + i + afterXpath;
                    Driver.Click(By.XPath(actualpath));
                    selectedVal = Driver.FindElement(selectedShipment).FindElements(By.XPath("./option[@selected]"))[0].Text;                    
                    if (selectedVal != externalSite)
                    {
                        logger.Log(Status.Info, $"Requester table returns records other than selected facility:({selectedVal})");
                    }
                   
                    Driver.Click(returnToList);
                }               
                return selectedVal;
            }
            catch (Exception ex)
            {

                throw new Exception($"Failed   to verify  roi facility records  with details as Message : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }



        public void ClickBillingOfficeDeliveryCheckbox()
        {
            try
            {
                if(Driver.FindElement(bilingOfcDeliveryChkbox).Selected==false)
                {
                    Driver.Click(bilingOfcDeliveryChkbox);
                }
                Driver.Click(searchButton);
               
            }
            catch (Exception ex)
            {

                throw new Exception($"Failed   to click  billing office delivery check box   with details as Message : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }


        public void ClickBillingOfficeTargetCheckbox()
        {
            try
            {
                if (Driver.FindElement(bilingOfcDeliveryChkbox).Selected == true)
                {
                    Driver.Click(bilingOfcDeliveryChkbox);
                }
                Driver.Click(bilingOfficeTargetFacilityDrp);
                Driver.Click(searchButton);
                Driver.Click(firstRowRecord);
                Driver.ScrollToElement(bilingOfcDrp);

            }
            catch (Exception ex)
            {

                throw new Exception($"Failed   to click  billing office delivery check box   with details as Message : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }


        public bool  VerifyBillingOfcDeliveryRecors()
        {
            try
            {

                Driver.Click(firstRowRecord);
                bool isSelected = Driver.FindElementBy(deliveryChkbox).Selected;
                Driver.ScrollToElement(deliveryChkbox);
                return isSelected;


            }
            catch (Exception ex)
            {

                throw new Exception($"Failed   to verify  roi facility records  with details as Message : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }



        public string VerifyEmailDeliveryRecords()
        {
            try
            {

                Driver.Click(firstRowRecord);
                selectedVal = Driver.FindElement(selectedShipment).FindElements(By.XPath("./option[@selected]"))[0].Text;
                Driver.ScrollToElement(selectedShipment);
                return selectedVal;


            }
            catch (Exception ex)
            {

                throw new Exception($"Failed   to verify  roi facility records  with details as Message : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }

        public void ReturnToList()
        {
            try
            {
                Driver.Click(returnToList);
            }
            catch (Exception ex)
            {

                throw new Exception($"Failed   to click return to list with details as Message : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }


        public void SearchRecordForInternalFacility(string facilityVal)
        {
            try
            {
                Driver.Click(clearFields);
                Driver.SendKeys(internalFacilityDrpdown, facilityVal);
                Driver.Click(notValidatedChkBox);
                Driver.Click(requestDocsScannedChkBox);
                Driver.Click(searchButton);
            }
            catch (Exception ex)
            {

                throw new Exception($"Failed  search records with details as Message : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }

        }
        public bool VerifyInternalFacilityRecords()
        {
            try
            {
                Driver.Click(firstRowRecord);
                bool  isChecked = Driver.FindElementBy(validatedChkbox).Selected;
                return isChecked;
            }
            
            catch (Exception ex)
            {

                throw new Exception($"Failed  to verify roi facility records with details as Message : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
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

                throw new Exception($"Failed  to click excel export with details as Message : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }

        public int GetTableRowCount()
        {
            try
            {
               int count= Driver.FindElements(tableRowCount).Count;
                return count;
            }
            catch (Exception ex)
            {

                throw new Exception($"Failed  to get row count with details as Message : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }

        public string GetTableData(int rowCount)
        {
            try
            {
                IWebElement idVal = Driver.FindElementBy(By.XPath($"//table[@id='mrocontent_dgRequesters']/tbody/tr[{rowCount}]/td[2]"));
                return idVal.Text;
            }
            catch (Exception ex)
            {

                throw new Exception($"Failed  to get row count with details as Message : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }

        public void ApplyFiltersAndSearchData()
        {
            Driver.Click(clearFieldsBtn);
            string sOrganization = IniHelper.ReadConfig("ROIAdminRequesterListReportEnhancementTest", "Organization");
            Driver.SendKeys(organizationField, sOrganization);
            Driver.SendKeys(internalFacilityDrpdown, "[All facilities]");
            Driver.SendKeys(typesDrpdown, "[All Types]");
            Driver.Click(searchButton);
        }

        public void ClickOrganizationLink()
        {
            try
            {
                int numOfRows = Driver.FindElements(By.XPath("//table[@id='mrocontent_dgRequesters']//tr")).Count;
                string beforeXpath = "//table[@id='mrocontent_dgRequesters']//tr[";
                string afterXpath = "]//td[3]//a";
                for (int i = 2; i <= numOfRows; i++)
                {
                    string actualpath = beforeXpath + i + afterXpath;
                    Driver.Click(By.XPath(actualpath));
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to click on organization link with Message : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }


        public void ClickOnSearch()
        {
            try
            {
                Driver.Click(clearFields);
                Driver.SendKeys(organizationField, "Test");
                Driver.SendKeys(typesDrpdown, "Internal");
                Driver.Click(searchButton);
            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to click search with Message : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }

        public void ClickTestPortal()
        {
            try
            {
                Driver.Click(testPortalHyperlink);
            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to click test portal hyper link with Message : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }


    }
}
