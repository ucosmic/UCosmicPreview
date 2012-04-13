namespace UCosmic.Domain.Establishments
{
    public class GetEstablishmentBySamlEntityIdHandler : IHandleQueries<GetEstablishmentBySamlEntityIdQuery, Establishment>
    {
        private readonly IQueryEntities _entities;

        public GetEstablishmentBySamlEntityIdHandler(IQueryEntities entities)
        {
            _entities = entities;
        }

        public Establishment Handle(GetEstablishmentBySamlEntityIdQuery query)
        {
            return _entities.Establishments
                .EagerLoad(query.EagerLoad, _entities)
                .BySamlEntityId(query.SamlEntityId)
            ;
        }
    }
}
