using AventStack.ExtentReports;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MRO.ROI.Automation.Selenium;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using static MRO.ROI.Automation.Utility.IniFile;

namespace MRO.ROI.Automation.Pages
{
    public class ROIAdminUSPSRateManagementPage
    {
        public RemoteWebDriver Driver;
        public ExtentTest logger;
        public TestContext Context;
        public ROIAdminUSPSRateManagementPage(RemoteWebDriver driver, ExtentTest _loger, TestContext _context)
        {
            Driver = driver;
            logger = _loger;
            Context = _context;
        }

        By updateZones = By.XPath("//input[@id='mrocontent_btnUpdateZones']");
        By zipCodeElement = By.XPath("//input[@id='ZIPCode3Digit']");
        By submit = By.XPath("//div[@id='zone-chart-1']//input[@value='Submit']");
        By valleyForgeVFZone = By.XPath("(//div[contains(text(),'193')]/../..//div//div)[2]");
        By valleyDellasVFZone = By.XPath("(//div[contains(text(),'760')]/../..//div//div)[2]");
        By valleyForgeVFZoneDomesticSite = By.XPath("//div[@id='zone-chart-col-0-zone']//div[33]");
        By dellasVFZoneDomesticSite = By.XPath("//div[@class='zone-chart-cell' and contains(text(),'760')]/../following-sibling::div//div[22]");
        By zipCodeColumn = By.XPath("//div[@id='mrocontent_GridZones_c0_i']");
        By vfZoneColumn = By.XPath("//div[@id='mrocontent_GridZones_c0_i']");
        By dellasZoneColumn = By.XPath("//div[@id='mrocontent_GridZones_c0_i']");
        By zoneChartColumns = By.XPath("//div[@id='mrocontent_GridZones_headerRow']//div//div");

        /// <summary>
        /// Click on manage zones
        /// </summary>
        public void NavigateToTab(string tabName)
        {
            Driver.SleepTheThread(10);
            Automation.Common.Iframe frame = new Automation.Common.Iframe(Driver, logger, Context);
            frame.SwitchToRoiFrame();
            Driver.Wait(TimeSpan.FromSeconds(1));
            try
            {
                Driver.DirectClick(By.XPath($"//td[contains(text(),'{tabName}')]"));
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to click on {tabName} Message : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }

        }

