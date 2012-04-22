
namespace UCosmic.Domain.People
{
    public class FindPeopleWithFirstNameQuery : BasePeopleQuery, IDefineQuery<Person[]>
    {
        public string Term { get; set; }
        public StringMatchStrategy TermMatchStrategy { get; set; }
    }
}
