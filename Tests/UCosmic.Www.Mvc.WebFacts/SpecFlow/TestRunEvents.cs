using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.ServiceProcess;
using OpenQA.Selenium;
using TechTalk.SpecFlow;
using UCosmic.Domain;
using UCosmic.Impl.Orm;
using UCosmic.Impl.Seeders;
using UCosmic.Www.Mvc.Areas.Identity;

namespace UCosmic.Www.Mvc
{
    [Binding]
    public class TestRunEvents
    {
        protected static readonly IDictionary<string, int> BrowserIndices = new Dictionary<string, int>();
        protected static List<IWebDriver> Browsers { get { return WebDriverContext.Browsers; } }
        protected static int ScenarioCount { get; set; }
        protected static readonly List<IWebDriver> InactiveBrowsers = new List<IWebDriver>();

        [BeforeTestRun]
        // ReSharper disable UnusedMember.Global
        public static void BeforeTestRun()
        // ReSharper restore UnusedMember.Global
        {
            // warm up the browsers
            WarmUpBrowserBeforeTestRun();

            var index = 0;
            foreach (var browser in Browsers)
            {
                // store original locations of each browser in the list
                BrowserIndices.Add(browser.Name(), index++);

                // start off on the homepage
                browser.Navigate().GoToUrl(AppConfig.BaseUrl);
                browser.WaitUntil(b => b.Url.Equals(AppConfig.BaseUrl + "/"), string.Format(
                    "Test run failed to initialize in @Browser because it did not arrive at the home page."), 30);
            }

            RestartDbServerBeforeTestRun();
            InitializeAndSeedDbBeforeTestRun();
            ClearTransportLevelErrorsBeforeTestRun();
        }

        private static void WarmUpBrowserBeforeTestRun()
        {
            if (AppConfig.WarmUpBrowsersBeforeTestRun)
            {
                Browsers.ForEach(b => b.OpenWindow());
            }
        }

        private static void RestartDbServerBeforeTestRun()
        {
            if (!AppConfig.RestartDbServerBeforeTestRun) return;

            var dbService = ServiceController.GetServices()
                .Single(c => c.ServiceName.Equals("MSSQL$SQLEXPRESS"));
            if (dbService.Status != ServiceControllerStatus.Stopped)
            {
                dbService.Stop(); // run VS as admin to avoid exceptions here
                dbService.WaitForStatus(ServiceControllerStatus.Stopped);
            }
            dbService.Start();
            dbService.WaitForStatus(ServiceControllerStatus.Running);
        }

        private static void InitializeAndSeedDbBeforeTestRun()
        {
            if (!AppConfig.InitializeAndSeedDbBeforeTestRun) return;

            var initializer = DependencyInjector.Current.GetService<IDatabaseInitializer<UCosmicContext>>();
            var seeder = DependencyInjector.Current.GetService<ISeedDb>();

            var context = (UCosmicContext) DependencyInjector.Current.GetService<IUnitOfWork>();
            if (initializer != null)
            {
                Database.SetInitializer(initializer);
                context.Database.Initialize(true);
            }
            if (seeder != null)
                seeder.Seed(context);
        }

        private static void ClearTransportLevelErrorsBeforeTestRun()
        {
            if (AppConfig.RestartDbServerBeforeTestRun && AppConfig.ClearTransportLevelErrorsAfterDbServerRestart)
            {
                var firstBrowser = Browsers.FirstOrDefault();
                if (firstBrowser != null)
                {
                    bool errorExists;
                    const string errorMessage = "A transport-level error has occurred";
                    var signOnPage = new SignOnPage(firstBrowser);
                    var personalHomePage = new MyHomePage(firstBrowser);
                    do // after restarting db server, first couple of form submissions may have
                    {
                        // transport-level errors. these can be cleared by resubmitting the forms.
                        errorExists = false;

                        // go to sign in page
                        firstBrowser.Navigate().GoToUrl(signOnPage.AbsoluteUrl);
                        firstBrowser.WaitUntil(b => b.Url.StartsWith(signOnPage.AbsoluteUrl), string.Format(
                            "Test run failed to initialize in @Browser because it did not arrive at the sign on page."));

                        // attempt to sign in
                        var element = firstBrowser.WaitUntil(b => b.FindElement(By.Id("EmailAddress")), string.Format(
                            "Test run failed to initialize in @Browser because it could not find an EmailAddress text box on the sign in page."));
                        element.ClearAndSendKeys("ludwigd1@uc.edu");
                        element = firstBrowser.WaitUntil(b => b.FindElement(By.Id("Password")), string.Format(
                            "Test run failed to initialize in @Browser because it could not find a Password text box on the sign in page."));
                        element.ClearAndSendKeys("asdfasdf");
                        element = firstBrowser.WaitUntil(b => b.FindElement(By.Id("sign_in_submit_button")),
                                                         string.Format(
                                                             "Test run failed to initialize in @Browser because it could not find a sign in submit button."));
                        element.ClickButton();
                        firstBrowser.WaitUntil(b => b.Url.StartsWith(signOnPage.AbsoluteUrl)
                                                    || b.Url.StartsWith(personalHomePage.AbsoluteUrl), string.Format(
                                                        "Test run failed to initialize in @Browser because it did not arrive at the sign in or about me page."));

                        // look for a transport-level error ysod
                        var error = firstBrowser.TryFindElement(By.TagName("body"));
                        if (error != null && error.Text.Contains(errorMessage))
                            continue; // try again

                        // go to the about me page
                        firstBrowser.Navigate().GoToUrl(personalHomePage.AbsoluteUrl);
                        firstBrowser.WaitUntil(b => b.Url.StartsWith(personalHomePage.AbsoluteUrl), string.Format(
                            "Test run failed to initialize in @Browser because it did not arrive at the about me page."));

                        // attempt to save the form (without making any changes)
                        element = firstBrowser.WaitUntil(b => b.FindElement(By.Id("person_name_submit_button")),
                                                         string.Format(
                                                             "Test run failed to initialize in @Browser because it could not find a submit button for the about me form."));
                        element.ClickButton();
                        firstBrowser.WaitUntil(b => b.Url.StartsWith(personalHomePage.AbsoluteUrl), string.Format(
                            "Test run failed to initialize in @Browser because it did not arrive at the about me page."));

                        // look for a transport-level error ysod
                        error = firstBrowser.TryFindElement(By.TagName("body"));
                        if (error != null && error.Text.Contains(errorMessage))
                            errorExists = true; // try again
                    } while (errorExists);
                }
            }
        }

    }
}
