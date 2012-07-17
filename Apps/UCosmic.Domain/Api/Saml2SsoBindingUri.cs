namespace UCosmic
{
    public static class Saml2SsoBindingUri
    {
        private const string HttpPost = "urn:oasis:names:tc:SAML:2.0:bindings:HTTP-POST";
        private const string HttpRedirect = "urn:oasis:names:tc:SAML:2.0:bindings:HTTP-Redirect";

        public static Saml2SsoBinding AsSaml2SsoBinding(this string bindingUri)
        {
            switch (bindingUri)
            {
                case HttpPost:
                    return Saml2SsoBinding.HttpPost;
                case HttpRedirect:
                    return Saml2SsoBinding.HttpRedirect;
            }
            return Saml2SsoBinding.NotSpecified;
        }

        public static string AsUriString(this Saml2SsoBinding binding)
        {
            switch (binding)
            {
                case Saml2SsoBinding.HttpPost:
                    return HttpPost;
                case Saml2SsoBinding.HttpRedirect:
                    return HttpRedirect;
                default:
                    return null;
            }
        }
    }
}
