using UCosmic.Domain.Establishments;
using UCosmic.Domain.Identity;

namespace UCosmic.Www.Mvc.Areas.Identity.Services
{
    public class SignOnServices
    {
        public SignOnServices(IManageConfigurations configurationManager
            , ILogExceptions exceptionLogger
            , EstablishmentFacade establishments
            , UserFacade users
            , ISignUsers userSigner
            , ISignMembers members
            , IProvideSaml2Service saml2ServiceProvider
        )
        {
            Configuration = configurationManager;
            Establishments = establishments;
            Users = users;
            UserSigner = userSigner;
            Members = members;
            Saml2ServiceProvider = saml2ServiceProvider;
        }

        public IManageConfigurations Configuration { get; private set; }
        public EstablishmentFacade Establishments { get; private set; }
        public UserFacade Users { get; private set; }
        public ISignUsers UserSigner { get; private set; }
        public ISignMembers Members { get; private set; }
        public IProvideSaml2Service Saml2ServiceProvider { get; private set; }

    }
}