using System;
using System.Linq.Expressions;
using UCosmic.Domain.Identity;

namespace UCosmic.Domain.People
{
    public class GetMyEmailAddressByNumberHandler : IHandleQueries<GetMyEmailAddressByNumberQuery, EmailAddress>
    {
        private readonly IProcessQueries _queryProcessor;

        public GetMyEmailAddressByNumberHandler(IProcessQueries queryProcessor)
        {
            _queryProcessor = queryProcessor;
        }

        public EmailAddress Handle(GetMyEmailAddressByNumberQuery query)
        {
            if (query == null) throw new ArgumentNullException("query");

            var user = _queryProcessor.Execute(
                new GetUserByNameQuery
                {
                    Name = query.Principal.Identity.Name,
                    EagerLoad = new Expression<Func<User, object>>[]
                    {
                        u => u.Person.Emails,
                    },
                }
            );

            if (user == null) return null;

            var email = user.Person.GetEmail(query.Number);

            return email;
        }
    }
}
