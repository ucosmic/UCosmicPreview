using UCosmic.Domain.Establishments;

namespace UCosmic.Www.Mvc.Areas.Identity.Services
{
    public class SignOnServices
    {
        public SignOnServices(
            ISignUsers userSigner
            , ISignMembers members
            , IProcessQueries queryProcessor
            , IHandleCommands<SendSamlAuthnRequestCommand> authnRequestHandler
            , IHandleCommands<SignOnSamlUserCommand> authnResponseHandler
        )
        {
            UserSigner = userSigner;
            Members = members;
            QueryProcessor = queryProcessor;
            SendSamlAuthnRequestHandler = authnRequestHandler;
            SignOnSamlUserHandler = authnResponseHandler;
        }

        public ISignUsers UserSigner { get; private set; }
        public ISignMembers Members { get; private set; }
        public IProcessQueries QueryProcessor { get; private set; }
        public IHandleCommands<SendSamlAuthnRequestCommand> SendSamlAuthnRequestHandler { get; private set; }
        public IHandleCommands<SignOnSamlUserCommand> SignOnSamlUserHandler { get; private set; }

    }
}