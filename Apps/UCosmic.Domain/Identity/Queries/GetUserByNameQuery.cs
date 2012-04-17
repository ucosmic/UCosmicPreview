using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace UCosmic.Domain.Identity
{
    public class GetUserByNameQuery : BaseUserQuery, IDefineQuery<User>
    {
        public string Name { get; set; }
    }
}
