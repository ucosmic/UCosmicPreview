using System;
using System.Web.Mvc;
using System.Web.Routing;
using UCosmic.Impl;
using UCosmic.Www.Mvc.Areas.Identity.Models;
using UCosmic.Www.Mvc.Controllers;

namespace UCosmic.Www.Mvc.Areas.Identity.Controllers
{
    public class ServiceProviderMetadataServices
    {
        public ServiceProviderMetadataServices(IManageConfigurations configurationManager
            , IStoreSamlCertificates samlCertificates
        )
        {
            Configuration = configurationManager;
            SamlCertificates = samlCertificates;
        }

        public IManageConfigurations Configuration { get; private set; }
        public IStoreSamlCertificates SamlCertificates { get; private set; }
    }

    public partial class ServiceProviderMetadataController : Controller
    {
        private readonly ServiceProviderMetadataServices _services;

        public ServiceProviderMetadataController(ServiceProviderMetadataServices services)
        {
            _services = services;
        }

        public virtual PartialViewResult Real(string contentType = null)
        {
            return Get(contentType);
        }

        public virtual PartialViewResult Test(string contentType = null)
        {
            return Get(contentType, false);
        }

        [NonAction]
        private PartialViewResult Get(string contentType, bool isReal = true)
        {
            var samlCertificates = isReal
                ? _services.SamlCertificates // use real cert by default
                : new TestSamlCertificateStorage(_services.Configuration);
            var encryptionCertificate = samlCertificates.GetEncryptionCertificate();
            var signingCertificate = samlCertificates.GetSigningCertificate();
            var model = new ServiceProviderEntityDescriptor
            {
                SigningX509SubjectName = signingCertificate.SubjectName.Name,
                SigningX509Certificate = Convert.ToBase64String(signingCertificate.RawData),
                EncryptionX509SubjectName = encryptionCertificate.SubjectName.Name,
                EncryptionX509Certificate = Convert.ToBase64String(encryptionCertificate.RawData),
                EntityId = isReal
                    ? _services.Configuration.SamlRealServiceProviderEntityId
                    : _services.Configuration.SamlTestServiceProviderEntityId,
            };

            // NOTE: http://docs.oasis-open.org/security/saml/v2.0/saml-metadata-2.0-os.pdf section 4.1.1
            Response.ContentType = "application/samlmetadata+xml";
            if ("xml".Equals(contentType, StringComparison.OrdinalIgnoreCase))
                Response.ContentType = "text/xml";

            return PartialView(MVC.Identity.Shared.Views.metadata, model);
        }
    }

    public static class ServiceProviderMetadataRouter
    {
        private static readonly string Area = MVC.Identity.Name;
        private static readonly string Controller = MVC.Identity.ServiceProviderMetadata.Name;

        public class RealRoute : MvcRoute
        {
            public RealRoute()
            {
                Url = "sign-on/saml/2/metadata";
                DataTokens = new RouteValueDictionary(new { area = Area });
                Defaults = new RouteValueDictionary(new
                {
                    controller = Controller,
                    action = MVC.Identity.ServiceProviderMetadata.ActionNames.Real,
                });
                Constraints = new RouteValueDictionary(new
                {
                    httpMethod = new HttpMethodConstraint("GET"),
                });
            }
        }

        public class TestRoute : MvcRoute
        {
            public TestRoute()
            {
                Url = "sign-on/saml/2/metadata/develop";
                DataTokens = new RouteValueDictionary(new { area = Area });
                Defaults = new RouteValueDictionary(new
                {
                    controller = Controller,
                    action = MVC.Identity.ServiceProviderMetadata.ActionNames.Test,
                });
                Constraints = new RouteValueDictionary(new
                {
                    httpMethod = new HttpMethodConstraint("GET"),
                });
            }
        }
    }
}
