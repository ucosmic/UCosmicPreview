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
        [ReturnUrlReferrer(SignInRouter.GetRoute.SignInUrl)]
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

        public class GetRoute : Route
        {
            public GetRoute()
                : base("forgot-password", new MvcRouteHandler())
            {
                DataTokens = new RouteValueDictionary(new { area = Area });
                Defaults = new RouteValueDictionary(new
                {
                    controller = Controller,
                    action = MVC.Identity.ForgotPassword.ActionNames.Get,
                });
                Constraints = new RouteValueDictionary(new
                {
                    httpMethod = new HttpMethodConstraint("GET"),
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
                    action = MVC.Identity.ForgotPassword.ActionNames.Post,
                });
                Constraints = new RouteValueDictionary(new
                {
                    httpMethod = new HttpMethodConstraint("POST"),
                });
            }
        }

        public class ValidateEmailAddressRoute : Route
        {
            public ValidateEmailAddressRoute()
                : base("forgot-password/validate", new MvcRouteHandler())
            {
                DataTokens = new RouteValueDictionary(new { area = Area });
                Defaults = new RouteValueDictionary(new
                {
                    controller = Controller,
                    action = MVC.Identity.ForgotPassword.ActionNames.ValidateEmailAddress,
                });
                Constraints = new RouteValueDictionary(new
                {
                    httpMethod = new HttpMethodConstraint("POST"),
                });
            }
        }
    }
}
