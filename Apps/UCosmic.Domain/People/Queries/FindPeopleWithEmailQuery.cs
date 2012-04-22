
namespace UCosmic.Domain.People
{
    public class FindPeopleWithEmailQuery : BasePeopleQuery, IDefineQuery<Person[]>
    {
        public string Term { get; set; }
        public StringMatchStrategy TermMatchStrategy { get; set; }
    }
}
