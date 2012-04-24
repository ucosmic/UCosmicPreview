using System;
using UCosmic.Domain.People;

namespace UCosmic.Domain.Email
{
    public class ComposeEmailMessageHandler : IHandleQueries<ComposeEmailMessageQuery, EmailMessage>
    {
        private readonly IManageConfigurations _configurationManager;

        public ComposeEmailMessageHandler(IManageConfigurations configurationManager)
        {
            _configurationManager = configurationManager;
        }

        public EmailMessage Handle(ComposeEmailMessageQuery query)
        {
            if (query == null) throw new ArgumentNullException("query");

            var message = new EmailMessage
            {
                // subject & body
                Subject = query.Template.SubjectFormat.FormatTemplate(query.Formatters),
                Body = query.Template.BodyFormat.FormatTemplate(query.Formatters),

                // from address (has failsafe from address)
                FromAddress = query.Template.FromAddress ??
                    _configurationManager.EmailDefaultFromAddress ?? "no-reply@ucosmic.com",
                FromDisplayName = query.Template.FromDisplayName ??
                    _configurationManager.EmailDefaultFromDisplayName,

                // reply-to address
                ReplyToAddress = query.Template.ReplyToAddress ??
                    _configurationManager.EmailDefaultReplyToAddress,
                ReplyToDisplayName = query.Template.ReplyToDisplayName ??
                    _configurationManager.EmailDefaultReplyToDisplayName,

                // to address
                ToAddress = query.ToEmailAddress.Value,
                ToPerson = query.ToEmailAddress.Person,
                Number = query.ToEmailAddress.Person.Messages.NextNumber(),

                FromEmailTemplate = query.Template.Name,
                ComposedOnUtc = DateTime.UtcNow,
            };

            return message;
        }
    }
}
