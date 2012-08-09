using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using ServiceLocatorPattern;
using Should;

namespace UCosmic.Www.Mvc
{
    [TestClass]
    public class WebDriverContext
    {
        public static List<IWebDriver> Browsers { get; private set; }

        [AssemblyInitialize]
        public static void InitializeTestSuite(TestContext testContext)
        {
            // register routes for UrlHelper
            GlobalAsaxFacts.RegisterAllRoutes(testContext);

            // use simple injector for dependency injection
            ServiceProviderLocator.SetProvider(new SimpleServiceProvider());

            // for the Chrome driver to start, chromedriver.exe should automatically copy from
            // the test project to /bin/Debug during build.
            // as of October 2011, please instead put chromedriver.exe directly in the C:\ drive

            // for IE driver to start, make sure "Enable Protected Mode" is checked for all zones:
            // go to Internet Options, and click the Security tab
            // ensure "Enable Protected Mode" is checked for Internet, Local intranet,
            // Trusted sites, and Restricted sites.

            // inject browser dependencies from IoC into static Browsers list property
            Browsers = new List<IWebDriver>(ServiceProviderLocator.Current.GetServices<IWebDriver>());
        }

        [AssemblyCleanup]
        public static void ConcludeTestSuite()
        {
            Browsers.ForEach(b => b.Quit());
            Browsers = null;
        }

        [TestMethod]
        public void ThereIsAtLeastOneBrowserUnderTest()
        {
            Browsers.ShouldNotBeNull();
            Browsers.Count.ShouldBeInRange(1, int.MaxValue);
        }
    }
}
