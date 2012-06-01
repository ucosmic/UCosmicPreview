using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace UCosmic.Domain.Activities
{
    public abstract class BaseActivityQuery : BaseQuery
    {
        public IEnumerable<Expression<Func<Activity, object>>> EagerLoad { get; set; }
    }
}