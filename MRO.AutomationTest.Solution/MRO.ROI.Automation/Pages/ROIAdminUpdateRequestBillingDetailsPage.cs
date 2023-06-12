using AventStack.ExtentReports;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MRO.ROI.Automation.Selenium;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Remote;
using System;

namespace MRO.ROI.Automation.Pages
{
    public class ROIAdminUpdateRequestBillingDetailsPage
    {
        public RemoteWebDriver Driver;
        public ExtentTest logger;
        public TestContext Context;
        public ROIAdminUpdateRequestBillingDetailsPage(RemoteWebDriver driver, ExtentTest _loger, TestContext _context)
        {
            Driver = driver;
            logger = _loger;
            Context = _context;
        }

        public By changelinkElement = By.XPath("//a[@id='mrocontent_lnkChangeRequestRate']");
        public By thirdPageSelection = By.XPath("//*[@id='mrocontent_tblRates']/tbody/tr[1]/td[3]/a[3]");
        public By customPostageElement = By.XPath("//a[contains(text(),'Reg_CustomPostageRate')]");
        public By applyRateValue = By.XPath("//span[contains(text(),'Reg_CustomPostageRate')]");
        public By roiAdminElement = By.XPath("//td[contains(text(),'ROIAdmin')]");
        public By recentRequestElement = By.XPath("//td[contains(text(),'Recent Requests')]");
        public By firstElementInRecentRequest = (By.XPath("(//table[starts-with(@id,'mroheader_')])[2]//tr"));
        public By rAlphabetSelection = By.XPath("//*[@id='mrocontent_alphaFilter_lnkR']");
        public By lnkRegressionBaseRate = By.XPath("//a[contains(text(),'Regression_BaseRate')]");
        public By applyRatebtn = By.XPath("//input[@id='mrocontent_cmdApplyRate']");
        public By saveAndExitBtn = By.XPath("//input[@id='mrocontent_cmdSaveExit']");
        public By selectHyperlink = By.XPath("//a[@id='mrocontent_lnkSelectRequester']");
        internal const string _isSelectHyperlinkPresent = "//a[@id='mrocontent_lnkSelectRequester']";
        public By RGBRSelect = By.XPath("//a[@id='mrocontent_lnkSelectRequester']");
        public By UpdateAnApply = By.XPath("//input[@id='mrocontent_cmdUpdatePages']");
        public By pageFee1 = By.XPath("//input[@id='mrocontent_txtPageFee1']");
        public By changeAdjustmentAmount = By.XPath("//input[@id='mrocontent_txtChangeAdjust']");
        public By changeBtn = By.XPath("//input[@id='mrocontent_cmdChangeAdjust']");
        public By doneBtn = By.XPath("//input[@id='mrocontent_cmdDone']");
        public By sAlphabetSelection = By.XPath("//*[@id='mrocontent_alphaFilter_lnkS']");
        public By lnkStateRate = By.XPath("//a[contains(text(),'State Rate - PA')]");

        /// <summary>
        /// Assign Apply rate to Reg_CustomPostageRate
        /// </summary>
        public string UpdateBillingInfoForFTP()
        {
            try
            {
                IWebElement change = Driver.FindElementBy(changelinkElement);
                change.Click();
                IWebElement rAlphabet=Driver.FindElementBy(rAlphabetSelection);
                rAlphabet.Click();
                IWebElement pageThreeClick = Driver.FindElementBy(thirdPageSelection);
                pageThreeClick.Click();
                IWebElement postageElement = Driver.FindElementBy(customPostageElement);
                postageElement.Click();
                string appliedRate = Driver.FindElementBy(applyRateValue).Text;
                return appliedRate;
                //if (appliedRate == "Reg_CustomPostageRate")
                //{
                //    logger.Log(Status.Info, "Applied Rate is Successfully  updated to Reg_CustomPostageRate");
                //}
                //else
                //{
                //    Assert.Fail("Failed to validate applied Rate is Successfully updated to Reg_CustomPostageRate");
                //}
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to upadte apply rate  with  detail exception as Message : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }

        }

