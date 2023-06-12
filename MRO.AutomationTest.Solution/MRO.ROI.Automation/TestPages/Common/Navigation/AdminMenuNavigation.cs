using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MRO.ROI.Automation.Common.Navigation
{
    public class AdminMenuNavigation
    {
        public class ROIAdmnin
        {
            //private const string ROIRequestsTopMenuId = "mroheader_MROPageHead1_ctl01_ctl00_mnuMainMenu-menuItem000";
            private const string ROIRequestsTopMenu = "ROIAdmin";           

            public class FindRequest
            {
                public static void Select()
                {
                    MenuSelector.Select(ROIRequestsTopMenu, "Find a Request");
                }
            }

            public class ActionList
            {
                public static void Select()
                {
                    MenuSelector.Select(ROIRequestsTopMenu, "Action List");
                }
            }
        }

        public class MROAnalyze
        {
            //TODO: Add nested classes and static methods
        }

        public class Reports
        {
            //TODO: Add nested classes and static methods
        }

        public class Financial
        {
            //TODO: Add nested classes and static methods
        }

        public class Requesters
        {
            //TODO: Add nested classes and static methods
        }

        public class Shipping
        {
            //TODO: Add nested classes and static methods
        }

        public class Users
        {
            //TODO: Add nested classes and static methods
        }

        public class Facilities
        {
            //TODO: Add nested classes and static methods
        }

        public class System
        {
            //TODO: Add nested classes and static methods
        }

        public class Profile
        {
            //TODO: Add nested classes and static methods
        }

        public class Help
        {
            //TODO: Add nested classes and static methods
        }
    }
}
