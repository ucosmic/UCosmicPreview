using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace UCosmic.Domain.Identity
{
    public abstract class BaseUsersQuery : BaseUserQuery
    {
        public IDictionary<Expression<Func<User, object>>, OrderByDirection> OrderBy { get; set; }
    }
}
