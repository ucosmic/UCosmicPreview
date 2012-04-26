using System.Configuration;

namespace UCosmic.Impl
{
    public class DotNetConfigurationManager : IManageConfigurations
    {
        public bool IsDeployedTo(string deployToTarget) { return GetString(AppSettingsKey.DeployedTo) == deployToTarget; }
        public bool IsDeployedToCloud { get { return IsDeployedTo(DeployToTarget.Preview) || IsDeployedTo(DeployToTarget.Www); } }

        public string SignUpUrl { get { return GetString(AppSettingsKey.SignUpUrl); } }
        public string SignUpEmailConfirmationUrlFormat { get { return GetString(AppSettingsKey.SignUpEmailConfirmationUrlFormat); } }

        public string PasswordResetUrl { get { return GetString(AppSettingsKey.PasswordResetUrl); } }
        public string PasswordResetConfirmationUrlFormat { get { return GetString(AppSettingsKey.PasswordResetConfirmationUrlFormat); } }

        public string EmailConfirmationUrlFormat { get { return GetString(AppSettingsKey.EmailConfirmationUrlFormat); } }

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

        public string SamlServiceProviderEntityId { get { return GetString(AppSettingsKey.SamlServiceProviderEntityId); } }
        public string SamlCertificateThumbprint { get { return GetString(AppSettingsKey.SamlCertificateThumbprint); } }

        private static string GetString(AppSettingsKey key) { return ConfigurationManager.AppSettings[key.ToString()]; }

    }
}