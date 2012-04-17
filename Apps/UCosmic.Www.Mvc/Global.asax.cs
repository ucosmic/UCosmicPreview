using System;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Elmah.Contrib.Mvc;
using FluentValidation;
using FluentValidation.Mvc;
using UCosmic.Domain;
using UCosmic.Orm;
using UCosmic.Seeders;
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
            AutoMapperConfig.Configure();

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
            SimpleHttpContextLifestyleExtensions.DisposeInstance<IUnitOfWork>();
        }

        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            //filters.Add(new HandleErrorAttribute()); // default MVC setting
            filters.Add(new ElmahHandleErrorAttribute());
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}"); // default MVC setting
            routes.IgnoreRoute("favicon.ico"); // prevent 404's for favicon

            // NOTE: Default route mappings are disabled. Allow only explicitly-defined routes.
        }

        private static void SetUpDependencyInjection()
        {
            // use simple infrastructure injector
            //var injector = new UnityDependencyInjector();
            var injector = new SimpleDependencyInjector();
            DependencyInjector.Set(injector);

            // use infrastructure service locator for MVC dependency resolution
            DependencyResolver.SetResolver(new MvcDependencyResolver());

            var providers = FilterProviders.Providers.OfType<FilterAttributeFilterProvider>().ToList();
            providers.ForEach(provider => FilterProviders.Providers.Remove(provider));
            FilterProviders.Providers.Add(DependencyInjector.Current.GetService<SimpleFilterAttributeFilterProvider>());
        }

        public static void SetUpFluentValidation()
        {
            FluentValidationModelValidatorProvider.Configure(
                provider =>
                {
                    provider.ValidatorFactory = new FluentValidatorFactory(DependencyInjector.Current);
                }
            );
            ValidatorOptions.CascadeMode = CascadeMode.StopOnFirstFailure;
        }

        private static void SeedDb()
        {
            // check DI for database seeder
            var seeder = DependencyInjector.Current.GetService<ISeedDb>();
            if (seeder == null) return;
            using (var context = DependencyInjector.Current.GetService<IUnitOfWork>())
            {
                seeder.Seed(context as UCosmicContext);
            }
        }

        public override string GetVaryByCustomString(HttpContext context, string custom)
        {
            return "User".Equals(custom, StringComparison.OrdinalIgnoreCase)
                ? Thread.CurrentPrincipal.Identity.Name
                : base.GetVaryByCustomString(context, custom);
        }
    }
}