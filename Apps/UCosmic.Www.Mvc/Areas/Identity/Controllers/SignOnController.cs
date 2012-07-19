using System;
using System.Linq.Expressions;
using System.Web.Mvc;
using System.Web.Routing;
using FluentValidation.Mvc;
using UCosmic.Domain;
using UCosmic.Domain.Establishments;
using UCosmic.Www.Mvc.Areas.Identity.Models;
using UCosmic.Www.Mvc.Controllers;

namespace UCosmic.Www.Mvc.Areas.Identity.Controllers
{
    public class SignOnServices
    {
        public SignOnServices(IProcessQueries queryProcessor
            , IProvideSaml2Service samlServiceProvider
            , IManageConfigurations configurationManager
            , IHandleCommands<UpdateSamlSignOnMetadataCommand> commandHandler
        )
        {
            QueryProcessor = queryProcessor;
            SamlServiceProvider = samlServiceProvider;
            ConfigurationManager = configurationManager;
            CommandHandler = commandHandler;
        }

        public IProcessQueries QueryProcessor { get; private set; }
        public IProvideSaml2Service SamlServiceProvider { get; private set; }
        public IManageConfigurations ConfigurationManager { get; private set; }
        public IHandleCommands<UpdateSamlSignOnMetadataCommand> CommandHandler { get; private set; }
    }

    public partial class SignOnController : BaseController
    {
        private readonly SignOnServices _services;

        public SignOnController(SignOnServices services)
        {
            _services = services;
        }

        [HttpGet]
        [ActionName("sign-on")]
        [OpenTopTab(TopTabName.Home)]
        [ValidateSigningEmail]
        [ValidateSigningReturnUrl]
        public virtual ActionResult Get(string returnUrl)
        {
            var model = new SignOnForm(HttpContext, returnUrl);
            return View(model);
        }

        [HttpPost]
        [OutputCache(VaryByParam = SignOnForm.EmailAddressPropertyName, Duration = 1800)]
        public virtual JsonResult ValidateEmailAddress(
            [CustomizeValidator(Properties = SignOnForm.EmailAddressPropertyName)] SignOnForm model)
        {
            // form is valid unless email address is eligible
            return ValidateRemote(SignOnForm.EmailAddressPropertyName);
        }

        [HttpPost]
        [UnitOfWork]
        [ActionName("sign-on")]
        [OpenTopTab(TopTabName.Home)]
        [ValidateAntiForgeryToken]
        [ValidateSigningEmail(ParamName = "model")]
        public virtual ActionResult Post(SignOnForm model)
        {
            if (model == null) return HttpNotFound();

            if (!ModelState.IsValid) return View(model);

            // get the establishment for this email address
            var establishment = _services.QueryProcessor.Execute(
                new GetEstablishmentByEmailQuery(model.EmailAddress)
                {
                    EagerLoad = new Expression<Func<Establishment, object>>[]
                    {
                        e => e.SamlSignOn,
                    }
                }
            );

            // update the provider metadata
            _services.CommandHandler.Handle(
                new UpdateSamlSignOnMetadataCommand
                {
                    SamlSignOn = establishment.SamlSignOn,
                }
            );

            // clear the email from temp data
            TempData.SigningEmailAddress(null);

            // send the authn request
            _services.SamlServiceProvider.SendAuthnRequest(
                establishment.SamlSignOn.SsoLocation,
                establishment.SamlSignOn.SsoBinding.AsSaml2SsoBinding(),
                _services.ConfigurationManager.SamlRealServiceProviderEntityId,
                model.ReturnUrl ?? Url.Action(MVC.Identity.MyHome.Get()),
                HttpContext
            );

            // wait for the authn response
            return new EmptyResult();
        }

        public const string SuccessMessageFormat = "You are now signed on to UCosmic as {0}.";
    }

    public static class SignOnRouter
    {
        private static readonly string Area = MVC.Identity.Name;
        private static readonly string Controller = MVC.Identity.SignOn.Name;

        public static void RegisterRoutes(AreaRegistrationContext context)
        {
            RootActionRouter.RegisterRoutes(typeof(SignOnRouter), context, Area, Controller);
        }

        // ReSharper disable UnusedMember.Global

        public static class Get
        {
            public const string Route = "sign-on";
            private static readonly string Action = MVC.Identity.SignOn.ActionNames.Get;
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
            private static readonly string Action = MVC.Identity.SignOn.ActionNames.Post;
            public static void MapRoutes(AreaRegistrationContext context, string area, string controller)
            {
                var defaults = new { area, controller, action = Action, };
                var constraints = new { httpMethod = new HttpMethodConstraint("POST") };
                context.MapRoute(null, Route, defaults, constraints);
            }
        }

        public static class ValidateEmailAddress
        {
            public const string Route = "sign-on/validate";
            private static readonly string Action = MVC.Identity.SignOn.ActionNames.ValidateEmailAddress;
            public static void MapRoutes(AreaRegistrationContext routes, string area, string controller)
            {
                var defaults = new { area, controller, action = Action, };
                var constraints = new { httpMethod = new HttpMethodConstraint("POST") };
                routes.MapRoute(null, Route, defaults, constraints);
            }
        }

        // ReSharper restore UnusedMember.Global
    }
}
