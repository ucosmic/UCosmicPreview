using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace UCosmic.Domain.Identity
{
    public abstract class BaseUserQuery
    {
        public IEnumerable<Expression<Func<User, object>>> EagerLoad { get; set; }
    }
}
