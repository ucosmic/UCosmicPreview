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

    [EnforceHttps]
    public partial class ServiceProviderMetadataController : Controller
    {
        private readonly ServiceProviderMetadataServices _services;

        public ServiceProviderMetadataController(ServiceProviderMetadataServices services)
        {
            _services = services;
        }

        public virtual PartialViewResult Index(string contentType = null)
        {
            var encryptionCertificate = _services.SamlCertificates.GetEncryptionCertificate();
            var signingCertificate = _services.SamlCertificates.GetSigningCertificate();
            var model = new ServiceProviderEntityDescriptor
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

            return PartialView(MVC.Identity.Shared.Views.metadata, model);
        }

        public virtual PartialViewResult Development(string contentType = null)
        {
            var samlCertificates = new PublicSamlCertificateStorage();
            var encryptionCertificate = samlCertificates.GetEncryptionCertificate();
            var signingCertificate = samlCertificates.GetSigningCertificate();
            var model = new ServiceProviderEntityDescriptor
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

            return PartialView(MVC.Identity.Shared.Views.metadata, model);
        }
    }

    public static class ServiceProviderMetadataRouter
    {
        private static readonly string Area = MVC.Identity.Name;
        private static readonly string Controller = MVC.Identity.ServiceProviderMetadata.Name;

        public static void RegisterRoutes(AreaRegistrationContext context)
        {
            RootActionRouter.RegisterRoutes(typeof(ServiceProviderMetadataRouter), context, Area, Controller);
        }

        // ReSharper disable UnusedMember.Global

        public static class Index
        {
            public const string Route = "sign-on/saml/2/metadata";
            private static readonly string Action = MVC.Identity.ServiceProviderMetadata.ActionNames.Index;
            public static void MapRoutes(AreaRegistrationContext context, string area, string controller)
            {
                var defaults = new { area, controller, action = Action, };
                var constraints = new { httpMethod = new HttpMethodConstraint("GET") };
                context.MapRoute(null, Route, defaults, constraints);
            }
        }

        public static class Development
        {
            public const string Route = "sign-on/saml/2/metadata/develop";
            private static readonly string Action = MVC.Identity.ServiceProviderMetadata.ActionNames.Development;
            public static void MapRoutes(AreaRegistrationContext context, string area, string controller)
            {
                var defaults = new { area, controller, action = Action, };
                var constraints = new { httpMethod = new HttpMethodConstraint("GET") };
                context.MapRoute(null, Route, defaults, constraints);
            }
        }

        // ReSharper restore UnusedMember.Global

    }
}
