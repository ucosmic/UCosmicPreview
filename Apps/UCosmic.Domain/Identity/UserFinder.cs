using System.Collections.Generic;
using System.Linq;

namespace UCosmic.Domain.Identity
{
    public class UserFinder : RevisableEntityFinder<User>
    {
        public UserFinder(IQueryEntities entityQueries)
            : base(entityQueries)
        {
        }

        public override ICollection<User> FindMany(RevisableEntityQueryCriteria<User> criteria)
        {
            var query = InitializeQuery(EntityQueries.Users, criteria);
            var finder = criteria as UserQuery ?? new UserQuery();

            // apply autocomplete term
            if (!string.IsNullOrWhiteSpace(finder.AutoCompleteTerm))
            {
                query = query.Where(e =>

                    // find usernames containing the term
                    e.UserName.Contains(finder.AutoCompleteTerm)
                );
            }

            query = FinalizeQuery(query, criteria);

            var results = query.ToList();
            return results;
        }
    }
}