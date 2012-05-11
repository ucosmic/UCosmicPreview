using Microsoft.VisualStudio.TestTools.UnitTesting;
using Should;
using UCosmic.Domain.Establishments;
using UCosmic.Domain.People;

namespace UCosmic.Domain.Identity
{
    // ReSharper disable UnusedMember.Global
    public class SendConfirmEmailMessageCommandFacts
    // ReSharper restore UnusedMember.Global
    {
        [TestClass]
        public class TheEmailTemplateNameProperty
        {
            [TestMethod]
            public void ReturnsResetPassword_WhenIntentMatches()
            {
                var command = new SendConfirmEmailMessageCommand
                {
                    Intent = EmailConfirmationIntent.ResetPassword,
                };
                command.TemplateName.ShouldEqual(EmailTemplateName.ResetPasswordConfirmation);
            }

            [TestMethod]
            public void ReturnsCreatePassword_WhenIntentMatches()
            {
                var command = new SendConfirmEmailMessageCommand
                {
                    Intent = EmailConfirmationIntent.CreatePassword,
                };
                command.TemplateName.ShouldEqual(EmailTemplateName.CreatePasswordConfirmation);
            }
        }
    }
}
