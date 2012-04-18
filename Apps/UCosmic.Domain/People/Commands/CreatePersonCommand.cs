namespace UCosmic.Domain.People
{
    public class CreatePersonCommand
    {
        public string DisplayName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public bool UserIsRegistered { get; set; }
        public Person CreatedPerson { get; internal set; }
        internal string UserPersonDisplayName { get; set; }
    }
}
