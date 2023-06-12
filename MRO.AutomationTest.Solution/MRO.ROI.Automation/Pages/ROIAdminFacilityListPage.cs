using AventStack.ExtentReports;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MRO.ROI.Automation.Common;
using MRO.ROI.Automation.Selenium;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using System;

namespace MRO.ROI.Automation.Pages
{
    public class ROIAdminFacilityListPage
    {
        public RemoteWebDriver Driver;
        public ExtentTest logger;
        public TestContext Context;
        public ROIAdminFacilityListPage(RemoteWebDriver driver, ExtentTest _loger, TestContext _context)
        {
            Driver = driver;
            logger = _loger;
            Context = _context;
        }

        public By dAlphabetHyperLink = By.XPath("//*[@id='mrocontent_alphaFilter_lnkD']");
        public By dukefacilityIcon = By.XPath("//a[contains(text(),'Duke University Health System')]/..//a");
        public By showAlphaChk = By.Id("mrocontent_cbAlphaFilter");
        public string roiTestFacilityIcon = "//*[@href=\"javascript:frmServer.mrocontent_tblFacilities_hidCommand.value='Login|1';frmServer.mrocontent_tblFacilities_btnCommand.click();\"]";
        public By showAlphabetChckBx = By.Id("mrocontent_cbAlphaFilter");
        public By mAlphabetHyperLink = By.XPath("//*[@id='mrocontent_alphaFilter_lnkM']");
        public By tblFacilities = By.XPath("//*[@id='mrocontent_tblFacilities']/tbody/tr[1]/td[3]/a[4]");
        public string facilityIconLink = "//*[@href=\"javascript:frmServer.mrocontent_tblFacilities_hidCommand.value='Login|537';frmServer.mrocontent_tblFacilities_btnCommand.click();\"]";
        public By facilityIcon = By.XPath("//a[contains(text(),'MRO Automated Regression Test')]/..//a");

        public By FacilityListPageHeader = By.XPath("//td[@id='MasterHeaderText']");
        public By RAlphabetHyperLink = By.XPath("//a[@id='mrocontent_alphaFilter_lnkR']");
        public By TblFacilities = By.XPath("//*[@id='mrocontent_tblFacilities']/tbody/tr[1]/td[3]/a[4]");
        public string FacilityIconLink = "//*[@href=\"javascript:frmServer.mrocontent_tblFacilities_hidCommand.value='ViewFeatures|1';frmServer.mrocontent_tblFacilities_btnCommand.click();\"]";
        public string FacilityIconLink1 = "//*[@href=\"javascript:frmServer.mrocontent_tblFacilities_hidCommand.value='ViewFeatures|3';frmServer.mrocontent_tblFacilities_btnCommand.click();\"]";
        public string ROITCompIconLink = "//*[@href=\"javascript:frmServer.mrocontent_tblFacilities_hidCommand.value='Login|1';frmServer.mrocontent_tblFacilities_btnCommand.click();\"]";
        public By rAlphabetHyperLink = By.XPath("//a[@id='mrocontent_alphaFilter_lnkR']");
        public By selectPageNumber3lnk = By.XPath("//*[@id='mrocontent_tblFacilities']/tbody/tr[1]/td[3]/a[3]");
        public By roiNativePdfTestFacilityIcon = By.XPath("//*[@id='mrocontent_dgFacilities']/tbody/tr[6]/td[3]/a[1]");
        public By roiTestFaciltyComputericon = By.XPath("//a[contains(text(),'ROI Test Facility')]/..//a");
        public string pageNumberTwoLink = "//*[@href=\"javascript:frmServer.mrocontent_tblFacilities_hidCommand.value='Pager|2';frmServer.mrocontent_tblFacilities_btnCommand.click();\"]";
        public By tblFacilitiesLinkEight = By.XPath("//*[@id='mrocontent_tblFacilities']//a[contains(text(),'8')]");
        public By mroExpressTestFacility = By.XPath("//a[contains(text(),'MRO eXpress TEST')]/..//a");
        public By mroFacilityList = By.XPath("//a[contains(text(),'M')]");
        public string mroartfGearIcon = "//*[@href=\"javascript:frmServer.mrocontent_tblFacilities_hidCommand.value='ViewFeatures|537';frmServer.mrocontent_tblFacilities_btnCommand.click();\"]";
        public string RenownHealthGearIcon = "//*[@href=\"javascript:frmServer.mrocontent_tblFacilities_hidCommand.value='ViewFeatures|154';frmServer.mrocontent_tblFacilities_btnCommand.click();\"]";
        public By roiNativePdfTestFacilityGearIcon = By.XPath("//a[contains(text(),'ROI Native PDF Test')]//following-sibling::a");
        public string ROITFacility = "//*[@href=\"javascript:frmServer.mrocontent_tblFacilities_hidCommand.value='Select|1';frmServer.mrocontent_tblFacilities_btnCommand.click();\"]";


