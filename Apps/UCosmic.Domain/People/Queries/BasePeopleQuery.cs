using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace UCosmic.Domain.People
{
    public abstract class BasePeopleQuery : BasePersonQuery
    {
        public IDictionary<Expression<Func<Person, object>>, OrderByDirection> OrderBy { get; set; }
    }
}
