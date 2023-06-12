namespace MRO.ROI.Automation.Selenium
{
    public static partial class PageElements
    {
        public static class ROIAdminList
        {
            internal const string computericon_xpath = "//*[@href=\"javascript:frmServer.mrocontent_tblFacilities_hidCommand.value='Login|1';frmServer.mrocontent_tblFacilities_btnCommand.click();\"]";// "//*[@id=\"mrocontent_dgFacilities\"]/tbody/tr[4]/td[3]/a[1]/img";
            //  internal const string computericon_xpath = "//*[text()=\"ROI Test Facility\"]";
            //seleniumautomation login old one( "//*[@id=\"mrocontent_dgFacilities\"]/tbody/tr[2]/td[3]/a[1]/img";)

            //*[@id="mrocontent_dgFacilities"]/tbody/tr[4]/td[3]/a[1]

            // internal const string computericon_xpath = "//*[@id=\"mrocontent_dgFacilities\"]/tbody/tr[4]/td[3]/a[1]/img"; //roi-tmirza
            //*[@id=\"mrocontent_dgFacilities\"]/tbody/tr[2]/td[3]/a[1]/img
            internal const string adminlookupid_id = "mroheader_ctl02_ctl03_imgQuery";
            public const string adminchange_id = "mrocontent_lnkChangeRequestRate";
            public const string admincharr_id = "mrocontent_alphaFilter_lnkR";
            //# mrocontent_alphaFilter_lnkR
            public const string adminselecrate_id = "mrocontent_dgRates";
            internal const string factylookupid_xpath = "//*[@title='Look up by Request ID']";
            internal const string requestid_id = "mrocontent_tdRequestID";

            //    internal const string mrocontenttdRequestID_id = "mrocontent_tdRequestID";

            internal const string roiadminassgnreq_id = "mrocontent_btnAssignRequester";
            internal const string drilltofacility_id = "mrocontent_lnkFacility";
            internal const string roiadminorg_id = "mrocontent_txtSearchOrganization";
            internal const string roiadminsearchbtn_id = "mrocontent_btnSearchRequester";
            internal const string roiadminchkbox_id = "mrocontent_dgRequester_ctl00_ctl04_ClientSelectColumn1SelectCheckBox";
            internal const string oiadmincpyallbtn_id = "btnCopyAll";
            internal const string roisavebtn_id = "mrocontent_btnSave";
            internal const string roidonebtn_id = "mrocontent_btnClose";
            public const string roiadminapplyrate_id = "mrocontent_cmdApplyRate"; //apply rate button id
            //get the amount for invoice amount,balance due,adjusted balance;
            internal const string roiadminvamt_id = "mrocontent_tdInvoiceAmount";
            internal const string roiadminbaldue_id = "mrocontent_tdBalanceDue";
            internal const string roiadmadjbal_id = "mrocontent_tdAdjustedBalance";
            internal const string roidlogoutbtn_id = "mroheader_ctl02_ctl03_imgLogout";


            // page fee 1 updated on request status page verify the invoice amount,balance due,adjusted balanced updated to $26.00.  Added a 1$
            internal const string roiadminqcpass_id = "mrocontent_cmdPassQC"; // before changing the rate need to click on qc pass button.
            //change rate
            internal const string roiadmchgrate_id = "mrocontent_lnkRate";
            // update request billing details page(#reqeust id 9231930)
            internal const string roiadmpagefee2_id = "mrocontent_txtPageFee2";
            internal const string storedonpaper1_id = "mrocontent_txtPages";
            internal const string storedelectronically1_id = "mrocontent_txtPagesElec";
            internal const string updatenapply_id = "mrocontent_cmdUpdatePages";
            internal const string roiadmsavenexitbtn_xpath = "//*[value()=\"Save & Exit\"]";
            //    "//*[text()=\"MRO Delivery\"]";  //*[@id="mrocontent_cmdSaveExit"]
            internal const string roiadmsavenexitbtn_id = "mrocontent_cmdSaveExit";
            internal const string roiadmcreateinvoice_id = "mrocontent_cmdCreateInvoice";

            //    mrocontent_cmdCreateInvoice
            public const string confirmDialog_xpath = "//*[@aria-describedby='dialog-confirm']//*[text()='Yes']";
            public const string confirmDialog1_xpath = "//*[@aria-describedby='dialog-confirm']//*[text()='OK']";
            internal const string invoicetext_id = "mrocontent_mrocontent_ctl166Panel";
            internal const string invoicetext_xpath = "//*[@id=\"mrocontent_tblFaxPackages\"]//td[text()='Invoice']";
            public const string roilogcheck_id = "mrocontent_cmdLogCheck";
            internal const string roichecknbr_id = "mrocontent_txtCheckNumber";
            internal const string roibaldue_id = "mrocontent_tdBalanceDue";
            internal const string roipaymentamt_id = "mrocontent_txtPaymentAmount";
            internal const string roilogcheckviewrequester_id = "mrocontent_cmdViewRequest";
            internal const string roiremoverequester_id = "mrocontent_cmdRemoveRequester";

            // Cancel Request
            public const string cancelrequest_id = "mrocontent_cmdRqrCancel";
            public const string canceldate_id = "mrocontent_lblRqrCancel";
            public const string requeststatus_id = "mrocontent_tdRequestStatus";
            public const string updateinfo_id = "mrocontent_cmdUpdateInfo";
            public const string cancelbyrequester_id = "mrocontent_txtRqrCancel";
            public const string updatebtn_id = "mrocontent_cmdUpdate";
            public const string l1retrievalfees_id = "mrocontent_tdRetrievalFee";
            public const string l1adjustedbalance_id = "mrocontent_tdAdjustedBalance";

            //Add issue at Admin side                
            public const string adminaddissue_id = "mrocontent_cmdAddIssue";
            internal const string adminissue_id = "mrocontent_cmbBxIssues_Input";
            internal const string admincomments_id = "mrocontent_txtAddComments";
            internal const string adminaddissubtn_id = "mrocontent_cmdAdd";
            internal const string adminviewaction_id = "mrocontent_btnActions";
            internal const string adminmroaction_id = "mrocontent_ddlMROActions";
            internal const string adminmronotes_id = "mrocontent_txtNotes";
            public const string adminaddactionbtn_id = "mrocontent_cmdAddAction";
            public const string adminrequestactionclose_xpath = "//*[contains(@id,'mrocontent_dgActions_CloseStatus')]";
            public const string admindocrequest_id = "actLink";
            public const string worksummarydocreqid_id = "documents-required";
            public const string worksummarynoscanid_id = "no-request-docs-scanned";
            public const string admindocrequest_xpath = "(//td[@class=\" kpi - text kpi-success\"])";
            //   "(//td[@class=\"MastheadText\"])";
            public const string admindocreqtotal_xpath = "//*[@id='mrocontent_tblRequest']/tbody/tr/td[2]";

            public const string adminelectotal_xpath = "//*[@id='mrocontent_ctl00_tblResults']/tbody/tr/td[2]";

            public const string pendingRequst_xpath = "//*[@id='mrocontent_MROReportGridBanner_lblRows']";



            //"//*[@id='mrocontent_dgActions']/tbody//tr[3]/td[7][text()='Close']";

            //"//*[@title='Close this action and go to Request Status Screen']"; 


            //"//*[@title='Close this action and go to Request Status Screen']";


            //Add issue at Admin side
            public const string adminQATestSP = "mrocontent_txtQASP";
            public const string adminQATestP1 = "mrocontent_txtP1";
            public const string adminQATestP2 = "mrocontent_txtP2";
            public const string adminQATestP3 = "mrocontent_txtP3";
            public const string adminQATestExecute = "mrocontent_cmdExecute";
            public const string adminQATestResults = "mrocontent_txtResults";
            public const string adminQAUpdateVersion = "mrocontent_cmdVersion";
            public const string adminQAVersion = "mrocontent_txtVersion";
        }

        public static class FacilityWorkSummaryPage
        {

            // internal const string facilityLogOutButton_Xpath = "//img[@title='Log Out']";
            //   internal const string facillogoutbtn_id = "mroheader_ctl02_ctl03_imgLogout";
            public const string facillogoutbtn_id = "mroheader_MROPageHead1_ctl03_imgLogout";
            internal const string facworksummactionlist_id = "rdDashboardCaptionID";
            // internal const string facworksummactionlist_id = "rdDashboardCaptionID";

            internal const string facililtyworksummary_xpath = "//*[@class=\"MastheadText\"]";
            internal const string facActionListPage_xpath = "//*[@class=\"MastheadText\"]";


        }
        //  rdDashboardCaptionID

        internal static class Admin
        {

            // internal const string facilityLogOutButton_Xpath = "//img[@title='Log Out']";
            internal const string facillogoutbtn_id = "mroheader_ctl02_ctl03_imgLogout";
            internal const string facworksummactionlist_id = "rdDashboardCaptionID";
            // internal const string facworksummactionlist_id = "rdDashboardCaptionID";

            internal const string facililtyworksummary_xpath = "//*[@class=\"MastheadText\"]";


        }
    }
}