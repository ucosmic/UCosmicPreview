using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Support.UI;

namespace UCosmic.Www.Mvc.WebDriver
{
    public static class WebDriverExtensions
    {
        #region Identification

        public static string Name(this IWebDriver browser)
        {
            // return a friendly browser name for error messages, etc
            if (browser == null) return BrowserName.Null;
            if (typeof(FirefoxDriver) == browser.GetType()) return BrowserName.Firefox;
            if (typeof(ChromeDriver) == browser.GetType()) return BrowserName.Chrome;
            return typeof(InternetExplorerDriver) == browser.GetType()
                       ? BrowserName.InternetExplorer
                       : BrowserName.Unknown;
        }

        public static bool IsChrome(this IWebDriver browser)
        {
            return browser.Name().Equals(BrowserName.Chrome);
        }

        public static bool IsFirefox(this IWebDriver browser)
        {
            return browser.Name().Equals(BrowserName.Firefox);
        }

        public static bool IsInternetExplorer(this IWebDriver browser)
        {
            return browser.Name().Equals(BrowserName.InternetExplorer);
        }

        public static IWebDriver Chrome(this IEnumerable<IWebDriver> browsers)
        {
            return browsers.SingleOrDefault(b => b.IsChrome());
        }

        public static IWebDriver Firefox(this IEnumerable<IWebDriver> browsers)
        {
            return browsers.SingleOrDefault(b => b.IsFirefox());
        }

        public static IWebDriver InternetExplorer(this IEnumerable<IWebDriver> browsers)
        {
            return browsers.SingleOrDefault(b => b.IsInternetExplorer());
        }

        #endregion
        #region Size & Position

        public static void OpenWindow(this IWebDriver browser, string url = null, string name = null)
        {
            var options = string.Format("left={0},top=0,width={1},scrollbars=1,resizable=1,location=1,status=1,toolbar=1,menubar={2}",
                browser.ConfigWindowLeft(), AppConfig.BrowserWindowWidth, browser.ConfigMenubar());
            var script = string.Format("window.open('{0}', '{1}', '{2}');", null, name, options);
            browser.ExecuteScript(script);
            1000.WaitThisManyMilleseconds();
            browser.Close();
            browser.SwitchTo().Window(name ?? string.Empty);
            1000.WaitThisManyMilleseconds();
            browser.ExecuteScript(BrowserResizeScript);
            1000.WaitThisManyMilleseconds();
            browser.ExecuteScript(string.Format("window.moveTo({0}, 0);", browser.ConfigWindowLeft()));
            1000.WaitThisManyMilleseconds();
            if (!string.IsNullOrWhiteSpace(url))
                browser.Navigate().GoToUrl(url);
        }

        public static void PrepareForScenario(this IEnumerable<IWebDriver> browsers, int scenarioCount)
        {
            foreach (var browser in from browser in browsers
                                    let resetInterval = browser.ConfigResetInterval()
                                    where scenarioCount > 0 && resetInterval > 0 && scenarioCount % resetInterval == 0
                                    select browser)
            {
                browser.OpenWindow();
            }
        }

        private static int ConfigResetInterval(this IWebDriver browser)
        {
            if (browser.IsChrome()) return AppConfig.ChromeResetInterval;
            if (browser.IsFirefox()) return AppConfig.FirefoxResetInterval;
            if (browser.IsInternetExplorer()) return AppConfig.InternetExplorerResetInterval;
            return 0;
        }

        private static int ConfigWindowLeft(this IWebDriver browser)
        {
            if (browser.IsChrome()) return AppConfig.ChromeWindowLeft;
            if (browser.IsFirefox()) return AppConfig.FirefoxWindowLeft;
            if (browser.IsInternetExplorer()) return AppConfig.InternetExplorerWindowLeft;
            return AppConfig.BrowserWindowLeft;
        }

        private static int ConfigMenubar(this IWebDriver browser)
        {
            return browser.IsChrome() ? 0 : 1;
        }

        private static string BrowserResizeScript
        {
            get
            {
                const string scriptFormat = @"
                    var currentWidth = 1;
                    if (typeof(window.innerWidth) != 'undefined') {
                        currentWidth = window.innerWidth;
                    } else if (typeof(document.documentElement) != 'undefined'
                        && typeof(document.documentElement.clientWidth) != 'undefined'
                        && document.documentElement.clientWidth > 0) {
                        currentWidth = document.documentElement.clientWidth;
                    } else {
                        currentWidth = document.getElementsByTagName('body')[0].clientWidth;
                    }
                    if (currentWidth < {MinimumBrowserWindowWidth}) {
                        window.resizeTo({MinimumBrowserWindowWidth}, window.screen.availHeight);
                    }
                ";
                return scriptFormat.Replace("{MinimumBrowserWindowWidth}",
                    AppConfig.BrowserWindowWidth.ToString(CultureInfo.InvariantCulture));
            }
        }

        #endregion
        #region Javascript

        public static object ExecuteScript(this IWebDriver browser, string script)
        {
            return ((IJavaScriptExecutor)browser).ExecuteScript(script);
        }

        #endregion
        #region WebDriverWait Support

        [DebuggerStepThrough]
        public static T WaitUntil<T>(this IWebDriver browser, Func<IWebDriver, T> condition,
            string failMessage, int? timeoutSeconds = null)
        {
            var timeoutSecondsInt = (timeoutSeconds.HasValue) 
                ? timeoutSeconds.Value 
                : AppConfig.DefaultWebDriverWaitSeconds;

            // helper for inserting browser name in failure messages
            failMessage = failMessage.Replace("@Browser", string.Format("{0} browser", browser.Name()));
            try
            {
                // if the condition does not become true within timeoutSeconds, an exception will be thrown.
                return new WebDriverWait(browser, new TimeSpan(0, 0, timeoutSecondsInt))
                    .Until(condition);
            }
            catch (Exception ex)
            {
                // if an exception was thrown, fail the test and return
                Assert.Fail(string.Format("{0} ({1})", failMessage, ex.Message));
            }
            // ReSharper disable HeuristicUnreachableCode
            return default(T); // need return value to compile
            // ReSharper restore HeuristicUnreachableCode
        }

        //[DebuggerStepThrough]
        //public static T WaitUntil<T>(this IWebDriver browser, Func<IWebDriver, T> condition, string failMessage)
        //{
        //    return browser.WaitUntil(condition, failMessage, AppConfig.DefaultWebDriverWaitSeconds);
        //}

        public static IWebElement TryFindElement(this IWebDriver browser, By by)
        {
            try { return browser.FindElement(by); }
            catch { return null; }
        }

        #endregion

    }

    public static class BrowserName
    {
        public const string Firefox = "Firefox";
        public const string Chrome = "Chrome";
        public const string InternetExplorer = "Internet Explorer";
        public const string Unknown = "UNKNOWN";
        public const string Null = "NULL";
    }

}

