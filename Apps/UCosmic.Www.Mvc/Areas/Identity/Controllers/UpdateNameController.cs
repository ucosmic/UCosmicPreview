using System;
using System.Linq.Expressions;
using System.Web.Mvc;
using System.Web.Routing;
using AutoMapper;
using UCosmic.Domain.Identity;
using UCosmic.Www.Mvc.Areas.Identity.Models;
using UCosmic.Www.Mvc.Controllers;
using UCosmic.Domain.People;

namespace UCosmic.Www.Mvc.Areas.Identity.Controllers
{
    public class UpdateNameServices
    {
        public UpdateNameServices(
            IProcessQueries queryProcessor
            , IHandleCommands<UpdateMyNameCommand> commandHandler
        )
        {
            QueryProcessor = queryProcessor;
            CommandHandler = commandHandler;
        }

        public IProcessQueries QueryProcessor { get; private set; }
        public IHandleCommands<UpdateMyNameCommand> CommandHandler { get; private set; }
    }

    [Authorize]
    public partial class UpdateNameController : BaseController
    {
        private readonly UpdateNameServices _services;

        public UpdateNameController(UpdateNameServices services)
        {
            _services = services;
        }

        [HttpGet]
        [NullLayoutOnChildAction]
        [ActionName("update-name")]
        [OpenTopTab(TopTabName.Home)]
        public virtual ActionResult Get()
        {
            var user = _services.QueryProcessor.Execute(
                new GetUserByNameQuery
                {
                    Name = User.Identity.Name,
                    EagerLoad = new Expression<Func<User, object>>[]
                    {
                        u => u.Person.Emails,
                    }
                }
            );

            if (user == null) return HttpNotFound();
            return PartialView(Mapper.Map<UpdateNameForm>(user.Person));
        }

        [HttpPut]
        [UnitOfWork]
        [OpenTopTab(TopTabName.Home)]
        [ActionName("update-name")]
        public virtual ActionResult Put(UpdateNameForm model)
        {
            // make sure model is not null
            if (model == null) return HttpNotFound();

            // make sure model state is valid
            if (!ModelState.IsValid) return PartialView(model);

            // execute command, set feedback message, and redirect
            var command = Mapper.Map<UpdateMyNameCommand>(model);
            command.Principal = User;
            _services.CommandHandler.Handle(command);
            SetFeedbackMessage(command.ChangedState
                ? SuccessMessage
                : NoChangesMessage
            );
            return Redirect(UpdateNameForm.ReturnUrl);
        }

        public const string SuccessMessage = "Your info was successfully updated.";
        public const string NoChangesMessage = "No changes were made.";
    }

    public static class UpdateNameRouter
    {
        private static readonly string Area = MVC.Identity.Name;
        private static readonly string Controller = MVC.Identity.UpdateName.Name;

        public static void RegisterRoutes(AreaRegistrationContext context)
        {
            RootActionRouter.RegisterRoutes(typeof(UpdateNameRouter), context, Area, Controller);
            UpdateNameProfiler.RegisterProfiles();
        }

        // ReSharper disable UnusedMember.Global

        public static class Get
        {
            public const string Route = "my/name/edit";
            private static readonly string Action = MVC.Identity.UpdateName.ActionNames.Get;
            public static void MapRoutes(AreaRegistrationContext context, string area, string controller)
            {
                var defaults = new { area, controller, action = Action, };
                var constraints = new { httpMethod = new HttpMethodConstraint("GET"), };
                context.MapRoute(null, Route, defaults, constraints);
            }
        }

        public static class Put
        {
            public const string Route = "my/name";
            private static readonly string Action = MVC.Identity.UpdateName.ActionNames.Put;
            public static void MapRoutes(AreaRegistrationContext context, string area, string controller)
            {
                var defaults = new { area, controller, action = Action, };
                var constraints = new { httpMethod = new HttpMethodConstraint("POST", "PUT"), };
                context.MapRoute(null, Route, defaults, constraints);
            }
        }
    }
}
