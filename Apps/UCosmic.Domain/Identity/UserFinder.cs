using System;
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

            // apply username
            if (!string.IsNullOrWhiteSpace(finder.UserName))
                return new[] { query.SingleOrDefault(u => finder.UserName.Equals(u.UserName, StringComparison.OrdinalIgnoreCase)) };

            // apply saml2 subject name identifier
            if (!string.IsNullOrWhiteSpace(finder.Saml2SubjectNameId))
                return new[] { query.SingleOrDefault(u => finder.Saml2SubjectNameId.Equals(u.Saml2SubjectNameId, StringComparison.OrdinalIgnoreCase)) };

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