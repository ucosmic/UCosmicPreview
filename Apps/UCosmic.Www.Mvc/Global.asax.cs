using System;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Elmah.Contrib.Mvc;
using FluentValidation.Mvc;
using ServiceLocatorPattern;
using UCosmic.Impl;
using UCosmic.Impl.Orm;
using UCosmic.Impl.Seeders;
using UCosmic.Www.Mvc.Controllers;
using UCosmic.Www.Mvc.Models;

namespace UCosmic.Www.Mvc
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode,
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            // default MVC application start tasks
            AreaRegistration.RegisterAllAreas();
            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);

            ViewEngines.Engines.Clear();
            ViewEngines.Engines.Add(new RazorViewEngine());

            // setup dependency injection
            SetUpDependencyInjection();

            // set up fluent validation
            SetUpFluentValidation();

            // configure automapper
            AutoMapperRegistration.RegisterAllProfiles();

            // seed the database
            SeedDb();
        }

        protected void Application_Error()
        {
            var ex = Server.GetLastError();
            if (ex == null) return;

            // When app's httpRuntime maxRequestLength is GREATER THAN server's requestLimits maxAllowedContentLength,
            // server will generate an HTTP 404.13 header and Application_Error will NOT kick in.
            // Otherwise, when app's httpRuntime maxRequestLength is LESS THAN server's requestLimits maxAllowedContentLength,
            // Application_Error will kick in and redirect with a path parameter.
            if (ex.GetType() != typeof(HttpException)
                || string.Compare(ex.Message, "Maximum request length exceeded.", false, CultureInfo.GetCultureInfo("en")) != 0)
                return;
            Server.ClearError();
            Response.Redirect(string.Format("~/errors/file-upload-too-large.html?path={0}",
                Server.UrlEncode(Request.CurrentExecutionFilePath)));
        }

        protected void Application_EndRequest()
        {
            SimpleHttpContextLifestyleExtensions.DisposeInstance<UCosmicContext>();
        }

        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            //filters.Add(new HandleErrorAttribute()); // default MVC setting
            filters.Add(new ElmahHandleErrorAttribute());
            filters.Add(new EnforceHttpsAttribute());
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}"); // default MVC setting
            routes.IgnoreRoute("favicon.ico"); // prevent 404's for favicon

            // NOTE: Default route mappings are disabled. Allow only explicitly-defined routes.
        }

        private static void SetUpDependencyInjection()
        {
            var containerConfiguration = new ContainerConfiguration
            {
                IsDeployedToCloud = WebConfig.IsDeployedToCloud,
                GeoPlanetAppId = WebConfig.GeoPlanetAppId,
                GeoNamesUserName = WebConfig.GeoNamesUserName,
            };
            var serviceProvider = new SimpleDependencyInjector(containerConfiguration);
            ServiceProviderLocator.SetProvider(serviceProvider);

            // use infrastructure service locator for MVC dependency resolution
            DependencyResolver.SetResolver(new MvcDependencyResolver());

            var providers = FilterProviders.Providers.OfType<FilterAttributeFilterProvider>().ToList();
            providers.ForEach(provider => FilterProviders.Providers.Remove(provider));
            FilterProviders.Providers.Add(ServiceProviderLocator.Current.GetService<SimpleFilterAttributeFilterProvider>());
        }

        private static void SetUpFluentValidation()
        {
            FluentValidationModelValidatorProvider.Configure(
                provider =>
                {
                    provider.ValidatorFactory = new FluentValidatorFactory(ServiceProviderLocator.Current);
                    provider.AddImplicitRequiredValidator = false;
                }
            );
        }

        private static void SeedDb()
        {
            // check DI for database seeder
            var seeder = ServiceProviderLocator.Current.GetService<ISeedDb>();
            if (seeder == null) return;
            using (var context = ServiceProviderLocator.Current.GetService<IUnitOfWork>())
            {
                seeder.Seed(context as UCosmicContext);
            }
        }

        public override string GetVaryByCustomString(HttpContext context, string custom)
        {
            return BaseController.VaryByCustomUser.Equals(custom, StringComparison.OrdinalIgnoreCase)
                ? User.Identity.Name
                : base.GetVaryByCustomString(context, custom);
        }
    }
}