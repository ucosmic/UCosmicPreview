using System;
using System.IO;
using System.Web.Mvc;
using UCosmic.Domain;
using UCosmic.Domain.Establishments;

namespace UCosmic.Www.Mvc.Areas.Identity.Controllers
{
    public partial class Saml2Controller : Controller
    {
        private readonly IManageConfigurations _configurationManager;
        private readonly IProvideSaml2Service _samlServiceProvider;
        private readonly EstablishmentSamlSignOnLiaison _establishmentSamlAssistant;

        public Saml2Controller(IParseSaml2Metadata samlMetadata, IConsumeHttp http, 
            IManageConfigurations configurationManager, IProvideSaml2Service samlServiceProvider, 
            IQueryEntities entityQueries)
        {
            _configurationManager = configurationManager;
            _samlServiceProvider = samlServiceProvider;
            _establishmentSamlAssistant = new EstablishmentSamlSignOnLiaison(
                samlMetadata, http, entityQueries);
        }

        public virtual ActionResult ComponentSpaceSignOn(string emailAddress, string returnUrl)
        {
            // TODO this should be a post from a form that accepts email and return URL.
            if (string.IsNullOrWhiteSpace(emailAddress))
            {
                throw new NotImplementedException("Email address is required.");
            }

            if (!_establishmentSamlAssistant.IsEmailAddressValid(emailAddress))
            {
                // TODO user should not have made it this far. Redirect back to email address in workflow
                throw new NotImplementedException("This user should not sign on with SAML.");
            }

            if (ModelState.IsValid)
            {
                var samlSignOn = _establishmentSamlAssistant.GetSamlSignOnFor(emailAddress);
                _samlServiceProvider.SendAuthnRequest(samlSignOn.SsoLocation, samlSignOn.SsoBinding.AsSaml2SsoBinding(),
                    _configurationManager.SamlServiceProviderEntityId, returnUrl, HttpContext);
                return new EmptyResult();
            }

            return View();
        }

        [HttpPost]
        public virtual ActionResult ComponentSpaceSignOn()
        {
            var samlResponse = _samlServiceProvider.ReceiveSamlResponse(Saml2SsoBinding.HttpPost, HttpContext);

            // Check that response is from a valid issuer
            var isTrustedIssuer = _establishmentSamlAssistant.IsIssuerTrusted(samlResponse.IssuerNameIdentifier);
            if (!isTrustedIssuer) throw new InvalidOperationException(string.Format(
                "Issuer '{0}' does not appear to be trusted.", samlResponse.IssuerNameIdentifier));

            // Verify the response's signature.
            if (!samlResponse.VerifySignature())
                throw new InvalidOperationException("The SAML response signature failed to verify.");

            var subjectNameIdentifier = samlResponse.SubjectNameIdentifier;
            var relayResourceUrl = samlResponse.RelayResourceUrl;
            var eduPrincipalPersonName = samlResponse.GetAttributeValueByFriendlyName(SamlAttributeFriendlyName.EduPersonPrincipalName);


            return View();
        }

        public virtual ViewResult Metadata()
        {
            string lines;
            using (var reader = new StreamReader(Server.MapPath("~/bin/App_Data/saml/service-providers/development.xml")))
                lines = reader.ReadToEnd();
            lines = lines.Replace(@" WantAssertionsSigned=""true""", string.Empty)
                .Replace(@" AuthnRequestsSigned=""true""", string.Empty)
                .Replace(@" isDefault=""true""", string.Empty);
            ViewBag.Xml = lines;

            // NOTE: http://docs.oasis-open.org/security/saml/v2.0/saml-metadata-2.0-os.pdf section 4.1.1 
            Response.ContentType = "application/samlmetadata+xml";
            return View();
        }

        public virtual ActionResult SignOnSuccess()
        {
            return View();
        }

        public virtual ActionResult SignOff()
        {
            return RedirectToAction(MVC.Identity.Saml2.SignOffSuccess());
        }

        public virtual ActionResult SignOffSuccess()
        {
            return View();
        }

        //public virtual ActionResult WifSignOn()
        //{
        //    Saml2AuthenticationModule.Current.SignIn("~/sign-on/saml2/success", "https://idp.testshib.org/idp/shibboleth");
        //    return new EmptyResult();
        //}

    }
}
