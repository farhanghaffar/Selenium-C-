using AventStack.ExtentReports;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Internal;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MRO.ROI.Automation.Selenium
{
    public static class SeleniumHelper
    {
        public static void SwitchToDefaultContent(this RemoteWebDriver driver)
        {
            try
            {
                driver.SwitchTo().DefaultContent();
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to switch to default content Exception detail: {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }

        public static void SelectCheckBoxIfUnchecked(this RemoteWebDriver driver, By by)
        {
            try
            {
                IWebElement checkBoxElement = driver.FindElementBy(by);

                if (checkBoxElement != null)
                {
                    string value = checkBoxElement.GetAttribute("checked");

                    if (value != "true")
                    {
                        checkBoxElement.Click();
                    }

                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to check show alphabet filter, Exception detail: {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }

        public static IWebElement Highlight(RemoteWebDriver Driver, IWebElement element, Boolean isOrange = false)
        {
            try
            {
                String script = String.Format(@"arguments[0].style.cssText = ""border-width: 4px; border-style: solid; border-color: {0}"";", isOrange ? "orange" : "red");
                   
                IJavaScriptExecutor jsExecutor = (IJavaScriptExecutor)Driver;
                    jsExecutor.ExecuteScript(script, new object[] { element });
            }
            catch
            {
            }
            return element;
        }


        public static IWebElement FindElementByWithOutThrow(this RemoteWebDriver driver, By by, int TimeOutValue = 15)
        {
            IWebElement webElement = null;
            Exception ex = new Exception();
            for (int i = 0; i < TimeOutValue; i++)
            {
                try
                {
                    Thread.Sleep(500);
                    webElement = driver.FindElement(by);

                    if (webElement != null && webElement.Displayed == true && webElement.Enabled == true && webElement.Size.Height > 0)
                    {
                        driver.SleepTheThread(1);
                        break;
                    }
                }

                catch (Exception exe)
                {
                    ex = exe;
                    driver.SleepTheThread(1);
                }
            }
            return webElement;
        }

        public static IWebElement FindElementUntillElementDisplayed(this RemoteWebDriver driver, By by, int TimeOutValue = 15)
        {
            IWebElement webElement = null;
            Exception ex = new Exception();
            for (int i = 0; i < TimeOutValue; i++)
            {
                try
                {
                    Thread.Sleep(500);
                    webElement = driver.FindElement(by);

                    if (webElement != null && webElement.Displayed == true && webElement.Enabled == true && webElement.Size.Height > 0)
                    {
                        webElement = driver.FindElement(by);
                        driver.SleepTheThread(1);
                        break;
                    }
                    webElement = null;
                }

                catch (Exception exe)
                {
                    ex = exe;
                    driver.SleepTheThread(1);
                }
            }
            return webElement;
        }


        public static bool WaitTillElementDisappear(this RemoteWebDriver driver, By by, int MaxTimeToWait = 60)
        {
            bool elementDisappeared = false;
            try
            {
                for (int i = 0; i < MaxTimeToWait; i++)
                {
                    try
                    {
                        IWebElement ele = driver.FindElementBy(by,3);
                        if (ele == null || ele.Displayed == false)
                        {
                            elementDisappeared = true;
                            break;
                        }
                        driver.SleepTheThread(1);
                    }
                    catch
                    {
                        elementDisappeared = true;
                        break;
                    }
                }
                return elementDisappeared;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static void MoveToElement(this RemoteWebDriver driver, By by)
        {
            try
            {
                IWebElement element = driver.FindElementBy(by);
                Actions action = new Actions(driver);
                action.MoveToElement(element);
                action.Click().Perform();

            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to move and click with by :{by} and whose exception message is :{ex.Message} \n whose stack trace is :{ex.StackTrace}");
            }
        }


        public static void MoveToElementActions(this RemoteWebDriver driver, IWebElement element)
        {
            try
            {
                Actions action = new Actions(driver);
                action.MoveToElement(element);
                action.Perform();

            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to move whose exception message is :{ex.Message} \n whose stack trace is :{ex.StackTrace}");
            }
        }

        public static void Click(this RemoteWebDriver driver, By by, int timeOut =10)
        {
            Exception ex = null; ;
            bool isClicked = false;
            int retryCount = 4;
            int runCount = 1;
            Actions actions = new Actions(driver);
            while (runCount != retryCount)
            {
                try
                {
                    IWebElement element = driver.FindElementBy(by, timeOut);
                    actions.MoveToElement(element).Click().Build().Perform();
                    isClicked = true;
                    break;

                }
                catch(StaleElementReferenceException ex1)
                {
                    driver.SleepTheThread(1);
                    runCount += 1;
                    ex = ex1;
                }
                catch (Exception _ex)
                {
                    driver.SleepTheThread(1);
                    runCount += 1;
                    ex = _ex;
                }
               
            }
            if(isClicked==false)
            {
                throw new Exception($"Failed to click the element with by :{by} {Environment.NewLine} Excepion:{ex}");
            }
        }

        public static void SendKeysUsingJavaScript(this RemoteWebDriver driver, By by, string valueToSend)
        {
            try
            {
                IWebElement element = driver.FindElementBy(by);
                IJavaScriptExecutor js = (IJavaScriptExecutor)driver;

                ((IJavaScriptExecutor)js).ExecuteScript($"arguments[0].value='{valueToSend}';", element);
            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to send keys Exception messag is : {ex}");
            }

        }

        public static void ClickOnDisplayedElement(this RemoteWebDriver driver, By by, int timeOut = 10)
        {
            Exception ex = null; ;
            bool isClicked = false;
            int retryCount = 4;
            int runCount = 1;
            Actions actions = new Actions(driver);
            while (runCount != retryCount)
            {
                try
                {
                    var elements = driver.FindElementsBy(by, timeOut);
                    foreach (var element in elements)
                    {
                        if (element.Displayed)
                        {
                            driver.SleepTheThread(2);
                            element.Click();
                            driver.SleepTheThread(2);
                            isClicked = true;
                            break;
                        }
                    }
                    if (isClicked == true)
                    { break; }
                    else
                    {
                        driver.SleepTheThread(1);
                        runCount += 1;
                    }
                
                }
                catch
                {
                    
                }

            }
            if (isClicked == false)
            {
                throw new Exception($"Failed to click the element with by :{by} {Environment.NewLine} Excepion:{ex}");
            }

        }

        public static void JavaScriptClick(this RemoteWebDriver driver, By by)
        {
            Exception ex = null; ;
            bool isClicked = false;
            int retryCount = 4;
            int runCount = 1;
            Actions actions = new Actions(driver);
            while (runCount != retryCount)
            {
                try
                {
                    IWebElement element = driver.FindElementBy(by);
                    String javascript = "arguments[0].click()";
                    IJavaScriptExecutor jsExecutor = (IJavaScriptExecutor)driver;
                    jsExecutor.ExecuteScript(javascript, element);
                    isClicked = true;
                    break;

                }
                catch (Exception _ex)
                {
                    driver.SleepTheThread(1);
                    runCount += 1;
                    ex = _ex;
                }

            }
            if (isClicked == false)
            {
                throw new Exception($"Failed to click the element with by :{by} {Environment.NewLine} Excepion:{ex}");
            }
        }

        public static void JavaScriptClickWithElement(this RemoteWebDriver driver, IWebElement element)
        {
            Exception ex = null; ;
            bool isClicked = false;
            int retryCount = 4;
            int runCount = 1;
            Actions actions = new Actions(driver);
            while (runCount != retryCount)
            {
                try
                {
                    String javascript = "arguments[0].click()";
                    IJavaScriptExecutor jsExecutor = (IJavaScriptExecutor)driver;
                    jsExecutor.ExecuteScript(javascript, element);
                    isClicked = true;
                    break;

                }
                catch (Exception _ex)
                {
                    driver.SleepTheThread(1);
                    runCount += 1;
                    ex = _ex;
                }

            }
            if (isClicked == false)
            {
                throw new Exception($"Failed to click the element {Environment.NewLine} Excepion:{ex}");
            }
        }

        public static void ClickAndCheckNextElement(this RemoteWebDriver driver, By by, By nextBy)
        {
            IWebElement nextElement = null;
            Actions actions = new Actions(driver);
            try
            {
                IWebElement element = driver.FindElementByWithOutThrow(by, 4);
                if (element != null)
                {
                    element.Click();
                }
                driver.SleepTheThread(3);
                nextElement = driver.FindElementByWithOutThrow(nextBy, 4);
                int i = 1;
                while (i <= 5 && nextElement == null)
                {
                    IWebElement _element = driver.FindElementByWithOutThrow(by, 6);
                    if (_element != null)
                    {
                        _element.Click();
                    }
                    driver.SleepTheThread(3);
                    nextElement = driver.FindElementByWithOutThrow(nextBy, 6);
                    i++;

                }
            }
            catch (Exception _ex)
            {
                throw new Exception($"Failed to click the element {Environment.NewLine} Excepion:{_ex}");
            }
        }

        public static void AcceptAlert(this RemoteWebDriver driver)
        {
            try
            {
                driver.SwitchTo().Alert().Accept();
            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to accept the alert {Environment.NewLine} Excepion:{ex}");
            }
        }


        public static void SwitchToAlert(this RemoteWebDriver driver)
        {
            //try
            //{
            //    driver.SleepTheThread(2);
            //    driver.SwitchTo().Alert().Accept();
            //}
            //catch (Exception ex)
            //{
            //    throw new Exception($"Failed to accept the alert {Environment.NewLine} Excepion:{ex}");
            //}
        }
        public static void DirectClick(this RemoteWebDriver driver, By by, int timeOut=10)
        {
            Exception ex = null; ;
            bool isClicked = false;
            int retryCount = 4;
            int runCount = 1;
            Actions actions = new Actions(driver);
            while (runCount != retryCount)
            {
                try
                {
                    IWebElement element = driver.FindElementBy(by,timeOut);
                    element.Click();
                    isClicked = true;
                    break;

                }
                catch (StaleElementReferenceException ex1)
                {
                    driver.SleepTheThread(1);
                    runCount += 1;
                    ex = ex1;
                }
                catch (Exception _ex)
                {
                    driver.SleepTheThread(1);
                    runCount += 1;
                    ex = _ex;
                }

            }
            if (isClicked == false)
            {
                throw new Exception($"Failed to click the element with by :{by} {Environment.NewLine} Excepion:{ex}");
            }
        }

        public static void SwitchToWindow(this RemoteWebDriver driver,string windowName)
        {
            try
            {
                List<string> windowHandles=driver.WindowHandles.ToList<string>();
                foreach (var window in windowHandles)
                {
                    driver.SwitchTo().Window(window);
                    if (driver.Title == windowName)
                    {
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to switch to wndiow whose window name is {windowName} {Environment.NewLine} Excepion:{ex}");
            }
            
        }

        public static void SwitchToWindowAndClose(this RemoteWebDriver driver, string windowName)
        {
            try
            {
                List<string> windowHandles = driver.WindowHandles.ToList<string>();
                foreach (var window in windowHandles)
                {
                    driver.SwitchTo().Window(window);
                    if (driver.Title == windowName)
                    {
                        driver.Close();
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to switch to wndiow whose window name is {windowName} {Environment.NewLine} Excepion:{ex}");
            }
        }

        public static void CloseTheWindow(this RemoteWebDriver driver, string windowName)
        {
            try
            {
                List<string> windowHandles = driver.WindowHandles.ToList<string>();
                foreach (var window in windowHandles)
                {
                    if (window == windowName)
                    {
                        driver.Close();
                        break;
                    }
                }
            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to close the window {Environment.NewLine} Excepion:{ex}");
            }
        }

        public static void SwitchToWindowAndCloseOtheThanMatchedWindow(this RemoteWebDriver driver, string windowName)
        {
            try
            {
                List<string> windowHandles = driver.WindowHandles.ToList<string>();
                foreach (var window in windowHandles)
                {
                    driver.SwitchTo().Window(window);
                    if (driver.Title != windowName)
                    {
                        driver.Close();
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to switch to wndiow whose window name is {windowName} {Environment.NewLine} Excepion:{ex}");
            }
        }

        public static string GetText(this RemoteWebDriver driver, By by)
        {
            string text = string.Empty;
            try
            {
                IWebElement element = driver.FindElementBy(by);
                driver.ScrollIntoView(element);
                text = element.Text;
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to return the text {Environment.NewLine} Excepion:{ex}");
            }
            return text;

        }

        public static string GetTextUsingJavaScript(this RemoteWebDriver driver, string by)
        {
            string text = string.Empty;
            try
            {
                IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
                string script = $"document.evaluate({by}, document, null, XPathResult.FIRST_ORDERED_NODE_TYPE, null).singleNodeValue;";
                var _text= js.ExecuteScript(script);
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to return the text {Environment.NewLine} Excepion:{ex}");
            }
            return text;

        }

        public static void SendKeys(this RemoteWebDriver driver, By by, string textToSend)
        {
            int retryCount = 4;
            int runCount = 1;
            Actions actions = new Actions(driver);
            while (runCount != retryCount)
            {
                try
                {
                    IWebElement element = driver.FindElementBy(by);
                    actions.MoveToElement(element);
                    actions.Click();
                    actions.SendKeys(textToSend).Build().Perform();
                    break;

                }
                catch (Exception ex)
                {
                    driver.SleepTheThread(1);
                    runCount += 1;

                }
            }
        }

        public static IWebElement FindElementBy(this RemoteWebDriver driver, By by, int TimeOutValue = 60)
        {
            IWebElement webElement = null;
            Exception ex = new Exception();
            for (int i = 0; i < TimeOutValue; i++)
            {
                try
                {
                    webElement = driver.FindElement(by);
                    if (webElement != null)
                    {
                        driver.SleepTheThread(1);
                        break;
                    }
                }
                
                catch (Exception exe)
                {
                    ex = exe;
                    driver.SleepTheThread(1);
                }
            }
            if (webElement == null)
            {

                throw new Exception($"Failed to find element with by :{by} and whose exception message is :{ex.Message} \n whose stack trace is :{ex.StackTrace}");
            }
            return webElement;
        }

        public static IWebElement FindElementByEvenHidden(this RemoteWebDriver driver, By by, int TimeOutValue = 60)
        {
            IWebElement webElement = null;
            Exception ex = new Exception();
            for (int i = 0; i < TimeOutValue; i++)
            {
                try
                {
                    Thread.Sleep(500);
                    webElement = driver.FindElement(by);
                    if (webElement != null)
                    {
                        driver.SleepTheThread(1);
                        break;
                    }
                }

                catch (Exception exe)
                {
                    ex = exe;
                    driver.SleepTheThread(1);
                }
            }
            if (webElement == null)
            {

                throw new Exception($"Failed to find element with by :{by} and whose exception message is :{ex.Message} \n whose stack trace is :{ex.StackTrace}");
            }
            return webElement;
        }

        public static List<IWebElement> FindElementsBy(this RemoteWebDriver driver, By by, int TimeOutValue = 60)
        {
            ReadOnlyCollection<IWebElement> webElements = null;

            List<IWebElement> elements = new List<IWebElement>();
            try
            {
                for (int i = 0; i < TimeOutValue; i++)
                {
                    try
                    {
                        webElements = driver.FindElements(by);

                        if (webElements.Count > 0)
                        {
                            break;
                        }
                    }
                    catch
                    {
                        driver.SleepTheThread(1);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to find element with by :{by} and whose exception message is :{ex.Message} \n whose stack trace is :{ex.Message}");
            }

            if (webElements != null)
            {
                foreach (var webelement in webElements)
                {
                    elements.Add(webelement);

                }
            }
            return elements;
        }

        public static List<string> FindElementsByReturnsInnerText(this RemoteWebDriver driver, By by, int TimeOutValue = 60)
        {
            ReadOnlyCollection<IWebElement> webElements = null;

            List<string> elements = new List<string>();
            try
            {
                for (int i = 0; i < TimeOutValue; i++)
                {
                    try
                    {
                        webElements = driver.FindElements(by);

                        if (webElements.Count > 0)
                        {
                            break;
                        }
                    }
                    catch
                    {
                        driver.SleepTheThread(1);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to find element with by :{by} and whose exception message is :{ex.Message} \n whose stack trace is :{ex.Message}");
            }

            if (webElements != null)
            {
                foreach (var webelement in webElements)
                {
                    elements.Add(webelement.Text);
                }
            }
            return elements;
        }
        public static void ExecuteJavascript(this RemoteWebDriver driver, string script)
        {
            try
            {
                IJavaScriptExecutor js = (IJavaScriptExecutor)driver;

                ((IJavaScriptExecutor)js).ExecuteScript(script);
            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to execute the script {script} whose exception message is :{ex.Message} \n whose stack trace is :{ex.Message}");
            }
        }

        public static void ScrollToEndOfThePage(this RemoteWebDriver driver)
        {
            try
            {
                driver.ExecuteJavascript("window.scrollTo(0, document.body.scrollHeight)");
                driver.SleepTheThread(5);
            }
            catch 
            {

            }
        }

        public static void SendKeysUsingJavaScript(this RemoteWebDriver driver, string id, string valueToSend)
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;

            ((IJavaScriptExecutor)js).ExecuteScript($"document.getElementById({id}).setAttribute('value', '{valueToSend}')");
        }

        public static void ScrollIntoViewAndClick(this RemoteWebDriver driver, By by)
        {
            try
            {
                var element = driver.FindElementBy(by);
                IJavaScriptExecutor executor = (IJavaScriptExecutor)driver;
                executor.ExecuteScript("arguments[0].scrollIntoView(true);", element);
                driver.DirectClick(by);
            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to scroll into view and click Exception messag is : {ex}");
            }
        }

        public static void ScrollIntoView(this RemoteWebDriver driver, IWebElement element = null, By by =null)
        {
            IWebElement _element = null; 
            try
            {
                if(by!=null)
                {
                    _element = driver.FindElementBy(by);
                }
                else
                {
                    _element = element;
                }
                IJavaScriptExecutor executor = (IJavaScriptExecutor)driver;
                executor.ExecuteScript("arguments[0].scrollIntoView(true);", _element);
            }
            catch (Exception ex)
            {
            }
        }


        public static void ScrollToElement(this RemoteWebDriver driver, By by)
        {
            try
            {
                var element = driver.FindElementBy(by);
                IJavaScriptExecutor executor = (IJavaScriptExecutor)driver;
                executor.ExecuteScript("arguments[0].scrollIntoView(true);", element);
            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to scroll into view and click Exception messag is : {ex}");
            }
        }

        public static void ScrollToElementUsingElement(this RemoteWebDriver driver, IWebElement element)
        {
            try
            {
                IJavaScriptExecutor executor = (IJavaScriptExecutor)driver;
                executor.ExecuteScript("arguments[0].scrollIntoView(true);", element);
            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to scroll into view and click Exception messag is : {ex}");
            }
        }

        public static void SelectValueFromOptionsTypeDD(this RemoteWebDriver driver, By by, string valueToSelect)
        {
            try
            {
                var ddElement = driver.FindElement(by);
                var selectElement = new SelectElement(ddElement);
                selectElement.SelectByValue(valueToSelect);
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to select a value from DD Exception messag is : {ex}");
            }
        }

        public static void SelectNameFromOptionsTypeDD(this RemoteWebDriver driver, By by, string nameToSelect)
        {
            try
            {
                var ddElement = driver.FindElement(by);
                var selectElement = new SelectElement(ddElement);
                selectElement.SelectByText(nameToSelect);
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to select a name from DD Exception messag is : {ex}");
            }
        }

        public static void SelectValueFromDD(this RemoteWebDriver driver, By by, string valueToSelect)
        {
            try
            {
                Actions actions = new Actions(driver);
                var elements = driver.FindElementsBy(by);
                if (elements.Count > 0)
                {
                    for (int i = 0; i <elements.Count; i++)
                    {
                        if (elements[i].Displayed && elements[i].Enabled)
                        {
                            if (elements[i].Text.Equals(valueToSelect))
                            {
                                actions.MoveToElement(elements[i]);
                                driver.SleepTheThread(1);
                                actions.Click().Build().Perform();
                                break;
                            }
                        }
                    }
                }

            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to select the value from DD whose value is :{valueToSelect}");
            }
        }

        public static void SelectValueFromDDDirectClick(this RemoteWebDriver driver, By by, string valueToSelect)
        {
            try
            {
                var elements = driver.FindElementsBy(by);
                if (elements.Count > 0)
                {
                    for (int i = 0; i < elements.Count; i++)
                    {
                        if (elements[i].Displayed && elements[i].Enabled)
                        {
                            if (elements[i].Text.Equals(valueToSelect))
                            {
                                elements[i].Click();
                                break;
                            }
                        }
                    }
                }

            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to select the value from DD whose value is :{valueToSelect}");
            }
        }

        public static void ClearContent(this RemoteWebDriver driver, By by)
        {
            try
            {
                IWebElement ele = driver.FindElementBy(by);
                int count = ele.GetAttribute("value").Length;
                while (count > 0)
                {
                    ele.SendKeys(Keys.Backspace);
                    count--;
                }

            }
            catch (Exception ex)
            {

            }

        }

        public static void ClearText(this RemoteWebDriver driver, By by)
        {
            IWebElement ele = driver.FindElementBy(by);
            ele.Clear();
            try
            {
                ele = driver.FindElementBy(by);
                if (!String.IsNullOrEmpty(ele.Text))
                {
                    driver.ClearContent(by);
                }
            }
            catch (Exception ex)
            {

            }

        }

        public static IJavaScriptExecutor Scripts(this RemoteWebDriver driver)
        {
            return (IJavaScriptExecutor)driver;
        }

        public static void WaitUntilDOMLoaded(this RemoteWebDriver driver)
        {
            //driver.IncreaseOnWait();
            var wait = new WebDriverWait(driver, new TimeSpan(0, 0, 55));
            wait.Until(x => driver.Scripts().ExecuteScript("return document.readyState").Equals("complete"));
            driver.TurnOnWait();
        }

        public static void TurnOffWait(this RemoteWebDriver driver)
        {
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(0);
        }

        public static void TurnOnWait(this RemoteWebDriver driver)
        {
            //driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(50);
        }

        public static void Wait(this RemoteWebDriver driver, TimeSpan timeSpan)
        {
            Thread.Sleep((int)(timeSpan.TotalSeconds * 1000));
        }

        public static int GenerateRandomNumber(int firstNumber, int lastNumber)
        {
            try
            {
                Random rand = new Random();
                return rand.Next(firstNumber, lastNumber);
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to generate random number: " + ex.Message);
            }
        }

        public static void SleepTheThread(this RemoteWebDriver driver,int seconds)
        {
            try
            {
                Thread.Sleep(seconds * 1000);
            }
            catch (Exception ex)
            {
                throw new Exception("There is some exception occured : " + ex.Message);
            }
        }

        public static void WaitForPageToLoad(this RemoteWebDriver driver, double PageLoadTimeOutinSeconds = 120.0)
        {
            try
            {

                bool objAvailable = false;
                string str_ExceptionMessage = string.Empty;
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(PageLoadTimeOutinSeconds));
                IJavaScriptExecutor javascript = driver as IJavaScriptExecutor;
                if (javascript == null)
                    throw new ArgumentException("driver", "Driver must support javascript execution");
                objAvailable = wait.Until((d) =>
                {
                    try
                    {
                        string readyState = javascript.ExecuteScript(
                        "if (document.readyState) return document.readyState;").ToString();
                        return readyState.ToLower() == "complete";
                    }
                    catch (InvalidOperationException e)
                    {
                        str_ExceptionMessage = "unable to get browser";
                        //Window is no longer available
                        return e.Message.ToLower().Contains("unable to get browser");
                    }
                    catch (WebDriverException e)
                    {
                        //Browser is no longer available
                        str_ExceptionMessage = "unable to connect";
                        return e.Message.ToLower().Contains("unable to connect");
                    }
                    catch (Exception ex)
                    {
                        str_ExceptionMessage = ex.Message;
                        return false;
                    }
                });
            }
            catch
            {
            }
        }

        public static string ToShortMonthName(this DateTime dateTime)
        {
            return System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.GetAbbreviatedMonthName(dateTime.Month);
        }

        public static string GetYear(this DateTime dateTime)
        {
            return dateTime.Year.ToString();
        }
        public static class ROITestFileUploads
        {
            public static string LocationPath = Path.GetFullPath(Path.Combine(Assembly.GetExecutingAssembly().Location,"..", "Files"));
        }

        public static void HighlightingWebElement(this RemoteWebDriver driver, By by)
        {
            try
            {
                var element = driver.FindElementBy(by);
                IJavaScriptExecutor executor = (IJavaScriptExecutor)driver;
                executor.ExecuteScript("arguments[0].style.border='3px solid red'", element);
            }
            catch (Exception ex)
            {



                throw new Exception($"Failed to Highlighting WebElement, Exception messag is : {ex}");
            }
        }

        public static bool VerifyWebElement(this RemoteWebDriver driver, By by)
        {
            try
            {
                bool isDisplayed = false;
                IWebElement objWebElement = driver.FindElementBy(by);
                if (objWebElement.Displayed == true)
                {
                    isDisplayed = true;
                }
                else
                {
                    isDisplayed = false;
                }
                return isDisplayed;
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to verify edit requester info page : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");

            }
        }

        public static void WaitInSeconds(this RemoteWebDriver driver, int second)
        {
            Thread.Sleep((int)(TimeSpan.FromSeconds(second).TotalSeconds * 1000));
        }

        public static IWebElement FindElementById(this RemoteWebDriver driver, By by, int TimeOutValue = 60)
        {
            IWebElement webElement = null;
            Exception ex = new Exception();
            for (int i = 0; i < TimeOutValue; i++)
            {
                try
                {
                    webElement = driver.FindElementById(by);
                    if (webElement != null)
                    {
                        driver.SleepTheThread(1);
                        break;
                    }
                }

                catch (Exception exe)
                {
                    ex = exe;
                    driver.SleepTheThread(1);
                }
                if(i == TimeOutValue)
                {
                    break;
                }
            }
            if (webElement == null)
            {

                throw new Exception($"Failed to find element with by :{by} and whose exception message is :{ex.Message} \n whose stack trace is :{ex.StackTrace}");
            }
            return webElement;
        }
        public static void ScrollIntoViewSmoothly(this RemoteWebDriver driver, By by)
        {
            IJavaScriptExecutor je = (IJavaScriptExecutor)driver;
            je.ExecuteScript("arguments[0].scrollIntoView({behavior: 'smooth', block: 'center', inline: 'nearest'})",
                    driver.FindElementBy(by));
        }

        public static bool isElementDisplayed(this RemoteWebDriver driver, By by)
        {
            bool isDisplay = false; ;
            try
            {
                driver.SleepTheThread(1);
                isDisplay =  driver.FindElementBy(by, 10).Displayed;
                return isDisplay;
            }
            catch
            {
                return isDisplay;
            }
        }

        public static void HideElement(this RemoteWebDriver driver, By by)
        {
            IJavaScriptExecutor je = (IJavaScriptExecutor)driver;
            je.ExecuteScript("arguments[0].style.display = 'none';",
                    driver.FindElementBy(by));
        }

        public static bool IsAlertPresent(this RemoteWebDriver driver)
        {
            try
            {
                var wait = new WebDriverWait(driver, new TimeSpan(0, 0, 60));
                wait.Until(ExpectedConditions.AlertIsPresent());
                driver.SwitchTo().Alert();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public static string GetAttributeValue(this RemoteWebDriver driver, By by, string attribute)
        {
            string text = string.Empty;
            try
            {
                IWebElement element = driver.FindElementBy(by);
                driver.ScrollIntoView(element);
                text = element.GetAttribute(attribute);
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to return the text {Environment.NewLine} Excepion:{ex}");
            }
            return text;

        }
        public static List<IWebElement> FindElementsByInTenSeconds(this RemoteWebDriver driver, By by, int TimeOutValue = 10)
        {
            ReadOnlyCollection<IWebElement> webElements = null;

            List<IWebElement> elements = new List<IWebElement>();
            try
            {
                for (int i = 0; i < TimeOutValue; i++)
                {
                    try
                    {
                        webElements = driver.FindElements(by);

                        if (webElements.Count > 0)
                        {
                            break;
                        }
                    }
                    catch
                    {
                        driver.SleepTheThread(1);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to find element with by :{by} and whose exception message is :{ex.Message} \n whose stack trace is :{ex.Message}");
            }

            if (webElements != null)
            {
                foreach (var webelement in webElements)
                {
                    elements.Add(webelement);

                }
            }
            return elements;
        }

        public static int ReturnElementListSize(this RemoteWebDriver driver, By locator)
        {
            List<IWebElement> elements = new List<IWebElement>();

            elements = FindElementsByInTenSeconds(driver, locator);

            return elements.Count;
        }

        public static void RefreshWebPage(this RemoteWebDriver driver)
        {
            driver.Navigate().Refresh();
        }
    }
}
