using System;
using FluentAutomation;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using TechTalk.SpecFlow;

namespace WebScenarios.Steps
{
    [Binding]
    public class StepsBase
    {
        protected static IWebDriver Driver;

        [BeforeTestRun]
        public static void BeforeTestRun()
        {
            Win32Magic.MinimizeAllWindows();
        }

        [AfterTestRun]
        public static void AfterTestRun()
        {
            Win32Magic.RestoreAllWindows();
        }

        public StepsBase()
        {
        }

        [BeforeFeature]
        public static void BeforeFeature()
        {
            Driver = new FirefoxDriver();
            var size = Driver.Manage().Window.Size;
            size.Width = 1000;
            Driver.Manage().Timeouts().SetPageLoadTimeout(TimeSpan.FromSeconds(20));
            Driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(5));
            Driver.Manage().Window.Size = size;
            
            //SeleniumWebDriver.Bootstrap();
        }

        [AfterFeature()]
        public static void AfterFeature()
        {
            Driver.Quit();
        }
    }
}