using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace UCosmic.Domain.Places
{
    public abstract class BasePlaceQuery : BaseQuery
    {
        public IEnumerable<Expression<Func<Place, object>>> EagerLoad { get; set; }
    }
}