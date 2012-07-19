using System;
using System.Linq;

namespace UCosmic.Domain.Establishments
{
    public class GetEstablishmentTypeByEnglishNameQuery : BaseEntityQuery<EstablishmentType>, IDefineQuery<EstablishmentType>
    {
        public GetEstablishmentTypeByEnglishNameQuery(string englishName)
        {
            if (string.IsNullOrWhiteSpace(englishName))
                throw new ArgumentException("Cannot be null or white space.", "englishName");
            EnglishName = englishName;
        }

        public string EnglishName { get; private set; }
    }

    public class GetEstablishmentTypeByEnglishNameHandler : IHandleQueries<GetEstablishmentTypeByEnglishNameQuery, EstablishmentType>
    {
        private readonly IQueryEntities _entities;

        public GetEstablishmentTypeByEnglishNameHandler(IQueryEntities entities)
        {
            _entities = entities;
        }

        public EstablishmentType Handle(GetEstablishmentTypeByEnglishNameQuery query)
        {
            if (query == null) throw new ArgumentNullException("query");

            return _entities.Get<EstablishmentType>()
                .EagerLoad(query.EagerLoad, _entities)
                .SingleOrDefault(t => query.EnglishName.Equals(t.EnglishName, StringComparison.OrdinalIgnoreCase))
            ;
        }
    }
}
