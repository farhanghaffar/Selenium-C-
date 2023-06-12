using System;

namespace MRO.ROI.Automation.Common.Navigation
{
    public class FacilityMenuNavigation
    {
        public class ROIRequests
        {
            private const string ROIRequestsTopMenu = "ROI Requests";

            public class PatientLookup
            {
                public static void Select()
                {
                    MenuSelector.Select(ROIRequestsTopMenu, "Patient Lookup");
                }
            }
            public class Facilities
            {
                public static void Select()
                {
                    MenuSelector.Select("Facilities", "Facility List");
                }
            }
            public class LogNewRequest
            {
                public static void Select()
                {
                    MenuSelector.Select(ROIRequestsTopMenu, "Log a New Request");
                }

                public static void acceptalaert()
                {
                    throw new NotImplementedException();
                }
            }
            
            public class FindRequest
            {
                public static void Select()
                {
                    MenuSelector.Select(ROIRequestsTopMenu, "Find a Request");
                }
            }
        }

        public class Batches
        {
            //TODO: Add nested classes and static methods
        }

        public class MROAnalyze
        {
            //TODO: Add nested classes and static methods
        }

        public class Reports
        {
            //TODO: Add nested classes and static methods
        }

        public class Audits
        {
            //TODO: Add nested classes and static methods
        }

        public class Restrictions
        {
            //TODO: Add nested classes and static methods
        }

        public class Disclosures
        {
            //TODO: Add nested classes and static methods
        }

        public class Users
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