        /// <summary>
        ////Go to Recent Request Page
        /// </summary>
        public ROIAdminRequestStatusPage SelectRecentRequestIDFromROIAdmin()
        {
            try
            {
                Actions action = new Actions(Driver);
                IWebElement roiRequest = Driver.FindElementBy(roiAdminElement);
                action.MoveToElement(roiRequest).Perform();
                IWebElement recentRequest = Driver.FindElementBy(recentRequestElement);
                action.MoveToElement(recentRequest).Perform();
                IWebElement submenuItem = Driver.FindElementBy(firstElementInRecentRequest);
                action.MoveToElement(submenuItem).Click().Build().Perform();
                Driver.Wait(TimeSpan.FromSeconds(1));
            }
            catch(Exception ex)
            {
                throw new Exception($"Failed to navigate Recent Request  with details exception as Message : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
            return new ROIAdminRequestStatusPage(Driver,logger,Context);

        }

        public void UpdateRegressionBaseRate()
        {
            try
            {
                WebElementHelper helper = new WebElementHelper(Driver, logger, Context);
                if (helper.IsElementPresent(_isSelectHyperlinkPresent))
                {
                    Driver.FindElementBy(selectHyperlink).Click();
                    IWebElement change = Driver.FindElementBy(changelinkElement);
                    change.Click();
                    IWebElement rAlphabet = Driver.FindElementBy(rAlphabetSelection);
                    rAlphabet.Click();
                    //IWebElement pageThreeClick = Driver.FindElementBy(thirdPageSelection);
                    //pageThreeClick.Click();
                    IWebElement regressionBaseRate = Driver.FindElementBy(lnkRegressionBaseRate);
                    regressionBaseRate.Click();
                    IWebElement applyRate = Driver.FindElementBy(applyRatebtn);
                    applyRate.Click();
                    IWebElement saveAndExit = Driver.FindElementBy(saveAndExitBtn);
                    saveAndExit.Click();
                }
                else
                {
                    IWebElement change = Driver.FindElementBy(changelinkElement);
                    change.Click();
                    IWebElement rAlphabet = Driver.FindElementBy(rAlphabetSelection);
                    rAlphabet.Click();
                    IWebElement pageThreeClick = Driver.FindElementBy(thirdPageSelection);
                    pageThreeClick.Click();
                    IWebElement regressionBaseRate = Driver.FindElementBy(lnkRegressionBaseRate);
                    regressionBaseRate.Click();
                    IWebElement applyRate = Driver.FindElementBy(applyRatebtn);
                    applyRate.Click();
                    IWebElement saveAndExit = Driver.FindElementBy(saveAndExitBtn);
                    saveAndExit.Click();
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to update Regression base rate : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }

        }

        public void ClickonRGBRSelect()
        {
            try
            {
                IWebElement rgbrSelect = Driver.FindElementBy(RGBRSelect);
                rgbrSelect.Click();

            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to update Regression base rate : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }

        }

        public void ClickUpdateAnApply()
        {
            try
            {
                IWebElement updateAndApply = Driver.FindElementBy(UpdateAnApply);
                updateAndApply.Click();

            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to click on update and apply button : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }

        }

        public void ClickSaveAndExit()
        {
            try
            {
                IWebElement saveAndExit = Driver.FindElementBy(saveAndExitBtn);
                saveAndExit.Click();

            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to click on save and exit : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }

        }

        /// <summary>
        /// Assign Apply rate to Reg_CustomPostageRate
        /// </summary>
        public void UpdateRegCustomPostageRate()
        {
            try
            {
                IWebElement change = Driver.FindElementBy(changelinkElement);
                change.Click();
                IWebElement rAlphabet = Driver.FindElementBy(rAlphabetSelection);
                rAlphabet.Click();
                IWebElement pageThreeClick = Driver.FindElementBy(thirdPageSelection);
                pageThreeClick.Click();
                IWebElement postageElement = Driver.FindElementBy(customPostageElement);
                postageElement.Click();
                string appliedRate = Driver.FindElementBy(applyRateValue).Text;
                if (appliedRate == "Reg_CustomPostageRate")
                {
                    logger.Log(Status.Info, "Applied Rate is successfully updated to Reg_CustomPostageRate");
                }
                else
                {
                    Assert.Fail("Failed to validate applied Rate is successfully updated to Reg_CustomPostageRate");
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to upadte apply rate with detail exception as Message : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }
        public void UpdatePageFee1()
        {
            try
            {
                Driver.ClearText(pageFee1);
                Driver.SendKeys(pageFee1, "16.20");
                Driver.Click(saveAndExitBtn);
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to update page fee1 value with detail exception as Message : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }
        public void ChangeAdjustmentAmount()
        {
            try
            {
                Driver.ClearText(changeAdjustmentAmount);
                Driver.SendKeys(changeAdjustmentAmount, "20.00");
                Driver.Click(changeBtn);
                Driver.Click(doneBtn);
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to update adjustment amount with detail exception as Message : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }

        }

        public void UpdateStateRate()
        {
            try
            {
                WebElementHelper helper = new WebElementHelper(Driver, logger, Context);
                if (helper.IsElementPresent(_isSelectHyperlinkPresent))
                {
                    Driver.FindElementBy(selectHyperlink).Click();
                    IWebElement change = Driver.FindElementBy(changelinkElement);
                    change.Click();
                    IWebElement rAlphabet = Driver.FindElementBy(sAlphabetSelection);
                    rAlphabet.Click();                   
                    IWebElement stateRate = Driver.FindElementBy(lnkStateRate);
                    stateRate.Click();
                    IWebElement applyRate = Driver.FindElementBy(applyRatebtn);
                    applyRate.Click();
                    IWebElement saveAndExit = Driver.FindElementBy(saveAndExitBtn);
                    saveAndExit.Click();
                }
                else
                {
                    IWebElement change = Driver.FindElementBy(changelinkElement);
                    change.Click();
                    IWebElement rAlphabet = Driver.FindElementBy(sAlphabetSelection);
                    rAlphabet.Click();
                    //IWebElement pageThreeClick = Driver.FindElementBy(thirdPageSelection);
                    //pageThreeClick.Click();
                    IWebElement stateRate = Driver.FindElementBy(lnkStateRate);
                    stateRate.Click();
                    IWebElement applyRate = Driver.FindElementBy(applyRatebtn);
                    applyRate.Click();
                    IWebElement saveAndExit = Driver.FindElementBy(saveAndExitBtn);
                    saveAndExit.Click();
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to update Regression base rate : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }


        }
        public void ChangeAdjustmentAmount(string adjbal)
        {
            try
            {
                Driver.ClearText(changeAdjustmentAmount);
                Driver.SendKeys(changeAdjustmentAmount, adjbal);
                Driver.Click(changeBtn);
                Driver.Click(doneBtn);
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to update adjustment amount with detail exception as Message : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }

        }
    }
}
