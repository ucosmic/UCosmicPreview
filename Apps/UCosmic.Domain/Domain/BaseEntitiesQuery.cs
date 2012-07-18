using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace UCosmic.Domain
{
    public abstract class BaseEntitiesQuery<TEntity> : BaseEntityQuery<TEntity>
    {
        public IDictionary<Expression<Func<TEntity, object>>, OrderByDirection> OrderBy { get; set; }
    }
}
