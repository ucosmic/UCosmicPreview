using System;
using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using UCosmic.Www.Mvc.Areas.Identity.Models.SignOn;
using UCosmic.Www.Mvc.Areas.Identity.Services;
using UCosmic.Www.Mvc.Controllers;

namespace UCosmic.Www.Mvc.Areas.Identity.Controllers
{
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

        [HttpGet]
        [ActionName("sign-on")]
        [OpenTopTab(TopTabName.Home)]
        public virtual ViewResult Begin(string returnUrl)
        {
            var model = new SignOnBeginForm { ReturnUrl = returnUrl };
            return View(model);
        }

        [HttpPost]
        [ActionName("sign-on")]
        [OpenTopTab(TopTabName.Home)]
        public virtual ActionResult Begin(SignOnBeginForm model)
        {
            if (ModelState.IsValid)
            {
                if (model.EmailAddress.EndsWith("@testshib.org", StringComparison.OrdinalIgnoreCase))
                {
                    var samlSignOn = _services.Establishments.GetSamlSignOnFor(model.EmailAddress);
                    _services.Saml2ServiceProvider.SendAuthnRequest(samlSignOn.SsoLocation, samlSignOn.SsoBinding.AsSaml2SsoBinding(),
                        _services.Configuration.SamlServiceProviderEntityId, model.ReturnUrl, HttpContext);
                    return new EmptyResult();
                }

            }
            return View(model);
        }

        [HttpPost]
        [ActionName("post")]
        public virtual ActionResult Saml2Post()
        {
            var samlResponse = _services.Saml2ServiceProvider.ReceiveSamlResponse(Saml2SsoBinding.HttpPost, HttpContext);

            // Check that response is from a valid issuer
            var isTrustedIssuer = _services.Establishments.IsIssuerTrusted(samlResponse.IssuerNameIdentifier);
            if (!isTrustedIssuer) throw new InvalidOperationException(string.Format(
                "Issuer '{0}' does not appear to be trusted.", samlResponse.IssuerNameIdentifier));

            // Verify the response's signature.
            if (!samlResponse.VerifySignature())
                throw new InvalidOperationException("The SAML response signature failed to verify.");

            var subjectNameIdentifier = samlResponse.SubjectNameIdentifier;
            var eduPrincipalPersonName = samlResponse.GetAttributeValueByFriendlyName(SamlAttributeFriendlyName.EduPersonPrincipalName);

            var user = _services.Users.GetOrCreate(eduPrincipalPersonName, true, subjectNameIdentifier);
            _services.UserSigner.SignOn(user.UserName);

            return Redirect(GetReturnUrl(samlResponse.RelayResourceUrl));
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
                "/"                                                     // sign in from root should go to default url
            };

            //// foreach conversion to linq expression
            //foreach (var invalidReturnUrl in invalidReturnUrls)
            //{
            //    if (suggestedReturnUrl.StartsWith(invalidReturnUrl, StringComparison.OrdinalIgnoreCase))
            //        return _identityFacade.UserSigner.DefaultSignedInUrl;
            //}
            return invalidReturnUrls.Any(invalidReturnUrl => 
                suggestedReturnUrl.StartsWith(invalidReturnUrl, StringComparison.OrdinalIgnoreCase)) 
                    ? _services.UserSigner.DefaultSignedOnUrl 
                    : suggestedReturnUrl;
        }

        #endregion
        #region Saml2Integrations

        [HttpGet]
        [ActionName("providers")]
        [OpenTopTab(TopTabName.Home)]
        public virtual ActionResult Saml2Integrations()
        {
            var models = Mapper.Map<Saml2IntegrationInfo[]>
                (_services.Establishments.GetSaml2Integrations());
            return View(models);
        }

        #endregion
    }
}
