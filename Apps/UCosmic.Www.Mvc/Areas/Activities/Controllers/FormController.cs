using System;
using System.Linq.Expressions;
using System.Web.Mvc;
using System.Web.Routing;
using UCosmic.Domain.Activities;
using UCosmic.Www.Mvc.Areas.Activities.Models;
using UCosmic.Www.Mvc.Controllers;
using UCosmic.Www.Mvc.Areas.Identity.Controllers;
using AutoMapper;

namespace UCosmic.Www.Mvc.Areas.Activities.Controllers
{
    public class FormServices
    {
        public FormServices(IProcessQueries queryProcessor
            , IHandleCommands<CreateNewActivityCommand> createCommandHandler
        )
        {
            QueryProcessor = queryProcessor;
            CreateCommandHandler = createCommandHandler;
        }

        public IProcessQueries QueryProcessor { get; private set; }
        public IHandleCommands<CreateNewActivityCommand> CreateCommandHandler { get; private set; }
    }

    [Authenticate]
    public partial class FormController : BaseController
    {
        private readonly FormServices _services;

        public FormController(FormServices services)
        {
            _services = services;
        }

        [HttpGet]
        [UnitOfWork]
        public virtual ActionResult New()
        {
            var command = new CreateNewActivityCommand
            {
                Principal = User,
            };
            _services.CreateCommandHandler.Handle(command);
            return RedirectToAction(MVC.Activities.Form.Get(command.CreatedActivity.Number));
        }

        [HttpGet]
        [ActionName("form")]
        [HttpNotFoundOnNullModel]
        [ReturnUrlReferrer(MyHomeRouter.Get.Route)]
        [OpenTopTab(TopTabName.FacultyStaff)]
        public virtual ActionResult Get(int number)
        {
            var activity = _services.QueryProcessor.Execute(
                new GetMyActivityByNumberQuery
                {
                    Principal = User,
                    Number = number,
                    EagerLoad = new Expression<Func<Activity, object>>[]
                    {
                        a => a.DraftedTags,
                    },
                }
            );
            var model = Mapper.Map<Form>(activity);
            return View(model);
        }

        [HttpPut]
        [ActionName("form")]
        [OpenTopTab(TopTabName.FacultyStaff)]
        public virtual ActionResult Put(Form model)
        {
            if (!ModelState.IsValid)
                return View(model);
            return View(model);
        }
    }

    public static class FormRouter
    {
        private static readonly string Area = MVC.Activities.Name;
        private static readonly string Controller = MVC.Activities.Form.Name;

        public static void RegisterRoutes(AreaRegistrationContext context)
        {
            RootActionRouter.RegisterRoutes(typeof(FormRouter), context, Area, Controller);
            FormProfiler.RegisterProfiles();
        }

        // ReSharper disable UnusedMember.Global

        public static class New
        {
            public const string Route = "my/activities/new";
            private static readonly string Action = MVC.Activities.Form.ActionNames.New;
            public static void MapRoutes(AreaRegistrationContext context, string area, string controller)
            {
                var defaults = new { area, controller, action = Action, };
                var constraints = new { httpMethod = new HttpMethodConstraint("GET"), };
                context.MapRoute(null, Route, defaults, constraints);
            }
        }

        public static class Get
        {
            public const string Route = "my/activities/{number}/edit";
            private static readonly string Action = MVC.Activities.Form.ActionNames.Get;
            public static void MapRoutes(AreaRegistrationContext context, string area, string controller)
            {
                var defaults = new { area, controller, action = Action, };
                var constraints = new
                {
                    httpMethod = new HttpMethodConstraint("GET"),
                    number = new PositiveIntegerRouteConstraint(),
                };
                context.MapRoute(null, Route, defaults, constraints);
            }
        }

        public static class Put
        {
            public const string Route = "my/activities/{number}";
            private static readonly string Action = MVC.Activities.Form.ActionNames.Put;
            public static void MapRoutes(AreaRegistrationContext context, string area, string controller)
            {
                var defaults = new { area, controller, action = Action, };
                var constraints = new
                {
                    httpMethod = new HttpMethodConstraint("POST", "PUT"),
                    number = new PositiveIntegerRouteConstraint(),
                };
                context.MapRoute(null, Route, defaults, constraints);
            }
        }

        // ReSharper restore UnusedMember.Global
    }
}
