using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Web.Mvc;
using System.Web.Routing;
using AutoMapper;
using UCosmic.Domain;
using UCosmic.Domain.Activities;
using UCosmic.Www.Mvc.Areas.Activities.Models;
using UCosmic.Www.Mvc.Controllers;

namespace UCosmic.Www.Mvc.Areas.Activities.Controllers
{
    public class ActivityListServices
    {
        public ActivityListServices(IProcessQueries queryProcessor)
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
                PagerOptions = new PagerOptions
                {
                    PageSize = ShortListLength,
                },
                OrderBy = new Dictionary<Expression<Func<Activity, object>>, OrderByDirection>
                {
                    { a => a.UpdatedOn, OrderByDirection.Descending },
                },
            };
            var activities = _services.QueryProcessor.Execute(query);
            var model = Mapper.Map<ActivitiesPage>(activities);
            return PartialView(model);
        }

        [HttpGet]
        [ActionName("activities-page")]
        [OpenTopTab(TopTabName.FacultyStaff)]
        public virtual ActionResult Page(int pageNumber = 1)
        {
            if (pageNumber < 1)
                return RedirectToAction(MVC.Activities.ActivityList.Page());

            var query = new FindMyActivitiesQuery
            {
                Principal = User,
                PagerOptions = new PagerOptions
                {
                    PageNumber = pageNumber,
                    PageSize = int.MaxValue,
                },
                EagerLoad = new Expression<Func<Activity, object>>[]
                {
                    a => a.Tags,
                },
                OrderBy = new Dictionary<Expression<Func<Activity, object>>, OrderByDirection>
                {
                    { a => a.DraftedValues.Title, OrderByDirection.Ascending },
                    { a => a.Values.Title, OrderByDirection.Ascending },
                },
            };
            var activities = _services.QueryProcessor.Execute(query);
            if (activities.PageCount > 0 && activities.PageCount < pageNumber)
                return RedirectToAction(MVC.Activities.ActivityList.Page(activities.PageCount));
            var model = Mapper.Map<ActivitiesPage>(activities);
            return View(model);
        }
    }

    public static class ActivityListRouter
    {
        private static readonly string Area = MVC.Activities.Name;
        private static readonly string Controller = MVC.Activities.ActivityList.Name;

        public static void RegisterRoutes(AreaRegistrationContext context)
        {
            RootActionRouter.RegisterRoutes(typeof(ActivityListRouter), context, Area, Controller);
            ActivitiesPageProfiler.RegisterProfiles();
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

        public static class Page
        {
            public static readonly string[] Routes = new[] { "my/activities", "my/activities/page-{pageNumber}" };
            private static readonly string Action = MVC.Activities.ActivityList.ActionNames.Page;
            public static void MapRoutes(AreaRegistrationContext context, string area, string controller)
            {
                var defaults = new
                {
                    area, controller, action = Action,
                    pageNumber = UrlParameter.Optional,
                };
                var constraints = new { httpMethod = new HttpMethodConstraint("GET"), };
                context.MapRoutes(null, Routes, defaults, constraints);
            }
        }

        // ReSharper restore UnusedMember.Global
    }
}
