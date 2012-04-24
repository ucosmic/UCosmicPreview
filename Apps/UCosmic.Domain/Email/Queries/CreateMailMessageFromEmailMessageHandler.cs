using System;
using System.Net.Mail;

namespace UCosmic.Domain.Email
{
    public class CreateMailMessageFromEmailMessageHandler : IHandleQueries<CreateMailMessageFromEmailMessageQuery, MailMessage>
    {
        private readonly IManageConfigurations _configurationManager;

        public CreateMailMessageFromEmailMessageHandler(IManageConfigurations configurationManager)
        {
            _configurationManager = configurationManager;
        }

        public MailMessage Handle(CreateMailMessageFromEmailMessageQuery query)
        {
            if (query == null) throw new ArgumentNullException("query");

            // initialize message
            var email = query.EmailMessage;
            var mail = new MailMessage
            {
                From = new MailAddress(email.FromAddress, email.FromDisplayName),
                Subject = email.Subject,
                Body = email.Body,
            };

            // intercept recipient during development
            if (_configurationManager.IsDeployedToCloud)
                mail.To.Add(new MailAddress(email.ToAddress, email.ToPerson.DisplayName));

            else
                foreach (var interceptAddress in _configurationManager.EmailInterceptAddresses.Explode(";"))
                    mail.To.Add(new MailAddress(interceptAddress, string.Format(
                        "Intended for {0} (UCosmic Mail Intercept)", email.ToAddress)));

            // reply-to address
            if (!string.IsNullOrWhiteSpace(email.ReplyToAddress))
                mail.ReplyToList.Add(new MailAddress(email.ReplyToAddress, email.FromAddress));

            return mail;
        }
    }
}
