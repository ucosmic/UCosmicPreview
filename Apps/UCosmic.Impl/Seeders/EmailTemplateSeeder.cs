using System;
using System.Linq;
using UCosmic.Domain.Email;
using UCosmic.Impl.Orm;

namespace UCosmic.Impl.Seeders
{
    public class EmailTemplateSeeder : ISeedDb
    {
        public void Seed(UCosmicContext context)
        {
            new EmailTemplatePreview4Seeder().Seed(context);
        }

        private class EmailTemplatePreview4Seeder : UCosmicDbSeeder
        {
            public override void Seed(UCosmicContext context)
            {
                Context = context;

                #region Sign Up Email Confirmation

                //var signUpEmailConfirmation = Context.EmailTemplates.ByName(EmailTemplateName.SignUpConfirmation);
                var signUpEmailConfirmation = Context.EmailTemplates.SingleOrDefault(e => e.Establishment == null
                    && e.Name.Equals(EmailTemplateName.SignUpConfirmation, StringComparison.OrdinalIgnoreCase));
                if (signUpEmailConfirmation == null)
                {
                    signUpEmailConfirmation = new EmailTemplate
                    {
                        Name = EmailTemplateName.SignUpConfirmation,
                    };
                    Context.EmailTemplates.Add(signUpEmailConfirmation);
                }
                signUpEmailConfirmation.SubjectFormat = "Confirm your email address for UCosmic.com";
                signUpEmailConfirmation.Instructions =
                    "This is a template for the email sent when a new user signs up for ucosmic.com. \r\n\r\n" +
                    "There are four (4) placeholders that will be used to inject variables into the message body: \r\n" +
                    "{EmailAddress} <- The email address for which ownership must be confirmed. \r\n" +
                    "{ConfirmationCode} <- The code that can be entered to validate ownership. \r\n" +
                    "{ConfirmationUrl} <- The URL that can be visited to validate ownership. \r\n" +
                    "{StartUrl} <- The URL where a new confirmation can be generated after this one expires. \r\n\r\n" +
                    "Type the variables between the curly braces {LikeThis} in the template below. \r\n" +
                    "When a new email is generated from this template, the values will be replaced as long as they appear exactly as above.";
                signUpEmailConfirmation.BodyFormat =
                    "You have requested access to UCosmic.com using the email address '{EmailAddress}'. " +
                    "To confirm your ownership of this email address, please do one of the following: \r\n\r\n" +
                    "If ucosmic.com is still open in your browser window, go to it and enter the following Confirmation Code: \r\n" +
                    "{ConfirmationCode}\r\n^ copy the code above ^ \r\n\r\n" +
                    "If you have closed your browser, click the link below or copy and paste the URL into your browser: \r\n" +
                    "{ConfirmationUrl}\r\n^ click or copy the URL above ^ \r\n\r\n" +
                    "The code and URL above will expire in 2 hours. If you to not confirm by then, you must return to " +
                    "{StartUrl} to generate a new confirmation email.\r\n\r\n" +
                    "If you did NOT request access to UCosmic.com using the above email address, please reply to this email to tell us.\r\n\r\n" +
                    "Enjoy your UCosmic voyage! \r\n\r\n";
                Context.SaveChanges();

                #endregion
                #region Password Reset Confirmation

                //var passwordResetConfirmation = Context.EmailTemplates.ByName(EmailTemplateName.PasswordResetConfirmation);
                var passwordResetConfirmation = Context.EmailTemplates.SingleOrDefault(e => e.Establishment == null
                    && e.Name.Equals(EmailTemplateName.PasswordResetConfirmation, StringComparison.OrdinalIgnoreCase));
                if (passwordResetConfirmation == null)
                {
                    passwordResetConfirmation = new EmailTemplate
                    {
                        Name = EmailTemplateName.PasswordResetConfirmation,
                    };
                    Context.EmailTemplates.Add(passwordResetConfirmation);
                }
                passwordResetConfirmation.SubjectFormat = "Password reset instructions for UCosmic.com";
                passwordResetConfirmation.Instructions =
                    "This is a template for the email sent when a user requests a ucosmic.com password reset. \r\n\r\n" +
                    "There are four (4) placeholders that will be used to inject variables into the message body: \r\n" +
                    "{EmailAddress} <- The email address for which ownership must be confirmed. \r\n" +
                    "{ConfirmationCode} <- The code that can be entered to validate ownership. \r\n" +
                    "{ConfirmationUrl} <- The URL that can be visited to validate ownership. \r\n" +
                    "{PasswordResetUrl} <- The URL where a password reset request can be generated after this one expires. \r\n\r\n" +
                    "Type the variables between the curly braces {LikeThis} in the template below. \r\n" +
                    "When a new email is generated from this template, the values will be replaced as long as they appear exactly as above.";
                passwordResetConfirmation.BodyFormat =
                    "You have requested to reset your UCosmic.com password using the email address '{EmailAddress}'. " +
                    "To confirm your ownership of this email address, please do one of the following: \r\n\r\n" +
                    "If ucosmic.com is still open in your browser window, go to it and enter the following Confirmation Code: \r\n" +
                    "{ConfirmationCode}\r\n^ copy the code above ^ \r\n\r\n" +
                    "If you have closed your browser, click the link below or copy and paste the URL into your browser: \r\n" +
                    "{ConfirmationUrl}\r\n^ click or copy the URL above ^ \r\n\r\n" +
                    "The code and URL above will expire in 2 hours. If you to not confirm and reset your password by then, you must return to " +
                    "{PasswordResetUrl} to generate a new password reset request.\r\n\r\n" +
                    "If you did NOT initiate a UCosmic.com password reset using the above email address, please reply to this email to tell us.\r\n\r\n" +
                    "Thank you, \r\n\r\nThe UCosmic Team";
                Context.SaveChanges();

                #endregion
            }
        }
    }
}