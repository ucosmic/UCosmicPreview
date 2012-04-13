using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace UCosmic.Domain.Establishments
{
    public class FindSamlIntegratedEstablishmentsQuery : IDefineQuery<Establishment[]>
    {
        public IEnumerable<Expression<Func<Establishment, object>>> EagerLoad { get; set; }
    }
}
