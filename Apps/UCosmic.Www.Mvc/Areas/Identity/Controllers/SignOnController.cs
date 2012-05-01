using System;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Mvc;
using System.Web.Routing;
using FluentValidation.Mvc;
using UCosmic.Domain.Establishments;
using UCosmic.Www.Mvc.Areas.Identity.Models;
using UCosmic.Www.Mvc.Controllers;

namespace UCosmic.Www.Mvc.Areas.Identity.Controllers
{
    public class SignOnServices
    {
        public SignOnServices(
            ISignUsers userSigner
            , ISignMembers members
            , IProcessQueries queryProcessor
            , IHandleCommands<SendSamlAuthnRequestCommand> authnRequestHandler
            , IHandleCommands<SignOnSamlUserCommand> authnResponseHandler
        )
        {
            UserSigner = userSigner;
            Members = members;
            QueryProcessor = queryProcessor;
            SendSamlAuthnRequestHandler = authnRequestHandler;
            SignOnSamlUserHandler = authnResponseHandler;
        }

        public ISignUsers UserSigner { get; private set; }
        public ISignMembers Members { get; private set; }
        public IProcessQueries QueryProcessor { get; private set; }
        public IHandleCommands<SendSamlAuthnRequestCommand> SendSamlAuthnRequestHandler { get; private set; }
        public IHandleCommands<SignOnSamlUserCommand> SignOnSamlUserHandler { get; private set; }

    }

    public partial class SignOnController : BaseController
    {
        #region Construction & DI

        private readonly SignOnServices _services;

        public SignOnController(SignOnServices services)
        {
            _services = services;
        }

        #endregion
        #region SignOn

        [HttpPost]
        [OutputCache(VaryByParam = SignOnBeginForm.EmailAddressPropertyName, Duration = 1800)]
        public virtual JsonResult ValidateEmailAddress(
            [CustomizeValidator(Properties = SignOnBeginForm.EmailAddressPropertyName)] SignOnBeginForm model)
        {
            // form must not be valid unless email address is eligible
            return ValidateRemote(SignOnBeginForm.EmailAddressPropertyName);
        }

        [HttpGet]
        [ActionName("sign-on")]
        [OpenTopTab(TopTabName.Home)]
        public virtual ActionResult Begin(string returnUrl)
        {
            // there are certain URL's we don't want to return to after signing on
            if (!string.IsNullOrWhiteSpace(returnUrl))
            {
                var correctedReturnUrl = GetReturnUrl(returnUrl);
                if (correctedReturnUrl != returnUrl)
                    return RedirectToAction(MVC.Identity.SignOn.Begin(correctedReturnUrl));
            }

            // pass the return url into the view model for POST
            var model = new SignOnBeginForm { ReturnUrl = returnUrl };
            return View(model);
        }

        [HttpPost]
        [UnitOfWork]
        [ActionName("sign-on")]
        [OpenTopTab(TopTabName.Home)]
        public virtual ActionResult Begin(SignOnBeginForm model)
        {
            if (ModelState.IsValid)
            {
                // get the establishment for this email address
                var establishment = _services.QueryProcessor.Execute(
                    new GetEstablishmentByEmailQuery
                    {
                        Email = model.EmailAddress,
                        EagerLoad = new Expression<Func<Establishment, object>>[]
                        {
                            e => e.SamlSignOn,
                        }
                    }
                );

                // sign on user using SAML2
                if (establishment.HasSamlSignOn())
                {
                    _services.SendSamlAuthnRequestHandler.Handle(
                        new SendSamlAuthnRequestCommand
                        {
                            SamlSignOn = establishment.SamlSignOn,
                            ReturnUrl = model.ReturnUrl,
                            HttpContext = HttpContext,
                        }
                    );
                    return new EmptyResult();
                }
            }
            return View(model);
        }

        [HttpPost]
        [UnitOfWork]
        [ActionName("post")]
        public virtual ActionResult Saml2Post()
        {
            var command = new SignOnSamlUserCommand
            {
                SsoBinding = Saml2SsoBinding.HttpPost,
                HttpContext = HttpContext,
            };

            _services.SignOnSamlUserHandler.Handle(command);

            return Redirect(GetReturnUrl(command.ReturnUrl));
        }

        [NonAction]
        private string GetReturnUrl(string suggestedReturnUrl)
        {
            suggestedReturnUrl = !string.IsNullOrWhiteSpace(suggestedReturnUrl)
                ? suggestedReturnUrl
                : _services.UserSigner.DefaultSignedOnUrl;

            // return URL should not lead to the following pages:
            var invalidReturnUrls = new[]
            {
                Url.Action(MVC.Identity.SignIn.SignIn()),               // back to sign in
                Url.Action(MVC.Identity.SignOn.Begin()),                // back to sign on
                Url.Action(MVC.Identity.SignIn.SignOut()),              // over to sign out
                Url.Action(MVC.Identity.SignUp.SendEmail()),            // over to sign up
                "/sign-up/confirm-email/",                              // sign up email confirmation
                "/confirm-password-reset/t-",                           // password reset email confirmation
                Url.Action(MVC.Identity.Password.ForgotPassword()),     // over to password reset
            };

            //// foreach conversion to linq expression
            //foreach (var invalidReturnUrl in invalidReturnUrls)
            //{
            //    if (suggestedReturnUrl.StartsWith(invalidReturnUrl, StringComparison.OrdinalIgnoreCase))
            //        return _identityFacade.UserSigner.DefaultSignedInUrl;
            //}
            var returnUrl = invalidReturnUrls.Any(invalidReturnUrl =>
                suggestedReturnUrl.StartsWith(invalidReturnUrl, StringComparison.OrdinalIgnoreCase)) ||
                    suggestedReturnUrl == "/" // sign in from root should go to default url
                    ? _services.UserSigner.DefaultSignedOnUrl
                    : suggestedReturnUrl;

            return returnUrl;
        }

        #endregion
    }

    public static class SignOnRouter
    {
        private static readonly string Area = MVC.Identity.Name;
        private static readonly string Controller = MVC.Identity.SignOn.Name;

        public static void RegisterRoutes(AreaRegistrationContext context)
        {
            if (!WebConfig.IsDeployedToCloud)
                RootActionRouter.RegisterRoutes(typeof(SignOnRouter), context, Area, Controller);
        }

        // ReSharper disable UnusedMember.Global

        public static class Begin
        {
            public const string Route = "sign-on";
            private static readonly string Action = MVC.Identity.SignOn.ActionNames.Begin;
            public static void MapRoutes(AreaRegistrationContext context, string area, string controller)
            {
                var defaults = new { area, controller, action = Action, };
                var constraints = new { httpMethod = new HttpMethodConstraint("GET", "POST") };
                context.MapRoute(null, Route, defaults, constraints);
            }
        }

        public static class ValidateEmailAddress
        {
            public const string Route = "sign-on/validate/email";
            private static readonly string Action = MVC.Identity.SignOn.ActionNames.ValidateEmailAddress;
            public static void MapRoutes(AreaRegistrationContext routes, string area, string controller)
            {
                var defaults = new { area, controller, action = Action, };
                var constraints = new { httpMethod = new HttpMethodConstraint("POST") };
                routes.MapRoute(null, Route, defaults, constraints);
            }
        }
        public static class Saml2Post
        {
            public const string Route = "sign-on/saml/2/post";
            private static readonly string Action = MVC.Identity.SignOn.ActionNames.Saml2Post;
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
