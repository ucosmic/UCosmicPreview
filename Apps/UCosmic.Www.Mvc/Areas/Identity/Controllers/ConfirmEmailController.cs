using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;
using AutoMapper;
using FluentValidation.Mvc;
using UCosmic.Domain.Email;
using UCosmic.Domain.People;
using UCosmic.Www.Mvc.Areas.Identity.Models;
using UCosmic.Www.Mvc.Controllers;
using UCosmic.Www.Mvc.Routes;

namespace UCosmic.Www.Mvc.Areas.Identity.Controllers
{
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
        public virtual ActionResult Get(ConfirmEmailQuery query)
        {
            if (query == null) return HttpNotFound();

            // first map the secret code into the model
            var model = Mapper.Map<ConfirmEmailForm>(query);
            if (!ModelState.IsValid) return DeniedView(model);

            // query to map domain values into the model
            var confirmation = _services.QueryProcessor.Execute(
                new GetEmailConfirmationQuery(query.Token)
            );

            // map everything except the secret code
            Mapper.Map(confirmation, model);

            return PartialView(model);
        }

        [HttpPost]
        [UnitOfWork]
        [ValidateAntiForgeryToken]
        [ActionName("confirm-email")]
        [OpenTopTab(TopTabName.Home)]
        public virtual ActionResult Post(ConfirmEmailForm model)
        {
            if (model == null) return HttpNotFound();

            if (!ModelState.IsValid) return DeniedView(model);

            // execute the command
            var command = Mapper.Map<RedeemEmailConfirmationCommand>(model);
            _services.CommandHandler.Handle(command);

            // set feedback message
            SetFeedbackMessage(SuccessMessageForIntent[model.Intent]);

            // set temp data values
            TempData.EmailConfirmationTicket(command.Ticket);

            return RedirectToTicketAction(model);
        }

        [NonAction]
        private ActionResult DeniedView(ConfirmEmailForm model)
        {
            // return 404 if the confirmation could not be found
            if (ModelState.ContainsKey(ConfirmEmailForm.TokenPropertyName) &&
                ModelState[ConfirmEmailForm.TokenPropertyName].Errors.Any())
                return HttpNotFound();

            // confirmations can only be redeemed within 2 hours of issuance.
            if (ModelState.ContainsKey(ConfirmEmailForm.IsExpiredPropertyName) &&
                ModelState[ConfirmEmailForm.IsExpiredPropertyName].Errors.Any())
                return PartialView(MVC.Identity.ConfirmEmail.Views._denied,
                    new ConfirmDeniedModel(ConfirmDeniedBecause.ConfirmationIsExpired, model.Intent));

            // if confirmation is already redeemed, forward to ticketed step
            if (ModelState.ContainsKey(ConfirmEmailForm.IsRedeemedPropertyName) &&
                ModelState[ConfirmEmailForm.IsRedeemedPropertyName].Errors.Any())
                return RedirectToTicketAction(model);

            return PartialView(MVC.Identity.ConfirmEmail.Views.confirm_email, model);
        }

        [NonAction]
        private ActionResult RedirectToTicketAction(ConfirmEmailForm model)
        {
            switch (model.Intent)
            {
                case EmailConfirmationIntent.PasswordReset:
                    return RedirectToRoute(new
                    {
                        area = MVC.Passwords.Name,
                        controller = MVC.Passwords.ResetPassword.Name,
                        action = MVC.Passwords.ResetPassword.ActionNames.Get,
                        token = model.Token,
                    });
            }
            throw new NotSupportedException(string.Format(
                "The email confirmation intent '{0}' is not supported.",
                    model.Intent));
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

    public static class ConfirmEmailRouter
    {
        private static readonly string Area = MVC.Identity.Name;
        private static readonly string Controller = MVC.Identity.ConfirmEmail.Name;

        public static void RegisterRoutes(AreaRegistrationContext context)
        {
            DefaultRouter.RegisterRoutes(typeof(ConfirmEmailRouter), context, Area, Controller);
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
