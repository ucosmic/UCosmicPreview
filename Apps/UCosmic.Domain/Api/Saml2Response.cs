using System.Security.Cryptography.X509Certificates;
using System.Web;
using System.Xml;

namespace UCosmic
{
    public abstract class Saml2Response
    {
        protected Saml2Response(XmlElement responseElement, string relayStateId, Saml2SsoBinding spBinding,
            X509Certificate2 encryptionCertificate, HttpContextBase httpContext)
        {
            ResponseElement = responseElement;
            RelayStateId = relayStateId;
            ServiceProviderBinding = spBinding;
            EncryptionCertificate = encryptionCertificate;
            HttpContext = httpContext;
        }

        protected Saml2Response()
        {
        }

        protected XmlElement ResponseElement { get; private set; }
        protected string RelayStateId { get; private set; }
        protected HttpContextBase HttpContext { get; private set; }
        public Saml2SsoBinding ServiceProviderBinding { get; private set; }
        protected X509Certificate2 EncryptionCertificate { get; private set; }
        public abstract bool IsSigned { get; }
        public abstract bool VerifySignature();
        public abstract string SubjectNameIdentifier { get; }
        public abstract string IssuerNameIdentifier { get; }
        public abstract string RelayResourceUrl { get; }
        protected abstract string GetAttributeValueByFriendlyName(SamlAttributeFriendlyName friendlyName);
        protected abstract string[] GetAttributeValuesByFriendlyName(SamlAttributeFriendlyName friendlyName);
        protected abstract string GetAttributeValueByName(SamlAttributeName name);
        protected abstract string[] GetAttributeValuesByName(SamlAttributeName name);
        public abstract string EduPersonTargetedId { get; }
        public abstract string EduPersonPrincipalName { get; }
        public abstract string[] EduPersonScopedAffiliations { get; }
        public abstract string CommonName { get; }
        public abstract string DisplayName { get; }
        public abstract string GivenName { get; }
        public abstract string SurName { get; }
        public abstract string[] Mails { get; }
    }
}
