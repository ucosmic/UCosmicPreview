namespace UCosmic.Domain.Establishments
{
    public class EmailTemplate : RevisableEntity
    {
        protected internal EmailTemplate()
        {
        }

        public int? EstablishmentId { get; protected internal set; }
        public virtual Establishment Establishment { get; protected set; }

        public string Name { get; protected internal set; }

        public string SubjectFormat { get; protected internal set; }
        public string BodyFormat { get; protected internal set; }
        public string Instructions { get; protected internal set; }

        public string FromAddress { get; protected internal set; }
        public string FromDisplayName { get; protected internal set; }

        public string ReplyToAddress { get; protected internal set; }
        public string ReplyToDisplayName { get; protected internal set; }
    }
}