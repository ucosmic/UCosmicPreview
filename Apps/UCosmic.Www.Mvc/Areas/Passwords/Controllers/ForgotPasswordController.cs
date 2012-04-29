using System.Web.Mvc;
using System.Web.Routing;
using AutoMapper;
using FluentValidation.Mvc;
using UCosmic.Domain.Email;
using UCosmic.Www.Mvc.Areas.Identity.Controllers;
using UCosmic.Www.Mvc.Areas.Passwords.Models;
using UCosmic.Www.Mvc.Controllers;

namespace UCosmic.Www.Mvc.Areas.Passwords.Controllers
{
    public partial class ForgotPasswordController : BaseController
    {
        private readonly ForgotPasswordServices _services;

        public ForgotPasswordController(ForgotPasswordServices services)
        {
            _services = services;
        }

        [HttpGet]
        [OpenTopTab(TopTabName.Home)]
        [ActionName("forgot-password")]
        [ReturnUrlReferrer(SignOnRouter.Begin.Route)]
        public virtual PartialViewResult Get()
        {
            var model = new ForgotPasswordForm();
            return PartialView(model);
        }

        [HttpPost]
        [UnitOfWork]
        [ValidateAntiForgeryToken]
        [OpenTopTab(TopTabName.Home)]
        [ActionName("forgot-password")]
        public virtual ActionResult Post(ForgotPasswordForm model)
        {
            if (model == null) return HttpNotFound();

            if (!ModelState.IsValid) return PartialView(model);

            // execute command, set feedback message, and redirect
            var command = Mapper.Map<SendConfirmEmailMessageCommand>(model);
            _services.CommandHandler.Handle(command);
            SetFeedbackMessage(string.Format(SuccessMessageFormat, model.EmailAddress));
            return RedirectToRoute(new
            {
                area = MVC.Identity.Name,
                controller = MVC.Identity.ConfirmEmail.Name,
                action = MVC.Identity.ConfirmEmail.ActionNames.Get,
                token = command.ConfirmationToken,
            });
        }

        public const string SuccessMessageFormat = "A password reset email has been sent to {0}.";

        [HttpPost]
        [OutputCache(VaryByParam = "*", Duration = 1800)]
        public virtual JsonResult ValidateEmailAddress(
            [CustomizeValidator(Properties = ForgotPasswordForm.EmailAddressPropertyName)] ForgotPasswordForm model)
        {
            return ValidateRemote(ForgotPasswordForm.EmailAddressPropertyName);
        }
    }

    public class ForgotPasswordServices
    {
        public ForgotPasswordServices(IProcessQueries queryProcessor
            , IHandleCommands<SendConfirmEmailMessageCommand> commandHandler
        )
        {
            QueryProcessor = queryProcessor;
            CommandHandler = commandHandler;
        }

        public IProcessQueries QueryProcessor { get; private set; }
        public IHandleCommands<SendConfirmEmailMessageCommand> CommandHandler { get; private set; }
    }

    public static class ForgotPasswordRouter
    {
        private static readonly string Area = MVC.Passwords.Name;
        private static readonly string Controller = MVC.Passwords.ForgotPassword.Name;

        public static void RegisterRoutes(AreaRegistrationContext context)
        {
            RootActionRouter.RegisterRoutes(typeof(ForgotPasswordRouter), context, Area, Controller);
        }

        // ReSharper disable UnusedMember.Global

        public static class Get
        {
            public const string Route = "forgot-password";
            private static readonly string Action = MVC.Passwords.ForgotPassword.ActionNames.Get;
            public static void MapRoutes(AreaRegistrationContext context, string area, string controller)
            {
                var defaults = new { area, controller, action = Action, };
                var constraints = new { httpMethod = new HttpMethodConstraint("GET"), };
                context.MapRoute(null, Route, defaults, constraints);
            }
        }

        public static class Post
        {
            public const string Route = Get.Route;
            private static readonly string Action = MVC.Passwords.ForgotPassword.ActionNames.Post;
            public static void MapRoutes(AreaRegistrationContext context, string area, string controller)
            {
                var defaults = new { area, controller, action = Action, };
                var constraints = new { httpMethod = new HttpMethodConstraint("POST"), };
                context.MapRoute(null, Route, defaults, constraints);
            }
        }

        public static class ValidateEmailAddress
        {
            public const string Route = "forgot-password/validate";
            private static readonly string Action = MVC.Passwords.ForgotPassword.ActionNames.ValidateEmailAddress;
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
