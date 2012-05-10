namespace UCosmic.Www.Mvc.Areas.Identity.Models
{
    public class ServiceProviderEntityDescriptor
    {
        public string EntityId { get; set; }

        public string SigningX509KeyName { get { return SigningX509SubjectName.Substring(3); } }
        public string SigningX509SubjectName { get; set; }
        public string SigningX509Certificate { get; set; }

        public string EncryptionX509KeyName { get { return EncryptionX509SubjectName.Substring(3); } }
        public string EncryptionX509SubjectName { get; set; }
        public string EncryptionX509Certificate { get; set; }
    }
}