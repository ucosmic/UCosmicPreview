using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace UCosmic.Domain.Establishments
{
    public class GetEstablishmentByEmailQuery : BaseEstablishmentQuery, IDefineQuery<Establishment>
    {
        public string Email { get; set; }
    }
}
