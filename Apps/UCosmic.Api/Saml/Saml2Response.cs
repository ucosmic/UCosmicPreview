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
        public abstract string GetAttributeValueByFriendlyName(SamlAttributeFriendlyName friendlyName);
        public abstract string[] GetAttributeValuesByFriendlyName(SamlAttributeFriendlyName friendlyName);
    }
}
