using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace UCosmic.Domain.People
{
    public abstract class BasePersonQuery : BaseQuery
    {
        public IEnumerable<Expression<Func<Person, object>>> EagerLoad { get; set; }
    }
}