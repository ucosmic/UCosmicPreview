using System;
using System.Collections.Generic;
using UCosmic.Domain.People;

namespace UCosmic.Domain.Email
{
    public class ComposeEmailMessageQuery : IDefineQuery<EmailMessage>
    {
        public ComposeEmailMessageQuery(EmailTemplate template, EmailAddress toEmailAddress)
        {
            if (template == null) throw new ArgumentNullException("template");
            if (toEmailAddress == null) throw new ArgumentNullException("toEmailAddress");
            Template = template;
            ToEmailAddress = toEmailAddress;
        }

        public EmailTemplate Template { get; private set; }
        public EmailAddress ToEmailAddress { get; private set; }
        public IDictionary<string, string> Formatters { get; set; }
    }
}
