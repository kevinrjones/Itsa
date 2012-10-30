using System;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.PageObjects;
using WebSiteSpecifications.PageLibrary.Base;

namespace WebScenarios.PageLibrary.Base
{
    public abstract partial class PageBase : CommonBase
    {
        public string BaseURL { get; set; }
        public virtual string DefaultTitle { get { return ""; } }

        protected TPage GetInstance<TPage>(RemoteWebDriver driver = null, string expectedTitle = "") where TPage : PageBase, new()
        {
            return GetInstance<TPage>(driver ?? Driver, BaseURL, expectedTitle);
        }

        protected static TPage GetInstance<TPage>(RemoteWebDriver driver, string baseUrl, string expectedTitle = "") where TPage : PageBase, new()
        {
            var pageInstance = new TPage()
            {
                Driver = driver,
                BaseURL = baseUrl
            };
            PageFactory.InitElements(driver, pageInstance);

            if (string.IsNullOrWhiteSpace(expectedTitle)) expectedTitle = pageInstance.DefaultTitle;

            //wait up to 5s for an actual page title since Selenium no longer waits for page to load after 2.21
            new OpenQA.Selenium.Support.UI.WebDriverWait(driver, TimeSpan.FromSeconds(5))
                .Until<IWebElement>((d) => d.FindElement(By.TagName("body")));

//            AssertIsEqual(expectedTitle, driver.Title, "Page Title");

            return pageInstance;
        }

        /// <summary>
        /// Asserts that the current page is of the given type
        /// </summary>
        public void Is<TPage>() where TPage : PageBase, new()
        {
            if (!(this is TPage))
            {
                throw new AssertionException(String.Format("Page Type Mismatch: Current page is not a '{0}'", typeof(TPage).Name));
            }
        }

        /// <summary>
        /// Inline cast to the given page type
        /// </summary>
        public TPage As<TPage>() where TPage : PageBase, new()
        {
            return (TPage)this;
        }
    }
}