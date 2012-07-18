using System.Linq;

namespace UCosmic.Domain.Establishments
{
    public class FindSamlIntegratedEstablishmentsQuery : BaseEstablishmentQuery, IDefineQuery<Establishment[]>
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
            return _entities.Establishments
                .EagerLoad(query.EagerLoad, _entities)
                .SamlIntegrated()
                .ToArray()
            ;
        }
    }
}
