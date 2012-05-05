//using System.Web;

//namespace UCosmic.Domain.Establishments
//{
//    public class ReceiveSaml2ResponseQuery : IDefineQuery<Saml2Response>
//    {
//        public Saml2SsoBinding SsoBinding { get; set; }
//        public HttpContextBase HttpContext { get; set; }
//    }

//    public class ReceiveSaml2ResponseHandler : IHandleQueries<ReceiveSaml2ResponseQuery, Saml2Response>
//    {
//        private readonly IProvideSaml2Service _saml2ServiceProvider;

//        public ReceiveSaml2ResponseHandler(IProvideSaml2Service saml2ServiceProvider)
//        {
//            _saml2ServiceProvider = saml2ServiceProvider;
//        }

//        public Saml2Response Handle(ReceiveSaml2ResponseQuery query)
//        {
//            return _saml2ServiceProvider.ReceiveSamlResponse(query.SsoBinding, query.HttpContext);
//        }
//    }
//}
