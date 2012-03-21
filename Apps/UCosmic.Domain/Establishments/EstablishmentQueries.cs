using System;
using System.Linq;

namespace UCosmic.Domain.Establishments
{
    internal static class EstablishmentQueries
    {
        internal static Establishment ByEmail(this IQueryable<Establishment> queryable, string email)
        {
            var establishment = queryable.SingleOrDefault(e => e.EmailDomains.Any(d => 
                d.Value.Equals(email.GetEmailDomain(), StringComparison.OrdinalIgnoreCase)));
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
            return queryable.Where(e => e.SamlSignOn != null);
        }
    }
}
