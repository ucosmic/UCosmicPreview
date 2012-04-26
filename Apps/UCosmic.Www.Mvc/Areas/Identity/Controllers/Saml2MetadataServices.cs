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
}