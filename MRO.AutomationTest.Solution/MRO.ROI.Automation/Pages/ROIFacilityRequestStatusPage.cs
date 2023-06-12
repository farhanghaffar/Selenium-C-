using AventStack.ExtentReports;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MRO.ROI.Automation.Common.Navigation;
using MRO.ROI.Automation.Pages;
using MRO.ROI.Automation.Pages.Common;
using MRO.ROI.Automation.Selenium;
using MRO.ROI.Automation.Utility;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using System;
using System.IO;
using System.Reflection;
using EAGetMail;
using System.Linq;
using static MRO.ROI.Automation.Utility.IniFile;
using OpenQA.Selenium.Support.UI;
using System.Collections.Generic;
using MRO.ROI.Automation.Common;

namespace MRO.ROI.Automation.Pages
{
    public class ROIFacilityRequestStatusPage
    {
        public RemoteWebDriver Driver;
        public ExtentTest logger;
        public TestContext Context;
        public CSVReader csvReader;

        public ROIFacilityRequestStatusPage(RemoteWebDriver driver, ExtentTest _loger, TestContext _context)
        {
            Driver = driver;
            logger = _loger;
            Context = _context;
        }
        public Random random = new Random();
        public By logoutElement = By.XPath("//img[@title='Log Out']");
        public By headerElement = By.XPath("//td[@id='MasterHeaderText']");
        public By reopenCheckBox = By.XPath("//input[@id='mrocontent_cbChangeClosed']");
        public const string importPDFFrame = "radWndPrompt";
        public const string importPDFFilename = "8.pdf";
        public const string importPDFFilename1 = "7.pdf";
        public By selectFileButton = By.Id("mrocontent_rauFileUploadfile0");
        public By importPDFButton = By.Id("mrocontent_btnImportDoc");
        public By importCloseButton = By.Id("mrocontent_btnCloseImport");
        public By removeButton = By.XPath("//*[@id='mrocontent_rauFileUploadrow0']/span[3]");
        public By addNoRecordslink = By.XPath("//a[@id='mrocontent_lnkAddNRS']");
        public By importBuutonforAddNoRecords = By.XPath("(//input[contains(@id,'mrocontent_btnImportPDFDocs')])[3]");
        public By deleteCheckbox = By.XPath("//input[starts-with(@id,'mrocontent_Scanned_')]");
        public By yesButton = By.XPath("//button[contains(text(),'Yes')]");
        public By requestPageImportButton = By.XPath("(//input[contains(@id,'mrocontent_btnImportPDFDocs')])[1]");
        public By yesRadioButton = By.Id("rbYes");
        public By releaseRequestButton = By.Id("btnRelease");
        public By uploadedTxt = By.XPath("//tr[contains(@id,'mrocontent_custPDFImportCtl_RadGridImport_ct')][last()]//td[contains(text(),'Uploaded')]");
        public By importRequestPDFButton = By.XPath("//span[contains(text(),'Request Pages')]/../..//input[@value='Import']");
        public By importPatientPDFButton = By.XPath("//span[contains(text(),'Patient Pages')]/../..//input[@value='Import']");
        public By releseRequestButton = By.XPath("//a[contains(text(),'Release Request')]");
        public string importRequestPDFFile = "Mile.pdf";
        public string importPatientPDFFile = "22.pdf";
        string importRequestPDFFileMulti = "5.pdf";
        string importPatientPDFFileMulti = "6.pdf";
        public By getRequestID = By.XPath("//div[contains(text(), 'Request Status')]");
        string importMROPDFFile = "4.pdf";
        string importMROPDFFile1 = "21.pdf";
        public By addComponentLnk = By.Id("mrocontent_lnkAddComponent");
        public By importMROTestPDFButton = By.XPath("(//input[starts-with(@id,'mrocontent_btnImportPDFDocs')])[3]");
        public By importMROTestPDFButtonTest1 = By.XPath("//a[contains(text(),'Test1')]/../..//input[@value='Import']");
        public By importMROTestPDFButtonTest2 = By.XPath("//a[contains(text(),'Test2')]/../..//input[@value='Import']");
        public By viewDocumentButton = By.XPath("//button[@id='mrocontent_lnkWVL']");
        public By verifyDocuments = By.XPath("//a[@class='btn btn-default btn-sm btn-spacer active viewer-btn ng-binding ng-scope btn-primary']");
        public By linkToAnotherRequestBtn = By.XPath("//input[@id='mrocontent_cmdLinkRequest']");
        public By verifyLinkToAnotherRequestPage = By.XPath("//td[@id='MasterHeaderText']");
        public By verifyLinkedToRequest = By.XPath("//span[@id='mrocontent_lblLinkRequest']");
        public By PatientPagesProgress = By.XPath("//tr[@class='PatientPages drop-area dz-clickable']//td[@align='right']");
        public By selectComponentLocation = By.XPath("//select[@id='mrocontent_lstLocations']");
        public By getTotalPatientPages = By.XPath("//td[text()='Total Patient Records']/following-sibling::td[2]");
        public By verifyPatientDocuments = By.XPath("//div[@class='container-fluid']//a[contains(text(),'Patient Doc')]//span");
        public By auditTrailButton = By.XPath("//input[@id='mrocontent_cmdAuditTrail']");
        public By verifyImportedDocumentStatus = By.XPath("(//tr[starts-with(@id,'mrocontent_custPDFImportCtl_RadGridImport')]//td[6])[1]");
        public string importPatientDuplicatePDFMulti = "9.pdf";
        public By getPatientPages = By.XPath("//tr[@class='PatientPages drop-area dz-clickable']//td[3]");
        public By requestStatusEle = By.Id("mrocontent_lblRequestStatus");
        public string csvFileName = "InternalPortalWithComplianceHold.csv";
        public By setIdLink = By.Id("mrocontent_lnkEPICEdit");
        public By epicRoiTextBox = By.Id("mrocontent_txtEPICEdit");
        public By checkMarkEle = By.Id("mrocontent_imgBtnChangeEPICEdit");
        public By extReleaseIdEle = By.Id("mrocontent_tdExternalReleaseID");
        public By epicRoiIdEle = By.Id("mrocontent_lblEPICID");
        public const string roiTextValue = "test";
        public By complianceHoldChkBox = By.Id("mrocontent_cbComplianceHold");
        public By drillIntoFacilitylink = By.Id("mrocontent_lnkFacility");
        public By updateInfoButton = By.Id("mrocontent_cmdUpdateInfo");
        public By locationDropdownEle = By.Id("mrocontent_lstLocation_Arrow");
        public By kingOfPrussiaLocSelection = By.XPath("//li[contains(text(),'King of Prussia')]");
        public const string locationVal = "King of Prussia";
        public By locationDropdown = By.XPath("//div[@id='mrocontent_lstLocation_DropDown']//ul/li");
        public By updateBtn = By.XPath("//input[@id='mrocontent_cmdUpdate']");
        public By unReleaseEle = By.XPath("//a[@id='mrocontent_UnRelease_']");
        public By requestPageTxt = By.XPath("//tr[@class='RequestPages']//td[1]/../td[3]");
        public By requestPageTxtOnViewDoc = By.XPath("//a[@class='btn btn-default btn-sm btn-spacer active viewer-btn ng-binding ng-scope btn-primary']//span");
        public By totalRequestAndPatientTxtOnViewDoc = By.XPath("//a[@class='btn btn-default btn-sm btn-spacer active viewer-btn ng-binding ng-scope'][2]//span");
        public By requestPagesCount = By.XPath("//tr[@class='RequestPages drop-area dz-clickable']//td[1]/../td[3]");
        public By payByCC = By.XPath("//input[@id='mrocontent_cmdPayCreditCard']");
        Random ran = new Random();
        string importPatientYalePdf = "Yale.pdf";
        public By setEpicRoiIdLnk = By.XPath("//a[@id='mrocontent_lnkEPICEdit']");
        public By setEpicRoiIdValue = By.XPath("//input[@id='mrocontent_txtEPICEdit']");
        public By saveDeliveryDateBtn = By.XPath("//input[@id='mrocontent_imgBtnChangeEPICEdit']");
        public By deliveryMethod = By.XPath("//span[@id='mrocontent_lblRequestType']");
        public By UnRelease = By.XPath("//a[@id='mrocontent_UnRelease_']");
        public string importCarilion = "Carilion.PDF";
        public By locationElement = By.Id("mrocontent_lblLocation");
        public By patientNameElement = By.Id("mrocontent_tdPatientName");
        public By createdUsername = By.XPath("//span[@id='mroheader_ctl02_ctl00_lblUserLogin' and @class='BodyType']");
        public By CreatePDF = By.XPath("//input[@id='mrocontent_btnCreateNewPDF']");
        public By ShipmentStatus = By.XPath("//table[@id='mrocontent_tblShipments']//tr[2]//td[3]");
        public By ShipmentDate = By.XPath("//table[@id='mrocontent_tblShipments']//span");
        public By Download = By.XPath("//input[@value='Download']");
        public string importRequestPDFFile1 = "TestSample1.pdf";
        public string importPatientPDFFile1 = "TestSample2.pdf";
        public string importAddCompPDFFile = "TestSample3.pdf";
        public string importAddCeritificationCompPDFFile = "TestSample4.pdf";
        public string importAddNoRecordCompPDFFile = "TestSample5.pdf";
        public string imporAddCorrespondenceCompPDFFile = "TestSample6.pdf";
        public By txtDescription = By.XPath("//input[@id='mrocontent_cmbBxComponents_Input']");
        public By addButton = By.XPath("//input[@id='mrocontent_cmdAddComponent']");
        public By addComponentButton = By.XPath("//input[@id='mrocontent_cmdAddEdit']");
        public By addCertificationComp = By.XPath("//a[@id='mrocontent_lnkAddCertification']");
        public By importAddCertificationComp = By.XPath("(//input[contains(@id,'mrocontent_btnImportPDFDocs')])[3]");
        public By importAddNoRecordsComp = By.XPath("(//input[contains(@id,'mrocontent_btnImportPDFDocs')])[4]");
        public By addCorrespondenceComp = By.XPath("//a[@id='mrocontent_lnkAddCorrepsondenceComponent']");
        public By importAddCorrespondenceComp = By.XPath("(//input[contains(@id,'mrocontent_btnImportPDFDocs')])[6]");
        public string importYaleTest = "Yale Test PDF.pdf";
        public string importUnityPoint2 = "UnityPoint2.pdf";
        public string importWellspan = "Wellspan.pdf";
        public string importUnityPoint3 = "UnityPoint3.pdf";
        public string importPatientPDF = "samplepdfdoc-converted.pdf";
        public string importStatOptionPDF = "StatOption.pdf";
        public By changeAmountLnk = By.XPath("//a[@id='mrocontent_lnkUpdatePrepayment']");
        public By creditCardRadio = By.XPath("//input[@id='mrocontent_rbCreditCard']");
        public By paymentAmountTxt = By.XPath("//input[@id='mrocontent_custCCPayment_txtBxPayment']");
        public By continueBtn = By.XPath("//input[@id='mrocontent_custCCPayment_btnEnterCCInfo']");

        public By requestImagingDeliveryBtn = By.XPath("//input[@id='mrocontent_cmdRequestSharedDelivery']");
        public By onHoldChk = By.XPath("//input[@id='mrocontent_cbOnHold']");
        public By onHoldReasonDrp = By.XPath("//select[@id='ddlReasons']");
        public By saveBtn = By.XPath("//input[@id='btnSave']");
        public By requestStatus = By.XPath("//span[@id='mrocontent_lblOnHoldReason']");


        public By shipToTxt = By.XPath("//td[@id='mrocontent_tdShipRequester']//b");
        public By emailId = By.XPath("//td[@id='mrocontent_tdShipRequester']//a");
        public string twoPagePdf = "2.pdf";
        public string test2pdf = "Test2.pdf";
        public string test1Pdf = "Test1.pdf";
        public string Test800Pages = "800.pdf";
        public By testonepdfCount = By.XPath("(//td[text()='Patient Pages']/following-sibling::td[1])[1]");

        public By manageIssuesHyperLink = By.XPath("//a[@id='mrocontent_lnkIssues']");
        public By addIssueBtn = By.XPath("//input[@id='mrocontent_cmdAddIssue']");
        public By listOfIssues = By.XPath("//a[@id='mrocontent_lnkOpenIssues']");
        public By actionMessageDrp = By.XPath("//select[@id='mrocontent_lstActions']");
        public By sendMessageToMROBtn = By.XPath("//input[@name='mrocontent$cmdSendMessage']");
        public By messageText = By.XPath("//div[@id='mrocontent_pnlMessages']/table[@id='mrocontent_tblMsgs']//tr[2]//td[2]");
        public By actionMsgFrame = By.XPath("//iframe[@id='mrocontent_frameMessages']");
        public By UserMenuOption = By.XPath("//td[starts-with(@id,'mroheader_') and text()='Users']");
        public By ListAllUsersOption = By.XPath("//td[contains(text(),'List All Users')]");

        public By addImagingDeliveyBtn = By.XPath("//input[@id='mrocontent_cmdAddSharedDelivery']");
        public By shipmentDate = By.XPath("//input[@id='mrocontent_txtDeliveryDate']");
        public By itemCount = By.XPath("//input[@id='mrocontent_txtPageCount']");
        public By carrier = By.XPath("//select[@id='mrocontent_lstCarrier']");
        public By specialInstructions = By.XPath("//input[@id='mrocontent_txtSpecialDelivery']");
        public By saveDeliveryInfoBtn = By.XPath("//input[@id='mrocontent_cmdOk']");
        public string import400pagespdf = "test400.pdf";
        public string import800pagespdf = "test800.pdf";
        public string importtestpdf = "test.pdf";
        public By importCancelButton = By.Id("mrocontent$btnCancel");//
        public By fileCancelButton = By.XPath("//span[@class='ruButton ruCancel']");
        public By fileRemoveButton = By.XPath("//span[@class='ruButton ruRemove']");
        public By cancelImportDocuments = By.Id("mrocontent_btnCancel");

        public By referenceId = By.XPath("//input[@id='mrocontent_txtRefID']");
        public By claimId = By.XPath("//input[@id='mrocontent_txtClaimID']");
        public By cancelBtn = By.XPath("//input[@id='mrocontent_cmdCancel']");
        public By updatePtInfoBtn = By.XPath("//input[@id='mrocontent_cmdUpdatePtInfo']");
        //
        public string originalPdf = "OriginalTest1.pdf";
        public string duplicatePdf = "DuplicateTest2.pdf";

        public By test2pdfCount = By.XPath("(//td[text()='Patient Pages']/following-sibling::td[1])[2]");
        public By test3pdfCount = By.XPath("(//td[text()='Patient Pages']/following-sibling::td[1])[3]");
        public By verifyRequestDocuments = By.XPath("//div[@class='container-fluid']//a[contains(text(),'Request Docs')]//span");
        public By entireChart = By.XPath("//div[@class='container-fluid']//a[contains(text(),'[Entire Chart]')]//span");
        public string panPDF = "PAN.pdf";
        public By dobElement = By.Id("mrocontent_tdDOB");
        public string twentyPagespdf = "20pages.pdf";
        public By LinkedReqId = By.XPath("//span[@id='mrocontent_lblLinkRequest']");

        public By requestDocuments = By.XPath("//div[@class='container-fluid']//span[contains(text(),'Request Docs')]");
        public By patientDocuments = By.XPath("//div[@class='container-fluid']//a[contains(text(),'Patient Docs')]");
        public By entireChartDocu = By.XPath("//div[@class='container-fluid']//a[contains(text(),'[Entire Chart]')]");
        public By requestDoCDrp = By.XPath("//select[@class='ng-pristine ng-valid ng-not-empty ng-touched']");
        public By report = By.XPath("//td[@id='mroheader_ctl02_ctl01_ctl00_mnuMainMenu-menuItem006']");
        public By deliveryReport = By.XPath("//td[@id='mroheader_ctl02_ctl01_ctl00_mnuMainMenu-menuItem006-subMenu-menuItem010']");
        public By closeRequestBtn = By.XPath("//input[@id='mrocontent_cmdCloseRequest']");


