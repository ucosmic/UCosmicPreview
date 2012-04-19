
namespace UCosmic.Domain.People
{
    public class FindDistinctSalutationsQuery : IDefineQuery<string[]>
    {
        public string[] Exclude { get; set; }
    }
}
