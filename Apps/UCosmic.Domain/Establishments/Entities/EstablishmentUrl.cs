namespace UCosmic.Domain.Establishments
{
    public class EstablishmentUrl : RevisableEntity
    {
        public virtual Establishment ForEstablishment { get; set; }

        public string Value { get; set; }

        public bool IsOfficialUrl { get; set; }

        public bool IsFormerUrl { get; set; }

        public override string ToString()
        {
            return Value;
        }
    }
}