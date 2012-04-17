using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace UCosmic.Domain.Identity
{
    public class AutoCompleteUsersByNameQuery : BaseUsersQuery, IDefineQuery<User[]>
    {
        public string Term { get; set; }
        public IEnumerable<Guid> ExcludeEntityIds { get; set; }
    }
}
