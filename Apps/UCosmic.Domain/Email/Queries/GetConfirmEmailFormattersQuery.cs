using System;
using System.Collections.Generic;
using UCosmic.Domain.People;

namespace UCosmic.Domain.Email
{
    public class GetConfirmEmailFormattersQuery : IDefineQuery<IDictionary<string, string>>
    {
        public GetConfirmEmailFormattersQuery(EmailConfirmation confirmation)
        {
            if (confirmation == null) throw new ArgumentNullException("confirmation");
            Confirmation = confirmation;
        }

        public EmailConfirmation Confirmation { get; private set; }
        public IDictionary<string, string> AdditionalFormatters { get; set; }
    }
}
