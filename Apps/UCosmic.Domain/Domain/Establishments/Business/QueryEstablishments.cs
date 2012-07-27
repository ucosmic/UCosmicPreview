using System;
using System.Linq;
using System.Linq.Expressions;
using LinqKit;

namespace UCosmic.Domain.Establishments
{
    internal static class QueryEstablishments
    {
        internal static Establishment ByEmail(this IQueryable<Establishment> queryable, string email)
        {
            var emailDomain = email.GetEmailDomain();
            var establishment = queryable.SingleOrDefault
            (
                e =>
                e.EmailDomains.Any
                (
                    d =>
                    d.Value.Equals(emailDomain, StringComparison.OrdinalIgnoreCase)
                )
            );
            return establishment;
        }

        internal static Establishment ByUrl(this IQueryable<Establishment> queryable, string url)
        {
            var establishment = queryable.SingleOrDefault
            (
                e =>
                e.Urls.Any
                (
                    u =>
                    u.Value.Equals(url, StringComparison.OrdinalIgnoreCase)
                )
            );
            return establishment;
        }

        internal static Establishment BySamlEntityId(this IQueryable<Establishment> queryable, string entityId)
        {
            if (entityId == null) throw new ArgumentNullException("entityId");
            if (string.IsNullOrWhiteSpace(entityId)) throw new ArgumentException(string.Format(
                "The SAML EntityID '{0}' cannot be a null or whitespace string.", entityId));

            var establishment = queryable.SamlIntegrated().SingleOrDefault(e =>
                entityId.Equals(e.SamlSignOn.EntityId, StringComparison.OrdinalIgnoreCase));
            return establishment;
        }

        internal static IQueryable<Establishment> SamlIntegrated(this IQueryable<Establishment> queryable)
        {
            return queryable.Where(establishment => establishment.SamlSignOn != null);
        }

        internal static IQueryable<Establishment> IsRoot(this IQueryable<Establishment> queryable)
        {
            return queryable.Where(establishment => establishment.Parent == null);
        }

        internal static IQueryable<Establishment> IsNotRoot(this IQueryable<Establishment> queryable)
        {
            return queryable.Where(establishment => establishment.Parent != null);
        }

        internal static IQueryable<Establishment> WithAnyChildren(this IQueryable<Establishment> queryable)
        {
            return queryable.Where(establishment => establishment.Children.Any());
        }

        internal static IQueryable<Establishment> WithoutAnyChildren(this IQueryable<Establishment> queryable)
        {
            return queryable.Where(establishment => !establishment.Children.Any());
        }

        internal static IQueryable<Establishment> WithName(this IQueryable<Establishment> queryable, string term, StringMatchStrategy matchStrategy)
        {
            var names = QueryEstablishmentNames.SearchTermMatches(term, matchStrategy);
            // ReSharper disable ConvertClosureToMethodGroup
            Expression<Func<Establishment, bool>> establishments = establishment => establishment.Names.Any(name => names.Invoke(name));
            // ReSharper restore ConvertClosureToMethodGroup
            return queryable.AsExpandable().Where(establishments.Expand());
        }

        internal static IQueryable<Establishment> WithNameOrUrl(this IQueryable<Establishment> queryable, string term, StringMatchStrategy matchStrategy)
        {
            var names = QueryEstablishmentNames.SearchTermMatches(term, matchStrategy);
            var urls = QueryEstablishmentUrls.SearchTermMatches(term, matchStrategy);
            // ReSharper disable ConvertClosureToMethodGroup
            Expression<Func<Establishment, bool>> establishments = establishment =>
                establishment.Names.Any(name => names.Invoke(name)) ||
                establishment.Urls.Any(url => urls.Invoke(url));
            // ReSharper restore ConvertClosureToMethodGroup
            return queryable.AsExpandable().Where(establishments.Expand());
        }
    }
}
