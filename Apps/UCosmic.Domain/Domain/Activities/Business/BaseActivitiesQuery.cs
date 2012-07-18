using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace UCosmic.Domain.Activities
{
    public abstract class BaseActivitiesQuery : BaseActivityQuery
    {
        public IDictionary<Expression<Func<Activity, object>>, OrderByDirection> OrderBy { get; set; }
    }
}
