using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;
using AutoMapper;
using FluentValidation.Mvc;
using UCosmic.Domain.Email;
using UCosmic.Domain.Identity;
using UCosmic.Www.Mvc.Areas.Identity.Models;
using UCosmic.Www.Mvc.Areas.Passwords.Models;
using UCosmic.Www.Mvc.Controllers;
using UCosmic.Www.Mvc.Routes;
using UCosmic.Domain.People;

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
        [ValidateEmailConfirmationTicket(EmailConfirmationIntent.PasswordReset)]
        public virtual ActionResult Get(ResetPasswordQuery query)
        {
            if (query == null) return HttpNotFound();

            // return denial view if there is a problem
            var model = Mapper.Map<ResetPasswordForm>(query);
            if (!ModelState.IsValid) return DeniedView(model);

            // query to map domain values into the model
            var confirmation = _services.QueryProcessor.Execute(
                new GetEmailConfirmationQuery(query.Token)
            );

            // convert confirmation to form
            model = Mapper.Map<ResetPasswordForm>(confirmation);

            return PartialView(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [OpenTopTab(TopTabName.Home)]
        [ActionName("reset-password")]
        [ValidateEmailConfirmationTicket(EmailConfirmationIntent.PasswordReset)]
        public virtual ActionResult Post(ResetPasswordForm model)
        {
            if (model == null) return HttpNotFound();

            if (!ModelState.IsValid) return DeniedView(model);

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

        [NonAction]
        private ActionResult DeniedView(ResetPasswordForm model)
        {
            // return 404 if the confirmation could not be found
            if (ModelState.ContainsKey(ConfirmEmailForm.TokenPropertyName) &&
                ModelState[ConfirmEmailForm.TokenPropertyName].Errors.Any())
                return HttpNotFound();

            // deny the action if the ticket is not valid
            if (ModelState.ContainsKey(ValidateEmailConfirmationTicketAttribute.TicketPropertyName) &&
                ModelState[ValidateEmailConfirmationTicketAttribute.TicketPropertyName].Errors.Any())
                return PartialView(MVC.Passwords.ResetPassword.Views._denied);

            // deny the action if the intent is not to reset password
            if (ModelState.ContainsKey(ValidateEmailConfirmationTicketAttribute.IntentPropertyName) &&
                ModelState[ValidateEmailConfirmationTicketAttribute.IntentPropertyName].Errors.Any())
                return PartialView(MVC.Passwords.ResetPassword.Views._denied);

            return PartialView(MVC.Passwords.ResetPassword.Views.reset_password, model);
        }

        public const string SuccessMessage = "You can now use your new password to sign in.";

        [HttpPost]
        public virtual JsonResult ValidatePasswordConfirmation(
            [CustomizeValidator(Properties = ResetPasswordForm.PasswordConfirmationPropertyName)] ResetPasswordForm model)
        {
            return ValidateRemote(ResetPasswordForm.PasswordConfirmationPropertyName);
        }
    }

    public static class ResetPasswordRouter
    {
        private static readonly string Area = MVC.Passwords.Name;
        private static readonly string Controller = MVC.Passwords.ResetPassword.Name;

        public static void RegisterRoutes(AreaRegistrationContext context)
        {
            DefaultRouter.RegisterRoutes(typeof(ResetPasswordRouter), context, Area, Controller);
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
            public const string Route = "reset-password2";
            private static readonly string Action = MVC.Passwords.ResetPassword.ActionNames.Post;
            public static void MapRoutes(AreaRegistrationContext context, string area, string controller)
            {
                var defaults = new { area, controller, action = Action, };
                var constraints = new { httpMethod = new HttpMethodConstraint("POST"), };
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