        public string mroStLukesTest = "//*[@href=\"javascript:frmServer.mrocontent_tblFacilities_hidCommand.value='Select|10060';frmServer.mrocontent_tblFacilities_btnCommand.click();\"]";
        public By selectPageNumber8lnk = By.XPath("//*[@id='mrocontent_tblFacilities']/tbody/tr[1]/td[3]/a[8]");
        public By mroStLukesTestFaciltyComputericon = By.XPath("//a[contains(text(),'MRO St Lukes Missouri eLink Test 3.0')]/..//a");
        public By uAlphabetHyperLink = By.XPath("//a[@id='mrocontent_alphaFilter_lnkU']");
        public string UniversalHealthCompIconLink = "//*[@href=\"javascript:frmServer.mrocontent_tblFacilities_hidCommand.value='Login|492';frmServer.mrocontent_tblFacilities_btnCommand.click();\"]";
        public string roiNativePdfCompIconLink = "//*[@href=\"javascript:frmServer.mrocontent_tblFacilities_hidCommand.value='Login|539';frmServer.mrocontent_tblFacilities_btnCommand.click();\"]";

        public string roitFacility = "//*[@href=\"javascript:frmServer.mrocontent_tblFacilities_hidCommand.value='Select|8';frmServer.mrocontent_tblFacilities_btnCommand.click();\"]";
        public string ROIRFacility = "//*[@href=\"javascript:frmServer.mrocontent_tblFacilities_hidCommand.value='Select|2';frmServer.mrocontent_tblFacilities_btnCommand.click();\"]";
        public By workSummaryHeading = By.XPath("(//*[contains(text(), 'Work Summary')])[last()]");
        public By clickFirstLink = By.XPath("(//tr[@bgColor='#ddf0fc']//td//a[contains(@href, '2')])[1]");

        /// <summary>
        /// Go to MRO Automation Test Facility
        /// </summary>

