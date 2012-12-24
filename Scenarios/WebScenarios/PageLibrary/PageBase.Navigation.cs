using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.PageObjects;
using WebScenarios.PageLibrary.Pages;

// ReSharper disable CheckNamespace
namespace WebScenarios.PageLibrary.Base
// ReSharper restore CheckNamespace
{
    public abstract partial class PageBase
    {
        public static LogonPage LoadLogonPage(RemoteWebDriver driver, string baseURL)
        {
            driver.Navigate().GoToUrl(baseURL.TrimEnd(new[] { '/' }) + LogonPage.URL);
            if (driver.Url.EndsWith("User/New"))
            {
                var registrationPage = GetInstance<RegistrationPage>(driver, baseURL, "");
                var page = registrationPage.RegisterUser(LogonPage.LogonUserName, LogonPage.LogonPassword, LogonPage.RegisterEmail);
                page.LogoutUser();
            }
            return GetInstance<LogonPage>(driver, baseURL, "");
        }

        public static RegistrationPage LoadRegisterPage(RemoteWebDriver driver, string baseURL)
        {
            driver.Navigate().GoToUrl(baseURL.TrimEnd(new[] { '/' }) + RegistrationPage.URL);
            if(!driver.Url.EndsWith(RegistrationPage.URL))
            {
                var logonPage = GetInstance<LogonPage>(driver, baseURL, "");
                logonPage.Logon(LogonPage.LogonUserName, LogonPage.LogonPassword);
                //IWebElement logout = driver.FindElementById("logout");
                //logout.Click();
                IWebElement derigester = driver.FindElementById("deregister");
                derigester.Click();
            }
            return GetInstance<RegistrationPage>(driver, baseURL, "");
        }
    }
}
