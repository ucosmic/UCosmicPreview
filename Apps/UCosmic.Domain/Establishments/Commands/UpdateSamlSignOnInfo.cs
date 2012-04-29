namespace UCosmic.Domain.Establishments
{
    public class UpdateSamlSignOnInfoCommand
    {
        public Establishment Establishment { get; set; }
        public string EntityId { get; set; }
        public string MetadataUrl { get; set; }
    }

    public class UpdateSamlSignOnInfoHandler : IHandleCommands<UpdateSamlSignOnInfoCommand>
    {
        private readonly IHandleCommands<UpdateSamlSignOnMetadataCommand> _samlMetadataHandler;

        public UpdateSamlSignOnInfoHandler(IHandleCommands<UpdateSamlSignOnMetadataCommand> samlMetadataHandler)
        {
            _samlMetadataHandler = samlMetadataHandler;
        }

        public void Handle(UpdateSamlSignOnInfoCommand command)
        {
            // when no SAML sign on exists, create one
            if (!command.Establishment.HasSamlSignOn())
            {
                command.Establishment.SamlSignOn = new EstablishmentSamlSignOn
                {
                    EntityId = command.EntityId,
                    MetadataUrl = command.MetadataUrl,
                };
            }

            // when EntityId or MetadataUrl have changed, reset for url/xml update
            else if (command.Establishment.SamlSignOn.EntityId != command.EntityId
                && command.Establishment.SamlSignOn.MetadataUrl != command.MetadataUrl)
            {
                command.Establishment.SamlSignOn.EntityId = command.EntityId;
                command.Establishment.SamlSignOn.MetadataUrl = command.MetadataUrl;
                command.Establishment.SamlSignOn.MetadataXml = null;
                command.Establishment.SamlSignOn.SsoLocation = null;
                command.Establishment.SamlSignOn.SsoBinding = null;
            }

            // finally, update the xml using metadata url
            _samlMetadataHandler.Handle(
                new UpdateSamlSignOnMetadataCommand
                {
                    SamlSignOn = command.Establishment.SamlSignOn
                }
            );
        }
    }
}
