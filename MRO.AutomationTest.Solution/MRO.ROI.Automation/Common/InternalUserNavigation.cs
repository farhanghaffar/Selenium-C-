using AventStack.ExtentReports;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium.Remote;

namespace MRO.ROI.Automation.Common.Navigation
{
    public class InternalUserNavigation
    {
        public class CreateARequest
        {
            
            private const string ROIRequestsTopMenu = "Create a Request";

            public class CreateAPortalRequest
            {
                public RemoteWebDriver Driver;
                public ExtentTest logger;
                public TestContext Context;
                public CreateAPortalRequest(RemoteWebDriver driver, ExtentTest _loger, TestContext _context)
                {
                    Driver = driver;
                    logger = _loger;
                    Context = _context;
                }
                public  void Select()
                {
                    MenuSelector mwenu = new MenuSelector(Driver,logger,Context);
                    mwenu.Select(ROIRequestsTopMenu, "Create a Portal Request");
                }
            }


            public class Help
            {
                //TODO: Add nested classes and static methods
            }
        }
    }
}









