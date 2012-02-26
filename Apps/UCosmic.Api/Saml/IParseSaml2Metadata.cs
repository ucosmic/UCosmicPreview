namespace UCosmic
{
    public interface IParseSaml2Metadata
    {
        string GetEntityDescriptor(string fromEntitiesDescriptorXml, string forEntityId);
        string GetIdpSsoServiceLocation(string fromEntityDescriptorXml, params Saml2SsoBinding[] allowedBindings);
        string GetIdpSsoServiceBinding(string fromEntityDescriptorXml, string location);
    }
}