        string test1200pdf = "Test1200Pdf.pdf";
        string photoPdf = "PhotoShop.pdf";
        string larsentif = "lanscater.tif";

        public By replyBtn = By.XPath("//input[@title='Send a Reply to this Message']");
        public By replyToMsgTxtBox = By.XPath("//textarea[@id='mrocontent_txtReply']");
        public By sendRplyBtn = By.XPath("//input[@id='mrocontent_cmdSendReply']");

        public By reqPreauthorization = By.XPath("//input[@ id='mrocontent_cmdPreAuth']");
        public string TwoHundredPDF = "200.pdf";
        public By ssnValue = By.Id("mrocontent_lblSSN");
        public By viewPatientSSN = By.Id("mrocontent_imgBtnViewPatientSSN");
        public By waitingPrepay = By.XPath("//input[@ id='mrocontent_cbWaitingPrepay']");
        public By assignRequester = By.XPath("//input[@ id='mrocontent_btnAssignRequester']");
        public By testAttorney = By.XPath("//a[@ id='mrocontent_lnkRequester']");
        public By licenseRequireLable = By.XPath("//*[@id='mrocontent_CallbackPanel_Selection_trial']");
        public By scanPatientBtn = By.XPath("(//span[contains(text(),'Patient Pages')]/../..//input[@value='Scan'])[2]");
        public By patientScanIframe = By.XPath("//iframe[@name='radWndWebTwainScan']");
        public By scanBtnInPatientIframe = By.XPath("//button[@id='cmdScan']");
        public By acceptBtnInPatientIframe = By.XPath("//button[@id='cmdAccept']");
        public By scannedDocumentStatus = By.XPath("((//tr[starts-with(@id,'mrocontent_custPDFImportCtl_RadGridImport')])[last()]//td[text()='Uploaded'])[1]");


