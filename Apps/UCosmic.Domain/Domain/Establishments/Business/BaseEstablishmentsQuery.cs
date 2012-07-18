using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace UCosmic.Domain.Establishments
{
    public abstract class BaseEstablishmentsQuery : BaseEstablishmentQuery
    {
        public IDictionary<Expression<Func<Establishment, object>>, OrderByDirection> OrderBy { get; set; }
    }
}
