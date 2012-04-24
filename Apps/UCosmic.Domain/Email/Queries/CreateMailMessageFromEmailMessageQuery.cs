using System;
using System.Net.Mail;
using UCosmic.Domain.People;

namespace UCosmic.Domain.Email
{
    public class CreateMailMessageFromEmailMessageQuery : IDefineQuery<MailMessage>
    {
        public CreateMailMessageFromEmailMessageQuery(EmailMessage emailMessage)
        {
            if (emailMessage == null) throw new ArgumentNullException("emailMessage");
            EmailMessage = emailMessage;
        }

        public EmailMessage EmailMessage { get; private set; }
    }
}
