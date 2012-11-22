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

        [FindsBy(How = How.LinkText, Using = "Deregister")]
        public IWebElement Unregister { get; set; }

        [FindsBy(How = How.Id, Using = "deregister")]
        public IWebElement UnRegister { get; set; }

        [FindsBy(How = How.Id, Using = "logout")]
        public IWebElement Logout { get; set; }

        public PageBase UnRegisterUser()
        {
            UnRegister.Click();
            return GetInstance<RegistrationPage>();
        }

        public PageBase LogoutUser()
        {
            Logout.Click();
            return GetInstance<LogonPage>();
        }
    }
}