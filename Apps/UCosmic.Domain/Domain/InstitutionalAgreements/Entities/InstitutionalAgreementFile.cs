namespace UCosmic.Domain.InstitutionalAgreements
{
    public class InstitutionalAgreementFile : RevisableEntity
    {
        internal const string PathFormat = "/institutional-agreements/{0}/{1}";

        protected internal InstitutionalAgreementFile()
        {
        }

        public virtual InstitutionalAgreement Agreement { get; protected internal set; }

        //public byte[] Content { get; protected internal set; }
        public int Length { get; protected internal set; }
        public string MimeType { get; protected internal set; }
        public string Name { get; protected internal set; }
        public string Path { get; protected internal set; }
    }
}