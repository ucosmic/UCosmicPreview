using System;
using System.Linq;

namespace UCosmic.Domain.Establishments
{
    public class FindAllEstablishmentTypesQuery : BaseEntitiesQuery<EstablishmentType>, IDefineQuery<EstablishmentType[]>
    {
    }

    public class FindAllEstablishmentTypesHandler : IHandleQueries<FindAllEstablishmentTypesQuery, EstablishmentType[]>
    {
        private readonly IQueryEntities _entities;

        public FindAllEstablishmentTypesHandler(IQueryEntities entities)
        {
            _entities = entities;
        }

        public EstablishmentType[] Handle(FindAllEstablishmentTypesQuery query)
        {
            if (query == null) throw new ArgumentNullException("query");

            var results = _entities.Query<EstablishmentType>()
                .EagerLoad(_entities, query.EagerLoad)
                .OrderBy(query.OrderBy);

            return results.ToArray();
        }
    }
}
