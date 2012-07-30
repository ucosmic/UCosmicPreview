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
            , IHandleCommands<PurgeMyActivityCommand> purgeCommandHandler
        )
        {
            QueryProcessor = queryProcessor;
            CreateCommandHandler = createCommandHandler;
            DraftCommandHandler = draftCommandHandler;
            UpdateCommandHandler = updateCommandHandler;
            PurgeCommandHandler = purgeCommandHandler;
        }

        public IProcessQueries QueryProcessor { get; private set; }
        public IHandleCommands<CreateMyNewActivityCommand> CreateCommandHandler { get; private set; }
        public IHandleCommands<DraftMyActivityCommand> DraftCommandHandler { get; private set; }
        public IHandleCommands<UpdateMyActivityCommand> UpdateCommandHandler { get; private set; }
        public IHandleCommands<PurgeMyActivityCommand> PurgeCommandHandler { get; private set; }
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
        [HttpNotFoundOnNullModel]
        [ActionName("activity-form")]
        [OpenTopTab(TopTabName.FacultyStaff)]
        [ReturnUrlReferrer(MyHomeRouter.Get.Route)]
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
            if (activity == null) return HttpNotFound();
            var model = Mapper.Map<ActivityForm>(activity);
            if (model.Mode == ActivityMode.Protected)
                model.Mode = ActivityMode.Public;
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
            if (model.Mode == ActivityMode.Protected)
                model.Mode = ActivityMode.Public;
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
            if (model.Mode == ActivityMode.Protected)
                model.Mode = ActivityMode.Public;
            Mapper.Map(model, command);
            _services.DraftCommandHandler.Handle(command);
            return Json(null);
        }

        [HttpGet]
        [ActionName("activity-delete")]
        [OpenTopTab(TopTabName.FacultyStaff)]
        public virtual ViewResult Delete(int number, string returnUrl)
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
            model.ReturnUrl = returnUrl;
            return View(model);
        }

        [HttpDelete]
        [UnitOfWork]
        public virtual ActionResult Destroy(int number, string returnUrl)
        {
            var command = new PurgeMyActivityCommand
            {
                Principal = User,
                Number = number,
            };
            _services.PurgeCommandHandler.Handle(command);
            return Redirect(returnUrl);
        }
    }

    public static class ActivityFormRouter
    {
        private static readonly string Area = MVC.Activities.Name;
        private static readonly string Controller = MVC.Activities.ActivityForm.Name;

        public class NewRoute : Route
        {
            public NewRoute()
                : base("my/activities/new", new MvcRouteHandler())
            {
                DataTokens = new RouteValueDictionary(new { area = Area });
                Defaults = new RouteValueDictionary(new
                {
                    controller = Controller,
                    action = MVC.Activities.ActivityForm.ActionNames.New,
                });
                Constraints = new RouteValueDictionary(new
                {
                    httpMethod = new HttpMethodConstraint("GET"),
                });
            }
        }

        public class GetRoute : Route
        {
            public GetRoute()
                : base("my/activities/{number}/edit", new MvcRouteHandler())
            {
                DataTokens = new RouteValueDictionary(new { area = Area });
                Defaults = new RouteValueDictionary(new
                {
                    controller = Controller,
                    action = MVC.Activities.ActivityForm.ActionNames.Get,
                });
                Constraints = new RouteValueDictionary(new
                {
                    httpMethod = new HttpMethodConstraint("GET"),
                    number = new PositiveIntegerRouteConstraint(),
                });
            }
        }

        public class PutRoute : Route
        {
            public PutRoute()
                : base("my/activities/{number}", new MvcRouteHandler())
            {
                DataTokens = new RouteValueDictionary(new { area = Area });
                Defaults = new RouteValueDictionary(new
                {
                    controller = Controller,
                    action = MVC.Activities.ActivityForm.ActionNames.Put,
                });
                Constraints = new RouteValueDictionary(new
                {
                    httpMethod = new HttpMethodOverrideConstraint("POST", "PUT"),
                    number = new PositiveIntegerRouteConstraint(),
                });
            }
        }

        public class DraftRoute : Route
        {
            public DraftRoute()
                : base("my/activities/{number}/draft", new MvcRouteHandler())
            {
                DataTokens = new RouteValueDictionary(new { area = Area });
                Defaults = new RouteValueDictionary(new
                {
                    controller = Controller,
                    action = MVC.Activities.ActivityForm.ActionNames.Draft,
                });
                Constraints = new RouteValueDictionary(new
                {
                    httpMethod = new HttpMethodOverrideConstraint("POST", "PUT"),
                    number = new PositiveIntegerRouteConstraint(),
                });
            }
        }

        public class DeleteRoute : Route
        {
            public DeleteRoute()
                : base("my/activities/{number}/delete", new MvcRouteHandler())
            {
                DataTokens = new RouteValueDictionary(new { area = Area });
                Defaults = new RouteValueDictionary(new
                {
                    controller = Controller,
                    action = MVC.Activities.ActivityForm.ActionNames.Delete,
                });
                Constraints = new RouteValueDictionary(new
                {
                    httpMethod = new HttpMethodConstraint("GET"),
                    number = new PositiveIntegerRouteConstraint(),
                });
            }
        }

        public class DestroyRoute : Route
        {
            public DestroyRoute()
                : base("my/activities/{number}", new MvcRouteHandler())
            {
                DataTokens = new RouteValueDictionary(new { area = Area });
                Defaults = new RouteValueDictionary(new
                {
                    controller = Controller,
                    action = MVC.Activities.ActivityForm.ActionNames.Destroy,
                });
                Constraints = new RouteValueDictionary(new
                {
                    httpMethod = new HttpMethodOverrideConstraint("POST", "DELETE"),
                    number = new PositiveIntegerRouteConstraint(),
                });
            }
        }
    }
}
