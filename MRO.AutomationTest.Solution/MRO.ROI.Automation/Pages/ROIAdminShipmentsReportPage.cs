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

namespace MRO.ROI.Automation.Pages
{
   public class ROIAdminShipmentsReportPage
    {
        public RemoteWebDriver Driver;
        public ExtentTest logger;
        public TestContext Context;
        public ROIAdminShipmentsReportPage(RemoteWebDriver driver, ExtentTest _loger, TestContext _context)
        {
            Driver = driver;
            logger = _loger;
            Context = _context;
        }

        public By HeaderText = By.XPath("//td[@id='MasterHeaderText']");
        public By PrintRoom = By.XPath("//select[@id='mrocontent_lstShipOrigins']");
        public By OrderDateFrom = By.XPath("//input[@id='mrocontent_txtOrderFrom']");
        public By OrderDateTo = By.XPath("//input[@id='mrocontent_txtOrderTo']");
        public By ShippedDateFrom = By.XPath("//input[@id='mrocontent_txtShipFrom']");
        public By ShippedDateTo = By.XPath("//input[@id='mrocontent_txtShipTo']");
        public By Facility = By.XPath("//select[@id='mrocontent_lstFacility']");
        public By RequestType = By.XPath("//select[@id='mrocontent_lstRequestType']");
        public By RequesterType = By.XPath("//select[@id='mrocontent_lstRequesterType']");
        public By ParentBusiness = By.XPath("//select[@id='mrocontent_lstParentBusinessID']");
        public By CreateReport = By.XPath("//input[@id='mrocontent_cmdCreate']");
        public By ExcelIcon = By.XPath("//img[@alt='Export to Excel']");
        public By ShipmentType = By.XPath("//select[@id='mrocontent_lstShipmentType']");
        public By VeryfyNoResults = By.XPath("//td[contains(text(),'No results found for this table!')]");
        public By Logout = By.XPath("//img[@id='mroheader_ctl02_ctl03_imgLogout']");
        public By SearchResults = By.XPath("//td[contains(text(),'Search Results')]");
        public By NoResults = By.XPath("//td[contains(text(),'No results found for this table!')]");
        public By ValueDallas = By.XPath("//select[@id='mrocontent_lstShipOrigins']//option[text()='Dallas']");
        public By ValueValleyForge = By.XPath("//select[@id='mrocontent_lstShipOrigins']//option[text()='Valley Forge']");
        public By CD = By.XPath("//table[@id='mrocontent_dgShipments']//tr[@class='TableBody']//td[2]");
        public By MAIL = By.XPath("//table[@id='mrocontent_dgShipments']//tr[@class='TableBody']//td[2]");
        public By INDPDF = By.XPath("//table[@id='mrocontent_dgShipments']//tr[@class='TableBody']//td[2]");
        public By PDF = By.XPath("//table[@id='mrocontent_dgShipments']//tr[@class='TableBody']//td[2]");
        public By AppealsShipment = By.XPath("//td[contains(text(),'Appeals Shipment')]");
        public By CanceledOn = By.XPath("//td[contains(text(),'Cancelled on')]");
        public By ShipmentID = By.XPath("//table[@id='mrocontent_dgShipments']//tr[@class='TableBody']//td[1]");
        public By OrderedOn = By.XPath("//a[contains(text(),'Ordered on')]");
        public By NoResultsFound = By.XPath("//*[contains(text(), 'No results found for this table!')]");
        

        public void VerifyHeader()
        {
            string headertext = Driver.FindElementBy(HeaderText).Text;
            Assert.AreEqual(headertext, "Shipments Report");
        }


        public void VerifyPrintRoomDropdown()
        {
            IWebElement printRoom = Driver.FindElementBy(PrintRoom);            
            printRoom.Click();            
            string dallas = Driver.FindElementBy(ValueDallas).Text;
            Assert.AreEqual(dallas, "Dallas");
            string valleyForge = Driver.FindElementBy(ValueValleyForge).Text;
            Assert.AreEqual(valleyForge, "Valley Forge");
            var selectPrintRoom = new SelectElement(printRoom);
            selectPrintRoom.SelectByText("");
        }

