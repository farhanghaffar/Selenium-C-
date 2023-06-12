namespace MRO.ROI.Automation.Selenium
{
    public static partial class PageElements
    {
        internal static class FacilityLoginPage
        {
            internal const string facilityLink_Xpath = "//td[@id='mroheader_Mropagehead1_Mromenus1_Mrotopmenu1_mnuMainMenu-menuItem008']";
            internal const string facilityCode_Id = "mrocontent_txtClientCode";
            internal const string facilityOkButton = "mrocontent_cmdSelect";
            internal const string facilityLogOutButton_Xpath = "//img[@title='Log Out']";
        }

        internal static class ROIAdminLoginPage
        {
            internal const string roiAdmin_xpath = "//td[@id='mroheader_Mropagehead1_Mromenus1_Mrotopmenu1_mnuMainMenu-menuItem006']";
            internal const string roiAdminUsername_xpath = "//input[@id='mrocontent_txtUserName']";
            internal const string roiAdminPassword_xpath = "//input[@id='mrocontent_txtPassword']";
            internal const string okButton_xpath = "//input[@id='mrocontent_cmdLogin']";
        }
        public static class LogNewRequestPage
        {
            internal const string mroDelivery_xpath = "//*[text()='MRO Delivery']";
            internal const string mroDelSelectionTest_xpath = "//a[@class='rtsLink rtsSelected']//span[text()='MRO Delivery']";
            internal const string onsitedelivery_xpath = "//*[text()=\"On-Site Delivery\"]";
            internal const string onsitedeliverySelectionTest_xpath = "//*[@class=\"rtsLink rtsSelected\"]//span[text()='On-Site Delivery']";
            internal const string internalportalbtn_xpath = "//*[text()=\"Internal Portal\"]";
            internal const string internalportalSelectionTest_xpath = "//*[@class=\"rtsLink rtsSelected\"]//span[text()='Internal Portal']";
            internal const string billingofficebtn_xpath = "//*[text()=\"Billing Office\"]";
            internal const string docrequiredall_xpath = "//*[text()=\"All\"]";
            public const string firstName_Id = "mrocontent_txtFirstName";
            public const string lastName_Id = "mrocontent_txtLastName";
            internal const string dateOfBith_Id = "mrocontent_txtDOB";

            internal const string dlvymethod_xpath = "//*[@id=\"mrocontent_lstRequesterType\"]//option[2]";
            internal const string dlvymethod_id = "mrocontent_lblRequestType";
            internal const string locationDropDown_id = "mrocontent_lstLocation_Input";
            internal const string requesterdate_id = "mrocontent_txtRequestRcvdDate";
            internal const string requestStatusButton_Id = "mrocontent_cmdViewRequest";
            internal const string requestpreauth_id = "mrocontent_cmdPreAuth";
            internal const string changedlvymethod_id = "mrocontent_lstChanges";
            internal const string scanButton_Id = "mrocontent_cmdScan_";
            internal const string sendmsgmrobtn_id = "mrocontent_lstActions";
            public const string sendmsgmrotxt_id = "mrocontent_txtNewMessage";
            //sendmessagemrotext_id=mrocontent_txtNewMessage
            //sendmessagemrobutton_id=mrocontent_lstActions
            internal const string acceptyes_id = "rbYes";
            internal const string btnrelease_id = "btnRelease";
            public const string mrocontenttdRequestID_id = "mrocontent_tdRequestID";
            internal const string requesterfax_id = "mrocontent_txtOSDRequesterFax";
            internal const string storedonpaper_id = "mrocontent_txtPagesM";
            internal const string storedelectronically_id = "mrocontent_txtPagesE";
            internal const string createinvoiceButton_id = "mrocontent_lnkCalculate";
            internal const string cashCheck_id = "mrocontent_rbCash";
            internal const string amount_id = "mrocontent_custCashPaymentHistory_RadGridPaymentHistory_ctl00_ctl02_ctl01_TB_cyAmount";
            internal const string amountuncheck_xpath = "//*[@id=\"mrocontent_custCashPaymentHistory_RadGridPaymentHistory_ctl00_ctl02_ctl01_PerformInsertButton\"]";
            internal const string sendfax_xpath = "//*[@id=\"mrocontent_cmdSendFax\"]";
            internal const string dlvfaxonsitereqtype_id = "mrocontent$lstRequesterType";
            //*[@id="mrocontent_cmdDeliver"]
            internal const string osddlvrecords_xpath = "//*[@id=\"mrocontent_cmdDeliver\"]";
            internal const string balancedue_xpath = "//*[@id=\"mrocontent_tdBalanceDue\"]";
            internal const string dobvalidation_id = "mrocontent_tdDOB";
            internal const string cashcheck_xpath = "//*[@id=\"mrocontent_rbCash\"]";
            internal const string facillogoutbtn_id = "mroheader_ctl02_ctl03_imgLogout";
            public const string ratesfacilogoutbtn_id = "mroheader_MROPageHead1_ctl03_imgLogout";
            internal const string requestRecievedDate_Id = "mrocontent_txtRequestRcvdDate";
            internal const string requesterType_Id = "mrocontent_lstOSDRequesterType";
            internal const string logRequestBtn_Id = "mrocontent_cmdLogRequest";
            internal const string ignoreDuplicatesChk_Id = "mrocontent_cbDuplicates";
            internal const string intlocation_id = "mrocontent_lstLocations";
            internal const string inttextnotes_id = "mrocontent_txtNotes";
            internal const string intcreaterequest_id = "mrocontent_cmdCreateRequest";
        }

        internal static class FacilityRequestStatusPage
        {
            internal const string scanButton_Id = "mrocontent_cmdScan_";
            internal const string acceptYes_Id = "rbYes";
            internal const string btnRelease_Id = "btnRelease";
            internal const string mroContenttdRequestID_Id = "mrocontent_tdRequestID";
            internal const string deliverfaxosdnow_id = "mrocontent_cmdDeliverOSD";
            internal const string requesterFax_Id = "mrocontent_txtOSDRequesterFax";
            internal const string storedOnPaper_Id = "mrocontent_txtPagesM";
            internal const string storedElectronically_Id = "mrocontent_txtPagesE";
            internal const string createInvoiceButton_Id = "mrocontent_lnkCalculate";
            internal const string cashCheck_Id = "mrocontent_rbCash";
            internal const string amount_Id = "mrocontent_custCashPaymentHistory_RadGridPaymentHistory_ctl00_ctl02_ctl01_TB_cyAmount";
            internal const string amountUncheck_Xpath = "//*[@id=\"mrocontent_custCashPaymentHistory_RadGridPaymentHistory_ctl00_ctl02_ctl01_PerformInsertButton\"]";
            internal const string sendFax_Xpath = "//*[@id=\"mrocontent_cmdSendFax\"]";
            internal const string balanceDue_Xpath = "//*[@id=\"mrocontent_tdBalanceDue\"]";
            internal const string dobValidation_Id = "mrocontent_tdDOB";
            internal const string cashCheck_Xpath = "//*[@id=\"mrocontent_rbCash\"]";
            internal const string internalPortalBtn_Xpath = "//*[text()=\"Internal Portal\"]";
            internal const string organizationtext_id = "mrocontent_txtRequesterName";
            //Pre Authorization required for RequestPreAuthorizationTest xpaths.
            internal const string requeststatusid_xpath = "(//td[@class=\"MastheadText\"])"; //Getting a request status id from request status page.
            internal const string estpapstored_id = "mrocontent_txtPageCount";
            internal const string estelcstored_id = "mrocontent_txtPageCountElec";
            internal const string reqpreauthorizationbtn_id = "mrocontent_cmdOk";
            internal const string actionmessage_id = "mrocontent_pnlMessages";
        }
    }
}