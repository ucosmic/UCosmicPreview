namespace UCosmic.Domain.InstitutionalAgreements
{
    public class InstitutionalAgreementFile : RevisableEntity
    {
        protected internal InstitutionalAgreementFile()
        {
        }

        public virtual InstitutionalAgreement Agreement { get; protected internal set; }

        public byte[] Content { get; protected internal set; }
        public int Length { get; protected internal set; }
        public string MimeType { get; protected internal set; }
        public string Name { get; protected internal set; }
    }
}