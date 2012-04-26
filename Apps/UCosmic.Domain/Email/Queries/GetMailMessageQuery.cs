using System;
using System.Net.Mail;
using UCosmic.Domain.People;

namespace UCosmic.Domain.Email
{
    public class GetMailMessageQuery : IDefineQuery<MailMessage>
    {
        public GetMailMessageQuery(EmailMessage emailMessage)
        {
            if (emailMessage == null) throw new ArgumentNullException("emailMessage");
            EmailMessage = emailMessage;
        }

        public EmailMessage EmailMessage { get; private set; }
    }
}
