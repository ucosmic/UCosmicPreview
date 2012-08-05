using System;
using System.Linq;

namespace UCosmic.Domain.Establishments
{
    public class EstablishmentCategoryByCode : BaseEntityQuery<EstablishmentCategory>, IDefineQuery<EstablishmentCategory>
    {
        public EstablishmentCategoryByCode(string code)
        {
            if (string.IsNullOrWhiteSpace(code))
                throw new ArgumentException("Cannot be null or white space", "code");
            Code = code;
        }

        public string Code { get; private set; }
    }

    public class HandleEstablishmentCategoryByCodeQuery : IHandleQueries<EstablishmentCategoryByCode, EstablishmentCategory>
    {
        private readonly IQueryEntities _entities;

        public HandleEstablishmentCategoryByCodeQuery(IQueryEntities entities)
        {
            _entities = entities;
        }

        public EstablishmentCategory Handle(EstablishmentCategoryByCode query)
        {
            if (query == null) throw new ArgumentNullException("query");

            return _entities.Query<EstablishmentCategory>()
                .EagerLoad(_entities, query.EagerLoad)
                .SingleOrDefault(t => t.Code.Equals(query.Code, StringComparison.OrdinalIgnoreCase))
            ;
        }
    }
}
