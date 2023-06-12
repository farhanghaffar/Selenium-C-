using Microsoft.VisualStudio.TestTools.UnitTesting;
using MRO.ROI.Automation.Pages.Common;
using MRO.ROI.Automation.Selenium;
using OpenQA.Selenium;
using System;
using System.IO;
using System.Reflection;
using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Chrome;
using AventStack.ExtentReports.Reporter.Configuration;
using OpenQA.Selenium.Firefox;
using System.Net.Mail;
using System.Net;
using OpenQA.Selenium.IE;
using AventStack.ExtentReports.MarkupUtils;
using System.Security.Principal;
using MRO.ROI.Automation;
using MRO.ROI.Automation.Utility;

//[assembly: Parallelize(Workers = 3, Scope = ExecutionScope.MethodLevel)]

namespace MRO.ROI.Test.Utilities
{
	[TestClass]
	public class ROIBaseTest
	{
		public static string createdDateTime = DateTime.Now.ToString("yyyy-MM-dd_hh-mm-ss-tt");
		public static ExtentReports extent = new ExtentReports();
		public static ExtentHtmlReporter extenthtml = GetHtmlReporter();
		public ExtentTest logger;
		public static string projectPath;
		public static string reportPath;
		public static string BaseAddress = string.Empty;
		public static string ReportsLocation = string.Empty;
		public ROITestArea _testArea;
		public static string hqiisstgURL = string.Empty;
		public TestContext TestContext { get; set; }
		public static string iniFileBaseLocation = string.Empty;
		public RemoteWebDriver driver = null;
        private ROITestArea rOIAdmin;

        public ROIBaseTest(ROITestArea testArea)
		{
			_testArea = testArea;
		}
        [TestInitialize]
		public void SetupTests()
		{
				BaseAddress = TestContext.Properties["webAppUrl"].ToString();
				ReportsLocation = TestContext.Properties["ReportsLocation"].ToString();
				iniFileBaseLocation = TestContext.Properties["IniFileBaseLocation"].ToString();
				hqiisstgURL = TestContext.Properties["hqiisstgwebAppUrl"].ToString();
		}

		[AssemblyInitialize]
		public static void InitailizeReport(TestContext testcontexts)
        {
			extent.AttachReporter(extenthtml);
		}

		[AssemblyCleanup]
		public static void CleanResourcesAndSendEmail()
		{
			try
			{
				extent.Flush();
				string sourcePath = $"{Path.GetFullPath(Path.Combine(Assembly.GetExecutingAssembly().Location, "..", "..", "..", "Reports\\"))}";
				// UnzipFiles.ZipFileAndCopyToShareLocation(sourcePath, ReportsLocation);
				//CopyFilesRecursively(sourcePath, ReportsLocation);
				//SendReportThroughEmail();

			}
			catch (Exception ex)
			{
				throw new Exception($"There is a issue occured while assembly cleanup whose exception message is: {ex.Message} {Environment.NewLine} Whose stackTrace is: {ex.StackTrace}");
			}

		}

		public void Cleanup(RemoteWebDriver driver)
		{
			try
			{
				driver.Close();
				driver.Quit();
				
			}
			catch (Exception ex)
			{
			}
		}

		public void LogException(RemoteWebDriver driver, ExtentTest logger, Exception ex)
		{

			logger.Log(Status.Fail, "Test failed with exception", TakeScreenShotAtStep());
			logger.Log(Status.Error, MarkupHelper.CreateTable(
				new string[,]
				{
						{"Exception", ex.Message },
						{"StackTrace", ex.StackTrace }
				}));
			Cleanup(driver);
			Assert.Fail(ex.ToString());
		}

		public void LogExceptionForManualExecution(ExtentTest logger, string message)
		{

			logger.Log(Status.Skip, "Test skipped for manual execution", TakeScreenShotAtStep());
			logger.Log(Status.Skip, MarkupHelper.CreateTable(
				new string[,]
				{
						{"Message",  message}
				}));
		}

