using System;
using System.Linq;
using UCosmic.Domain.Establishments;
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

                #region Create Password Confirmation

                var createPasswordName = EmailTemplateName.CreatePasswordConfirmation.AsSentenceFragment();
                var createPasswordTemplate = Context.EmailTemplates.SingleOrDefault(e => e.Establishment == null
                    && e.Name.Equals(createPasswordName, StringComparison.OrdinalIgnoreCase));
                if (createPasswordTemplate == null)
                {
                    createPasswordTemplate = new EmailTemplate
                    {
                        Name = createPasswordName,
                    };
                    Context.EmailTemplates.Add(createPasswordTemplate);
                }
                createPasswordTemplate.SubjectFormat = "Confirm your email address for UCosmic.com";
                createPasswordTemplate.Instructions =
@"This is a template for the email sent when a new user signs up for UCosmic.com. People will use this message to create a password and sign on for the first time.

There are four (4) placeholders that will be used to inject variables into the message body:
{EmailAddress} <- The email address for which ownership must be confirmed.
{ConfirmationCode} <- The code that can be entered to validate ownership.
{ConfirmationUrl} <- The URL that can be visited to validate ownership.
{SendFromUrl} <- The URL where a new confirmation can be generated after this one expires.

Type the variables between the curly braces {LikeThis} in the template below.
When a new email is generated from this template, the values will be replaced as long as they appear exactly as above.";

                createPasswordTemplate.BodyFormat =
@"You have requested access to UCosmic.com using the email address '{EmailAddress}'.To confirm your ownership of this email address, please do one of the following:

If UCosmic.com is still open in your browser window, go to it and enter the following Confirmation Code:
{ConfirmationCode}
^ copy the code above ^

If you have closed your browser, click the link below or copy and paste the URL into your browser:
{ConfirmationUrl}
^ click or copy the URL above ^

The code and URL above will expire in 2 hours. If you to not confirm by then, you must return to {SendFromUrl} to generate a new confirmation email.

If you did NOT request access to UCosmic.com using the above email address, please reply to this email to tell us.

Enjoy your UCosmic voyage!";

                Context.SaveChanges();

                #endregion
                #region Reset Password Confirmation

                var resetPasswordName = EmailTemplateName.ResetPasswordConfirmation.AsSentenceFragment();
                var resetPasswordTemplate = Context.EmailTemplates.SingleOrDefault(e => e.Establishment == null
                    && e.Name.Equals(resetPasswordName, StringComparison.OrdinalIgnoreCase));
                if (resetPasswordTemplate == null)
                {
                    resetPasswordTemplate = new EmailTemplate
                    {
                        Name = resetPasswordName,
                    };
                    Context.EmailTemplates.Add(resetPasswordTemplate);
                }
                resetPasswordTemplate.SubjectFormat = "Password reset instructions for UCosmic.com";
                resetPasswordTemplate.Instructions =
@"This is a template for the email sent when a user requests a UCosmic.com password reset. People will use this message to override a forgotten password.

There are four (4) placeholders that will be used to inject variables into the message body:
{EmailAddress} <- The email address for which ownership must be confirmed.
{ConfirmationCode} <- The code that can be entered to validate ownership.
{ConfirmationUrl} <- The URL that can be visited to validate ownership.
{SendFromUrl} <- The URL where a password reset request can be generated after this one expires.

Type the variables between the curly braces {LikeThis} in the template below.
When a new email is generated from this template, the values will be replaced as long as they appear exactly as above.";

                resetPasswordTemplate.BodyFormat =
@"You have requested to reset your UCosmic.com password using the email address '{EmailAddress}'. To confirm your ownership of this email address, please do one of the following:

If UCosmic.com is still open in your browser window, go to it and enter the following Confirmation Code:
{ConfirmationCode}
^ copy the code above ^

If you have closed your browser, click the link below or copy and paste the URL into your browser:
{ConfirmationUrl}
^ click or copy the URL above ^

The code and URL above will expire in 2 hours. If you to not confirm and reset your password by then, you must return to {SendFromUrl} to generate a new password reset request.

If you did NOT initiate a UCosmic.com password reset using the above email address, please reply to this email to tell us.

Thank you,

The UCosmic Team";

                Context.SaveChanges();

                #endregion
            }
        }
    }
}