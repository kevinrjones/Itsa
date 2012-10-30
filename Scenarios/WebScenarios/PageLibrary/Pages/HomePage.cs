using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using WebScenarios.PageLibrary.Base;
using WebSiteSpecifications.PageLibrary.Base;

namespace WebScenarios.PageLibrary.Pages
{
    public class HomePage : PageBase
    {
        [FindsBy(How = How.Id, Using = "username")]
        public IWebElement UserName { get; set; }
    }
}