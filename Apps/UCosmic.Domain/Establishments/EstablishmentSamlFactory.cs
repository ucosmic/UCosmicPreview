using System;

namespace UCosmic.Domain.Establishments
{
    internal static class EstablishmentSamlFactory
    {
        internal static EstablishmentSamlSignOn Create(string samlEntityId, string metadataUrl)
        {
            var samlSignOn = new EstablishmentSamlSignOn
            {
                EntityId = samlEntityId,
                MetadataUrl = metadataUrl,
            };

            EnsureMetadataIsCached(samlSignOn);

            return samlSignOn;
        }

        internal static void EnsureMetadataIsCached(EstablishmentSamlSignOn samlSignOn)
        {
            if (!string.IsNullOrWhiteSpace(samlSignOn.MetadataXml) // metadata has already been loaded
                && samlSignOn.UpdatedOnUtc.HasValue // metadata has already been parsed & cached
                && samlSignOn.UpdatedOnUtc.Value.AddDays(7) > DateTime.UtcNow) // metadata was checked less than a week ago
                return; // nothing has changed

            // resolve dependencies: okay to use service locator here, dependencies are not always required.
            var httpClient = DependencyInjector.Current.GetService<IConsumeHttp>();
            var samlMetadataParser = DependencyInjector.Current.GetService<IParseSaml2Metadata>();

            // load published metadata over http
            var entitiesDescriptorXml = httpClient.Get(samlSignOn.MetadataUrl);

            // metadata may be entities descriptor, need specific entity descriptor
            samlSignOn.MetadataXml = samlMetadataParser.GetEntityDescriptor(
                entitiesDescriptorXml, samlSignOn.EntityId);

            // currently only support http-post and http-redirect bindings
            samlSignOn.SsoLocation = samlMetadataParser.GetIdpSsoServiceLocation(
                samlSignOn.MetadataXml, Saml2SsoBinding.HttpPost, Saml2SsoBinding.HttpRedirect);

            // update binding for the negotiated endpoint
            samlSignOn.SsoBinding = samlMetadataParser.GetIdpSsoServiceBinding(
                samlSignOn.MetadataXml, samlSignOn.SsoLocation);

            // metadata is cached on the entity
            samlSignOn.UpdatedOnUtc = DateTime.UtcNow;
        }
    }
}
