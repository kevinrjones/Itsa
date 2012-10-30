using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using WebScenarios.PageLibrary.Base;

namespace WebScenarios.PageLibrary.Pages
{
    public class LogonPage : PageBase
    {
        public const string URL = "/";

        public override string DefaultTitle { get { return "Sign in to ItSa"; } }

        public IndexPage Logon(string userName, string password)
        {
            return GetInstance<IndexPage>();
        }



        [FindsBy(How = How.Id , Using = "UserName")]
        public IWebElement UserName { get; set; }

        [FindsBy(How = How.Id, Using = "Password")]
        public IWebElement Password { get; set; }

        [FindsBy(How = How.Id, Using = "signin")]
        public IWebElement Signin { get; set; }

        public void SetUserName(string username)
        {
            UserName.SendKeys(username);
        }

        public void SetPassword(string password)
        {
            Password.SendKeys(password);
        }

        public HomePage Logon()
        {
            Signin.Click();
            return GetInstance<HomePage>();
        }
    }
}
