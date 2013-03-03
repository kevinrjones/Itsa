using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow;

namespace WebScenarios.Steps
{
    [Binding]
    class ItsaSteps : StepsBase
    {
        [Given(@"I am registered")]
        public void GivenIAmRegistered()
        {
            ScenarioContext.Current.Pending();
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
