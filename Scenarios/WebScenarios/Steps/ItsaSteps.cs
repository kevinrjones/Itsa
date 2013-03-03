using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAutomation.Interfaces;
using OpenQA.Selenium;
using TechTalk.SpecFlow;

namespace WebScenarios.Steps
{
    [Binding]
    class ItsaSteps : StepsBase
    {
        private void Login()
        {
            
            //Func<IElement> elem = I.Find("a[href=signin]");
            IWebElement dropDown = Driver.FindElement(By.CssSelector("a[href=signin]"));
            dropDown.Click();

            Driver.FindElement(By.CssSelector("#authn_userName")).SendKeys("user");
            Driver.FindElement(By.CssSelector("#authn_userPassword")).SendKeys("password");
            Driver.FindElement(By.CssSelector("#authn_signIn")).Click();

            //IElement result = null;
            //try
            //{
            //    result = elem();
            //}
            //catch
            //{
            //}
            //if (result != null)
            //{
            //    I.Click(elem);
            //    I.WaitUntil(() => I.Expect.Exists("#authn_userName"));
            //}
            //I.Enter("user").In("#authn_userName");
            //I.Enter("password").In("#authn_userPassword");
            //I.Click("#authn_signIn");
        }

        [Given(@"I am registered")]
        public void GivenIAmRegistered()
        {
            Driver.Navigate().GoToUrl("http://test.itsa.com");
            //I.Open("http://dev.itsa.com");
            Login();
        }


        [Given(@"I am not logged on")]
        public void GivenIAmNotLoggedOn()
        {
            ScenarioContext.Current.Pending();
        }

        [Given(@"I enter valid credentials into the logon form")]
        public void GivenIEnterValidCredentialsIntoTheLogonForm()
        {
            ScenarioContext.Current.Pending();
        }

        [When(@"I press logon")]
        public void WhenIPressLogon()
        {
            ScenarioContext.Current.Pending();
        }

        [Then(@"the ItsA user page should be shown")]
        public void ThenTheItsAUserPageShouldBeShown()
        {
            ScenarioContext.Current.Pending();
        }
    }
}
