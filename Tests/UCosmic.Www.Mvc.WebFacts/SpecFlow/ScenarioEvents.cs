using System.Linq;
using OpenQA.Selenium;
using TechTalk.SpecFlow;
using UCosmic.Impl;
using UCosmic.Www.Mvc.WebDriver;

namespace UCosmic.Www.Mvc.SpecFlow
{
    [Binding]
    // ReSharper disable ClassNeverInstantiated.Global
    // ReSharper disable UnusedMember.Global
    // ReSharper disable MemberCanBePrivate.Global
    public class ScenarioEvents : TestRunEvents
    {
        #region All Scenarios

        [BeforeScenario]
        public void BeforeEveryScenario()
        {
            Browsers.PrepareForScenario(ScenarioCount);
        }

        [AfterScenario]
        public void AfterEveryScenario()
        {
            ScenarioCount++; // increment the scenario count

            // make single browser ambiguous
            RestoreAfterScenario();
        }

        #endregion
        #region Remove Before Scenario

        [BeforeScenario("NotInChrome")]
        public void RemoveChromeBeforeScenario()
        {
            RemoveBeforeScenario(Browsers.Chrome());
        }

        [BeforeScenario("NotInFirefox")]
        public void RemoveFirefoxBeforeScenario()
        {
            RemoveBeforeScenario(Browsers.Firefox());
        }

        [BeforeScenario("NotInInternetExplorer")]
        public void RemoveInternetExplorerBeforeScenario()
        {
            RemoveBeforeScenario(Browsers.InternetExplorer());
        }

        private static void RemoveBeforeScenario(IWebDriver browser)
        {
            if (browser == null || !Browsers.Contains(browser)) return;

            InactiveBrowsers.Add(browser);
            Browsers.Remove(browser);
        }

        #endregion
        #region Remove Others Before Scenario

        [BeforeScenario("OnlyInChrome")]
        public void OnlyInChromeBeforeScenario()
        {
            RemoveOthersBeforeScenario(Browsers.Chrome());
        }

        [BeforeScenario("OnlyInFirefox")]
        public void OnlyInFirefoxBeforeScenario()
        {
            RemoveOthersBeforeScenario(Browsers.Firefox());
        }

        [BeforeScenario("OnlyInInternetExplorer")]
        public void OnlyInInternetExplorerBeforeScenario()
        {
            RemoveOthersBeforeScenario(Browsers.InternetExplorer());
        }

        public static void RemoveOthersBeforeScenario(IWebDriver browser)
        {
            InactiveBrowsers.AddRange(Browsers);
            Browsers.Clear();

            if (browser == null || !InactiveBrowsers.Contains(browser)) return;

            Browsers.Add(InactiveBrowsers.Single(b => b == browser));
            InactiveBrowsers.Remove(Browsers.Single(b => b == browser));
        }

        #endregion
        #region Restore All After Scenario

        [AfterScenario("NotInChrome")]
        [AfterScenario("NotInFirefox")]
        [AfterScenario("NotInInternetExplorer")]
        [AfterScenario("OnlyInChrome")]
        [AfterScenario("OnlyInFirefox")]
        [AfterScenario("OnlyInInternetExplorer")]
        public void RestoreAfterScenario()
        {
            InactiveBrowsers.ForEach(browser =>
            {
                if (Browsers.Contains(browser)) return;
                var browserName = browser.Name();
                if (BrowserIndices[browserName] <= Browsers.Count)
                    Browsers.Insert(BrowserIndices[browserName], browser);
                else Browsers.Add(browser);
            });
            InactiveBrowsers.Clear();
        }

        #endregion
        #region Generated Email

        [BeforeTestRun]
        [AfterScenario("GeneratesEmail")]
        public static void DeleteGeneratedEmail()
        {
            new WebRequestHttpConsumer().Get(AppConfig.TestMailReset.ToAbsoluteUrl());
        }
        #endregion
    }
    // ReSharper restore ClassNeverInstantiated.Global
    // ReSharper restore UnusedMember.Global
    // ReSharper restore MemberCanBePrivate.Global

    public static class ScenarioContextKeys
    {
        public const string EmailMessages = "EmailMessages";
    }

}
