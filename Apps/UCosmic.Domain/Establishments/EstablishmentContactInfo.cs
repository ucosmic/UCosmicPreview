namespace UCosmic.Domain.Establishments
{
    public class EstablishmentContactInfo
    {
        public string Phone { get; set; }

        public string Fax { get; set; }

        public string Email { get; set; }

        public bool HasValue
        {
            get
            {
                return !string.IsNullOrWhiteSpace(Phone)
                    || !string.IsNullOrWhiteSpace(Fax)
                    || !string.IsNullOrWhiteSpace(Email);
            }
        }
    }
}