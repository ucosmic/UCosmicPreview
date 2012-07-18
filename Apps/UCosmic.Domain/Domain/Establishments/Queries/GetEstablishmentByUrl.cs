using System;

namespace UCosmic.Domain.Establishments
{
    public class GetEstablishmentByUrlQuery : BaseEstablishmentQuery, IDefineQuery<Establishment>
    {
        public string Url { get; set; }
    }

    public class GetEstablishmentByUrlHandler : IHandleQueries<GetEstablishmentByUrlQuery, Establishment>
    {
        private readonly IQueryEntities _entities;

        public GetEstablishmentByUrlHandler(IQueryEntities entities)
        {
            _entities = entities;
        }

        public Establishment Handle(GetEstablishmentByUrlQuery query)
        {
            if (query == null) throw new ArgumentNullException("query");

            if (string.IsNullOrWhiteSpace(query.Url))
                return null;

            return _entities.Establishments
                .EagerLoad(query.EagerLoad, _entities)
                .ByUrl(query.Url)
            ;
        }
    }
}
