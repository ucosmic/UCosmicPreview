using System;
using UCosmic.Domain.People;

namespace UCosmic.Domain.Email
{
    public class GetEmailConfirmationQuery : BasePersonQuery, IDefineQuery<EmailConfirmation>
    {
        public GetEmailConfirmationQuery(Guid token)
        {
            if (token == Guid.Empty) throw new ArgumentException("Guid cannot be empty.", "token");
            Token = token;
        }

        public Guid Token { get; private set; }
    }
}
