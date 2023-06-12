using AventStack.ExtentReports;
using AventStack.ExtentReports.MarkupUtils;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MRO.ROI.Automation.Common.Navigation;
using MRO.ROI.Automation.Pages;
using MRO.ROI.Automation.Pages.Common;
using MRO.ROI.Automation.Pages.ROIFacility;
using MRO.ROI.Automation.Selenium;
using MRO.ROI.Automation.Utility;
using MRO.ROI.Test.Utilities;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;

namespace MRO.ROI.Test
{
	class MRO_ROI_Ledger_Test
	{
		[TestClass]
		public class RoiAdminTest : ROITestBase
		{
			public RoiAdminTest() : base(ROITestArea.ROIAdmin)
			{

			}

			[TestMethod]
			//[TestCategory(ROITestCategory.Regression)]
			public void MRO_ROI_Ledger_Test1()
			{
				Driver.logger = Driver.extent.CreateTest("Ledger1_Two_Payments");
				MRO_ROI_Ledger_Test(null, false, false, true, false, true);
			}

			[TestMethod]
			//[TestCategory(ROITestCategory.Regression)]
			public void MRO_ROI_Ledger_Test2()
			{
				Driver.logger = Driver.extent.CreateTest("Ledger2_Two_Invoices");
				MRO_ROI_Ledger_Test("Test Attorney", false, true, false, true, false);
			}

			[TestMethod]
			//[TestCategory(ROITestCategory.Regression)]
			public void MRO_ROI_Ledger_Test3()
			{
				Driver.logger = Driver.extent.CreateTest("Ledger3_Req_Rqr_Invoice_Payment_Invoice_Payment");
				MRO_ROI_Ledger_Test("Test Attorney", false, true, true, true, true);
			}

			[TestMethod]
			//[TestCategory(ROITestCategory.Regression)]
			public void MRO_ROI_Ledger_Test4()
			{
				Driver.logger = Driver.extent.CreateTest("Ledger4_Exp_Rqr_Invoice_Payment_Invoice_Payment");
				MRO_ROI_Ledger_Test("TEST_Ex_Attorney", false, true, true, true, true);
			}

			[TestMethod]
			//[TestCategory(ROITestCategory.Regression)]
			public void MRO_ROI_Ledger_Test5()
			{
				Driver.logger = Driver.extent.CreateTest("Ledger5_PW_Rqr_Invoice_Payment_Invoice-_Payment");
				MRO_ROI_Ledger_Test("TEST_PW_Attorney", false, true, true, true, true);
			}

			[TestMethod]
			//[TestCategory(ROITestCategory.Regression)]
			public void MRO_ROI_Ledger_Test6()
			{
				Driver.logger = Driver.extent.CreateTest("Ledger6_Exp_Rqr_Invoice_Payment_Invoice+_Payment");
				MRO_ROI_Ledger_Test("TEST_Ex_Attorney", false, true, true, true, true, "Rate1-5", "Rate1-4");
			}


			public void MRO_ROI_Ledger_Test(string sRequester,
												bool bFAR,
												bool bInvoice1,
												bool bPayment1,
												bool bInvoice2,
												bool bPayment2,
												string sRate1 = "Rate1-4",
												string sRate2 = "Rate1-5",
												decimal cyPayment1 = 10,
												decimal cyPayment2 = 15,
												string sNewVersion = "ROI3-Ron",
												string sOldVersion = "ROI3-Dev"
												)
			{
				try
				{
					Driver.MROLogInfo("Login Complete");

					int nRequestID1 = MROFunctions.CreateRequestInVersion(sOldVersion, 5);
					int nRequestID2 = MROFunctions.CreateRequestInVersion(sNewVersion, 5);
					if (bInvoice1)
					{
						//MROFunctions.OverrideVersion(sOldVersion);
						Driver.SetVersion(sOldVersion);
						MROFunctions.AssignRequest(nRequestID1, sRequester);
						MROFunctions.SetRate(nRequestID1 + "", sRate1);
						MROFunctions.MROroilookupidadmin(nRequestID1 + "");
						RequestStatus.roiCreateInvoice();

						MROFunctions.OverrideVersion(sNewVersion);
						//Driver.SetVersion(sNewVersion);
						MROFunctions.AssignRequest(nRequestID2, sRequester);
						MROFunctions.SetRate(nRequestID2 + "", sRate1);
						ROIAdminFacalitiesListPage.roilookupidadmin(nRequestID2 + "");
						RequestStatus.roiCreateInvoice();
						MROFunctions.ValidateRequests("First Invoice", nRequestID1, nRequestID2);
					}
					if (bPayment1)
					{
						//MROFunctions.OverrideVersion(sOldVersion);
						Driver.SetVersion(sOldVersion);
						MROFunctions.AddMROPayment(nRequestID1, cyPayment1);

						MROFunctions.OverrideVersion(sNewVersion);
						MROFunctions.AddMROPayment(nRequestID2, cyPayment1);
						MROFunctions.ValidateRequests("First Payment", nRequestID1, nRequestID2);
					}

					if (bInvoice2)
					{
						//MROFunctions.OverrideVersion(sOldVersion);
						Driver.SetVersion(sOldVersion);
						MROFunctions.SetRate(nRequestID1 + "", sRate2);

						MROFunctions.OverrideVersion(sNewVersion);
						MROFunctions.SetRate(nRequestID2 + "", sRate2);
						MROFunctions.ValidateRequests("Second Invoice", nRequestID1, nRequestID2);
					}

					if (bPayment2)
					{
						//MROFunctions.OverrideVersion(sOldVersion);
						Driver.SetVersion(sOldVersion);
						MROFunctions.AddMROPayment(nRequestID1, cyPayment2);

						MROFunctions.OverrideVersion(sNewVersion);
						MROFunctions.AddMROPayment(nRequestID2, cyPayment2);
						MROFunctions.ValidateRequests("Second Payment", nRequestID1, nRequestID2);
					}

					Driver.logger.Pass("Ledger on Versions " + sOldVersion + " & " + sNewVersion + " are equal - test Passed");
				}
				catch (Exception ex)
				{
					Driver.takeScreenShot();
					Driver.MROLog(Status.Fail, "Test failed with exception"); //Logging fail
					Driver.logger.Log(Status.Error, MarkupHelper.CreateTable(
						new string[,]
						{
						{"Exception", ex.Message },
						{"StackTrace", ex.StackTrace }
						})); //Logging Error in a tabular format
					Assert.Fail(ex.Message);
				}
			}


		}

	}
}