        /// <summary>
        /// Set values for the fields and click on create report
        /// </summary>
        public void SetFilterSelectionsAndCreateReport()
        {
            try
            {
                IWebElement orderDateFrom = Driver.FindElementBy(OrderDateFrom);
                orderDateFrom.Clear();
                orderDateFrom.SendKeys("12/22/2020");
                IWebElement orderDateTo = Driver.FindElementBy(OrderDateTo);
                orderDateTo.Clear();
                var todaysDate = String.Format("{0:M/dd/yyyy}", DateTime.Now).Replace("-", "/");                
                orderDateTo.SendKeys(todaysDate);
                IWebElement shippedDateFrom = Driver.FindElementBy(ShippedDateFrom);
                shippedDateFrom.Clear();
                shippedDateFrom.SendKeys("12/22/2020");
                IWebElement shippedDateTo = Driver.FindElementBy(ShippedDateTo);
                shippedDateTo.Clear();   
                shippedDateTo.SendKeys(todaysDate);
                var facilityDropDown = Driver.FindElementBy(Facility);
                var selectFacility = new SelectElement(facilityDropDown);
                selectFacility.SelectByText("[All]");
                //selectFacility.SelectByText("123Hard Facility");
                var requestTypeDropDown = Driver.FindElementBy(RequestType);
                var selectRequestType = new SelectElement(requestTypeDropDown);
                selectRequestType.SelectByText("[Show All]");
                var requesterTypeDropDown = Driver.FindElementBy(RequesterType);
                var selectRequesterType = new SelectElement(requesterTypeDropDown);
                selectRequesterType.SelectByText("[All Types]");
                var parentBusiness = Driver.FindElementBy(ParentBusiness);
                var selectParentBusiness = new SelectElement(parentBusiness);
                selectParentBusiness.SelectByText("[All]");
                IWebElement createReport = Driver.FindElementBy(CreateReport);
                createReport.Click();
            }
            catch(Exception ex)
            {
                throw new Exception($"Failed to set filter selection and create report : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }

        /// <summary>
        /// Click on excel icon
        /// </summary>
        public void ClickOnExcelIcon()
        {
            try
            {
                IWebElement excelIcon = Driver.FindElementBy(ExcelIcon);
                excelIcon.Click();
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to download the excel file : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }
        /// <summary>
        /// Set shipment type to CD Mailed to Requester and click create report
        /// </summary>
        public void SetShipmentTypeToCDMailedToRequesterAndCreateReport()
        {
            try
            {
                var shipmentTypeTypeDropDown = Driver.FindElementBy(ShipmentType);
                var selectShipmentType = new SelectElement(shipmentTypeTypeDropDown);
                selectShipmentType.SelectByText("CD Mailed to Requester");
                IWebElement createReport = Driver.FindElementBy(CreateReport);
                createReport.Click();
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to set shipmenttype as cd mailed to requester : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }
        /// <summary>
        /// Set shipment type to Paper Mailed to Requester and click create report
        /// </summary>
        public void SetShipmentTypeToPaperMailedToRequesterAndCreateReport()
        {
            try
            {
                var shipmentTypeTypeDropDown = Driver.FindElementBy(ShipmentType);
                var selectShipmentType = new SelectElement(shipmentTypeTypeDropDown);
                selectShipmentType.SelectByText("Paper Mailed to Requester");
                IWebElement createReport = Driver.FindElementBy(CreateReport);
                createReport.Click();
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to set shipmenttype as Paper mailed to requester : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }
        /// <summary>
        /// Set shipment typw to blank and print room as dallas and create report
        /// </summary>
        public void SetShipmentTypeToBlankAndPrintRoomAsDallasAndCreateReport()
        {
            try
            {
                var shipmentTypeTypeDropDown = Driver.FindElementBy(ShipmentType);
                var selectShipmentType = new SelectElement(shipmentTypeTypeDropDown);
                selectShipmentType.SelectByText("");
                IWebElement printRoom = Driver.FindElementBy(PrintRoom);
                printRoom.SendKeys("Dallas");
                IWebElement createReport = Driver.FindElementBy(CreateReport);
                createReport.Click();
                
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to set shipmenttype as Paper mailed to requester : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }

        public void SetShipmentTypeToBlankAndPrintRoomAsValleyForgeAndCreateReport()
        {
            try
            {
                var shipmentTypeTypeDropDown = Driver.FindElementBy(ShipmentType);
                var selectShipmentType = new SelectElement(shipmentTypeTypeDropDown);
                selectShipmentType.SelectByText("");
                IWebElement printRoom = Driver.FindElementBy(PrintRoom);
                printRoom.SendKeys("Valley Forge");
                IWebElement createReport = Driver.FindElementBy(CreateReport);
                createReport.Click();

            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to set shipmenttype as Paper mailed to requester : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }
        /// <summary>
        /// Verify Serch Results list displayed with list of data
        /// </summary>
        public void VerifyReturnedReports()
        {
            try
            {              
                string results = Driver.FindElementBy(OrderedOn).Text;
                Assert.AreEqual(results, "Ordered on");                
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to verify returned reports : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }

        /// <summary>
        /// Verify no results returned
        /// </summary>

        public void VerifyNoResultsReturned()
        {
            string noResultss = string.Empty;
            try
            {
                string results = Driver.FindElementBy(NoResults).Text;
                Assert.AreEqual(results, "No results found for this table!");
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to verify the results : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
           
        }

        public void VerifyCDReportsReturned()
        {
            var cDReports = Driver.FindElementsBy(CD);
            foreach (var  cdReport in cDReports)
            {
                Assert.AreEqual(cdReport.Text, "CD");
            }
                        
        }

        public void VerifyEmailReportsReturned()
        {
            var mailReports = Driver.FindElementsBy(MAIL);
            foreach (var mailReport in mailReports)
            {
                Assert.AreEqual(mailReport.Text, "MAIL");
            }
        }

        public void VerifyINTPDFReportsReturned()
        {
            var indpdfReports = Driver.FindElementsBy(INDPDF);
            foreach (var indpdfReport in indpdfReports)
            {
                Assert.AreEqual(indpdfReport.Text, "INT_PDF");
            }
        }

        /// <summary>
        /// Set any filters, So setting Print Room to Empty and shipment type to empty verifying the reports
        /// </summary>
        public void SetAnyFilters()
        {
            try
            {
                var shipmentTypeTypeDropDown = Driver.FindElementBy(ShipmentType);
                var selectShipmentType = new SelectElement(shipmentTypeTypeDropDown);
                selectShipmentType.SelectByText("Internal Facility PDF");
                var printRoomDropdown = Driver.FindElementBy(PrintRoom);
                var selectPrintRoom = new SelectElement(printRoomDropdown);
                selectPrintRoom.SelectByText("");
                IWebElement createReport = Driver.FindElementBy(CreateReport);
                createReport.Click();
                

            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to set shipmenttype as Paper mailed to requester : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }


        public void ClickLogout()
        {
            try
            {
                IWebElement logout = Driver.FindElementBy(Logout);
                logout.Click();

            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to click logout : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }

        public string ReturnShipmentIDFromTable(int rowCount)
        {
            try
            {
               IWebElement shipmentID =  Driver.FindElementBy(By.XPath($"(//table[@id='mrocontent_tblShipments']//tr[@class='TableBody']//td[1]//a)[{rowCount}]"));
                return shipmentID.Text;
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to return shipment id from table : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }

        public string GetTotalRowCountFromShipmentTable()
        {
            try
            {
                IWebElement totalRowCount = Driver.FindElementBy(By.XPath($"//table[@id='mrocontent_tblShipments']//td[contains(text(),'Search Results')]"));
                return totalRowCount.Text.Split(' ')?.Last().Replace(')', ' ').Trim();
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to return row count from table : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }

        public void SetShipmentTypeToPDFEDeliveryToRequesterAndCreateReport()
        {
            try
            {
                var shipmentTypeTypeDropDown = Driver.FindElementBy(ShipmentType);
                var selectShipmentType = new SelectElement(shipmentTypeTypeDropDown);
                selectShipmentType.SelectByText("PDF E-delivery");
                IWebElement createReport = Driver.FindElementBy(CreateReport);
                createReport.Click();
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to set shipmenttype as PDF E-delivery to requester : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }

        public void VerifyPDFReportsReturned()
        {
            try
            {
                var pDFReports = Driver.FindElementsBy(PDF);
                foreach (var pdfReport in pDFReports)
                {
                    Assert.AreEqual(pdfReport.Text, "PDF");
                }

            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to set shipmenttype as PDF E-delivery to requester : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
            

        }

        public string GetFirstShipmentID()
        {
            string firstShipmentId = string.Empty;
            try
            {
                string shipmentIDAtIndexOne = string.Empty;
                List<IWebElement> elements = Driver.FindElementsBy(By.XPath("//table[@id='mrocontent_dgShipments']//tr[@class='TableBody']//td[1]"));
                List<string> shipmentIDs = new List<string>();
                if (elements.Count > 0)
                {
                    foreach (var _shipmentID in elements)
                    {
                        IWebElement elementAtFirstIndex = elements[0];
                        firstShipmentId = elementAtFirstIndex.Text;
                        shipmentIDs.Add(firstShipmentId);
                    }
                }
                 shipmentIDAtIndexOne = shipmentIDs[1];
                return shipmentIDAtIndexOne;
            }
            
            catch (Exception ex)
            {

                throw new Exception($"Failed to set shipmenttype as PDF E-delivery to requester : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
            
        }

        public string GetThirdShipmentID()
        {
            string thirdShipmentId = string.Empty;
            try
            {
                string shipmentIDAtIndexthree = string.Empty;
                List<IWebElement> elements = Driver.FindElementsBy(By.XPath("//table[@id='mrocontent_dgShipments']//tr[@class='TableBody']//td[1]"));
                List<string> shipmentIDs = new List<string>();
                if (elements.Count > 0)
                {
                    foreach (var _shipmentID in elements)
                    {
                        IWebElement elementAtThirdIndex = elements[2];
                        thirdShipmentId = elementAtThirdIndex.Text;
                        shipmentIDs.Add(thirdShipmentId);
                    }
                }
                shipmentIDAtIndexthree = shipmentIDs[3];
                return shipmentIDAtIndexthree;
            }

            catch (Exception ex)
            {

                throw new Exception($"Failed to set shipmenttype as PDF E-delivery to requester : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }

        }

        public bool IsNoDataFoundDisplaying()
        {
            bool isPresent = false;
            isPresent = Driver.isElementDisplayed(NoResultsFound);
            return isPresent;
        }
    }
}
