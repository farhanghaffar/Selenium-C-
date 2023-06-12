using AventStack.ExtentReports;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MRO.ROI.Automation.Selenium;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MRO.ROI.Automation.Pages
{
   public class ROIFacilityFeaturesROITestFacilityPage
    {
        public RemoteWebDriver Driver;
        public ExtentTest logger;
        public TestContext Context;
        public ROIFacilityFeaturesROITestFacilityPage(RemoteWebDriver driver, ExtentTest _loger, TestContext _context)
        {
            Driver = driver;
            logger = _loger;
            Context = _context;
        }

        public By ROI = By.XPath("//div[@class='rtsLevel rtsLevel1']//ul[@class='rtsUL']//li[2]");
        public By HasActionsAtLogging = By.XPath("//input[@id='mrocontent_cbHasActionsAtLogging']");
        public By UpdateFeatures = By.XPath("//input[@id='mrocontent_cmdUpdate']");
        public By ELink = By.XPath("//div[@class='rtsLevel rtsLevel1']//ul[@class='rtsUL']//li[6]");
        public By AddPDFPagesReleased = By.XPath("//input[@id='mrocontent_cbAddPDFPagesToReleasedDeliveredOSD']");
        public By MRNPatientLookUp = By.XPath("//input[@id='mrocontent_cbAutoMRNLookup']");
        public By chkHasPurposeOfUse = By.XPath("//input[@id='mrocontent_cbHasPurposeOfUse']");
        public By chkHasSSN = By.XPath("//input[@id='mrocontent_cbHasSSN']");
        public By licenseRequireLable = By.XPath("//*[@id='mrocontent_ctlFacilityAOD_CustomItemLocList2_tvLocations2_trial']");
        public void ClickOnROI()
        {
            try
            {
                Driver.WaitInSeconds(1);
                try
                {
                    HideLicenseRequireLabelInROiFacilityIfShowing();

                }catch(Exception ex)
                {

                }
                IWebElement roiTab = Driver.FindElementBy(ROI);
                roiTab.Click();

            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to click on ROI as  new Exception: {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");

            }
        }

        public void CheckHasActionsAtLogging()
        {
            try
            {
                IWebElement hasActionsAtLogging = Driver.FindElementBy(HasActionsAtLogging);

                if (hasActionsAtLogging != null)
                {
                    string value = hasActionsAtLogging.GetAttribute("checked");

                    if (value != "true")
                    {
                        hasActionsAtLogging.Click();
                    }

                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to check has action at logging: {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }

        public void CheckHasMRNPatientLookUp()
        {
            try
            {
                IWebElement hasMRNPatientLookUp = Driver.FindElementBy(MRNPatientLookUp);

                if (hasMRNPatientLookUp != null)
                {
                    string value = hasMRNPatientLookUp.GetAttribute("checked");

                    if (value != "true")
                    {
                        hasMRNPatientLookUp.Click();
                    }

                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to check has action at logging: {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }

        public void UnCheckHasActionsAtLogging()
        {
            try
            {
                IWebElement hasActionsAtLogging = Driver.FindElementBy(HasActionsAtLogging);

                if (hasActionsAtLogging != null)
                {
                    string value = hasActionsAtLogging.GetAttribute("checked");

                    if (value != "false")
                    {
                        hasActionsAtLogging.Click();
                    }

                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to check has action at logging: {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }

        public void ClickUpdateFeatures()
        {
            try
            {
                IWebElement updateFeatures = Driver.FindElementBy(UpdateFeatures);
                updateFeatures.Click();
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to click on update features {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }

        public void ClickOnELink()
        {
            try
            {
                Driver.ScrollIntoViewSmoothly(ELink);
                IWebElement roiTab = Driver.FindElementBy(ELink);
                roiTab.Click();

            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to click on ELink as  new Exception: {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");

            }
        }

        public void UnCheckAddPDFPagesReleased()
        {
            try
            {
                IWebElement addPDFPagesReleased = Driver.FindElementBy(AddPDFPagesReleased);

                if (addPDFPagesReleased != null)
                {
                    string value = addPDFPagesReleased.GetAttribute("checked");

                    if (value != "true"  && !(string.IsNullOrEmpty(value)))
                    {
                        addPDFPagesReleased.Click();
                    }

                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to check has action at logging: {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }

        public void CheckHasPuroseOfUse()
        {
            try
            {
                IWebElement chHasPurposeOfUse = Driver.FindElementBy(chkHasPurposeOfUse);

                if (chHasPurposeOfUse != null)
                {
                    string value = chHasPurposeOfUse.GetAttribute("checked");

                    if (value != "true")
                    {
                        chHasPurposeOfUse.Click();
                    }

                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to check has purpose of use: {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }

        public void CheckHasSSN()
        {
            try
            {
                IWebElement hasSSN = Driver.FindElementBy(chkHasSSN);

                if (hasSSN != null)
                {
                    string value = hasSSN.GetAttribute("checked");
                    if (value != "true")
                    {
                        hasSSN.Click();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to check Has SSn checkbox: {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }
        public void HideLicenseRequireLabelInROiFacilityIfShowing()
        {
            bool isDisplay = Driver.isElementDisplayed(licenseRequireLable);
            if (isDisplay)
            {
                Driver.HideElement(licenseRequireLable);
            }

        }
    }
}