		public MediaEntityModelProvider TakeScreenShotAtStep()
		{
			try
			{
				return MediaEntityBuilder.CreateScreenCaptureFromBase64String(ReturnScreenShotLocation()).Build();

			}
			catch (Exception ex)
			{
				throw new Exception($"Failed to take screenshot at step Message is: {ex.Message} {Environment.NewLine} Whose stackTrace is: {ex.StackTrace}");
			}
		}
		public RemoteWebDriver Init(string browser)
		{
			try
			{
				string username = string.Empty;
				string password = string.Empty;
				string driverPath = TestContext.Properties["driverPath"].ToString();

				if (string.IsNullOrEmpty(driverPath))
					driverPath = Path.GetFullPath(Path.Combine(Assembly.GetExecutingAssembly().Location, "..", "..", "..", "Utilities"));

				

				switch (browser)
				{
					case "Chrome":
						string path = Path.GetPathRoot(Environment.SystemDirectory);
						var chromeoptions = new ChromeOptions();
						chromeoptions.AddArguments("--start-maximized");
						chromeoptions.AddArgument("no-sandbox");
						chromeoptions.PageLoadStrategy = PageLoadStrategy.Eager;
						driver = new ChromeDriver(ChromeDriverService.CreateDefaultService(), chromeoptions, TimeSpan.FromMinutes(10));
						driver.Manage().Cookies.DeleteAllCookies();
						driver.Manage().Timeouts().PageLoad.Add(System.TimeSpan.FromSeconds(300));
						break;

					case "FireFox":
						driver = new FirefoxDriver();
						driver.Wait(TimeSpan.FromSeconds(2));
						driver.Manage().Window.Maximize();
						break;

					case "IE":
						InternetExplorerOptions options = new InternetExplorerOptions();
						options.IntroduceInstabilityByIgnoringProtectedModeSettings = true;
						options.EnsureCleanSession = true;
						driver = new InternetExplorerDriver();
						driver.Manage().Window.Maximize();
						break;

					default:
						var _chromeoptions = new ChromeOptions();
						_chromeoptions.AddArgument("no-sandbox");
						driver = new ChromeDriver(_chromeoptions);
						driver.Manage().Cookies.DeleteAllCookies();
						driver.Manage().Timeouts().PageLoad.Add(System.TimeSpan.FromSeconds(180));
						driver.Manage().Window.Maximize();
						break;
				}

				LoginPage loginPage = new LoginPage(driver, logger, TestContext);
				LoginCommand command = new LoginCommand(driver, logger);

				switch (_testArea)
				{
					case ROITestArea.ROIFacility:
						GetCredentials("facility", out username, out password);
						loginPage.GoToROIFaclityLoginPage(BaseAddress);
						loginPage.Login(username, password);
						driver.WaitUntilDOMLoaded();
						break;
					case ROITestArea.ROIAdmin:
						GetCredentials("admin", out username, out password);
						loginPage.GoToROIAdminLoginPage(BaseAddress);
						loginPage.Login(username, password);
						driver.WaitUntilDOMLoaded();
						break;
					case ROITestArea.ROIExternalRequesterPortal:
						GetCredentials("rqrPortal", out username, out password);
						loginPage.GoToROIExternalRequesterPortal(BaseAddress);
						loginPage.Login(username, password);
						driver.WaitUntilDOMLoaded();
						break;
					case ROITestArea.ROIInternalRequesterPortal:
						GetCredentials("intPortal", out username, out password);
						loginPage.GoToROIInternalRequesterPortal(BaseAddress);
						loginPage.Login(username, password);
						driver.WaitUntilDOMLoaded();
						break;
					case ROITestArea.IronMountainROIFacility:
						GetCredentials("ironmountain", out username, out password);
						loginPage.GoToIronMountainROIFaclityLoginPage(BaseAddress);
						loginPage.Login(username, password);
						driver.WaitUntilDOMLoaded();
						break;
					case ROITestArea.ROIAdminTwo:
						GetCredentials("adminTwo", out username, out password);
						loginPage.GoToROIAdminLoginPage(BaseAddress);
						loginPage.Login(username, password);
						driver.WaitUntilDOMLoaded();
						break;
					case ROITestArea.QbReport:
						GetCredentials("QbReport", out username, out password);
						loginPage.GoToROIAdminLoginPage(BaseAddress);
						loginPage.Login(username, password);
						driver.WaitUntilDOMLoaded();
						break;
					case ROITestArea.ROIAdminBOE:
						GetCredentials("boeAdmin", out username, out password);
						loginPage.GoToROIAdminLoginPage(BaseAddress);
						loginPage.Login(username, password);
						driver.WaitUntilDOMLoaded();
						break;
					case ROITestArea.ROIAdminhqiisstg:
						GetCredentials("admin", out username, out password);
						loginPage.GoToROIAdminhqiisstgLoginPage(hqiisstgURL);
						loginPage.Login(username, password);
						driver.WaitUntilDOMLoaded();
						break;
					case ROITestArea.ROITestFacility:
						GetCredentials("roiFacility", out username, out password);
						loginPage.GoToROITestFaclityLoginPage(BaseAddress);
						loginPage.Login(username, password);
						driver.WaitUntilDOMLoaded();
						break;
					case ROITestArea.TestingFacility:
						GetCredentials("dummyFacility", out username, out password);
						loginPage.GoToROITestFaclityLoginPage(BaseAddress);
						loginPage.Login(username, password);
						driver.WaitUntilDOMLoaded();
						break;

					case ROITestArea.CBOTestFacility:
						GetCredentials("cbo", out username, out password);
						loginPage.GoToROITestFaclityLoginPage(BaseAddress);
						loginPage.Login(username, password);
						driver.WaitUntilDOMLoaded();
						break;
					default:
						break;
				}
			}
			catch (Exception ex)
			{			
				logger.Log(Status.Fail, "Test failed with exception", TakeScreenShotAtStep());
				logger.Log(Status.Error, MarkupHelper.CreateTable(
					new string[,]
					{
						{"Exception", ex.Message },
						{"StackTrace", ex.StackTrace }
					}));
				
				Assert.Fail(ex.ToString());
				Cleanup(driver);
				throw new Exception($"Failed to initilize driver whose exception message is: {ex.Message} {Environment.NewLine} Whose stackTrace is: {ex.StackTrace}");
			}

			return driver;
		}

