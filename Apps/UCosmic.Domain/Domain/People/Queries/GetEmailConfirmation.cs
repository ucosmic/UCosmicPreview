using System;
using System.Linq;
using System.Linq.Expressions;

namespace UCosmic.Domain.People
{
    public class GetEmailConfirmationQuery : BaseEntityQuery<Person>, IDefineQuery<EmailConfirmation>
    {
        public GetEmailConfirmationQuery(Guid token)
        {
            if (token == Guid.Empty) throw new ArgumentException("Guid cannot be empty.", "token");
            Token = token;
        }

        public Guid Token { get; private set; }
    }

    public class GetEmailConfirmationHandler : IHandleQueries<GetEmailConfirmationQuery, EmailConfirmation>
    {
        private readonly IQueryEntities _entities;

        public GetEmailConfirmationHandler(IQueryEntities entities)
        {
            _entities = entities;
        }

        public EmailConfirmation Handle(GetEmailConfirmationQuery query)
        {
            if (query == null) throw new ArgumentNullException("query");

            query.EagerLoad = query.EagerLoad ??
                new Expression<Func<Person, object>>[]
                {
                    p => p.Emails.Select(e => e.Confirmations),
                    p => p.User,
                };

            return _entities.Get<Person>()
                .EagerLoad(query.EagerLoad, _entities)
                .ByEmailConfirmation(query.Token)
                .GetEmailConfirmation(query.Token)
            ;
        }
    }
}
