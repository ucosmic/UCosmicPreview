using System;
using System.Security.Cryptography.X509Certificates;

namespace UCosmic.Impl
{
    // ReSharper disable UnusedMember.Global
    public class PrivateSamlCertificateStorage : IStoreSamlCertificates
    // ReSharper restore UnusedMember.Global
    {
        protected readonly IManageConfigurations ConfigurationManager;

        public PrivateSamlCertificateStorage(IManageConfigurations configurationManager)
        {
            ConfigurationManager = configurationManager;
        }

        public X509Certificate2 GetSigningCertificate() { return GetCertificate(); }
        public X509Certificate2 GetEncryptionCertificate() { return GetCertificate(); }

        protected virtual string Thumbprint
        {
            get { return ConfigurationManager.SamlCertificateThumbprint; }
        }

        private X509Certificate2 GetCertificate()
        {
            var store = new X509Store(StoreName.My, StoreLocation.LocalMachine);
            try
            {
                store.Open(OpenFlags.ReadOnly);
                var certificates = store.Certificates.Find(X509FindType.FindByThumbprint, Thumbprint, false);
                if (certificates.Count < 1)
                {
                    throw new InvalidOperationException(string.Format(
                        "Could not find certificate with thumbprint '{0}' in My LocalMachine store.",
                            Thumbprint));
                }
                return certificates[0];
            }
            finally
            {
                store.Close();
            }
        }
    }
}