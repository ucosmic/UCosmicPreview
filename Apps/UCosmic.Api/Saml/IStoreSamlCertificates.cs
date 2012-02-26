using System.Security.Cryptography.X509Certificates;

namespace UCosmic
{
    public interface IStoreSamlCertificates
    {
        X509Certificate2 GetSigningCertificate();
        X509Certificate2 GetEncryptionCertificate();
    }
}
