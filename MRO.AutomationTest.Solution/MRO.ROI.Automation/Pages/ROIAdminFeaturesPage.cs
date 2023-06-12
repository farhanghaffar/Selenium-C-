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
using System.Collections.Generic;
using static MRO.ROI.Automation.Utility.IniFile;

namespace MRO.ROI.Automation.Pages
{
    public class ROIAdminFeaturesPage
    {
        public RemoteWebDriver Driver;
        public ExtentTest logger;
        public TestContext Context;
        public ROIAdminFeaturesPage(RemoteWebDriver driver, ExtentTest _loger, TestContext _context)
        {
            Driver = driver;
            logger = _loger;
            Context = _context;
        }


        public By contextDropdown = By.Id("mrocontent_ctlCategories_lstContext");
        public By aiDashboard = By.XPath("//div[text()='AIDashboard']");
        public By aiDashboardUsers = By.XPath("//td[ starts-with(@class,'eo_css_ctrl_mrocontent_ctlCategories_pcmMenuFeature')]//div[text()='Users']");
        public By facilityDropdown = By.XPath("//div[@id='mrocontent_pnlUsers']//table//select[@id='mrocontent_ctlUsers_lstFac']");
        public By selectingUserId = By.XPath("//select[@id='mrocontent_ctlUsers_lstAvailable']//option[text()='Kothuri, Anjali (cigniti-akothuri)']");
        public By addButton = By.Id("mrocontent_ctlUsers_cmdAdd");
        public By categoriesLink = By.XPath("//td[text()='Categories']");
        public By removeButton = By.Id("mrocontent_ctlUsers_cmdRemove");
        public const string selectedUser = "//select[@id='mrocontent_ctlUsers_lstIncluded']//option[text()='Kothuri, Anjali (cigniti-akothuri)']";
        public By loggingDashboard = By.XPath("//div[contains(text(),'Logging Dashboard')]");
        public By loggingDashboardUsers = By.XPath("//td[ starts-with(@class,'eo_css_ctrl_mrocontent_ctlCategories_pcmMenuFeature')]//div[text()='Users']");
        public By facilityLocationRequestExport = By.XPath("//div[contains(text(),'Can See Facility Location Requester Export')]");
        public By requesterLocationCode = By.XPath("//div[contains(text(),'Can Edit Requester Location Codes')]");
        public By facilityLocationRequestExportUsers = By.XPath("//td[ starts-with(@class,'eo_css_ctrl_mrocontent_ctlCategories_pcmMenuFeature')]//div[text()='Users']");
        public By requesterLocationCodeUsers = By.XPath("//td[ starts-with(@class,'eo_css_ctrl_mrocontent_ctlCategories_pcmMenuFeature')]//div[text()='Users']");
        public By actionSummaryViewAll = By.XPath("//div[contains(text(),'Action Summary View All')]");
        public By actionSummaryUsers = By.XPath("//td[ starts-with(@class,'eo_css_ctrl_mrocontent_ctlCategories_pcmMenuFeature')]//div[text()='Users']");
        public By facilityPatientLookup = By.XPath("//div[contains(text(),'Facility Patient Lookup')]");
        public By patientLookUpUsers = By.XPath("//td[ starts-with(@class,'eo_css_ctrl_mrocontent_ctlCategories_pcmMenuFeature')]//div[text()='Users']");
        public By issueValidationReport = By.XPath("//div[contains(text(),'Issue Validation Report')]");
        public By issueValidationReportUsers = By.XPath("//td[ starts-with(@class,'eo_css_ctrl_mrocontent_ctlCategories_pcmMenuFeature')]//div[text()='Users']");

        public By syncLink = By.XPath("//td[contains(text(),'Sync')]");
        public By addnewEnymsBtn = By.XPath("//input[@id='mrocontent_ctlSync_cmdAdd']");
        public By headerElement = By.XPath("//td[@id='MasterHeaderText']");
        public By usersElement = By.XPath("//td[starts-with(text(),'Users')]");
        public By rolesElement = By.XPath("//td[starts-with(text(),'Roles')]");
        public By contextDrp = By.XPath("//select[@id='mrocontent_ctlList_lstContext']");
        public By facilityDrp = By.XPath("//select[@id='mrocontent_ctlList_lstFac']");

