using System;
using System.Linq;

namespace UCosmic.Domain.People
{
    public class FindDistinctSuffixesQuery : IDefineQuery<string[]>
    {
        public string[] Exclude { get; set; }
    }

    public class FindDistinctSuffixesHandler : IHandleQueries<FindDistinctSuffixesQuery, string[]>
    {
        private readonly IQueryEntities _entities;

        public FindDistinctSuffixesHandler(IQueryEntities entities)
        {
            _entities = entities;
        }

        public string[] Handle(FindDistinctSuffixesQuery query)
        {
            if (query == null) throw new ArgumentNullException("query");

            var results = _entities.Read<Person>()
                .WithNonEmptySuffix()
                .SelectSuffixes()
                .Distinct()
            ;

            if (query.Exclude != null && query.Exclude.Length > 0)
                results = results.Where(s => !query.Exclude.Contains(s));

            return results.ToArray();
        }
    }
}
