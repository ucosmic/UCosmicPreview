using System;
using System.Collections.Generic;
using System.Linq;

namespace UCosmic.Domain.Identity
{
    public class RoleFinder : RevisableEntityFinder<Role>
    {
        public RoleFinder(IQueryEntities entityQueries)
            : base(entityQueries)
        {
        }

        public override ICollection<Role> FindMany(RevisableEntityQueryCriteria<Role> criteria)
        {
            var query = InitializeQuery(EntityQueries.Roles, criteria);
            var finder = criteria as RoleQuery ?? new RoleQuery();

            // apply name
            if (!string.IsNullOrWhiteSpace(finder.Name))
                return new List<Role>{ query.SingleOrDefault(e => 
                    e.Name.Equals(finder.Name, StringComparison.OrdinalIgnoreCase)) };

            // apply slug
            if (!string.IsNullOrWhiteSpace(finder.Slug))
                return new List<Role>{ query.SingleOrDefault(e => 
                    e.Name.Equals(finder.Slug.Replace("-", " "), StringComparison.OrdinalIgnoreCase)) };

            // apply granted to username 
            if (!string.IsNullOrWhiteSpace(finder.GrantToUser))
                query = query.Where(role => role.Grants.Any(grant =>
                    grant.IsCurrent && !grant.IsArchived && !grant.IsDeleted
                        && grant.User.UserName.Equals(finder.GrantToUser, StringComparison.OrdinalIgnoreCase)));

            query = FinalizeQuery(query, criteria);

            var results = query.ToList();
            return results;
        }
    }
}