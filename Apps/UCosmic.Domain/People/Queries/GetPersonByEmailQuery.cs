namespace UCosmic.Domain.People
{
    public class GetPersonByEmailQuery : IDefineQuery<Person>
    {
        public string Email { get; set; }
    }
}
