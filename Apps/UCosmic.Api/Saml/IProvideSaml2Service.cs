using System.Web;

namespace UCosmic
{
    public interface IProvideSaml2Service
    {
        void SendAuthnRequest(string idpLocation, Saml2SsoBinding idpBinding,
            string fromSpEntityId, string returnUrl, HttpContextBase httpContext);

        Saml2Response ReceiveSamlResponse(Saml2SsoBinding spBinding, HttpContextBase httpContext);
    }
}
