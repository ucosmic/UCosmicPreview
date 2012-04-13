namespace UCosmic.Domain.Establishments
{
    public class SendSamlAuthnRequestHandler : IHandleCommands<SendSamlAuthnRequestCommand>
    {
        private readonly IProvideSaml2Service _saml2ServiceProvider;
        private readonly IManageConfigurations _configurationManager;
        private readonly IHandleCommands<UpdateSamlSignOnMetadataCommand> _samlMetadataHandler;

        public SendSamlAuthnRequestHandler(IProvideSaml2Service saml2ServiceProvider
            , IManageConfigurations configurationManager
            , IHandleCommands<UpdateSamlSignOnMetadataCommand> samlMetadataHandler
        )
        {
            _saml2ServiceProvider = saml2ServiceProvider;
            _configurationManager = configurationManager;
            _samlMetadataHandler = samlMetadataHandler;
        }

        public void Handle(SendSamlAuthnRequestCommand command)
        {
            // first make sure the metadata is up to date
            _samlMetadataHandler.Handle(
                new UpdateSamlSignOnMetadataCommand
                {
                    SamlSignOn = command.SamlSignOn,
                }
            );

            // send the authn request
            _saml2ServiceProvider.SendAuthnRequest(
                command.SamlSignOn.SsoLocation,
                command.SamlSignOn.SsoBinding.AsSaml2SsoBinding(),
                _configurationManager.SamlServiceProviderEntityId,
                command.ReturnUrl,
                command.HttpContext
            );
        }
    }
}
