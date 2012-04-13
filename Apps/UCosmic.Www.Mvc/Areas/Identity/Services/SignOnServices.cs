using UCosmic.Domain.Establishments;
using UCosmic.Domain.Identity;
using UCosmic.Domain;

namespace UCosmic.Www.Mvc.Areas.Identity.Services
{
    public class SignOnServices
    {
        public SignOnServices(IManageConfigurations configurationManager
            , EstablishmentFacade establishments
            , UserFacade users
            , ISignUsers userSigner
            , ISignMembers members
            , IProvideSaml2Service saml2ServiceProvider
            , IProcessQueries queryProcessor
            , IHandleCommands<SendSamlAuthnRequestCommand> authnRequestHandler
        )
        {
            Configuration = configurationManager;
            Establishments = establishments;
            Users = users;
            UserSigner = userSigner;
            Members = members;
            Saml2ServiceProvider = saml2ServiceProvider;
            QueryProcessor = queryProcessor;
            SendSamlAuthnRequestHandler = authnRequestHandler;
        }

        public IManageConfigurations Configuration { get; private set; }
        public EstablishmentFacade Establishments { get; private set; }
        public UserFacade Users { get; private set; }
        public ISignUsers UserSigner { get; private set; }
        public ISignMembers Members { get; private set; }
        public IProvideSaml2Service Saml2ServiceProvider { get; private set; }
        public IProcessQueries QueryProcessor { get; private set; }
        public IHandleCommands<SendSamlAuthnRequestCommand> SendSamlAuthnRequestHandler { get; private set; }

    }
}