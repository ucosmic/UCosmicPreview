using System;
using System.Collections.Generic;
using System.Linq;

namespace UCosmic.Domain.Identity
{
    public class AutoCompleteUsersByNameQuery : BaseEntitiesQuery<User>, IDefineQuery<User[]>
    {
        public string Term { get; set; }
        public IEnumerable<Guid> ExcludeEntityIds { get; set; }
    }

    public class AutoCompleteUsersByNameHandler : IHandleQueries<AutoCompleteUsersByNameQuery, User[]>
    {
        private readonly IQueryEntities _entities;

        public AutoCompleteUsersByNameHandler(IQueryEntities entities)
        {
            _entities = entities;
        }

        public User[] Handle(AutoCompleteUsersByNameQuery query)
        {
            return _entities.Query<User>()
                .EagerLoad(_entities, query.EagerLoad)
                .Exclude(query.ExcludeEntityIds)
                .AutoComplete(query.Term)
                .OrderBy(query.OrderBy)
                .ToArray()
            ;
        }
    }
}
