using System;
using System.Linq;
using System.Linq.Expressions;
using UCosmic.Domain.People;

namespace UCosmic.Domain.Email
{
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

            return _entities.People
                .EagerLoad(query.EagerLoad, _entities)
                .ByEmailConfirmation(query.Token)
                .GetEmailConfirmation(query.Token)
            ;
        }
    }
}
