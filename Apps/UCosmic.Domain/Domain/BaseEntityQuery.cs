using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace UCosmic.Domain
{
    public abstract class BaseEntityQuery<TEntity>
    {
        public IEnumerable<Expression<Func<TEntity, object>>> EagerLoad { get; set; }
    }
}
