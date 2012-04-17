using System;
using System.Linq.Expressions;
using UCosmic.Domain.Identity;

namespace UCosmic.Domain.People
{
    public class GetEmailAddressByUserNameAndNumberHandler : IHandleQueries<GetEmailAddressByUserNameAndNumberQuery, EmailAddress>
    {
        private readonly IProcessQueries _queryProcessor;

        public GetEmailAddressByUserNameAndNumberHandler(IProcessQueries queryProcessor)
        {
            _queryProcessor = queryProcessor;
        }

        public EmailAddress Handle(GetEmailAddressByUserNameAndNumberQuery query)
        {
            var user = _queryProcessor.Execute(
                new GetUserByNameQuery
                {
                    Name = query.UserName,
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
