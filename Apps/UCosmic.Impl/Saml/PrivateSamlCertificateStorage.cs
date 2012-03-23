using System;
using System.Security.Cryptography.X509Certificates;

namespace UCosmic
{
    // ReSharper disable UnusedMember.Global
    public class PrivateSamlCertificateStorage : IStoreSamlCertificates
    // ReSharper restore UnusedMember.Global
    {
        private readonly IManageConfigurations _config;

        public PrivateSamlCertificateStorage(IManageConfigurations config)
        {
            _config = config;
        }

        public X509Certificate2 GetSigningCertificate() { return GetCertificate(); }
        public X509Certificate2 GetEncryptionCertificate() { return GetCertificate(); }

        private X509Certificate2 GetCertificate()
        {
            var store = new X509Store(StoreName.My, StoreLocation.LocalMachine);
            try
            {
                store.Open(OpenFlags.ReadOnly);
                var thumbprint = _config.SamlCertificateThumbprint;
                var certificates = store.Certificates.Find(X509FindType.FindByThumbprint, thumbprint, false);
                if (certificates.Count < 1)
                {
                    throw new InvalidOperationException(string.Format(
                        "Could not find certificate with thumbprint '{0}' in My LocalMachine store.",
                            _config.SamlCertificateThumbprint));
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