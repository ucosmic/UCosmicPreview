namespace UCosmic.Domain.People
{
    public class GenerateDisplayNameQuery : IDefineQuery<string>
    {
        public string Salutation { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Suffix { get; set; }
    }
}
