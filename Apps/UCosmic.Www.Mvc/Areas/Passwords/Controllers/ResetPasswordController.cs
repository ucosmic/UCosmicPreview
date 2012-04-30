using System;
using System.Web.Mvc;
using System.Web.Routing;
using AutoMapper;
using FluentValidation.Mvc;
using UCosmic.Domain.Email;
using UCosmic.Domain.Identity;
using UCosmic.Domain.People;
using UCosmic.Www.Mvc.Areas.Identity.Controllers;
using UCosmic.Www.Mvc.Areas.Passwords.Models;
using UCosmic.Www.Mvc.Controllers;

namespace UCosmic.Www.Mvc.Areas.Passwords.Controllers
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
        //[ValidateEmailConfirmationTicket(EmailConfirmationIntent.PasswordReset)]
        [ValidateRedeemTicket("token", EmailConfirmationIntent.PasswordReset)]
        public virtual ActionResult Get(Guid token)
        {
            // get the confirmation from the db
            var confirmation = _services.QueryProcessor.Execute(
                new GetEmailConfirmationQuery(token)
            );

            // convert confirmation to form
            var model = Mapper.Map<ResetPasswordForm>(confirmation);

            // return partial view
            return PartialView(model);
        }

        [HttpPost]
        public virtual JsonResult ValidatePasswordConfirmation(
            [CustomizeValidator(Properties = ResetPasswordForm.PasswordConfirmationPropertyName)] ResetPasswordForm model)
        {
            return ValidateRemote(ResetPasswordForm.PasswordConfirmationPropertyName);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [OpenTopTab(TopTabName.Home)]
        [ActionName("reset-password")]
        [ValidateRedeemTicket("model", EmailConfirmationIntent.PasswordReset)]
        public virtual ActionResult Post(ResetPasswordForm model)
        {
            if (model == null) return HttpNotFound();

            if (!ModelState.IsValid) return PartialView(model);

            // execute command
            var command = Mapper.Map<ResetPasswordCommand>(model);
            command.Ticket = TempData.EmailConfirmationTicket();
            _services.CommandHandler.Handle(command);

            // clear the ticket
            TempData.EmailConfirmationTicket(null);

            // set feedback message
            SetFeedbackMessage(SuccessMessage);

            // redirect to sign on
            return RedirectToAction(MVC.Identity.SignOn.Begin());
        }

        public const string SuccessMessage = "You can now use your new password to sign in.";
    }

    public static class ResetPasswordRouter
    {
        private static readonly string Area = MVC.Passwords.Name;
        private static readonly string Controller = MVC.Passwords.ResetPassword.Name;

        public static void RegisterRoutes(AreaRegistrationContext context)
        {
            RootActionRouter.RegisterRoutes(typeof(ResetPasswordRouter), context, Area, Controller);
        }

        // ReSharper disable UnusedMember.Global

        public static class Get
        {
            public const string Route = "reset-password2/{token}";
            private static readonly string Action = MVC.Passwords.ResetPassword.ActionNames.Get;
            public static void MapRoutes(AreaRegistrationContext context, string area, string controller)
            {
                var defaults = new { area, controller, action = Action, };
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
            public const string Route = Get.Route;
            private static readonly string Action = MVC.Passwords.ResetPassword.ActionNames.Post;
            public static void MapRoutes(AreaRegistrationContext context, string area, string controller)
            {
                var defaults = new { area, controller, action = Action, };
                var constraints = new
                {
                    httpMethod = new HttpMethodConstraint("POST"),
                    token = new NonEmptyGuidRouteConstraint(),
                };
                context.MapRoute(null, Route, defaults, constraints);
            }
        }

        public static class ValidatePasswordConfirmation
        {
            public const string Route = "reset-password2/validate";
            private static readonly string Action = MVC.Passwords.ResetPassword.ActionNames.ValidatePasswordConfirmation;
            public static void MapRoutes(AreaRegistrationContext context, string area, string controller)
            {
                var defaults = new { area, controller, action = Action, };
                var constraints = new { httpMethod = new HttpMethodConstraint("POST"), };
                context.MapRoute(null, Route, defaults, constraints);
            }
        }

        // ReSharper restore UnusedMember.Global
    }
}
