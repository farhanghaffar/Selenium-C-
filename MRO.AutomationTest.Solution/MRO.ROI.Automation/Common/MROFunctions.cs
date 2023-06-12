using AventStack.ExtentReports;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MRO.ROI.Automation.Selenium;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MRO.ROI.Automation.Selenium.PageElements;

namespace MRO.ROI.Automation.Pages.Common
{
	public class MROFunctions 
	{
		public RemoteWebDriver Driver;
		public ExtentTest logger;
		public TestContext Context;
		public MROFunctions(RemoteWebDriver driver, ExtentTest _loger, TestContext _context)
		{
			Driver = driver;
			logger = _loger;
			Context = _context;
		}

		public  void ClickYes()
		{
			Driver.Wait(TimeSpan.FromSeconds(5));
			Driver.FindElement(By.Id("rbYes")).Click();
			WebElementHelper webElementHelper = new WebElementHelper(Driver, logger, Context);
			webElementHelper.Click_Enter();
		}
		
	}
}
