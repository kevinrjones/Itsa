using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using WebScenarios.PageLibrary.Base;

namespace WebScenarios.PageLibrary.Pages
{
    public class RegistrationPage : PageBase
    {
        public const string URL = "/user/new";

        public override string DefaultTitle { get { return "Register"; } }

        public IndexPage Logon(string userName, string password)
        {
            return GetInstance<IndexPage>();
        }

        [FindsBy(How = How.Id , Using = "UserName")]
        public IWebElement UserName { get; set; }

        [FindsBy(How = How.Id, Using = "Password")]
        public IWebElement Password { get; set; }

        [FindsBy(How = How.Id, Using = "RepeatPassword")]
        public IWebElement RepeatPassword { get; set; }

        [FindsBy(How = How.Id, Using = "Email")]
        public IWebElement EMail { get; set; }

        [FindsBy(How = How.Id, Using = "register")]
        public IWebElement RegisterLink { get; set; }

        [FindsBy(How = How.Id, Using = "create")]
        public IWebElement RegisterButton { get; set; }

        public void SetUserName(string username)
        {
            UserName.SendKeys(username);
        }

        public void SetPassword(string password)
        {
            Password.SendKeys(password);
        }

        public void SetRepeatPassword(string password)
        {
            RepeatPassword.SendKeys(password);
        }

        public void SetEMail(string email)
        {
            EMail.SendKeys(email);
        }

        public UserPage RegisterUser()
        {
            Thread.Sleep(100);
            RegisterButton.Click();
            return GetInstance<UserPage>();
        }

        public UserPage RegisterUser(string name, string password, string email)
        {
            SetUserName(name);
            SetPassword(password);
            SetRepeatPassword(password);
            SetEMail(email);
            return RegisterUser();
        }

    }
}
