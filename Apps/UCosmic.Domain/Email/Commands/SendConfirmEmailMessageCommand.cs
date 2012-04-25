using System;
using UCosmic.Domain.People;

namespace UCosmic.Domain.Email
{
    public class SendConfirmEmailMessageCommand
    {
        public string EmailAddress { get; set; }
        public string Intent { get; set; }

        internal string EmailTemplateName
        {
            get
            {
                switch (Intent)
                {
                    case EmailConfirmationIntent.PasswordReset:
                        return Email.EmailTemplateName.PasswordResetConfirmation;

                    case EmailConfirmationIntent.SignUp:
                        return Email.EmailTemplateName.SignUpConfirmation;
                }
                throw new NotSupportedException(string.Format(
                    "Email confirmation intent '{0}' is not supported.",
                        Intent));
            }
        }
        public Guid ConfirmationToken { get; internal set; }
    }
}
