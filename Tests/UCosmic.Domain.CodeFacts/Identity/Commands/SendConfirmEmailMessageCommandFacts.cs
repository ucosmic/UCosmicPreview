using System;
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
                    Intent = EmailConfirmationIntent.PasswordReset,
                };
                command.TemplateName.ShouldEqual(EmailTemplateName.PasswordResetConfirmation);
            }

            [TestMethod]
            public void ReturnsSignUp_WhenIntentMatches()
            {
                var command = new SendConfirmEmailMessageCommand
                {
                    Intent = EmailConfirmationIntent.SignUp,
                };
                command.TemplateName.ShouldEqual(EmailTemplateName.SignUpConfirmation);
            }

            [TestMethod]
            public void ThrowsNotSupportedException_ForUnexpectedIntent()
            {
                NotSupportedException exception = null;
                var command = new SendConfirmEmailMessageCommand
                {
                    Intent = "something unexpected",
                };

                try
                {
                    command.TemplateName.ShouldBeNull();
                }
                catch (NotSupportedException ex)
                {
                    exception = ex;
                }

                exception.ShouldNotBeNull();
                // ReSharper disable PossibleNullReferenceException
                exception.Message.ShouldContain(command.Intent);
                // ReSharper restore PossibleNullReferenceException
            }
        }
    }
}