        public void ClickOnFedExRates()
        {
            Driver.ClickAndCheckNextElement(By.XPath($"//table[@smenuname='mnuROIAdmin']//td[contains(text(),'System')]"), By.XPath($"//td[contains(text(),'Rates & Fees')]"));
            Driver.Wait(TimeSpan.FromSeconds(2));
            IWebElement element = Driver.FindElementBy(By.XPath($"//td[contains(text(),'Rates & Fees')]"));
            Driver.MoveToElementActions(element);
            Driver.Wait(TimeSpan.FromSeconds(2));
            Driver.DirectClick(By.XPath("//td[contains(text(),'FedEx Rates')]"));
            Driver.SleepTheThread(2);
        }

       
        public void UpdateZones()
        {
            try
            {
                Driver.DirectClick(updateZones);
                Driver.WaitTillElementDisappear(By.XPath("//td[contains(text(),'Loading...')]"), 300);
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to update zones Message : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }

        public void NavigateToUSPSDomesticZoneChart()
        {
            try
            {
                string uspsUrl = Context.Properties["USPSZonesUrl"].ToString();
                IJavaScriptExecutor js = (IJavaScriptExecutor)Driver;
                ((IJavaScriptExecutor)js).ExecuteScript("window.open('" + uspsUrl + "');");
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to naviate to USPS domestic zones chart Message : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }

        public void GetUSPSVFZones()
        {
            try
            {
                string zipCode = IniHelper.ReadConfig("USPSZones", "VFZipCode");
                Driver.SwitchToWindow("Domestic Zone Chart");
                Driver.SendKeys(zipCodeElement, zipCode);
                Driver.DirectClick(submit);
                Driver.SleepTheThread(3);
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to get usps zones Message : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }


        public void GetUSPSDellasZones()
        {
            try
            {
                string zipCode = IniHelper.ReadConfig("USPSZones", "DellasZipCode");
                Driver.ClearContent(zipCodeElement);
                Driver.SendKeys(zipCodeElement, zipCode);
                Driver.DirectClick(submit);
                Driver.SleepTheThread(3);
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to get usps zones Message : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }


        [DllImport("user32.dll")]
        public static extern void mouse_event(uint dwFlags, uint dx, uint dy, uint cButtons, int dwExtraInfo);

        public int GetVallyForgeVFZone()
        {
            int retryCount = 0;
            int vfZoneValue = 0;
            try
            {
                IWebElement scrollElement = Driver.FindElementByEvenHidden(By.CssSelector("div#mrocontent_GridZones_scrollOutter"));
                System.Windows.Forms.Cursor.Position = new System.Drawing.Point(scrollElement.Size.Width + 17, scrollElement.Size.Height);
                IWebElement vfZone = null;
                while(vfZone==null && retryCount<50 )
                {
                    mouse_event(0x0002, 0, 0, 10, 0);
                    mouse_event(0x0004, 0, 0, 10, 0);
                    scrollElement = Driver.FindElementByEvenHidden(By.CssSelector("div#mrocontent_GridZones_scrollOutter"));
                    System.Windows.Forms.Cursor.Position = new System.Drawing.Point((scrollElement.Size.Width+17), (scrollElement.Size.Height));
                    vfZone = Driver.FindElementUntillElementDisplayed(By.XPath("//*[contains(text(),'193')]/../..//div[2]//div"),1);
                    retryCount++;
                }

                if(vfZone!=null)
                {
                    vfZoneValue= Convert.ToInt32(vfZone.Text);
                }
                Driver.ScrollToEndOfThePage();
                return vfZoneValue; 
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to get vf zone Message : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }

        public void wheel_element(IWebElement element, int deltaY, int offsetX, int offsetY)
        {
            try
            {
                String script = "var element = arguments[0];"
                  + "var deltaY = arguments[1];"
                  + "var box = element.getBoundingClientRect();"
                  + "var clientX = box.left + (arguments[2] || box.width / 2);"
                  + "var clientY = box.top + (arguments[3] || box.height / 2);"
                  + "var target = element.ownerDocument.elementFromPoint(clientX, clientY);"
                  + "for (var e = target; e; e = e.parentElement) {"
                    + "if (e === element) {"
                  + "target.dispatchEvent(new MouseEvent('mouseover', {view: window, bubbles: true, cancelable: true, clientX: clientX, clientY: clientY}));"
                  + "target.dispatchEvent(new MouseEvent('mousemove', {view: window, bubbles: true, cancelable: true, clientX: clientX, clientY: clientY}));"
                  + "target.dispatchEvent(new WheelEvent('wheel',     {view: window, bubbles: true, cancelable: true, clientX: clientX, clientY: clientY, deltaY: deltaY}));"
                  + "return;"
                    + "}"
                  + "}";

                IWebElement parent = (IWebElement)((IJavaScriptExecutor)Driver).ExecuteScript("return arguments[0].parentNode;", element);
                ((IJavaScriptExecutor)Driver).ExecuteScript(script, parent, deltaY, offsetX, offsetY);
            }
            catch (WebDriverException e)
            {
            }
        }

        public int GetDellasVFZone()
        {
            int retryCount = 0;
            int vfDellasValue = 0;
            try
            {
                IWebElement scrollElement = Driver.FindElementByEvenHidden(By.CssSelector("div#mrocontent_GridZones_scrollOutter"),5);
                System.Windows.Forms.Cursor.Position = new System.Drawing.Point(scrollElement.Size.Width+17, scrollElement.Size.Height);
                IWebElement vfZone = null;
                while (vfZone == null && retryCount < 50)
                {
                    mouse_event(0x0002, 0, 0, 10, 0);
                    mouse_event(0x0004, 0, 0, 10, 0);
                    scrollElement = Driver.FindElementByEvenHidden(By.CssSelector("div#mrocontent_GridZones_scrollOutter"),5);
                    System.Windows.Forms.Cursor.Position = new System.Drawing.Point((scrollElement.Size.Width+17), (scrollElement.Size.Height));
                    vfZone = Driver.FindElementUntillElementDisplayed(By.XPath("(//*[contains(text(),'760')])[3]/../..//div[3]//div"), 1);
                    retryCount++;
                }

                if (vfZone != null)
                {
                    vfDellasValue = Convert.ToInt32(vfZone.Text);
                }

                return vfDellasValue;
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to get vf zone Message : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }



        public int GetVallyForgeVFZoneFromDomesticZoneChartSite()
        {
            try
            {
                Driver.ScrollIntoView(by: valleyForgeVFZoneDomesticSite);
                string vfZone = Driver.GetText(valleyForgeVFZoneDomesticSite);
                return Convert.ToInt32(vfZone.Replace('*', ' ').Trim());
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to get vf zone from domestic site  Message : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }

        public int GetDellasVFZoneFromDomesticZoneChartSite()
        {
            try
            {
                Driver.ScrollIntoView(by: dellasVFZoneDomesticSite);
                string vfZone = Driver.GetText(dellasVFZoneDomesticSite);
                return Convert.ToInt32(vfZone.Replace('*', ' ').Trim());
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to get dellas vf zone from doemstic site  Message : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }

        public void SwitchToWindow(string windowName)
        {
            try
            {
                Driver.SwitchToWindow(windowName);
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to switch to {windowName} window Message : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }

        }

        public List<string> GetActualZoneColumns()
        {
            List<string> columns = new List<string>();

            try
            {
                var zoneChartColumns = IniHelper.ReadConfig("ZoneChartColumns", "ZoneColumns").ToString().Split(',');
                foreach (var item in zoneChartColumns)
                {
                    columns.Add(item);
;                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to initilize zone chart columns from ini file Message : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
            return columns;
        }

        public void ClickOnUSPSRates()
        {
            Driver.ClickAndCheckNextElement(By.XPath($"//li//a[text()= 'System']"), By.XPath($"//a[contains(text(),'Rates & Fees')]"));
            Driver.Wait(TimeSpan.FromSeconds(2));
            IWebElement element = Driver.FindElementBy(By.XPath($"//a[contains(text(),'Rates & Fees')]"));
            Driver.MoveToElementActions(element);
            Driver.Wait(TimeSpan.FromSeconds(2));
            Driver.SleepTheThread(2);
            Driver.DirectClick(By.XPath("//a[contains(text(),'USPS Rates')]"));
        }

        public List<string> GetZoneChartColumns()
        {
            List<string> columns = new List<string>();

            try
            {
                List<IWebElement> zoneChartColumnEle = Driver.FindElementsBy(zoneChartColumns);
                foreach (var zoneColumn in zoneChartColumnEle)
                {
                    columns.Add(zoneColumn.Text);
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to get zone chart columns Message : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
            return columns;
        }
    }
}

