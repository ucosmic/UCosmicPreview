using System;
using System.Web.Mvc;
using UCosmic.Impl;
using UCosmic.Www.Mvc.Areas.Identity.Models;
using UCosmic.Www.Mvc.Controllers;

namespace UCosmic.Www.Mvc.Areas.Identity.Controllers
{
    [EnforceHttps]
    public partial class Saml2MetadataController : Controller
    {
        private readonly Saml2MetadataServices _services;

        public Saml2MetadataController(Saml2MetadataServices services)
        {
            _services = services;
        }

        public virtual ViewResult Index(string contentType = null)
        {
            var encryptionCertificate = _services.SamlCertificates.GetEncryptionCertificate();
            var signingCertificate = _services.SamlCertificates.GetSigningCertificate();
            var model = new Saml2EntityDescriptorModel
            {
                SigningX509SubjectName = signingCertificate.SubjectName.Name,
                SigningX509Certificate = Convert.ToBase64String(signingCertificate.RawData),
                EncryptionX509SubjectName = encryptionCertificate.SubjectName.Name,
                EncryptionX509Certificate = Convert.ToBase64String(encryptionCertificate.RawData),
                EntityId = _services.Configuration.SamlServiceProviderEntityId,
            };

            // NOTE: http://docs.oasis-open.org/security/saml/v2.0/saml-metadata-2.0-os.pdf section 4.1.1 
            Response.ContentType = "application/samlmetadata+xml";
            if ("xml".Equals(contentType, StringComparison.OrdinalIgnoreCase))
                Response.ContentType = "text/xml";

            return View(model);
        }

        public virtual ViewResult Development(string contentType = null)
        {
            var samlCertificates = new PublicSamlCertificateStorage();
            var encryptionCertificate = samlCertificates.GetEncryptionCertificate();
            var signingCertificate = samlCertificates.GetSigningCertificate();
            var model = new Saml2EntityDescriptorModel
            {
                SigningX509SubjectName = signingCertificate.SubjectName.Name,
                SigningX509Certificate = Convert.ToBase64String(signingCertificate.RawData),
                EncryptionX509SubjectName = encryptionCertificate.SubjectName.Name,
                EncryptionX509Certificate = Convert.ToBase64String(encryptionCertificate.RawData),
                EntityId = "https://develop.ucosmic.com/sign-on/saml/2",
            };

            // NOTE: http://docs.oasis-open.org/security/saml/v2.0/saml-metadata-2.0-os.pdf section 4.1.1 
            Response.ContentType = "application/samlmetadata+xml";
            if ("xml".Equals(contentType, StringComparison.OrdinalIgnoreCase))
                Response.ContentType = "text/xml";

            return View(Views.Index, model);
        }
    }
}
