using System.Web.Mvc;
using System.Web.Routing;
using AutoMapper;
using UCosmic.Domain.Establishments;
using UCosmic.Www.Mvc.Areas.Saml.Models;
using UCosmic.Www.Mvc.Controllers;

namespace UCosmic.Www.Mvc.Areas.Saml.Controllers
{
    public class ListIdentityProvidersServices
    {
        public ListIdentityProvidersServices(IProcessQueries queryProcessor
        )
        {
            QueryProcessor = queryProcessor;
        }

        public IProcessQueries QueryProcessor { get; private set; }
    }

    public partial class ListIdentityProvidersController : BaseController
    {
        private readonly ListIdentityProvidersServices _services;

        public ListIdentityProvidersController(ListIdentityProvidersServices services)
        {
            _services = services;
        }

        [HttpGet]
        [ActionName("providers")]
        [OpenTopTab(TopTabName.Home)]
        public virtual ActionResult Get()
        {
            var entities = _services.QueryProcessor.Execute(
                new FindSamlIntegratedEstablishmentsQuery()
            );

            var models = Mapper.Map<IdentityProviderListItem[]>(entities);
            return View(models);
        }
    }

    public static class ListIdentityProvidersRouter
    {
        private static readonly string Area = MVC.Saml.Name;
        private static readonly string Controller = MVC.Saml.ListIdentityProviders.Name;

        public static void RegisterRoutes(AreaRegistrationContext context)
        {
            RootActionRouter.RegisterRoutes(typeof(ListIdentityProvidersRouter), context, Area, Controller);
        }

        // ReSharper disable UnusedMember.Global

        public static class Get
        {
            public const string Route = "sign-on/saml/2/providers";
            private static readonly string Action = MVC.Saml.ListIdentityProviders.ActionNames.Get;
            public static void MapRoutes(AreaRegistrationContext context, string area, string controller)
            {
                var defaults = new { area, controller, action = Action, };
                var constraints = new { httpMethod = new HttpMethodConstraint("GET") };
                context.MapRoute(null, Route, defaults, constraints);
            }
        }

        // ReSharper restore UnusedMember.Global
    }
}
