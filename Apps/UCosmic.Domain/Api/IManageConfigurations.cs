namespace UCosmic.Domain
{
    public interface IManageConfigurations
    {
        string DeployedTo { get; }
        bool IsDeployedTo(string deployToTarget);
        bool IsDeployedToCloud { get; }

        string GeoNamesUserName { get; }
        string GeoPlanetAppId { get; }

        string SamlRealServiceProviderEntityId { get; }
        string SamlRealCertificateThumbprint { get; }
        string SamlTestServiceProviderEntityId { get; }
        string SamlTestCertificateThumbprint { get; }

        string ConfirmEmailUrlFormat { get; }

        string EmailDefaultFromAddress { get; }
        string EmailDefaultFromDisplayName { get; }

        string EmailDefaultReplyToAddress { get; }
        string EmailDefaultReplyToDisplayName { get; }

        string EmailEmergencyAddresses { get; }
        string EmailInterceptAddresses { get; }

        string TestMailServer { get; }
        string TestMailInbox { get; }
    }
}
