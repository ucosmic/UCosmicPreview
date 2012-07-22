using System;
using System.Security.Principal;

namespace UCosmic.Domain.People
{
    public class GetMyEmailAddressByNumberQuery : IDefineQuery<EmailAddress>
    {
        public IPrincipal Principal { get; set; }
        public int Number { get; set; }
    }

    public class GetMyEmailAddressByNumberHandler : IHandleQueries<GetMyEmailAddressByNumberQuery, EmailAddress>
    {
        private readonly IQueryEntities _entities;

        public GetMyEmailAddressByNumberHandler(IQueryEntities entities)
        {
            _entities = entities;
        }

        public EmailAddress Handle(GetMyEmailAddressByNumberQuery query)
        {
            if (query == null) throw new ArgumentNullException("query");

            return _entities.Query<EmailAddress>()
                .ByUserNameAndNumber(query.Principal.Identity.Name, query.Number);
        }
    }
}
