using System;
using System.Collections.Generic;
using System.Linq;

namespace UCosmic.Domain.People
{
    public class PersonFinder : RevisableEntityFinder<Person>
    {
        public PersonFinder(IQueryEntities entityQueries)
            : base(entityQueries)
        {
        }

        public override ICollection<Person> FindMany(RevisableEntityQueryCriteria<Person> criteria)
        {
            var query = InitializeQuery(EntityQueries.People, criteria);
            var finder = criteria as PersonQuery ?? new PersonQuery();

            // apply principal
            if (finder.Principal != null)
                query = query.Where(p => p.User != null
                    && p.User.Name.Equals(finder.Principal.Identity.Name,
                        StringComparison.OrdinalIgnoreCase));

            //// apply email entityId
            //if (finder.EmailEntityId.HasValue && finder.EmailEntityId != Guid.Empty)
            //    query = query.Where(p => p.Emails.Any(e =>
            //        e.EntityId == finder.EmailEntityId.Value));

            // apply email address
            if (finder.EmailAddress != null)
                query = query.Where(p => p.Emails.Any(e =>
                    e.Value.Equals(finder.EmailAddress, StringComparison.OrdinalIgnoreCase)));

            // apply email confirmation token
            if (finder.EmailConfirmationToken != null && finder.EmailConfirmationToken != Guid.Empty
                && !string.IsNullOrWhiteSpace(finder.EmailConfirmationIntent))
                query = query.Where(p => p.Emails.Any(e =>
                    e.Confirmations.Any(c =>
                        c.Token == finder.EmailConfirmationToken.Value
                            && c.Intent == finder.EmailConfirmationIntent)));

            // apply first name starts with
            if (!string.IsNullOrWhiteSpace(finder.AutoCompleteFirstNamePrefix))
                query = query.Where(p => p.FirstName != null &&
                    p.FirstName.Trim().ToLower().StartsWith(finder.AutoCompleteFirstNamePrefix.Trim().ToLower()));

            // apply last name starts with
            if (!string.IsNullOrWhiteSpace(finder.AutoCompleteLastNamePrefix))
                query = query.Where(p => p.LastName != null &&
                    p.LastName.Trim().ToLower().StartsWith(finder.AutoCompleteLastNamePrefix.Trim().ToLower()));

            // apply first name starts with
            if (!string.IsNullOrWhiteSpace(finder.AutoCompleteEmailTerm))
                query = query.Where(p => p.Emails.Any(e => e.Value.Contains(finder.AutoCompleteEmailTerm.Trim())));

            query = FinalizeQuery(query, criteria);

            var results = query.ToList();
            return results;
        }

        public IEnumerable<string> GetDistinctSalutations()
        {
            return EntityQueries.People.Where(p => p.Salutation != null && p.Salutation.Trim() != string.Empty)
                .Select(p => p.Salutation).Distinct().ToArray();
        }

        public IEnumerable<string> GetDistinctSuffixes()
        {
            return EntityQueries.People.Where(p => p.Suffix != null && p.Suffix.Trim() != string.Empty)
                .Select(p => p.Suffix).Distinct().ToArray();
        }
    }
}