		public RemoteWebDriver NavigateToMROExpressURL()
        {
            try
            {
				string path = Path.GetPathRoot(Environment.SystemDirectory);
				var chromeoptions = new ChromeOptions();
				chromeoptions.AddArguments("--start-maximized");
				chromeoptions.AddArgument("no-sandbox");
				chromeoptions.PageLoadStrategy = PageLoadStrategy.Eager;
				driver = new ChromeDriver(ChromeDriverService.CreateDefaultService(), chromeoptions, TimeSpan.FromMinutes(10));
				driver.Manage().Cookies.DeleteAllCookies();
				driver.Manage().Timeouts().PageLoad.Add(System.TimeSpan.FromSeconds(300));
				driver.Navigate().GoToUrl(TestContext.Properties["MroExpressUrl"].ToString());
				driver.SleepTheThread(5);
			}
            catch (Exception ex)
            {
				throw new Exception($"Failed to login to mroexpress wizard page whose exception message is: {ex.Message} {Environment.NewLine} Whose stackTrace is: {ex.StackTrace}");
			}

			return driver;
        }

		public static void CopyFilesRecursively(string sourcePath, string targetPath)
		{
			try
			{
				foreach (string dirPath in Directory.GetDirectories(sourcePath, "*.zip", SearchOption.AllDirectories))
				{
					Directory.CreateDirectory(dirPath.Replace(sourcePath, targetPath));
				}
				foreach (string newPath in Directory.GetFiles(sourcePath, "*.*", SearchOption.AllDirectories))
				{
					File.Copy(newPath, newPath.Replace(sourcePath, targetPath), true);
				}
			}
			catch (Exception ex)
			{
				throw new Exception($"Failed to copy files to shared location, whose exception message is: {ex.Message} {Environment.NewLine} Whose stackTrace is: {ex.StackTrace}");
			}

		}

		public static void SendReportThroughEmail()
		{
			try
			{
				string index = $"{Path.GetFullPath(Path.Combine(Assembly.GetExecutingAssembly().Location, "..", "..", "..", "Reports\\"))}" + createdDateTime + @"\index.html";
				var fromAddress = new MailAddress("mroautomationteam@gmail.com", "MRO Automation Team");
				string[] multiple = { "cigniti-mro@cigniti.com" };
				const string fromPassword = "Cigniti@1234";
				const string subject = "Automation Report";
				const string body = @"
Hi Team,

An automation pipeline has triggered and regression tests got executed and report has generated.

Kindly refer to the attachments for pass or fail report.

Warm Regards,
MRO Automation Team
";
				MailMessage message = new MailMessage()
				{
					Subject = subject,
					Body = body
				};
				message.From = fromAddress;
				foreach (string multiple_email in multiple)
				{
					message.To.Add(new MailAddress(multiple_email));
				}
				message.Attachments.Add(new Attachment(index));

				var smtp = new SmtpClient
				{
					Host = "smtp.gmail.com",
					Port = 587,
					EnableSsl = true,
					DeliveryMethod = SmtpDeliveryMethod.Network,
					UseDefaultCredentials = false,
					Credentials = new NetworkCredential(fromAddress.Address, fromPassword),
				};

				smtp.Send(message);
			}
			catch (Exception ex)
			{
				throw new Exception($"Failed  to send email whose exception message is: {ex.Message} {Environment.NewLine} Whose stackTrace is: {ex.StackTrace}");
			}
		}

