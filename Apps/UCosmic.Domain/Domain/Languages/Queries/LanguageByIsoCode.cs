using System;

namespace UCosmic.Domain.Languages
{
    public class LanguageByIsoCode : BaseEntityQuery<Language>, IDefineQuery<Language>
    {
        public string IsoCode { get; set; }
    }

    public class HandleLanguageByIsoCodeQuery : IHandleQueries<LanguageByIsoCode, Language>
    {
        private readonly IQueryEntities _entities;

        public HandleLanguageByIsoCodeQuery(IQueryEntities entities)
        {
            _entities = entities;
        }

        public Language Handle(LanguageByIsoCode query)
        {
            if (query == null) throw new ArgumentNullException("query");

            var result = _entities.Query<Language>()
                .EagerLoad(_entities, query.EagerLoad)
                .ByIsoCode(query.IsoCode)
            ;

            return result;
        }
    }
}
