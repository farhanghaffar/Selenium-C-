using AventStack.ExtentReports;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MRO.ROI.Automation.Selenium;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using static MRO.ROI.Automation.Utility.IniFile;

namespace MRO.ROI.Automation.Pages
{
    public  class ROIAdminRequestStatusPage
    {
        public RemoteWebDriver Driver;
        public ExtentTest logger;
        public TestContext Context;
        public ROIAdminRequestStatusPage(RemoteWebDriver driver, ExtentTest _loger, TestContext _context)
        {
            Driver = driver;
            logger = _loger;
            Context = _context;
        }

        public const string CDSelection = "CD";
        public const string applyRatelink = "mrocontent_lnkRate";
        public const string ftpSelection = "FTP";
        public By assignRequesterButton = By.Id("mrocontent_btnAssignRequester");
        public By applyRateHyperlink = By.XPath("//a[@id='mrocontent_lnkRate']");
        public By DeliveryOverrideDropdown = By.XPath("//select[@id='mrocontent_lstShipmentMethod']");
        public By ftpOptionSelection = By.XPath("//a[contains(text(),'FTP')]");       
        public By deliveryOverrideSaveButton = By.XPath("//input[@id='mrocontent_btnSaveShipOverride']");
        public By addHyperlink = By.XPath("//*[@id='mrocontent_lnkAddShipment']");
        public By qcPassButton = By.Id("mrocontent_cmdPassQC");
        public By applyRateButton = By.Id("mrocontent_lnkRate");
        public By rateButton = By.XPath("//*[@id='mrocontent_cmdApplyRate']");
        public By createInvoiceButton = By.Id("mrocontent_cmdCreateInvoice");
        public By confirmDialogYesButton = By.XPath("//*[@aria-describedby='dialog-confirm']//*[text()='Yes']");
        public By invoicename = By.XPath("//*[@id=\"mrocontent_tblFaxPackages\"]//td[text()='Invoice']");
        public By requestHistoryButton = By.XPath("//input[@id='mrocontent_cmdEventHistory']");
        public By addissueButton = By.XPath("//input[@id='mrocontent_cmdAddIssue']");
        public By Close = By.XPath("//input[starts-with(@id,'mrocontent_lnkIssueClosed')]");
        public By CDOptionSelection = By.XPath("//a[contains(text(),'CD')]");
        public By adjustedBalanceAmount = By.XPath("//td[@id='mrocontent_tdAdjustedBalance']");
        public By verifyInvoice = By.XPath("//table[@id='mrocontent_tblFaxPackages']//tr[3]//td[contains(text(),'Invoice')]");
        public By ledgerDetailButton = By.XPath("//input[@id='mrocontent_cmdLedgerDetail']");
        public By viewActionlink = By.Id("mrocontent_btnActions");
        public By requestLink = By.XPath("//*[@id='MasterHeaderText']/a");
        public By backToRequestBtn = By.Id("mrocontent_cmdRequest");
        public By typeElement = By.XPath("//*[@id='mrocontent_dgActions']/tbody/tr[2]/td[4]");
        public By notesElement = By.XPath("//*[@id='mrocontent_dgActions']/tbody/tr[2]/td[5]");
        public By closeCheckbox = By.XPath("//input[@title='Close this action and go to Request Status Screen']");
        public const string Type = "Location Updated";
        public const string notes = "Possible Impact to Assigned Rate. Please Review and Adjust if Needed";
        public By updateInfoBtn = By.Id("mrocontent_cmdUpdatePatient");
        public By locationDrp = By.XPath("//div[@id='mrocontent_lstLocation']//input[@id='mrocontent_lstLocation_Input']");
        public const string locationVal = "Boston Proper";
        public By locationDropdown = By.XPath("//div[@id='mrocontent_lstLocation_DropDown']//ul/li");
        public By updateBtn = By.Id("mrocontent_cmdUpdate");
        public By adjustedBal = By.XPath("//td[@id='mrocontent_tdAdjustedBalance']");
        public const string capeLocationVal = "123Cape May";
        public By addShippmentDropdown = By.Id("mrocontent_lstShipmentType");
        public const string emailSelection = "EMAIL";
        public By shippableDate = By.XPath("//table[@id='mrocontent_tblShipments']/tbody/tr[5]/td[2]");
        public By secondShipmentLink = By.XPath("//tr[@id='mrocontent_trCarrier']/../tr[6]/td/a[contains(text(),'EMAIL')]");
        public By invoiceIdElement = By.XPath("//a[@title='View package']");
        public By mailLink = By.XPath("//a[contains(text(),'EMAIL')]");
        public By emailShippedDate = By.XPath("//table[@id='mrocontent_tblShipments']/tbody/tr[5]/td[3]");
        public By requestStatusHeader = By.XPath("//td[@id='MasterHeaderText']");
        public By aetnaTestFaxlnk = By.XPath("//a[@id='mrocontent_lnkShipRequester']");
        public By panTxtOnAssignROIRequester = By.XPath("//td[text()='PAN:']//following-sibling::td[2]");
        public By reAssignRequesterBtn = By.XPath("//input[@id='mrocontent_btnReAssignRequester']");
        public By panOnRSS = By.XPath("//td[@id='mrocontent_tdPAN']");
        public By updateinfoBtn = By.XPath("//input[@id='mrocontent_cmdUpdatePatient']");
        public const string closeChkBox = "//input[@title='Close this action and go to Request Status Screen']";
        public By reAssignRequesterTxt = By.XPath("//a[@id='mrocontent_lnkRequester']");
        public By shipToTxt = By.XPath("//a[@id='mrocontent_lnkShipRequester']");
        public By rateAppliedTxt = By.XPath("//span[@id='mrocontent_lblRate']");
        public By emailHyperlink = By.XPath("//*[@id='mrocontent_tblShipments']/tbody/tr[5]/td[1]/a");
        public By LogCheck = By.XPath("//input[@id='mrocontent_cmdLogCheck']");
        public By L2AdjustableBalance = By.XPath("//span[@id='mrocontent_lblL2ARBalance']");
        public By addDropdwonElement = By.XPath("//select[@id='mrocontent_lstShipmentType']");
        public By TestFaciltyCBO = By.XPath("//a[@id='mrocontent_lnkRequester']");
        public By AJAssociates = By.XPath("//a[@id='mrocontent_lnkShipRequester']");
        public By RecordsSentByFacility = By.XPath("//td[@id='mrocontent_tdDocsScanned']");
        public By SendIssueButton = By.XPath("//input[@name='mrocontent$cmdCreateIssuesPackage']");
        public By YesCreateInvoice = By.XPath("//div[@class='ui-dialog-buttonset']//button[1]");
        public const string extUpload = "ExtUpload";
        public By extUploadHyperlink = By.XPath("//*[@id='mrocontent_tblShipments']/tbody/tr[5]/td[1]/a");
        public By lnkFacility = By.XPath("//a[@id='mrocontent_lnkFacility']");
        public By emailId = By.XPath("//td[@id='mrocontent_tdShipEmail']//a");
        public By roiadmin = By.XPath("//*[starts-with(text(),'ROIAdmin')]");
        public By auditLog = By.XPath("//*[contains(text(),'Audit Log')]");
        public By retrievalFee = By.XPath("//td[@id='mrocontent_tdRetrievalFee']");
        public By pageFee1 = By.XPath("//td[@id='mrocontent_tdPageFee1']");
        public By shippingFee = By.XPath("//td[@id='mrocontent_tdShipping']");
        public By salesTax = By.XPath("//td[@id='mrocontent_tdSalesTax']");
        public By innvoiceAmount = By.XPath("//td[@id='mrocontent_tdInvoiceAmount']");
        public By adjustments = By.XPath("//td[@id='mrocontent_tdAdjustments']");
        public By adjustedBalance = By.XPath("//td[@id='mrocontent_tdAdjustedBalance']");
        public By PayByCCBtn = By.XPath("//input[@id='mrocontent_cmdPayCreditCard']");
        public By manageIssuesBtn = By.XPath("//input[@id='mrocontent_cmdManageIssues']");
        public By commentsTxtbox = By.XPath("//textarea[@id='mrocontent_txtEditComments']");
        public By updateCommentsbtn = By.XPath("//input[@id='mrocontent_cmdUpdateComment']");
        public By doneBtn = By.XPath("//input[@id='mrocontent_cmdEditDone']");
        public By issueMagnifierIcon = By.XPath("//input[@title='View Shipment']");
        public By correspondenceBtn = By.XPath("//input[@id='mrocontent_cmdCorrespondence']");
        public By issueDropdown = By.XPath("//select[@id='mrocontent_lstIssues']");
        public By comments = By.XPath("//textarea[@id='mrocontent_txtAddComments']");
        public By addCoresspondenceBtn = By.XPath("//input[@id='mrocontent_cmdAdd']");
        public By correspondenceId = By.XPath("(//a[@title='View package'])[2]");
        public By correspondenceMagnifierIcon = By.XPath("(//input[@title='View Shipment'])[2]");
        public By closeBtn = By.XPath("//input[@value='Close']");
        public By invoiceId= By.XPath("//td[starts-with(text(),'Invoice')]");
        public By correspondenceHistoryElement = By.XPath("//td[contains(text(),'Correspondence History')]");
        public By selectAllPackagesButton = By.XPath("//input[@id='SelectAllCheckbox']");
        public By printSelectedDocuments = By.XPath("//button[@title ='Print selected documents']");
        public By purposeOfUseText = By.XPath("//td[@id='mrocontent_tdPurposeOfUse']");
        public const string mailSelection = "MAIL";
        public By requestStatus = By.XPath("//td[@id='mrocontent_tdRequestStatus']");
        public By updateInfoButton = By.XPath("//input[@id='mrocontent_cmdUpdateInfo']");//
        public By cancelledDate = By.XPath("//input[@id='mrocontent_txtRqrCancel']");
        public const string pdfSelection = "PDF";
        public By requestStatusVal = By.XPath("//td[@id='mrocontent_tdRequestStatus']");
        public By cancelBtn = By.XPath("//input[@id='mrocontent_cmdRqrCancel']");
        public By disabledPayByCC = By.XPath("//input[@id='mrocontent_cmdPayCreditCard' and @disabled='disabled']");
        public By AdjBalance = By.Id("mrocontent_lblL2ARBalance");
        public By SetNonBillableButton = By.XPath("//td[@id='mrocontent_tdRate']/input[2]");
        public By nextBtn = By.XPath("//input[@id='mrocontent_cmdFinishEvent']");
        public By roiAdmin = By.XPath("//td[contains(text(),'ROIAdmin')]");
        public By invoicingQueueElement = By.XPath("//td[contains(text(),'Invoicing Queue')]");
        public By ClearInvoiceButton = By.Id("mrocontent_cmdClearInvoice");
        public const string faxSelection = "FAX";
        public By faxNumTxtbox = By.XPath("//input[@id='mrocontent_txtFax']");
        public By saveBtn = By.XPath("//input[@id='mrocontent_cmdSave']");
        public By requesterValue = By.XPath("//a[@id='mrocontent_lnkRequester']");
        public By requesterupdateinfoBtn = By.XPath("//input[@id='mrocontent_cmdUpdateRequesterInfo']");
        public By findarequest = By.XPath("//td[contains(text(),'Find a Request')]");
        public By requesterType = By.XPath("//td[@id='mrocontent_tdRequestType']");
        public By addActionLink = By.XPath("//input[@id='mrocontent_btnActions']");

