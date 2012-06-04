using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Web.Mvc;
using System.Web.Routing;
using UCosmic.Domain;
using UCosmic.Domain.Activities;
using UCosmic.Www.Mvc.Areas.Activities.Models;
using UCosmic.Www.Mvc.Controllers;
using AutoMapper;

namespace UCosmic.Www.Mvc.Areas.Activities.Controllers
{
    public class ActivityListServices
    {
        public ActivityListServices(IProcessQueries queryProcessor
        )
        {
            QueryProcessor = queryProcessor;
        }

        public IProcessQueries QueryProcessor { get; private set; }
    }

    [Authenticate]
    public partial class ActivityListController : BaseController
    {
        private readonly ActivityListServices _services;

        public ActivityListController(ActivityListServices services)
        {
            _services = services;
        }

        public const int ShortListLength = 5;

        [HttpGet]
        [ActionName("_short-list")]
        public virtual PartialViewResult ShortList()
        {
            var query = new FindMyActivitiesQuery
            {
                Principal = User, 
                MaxResults = ShortListLength, 
                OrderBy = new Dictionary<Expression<Func<Activity, object>>, OrderByDirection>
                {
                    { a => a.UpdatedOn, OrderByDirection.Descending },
                },
            };
            var activities = _services.QueryProcessor.Execute(query);
            var models = Mapper.Map<ActivityListItem[]>(activities);
            foreach (var model in models)
                model.Total = query.Total;
            return PartialView(models);
        }
    }

    public static class ActivityListRouter
    {
        private static readonly string Area = MVC.Activities.Name;
        private static readonly string Controller = MVC.Activities.ActivityList.Name;

        public static void RegisterRoutes(AreaRegistrationContext context)
        {
            RootActionRouter.RegisterRoutes(typeof(ActivityListRouter), context, Area, Controller);
            ListItemProfiler.RegisterProfiles();
        }

        // ReSharper disable UnusedMember.Global

        public static class ShortList
        {
            public const string Route = "my/activities/short-list";
            private static readonly string Action = MVC.Activities.ActivityList.ActionNames.ShortList;
            public static void MapRoutes(AreaRegistrationContext context, string area, string controller)
            {
                var defaults = new { area, controller, action = Action, };
                var constraints = new { httpMethod = new HttpMethodConstraint("GET"), };
                context.MapRoute(null, Route, defaults, constraints);
            }
        }

        // ReSharper restore UnusedMember.Global
    }
}
