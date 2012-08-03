using System.Linq;

namespace UCosmic.Domain.Establishments
{
    public class FindSamlIntegratedEstablishmentsQuery : BaseEntitiesQuery<Establishment>, IDefineQuery<Establishment[]>
    {
    }

    public class FindSamlIntegratedEstablishmentsHandler : IHandleQueries<FindSamlIntegratedEstablishmentsQuery, Establishment[]>
    {
        private readonly IQueryEntities _entities;

        public FindSamlIntegratedEstablishmentsHandler(IQueryEntities entities)
        {
            _entities = entities;
        }

        public Establishment[] Handle(FindSamlIntegratedEstablishmentsQuery query)
        {
            return _entities.Query<Establishment>()
                .EagerLoad(_entities, query.EagerLoad)
                .SamlIntegrated()
                .OrderBy(query.OrderBy)
                .ToArray()
            ;
        }
    }
}