        public ROIAdminFacilityWorkSummarypage GoToMROARTestFacility()
        {

            try
            {
                CheckShowAlphabetFilter();
                IWebElement malphabetLink = Driver.FindElementBy(mAlphabetHyperLink);
                malphabetLink.Click();
                IWebElement tblFaciliesClick = Driver.FindElementBy(tblFacilities);
                tblFaciliesClick.Click();
                Driver.Click(facilityIcon);
            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to click mro test facility Exception detail as  new Exception: {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");

            }
            return new ROIAdminFacilityWorkSummarypage(Driver, logger, Context);
        }

        public ROIAdminFacilityWorkSummarypage GoToMROARTestFacilityForROIAdminUsers()
        {

            try
            {
                Driver.SleepTheThread(5);
                Automation.Common.Iframe frame = new Automation.Common.Iframe(Driver, logger, Context);
                frame.SwitchToRoiFrame();
                Driver.Wait(TimeSpan.FromSeconds(1));
                CheckShowAlphabetFilter();
                IWebElement malphabetLink = Driver.FindElementBy(mAlphabetHyperLink);
                malphabetLink.Click();
                try
                {
                    By ele = By.XPath("//a[contains(text(), 'MRO Automated Regression Test')]");
                    Driver.Click(ele);

                }
                catch (Exception ex) 
                {
                    IWebElement tblFaciliesClick = Driver.FindElementBy(tblFacilities);
                    tblFaciliesClick.Click();
                    Driver.Click(facilityIcon);
                }
            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to click mro test facility Exception detail as  new Exception: {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");

            }
            return new ROIAdminFacilityWorkSummarypage(Driver, logger, Context);
        }

        public void NavigateToMROAutomationRegressionTestFacility()
        {
            try
            {
                CheckShowAlphabetFilter();
                IWebElement malphabetLink = Driver.FindElementBy(mAlphabetHyperLink);
                malphabetLink.Click();
                IWebElement tblFaciliesClick = Driver.FindElementBy(tblFacilities);
                tblFaciliesClick.Click();
                Driver.Click(facilityIcon);
            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to click mro test facility Exception detail as  new Exception: {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");

            }

        }

        /// <summary>
        /// Go to ROI Test Facility
        /// </summary>
        public ROIFacilityWorkSummaryPage GoToROITestFacility()
        {
            try
            {
                Automation.Common.Iframe frame = new Automation.Common.Iframe(Driver, logger, Context);
                frame.SwitchToRoiFrame();
                Driver.WaitInSeconds(1);
                Driver.FindElementBy(showAlphaChk).Click();
                WebElementHelper helper = new WebElementHelper(Driver, logger, Context);
                Driver.WaitInSeconds(2);
                helper.ScrollIntoView(roiTestFacilityIcon, FindElementBy.Xpath);
                helper.Click_Action(roiTestFacilityIcon, FindElementBy.Xpath);
                Driver.WaitInSeconds(5);
                Driver.SwitchTo().DefaultContent();

            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to go to ROI Test facility Exception detail as : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }

            return new ROIFacilityWorkSummaryPage(Driver, logger, Context);
        }


        /// <summary>
        /// Verify Header of the page
        /// </summary>
        public void VerifyHeader()
        {
            try
            {
                string headerText = Driver.FindElementBy(FacilityListPageHeader).Text;
                Assert.AreEqual(headerText, "ROI Facility List");
            }
            catch (Exception ex)
            {

                throw new Exception($"Failed with Message Unable to verify Header : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }
        public void ClickOnROITFGearIcon()
        {
            try
            {
                Iframe frame = new Iframe(Driver, logger, Context);
                frame.SwitchToRoiFrame();
                Driver.WaitInSeconds(1);

                bool result = Driver.FindElementBy(showAlphabetChckBx).Selected;
                if (result == false)
                {
                    IWebElement alphabetCheckbox = Driver.FindElementBy(showAlphabetChckBx);
                    alphabetCheckbox.Click();
                }
                IWebElement malphabetLink = Driver.FindElementBy(RAlphabetHyperLink);
                malphabetLink.Click();
                WebElementHelper helper = new WebElementHelper(Driver, logger, Context);
                helper.ScrollIntoView(FacilityIconLink, FindElementBy.Xpath);
                helper.Click_Action(FacilityIconLink, FindElementBy.Xpath);
                frame.switchToDefaut();
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to go to click on ROI test facility gear icon Exception detail as  new Exception: {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");

            }

        }
        public void ClickOnRothInstFTGearIcon()
        {
            try
            {
                bool result = Driver.FindElementBy(showAlphabetChckBx).Selected;
                if (result == false)
                {
                    IWebElement alphabetCheckbox = Driver.FindElementBy(showAlphabetChckBx);
                    alphabetCheckbox.Click();
                }
                IWebElement malphabetLink = Driver.FindElementBy(RAlphabetHyperLink);
                malphabetLink.Click();
                WebElementHelper helper = new WebElementHelper(Driver, logger, Context);
                helper.ScrollIntoView(FacilityIconLink1, FindElementBy.Xpath);
                helper.Click_Action(FacilityIconLink1, FindElementBy.Xpath);
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to go to click on Rothman test facility gear icon Exception detail as  new Exception: {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");

            }

        }

        /// <summary>
        /// Go to Duke University Health System
        /// </summary>
        public ROIAdminFacilityWorkSummarypage GoToDukeUniversity()
        {

            try
            {
                bool result = Driver.FindElementBy(showAlphabetChckBx).Selected;
                if (result == false)
                {
                    Driver.Click(showAlphabetChckBx);
                }
                Driver.Click(dAlphabetHyperLink);
                Driver.Click(dukefacilityIcon);
            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to click Duke University  Exception detail as  new Exception: {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");

            }
            return new ROIAdminFacilityWorkSummarypage(Driver, logger, Context);
        }

        public void FilterparentBusinessUnit(string buName)
        {
            try
            {
                Driver.SendKeys(By.CssSelector("select#mrocontent_lstParentBusiness"), buName);
            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to filter parent BU Exception: {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }


        public void ClickOnSpecificAlphabet(string alphabet)
        {
            try
            {
                Driver.SleepTheThread(3);
                System.Collections.Generic.List<IWebElement> alphabets = Driver.FindElementsBy(By.XPath("//div[@id='mrocontent_pnlAlpha']//a"));

                foreach (var item in alphabets)
                {

                    if (item.Text == alphabet)
                    {
                        Driver.SleepTheThread(1);
                        String javascript = "arguments[0].click()";
                        IJavaScriptExecutor jsExecutor = (IJavaScriptExecutor)Driver;
                        jsExecutor.ExecuteScript(javascript, item);

                        var names = Driver.FindElementsBy(By.XPath("//tr[@class='TableBody']//td[3]//a[2]"));
                        if (names.Count > 0)
                        {
                            if (names[0].Text?[0] == Convert.ToChar(alphabet))
                            {
                                break;
                            }
                            else
                            {
                                item.Click();
                            }
                        }

                        break;
                    }
                }
            }
            catch (Exception ex)
            {

                throw;
            }

        }


        public void ClickOnComputerIcon(string facilityName, string alphabet)
        {
            try
            {

                CheckShowAlphabetFilter();
                IWebElement icon = Driver.FindElementByWithOutThrow(By.XPath($"//a[contains(text(),'{facilityName}')]/..//a//img"), 4);
                Driver.SleepTheThread(1);
                IWebElement secondlink = Driver.FindElementByWithOutThrow(By.XPath($"//table[@id='mrocontent_tblFacilities']//a[contains(text(),'2')]"));
                if (secondlink != null)
                {
                    secondlink.Click();
                }
                else
                {
                    ClickOnSpecificAlphabet(alphabet);
                    IWebElement oneLink = Driver.FindElementByWithOutThrow(By.XPath($"//table[@id='mrocontent_tblFacilities']//a[contains(text(),'1')]"));
                    oneLink.Click();
                    Driver.Click(By.XPath($"//table[@id='mrocontent_tblFacilities']//a[contains(text(),'2')]"));
                }
                if (icon == null)
                {
                    for (int i = 3; i < 8; i++)
                    {
                        Driver.SleepTheThread(1);
                        Driver.DirectClick(By.XPath($"//table[@id='mrocontent_tblFacilities']//a[contains(text(),'{i}')]"));
                        Driver.SleepTheThread(1);
                        icon = Driver.FindElementByWithOutThrow(By.XPath($"//a[contains(text(),'{facilityName}')]/..//a//img"), 6);
                        if (icon != null)
                        {
                            break;
                        }
                    }

                }

                Driver.Click(By.XPath($"//a[contains(text(),'{facilityName}')]/..//a//img"));
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to click computer icon by facility name{facilityName}  Exception detail as  new Exception: {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }

        public void CheckShowAlphabetFilter()
        {
            try
            {
                IWebElement alphabetElement = Driver.FindElementBy(By.XPath("//input[@id='mrocontent_cbAlphaFilter']"));

                if (alphabetElement != null)
                {
                    string value = alphabetElement.GetAttribute("checked");

                    if (value != "true")
                    {
                        alphabetElement.Click();
                    }

                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to check show alphabet filter, Exception detail: {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }

        public void ClickOnROITCompIcon()
        {
            try
            {
                bool result = Driver.FindElementBy(showAlphabetChckBx).Selected;
                if (result == false)
                {

                    IWebElement alphabetCheckbox = Driver.FindElementBy(showAlphabetChckBx);
                    alphabetCheckbox.Click();
                }

                IWebElement malphabetLink = Driver.FindElementBy(RAlphabetHyperLink);
                malphabetLink.Click();

                WebElementHelper helper = new WebElementHelper(Driver, logger, Context);
                helper.ScrollIntoView(ROITCompIconLink, FindElementBy.Xpath);
                helper.Click_Action(ROITCompIconLink, FindElementBy.Xpath);
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to go to click on Rothman test facility gear icon Exception detail as new Exception: {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");

            }

        }

        /// <summary>
        /// Go to ROI native pdf test facility
        /// </summary>
        public ROIAdminFacilityWorkSummarypage GoToRoiNativePdfTestFacility()
        {

            try
            {
                bool result = Driver.FindElementBy(showAlphabetChckBx).Selected;
                if (result == false)
                {
                    IWebElement alphabetCheckbox = Driver.FindElementBy(showAlphabetChckBx);
                    alphabetCheckbox.Click();
                }
                Driver.Click(rAlphabetHyperLink);
                Driver.Click(selectPageNumber3lnk);
                Driver.JavaScriptClick(roiNativePdfTestFacilityIcon);
            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to click roi native pdf test facility Exception detail as new Exception: {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");

            }
            return new ROIAdminFacilityWorkSummarypage(Driver, logger, Context);
        }

        public void GotoROITestFacilityComputerIcon()
        {

            try
            {
                Driver.SleepTheThread(10);
                Automation.Common.Iframe frame = new Automation.Common.Iframe(Driver, logger, Context);
                frame.SwitchToRoiFrame();
                Driver.Wait(TimeSpan.FromSeconds(1));

                CheckShowAlphabetFilter();
                IWebElement ralphabetLink = Driver.FindElementBy(RAlphabetHyperLink);
                ralphabetLink.Click();
                Driver.Click(roiTestFaciltyComputericon);
                Driver.SwitchTo().DefaultContent();
                Driver.WaitInSeconds(1);
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to click mro test facility Exception detail as  new Exception: {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");

            }

        }

        public void ClickOnRenownHealth()
        {
            try
            {
                bool result = Driver.FindElementBy(showAlphabetChckBx).Selected;
                if (result == false)
                {
                    IWebElement alphabetCheckbox = Driver.FindElementBy(showAlphabetChckBx);
                    alphabetCheckbox.Click();
                }
                IWebElement RalphabetLink = Driver.FindElementBy(RAlphabetHyperLink);
                RalphabetLink.Click();
                WebElementHelper helper = new WebElementHelper(Driver, logger, Context);
                helper.ScrollIntoView(pageNumberTwoLink, FindElementBy.Xpath);
                helper.Click_Action(pageNumberTwoLink, FindElementBy.Xpath);
                //Driver.Click(RenownHealthGearIcon);               
                helper.ScrollIntoView(RenownHealthGearIcon, FindElementBy.Xpath);
                helper.Click_Action(RenownHealthGearIcon, FindElementBy.Xpath);

            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to click Renown health as new Exception: {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");

            }
        }

        public void GoToMROExpressTestFacility()
        {
            try
            {
                CheckShowAlphabetFilter();
                Driver.Click(clickFirstLink);
                Driver.ClickAndCheckNextElement(mAlphabetHyperLink, mroFacilityList);
                Driver.ClickAndCheckNextElement(tblFacilitiesLinkEight, mroExpressTestFacility);
                Driver.DirectClick(mroExpressTestFacility);
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to click on MRO express test facility Exception: {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }

        }
        public void ClickOnMROARTFGearIcon()
        {
            try
            {
                bool result = Driver.FindElementBy(showAlphabetChckBx).Selected;
                if (result == false)
                {
                    Driver.Click(showAlphabetChckBx);
                }
                Driver.Click(mAlphabetHyperLink);
                Driver.Click(tblFacilities);
                WebElementHelper helper = new WebElementHelper(Driver, logger, Context);
                helper.ScrollIntoView(mroartfGearIcon, FindElementBy.Xpath);
                helper.Click_Action(mroartfGearIcon, FindElementBy.Xpath);
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to go to click on ROI test facility gear icon Exception detail as  new Exception: {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");

            }
        }
        /// <summary>
        /// Click on gear icon for roi native pdf test facility
        /// </summary>
        public void ClickOnGearIconROINativePdfTestFacility()
        {
            try
            {
                bool result = Driver.FindElementBy(showAlphabetChckBx).Selected;
                if (result == false)
                {
                    IWebElement alphabetCheckbox = Driver.FindElementBy(showAlphabetChckBx);
                    alphabetCheckbox.Click();
                }
                Driver.Click(rAlphabetHyperLink);
                Driver.Click(selectPageNumber3lnk);
                Driver.JavaScriptClick(roiNativePdfTestFacilityGearIcon);
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to click roi native pdf test facility gear icon Exception detail as  new Exception: {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }

        public void ClickOnROITFacility()
        {
            try
            {
                bool result = Driver.FindElementBy(showAlphabetChckBx).Selected;
                if (result == false)
                {

                    IWebElement alphabetCheckbox = Driver.FindElementBy(showAlphabetChckBx);
                    alphabetCheckbox.Click();
                }

                IWebElement malphabetLink = Driver.FindElementBy(RAlphabetHyperLink);
                malphabetLink.Click();

                WebElementHelper helper = new WebElementHelper(Driver, logger, Context);
                helper.ScrollIntoView(ROITFacility, FindElementBy.Xpath);
                helper.Click_Action(ROITFacility, FindElementBy.Xpath);
               
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to go to click on ROI test facility Exception detail as new Exception: {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");

            }
        }


        public void GoToMROSTLukesFacility()
        {
            try
            {
                CheckShowAlphabetFilter();
               
                WebElementHelper helper = new WebElementHelper(Driver, logger, Context);
                Driver.Click(mAlphabetHyperLink);
                Driver.Click(selectPageNumber8lnk);
                Driver.Wait(TimeSpan.FromSeconds(2));
                helper.ScrollIntoView(mroStLukesTest, FindElementBy.Xpath);
                helper.Click_Action(mroStLukesTest, FindElementBy.Xpath);
            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to go to MRO st lukes facility Exception detail as : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }

           
        }


        public void GoToMROSTLukesFacilityComputerIcon()
        {

            try
            {
                CheckShowAlphabetFilter();
                IWebElement ralphabetLink = Driver.FindElementBy(mAlphabetHyperLink);
                ralphabetLink.Click();
                Driver.Click(selectPageNumber8lnk);
                Driver.Click(mroStLukesTestFaciltyComputericon);
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to click  computer icon for mro  st lukes  facility Exception detail as  new Exception: {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");

            }

        }

        public void ClickOnUniversalHealthSystemCompIcon()
        {
            try
            {
                bool result = Driver.FindElementBy(showAlphabetChckBx).Selected;
                if (result == false)
                {

                    IWebElement alphabetCheckbox = Driver.FindElementBy(showAlphabetChckBx);
                    alphabetCheckbox.Click();
                }

                Driver.Click(uAlphabetHyperLink);
                WebElementHelper helper = new WebElementHelper(Driver, logger, Context);
                helper.ScrollIntoView(pageNumberTwoLink, FindElementBy.Xpath);
                helper.Click_Action(pageNumberTwoLink, FindElementBy.Xpath);               

                helper.ScrollIntoView(UniversalHealthCompIconLink, FindElementBy.Xpath);
                helper.Click_Action(UniversalHealthCompIconLink, FindElementBy.Xpath);
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to go to click on Rothman test facility gear icon Exception detail as new Exception: {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");

            }

        }

        public void ClickOnComputerIconROINativePdfTestFacility()
        {
            try
            {
                bool result = Driver.FindElementBy(showAlphabetChckBx).Selected;
                if (result == false)
                {
                    IWebElement alphabetCheckbox = Driver.FindElementBy(showAlphabetChckBx);
                    alphabetCheckbox.Click();
                }
                Driver.Click(rAlphabetHyperLink);
                Driver.Click(selectPageNumber3lnk);
                WebElementHelper helper = new WebElementHelper(Driver, logger, Context);
                helper.ScrollIntoView(roiNativePdfCompIconLink, FindElementBy.Xpath);
                helper.Click_Action(roiNativePdfCompIconLink, FindElementBy.Xpath);
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to click roi native pdf test facility gear icon Exception detail as  new Exception: {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }

        public ROIFacilityWorkSummaryPage GoToROITestFacilityName()
        {
            try
            {
                Driver.FindElementBy(showAlphaChk).Click();
                WebElementHelper helper = new WebElementHelper(Driver, logger, Context);
                helper.ScrollIntoView(roitFacility, FindElementBy.Xpath);
                helper.Click_Action(roitFacility, FindElementBy.Xpath);
            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to go to ROI Test facility Exception detail as : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }

            return new ROIFacilityWorkSummaryPage(Driver, logger, Context);
        }

        // Sandeep for "S"

        public ROIFacilityWorkSummaryPage GoToROITestFacilityNames()
        {
            try
            {
                Driver.FindElementBy(showAlphaChk).Click();
                WebElementHelper helper = new WebElementHelper(Driver, logger, Context);
                helper.ScrollIntoView(roitFacility, FindElementBy.Xpath);
                helper.Click_Action(roitFacility, FindElementBy.Xpath);
            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to go to ROI Test facility Exception detail as : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }

            return new ROIFacilityWorkSummaryPage(Driver, logger, Context);
        }
        // Sandeep for "R"

        public ROIFacilityWorkSummaryPage GoToROITestFacilityNamer()
        {
            try
            {
                Driver.FindElementBy(showAlphaChk).Click();
                WebElementHelper helper = new WebElementHelper(Driver, logger, Context);
                helper.ScrollIntoView(ROIRFacility, FindElementBy.Xpath);
                helper.Click_Action(ROIRFacility, FindElementBy.Xpath);
            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to go to ROI Test facility Exception detail as : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }

            return new ROIFacilityWorkSummaryPage(Driver, logger, Context);
        }
        public bool IsWorkSummaryHeadingShowing()
        {
            return Driver.isElementDisplayed(workSummaryHeading);
        }

    }
}
