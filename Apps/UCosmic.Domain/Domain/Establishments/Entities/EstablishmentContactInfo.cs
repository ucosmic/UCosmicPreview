namespace UCosmic.Domain.Establishments
{
    public class EstablishmentContactInfo
    {
        protected internal EstablishmentContactInfo()
        {
        }

        public string Phone { get; protected internal set; }
        public string Fax { get; protected internal set; }
        public string Email { get; protected internal set; }
    }
}