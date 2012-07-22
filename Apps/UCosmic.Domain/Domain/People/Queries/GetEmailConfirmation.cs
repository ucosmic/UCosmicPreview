using System;

namespace UCosmic.Domain.People
{
    public class GetEmailConfirmationQuery : BaseEntityQuery<EmailConfirmation>, IDefineQuery<EmailConfirmation>
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

            return _entities.Query<EmailConfirmation>()
                .EagerLoad(query.EagerLoad, _entities)
                .ByToken(query.Token);
        }
    }
}
