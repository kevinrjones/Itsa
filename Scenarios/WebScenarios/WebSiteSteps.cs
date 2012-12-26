using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using FluentAssertions;
using NUnit.Framework;
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
            page.CreateBlogPost.Displayed.Should().BeTrue();
        }

        [Given(@"I am a reader")]
        public void GivenIAmAReader()
        {
            GivenIAmNotLoggedOn();
        }

        [Given(@"There are posts available")]
        [Given(@"I browse to the home page")]
        public void GivenThereArePostsAvailable()
        {
            CurrentPage = PageBase.LoadHomePage(CurrentDriver, Settings.CurrentSettings.Url);            
        }

        [Then(@"I can read the latest posts")]
        public void ThenICanReadTheLatestPosts()
        {
            ScenarioContext.Current.Pending();   
        }

        [Then(@"the add blog post button is not visible")]
        public void ThenTheAddBlogPostButtonIsNotVisible()
        {
            Thread.Sleep(100);
            var page = CurrentPage.As<HomePage>();
            page.AddBlogPost.Displayed.Should().Be(false);
        }
    }
}
