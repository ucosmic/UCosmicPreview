using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace UCosmic.Domain.Establishments
{
    public class FindEstablishmentByEmailQuery : IDefineQuery<Establishment>
    {
        public string Email { get; set; }

        public IEnumerable<Expression<Func<Establishment, object>>> EagerLoad { get; set; }
    }
}
