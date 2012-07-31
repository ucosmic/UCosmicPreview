using System;
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
    public class ResetPasswordServices
    {
        public ResetPasswordServices(IProcessQueries queryProcessor
            , IHandleCommands<ResetPasswordCommand> commandHandler
        )
        {
            QueryProcessor = queryProcessor;
            CommandHandler = commandHandler;
        }

        public IProcessQueries QueryProcessor { get; private set; }
        public IHandleCommands<ResetPasswordCommand> CommandHandler { get; private set; }
    }

    public partial class ResetPasswordController : BaseController
    {
        private readonly ResetPasswordServices _services;

        public ResetPasswordController(ResetPasswordServices services)
        {
            _services = services;
        }

        [HttpGet]
        [OpenTopTab(TopTabName.Home)]
        [ActionName("reset-password")]
        [ValidateRedeemTicket("token", EmailConfirmationIntent.ResetPassword)]
        public virtual ActionResult Get(Guid token)
        {
            // skip when there is an empty token
            if (token == Guid.Empty) return HttpNotFound();

            // get the confirmation from the db
            var confirmation = _services.QueryProcessor.Execute(
                new GetEmailConfirmationQuery(token)
            );
            if (confirmation == null) return HttpNotFound();

            // convert confirmation to form
            var model = Mapper.Map<ResetPasswordForm>(confirmation);

            // return partial view
            return View(model);
        }

        [HttpPost]
        public virtual JsonResult ValidatePasswordConfirmation(
            [CustomizeValidator(Properties = ResetPasswordForm.PasswordConfirmationPropertyName)] ResetPasswordForm model)
        {
            return ValidateRemote(ResetPasswordForm.PasswordConfirmationPropertyName);
        }

        [HttpPost]
        [UnitOfWork]
        [ValidateAntiForgeryToken]
        [OpenTopTab(TopTabName.Home)]
        [ActionName("reset-password")]
        [ValidateRedeemTicket("model", EmailConfirmationIntent.ResetPassword)]
        public virtual ActionResult Post(ResetPasswordForm model)
        {
            if (model == null) return HttpNotFound();

            if (!ModelState.IsValid) return View(model);

            // execute command
            var command = Mapper.Map<ResetPasswordCommand>(model);
            command.Ticket = TempData.EmailConfirmationTicket();
            _services.CommandHandler.Handle(command);

            // clear the ticket
            TempData.EmailConfirmationTicket(null);

            // set feedback message
            SetFeedbackMessage(SuccessMessage);

            // redirect to sign on
            return RedirectToAction(MVC.Identity.SignIn.Get());
        }

        public const string SuccessMessage = "You can now use your new password to sign on.";
    }

    public static class ResetPasswordRouter
    {
        private static readonly string Area = MVC.Identity.Name;
        private static readonly string Controller = MVC.Identity.ResetPassword.Name;

        public class GetRoute : Route
        {
            public GetRoute()
                : base("reset-password/{token}", new MvcRouteHandler())
            {
                DataTokens = new RouteValueDictionary(new { area = Area });
                Defaults = new RouteValueDictionary(new
                {
                    controller = Controller,
                    action = MVC.Identity.ResetPassword.ActionNames.Get,
                });
                Constraints = new RouteValueDictionary(new
                {
                    httpMethod = new HttpMethodConstraint("GET"),
                    token = new NonEmptyGuidRouteConstraint(),
                });
            }
        }

        public class PostRoute : GetRoute
        {
            public PostRoute()
            {
                Defaults = new RouteValueDictionary(new
                {
                    controller = Controller,
                    action = MVC.Identity.ResetPassword.ActionNames.Post,
                });
                Constraints = new RouteValueDictionary(new
                {
                    httpMethod = new HttpMethodConstraint("POST"),
                    token = new NonEmptyGuidRouteConstraint(),
                });
            }
        }

        public class ValidatePasswordConfirmationRoute : Route
        {
            public ValidatePasswordConfirmationRoute()
                : base("reset-password/validate", new MvcRouteHandler())
            {
                DataTokens = new RouteValueDictionary(new { area = Area });
                Defaults = new RouteValueDictionary(new
                {
                    controller = Controller,
                    action = MVC.Identity.ResetPassword.ActionNames.ValidatePasswordConfirmation,
                });
                Constraints = new RouteValueDictionary(new
                {
                    httpMethod = new HttpMethodConstraint("POST"),
                });
            }
        }
    }
}
