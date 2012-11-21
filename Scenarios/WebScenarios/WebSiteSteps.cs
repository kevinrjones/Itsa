using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
            page.SetUserName("admin");
            page.SetPassword("letmein");
        }

        [When(@"I press logon")]
        public void WhenIPressLogon()
        {
            CurrentPage = CurrentPage.As<LogonPage>().Logon();
        }

        [Then(@"the ItsA home page should be shown")]
        public void ThenTheItSaHomePageShouldBeShown()
        {
            var page = CurrentPage.As<HomePage>();
            Thread.Sleep(1000);
            Assert.That(page.UserName.Text, Is.EqualTo("admin"));
        }

        [Given(@"I do not exist as a user")]
        public void GivenIDoNotExistAsAUser()
        {
            GivenIAmNotLoggedOn();
            ScenarioContext.Current.Pending();
        }

        [Given(@"I enter valid details into the registration form")]
        public void GivenIEnterValidDetailsIntoTheRegistrationForm()
        {
            ScenarioContext.Current.Pending();
        }

        [When(@"I press register")]
        public void WhenIPressRegister()
        {
            ScenarioContext.Current.Pending();
        }

        [When(@"I press unregister")]
        public void WhenIPressUnregister()
        {
            ScenarioContext.Current.Pending();
        }

        [Given(@"I exist as a user")]
        public void GivenIExistAsAUser()
        {
            ScenarioContext.Current.Pending();
        }

        [Then(@"I should be asked to confirm the deregistration")]
        public void ThenIShouldBeAskedToConfirmTheDeregistration()
        {
            ScenarioContext.Current.Pending();
        }
    }
}
