namespace UCosmic.Domain.Establishments
{
    public class EstablishmentBySamlEntityId : BaseEntitiesQuery<Establishment>, IDefineQuery<Establishment>
    {
        public string SamlEntityId { get; set; }
    }

    public class HandleEstablishmentBySamlEntityId : IHandleQueries<EstablishmentBySamlEntityId, Establishment>
    {
        private readonly IQueryEntities _entities;

        public HandleEstablishmentBySamlEntityId(IQueryEntities entities)
        {
            _entities = entities;
        }

        public Establishment Handle(EstablishmentBySamlEntityId query)
        {
            return _entities.Read<Establishment>()
                .EagerLoad(query.EagerLoad, _entities)
                .BySamlEntityId(query.SamlEntityId)
            ;
        }
    }
}
