namespace UCosmic.Domain.Establishments
{
    public class EmailTemplate : RevisableEntity
    {
        public int? EstablishmentId { get; set; }
        public virtual Establishment Establishment { get; set; }

        public string Name { get; set; }

        public string Instructions { get; set; }

        public string SubjectFormat { get; set; }

        public string FromAddress { get; set; }

        public string FromDisplayName { get; set; }

        public string ReplyToAddress { get; set; }

        public string ReplyToDisplayName { get; set; }

        public string BodyFormat { get; set; }
    }
}