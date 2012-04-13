using System;

namespace UCosmic.Domain.Establishments
{
    public class UpdateSamlSignOnMetadataHandler : IHandleCommands<UpdateSamlSignOnMetadataCommand>
    {
        private readonly IConsumeHttp _httpConsumer;
        private readonly IParseSaml2Metadata _saml2MetadataParser;

        public UpdateSamlSignOnMetadataHandler(IConsumeHttp httpConsumer, IParseSaml2Metadata saml2MetadataParser)
        {
            _httpConsumer = httpConsumer;
            _saml2MetadataParser = saml2MetadataParser;
        }

        public void Handle(UpdateSamlSignOnMetadataCommand command)
        {
            if (
                !string.IsNullOrWhiteSpace(command.SamlSignOn.MetadataXml) &&       // if metadata has already been loaded, and
                command.SamlSignOn.UpdatedOnUtc.HasValue &&                         // metadata has already been parsed and cached, and
                command.SamlSignOn.UpdatedOnUtc.Value.AddDays(7) > DateTime.UtcNow  // metadata was checked less than a week ago, then
            ) return;                                                               // nothing has changed

            // load published metadata over http
            var entitiesDescriptorXml = _httpConsumer.Get(command.SamlSignOn.MetadataUrl);

            // metadata may be entities descriptor, need specific entity descriptor
            command.SamlSignOn.MetadataXml = _saml2MetadataParser.GetEntityDescriptor(
                entitiesDescriptorXml, command.SamlSignOn.EntityId);

            // currently only support http-post and http-redirect bindings
            command.SamlSignOn.SsoLocation = _saml2MetadataParser.GetIdpSsoServiceLocation(
                command.SamlSignOn.MetadataXml, Saml2SsoBinding.HttpPost, Saml2SsoBinding.HttpRedirect);

            // update binding for the negotiated endpoint
            command.SamlSignOn.SsoBinding = _saml2MetadataParser.GetIdpSsoServiceBinding(
                command.SamlSignOn.MetadataXml, command.SamlSignOn.SsoLocation);

            // metadata is now cached on the entity, check again after 7 days
            command.SamlSignOn.UpdatedOnUtc = DateTime.UtcNow;
        }
    }
}
