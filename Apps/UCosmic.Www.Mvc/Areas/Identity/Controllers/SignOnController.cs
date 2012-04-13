using System;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Mvc;
using AutoMapper;
using FluentValidation.Mvc;
using UCosmic.Domain.Establishments;
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

        [HttpPost]
        [OutputCache(VaryByParam = SignOnBeginForm.EmailAddressPropertyName, Duration = 1800)]
        public virtual JsonResult ValidateEmailAddress(
            [CustomizeValidator(Properties = SignOnBeginForm.EmailAddressPropertyName)] SignOnBeginForm model)
        {
            // form must not be valid unless email address is eligible
            return ValidateRemote(JsonRequestBehavior.DenyGet);
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
                    new FindEstablishmentByEmailQuery
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
            var samlResponse = _services.QueryProcessor.Execute(
                new ReceiveSaml2ResponseQuery
                {
                    SsoBinding = Saml2SsoBinding.HttpPost,
                    HttpContext = HttpContext,
                }
            );

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
        #region Saml2Integrations

        [HttpGet]
        [ActionName("providers")]
        [OpenTopTab(TopTabName.Home)]
        public virtual ActionResult Saml2Integrations()
        {
            var entities = _services.QueryProcessor.Execute(
                new FindSamlIntegratedEstablishmentsQuery()
            );

            var models = Mapper.Map<Saml2IntegrationInfo[]>(entities);
            return View(models);
        }

        #endregion
    }
}
