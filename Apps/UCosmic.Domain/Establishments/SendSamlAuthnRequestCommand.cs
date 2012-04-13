using System.Web;

namespace UCosmic.Domain.Establishments
{
    public class SendSamlAuthnRequestCommand
    {
        public EstablishmentSamlSignOn SamlSignOn { get; set; }
        public string ReturnUrl { get; set; }
        public HttpContextBase HttpContext { get; set; }
    }
}
