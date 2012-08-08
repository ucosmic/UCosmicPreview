using System;

namespace UCosmic.Domain.Languages
{
    public class LanguageByIsoCode : BaseEntityQuery<Language>, IDefineQuery<Language>
    {
        public LanguageByIsoCode(string isoCode)
        {
            IsoCode = isoCode;
        }

        public string IsoCode { get; private set; }
    }

    public class HandleLanguageByIsoCodeQuery : IHandleQueries<LanguageByIsoCode, Language>
    {
        private readonly ICommandEntities _entities;

        public HandleLanguageByIsoCodeQuery(ICommandEntities entities)
        {
            _entities = entities;
        }

        public Language Handle(LanguageByIsoCode query)
        {
            if (query == null) throw new ArgumentNullException("query");

            var result = _entities.Get<Language>()
                .EagerLoad(_entities, query.EagerLoad)
                .ByIsoCode(query.IsoCode)
            ;

            return result;
        }
    }
}
