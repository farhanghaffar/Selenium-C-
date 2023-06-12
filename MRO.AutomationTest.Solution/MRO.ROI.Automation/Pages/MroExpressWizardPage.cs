using AventStack.ExtentReports;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MRO.ROI.Automation.Selenium;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;

namespace MRO.ROI.Automation.Pages
{
    public class MroExpressWizardPage
    {
        public RemoteWebDriver Driver;
        public ExtentTest logger;
        public TestContext Context;
        public MroExpressWizardPage(RemoteWebDriver driver, ExtentTest _loger, TestContext _context)
        {
            Driver = driver;
            logger = _loger;
            Context = _context;
        }

        public By get_Started = By.XPath("//span[@class='v-btn__content' and contains(text(),'Get Started')]");
        public By goodShephered_MedicalCenter = By.XPath("//button[@value='MROGoodShepherdMedicalCenter']");
        public By want_MymedicalRecords = By.XPath("//button[contains(text(),'want my medical')]");
        public By first_Name = By.XPath("//label[text()='FIRST NAME']/../input");
        public By last_Name = By.XPath("//label[text()='LAST NAME']/../input");
        public By middle_Name = By.XPath("//label[text()='MIDDLE NAME']/../input");
        public By next = By.XPath("//span[contains(text(),'Next')]");
        public By dateOfBirth = By.XPath("//input[starts-with(@id,'input-')]");
        public By Email = By.XPath("//label[text()='EMAIL']/../input");
        public By confirm_Email = By.XPath("//label[text()='CONFIRM EMAIL']/../input");
        public By send_EmailConfirmation_Checkbox = By.XPath("//label[@for='checkbox']");
        public By street = By.XPath("//label[text()='STREET']/../input");
        public By apartment_Building = By.XPath("//label[text()='APARTMENT/BUILDING']/../input");
        public By city = By.XPath("//label[text()='CITY']/../input");
        public By state = By.XPath("//label[text()='STATE']/../input");
        public By zip_Code = By.XPath("//label[text()='ZIP CODE']/../input");
        public By start_Date = By.XPath("//label[text()='START DATE']/../input");
        public By end_Date = By.XPath("//label[text()='END DATE']/../input");
        public By my_MedicalRecords = By.XPath("//label[contains(text(),'my medical records')]");
        public By sensitive_Information_CheckBx = By.XPath("//input[@type='checkbox']/following::label");
        public By patient_Requests = By.XPath("//label[text()='Patient Request']/following::label");
        public By my_Self = By.XPath("//button[contains(text(),'Myself')]");
        public By email = By.XPath("//label[text()='Email']");
        public By email_Vrf = By.XPath("//label[text()='EMAIL']/../input");
        public By confirm_Email_Vrf = By.XPath("//label[text()='CONFIRM EMAIL']/../input");
        public By on_Specific_Date = By.XPath("//label[text()='On specific date']");
        public By specify_Date = By.XPath("//label[text()='SPECIFY DATE']/..//input");
        public By have_DeadLine = By.XPath("//button[contains(text(),'have a deadline')]");
        public By text_Area = By.XPath("//label[text()='ADDITIONAL DETAILS']/../textarea");
        public By mobile_Number_Extension = By.XPath("//div[@class='v-select__slot']//i");
        public By extension_Number_Button = By.XPath(" //div[contains(text(),'+91')]");
        public By mobile_Number_TextBox = By.XPath("//label[text()='ENTER MOBILE NO']/..//input");
        public By skip = By.XPath("//span[contains(text(),'Skip')]");
        public By drivers_License = By.XPath("//label[text()='Drivers License']");
        public By ok_Button = By.XPath("//span[contains(text(),'Ok')]");
        public By attachment_Button = By.XPath("//span[@class='v-btn__content']//i[contains(@class,'attachment')]");
        public By document_Identity = By.XPath("//label[contains(text(),'IDENTITY DOCUMENT')]/..");
        public By save_Next_Button = By.XPath("//span[contains(text(),'Save & Next')]");
        public By review_Request_OkButton = By.XPath("//div[@class='v-card__text' and contains(text(),'Review')]/..//span[contains(text(),'Ok' )]");
        public By sign_Request_Button = By.XPath("//span[contains(text(),'Sign request')]");
        public By signature_Button = By.CssSelector("div#signature");
        public By add_Signature_Button = By.XPath("//button[contains(text(),'Add my signature')]");
        public By submit_Button = By.XPath("//span[contains(text(),'Submit Request')]");
        public By done_Button = By.XPath("//button[contains(text(),'done')]");
        public By rate_Us_Button = By.XPath("//span[contains(text(),'Rate Us')]");
        public By close_Button = By.XPath(" //span[contains(text(),'Close')]");

