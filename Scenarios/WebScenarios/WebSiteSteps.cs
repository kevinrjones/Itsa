using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using FluentAssertions;
using NUnit.Framework;
using OpenQA.Selenium;
using TechTalk.SpecFlow;
using WebScenarios.PageLibrary.Base;
using WebScenarios.PageLibrary.Pages;
using WebScenarios.Setup;
using WebSiteSpecifications.Fixtures;

namespace WebScenarios
{
    [Binding]
    public class WebSiteSteps : FeaturesBase
    {

        // For additional details on SpecFlow step definitions see http://go.specflow.org/doc-stepdef
        [Given(@"I am not logged on")]
        public void GivenIAmNotLoggedOn()
        {
            CurrentDriver.Manage().Cookies.DeleteAllCookies();
        }

        [Given(@"I enter valid credentials into the logon form")]
        public void GivenIEnterValidCredentialsIntoTheLogonForm()
        {
            CurrentPage = PageBase.LoadLogonPage(CurrentDriver, Settings.CurrentSettings.Url);
            var page = CurrentPage.As<LogonPage>();
            page.SetUserName(LogonPage.LogonUserName);
            page.SetPassword(LogonPage.LogonPassword);
        }

        [When(@"I press logon")]
        public void WhenIPressLogon()
        {
            CurrentPage = CurrentPage.As<LogonPage>().Logon();
        }

        [Then(@"the ItsA user page should be shown")]
        public void ThenTheItSaUserPageShouldBeShown()
        {
            var page = CurrentPage.As<HomePage>();
            Thread.Sleep(1000);
            Assert.That(page.UserName.Text, Is.EqualTo(LogonPage.LogonUserName));
        }

        [Given(@"I do not exist as a user")]
        public void GivenIDoNotExistAsAUser()
        {
            CurrentPage = PageBase.LoadRegisterPage(CurrentDriver, Settings.CurrentSettings.Url);
        }

        [Given(@"I enter valid details into the registration form")]
        public void GivenIEnterValidDetailsIntoTheRegistrationForm()
        {
            var page = CurrentPage.As<RegistrationPage>();
            page.SetUserName(LogonPage.LogonUserName);
            page.SetPassword(LogonPage.LogonPassword);
            page.SetRepeatPassword(LogonPage.LogonPassword);
            page.SetEMail(LogonPage.RegisterEmail);
        }

        [When(@"I press register")]
        public void WhenIPressRegister()
        {
            CurrentPage = CurrentPage.As<RegistrationPage>().RegisterUser();
        }

        [Given(@"I am registered")]
        public void GivenIAmRegistered()
        {
            CurrentPage = PageBase.LoadLogonPage(CurrentDriver, Settings.CurrentSettings.Url);
        }

        [Given(@"I am logged on")]
        public void GivenIAmLoggedOn()
        {
            GivenIEnterValidCredentialsIntoTheLogonForm();
            WhenIPressLogon();
        }

        [When(@"I press unregister")]
        public void WhenIPressUnregister()
        {
            CurrentPage = CurrentPage.As<HomePage>().UnRegisterUser();
        }
        [Then(@"I should be asked to confirm the deregistration")]
        public void ThenIShouldBeAskedToConfirmTheDeregistration()
        {
            ScenarioContext.Current.Pending();
        }

        [Then(@"the ItsA register page should be shown")]
        public void ThenTheItsARegisterPageShouldBeShown()
        {
            var page = CurrentPage.As<RegistrationPage>();
            Assert.That(page.RegisterLink.Text, Is.EqualTo("Register"));
        }


        [When(@"I press add blog post")]
        [Given(@"I press add blog post")]
        public void WhenIPressAddBlogPost()
        {
            var page = CurrentPage.As<HomePage>();
            page.AddBlogPost.Displayed.Should().Be(true);
            page.AddBlogPost.Click();
        }

        [Then(@"the new blog post screen show be shown")]
        public void ThenTheNewBlogPostScreenShowBeShown()
        {
            var page = CurrentPage.As<HomePage>();
            Thread.Sleep(100);
            page.CreateBlogPostPanel.Displayed.Should().BeTrue();
        }

        [Given(@"I am a reader")]
        public void GivenIAmAReader()
        {
            GivenIAmNotLoggedOn();
        }

        [Given(@"I browse to the home page")]
        public void GivenThereArePostsAvailable()
        {
            CurrentPage = PageBase.LoadHomePage(CurrentDriver, Settings.CurrentSettings.Url);            
        }

        public void AddBlogPost()
        {
        }

        [Then(@"I can read the latest posts")]
        public void ThenICanReadTheLatestPosts()
        {
            var page = CurrentPage.As<HomePage>();
            Thread.Sleep(100);
            var elems = page.BlogPosts.FindElements(By.CssSelector("ul#posts li"));
            elems.Count.Should().NotBe(0);

        }

        [Then(@"the add blog post button is not visible")]
        public void ThenTheAddBlogPostButtonIsNotVisible()
        {
            Thread.Sleep(100);
            var page = CurrentPage.As<HomePage>();
            page.AddBlogPost.Displayed.Should().Be(false);
        }

        [Given(@"I enter and title and body")]
        public void GivenIEnterAndTitleAndBody()
        {
            var page = CurrentPage.As<HomePage>();
            Thread.Sleep(1000);
            page.Title.SendKeys("title");
            page.Body.SendKeys("body");
        }

        [When(@"I add the post")]
        public void WhenIAddThePost()
        {
            var page = CurrentPage.As<HomePage>();
            page.CreateBlogPost.Click();
        }

        [Then(@"the post should be visible")]
        public void ThenThePostShouldBeVisible()
        {
            var page = CurrentPage.As<HomePage>();
            Thread.Sleep(100);
            page.Message.Text.Should().Be("Post added");
            var elems = page.BlogPosts.FindElements(By.CssSelector("ul#posts li"));
            elems.Count.Should().NotBe(0);
        }


        [When(@"I click the delete post element")]
        public void WhenIClickTheDeletePostElement()
        {
            var page = CurrentPage.As<HomePage>();
            Thread.Sleep(100);
            page.DeleteBlogPost.Click();
        }

        [Then(@"the delete blog post element show be shown")]
        public void ThenTheDeleteBlogPostElementShowBeShown()
        {
            var page = CurrentPage.As<HomePage>();
            page.DeleteBlogPost.Displayed.Should().BeTrue();
        }

        [Then(@"the post should deleted")]
        public void ThenThePostShouldDeleted()
        {
            var page = CurrentPage.As<HomePage>();
            Thread.Sleep(100);
            page.Message.Text.Should().Be("Post deleted");
            var elems = page.BlogPosts.FindElements(By.CssSelector("ul#posts li"));
            elems.Count.Should().Be(0);
        }

        [BeforeScenario("addpost")]
        public void AddPost()
        {
            GivenIAmLoggedOn();
            WhenIPressAddBlogPost();
            GivenIEnterAndTitleAndBody();
            WhenIAddThePost();
        }

        [AfterScenario("deletepost")]
        public void RemovePost()
        {
            GivenIAmLoggedOn();
            WhenIClickTheDeletePostElement();
        }

        [Then(@"the delete blog post button is not visible")]
        public void ThenTheDeleteBlogPostButtonIsNotVisible()
        {
            var page = CurrentPage.As<HomePage>();
            Thread.Sleep(200);
            page.DeleteBlogPost.Displayed.Should().BeFalse();
        }
    }
}
