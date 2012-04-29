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

    public class GetConfirmEmailFormattersHandler : IHandleQueries<GetConfirmEmailFormattersQuery, IDictionary<string, string>>
    {
        private readonly IManageConfigurations _configurationManager;

        public GetConfirmEmailFormattersHandler(IManageConfigurations configurationManager)
        {
            _configurationManager = configurationManager;
        }

        public IDictionary<string, string> Handle(GetConfirmEmailFormattersQuery query)
        {
            var formatters = new Dictionary<string, string>
            {
                { "{EmailAddress}", query.Confirmation.EmailAddress.Value },
                { "{ConfirmationCode}", query.Confirmation.SecretCode },
                { "{ConfirmationUrl}", string.Format(_configurationManager.EmailConfirmationUrlFormat,
                    query.Confirmation.Token,
                    query.Confirmation.SecretCode.UrlEncoded())
                },
            };

            switch (query.Confirmation.Intent)
            {
                case EmailConfirmationIntent.SignUp:
                    formatters.Add("{StartUrl}", _configurationManager.SignUpUrl);
                    break;
                case EmailConfirmationIntent.PasswordReset:
                    formatters.Add("{PasswordResetUrl}", _configurationManager.PasswordResetUrl);
                    break;
            }

            if (query.AdditionalFormatters != null && query.AdditionalFormatters.Count > 0)
                foreach (var additionalFormatter in query.AdditionalFormatters)
                    if (!formatters.ContainsKey(additionalFormatter.Key))
                        formatters.Add(additionalFormatter.Key, additionalFormatter.Value);
                    else formatters[additionalFormatter.Key] = additionalFormatter.Value;

            return formatters;
        }
    }
}
