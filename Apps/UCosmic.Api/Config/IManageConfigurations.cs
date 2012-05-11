namespace UCosmic
{
    public interface IManageConfigurations
    {
        bool IsDeployedTo(string deployToTarget);
        bool IsDeployedToCloud { get; }

        string SignUpUrl { get; }
        string ForgotPasswordUrl { get; }
        string ConfirmEmailUrlFormat { get; }

        string TestMailServer { get; }
        string TestMailInbox { get; }

        string EmailDefaultFromAddress { get; }
        string EmailDefaultFromDisplayName { get; }

        string EmailDefaultReplyToAddress { get; }
        string EmailDefaultReplyToDisplayName { get; }

        string EmailEmergencyAddresses { get; }
        string EmailInterceptAddresses { get; }

        string GeoNamesUserName { get; }
        string GeoPlanetAppId { get; }

        string SamlRealServiceProviderEntityId { get; }
        string SamlRealCertificateThumbprint { get; }
        string SamlTestServiceProviderEntityId { get; }
        string SamlTestCertificateThumbprint { get; }
    }
}
