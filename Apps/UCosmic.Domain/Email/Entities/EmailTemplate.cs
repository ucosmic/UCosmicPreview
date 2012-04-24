using System;
using System.Collections.Generic;
using UCosmic.Domain.Establishments;
using UCosmic.Domain.People;

namespace UCosmic.Domain.Email
{
    public class EmailTemplate : RevisableEntity
    {
        public int? EstablishmentId { get; set; }
        public virtual Establishment Establishment { get; set; }

        public string Name { get; set; }

        public string Instructions { get; set; }

        public string SubjectFormat { get; set; }

        public string FromAddress { get; set; }

        public string FromDisplayName { get; set; }

        public string ReplyToAddress { get; set; }

        public string ReplyToDisplayName { get; set; }

        public string BodyFormat { get; set; }

        public EmailMessage ComposeMessageTo(EmailAddress to, IDictionary<string, string> variables, IManageConfigurations config)
        {
            var message = new EmailMessage
            {
                ToPerson = to.Person,
                Number = to.Person.Messages.NextNumber(),

                // subject & body
                Subject = SubjectFormat.FormatTemplate(variables),
                Body = BodyFormat.FormatTemplate(variables),

                // from address (has failsafe from address)
                FromAddress = FromAddress ??
                    config.EmailDefaultFromAddress ?? "no-reply@ucosmic.com",
                FromDisplayName = FromDisplayName ??
                    config.EmailDefaultFromDisplayName,

                // reply-to address
                ReplyToAddress = ReplyToAddress ??
                    config.EmailDefaultReplyToAddress,

                ReplyToDisplayName = ReplyToDisplayName ??
                    config.EmailDefaultReplyToDisplayName,

                FromEmailTemplate = Name,
                ToAddress = to.Value,
                ComposedOnUtc = DateTime.UtcNow,
            };

            return message;
        }
    }

    public static class EmailTemplateName
    {
        public const string SignUpConfirmation = "Sign Up Email Confirmation";
        public const string PasswordResetConfirmation = "Password Reset Email Confirmation";
    }

}