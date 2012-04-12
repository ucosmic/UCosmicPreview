namespace UCosmic.Domain.Establishments
{
    public class FindEstablishmentByEmailHandler : IHandleQueries<FindEstablishmentByEmailQuery, Establishment>
    {
        private readonly IQueryEntities _entities;

        public FindEstablishmentByEmailHandler(IQueryEntities entities)
        {
            _entities = entities;
        }

        public Establishment Handle(FindEstablishmentByEmailQuery query)
        {
            return _entities.Establishments
                .EagerLoad(query.EagerLoad, _entities)
                .ByEmail(query.Email);
        }
    }
}
