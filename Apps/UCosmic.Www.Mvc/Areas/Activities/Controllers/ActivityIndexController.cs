using System.Web.Mvc;
using System.Web.Routing;
using UCosmic.Domain.Establishments;
using UCosmic.Www.Mvc.Controllers;

namespace UCosmic.Www.Mvc.Areas.Activities.Controllers
{
    public class ActivityIndexServices
    {
        public ActivityIndexServices(IProcessQueries queryProcessor)
        {
            QueryProcessor = queryProcessor;
        }

        public IProcessQueries QueryProcessor { get; private set; }
    }

    public partial class ActivityIndexController : BaseController
    {
        private readonly ActivityIndexServices _services;

        public ActivityIndexController(ActivityIndexServices services)
        {
            _services = services;
        }

        [HttpGet]
        public virtual RedirectToRouteResult Get()
        {
            var tenantUrl = HttpContext.SkinCookie();

            // check username
            if (User.Identity.Name.IsNotNullOrWhiteSpace())
            {
                var tenant = _services.QueryProcessor.Execute(
                    new GetEstablishmentByEmailQuery(User.Identity.Name));
                if (tenant != null)
                {
                    if (tenantUrl.IsNullOrWhiteSpace())
                        return RedirectToAction(MVC.Common.Skins.Change(tenant.WebsiteUrl, Url.Action(MVC.Activities.ActivitySearch.Get(tenant.WebsiteUrl))));
                    return RedirectToAction(MVC.Activities.ActivitySearch.Get(tenant.WebsiteUrl));
                }
            }

            // check skin cookie
            if (tenantUrl != null)
                return RedirectToAction(MVC.Activities.ActivitySearch.Get(tenantUrl));

            return RedirectToAction(MVC.Common.Features.Requirements("faculty-staff"));
        }
    }

    public static class ActivityIndexRouter
    {
        private static readonly string Area = MVC.Activities.Name;
        private static readonly string Controller = MVC.Activities.ActivityIndex.Name;

        public class GetRoute : MvcRoute
        {
            public GetRoute()
            {
                Url = "faculty-staff";
                DataTokens = new RouteValueDictionary(new { area = Area });
                Defaults = new RouteValueDictionary(new
                {
                    controller = Controller,
                    action = MVC.Activities.ActivityIndex.ActionNames.Get,
                });
                Constraints = new RouteValueDictionary(new
                {
                    httpMethod = new HttpMethodConstraint("GET"),
                });
            }
        }
    }
}
