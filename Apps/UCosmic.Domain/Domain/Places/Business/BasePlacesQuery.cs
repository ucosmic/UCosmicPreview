using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace UCosmic.Domain.Places
{
    public abstract class BasePlacesQuery : BasePlaceQuery
    {
        public IDictionary<Expression<Func<Place, object>>, OrderByDirection> OrderBy { get; set; }
    }
}
