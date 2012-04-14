using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using UCosmic.Domain.People;
using System.Security.Principal;

namespace UCosmic.Domain.InstitutionalAgreements
{
    public class InstitutionalAgreementFinder : RevisableEntityFinder<InstitutionalAgreement>
    {
        public InstitutionalAgreementFinder(IQueryEntities entityQueries)
            : base(entityQueries)
        {
        }

        public override ICollection<InstitutionalAgreement> FindMany(RevisableEntityQueryCriteria<InstitutionalAgreement> criteria)
        {
            var query = InitializeQuery(EntityQueries.InstitutionalAgreements, criteria);
            var finder = criteria as InstitutionalAgreementQuery ?? new InstitutionalAgreementQuery();

            // apply file entity id
            if (finder.FileEntityId.HasValue)
                return new[] { query.SingleOrDefault(e => e.Files.Any(file => 
                    file.EntityId == finder.FileEntityId.Value)) };

            // apply has umbrella
            if (finder.HasUmbrella.HasValue)
                query = (finder.HasUmbrella.Value)
                    ? query.Where(e => e.Umbrella != null)
                    : query.Where(e => e.Umbrella == null);

            // apply has children
            if (finder.HasChildren.HasValue)
                query = (finder.HasChildren.Value)
                    ? query.Where(e => e.Children.Any())
                    : query.Where(e => !e.Children.Any());

            // apply principal context
            if (finder.PrincipalContext != null)
            {
                // select all agreements where this user's default affiliation is the owner
                Expression<Func<Affiliation, bool>> principalDefaultAffiliation = affiliation =>
                    affiliation.IsDefault && affiliation.Person.User != null
                        && affiliation.Person.User.Name.Equals(finder.PrincipalContext.Identity.Name,
                            StringComparison.OrdinalIgnoreCase);
                query = query.Where(agreement => agreement.Participants.Any(
                    participant => participant.IsOwner // only get owning participants
                    && ( // where the owning participant is user's default establishment
                            participant.Establishment.Affiliates.AsQueryable().Any(principalDefaultAffiliation)
                            || // or the owning participant's ancestor is user's default establishment
                            participant.Establishment.Ancestors.Any(ancestor =>
                                ancestor.Ancestor.Affiliates.AsQueryable().Any(principalDefaultAffiliation)
                        )
                    )
                ));
            }

            // apply owned by establishment URL
            if (!string.IsNullOrWhiteSpace(finder.OwnedByEstablishmentUrl))
            {
                query = query.Where(e =>
                    e.Participants.Any(p => p.IsOwner &&
                        (finder.OwnedByEstablishmentUrl.Equals(p.Establishment.WebsiteUrl, StringComparison.OrdinalIgnoreCase)) ||
                        p.Establishment.Ancestors.Any(h => finder.OwnedByEstablishmentUrl.Equals(h.Ancestor.WebsiteUrl, StringComparison.OrdinalIgnoreCase))
                    )
                );
            }

            query = FinalizeQuery(query, criteria);

            var results = query.ToList();
            return results;
        }

        public IEnumerable<InstitutionalAgreement> GetUmbrellaOptions(int agreementRevisionId, IPrincipal principal)
        {
            var options = FindMany(InstitutionalAgreementsWith.PrincipalContext(principal)
                .EagerLoad(e => e.Ancestors)
            );
            var exclusions = options.Where(e => e.Ancestors.Any(ancestor =>
                ancestor.Ancestor.RevisionId == agreementRevisionId)).Select(e => e.RevisionId);
            return options.Where(e => !exclusions.Contains(e.RevisionId) && agreementRevisionId != e.RevisionId);
        }
    }

    public static class InstitutionalAgreementFinderExtensions
    {
        public static InstitutionalAgreement OwnedBy(this InstitutionalAgreement agreement, IPrincipal principal)
        {
            //if (agreement == null || principal == null) return null;
            //var isOwned =
            //    agreement.Participants.Where(p => p.IsOwner).Any(
            //        p =>
            //        p.Establishment.Affiliates.Any(
            //            a =>
            //            a.IsDefault && a.Person.User != null &&
            //            a.Person.User.UserName.Equals(principal.Identity.Name, StringComparison.OrdinalIgnoreCase))
            //        ||
            //        p.Establishment.Ancestors.Any(n => n.Ancestor.Affiliates.Any(
            //            a =>
            //            a.IsDefault && a.Person.User != null &&
            //            a.Person.User.UserName.Equals(principal.Identity.Name, StringComparison.OrdinalIgnoreCase)))
            //    );
            //return (isOwned) ? agreement : null;
            return (agreement.IsOwnedBy(principal)) ? agreement : null;
        }

        public static bool IsOwnedBy(this InstitutionalAgreement agreement, IPrincipal principal)
        {
            if (agreement == null || principal == null) return false;
            var isOwned =
                agreement.Participants.Where(p => p.IsOwner).Any(
                    p =>
                    p.Establishment.Affiliates.Any(
                        a =>
                        a.IsDefault && a.Person.User != null &&
                        a.Person.User.Name.Equals(principal.Identity.Name, StringComparison.OrdinalIgnoreCase))
                    ||
                    p.Establishment.Ancestors.Any(n => n.Ancestor.Affiliates.Any(
                        a =>
                        a.IsDefault && a.Person.User != null &&
                        a.Person.User.Name.Equals(principal.Identity.Name, StringComparison.OrdinalIgnoreCase)))
                );
            return isOwned;
        }
    }
}
