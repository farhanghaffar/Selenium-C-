using AventStack.ExtentReports;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MRO.ROI.Automation.Selenium;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.UI;
using System;
namespace MRO.ROI.Automation.Pages
{
    public class ROIAdminPackingListsPage
    {
        public RemoteWebDriver Driver;
        public ExtentTest logger;
        public TestContext Context;
        public ROIAdminPackingListsPage(RemoteWebDriver driver, ExtentTest _loger, TestContext _context)
        {
            Driver = driver;
            logger = _loger;
            Context = _context;
        }

        public By btnCreate = By.XPath("//input[@id ='mrocontent_btnAddPackingList']");
        public By chkAllComponents = By.XPath("//input[@id='mrocontent_custComponentPrintOrder_RadGridComponentPrintOrder_ctl00_ctl02_ctl00_cbSelectAllRows']");
        //public By actionTypeDropdown = By.XPath("//select[@id ='mrocontent_lstCanSee']");
        //public By requesterTypeDropdown = By.XPath("//select[@id='mrocontent_lstRequesterType']");//
        //public By requestTypeDropdown = By.XPath("//select[@id='mrocontent_lstType']");
        //public By resultantMessage = By.XPath("//table[@id='mrocontent_tblRequests']//tr//td");
        //public By btnExportToExcel = By.XPath("//img[@alt='Export to Excel']");
        //public By lnkToday = By.XPath("//div[@class='ranges']/ul/li[1]");
        //public By actionDropdownSelection = By.XPath("//select[@id='mrocontent_lstType']");
        //public By addActionBtn = By.XPath("//input[@id='mrocontent_cmdAdd']");


        public void CheckAllComponentsFromPackingListAndCreateShipment()
        {
            try
            {
                bool isCheck = Driver.FindElementBy(chkAllComponents).Selected;
                if (isCheck == false)
                {
                    Driver.Click(chkAllComponents);
                    Driver.DirectClick(btnCreate);
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to check all components from packing list with message : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }
    }
}
