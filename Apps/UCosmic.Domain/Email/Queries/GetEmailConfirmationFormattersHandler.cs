using System.Collections.Generic;
using UCosmic.Domain.People;

namespace UCosmic.Domain.Email
{
    public class GetEmailConfirmationFormattersHandler : IHandleQueries<GetEmailConfirmationFormattersQuery, IDictionary<string, string>>
    {
        private readonly IManageConfigurations _configurationManager;

        public GetEmailConfirmationFormattersHandler(IManageConfigurations configurationManager)
        {
            _configurationManager = configurationManager;
        }

        public IDictionary<string, string> Handle(GetEmailConfirmationFormattersQuery query)
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
