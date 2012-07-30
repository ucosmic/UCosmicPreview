using System.Web.Mvc;
using System.Web.Routing;
using AutoMapper;
using FluentValidation.Mvc;
using UCosmic.Domain.Identity;
using UCosmic.Www.Mvc.Areas.Identity.Models;
using UCosmic.Www.Mvc.Controllers;

namespace UCosmic.Www.Mvc.Areas.Identity.Controllers
{
    public class ForgotPasswordServices
    {
        public ForgotPasswordServices(
            IHandleCommands<SendConfirmEmailMessageCommand> commandHandler
        )
        {
            CommandHandler = commandHandler;
        }

        public IHandleCommands<SendConfirmEmailMessageCommand> CommandHandler { get; private set; }
    }

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
        [ReturnUrlReferrer(SignInRouter.Get.Route)]
        public virtual ViewResult Get()
        {
            var model = new ForgotPasswordForm
            {
                EmailAddress = HttpContext.SigningEmailAddressCookie() ??
                               TempData.SigningEmailAddress(),
            };
            return View(model);
        }

        [HttpPost]
        [OutputCache(VaryByParam = "*", Duration = 1800)]
        public virtual JsonResult ValidateEmailAddress(
            [CustomizeValidator(Properties = ForgotPasswordForm.EmailAddressPropertyName)] ForgotPasswordForm model)
        {
            return ValidateRemote(ForgotPasswordForm.EmailAddressPropertyName);
        }

        [HttpPost]
        [UnitOfWork]
        [ValidateAntiForgeryToken]
        [OpenTopTab(TopTabName.Home)]
        [ActionName("forgot-password")]
        public virtual ActionResult Post(ForgotPasswordForm model)
        {
            if (model == null) return HttpNotFound();

            if (!ModelState.IsValid) return View(model);

            // execute command
            var command = Mapper.Map<SendConfirmEmailMessageCommand>(model);
            command.SendFromUrl = Url.Action(MVC.Identity.ForgotPassword.Get());
            _services.CommandHandler.Handle(command);

            // flash feedback message
            SetFeedbackMessage(string.Format(SuccessMessageFormat, model.EmailAddress));

            // redirect to confirm email
            return RedirectToRoute(new
            {
                area = MVC.Identity.Name,
                controller = MVC.Identity.ConfirmEmail.Name,
                action = MVC.Identity.ConfirmEmail.ActionNames.Get,
                token = command.ConfirmationToken,
            });
        }

        public const string SuccessMessageFormat = "A password reset email has been sent to {0}.";
    }

    public static class ForgotPasswordRouter
    {
        private static readonly string Area = MVC.Identity.Name;
        private static readonly string Controller = MVC.Identity.ForgotPassword.Name;

        public static void RegisterRoutes(AreaRegistrationContext context)
        {
            RootActionRouter.RegisterRoutes(typeof(ForgotPasswordRouter), context, Area, Controller);
        }

        // ReSharper disable UnusedMember.Global

        public static class Get
        {
            public const string Route = "forgot-password";
            private static readonly string Action = MVC.Identity.ForgotPassword.ActionNames.Get;
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
            private static readonly string Action = MVC.Identity.ForgotPassword.ActionNames.Post;
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
            private static readonly string Action = MVC.Identity.ForgotPassword.ActionNames.ValidateEmailAddress;
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
