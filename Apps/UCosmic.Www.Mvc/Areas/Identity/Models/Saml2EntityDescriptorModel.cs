namespace UCosmic.Www.Mvc.Areas.Identity.Models
{
    public class Saml2EntityDescriptorModel
    {
        public string EntityId { get; set; }

        public string SigningX509SubjectName { get; set; }
        public string SigningX509Certificate { get; set; }

        public string EncryptionX509SubjectName { get; set; }
        public string EncryptionX509Certificate { get; set; }
    }
}