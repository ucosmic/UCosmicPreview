using System;
using System.Collections.Generic;

namespace UCosmic.Domain.People
{
    public class GetConfirmEmailFormattersQuery : IDefineQuery<IDictionary<string, string>>
    {
        public GetConfirmEmailFormattersQuery(EmailConfirmation confirmation, string sendFromUrl)
        {
            if (confirmation == null) throw new ArgumentNullException("confirmation");
            if (sendFromUrl == null) throw new ArgumentException("Cannot be null or white space.", "sendFromUrl");
            Confirmation = confirmation;
            SendFromUrl = sendFromUrl;
        }

        public EmailConfirmation Confirmation { get; private set; }
        public string SendFromUrl { get; private set; }
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
                { "{ConfirmationUrl}", string.Format(_configurationManager.ConfirmEmailUrlFormat,
                    query.Confirmation.Token,
                    query.Confirmation.SecretCode.UrlEncoded())
                },
                { "{SendFromUrl}", string.Format("https://{0}{1}", _configurationManager.DeployedTo, query.SendFromUrl) }
            };

            return formatters;
        }
    }
}
