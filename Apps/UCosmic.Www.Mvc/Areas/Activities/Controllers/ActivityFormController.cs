using System;
using System.Linq.Expressions;
using System.Web.Mvc;
using System.Web.Routing;
using AutoMapper;
using UCosmic.Domain.Activities;
using UCosmic.Www.Mvc.Areas.Activities.Models;
using UCosmic.Www.Mvc.Areas.Identity.Controllers;
using UCosmic.Www.Mvc.Controllers;

namespace UCosmic.Www.Mvc.Areas.Activities.Controllers
{
    public class ActivityFormServices
    {
        public ActivityFormServices(IProcessQueries queryProcessor
            , IHandleCommands<CreateMyNewActivityCommand> createCommandHandler
            , IHandleCommands<DraftMyActivityCommand> draftCommandHandler
            , IHandleCommands<UpdateMyActivityCommand> updateCommandHandler
        )
        {
            QueryProcessor = queryProcessor;
            CreateCommandHandler = createCommandHandler;
            DraftCommandHandler = draftCommandHandler;
            UpdateCommandHandler = updateCommandHandler;
        }

        public IProcessQueries QueryProcessor { get; private set; }
        public IHandleCommands<CreateMyNewActivityCommand> CreateCommandHandler { get; private set; }
        public IHandleCommands<DraftMyActivityCommand> DraftCommandHandler { get; private set; }
        public IHandleCommands<UpdateMyActivityCommand> UpdateCommandHandler { get; private set; }
    }

    [Authenticate]
    public partial class ActivityFormController : BaseController
    {
        private readonly ActivityFormServices _services;

        public ActivityFormController(ActivityFormServices services)
        {
            _services = services;
        }

        [HttpGet]
        [UnitOfWork]
        public virtual ActionResult New()
        {
            var command = new CreateMyNewActivityCommand
            {
                Principal = User,
            };
            _services.CreateCommandHandler.Handle(command);
            return RedirectToAction(MVC.Activities.ActivityForm.Get(command.CreatedActivity.Number));
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
            var model = Mapper.Map<ActivityForm>(activity);
            return View(model);
        }

        [HttpPut]
        [UnitOfWork]
        public virtual JsonResult Put(int number, ActivityForm model)
        {
            var command = new UpdateMyActivityCommand
            {
                Principal = User,
                Number = number,
            };
            Mapper.Map(model, command);
            _services.UpdateCommandHandler.Handle(command);
            var message = ModelState.IsValid ? SuccessMessage : null;
            return Json(message);
        }

        public const string SuccessMessage = "Your changes have been saved successfully.";

        [HttpPut]
        [UnitOfWork]
        public virtual JsonResult Draft(int number, ActivityForm model)
        {
            var command = new DraftMyActivityCommand
            {
                Principal = User,
                Number = number,
            };
            Mapper.Map(model, command);
            _services.DraftCommandHandler.Handle(command);
            return Json(null);
        }
    }

    public static class ActivityFormRouter
    {
        private static readonly string Area = MVC.Activities.Name;
        private static readonly string Controller = MVC.Activities.ActivityForm.Name;

        public static void RegisterRoutes(AreaRegistrationContext context)
        {
            RootActionRouter.RegisterRoutes(typeof(ActivityFormRouter), context, Area, Controller);
            ActivityProfiler.RegisterProfiles();
        }

        // ReSharper disable UnusedMember.Global

        public static class New
        {
            public const string Route = "my/activities/new";
            private static readonly string Action = MVC.Activities.ActivityForm.ActionNames.New;
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
            private static readonly string Action = MVC.Activities.ActivityForm.ActionNames.Get;
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
            private static readonly string Action = MVC.Activities.ActivityForm.ActionNames.Put;
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

        public static class Draft
        {
            public const string Route = "my/activities/{number}/draft";
            private static readonly string Action = MVC.Activities.ActivityForm.ActionNames.Draft;
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
