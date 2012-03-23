using System;
using System.Linq;
using System.Xml;
using ComponentSpace.SAML2.Metadata;

namespace UCosmic
{
    // ReSharper disable ClassNeverInstantiated.Global
    public class ComponentSpaceSaml2MetadataParser : IParseSaml2Metadata
    // ReSharper restore ClassNeverInstantiated.Global
    {
        public string GetEntityDescriptor(string fromEntitiesDescriptorXml, string forEntityId)
        {
            var entityDescriptor = ExtractEntityDescriptor(fromEntitiesDescriptorXml, forEntityId);
            return (entityDescriptor != null)
                ? entityDescriptor.ToXml().OuterXml
                : null;
        }

        public string GetIdpSsoServiceLocation(string fromEntityDescriptorXml, params Saml2SsoBinding[] allowedBindings)
        {
            string serviceLocation = null;
            if (allowedBindings != null && allowedBindings.Length > 0)
            {
                //foreach (var allowedBinding in allowedBindings)
                //{
                //    var ssoEndpoint = ExtractIdpSsoServiceEndpointByBinding(fromEntityDescriptorXml, allowedBinding);
                //    if (ssoEndpoint != null) serviceLocation = ssoEndpoint.Location;
                //    break;
                //}
                foreach (var ssoEndpoint in allowedBindings.Select(allowedBinding =>
                    ExtractIdpSsoServiceEndpointByBinding(fromEntityDescriptorXml, allowedBinding)))
                {
                    if (ssoEndpoint != null) serviceLocation = ssoEndpoint.Location;
                    break;
                }
            }
            else
            {
                var ssoEndpoint = ExtractIdpSsoServiceEndpointByBinding(fromEntityDescriptorXml);
                if (ssoEndpoint != null) serviceLocation = ssoEndpoint.Location;
            }
            return serviceLocation;
        }

        public string GetIdpSsoServiceBinding(string fromEntityDescriptorXml, string location)
        {
            var ssoEndpoint = ExtractIdpSsoServiceEndpointByLocation(fromEntityDescriptorXml, location);
            return (ssoEndpoint != null) ? ssoEndpoint.Binding : null;
        }

        private static EntityDescriptor ExtractEntityDescriptor(string fromXml, string forEntityId = null)
        {
            // extract entity descriptor from xml
            if (string.IsNullOrWhiteSpace(fromXml)) throw new ArgumentNullException("fromXml");

            // load xml into a document
            var xmlDocument = new XmlDocument();
            xmlDocument.LoadXml(fromXml);

            // xml may only be a single entity descriptor
            if (EntityDescriptor.IsValid(xmlDocument.DocumentElement))
            {
                // create a new entity descriptor
                var entityDescriptor = new EntityDescriptor(xmlDocument.DocumentElement);

                // entity descriptor is only valid when forEntityId argument has no value
                // or when the forEntityId argument has value and matches the entity descriptor
                if (string.IsNullOrWhiteSpace(forEntityId) // entityID match not enforced
                    || forEntityId.Equals(entityDescriptor.EntityID.URI, // entityID match enforced
                        StringComparison.OrdinalIgnoreCase))
                    return entityDescriptor;

                // forEntityId argument has value, and does not match entity descriptor
                return null;
            }

            // xml may be a n entities descriptor container
            if (EntitiesDescriptor.IsValid(xmlDocument.DocumentElement))
            {
                // create a new entities descriptor
                var entitiesDescriptor = new EntitiesDescriptor(xmlDocument.DocumentElement);

                // when forEntityId is specified, return specific entity descriptor
                // when forEntityId is not specified, return the first entity descriptor
                return (!string.IsNullOrWhiteSpace(forEntityId))
                    ? entitiesDescriptor.GetEntityDescriptor(forEntityId)
                    : entitiesDescriptor.EntityDescriptors.FirstOrDefault();
            }

            return null;
        }

        private static EndpointType ExtractIdpSsoServiceEndpointByBinding(string fromEntityDescriptorXml, Saml2SsoBinding? binding = null)
        {
            // extract entity descriptor from xml
            var entityDescriptor = ExtractEntityDescriptor(fromEntityDescriptorXml);

            // there must be an idp sso descriptor to contain the service endpoint
            var idpSsoDescriptor = entityDescriptor.IDPSSODescriptors.FirstOrDefault();
            if (idpSsoDescriptor == null) return null;

            // when binding is specified return specific binding, otherwise return first
            return (binding.HasValue)
                ? idpSsoDescriptor.SingleSignOnServices.SingleOrDefault(d => d.Binding == binding.Value.AsUriString())
                : idpSsoDescriptor.SingleSignOnServices.FirstOrDefault();
        }

        private static EndpointType ExtractIdpSsoServiceEndpointByLocation(string fromEntityDescriptorXml, string location)
        {
            // extract idp sso service endpoint by location
            if (string.IsNullOrWhiteSpace(location)) return null;

            // extract entity descriptor from xml
            var entityDescriptor = ExtractEntityDescriptor(fromEntityDescriptorXml);

            // there must be an idp sso descriptor to contain the service endpoint
            var idpSsoDescriptor = entityDescriptor.IDPSSODescriptors.FirstOrDefault();

            // only return the 
            return (idpSsoDescriptor != null)
                ? idpSsoDescriptor.SingleSignOnServices.FirstOrDefault(d => d.Location == location)
                : null;
        }
    }
}