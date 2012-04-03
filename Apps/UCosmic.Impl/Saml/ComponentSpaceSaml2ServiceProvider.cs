using System;
using System.Web;
using System.Xml;
using ComponentSpace.SAML2;
using ComponentSpace.SAML2.Assertions;
using ComponentSpace.SAML2.Bindings;
using ComponentSpace.SAML2.Profiles.SSOBrowser;
using ComponentSpace.SAML2.Protocols;

namespace UCosmic
{
    // ReSharper disable ClassNeverInstantiated.Global
    public class ComponentSpaceSaml2ServiceProvider : IProvideSaml2Service
    // ReSharper restore ClassNeverInstantiated.Global
    {
        private readonly IStoreSamlCertificates _certificates;

        public ComponentSpaceSaml2ServiceProvider(IStoreSamlCertificates certificatesStore)
        {
            _certificates = certificatesStore;
        }

        public void SendAuthnRequest(string idpLocation, Saml2SsoBinding idpBinding,
            string fromSpEntityId, string returnUrl, HttpContextBase httpContext)
        {
            // Create the authentication request.
            var authnRequest = new AuthnRequest
            {
                Destination = idpLocation,
                Issuer = new Issuer(fromSpEntityId),
                ForceAuthn = false,
                NameIDPolicy = new NameIDPolicy(null, null, true),
            };

            // Serialize the authentication request to XML for transmission.
            var authnRequestXml = authnRequest.ToXml();

            // Don't sign if using HTTP redirect as the generated query string is too long for most browsers.
            if (idpBinding != Saml2SsoBinding.HttpRedirect)
            {
                // Sign the authentication request.
                var signingCertificate = _certificates.GetSigningCertificate();
                SAMLMessageSignature.Generate(authnRequestXml, signingCertificate.PrivateKey, signingCertificate);
            }

            // Create and cache the relay state so we remember which SP resource the user wishes to access after SSO.
            SAML.HttpContext = httpContext;
            string relayState = null;
            if (!string.IsNullOrWhiteSpace(returnUrl))
                relayState = RelayStateCache.Add(new RelayState(returnUrl, null));

            // Send the authentication request to the identity provider over the configured binding.
            switch (idpBinding)
            {
                case Saml2SsoBinding.HttpPost:
                    ServiceProvider.SendAuthnRequestByHTTPPost(httpContext.Response, idpLocation, authnRequestXml, relayState);
                    httpContext.Response.End();
                    break;

                case Saml2SsoBinding.HttpRedirect:
                    var encryptionCertificate = _certificates.GetEncryptionCertificate();
                    ServiceProvider.SendAuthnRequestByHTTPRedirect(httpContext.Response, idpLocation, authnRequestXml, relayState,
                        encryptionCertificate.PrivateKey);
                    break;

                default:
                    throw new NotSupportedException(string.Format(
                        "The '{0}' binding is currently not supported.", idpBinding.AsUriString()));
            }
        }

        public Saml2Response ReceiveSamlResponse(Saml2SsoBinding spBinding, HttpContextBase httpContext)
        {
            XmlElement responseElement; string relayState;
            ServiceProvider.ReceiveSAMLResponseByHTTPPost(httpContext.Request, out responseElement, out relayState);
            var info = new ComponentSpaceSaml2Response(responseElement, relayState, spBinding,
                _certificates.GetEncryptionCertificate(), httpContext);
            return info;
        }

    }
}