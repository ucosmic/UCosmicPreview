using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;
using UCosmic.Domain.Establishments;
using UCosmic.Www.Mvc.Areas.Activities.Models;
using UCosmic.Www.Mvc.Controllers;
using AutoMapper;

namespace UCosmic.Www.Mvc.Areas.Activities.Controllers
{
    public class ActivitySearchServices
    {
        public ActivitySearchServices(IProcessQueries queryProcessor)
        {
            QueryProcessor = queryProcessor;
        }

        public IProcessQueries QueryProcessor { get; private set; }
    }

    public partial class ActivitySearchController : BaseController
    {
        private readonly ActivitySearchServices _services;

        public ActivitySearchController(ActivitySearchServices services)
        {
            _services = services;
        }

        [HttpGet]
        [ActionName("activity-results")]
        public virtual ActionResult Get(string establishment, string keyword)
        {
            // return 404 when there is no establishment
            if (string.IsNullOrWhiteSpace(establishment)) return HttpNotFound();

            // redirect to canonical route when keyword is in the query string
            if (Request.QueryString.AllKeys.Contains("keyword", new CaseInsensitiveStringComparer()))
                return RedirectToRoute(new
                {
                    area = Area,
                    controller = Name,
                    action = MVC.Activities.ActivitySearch.ActionNames.Get,
                    establishment,
                    keyword,
                });

            //if (pageNumber < 1)
            //    return RedirectToAction(MVC.Activities.ActivitySearch.Page());

            //var query = new FindMyActivitiesQuery
            //{
            //    Principal = User,
            //    PagerOptions = new PagerOptions
            //    {
            //        //PageNumber = pageNumber,
            //        PageSize = int.MaxValue,
            //    },
            //    EagerLoad = new Expression<Func<Activity, object>>[]
            //    {
            //        a => a.Tags,
            //    },
            //    OrderBy = new Dictionary<Expression<Func<Activity, object>>, OrderByDirection>
            //    {
            //        { a => a.DraftedValues.Title, OrderByDirection.Ascending },
            //        { a => a.Values.Title, OrderByDirection.Ascending },
            //    },
            //};
            //var activities = _services.QueryProcessor.Execute(query);
            ////if (activities.PageCount > 0 && activities.PageCount < pageNumber)
            ////    return RedirectToAction(MVC.Activities.ActivitySearch.Page(establishment, activities.PageCount));
            //var model = Mapper.Map<ActivitiesPage>(activities);
            var models = new ActivityResults(Enumerable.Empty<ActivityResults.ActivityResult>(), 0);
            var tenant = _services.QueryProcessor.Execute(
                new GetEstablishmentByUrlQuery
                {
                    Url = establishment,
                }
            );
            Mapper.Map(tenant, models);
            return View(models);
        }
    }

    public static class ActivitySearchRouter
    {
        private static readonly string Area = MVC.Activities.Name;
        private static readonly string Controller = MVC.Activities.ActivitySearch.Name;

        public static void RegisterRoutes(AreaRegistrationContext context)
        {
            RootActionRouter.RegisterRoutes(typeof(ActivitySearchRouter), context, Area, Controller);
            ActivityResultsProfiler.RegisterProfiles();
        }

        // ReSharper disable UnusedMember.Global

        public static class Get
        {
            public const string Route = "{establishment}/activities/{keyword}";
            private static readonly string Action = MVC.Activities.ActivitySearch.ActionNames.Get;
            public static void MapRoutes(AreaRegistrationContext context, string area, string controller)
            {
                var defaults = new
                {
                    area, controller, action = Action,
                    keyword = UrlParameter.Optional,
                };
                var constraints = new { httpMethod = new HttpMethodConstraint("GET"), };
                context.MapRoute(null, Route, defaults, constraints);
            }
        }

        // ReSharper restore UnusedMember.Global
    }
}
