using System;
using System.Linq;

namespace UCosmic.Domain.Establishments
{
    public class EstablishmentTypeByEnglishName : BaseEntityQuery<EstablishmentType>, IDefineQuery<EstablishmentType>
    {
        public EstablishmentTypeByEnglishName(string englishName)
        {
            if (string.IsNullOrWhiteSpace(englishName))
                throw new ArgumentException("Cannot be null or white space", "englishName");
            EnglishName = englishName;
        }

        public string EnglishName { get; private set; }
    }

    public class HandleEstablishmentTypeByEnglishNameQuery : IHandleQueries<EstablishmentTypeByEnglishName, EstablishmentType>
    {
        private readonly IQueryEntities _entities;

        public HandleEstablishmentTypeByEnglishNameQuery(IQueryEntities entities)
        {
            _entities = entities;
        }

        public EstablishmentType Handle(EstablishmentTypeByEnglishName query)
        {
            if (query == null) throw new ArgumentNullException("query");

            return _entities.Query<EstablishmentType>()
                .EagerLoad(_entities, query.EagerLoad)
                .SingleOrDefault(t => query.EnglishName.Equals(t.EnglishName, StringComparison.OrdinalIgnoreCase))
            ;
        }
    }
}
