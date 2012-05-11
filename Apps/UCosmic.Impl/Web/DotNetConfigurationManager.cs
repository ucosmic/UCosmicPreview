using System.Configuration;

namespace UCosmic.Impl
{
    public class DotNetConfigurationManager : IManageConfigurations
    {
        public bool IsDeployedTo(string deployToTarget) { return GetString(AppSettingsKey.DeployedTo) == deployToTarget; }
        public bool IsDeployedToCloud { get { return IsDeployedTo(DeployToTarget.Preview) || IsDeployedTo(DeployToTarget.Www); } }

        public string SignUpUrl { get { return GetString(AppSettingsKey.SignUpUrl); } }
        public string ForgotPasswordUrl { get { return GetString(AppSettingsKey.ForgotPasswordUrl); } }

        public string ConfirmEmailUrlFormat { get { return GetString(AppSettingsKey.ConfirmEmailUrlFormat); } }

        public string TestMailServer { get { return GetString(AppSettingsKey.TestMailServer); } }
        public string TestMailInbox { get { return GetString(AppSettingsKey.TestMailInbox); } }

        public string EmailDefaultFromAddress { get { return GetString(AppSettingsKey.EmailDefaultFromAddress); } }
        public string EmailDefaultFromDisplayName { get { return GetString(AppSettingsKey.EmailDefaultFromDisplayName); } }

        public string EmailDefaultReplyToAddress { get { return GetString(AppSettingsKey.EmailDefaultReplyToAddress); } }
        public string EmailDefaultReplyToDisplayName { get { return GetString(AppSettingsKey.EmailDefaultReplyToDisplayName); } }

        public string EmailEmergencyAddresses { get { return GetString(AppSettingsKey.EmailEmergencyAddresses); } }
        public string EmailInterceptAddresses { get { return GetString(AppSettingsKey.EmailInterceptAddresses); } }

        public string GeoNamesUserName { get { return GetString(AppSettingsKey.GeoNamesUserName); } }
        public string GeoPlanetAppId { get { return GetString(AppSettingsKey.GeoPlanetAppId); } }

        public string SamlRealServiceProviderEntityId { get { return GetString(AppSettingsKey.SamlRealServiceProviderEntityId); } }
        public string SamlRealCertificateThumbprint { get { return GetString(AppSettingsKey.SamlRealCertificateThumbprint); } }
        public string SamlTestServiceProviderEntityId { get { return GetString(AppSettingsKey.SamlTestServiceProviderEntityId); } }
        public string SamlTestCertificateThumbprint { get { return GetString(AppSettingsKey.SamlTestCertificateThumbprint); } }

        private static string GetString(AppSettingsKey key) { return ConfigurationManager.AppSettings[key.ToString()]; }

    }
}