using System.Web;

namespace UCosmic.Domain.Establishments
{
    public class SignOnSamlUserCommand
    {
        public Saml2SsoBinding SsoBinding { get; set; }
        public HttpContextBase HttpContext { get; set; }
        public string ReturnUrl { get; set; }
    }
}
