using System.Linq;

namespace UCosmic.Domain.Identity
{
    public class AutoCompleteUsersByNameHandler : IHandleQueries<AutoCompleteUsersByNameQuery, User[]>
    {
        private readonly IQueryEntities _entities;

        public AutoCompleteUsersByNameHandler(IQueryEntities entities)
        {
            _entities = entities;
        }

        public User[] Handle(AutoCompleteUsersByNameQuery query)
        {
            return _entities.Users
                .EagerLoad(query.EagerLoad, _entities)
                .Exclude(query.ExcludeEntityIds)
                .AutoComplete(query.Term)
                .OrderBy(query.OrderBy)
                .ToArray()
            ;
        }
    }
}
