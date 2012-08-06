namespace UCosmic.Domain.Activities
{
    public class DraftedTag : Entity, IAmNumbered
    {
        protected internal DraftedTag()
        {
        }

        public int ActivityPersonId { get; protected internal set; }
        public int ActivityNumber { get; protected internal set; }
        public virtual Activity Activity { get; protected internal set; }

        public int Number { get; protected internal set; }
        public string Text { get; protected internal set; }

        public string DomainTypeText { get; protected set; }
        public ActivityTagDomainType DomainType
        {
            get { return DomainTypeText.AsEnum<ActivityTagDomainType>(); }
            protected internal set { DomainTypeText = value.AsSentenceFragment(); }
        }

        public int? DomainKey { get; protected internal set; }
    }
}