        public void SlectDate(string year, string date, string month)
        {
            try
            {
                IWebElement yearElement = Driver.FindElementBy(By.XPath($"//ul[@class='v-date-picker-years']//li[contains(text(),'{year}')]"));
                yearElement.Click();
                IWebElement monthElement = Driver.FindElementBy(By.XPath($"//*[@class='v-btn__content' and contains(text(),'{month}')]"));
                monthElement.Click();
                List<IWebElement> dateElements = Driver.FindElementsBy(By.XPath($"//td//button"));
                if (dateElements.Count > 0)
                {
                    for (int i = 0; i < dateElements.Count; i++)
                    {
                        if (dateElements[i].Text == date)
                        {
                            Driver.SleepTheThread(3);
                            dateElements[i].Click();
                            break;
                        }
                        dateElements = Driver.FindElementsBy(By.XPath($"//td//button"));
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to selct the date  Message : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }

        }

        public void EnableSensitiveInformation()
        {
            try
            {

                Driver.SleepTheThread(5);
                List<IWebElement> sensitive_Information_List = Driver.FindElementsBy(sensitive_Information_CheckBx);

                if (sensitive_Information_List.Count > 0)
                {
                    foreach (var checkBox in sensitive_Information_List)
                    {
                        checkBox.Click();
                        Driver.SleepTheThread(1);
                        sensitive_Information_List = Driver.FindElementsBy(sensitive_Information_CheckBx);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to enable sensitive information  Message : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }

        }

        public void SubitMROExpressServiceRequest()
        {
            try
            {
                Driver.DirectClick(get_Started);
                Driver.DirectClick(goodShephered_MedicalCenter);
                Driver.DirectClick(want_MymedicalRecords);
                Driver.SendKeys(first_Name, "FN-TestMRO");
                logger.Log(Status.Info, "First name entered as : FN-TestMRO");
                Driver.SendKeys(middle_Name, "MRO");
                logger.Log(Status.Info, "Middle name entered as : MRO");
                Driver.SendKeys(last_Name, "LN-MRO");
                logger.Log(Status.Info, "Last name entered as : LN-MRO");
                Driver.DirectClick(next);
                Driver.DirectClick(dateOfBirth);
                SlectDate("1991", "22", "Jan");
                Driver.SleepTheThread(4);
                Driver.DirectClick(next);
                Driver.SendKeys(Email, "mroautomationteam@gmail.com");
                Driver.SendKeys(confirm_Email, "mroautomationteam@gmail.com");
                logger.Log(Status.Info, "Email entered as : mroautomationteam@gmail.com");
                Driver.Click(send_EmailConfirmation_Checkbox);
                Driver.DirectClick(next);
                Driver.SendKeys(street, "CHRIS NISWANDEE");
                Driver.SendKeys(apartment_Building, "795 E DRAGRAM");
                Driver.SendKeys(city, "TUCSON");
                Driver.SendKeys(state, "AZ");
                Driver.SendKeys(zip_Code, "85705");
                Driver.DirectClick(next);
                Driver.DirectClick(start_Date);
                SlectDate("2021", "22", "Jan");
                Driver.DirectClick(end_Date);
                SlectDate("2021", "22", "Mar");
                Driver.DirectClick(next);
                Driver.DirectClick(my_MedicalRecords);
                Driver.DirectClick(next);
                EnableSensitiveInformation();
                Driver.DirectClick(next);
                Driver.DirectClick(patient_Requests);
                Driver.DirectClick(next);
                Driver.DirectClick(my_Self);
                Driver.DirectClick(next);
                Driver.DirectClick(email);
                string emailText = Driver.GetText(email_Vrf);
                string confirm_emailText = Driver.GetText(confirm_Email_Vrf);
                Driver.DirectClick(next);
                Driver.DirectClick(on_Specific_Date);
                Driver.DirectClick(specify_Date);
                string month = DateTime.Now.AddMonths(1).ToShortMonthName();
                string year = DateTime.Now.GetYear();
                SlectDate(year, "22", month);
                Driver.DirectClick(next);
                Driver.DirectClick(have_DeadLine);
                Driver.DirectClick(next);
                Driver.SendKeys(text_Area, "This a test for MRO express wizard");
                logger.Log(Status.Info, "Test area entered as : This a test for MRO express wizard");
                Driver.DirectClick(next);
                Driver.DirectClick(mobile_Number_Extension);
                Driver.DirectClick(extension_Number_Button);
                Driver.SendKeys(mobile_Number_TextBox, "6109947500");
                Driver.DirectClick(skip);
                Driver.DirectClick(drivers_License);
                Driver.DirectClick(next);
                Driver.DirectClick(ok_Button);
                Driver.DirectClick(attachment_Button);
                Driver.DirectClick(document_Identity);
                Driver.SleepTheThread(5);

                //autoit script
                string picturePath = Path.GetFullPath(Path.Combine(Assembly.GetExecutingAssembly().Location, "..", "..", "..", "..", "MRO.ROI.Automation\\", "Files\\", "SamplePicture.png"));
                string exeLocation = Path.GetFullPath(Path.Combine(Assembly.GetExecutingAssembly().Location, "..", "..", "..", "Utilities\\", "UploadPicture.exe"));
                Process.Start(exeLocation, picturePath);

                Driver.DirectClick(save_Next_Button);
                Driver.DirectClick(review_Request_OkButton);
                Driver.DirectClick(sign_Request_Button);
                Driver.DirectClick(signature_Button);
                Driver.DirectClick(add_Signature_Button);
                Driver.DirectClick(submit_Button);
                Driver.DirectClick(done_Button);
                Driver.DirectClick(rate_Us_Button);
                Driver.DirectClick(close_Button);

            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to create mro express request Message : {ex.Message} {Environment.NewLine} Whose StackTrace : {ex.StackTrace}");
            }
        }

    }
}

