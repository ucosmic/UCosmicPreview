namespace UCosmic.Domain.Establishments
{
    public class EstablishmentUrl : RevisableEntity
    {
        protected internal EstablishmentUrl()
        {
        }

        public virtual Establishment ForEstablishment { get; protected internal set; }
        public string Value { get; protected internal set; }

        public bool IsOfficialUrl { get; protected internal set; }
        public bool IsFormerUrl { get; protected internal set; }

        public override string ToString()
        {
            return Value;
        }
    }
}