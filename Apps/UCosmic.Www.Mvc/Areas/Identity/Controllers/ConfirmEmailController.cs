using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Web.Routing;
using AutoMapper;
using FluentValidation.Mvc;
using UCosmic.Domain.Identity;
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
            model.IsUrlConfirmation = !String.IsNullOrWhiteSpace(secretCode);

            // return partial view
            return View(model);
        }

        [HttpPost]
        public virtual JsonResult ValidateSecretCode(
            [CustomizeValidator(Properties = ConfirmEmailForm.SecretCodePropertyName)] ConfirmEmailForm model)
        {
            return ValidateRemote(ConfirmEmailForm.SecretCodePropertyName);
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

            if (!ModelState.IsValid) return View(model);

            // execute the command
            var command = Mapper.Map<RedeemEmailConfirmationCommand>(model);
            _services.CommandHandler.Handle(command);

            // set temp data values
            TempData.EmailConfirmationTicket(command.Ticket);

            // set feedback message
            SetFeedbackMessage(SuccessMessageForIntent[model.Intent]);

            // redirect to ticketed action
            var redeemedRoute = GetRedeemedRouteValues(model.Token, model.Intent);
            return RedirectToRoute(redeemedRoute);
        }

        public static readonly IDictionary<EmailConfirmationIntent, string> SuccessMessageForIntent =
            new Dictionary<EmailConfirmationIntent, string>
            {
                { EmailConfirmationIntent.CreatePassword, "Your email address has been confirmed. Please create your password now." },
                { EmailConfirmationIntent.ResetPassword, "Your email address has been confirmed. Please reset your password now." },
            };

        internal static RouteValueDictionary GetRedeemedRouteValues(Guid token, EmailConfirmationIntent intent)
        {
            switch (intent)
            {
                case EmailConfirmationIntent.ResetPassword:
                    return new RouteValueDictionary(new
                    {
                        area = MVC.Identity.Name,
                        controller = MVC.Identity.ResetPassword.Name,
                        action = MVC.Identity.ResetPassword.ActionNames.Get,
                        token,
                    });
                case EmailConfirmationIntent.CreatePassword:
                    return new RouteValueDictionary(new
                    {
                        area = MVC.Identity.Name,
                        controller = MVC.Identity.CreatePassword.Name,
                        action = MVC.Identity.CreatePassword.ActionNames.Get,
                        token,
                    });
                default:
                    throw new NotSupportedException(String.Format(
                        "The email confirmation intent '{0}' is not supported.",
                        intent));
            }
        }
    }

    public static class ConfirmEmailRouter
    {
        private static readonly string Area = MVC.Identity.Name;
        private static readonly string Controller = MVC.Identity.ConfirmEmail.Name;

        public class GetRoute : MvcRoute
        {
            public GetRoute()
            {
                Url = "confirm-email/{token}/{secretCode}";
                DataTokens = new RouteValueDictionary(new { area = Area });
                Defaults = new RouteValueDictionary(new
                {
                    controller = Controller,
                    action = MVC.Identity.ConfirmEmail.ActionNames.Get,
                    secretCode = UrlParameter.Optional,
                });
                Constraints = new RouteValueDictionary(new
                {
                    httpMethod = new HttpMethodConstraint("GET"),
                    token = new NonEmptyGuidRouteConstraint(),
                });
            }
        }

        public class PostRoute : MvcRoute
        {
            public PostRoute()
            {
                Url = "confirm-email/{token}";
                DataTokens = new RouteValueDictionary(new { area = Area });
                Defaults = new RouteValueDictionary(new
                {
                    controller = Controller,
                    action = MVC.Identity.ConfirmEmail.ActionNames.Post,
                });
                Constraints = new RouteValueDictionary(new
                {
                    httpMethod = new HttpMethodConstraint("POST"),
                    token = new NonEmptyGuidRouteConstraint(),
                });
            }
        }

        public class ValidateSecretCodeRoute : MvcRoute
        {
            public ValidateSecretCodeRoute()
            {
                Url = "confirm-email/validate";
                DataTokens = new RouteValueDictionary(new { area = Area });
                Defaults = new RouteValueDictionary(new
                {
                    controller = Controller,
                    action = MVC.Identity.ConfirmEmail.ActionNames.ValidateSecretCode,
                });
                Constraints = new RouteValueDictionary(new
                {
                    httpMethod = new HttpMethodConstraint("POST"),
                });
            }
        }
    }
}
