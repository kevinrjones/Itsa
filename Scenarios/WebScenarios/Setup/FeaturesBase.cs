using OpenQA.Selenium.Remote;
using TechTalk.SpecFlow;
using WebScenarios.Fixtures;
using WebScenarios.PageLibrary.Base;
using WebSiteSpecifications.Fixtures;
using WebSiteSpecifications.Values;

namespace WebScenarios.Setup
{
    [Binding]
    public class FeaturesBase : TestFixtureBase
    {
        #region Properties for Readability

        /// <summary>
        /// Shortcut property to Settings.CurrentSettings.Defaults for readability
        /// </summary>
        protected DefaultValues Default { get { return Settings.CurrentSettings.Defaults; } }


        #endregion

        public PageBase CurrentPage
        {
            get { return (PageBase)ScenarioContext.Current["CurrentPage"]; }
            set { ScenarioContext.Current["CurrentPage"] = value; }
        }

        [BeforeFeature()]
        public static void BeforeFeature()
        {
            CreateDriver();
        }

        [AfterFeature()]
        public static void AfterFeature()
        {
            TeardownDriver();
        }

        [BeforeScenario("UI")]
        public void BeforeScenario()
        {
            if (!ScenarioContext.Current.ContainsKey("CurrentDriver"))
            {
                Test_Setup();
                ScenarioContext.Current.Add("CurrentDriver", CurrentDriver);
            }
            else
            {
                CurrentDriver = (RemoteWebDriver)ScenarioContext.Current["CurrentDriver"];
            }
        }

        [AfterScenario("UI")]
        public void AfterScenario()
        {
            if (ScenarioContext.Current.ContainsKey("CurrentDriver"))
            {
                Test_Teardown();
                ScenarioContext.Current.Remove("CurrentDriver");
            }
        }
    }
}