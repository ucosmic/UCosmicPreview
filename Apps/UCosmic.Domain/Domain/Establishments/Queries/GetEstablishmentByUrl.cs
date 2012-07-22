using System;

namespace UCosmic.Domain.Establishments
{
    public class GetEstablishmentByUrlQuery : BaseEntityQuery<Establishment>, IDefineQuery<Establishment>
    {
        public GetEstablishmentByUrlQuery(string url)
        {
            if (string.IsNullOrWhiteSpace(url))
                throw new ArgumentException("Cannot be null or white space.", "url");
            Url = url;
        }

        public string Url { get; private set; }
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

            return _entities.Query<Establishment>()
                .EagerLoad(query.EagerLoad, _entities)
                .ByUrl(query.Url)
            ;
        }
    }
}
