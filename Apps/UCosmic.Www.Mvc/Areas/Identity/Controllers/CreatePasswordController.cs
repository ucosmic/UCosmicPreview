using System;
using System.Web.Mvc;
using System.Web.Routing;
using AutoMapper;
using FluentValidation.Mvc;
using UCosmic.Domain.Email;
using UCosmic.Domain.Identity;
using UCosmic.Domain.People;
using UCosmic.Www.Mvc.Areas.Identity.Models;
using UCosmic.Www.Mvc.Controllers;

namespace UCosmic.Www.Mvc.Areas.Identity.Controllers
{
    public class CreatePasswordServices
    {
        public CreatePasswordServices(IProcessQueries queryProcessor
            , IHandleCommands<CreatePasswordCommand> commandHandler
        )
        {
            QueryProcessor = queryProcessor;
            CommandHandler = commandHandler;
        }

        public IProcessQueries QueryProcessor { get; private set; }
        public IHandleCommands<CreatePasswordCommand> CommandHandler { get; private set; }
    }

    public partial class CreatePasswordController : BaseController
    {
        private readonly CreatePasswordServices _services;

        public CreatePasswordController(CreatePasswordServices services)
        {
            _services = services;
        }

        [HttpGet]
        [OpenTopTab(TopTabName.Home)]
        [ActionName("create-password")]
        [ValidateRedeemTicket("token", EmailConfirmationIntent.SignUp)]
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
            var model = Mapper.Map<CreatePasswordForm>(confirmation);

            // return partial view
            return View(model);
        }

        [HttpPost]
        public virtual JsonResult ValidatePasswordConfirmation(
            [CustomizeValidator(Properties = CreatePasswordForm.PasswordConfirmationPropertyName)] CreatePasswordForm model)
        {
            return ValidateRemote(CreatePasswordForm.PasswordConfirmationPropertyName);
        }

        [HttpPost]
        [UnitOfWork]
        [ValidateAntiForgeryToken]
        [OpenTopTab(TopTabName.Home)]
        [ActionName("create-password")]
        [ValidateRedeemTicket("model", EmailConfirmationIntent.SignUp)]
        public virtual ActionResult Post(CreatePasswordForm model)
        {
            if (model == null) return HttpNotFound();
            var confirmation = _services.QueryProcessor.Execute(
                new GetEmailConfirmationQuery(model.Token)
            );
            if (confirmation == null) return HttpNotFound();

            if (!ModelState.IsValid) return View(model);

            // execute command
            var command = Mapper.Map<CreatePasswordCommand>(model);
            command.Ticket = TempData.EmailConfirmationTicket();
            _services.CommandHandler.Handle(command);

            // clear the ticket & set the email
            TempData.EmailConfirmationTicket(null);
            TempData.SigningEmailAddress(confirmation.EmailAddress.Value);

            // set feedback message
            SetFeedbackMessage(SuccessMessage);

            // redirect to sign on
            return RedirectToAction(MVC.Identity.SignIn.Get());
        }

        public const string SuccessMessage = "You can now use your password to sign on.";
    }

    public static class CreatePasswordRouter
    {
        private static readonly string Area = MVC.Identity.Name;
        private static readonly string Controller = MVC.Identity.CreatePassword.Name;

        public static void RegisterRoutes(AreaRegistrationContext context)
        {
            RootActionRouter.RegisterRoutes(typeof(CreatePasswordRouter), context, Area, Controller);
            CreatePasswordProfiler.RegisterProfiles();
        }

        // ReSharper disable UnusedMember.Global

        public static class Get
        {
            public const string Route = "create-password/{token}";
            private static readonly string Action = MVC.Identity.CreatePassword.ActionNames.Get;
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
            private static readonly string Action = MVC.Identity.CreatePassword.ActionNames.Post;
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
            public const string Route = "create-password/validate";
            private static readonly string Action = MVC.Identity.ResetPassword.ActionNames.ValidatePasswordConfirmation;
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
