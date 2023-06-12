using AventStack.ExtentReports;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium.Remote;
using System;

namespace MRO.ROI.Automation.Common.Navigation
{
    public class FacilityMenuNavigation
    {
        public RemoteWebDriver Driver;
        public ExtentTest logger;
        public TestContext Context;
        public FacilityMenuNavigation(RemoteWebDriver driver, ExtentTest _loger, TestContext _context)
        {
            Driver = driver;
            logger = _loger;
            Context = _context;
        }

        public class ROIRequests
        {
            private const string ROIRequestsTopMenu = "ROI Requests";

            public class PatientLookup
            {
                public RemoteWebDriver Driver;
                public ExtentTest logger;
                public TestContext Context;
                public PatientLookup(RemoteWebDriver driver, ExtentTest _loger, TestContext _context)
                {
                    Driver = driver;
                    logger = _loger;
                    Context = _context;
                }
                public  void Select()
                {
                    MenuSelector menu = new MenuSelector(Driver, logger,Context);
                    menu. Select(ROIRequestsTopMenu, "Patient Lookup");
                }
            }
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

                public  void Select()
                {
                    MenuSelector menu = new MenuSelector(Driver, logger, Context);
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

                public  void Select()
                {
                    MenuSelector menu = new MenuSelector(Driver, logger,Context);
                    menu.Select(ROIRequestsTopMenu, "Log a New Request");
                }

                public  void Acceptalaert()
                {
                    throw new NotImplementedException();
                }
                public void SelectDocsRequired()
                {
                    MenuSelector menu = new MenuSelector(Driver, logger, Context);
                    menu.Select(ROIRequestsTopMenu, "Docs Required");
                }
            }
            
            public class FindRequest
            {
                public RemoteWebDriver Driver;
                public ExtentTest logger;
                public TestContext Context;
                public FindRequest(RemoteWebDriver driver, ExtentTest _loger, TestContext _context)
                {
                    Driver = driver;
                    logger = _loger;
                    Context = _context;
                }

                public  void Select()
                {
                    MenuSelector menu = new MenuSelector(Driver,logger,Context);
                    menu.Select(ROIRequestsTopMenu, "Find a Request");
                }
               
            }
        }


       
    }
}
