using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace UCosmic.Domain.Identity
{
    public class GetUserByEduPersonTargetedIdQuery : IDefineQuery<User>
    {
        public string EduPersonTargetedId { get; set; }

        public IEnumerable<Expression<Func<User, object>>> EagerLoad { get; set; }
    }
}
