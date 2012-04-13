using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace UCosmic.Domain.Establishments
{
    public class GetEstablishmentBySamlEntityIdQuery : IDefineQuery<Establishment>
    {
        public string SamlEntityId { get; set; }
        public IEnumerable<Expression<Func<Establishment, object>>> EagerLoad { get; set; }
    }
}
