using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using WebScenarios.PageLibrary.Base;

namespace WebScenarios.PageLibrary.Pages
{
    public class HomePage : Base.PageBase
    {
        public static string URL = "";
        public override string DefaultTitle { get { return ""; } }

        [FindsBy(How = How.Id, Using = "username")]
        public IWebElement UserName { get; set; }

        [FindsBy(How = How.LinkText, Using = "Deregister")]
        public IWebElement Unregister { get; set; }

        [FindsBy(How = How.Id, Using = "deregister")]
        public IWebElement UnRegister { get; set; }

        [FindsBy(How = How.Id, Using = "logout")]
        public IWebElement Logout { get; set; }

        [FindsBy(How = How.Id, Using = "addBlogPost")]
        public IWebElement AddBlogPost { get; set; }

        [FindsBy(How = How.Id, Using = "createBlogPostPanel")]
        public IWebElement CreateBlogPostPanel { get; set; }

        [FindsBy(How = How.Id, Using = "createBlogPost")]
        public IWebElement CreateBlogPost { get; set; }

        [FindsBy(How = How.CssSelector, Using = "ul#posts li:nth-of-type(1) input")]
        public IWebElement DeleteBlogPost { get; set; }

        [FindsBy(How = How.Id, Using = "message")]
        public IWebElement Message { get; set; }

        [FindsBy(How = How.Id, Using = "postTitle")]
        public IWebElement Title { get; set; }

        [FindsBy(How = How.Id, Using = "postBody")]
        public IWebElement Body { get; set; }

        [FindsBy(How = How.Id, Using = "posts")]
        public IWebElement BlogPosts { get; set; }

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