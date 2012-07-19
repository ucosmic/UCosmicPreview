//using System;
//using System.Collections.Generic;
//using System.Linq;

//namespace UCosmic.Domain.Establishments
//{
//    public class EstablishmentFinder : RevisableEntityFinder<Establishment>
//    {
//        public EstablishmentFinder(IQueryEntities entityQueries)
//            : base(entityQueries)
//        {
//        }

//        public override ICollection<Establishment> FindMany(RevisableEntityQueryCriteria<Establishment> criteria)
//        {
//            var query = InitializeQuery(EntityQueries.Establishments, criteria);
//            var finder = criteria as EstablishmentQuery ?? new EstablishmentQuery();

//            // apply has parent
//            if (finder.HasParent.HasValue)
//                query = (finder.HasParent.Value)
//                    ? query.Where(e => e.Parent != null)
//                    : query.Where(e => e.Parent == null);

//            // apply has children
//            if (finder.HasChildren.HasValue)
//                query = (finder.HasChildren.Value)
//                    ? query.Where(e => e.Children.Any())
//                    : query.Where(e => !e.Children.Any());

//            // apply website URL
//            if (!string.IsNullOrWhiteSpace(finder.WebsiteUrl))
//                query = query.Where(e => finder.WebsiteUrl.Equals(e.WebsiteUrl, StringComparison.OrdinalIgnoreCase) 
//                    || e.Urls.Any(u => finder.WebsiteUrl.Equals(u.Value, StringComparison.OrdinalIgnoreCase)));

//            // apply email domain
//            if (!string.IsNullOrWhiteSpace(finder.EmailDomain))
//            {
//                const char at = '@';
//                var emailDomain = finder.EmailDomain;
//                if (emailDomain.Contains(at) && finder.EmailDomain.IndexOf(at) == finder.EmailDomain.LastIndexOf(at))
//                    emailDomain = emailDomain.Substring(emailDomain.IndexOf(at));

//                query = query.Where(e => e.EmailDomains.AsQueryable().Any(d =>
//                    d.IsCurrent && !e.IsArchived && !e.IsDeleted
//                        && d.Value.Equals(emailDomain, StringComparison.OrdinalIgnoreCase)));
//            }

//            // apply autocomplete term
//            if (!string.IsNullOrWhiteSpace(finder.AutoCompleteTerm))
//            {
//                query = query.Where(e =>

//                    // find names containing the term
//                    e.Names.Any(n => (n.Text.Contains(finder.AutoCompleteTerm)
//                        || (n.AsciiEquivalent != null
//                            && n.AsciiEquivalent.Contains(finder.AutoCompleteTerm)))
//                        && n.IsCurrent && !n.IsDeleted && !n.IsArchived)

//                    // find URL's containing the term
//                    || e.Urls.Any(u => u.Value.Contains(finder.AutoCompleteTerm)
//                        && u.IsCurrent && !u.IsDeleted && !u.IsArchived)

//                    // find ancestor names containing the term
//                    || e.Ancestors.Any(a => a.Ancestor.Names.Any(n => (n.Text.Contains(finder.AutoCompleteTerm)
//                        || (n.AsciiEquivalent != null
//                            && n.AsciiEquivalent.Contains(finder.AutoCompleteTerm)))
//                        && n.IsCurrent && !n.IsDeleted && !n.IsArchived))

//                    // find ancestor URL's containing the term
//                    || e.Ancestors.Any(a => a.Ancestor.Urls.Any(u => u.Value.Contains(finder.AutoCompleteTerm)
//                        && u.IsCurrent && !u.IsDeleted && !u.IsArchived))
//                );
//            }

//            // apply saml entity id
//            if (!string.IsNullOrWhiteSpace(finder.SamlEntityId))
//                query = query.Where(e => e.SamlSignOn != null && finder.SamlEntityId.Equals(e.SamlSignOn.EntityId, StringComparison.OrdinalIgnoreCase));

//            query = FinalizeQuery(query, criteria);

//            var results = query.ToList();
//            return results;
//        }
//    }
//}