        public string rqrPortalComposeLink = "//*[@href=\"javascript:frmServer.mrocontent_ctlList_tblReport_hidCommand.value='Compose|497';frmServer.mrocontent_ctlList_tblReport_btnCommand.click();\"]";
        public By notificationchkbox = By.XPath("//*[@id='eo_ele_79']");
        public By saveBtn = By.XPath("//input[@id='mrocontent_ctlCompose_cmdSaveFeatures']");
        string rqrPortalComposeLinks = "//tr[@class='TableHeader']/td/a[1]";

        public By issueValidationReportLable = By.XPath("//span[contains(text(), 'Issue Validation Report')]");
        public By requiresLicenseLable = By.XPath("//*[@id='mrocontent_TabStripE_Req_trial']");



        public string VerifySelectedContext()
        {
            try
            {
                string selectedContext = Driver.FindElement(contextDropdown).FindElements(By.XPath("./option[@selected]"))[0].Text;
                return selectedContext;
            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to verify selected context  with details message as : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }
        public void ClickOnAIDashboard()
        {
            try

            {
                Driver.Click(aiDashboard);
                Actions action = new Actions(Driver);
                IWebElement aiDashboardItem = Driver.FindElementBy(aiDashboard);
                action.MoveToElement(aiDashboardItem).ContextClick().Build().Perform();
                Driver.Wait(TimeSpan.FromSeconds(1));
                Driver.Click(aiDashboardUsers);

            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to click AI Dashboard with details message as : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }

        }
        public void AddAIDashboardAtAdminSide(string option)
        {
            try
            {
                string facilityVal = IniHelper.ReadConfig("ROIPivotAICreatePageForDashboardTest", "facility");
                string userName = IniHelper.ReadConfig("ROIPivotAICreatePageForDashboardTest", "username");
                 Driver.SendKeys(facilityDropdown, facilityVal);
                logger.Log(Status.Info, "Selected facility: " + facilityVal);

                Console.WriteLine(userName);
                Console.WriteLine("Kothuri, AnjaliK (cigniti-akothuri)");
                string selectedUser1 = $"//select[@name='mrocontent$ctlUsers$lstAvailable']//option[text()='Kothuri, AnjaliK (cigniti-akothuri)']";
                
                WebElementHelper helper = new WebElementHelper(Driver, logger, Context);
                bool isPresent = helper.IsElementPresent(selectedUser1);
                if (option == "Add")
                {
                  
                    Driver.Click(By.XPath(selectedUser1));
                    Driver.WaitInSeconds(3);
                    Driver.Click(addButton);
                    Driver.WaitInSeconds(3);

                }

                if (option == "Remove")
                {
                    Driver.Click(By.XPath($"//option[text()='{userName}']"));
                    Driver.Click(removeButton);
                }
                logger.Log(Status.Info,"Added user in list: "+userName);

            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to add AI Dashboard with details message as : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }
        public void ClickOnLoggingDashboard()
        {
            try
            {
                Driver.Click(loggingDashboard);
                Actions action = new Actions(Driver);
                IWebElement loggingDashboardItem = Driver.FindElementBy(loggingDashboard);
                action.MoveToElement(loggingDashboardItem).ContextClick().Build().Perform();
                Driver.Wait(TimeSpan.FromSeconds(1));
                Driver.Click(loggingDashboardUsers);

            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to click logging Dashboard with details message as : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }

        }
        public void AddLoggingDashboardAtAdminSide(string option)
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

                throw new Exception($"Failed to add logging Dashboard with details message as : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }
        public void ClickOnFacilityLocationExport()
        {
            try
            {
                Driver.Click(facilityLocationRequestExport);
                Actions action = new Actions(Driver);
                IWebElement exportItem = Driver.FindElementBy(facilityLocationRequestExport);
                action.MoveToElement(exportItem).ContextClick().Build().Perform();
                Driver.Wait(TimeSpan.FromSeconds(1));
                Driver.Click(facilityLocationRequestExportUsers);

            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to click location export users with details message as : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }

        }


        public void ClickOnRequesterLocationCode()
        {
            try
            {
                Driver.Click(requesterLocationCode);
                Actions action = new Actions(Driver);
                IWebElement locationCodeItem = Driver.FindElementBy(requesterLocationCode);
                action.MoveToElement(locationCodeItem).ContextClick().Build().Perform();
                Driver.Wait(TimeSpan.FromSeconds(1));
                Driver.Click(requesterLocationCodeUsers);

            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to click requester location codes with details message as : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }

        }


        public void AddCanEditRequesterLocationCodes(string option)
        {
            try
            {
                string facilityVal = IniHelper.ReadConfig("ROIFacilitiesAndLocationsRequesterExportFacilitiesAndLocationsListTest", "facility");
                string userName = IniHelper.ReadConfig("ROIFacilitiesAndLocationsRequesterExportFacilitiesAndLocationsListTest", "username");
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
                //Driver.Click(categoriesLink);

            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to add Can Edit Requester Location Codes with details message as : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }



        public void CanSeeFacilityLocationRequesterExport(string option)
        {
            try
            {
                string facilityVal = IniHelper.ReadConfig("ROIFacilitiesAndLocationsRequesterExportFacilitiesAndLocationsListTest", "facility");
                string userName = IniHelper.ReadConfig("ROIFacilitiesAndLocationsRequesterExportFacilitiesAndLocationsListTest", "username");
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
                //Driver.Click(categoriesLink);

            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to add Can Edit Requester Location Codes with details message as : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }

        public void ClickOnCatagories()
        {
            try
            {

                Driver.Click(categoriesLink);
            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to click catagories with details message as : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }

        }

        public void VerifyFacilityLocationExportRemovedorNot()
        {
            try
            {
                string userName = IniHelper.ReadConfig("ROIFacilitiesAndLocationsRequesterExportFacilitiesAndLocationsListTest", "username");
                string selectedUser1 = $"//select[@id='mrocontent_ctlUsers_lstIncluded']//option[text()='{userName}']";
                WebElementHelper helper = new WebElementHelper(Driver, logger, Context);
                bool isPresent = helper.IsElementPresent(selectedUser1);
                if (isPresent == true)
                {
                    Driver.Click(By.XPath($"//option[text()='{userName}']"));
                    Driver.Click(removeButton);
                }
                else
                {
                    logger.Log(Status.Info, "Verified  user doesn't have Can Edit Requester Location Codes feature ");
                }
            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to click catagories with details message as : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }


        public void VerifyRequesterLocationCodeRemovedorNot()
        {
            try
            {
                string userName = IniHelper.ReadConfig("ROIFacilitiesAndLocationsRequesterExportFacilitiesAndLocationsListTest", "username");
                string selectedUser1 = $"//select[@id='mrocontent_ctlUsers_lstIncluded']//option[text()='{userName}']";
                WebElementHelper helper = new WebElementHelper(Driver, logger, Context);
                bool isPresent = helper.IsElementPresent(selectedUser1);
                if (isPresent == true)
                {
                    Driver.Click(By.XPath($"//option[text()='{userName}']"));
                    Driver.Click(removeButton);
                }
                else
                {
                    logger.Log(Status.Info, "Verified  user doesn't have Can Edit Requester Location Codes feature ");
                }
            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to click catagories with details message as : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }

        public void ClickOnActionSummaryViewAll()
        {
            try
            {
                Driver.Click(actionSummaryViewAll);
                Actions action = new Actions(Driver);
                IWebElement actionSummaryItem = Driver.FindElementBy(actionSummaryViewAll);
                action.MoveToElement(actionSummaryItem).ContextClick().Build().Perform();
                Driver.Wait(TimeSpan.FromSeconds(1));
                Driver.Click(actionSummaryUsers);

            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to click action summary view all with details message as : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }

        }

        public void AddActionSummaryAtAdminSide(string option)
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

                throw new Exception($"Failed to add logging Dashboard with details message as : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }

        public void ClickOnFacilityPatientLookUp()
        {
            try
            {
                Driver.Click(facilityPatientLookup);
                Actions action = new Actions(Driver);
                IWebElement lookupItem = Driver.FindElementBy(facilityPatientLookup);
                action.MoveToElement(lookupItem).ContextClick().Build().Perform();
                Driver.Wait(TimeSpan.FromSeconds(1));
                Driver.Click(patientLookUpUsers);

            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to click logging Dashboard with details message as : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }

        }

        public void AddFacilityPatientLookUp(string option)
        {
            try
            {
                string facilityVal = IniHelper.ReadConfig("ROIAdminDrillInUserPermissionPatientLookupTest", "facility");
                string userName = IniHelper.ReadConfig("ROIAdminDrillInUserPermissionPatientLookupTest", "username");
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


            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to add logging Dashboard with details message as : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }

        public void AddIssueValidationReport(string option)
        {
            try
            {
                string facilityVal = IniHelper.ReadConfig("ROIAdminDrillInUserPermissionPatientLookupTest", "facility");
                string userName = IniHelper.ReadConfig("ROIAdminDrillInUserPermissionPatientLookupTest", "username");
                Driver.SendKeys(facilityDropdown, facilityVal);
               /* string selectedUser1 = $"//select[@id='mrocontent_ctlUsers_lstIncluded']//option[text()='{userName}']";
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
                }*/


            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to add logging Dashboard with details message as : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }

        public void ClickOnIssueValidationReport()
        {
            try
            {
                Driver.Click(issueValidationReport);
                Actions action = new Actions(Driver);
                IWebElement report = Driver.FindElementBy(issueValidationReport);
                action.MoveToElement(report).ContextClick().Build().Perform();
                Driver.Wait(TimeSpan.FromSeconds(1));
                Driver.Click(issueValidationReportUsers);

            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to click issue validation report with details message as : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }

        }

        public void ClickOnSync()
        {
            try
            {

                Driver.Click(syncLink);
            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to click sync link with details message as : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }

        }

        public void ClickAddNewEnumsDatabase()
        {
            try
            {

                Driver.Click(addnewEnymsBtn);
            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to click add new enums to database button with details message as : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }

        }

        public string VerifyHeader()
        {
            try
            {

                string header = Driver.GetText(headerElement);
                return header;
            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to return header with details message as : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }

        public void SelectRoles()
        {
            try
            {
                ROIMenuSelector menu = new ROIMenuSelector(Driver, logger, Context);
                menu.SelectRoiAdmin("Users", "Roles");
            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to select roles with details message as : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }

        }

        public void SelectContext(string type, string facilityVal)
        {
            try
            {
                Driver.SendKeys(contextDrp, type);
                Driver.SendKeys(facilityDrp, facilityVal);
                Driver.Wait(TimeSpan.FromSeconds(2));


            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to select context with details message as : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }

        }

        public bool VerifyRqrPortal()
        {
            try
            {
                string rqrPortal = "//a[contains(text(),'RqrPortal - User')]";
                WebElementHelper helper = new WebElementHelper(Driver, logger, Context);
                bool isDisplayed = helper.IsElementPresent(rqrPortal);
                return isDisplayed;

            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to select context with details message as : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }

        }

        public void ClickOnRqrPortalComposeLink()
        {
            try
            {

                WebElementHelper helper = new WebElementHelper(Driver, logger, Context);
                //helper.ScrollIntoView(rqrPortalComposeLink, FindElementBy.Xpath);
                helper.Click_Action(rqrPortalComposeLink, FindElementBy.Xpath);
                Driver.Wait(TimeSpan.FromSeconds(2));
            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to click on rqr portal compose link with details message as : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }


        public void UnCheckNotificationFloder()
        {
            try
            {

                Driver.Click(notificationchkbox);
                Driver.Click(saveBtn);


            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to click on notifications with details message as : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }

        public void ClickOnRqrPortalComposeLinks()
        {
            try
            {

                WebElementHelper helper = new WebElementHelper(Driver, logger, Context);
                helper.Click_Action(rqrPortalComposeLinks, FindElementBy.Xpath);
                Driver.Wait(TimeSpan.FromSeconds(2));
            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to click on rqr portal compose link with details message as : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }

        public bool IsIssueValidationReportLableShowing()
        {
            return Driver.isElementDisplayed(issueValidationReportLable);
        }

        public void HideRequiresLicenseLable()
        {
            if (Driver.isElementDisplayed(requiresLicenseLable))
            {
                Driver.HideElement(requiresLicenseLable);
            }

        }
    }


}
