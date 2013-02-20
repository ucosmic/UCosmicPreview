using System;
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
            // detect SAML SSO from skin cookie
            var skin = HttpContext.SkinCookie();
            if (!string.IsNullOrWhiteSpace(skin))
            {
                // get the establishment for this skin
                var establishment = _services.QueryProcessor.Execute(
                    new GetEstablishmentByUrlQuery(skin)
                    {
                        EagerLoad = new Expression<Func<Establishment, object>>[]
                        {
                            e => e.SamlSignOn,
                        }
                    }
                );
                if (establishment != null && establishment.HasSamlSignOn())
                {
                    PushToSamlSso(establishment, returnUrl);

                    // wait for the authn response
                    return new EmptyResult();
                }
            }

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

            PushToSamlSso(establishment, model.ReturnUrl);

            // wait for the authn response
            return new EmptyResult();
        }

        public const string SuccessMessageFormat = "You are now signed on to UCosmic as {0}.";

        [NonAction]
        private void PushToSamlSso(Establishment establishment, string returnUrl)
        {
            if (establishment == null) return;

            // update the provider metadata
            _services.CommandHandler.Handle(
                new UpdateSamlSignOnMetadataCommand
                {
                    EstablishmentId = establishment.RevisionId,
                }
            );

            // clear the email from temp data
            TempData.SigningEmailAddress(null);

            // send the authn request
            _services.SamlServiceProvider.SendAuthnRequest(
                establishment.SamlSignOn.SsoLocation,
                establishment.SamlSignOn.SsoBinding.AsSaml2SsoBinding(),
                _services.ConfigurationManager.SamlRealServiceProviderEntityId,
                returnUrl ?? Url.Action(MVC.Identity.MyHome.Get()),
                HttpContext
            );
        }
    }

    public static class SignOnRouter
    {
        private static readonly string Area = MVC.Identity.Name;
        private static readonly string Controller = MVC.Identity.SignOn.Name;

        public class GetRoute : MvcRoute
        {
            public GetRoute()
            {
                Url = "sign-on";
                DataTokens = new RouteValueDictionary(new { area = Area });
                Defaults = new RouteValueDictionary(new
                {
                    controller = Controller,
                    action = MVC.Identity.SignOn.ActionNames.Get,
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
                    action = MVC.Identity.SignOn.ActionNames.Post,
                });
                Constraints = new RouteValueDictionary(new
                {
                    httpMethod = new HttpMethodConstraint("POST"),
                });
            }
        }

        public class ValidateEmailAddressRoute : MvcRoute
        {
            public ValidateEmailAddressRoute()
            {
                Url = "sign-on/validate";
                DataTokens = new RouteValueDictionary(new { area = Area });
                Defaults = new RouteValueDictionary(new
                {
                    controller = Controller,
                    action = MVC.Identity.SignOn.ActionNames.ValidateEmailAddress,
                });
                Constraints = new RouteValueDictionary(new
                {
                    httpMethod = new HttpMethodConstraint("POST"),
                });
            }
        }
    }
}
