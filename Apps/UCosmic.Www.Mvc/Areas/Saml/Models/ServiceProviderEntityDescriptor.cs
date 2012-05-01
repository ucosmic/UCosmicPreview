namespace UCosmic.Www.Mvc.Areas.Saml.Models
{
    public class ServiceProviderEntityDescriptor
    {
        public string EntityId { get; set; }

        public string SigningX509SubjectName { get; set; }
        public string SigningX509Certificate { get; set; }

        public string EncryptionX509SubjectName { get; set; }
        public string EncryptionX509Certificate { get; set; }
    }
}