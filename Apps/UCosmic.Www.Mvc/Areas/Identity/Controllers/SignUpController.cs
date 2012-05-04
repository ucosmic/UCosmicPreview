using System.Web.Mvc;
using UCosmic.Www.Mvc.Areas.Identity.Models;
using UCosmic.Www.Mvc.Controllers;
using System.Web.Routing;
using UCosmic.Domain.Establishments;
using AutoMapper;

namespace UCosmic.Www.Mvc.Areas.Identity.Controllers
{
    public class SignUpServices
    {
        public SignUpServices(IHandleCommands<SendSignUpMessageCommand> commandHandler)
        {
            CommandHandler = commandHandler;
        }

        public IHandleCommands<SendSignUpMessageCommand> CommandHandler { get; private set; }
    }

    [EnforceHttps]
    public partial class SignUpController : BaseController
    {
        private readonly SignUpServices _services;

        public SignUpController(SignUpServices services)
        {
            _services = services;
        }

        [HttpGet]
        [ActionName("sign-up")]
        [OpenTopTab(TopTabName.Home)]
        [ValidateSigningEmail]
        public virtual ActionResult Get(string returnUrl)
        {
            var model = new SignUpForm(HttpContext, TempData, returnUrl);
            return View(model);
        }

        [HttpPost]
        [UnitOfWork]
        [ActionName("sign-up")]
        [OpenTopTab(TopTabName.Home)]
        [ValidateAntiForgeryToken]
        [ValidateSigningEmail(ParamName = "model")]
        public virtual ActionResult Post(SignUpForm model)
        {
            if (model == null) return HttpNotFound();

            if (!ModelState.IsValid) return PartialView(model);

            // execute command
            var command = Mapper.Map<SendSignUpMessageCommand>(model);
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

        public const string SuccessMessageFormat = "A sign up confirmation email has been sent to {0}.";
    }

    public static class SignUpRouter
    {
        private static readonly string Area = MVC.Identity.Name;
        private static readonly string Controller = MVC.Identity.SignUp.Name;

        public static void RegisterRoutes(AreaRegistrationContext context)
        {
            RootActionRouter.RegisterRoutes(typeof(SignUpRouter), context, Area, Controller);
        }

        // ReSharper disable UnusedMember.Global

        public static class Get
        {
            public const string Route = "sign-up";
            private static readonly string Action = MVC.Identity.SignUp.ActionNames.Get;
            public static void MapRoutes(AreaRegistrationContext context, string area, string controller)
            {
                var defaults = new { area, controller, action = Action, };
                var constraints = new { httpMethod = new HttpMethodConstraint("GET") };
                context.MapRoute(null, Route, defaults, constraints);
            }
        }

        public static class Post
        {
            public const string Route = Get.Route;
            private static readonly string Action = MVC.Identity.SignUp.ActionNames.Post;
            public static void MapRoutes(AreaRegistrationContext context, string area, string controller)
            {
                var defaults = new { area, controller, action = Action, };
                var constraints = new { httpMethod = new HttpMethodConstraint("POST") };
                context.MapRoute(null, Route, defaults, constraints);
            }
        }

        // ReSharper restore UnusedMember.Global
    }
}
