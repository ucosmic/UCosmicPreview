using System;
using System.Linq;

namespace UCosmic.Domain.Establishments
{
    public class UpdateSamlSignOnMetadataCommand
    {
        public int EstablishmentId { get; set; }
    }

    public class UpdateSamlSignOnMetadataHandler : IHandleCommands<UpdateSamlSignOnMetadataCommand>
    {
        private readonly ICommandEntities _entities;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IConsumeHttp _httpConsumer;
        private readonly IParseSaml2Metadata _saml2MetadataParser;

        public UpdateSamlSignOnMetadataHandler(ICommandEntities entities
            , IUnitOfWork unitOfWork
            , IConsumeHttp httpConsumer
            , IParseSaml2Metadata saml2MetadataParser)
        {
            _entities = entities;
            _unitOfWork = unitOfWork;
            _httpConsumer = httpConsumer;
            _saml2MetadataParser = saml2MetadataParser;
        }

        public void Handle(UpdateSamlSignOnMetadataCommand command)
        {
            var samlSignOn = _entities.Get<EstablishmentSamlSignOn>()
                .SingleOrDefault(x => x.Id == command.EstablishmentId);
            if (samlSignOn == null) throw new InvalidOperationException(string.Format(
                "Unable to locate SAML sign on information for establishment '{0}'.", command.EstablishmentId));

            if (
                !string.IsNullOrWhiteSpace(samlSignOn.MetadataXml) &&       // if metadata has already been loaded, and
                samlSignOn.UpdatedOnUtc.HasValue &&                         // metadata has already been parsed and cached, and
                samlSignOn.UpdatedOnUtc.Value.AddDays(7) > DateTime.UtcNow  // metadata was checked less than a week ago, then
            ) return;                                                       // nothing has changed

            // load published metadata over http
            var entitiesDescriptorXml = _httpConsumer.Get(samlSignOn.MetadataUrl);

            // metadata may be entities descriptor, need specific entity descriptor
            samlSignOn.MetadataXml = _saml2MetadataParser.GetEntityDescriptor(
                entitiesDescriptorXml, samlSignOn.EntityId);

            // currently only support http-post and http-redirect bindings
            samlSignOn.SsoLocation = _saml2MetadataParser.GetIdpSsoServiceLocation(
                samlSignOn.MetadataXml, Saml2SsoBinding.HttpPost, Saml2SsoBinding.HttpRedirect);

            // update binding for the negotiated endpoint
            samlSignOn.SsoBinding = _saml2MetadataParser.GetIdpSsoServiceBinding(
                samlSignOn.MetadataXml, samlSignOn.SsoLocation);

            // metadata is now cached on the entity, check again after 7 days
            samlSignOn.UpdatedOnUtc = DateTime.UtcNow;

            _unitOfWork.SaveChanges();
        }
    }
}
