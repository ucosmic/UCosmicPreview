using System.Linq;

namespace UCosmic.Domain.InstitutionalAgreements
{
    internal static class QueryInstitutionalAgreements
    {
        internal static IQueryable<InstitutionalAgreement> IsRoot(this IQueryable<InstitutionalAgreement> queryable)
        {
            return queryable.Where(a => a.Umbrella == null);
        }

        internal static IQueryable<InstitutionalAgreement> WithAnyChildren(this IQueryable<InstitutionalAgreement> queryable)
        {
            return queryable.Where(a => a.Children.Any());
        }
    }
}
