using System.Web.Mvc;
using System.Web.Routing;
using AutoMapper;
using UCosmic.Domain.Establishments;
using UCosmic.Www.Mvc.Areas.Identity.Models;
using UCosmic.Www.Mvc.Controllers;

namespace UCosmic.Www.Mvc.Areas.Identity.Controllers
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
        private static readonly string Area = MVC.Identity.Name;
        private static readonly string Controller = MVC.Identity.ListIdentityProviders.Name;

        public class GetRoute : MvcRoute
        {
            public GetRoute()
            {
                Url = "sign-on/saml/2/providers";
                DataTokens = new RouteValueDictionary(new { area = Area });
                Defaults = new RouteValueDictionary(new
                {
                    controller = Controller,
                    action = MVC.Identity.ListIdentityProviders.ActionNames.Get,
                });
                Constraints = new RouteValueDictionary(new
                {
                    httpMethod = new HttpMethodConstraint("GET"),
                });
            }
        }
    }
}
