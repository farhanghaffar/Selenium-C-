namespace MRO.ROI.Automation.Common.Navigation
{
    public class InternalUserNavigation
    {
        public class CreateARequest
        {
            private const string ROIRequestsTopMenu = "Create a Request";

            public class CreateAPortalRequest
            {
                public static void Select()
                {
                    MenuSelector.Select(ROIRequestsTopMenu, "Create a Portal Request");
                }
            }


            public class Help
            {
                //TODO: Add nested classes and static methods
            }
        }
    }
}









