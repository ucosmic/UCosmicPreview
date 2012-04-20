
namespace UCosmic.Domain.People
{
    public class GetPersonByIdQuery : BasePersonQuery, IDefineQuery<Person>
    {
        public int Id { get; set; }
    }
}
