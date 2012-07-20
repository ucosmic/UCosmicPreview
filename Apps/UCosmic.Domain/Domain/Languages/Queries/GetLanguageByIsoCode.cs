using System;

namespace UCosmic.Domain.Languages
{
    public class GetLanguageByIsoCodeQuery : BaseEntityQuery<Language>, IDefineQuery<Language>
    {
        public string IsoCode { get; set; }
    }

    public class GetLanguageByIsoCodeHandler : IHandleQueries<GetLanguageByIsoCodeQuery, Language>
    {
        private readonly IQueryEntities _entities;

        public GetLanguageByIsoCodeHandler(IQueryEntities entities)
        {
            _entities = entities;
        }

        public Language Handle(GetLanguageByIsoCodeQuery query)
        {
            if (query == null) throw new ArgumentNullException("query");

            var result = _entities.Get<Language>()
                .EagerLoad(query.EagerLoad, _entities)
                .ByIsoCode(query.IsoCode)
            ;

            return result;
        }
    }
}
