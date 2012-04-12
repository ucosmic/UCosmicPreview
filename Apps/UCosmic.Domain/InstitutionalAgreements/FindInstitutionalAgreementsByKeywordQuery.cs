using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace UCosmic.Domain.InstitutionalAgreements
{
    public class FindInstitutionalAgreementsByKeywordQuery : IDefineQuery<InstitutionalAgreement[]>
    {
        public string Keyword { get; set; }
        public int EstablishmentId { get; set; }
        public IEnumerable<Expression<Func<InstitutionalAgreement, object>>> EagerLoad { get; set; }
        public IDictionary<Expression<Func<InstitutionalAgreement, object>>, OrderByDirection> OrderBy { get; set; }
    }
}
