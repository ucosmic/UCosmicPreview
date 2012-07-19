using System.Web.Mvc;
using System.Web.Routing;
using UCosmic.Domain.Establishments;
using UCosmic.Www.Mvc.Areas.Activities.Models;
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

        public static void RegisterRoutes(AreaRegistrationContext context)
        {
            RootActionRouter.RegisterRoutes(typeof(ActivityIndexRouter), context, Area, Controller);
            ActivityResultsProfiler.RegisterProfiles();
        }

        // ReSharper disable UnusedMember.Global

        public static class Get
        {
            public const string Route = "faculty-staff";
            private static readonly string Action = MVC.Activities.ActivityIndex.ActionNames.Get;
            public static void MapRoutes(AreaRegistrationContext context, string area, string controller)
            {
                var defaults = new
                {
                    area, controller, action = Action,
                };
                var constraints = new { httpMethod = new HttpMethodConstraint("GET"), };
                context.MapRoute(null, Route, defaults, constraints);
            }
        }

        // ReSharper restore UnusedMember.Global
    }
}
