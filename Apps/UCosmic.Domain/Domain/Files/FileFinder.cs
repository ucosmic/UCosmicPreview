using System.Collections.Generic;
using System.Linq;

namespace UCosmic.Domain.Files
{
    public class FileFinder : RevisableEntityFinder<LooseFile>
    {
        public FileFinder(IQueryEntities entityQueries)
            : base(entityQueries)
        {
        }

        public override ICollection<LooseFile> FindMany(RevisableEntityQueryCriteria<LooseFile> criteria)
        {
            var query = InitializeQuery(EntityQueries.Files, criteria);

            query = FinalizeQuery(query, criteria);

            var results = query.ToList();
            return results;
        }
    }
}