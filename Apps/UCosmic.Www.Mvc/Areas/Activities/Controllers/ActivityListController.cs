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

        public class ShortListRoute : Route
        {
            public ShortListRoute()
                : base("my/activities/short-list", new MvcRouteHandler())
            {
                DataTokens = new RouteValueDictionary(new { area = Area, });
                Defaults = new RouteValueDictionary(new
                {
                    controller = Controller,
                    action = MVC.Activities.ActivityList.ActionNames.ShortList,
                });
                Constraints = new RouteValueDictionary(new
                {
                    httpMethod = new HttpMethodConstraint("GET"),
                });
            }
        }

        public class PageRoute : Route
        {
            public PageRoute()
                : base("my/activities", new MvcRouteHandler())
            {
                DataTokens = new RouteValueDictionary(new { area = Area, });
                Defaults = new RouteValueDictionary(new
                {
                    controller = Controller,
                    action = MVC.Activities.ActivityList.ActionNames.Page,
                    pageNumber = UrlParameter.Optional,
                });
                Constraints = new RouteValueDictionary(new
                {
                    httpMethod = new HttpMethodConstraint("GET"),
                });
            }
        }

        public class PageRouteWithPageNumber : PageRoute
        {
            public PageRouteWithPageNumber()
            {
                Url = "my/activities/page-{pageNumber}";
            }
        }
    }
}
