using AventStack.ExtentReports;
//using Microsoft.VisualStudio.TestTools.UnitTesting;
using MRO.ROI.Automation.Common.Navigation;
using MRO.ROI.Automation.Pages.ROIFacility;
using MRO.ROI.Automation.Selenium;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MRO.ROI.Automation.Pages.Common
{
	public static class MROFunctions
	{
		public static void ClickYes()
		{
			Driver.Wait(TimeSpan.FromSeconds(5));
			Driver.FindElement(By.Id("rbYes")).Click();
			WebElementHelper.Click_Enter();
		}


		public static int CreateRequestInVersion(string sVersion, int nPatientPages = 2, int nRequestPages = 2)
		{
			//MROroilookupidadmin("23627445");
			//return 23627445;
			LoginFacilityVersion(sVersion);
			int nRequestID = CreateRequest(nPatientPages, nRequestPages);
			Driver.MROLogInfo("Request ID: " + nRequestID + " in version: " + sVersion);

			LogNewRequestPage.facillogoutbutton();
			return nRequestID;
		}


		public static void OverrideVersion(string sVersion)
		{
			LoginFacilityVersion(sVersion);
			var loutbutton = Driver.FindElement(By.Id(PageElements.FacilityWorkSummaryPage.facillogoutbtn_id));
			loutbutton.Click();
		}

		public static void LoginFacilityVersion(string sVersion)
		{
			Driver.MROSelectRoiAdmin("Users", "Admin List");

			Driver.FindElement(By.Id(PageElements.ROIRequesterPortal.userlogin_id)).Clear();
			Driver.FindElement(By.Id(PageElements.LogNewRequestPage.firstName_Id)).Clear();
			Driver.FindElement(By.Id(PageElements.LogNewRequestPage.firstName_Id)).SendKeys("Sele");
			Driver.FindElement(By.Id(PageElements.LogNewRequestPage.lastName_Id)).Clear();
			Driver.FindElement(By.Id(PageElements.LogNewRequestPage.lastName_Id)).SendKeys("Auto");
			Driver.FindElement(By.Id(PageElements.ROIRequesterPortal.roiReqeusterPortalSearchBtn_Id)).Click();
			//Driver.Wait(TimeSpan.FromSeconds(3));
			Driver.FindElement(By.XPath(PageElements.ROIRequesterPortal.adminverovrride_xpath)).Click();
			Driver.FindElement(By.Id(PageElements.ROIRequesterPortal.verrsionovrride_id)).Clear();
			Driver.FindElement(By.Id(PageElements.ROIRequesterPortal.verrsionovrride_id)).SendKeys(sVersion);
			Driver.FindElement(By.Id(PageElements.ROIRequesterPortal.versavebtn_id)).Click();


			Driver.MROSelectRoiAdmin("Facilities", "Facility List"); ROIAdminFacalitiesListPage.gotoROITestFacility();
			//Driver.Wait(TimeSpan.FromSeconds(10));

			//if (bReturn)
			//	{
			//	Driver.FindElement(By.Id(PageElements.LogNewRequestPage.ratesfacilogoutbtn_id)).Click();
			//	Driver.Wait(TimeSpan.FromSeconds(5));
			//	}

			//   LogNewRequestPage.facillogoutbutton();
			//facility drill down
			//logout of facility
			Driver.MROLogInfo("Version: " + sVersion);
		}

		public static void SetRate(string reqId, string rate)
		{

			Driver.MROLogInfo("Start Rate Update");
			MROroilookupidadmin(reqId);
			// Driver.MROLogInfo("Testing Rate for Request ID: " + reqId);
			RequestStatus.applyRate();
			string data1 = Driver.FindElement(By.Id("mrocontent_lblPatient")).Text;
			Driver.MROLogInfo("Patient name: " + data1);
			Driver.FindElement(By.Id(PageElements.ROIAdminList.adminchange_id)).Click();
			//Driver.Wait(TimeSpan.FromSeconds(5));
			Driver.FindElement(By.Id(PageElements.ROIAdminList.admincharr_id)).Click();
			//Driver.Wait(TimeSpan.FromSeconds(5));
			Driver.FindElement(By.LinkText(rate)).Click();

			string ratetext = Driver.FindElement(By.Id("mrocontent_lblAppliedRate")).Text;
			logger.Pass(ratetext);
			Driver.FindElement(By.Id(PageElements.ROIAdminList.roiadminapplyrate_id)).Click();
			//Driver.Wait(TimeSpan.FromSeconds(5));
			if (!WebElementHelper.IsElementMissing(By.Id("mrocontent_tdRateDescription"), 1))
			{
				string data = Driver.FindElement(By.Id("mrocontent_tdRateDescription")).Text;
				Driver.MROLogInfo("(Rate Calculation String) " + data);
			}

			//IJavaScriptExecutor js = (IJavaScriptExecutor)Driver.Instance;
			//js.ExecuteScript("window.scrollBy(0,130)");
			//Driver.Wait(TimeSpan.FromSeconds(3));
			//Driver.takeScreenShot();
			//js.ExecuteScript("window.scrollBy(0,-130)");
			Driver.MROLogInfo("Rate Update Completed");

		}

		public static int CreateRequest(int nPatientPages = 2, int nRequestPages = 2)
		{
			Driver.MROLogInfo("Start Request Creation");
			MenuSelector.Select("ROI Requests", "Log a New Request");

			//   LogNewRequestPage.GoToLogNewRequestPage();
			//   Assert.IsTrue(LogNewRequestPage.IsAtLogNewRequestPage, "Failed to navigate to Log New Request page.");
			bool tab = LogNewRequestPage.ClickMRODeliveryTab();
			//Assert.IsTrue(tab, "Failed to click on MRO delivery tab");

			//   Assert.IsTrue(mroDelTab, "Failed to click on MRO delivery tab");

			LogNewRequestPage.CreateNewMRODeliveryRequest(nRequestPages);

			//Assert.IsTrue(LogNewRequestPage.NewRequestCreated, "Failed to create new MRO delivery request");
			// LogNewRequestPage.CreateRequest();
			//    Automation.Utility.ScannerUtil.ScanDocuments();
			string requestID = ROIAdminFacalitiesListPage.getRequestid();
			//LogNewRequestPage.RequestStatus();
			//LogNewRequestPage.PatientNameValidation();
			//WebElementHelper.ScrollIntoView("mrocontent_cmdScan_");
			LogNewRequestPage.GoToRequestStatusPage();
			//Assert.IsTrue(FacilityRequestStatusPage.IsAtRequestStatusPage, "Failed to navigate to facility request status page.");

			FacilityRequestStatusPage.ScanPatientPages(LogNewRequestPage.PatientFirstName, LogNewRequestPage.PatientLastName, nPatientPages);
			FacilityRequestStatusPage.ReleaseRequest();

			Driver.Wait(TimeSpan.FromSeconds(1));
			WebElementHelper.Click_Enter();


			Driver.MROLogInfo("Request Created & Released " + requestID);
			return int.Parse(requestID);
		}

		public static void AssignRequest(int nRequestID, string sRequester)
		{
			Driver.MROLogInfo("Start Assignment of " + nRequestID + " to " + sRequester);
			MROroilookupidadmin(nRequestID + "");
			RequestStatus.roiAssignReq();
			RequestStatus.roiAdminSearch(sRequester);
			RequestStatus.roiSaveDonebtn();

			RequestStatus.qcStatus();
			//RequestStatus.roiCreateInvoice();
			Driver.MROLogInfo("Assigned " + nRequestID + " to " + sRequester);
		}

		public static void MROroilookupidadmin(string rqsID)
		{
			Driver.MROLogInfo("Start Goto Request Page for " + rqsID);
			//Driver.Wait(TimeSpan.FromSeconds(5));
			Driver.FindElement(By.Id(PageElements.ROIAdminList.adminlookupid_id)).Click();
			//Driver.Wait(TimeSpan.FromSeconds(1));

			Driver.SwitchTo().Alert().SendKeys(rqsID);
			Driver.SwitchTo().Alert().Accept();
			Driver.MROLogInfo("Complete Goto Request Page for " + rqsID);
		}

		public static void AddMROPayment(int nRequestID, Decimal? cyPayment)
		{
			Driver.MROLogInfo("Start MRO Payment of " + cyPayment);
			MROroilookupidadmin(nRequestID + "");

			//Driver.Wait(TimeSpan.FromSeconds(5));
			Driver.FindElement(By.Id("mrocontent_cmdLedgerDetail")).Click();
			Driver.Click("//*[@id='mrocontent_cmdPayments']");
			Driver.Type("//*[@id='mrocontent_txtAmount']", cyPayment + "");
			Driver.Type("//*[@id='mrocontent_txtDate']", ".");
			Driver.Click("//*[@id='mrocontent_cmdAdd']");

			//RequestStatus.roiLogCheck();
			//string amountDue = cyPayment + "";
			//if (cyPayment == null)
			//	{
			//	amountDue = ROIAdminLogChecks.roiamountdue();
			//	}
			//ROIAdminLogChecks.roiCheckNbr();
			//ROIAdminLogChecks.roiamountdue();
			//ROIAdminLogChecks.paymentamount(amountDue);
			//RequestStatus.roiLogCheck();
			//ClickYes();
			Driver.MROLogInfo("Complete MRO Payment of " + cyPayment);
		}



		public static void ValidateRequests(string sMessage, int nRequestID1, int nRequestID2)
		{
			Driver.MROSelectRoiAdmin("Profile", "QA Test");
			//Driver.Wait(TimeSpan.FromSeconds(5));
			Driver.FindElement(By.Id(PageElements.ROIAdminList.adminQATestP1)).Clear();
			Driver.FindElement(By.Id(PageElements.ROIAdminList.adminQATestP1)).SendKeys(nRequestID1 + "");
			Driver.FindElement(By.Id(PageElements.ROIAdminList.adminQATestP2)).Clear();
			Driver.FindElement(By.Id(PageElements.ROIAdminList.adminQATestP2)).SendKeys(nRequestID2 + "");
			Driver.FindElement(By.Id(PageElements.ROIAdminList.adminQATestP3)).Clear();

			Driver.ExecuteValidateSP("A1_CheckL1AcrossRequests", sMessage, nRequestID1, nRequestID2);
			Driver.ExecuteValidateSP("A1_CheckL2AcrossRequests", sMessage, nRequestID1, nRequestID2);
			Driver.ExecuteValidateSP("A1_CheckVertexAcrossRequests", sMessage, nRequestID1, nRequestID2);
		}

	}
}
