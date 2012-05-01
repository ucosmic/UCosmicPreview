using System;
using System.Web.Mvc;
using System.Web.Routing;
using UCosmic.Impl;
using UCosmic.Www.Mvc.Areas.Identity.Models;
using UCosmic.Www.Mvc.Controllers;

namespace UCosmic.Www.Mvc.Areas.Identity.Controllers
{
    public class Saml2MetadataServices
    {
        public Saml2MetadataServices(IManageConfigurations configurationManager
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

    public static class Saml2MetadataRouter
    {
        private static readonly string Area = MVC.Identity.Name;
        private static readonly string Controller = MVC.Identity.Saml2Metadata.Name;

        public static void RegisterRoutes(AreaRegistrationContext context)
        {
            RootActionRouter.RegisterRoutes(typeof(Saml2MetadataRouter), context, Area, Controller);
        }

        // ReSharper disable UnusedMember.Global

        public static class Index
        {
            public const string Route = "sign-on/saml/2/metadata";
            private static readonly string Action = MVC.Identity.Saml2Metadata.ActionNames.Index;
            public static void MapRoutes(AreaRegistrationContext context, string area, string controller)
            {
                if (WebConfig.IsDeployedToCloud) return;

                var defaults = new { area, controller, action = Action, };
                var constraints = new { httpMethod = new HttpMethodConstraint("GET") };
                context.MapRoute(null, Route, defaults, constraints);
            }
        }

        public static class Development
        {
            public const string Route = "sign-on/saml/2/metadata/develop";
            private static readonly string Action = MVC.Identity.Saml2Metadata.ActionNames.Development;
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
