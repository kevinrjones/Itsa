using System;
using NUnit.Framework;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium;
using System.Drawing.Imaging;

namespace WebScenarios.Fixtures
{
    public class TestFixtureBase
    {
        protected static RemoteWebDriver CurrentDriver { get; set; }

        [SetUp]
        public void Test_Setup()
        {
        }



        [TearDown]
        public void Test_Teardown()
        {
            try
            {
                if (TestContext.CurrentContext.Result.Status == TestStatus.Failed
                                && CurrentDriver is ITakesScreenshot)
                {
                    ((ITakesScreenshot)CurrentDriver).GetScreenshot().SaveAsFile(TestContext.CurrentContext.Test.FullName + ".jpg", ImageFormat.Jpeg);
                }
            }
            catch { }       // null ref exception occurs from accessing TestContext.CurrentContext.Result.Status property

        }

        public static void CreateDriver()
        {
            //CurrentDriver = new ChromeDriver();
            CurrentDriver = new FirefoxDriver();
            CurrentDriver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromMilliseconds(2000));
            //FirefoxBinary fb;
            //if (!String.IsNullOrWhiteSpace(Settings.CurrentSettings.FirefoxBinaryPath))
            //{
            //    fb = new FirefoxBinary(Settings.CurrentSettings.FirefoxBinaryPath);
            //}
            //else
            //{
            //    fb = new FirefoxBinary();
            //}
            //CurrentDriver = new FirefoxDriver(fb, new FirefoxProfile());
        }

        public static void TeardownDriver()
        {
            try
            {
                CurrentDriver.Quit();
            }
            catch { }
            
        }
    }

}