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
            driver.Navigate().GoToUrl(baseURL.TrimEnd(new char[] { '/' }) + LogonPage.URL);
            return GetInstance<LogonPage>(driver, baseURL, "");
        }
    }
}
