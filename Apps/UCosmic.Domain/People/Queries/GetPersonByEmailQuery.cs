using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace UCosmic.Domain.People
{
    public class GetPersonByEmailQuery : IDefineQuery<Person>
    {
        public string Email { get; set; }
        public IEnumerable<Expression<Func<Person, object>>> EagerLoad { get; set; }
    }
}
