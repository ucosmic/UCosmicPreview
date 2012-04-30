using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Web.Routing;
using AutoMapper;
using FluentValidation.Mvc;
using UCosmic.Domain.Email;
using UCosmic.Domain.People;
using UCosmic.Www.Mvc.Areas.Identity.Models;
using UCosmic.Www.Mvc.Controllers;

namespace UCosmic.Www.Mvc.Areas.Identity.Controllers
{
    public class ConfirmEmailServices
    {
        public ConfirmEmailServices(IProcessQueries queryProcessor
            , IHandleCommands<RedeemEmailConfirmationCommand> commandHandler
        )
        {
            QueryProcessor = queryProcessor;
            CommandHandler = commandHandler;
        }

        public IProcessQueries QueryProcessor { get; private set; }
        public IHandleCommands<RedeemEmailConfirmationCommand> CommandHandler { get; private set; }
    }

    [EnforceHttps]
    public partial class ConfirmEmailController : BaseController
    {
        private readonly ConfirmEmailServices _services;

        public ConfirmEmailController(ConfirmEmailServices services)
        {
            _services = services;
        }

        [HttpGet]
        [ActionName("confirm-email")]
        [OpenTopTab(TopTabName.Home)]
        [ValidateConfirmEmail("token")]
        public virtual ActionResult Get(Guid token, string secretCode)
        {
            // get the confirmation from the db
            var confirmation = _services.QueryProcessor.Execute(
                new GetEmailConfirmationQuery(token)
            );
            if (confirmation == null) return HttpNotFound();

            // convert to viewmodel then set the secret code if url confirmation
            var model = Mapper.Map<ConfirmEmailForm>(confirmation);
            model.SecretCode = secretCode;
            model.IsUrlConfirmation = !string.IsNullOrWhiteSpace(secretCode);

            // return partial view
            return PartialView(model);
        }

        [HttpPost]
        [UnitOfWork]
        [ValidateAntiForgeryToken]
        [ActionName("confirm-email")]
        [OpenTopTab(TopTabName.Home)]
        [ValidateConfirmEmail("model")]
        public virtual ActionResult Post(ConfirmEmailForm model)
        {
            if (model == null) return HttpNotFound();

            if (!ModelState.IsValid) return PartialView(model);

            // execute the command
            var command = Mapper.Map<RedeemEmailConfirmationCommand>(model);
            _services.CommandHandler.Handle(command);

            // set temp data values
            TempData.EmailConfirmationTicket(command.Ticket);

            // set feedback message
            SetFeedbackMessage(SuccessMessageForIntent[model.Intent]);

            // redirect to ticketed action
            var redeemedRoute = ValidateConfirmEmailAttribute
                .GetRedeemedRouteValues(model.Token, model.Intent);
            return RedirectToRoute(redeemedRoute);
        }

        public static readonly IDictionary<string, string> SuccessMessageForIntent = new Dictionary<string, string>
        {
            { EmailConfirmationIntent.SignUp, "Your email address has been confirmed. Please create your password now." },
            { EmailConfirmationIntent.PasswordReset, "Your email address has been confirmed. Please reset your password now." },
        };

        [HttpPost]
        public virtual JsonResult ValidateSecretCode(
            [CustomizeValidator(Properties = ConfirmEmailForm.SecretCodePropertyName)] ConfirmEmailForm model)
        {
            return ValidateRemote(ConfirmEmailForm.SecretCodePropertyName);
        }
    }

    public static class ConfirmEmailRouter
    {
        private static readonly string Area = MVC.Identity.Name;
        private static readonly string Controller = MVC.Identity.ConfirmEmail.Name;

        public static void RegisterRoutes(AreaRegistrationContext context)
        {
            RootActionRouter.RegisterRoutes(typeof(ConfirmEmailRouter), context, Area, Controller);
        }

        // ReSharper disable UnusedMember.Global

        public static class Get
        {
            public const string Route = "confirm-email2/{token}/{secretCode}";
            private static readonly string Action = MVC.Identity.ConfirmEmail.ActionNames.Get;
            public static void MapRoutes(AreaRegistrationContext context, string area, string controller)
            {
                var defaults = new
                {
                    area,
                    controller,
                    action = Action,
                    secretCode = UrlParameter.Optional,
                };
                var constraints = new
                {
                    httpMethod = new HttpMethodConstraint("GET"),
                    token = new NonEmptyGuidRouteConstraint(),
                };
                context.MapRoute(null, Route, defaults, constraints);
            }
        }

        public static class Post
        {
            public const string Route = "confirm-email2/{token}";
            private static readonly string Action = MVC.Identity.ConfirmEmail.ActionNames.Post;
            public static void MapRoutes(AreaRegistrationContext context, string area, string controller)
            {
                var defaults = new
                {
                    area,
                    controller,
                    action = Action,
                };
                var constraints = new
                {
                    httpMethod = new HttpMethodConstraint("POST"),
                    token = new NonEmptyGuidRouteConstraint(),
                };
                context.MapRoute(null, Route, defaults, constraints);
            }
        }

        public static class ValidateSecretCode
        {
            public const string Route = "confirm-email2/validate";
            private static readonly string Action = MVC.Identity.ConfirmEmail.ActionNames.ValidateSecretCode;
            public static void MapRoutes(AreaRegistrationContext context, string area, string controller)
            {
                var defaults = new { area, controller, action = Action, };
                var constraints = new
                {
                    httpMethod = new HttpMethodConstraint("POST"),
                };
                context.MapRoute(null, Route, defaults, constraints);
            }
        }

        // ReSharper restore UnusedMember.Global
    }
}
