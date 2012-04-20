
namespace UCosmic.Domain.People
{
    public class GetPersonByEmailQuery : BasePersonQuery, IDefineQuery<Person>
    {
        public string Email { get; set; }
    }
}