        public void GotToCCShoppingCartPage()
        {
            try
            {
                Driver.DirectClick(payByCC);
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to navigate to cc shopping cart page Message : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }

        /// <summary>
        /// Click on add a component
        /// </summary>
        public ROIFacilityAddComponentPage ClickAddComponent()
        {
            try
            {
                Driver.ScrollToEndOfThePage();
                Driver.DirectClick(addComponentLnk);
            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to open add component page  : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }

            return new ROIFacilityAddComponentPage(Driver, logger, Context);
        }


        /// <summary>
        /// Import PDF MRO Automation Test Files
        /// </summary>
        public ROIAdminFacilityListPage ImportMROTestPDFFiles()
        {
            try
            {
                Driver.ScrollToEndOfThePage();
                Driver.DirectClick(importMROTestPDFButtonTest1);
                Driver.SwitchTo().Frame(importPDFFrame);
                //Driver.FindElementBy(selectFileButton).SendKeys(Path.GetFullPath(Path.Combine(Assembly.GetExecutingAssembly().Location, "..", "..", "..", "..", "MRO.ROI.Automation", "Files", importMROPDFFile)));

                string fulldirectory = SeleniumHelper.ROITestFileUploads.LocationPath;
                string pdfFileName = $"\\{importMROPDFFile}";
                string requestDocsPath = fulldirectory + pdfFileName;
                Driver.FindElementBy(selectFileButton).SendKeys(requestDocsPath);

                Driver.ClickAndCheckNextElement(importPDFButton, importCloseButton);
                Driver.DirectClick(importCloseButton);
                WaitUntillFileStatusChange();
                logger.Log(Status.Pass, $"PDF files imported");

            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to import mro automation test pdf files : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
            return new ROIAdminFacilityListPage(Driver, logger, Context);
        }

        public void ImportPatientPages()
        {
            try
            {
                Driver.ScrollIntoViewAndClick(By.XPath("//span[contains(text(),'Patient Pages')]/../..//input[@value='Import']"));
                Driver.SwitchTo().Frame(importPDFFrame);
                //Driver.FindElementBy(selectFileButton).SendKeys(Path.GetFullPath(Path.Combine(Assembly.GetExecutingAssembly().Location, "..", "..", "..", "..", "MRO.ROI.Automation", "Files", importPatientPDFFile)));

                string fulldirectory = SeleniumHelper.ROITestFileUploads.LocationPath;
                string pdfFileName = $"\\{importPatientPDFFile}";
                string requestDocsPath = fulldirectory + pdfFileName;
                Driver.FindElementBy(selectFileButton).SendKeys(requestDocsPath);

                Driver.ClickAndCheckNextElement(importPDFButton, importCloseButton);
                Driver.DirectClick(importCloseButton);
                WaitUntillFileStatusChange();
                logger.Log(Status.Pass, $"Patient PDF file imported");

            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to import Patient PDF file : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }

        public void ImportRequestPages()
        {
            try
            {
                Driver.ScrollToEndOfThePage();
                Driver.DirectClick(importRequestPDFButton);
                Driver.SwitchTo().Frame(importPDFFrame);
                // Driver.FindElementBy(selectFileButton).SendKeys(Path.GetFullPath(Path.Combine(Assembly.GetExecutingAssembly().Location, "..", "..", "..", "..", "MRO.ROI.Automation", "Files", importRequestPDFFile)));

                string fulldirectory = SeleniumHelper.ROITestFileUploads.LocationPath;
                string pdfFileName = $"\\{importRequestPDFFile}";
                string requestDocsPath = fulldirectory + pdfFileName;
                Driver.FindElementBy(selectFileButton).SendKeys(requestDocsPath);

                Driver.ClickAndCheckNextElement(importPDFButton, importCloseButton);
                Driver.DirectClick(importCloseButton);
                logger.Log(Status.Pass, $"Request PDF file imported");

            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to import request PDF file : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }

        /// <summary>
        /// Import PDF MRO Automation Test Files
        /// </summary>
        public ROIAdminFacilityListPage ImportMROTestPDFFiles1()
        {
            try
            {
                Driver.ScrollToEndOfThePage();
                Driver.DirectClick(importMROTestPDFButtonTest2);
                Driver.SwitchTo().Frame(importPDFFrame);
                //Driver.FindElementBy(selectFileButton).SendKeys(Path.GetFullPath(Path.Combine(Assembly.GetExecutingAssembly().Location, "..", "..", "..", "..", "MRO.ROI.Automation", "Files", importMROPDFFile1)));

                string fulldirectory = SeleniumHelper.ROITestFileUploads.LocationPath;
                string pdfFileName = $"\\{importMROPDFFile1}";
                string requestDocsPath = fulldirectory + pdfFileName;
                Driver.FindElementBy(selectFileButton).SendKeys(requestDocsPath);

                Driver.ClickAndCheckNextElement(importPDFButton, importCloseButton);
                Driver.DirectClick(importCloseButton);
                WaitUntillFileStatusChange();
                logger.Log(Status.Pass, $"PDF files Imported");
            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to import mro automation test pdf files  : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }

            return new ROIAdminFacilityListPage(Driver, logger, Context);

        }



        /// <summary>
        /// Reopen Request
        /// </summary>
        public ROIFacilityRequestStatusPage ReOpenRequest()
        {
            //try
            //{
            //    Driver.Click(reopenCheckBox);
            //}
            //catch (Exception ex)
            //{

            //    throw new Exception($"Failed  to reopen Request with details as Message : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            //}
            return new ROIFacilityRequestStatusPage(Driver, logger, Context);

        }


        /// <summary>
        /// To Import Pdf files for Request Status and Add no components
        /// </summary>
        public ROIFacilityRequestStatusPage ImportPdfFilesForAddNoRecordsComponent()
        {
            try
            {
                Driver.ScrollToEndOfThePage();
                Driver.DirectClick(requestPageImportButton);
                Driver.SwitchTo().Frame(importPDFFrame);
                //Driver.FindElementBy(selectFileButton).SendKeys(Path.GetFullPath(Path.Combine(Assembly.GetExecutingAssembly().Location, "..", "..", "..", "..", "MRO.ROI.Automation", "Files", importPDFFilename)));
                string fulldirectory = SeleniumHelper.ROITestFileUploads.LocationPath;
                string pdfFileName = $"\\{importPDFFilename}";
                string requestDocsPath = fulldirectory + pdfFileName;
                Driver.FindElementBy(selectFileButton).SendKeys(requestDocsPath);


                Driver.ClickAndCheckNextElement(importPDFButton, importCloseButton);
                Driver.JavaScriptClick(importCloseButton);
                logger.Log(Status.Info, $"Successfully imported pdf file ({importPDFFilename})");
                Driver.Click(addNoRecordslink);
                //Driver.ScrollToEndOfThePage();
                Driver.DirectClick(importBuutonforAddNoRecords);
                Driver.SwitchTo().Frame(importPDFFrame);
                //Driver.FindElementBy(selectFileButton).SendKeys(Path.GetFullPath(Path.Combine(Assembly.GetExecutingAssembly().Location, "..", "..", "..", "..", "MRO.ROI.Automation", "Files", importPDFFilename1)));              
                string pdfFileName1 = $"\\{importPDFFilename1}";
                string requestDocsPath1 = fulldirectory + pdfFileName1;
                Driver.FindElementBy(selectFileButton).SendKeys(requestDocsPath1);


                Driver.ClickAndCheckNextElement(importPDFButton, importCloseButton);
                Driver.JavaScriptClick(importCloseButton);
                logger.Log(Status.Info, $"Successfully imported pdf file ({importPDFFilename1})");
                Driver.ScrollToEndOfThePage();
                Driver.DirectClick(deleteCheckbox);
                Driver.WaitUntilDOMLoaded();
                Driver.DirectClick(yesButton);
                Driver.WaitUntilDOMLoaded();
            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to import pdf files for Request status and add no records component with details as Message : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }

            return new ROIFacilityRequestStatusPage(Driver, logger, Context);

        }

        /// <summary>
        /// Release Request for  Add no components
        /// </summary>
        public ROIFacilityRequestStatusPage ReleaseRequestForAddnoRecordsComponent()
        {

            try
            {
                Driver.SwitchTo().Frame(importPDFFrame);
                IWebElement yesRadioButt = Driver.FindElementBy(yesRadioButton);
                yesRadioButt.Click();
                IWebElement releaseRequest = Driver.FindElementBy(releaseRequestButton);
                releaseRequest.Click();
                WebElementHelper webElementHelper = new WebElementHelper(Driver, logger, Context);
                webElementHelper.Click_Enter();
            }
            catch (Exception ex)
            {

                throw new Exception($"Failed  to  release request with details as Message : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
            return new ROIFacilityRequestStatusPage(Driver, logger, Context);


        }

        /// <summary>
        /// To Get  created RequestId form Facility side
        /// </summary>
        public ROIAdminHomePage GetRequestIdFromFacilitySide(string BaseAddress)
        {
            try
            {
                string requestId = Driver.FindElementBy(headerElement, 120).Text;
                string id = requestId.Substring(17, 8);
                Driver.WaitUntilDOMLoaded();
                LoginPage loginPage = new LoginPage(Driver, logger, Context);
                loginPage.GoToROIAdminLoginPage(BaseAddress);
                ROIAdminHomePage rOIAdminHomePage = new ROIAdminHomePage(Driver, logger, Context);
                rOIAdminHomePage.ROIAdminLoginForSpecificUser();
                rOIAdminHomePage.ROIlookupByRequestId(id);
                Driver.WaitUntilDOMLoaded();
            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to get request id with details as Message : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }

            return new ROIAdminHomePage(Driver, logger, Context);

        }



        /// <summary>
        /// Re open the request
        /// </summary>

        public ROIAdminFacilityListPage ReOpenRequestID()
        {
            try
            {
                Driver.FindElementBy(reopenCheckBox).Click();
            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to reopen the request id : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
            return new ROIAdminFacilityListPage(Driver, logger, Context);
        }
        /// <summary>
        /// Import pdf files
        /// </summary>
        public ROIAdminFacilityListPage ImportPdfFiles()
        {
            try
            {
                Driver.ScrollToEndOfThePage();
            //    Driver.ScrollIntoViewSmoothly(importRequestPDFButton);
                Console.WriteLine("Scroll elemnt");
              //  Driver.DirectClick(importRequestPDFButton);
                Console.WriteLine("Click on pdf report button");
                // By roiFrame = By.XPath("//Iframe[@name='radWndPrompt']");

       // IWebElement element = Driver.FindElementBy(roiFrame);
                Driver.SwitchTo().Frame("radWndPrompt");
               // Driver.SwitchTo().Frame(importPDFFrame);
                //Driver.FindElementBy(selectFileButton).SendKeys(Path.GetFullPath(Path.Combine(Assembly.GetExecutingAssembly().Location, "..", "..", "..", "..", "MRO.ROI.Automation", "Files", importRequestPDFFile)));
                Console.WriteLine("Swith frame");

                string fulldirectory = SeleniumHelper.ROITestFileUploads.LocationPath;
                string requesPagesFileName = $"\\{importRequestPDFFile}";
                string requestDocsPath = fulldirectory + requesPagesFileName;

                Driver.FindElementBy(selectFileButton).SendKeys(requestDocsPath);
                Console.WriteLine("File imp");

                Driver.ClickAndCheckNextElement(importPDFButton, importCloseButton);
                Driver.DirectClick(importCloseButton);
                Driver.SwitchToDefaultContent();
                Driver.ScrollToEndOfThePage();
                Driver.SleepTheThread(10);
                Automation.Common.Iframe frame = new Automation.Common.Iframe(Driver, logger, Context);
                frame.SwitchToRoiFrame();
                Driver.Wait(TimeSpan.FromSeconds(1));
                Driver.ScrollIntoViewSmoothly(importPatientPDFButton);
                Driver.DirectClick(importPatientPDFButton);
                Driver.SwitchTo().Frame(importPDFFrame);
                // Driver.FindElementBy(selectFileButton).SendKeys(Path.GetFullPath(Path.Combine(Assembly.GetExecutingAssembly().Location, "..", "..", "..", "..", "MRO.ROI.Automation", "Files", importPatientPDFFile)));

                string patientPagesFileName = $"\\{importPatientPDFFile}";
                string patientDocsPath = fulldirectory + patientPagesFileName;

                Driver.FindElementBy(selectFileButton).SendKeys(patientDocsPath);

                Driver.ClickAndCheckNextElement(importPDFButton, importCloseButton);
                Driver.DirectClick(importCloseButton);
                frame.switchToDefaut();
                frame.SwitchToRoiFrame();
                WaitUntillFileStatusChange();
                logger.Log(Status.Pass, "PDF files imported");
            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to import pdf files : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
            return new ROIAdminFacilityListPage(Driver, logger, Context);
        }

        public ROIAdminFacilityListPage CDImportPdfFiles()
        {
            try
            {
                Driver.ScrollToEndOfThePage();
                Driver.DirectClick(importRequestPDFButton);
                Driver.SwitchTo().Frame(importPDFFrame);
                // Driver.FindElementBy(selectFileButton).SendKeys(Path.GetFullPath(Path.Combine(Assembly.GetExecutingAssembly().Location, "..", "..", "..", "..", "MRO.ROI.Automation", "Files", importRequestPDFFileMulti)));
                string fulldirectory = SeleniumHelper.ROITestFileUploads.LocationPath;
                string requesPagesFileName = $"\\{importRequestPDFFileMulti}";
                string requestDocsPath = fulldirectory + requesPagesFileName;

                Driver.FindElementBy(selectFileButton).SendKeys(requestDocsPath);
                Driver.ClickAndCheckNextElement(importPDFButton, importCloseButton);
                Driver.DirectClick(importCloseButton);
                Driver.ScrollToEndOfThePage();
                Driver.DirectClick(importPatientPDFButton);
                Driver.SwitchTo().Frame(importPDFFrame);
                // Driver.FindElementBy(selectFileButton).SendKeys(Path.GetFullPath(Path.Combine(Assembly.GetExecutingAssembly().Location, "..", "..", "..", "..", "MRO.ROI.Automation", "Files", importPatientPDFFileMulti)));
                string patientPagesFileName = $"\\{importPatientPDFFileMulti}";
                string patientDocsPath = fulldirectory + patientPagesFileName;

                Driver.FindElementBy(selectFileButton).SendKeys(patientDocsPath);
                Driver.ClickAndCheckNextElement(importPDFButton, importCloseButton);
                Driver.DirectClick(importCloseButton);
                logger.Log(Status.Pass, "PDF files imported");
            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to import pdf files : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
            return new ROIAdminFacilityListPage(Driver, logger, Context);
        }

        /// <summary>
        /// Release RequestID
        /// </summary>
        public ROIAdminFacilityListPage ReleaseRequestID()
        {
            try
            {
                WaitUntillFileStatusChange();
                //string text= Driver.GetText(PatientPagesProgress);
                //WaitUntillFileSizeGraterThanZero(text);
                WebElementHelper elementHelper = new WebElementHelper(Driver, logger, Context);
                elementHelper.ScrollIntoView1();
                Driver.ScrollToElement(releseRequestButton);
                Driver.DirectClick(releseRequestButton);
                Driver.SwitchTo().Frame(importPDFFrame);
                Driver.Click(yesRadioButton);
                Driver.Click(By.Id(PageElements.FacilityRequestStatusPage.btnRelease_Id));
                // WebElementHelper webElementHelper = new WebElementHelper(Driver, logger, Context);
                // webElementHelper.Click_Enter();
                Driver.SwitchTo().Alert().Accept();
                Driver.SleepTheThread(5);
            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to release the request : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }

            return new ROIAdminFacilityListPage(Driver, logger, Context);
        }

        public void UnReleaseRequestID()
        {
            try
            {
                Driver.ScrollIntoViewAndClick(unReleaseEle);
                Driver.SleepTheThread(5);
            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to unrelease the request : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }


        /// <summary>
        /// To Get  created RequestId form Facility side and logout
        /// </summary>
        public ROIAdminHomePage GetRequestId()
        {
            string requestId = string.Empty;
            try
            {
                Driver.MoveToElement(headerElement);
                requestId = Driver.FindElementBy(headerElement).Text;
                string id = requestId.Substring(17, 8);
                Driver.WaitUntilDOMLoaded();
                Driver.Click(logoutElement);
                ROIAdminHomePage rOIAdminHomePage = new ROIAdminHomePage(Driver, logger, Context);
                rOIAdminHomePage.ROIlookupByRequestId(id);
                Driver.WaitUntilDOMLoaded();
            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to get request id with details as Message : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }

            return new ROIAdminHomePage(Driver, logger, Context);

        }

        public void WaitUntillFileStatusChange()
        {
            Driver.ScrollToEndOfThePage();
            IWebElement element = null;
            int retry = 1;
            while (retry <= 5 && element == null)
            {
                try
                {
                    element = Driver.FindElementBy(uploadedTxt, 30);
                }
                catch (Exception ex)
                {
                    Driver.DirectClick(By.XPath("//a[@title='Refresh']"));
                    retry++;

                }
            }

            if (element == null)
            {
                throw new Exception($"Even afetr multiple refresh, request or patient pages status not upadetd to uploaded status");
            }
        }

        /// <summary>
        /// Get the request id
        /// </summary>
        public string GetRequestID()
        {
            string id = string.Empty;
            string requestID = String.Empty;
            try
            {
                bool isPresent = Driver.isElementDisplayed(getRequestID);
                if (isPresent)
                {
                    requestID = Driver.FindElementBy(getRequestID).Text.ToString();
                }
                else
                {
                    try
                    {
                        By path = By.XPath("//div[@class='page-heading']//div[contains(text(), 'Request')]");
                        requestID = Driver.FindElementBy(path).Text.ToString();
                    } catch (Exception exp)
                    {

                    }
                }

            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to get the request id : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }

            if (string.IsNullOrEmpty(requestID))
            {
                // ROIMenuSelector menu = new ROIMenuSelector(Driver, logger, Context);
                // menu.SelectRecentRequestID();

                requestID = Driver.FindElementBy(By.XPath("//td[@id='MasterHeaderText']")).Text.ToString();
               
            }
           
                id = requestID.Split('#')[1].ToString().Trim();
                id = id.Replace(')', ' ').Trim();
            

            return id;
        }
        public void WaitUntillFileSizeGraterThanZero(string text)
        {
            int value = Convert.ToInt32(text);

            int timeOut = 7;
            int initialValue = 1;
            try
            {
                while (initialValue != timeOut)
                {
                    if (value > 0)

                    {
                        break;
                    }
                    Driver.SleepTheThread(1);
                    initialValue++;
                }
            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to wait fo the file size grater than zero Message : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }

        /// <summary>
        /// Click on link to another request page
        /// </summary>
        public ROIFacilityLinkToAnotherRequestPage ClickOnLinkToAnotherRequest()
        {
            try
            {
                IWebElement linkToAnotherRequest = Driver.FindElementBy(linkToAnotherRequestBtn);
                linkToAnotherRequest.Click();
                IWebElement verifyLinkToAnotherRequest = Driver.FindElementBy(verifyLinkToAnotherRequestPage);
                if (verifyLinkToAnotherRequest.Displayed == false)
                {
                    Assert.Fail("Failed to open the link to anothe request page");
                }

            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to click and verify link to another request : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }

            return new ROIFacilityLinkToAnotherRequestPage(Driver, logger, Context);

        }
        /// <summary>
        /// Verify linked to request to another request
        /// </summary>
        public bool VerifyLinkedToRequestToAnotherRequest()
        {
            bool isVerified = false;
            try
            {
                Driver.ScrollToElement(verifyLinkedToRequest);
                IWebElement linkToAnotherRequest = Driver.FindElementBy(verifyLinkedToRequest);
                if (linkToAnotherRequest.Displayed == true)
                {
                    isVerified = true;
                }
                return isVerified;
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to verify link to another request : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }

        }

        /// <summary>
        /// select Recent RequestID
        /// </summary>
        public void SelectRecentRequest()
        {
            try
            {
                ROIMenuSelector menuSelector = new ROIMenuSelector(Driver, logger, Context);
                menuSelector.SelectRecentRequestID();
            }
            catch (Exception ex)
            {


                throw new Exception($"Failed to select recent request id  : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }

        }

        /// <summary>
        /// Click on view documents and verify files
        /// </summary>
        public bool ClickViewDocumentsAndReturnIfCopiedpatientDocumentsDisplayed()
        {
            bool isDisplayed = false;
            try
            {
                Driver.Click(viewDocumentButton);
                WaitUntillPageLoaded();
                Driver.SwitchToWindow("MRO Viewer");
                Driver.Manage().Window.Maximize();
                IWebElement verifyDocument = Driver.FindElementBy(verifyDocuments, 10);
                if (verifyDocument.Displayed == true)
                {
                    isDisplayed = true;
                }
                Driver.Wait(TimeSpan.FromSeconds(1));
                Driver.SwitchToWindowAndClose("MRO Viewer");
                Driver.SleepTheThread(3);
                Driver.SwitchToWindow("ROI Log: Request Status");
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to click and verify view documents : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
            return isDisplayed;
        }

        /// <summary>
        /// Click on view documents and verify files
        /// </summary>
        public int ClickViewDocumnetAndReturnPatientDocumentsCount()
        {
            int patientDocCount = 0;
            try
            {
                Driver.Click(viewDocumentButton);
                WaitUntillPageLoaded();
                Driver.Manage().Window.Maximize();
                IWebElement patientDocsElement = Driver.FindElementBy(verifyPatientDocuments, 15);
                if (patientDocsElement != null)
                {
                    patientDocCount = Convert.ToInt32(patientDocsElement.Text.Trim());
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to verify patient pages count on view documents : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
            return patientDocCount;
        }



        public void WaitUntillPageLoaded()
        {
            IWebElement verifyDocument = null;
            int retry = 1;
            while (retry <= 5 && verifyDocument == null)
            {
                try
                {
                    Driver.SwitchToWindow("MRO Viewer");
                    verifyDocument = Driver.FindElementBy(verifyPatientDocuments, 20);
                }
                catch (Exception ex)
                {
                    Driver.SleepTheThread(1);
                    retry++;

                }
            }
        }

        /// <summary>
        /// get total patient pages
        /// </summary>
        /// <returns></returns>
        public int GetTotalPatientPagesCount()
        {
            int getCountTotalPatientPages;
            try
            {
                Driver.ScrollToEndOfThePage();
                string getTotalPatientPagesCount = Driver.FindElementBy(getTotalPatientPages).Text.ToString();
                getCountTotalPatientPages = Convert.ToInt32(getTotalPatientPagesCount.Trim());
                return getCountTotalPatientPages;
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to get total patient pages count : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }

        }


        public string FormatAndGetRequestId()
        {
            try
            {
                string requestId = Driver.GetText(headerElement);
                string id = requestId.Substring(17, 8);
                return id;
            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to get requestId with deatails as Message : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }

        }

        /// <summary>
        /// Click on AuditTrail Button
        /// </summary>
        public void ClickOnAuditTrail()
        {
            try
            {
                Driver.ScrollIntoViewAndClick(auditTrailButton);
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to get request id with details as Message : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }

        }


        /// <summary>
        /// Verify Status Under ImportDocument
        /// </summary>
        public bool VerifyStatusUnderImportDocument()
        {
            WaitUntillFileStatusChange();
            bool isStatus = false;
            try
            {
                string statusOfImportedDocs = Driver.GetText(verifyImportedDocumentStatus).ToString();
                if (statusOfImportedDocs == "Uploaded")
                {
                    isStatus = true;
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to validate status under import document is uploaded : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
            return isStatus;
        }

        public ROIAdminFacilityListPage ImportDuplicatePDFFiles()
        {
            try
            {
                Driver.ScrollToEndOfThePage();
                Driver.DirectClick(importRequestPDFButton);
                Driver.SwitchTo().Frame(importPDFFrame);
                Driver.FindElementBy(selectFileButton).SendKeys(Path.GetFullPath(Path.Combine(Assembly.GetExecutingAssembly().Location, "..", "..", "..", "..", "MRO.ROI.Automation", "Files", importPatientDuplicatePDFMulti)));
                Driver.ClickAndCheckNextElement(importPDFButton, importCloseButton);
                Driver.DirectClick(importCloseButton);
                Driver.ScrollToEndOfThePage();
                Driver.DirectClick(importPatientPDFButton);
                Driver.SwitchTo().Frame(importPDFFrame);
                Driver.FindElementBy(selectFileButton).SendKeys(Path.GetFullPath(Path.Combine(Assembly.GetExecutingAssembly().Location, "..", "..", "..", "..", "MRO.ROI.Automation", "Files", importPatientDuplicatePDFMulti)));
                Driver.ClickAndCheckNextElement(importPDFButton, importCloseButton);
                Driver.DirectClick(importCloseButton);
                logger.Log(Status.Info, $"Successfully imported patient pdf file called ({importPatientDuplicatePDFMulti})");
            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to import pdf files : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
            return new ROIAdminFacilityListPage(Driver, logger, Context);
        }

        /// <summary>
        /// Go to RSS Page and verify request status
        /// </summary>
        public string VerifyRequestStatus()
        {
            try
            {
                bool result = Driver.FindElementBy(complianceHoldChkBox).Selected;
                if (result == true)
                {
                    logger.Log(Status.Pass, "Successfully verified compliance hold checkbox checked ");
                }
                string status = Driver.GetText(requestStatusEle);
                return status;
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to to verify request status with deatails as Message : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }

        /// <summary>
        /// Release request
        /// </summary>
        public string ReleaseRequestForInternalRequest()
        {
            try
            {
                Driver.Click(releseRequestButton);
                Driver.Navigate().Refresh();
                string status = Driver.GetText(requestStatusEle);
                return status;


            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to release request with deatails as Message : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }

        /// <summary> 
        /// Facility logout
        /// </summary>
        public void FacilityLogout()
        {
            try
            {
                Driver.Wait(TimeSpan.FromSeconds(2));
                Driver.Click(logoutElement);
                Driver.Wait(TimeSpan.FromSeconds(2));
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to logout from facility side with details as Message : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }

        }

        /// <summary> 
        /// Go back to facility side and importpdf
        /// </summary>
        public void DrillIntoFacility()
        {
            try
            {
                Driver.DirectClick(drillIntoFacilitylink);
                Driver.Click(unReleaseEle);
                Driver.Click(importPatientPDFButton);
                Driver.SwitchTo().Frame(importPDFFrame);
                Driver.FindElementBy(selectFileButton).SendKeys(Path.GetFullPath(Path.Combine(Assembly.GetExecutingAssembly().Location, "..", "..", "..", "..", "MRO.ROI.Automation", "Files", importMROPDFFile1)));
                Driver.FindElementBy(removeButton, 180);
                Driver.SleepTheThread(7);
                Driver.ClickAndCheckNextElement(importPDFButton, importCloseButton);
                Driver.DirectClick(importCloseButton);
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to import pdf documents  with deatails as Message : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }


        /// <summary> 
        /// Click on updateInfo
        /// </summary>
        public void UpdateLocation()
        {
            try
            {
                Driver.Click(updateInfoButton);
                Driver.Click(locationDropdownEle);
                Driver.ScrollIntoViewAndClick(kingOfPrussiaLocSelection);
                Driver.SleepTheThread(4);
                Driver.ScrollIntoViewAndClick(updateBtn);
                Driver.SleepTheThread(4);
                ///Driver.DirectClick(logoutElement);

            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to update location with deatails as Message : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }

        }

        /// <summary>
        /// get total patient pages
        /// </summary>
        /// <returns></returns>
        public int GetRequestPagesCount()
        {
            int getRequestPages;
            try
            {
                Driver.ScrollToEndOfThePage();
                string getRequestPagesCount = Driver.FindElementBy(requestPageTxt).Text.ToString();
                getRequestPages = Convert.ToInt32(getRequestPagesCount.Trim());
                return getRequestPages;
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to get request pages count : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }

        }

        /// <summary>
        /// Click on view documents and verify files
        /// </summary>

        public int ReturnRequestDocumentsCount()
        {
            int requestDocCount = 0;
            try
            {
                IWebElement requestDocsElement = Driver.FindElementBy(requestPageTxtOnViewDoc);
                if (requestDocsElement != null)
                {
                    requestDocCount = Convert.ToInt32(requestDocsElement.Text.Trim());

                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to verify request pages count on view documents : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
            return requestDocCount;
        }

        /// <summary>
        /// Click on view documents and verify files
        /// </summary>
        public int ReturnTotalPatientAndRequestDocumentsCount()
        {
            int patientAndRequestDocCount = 0;
            try
            {
                IWebElement patientAndRequestDocsElement = Driver.FindElementBy(totalRequestAndPatientTxtOnViewDoc, 15);
                if (patientAndRequestDocsElement != null)
                {
                    patientAndRequestDocCount = Convert.ToInt32(patientAndRequestDocsElement.Text.Trim());
                }
                Driver.Wait(TimeSpan.FromSeconds(1));
                Driver.SwitchToWindowAndClose("MRO Viewer");
                Driver.SleepTheThread(3);
                Driver.SwitchToWindow("ROI Log: Request Status");
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to verify total patient and request pages count on view documents : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
            return patientAndRequestDocCount;
        }

        /// <summary>
        /// get patient pages
        /// </summary>
        /// <returns></returns>
        public int GetPatientPagesCount()
        {
            int getCountPatientPages;
            try
            {
                string getPatientPagesCount = Driver.FindElementBy(getPatientPages).Text.ToString();
                getCountPatientPages = Convert.ToInt32(getPatientPagesCount.Trim());
                return getCountPatientPages;
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to get total patient pages : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }

        }

        /// <summary>
        /// get request pages
        /// </summary>
        /// <returns></returns>
        public int GetRequestPagesCountOnRs()
        {
            int getCountRequestPagesCount;
            try
            {
                string getRequestPagesCount = Driver.FindElementBy(requestPagesCount).Text.ToString();
                getCountRequestPagesCount = Convert.ToInt32(getRequestPagesCount.Trim());
                return getCountRequestPagesCount;
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to get request pages count on RS page : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }

        }
        public string ClickOnSetId()
        {
            try
            {
                Driver.Click(setIdLink);
                Driver.Click(epicRoiTextBox);
                int epicValue = ran.Next(10000000, 99999999);
                Driver.SendKeys(epicRoiTextBox, epicValue.ToString());
                Driver.Click(checkMarkEle);
                Driver.SwitchTo().Alert().SendKeys(roiTextValue);
                Driver.SwitchTo().Alert().Accept();
                string setId = Driver.GetText(epicRoiIdEle);
                return setId;
            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to click setId link with deatails as Message : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }

        public string ReturnExtReleaseId()
        {
            try
            {
                string ExtId = Driver.GetText(extReleaseIdEle);
                return ExtId;
            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to return ExtId with deatails as Message : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }

        }

        public bool CheckViewDocumentsDisabled()
        {
            bool isDisabled = false;
            try
            {
                bool result = Driver.FindElement(viewDocumentButton).Displayed;
                IWebElement viewDocumentsElement = Driver.FindElementBy(viewDocumentButton);

                if (viewDocumentsElement != null)
                {
                    string value = viewDocumentsElement.GetAttribute("disabled");

                    if (value == "true")
                    {
                        isDisabled = true;
                    }

                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to check view documents, Exception detail: {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
            return isDisabled;
        }
        public void UncheckComplianceHoldCheckbox()
        {
            try
            {
                Driver.Click(complianceHoldChkBox);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public ROIAdminFacilityListPage ImportPdfFilesForInternalRequest()
        {
            try
            {
                Driver.ScrollToEndOfThePage();
                Driver.DirectClick(importRequestPDFButton);
                Driver.SwitchTo().Frame(importPDFFrame);
                //Driver.FindElementBy(selectFileButton).SendKeys(Path.GetFullPath(Path.Combine(Assembly.GetExecutingAssembly().Location, "..", "..", "..", "..", "MRO.ROI.Automation", "Files", importRequestPDFFile)));

                string fulldirectory = SeleniumHelper.ROITestFileUploads.LocationPath;
                string requesPagesFileName = $"\\{importRequestPDFFile}";
                string requestDocsPath = fulldirectory + requesPagesFileName;
                Driver.FindElementBy(selectFileButton).SendKeys(requestDocsPath);

                Driver.ClickAndCheckNextElement(importPDFButton, importCloseButton);
                Driver.DirectClick(importCloseButton);
                Driver.ScrollToEndOfThePage();
                Driver.DirectClick(importPatientPDFButton);
                Driver.SwitchTo().Frame(importPDFFrame);
                //Driver.FindElementBy(selectFileButton).SendKeys(Path.GetFullPath(Path.Combine(Assembly.GetExecutingAssembly().Location, "..", "..", "..", "..", "MRO.ROI.Automation", "Files", importPatientYalePdf)));

                string patientPagesFileName = $"\\{importPatientYalePdf}";
                string patientDocsPath = fulldirectory + patientPagesFileName;
                Driver.FindElementBy(selectFileButton).SendKeys(patientDocsPath);

                Driver.ClickAndCheckNextElement(importPDFButton, importCloseButton);
                Driver.DirectClick(importCloseButton);
                WaitUntillFileStatusChange();
                logger.Log(Status.Pass, "PDF files imported");
            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to import pdf files : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
            return new ROIAdminFacilityListPage(Driver, logger, Context);
        }
        /// <summary>
        /// set epic roi id and click on save delivery date
        /// </summary>
        public int SetEpicRoiIdAndClickOnSaveDeliveryDate()
        {
            try
            {
                Driver.Click(setEpicRoiIdLnk);
                int ID = random.Next(10, 2000);
                IWebElement setEpicRoiValue = Driver.FindElementBy(setEpicRoiIdValue);
                setEpicRoiValue.SendKeys(ID.ToString());
                logger.Log(Status.Info, $"Entered epic roi id ({ID.ToString()})");
                Driver.Click(saveDeliveryDateBtn);
                Driver.SleepTheThread(3);
                return ID;
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to set epic roi id : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }

        /// <summary>
        /// Epic Roi id lookup
        /// </summary>       

        public void epicRoiIDLookup(int epicRoiID)
        {
            try
            {
                Driver.SwitchTo().Alert().SendKeys(epicRoiID.ToString());
                Driver.SwitchTo().Alert().Accept();
                Driver.SleepTheThread(3);
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to provide epic roi id with details as Message : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }

        }
        public void ReleaseRequest()
        {
            try
            {
                WaitUntillFileStatusChange();
                WebElementHelper elementHelper = new WebElementHelper(Driver, logger, Context);
                elementHelper.ScrollIntoView1();
                Driver.DirectClick(releseRequestButton);
                Driver.SleepTheThread(10);
                //Driver.SwitchTo().Frame(importPDFFrame);
                //Driver.Click(yesRadioButton);                
                Driver.SwitchTo().Alert().Accept();
                Driver.SleepTheThread(5);
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to release the request : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }

        }
        public string GetDeliveryMethod()
        {
            try
            {
                string deliveryMethodTxt = Driver.GetText(deliveryMethod);
                return deliveryMethodTxt;
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to verify delivery method: {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }

        public void ClickUnRelease()
        {
            try
            {
                IWebElement unRelease = Driver.FindElementBy(UnRelease);
                unRelease.Click();
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to get request pages count on RS page : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }

        }


        public string ReturnRequestCreatedUser()
        {
            try
            {
                string createdUser = Driver.GetText(createdUsername);
                createdUser = createdUser.Replace('(', ' ').Replace(')', ' ').Trim();
                return createdUser;

            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to return request created user ,Exception details as : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }
        public string VerifyPatientName()
        {
            try
            {
                string patientName = Driver.GetText(patientNameElement);
                return patientName;
            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to verify patient name  ,Exception details as : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }
        public string VerifyLocation()
        {
            try
            {
                string location = Driver.GetText(locationElement);
                return location;
            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to return location ,Exception details as : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }
        public ROIAdminFacilityListPage ImportPdfFilesForNewInvoiceRevision()
        {
            try
            {
                Driver.ScrollToEndOfThePage();
                Driver.DirectClick(importRequestPDFButton);
                Driver.SwitchTo().Frame(importPDFFrame);
                //Driver.FindElementBy(selectFileButton).SendKeys(Path.GetFullPath(Path.Combine(Assembly.GetExecutingAssembly().Location, "..", "..", "..", "..", "MRO.ROI.Automation", "Files", importRequestPDFFile)));

                string fulldirectory = SeleniumHelper.ROITestFileUploads.LocationPath;
                string requesPagesFileName = $"\\{importRequestPDFFile}";
                string requestDocsPath = fulldirectory + requesPagesFileName;
                Driver.FindElementBy(selectFileButton).SendKeys(requestDocsPath);

                Driver.ClickAndCheckNextElement(importPDFButton, importCloseButton);
                Driver.DirectClick(importCloseButton);
                Driver.ScrollToEndOfThePage();
                Driver.DirectClick(importPatientPDFButton);
                Driver.SwitchTo().Frame(importPDFFrame);
                //Driver.FindElementBy(selectFileButton).SendKeys(Path.GetFullPath(Path.Combine(Assembly.GetExecutingAssembly().Location, "..", "..", "..", "..", "MRO.ROI.Automation", "Files", importPatientYalePdf)));

                string patientPagesFileName = $"\\{importPatientYalePdf}";
                string patientDocsPath = fulldirectory + patientPagesFileName;
                Driver.FindElementBy(selectFileButton).SendKeys(patientDocsPath);

                Driver.ClickAndCheckNextElement(importPDFButton, importCloseButton);
                Driver.DirectClick(importCloseButton);
                WaitUntillFileStatusChange();
                logger.Log(Status.Pass, "PDF files imported");
            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to import pdf files : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
            return new ROIAdminFacilityListPage(Driver, logger, Context);
        }

        public void ClickCreatePDF()
        {
            try
            {
                IWebElement createPDFBtn = Driver.FindElementBy(CreatePDF);
                createPDFBtn.Click();
                Driver.SwitchTo().Alert().Accept();

            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to get records sent by facility date with deatails as Message : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }

        }

        public void ReloadReqID()
        {
            try
            {
                //WebElementHelper helper = new WebElementHelper(Driver, logger, Context);
                //helper.ScrollIntoView("mrocontent_cmdLogRequest", FindElementBy.Id);
                //Driver.FindElementBy(logRequestButton).Click();
                MenuSelector menuSelector = new MenuSelector(Driver, logger, Context);
                menuSelector.SelectRecentRequestID();
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to reload the request id in RS page : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }

        }

        public string GetShipmentStatus()
        {
            try
            {
                //WebElementHelper helper = new WebElementHelper(Driver, logger, Context);
                //helper.ScrollIntoView("mrocontent_btnCreateNewPDF", FindElementBy.Id);
                //Driver.ScrollToElement(ShipmentStatus);
                string shipmentStatus = Driver.FindElementBy(ShipmentStatus).Text;
                return shipmentStatus;
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to get shipment status on RS page : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }

        }

        public string GetShipmentDate()
        {
            try
            {
                string shipmentDate = Driver.FindElementBy(ShipmentDate).Text;
                return shipmentDate;
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to get shipped date on RS page : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }

        }

        public bool VerifyDownloadBtnVisible()
        {
            bool isDisplayed = false;
            try
            {
                IWebElement verifyDownloadBtn = Driver.FindElementBy(Download, 10);
                if (verifyDownloadBtn.Displayed == true)
                {
                    isDisplayed = true;
                }

            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to click and verify view documents : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
            return isDisplayed;
        }

        public string GetRequestIDFromEmail(string mailLocation, string requestID)
        {
            string newRequestID = string.Empty;
            try
            {
                Driver.SleepTheThread(4);
                DirectoryInfo dir = new DirectoryInfo(mailLocation);
                FileInfo[] files = dir.GetFiles().OrderByDescending(p => p.CreationTime).ToArray();
                foreach (FileInfo file in files)
                {
                    newRequestID = ParseEmailAndGetRequestData(file.FullName, requestID);
                    if (newRequestID != "" && newRequestID.Length > 1)
                    {
                        break; // get out of the loop
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to create email with the request id : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
            return newRequestID;
        }

        public static string ParseEmailAndGetRequestData(string emlFile, string requestID)
        {
            // bool isEmailCreated = false;
            string requestIDFromMail = string.Empty;
            try
            {
                Mail oMail = new Mail("TryIt");
                oMail.Load(emlFile, false);
                //Parse Mail Text/Plain body
                string mailContent = oMail.TextBody;
                // Parse Mail From, Sender
                string mailFrom = oMail.From.ToString();
                // Parse Mail Subject
                string mailSubject = oMail.Subject;
                int index = mailSubject.IndexOf("ID");
                if (index != -1)
                {
                    requestIDFromMail = mailSubject.Substring(index + 2);
                    int charindex = requestIDFromMail.IndexOf(")");
                    if (charindex >= 0)
                        requestIDFromMail = requestIDFromMail.Substring(0, charindex);
                    requestIDFromMail = requestIDFromMail.Trim();
                }

            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to parse the email content with message : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
            return requestIDFromMail;

        }
        public void ImportPdfFilesforAllComponents()
        {
            try
            {
                Driver.ScrollToEndOfThePage();
                Driver.DirectClick(importRequestPDFButton);
                Driver.SwitchTo().Frame(importPDFFrame);
                //Driver.FindElementBy(selectFileButton).SendKeys(Path.GetFullPath(Path.Combine(Assembly.GetExecutingAssembly().Location, "..", "..", "..", "..", "MRO.ROI.Automation", "Files", importRequestPDFFile1)));

                string fulldirectory = SeleniumHelper.ROITestFileUploads.LocationPath;
                string requesPagesFileName = $"\\{importRequestPDFFile1}";
                string requestDocsPath = fulldirectory + requesPagesFileName;

                Driver.FindElementBy(selectFileButton).SendKeys(requestDocsPath);

                Driver.ClickAndCheckNextElement(importPDFButton, importCloseButton);
                Driver.DirectClick(importCloseButton);

                Driver.DirectClick(importPatientPDFButton);
                Driver.SwitchTo().Frame(importPDFFrame);
                //Driver.FindElementBy(selectFileButton).SendKeys(Path.GetFullPath(Path.Combine(Assembly.GetExecutingAssembly().Location, "..", "..", "..", "..", "MRO.ROI.Automation", "Files", importPatientPDFFile1)));
                string patientPagesFileName = $"\\{importPatientPDFFile1}";
                string patientDocsPath = fulldirectory + patientPagesFileName;
                Driver.FindElementBy(selectFileButton).SendKeys(patientDocsPath);

                Driver.ClickAndCheckNextElement(importPDFButton, importCloseButton);
                Driver.DirectClick(importCloseButton);

                Driver.DirectClick(addComponentLnk);
                Driver.SleepTheThread(5);
                string descTxt1 = IniHelper.ReadConfig("ROIAdminDragAndDropPDFFileIntoPatientPagesComponent", "Description1");
                var selectComponentLoc = Driver.FindElementBy(selectComponentLocation);
                var selectComponent = new SelectElement(selectComponentLoc);
                selectComponent.SelectByText("Allscripts");
                Driver.SendKeys(txtDescription, descTxt1);
                Driver.DirectClick(addButton);
                Driver.DirectClick(addComponentButton);
                Driver.DirectClick(importMROTestPDFButtonTest1);
                Driver.SwitchTo().Frame(importPDFFrame);
                // Driver.FindElementBy(selectFileButton).SendKeys(Path.GetFullPath(Path.Combine(Assembly.GetExecutingAssembly().Location, "..", "..", "..", "..", "MRO.ROI.Automation", "Files", importAddCompPDFFile)));
                string requesPagesFileName1 = $"\\{importAddCompPDFFile}";
                string requestDocsPath1 = fulldirectory + requesPagesFileName1;

                Driver.FindElementBy(selectFileButton).SendKeys(requestDocsPath1);

                Driver.ClickAndCheckNextElement(importPDFButton, importCloseButton);
                Driver.DirectClick(importCloseButton);

                Driver.DirectClick(addCertificationComp);
                Driver.SleepTheThread(5);
                Driver.DirectClick(importAddCertificationComp);
                Driver.SwitchTo().Frame(importPDFFrame);
                //Driver.FindElementBy(selectFileButton).SendKeys(Path.GetFullPath(Path.Combine(Assembly.GetExecutingAssembly().Location, "..", "..", "..", "..", "MRO.ROI.Automation", "Files", importAddCeritificationCompPDFFile)));
                string requesPagesFileName2 = $"\\{importAddCeritificationCompPDFFile}";
                string requestDocsPath2 = fulldirectory + requesPagesFileName2;

                Driver.FindElementBy(selectFileButton).SendKeys(requestDocsPath2);

                Driver.ClickAndCheckNextElement(importPDFButton, importCloseButton);
                Driver.DirectClick(importCloseButton);

                Driver.DirectClick(addNoRecordslink);
                Driver.SleepTheThread(5);
                Driver.DirectClick(importAddNoRecordsComp);
                Driver.SwitchTo().Frame(importPDFFrame);
                //Driver.FindElementBy(selectFileButton).SendKeys(Path.GetFullPath(Path.Combine(Assembly.GetExecutingAssembly().Location, "..", "..", "..", "..", "MRO.ROI.Automation", "Files", importAddNoRecordCompPDFFile)));
                string requesPagesFileName3 = $"\\{importAddNoRecordCompPDFFile}";
                string requestDocsPath3 = fulldirectory + requesPagesFileName3;
                Driver.FindElementBy(selectFileButton).SendKeys(requestDocsPath3);

                Driver.ClickAndCheckNextElement(importPDFButton, importCloseButton);
                Driver.DirectClick(importCloseButton);

                Driver.DirectClick(addCorrespondenceComp);
                Driver.SleepTheThread(5);
                Driver.DirectClick(importAddCorrespondenceComp);
                Driver.SwitchTo().Frame(importPDFFrame);
                //Driver.FindElementBy(selectFileButton).SendKeys(Path.GetFullPath(Path.Combine(Assembly.GetExecutingAssembly().Location, "..", "..", "..", "..", "MRO.ROI.Automation", "Files", imporAddCorrespondenceCompPDFFile)));
                string requesPagesFileName4 = $"\\{imporAddCorrespondenceCompPDFFile}";
                string requestDocsPath4 = fulldirectory + requesPagesFileName4;
                Driver.FindElementBy(selectFileButton).SendKeys(requestDocsPath4);

                Driver.ClickAndCheckNextElement(importPDFButton, importCloseButton);
                Driver.DirectClick(importCloseButton);
                WaitUntillFileStatusChange();
                logger.Log(Status.Pass, "PDF files imported");
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to import pdf files : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }
        public void ImportRequestPagesForPDFDelivery()
        {
            try
            {
                Driver.ScrollToEndOfThePage();
                Driver.DirectClick(importRequestPDFButton);
                Driver.SwitchTo().Frame(importPDFFrame);
                //Driver.FindElementBy(selectFileButton).SendKeys(Path.GetFullPath(Path.Combine(Assembly.GetExecutingAssembly().Location, "..", "..", "..", "..", "MRO.ROI.Automation", "Files", importYaleTest)));

                string fulldirectory = SeleniumHelper.ROITestFileUploads.LocationPath;
                string requesPagesFileName = $"\\{importYaleTest}";
                string requestDocsPath = fulldirectory + requesPagesFileName;

                Driver.FindElementBy(selectFileButton).SendKeys(requestDocsPath);

                Driver.ClickAndCheckNextElement(importPDFButton, importCloseButton);
                Driver.DirectClick(importCloseButton);

                Driver.ScrollToEndOfThePage();
                Driver.DirectClick(importRequestPDFButton);
                Driver.SwitchTo().Frame(importPDFFrame);
                // Driver.FindElementBy(selectFileButton).SendKeys(Path.GetFullPath(Path.Combine(Assembly.GetExecutingAssembly().Location, "..", "..", "..", "..", "MRO.ROI.Automation", "Files", importUnityPoint2)));

                string requesPagesFileName2 = $"\\{importUnityPoint2}";
                string requestDocsPath2 = fulldirectory + requesPagesFileName2;
                Driver.FindElementBy(selectFileButton).SendKeys(requestDocsPath2);

                Driver.ClickAndCheckNextElement(importPDFButton, importCloseButton);
                Driver.DirectClick(importCloseButton);

                Driver.ScrollToEndOfThePage();
                Driver.DirectClick(importRequestPDFButton);
                Driver.SwitchTo().Frame(importPDFFrame);
                // Driver.FindElementBy(selectFileButton).SendKeys(Path.GetFullPath(Path.Combine(Assembly.GetExecutingAssembly().Location, "..", "..", "..", "..", "MRO.ROI.Automation", "Files", importWellspan)));
                string requesPagesFileName3 = $"\\{importWellspan}";
                string requestDocsPath3 = fulldirectory + requesPagesFileName3;
                Driver.FindElementBy(selectFileButton).SendKeys(requestDocsPath3);

                Driver.ClickAndCheckNextElement(importPDFButton, importCloseButton);
                Driver.DirectClick(importCloseButton);
                //
                Driver.ScrollToEndOfThePage();
                Driver.DirectClick(importRequestPDFButton);
                Driver.SwitchTo().Frame(importPDFFrame);
                //Driver.FindElementBy(selectFileButton).SendKeys(Path.GetFullPath(Path.Combine(Assembly.GetExecutingAssembly().Location, "..", "..", "..", "..", "MRO.ROI.Automation", "Files", importUnityPoint3)));
                string requesPagesFileName4 = $"\\{importUnityPoint3}";
                string requestDocsPath4 = fulldirectory + requesPagesFileName4;
                Driver.FindElementBy(selectFileButton).SendKeys(requestDocsPath4);

                Driver.ClickAndCheckNextElement(importPDFButton, importCloseButton);
                Driver.DirectClick(importCloseButton);
                WaitUntillFileStatusChange();
                logger.Log(Status.Pass, "PDF files imported");

            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to import PDF files: {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }

        public void ImportPatientPagesForPDFDelivery()
        {
            try
            {
                Driver.ScrollIntoViewAndClick(By.XPath("//span[contains(text(),'Patient Pages')]/../..//input[@value='Import']"));
                Driver.SwitchTo().Frame(importPDFFrame);
                //Driver.FindElementBy(selectFileButton).SendKeys(Path.GetFullPath(Path.Combine(Assembly.GetExecutingAssembly().Location, "..", "..", "..", "..", "MRO.ROI.Automation", "Files", importPatientPDF)));
                string fulldirectory = SeleniumHelper.ROITestFileUploads.LocationPath;
                string patientPagesFileName = $"\\{importPatientPDF}";
                string patientDocsPath = fulldirectory + patientPagesFileName;

                Driver.FindElementBy(selectFileButton).SendKeys(patientDocsPath);
                Driver.ClickAndCheckNextElement(importPDFButton, importCloseButton);
                Driver.DirectClick(importCloseButton);
                WaitUntillFileStatusChange();
                logger.Log(Status.Pass, $"Patient PDF file imported");

            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to import Patient PDF file : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }

        public void ClickPendingExternalSiteUploads()
        {
            try
            {
                ROIMenuSelector selector = new ROIMenuSelector(Driver, logger, Context);
                selector.Select("ROI Requests", "Pending External Site Uploads");
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed  to navigate to Pending External Site Uploads page with Message : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }

        public void ImportPatientPagesForOnsiteDelivery()
        {
            try
            {
                Driver.ScrollIntoViewAndClick(By.XPath("//span[contains(text(),'Patient Pages')]/../..//input[@value='Import']"));
                Driver.SwitchTo().Frame(importPDFFrame);
                //
                string fulldirectory = SeleniumHelper.ROITestFileUploads.LocationPath;
                string patinetPagesFileName = $"\\{importStatOptionPDF}";
                string patientDocsPath = fulldirectory + patinetPagesFileName;
                Driver.FindElementBy(selectFileButton).SendKeys(patientDocsPath);
                //
                //Driver.FindElementBy(selectFileButton).SendKeys(Path.GetFullPath(Path.Combine(Assembly.GetExecutingAssembly().Location, "..", "..", "..", "..", "MRO.ROI.Automation", "Files", importStatOptionPDF)));
                Driver.ClickAndCheckNextElement(importPDFButton, importCloseButton);
                Driver.DirectClick(importCloseButton);
                WaitUntillFileStatusChange();
                logger.Log(Status.Pass, $"Patient PDF file imported");
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to import Patient PDF file : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }


        public void SelectEnterpriseDashboard()
        {
            try
            {

                ROIMenuSelector selector = new ROIMenuSelector(Driver, logger, Context);
                selector.Select("MRO Analyze", "Enterprise Dashboard");
            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to select Enterprise dashboard   as : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }


        }
        public void ClickOnChangeAmountAndMakeOnsitePayment(string amount)
        {
            try
            {
                string adjustmentAmount = amount.Split('$')[1].ToString().Trim();
                Driver.Click(changeAmountLnk);
                Driver.SleepTheThread(5);
                Driver.Click(creditCardRadio);
                Driver.SendKeys(paymentAmountTxt, adjustmentAmount);
                Driver.Click(continueBtn);
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to click on change amount and make onsite payment : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }

        }
        public void clickOnRequestImagingDelivery()
        {
            try
            {
                Driver.Click(requestImagingDeliveryBtn);
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to click on request imaging delivery : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }

        }

        public void ImportPdfFilesForPurposeOfUse()
        {
            try
            {
                Driver.ScrollToEndOfThePage();
                Driver.DirectClick(importRequestPDFButton);
                Driver.SwitchTo().Frame(importPDFFrame);
                //Driver.FindElementBy(selectFileButton).SendKeys(Path.GetFullPath(Path.Combine(Assembly.GetExecutingAssembly().Location, "..", "..", "..", "..", "MRO.ROI.Automation", "Files", importRequestPDFFile)));

                string fulldirectory = SeleniumHelper.ROITestFileUploads.LocationPath;
                string requesPagesFileName = $"\\{importPatientPDF}";
                string requestDocsPath = fulldirectory + requesPagesFileName;

                Driver.FindElementBy(selectFileButton).SendKeys(requestDocsPath);

                Driver.ClickAndCheckNextElement(importPDFButton, importCloseButton);
                Driver.DirectClick(importCloseButton);
                Driver.ScrollToEndOfThePage();
                Driver.DirectClick(importPatientPDFButton);
                Driver.SwitchTo().Frame(importPDFFrame);

                string patientPagesFileName = $"\\{importCarilion}";
                string patientDocsPath = fulldirectory + patientPagesFileName;

                Driver.FindElementBy(selectFileButton).SendKeys(patientDocsPath);

                Driver.ClickAndCheckNextElement(importPDFButton, importCloseButton);
                Driver.DirectClick(importCloseButton);
                WaitUntillFileStatusChange();
            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to import pdf files : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }
        public void ClickOnHoldCheckbox()
        {
            try
            {
                HideLicenseRequireLabelIfShowing();
                Driver.ScrollIntoViewSmoothly(onHoldChk);
                Driver.DirectClick(onHoldChk);
                Driver.SleepTheThread(2);
                Driver.SwitchTo().Frame(importPDFFrame);
                var _onHoldDeasonDrp = Driver.FindElementBy(onHoldReasonDrp);
                var selectElement = new SelectElement(_onHoldDeasonDrp);
                selectElement.SelectByText("Test");
                Driver.Click(saveBtn);
                Driver.SwitchTo().DefaultContent();
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to click on-hold checkbox : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }

        public bool VerifyOnHoldReason()
        {
            try
            {
                HideLicenseRequireLabelIfShowing();
                bool isDisplayed = false;
                string onHoldReasonTxt = Driver.FindElementBy(requestStatus).Text.Trim();
                if (onHoldReasonTxt == "Reason: Test")
                {
                    isDisplayed = true;
                }
                return isDisplayed;
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to verify on hold reason : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }
        public string VerifyShipTo()
        {
            try
            {
                string shipToValue = Driver.GetText(shipToTxt).Trim();
                return shipToValue;
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to verify shipto : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }


        public string VerifyEmailId()
        {
            try
            {
                string emailTxt = Driver.GetText(emailId).Trim();
                return emailTxt;
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to verify email : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }
        public bool VerifyWaitingPrepaymentTxtVisible()
        {
            try
            {
                Driver.ScrollToElement(By.XPath("//span[@id='mrocontent_lblPreAuth']"));
                WebElementHelper helper = new WebElementHelper(Driver, logger, Context);
                bool text = helper.IsElementPresent("//span[@id='mrocontent_lblPreAuth']");
                return text;
            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to verify waiting prepayment text : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }
        public void SelectSummaryDashboard()
        {
            try
            {

                ROIMenuSelector selector = new ROIMenuSelector(Driver, logger, Context);
                selector.Select("MRO Analyze", "Summary Dashboard");
            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to select summary dashboard   as : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }


        public int ClickOnMaginifierIcon()
        {
            int patientDocCount = 0;
            try
            {

                Driver.Click(By.XPath("//*[@id='mrocontent_custPDFImportCtl_RadGridImport_ctl00_ctl06_btnView']/i"));
                Driver.SwitchToWindow("MRO Viewer");
                Driver.Manage().Window.Maximize();
                IWebElement patientDocsElement = Driver.FindElementBy(verifyPatientDocuments, 15);
                if (patientDocsElement != null)
                {
                    patientDocCount = Convert.ToInt32(patientDocsElement.Text.Trim());
                }

                Driver.Wait(TimeSpan.FromSeconds(1));
                Driver.SwitchToWindowAndClose("MRO Viewer");
                Driver.SleepTheThread(3);
                Driver.SwitchToWindow("ROI Log: Request Status");

            }
            catch (Exception ex)
            {

                throw;
            }
            return patientDocCount;
        }

        public int GetTotalPatientPagesCountForFirstPdf()
        {
            int getCountTotalPatientPages;
            try
            {
                Driver.ScrollToEndOfThePage();
                string getTotalPatientPagesCount = Driver.FindElementBy(testonepdfCount).Text.ToString();
                getCountTotalPatientPages = Convert.ToInt32(getTotalPatientPagesCount.Trim());
                return getCountTotalPatientPages;
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to get total patient pages count : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }

        }
        public void ClickOnChangeAmountOrViewDetails(string amount)
        {
            try
            {
                string adjustmentAmount = amount.Split('$')[1].ToString().Trim();
                Driver.Click(changeAmountLnk);
                Driver.SleepTheThread(5);
                Driver.Click(creditCardRadio);
                Driver.SendKeys(paymentAmountTxt, adjustmentAmount);
                Driver.Click(continueBtn);
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to click on change amount and make onsite payment : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }

        }

        public void clickOnManageIssue()
        {
            try
            {
                Driver.Click(manageIssuesHyperLink);
                Driver.Click(addIssueBtn);
            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to click manage issue hyper link : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }

        public void ClickOnListOfIssues()
        {
            try
            {
                Driver.Click(listOfIssues);

            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to click list of issues hyper link : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }

        public void GoToActionList()
        {
            ROIMenuSelector selector = new ROIMenuSelector(Driver, logger, Context);
            try
            {
                selector.SelectRoiAdminMenuOptions("mnuROIFacilityUser", "ROI Requests", "Action List");
            }
            catch (Exception ex)
            {
                selector.Select("ROI Requests", "Action List");

                throw new Exception($"Failed to go to action list page : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }

        public void SelectActionMessage()
        {
            try
            {


                string actionMsg = IniHelper.ReadConfig("ROIAddLoggingUserColumnToFacilityActionListTest", "NoAuthorization");
                Driver.SendKeys(actionMessageDrp, actionMsg);
                Driver.Click(sendMessageToMROBtn);
            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to select action message from dropdown : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }
        public string VerifyActionMessage()
        {
            try
            {
                IWebElement frame = Driver.FindElementBy(actionMsgFrame);
                Driver.SwitchTo().Frame(frame);
                string message = Driver.GetText(messageText);
                Driver.Navigate().Refresh();
                return message;

            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to return action message : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }

        public void SelectAllUsers()
        {
            try
            {
                Driver.ClickAndCheckNextElement(UserMenuOption, ListAllUsersOption);
                Driver.DirectClick(ListAllUsersOption);
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to go to action list page : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }


        public void SwitchToDefaultWindow()
        {
            try
            {
                Driver.SwitchToDefaultContent();
            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to switch default window with details as: {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }

        public void ClickOnAddImagingDelivery()
        {
            try
            {
                Driver.Click(addImagingDeliveyBtn);
            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to click imaging delivery button with details as: {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }


        public void AddShippingDetails()
        {
            try
            {
                string todayDate = String.Format("{0:M/dd/yyyy}", DateTime.Now).Replace("-", "/");
                Driver.SendKeys(shipmentDate, todayDate);
                Driver.ClearText(itemCount);
                Driver.SendKeys(itemCount, "1");
                Driver.SendKeys(carrier, "Federal Express");
                Driver.SendKeys(specialInstructions, "none");
                Driver.Click(saveDeliveryInfoBtn);


            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to clcik save delivery infowith details as: {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }

        public bool VerifyShipmentType()
        {
            try
            {
                string imagingDelivery = "//a[@title='Edit Shipment']";
                WebElementHelper helper = new WebElementHelper(Driver, logger, Context);
                bool isDisplayed = helper.IsElementPresent(imagingDelivery);
                return isDisplayed;


            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to verify shipment type with details as: {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }

        public string VerifyShipmentStatus()
        {
            try
            {
                string shipmentStatus = Driver.FindElementBy(ShipmentStatus).Text;
                return shipmentStatus;
            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to verify shipment status with details as: {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }

        /// <summary>
        /// Import pdf files
        /// </summary>
        public void ImportPdfFilesForIntegrateDelivery()
        {
            try
            {
                Driver.ScrollToEndOfThePage();
                Driver.DirectClick(importRequestPDFButton);
                Driver.SwitchTo().Frame(importPDFFrame);
                //Driver.FindElementBy(selectFileButton).SendKeys(Path.GetFullPath(Path.Combine(Assembly.GetExecutingAssembly().Location, "..", "..", "..", "..", "MRO.ROI.Automation", "Files", importRequestPDFFile)));

                string fulldirectory = SeleniumHelper.ROITestFileUploads.LocationPath;
                string requesPagesFileName = $"\\{importRequestPDFFile}";
                string requestDocsPath = fulldirectory + requesPagesFileName;

                Driver.FindElementBy(selectFileButton).SendKeys(requestDocsPath);

                Driver.ClickAndCheckNextElement(importPDFButton, importCloseButton);
                Driver.DirectClick(importCloseButton);
                Driver.ScrollToEndOfThePage();
                Driver.DirectClick(importPatientPDFButton);
                Driver.SwitchTo().Frame(importPDFFrame);
                // Driver.FindElementBy(selectFileButton).SendKeys(Path.GetFullPath(Path.Combine(Assembly.GetExecutingAssembly().Location, "..", "..", "..", "..", "MRO.ROI.Automation", "Files", importPatientPDFFile)));

                string patientPagesFileName = $"\\{importRequestPDFFile1}";
                string patientDocsPath = fulldirectory + patientPagesFileName;

                Driver.FindElementBy(selectFileButton).SendKeys(patientDocsPath);

                Driver.ClickAndCheckNextElement(importPDFButton, importCloseButton);
                Driver.DirectClick(importCloseButton);
                WaitUntillFileStatusChange();
                logger.Log(Status.Pass, "PDF files imported");
            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to import pdf files : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }


        public void ImportTwoPatientPages()
        {
            try
            {
                Driver.ScrollIntoViewAndClick(By.XPath("//span[contains(text(),'Patient Pages')]/../..//input[@value='Import']"));
                Driver.SwitchTo().Frame(importPDFFrame);
                Driver.FindElementBy(selectFileButton).SendKeys(Path.GetFullPath(Path.Combine(Assembly.GetExecutingAssembly().Location, "..", "..", "..", "..", "MRO.ROI.Automation", "Files", twoPagePdf)));

                //string fulldirectory = SeleniumHelper.ROITestFileUploads.LocationPath;
                //string pdfFileName = $"\\{twoPagePdf}";
                //string requestDocsPath = fulldirectory + pdfFileName;
                //Driver.FindElementBy(selectFileButton).SendKeys(requestDocsPath);

                Driver.ClickAndCheckNextElement(importPDFButton, importCloseButton);
                Driver.DirectClick(importCloseButton);
                WaitUntillFileStatusChange();
                logger.Log(Status.Pass, $"Patient PDF file imported");

            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to import Patient PDF file : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }

        public void ImportPatientPagesForMultiPartEmailShipment()
        {
            try
            {
                Driver.DirectClick(importRequestPDFButton);
                Driver.SwitchTo().Frame(importPDFFrame);
                string fulldirectory = SeleniumHelper.ROITestFileUploads.LocationPath;
                string requesPagesFileName = $"\\{importRequestPDFFile}";
                string requestDocsPath = fulldirectory + requesPagesFileName;
                Driver.FindElementBy(selectFileButton).SendKeys(requestDocsPath);
                Driver.ClickAndCheckNextElement(importPDFButton, importCloseButton);
                Driver.DirectClick(importCloseButton);


                Driver.DirectClick(importPatientPDFButton);
                Driver.SwitchTo().Frame(importPDFFrame);
                //
                string patientPagesFileName2 = $"\\{importMROPDFFile}";
                string patientDocsPath2 = fulldirectory + patientPagesFileName2;
                Driver.FindElementBy(selectFileButton).SendKeys(patientDocsPath2);
                Driver.ClickAndCheckNextElement(importPDFButton, importCloseButton);
                Driver.DirectClick(importCloseButton);

                Driver.DirectClick(importPatientPDFButton);
                Driver.SwitchTo().Frame(importPDFFrame);
                //
                string patientPagesFileName3 = $"\\{importMROPDFFile1}";
                string patientDocsPath3 = fulldirectory + patientPagesFileName3;
                Driver.FindElementBy(selectFileButton).SendKeys(patientDocsPath3);
                Driver.ClickAndCheckNextElement(importPDFButton, importCloseButton);
                Driver.DirectClick(importCloseButton);

                Driver.DirectClick(importPatientPDFButton);
                Driver.SwitchTo().Frame(importPDFFrame);
                //
                string patientPagesFileName4 = $"\\{importPatientPDF}";
                string patientDocsPath4 = fulldirectory + patientPagesFileName4;
                Driver.FindElementBy(selectFileButton).SendKeys(patientDocsPath4);
                Driver.ClickAndCheckNextElement(importPDFButton, importCloseButton);
                Driver.DirectClick(importCloseButton);

                WaitUntillFileStatusChange();
                logger.Log(Status.Pass, $"Patient PDF file imported");
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to import Patient PDF file : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }

        }

        public bool VerifyActionMessageAtROITestFacility()
        {
            try
            {
                WebElementHelper helper = new WebElementHelper(Driver, logger, Context);
                string isActionMsg = "//option[contains(text(),'Please Process ASAP - RS Henry Ford')]";
                Driver.Click(actionMessageDrp);
                Driver.Wait(TimeSpan.FromSeconds(2));
                bool isActionMsgDisplayed = helper.IsElementPresent(isActionMsg);
                return isActionMsgDisplayed;

            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to select action message from dropdown : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }

        public bool VerifyActionMessageAtFacilitySide()
        {
            try
            {
                WebElementHelper helper = new WebElementHelper(Driver, logger, Context);
                string isActionMsg = "//option[contains(text(),'Please Process ASAP, requester needs records - RS Henry Ford')]";
                Driver.Click(actionMessageDrp);
                Driver.Wait(TimeSpan.FromSeconds(2));
                bool isActionMsgDisplayed = helper.IsElementPresent(isActionMsg);
                return isActionMsgDisplayed;

            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to select action message from dropdown : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }

        public void SelectDocumentsAndVerifyStatus()
        {
            bool isRemoveLinksAvailable = false;
            try
            {
                Driver.DirectClick(importRequestPDFButton);
                Driver.SwitchTo().Frame(importPDFFrame);
                string fulldirectory = SeleniumHelper.ROITestFileUploads.LocationPath;
                string requesPagesFileName = $"\\{twoPagePdf}";
                string requestDocsPath = fulldirectory + requesPagesFileName;
                Driver.FindElementBy(selectFileButton).SendKeys(requestDocsPath);
                Driver.ClickAndCheckNextElement(importPDFButton, importCloseButton);
                Driver.DirectClick(importCloseButton);
                Driver.DirectClick(importPatientPDFButton);
                Driver.SwitchTo().Frame(importPDFFrame);
                //
                string patientPagesFileName2 = $"\\{import800pagespdf}";
                string patientDocsPath2 = fulldirectory + patientPagesFileName2;

                string patientPagesFileName3 = $"\\{import400pagespdf}";
                string patientDocsPath3 = fulldirectory + patientPagesFileName3;

                string patientPagesFileName4 = $"\\{importtestpdf}";
                string patientDocsPath4 = fulldirectory + patientPagesFileName4;

                Driver.FindElementBy(selectFileButton).SendKeys(patientDocsPath2 + "\n " + patientDocsPath3 + "\n " + patientDocsPath4);

                WaitUntillFileUploadStatusChange();

                isRemoveLinksAvailable = VerifyRemoveLinksForPatientDocuments();
                if (isRemoveLinksAvailable)
                {
                    logger.Log(Status.Pass, "Verified the status color for each pdf file is being changed from yellow to green");
                    logger.Log(Status.Pass, "Verified that each file has a(Remove) hyper text after it");
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to verify the status of selected pdf files and remove hyperlinks : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }

        public bool VerifyRemoveLinksForPatientDocuments()
        {
            bool isRemoveHyperlinkDisplayed = false;
            try
            {

                List<IWebElement> removeHyperlinks = Driver.FindElementsBy(By.XPath("//ul[@id='mrocontent_rauFileUploadListContainer']//li//span[@class='ruButton ruRemove']"));
                if (removeHyperlinks.Count > 0)
                {
                    for (int i = 0; i < removeHyperlinks.Count; i++)
                    {
                        Driver.Wait(TimeSpan.FromSeconds(2));
                        if (removeHyperlinks[i].Text.Equals("Remove")) { isRemoveHyperlinkDisplayed = true; }
                    }
                }
            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to validate remove hyperlinks : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
            return isRemoveHyperlinkDisplayed;
        }

        public void WaitUntillFileUploadStatusChange()
        {
            IWebElement element = null;
            int retry = 1;
            while (retry <= 5 && element == null)
            {
                try
                {
                    element = Driver.FindElementBy(By.XPath("//ul[@id='mrocontent_rauFileUploadListContainer']//li//span[@class='ruButton ruRemove']"), 30);
                }
                catch (Exception ex)
                {
                    element = Driver.FindElementBy(By.XPath("//ul[@id='mrocontent_rauFileUploadListContainer']//li//span[@class='ruButton ruRemove']"), 30);
                    retry++;

                }
            }

            if (element == null)
            {
                throw new Exception($"Even after multiple attempts, patient pages status not updatedd to green status");
            }
        }

        public bool RemoveLinksForPatientDocuments()
        {
            bool isRemoveHyperlinkDeleted = false;
            try
            {

                List<IWebElement> removeHyperlinks = Driver.FindElementsBy(By.XPath("//ul[@id='mrocontent_rauFileUploadListContainer']//li//span[@class='ruButton ruRemove']"));
                if (removeHyperlinks.Count > 0)
                {
                    for (int i = 0; i < removeHyperlinks.Count; i++)
                    {
                        Driver.Wait(TimeSpan.FromSeconds(5));
                        removeHyperlinks[i].Click();
                        isRemoveHyperlinkDeleted = true;
                    }
                }
                Driver.DirectClick(cancelImportDocuments);
            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to remove hyperlinks for pdf files : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }

            return isRemoveHyperlinkDeleted;
        }

        public void SelectRequestDocumentsAndCancel()
        {

            try
            {
                Driver.DirectClick(importRequestPDFButton);
                Driver.SwitchTo().Frame(importPDFFrame);
                string fulldirectory = SeleniumHelper.ROITestFileUploads.LocationPath;
                string requesPagesFileName = $"\\{import800pagespdf}";
                string requestDocsPath = fulldirectory + requesPagesFileName;
                Driver.FindElementBy(selectFileButton).SendKeys(requestDocsPath);
                Driver.DirectClick(fileCancelButton);
                Driver.DirectClick(fileRemoveButton);
                Driver.DirectClick(cancelImportDocuments);

            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to cancel the pdf uploads {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }

        public int GetRowCountFromImportDocumentsTable()
        {
            string sRowCount = string.Empty;
            int rowCount = 0;

            try
            {
                sRowCount = Driver.GetText(By.XPath("//tr[@class='rgFooter']/td[1]"));
                if (sRowCount.Equals("1 row")) { rowCount = 1; }

            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to cancel the pdf uploads {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
            return rowCount;
        }

        public void UploadDocumentsAndVerifyStatus()
        {
            bool isRemoveLinksAvailable = false;//
            try
            {
                Driver.DirectClick(importRequestPDFButton);
                Driver.SwitchTo().Frame(importPDFFrame);
                string fulldirectory = SeleniumHelper.ROITestFileUploads.LocationPath;
                string requesPagesFileName = $"\\{twoPagePdf}";
                string requestDocsPath = fulldirectory + requesPagesFileName;
                Driver.FindElementBy(selectFileButton).SendKeys(requestDocsPath);
                Driver.ClickAndCheckNextElement(importPDFButton, importCloseButton);
                Driver.DirectClick(importCloseButton);
                Driver.DirectClick(importPatientPDFButton);
                Driver.SwitchTo().Frame(importPDFFrame);
                //
                string patientPagesFileName2 = $"\\{import800pagespdf}";
                string patientDocsPath2 = fulldirectory + patientPagesFileName2;

                string patientPagesFileName3 = $"\\{import400pagespdf}";
                string patientDocsPath3 = fulldirectory + patientPagesFileName3;

                string patientPagesFileName4 = $"\\{importtestpdf}";
                string patientDocsPath4 = fulldirectory + patientPagesFileName4;

                Driver.FindElementBy(selectFileButton).SendKeys(patientDocsPath2 + "\n " + patientDocsPath3 + "\n " + patientDocsPath4);

                if (isRemoveLinksAvailable)
                {
                    logger.Log(Status.Pass, "Verified the the status color for each pdf file is being changed from yellow to green");
                    logger.Log(Status.Pass, "Verified that each file has a(Remove) hyper text after it");
                }

                Driver.ClickAndCheckNextElement(importPDFButton, importCloseButton);
                Driver.DirectClick(importCloseButton);
                WaitUntillFileStatusChange();
                logger.Log(Status.Pass, $"Patient PDF file imported");
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to verify the status of selected pdf files: {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }

        public string UploadDuplicatePatientPagesAndGetErrorMessage()
        {
            string sErrorMessage = string.Empty;
            try
            {
                string fulldirectory = SeleniumHelper.ROITestFileUploads.LocationPath;
                Driver.DirectClick(importPatientPDFButton);
                Driver.SwitchTo().Frame(importPDFFrame);
                //
                string patientPagesFileName2 = $"\\{import800pagespdf}";
                string patientDocsPath2 = fulldirectory + patientPagesFileName2;
                string patientPagesFileName3 = $"\\{import400pagespdf}";
                string patientDocsPath3 = fulldirectory + patientPagesFileName3;
                string patientPagesFileName4 = $"\\{importtestpdf}";
                string patientDocsPath4 = fulldirectory + patientPagesFileName4;
                Driver.FindElementBy(selectFileButton).SendKeys(patientDocsPath2 + "\n " + patientDocsPath3 + "\n " + patientDocsPath4);
                Driver.ClickAndCheckNextElement(importPDFButton, importCloseButton);
                sErrorMessage = GetErrorMessageFromImportDocuments();
                Driver.DirectClick(importCloseButton);
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to verify the status of selected pdf files: {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
            return sErrorMessage;
        }

        public string GetErrorMessageFromImportDocuments()
        {
            string sMessage = string.Empty;
            try
            {
                sMessage = Driver.GetText(By.XPath("//table[@class='uploaded-files']//tbody//tr[1]//td[3]"));

            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to get error message from import documents windows: {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
            return sMessage;
        }

        //
        public void ImportDocumentsForLargeFaxDownload()
        {
            try
            {
                Driver.DirectClick(importRequestPDFButton);
                Driver.SwitchTo().Frame(importPDFFrame);
                string fulldirectory = SeleniumHelper.ROITestFileUploads.LocationPath;
                string requesPagesFileName = $"\\{twoPagePdf}";
                string requestDocsPath = fulldirectory + requesPagesFileName;
                Driver.FindElementBy(selectFileButton).SendKeys(requestDocsPath);
                Driver.ClickAndCheckNextElement(importPDFButton, importCloseButton);
                Driver.DirectClick(importCloseButton);
                Driver.DirectClick(importPatientPDFButton);
                Driver.SwitchTo().Frame(importPDFFrame);
                //
                string patientPagesFileName2 = $"\\{importAddCeritificationCompPDFFile}";
                string patientDocsPath2 = fulldirectory + patientPagesFileName2;

                string patientPagesFileName3 = $"\\{importAddNoRecordCompPDFFile}";
                string patientDocsPath3 = fulldirectory + patientPagesFileName3;

                string patientPagesFileName4 = $"\\{imporAddCorrespondenceCompPDFFile}";
                string patientDocsPath4 = fulldirectory + patientPagesFileName4;

                string patientPagesFileName5 = $"\\{importMROPDFFile}";
                string patientDocsPath5 = fulldirectory + patientPagesFileName5;

                Driver.FindElementBy(selectFileButton).SendKeys(patientDocsPath2 + "\n " + patientDocsPath3 + "\n " + patientDocsPath4 + "\n " + patientDocsPath5);

                Driver.ClickAndCheckNextElement(importPDFButton, importCloseButton);
                Driver.DirectClick(importCloseButton);
                WaitUntillFileStatusChange();
                logger.Log(Status.Pass, "PDF Files imported");
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to upload pdf files: {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }

        public void ImportPatientDocumentsForLargeFaxDownload()
        {
            try
            {
                string fulldirectory = SeleniumHelper.ROITestFileUploads.LocationPath;
                Driver.DirectClick(importPatientPDFButton);
                Driver.SwitchTo().Frame(importPDFFrame);
                //
                string patientPagesFileName2 = $"\\{importPatientPDFFile}";
                string patientDocsPath2 = fulldirectory + patientPagesFileName2;
                Driver.FindElementBy(selectFileButton).SendKeys(patientDocsPath2);
                Driver.ClickAndCheckNextElement(importPDFButton, importCloseButton);
                Driver.DirectClick(importCloseButton);
                WaitUntillFileStatusChange();
                logger.Log(Status.Pass, "Patient PDF Files Imported");
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to upload pdf files: {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }


        public string GetClaimId()
        {
            try
            {
                string claimNum = Driver.FindElementBy(claimId).GetAttribute("value");
                return claimNum;
            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to get claim id with details as: {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }

        public void ClickOnUpdatePtInfo()
        {
            try
            {
                Driver.Click(updatePtInfoBtn);
            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to click update info with details as: {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }

        public void ClickOnUpdateInfo()
        {
            try
            {
                Driver.Click(updateInfoButton);
            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to click update info with details as: {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }
        public void ClickOnCancelBtn()
        {
            try
            {
                Driver.Click(cancelBtn);
                Driver.Wait(TimeSpan.FromSeconds(2));
            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to click update info with details as: {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }

        public string GetReferenceId()
        {
            try
            {
                string referenceum = Driver.FindElementBy(referenceId).GetAttribute("value");
                Driver.ScrollToElement(referenceId);
                return referenceum;
            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to get reference  id with details as: {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }

        public void ImportDocumentsForLargeFaxDownloadByDeliveryOverride()
        {
            try
            {
                Driver.DirectClick(importRequestPDFButton);
                Driver.SwitchTo().Frame(importPDFFrame);
                string fulldirectory = SeleniumHelper.ROITestFileUploads.LocationPath;
                string requesPagesFileName = $"\\{twoPagePdf}";
                string requestDocsPath = fulldirectory + requesPagesFileName;
                Driver.FindElementBy(selectFileButton).SendKeys(requestDocsPath);
                Driver.ClickAndCheckNextElement(importPDFButton, importCloseButton);
                Driver.DirectClick(importCloseButton);
                Driver.DirectClick(importPatientPDFButton);
                Driver.SwitchTo().Frame(importPDFFrame);
                //
                string patientPagesFileName2 = $"\\{importMROPDFFile}";
                string patientDocsPath2 = fulldirectory + patientPagesFileName2;

                string patientPagesFileName3 = $"\\{importRequestPDFFile1}";
                string patientDocsPath3 = fulldirectory + patientPagesFileName3;

                string patientPagesFileName4 = $"\\{importPatientPDFFile1}";
                string patientDocsPath4 = fulldirectory + patientPagesFileName4;

                Driver.FindElementBy(selectFileButton).SendKeys(patientDocsPath2 + "\n " + patientDocsPath3 + "\n " + patientDocsPath4);
                Driver.ClickAndCheckNextElement(importPDFButton, importCloseButton);
                Driver.DirectClick(importCloseButton);
                WaitUntillFileStatusChange();
                logger.Log(Status.Pass, "PDF Files imported");
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to upload pdf files: {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }

        public void ImportPatientDocumentsForLargeFaxDownloadByDeliveryOverride()
        {
            try
            {
                Driver.DirectClick(importRequestPDFButton);
                Driver.SwitchTo().Frame(importPDFFrame);
                string fulldirectory = SeleniumHelper.ROITestFileUploads.LocationPath;
                string requesPagesFileName = $"\\{twoPagePdf}";
                string requestDocsPath = fulldirectory + requesPagesFileName;
                Driver.FindElementBy(selectFileButton).SendKeys(requestDocsPath);
                Driver.ClickAndCheckNextElement(importPDFButton, importCloseButton);
                Driver.DirectClick(importCloseButton);
                Driver.DirectClick(importPatientPDFButton);
                Driver.SwitchTo().Frame(importPDFFrame);
                //
                string patientPagesFileName2 = $"\\{importMROPDFFile}";
                string patientDocsPath2 = fulldirectory + patientPagesFileName2;


                Driver.FindElementBy(selectFileButton).SendKeys(patientDocsPath2);
                Driver.ClickAndCheckNextElement(importPDFButton, importCloseButton);
                Driver.DirectClick(importCloseButton);
                WaitUntillFileStatusChange();
                logger.Log(Status.Pass, "PDF Files imported");
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to upload pdf files: {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }

        public void ImportMultiplePdfFiles()
        {
            try
            {
                Driver.DirectClick(importRequestPDFButton);
                Driver.SwitchTo().Frame(importPDFFrame);
                string fulldirectory = SeleniumHelper.ROITestFileUploads.LocationPath;
                string requesPagesFileName = $"\\{twoPagePdf}";
                string requestDocsPath = fulldirectory + requesPagesFileName;
                Driver.FindElementBy(selectFileButton).SendKeys(requestDocsPath);
                Driver.ClickAndCheckNextElement(importPDFButton, importCloseButton);
                Driver.DirectClick(importCloseButton);
                Driver.DirectClick(importPatientPDFButton);
                Driver.SwitchTo().Frame(importPDFFrame);
                //
                string patientPagesFileName2 = $"\\{originalPdf}";
                string patientDocsPath2 = fulldirectory + patientPagesFileName2;

                string patientPagesFileName3 = $"\\{duplicatePdf}";
                string patientDocsPath3 = fulldirectory + patientPagesFileName3;

                string patientPagesFileName4 = $"\\{import800pagespdf}";
                string patientDocsPath4 = fulldirectory + patientPagesFileName4;

                Driver.FindElementBy(selectFileButton).SendKeys(patientDocsPath2 + "\n " + patientDocsPath3 + "\n " + patientDocsPath4);
                Driver.ClickAndCheckNextElement(importPDFButton, importCloseButton);
                Driver.DirectClick(importCloseButton);
                WaitUntillFileStatusChange();
                logger.Log(Status.Pass, "PDF Files imported");

            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to import pdf files : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }

        }


        public int ClickOnMaginifierIconForSecondPdf()
        {
            int patientDocCount = 0;
            try
            {

                Driver.Click(By.XPath("//*[@id='mrocontent_custPDFImportCtl_RadGridImport_ctl00_ctl08_btnView']/i"));
                Driver.SwitchToWindow("MRO Viewer");
                Driver.Manage().Window.Maximize();
                IWebElement patientDocsElement = Driver.FindElementBy(verifyPatientDocuments, 15);
                if (patientDocsElement != null)
                {
                    patientDocCount = Convert.ToInt32(patientDocsElement.Text.Trim());
                }

                Driver.Wait(TimeSpan.FromSeconds(1));
                Driver.SwitchToWindowAndClose("MRO Viewer");
                Driver.SleepTheThread(3);
                Driver.SwitchToWindow("ROI Log: Request Status");

            }
            catch (Exception ex)
            {

                throw;
            }
            return patientDocCount;
        }

        public int ClickOnMaginifierIconForThirdPdf()
        {
            int patientDocCount = 0;
            try
            {

                Driver.Click(By.XPath("//*[@id='mrocontent_custPDFImportCtl_RadGridImport_ctl00_ctl10_btnView']/i"));
                Driver.SwitchToWindow("MRO Viewer");
                Driver.Manage().Window.Maximize();
                IWebElement patientDocsElement = Driver.FindElementBy(verifyPatientDocuments, 15);
                if (patientDocsElement != null)
                {
                    patientDocCount = Convert.ToInt32(patientDocsElement.Text.Trim());
                }

                Driver.Wait(TimeSpan.FromSeconds(1));
                Driver.SwitchToWindowAndClose("MRO Viewer");
                Driver.SleepTheThread(3);
                Driver.SwitchToWindow("ROI Log: Request Status");

            }
            catch (Exception ex)
            {

                throw;
            }
            return patientDocCount;
        }

        public int GetTotalPatientPagesCountForSecondPdf()
        {
            int getCountTotalPatientPages;
            try
            {
                Driver.ScrollToEndOfThePage();
                string getTotalPatientPagesCount = Driver.FindElementBy(test2pdfCount).Text.ToString();
                getCountTotalPatientPages = Convert.ToInt32(getTotalPatientPagesCount.Trim());
                return getCountTotalPatientPages;
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to get total patient pages count : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }

        }

        public int GetTotalPatientPagesCountForThirdPdf()
        {
            int getCountTotalPatientPages;
            try
            {
                Driver.ScrollToEndOfThePage();
                string getTotalPatientPagesCount = Driver.FindElementBy(test3pdfCount).Text.ToString();
                getCountTotalPatientPages = Convert.ToInt32(getTotalPatientPagesCount.Trim());
                return getCountTotalPatientPages;
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to get total patient pages count : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }

        }


        public void ImportDocumentsForFacilityDeliveryReport()
        {
            try
            {
                Driver.DirectClick(importRequestPDFButton);
                Driver.SwitchTo().Frame(importPDFFrame);
                string fulldirectory = SeleniumHelper.ROITestFileUploads.LocationPath;
                string requesPagesFileName = $"\\{twoPagePdf}";
                string requestDocsPath = fulldirectory + requesPagesFileName;
                string requesPagesFileName1 = $"\\{panPDF}";
                string requestDocsPath2 = fulldirectory + requesPagesFileName1;
                Driver.FindElementBy(selectFileButton).SendKeys(requestDocsPath + "\n " + requestDocsPath2);
                Driver.ClickAndCheckNextElement(importPDFButton, importCloseButton);
                Driver.DirectClick(importCloseButton);
                Driver.DirectClick(importPatientPDFButton);
                Driver.SwitchTo().Frame(importPDFFrame);
                //
                string patientPagesFileName2 = $"\\{importMROPDFFile}";
                string patientDocsPath2 = fulldirectory + patientPagesFileName2;

                Driver.FindElementBy(selectFileButton).SendKeys(patientDocsPath2);
                Driver.ClickAndCheckNextElement(importPDFButton, importCloseButton);
                Driver.DirectClick(importCloseButton);
                WaitUntillFileStatusChange();
                logger.Log(Status.Pass, "PDF Files imported");
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to upload pdf files: {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }
        public void ImportDocumentsForFacility()
        {
            try
            {
                Driver.DirectClick(importRequestPDFButton);
                Driver.SwitchTo().Frame(importPDFFrame);
                string fulldirectory = SeleniumHelper.ROITestFileUploads.LocationPath;
                string requesPagesFileName = $"\\{twoPagePdf}";
                string requestDocsPath = fulldirectory + requesPagesFileName;
                Driver.FindElementBy(selectFileButton).SendKeys(requestDocsPath);
                Driver.ClickAndCheckNextElement(importPDFButton, importCloseButton);
                Driver.DirectClick(importCloseButton);
                Driver.DirectClick(importPatientPDFButton);
                Driver.SwitchTo().Frame(importPDFFrame);
                //
                string patientPagesFileName2 = $"\\{import800pagespdf}";
                string patientDocsPath2 = fulldirectory + patientPagesFileName2;
                Driver.FindElementBy(selectFileButton).SendKeys(patientDocsPath2);
                Driver.ClickAndCheckNextElement(importPDFButton, importCloseButton);
                Driver.DirectClick(importCloseButton);
                WaitUntillFileStatusChange();
                Driver.Navigate().Refresh();
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to import pdf files: {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }

        public string GetDOBForPatient()
        {
            try
            {
                string sDob = Driver.GetText(dobElement);
                return sDob;
            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to return date of birth,Exception details as : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }


        public void ImportTwentyPatientPages()
        {
            try
            {
                Driver.DirectClick(importRequestPDFButton);
                Driver.SwitchTo().Frame(importPDFFrame);
                string fulldirectory = SeleniumHelper.ROITestFileUploads.LocationPath;
                string requesPagesFileName = $"\\{twoPagePdf}";
                string requestDocsPath = fulldirectory + requesPagesFileName;
                Driver.FindElementBy(selectFileButton).SendKeys(requestDocsPath);
                Driver.ClickAndCheckNextElement(importPDFButton, importCloseButton);
                Driver.DirectClick(importCloseButton);
                Driver.DirectClick(importPatientPDFButton);
                Driver.SwitchTo().Frame(importPDFFrame);
                //
                string patientPagesFileName2 = $"\\{twentyPagespdf}";
                string patientDocsPath2 = fulldirectory + patientPagesFileName2;
                Driver.FindElementBy(selectFileButton).SendKeys(patientDocsPath2);
                WaitUntillFileStatusChange();
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to import pdf files: {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }
        public string Getlinkedrequestid()
        {
            try
            {
                string linkedReqId = Driver.GetText(LinkedReqId);

                return linkedReqId;
            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to get linked requestId with deatails as Message : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }

        }

        public void ClickOnViewDocuments()
        {
            try
            {
                Driver.Click(viewDocumentButton);
                WaitUntillPageLoaded();
                Driver.SwitchToWindow("MRO Viewer");
                Driver.Manage().Window.Maximize();
            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to click view documents button with details as: {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }
        public void ClickOnRequestDocuments()
        {
            try
            {

                Driver.Click(requestDocuments);


            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to click request document button with details as: {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }

        public void ClickOnPatientDocuments()
        {
            try
            {
                Driver.Click(patientDocuments);
                Driver.Wait(TimeSpan.FromSeconds(2));

            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to click request document button with details as: {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }

        public bool VerifyPageNumberTabs()
        {
            bool isDisplayed = false;

            try
            {

                WebElementHelper helper = new WebElementHelper(Driver, logger, Context);

                string tab100 = "//button[contains(text(),'1 - 100')]";
                string tab200 = "//button[contains(text(),'101 - 200')]";
                string tab300 = "//button[contains(text(),'201 - 300')]";
                string tab400 = "//button[contains(text(),'3011 - 400')]";
                string tab500 = "//button[contains(text(),'401 - 500')]";
                string tab600 = "//button[contains(text(),'501 - 600')]";
                bool tab1 = helper.IsElementPresent(tab100);
                bool tab2 = helper.IsElementPresent(tab100);
                bool tab3 = helper.IsElementPresent(tab100);
                bool tab4 = helper.IsElementPresent(tab100);
                bool tab5 = helper.IsElementPresent(tab100);
                bool tab6 = helper.IsElementPresent(tab100);

                if ((tab1 == true) && (tab2 == true) && (tab3 == true) && (tab4 == true) && (tab5 == true) && (tab6 == true))
                {
                    isDisplayed = true;
                }


            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to  verify pages number tabs with details as: {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
            return isDisplayed;
        }

        public void ClickDeliveryReport()
        {
            try
            {
                Driver.Click(report);
                Driver.Click(deliveryReport);
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to click Delivery Report Message : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }


        public void ImportDocumentsForRequestPages()
        {
            try
            {
                Driver.DirectClick(importRequestPDFButton);
                Driver.SwitchTo().Frame(importPDFFrame);
                string fulldirectory = SeleniumHelper.ROITestFileUploads.LocationPath;
                string requesPagesFileName = $"\\{twoPagePdf}";
                string requestDocsPath = fulldirectory + requesPagesFileName;
                string requesPagesFileName1 = $"\\{panPDF}";
                string requestDocsPath2 = fulldirectory + requesPagesFileName1;
                Driver.FindElementBy(selectFileButton).SendKeys(requestDocsPath + "\n " + requestDocsPath2);
                Driver.ClickAndCheckNextElement(importPDFButton, importCloseButton);
                Driver.DirectClick(importCloseButton);
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to upload pdf files: {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }

        public void ClickCloseRequest()
        {
            try
            {
                Driver.DirectClick(closeRequestBtn);
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to click the close request button Message : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }

        public ROIAdminFacilityListPage ImportFilesForSpecificPdfs()
        {
            try
            {
                Driver.ScrollToEndOfThePage();
                Driver.DirectClick(importRequestPDFButton);
                Driver.SwitchTo().Frame(importPDFFrame);
                //Driver.FindElementBy(selectFileButton).SendKeys(Path.GetFullPath(Path.Combine(Assembly.GetExecutingAssembly().Location, "..", "..", "..", "..", "MRO.ROI.Automation", "Files", importRequestPDFFile)));

                string fulldirectory = SeleniumHelper.ROITestFileUploads.LocationPath;
                string requesPagesFileName = $"\\{importRequestPDFFile}";
                string requestDocsPath = fulldirectory + requesPagesFileName;

                Driver.FindElementBy(selectFileButton).SendKeys(requestDocsPath);

                Driver.ClickAndCheckNextElement(importPDFButton, importCloseButton);
                Driver.DirectClick(importCloseButton);
                Driver.ScrollToEndOfThePage();
                Driver.DirectClick(importPatientPDFButton);
                Driver.SwitchTo().Frame(importPDFFrame);
                // Driver.FindElementBy(selectFileButton).SendKeys(Path.GetFullPath(Path.Combine(Assembly.GetExecutingAssembly().Location, "..", "..", "..", "..", "MRO.ROI.Automation", "Files", importPatientPDFFile)));

                string patientPagesFileName = $"\\{importCarilion}";
                string patientDocsPath = fulldirectory + patientPagesFileName;

                string patientPagesFileName4 = $"\\{import800pagespdf}";
                string patientDocsPath4 = fulldirectory + patientPagesFileName4;

                Driver.FindElementBy(selectFileButton).SendKeys(patientDocsPath + "\n " + patientDocsPath4);

                //Driver.FindElementBy(selectFileButton).SendKeys(patientDocsPath);

                Driver.ClickAndCheckNextElement(importPDFButton, importCloseButton);
                Driver.DirectClick(importCloseButton);
                WaitUntillFileStatusChange();
                logger.Log(Status.Pass, "PDF files imported");
            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to import pdf files : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
            return new ROIAdminFacilityListPage(Driver, logger, Context);
        }

        public void ImportPDFFileForAddComponent()
        {
            try
            {
                Driver.ScrollToEndOfThePage();
                Driver.DirectClick(importMROTestPDFButtonTest1);
                Driver.SwitchTo().Frame(importPDFFrame);
                //Driver.FindElementBy(selectFileButton).SendKeys(Path.GetFullPath(Path.Combine(Assembly.GetExecutingAssembly().Location, "..", "..", "..", "..", "MRO.ROI.Automation", "Files", importMROPDFFile)));

                string fulldirectory = SeleniumHelper.ROITestFileUploads.LocationPath;
                string pdfFileName = $"\\{test1200pdf}";
                string requestDocsPath = fulldirectory + pdfFileName;
                Driver.FindElementBy(selectFileButton).SendKeys(requestDocsPath);

                Driver.ClickAndCheckNextElement(importPDFButton, importCloseButton);
                Driver.DirectClick(importCloseButton);
                WaitUntillFileStatusChange();
                logger.Log(Status.Pass, $"PDF files imported");

            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to import test pdf files : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }

        }


        public void ImportPDFFileForAddComponentWithTwoPdfs()
        {
            try
            {
                Driver.ScrollToEndOfThePage();
                Driver.DirectClick(importMROTestPDFButtonTest2);
                Driver.SwitchTo().Frame(importPDFFrame);
                //Driver.FindElementBy(selectFileButton).SendKeys(Path.GetFullPath(Path.Combine(Assembly.GetExecutingAssembly().Location, "..", "..", "..", "..", "MRO.ROI.Automation", "Files", importMROPDFFile)));

                string fulldirectory = SeleniumHelper.ROITestFileUploads.LocationPath;
                string pdfFileName = $"\\{photoPdf}";
                string requestDocsPath = fulldirectory + pdfFileName;

                string pdfFileName1 = $"\\{larsentif}";
                string requestDocsPath1 = fulldirectory + pdfFileName1;
                // Driver.FindElementBy(selectFileButton).SendKeys(requestDocsPath);

                Driver.FindElementBy(selectFileButton).SendKeys(requestDocsPath + "\n " + requestDocsPath1);

                Driver.ClickAndCheckNextElement(importPDFButton, importCloseButton);
                Driver.DirectClick(importCloseButton);
                WaitUntillFileStatusChange();
                logger.Log(Status.Pass, $"PDF files imported");

            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to import test pdf files : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }

        }
        public void ImportDocumentsForDuplicateRequest()
        {
            try
            {
                Driver.DirectClick(importRequestPDFButton);
                Driver.SwitchTo().Frame(importPDFFrame);
                string fulldirectory = SeleniumHelper.ROITestFileUploads.LocationPath;
                string requesPagesFileName = $"\\{twoPagePdf}";
                string requestDocsPath = fulldirectory + requesPagesFileName;
                Driver.FindElementBy(selectFileButton).SendKeys(requestDocsPath);
                Driver.ClickAndCheckNextElement(importPDFButton, importCloseButton);
                Driver.DirectClick(importCloseButton);
                Driver.DirectClick(importPatientPDFButton);
                Driver.SwitchTo().Frame(importPDFFrame);
                //
                string patientPagesFileName4 = $"\\{importMROPDFFile}";
                string patientDocsPath4 = fulldirectory + patientPagesFileName4;
                Driver.FindElementBy(selectFileButton).SendKeys(patientDocsPath4);
                Driver.ClickAndCheckNextElement(importPDFButton, importCloseButton);
                Driver.DirectClick(importCloseButton);
                WaitUntillFileStatusChange();
                logger.Log(Status.Pass, "PDF Files imported");
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to import pdf files: {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }


        public void ClickReplyBtn()
        {
            try
            {
                IWebElement frame = Driver.FindElementBy(actionMsgFrame);
                Driver.SwitchTo().Frame(frame);
                Driver.Click(replyBtn);
            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to click reply button with details as  : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }

        }

        public bool VerifyMsgToFacilty()
        {
            try
            {
                string msgToFacility = "//textarea[@id='mrocontent_txtReply']";
                WebElementHelper helper = new WebElementHelper(Driver, logger, Context);
                bool isDisplayed = helper.IsElementPresent(msgToFacility);
                return isDisplayed;
            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to verify reply to message with details as  : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }

        }

        public void ClickSendReply(string type)
        {
            try
            {

                Driver.SendKeys(replyToMsgTxtBox, type);
                Driver.Click(sendRplyBtn);
            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to click send reply details as  : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }

        public void ReqPreAuthorization()
        {
            try
            {
                Driver.Click(reqPreauthorization);

            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to click Delivery Report Message : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }
        public void ImportMultiplePatientPdfFiles()
        {
            try
            {
                Driver.DirectClick(importRequestPDFButton);
                Driver.SwitchTo().Frame(importPDFFrame);
                string fulldirectory = SeleniumHelper.ROITestFileUploads.LocationPath;
                string requesPagesFileName = $"\\{twoPagePdf}";
                string requestDocsPath = fulldirectory + requesPagesFileName;
                Driver.FindElementBy(selectFileButton).SendKeys(requestDocsPath);
                Driver.ClickAndCheckNextElement(importPDFButton, importCloseButton);
                Driver.DirectClick(importCloseButton);
                Driver.DirectClick(importPatientPDFButton);
                Driver.SwitchTo().Frame(importPDFFrame);
                string patientPagesFileName2 = $"\\{importUnityPoint2}";
                string patientDocsPath2 = fulldirectory + patientPagesFileName2;
                string patientPagesFileName3 = $"\\{importCarilion}";
                string patientDocsPath3 = fulldirectory + patientPagesFileName3;



                string patientPagesFileName4 = $"\\{TwoHundredPDF}";
                string patientDocsPath4 = fulldirectory + patientPagesFileName4;

                Driver.FindElementBy(selectFileButton).SendKeys(patientDocsPath2 + "\n " + patientDocsPath3 + "\n " + patientDocsPath4);
                Driver.SleepTheThread(5);
                Driver.ClickAndCheckNextElement(importPDFButton, importCloseButton);
                IWebElement elements = Driver.FindElementBy(importPDFButton);
                if (elements.Enabled)
                {
                    elements.Click();
                }
                Driver.DirectClick(importCloseButton);
                WaitUntillFileStatusChange();
                logger.Log(Status.Pass, "PDF Files imported");

            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to import pdf files : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }

        }

        public bool VerifySSNNumber()
        {
            bool isDisplayed = false;
            try
            {

                Driver.HighlightingWebElement(ssnValue);
                IWebElement ssn = Driver.FindElementBy(ssnValue);
                if (ssn.Displayed == true)
                {
                    isDisplayed = true;
                }
                else
                {
                    isDisplayed = false;

                }
                return isDisplayed;
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to verify ssn number with details as : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }

        public void ClickOnMagnifierIconForSSN()
        {

            try
            {

                Driver.Click(viewPatientSSN);
                Driver.Wait(TimeSpan.FromSeconds(2));
                Driver.SwitchTo().Alert().Accept();
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to click magnifier icon for ssn with details as : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }
        public bool VerifyWaitingPrepaycheckbox()
        {
            bool isSelected = false;
            Driver.ScrollToElement(waitingPrepay);
            Driver.HighlightingWebElement(waitingPrepay);
            //Driver.Wait(TimeSpan.FromSeconds(10));
            try
            {
                if (Driver.FindElementBy(waitingPrepay).Selected == true)
                {
                    isSelected = true;
                }
                Driver.Wait(TimeSpan.FromSeconds(20));

            }
            catch (Exception ex)
            {



                throw new Exception($"Failed to verify waiting prepay check box with Message : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
            return isSelected;
        }

        public void AssignRequester()
        {



            try
            {



                Driver.Click(assignRequester);
                Driver.Wait(TimeSpan.FromSeconds(2));

            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to click on assign requester button : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }

        public void TestAttorney()
        {

            try
            {

                Driver.Click(testAttorney);
                Driver.Wait(TimeSpan.FromSeconds(2));

            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to click magnifier icon for ssn with details as : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }

        public void HideLicenseRequireLabelIfShowing()
        {
            bool isDisplay = Driver.isElementDisplayed(licenseRequireLable);
            if (isDisplay)
            {
                Driver.HideElement(licenseRequireLable);
            }

        }

        public void ScanPatientPage()
        {
            Driver.WaitInSeconds(5);
            Driver.ScrollIntoViewSmoothly(scanPatientBtn);
            logger.Log(Status.Info, "Click on Scan button Patient Pages row");
            Driver.Click(scanPatientBtn);
            Iframe frame = new Iframe(Driver, logger, Context);
            try
            {
                Driver.WaitInSeconds(3);
                logger.Log(Status.Info, "Switch to Patient popup");
                frame.SwitchToFrameByLocator(patientScanIframe);
            }
            catch (Exception ex )
            {
                Driver.DirectClick(scanPatientBtn);
            }
            frame.SwitchToFrameByLocator(patientScanIframe);

            logger.Log(Status.Info, "Click on scan button");
            Driver.Click(scanBtnInPatientIframe);
            Driver.WaitInSeconds(1);
            logger.Log(Status.Info, "Click on scan button");
            Driver.Click(scanBtnInPatientIframe);
            Driver.WaitInSeconds(1);
            logger.Log(Status.Info, "Click on accept button");
            Driver.Click(acceptBtnInPatientIframe);
            Driver.WaitInSeconds(1);
            frame.SwitchToRoiFrame();

        }

        public bool VerifyStatusForScannedDocument()
        {
            WaitUntillFileStatusChange();
            bool isStatus = false;
            try
            {
                isStatus = Driver.isElementDisplayed(scannedDocumentStatus);
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to validate status for scanned document is uploaded : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
            return isStatus;
        }

        /// <summary>
        /// Verify Status Under ImportDocument
        /// </summary>
        public bool VerifyStatusUnderImportDocument(int index)
        {
            WaitUntillFileStatusChange();
            bool isStatus = false;
            try
            {
                By documentStatus = By.XPath($"(//tr[starts-with(@id,'mrocontent_custPDFImportCtl_RadGridImport')]//td[6])[{index}]");
                string statusOfImportedDocs = Driver.GetText(documentStatus).ToString();
                if (statusOfImportedDocs == "Uploaded")
                {
                    isStatus = true;
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to validate status under import document is uploaded : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
            return isStatus;
        }

        /// <summary>
        /// Import pdf files
        /// </summary>
        public ROIAdminFacilityListPage ImportPdfFiles(string fileName, string patientfileName)
        {
            try
            {

                Driver.ScrollToEndOfThePage();
                Driver.ScrollIntoViewSmoothly(importRequestPDFButton);
                Driver.DirectClick(importRequestPDFButton);
                Driver.SwitchTo().Frame(importPDFFrame);
                //Driver.FindElementBy(selectFileButton).SendKeys(Path.GetFullPath(Path.Combine(Assembly.GetExecutingAssembly().Location, "..", "..", "..", "..", "MRO.ROI.Automation", "Files", importRequestPDFFile)));

                string fulldirectory = SeleniumHelper.ROITestFileUploads.LocationPath;
                string requesPagesFileName = $"\\{fileName}";
                string requestDocsPath = fulldirectory + requesPagesFileName;

                Driver.FindElementBy(selectFileButton).SendKeys(requestDocsPath);

                Driver.ClickAndCheckNextElement(importPDFButton, importCloseButton);
                Driver.DirectClick(importCloseButton);
                Driver.SwitchToDefaultContent();
                Driver.ScrollToEndOfThePage();
                
                Driver.SleepTheThread(10);
                Automation.Common.Iframe frame = new Automation.Common.Iframe(Driver, logger, Context);
                frame.SwitchToRoiFrame();
                WaitUntillFileStatusChange();
                logger.Log(Status.Pass, "PDF files imported");
            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to import pdf files : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
            return new ROIAdminFacilityListPage(Driver, logger, Context);
        }


    }
}