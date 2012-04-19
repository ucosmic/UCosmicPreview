
namespace UCosmic.Domain.People
{
    public class FindDistinctSuffixesQuery : IDefineQuery<string[]>
    {
        public string[] Exclude { get; set; }
    }
}
