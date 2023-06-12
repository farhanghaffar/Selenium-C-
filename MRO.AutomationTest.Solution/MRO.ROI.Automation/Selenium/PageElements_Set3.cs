using System;

namespace MRO.ROI.Automation.Selenium
{
    public static partial class PageElements
    {
        public static class ROIRequesterPortal
        //BVT EXT-Portal 
        {
            internal const string extRoiRequesterPortal_xpath = "//*[@id='mroheader_Mropagehead1_Mromenus1_Mrotopmenu1_mnuMainMenu-menuItem012']";
            internal const string intRoiRequesterPortal_xpath = "//*[@id='mroheader_Mropagehead1_Mromenus1_Mrotopmenu1_mnuMainMenu-menuItem008']";
            // ROI Requester Portal

            //   "//td[@id='mroheader_Mropagehead1_Mromenus1_Mrotopmenu1_mnuMainMenu-menuItem008']"; ROI Facility xpath

            internal const string roiRequesterPortalCode_Id = "mrocontent_txtClientCode";
            internal const string roiRequesterPortalOKBtn_Id = "mrocontent_cmdSelect";
            public const string roiReqeusterPortalSearchBtn_Id = "mrocontent_cmdSearch";
            // public const string adminverovrride_xpath = "//*[@id=\"mrocontent_dgROIAdamin\"]//td[text()='Automation, Selenium']";
            public const string adminverovrride_xpath = "//a[@title='edit user']";
            public const string verrsionovrride_id = "mrocontent_txtVersionOverride";
            public const string userlogin_id = "mrocontent_txtUser";
            public const string versavebtn_id = "mrocontent_cmdSave";
            public const string ratebaldue_id = "mrocontent_txtBalanceDue";

            //  internal const string extportalpatientname_xpath = "//*[text()=\"123, Testing\"]"; old stage1 server
            [Obsolete]
            internal const string extportalpatientname_xpath = "//*[text()=\"Again Task 322A, Test\"]";//DO NOT hard code dynamic value
            internal const string extrrequestrecords_id = "mroheader_ctl02_ctl01_ctl00_mnuMainMenu-menuItem010";
            internal const string extrequestrecords_id = "mroheader_ctl02_ctl01_ctl00_mnuMainMenu-menuItem010";
            internal const string recentlyusedfacility_id = "mrocontent_RadComboBox_RecentFacilities_Arrow";
            internal const string extportnextbtn_id = "mrocontent_btnNextBottom";
            internal const string extrequestedby_id = "mrocontent_ddlRequesters";
            internal const string extportbrowsebtn_id = "mrocontent_RadUpload1file0";


            internal const string mrocontent_txtBxLastName_id = "mrocontent_txtBxLastName";
            internal const string mrocontent_txtBxFirstName_id = "mrocontent_txtBxFirstName";
            internal const string mrocontent_txtBxDOB_id = "mrocontent_txtBxDOB";

            internal const string mrocontent_rbAnyOrAllRadioBtn_id = "mrocontent_rbAnyOrAll";
            internal const string mrocontent_btnLogRequestBtn_id = "mrocontent_btnLogRequest";
            internal const string mrocontent_cmdRequestStatusBtn_id = "mrocontent_cmdRequestStatus";
            internal const string extportalrequestid_xpath = "(//td[@class=\"TableBody\"]/b)[1]";
            internal const string extportalLogoutbtn_xpath = "//img[@title='Log Out']";

            //assertion
            internal const string mroassertions_xpath = "//td[@class=\"MastheadText\"]";
            internal const string intportassertion_xpath = "//td[@class=\"MastheadText\"]";



            //"//*[@id=\'mrocontent_dgRequests\']/tbody/tr[2]/td[4]/a]";
            //   "//[@id=\"mrocontent_dgRequests/tbody/tr[2]/td[4]/a\"]";
            //                              "//td[@id='mroheader_Mropagehead1_Mromenus1_Mrotopmenu1_mnuMainMenu-menuItem008']";
            //      "//*[@id=\"mrocontent_custCashPaymentHistory_RadGridPaymentHistory_ctl00_ctl02_ctl01_PerformInsertButton\"]";
            //*[@id="mrocontent_dgRequests"]/tbody/tr[2]/td[4]/a




        }

        //internal static string FacilityWorkSummaryPage(string v)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