		public void GetCredentials(string area, out string username, out string password)
		{

			try
			{
				if (TestContext != null)
				{
					username = TestContext.Properties[area + "UserName"].ToString();
					password = TestContext.Properties[area + "Password"].ToString();
				}
				else
				{
					switch (area)
					{
						case "facility":
							username = "akothuri";
							password = "TestingMRO@123";
							break;
						case "admin":
							username = "cigniti-akothuri";
							password = "TestingMRO@123";
							break;
						case "rqrPortal":
							username = "akothuri";
							password = "";
							break;
						case "intPortal":
							username = "int-seleniumautomation";
							password = "Testmro02$";
							break;
						case "ironmountain":
							username = "irmtmirza";
							password = "Abdul2007$";
							break;
						case "boeAdmin":
							username = "cigniti-mvadkapur";
							password = "Mammu@123";
							break;
						case "roiFacility":
							username = "Akothuri";
							password = "TestingMRO@123";
							break;
						case "cbo":
							username = "CBO-akothuri";
							password = "TestingMRO@123";
							break;
						default:
							username = "seleniumautomation";
							password = "Testauto1$";
							break;
					}
				}
			}
			catch (Exception ex)
			{

				throw new Exception($"Failed  to get credentials whose exception message is: {ex.Message} {Environment.NewLine} Whose stackTrace is: {ex.StackTrace}");
			}

		}
		public static ExtentHtmlReporter GetHtmlReporter()
		{
			ExtentHtmlReporter htmlReporter = null;
			try
			{
				string path = $"{Path.GetFullPath(Path.Combine(Assembly.GetExecutingAssembly().Location, "..", "..", "..", "Reports\\"))}" + createdDateTime + @"\MyOwnReport.html";
				htmlReporter = new ExtentHtmlReporter(path);
				htmlReporter.Config.Encoding = "UTF-8";
				htmlReporter.Config.Theme = Theme.Standard;
				htmlReporter.Config.DocumentTitle = "UI Test Automation Report";
				htmlReporter.Config.ReportName = "UI Test Automation";
				htmlReporter.Config.EnableTimeline = true;
				htmlReporter.Config.CSS = "canvas {height: 250px;}";
			}
			catch (Exception ex)
			{

				throw new Exception($"Failed to reurn screenshot with message : {ex.Message} {Environment.NewLine} Whose stacktrace : {ex.StackTrace}");
			}

			return htmlReporter;
		}

		public string ReturnScreenShotLocation()
		{
			string reportPath = String.Empty;
			string testCaseName = TestContext.TestName;
			Random rand = new Random();
			int value = rand.Next(1, 100000);

			try
			{
				reportPath = Path.GetFullPath(Path.Combine(Assembly.GetExecutingAssembly().Location, "..", "..", "..", "Reports\\")) + $"{createdDateTime}\\" + value + ".png";
				ITakesScreenshot ts = (ITakesScreenshot)driver;
				driver.SwitchToAlert();
				driver.SleepTheThread(1);
				Screenshot screenshot = ts.GetScreenshot();
				string pth = System.Reflection.Assembly.GetCallingAssembly().CodeBase;
				string localpath = new Uri(reportPath).LocalPath;
				screenshot.SaveAsFile(localpath, OpenQA.Selenium.ScreenshotImageFormat.Png);
				string imgFormat = ReturnBase64FormatImageString(localpath);
				return imgFormat;
			}
			catch (Exception ex)
			{
				throw new Exception($"Failed to reurn screenshot with message : {ex.Message} {Environment.NewLine} Whose stacktrace : {ex.StackTrace}");
			}
		}

		public string ReturnBase64FormatImageString(string screenshotPath)
		{
			string imgFormat;
			try
			{
				using (System.Drawing.Image image = System.Drawing.Image.FromFile(screenshotPath))
				{
					using (MemoryStream m = new MemoryStream())
					{
						string base64String;
						image.Save(m, image.RawFormat);
						byte[] imageBytes = m.ToArray();
						base64String = Convert.ToBase64String(imageBytes);
						imgFormat = "data:image/png;base64, " + base64String;
					}
				}
			}
			catch (Exception)
			{
				throw;
			}
			return imgFormat;
		}

	}

	public static class ROITestCategory
	{
		public const string BuildVerification = "BVT";
		public const string Regression = "REGRESSION";
		public const string RoundOne = "RoundOne";
		public const string Development = "Development";
		public const string Passed = "Passed";
	}


	public enum ROITestArea
	{
		ChartOnline,
		FilingCabinetOnline,
		MROAdmin,
		ParentBusinessAdmin,
		PatientPortal,
		ROIAdmin,
		ROIFacility,
		IronMountainROIFacility,
		ROIExternalRequesterPortal,
		ROIInternalRequesterPortal,
		ROITracker,
		MRO_ROI_Rate_Test,
		Reg_Outstanding_SubpoenasTest,
		ROIAdminTwo,
		QbReport,
		ROIAdminBOE,
		ROIAdminhqiisstg,
		ROITestFacility,
		TestingFacility,
		CBOTestFacility

	}

	public enum Browser
	{
		Chrome,
		FireFox,
		IE
	}
}

