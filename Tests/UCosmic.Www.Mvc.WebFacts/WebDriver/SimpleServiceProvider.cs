using System;
using System.Data.Entity;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using SimpleInjector;
using UCosmic.Impl.Orm;
using UCosmic.Impl.Seeders;
using UCosmic.Impl;
using OpenQA.Selenium;
using System.Collections.Generic;

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
            container.Register<ISeedDb, BrownfieldDbSeeder>();
            container.Register<UCosmicContext>();
            container.Register<IWrapDataConcerns, DataConcernsWrapper>();

            // register browsers
            container.RegisterSingle(() => new ChromeDriver());
            container.RegisterSingle(() => new InternetExplorerDriver());
            //container.RegisterSingle(() => new FirefoxDriver());
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

        public object GetService(Type serviceType)
        {
            return ((IServiceProvider)_container).GetService(serviceType);
        }
    }
}
