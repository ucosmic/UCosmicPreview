using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using SimpleInjector;
using UCosmic.Impl;
using UCosmic.Impl.Orm;
using UCosmic.Impl.Seeders;

namespace UCosmic.Www.Mvc
{
    public class SimpleServiceProvider : IServiceProvider
    {
        private readonly Container _container;

        public SimpleServiceProvider()
        {
            _container = Bootstrap();
        }

        private static Container Bootstrap()
        {
            var container = new Container();

            // register database types
            container.Register<IDatabaseInitializer<UCosmicContext>, BrownfieldInitializer>();
            container.Register<ISeedData, BrownfieldSeeder>();
            container.Register<UCosmicContext>();
            container.Register<IWrapDataConcerns, DataConcernsWrapper>();
            container.RegisterInitializer<UCosmicContext>(container.InjectProperties);

            // register browsers
            container.RegisterSingle(() => new ChromeDriver(GetChromeDriverDirectory()));
            container.RegisterSingle(() => new InternetExplorerDriver());
            container.RegisterSingle(() => new FirefoxDriver());
            var browsers = AllBrowsers(container);
            container.RegisterAll(browsers);

            // register other stuff
            container.Register<IManageConfigurations, DotNetConfigurationManager>();

            container.Verify();
            return container;
        }

        private static IEnumerable<IWebDriver> AllBrowsers(Container container)
        {
            if (container.GetRegistration(typeof(ChromeDriver)) != null)
                yield return container.GetInstance<ChromeDriver>();
            if (container.GetRegistration(typeof(InternetExplorerDriver)) != null)
                yield return container.GetInstance<InternetExplorerDriver>();
            if (container.GetRegistration(typeof(FirefoxDriver)) != null)
                yield return container.GetInstance<FirefoxDriver>();
        }

        private static string GetChromeDriverDirectory()
        {
            var directory = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory);
            while (directory != null && !directory.GetFiles("chromedriver.exe").Any())
                directory = directory.Parent;
            return directory != null ? directory.FullName : null;
        }

        public object GetService(Type serviceType)
        {
            return ((IServiceProvider)_container).GetService(serviceType);
        }
    }
}
