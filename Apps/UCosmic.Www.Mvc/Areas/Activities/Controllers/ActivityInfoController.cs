using System.Linq.Expressions;
using System.Web.Mvc;
using System.Web.Routing;
using UCosmic.Domain.Activities;
using UCosmic.Www.Mvc.Areas.Activities.Models;
using UCosmic.Www.Mvc.Controllers;
using System;
using AutoMapper;

namespace UCosmic.Www.Mvc.Areas.Activities.Controllers
{
    public class ActivityInfoServices
    {
        public ActivityInfoServices(IProcessQueries queryProcessor)
        {
            QueryProcessor = queryProcessor;
        }

        public IProcessQueries QueryProcessor { get; private set; }
    }

    public partial class ActivityInfoController : BaseController
    {
        private readonly ActivityInfoServices _services;

        public ActivityInfoController(ActivityInfoServices services)
        {
            _services = services;
        }

        [HttpGet]
        [ActionName("activity-info")]
        [OpenTopTab(TopTabName.FacultyStaff)]
        public virtual ActionResult Get(Guid entityId)
        {
            var entity = _services.QueryProcessor.Execute(
                new GetActivityByEntityIdQuery
                {
                    EntityId = entityId,
                    EagerLoad = new Expression<Func<Activity, object>>[]
                    {
                        a => a.Person,
                        a => a.Tags,
                    }
                }
            );
            var model = Mapper.Map<ActivityInfo>(entity);
            if (Request.IsAjaxRequest()) return PartialView(MVC.Activities.Shared.Views._activity_info, model);
            return View(model);
        }
    }

    public static class ActivityInfoRouter
    {
        private static readonly string Area = MVC.Activities.Name;
        private static readonly string Controller = MVC.Activities.ActivityInfo.Name;

        public class GetRoute : MvcRoute
        {
            public GetRoute()
            {
                Url = "{establishment}/activities/{entityId}";
                DataTokens = new RouteValueDictionary(new { area = Area });
                Defaults = new RouteValueDictionary(new
                {
                    controller = Controller,
                    action = MVC.Activities.ActivityInfo.ActionNames.Get,
                });
                Constraints = new RouteValueDictionary(new
                {
                    httpMethod = new HttpMethodConstraint("GET"),
                    entityId = new NonEmptyGuidRouteConstraint(),
                });
            }
        }
    }
}
