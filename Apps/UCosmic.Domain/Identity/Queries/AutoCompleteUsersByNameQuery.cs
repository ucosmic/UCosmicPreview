using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace UCosmic.Domain.Identity
{
    public class AutoCompleteUsersByNameQuery : IDefineQuery<User[]>
    {
        public string Term { get; set; }
        public IEnumerable<Guid> ExcludeEntityIds { get; set; }

        public IEnumerable<Expression<Func<User, object>>> EagerLoad { get; set; }
        public IDictionary<Expression<Func<User, object>>, OrderByDirection> OrderBy { get; set; }
    }
}
