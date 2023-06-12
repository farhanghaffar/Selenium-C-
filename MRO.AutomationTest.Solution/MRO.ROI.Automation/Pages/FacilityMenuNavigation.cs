using AventStack.ExtentReports;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MRO.ROI.Automation.Selenium;
using OpenQA.Selenium.Remote;
using System;

namespace MRO.ROI.Automation.Pages
{
    public class FacilityMenuNavigation
    {
        public class ROIRequests
        {
            private const string ROIRequestsTopMenu = "ROI Requests";

            public class Facilities
            {
                public RemoteWebDriver Driver;
                public ExtentTest logger;
                public TestContext Context;
                public Facilities(RemoteWebDriver driver, ExtentTest _loger, TestContext _context)
                {
                    Driver = driver;
                    logger = _loger;
                    Context = _context;
                }

                public void Select()
                {
                    ROIMenuSelector menu = new ROIMenuSelector(Driver, logger, Context);
                    menu.Select("Facilities", "Facility List");
                }
            }
            public class LogNewRequest
            {
                public RemoteWebDriver Driver;
                public ExtentTest logger;
                public TestContext Context;
                public LogNewRequest(RemoteWebDriver driver, ExtentTest _loger, TestContext _context)
                {
                    Driver = driver;
                    logger = _loger;
                    Context = _context;
                }


                public void Select()
                {
                    ROIMenuSelector menu = new ROIMenuSelector(Driver, logger, Context);
                    menu.Select(ROIRequestsTopMenu, "Log a New Request");
                }

                public static void acceptalaert()
                {
                    throw new NotImplementedException();
                }
            }
        }


    }
}
