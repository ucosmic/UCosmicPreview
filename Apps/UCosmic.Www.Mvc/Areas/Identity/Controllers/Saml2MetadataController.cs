using System;
using System.Web.Mvc;
using UCosmic.Www.Mvc.Areas.Identity.Models.Saml2Metadata;

namespace UCosmic.Www.Mvc.Areas.Identity.Controllers
{
    public partial class Saml2MetadataController : Controller
    {
        private readonly IManageConfigurations _configurationManager;
        private readonly IStoreSamlCertificates _samlCertificates;

        public Saml2MetadataController(IManageConfigurations configurationManager
            , IStoreSamlCertificates samlCertificates
        )
        {
            _configurationManager = configurationManager;
            _samlCertificates = samlCertificates;
        }

        public virtual ViewResult Index(string contentType = null)
        {
            var encryptionCertificate = _samlCertificates.GetEncryptionCertificate();
            var signingCertificate = _samlCertificates.GetSigningCertificate();
            var model = new Saml2EntityDescriptorModel
            {
                SigningX509SubjectName = signingCertificate.SubjectName.Name,
                SigningX509Certificate = Convert.ToBase64String(signingCertificate.RawData),
                EncryptionX509SubjectName = encryptionCertificate.SubjectName.Name,
                EncryptionX509Certificate = Convert.ToBase64String(encryptionCertificate.RawData),
                EntityId = _configurationManager.SamlServiceProviderEntityId,
            };

            // NOTE: http://docs.oasis-open.org/security/saml/v2.0/saml-metadata-2.0-os.pdf section 4.1.1 
            Response.ContentType = "application/samlmetadata+xml";
            if ("xml".Equals(contentType, StringComparison.OrdinalIgnoreCase))
                Response.ContentType = "text/xml";

            return View(model);
        }

        //public virtual ViewResult Metadata()
        //{
        //    string lines;
        //    using (var reader = new StreamReader(Server.MapPath("~/bin/App_Data/saml/service-providers/development.xml")))
        //        lines = reader.ReadToEnd();
        //    lines = lines.Replace(@" WantAssertionsSigned=""true""", string.Empty)
        //        .Replace(@" AuthnRequestsSigned=""true""", string.Empty)
        //        .Replace(@" isDefault=""true""", string.Empty);
        //    ViewBag.Xml = lines;

        //    // NOTE: http://docs.oasis-open.org/security/saml/v2.0/saml-metadata-2.0-os.pdf section 4.1.1 
        //    //Response.ContentType = "application/samlmetadata+xml";
        //    return View();
        //}

        //public virtual ActionResult ComponentSpaceSignOn(string emailAddress, string returnUrl)
        //{
        //    // TODO this should be a post from a form that accepts email and return URL.
        //    if (string.IsNullOrWhiteSpace(emailAddress))
        //    {
        //        throw new NotImplementedException("Email address is required.");
        //    }

        //    if (!_establishmentSamlAssistant.IsEmailAddressValidForSamlSignOn(emailAddress))
        //    {
        //        // TODO user should not have made it this far. Redirect back to email address in workflow
        //        throw new NotImplementedException("This user should not sign on with SAML.");
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        var samlSignOn = _establishmentSamlAssistant.GetSamlSignOnFor(emailAddress);
        //        _samlServiceProvider.SendAuthnRequest(samlSignOn.SsoLocation, samlSignOn.SsoBinding.AsSaml2SsoBinding(),
        //            _configurationManager.SamlServiceProviderEntityId, returnUrl, HttpContext);
        //        return new EmptyResult();
        //    }

        //    return View();
        //}

        //[HttpPost]
        //public virtual ActionResult ComponentSpaceSignOn()
        //{
        //    var samlResponse = _samlServiceProvider.ReceiveSamlResponse(Saml2SsoBinding.HttpPost, HttpContext);

        //    // Check that response is from a valid issuer
        //    var isTrustedIssuer = _establishmentSamlAssistant.IsIssuerTrusted(samlResponse.IssuerNameIdentifier);
        //    if (!isTrustedIssuer) throw new InvalidOperationException(string.Format(
        //        "Issuer '{0}' does not appear to be trusted.", samlResponse.IssuerNameIdentifier));

        //    // Verify the response's signature.
        //    if (!samlResponse.VerifySignature())
        //        throw new InvalidOperationException("The SAML response signature failed to verify.");

        //    var subjectNameIdentifier = samlResponse.SubjectNameIdentifier;
        //    var relayResourceUrl = samlResponse.RelayResourceUrl;
        //    var eduPrincipalPersonName = samlResponse.GetAttributeValueByFriendlyName(SamlAttributeFriendlyName.EduPersonPrincipalName);


        //    return View();
        //}

        //public virtual ActionResult SignOnSuccess()
        //{
        //    return View();
        //}

        //public virtual ActionResult SignOff()
        //{
        //    return RedirectToAction(MVC.Identity.Saml2Metadata.SignOffSuccess());
        //}

        //public virtual ActionResult SignOffSuccess()
        //{
        //    return View();
        //}

        //public virtual ActionResult WifSignOn()
        //{
        //    Saml2AuthenticationModule.Current.SignIn("~/sign-on/saml2/success", "https://idp.testshib.org/idp/shibboleth");
        //    return new EmptyResult();
        //}

    }
}
