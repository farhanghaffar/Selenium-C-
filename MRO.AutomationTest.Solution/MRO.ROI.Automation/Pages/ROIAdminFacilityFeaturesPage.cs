using AventStack.ExtentReports;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MRO.ROI.Automation.Selenium;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using System;

namespace MRO.ROI.Automation.Pages
{
    public class ROIAdminFacilityFeaturesPage
    {

        public RemoteWebDriver Driver;
        public ExtentTest logger;
        public TestContext Context;
        public ROIAdminFacilityFeaturesPage(RemoteWebDriver driver, ExtentTest _loger, TestContext _context)
        {
            Driver = driver;
            logger = _loger;
            Context = _context;
        }

        public By PageHeader = By.XPath("//font[contains(text(),'ROI Test Facility')]");
        public By RequesterAPI = By.XPath("//tbody/tr[2]/td[2]/div[1]/div[1]/ul[1]/li[15]/a[1]/span[1]/span[1]/span[1]");
        public By RequestEndpoint = By.XPath("//select[@id='mrocontent_lstInboundRequestEndpoint']");
        public By UpdateFeatures = By.XPath("//input[@id='mrocontent_cmdUpdate']");
        public By roiTab = By.XPath("//span[@class='rtsTxt' and contains(text(),'ROI')]");
        public By complianceHoldChck = By.Id("mrocontent_cbHasComplianceHold");
        public By updateFeaturesBtn = By.Id("mrocontent_cmdUpdate");       
        public By EmailNotice = By.XPath("//select[@id='mrocontent_lstEmailNotice']");
        public By EmailReceipts = By.XPath("//textarea[@id='mrocontent_txtEmailRecipients']");
        public By EmailFrequency = By.XPath("//select[@id='mrocontent_lstEmailFrequency']");
        public By hasAIDashboard = By.Id("mrocontent_cbHasAIDashboard");
        public By elinkTab = By.XPath("//span[contains(text(),'eLink')]");
        public By elinkInputConfigBtn = By.Id("mrocontent_cmdMRECFolders");
        public string pdfPasswordfield = "//input[@id='mrocontent_cmbBxReqFields_Input']";
        public string pdfPasswordfieldValue = "dtPatientBirth";
        public By pdfPasswordFormatString = By.XPath("//input[@id='mrocontent_txtBxPDFPwdFormat']");
        public string elinkPdfCompressionDPI = "//input[@id='mrocontent_txtELinkPDFCompressionDPI']";
        public string elinkPdfCompressionDPIValue = "300";
        public By shippingTab = By.XPath("//span[@class='rtsTxt' and contains(text(),'Shipping')]");
        public By ckhAllowOriginalPdfDelivery = By.Id("mrocontent_cbAllowOriginalPDFDelivery");
        public By featureFaciltyComputerIcon = By.XPath("//img[@alt ='Login to Facility']");
        public void VerifyHeader()
        {
            try
            {
                string headerText = Driver.FindElementBy(PageHeader).Text;
                Assert.AreEqual(headerText, "ROI Test Facility");

            }
            catch (Exception ex)
            {

                throw new Exception($"Failed with Message Unable to verify Header : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }

        public void SetRequeseterAPIEndPointEnable()
        {
            try
            {
                IWebElement requesterAPI = Driver.FindElementBy(RequesterAPI);
                requesterAPI.Click();               
                IWebElement SetRequestEndpoint = Driver.FindElementBy(RequestEndpoint);
                SetRequestEndpoint.SendKeys("Enabled");                
                IWebElement ClickUpdateFeatures = Driver.FindElementBy(UpdateFeatures);
                ClickUpdateFeatures.Click();
             }
            catch (Exception ex)
            {

                throw new Exception($"Failed with Message Unable to verify Header : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }
        public void SetRequeseterAPIEndPointdisable()
        {
            try
            {
                IWebElement requesterAPI = Driver.FindElementBy(RequesterAPI);
                requesterAPI.Click();
                IWebElement SetRequestEndpoint = Driver.FindElementBy(RequestEndpoint);
                SetRequestEndpoint.SendKeys("Disabled");
                IWebElement ClickUpdateFeatures = Driver.FindElementBy(UpdateFeatures);
                ClickUpdateFeatures.Click();
            }
            catch (Exception ex)
            {

                throw new Exception($"Failed with Message Unable to verify Header : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }

        /// <summary>
        /// Click on ROI Tab
        /// </summary>
        public void SelectROITab()
        {
            try
            {
                Driver.Click(roiTab);
                if (Driver.FindElementBy(complianceHoldChck).Selected == false)
                {
                    Driver.Click(complianceHoldChck);
                }
                Driver.ScrollIntoViewAndClick(updateFeaturesBtn);
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed  to click roiTab with details as Message : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }

        public void SetEmailConfigurationAndUpdateFeatures()
        {
            try
            {
                IWebElement emailNotice = Driver.FindElementBy(EmailNotice);
                emailNotice.SendKeys("Enabled");
                IWebElement emailReceipts = Driver.FindElementBy(EmailReceipts);
                emailReceipts.SendKeys("e006201@cigniti.com");
                IWebElement emailFrequency = Driver.FindElementBy(EmailFrequency);
                emailFrequency.SendKeys("Per Request");
                IWebElement updateFeatures = Driver.FindElementBy(UpdateFeatures);
                updateFeatures.Click();
            }
            catch (Exception ex)
            {

                throw new Exception($"Failed with Message Unable to Set EmailConfiguration And click UpdateFeatures : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }
        public void CheckHasAIDashboard()
        {
            try
            {
                Driver.Click(roiTab);
                bool isCheck = Driver.FindElementBy(hasAIDashboard).Selected;
                if(isCheck==false)
                {
                    Driver.Click(hasAIDashboard);
                }
             
                Driver.Click(updateFeaturesBtn);                
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed click roi tab with details message as : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }
        public bool UnCheckHasAIDashboard()
        {
            try
            {
                Driver.Click(roiTab);
                bool isDisplayed = false;
                bool isCheck = Driver.FindElementBy(hasAIDashboard).Selected;
                if (isCheck == true)
                {
                    Driver.Click(hasAIDashboard);
                    isDisplayed = false;
                    
                }
                
                Driver.Click(updateFeaturesBtn);
                return isDisplayed;

            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to uncheck has AI Dashboard with details message as : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }
        public void SelectELinkTab()
        {
            try
            {
                Driver.Click(elinkTab);
                Driver.ScrollIntoViewAndClick(elinkInputConfigBtn);

            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to click elink tab with details as Message : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }
        /// <summary>
        /// Click on eLink and update features
        /// </summary>
        public void ClickOnElinkAndUpdateFeatures()
        {
            try
            {
                Driver.Click(elinkTab);
                WebElementHelper helper = new WebElementHelper(Driver, logger, Context);
                helper.ScrollIntoView(pdfPasswordfield, FindElementBy.Xpath);
                Driver.FindElementBy(By.XPath(pdfPasswordfield)).Clear();
                Driver.FindElementBy(By.XPath(pdfPasswordfield)).SendKeys(pdfPasswordfieldValue);
                Driver.FindElementBy(pdfPasswordFormatString).Clear();
                Driver.FindElementBy(pdfPasswordFormatString).SendKeys("MMddyyyy");
                helper.ScrollIntoView(elinkPdfCompressionDPI, FindElementBy.Xpath);
                Driver.FindElementBy(By.XPath(elinkPdfCompressionDPI)).Clear();
                Driver.FindElementBy(By.XPath(elinkPdfCompressionDPI)).SendKeys(elinkPdfCompressionDPIValue);
                Driver.Click(updateFeaturesBtn);

            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to click on elink and update features : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }

        /// <summary>
        /// Click on Shipping Tab
        /// </summary>
        public void SelectShippingTab()
        {
            try
            {
                Driver.Click(shippingTab);
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed  to click  shipping tab with details as Message : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }
        public void CheckAllowOriginalPdfDelivery()
        {
            try
            {
                bool isCheck = Driver.FindElementBy(ckhAllowOriginalPdfDelivery).Selected;
                if (isCheck == false)
                {
                    Driver.Click(ckhAllowOriginalPdfDelivery);
                }

                Driver.Click(updateFeaturesBtn);
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed click roi tab with details message as : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }

        public void ClickFacilityComputerIcon()
        {
            try
            {
                Driver.Click(featureFaciltyComputerIcon);
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to click mro test facility Exception detail as  new Exception: {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");

            }
        }
    }
}
