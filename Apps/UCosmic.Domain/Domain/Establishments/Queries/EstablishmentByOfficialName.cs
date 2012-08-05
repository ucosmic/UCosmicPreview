using System;
using System.Linq;

namespace UCosmic.Domain.Establishments
{
    public class EstablishmentByOfficialName : BaseEntityQuery<Establishment>, IDefineQuery<Establishment>
    {
        public EstablishmentByOfficialName(string officialName)
        {
            if (string.IsNullOrWhiteSpace(officialName))
                throw new ArgumentException("Cannot be empty or white space.", "officialName");
            OfficialName = officialName;
        }

        public string OfficialName { get; private set; }
    }

    public class HandleEstablishmentByOfficialName : IHandleQueries<EstablishmentByOfficialName, Establishment>
    {
        private readonly IQueryEntities _entities;

        public HandleEstablishmentByOfficialName(IQueryEntities entities)
        {
            _entities = entities;
        }

        public Establishment Handle(EstablishmentByOfficialName query)
        {
            if (query == null) throw new ArgumentNullException("query");

            return _entities.Query<Establishment>()
                .EagerLoad(_entities, query.EagerLoad)
                .SingleOrDefault(x => query.OfficialName.Equals(x.OfficialName, StringComparison.OrdinalIgnoreCase))
            ;
        }
    }
}