        public By emailStatus = By.XPath("//table[@id='mrocontent_tblFaxHistory']//tr[4]//td[a]");
        public By invoce_ID = By.XPath("//table[@id='mrocontent_tblFaxPackages']//tr[4]//td[contains(text(),'Invoice')]");

        public By rssRequestStatus = By.XPath("//td[contains(text(),'RSS Request Status')]");
        public By requestIdTxtbox = By.XPath("//label[contains(text(),'Request ID:')]");
        public By searchIdBtn = By.XPath("//button[@type='submit']");
        public const string eXTUpload = "ExtUpload";
        public By eXTUploadlink = By.XPath("//a[contains(text(),'EXTUPLOAD')]");
        public string AddEmailShipmentReturnShippableDate()
        {
            try
            {
                SelectElement oSelect = new SelectElement(Driver.FindElementBy(addShippmentDropdown));
                oSelect.SelectByText(emailSelection);
                Driver.Click(addHyperlink);
                return Driver.GetText(shippableDate);

            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to add shipment Message : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }

        public void CheckForAnyIssuesAndCloseTheIssue()
        {
            try
            {
                Driver.Wait(TimeSpan.FromSeconds(2));
                Driver.DirectClick(By.XPath("//input[@id='mrocontent_cbActions']"));
                System.Collections.Generic.List<IWebElement> closeButtons = Driver.FindElementsBy(By.XPath("//span[text()='Close']/preceding-sibling::input"));
                if(closeButtons!=null && closeButtons.Count>0)
                {
                    int count = closeButtons.Count - 1;
                    closeButtons[count].Click();
                }               
                Driver.SleepTheThread(4);
            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to close the issue Message : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }

        }

        public void GoToShipmentDetailsWindow()
        {
            try
            {
                Driver.DirectClick(mailLink);
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to click on email link Message : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }

        }

        public string GetEmailShippedDate()
        {
            string shippedDate = string.Empty;
            int retryCount = 0;
            try
            {
                shippedDate = Driver.GetText(By.XPath("//table[@id='mrocontent_tblShipments']/tbody/tr[5]/td[3]"));
                while((string.IsNullOrEmpty(shippedDate)) && retryCount<6)
                {
                    Driver.Navigate().Refresh();
                    shippedDate =Driver.GetText(By.XPath("//table[@id='mrocontent_tblShipments']/tbody/tr[5]/td[3]"));
                    Driver.SleepTheThread(2);
                    retryCount++;
                }

                if(!string.IsNullOrEmpty(shippedDate))
                {
                   IWebElement element = Driver.FindElementBy(By.XPath("//table[@id='mrocontent_tblShipments']/tbody/tr[5]/td[3]"));
                    Driver.ScrollIntoView(element);
                }

            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to get email shipped date waited for shipment manager running Message : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }

            return shippedDate;
        }

        /// <summary>
        /// Go To AssignRequester Page
        /// </summary>
        public ROIAdminAssignROIRequesterPage assignRequester()
        {
            try
            {
                Driver.Click(assignRequesterButton);
            }
            catch (Exception ex)
            {

                throw new Exception($"Failed  to click AssignRequester buttonwith exception details as : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }

            return new ROIAdminAssignROIRequesterPage(Driver,logger,Context);
           

        }




        /// <summary>
        /// Go To Billing Info Page
        /// </summary>
        public ROIAdminUpdateRequestBillingDetailsPage RateLink()
        {
            try
            {
                WebElementHelper helpwr = new WebElementHelper(Driver, logger,Context);
                helpwr.ScrollIntoView(applyRatelink, FindElementBy.Id);        
                Driver.FindElementBy(applyRateHyperlink).Click();
            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to click apply rate hyper link with Message : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
            return new ROIAdminUpdateRequestBillingDetailsPage(Driver,logger,Context);

        }


        /// <summary>
        /// Delivery override Dropdown  Selection
        /// </summary>

        public string ToSelectFTPOption()
        {
            try
            {
               
                IWebElement addLink = Driver.FindElementBy(addHyperlink);
                addLink.Click();
                IWebElement ftpHyperlink = Driver.FindElementBy(ftpOptionSelection);
                ftpHyperlink.Click();
                string selectedValue = Driver.FindElement(addDropdwonElement).FindElements(By.XPath("./option[@selected]"))[0].Text;               
                return selectedValue;

            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to Click ftp hyper link with deatails as Message : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
            

        }

        public bool CheckCreateInvoiceDisabled()
        {
            bool isDisabled = false;
            try
            {
                IWebElement invoiceElement = Driver.FindElementBy(createInvoiceButton);

                if (invoiceElement != null)
                {
                    string value = invoiceElement.GetAttribute("disabled");

                    if (value == "true")
                    {
                        isDisabled = true;
                    }

                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to check show alphabet filter, Exception detail: {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
            return isDisabled;
        }

        /// <summary>
        /// Create Invoice 
        /// </summary>

        public ROIAdminRequestStatusPage CreateInvoice()
        {
            string invoice = string.Empty;
            try
            {
                if (CheckCreateInvoiceDisabled())
                {
                    Driver.DirectClick(By.XPath("//input[@id='mrocontent_cmdApplyRate']"));
                }
                Driver.Click(createInvoiceButton);
                if (Driver.FindElementsBy(confirmDialogYesButton).Count > 0)
                {
                    Driver.Click(confirmDialogYesButton);
                }
                invoice = Driver.FindElementBy(invoicename).Text;

            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to create Invoice with detail  Message : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }

            return new ROIAdminRequestStatusPage(Driver, logger, Context);
        }

        /// <summary>
        /// Clicks on Add Issue Button
        /// </summary>
        public void ClickAddIssueBtn()
        {
            try
            {
                Driver.FindElementBy(addissueButton).Click();

            }
            catch (Exception ex)
            {

                throw new Exception($"Failed with Message not able to click '?' : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }

        /// <summary>
        /// Clicks on Close Button and Accepts the Delete Alert.
        /// </summary>
        public void DeleteIssue()
        {
            try
            {
                Driver.FindElementBy(Close).Click();
                Driver.SwitchTo().Alert().Accept();
            }
            catch (Exception ex)
            {

                throw new Exception($"Failed with Message not able to click on close or unable to accept Alert. : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }

        /// <summary>
        ///  Select CD Option in Delivery override Dropdown  
        /// </summary>

        public ROIAdminShipmentDetailsPage SelectCDOption()
        {
            try
            {
                Driver.Click(qcPassButton);
                SelectElement oSelect = new SelectElement(Driver.FindElementBy(DeliveryOverrideDropdown));
                oSelect.SelectByText(CDSelection);
                Driver.Click(deliveryOverrideSaveButton);
                Driver.Click(rateButton);
                CreateInvoice();
                Driver.Click(addHyperlink);
                Driver.Click(CDOptionSelection);
            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to Click CD hyper link with deatails as Message : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
            return new ROIAdminShipmentDetailsPage(Driver,logger,Context);

        }

        public void DeliveryOverride(string overrideType)
        {
            try
            {
                Driver.MoveToElement(DeliveryOverrideDropdown);
                SelectElement oSelect = new SelectElement(Driver.FindElementBy(DeliveryOverrideDropdown));
                oSelect.SelectByText(overrideType);
                Driver.Click(deliveryOverrideSaveButton);
            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to override the delivery  Message : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }

        /// <summary>
        /// Click on pass docs Qc
        /// </summary>
        public void ClickPassDocsQC()
        {
            try
            {
                Driver.Click(qcPassButton);
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to Click on pass button docs QC : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }

        }

        /// <summary>
        /// Get adjusted balance amount on rss apge
        /// </summary>
        /// <returns></returns>
        public string GetAdjustedBalanceAmountOnRSS()
        {
            try
            {
                IWebElement adjustedBalElement = Driver.FindElementBy(adjustedBalanceAmount);
                Driver.ScrollIntoView(adjustedBalElement);
                string adjustedBalance = adjustedBalElement.Text.Trim();               
                return adjustedBalance;
            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to get adjusted balance amount on rss page : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }

        }

        /// <summary>
        /// Verify invoice created
        /// </summary>
        public bool VerifyInvoiceCreated()
        {
            bool isDisplayed = false;
            try
            {
                IWebElement invoice = Driver.FindElementBy(verifyInvoice);               
                if (invoice.Displayed == true)
                {
                    isDisplayed = true;
                    logger.Log(Status.Pass, "Successfully created invoice");
                }
                else
                {
                    isDisplayed = false;
                    logger.Log(Status.Info, "Failed to create invoice");
                }
                return isDisplayed;
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to verify invoice created : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }

        }

        /// <summary>
        /// Click on rate button
        /// </summary>
        public void ClickApplyRateButton()
        {
            try
            {
                Driver.Click(rateButton);
            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to Click on apply rate button under billing info : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }

        }

        /// <summary>
        /// Click on ledger detail button
        /// </summary>
        public void ClickLedgerDetailButton()
        {
            try
            {
                Driver.Click(ledgerDetailButton);
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to Click on ledger detail button : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }

        }

        /// <summary>
        ///  Go to view action 
        /// </summary>
        public bool ViewAction()
        {
            try
            {
                Driver.Click(viewActionlink);
                string _type = Driver.GetText(typeElement).Trim();
                string _notes = Driver.FindElementBy(notesElement).Text.Trim();
                string typeVal = IniHelper.ReadConfig("NewActionAfterLocationStateChangeonReleasedTest", "Type");
                string notesVal = IniHelper.ReadConfig("NewActionAfterLocationStateChangeonReleasedTest", "Notes");
                Assert.AreEqual(_type, typeVal);
                Assert.AreEqual(_notes, notesVal);
                Driver.Click(closeCheckbox);
                WebElementHelper helper = new WebElementHelper(Driver, logger, Context);
                bool isDisplayed=helper.IsElementPresent(closeChkBox);                
                return isDisplayed;
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to Click viewaction link with deatails as Message : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }

        /// <summary> 
        ///  Go to update info page 
        /// </summary>
        public string UpdateInfoAtRoiAdminSide(string location)
        {
            try
            {
                Driver.Click(updateInfoBtn);                
                Driver.SendKeys(locationDrp, location);              
                Driver.SleepTheThread(5);
                Driver.ScrollIntoViewAndClick(updateBtn);
                return location;
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to update location with deatails as Message : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }

        /// <summary> 
        /// Click on applyRate
        /// </summary>
        public void ApplyRate()
        {
            try
            {
                Driver.Click(rateButton);
                string adjustedBalance = Driver.GetText(adjustedBal);
                logger.Log(Status.Info, $" Rate is applied and adjustedBalance amount is:{adjustedBalance}");
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to click applyrate with deatails as Message : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }
        /// <summary>
        /// Click on QCPass Button
        /// </summary>
        public void ClickOnQcPassButton()
        {
            try
            {
                Driver.Click(qcPassButton);

            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to click qcPass button with deatails as Message : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }
        public void AddEmailShippment()
        {
            try
            {
                SelectElement oSelect = new SelectElement(Driver.FindElementBy(addShippmentDropdown));
                oSelect.SelectByText(emailSelection);
                Driver.Click(addHyperlink);
                string todayDate = String.Format("{0:M/dd/yyyy}", DateTime.Now).Replace("-", "/");
                string _shippableDate= Driver.GetText(shippableDate);
                Assert.AreEqual(todayDate, _shippableDate);
                logger.Log(Status.Info, "Shippment added successfully and shippable assigned to today's date");
                SelectElement emailOption = new SelectElement(Driver.FindElementBy(addShippmentDropdown));
                emailOption.SelectByText(emailSelection);
                Driver.Click(addHyperlink);              
            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to select email shippment with deatails as Message : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }
        public  void clickOnSecondEmailShipment()
        {
            try
            {
                Driver.ScrollIntoViewAndClick(secondShipmentLink);
            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to click secondary  email shippment with deatails as Message : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }
        public string GetInvoiceId()
        {
            try
            {
                string invoiceId = Driver.GetText(invoiceIdElement);
                return invoiceId;
            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to return invoice id with deatails as Message : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }
        /// <summary>
        /// Verify Request Status page
        /// </summary>
        public bool VerifyRequestStatusPage()
        {
            bool isHeader = false;
            try
            {
                IWebElement requestPageHeader = Driver.FindElementBy(requestStatusHeader);
                if (requestPageHeader.Displayed == true)
                {
                    isHeader = true;                    
                }
                else
                {
                    isHeader = false;

                }
                return isHeader;
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to verify request status page opened : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }
        /// <summary>
        /// Click on Aetna Test Fax
        /// </summary>
        public void ClickOnAetnaTestFax()
        {
            try
            {
                Driver.Click(aetnaTestFaxlnk);
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to click on aetna test fax : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }
        public void ClickOnReAssignROIRequester()
        {
            try
            {
                Driver.Click(reAssignRequesterBtn);
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to click on re assign roi requester : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }
        public bool VerifyPanOnRSS()
        {
            bool isDisplayed = false;
            try
            {
                IWebElement panTxt = Driver.FindElementBy(panOnRSS);
                if (panTxt.Displayed == true)
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
                throw new Exception($"Failed to verify pan number on the RSS : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }

        public int GetPanNumberOnAssignROIRequester()
        {
            try
            {
                string panNumberValue = Driver.FindElementBy(panTxtOnAssignROIRequester).Text.ToString();
                int panValue = Convert.ToInt32(panNumberValue.ToString().Trim());
                return panValue;
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to get pan number on assign roi requester page: {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }

        public void ClickOnUpdateInfo()
        {
            try
            {
                Driver.Click(updateinfoBtn);
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to click on update info : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }
        public int GetUpdatedPanNumberOnRSS()
        {
            try
            {
                string updatedPanNumberTxt = Driver.FindElementBy(panOnRSS).Text.ToString();
                string panValue = updatedPanNumberTxt.Split(':')[1].ToString().Trim();
                int panTxt = Convert.ToInt32(panValue);
                return panTxt;
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to get pan number on request status page: {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }
        public string VerifyReAssignRequester()
        {
            try
            {
                string reAssignRequesterValue = Driver.GetText(reAssignRequesterTxt).Trim();
                return reAssignRequesterValue;
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to verify re assign requester: {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
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

        public string VerifyRateRegressionBaseRate()
        {
            try
            {
                string rateAppliedValue = Driver.GetText(rateAppliedTxt).Trim();
                return rateAppliedValue;
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to verify rate applied regression base rate : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }
        public void ClickOnAddAndSelectEmail()
        {
            try
            {

                // Driver.Click(addHyperlink);
                // Driver.SleepTheThread(5);
                //  Driver.SwitchTo().Alert().Accept();
                SelectElement oSelect = new SelectElement(Driver.FindElementBy(addShippmentDropdown));
                oSelect.SelectByText(emailSelection);
                Driver.SleepTheThread(5);
                Driver.Click(addHyperlink);
                Driver.SleepTheThread(5);
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to click on add and select email : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }
        public void ClickOnEmailHyperlink()
        {
            try
            {
                Driver.Click(emailHyperlink);
                Driver.SleepTheThread(5);
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to click on email hyperlink : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }

        }
        public void ClickLogCheck()
        {
            try
            {
                Driver.FindElementBy(LogCheck).Click();

            }
            catch (Exception ex)
            {

                throw new Exception($"Failed with Message not able to click '?' : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }

        public string GetL2AdjustableBal()
        {
            try
            {
                string l2AdjustableBal = Driver.FindElementBy(L2AdjustableBalance).Text;
                return l2AdjustableBal;

            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to get l2 adjustable balance with Message : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }

        public string VerifyTestFacility()
        {
            try
            {
                string testFacility = Driver.FindElementBy(TestFaciltyCBO).Text;
                return testFacility;
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to get test facility: {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }

        public string VerifyShipText()
        {
            try
            {
                string shipText = Driver.FindElementBy(AJAssociates).Text;
                return shipText;
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to get test facility: {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }

        public string GetRecordsSentByFacilityDate()
        {
            try
            {
                string recordsSentDate = Driver.FindElementBy(RecordsSentByFacility).Text;
                return recordsSentDate;
            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to get records sent by facility date with deatails as Message : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }

        }

        public string GetSystemDate()
        {
            try
            {
                //string systemDate = DateTime.ParseExact(DateTime.Now.ToString(), "dd.MM.yyyy", CultureInfo.InvariantCulture).ToString("MM-dd-yyyy");
                string dateAndTime = DateTime.Now.ToShortDateString();
                //string systemDate = dateAndTime.
                return dateAndTime;
            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to get records sent by facility date with deatails as Message : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }

        }

        public void ClickSendIssueBtn()
        {
            try
            {
                IWebElement sendIssueBtn = Driver.FindElementBy(SendIssueButton);
                sendIssueBtn.Click();
                Driver.Wait(TimeSpan.FromSeconds(2));
                //IWebElement yesBtn = Driver.FindElementBy(confirmDialogYesButton);
                //sendIssueBtn.Click();
                Driver.Click(By.XPath("//button[contains(text(),'Yes')]"));
                Driver.Wait(TimeSpan.FromSeconds(2));

            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to get records sent by facility date with deatails as Message : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }

        }

        public void CreateInvoiceAndYes()
        {
            try
            {
                IWebElement createInvoice = Driver.FindElementBy(createInvoiceButton);
                createInvoice.Click();

                IWebElement clickYes = Driver.FindElementBy(YesCreateInvoice);
                clickYes.Click();
               
            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to click on create invoice and yes: {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }

        public string AddEmailAndClickAdd()
        {
            try
            {
                SelectElement oSelect = new SelectElement(Driver.FindElementBy(addShippmentDropdown));
                oSelect.SelectByText(emailSelection);
                Driver.Click(addHyperlink);
                return Driver.GetText(shippableDate);

            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to add Email Message : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }
        public void ClickOnAddAndSelectExtUpload()
        {
            try
            {
                SelectElement oSelect = new SelectElement(Driver.FindElementBy(addShippmentDropdown));
                oSelect.SelectByText(extUpload);
                Driver.SleepTheThread(5);
                Driver.Click(addHyperlink);
                Driver.SleepTheThread(5);
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to click on add and select extupload : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }
        public void ClickOnExtUploadHyperlink()
        {
            try
            {
                Driver.Click(extUploadHyperlink);
                Driver.SleepTheThread(5);
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to click on ext upload hyperlink : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }

        }

        public void DrillInToFacility()
        {
            try
            {
                Driver.Click(lnkFacility);
            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to click on facility link : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }

        public string VerifyEmail()
        {
            try
            {
                string email = Driver.GetText(emailId);
                return email;

            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to return email id: {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }

        public void ClickCorrespondensePackageByType(string packageType)
        {
            try
            {
                List<IWebElement> tableDataRows = Driver.FindElementsBy(By.XPath("//table[@id='mrocontent_tblFaxPackages']//tr"));

                for (int z = 0; z < tableDataRows.Count; z++)
                {
                    System.Collections.ObjectModel.ReadOnlyCollection<IWebElement> cells = tableDataRows[z].FindElements(By.TagName("td"));
                    string pkgType = cells[2].Text;
                    if (pkgType == packageType)
                    {
                        System.Collections.ObjectModel.ReadOnlyCollection<IWebElement> viewShipmentButton = tableDataRows[z].FindElements(By.XPath("//input[@title ='View Shipment']"));
                        viewShipmentButton[z].Click();
                    }
                }
            }

            catch (Exception ex)
            {
                throw new Exception($"Failed to click package with type PasswordLetter:{ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }


        }
        public string SelectedDeliveryMethod()
        {
            try
            {
                string type = Driver.FindElement(DeliveryOverrideDropdown).FindElements(By.XPath("./option[@selected]"))[0].Text;
                Driver.ScrollToElement(DeliveryOverrideDropdown);
                return type;
            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to return selected delivery method:{ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }
        public void ClickOnAuditLog()
        {
            try
            {
                Driver.ScrollToElement(roiadmin);
                Driver.Click(roiadmin);
                Driver.Click(auditLog);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public void ClickRequestHistoryButton()
        {
            try
            {
                Driver.Click(requestHistoryButton);
            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to Click on Request History button : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }

        }
        public bool VerifyRetrievalPageAndSalesTaxValues()
        {
            bool _isDisplayed = false;
            try
            {
                string retrievalFeeTxt = Driver.GetText(retrievalFee).Trim();
                string pageFeeTxt = Driver.GetText(pageFee1).Trim();
                string shippingFeeTxt = Driver.GetText(shippingFee).Trim();
                string salesTaxTxt = Driver.GetText(salesTax).Trim();
                string invoiceAmountTxt = Driver.GetText(innvoiceAmount).Trim();
                if (retrievalFeeTxt == "22.48" && pageFeeTxt == "6.20" && shippingFeeTxt == "1.40" && salesTaxTxt == "0.53" && invoiceAmountTxt == "$30.61")
                {
                    _isDisplayed = true;
                }
                else
                {
                    _isDisplayed = false;
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to verify retrieval, page fee and sales tax fee : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
            return _isDisplayed;
        }
        public bool VerifyUpdatedPageFee1()
        {
            bool _isDisplayed = false;
            try
            {
                string pageFeeTxt = Driver.GetText(pageFee1).Trim();
                if (pageFeeTxt == "16.20")
                {
                    _isDisplayed = true;
                }
                else
                {
                    _isDisplayed = false;
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to verify updated page fee1: {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
            return _isDisplayed;
        }
        public bool VerifyUpdatedSalesTaxAndInvoiceAmountValues()
        {
            bool _isDisplayed = false;
            try
            {
                string salesTaxTxt = Driver.GetText(salesTax).Trim();
                string invoiceAmountTxt = Driver.GetText(innvoiceAmount).Trim();
                if (salesTaxTxt == "1.23" && invoiceAmountTxt == "$41.31")
                {
                    _isDisplayed = true;
                }
                else
                {
                    _isDisplayed = false;
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to verify updated sales tax and invoice amount : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
            return _isDisplayed;
        }
        public bool VerifyUpdatedAdjustmentSalesTaxAndAdjustedBalance()
        {
            bool _isDisplayed = false;
            try
            {
                string salesTaxTxt = Driver.GetText(salesTax).Trim();
                string adjustmentsTxt = Driver.GetText(adjustments).Trim();
                string adjustedBalanceTxt = Driver.GetText(adjustedBalance).Trim();
                if (salesTaxTxt == "2.54" && adjustmentsTxt == "18.69" && adjustedBalanceTxt == "$61.31")
                {
                    _isDisplayed = true;
                }
                else
                {
                    _isDisplayed = false;
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to verify updated adjustment, sales tax and adjusted balance  amount : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
            return _isDisplayed;
        }

        /// <summary>
        /// Click on Pay by CC Button
        /// </summary>
        public void ClickOnPayByCCButton()
        {
            try
            {
                Driver.Click(PayByCCBtn);
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to click Pay by CC button with deatails as Message : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }

        public void ClickOnManageIssuesBtn()
        {
            try
            {
                Driver.Click(manageIssuesBtn);

            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to click manage issues button with deatails as Message : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }
       public void ClickOnComments(string comment)
        {
            try
            {
                Driver.Click(By.XPath("//select[@id='mrocontent_lstRequestIssues']//option[contains(text(),'Authorization Not Signed')]"));
                Driver.Click(commentsTxtbox);
                Driver.ClearText(commentsTxtbox);
                Driver.SendKeys(commentsTxtbox, comment);
                Driver.Click(updateCommentsbtn);
                Driver.SwitchTo().Alert().Accept();
                Driver.Click(doneBtn);

            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to click comments with deatails as Message : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        
        }
         public string  VerifyCorrespondenceIssue()
        {
            try
            {
                string issueId = Driver.GetText(By.XPath("//a[@title='View package']"));
                return issueId;
                Driver.Wait(TimeSpan.FromSeconds(2));


            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to verify correspondence issue with deatails as Message : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }

        public void ClickOnIssueMagnifierIcon()
        {
            try
            {

                Driver.Click(issueMagnifierIcon);
                Driver.SwitchToWindow("MRO Viewer");
                Driver.Manage().Window.Maximize();                
                Driver.Wait(TimeSpan.FromSeconds(1));
                Driver.SwitchToWindowAndClose("MRO Viewer");
                Driver.SleepTheThread(3);
                Driver.SwitchToWindow("Request Status");
            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to click magnifier icon with deatails as Message : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");

            }
        }

        public void ClickOnCorrespondenceIssueMagnifierIcon()
        {
            try
            {

                Driver.Click(correspondenceMagnifierIcon);
                Driver.SwitchToWindow("MRO Viewer");
                Driver.Manage().Window.Maximize();                
                Driver.Wait(TimeSpan.FromSeconds(1));
                Driver.SwitchToWindowAndClose("MRO Viewer");
                Driver.SleepTheThread(3);
                Driver.SwitchToWindow("Request Status");
            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to click magnifier icon  for correspondence issue with deatails as Message : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");

            }
        }

        public void ClickOnCorrespondence()
        {
            try
            {
                Driver.Click(correspondenceBtn);

            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to click correspondence button with deatails as Message : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }

        public void AddCoresspondenceIssue(string issue,string commentText)
        {
            try
            {
                Driver.SendKeys(issueDropdown, issue);
                Driver.Click(comments);
                Driver.SendKeys(comments, commentText);
                Driver.Click(addCoresspondenceBtn);
                Driver.Click(By.XPath("//button[contains(text(),'Yes')]"));


            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to click correspondence button with deatails as Message : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }

        public string VerifyCorrespondencePackage()
        {
            try
            {
               
                
                string issueId = Driver.GetText(correspondenceId);
                Driver.ScrollToElement(correspondenceId);
                return issueId;


            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to verify correspondence issue with deatails as Message : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }

        public void ClickOnCloseBtn()
        {
            try
            {
                Driver.Click(closeBtn);
                Driver.SwitchTo().Alert().Accept();
                Driver.Navigate().Refresh();

            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to click close button with deatails as Message : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }

        public bool CreatedInvoiceNumber()
        {
            bool isDisplayed = false;
            try
            {
                IWebElement invoice = Driver.FindElementBy(invoiceId);
                if (invoice.Displayed == true)
                {
                    isDisplayed = true;
                    Driver.ScrollIntoView(invoice);
                    logger.Log(Status.Pass, "Successfully created invoice");
                }
                else
                {
                    isDisplayed = false;
                    logger.Log(Status.Info, "Failed to create invoice");
                }
                return isDisplayed;
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to verify invoice created : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }

        }

        public void VerifyAlertMessage()
        {
            try
            {
                string alertMsg = IniHelper.ReadConfig("ROIIssuePackageCreationErrorMessageTest", "alert");
                Driver.SwitchTo().Alert().Equals(alertMsg);
                Driver.Wait(TimeSpan.FromSeconds(2));
            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to verify alert message : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }

        public bool VerifyCorrespondenceIssueCreatedOrNot()
        {
            try
            {

                string isIssueDisplayed = "//a[@title='View package']";
                WebElementHelper helper = new WebElementHelper(Driver, logger, Context);
                bool isDisplayed = helper.IsElementPresent(isIssueDisplayed);
                Driver.ScrollToElement(correspondenceHistoryElement);
                return isDisplayed;


            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to verify correspondence issue with deatails as Message : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }

        public void RecentRequest()
        {
            try
            {

                Actions action = new Actions(Driver);
                Driver.Wait(TimeSpan.FromSeconds(3));
                IWebElement roiRequest = Driver.FindElementBy(By.XPath("//*[contains(text(),'ROIAdmin')]"));
                action.MoveToElement(roiRequest).Perform();
                Driver.Wait(TimeSpan.FromSeconds(1));
                IWebElement recentRequest = Driver.FindElementBy(By.XPath("//*[contains(text(),'Recent Requests')]"), 10);
                action.MoveToElement(recentRequest).Perform();
                Driver.Wait(TimeSpan.FromSeconds(3));
                IWebElement submenuItem = Driver.FindElementBy(By.XPath("(//table[starts-with(@id,'mroheader_')])[2]//tr"));
                action.MoveToElement(submenuItem).Click().Build().Perform();
                Driver.Wait(TimeSpan.FromSeconds(1));
            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to click recent request with deatails as Message : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }

        public void NavigateToMROViewerAndGetData()
        {
            try
            {
                Driver.SwitchToWindow("MRO Viewer");
                Driver.Manage().Window.Maximize();
                Driver.Wait(TimeSpan.FromSeconds(1));
                Driver.DirectClick(selectAllPackagesButton);
                Driver.DirectClick(printSelectedDocuments);
                Driver.SwitchToWindowAndClose("MRO Viewer");
                Driver.SleepTheThread(3);
                Driver.SwitchToWindow("Request Status");
            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to click magnifier icon  for correspondence issue with deatails as Message : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");

            }
        }

        public string GetPurposeOfUseValue()
        {
            string sPurposeOfUse = string.Empty;
            try
            {
                 sPurposeOfUse = Driver.GetText(purposeOfUseText);
            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to return purpose of use: {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
            return sPurposeOfUse;
        }

        public string GetPurposeOfUseFromRequestHistoryPage()
        {
            string type = string.Empty;
            string info = string.Empty;
            string updatedInfo = string.Empty;
            try
            {
                List<IWebElement> tableDataRows = Driver.FindElementsBy(By.XPath("//table[@id='mrocontent_dgEvents']//tr"));

                for (int z = 0; z < tableDataRows.Count; z++)
                {
                    System.Collections.ObjectModel.ReadOnlyCollection<IWebElement> cells = tableDataRows[z].FindElements(By.TagName("td"));
                    type = cells[1].Text;
                    info = cells[2].Text;
                    if (type == "Requester Edit" &&info.Contains("Purpose Of Use"))
                    {
                        updatedInfo = cells[2].Text;
                    }
                }
                return updatedInfo;
            }

            catch (Exception ex)
            {
                throw new Exception($"Failed to get updated purpose of use value from Request History Page:{ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }


        }



        public void ClickOnAddAndSelectMail()
        {
            try
            {
                SelectElement oSelect = new SelectElement(Driver.FindElementBy(addShippmentDropdown));
                oSelect.SelectByText(mailSelection);
                Driver.SleepTheThread(5);
                Driver.DirectClick(addHyperlink);
                Driver.SleepTheThread(5);
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to click on add and select email : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }

        public void ClickOnMailHyperlink()
        {
            try
            {
                Driver.Click(emailHyperlink);
                Driver.SleepTheThread(5);
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to click on mail hyperlink : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }

        }
        public string GetRequestStatusFromRSS()
        {
            string updatedStatus = string.Empty;
            try
            {
                updatedStatus = Driver.FindElementBy(requestStatus).Text.ToString();
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to get request status on request status page: {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
            return updatedStatus;
        }

        public void ClickOnUpdateInfoButton()
        {
            try
            {
                Driver.Click(updateInfoButton);
                Driver.SleepTheThread(5);
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to click on Update Info button : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }

        }

        public void SetDateUndeCancelledByRequesterField()
        {
            try
            {
                var todaysDate = String.Format("{0:M/dd/yyyy}", DateTime.Now).Replace("-", "/");
                var requestCancelledDate = Driver.FindElementBy(cancelledDate);
                requestCancelledDate.SendKeys(todaysDate);
                Driver.Click(updateBtn);
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to click on Update Info button : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }

        }
        public void AddPdfShipment()
        {
            try
            {
                SelectElement oSelect = new SelectElement(Driver.FindElementBy(addShippmentDropdown));
                oSelect.SelectByText(pdfSelection);
                Driver.Click(addHyperlink);

            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to add shipment Message : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }

        public string GetRetrievalAmountOnRSS()
        {
            try
            {
                IWebElement retrievalFeeElement = Driver.FindElementBy(retrievalFee);
                Driver.ScrollIntoView(retrievalFeeElement);
                string retrievalBalance = retrievalFeeElement.Text.Trim();
                return retrievalBalance;
            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to get retrieval fee amount on rss page : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }


        }
        public string GetPageFeetOnRSS()
        {
            try
            {
                IWebElement pageFee1Element = Driver.FindElementBy(pageFee1);
                Driver.ScrollIntoView(pageFee1Element);
                string pageFee = pageFee1Element.Text.Trim();
                return pageFee;
            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to get page Fee amount on rss page : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }

        }

        public string GetShippingOnRSS()
        {
            try
            {
                IWebElement shippingFeeElement = Driver.FindElementBy(shippingFee);
                Driver.ScrollIntoView(shippingFeeElement);
                string shippingFeeVal = shippingFeeElement.Text.Trim();
                return shippingFeeVal;
            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to get shipping Fee amount on rss page : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }

        }

        public string GetRequestStatus()
        {
            try
            {
               string requestVal= Driver.GetText(requestStatusVal);
               return requestVal;

            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to get request status on rss page : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }

        }

        public void  ClickOnCancel()
        {
            try
            {
                Driver.Click(cancelBtn);
                if (Driver.FindElementsBy(confirmDialogYesButton).Count > 0)
                {
                    Driver.Click(confirmDialogYesButton);
                }
                Driver.Wait(TimeSpan.FromSeconds(2));

            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to click  cancel button on rss page : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }

        }

        public void VerifyPayByCCDisabled()
        {
            try
            {
                Driver.FindElement(disabledPayByCC);
                Driver.MoveToElement(disabledPayByCC);
                Assert.AreEqual(Driver.FindElement(disabledPayByCC).GetAttribute("title"), "Request has $0 or negative balance due.");
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to Pay By CC button : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }

        public void VerifyAdjustedBalance(string balance)
        {
            try
            {
                Driver.MoveToElement(AdjBalance);
                Assert.AreEqual(Driver.FindElement(AdjBalance).GetAttribute("innerText"), balance);
            }
            catch (Exception ex)
            {
                throw new Exception($"Adjusted balance is not as expected: {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }

        public void SetNonBillable()
        {
            try
            {
                Driver.Click(SetNonBillableButton);
                Driver.SwitchTo().Alert().Accept();
                Driver.SleepTheThread(5);
            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to click Set Non Billable button : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }

        public bool CreateInvoiceAndValidateThePopupMessage()
        {
            string invoice = string.Empty;
            bool isStatusValid = false;
            try
            {
                if (CheckCreateInvoiceDisabled())
                {
                    Driver.DirectClick(By.XPath("//input[@id='mrocontent_cmdApplyRate']"));
                }
                Driver.Click(createInvoiceButton);
                if (Driver.FindElementsBy(confirmDialogYesButton).Count > 0)
                {
                    Driver.Click(confirmDialogYesButton);
                }
                Driver.SleepTheThread(5);
                isStatusValid = IsErrorPopupVisibleForFaxShipmentPageLimit();
                return isStatusValid;
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to create Invoice with detail Message : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }

        public bool IsErrorPopupVisibleForFaxShipmentPageLimit()
        {
            string sMessage = string.Empty;
            bool isAlertVisible = false;
            try
            {
                var vAlert = Driver.SwitchTo().Alert();
                sMessage = vAlert.Text;
                if (sMessage.Equals("Fax Shipment Exceeds Max Limit (10) - Please Select a Different Shipment Type."))
                {
                    isAlertVisible = true;
                    Driver.SwitchTo().Alert().Accept();
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to get visibility status of the popup with message as : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
            return isAlertVisible;
        }

        public bool VerifyNextButtonIsVisibleOrNot()
        {
            try
            {
                string isNextBtn = "//input[@id='mrocontent_cmdFinishEvent']";
                WebElementHelper helper = new WebElementHelper(Driver, logger, Context);
                bool isDisplayed = helper.IsElementPresent(isNextBtn);
                return isDisplayed;

            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to verify processing status with message as : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }


        public void ClickOnNextButton()
        {
            try
            {
                Driver.Click(nextBtn);
                Driver.Wait(TimeSpan.FromSeconds(2));

            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to click on next button with message as : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }
        
        public void ClickInvoicingQueue()
        {
            try
            {
                Driver.Click(roiAdmin);
                Driver.Wait(TimeSpan.FromSeconds(2));
                Driver.FindElementBy(invoicingQueueElement).Click();
                Driver.Wait(TimeSpan.FromSeconds(2));
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to click on invoicing queue with details as : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }

        public void ClearInvoice()
        {
            try
            {
                IWebElement clearInvoice = Driver.FindElementBy(ClearInvoiceButton);
                clearInvoice.Click();
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to click on clear invoice: {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }

        public bool CheckClearInvoiceDisabled()
        {
            bool isDisabled = false;
            try
            {
                IWebElement invoiceElement = Driver.FindElementBy(ClearInvoiceButton);

                if (invoiceElement != null)
                {
                    string value = invoiceElement.GetAttribute("disabled");

                    if (value == "true")
                    {
                        isDisabled = true;
                    }

                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to check clear invoice button status with Exception detail: {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
            return isDisabled;
        }

        public void ClickOnAddAndSelectFax()
        {
            try
            {
                SelectElement oSelect = new SelectElement(Driver.FindElementBy(addShippmentDropdown));
                oSelect.SelectByText(faxSelection);
                Driver.SleepTheThread(3);
                Driver.DirectClick(addHyperlink);
                Driver.SleepTheThread(3);
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to click on add and select Fax : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }

        public void ClickOnTestAttroney()
        {
            try
            {
                Driver.Click(requesterValue);
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to click test attroney with details as: {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }


        public void ClickOnUpdateInfoForRequester()
        {
            try
            {
                Driver.Click(requesterupdateinfoBtn);
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to click on update info : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }

        public void EnterFaxNumber(string faxNum)
        {
            try
            {
                Driver.SendKeys(faxNumTxtbox, faxNum);
                Driver.Click(saveBtn);
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to click on update info : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }

        public bool VerifyFaxNumber()
        {

            try
            {
                string faxElement = "//td[@id='mrocontent_tdRqrFax']";
                WebElementHelper helper = new WebElementHelper(Driver, logger, Context);
                bool isDisplayed = helper.IsElementPresent(faxElement);
                return isDisplayed;


            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to click on update info : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }


        public void AddCoresspondenceIssueNo(string issue, string commentText)
        {
            try
            {
                Driver.SendKeys(issueDropdown, issue);
                Driver.Click(comments);
                Driver.SendKeys(comments, commentText);
                Driver.Click(addCoresspondenceBtn);
                Driver.Click(By.XPath("//button[contains(text(),'No')]"));
                Driver.SleepTheThread(1);
                Driver.ScrollToEndOfThePage();
            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to click correspondence button with deatails as Message : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }
        //sandeep
        public void ClickOnFindARequest()
        {
            try
            {
                Driver.ScrollToElement(roiadmin);
                Driver.Click(roiadmin);
                Driver.Click(findarequest);
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to click find request with deatails as Message : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }


        public string GetRequestType()
        {
            try
            {
                string type = Driver.GetText(requesterType);
                return type;

            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to get requester type with deatails as Message : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }


        public void ClickAddAction()
        {
            try
            {
                Driver.Click(addActionLink);

            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to click add action  with deatails as Message : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }

        public bool VerifyViewAction()
        {
            try
            {
                WebElementHelper helper = new WebElementHelper(Driver, logger, Context);
                string isDisplayed = "//input[@id='mrocontent_btnActions']";
                bool isViewactionExist = helper.IsElementPresent(isDisplayed);
                return isViewactionExist;

            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to click add action  with deatails as Message : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }


        /// <summary>
        /// Verify Invoice Status page
        /// </summary>
        public bool VerifyInvoiceID()
        {
            bool isHeader = false;
            try
            {
                Driver.ScrollToElement(invoce_ID);
                Driver.HighlightingWebElement(invoce_ID);
                IWebElement requestPageHeader = Driver.FindElementBy(invoce_ID);
                if (requestPageHeader.Displayed == true)
                {
                    isHeader = true;
                }
                else
                {
                    isHeader = false;

                }
                return isHeader;
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to verify invoice status : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }

        /// <summary>
        /// Verify Email Status page
        /// </summary>
        public bool VerifyEmailStatus()
        {
            bool isHeader = false;
            try
            {
                Driver.ScrollToElement(emailStatus);
                Driver.HighlightingWebElement(emailStatus);
                IWebElement requestPageHeader = Driver.FindElementBy(emailStatus);
                if (requestPageHeader.Displayed == true)
                {
                    isHeader = true;
                }
                else
                {
                    isHeader = false;

                }
                return isHeader;
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to verify email status : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }

        public void CloseTheIssue()
        {
            try
            {
                //Driver.DirectClick(By.XPath("//input[@id='mrocontent_cbActions']"));
                System.Collections.Generic.List<IWebElement> closeButtons = Driver.FindElementsBy(By.XPath("//span[text()='Close']/preceding-sibling::input"));
                int count = closeButtons.Count - 1;
                closeButtons[count].Click();
                Driver.SleepTheThread(4);
            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to close the issue Message : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }

        }

        public void ClickRSSRequestStatus()
        {
            try
            {
                Driver.Click(roiadmin);
                Driver.Wait(TimeSpan.FromSeconds(2));
                Driver.Click(rssRequestStatus);
            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to click RSS Request status with details as : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }

        public void FindRequest(string reqId)
        {
            try
            {
                Driver.SendKeys(requestIdTxtbox, reqId);
                Driver.Click(searchIdBtn);
            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to search request with details as : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }

        public ROIAdminShipmentDetailsPage SelectEXTUpload()
        {
            try
            {

                SelectElement oSelect = new SelectElement(Driver.FindElementBy(DeliveryOverrideDropdown));
                oSelect.SelectByText(eXTUpload);
                Driver.Click(deliveryOverrideSaveButton);
                Driver.Click(addHyperlink);
                Driver.Click(eXTUploadlink);
            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to Click EXTUpload hyper link with deatails as Message : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
            return new ROIAdminShipmentDetailsPage(Driver, logger, Context);

        }

        public string AdjustedBalance()
        {
            try
            {
                IWebElement adjustedBalElement = Driver.FindElementBy(adjustedBalanceAmount);
                Driver.ScrollIntoView(adjustedBalElement);
                string adjustedBalance = adjustedBalElement.Text.Trim();
                adjustedBalance = adjustedBalance.Replace("$", " ").Trim();
                return adjustedBalance;
            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to get adjusted balance amount on rss page : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }

        }
    }
}
