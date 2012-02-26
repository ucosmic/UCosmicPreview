using System;
using System.Collections.Generic;

namespace UCosmic.Domain.Establishments
{
    public class EstablishmentSamlSignOnLiaison
    {
        private readonly Dictionary<string, Establishment> _establishmentsByEmailAddress 
            = new Dictionary<string, Establishment>();
        private readonly Dictionary<string, Establishment> _establishmentsBySamlEntityId
            = new Dictionary<string, Establishment>();

        private readonly EstablishmentFinder _establishmentFinder;
        private readonly IParseSaml2Metadata _samlMetadataParser;
        private readonly IConsumeHttp _httpClient;

        public EstablishmentSamlSignOnLiaison(IParseSaml2Metadata saml2MetadataParserParser, 
            IConsumeHttp httpClient, IQueryEntities entityQueries)
        {
            _samlMetadataParser = saml2MetadataParserParser;
            _httpClient = httpClient;
            _establishmentFinder = new EstablishmentFinder(entityQueries);
        }

        public bool IsEmailAddressValid(string emailAddress)
        {
            var establishment = GetEstablishmentForEmailAddress(emailAddress);
            return (establishment != null && establishment.SamlSignOn != null);
        }

        public EstablishmentSamlSignOn GetSamlSignOnFor(string emailAddress)
        {
            EnsureMetadataIsStored(emailAddress);
            var establishment = GetEstablishmentForEmailAddress(emailAddress);
            return establishment.SamlSignOn;
        }

        public bool IsIssuerTrusted(string issuerNameIdentifier)
        {
            var establishment = GetEstablishmentForSamlEntityId(issuerNameIdentifier);
            return (establishment != null && establishment.SamlSignOn != null);
        }

        private Establishment GetEstablishmentForEmailAddress(string emailAddress)
        {
            if (!_establishmentsByEmailAddress.ContainsKey(emailAddress))
            {
                var establishment = _establishmentFinder
                    .FindOne(EstablishmentBy.EmailDomain(emailAddress)
                        .EagerLoad(e => e.SamlSignOn)
                        .ForInsertOrUpdate()
                    );
                _establishmentsByEmailAddress.Add(emailAddress, establishment);
            }

            return _establishmentsByEmailAddress[emailAddress];
        }

        private Establishment GetEstablishmentForSamlEntityId(string samlEntityId)
        {
            if (!_establishmentsBySamlEntityId.ContainsKey(samlEntityId))
            {
                var establishment = _establishmentFinder
                    .FindOne(EstablishmentBy.SamlEntityId(samlEntityId)
                        .EagerLoad(e => e.SamlSignOn)
                );
                _establishmentsBySamlEntityId.Add(samlEntityId, establishment);
            }

            return _establishmentsBySamlEntityId[samlEntityId];
        }

        private void ValidateEmailAddress(string emailAddress)
        {
            if (!IsEmailAddressValid(emailAddress))
                throw new InvalidOperationException(string.Format(
                    "Email address '{0}' is not valid for SAML 2 SSO.", emailAddress));
        }

        private void EnsureMetadataIsStored(string emailAddress)
        {
            ValidateEmailAddress(emailAddress);
            var establishment = GetEstablishmentForEmailAddress(emailAddress);
            var samlSignOn = establishment.SamlSignOn;

            if (!string.IsNullOrWhiteSpace(samlSignOn.MetadataXml) // metadata has already been loaded
                && samlSignOn.UpdatedOn.HasValue // metadata has already been parsed & cached
                && samlSignOn.UpdatedOn.Value.AddDays(7) > DateTime.UtcNow) // metadata was checked less than a week ago
                return; // nothing has changed

            // load published metadata over http
            var entitiesDescriptorXml = _httpClient.Get(samlSignOn.MetadataUrl);

            // metadata may be entities descriptor, need specific entity descriptor
            samlSignOn.MetadataXml = _samlMetadataParser.GetEntityDescriptor(
                entitiesDescriptorXml, samlSignOn.EntityId);

            //// update signing certificate
            //samlSignOn.SigningCertificate = _samlMetadataParser.GetIdpSsoCertificate(
            //    samlSignOn.MetadataXml, Saml2KeyDescriptorUse.Signing);

            //// update encryption certificate
            //samlSignOn.EncryptionCertificate = _samlMetadataParser.GetIdpSsoCertificate(
            //    samlSignOn.MetadataXml, Saml2KeyDescriptorUse.Encryption);

            // currently only support http-post and http-redirect bindings
            samlSignOn.SsoLocation = _samlMetadataParser.GetIdpSsoServiceLocation(
                samlSignOn.MetadataXml, Saml2SsoBinding.HttpPost, Saml2SsoBinding.HttpRedirect);

            // update binding for the negotiated endpoint
            samlSignOn.SsoBinding = _samlMetadataParser.GetIdpSsoServiceBinding(
                samlSignOn.MetadataXml, samlSignOn.SsoLocation);

            // metadata is cached on the entity
            samlSignOn.UpdatedOn = DateTime.UtcNow;
        }

    }
}