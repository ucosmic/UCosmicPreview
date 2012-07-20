using System;

namespace UCosmic.Domain.Establishments
{
    public class GetEstablishmentByEmailQuery : BaseEntityQuery<Establishment>, IDefineQuery<Establishment>
    {
        public GetEstablishmentByEmailQuery(string email)
        {
            if (string.IsNullOrWhiteSpace(email)) 
                throw new ArgumentException("Cannot be null or white space.", "email");
            Email = email;
        }
        public string Email { get; private set; }
    }

    public class GetEstablishmentByEmailHandler : IHandleQueries<GetEstablishmentByEmailQuery, Establishment>
    {
        private readonly IQueryEntities _entities;

        public GetEstablishmentByEmailHandler(IQueryEntities entities)
        {
            _entities = entities;
        }

        public Establishment Handle(GetEstablishmentByEmailQuery query)
        {
            if (query == null) throw new ArgumentNullException("query");

            if (string.IsNullOrWhiteSpace(query.Email) || !query.Email.Contains("@"))
                return null;

            return _entities.Get<Establishment>()
                .EagerLoad(query.EagerLoad, _entities)
                .ByEmail(query.Email)
            ;
        }
    }
}
