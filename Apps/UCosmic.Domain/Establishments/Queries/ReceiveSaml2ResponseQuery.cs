using System.Web;

namespace UCosmic.Domain.Establishments
{
    public class ReceiveSaml2ResponseQuery : IDefineQuery<Saml2Response>
    {
        public Saml2SsoBinding SsoBinding { get; set; }
        public HttpContextBase HttpContext { get; set; }
    }
}
