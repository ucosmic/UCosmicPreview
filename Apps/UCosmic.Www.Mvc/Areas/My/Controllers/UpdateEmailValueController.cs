using System;
using System.Web.Mvc;
using System.Web.Routing;
using AutoMapper;
using FluentValidation.Mvc;
using UCosmic.Domain.People;
using UCosmic.Www.Mvc.Areas.My.Models;
using UCosmic.Www.Mvc.Controllers;

namespace UCosmic.Www.Mvc.Areas.My.Controllers
{
    public class UpdateEmailValueServices
    {
        public UpdateEmailValueServices(
            IProcessQueries queryProcessor
            , IHandleCommands<UpdateMyEmailValueCommand> commandHandler
        )
        {
            QueryProcessor = queryProcessor;
            CommandHandler = commandHandler;
        }

        public IProcessQueries QueryProcessor { get; private set; }
        public IHandleCommands<UpdateMyEmailValueCommand> CommandHandler { get; private set; }
    }

    [Authorize]
    public partial class UpdateEmailValueController : BaseController
    {
        private readonly UpdateEmailValueServices _services;

        public UpdateEmailValueController(UpdateEmailValueServices services)
        {
            _services = services;
        }

        [HttpGet]
        [OpenTopTab(TopTabName.Home)]
        [ActionName("update-email-value")]
        [ReturnUrlReferrer(MyHomeRouter.Get.Route)]
        public virtual ActionResult Get(int number)
        {
            // get the email address
            var email = _services.QueryProcessor.Execute(
                new GetMyEmailAddressByNumberQuery
                {
                    Principal = User,
                    Number = number,
                }
            );

            if (email == null) return HttpNotFound();
            return PartialView(Mapper.Map<UpdateEmailValueForm>(email));
        }

        [HttpPut]
        [UnitOfWork]
        [OpenTopTab(TopTabName.Home)]
        [ActionName("update-email-value")]
        public virtual ActionResult Put(UpdateEmailValueForm model)
        {
            // make sure user owns this email address
            if (model == null || !User.Identity.Name.Equals(model.PersonUserName, StringComparison.OrdinalIgnoreCase))
                return HttpNotFound();

            // make sure model state is valid
            if (!ModelState.IsValid) return PartialView(model);

            // execute command, set feedback message, and redirect
            var command = Mapper.Map<UpdateMyEmailValueCommand>(model);
            command.Principal = User;
            _services.CommandHandler.Handle(command);
            SetFeedbackMessage(command.ChangedState
                ? string.Format(SuccessMessageFormat, model.Value)
                : NoChangesMessage
            );
            return Redirect(model.ReturnUrl);
        }

        public const string SuccessMessageFormat = "Your email address was successfully changed to {0}.";
        public const string NoChangesMessage = "No changes were made.";

        [HttpPost]
        [OutputCache(VaryByParam = "*", Duration = 1800)]
        public virtual JsonResult ValidateValue(
            [CustomizeValidator(Properties = UpdateEmailValueForm.ValuePropertyName)] UpdateEmailValueForm model)
        {
            return ValidateRemote(UpdateEmailValueForm.ValuePropertyName);
        }
    }

    public static class UpdateEmailValueRouter
    {
        private static readonly string Area = MVC.My.Name;
        private static readonly string Controller = MVC.My.UpdateEmailValue.Name;

        public static void RegisterRoutes(AreaRegistrationContext context)
        {
            RootActionRouter.RegisterRoutes(typeof(UpdateEmailValueRouter), context, Area, Controller);
            UpdateEmailValueProfiler.RegisterProfiles();
        }

        // ReSharper disable UnusedMember.Global

        public static class Get
        {
            public const string Route = "my/emails/{number}/change-spelling";
            private static readonly string Action = MVC.My.UpdateEmailValue.ActionNames.Get;
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
            public const string Route = "my/emails/{number}";
            private static readonly string Action = MVC.My.UpdateEmailValue.ActionNames.Put;
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

        public static class ValidateValue
        {
            public const string Route = "my/emails/{number}/change-spelling/validate";
            private static readonly string Action = MVC.My.UpdateEmailValue.ActionNames.ValidateValue;
            public static void MapRoutes(AreaRegistrationContext context, string area, string controller)
            {
                var defaults = new { area, controller, action = Action, };
                var constraints = new
                {
                    httpMethod = new HttpMethodConstraint("POST"),
                    number = new PositiveIntegerRouteConstraint(),
                };
                context.MapRoute(null, Route, defaults, constraints);
            }
        }

        // ReSharper restore UnusedMember.Global
    }
}
