using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using UCosmic.Domain.Establishments;
using UCosmic.Domain.People;

namespace UCosmic.Domain.Email
{
    public class EmailTemplate : RevisableEntity
    {
        public int? EstablishmentId { get; set; }
        public virtual Establishment Establishment { get; set; }

        [Required]
        [StringLength(150)]
        public string Name { get; set; }

        public string Instructions { get; set; }

        [Required]
        [StringLength(250)]
        public string SubjectFormat { get; set; }

        [StringLength(256)]
        public string FromAddress { get; set; }

        [StringLength(150)]
        public string FromDisplayName { get; set; }

        [StringLength(256)]
        public string ReplyToAddress { get; set; }

        [StringLength(150)]
        public string ReplyToDisplayName { get; set; }

        [Required]
        public string BodyFormat { get; set; }

        public EmailMessage ComposeMessageTo(EmailAddress to, IDictionary<string, string> variables, IManageConfigurations config)
        {
            var message = new EmailMessage
            {
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

                FromEmailTemplate = this,
                To = to,
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

    public static class EmailTemplateExtensions
    {
        public static EmailTemplate ByName(this IEnumerable<EmailTemplate> query, string name)
        {
            return (query != null)
                ? query.SingleOrDefault(e => e.Establishment == null
                    && e.Name.Equals(name, StringComparison.OrdinalIgnoreCase))
                : null;
        }
    }

}